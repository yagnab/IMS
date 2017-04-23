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

        }

        /// <summary>
        /// User double clicked a datagrid Row.
        /// Therefor, delete the row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemDisplayDatGrd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int indexDelete = ItemDisplayDatGrd.SelectedIndex;
            MessageBox.Show(indexDelete.ToString());
        }

        private void ItemDisplayDatGrd_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataGridRow tdr = sender as DataGridRow;
            MessageBox.Show(tdr.GetIndex().ToString());
        }
    }
}
