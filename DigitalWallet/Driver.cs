using DigitalWallet.Data;
using DigitalWallet.Model;
using DigitalWallet.Services;
using DigitalWallet.Services.Offers;
using System.Runtime.CompilerServices;

namespace DigitalWallet
{
    internal class Driver
    {
        static void Main(string[] args)
        {
            WalletRepository walletRepo = new WalletRepository();
            OfferRepository offerRepo = new OfferRepository();
            WalletService walletService = new WalletService(walletRepo);
            OfferManagement offerManagement = new OfferManagement(offerRepo, walletRepo, walletService);
            
            SeedOffers(offerRepo);

            outer: while (true)
            {
                Console.WriteLine("\nOPTIONS:");
                Console.WriteLine("1. Create wallet");
                Console.WriteLine("2. Transfer Amount");
                Console.WriteLine("3. Apply Offer");
                Console.WriteLine("4. Start A Fixed Deposit");
                Console.WriteLine("5. Account Statement");
                Console.WriteLine("6. Overview");
                Console.WriteLine("7. Exit");
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        Console.WriteLine("YOU SELECTED CREATE WALLET");
                        Console.WriteLine("Enter name");
                        String name = Console.ReadLine();
                        Console.WriteLine("Enter amount");
                        double amount = Convert.ToDouble(Console.ReadLine());
                        walletService.CreateWallet(new User(name), amount);
                        break;
                    case 2:
                        Console.WriteLine("YOU SELECTED TRANSFER");
                        Console.WriteLine("Enter SENDER account number");
                        int from = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter RECEIVER account number");
                        int to = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter amount");
                        double amount1 = Convert.ToDouble(Console.ReadLine());
                        walletService.Transfer(from, to, amount1, offerManagement);
                        break;
                    case 3:
                        Console.WriteLine("YOU SELECTED APPLY OFFER");
                        Console.WriteLine("Enter offer ID");
                        int offerId = Convert.ToInt32(Console.ReadLine());
                        offerManagement.ProcessOfferOnDemand(offerId);
                        break;
                    case 4:
                        Console.WriteLine("YOU SELECTED START A FIXED DEPOSIT");
                        Console.WriteLine("Enter account num");
                        int accountNum = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter deposit amount");
                        double depositAmount = Convert.ToDouble(Console.ReadLine());
                        walletService.StartAFixedDeposit(accountNum, depositAmount);    
                        break;
;                    case 5:
                        Console.WriteLine("YOU SELECTED ACCOUNT STATEMENT");
                        Console.WriteLine("Enter account num");
                        walletService.Statement(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case 6:
                        Console.WriteLine("YOU SELECTED OVERVIEW");
                        walletService.Overview();
                        break;

                    case 7:
                        Console.WriteLine("APPLICATION STOPPED");
                        goto outer;

                    default:
                        Console.WriteLine("YOU HAVE SELECTED INVALID OPTION. PLEASE REENTER");
                        break;
                }
            }
        }

        static void SeedOffers(OfferRepository offerRepo)
        {

            var offer1 = new Offer(1, "Offer1", OfferTrigger.OnTransaction);
            var offer2 = new Offer(2, "Offer2", OfferTrigger.OnDemand);
            var fixedDepositOffer = new Offer(3, "FixedDepositOffer", OfferTrigger.OnTransaction);

            offerRepo.Add(offer1);
            offerRepo.Add(offer2);
            offerRepo.Add(fixedDepositOffer);
        }
    }
}