using System.Windows.Controls;
using IMS.BL.DataModel;
using IMS.UI.ViewModels;
using System.Windows;

namespace IMS.UI
{
    /// <summary>
    /// Interaction logic for ViewCurrentDeliveriesPage.xaml
    /// </summary>
    public partial class ViewCurrentDeliveriesPage : Page
    {
        ViewCurrentDeliveriesVVM dataContext;
        public ViewCurrentDeliveriesPage()
        {
            InitializeComponent();

            //check if user has permission to add deliveries
            if (!LoginService.Instance.currentUser.IsAddDeliveryAllowed)
            {
                addDeliveryButton.Visibility = Visibility.Hidden;
            }

            //setting DataContext
            dataContext = new ViewCurrentDeliveriesVVM();
            DataContext = dataContext;
        }

        private void CurrentDeliveriesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentDelivery selectedDelivery = (CurrentDeliveriesList.SelectedItem as CurrentDelivery);
            dataContext.SelectionChanged(selectedDelivery);
            RefreshControls();
        }
        /// <summary>
        /// Will force controls to rerender
        /// Thus changes are shown to user
        /// </summary>
        void RefreshControls()
        {
            ItemDeliveriesDatGrid.UpdateLayout();
        }
    }
}
