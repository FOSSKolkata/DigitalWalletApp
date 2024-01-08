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
        private DateTime _createdOn;
        private double? _fixedDepositAmount;
        private DateTime? _fixedDepositCreatedOn; 

        public static Wallet Default = new Wallet(new User("Google"), 1000000000);  
        public Wallet(User user, double amount)
        {
            this._accountNumber = AccountNumberGenerator.GetNextAccountNumber();
            this._user = user;
            this._balance = amount;
            this._transactions = new HashSet<Transaction>();
            this._createdOn = DateTime.UtcNow;
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

        public double? FixedDepositAmount
        {
            get
            {
                return _fixedDepositAmount;
            }
        } 

        public DateTime? FixedDepositCreatedOn
        {
            get { return _fixedDepositCreatedOn; }
        }

        public DateTime CreatedOn { 
            get => _createdOn;
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

            if (transaction.FromWallet.Balance < transaction.Amount)
                throw new ArgumentException("Insufficient Balance");

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

        public void StartFixedDeposit(double depositAmount)
        {
            if (depositAmount > _balance)
                throw new ArgumentException("Insufficient Balance");

            _fixedDepositAmount = depositAmount;
            _fixedDepositCreatedOn = DateTime.UtcNow;
        }

        internal void DissolveFixedDeposit()
        {
            _fixedDepositAmount = null;
            _fixedDepositCreatedOn = null;
        }
    }
}

