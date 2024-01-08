using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalWallet.Model;

namespace DigitalWallet.Services
{
    internal class Offer1Service
    {
     
        private Wallet _wallet1;
        private Wallet _wallet2;
        private readonly WalletService _walletService;

        public Offer1Service(WalletService walletService)
        {
            _walletService = walletService;
        }
        private bool Validate()
        {
            if (_wallet1 == null)
            {
                Console.WriteLine("Wallet1 cannot be null");
                return false;
            }

            if (_wallet2 == null)
            {
                Console.WriteLine("Wallet2 cannot be null");
                return false;
            }

            return true;
        }

        private bool CheckCondition()
        {
            return _wallet1.Balance == _wallet2.Balance;
        }
        public void ApplyOffer(Offer offer, Transaction transaction)
        {
            _wallet1 = transaction.FromWallet;
            _wallet2 = transaction.ToWallet;

            if (Validate())
            {
                bool conditionMet = CheckCondition();
                if (conditionMet)
                {
                    _walletService.CreditOfferAmount(_wallet1.AccountNumber, 10, offer);
                    _walletService.CreditOfferAmount(_wallet2.AccountNumber, 10, offer);
                }
            }
        }
    }
}
