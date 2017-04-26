using System;
using System.Collections.Generic;
using System.Linq;
using IMS.BL.Repositories;
using IMS.BL.DataModel;
using IMS.UI;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IMS.UI.ViewModels
{
    public class ViewTransactionsPageVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Readonly so no need OnPropertyChanged()
        /// </summary>
        public List<Transaction> Transactions { get; private set; }

        int _CurrentTransactionID;
        public int CurrentTransactionID
        {
            get
            {
                return _CurrentTransactionID;
            }
            set
            {
                _CurrentTransactionID = value;
                OnPropertyChanged();
            }
        }

        decimal _CurrentTotalValue;
        public decimal CurrentTotalValue
        {
            get
            {
                return _CurrentTotalValue;
            }
            set
            {
                _CurrentTotalValue = value;
                OnPropertyChanged();
            }
        }

        DateTime _CurrentTimeOfTransaction;
        public DateTime CurrentTimeOfTransaction
        {
            get
            {
                return _CurrentTimeOfTransaction;
            }
            set
            {
                _CurrentTimeOfTransaction = value;
                OnPropertyChanged();
            }
        }

        private Transaction _Transaction;
        public Transaction CurrentTransaction
        {
            get
            {
                return _Transaction;
            }
            set
            {
                _Transaction = value;
                OnPropertyChanged();
            }
        }
        
        private List<ItemTransactionDisplay> _CurrentItemTransactionDisplays;
        public List<ItemTransactionDisplay> CurrentItemTransactionDisplays
        {
            get
            {
                return _CurrentItemTransactionDisplays;
            }
            set
            {
                _CurrentItemTransactionDisplays = value;
                OnPropertyChanged();
            }
        }

        private List<ItemTransaction> _CurrentItemTransactions;
        public List<ItemTransaction> CurrentItemTransactions
        {
            get
            {
                return _CurrentItemTransactions;
            }
            set
            {
                _CurrentItemTransactions = value;
                OnPropertyChanged();
            }
        }

        #region OnPropertyChanged
        //will tell view that a property has changed
        //may need to rerender a control
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged( [CallerMemberName] string caller = "" )
        {
            if ( PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
        #endregion

        public ViewTransactionsPageVM()
        {
            //assigns default values to display
            using (var TRepo = new TransactionRepo(new InventoryContext()))
            {
                Transactions = TRepo.AllTransactionsIncludeItemTransaction();
                TRepo.Complete();
            }

            //Default values from first transaction from db
            SelectionChanged(Transactions[0]);
        }

        /// <summary>
        /// Called when selection for
        /// CurrentItem has changed
        /// </summary>
        public void SelectionChanged(Transaction selectedTransaction)
        {
            //lists
            CurrentTransaction = selectedTransaction;
            CurrentItemTransactions = CurrentTransaction.ItemTransactions;
            CurrentItemTransactionDisplays = ItemTransactionDisplay.GetRange(CurrentItemTransactions);

            //scalar values
            CurrentTransactionID = CurrentTransaction.TransactionID;
            CurrentTotalValue = CurrentTransaction.TotalValue;
            CurrentTimeOfTransaction = CurrentTransaction.TimeOfTransaction;
        }
    }
}
