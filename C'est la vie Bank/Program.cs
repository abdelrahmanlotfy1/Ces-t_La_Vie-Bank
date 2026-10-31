using System;
using System.Linq;
using C_est_la_vie_Bank;

namespace Cest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankServices bank = new BankServices();
            Notifications notifier = new Notifications();

            while (true)
            {
                Console.WriteLine(" Cest La Vie Bank Console Menu\n ");
                Console.WriteLine("1. Open Account");
                Console.WriteLine("2. Deposit     ");
                Console.WriteLine("3. Withdraw    ");
                Console.WriteLine("4. Transfer    ");
                Console.WriteLine("5. Statement   ");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");

                string? choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter new Account ID (min 10 characters): ");
                            string id = Console.ReadLine() ?? "";
                            CurrentAccount newAcc = new CurrentAccount(id, 0);
                            bank.Accounts.Add(newAcc);

                            notifier.Subscribe(newAcc);
                            Console.WriteLine("Account opened successfully.");
                            break;

                        case "2":
                            Console.Write("Enter Account ID: ");
                            string depId = Console.ReadLine() ?? "";
                            Account depAcc = bank.Accounts.First(a => a.ID == depId);

                            Console.Write("Enter Amount to Deposit: ");
                            decimal depAmount = decimal.Parse(Console.ReadLine() ?? "0");
                            bank.Deposite(depAcc, depAmount);
                            break;

                        case "3":
                            Console.Write("Enter Account ID: ");
                            string withId = Console.ReadLine() ?? "";
                            Account withAcc = bank.Accounts.First(a => a.ID == withId);

                            Console.Write("Enter Amount to Withdraw: ");
                            decimal withAmount = decimal.Parse(Console.ReadLine() ?? "0");
                            bank.Withdraw(withAcc, withAmount);
                            break;

                        case "4":
                            Console.Write("Enter Sender Account ID: ");
                            string fromId = Console.ReadLine() ?? "";
                            Account fromAcc = bank.Accounts.First(a => a.ID == fromId);

                            Console.Write("Enter Receiver Account ID: ");
                            string toId = Console.ReadLine() ?? "";
                            Account toAcc = bank.Accounts.First(a => a.ID == toId);

                            Console.Write("Enter Amount to Transfer: ");
                            decimal transAmount = decimal.Parse(Console.ReadLine() ?? "0");

                            bool transferSuccess = bank.Transfer(fromAcc, toAcc, transAmount);
                            if (transferSuccess)
                                Console.WriteLine("Transfer completed.");
                            else
                                Console.WriteLine("Transfer failed (Insufficient funds or invalid input).");
                            break;

                        case "5":
                            Console.Write("Enter Account ID: ");
                            string statId = Console.ReadLine() ?? "";
                            var statements = bank.GetStatement(statId);

                            Console.WriteLine($"\n--- Transaction History for {statId} ---");
                            if (statements.Count == 0)
                            {
                                Console.WriteLine("No transactions found.");
                            }
                            else
                            {
                                foreach (var record in statements)
                                {
                                    Console.WriteLine(record.ToString());
                                }
                            }
                            break;

                        case "0":
                            Console.WriteLine("Thank you for using PocketBank!");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Error: Account not found in the system.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Validation Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected Error: {ex.Message}");
                }
            }
        }
    }
}