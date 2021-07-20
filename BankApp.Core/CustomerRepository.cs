using BankApp.Data;
using BankApp.Model;
using Commons;
using System;

namespace BankApp.Core
{
    public class CustomerRepository
    {
        /// <summary>
        /// Create new customer account
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string CreateAccount(Account model, int id)
        {
            var message = string.Empty;
            var previousAccountCount = DataStore.AccountTable.Count;
            if (model != null)
            {
                foreach (var customer in DataStore.CustomerTable)
                {
                    if (customer.Id == id)
                    {
                        model.CustomerId = id;
                        customer.Account.Add(
                          new Account()
                          {
                              AccountName = customer.FullName,
                              AccountNumber = model.AccountNumber,
                              AccountType = model.AccountType,
                              AccountBalance = model.AccountBalance
                          }
                        );
                    }
                }
                model.CustomerId = id;

                DataStore.AccountTable.Add(model);

                int updatedAccountCount = DataStore.AccountTable.Count;

                if (updatedAccountCount > previousAccountCount)
                    message = "Account created successfuly";
            }
            else
            {
                message = "please all fields are required";
            }

            return message;
        }

        /// <summary>
        /// Add money to customer account using specified Id
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public string MakeDeposit(double amount, int accountId)
        {
            var message = string.Empty;
            var previousTransactionCount = DataStore.TransactionHistoryTable.Count;
            var newTransaction = new TransactionHistory();

            

            foreach (var account in DataStore.AccountTable)
            {
                if (account.Id == accountId  && amount > 0)
                { 
                    newTransaction.Amount = amount;
                    newTransaction.Sender = account.AccountName;
                    newTransaction.TransactionType = Utils.TransactionType.Credit.ToString();
                    newTransaction.TransactionDate = DateTime.Now;
                    account.AccountBalance += amount;
                    newTransaction.Balance = account.AccountBalance;
                }
            }
            DataStore.TransactionHistoryTable.Add(newTransaction);

            int updatedTransactionCount = DataStore.AccountTable.Count;

            if (updatedTransactionCount > previousTransactionCount)
                message += "Transaction Succesful";

            else
            {
                message += "Transaction Failed";
            }

            return message;
        }

        /// <summary>
        /// Sends specified amount to another account
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="accountId"></param>
        /// <param name="receiverAccountNumber"></param>
        /// <param name="receiverAccountName"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public string SendMoney(double amount, int accountId, string receiverAccountNumber, string receiverAccountName, string description)
        {
            var message = string.Empty;
            var previousTransactionCount = DataStore.TransactionHistoryTable.Count;
            var newTransaction = new TransactionHistory();

            foreach (var account in DataStore.AccountTable)
            {
                if (account.Id == accountId)
                {
                    if (account.AccountType == Utils.AccountType.Current.ToString()) //Current account holder can empty their account
                    {
                        newTransaction.Amount = amount;
                        newTransaction.Sender = account.AccountName;
                        newTransaction.ReceiverAccountName = receiverAccountName;
                        newTransaction.ReceiverAccountNumber = receiverAccountNumber;
                        newTransaction.Description = description;
                        newTransaction.TransactionType = Utils.TransactionType.Debit.ToString();
                        account.AccountBalance -= amount;
                        newTransaction.Balance = account.AccountBalance;
                    }
                    //Savings account holders must maintain a minimum balance of N1000
                    else if (account.AccountType == Utils.AccountType.Savings.ToString() && account.AccountBalance > amount + 1000)
                    {
                        newTransaction.Amount = amount;
                        newTransaction.Sender = account.AccountName;
                        newTransaction.ReceiverAccountName = receiverAccountName;
                        newTransaction.ReceiverAccountNumber = receiverAccountNumber;
                        newTransaction.Description = description;
                        newTransaction.TransactionType = Utils.TransactionType.Debit.ToString();
                        account.AccountBalance -= amount;
                        newTransaction.Balance = account.AccountBalance;
                    }
                    else
                        return "Insufficient Funds.";
                    
                }
            }
            DataStore.TransactionHistoryTable.Add(newTransaction);
            int updatedTransactionCount = DataStore.AccountTable.Count;

            if (updatedTransactionCount > previousTransactionCount)
                message += "Transaction Succesful";

            else
            {
                message += "Transaction Failed";
            }
            return message;
        }

        /// <summary>
        /// Withdraws the amount out of the specified customer account.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="accountId"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public string MakeWithdrawal(double amount, int accountId, string description)
        {
            var message = string.Empty;
            var previousTransactionCount = DataStore.TransactionHistoryTable.Count;
            var newTransaction = new TransactionHistory();

            foreach (var account in DataStore.AccountTable)
            {
                if (account.Id == accountId)
                {
                    if (account.AccountType == Utils.AccountType.Current.ToString())
                    {
                        newTransaction.Amount = amount;
                        newTransaction.Description = description;
                        newTransaction.TransactionType = Utils.TransactionType.Debit.ToString();
                        account.AccountBalance -= amount;
                        newTransaction.Balance = account.AccountBalance;
                    }
                    else if (account.AccountType == Utils.AccountType.Savings.ToString() && account.AccountBalance > amount + 1000)
                    {
                        newTransaction.Amount = amount;
                        newTransaction.Description = description;
                        newTransaction.TransactionType = Utils.TransactionType.Debit.ToString();
                        // implement pin for security
                        account.AccountBalance -= amount;
                        newTransaction.Balance = account.AccountBalance;
                    }
                    else
                        return "Insufficient Funds.";


                }
            }
            DataStore.TransactionHistoryTable.Add(newTransaction);
            int updatedTransactionCount = DataStore.AccountTable.Count;

            if (updatedTransactionCount > previousTransactionCount)
                message += "Transaction Succesful";

            else
            {
                message += "Transaction Failed";
            }
            return message;

        }

        /// <summary>
        /// Get Customer account balance for specified account.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public string GetAccountBalance(int accountId)
        {
            var message = string.Empty;
            foreach (var account in DataStore.AccountTable)
            {
                if (!(account.Id == accountId))
                    message = "Account does not exist";
                else
                    message = $"Your Account Balance is N{account.AccountBalance.ToString()}";
            }

            return message;
        }

        /// <summary>
        /// Prints out customer account details and statements in a nicely formated table
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public string GetStatementOfAccount(int accountId)
        {
            var message = string.Empty;

            if (DataStore.TransactionHistoryTable.Count != 0)
            {
                Console.Clear();
                PrintTable.PrintLine();
                PrintTable.PrintRow("DATE", "DESCRIPTION", "AMOUNT", "BALANCE");
                PrintTable.PrintLine();

                foreach (var transaction in DataStore.TransactionHistoryTable)
                {
                    if (transaction.AccountId == accountId)
                    {
                        PrintTable.PrintRow(transaction.TransactionDate.LocalDateTime.Date.ToString(), transaction.Description, transaction.Amount.ToString(), transaction.Balance.ToString());
                        PrintTable.PrintLine();

                    }
                }
            }
            else
            {
                message += $"No transaction made yet";
            }

            return message;
        }
    }
}
