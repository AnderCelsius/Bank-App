using BankApp.Data;
using BankApp.Model;
using Commons;
using System;

namespace BankApp.Core
{
    public class CustomerRepository
    {
        int accountCount = 1;
        public static string RegisterCustomer(Customer model, string password)
        {
            string message = string.Empty;
            var previousUserCount = DataStore.CustomerTable.Count;

            if (model != null)
            {
                model.Password = password;

                DataStore.CustomerTable.Add(model);

                var updatedUserCount = DataStore.CustomerTable.Count;
                if (updatedUserCount > previousUserCount) //Confirm that a customer record have been updated in customer table.
                    message += "Registration successful";
                else
                    message += "Registration failed";
            }

            return message;
        }
        /// <summary>
        /// Create new customer account
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string CreateAccount(Account model, Customer customer)
        {
            var message = string.Empty;
            var previousAccountCount = DataStore.AccountTable.Count;
            if (model != null)
            {
                model.Id = accountCount;
                model.CustomerId = customer.Id;
                model.AccountName = customer.FullName;
                customer.Account.Add(model);
                accountCount++;
            }
            DataStore.AccountTable.Add(model);

            int updatedAccountCount = DataStore.AccountTable.Count;

            if (updatedAccountCount > previousAccountCount)
                message += "Account created successfuly";

            else
            {
                message += "please all fields are required";
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
                if (account.Id == accountId && amount > 0)
                {
                    newTransaction.AccountId = accountId;
                    newTransaction.Amount = amount;
                    newTransaction.Sender = account.AccountName;
                    newTransaction.TransactionType = Utils.TransactionType.Credit.ToString();
                    newTransaction.TransactionDate = DateTime.Now.Date;
                    account.AccountBalance += amount;
                    newTransaction.Balance = account.AccountBalance;
                }
            }
            DataStore.TransactionHistoryTable.Add(newTransaction);

            int updatedTransactionCount = DataStore.TransactionHistoryTable.Count;

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
                    if (account.AccountType == Utils.AccountType.Current.ToString() && account.AccountBalance >= amount) //Current account holder can empty their account
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
            int updatedTransactionCount = DataStore.TransactionHistoryTable.Count;

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
                    else if (account.AccountType == Utils.AccountType.Savings.ToString() && account.AccountBalance > (amount + 1000))
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
            int updatedTransactionCount = DataStore.TransactionHistoryTable.Count;

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
        public string GetAccountDetails()
        {
            var message = string.Empty;

            if (DataStore.AccountTable.Count != 0)
            {
                Console.Clear();
                PrintTable.PrintLine();
                PrintTable.PrintRow("FULL NAME", "ACCOUNT NUMBER", "ACCOUNT TYPE", "ACCOUNT BALANCE");
                PrintTable.PrintLine();

                foreach (var account in DataStore.AccountTable)
                {
                    PrintTable.PrintRow(account.AccountName, account.AccountNumber, account.AccountType, account.AccountBalance.ToString());
                    PrintTable.PrintLine();
                }
            }
            else
            {
                message += $"No Account Created yet";
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
                PrintTable.PrintRow($"ACCOUNT STATEMENT ON ACCOUNT NO {DataStore.AccountTable[accountId].AccountNumber}");
                PrintTable.PrintRow();
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
