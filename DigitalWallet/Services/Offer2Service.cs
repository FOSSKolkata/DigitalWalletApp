using DigitalWallet.Data;
using DigitalWallet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Services
{
    internal class Offer2Service
    {
        private readonly IRepository<Wallet, int> _walletRepo;

        private readonly WalletService _walletService;
        public Offer2Service(IRepository<Wallet, int> walletRepo, WalletService walletService)
        {
            _walletRepo = walletRepo;
            _walletService = walletService;
        }


        public void ApplyOffer(Offer offer)
        {
            List<Wallet> top3Wallets = _walletRepo
                                   .GetAll()
                                   .OrderByDescending(x => x.Transactions.Count)
                                   .Take(3)
                                   .ToList();

            if (top3Wallets.Count == 3)
                _walletService.CreditOfferAmount(top3Wallets[2].AccountNumber, 10, offer);
        }
    }
}
