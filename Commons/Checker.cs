using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Commons
{
    public class Checker
    {
        public static bool ValidateName(string name)
        {
            if (!Regex.IsMatch(name, @"[A-Za-z]")) //Names must only contain strings
                return false;
            return true;
        }

        public static bool ValidateEmail(string name)
        {
            if (!Regex.IsMatch(name, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")) //Names must only contain strings
                return false;
            return true;
        }

        public static bool ValidatePassword(string password)
        {
            if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,15}$"))
                return false;
            return true;
        }

        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            if (!Regex.IsMatch(phoneNumber, @"^[0]\d{10}$"))
                return false;
            return true;
        }

        public static bool ValidateTransAccount(string accountNumber)
        {
            if (!Regex.IsMatch(accountNumber, @"\d{10}$"))
                return false;
            return true;
        }
    }
}
