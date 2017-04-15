using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IMS.BL.DataModel;
using IMS.UI.ViewModels;

namespace IMS.UI
{
    /// <summary>
    /// Interaction logic for ViewTransactionsPage.xaml
    /// </summary>
    public partial class ViewTransactionsPage : Page
    {
        ViewTransactionsPageVM _dataContext;

        public ViewTransactionsPage()
        {
            InitializeComponent();
            //custom load event
            ViewTransactionsPage_Loaded();

        }
        
        private void TransactionsDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //update _dataContext
            //then update view on datagrid
            var selectedTransID = (this.TransactionsDisplay.SelectedItem as Transaction).TransactionID;

            using (var dbContext = new InventoryContext())
            {
                //cast selected item to transaction, then gets its id
                _dataContext.Transaction = dbContext.Transactions
                    .Include("ItemTransactions")
                    .Where(t => t.TransactionID == selectedTransID)
                    .First();
                _dataContext.ItemTransactions = _dataContext.Transaction.ItemTransactions;

                //Updates the datagrid view
                this.ItemTransactionDataGrid.UpdateLayout();

            }

        }

        /// <summary>
        /// The custom defined
        /// loaded event for this page
        /// </summary>
        private void ViewTransactionsPage_Loaded()
        {
            _dataContext = new ViewTransactionsPageVM();
            using (var dbContext = new InventoryContext())
            {
                //sets datacontext to first transaction from table
                _dataContext.Transactions = dbContext.Transactions.Include("ItemTransactions").ToList();
                if(_dataContext.Transaction != null)
                {
                    _dataContext.Transaction = _dataContext.Transactions.First();
                    _dataContext.ItemTransactions = _dataContext.Transaction.ItemTransactions.ToList();

                    this.DataContext = _dataContext;

                    this.TransactionsDisplay.SelectedIndex = 0;
                }
            }
        }
        
        void SetNewDataContext()
        {
            var selectedTransID = (this.TransactionsDisplay.SelectedItem as Transaction).TransactionID;
            
            using (var dbContext = new InventoryContext())
            {
                //cast selected item to transaction, then gets its id
                _dataContext.Transaction = dbContext.Transactions
                    .Include("ItemTransactions")
                    .Where(t => t.TransactionID == selectedTransID)
                    .First();
                _dataContext.ItemTransactions = _dataContext.Transaction.ItemTransactions;

                //Updates the datagrid view
                this.ItemTransactionDataGrid.UpdateLayout();
                
            }
            

        }
    }
}
