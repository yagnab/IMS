using IMS.BL.DataModel;
using IMS.BL.Repositories;
using IMS.BL.Validation;
using IMS.UI.ViewModels;
using System;
using System.Collections;
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

namespace IMS.UI.Views
{
    /// <summary>
    /// Interaction logic for TillModeView.xaml
    /// </summary>
    public partial class TillModeView : UserControl
    {
        TillModeViewVM dataContext;
        public TillModeView()
        {
            InitializeComponent();
            
            //Setting DataContext
            dataContext = new TillModeViewVM();
            DataContext = dataContext;
        }

        /// <summary>
        /// This input will
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewItemBtn_Click(object sender, RoutedEventArgs e)
        {
            //clear error from previous entry
            ErrorLbl.Content = "";

            string barcode = BarcodeTB.Text;
            string quantity = QuantityTB.Text;
            var tdrV = new TillDataRowValidation(barcode, quantity);

            //till row is valid
            if(tdrV.isTillDataRowValid)
            {
                //if description for item doesnt exist in datagrid
                if(!DoesDescriptionExist(tdrV.item.Description))
                {
                    //adding new TillDataRow
                    var newTDR = new TillDataRow(tdrV.item, tdrV.quantity);
                    AddRow(newTDR);

                    //update datagrid on screen
                    ItemDisplayDatGrd.UpdateLayout();
                }
                else
                {
                    ErrorLbl.Content = "That item already exists. Delete it and reenter.\n";
                }
            }
            else
            {
                ErrorLbl.Content = tdrV.ErrorMessage;
            }
           
            //allow for reuse
            tdrV.Complete();
            ClearInputs();
        }

        /// <summary>
        /// It will add the new row
        /// To the itemSource then update
        /// The datagrid
        /// </summary>
        /// <param name="barcode"></param>
        void AddRow(TillDataRow tdrAdd)
        {
            dataContext.Rows.Add(tdrAdd);
            ItemDisplayDatGrd.UpdateLayout();
        }
        void ClearInputs()
        {
            BarcodeTB.Text = "";
            QuantityTB.Text = "";
        }
        /// <summary>
        /// This will clear inputs
        /// The datagrid and errorLbl
        /// </summary>
        void ClearAll()
        {
            ClearInputs();
            ErrorLbl.Content = "";
            dataContext.Rows.Clear();
            ItemDisplayDatGrd.UpdateLayout();
        }
        /// <summary>
        /// Loop through all rows' quantity column
        /// and add to total. The total is refreshed
        /// afterwards.
        /// </summary>
        void UpdateTotal()
        {
            decimal total = 0m;

            //Loop through each row
            foreach (TillDataRow row in ItemDisplayDatGrd.ItemsSource)
            {
                //Add total for that row
                total += row.TotalPrice;
            }

            TotalDisplay.Content = "£" + total;
        }
        bool DoesDescriptionExist(string description)
        {
            foreach(TillDataRow tdr in ItemDisplayDatGrd.Items)
            {
                if(tdr.Description == description)
                {
                    return true;
                }
            }
            return false;
        }
        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //dictionary to map an item to its quantity in this transaction
                Dictionary<Item, int> itemToQuantity = new Dictionary<Item, int>();
                foreach (TillDataRow tdr in ItemDisplayDatGrd.Items)
                {
                    itemToQuantity.Add(tdr.Item, tdr.Quantity);
                }

                using (var tRepo = new TransactionRepo(new InventoryContext()))
                {
                    tRepo.AddNewTransaction(itemToQuantity);
                    tRepo.Complete();
                }

                //it worked
                MessageBox.Show("Transaction added");
                ClearAll();
            }
            catch(Exception)
            {
                MessageBox.Show("An error occured. Try again");
            }
        }

        //NOTE: User can delete item by clicking the button in the row
        
        /// <summary>
        /// Taking user to the items page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookupBtn_Click(object sender, RoutedEventArgs e)
        {
            var ui = new ItemsPage();
            LoginService.Instance.currentUIWindow.pageHolder.Content = ui;
        }
    }
}
