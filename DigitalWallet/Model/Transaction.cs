using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DigitalWallet.Model
{
    internal class Transaction
    {
        private Wallet _fromWallet;
        private Wallet _toWallet;
        private double _amount;
        private DateTime _createdOn;

        public Transaction(Wallet from, Wallet to, double amount)
        {
            this._fromWallet = from;
            this._toWallet = to;
            this._amount = amount;
            this._createdOn = DateTime.Now;
        }
  
        public Wallet FromWallet
        {
            get
            {
                return _fromWallet;
            }
        }

        public Wallet ToWallet
        {
            get
            {
                return _toWallet;
            }
        }

        public double Amount {
            get
            {
                return _amount;
            }
        }

        public DateTime CreatedOn
        {
            get
            {
                return _createdOn;
            }
        }

        public override string ToString()
        {
            if(_fromWallet == Wallet.Default)
                return "Offer credit [to=" + _toWallet.AccountNumber + ", amount=" + _amount + ", date=" + _createdOn + "]";
            
            return "Transaction [from=" + _fromWallet.AccountNumber + ", to=" + _toWallet.AccountNumber + ", amount=" + _amount + ", date=" + _createdOn + "]";
        }

    }
}
