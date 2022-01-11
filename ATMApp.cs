using System;
using System.Collections.Generic;
using System.Text;

namespace ATMApplication
{
    class ATMApp : ILogin, IBalance, IDeposit, IWithdraw
    {
        private static int tries;
        private const int maxTries = 3;
        private static decimal amountEntered;

        private static List<AccountInfo> _accountList;
        private static AccountInfo selectedAccount;
        private static AccountInfo inputAccount;

        public void Run()
        {
           PrintLogInMenu();

            while (true)
            {
                switch (Console.ReadLine()[0])
                {
                    case '1':
                        CheckPassword();

                        while (true)
                        {
                            PrintATMOptions();

                            switch (Console.ReadLine()[0])
                            {
                                case '1':
                                    CheckBalance(selectedAccount);
                                    break;
                                case '2':
                                    Deposit(selectedAccount);
                                    break;
                                case '3':
                                    Withdraw(selectedAccount);
                                    break;
                                case '4':
                                    System.Environment.Exit(1);
                                    break;

                                default:
                                    Console.WriteLine("Invalid Option! Try Again");
                                    break;
                            }
                        }

                    case '2':
                        System.Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("Invalid Option! Try Again");
                        break;
                }
            }
        }

        private void PrintLogInMenu()
        {
            Console.Clear();
            Console.WriteLine("ATM Main Menu");
            Console.WriteLine("1. Insert ATM card");
            Console.WriteLine("2. Exit");
        }

        private void PrintATMOptions()
        {
            Console.Clear();
            Console.WriteLine("Choose An Option to Proceed");
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdrawal");
            Console.WriteLine("4. Exit");
        }

        public void Initialize()
        {
            _accountList = new List<AccountInfo>
            {
                new AccountInfo() { Name = "Baris", AccountID=101, CardID = 1001, Password = 1234, Balance = 10000.00m },
                new AccountInfo() { Name = "Seyma", AccountID=102, CardID = 1002, Password = 5678, Balance = 12500.00m },
                new AccountInfo() { Name = "Melih", AccountID=103, CardID = 1003, Password = 0000, Balance = 2500.00m },
                new AccountInfo() { Name = "Emre", AccountID=104, CardID = 1003, Password = 0000, Balance = 2500.10m }
            };
        }

        public void CheckPassword()
        {
            bool pass = false;

            while (!pass)
            {
                inputAccount = new AccountInfo();

                Console.Write("Enter ATM Card Number: ");
                inputAccount.CardID = Convert.ToInt32(Console.ReadLine());

                foreach (AccountInfo account in _accountList)
                {
                    if (inputAccount.CardID.Equals(account.CardID))
                    {
                        selectedAccount = account;

                        Console.Write("Enter Card Password: ");
                        inputAccount.Password = Convert.ToInt32(Console.ReadLine());

                        if (inputAccount.Password.Equals(account.Password))
                        {
                                pass = true;
                        }
                        else
                        {
                            Console.WriteLine("Wrong Password!");
                            pass = false;
                            tries++;

                            if (tries >= maxTries)
                            {
                                Console.WriteLine("You entered wrong password. The program will exit.");
                                Console.ReadLine();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong card id!");
                    }
                }
                Console.Clear();
            }
        }

        public void CheckBalance(AccountInfo account)
        {
            Console.WriteLine($"Your current balance amount is: {account.Balance}");
            Console.ReadLine();
        }

        public void Deposit(AccountInfo account)
        {

            Console.WriteLine("Enter amount: ");
            amountEntered = Convert.ToDecimal(Console.ReadLine());

            if (amountEntered <= 0)
            {   Console.WriteLine("Amount needs to be more than zero. Try again.");
            }
            else
            {
                account.Balance = account.Balance + amountEntered;

                Console.WriteLine($"You have successfully deposited {amountEntered}");
                Console.WriteLine($"Your current balance is {account.Balance}");
            }
            Console.ReadLine();
        }

        public void Withdraw(AccountInfo account)
        {
            Console.Write("Enter amount: ");
            amountEntered = Convert.ToDecimal(Console.ReadLine());

            if (amountEntered <= 0)
                Console.WriteLine("Amount needs to be more than zero. Try again.");
            else if (amountEntered > account.Balance)
                Console.WriteLine($"You do not have enough fund to withdraw {amountEntered}");
            else
            {
                account.Balance = account.Balance - amountEntered;

                Console.WriteLine($"You have successfully withdrawn {amountEntered}");
                Console.WriteLine($"Your current balance is {account.Balance}");
            }
            Console.ReadLine();
        }
    }
}