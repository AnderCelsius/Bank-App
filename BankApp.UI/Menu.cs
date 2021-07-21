using BankApp.Model;
using BankApp.Core;
using System;
using System.Text.RegularExpressions;
using BankApp.Data;
using Commons;

namespace BankApp.UI
{
    class Menu
    {
        public void RunApp()
        {
            var newCustomer = new CustomerRepository();
            var customer = new Customer();

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
                    customer.FirstName = firstName;
                    customer.LastName = lastName;
                    customer.Email = email;
                    customer.PhoneNumber = phoneNumber;

                    var registeration = CustomerRepository.RegisterCustomer(customer, password);
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
                                // variables required to create account
                                var accountType = string.Empty;
                                while (true)
                                {
                                    Console.WriteLine("Select Account Type");
                                    var count = 1;
                                    foreach (var accType in Enum.GetNames(typeof(Utils.AccountType)))
                                    {
                                        Console.WriteLine($"{count}: {accType}");
                                        count++;
                                    }
                                    var response = Console.ReadLine();
                                    if (response == "1")
                                    {
                                        accountType += Utils.AccountType.Savings;
                                        break;
                                    }
                                    else if (response == "2")
                                    {
                                        accountType += Utils.AccountType.Current;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input");
                                        continue;
                                    }
                                }
                                // Create new account instance for the customer
                                var account = new Account
                                {
                                    AccountType = accountType
                                };

                                var message = newCustomer.CreateAccount(account, customer);
                                Console.WriteLine(message);
                            }

