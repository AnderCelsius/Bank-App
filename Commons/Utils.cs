using System;

namespace Commons
{
    public class Utils
    {
        public enum AccountType
        {
            Savings = 1,
            Current = 2
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
    }
}
