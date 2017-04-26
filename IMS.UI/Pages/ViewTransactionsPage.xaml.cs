using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using IMS.UI.ViewModels;
using IMS.BL.DataModel;
using IMS.BL.Repositories;

namespace IMS.UI
{
    /// <summary>
    /// Interaction logic for ViewTransactionsPage.xaml
    /// </summary>
    public partial class ViewTransactionsPage : Page
    {
        ViewTransactionsPageVM dataContext;

        public ViewTransactionsPage()
        {
            InitializeComponent();

            //setting DataContext
            dataContext = new ViewTransactionsPageVM();
            DataContext = dataContext;

            //initial selection change
            TransactionsDisplay.SelectedIndex = 0;
        }
        
        private void TransactionsDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //update dataContext w/ new transaction

            //datagrid display transactionID = 1 in index = 0; therefor + 1
            int selectedTransactionID = TransactionsDisplay.SelectedIndex + 1;

            Transaction selectedTransaction;
            using (var TRepo = new TransactionRepo(new InventoryContext()))
            {
                selectedTransaction = TRepo.GetByID(selectedTransactionID, "ItemTransactions");
                TRepo.Complete();
            }

            dataContext.SelectionChanged(selectedTransaction);
            UpdateAll();
        }

        void UpdateAll()
        {
            //rerender datagrids
            TransactionsDisplay.UpdateLayout();
            ItemTransactionDataGrid.UpdateLayout();

            //rerender labels
            TransactionIDLbl.UpdateLayout();
            TotalValueLbl.UpdateLayout();
            TimeOfTransaction.UpdateLayout();
        }
    }
}
