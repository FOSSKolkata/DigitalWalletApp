using DigitalWallet.Data;
using DigitalWallet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Services.Offers
{
    internal class Offer2Processor
    {
        private readonly IRepository<Wallet, int> _walletRepo;

        private readonly WalletService _walletService;
        public Offer2Processor(IRepository<Wallet, int> walletRepo, WalletService walletService)
        {
            _walletRepo = walletRepo;
            _walletService = walletService;
        }


        public void ApplyOffer(Offer offer)
        {
            List<Wallet> top3Wallets = _walletRepo
                                   .GetAll()
                                   .OrderByDescending(x => x.Transactions.Count)
                                   .ThenByDescending(x => x.Balance)
                                   .ThenByDescending(x => x.CreatedOn)
                                   .Take(3)
                                   .ToList();

            if (top3Wallets.Count == 3)
                _walletService.CreditOfferAmount(top3Wallets[2].AccountNumber, 10, offer);

            if (top3Wallets.Count >= 2)
                _walletService.CreditOfferAmount(top3Wallets[1].AccountNumber, 5, offer);

            if (top3Wallets.Count >= 1)
                _walletService.CreditOfferAmount(top3Wallets[0].AccountNumber, 2, offer);
        }
    }
}
