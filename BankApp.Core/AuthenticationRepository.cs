using BankApp.Data;
using BankApp.Model;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BankApp.Core
{
    public class AuthenticationRepository
    {
        public static bool Login(string email, string password)
        {
            bool isValid = false;
            foreach (var customer in DataStore.CustomerTable)
            {
                if (customer.Email == email && customer.Password == password)
                {
                    isValid = true;
                }
            }

            return isValid;
        }

        public static bool Logout(string email, string password)
        {
            return Login(email, password) == false;
        }

    }
}
