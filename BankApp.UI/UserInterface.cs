using BankApp.Model;
using System;
using System.Text.RegularExpressions;

namespace BankApp.UI
{
    class UserInterface
    {
        public void RunApp()
        {
            Console.WriteLine("WELCOME TO ONE BANK");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press enter to start or any other key to exit");
            Console.Write(">>>");
            string begin = Console.ReadLine();
            Console.Clear();

            while (string.IsNullOrWhiteSpace(begin))
            {
                Console.WriteLine("Press 1 to Register");
                Console.WriteLine("Press 2 to Log In");
                Console.WriteLine("Press 3 to Quit");
                Console.Write(">>>");
                try
                {
                    int choice = int.Parse(Console.ReadLine());
                    if (choice == 1)
                    {
                        // User Registration
                        // Required Fields
                        var firstName = string.Empty;
                        var lastName = string.Empty;
                        var email = string.Empty;
                        var phoneNumber = string.Empty;
                        var password = string.Empty;
                        var accountType = string.Empty;
                        Console.Clear();


                        Console.WriteLine("Please fill in all fields");
                        var isFirstName = true;
                        while (isFirstName)
                        {
                            Console.Write("First Name: ");
                            var response = Console.ReadLine();

                            Console.Clear();

                            if (!Regex.IsMatch(response, @"[A-Za-z]")) //Names must only contain strings
                            {
                                Console.WriteLine("Invalid input!");
                                Console.WriteLine("Name can only contain alphabeths");
                                Console.WriteLine("");
                                continue;
                            }
                            else
                            {
                                firstName += response;
                                isFirstName = false;
                            }
                        }

                        var isLastName = true;
                        while (isLastName)
                        {
                            Console.Write("Last Name: ");
                            var response = Console.ReadLine();

                            Console.Clear();

                            if (!Regex.IsMatch(response, @"[A-Za-z]")) //Names must only contain strings
                            {
                                Console.WriteLine("Invalid input!");
                                Console.WriteLine("Name can only contain alphabeths");
                                Console.WriteLine("");
                                continue;
                            }
                            else
                            {
                                lastName += response;
                                isLastName = false;
                            }
                        }

                        var isEmail = true;
                        while (isEmail)
                        {
                            Console.Write("Email: ");
                            var response = Console.ReadLine();

                            Console.Clear();

                            if (!Regex.IsMatch(response, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                            {
                                Console.WriteLine("Invalid email address!");
                                Console.WriteLine("");
                                continue;
                            }
                            else
                            {
                                email += response;
                                isEmail = false;
                            }

                        }

                        var isPhone = true;
                        while (isPhone)
                        {
                            Console.Write("Phone Number: ");
                            var response = Console.ReadLine();

                            Console.Clear();

                            if (!Regex.IsMatch(response, @"([0-9]{11})"))
                            {
                                Console.WriteLine("Invalid phone number");
                                Console.WriteLine("");
                                continue;
                            }
                            else
                            {
                                phoneNumber += response;
                                isPhone = false;
                            }
                        }

                        var isPassword = true;
                        while (isPassword)
                        {
                            Console.WriteLine($"Hint: Password should be minimum of 6 characters\n" +
                                $"      Should include alphanumeric and at least one special characters (@, #, $, %, ^, &, !)");
                            Console.WriteLine();
                            Console.Write("Password: ");
                            var response = Console.ReadLine().Trim();

                            Console.Clear();

                            if (!Regex.IsMatch(response, @"([0-9]{6})"))
                            {
                                Console.WriteLine("Invalid password");
                                Console.WriteLine("");
                                continue;
                            }
                            else
                            {
                                Console.Write("Confirm Password: ");
                                var response2 = Console.ReadLine().Trim();
                                if (response2 == response)
                                {
                                    password += response;
                                    isPassword = false;
                                }
                                else
                                {
                                    Console.WriteLine("Password did not match. Please try again");
                                    continue;
                                }
                            }

                        }
                    }
                    else if (choice == 2)
                    {
                        Console.Clear();
                        Console.WriteLine(""); //Enter login method
                        Console.ReadLine();
                        Console.Clear();


                        //User log in details
                        var email = string.Empty;
                        var phoneNumber = string.Empty;
                        var password = string.Empty;

                        var isValid = true;
                        while (isValid)
                        {
                            Console.Write("Enter Email/Phone Number: ");
                            var response = Console.ReadLine();

                            Console.Clear();

                            if (!Regex.IsMatch(response, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")  || !Regex.IsMatch(response, @"([0-9]{11})"))
                            {
                                Console.WriteLine("Invalid input");
                                Console.WriteLine("");
                                continue;
                            }
                            else
                            {
                                if (Regex.IsMatch(response, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                                {
                                    email += response;
                                    isValid = false;
                                }
                                    
                                phoneNumber += response;
                                isValid = false;
                            }

                        }

                        Customer customer = new Customer();

                        Console.WriteLine($"Welcome {customer.FullName}");
                        Console.WriteLine();
                        Console.WriteLine("To activate your account, please make deposit");
                        Console.WriteLine();
                        Console.WriteLine("* Savings Account holder --> minimum N1000");
                        Console.WriteLine("* Current Account holder --> minimum N5000");
                        Console.WriteLine();

                        var makeDeposit = true;
                        while (makeDeposit)
                        {
                            Console.WriteLine("Press 1 to make deposit");
                            Console.WriteLine("Press 2 to Log Out");
                            var response = Console.ReadLine();
                            if(response == "1")
                            {
                                // Call make deposit mwthod
                                // should return "Account Activated Successfully!
                            }
                            else if(response == "2")
                            {
                                //Call logout method
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please try again");
                                continue;
                            }
                        }
                            


                    }
                    else if (choice == 3)
                    {
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid Input");
                        continue;
                    }

                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Please choose a valid option");
                }
            }


        }
    }
}
