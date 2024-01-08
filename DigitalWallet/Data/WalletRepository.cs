using DigitalWallet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Data
{
    internal class WalletRepository : IRepository<Wallet, int>
    {
        List<Wallet> _wallets;
        public WalletRepository()
        {
            _wallets = new List<Wallet>();
            _wallets.Add(Wallet.Default);
        }

        public void Add(Wallet entity)
        {
            _wallets.Add(entity);
        }

        public Wallet Get(int id)
        {
            return _wallets.Where(x => x.AccountNumber == id).SingleOrDefault();
        }

        public List<Wallet> GetAll()
        {
            return _wallets;
        }
    }
}
