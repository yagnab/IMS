using IMS.BL.Validation;
using IMS.BL.Repositories;
using IMS.BL.DataModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System;

namespace IMS.UI
{
    /// <summary>
    /// Interaction logic for AddDeliveryPage.xaml
    /// </summary>
    public partial class AddDeliveryPage : Page
    {
        public AddDeliveryPage()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string barcode = barcodeTxt.Text;
            string quantity = addQuantityTxt.Text;
            var iqV = new IncreaseQuantityValidation(barcode, quantity);

            //request is valid
            if(iqV.isRequestValid)
            {
                try
                {
                    Item editItem;
                    //adding to item.QuantityStockLevel
                    using (var iRepo = new ItemRepository(new InventoryContext()))
                    {
                        editItem = iRepo.ItemByBarcode(barcode).First();
                        editItem.QuantityStockLevel += iqV.intQuantity;
                        iRepo.Complete();
                    }

                    //it worked
                    string message = string.Format("Quantity of {0} added to '{1}' (Barcode: {2})", quantity, editItem.Description, barcode);
                    MessageBox.Show(message);
                }
                catch(Exception)
                {
                    //operation to database didnt work
                    MessageBox.Show("An error occured. Try again");
                }
            }
            else
            {
                errorLbl.Content = iqV.ErrorMessage;
            }
            
            //Resets textboxes to state before user clicked anything
            SetToDefault();
        }

        private void barcodeTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = (TextBox)sender;
            tb.Text = "";
            tb.GotFocus -= barcodeTxt_GotFocus;
        }

        private void addQuantityTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = (TextBox)sender;
            tb.Text = "";
            tb.GotFocus -= barcodeTxt_GotFocus;
        }

        void SetToDefault()
        {
            barcodeTxt.Text = "Enter barcode";
            addQuantityTxt.Text = "Quantity to add";

            barcodeTxt.GotFocus += barcodeTxt_GotFocus;
            addQuantityTxt.GotFocus += addQuantityTxt_GotFocus;
        }
    }
}
