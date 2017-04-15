using System.Windows;
using System.Windows.Controls;
using IMS.BL.Validation;
using IMS.BL;
using IMS.UI.ViewModels;
using System.Data;
using System;
using System.Collections.Generic;
using System.Collections;

namespace IMS.UI
{
    
    /// <summary>
    /// Interaction logic for TillModePage.xaml
    /// </summary>
    public partial class TillModePage : Page
    {
         
        public TillModePage()
        {
            InitializeComponent();
        }
        /*
        private void zeroBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Clicked");
        }

        private void AddNewItemBtn_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";
            string barcode = BarcodeTB.Text;
            Item item = null;

            //If first stage of validation passed
            if (!IsBarcodeEmpty(barcode, errorMessage) )
            {
                //If pass second stage of validation
                if(DoesBarcodeExist(barcode, errorMessage, ref item))
                {
                    //Last stage of validation
                    ValidateQuantityInput(item, errorMessage);
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

        bool DoesBarcodeExist(string barcode, string errorMessage, ref Item item)
        {
            BarcodeValidation bv = new BarcodeValidation();
            item = bv.GetItemFromBarcode(barcode);

            //Barcode doesn't exist
            if (item == null)
            {
                errorMessage += bv.ErrorMessage;
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

        }
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
            }

            //Try to add new row
            AddNewTillRow(quantityInput, item, errorMessage);
        }
        
        void AddNewTillRow(int? quantity, Item item, string errorMessage)
        {
            //No validation errors occured
            if(errorMessage == "")
            {
                //Casting shouldn't fail as quanity wont be null if passed validation
                dataContext.AddNewRow(item, (int)quantity);
                ItemDisplayDatGrd.Items.Refresh();

                //Update total at bottom of page
                UpdateTotal();                   
            }
            else
            {
                ErrorLbl.Content = errorMessage;
            }
        }

        void UpdateTotal()
        {
            decimal  total = 0m;

            //Loop through each row
            foreach(TillDataRow row in ItemDisplayDatGrd.ItemsSource)
            {
                //Add total for that row
                total += row.TotalPrice;
            }

            TotalDisplay.Content = "£" + total;
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {

        }*/
    }
}
