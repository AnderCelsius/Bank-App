using BankApp.Model;
using System;
using System.Collections.Generic;

namespace BankApp.Data
{
    public class DataStore
    {
        public static List<Customer> CustomerTable { get; set; } = new List<Customer>();
        public static List<Account> AccountTable { get; set; } = new List<Account>();
        public static List<TransactionHistory> TransactionHistoryTable { get; set; } = new List<TransactionHistory>();
    }
}
