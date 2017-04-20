using IMS.BL.DataModel;
using System.Windows.Controls;
using IMS.UI.ViewModels;
using System.Windows;

namespace IMS.UI.Views
{
    /// <summary>
    /// Interaction logic for ViewCurrentDeliveriesView.xaml
    /// </summary>
    public partial class ViewCurrentDeliveriesView : UserControl
    {
        ViewCurrentDeliveriesVVM dataContext;
        public ViewCurrentDeliveriesView()
        {
            InitializeComponent();

            //check if user has permission to add deliveries
            if(!LoginService.Instance.currentUser.IsAddDeliveryAllowed)
            {
                addDeliveryButton.Visibility = Visibility.Hidden;
            }

            //setting DataContext
            dataContext = new ViewCurrentDeliveriesVVM();
            DataContext = dataContext;

            MessageBox.Show(CurrentDeliveriesList.ItemsSource.ToString());
            CurrentDeliveriesList.SelectedIndex = 0;
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
