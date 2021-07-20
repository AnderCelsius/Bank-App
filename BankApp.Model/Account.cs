using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Model
{
    public class Account
    {
        public int Id { get; private set; }
        public int CustomerId { get; set; }
        public int AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public double AccountBalance { get; set; }
        public TransactionHistory TransactionHistory { get; set; } = null;

        private int count;
        private int accountNumber = 100;
        private double accountBalance = 0;
        public Account()
        {
            Id += count++;
            AccountBalance = accountBalance;
            AccountNumber = accountNumber;
            accountNumber++;
        }
    }
}
