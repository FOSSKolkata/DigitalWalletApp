using DigitalWallet.Services;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DigitalWallet.Model
{
    internal class Wallet
    {
        private int _accountNumber;
        private User _user;
        private double _balance;
        private HashSet<Transaction> _transactions;

        public static Wallet Default = new Wallet(new User("Google"), 1000000000);  
        public Wallet(User user, double amount)
        {
            this._accountNumber = AccountNumberGenerator.GetNextAccountNumber();
            this._user = user;
            this._balance = amount;
            this._transactions = new HashSet<Transaction>();
        }

      
        public int AccountNumber
        {
            get
            {
                return _accountNumber;
            }
        }

        public User User { get; }

        public double Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                // validations
                _balance = value;
            }
        }

        public IReadOnlyCollection<Transaction> Transactions
        {
            get
            {
                return (IReadOnlyCollection<Transaction>)_transactions;
            }
        }

        public override String ToString()
        {
            return "Wallet [accountNumber=" + _accountNumber + ", name=" + this._user.Name + ", balance=" + _balance
                    + ", tranactions=" + _transactions + "]";
        }

        public void AddTransaction(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException("Transation cannot be null");


            if (transaction.FromWallet != this && transaction.ToWallet != this)
                throw new ArgumentException("Transation does not belong to the wallet");

            _transactions.Add(transaction);
        }

        public void CreditOfferAmount(double offerAmount)
        {
            if (offerAmount <= 0)
                throw new ArgumentNullException("Invalid offer amount");

            Transaction offerCredit = new Transaction(Wallet.Default, this, offerAmount);

            this._balance += offerCredit.Amount;
            this._transactions.Add(offerCredit);
        }
    }
}

