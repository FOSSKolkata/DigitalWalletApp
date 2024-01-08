using DigitalWallet.Data;
using DigitalWallet.Model;
using DigitalWallet.Services.Offers.TransactionTriggered;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Services.Offers
{
    internal class OfferManagement
    {
        private readonly IRepository<Offer, int> _offerRepo;
        private readonly IRepository<Wallet, int> _walletRepo;
        private readonly WalletService _walletService;
        public OfferManagement(IRepository<Offer, int> offerRepo,
            IRepository<Wallet, int> walletRepo,
            WalletService walletService)
        {
            _offerRepo = offerRepo;
            _walletRepo = walletRepo;
            _walletService = walletService;
        }

        public void ProcessOffersOnTransaction(Transaction transaction)
        {
            // Select all transactional offers 
            var offers = _offerRepo
                            .GetAll()
                            .Where(x => x.Trigger == OfferTrigger.OnTransaction)
                            .ToList();


            foreach (var offer in offers)
            {
                var offerProcessor = TransactionTriggeredOfferProcessorFactory.CreateInstance(offer, _walletService);

                offerProcessor.ProcessOffer(offer, transaction);
            }
        }

        public void ProcessOfferOnDemand(int offerId)
        {
            var offer = _offerRepo.Get(offerId);
            if(offer == null)
            {
                Console.WriteLine("Invalid offer id");
            }

            if(offer.Trigger == OfferTrigger.OnTransaction)
            {
                Console.WriteLine($"The offer {offer.Name} is applied automatically when a transaction is done");
                return;
            }

            if(offer.Name == "Offer2")
            {
                var offerProcessor = new Offer2Processor(_walletRepo, _walletService);
                offerProcessor.ApplyOffer(offer);
                Console.WriteLine("Offer applied");
            }
        }

    }
}
