using System;
using System.Collections.Generic;
using System.Text;

namespace Commons
{
    public class MenuOptions
    {
        public static string CreateAccountUI()
        {
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

            return accountType;
           
        }

        public static string TrasactionChannel()
        {
            var transactionChannelType = string.Empty;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Choose transaction channel: ");
                int count = 1;

                foreach (var transactionChannel in Enum.GetNames(typeof(Utils.TransactionDescription)))
                {
                    Console.WriteLine($"{count}: {transactionChannel}");
                    count++;
                }
                var response = Console.ReadLine();
                if (response == "1")
                {
                    transactionChannelType += Utils.TransactionDescription.POS;
                    break;
                }
                else if (response == "2")
                {
                    transactionChannelType += Utils.TransactionDescription.ATM;
                    break;
                }
                else if (response == "3")
                {
                    transactionChannelType += Utils.TransactionDescription.USSD;
                    break;
                }
                else if (response == "4")
                {
                    transactionChannelType += Utils.TransactionDescription.FIP;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again");
                    continue;
                }
            }
            return transactionChannelType;
        }
    }
}
