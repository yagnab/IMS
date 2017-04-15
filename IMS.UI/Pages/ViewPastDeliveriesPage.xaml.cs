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
    /// Interaction logic for ViewPastDeliveriesPage.xaml
    /// </summary>
    public partial class ViewPastDeliveriesPage : Page
    {
        ViewPastDeliveriesVM _dataContext;
        public ViewPastDeliveriesPage()
        {
            InitializeComponent();

            //Setting DataContext
            _dataContext = new ViewPastDeliveriesVM();
            DataContext = _dataContext;

            string message = "";
            foreach(PastDelivery pd in _dataContext.AllPastDeliveries)
            {
                message += pd.DeliveryID; 
            }

            MessageBox.Show(message);
            //Triggers selection changed event
            ViewPastDelLstBx.SelectedIndex = 0;
        }

        private void AddNewDelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewPastDelLstBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Updating _dataContext depending on the PastDelivery selected by user
            int selectedDeliveryID = (ViewPastDelLstBx.SelectedItem as PastDelivery).DeliveryID;
            using(var dbContext = new InventoryContext())
            {
                _dataContext.ChangeProperties(selectedDeliveryID, dbContext);
            }
            UpdateView();
        }

        void UpdateView()
        {
            //Updates items after _dataContext has changed
            PastDelItems.UpdateLayout();
        }
    }
}
