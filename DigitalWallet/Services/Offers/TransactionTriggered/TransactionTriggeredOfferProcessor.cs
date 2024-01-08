using DigitalWallet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Services.Offers.TransactionTriggered
{
    internal abstract class TransactionTriggeredOfferProcessor
    {
        public void ProcessOffer(Offer offer, Transaction transaction)
        {
            SetDataRequiredForOfferProcessing(transaction);

            if (Validate())
            {
                bool conditionMet = CheckCondition();
                if (conditionMet)
                {
                    ApplyOffer(offer);
                    Console.WriteLine($"Offer {offer.Name} applied");
                }
            }
        }

        protected abstract void SetDataRequiredForOfferProcessing(Transaction transaction);
        protected abstract bool Validate();
        protected abstract bool CheckCondition();
        protected abstract void ApplyOffer(Offer offer);
    }
}
