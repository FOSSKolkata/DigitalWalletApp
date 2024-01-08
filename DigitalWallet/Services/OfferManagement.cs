using DigitalWallet.Data;
using DigitalWallet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Services
{
    internal class OfferManagement
    {
        private readonly IRepository<Offer, int> _offerRepo;
        private readonly IRepository<Wallet, int> _walletRepo;
        private readonly WalletService _walletService;
        public OfferManagement(IRepository<Offer, int> offerRepo,
            IRepository<Wallet, int> _walletRepo,
            WalletService walletService)
        {
            _offerRepo = offerRepo;
            this._walletRepo = _walletRepo;
            _walletService = walletService;

        }

        public void ApplyOffersOnTransaction(Transaction transaction)
        {
            // Select all transactional offers 
            var offers = _offerRepo
                            .GetAll()
                            .Where(x => x.Trigger == OfferTrigger.OnTransaction)
                            .ToList();

            // Apply offers 
            foreach (var offer in offers)
            {
                if (offer.Name == "Offer1")
                {
                    Offer1Service offer1Service = new Offer1Service(_walletService);

                    offer1Service.ApplyOffer(offer, transaction);
                }
                else
                {
                    Console.WriteLine("No handler found for offer " + offer.Name );
                }

            }
        }

    }
}
