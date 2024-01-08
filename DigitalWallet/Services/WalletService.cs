using DigitalWallet.Data;
using DigitalWallet.Model;
using DigitalWallet.Services.Offers;

namespace DigitalWallet.Services
{
    internal class WalletService
    {
        IRepository<Wallet, int> _walletRepo;

        public WalletService(IRepository<Wallet, int> accountRepo)
        {
            _walletRepo = accountRepo;
        }

        public void CreateWallet(User user, double amount)
        {
            Wallet wallet = new Wallet(user, amount);

            _walletRepo.Add(wallet);

            Console.WriteLine("Account created for user " + user.Name + " with account number " + wallet.AccountNumber);
        }

        public void Transfer(int fromAccNum, int toAccNum, double transferAmount, OfferManagement offerManagement)
        {
            if (!Validate(fromAccNum, toAccNum, transferAmount))
            {
                return;
            }

            Wallet fromAccount = _walletRepo.Get(fromAccNum);
            Wallet toAccount = _walletRepo.Get(toAccNum);

            Transaction tran = new Transaction(fromAccount, toAccount, transferAmount);

            if (fromAccount.Balance < transferAmount)
            {
                Console.WriteLine("Insufficient Balance");
                return;
            }

            fromAccount.Balance -= transferAmount;
            toAccount.Balance += transferAmount;
            fromAccount.AddTransaction(tran);
            toAccount.AddTransaction(tran);

            if(fromAccount.FixedDepositAmount.HasValue 
                && fromAccount.Balance < fromAccount.FixedDepositAmount)
            {
                fromAccount.DissolveFixedDeposit();
            }

            offerManagement.ProcessOffersOnTransaction(tran);

            Console.WriteLine("Transfer Successful");
        }

        private bool Validate(int fromAccNum, int toAccNum, double transferAmount)
        {
            if (fromAccNum == toAccNum)
            {
                Console.WriteLine("Sender and Receiver cannot be same.");
                return false;
            }

            var fromAcc = _walletRepo.Get(fromAccNum);
            if (fromAcc == null || fromAcc == Wallet.Default)
            {
                Console.WriteLine("Invalid Sender account number");
                return false;
            }

            var toAcc = _walletRepo.Get(toAccNum);
            if (toAcc == null)
            {
                Console.WriteLine("Invalid Receiver account number");
                return false;
            }

            if (transferAmount < 0.0001)
            {
                Console.WriteLine("Amount too low");
                return false;
            }

            if(transferAmount > fromAcc.Balance)
            {
                Console.WriteLine("Insufficient balance");
            }

            return true;
        }

        public void StartAFixedDeposit(int accountNum, double amount)
        {
            Wallet account = _walletRepo.Get(accountNum);
            if (account == null)
            {
                Console.WriteLine("Invalid Account Number");
                return;
            }

            try
            {
                account.StartFixedDeposit(amount);
                Console.WriteLine($"Started a fixed deposit of {amount} for account num {account.AccountNumber}");
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Statement(int accountNum)
        {
            Wallet account = _walletRepo.Get(accountNum);
            if (account == null)
            {
                Console.WriteLine("Invalid Account Number");
                return;
            }

            Console.WriteLine("Summary for account number: " + accountNum);
            if( account.FixedDepositAmount.HasValue)
                Console.WriteLine("Fixed Deposit: " + account.FixedDepositAmount.Value.ToString());
            Console.WriteLine("Total Current Balance: " + account.Balance);
            Console.WriteLine("Your Transaction History");
            foreach (var transaction in account.Transactions)
                Console.WriteLine(transaction);

        }

        public void Overview()
        {
            foreach (var acc in _walletRepo.GetAll())
            {
                Console.WriteLine("Total balance for account number " + acc.AccountNumber + ": ");
                Console.Write(acc.Balance + " ");
                if (acc.FixedDepositAmount.HasValue)
                    Console.Write("that include a fixed deposit of amount: " + acc.FixedDepositAmount);
                Console.WriteLine();
            }
        }

        public void CreditOfferAmount(int toAccNum, double offerAmount, Offer offer)
        {
            var toWallet = _walletRepo.Get(toAccNum);
            if (toWallet == null)
            {
                Console.WriteLine("Invalid receiver account number");
                return;
            }

            toWallet.CreditOfferAmount(offerAmount);
        }
    }
}
