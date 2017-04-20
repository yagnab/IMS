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

        private void AddNewItemBtn_Click(object sender, RoutedEventArgs e)
        {
            string barcode = BarcodeTB.Text;
            var oldBV = new OldBarcodeValidation(barcode);

            //old barcode is valid
            if (oldBV.isOldBarcodeValid)
            {
                //finding out if quantity is valid
                string quantity = QuantityTB.Text;
                Item item;
                using (var iRepo = new ItemRepository(new InventoryContext()))
                {
                    item = iRepo.ItemByBarcode(barcode).First();
                    iRepo.Complete();
                }
                var qV = new QuantityValidation(quantity, item);

                //quantity is valid
                if (qV.isQuantityValid)
                {
                    ErrorLbl.Content = "It worked!";
                    ErrorLbl.Content += " Implement it now";
                }
                else
                {
                    ErrorLbl.Content = qV.ErrorMessage;
                }

                //allowing reuse
                qV.Complete();
            }
            else
            {
                ErrorLbl.Content = oldBV.ErrorMessage;
            }

            //allowing reuse
            oldBV.Complete();
        }

        /// <summary>
        /// Will prompt the user to add an item
        /// With the barcode they just entered
        /// </summary>
        /// <param name="barcode"></param>
        void AskToCreateNewItem(string barcode)
        {
            MessageBoxResult result = MessageBox.Show("Add a new item with that barcode?", "Add new item", MessageBoxButton.YesNo);

            if(result == MessageBoxResult.Yes)
            {
                
            }
            else
            {
                ClearInputs();
            }
        }
        void ClearInputs()
        {
            BarcodeTB.Text = "";
            QuantityTB.Text = "";
        }
        
        void AskToAddItemQuantity()
        {

        }

        /// <summary>
        /// If errorMessage == "" try adding item to row
        /// and update ItemDisplayDatGrid. Else show error
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="item"></param>
        /// <param name="errorMessage"></param>
        void AddNewTillRow(int? quantity, Item item, string errorMessage)
        {
            //No validation errors occured
            if (errorMessage == "")
            {
                //Casting shouldn't fail as quanity wont be null if passed validation
                //dataContext.AddNewRow(item, (int)quantity);
                ItemDisplayDatGrd.Items.Refresh();

                //Update total at bottom of page
                UpdateTotal();
            }
            else
            {
                ErrorLbl.Content = errorMessage;
            }
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

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
