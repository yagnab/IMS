using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.DataModel;
using IMS.UI;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IMS.UI.ViewModels
{
    public class ViewTransactionsPageVM : INotifyPropertyChanged
    {
        private Transaction _Transaction;
        public Transaction Transaction
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
        private List<Transaction> _Transactions;
        public List<Transaction> Transactions
        {
            get
            {
                return _Transactions;
            }
            set
            {
                _Transactions = value;
                OnPropertyChanged();
            }
        }
        private List<ItemTransaction> _ItemTransactions;
        public List<ItemTransaction> ItemTransactions
        {
            get
            {
                return _ItemTransactions;
            }
            set
            {
                _ItemTransactions = value;
                OnPropertyChanged();
            }
        }

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

    }
}
