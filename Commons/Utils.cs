using System;

namespace Commons
{
    public class Utils
    {
        public enum AccountType
        {
            Savings,
            Current
        }

        public enum TransactionType
        {
            Credit = 'C',
            Debit = 'D'
        }

        public enum TransactionDescription
        {
            POS,
            ATM,
            USSD,
            FIP,
        }

        public static string GenerateAccountNumber()
        {
            var accountNumber = string.Empty;

            String startWith = "32";
            Random generator = new Random();
            String r = generator.Next(0, 999999).ToString("D8");
            accountNumber = startWith + r;

            return accountNumber;
        }
    }
}
