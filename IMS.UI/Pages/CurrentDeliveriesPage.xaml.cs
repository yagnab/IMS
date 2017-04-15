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
using IMS.BL;
using IMS.BL.DataModel;

namespace IMS.UI
{
    /// <summary>
    /// Interaction logic for CurrentDeliveriesPage.xaml
    /// </summary>
    public partial class CurrentDeliveriesPage : Page
    {
        ViewCurrentDeliveriesPageVM _dataContext;
        public CurrentDeliveriesPage()
        {
            InitializeComponent();

            //Setting DataContext
            _dataContext = new ViewCurrentDeliveriesPageVM();
            DataContext = _dataContext;

            //Triggering SelectionChangedEvent
            ViewCurrDelLstBx.SelectedIndex = 0;

            //TODO Fix CurrentDeliveriesPage issues
        }

        private void AddDelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewCurrDelLstBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Casting to delivery instead of CurrentDelivery so its quicker
            int selectedDeliveryID = (ViewCurrDelLstBx.SelectedItem as Delivery).DeliveryID;
            using(var dbContext = new InventoryContext())
            {
                _dataContext.ChangeProperties(selectedDeliveryID, dbContext);
            }
        }
    }
}
