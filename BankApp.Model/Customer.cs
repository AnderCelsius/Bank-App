using System;
using System.Collections.Generic;

namespace BankApp.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return LastName + " " + FirstName; }
        }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Account> Account { get; set; }

        private static int count = 1;
        public Customer()
        {
            Id += count++;
            DateCreated = DateTime.Now;
            Account = new List<Account>();
        }

    }
}
