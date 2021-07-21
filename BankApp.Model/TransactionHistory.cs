using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Model
{
    public class TransactionHistory
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public double Amount { get; set; }
        public string TransactionType { get; set; }
        public string Sender { get; set; }
        public string ReceiverAccountName { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public string Description { get; set; }
        public double Balance { get; set; }
        public DateTimeOffset TransactionDate { get; set; }

        private int count = 1;
        public TransactionHistory()
        {
            Id += count++;
            TransactionDate = DateTime.Now;
        }
    }
}
