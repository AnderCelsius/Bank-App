using System;
using System.Collections.Generic;
using System.Text;
using Commons;

namespace BankApp.Model
{
    public class Account
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public double AccountBalance { get; set; }
        public List<TransactionHistory> TransactionHistory { get; set; } 

        private string accountNumber = Utils.GenerateAccountNumber();
        private double accountBalance = 0;
        public Account()
        {
            AccountBalance = accountBalance;
            AccountNumber = accountNumber;
            TransactionHistory = new List<TransactionHistory>();
        }
    }
}
