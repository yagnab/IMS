using IMS.BL;
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
using IMS.BL.Validation;

namespace IMS.UI.Views
{
    /// <summary>
    /// Interaction logic for CreateNewView.xaml
    /// </summary>
    public partial class CreateNewItemView : UserControl
    {
        public CreateNewItemView()
        {
            InitializeComponent();

            //Set ItemsSource for CatagoryCmbBx
            ItemCatagoryDisplay enums = new ItemCatagoryDisplay();
            CatagoryCmbBx.ItemsSource = enums.Catagories;
        }
        void BlankText(object sender)
        {
            var barcodeTxt = (TextBox)sender;
            //make text blank
            //then remove this event
            barcodeTxt.Text = "";
        }
        void SetToDefault()
        {
            //Set textboxes to default
            BarcodeTxt.Text = "Enter a barcode";
            DescTxt.Text = "Enter description";
            BarcodeTxt.Text = "Enter RRP";
            BarcodeTxt.Text = "Enter starting quantity level";

            //Default is first catagory
            CatagoryCmbBx.SelectedIndex = 0;

            //Add GotFocus again
            EnableGotFocusEvents();
        }
        void EnableGotFocusEvents()
        {
            //Allows changing of text via GotFocus events
            BarcodeTxt.GotFocus += BarcodeTxt_GotFocus;
            DescTxt.GotFocus += DescTxt_GotFocus;
            RRPTxt.GotFocus += RRPTxt_GotFocus;
            StartStockLvlTxt.GotFocus += StartStockLvlTxt_GotFocus;
        }
        private void BarcodeTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            BlankText(sender);
            BarcodeTxt.GotFocus -= BarcodeTxt_GotFocus;
        }

        private void DescTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            BlankText(sender);
            DescTxt.GotFocus -= DescTxt_GotFocus;
        }

        private void RRPTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            BlankText(sender);
            RRPTxt.GotFocus -= RRPTxt_GotFocus;
        }

        private void StartStockLvlTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            BlankText(sender);
            StartStockLvlTxt.GotFocus -= StartStockLvlTxt_GotFocus;
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";

            //Objects required for validaiton
            var bv = new BarcodeValidation();
            var dv = new DescriptionValidation();
            var qv = new QuantityValidation();
            var icv = new ItemCatagoryValidation();

            //Vars for validation
            string barcode = BarcodeTxt.Text;
            string description = DescTxt.Text;
            string rrp = RRPTxt.Text;
            string stockLevel = StartStockLvlTxt.Text;
            int catagoryIndex = CatagoryCmbBx.SelectedIndex;

            //Carry out validation
            errorMessage = ValidateFields(errorMessage, barcode, description, rrp, stockLevel,
                catagoryIndex, dv, bv, qv, icv);
            ValidationPassOrNot(errorMessage, barcode, rrp, description, qv, icv);

        }
        /// <summary>
        /// Carries out validation for RRPTxt
        /// If a value is actually entered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RRPTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            var rrpV = new RRPValidation();
            string rrp = RRPTxt.Text;

            //If validation failed
            if(!rrpV.ValidateRRPFromString(rrp))
            {
                //Shows error
                ErrorLbl.Content = rrpV.ErrorMessage;

                //Reset RRPTxt
                RRPTxt.Text = "Enter RRP";
                RRPTxt.GotFocus += RRPTxt_GotFocus;
            }
            else
            {
                RRPTxt.Text = rrpV.RRP.ToString();
            }
        }

        void AddItemToDB(string barcode, string description, decimal rrp, int stockLevel, ItemCatagory itemCatagory)
        {
            var item = UIObjectCreator.CreateNewItem(barcode, description, rrp, stockLevel, itemCatagory);

            if(item == null)
            {
                //Item wasn't added, set all input fields to default
                MessageBox.Show("Something went wrong.\nThat item was not added.\nTry again.");
                SetToDefault();   
            }
            else
            {
                MessageBox.Show("Item was succesfully added to the database.");
                SetToDefault();
            }
        }

        string ValidateFields(string errorMessage, string barcode, string description, string rrp, string stockLevel, int catagoryIndex, 
            DescriptionValidation dv, BarcodeValidation bv, QuantityValidation qv, ItemCatagoryValidation icv)
        {
            errorMessage += bv.ValidateNewBarcode(barcode);
            if (!dv.ValidateDescription(description))
            {
                errorMessage += dv.ErrorMessage;
            }
            errorMessage += StringValidation.IsStringNullOrWhiteSpace(rrp);

            if (!qv.ValidateNewQuantity(stockLevel))
            {
                errorMessage += qv.ErrorMessage;
            }

            if (!icv.ValidateItemCatagoryIndex(catagoryIndex))
            {
                errorMessage = icv.ErrorMessage;
            }

            return errorMessage;
        }

        void ValidationPassOrNot(string errorMessage, string barcode, string rrp, string description, QuantityValidation qv, ItemCatagoryValidation icv)
        {
            //Check if all validations passed
            if (errorMessage == "")
            {
                AddItemToDB(barcode, description, Convert.ToDecimal(rrp), qv.Quantity, icv.ItemCatagory);
            }
            else
            {
                ErrorLbl.Content = errorMessage;
            }
        }
    }
}
