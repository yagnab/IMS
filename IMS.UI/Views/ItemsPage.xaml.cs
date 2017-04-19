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
using IMS.UI.ViewModels;
using IMS.BL.DataModel;

namespace IMS.UI
{
    /// <summary>
    /// Interaction logic for ItemsPage.xaml
    /// </summary>
    public partial class ItemsPage : Page
    {
        ItemsPageVM _dataContext; 
        public ItemsPage()
        {
            InitializeComponent();

            //sets DataContext
            _dataContext = new ItemsPageVM();
            DataContext = _dataContext;
            ItemsLstBx.SelectedIndex = 0;
        }

        private void ItemsLstBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedItemID = (ItemsLstBx.SelectedItem as Item).ItemID;
            _dataContext.SetNewProperties(selectedItemID);
            RefreshControls();
        }

        void RefreshControls()
        {
            ItemArrivingDatGrd.UpdateLayout();
            ItemLeavingDatGrd.UpdateLayout();
        }
    }
}
