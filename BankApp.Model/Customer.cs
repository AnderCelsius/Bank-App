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
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordKey { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Account> Account { get; set; } = null;

        private int count;
        public Customer()
        {
            Id += count++;
            DateCreated = DateTime.Now;
        }
    }
}