                            else if (input == "2")
                            {
                                Console.Clear();
                                int accountId;
                                double amount = 0;

                                while (true)
                                {
                                    Console.WriteLine("Select Account to carry out transaction");
                                    foreach (var account in DataStore.AccountTable)
                                    {
                                        Console.WriteLine($"{account.Id}. {account.AccountName} {account.AccountNumber} {account.AccountType}");
                                    }
                                    var accId = Console.ReadLine();
                                    try
                                    {
                                        accountId = Convert.ToInt32(accId);
                                    }
                                    catch (Exception)
                                    {

                                        throw new FormatException("Customer Id must be a number");
                                    }

                                    Console.Clear();
                                    Console.Write($"Enter amount to deposit: ");
                                    var amountChoice = Console.ReadLine();

                                    while (true)
                                    {
                                        bool success = double.TryParse(amountChoice, out amount);
                                        if (success)
                                            break;
                                        else
                                        {
                                            Console.WriteLine("Invalid transaction! Please enter a number");
                                            continue;
                                        }
                                    }
                                    var deposit = newCustomer.MakeDeposit(amount, accountId);
                                    Console.WriteLine(deposit);
                                    break;
                                }
                            }
                            else if (input == "3")
                            {
                                // variables needed for withdrawal
                                int accountId;
                                double amount = 0;
                                string withdrawType = string.Empty;

                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Select Account to carry out transaction");
                                    foreach (var account in customer.Account)
                                    {
                                        Console.WriteLine($"{account.Id}. {account.AccountName} {account.AccountNumber} {account.AccountType}");
                                    }
                                    var accId = Console.ReadLine();
                                    bool success = int.TryParse(accId, out accountId);
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

                                    while (true)
                                    {
                                        Console.Write($"Choose withdrawal channel: ");
                                        int count = 1;

                                        foreach (var withdrawChannel in Enum.GetNames(typeof(Utils.TransactionDescription)))
                                        {
                                            Console.WriteLine($"{count}: {withdrawChannel}");
                                            count++;
                                        }
                                        var response = Console.ReadLine();
                                        if (response == "1")
                                        {
                                            withdrawType += Utils.TransactionDescription.POS;
                                            break;
                                        }
                                        else if (response == "2")
                                        {
                                            withdrawType += Utils.TransactionDescription.ATM;
                                            break;
                                        }
                                        else if (response == "3")
                                        {
                                            withdrawType += Utils.TransactionDescription.USSD;
                                            break;
                                        }
                                        else if (response == "4")
                                        {
                                            withdrawType += Utils.TransactionDescription.FIP;
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid input. Please try again");
                                            continue;
                                        }
                                    }

                                    var withdraw = newCustomer.MakeWithdrawal(amount, accountId, withdrawType);
                                    Console.WriteLine(withdraw);
                                    break;
                                }
                            }
                            else if (input == "4")
                            {
                                // variables needed for transfer
                                int accountId;
                                int otherAccountId;
                                double amount = 0;
                                string description = null;
                                string receiverAccNumber = string.Empty;
                                while (true)
                                {
                                    Console.WriteLine("Select Account to carry out transaction");
                                    foreach (var account in customer.Account)
                                    {
                                        Console.WriteLine($"{account.Id}. {account.AccountName} {account.AccountNumber} {account.AccountType}");
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
                                        int count = 1;
                                        string transferType = "";

                                        foreach (var transferChannel in Enum.GetNames(typeof(Utils.TransactionDescription)))
                                        {
                                            Console.WriteLine($"{count}: {transferChannel}");
                                            count++;
                                        }
                                        var response = Console.ReadLine();
                                        if (response == "1")
                                        {
                                            transferType += $"{Utils.TransactionDescription.POS} transfer";
                                            break;
                                        }
                                        else if (response == "2")
                                        {
                                            transferType += $"{Utils.TransactionDescription.ATM} transfer";
                                            break;
                                        }
                                        else if (response == "3")
                                        {
                                            transferType += $"{Utils.TransactionDescription.USSD} transfer";
                                            break;
                                        }
                                        else if (response == "4")
                                        {
                                            transferType += $"{Utils.TransactionDescription.FIP} transfer";
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
                                    var transferChoice = Console.ReadLine();

                                    if (transferChoice == "1")
                                    {
                                        foreach (var account in customer.Account)
                                        {
                                            if (customer.Account.Count < 2)
                                            {
                                                Console.WriteLine("You do not have any other account.");
                                                break;
                                            }
                                            Console.WriteLine("Select account to transfer to:");
                                            if (account.Id != accountId)
                                                Console.WriteLine($"press {account.Id}. {account.AccountName} {account.AccountNumber} {account.AccountType}");
                                        }
 
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

                                        Console.WriteLine(newCustomer.TransferToOtherAccount(amount, accountId, otherAccountId, description));
                                        break;
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

                                        var transfer = newCustomer.SendMoney(amount, accountId, receiverAccNumber, description);
                                        Console.WriteLine(transfer);
                                        break;
                                    }
                                
                                    else
                                    {
                                        Console.WriteLine("Invalid input! Please try again");
                                        continue;
                                    }


                                }  
                            }
                            else if (input == "5")
                            {
                                // variables needed for account balance infomation
                                int accountId;
                                while (true)
                                {
                                    Console.WriteLine("Select Account to carry out transaction");
                                    foreach (var account in customer.Account)
                                    {
                                        Console.WriteLine($"{account.Id}. {account.AccountName} {account.AccountNumber} {account.AccountType}");
                                    }
                                    var accId = Console.ReadLine();
                                    bool success = int.TryParse(accId, out accountId);
                                    if (!success)
                                    {
                                        Console.WriteLine("Customer Id must be a number");
                                        continue;
                                    }
                                    Console.Clear();
                                    var balance = newCustomer.GetAccountBalance(accountId);
                                    Console.WriteLine(balance);
                                    break;
                                }
                                
                            }
                            else if (input == "6")
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine(newCustomer.GetAccountDetails());
                                    break;
                                }

                            }
                            else if (input == "7")
                            {
                                // variables needed for account statement
                                int accountId;
                                while (true)
                                {
                                    Console.WriteLine("Select Account to carry out transaction");
                                    foreach (var account in customer.Account)
                                    {
                                        Console.WriteLine($"{account.Id}. {account.AccountName} {account.AccountNumber} {account.AccountType}");
                                    }
                                    var accId = Console.ReadLine();
                                    bool success = int.TryParse(accId, out accountId);
                                    if (!success)
                                    {
                                        Console.WriteLine("Customer Id must be a number");
                                        continue;
                                    }
                                    Console.Clear();
                                    Console.WriteLine(newCustomer.GetStatementOfAccount(accountId));
                                    break;
                                }
                            }
                            else if (input == "8")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input!");
                                continue;
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
