using IMS.BL;
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
            string errorMessage = "";
            string barcode = BarcodeTB.Text;
            Item item = null;

            //First stage validation
            if(!IsBarcodeEmpty(barcode, errorMessage))
            {
                if ( DoesBarcodeExist(barcode, ref errorMessage, ref item) )
                {

                }
            }

        }

        bool IsBarcodeEmpty(string barcode, string errorMessage)
        {
            //Validate barcode
            errorMessage += StringValidation.IsStringNullOrWhiteSpace(barcode);

            if (errorMessage != "")
            {
                ErrorLbl.Content = errorMessage;
                return true;
            }
            else
            {
                return false;
            }
        }

        bool DoesBarcodeExist(string barcode, ref string errorMessage, ref Item item)
        {
            BarcodeValidation bv = new BarcodeValidation();
            item = bv.GetItemFromBarcode(barcode);

            //Barcode doesn't exist
            if (item == null)
            {
                errorMessage += bv.ErrorMessage;
                ErrorLbl.Content = errorMessage;
                AskToCreateNewItem(barcode);
                return false;
            }
            return true;
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
        /// <summary>
        /// Try to figure out total quantity needed
        /// including items in till. If more needed than in stock
        /// prompt to add new items. Else, try to create new row
        /// </summary>
        /// <param name="item"></param>
        /// <param name="errorMessage"></param>
        void ValidateQuantityInput(Item item, string errorMessage)
        {
            //Validate quantity
            QuantityValidation qv = new QuantityValidation();

            IEnumerable rows = ItemDisplayDatGrd.ItemsSource;

            int? quantityInput = qv.ValidateQuantity(QuantityTB.Text, item, rows);

            //Validation failed
            if (quantityInput == null)
            {
                errorMessage += qv.ErrorMessage;
                //AskToAddItemQuantity();
            }

            //Try to add new row
            AddNewTillRow(quantityInput, item, errorMessage);
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
