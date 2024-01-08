using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Model;

namespace DigitalWallet.Services.Offers.TransactionTriggered
{
    internal class FixedDepositOfferProcessor: TransactionTriggeredOfferProcessor
    {

        private Wallet _wallet;

        private readonly WalletService _walletService;

        public FixedDepositOfferProcessor(WalletService walletService)
        {
            _walletService = walletService;
        }

        protected override void SetDataRequiredForOfferProcessing(Transaction transaction)
        {
            _wallet = transaction.FromWallet;
        }

        protected override bool Validate()
        {
            if (_wallet == null)
            {
                Console.WriteLine("Wallet cannot be null");
                return false;
            }

            return true;
        }

        protected override bool CheckCondition()
        {
            bool conditionMet = _wallet.FixedDepositAmount.HasValue;
            conditionMet &= _wallet.Transactions
                            .Where(x => x.CreatedOn > _wallet.FixedDepositCreatedOn)
                            .Count() == 5;

            return conditionMet;
        }

        protected override void ApplyOffer(Offer offer)
        {
            _walletService.CreditOfferAmount(_wallet.AccountNumber, 10, offer);
        }
    }
}
