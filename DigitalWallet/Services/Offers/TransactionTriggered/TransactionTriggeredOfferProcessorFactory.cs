using DigitalWallet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Services.Offers.TransactionTriggered
{
    internal static class TransactionTriggeredOfferProcessorFactory
    {
        public static TransactionTriggeredOfferProcessor CreateInstance(Offer offer, WalletService walletService)
        {
            if (offer.Trigger != OfferTrigger.OnTransaction)
                throw new ArgumentException("Invalid offer trigger type");

            if (offer.Name == "Offer1")
                return new Offer1Processor(walletService);
            else if(offer.Name == "FixedDepositOffer")
                return new FixedDepositOfferProcessor(walletService);

            throw new ArgumentException($"Could not find a process for offer {offer.Name}");
        }
    }
}
