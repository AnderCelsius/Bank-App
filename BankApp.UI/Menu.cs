using BankApp.Model;
using BankApp.Core;
using System;
using System.Text.RegularExpressions;
using BankApp.Data;
using Commons;
using System.Collections.Generic;

namespace BankApp.UI
{
    class Menu
    {
        public void RunApp()
        {
            var newCustomer = new CustomerRepository();
            var newTransaction = new TransactionRepository();

            Console.WriteLine("WELCOME TO ONE BANK");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press enter to start or any other key to exit");
            Console.Write(">>>");
            string begin = Console.ReadLine();
            Console.Clear();

            while (string.IsNullOrWhiteSpace(begin))
            {
                Console.WriteLine("Press 1 to Register");
                Console.WriteLine("Press 2 to Log In");
                Console.WriteLine("Press 3 to Quit");
                Console.Write(">>>");

                var choice = Console.ReadLine();
                if (choice == "1")
                {
                    // User Registration
                    // Required Fields
                    var firstName = string.Empty;
                    var lastName = string.Empty;
                    var email = string.Empty;
                    var phoneNumber = string.Empty;
                    var password = string.Empty;

                    Console.Clear();


                    Console.WriteLine("Please fill in all fields");
                    while (true)
                    {
                        Console.Write("First Name: ");
                        var input = Console.ReadLine();
                        var response = Checker.ValidateName(input);
                        Console.Clear();

                        if (response != true) //Names must only contain strings
                        {
                            Console.WriteLine("Invalid input!");
                            Console.WriteLine("Name can only contain alphabeths");
                            Console.WriteLine("");
                            continue;
                        }
                        else
                        {
                            firstName += input;
                            break;
                        }
                    }

                    while (true)
                    {
                        Console.Write("Last Name: ");
                        var input = Console.ReadLine();
                        var response = Checker.ValidateName(input);
                        Console.Clear();

                        if (response != true) //Names must only contain strings
                        {
                            Console.WriteLine("Invalid input!");
                            Console.WriteLine("Name can only contain alphabeths");
                            Console.WriteLine("");
                            continue;
                        }
                        else
                        {
                            lastName += input;
                            break;
                        }
                    }

                    while (true)
                    {
                        Console.Write("Email: ");
                        var input = Console.ReadLine();
                        var response = Checker.ValidateEmail(input);
                        Console.Clear();

                        if (response != true)
                        {
                            Console.WriteLine("Invalid email address!");
                            Console.WriteLine("");
                            continue;
                        }
                        else
                        {
                            email += input;
                            break;
                        }

                    }

                    while (true)
                    {
                        Console.Write("Phone Number: ");
                        var input = Console.ReadLine();
                        var response = Checker.ValidatePhoneNumber(input);
                        Console.Clear();

                        if (response != true)
                        {
                            Console.WriteLine("Invalid phone number");
                            Console.WriteLine("");
                            continue;
                        }
                        else
                        {
                            phoneNumber += input;
                            break;
                        }
                    }

                    while (true)
                    {
                        Console.WriteLine($"Hint: Password should be minimum of 6 characters\n" +
                            $"      Should include alphanumeric and at least one special characters (@, #, $, %, ^, &, !)");
                        Console.WriteLine();

                        Console.Write("Password: ");
                        var input = Console.ReadLine();
                        var response = Checker.ValidatePassword(input);
                        Console.Clear();

                        if (response != true)
                        {
                            Console.WriteLine("Invalid password");
                            Console.WriteLine("");
                            continue;
                        }
                        else
                        {
                            Console.Write("Confirm Password: ");
                            var input2 = Console.ReadLine().Trim();
                            if (input2 == input)
                            {
                                password += input;
                                Console.Clear();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Password did not match. Please try again");
                                continue;
                            }
                        }
                    }

                    // Set customer properties
                    var registeration = CustomerRepository.RegisterCustomer(firstName, lastName, email, phoneNumber, password);
                    Console.WriteLine(registeration);


                }
                else if (choice == "2")
                {
                    Console.Clear();

                    //User log in details
                    var email = string.Empty;
                    var password = string.Empty;

                    while (true)
                    {
                        Console.Write("Enter Email: ");
                        var input = Console.ReadLine();
                        var response = Checker.ValidateEmail(input);
                        Console.Clear();

                        if (response != true)
                        {
                            Console.WriteLine("Invalid input");
                            Console.WriteLine("");
                            continue;
                        }
                        else
                        {
                            email += input;
                            break;

                        }
                    }

                    while (true)
                    {
                        Console.Write("Enter Password: ");
                        var input = Console.ReadLine();
                        var response = Checker.ValidatePassword(input);
                        Console.Clear();

                        if (response != true)
                        {
                            Console.WriteLine("Invalid password");
                            Console.WriteLine("");
                            continue;
                        }
                        else
                        {
                            password += input;
                            Console.Clear();
                            break;
                        }
                    }
                    var login = AuthenticationRepository.Login(email, password);

                    if (login == true)
                    {
                        foreach (var customer in DataStore.CustomerTable)
                        {
                            if(customer.Email == email)
                            {
                                Console.Clear();
                                Console.WriteLine("Login Succesful");
                                while (true)
                                {
                                    Console.WriteLine($"Welcome {customer.FullName}");
                                    Console.WriteLine();
                                    Console.WriteLine("Press 1 to Create Account");
                                    Console.WriteLine("Press 2 to Make Deposit");
                                    Console.WriteLine("Press 3 to Make Withdrawal");
                                    Console.WriteLine("Press 4 to Send Money");
                                    Console.WriteLine("Press 5 to Check Balance");
                                    Console.WriteLine("Press 6 to Get Account Information");
                                    Console.WriteLine("Press 7 to Generate Statement of Account");
                                    Console.WriteLine("Press 8 to Log Out");

                                    var input = Console.ReadLine();

                                    if (input == "1")
                                    {
                                        Console.Clear();
                                        var accountType = MenuOptions.CreateAccountUI();
                                        // Create new account instance for the customer
                                        var account = new Account
                                        {
                                            AccountType = accountType
                                        };

                                        var message = newCustomer.CreateAccount(account, customer);
                                        Console.Clear();
                                        Console.WriteLine(message);
                                    }

                                    else if (input == "2")
                                    {
                                        Console.Clear(); 
                                        double amount = 0;

                                        while (true)
                                        {
                                            if (customer.Account.Count > 0)
                                            {
                                                int count = 1;
                                                string acntNum = "";
                                                var accNumMap = new Dictionary<string, string>();
                                                Console.WriteLine("Select Account to carry out transaction");
                                                foreach (var account in customer.Account)
                                                {
                                                    Console.WriteLine($"{count}. {account.AccountName} {account.AccountNumber} {account.AccountType}");
                                                    accNumMap.Add(count.ToString(), account.AccountNumber);
                                                    count++;
                                                }
                                                var accId = Console.ReadLine();

                                                if (accNumMap.TryGetValue(accId, out acntNum))
                                                {
                                                    Console.Clear();
                                                   

                                                    while (true)
                                                    {
                                                        Console.Write($"Enter amount to deposit: ");
                                                        var amountChoice = Console.ReadLine();

                                                        bool success = double.TryParse(amountChoice, out amount);
                                                        if (success)
                                                            break;
                                                        else
                                                        {
                                                            Console.WriteLine("Invalid transaction! Please enter a number");
                                                            continue;
                                                        }
                                                    }
                                                    var withdrawChannel = MenuOptions.TrasactionChannel();

                                                    var deposit = newTransaction.MakeDeposit(amount, acntNum, withdrawChannel);
                                                    Console.Clear();
                                                    Console.WriteLine(deposit);
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Sorry, you have chosen an invalid option");
                                                    break;
                                                }
    
                                            }
                                            else
                                            {
                                                Console.WriteLine("You do not have an account yet. Do you wish to create one?");
                                                Console.WriteLine("1. Create Account");
                                                Console.WriteLine("2. Back");
                                                var userInput = Console.ReadLine();
                                                if (userInput == "1")
                                                {
                                                    Console.Clear();
                                                    var accountType = MenuOptions.CreateAccountUI();
                                                    // Create new account instance for the customer
                                                    var account = new Account
                                                    {
                                                        AccountType = accountType
                                                    };

                                                    var message = newCustomer.CreateAccount(account, customer);
                                                    Console.Clear();
                                                    Console.WriteLine(message);
                                                }
                                                else if (userInput == "2")
                                                    break;
                                                else
                                                {
                                                    Console.WriteLine("Invalid input");
                                                    continue;
                                                }
                                            }

                                        }
                                    }
                                    else if (input == "3")
                                    {
                                        while (true)
                                        {
                                            Console.Clear();
                                            if(customer.Account.Count > 0)
                                            {
                                                int count = 1;
                                                Console.WriteLine("Select Account to carry out transaction");
                                                foreach (var account in customer.Account)
                                                {
                                                    Console.WriteLine($"{count}. {account.AccountName} {account.AccountNumber} {account.AccountType}");
                                                    count++;
                                                }
                                                var accId = Console.ReadLine();
                                                bool success = int.TryParse(accId, out int accountId);
                                                // variables needed for withdrawal
                                                double amount;
                                                if (!success)
                                                {
                                                    Console.WriteLine("Customer Id must be a number");
                                                    continue;
                                                }

                                                else
                                                {
                                                    Console.Write($"Enter amount to withdraw: ");
                                                    var amountChoice = Console.ReadLine();

                                                    while (true)
                                                    {
                                                        bool isSuccessful = double.TryParse(amountChoice, out amount);
                                                        if (isSuccessful)
                                                            break;
                                                        else
                                                        {
                                                            Console.WriteLine("Invalid transaction! Please enter a number");
                                                            continue;
                                                        }

                                                    }
                                                }

                                                var transactionChannel = MenuOptions.TrasactionChannel();

                                                string withdraw = newTransaction.MakeWithdrawal(amount, accountId, transactionChannel);
                                                Console.Clear();
                                                Console.WriteLine(withdraw);
                                                break;
                                            }
                                            
                                            else
                                            {
                                                Console.WriteLine("You do not have an account yet. Do you wish to create one?");
                                                Console.WriteLine("1. Create Account");
                                                Console.WriteLine("2. Back");
                                                var userInput = Console.ReadLine();
                                                if (userInput == "1")
                                                {
                                                    Console.Clear();
                                                    var accountType = MenuOptions.CreateAccountUI();
                                                    // Create new account instance for the customer
                                                    var account = new Account
                                                    {
                                                        AccountType = accountType
                                                    };

                                                    var message = newCustomer.CreateAccount(account, customer);
                                                    Console.Clear();
                                                    Console.WriteLine(message);
                                                }
                                                else if (userInput == "2")
                                                    break;
                                                else
                                                {
                                                    Console.WriteLine("Invalid input");
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                    else if (input == "4")
                                    {
                                        // variables needed for transfer
                                        int accountId;
                                        int otherAccountId;
                                        string description = null;
                                        string otherDescription = null;
                                        string receiverAccNumber = string.Empty;
                                        string senderAccNumber = string.Empty;
                                        while (true)
                                        {
                                            if(customer.Account.Count > 0)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Select Account to carry out transaction");
                                                var count = 1;
                                                foreach (var account in customer.Account)
                                                {
                                                    Console.WriteLine($"{count}. {account.AccountName} {account.AccountNumber} {account.AccountType}");
                                                    count++;
                                                    senderAccNumber = account.AccountNumber;
                                                }
                                                var accId = Console.ReadLine();
                                                bool success = int.TryParse(accId, out accountId);
                                                if (!success)
                                                {
                                                    Console.WriteLine("Customer Id must be a number");
                                                    continue;
                                                }

                                                while (true)
                                                {
                                                    Console.WriteLine($"Choose Transfer channel: ");
                                                    int cou = 1;

                                                    foreach (var transferChannel in Enum.GetNames(typeof(Utils.TransactionDescription)))
                                                    {
                                                        Console.WriteLine($"{cou}: {transferChannel}");
                                                        cou++;
                                                    }
                                                    var response = Console.ReadLine();
                                                    if (response == "1")
                                                    {
                                                        description += $"{Utils.TransactionDescription.POS} transfer";
                                                        otherDescription += $"{Utils.TransactionDescription.POS} transfer from {senderAccNumber}";
                                                        break;
                                                    }
                                                    else if (response == "2")
                                                    {
                                                        description += $"{Utils.TransactionDescription.ATM} transfer";
                                                        otherDescription += $"{Utils.TransactionDescription.ATM} transfer from {senderAccNumber}";
                                                        break;
                                                    }
                                                    else if (response == "3")
                                                    {
                                                        description += $"{Utils.TransactionDescription.USSD} transfer";
                                                        otherDescription += $"{Utils.TransactionDescription.POS} transfer from {senderAccNumber}";
                                                        break;
                                                    }
                                                    else if (response == "4")
                                                    {
                                                        description += $"{Utils.TransactionDescription.FIP} transfer";
                                                        otherDescription += $"{Utils.TransactionDescription.POS} transfer from {senderAccNumber}";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Invalid input. Please try again");
                                                        continue;
                                                    }
                                                }

                                                Console.WriteLine("");
                                                Console.WriteLine("******************************************");
                                                Console.WriteLine("Press 1 to transfer to your other accounts");
                                                Console.WriteLine("Press 2 to transfer to another customer");
                                                Console.WriteLine("Press 3 to cancel");
                                                var transferChoice = Console.ReadLine();

                                                double amount;
                                                if (transferChoice == "1")
                                                {
                                                    foreach (var account in customer.Account)
                                                    {
                                                        if (customer.Account.Count < 1)
                                                        {
                                                            Console.WriteLine("You do not have any other account.");
                                                            Console.WriteLine("Press 1 to create a new account");
                                                            Console.WriteLine("Press 2 to cancel operation");
                                                            break;
                                                        }
                                                        
                                                        else if (account.Id != accountId)
                                                        {
                                                            Console.WriteLine("Select account to transfer to:");
                                                            Console.WriteLine($"press {account.Id}. {account.AccountName} {account.AccountNumber} {account.AccountType}");
                                                            var accToTransferTo = Console.ReadLine();
                                                            bool isSuccessful = int.TryParse(accToTransferTo, out otherAccountId);
                                                            if (!isSuccessful)
                                                            {
                                                                Console.WriteLine("Customer Id must be a number");
                                                                continue;
                                                            }

                                                            while (true)
                                                            {
                                                                Console.Write($"Enter amount to Transfer: ");
                                                                var amountChoice = Console.ReadLine();
                                                                bool response = double.TryParse(amountChoice, out amount);
                                                                if (response)
                                                                    break;
                                                                else
                                                                {
                                                                    Console.WriteLine("Invalid transaction! Please enter a number");
                                                                    continue;
                                                                }

                                                            }

                                                            Console.WriteLine(newTransaction.TransferToOtherAccount(amount, accountId, otherAccountId, description, otherDescription));
                                                            break;
                                                        }

                                                    }



                                                }
                                                else if (transferChoice == "2")
                                                {
                                                    Console.Write($"Enter amount to Transfer: ");
                                                    var amountChoice = Console.ReadLine();

                                                    while (true)
                                                    {
                                                        bool isSuccessful = double.TryParse(amountChoice, out amount);
                                                        if (isSuccessful)
                                                            break;
                                                        else
                                                        {
                                                            Console.WriteLine("Invalid transaction! Please enter a number");
                                                            continue;
                                                        }

                                                    }

                                                    //SendMoney(double amount, int accountId, string receiverAccountNumber, string receiverAccountName, string description)

                                                    while (true)
                                                    {
                                                        Console.Write("Enter receiver account number:");
                                                        var inputedAccNum = Console.ReadLine();
                                                        var isValidAccNum = Checker.ValidateTransAccount(inputedAccNum);
                                                        Console.Clear();

                                                        if (isValidAccNum != true)
                                                        {
                                                            Console.WriteLine("Invalid account number. Please check and try again");
                                                            Console.WriteLine("");
                                                            continue;
                                                        }
                                                        else
                                                        {
                                                            receiverAccNumber += inputedAccNum;
                                                            Console.Clear();
                                                            break;
                                                        }
                                                    }

                                                    var transfer = newTransaction.SendMoney(amount, accountId, receiverAccNumber, description);
                                                    Console.Clear();
                                                    Console.WriteLine(transfer);
                                                    break;
                                                }
                                                else if (transferChoice == "3")
                                                    break;

                                                else
                                                {
                                                    Console.WriteLine("Invalid input! Please try again");
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("You do not have an account yet. Do you wish to create one?");
                                                Console.WriteLine("1. Create Account");
                                                Console.WriteLine("2. Back");
                                                var userInput = Console.ReadLine();
                                                if (userInput == "1")
                                                {
                                                    Console.Clear();
                                                    var accountType = MenuOptions.CreateAccountUI();
                                                    // Create new account instance for the customer
                                                    var account = new Account
                                                    {
                                                        AccountType = accountType
                                                    };

                                                    var message = newCustomer.CreateAccount(account, customer);
                                                    Console.Clear();
                                                    Console.WriteLine(message);
                                                }
                                                else if (userInput == "2")
                                                    break;
                                                else
                                                {
                                                    Console.WriteLine("Invalid input");
                                                    continue;
                                                }
                                            }



                                        }

                                    }
                                    else if (input == "5")
                                    {
                                        // variables needed for account balance infomation
                                        int accountId;
                                        while (true)
                                        {
                                            if(customer.Account.Count > 0)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Select Account to carry out transaction");
                                                int count = 1;
                                                foreach (var account in customer.Account)
                                                {
                                                    Console.WriteLine($"{count}. {account.AccountName} {account.AccountNumber} {account.AccountType}");
                                                    count++;
                                                }
                                                var accId = Console.ReadLine();
                                                bool success = int.TryParse(accId, out accountId);
                                                if (!success)
                                                {
                                                    Console.WriteLine("Customer Id must be a number");
                                                    continue;
                                                }
                                                Console.Clear();
                                                var balance = newTransaction.GetAccountBalance(accountId);
                                                Console.WriteLine(balance);
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("You do not have an account yet. Do you wish to create one?");
                                                Console.WriteLine("1. Create Account");
                                                Console.WriteLine("2. Back");
                                                var userInput = Console.ReadLine();
                                                if (userInput == "1")
                                                {
                                                    Console.Clear();
                                                    var accountType = MenuOptions.CreateAccountUI();
                                                    // Create new account instance for the customer
                                                    var account = new Account
                                                    {
                                                        AccountType = accountType
                                                    };

                                                    var message = newCustomer.CreateAccount(account, customer);
                                                    Console.Clear();
                                                    Console.WriteLine(message);
                                                }
                                                else if (userInput == "2")
                                                    break;
                                                else
                                                {
                                                    Console.WriteLine("Invalid input");
                                                    continue;
                                                }
                                            }
                                                
                                            
                                        }

                                    }
                                    else if (input == "6")
                                    {
                                        while (true)
                                        {
                                            Console.Clear();
                                            // variables needed for account statement
                                            if (customer.Account.Count < 1)
                                            {
                                                Console.WriteLine("You have not created any account yet. Please create an account first");
                                                break;
                                            }
                                            int count = 1;
                                            string acntNum = "";
                                            var accNumMap = new Dictionary<string, string>();
                                            Console.WriteLine("Select Account to carry out transaction");
                                            foreach (var account in customer.Account)
                                            {
                                                Console.WriteLine($"{count}. {account.AccountName} {account.AccountNumber} {account.AccountType}");
                                                accNumMap.Add(count.ToString(), account.AccountNumber);
                                                count++;
                                            }
                                            var accId = Console.ReadLine();

                                            if (accNumMap.TryGetValue(accId, out acntNum))
                                            {
                                                Console.Clear();
                                                Console.WriteLine(newTransaction.GetAccountDetails(acntNum));
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Sorry, you have chosen an invalid option");
                                                break;
                                            }

                                        }

                                    }
                                    else if (input == "7")
                                    {
                                        Console.Clear();
                                        // variables needed for account statement
                                        while (true)
                                        {
                                            if(customer.Account.Count < 1)
                                            {
                                                Console.WriteLine("You have not created any account yet. Please create an account first");
                                                break;
                                            }
                                            int count = 1;
                                            var accNumMap = new Dictionary<string, int>();
                                            Console.WriteLine("Select Account to carry out transaction");
                                            foreach (var account in customer.Account)
                                            {
                                                Console.WriteLine($"{count}. {account.AccountName} {account.AccountNumber} {account.AccountType}");
                                                accNumMap.Add(count.ToString(), account.Id);
                                                count++;
                                            }
                                            var accId = Console.ReadLine();

                                            if (accNumMap.TryGetValue(accId, out int acntNum))
                                            {
                                                Console.Clear();
                                                Console.WriteLine(newTransaction.GetStatementOfAccount(acntNum));
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Sorry, you have chosen an invalid option");
                                                break;
                                            }
                                            
                                        }
                                    }
                                    else if (input == "8")
                                    {
                                        var logout = AuthenticationRepository.Logout(email, password);
                                        if (logout == false)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Thank you for banking with us");
                                        }
     
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input!");
                                        continue;
                                    }
                                }
                            }
                            
                        } 
                        
                    }

                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Customer record not found!");
                        continue;
                    }

                }
                else if (choice == "3")
                    break;
                else
                {
                    Console.WriteLine("Invalid input. Please try again");
                    continue;
                }


            }


        }
    }
}
