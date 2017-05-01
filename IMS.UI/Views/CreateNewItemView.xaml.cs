﻿using IMS.BL.DataModel;
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
            barcodeTxt.Text = "";
        }
        void SetToDefault()
        {
            //Set textboxes to default
            BarcodeTxt.Text = "Enter a barcode";
            DescTxt.Text = "Enter description";
            RRPTxt.Text = "Enter RRP";
            StartStockLvlTxt.Text = "Enter the starting quantity";

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
            
            //Vars for validation
            string barcode = BarcodeTxt.Text;
            string description = DescTxt.Text;
            string rrp = RRPTxt.Text;
            string stockLevel = StartStockLvlTxt.Text;
            int catagoryIndex = CatagoryCmbBx.SelectedIndex;
            var niV = new NewItemValidation(barcode, description, rrp, stockLevel, catagoryIndex);

            //passed validation
            if(niV.isNewItemValid)
            {
                MessageBox.Show("Worked");
            }
            else
            {
                ErrorLbl.Content = niV.ErrorMessage;
            }
        }
        /// <summary>
        /// Carries out validation for RRPTxt
        /// If a value is actually entered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RRPTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            string rrp = RRPTxt.Text;

            try
            {
                decimal decRRP = Math.Round(Convert.ToDecimal(rrp), 2);

                //it worked
                RRPTxt.Text = decRRP.ToString();
            }
            catch(Exception)
            {
                ErrorLbl.Content = "RRP must be a decimal";

                //resetting to default
                RRPTxt.Text = "Enter RRP";
                RRPTxt.GotFocus += RRPTxt_GotFocus;
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

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            SetToDefault();
        }
        /*
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
}*/
        /*
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
        }*/
    }
}
