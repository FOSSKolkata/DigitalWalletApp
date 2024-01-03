using DigitalWallet.Data;
using DigitalWallet.Model;
using DigitalWallet.Services;

namespace DigitalWallet
{
    internal class Driver
    {
        static void Main(string[] args)
        {
            WalletService wService = new WalletService(new WalletRepository());
       
            outer: while (true)
            {
                Console.WriteLine("\nOPTIONS:");
                Console.WriteLine("1. Create wallet");
                Console.WriteLine("2. Transfer Amount");
                Console.WriteLine("3. Account Statement");
                Console.WriteLine("4. Overview");
                Console.WriteLine("5. Exit");
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        Console.WriteLine("YOU SELECTED CREATE WALLET");
                        Console.WriteLine("Enter name");
                        String name = Console.ReadLine();
                        Console.WriteLine("Enter amount");
                        double amount = Convert.ToDouble(Console.ReadLine());
                        wService.CreateWallet(new User(name), amount);
                        break;
                    case 2:
                        Console.WriteLine("YOU SELECTED TRANSFER");
                        Console.WriteLine("Enter SENDER account number");
                        int from = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter RECEIVER account number");
                        int to = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter amount");
                        double amount1 = Convert.ToDouble(Console.ReadLine());
                        wService.Transfer(from, to, amount1);
                        break;
                    case 3:
                        Console.WriteLine("YOU SELECTED ACCOUNT STATEMENT");
                        Console.WriteLine("Enter account num");
                        wService.Statement(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case 4:
                        Console.WriteLine("YOU SELECTED OVERVIEW");
                        wService.Overview();
                        break;

                    case 5:
                        Console.WriteLine("APPLICATION STOPPED");
                        goto outer;

                    default:
                        Console.WriteLine("YOU HAVE SELECTED INVALID OPTION. PLEASE REENTER");
                        break;
                }
            }
        }
    }
}