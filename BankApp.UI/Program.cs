using System;
using BankApp.Data;
using BankApp.Model;

namespace BankApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            //var DataBase = new DataStore();
            //DataBase.AddCustomer(new Customer() { Email = "obi@gmail.com"});
            //string email = "obi@gmail.com";
            //var cust = new Customer();
            //try
            //{
            //    cust = DataBase.CheckMail(email);
            //    Console.WriteLine("email found");
            //}
            //catch (Exception)
            //{
            //    Console.WriteLine("Invalid email or password");
            //}

            //Console.WriteLine("ENd");
            var myBank = new UserInterface();
            myBank.RunApp();
            
        }
    }
}
