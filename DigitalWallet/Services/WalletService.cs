using DigitalWallet.Data;
using DigitalWallet.Model;


namespace DigitalWallet.Services
{
    internal class WalletService
    {
        IRepository<Wallet> _walletRepo;
        public WalletService(IRepository<Wallet> accountRepo)
        {
            _walletRepo = accountRepo;
        }

        public void CreateWallet(User user, double amount)
        {
            Wallet wallet = new Wallet(user, amount);
            
            _walletRepo.Add(wallet);
            
            Console.WriteLine("Account created for user " + user.Name + " with account number " + wallet.AccountNumber);
        }

        public void Transfer(int fromAccNum, int toAccNum, double transferAmount)
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

            
            Console.WriteLine("Transfer Successful");
        }

        private bool Validate(int fromAccNum, int toAccNum, double transferAmount)
        {
            if (fromAccNum == toAccNum)
            {
                Console.WriteLine("Sender and Receiver cannot be same.");
                return false;
            }
            if (transferAmount < 0.0001)
            {
                Console.WriteLine("Amount too low");
                return false;
            }

            var fromAcc = _walletRepo.Get(fromAccNum);
            if (fromAcc == null)
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
            
            return true;
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
            Console.WriteLine("Current Balance: " + account.Balance);
            Console.WriteLine("Your Transaction History");
            foreach (var transaction in account.Transactions)
                Console.WriteLine(transaction);

        }

        public void Overview()
        {
            foreach (var acc in _walletRepo.GetAll())
            {
                Console.WriteLine("Balance for account number " + acc.AccountNumber + ": ");
                Console.WriteLine(acc.Balance);
            }
        }
    }
}
