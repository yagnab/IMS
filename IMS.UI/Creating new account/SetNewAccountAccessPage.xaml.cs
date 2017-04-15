using IMS.BL.DataModel;
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

namespace IMS.UI
{
    /// <summary>
    /// Interaction logic for SetNewAccountAccessPage.xaml
    /// </summary>
    
    public partial class SetNewAccountAccessPage : Page
    {
        public SetNewAccountAccessPage()
        {
            InitializeComponent();

            //Selects default values for comboboxes
            //Show name of new user at top of page
            LoadDefaultValues();
        }
        
        private void CreateAccBtn_Click(object sender, RoutedEventArgs e)
        {
            using(var dbContext = new InventoryContext())
            {
                TryCreateAccount(dbContext);
            }
        }
        
        void TryCreateAccount(InventoryContext dbContext)
        {
            string username = CreateNewAccountService.Instance.username;
            string password = CreateNewAccountService.Instance.password;
            bool[] cmbBxInputs = GetCmbBxInputs();

            //If the user is sure
            if (GetUserConfirmation())
            {
                //Creating new account
                //If adding new account to db was successful
                if (UIObjectCreator.CommitNewAccountToDB(dbContext, username, password, cmbBxInputs[0], cmbBxInputs[1], cmbBxInputs[2], cmbBxInputs[3]))
                {
                    MessageBox.Show("The new user was added");
                }
                else
                {
                    MessageBox.Show("Something went wrong.\nTry reentering the account's access level.");

                    //Resets inputs so user can retry
                    LoadDefaultValues();
                }
                
            }
        }

        bool[] GetCmbBxInputs()
        {
            Dictionary<int, bool> IndexToBool = new Dictionary<int, bool>();
            IndexToBool.Add(0, true);
            IndexToBool.Add(1, false);

            bool IsAdmin = IndexToBool[IsAdminCmbBx.SelectedIndex];
            bool IsAnalyticsAllowed = IndexToBool[IsAnalyticsAllowedCmbBx.SelectedIndex];
            bool IsEditTablesAllowed = IndexToBool[IsEditTablesAllowedCmbBx.SelectedIndex];
            bool IsAddNewDeliveriesAllowed = IndexToBool[IsAddDeliveriesAllowedCmbBx.SelectedIndex];

            return new bool[]
            {
                IsAdmin,
                IsAnalyticsAllowed,
                IsEditTablesAllowed,
                IsAddNewDeliveriesAllowed
            };
        }

        bool GetUserConfirmation()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure", "Create new account", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void LoadDefaultValues()
        {
            //Combobox defaults
            IsAddDeliveriesAllowedCmbBx.SelectedIndex = 1;
            IsAdminCmbBx.SelectedIndex = 1;
            IsAnalyticsAllowedCmbBx.SelectedIndex = 1;
            IsEditTablesAllowedCmbBx.SelectedIndex = 1;

            //Label at top index
            diplayUsername.Content = string.Format("Select {0}'s access level", CreateNewAccountService.Instance.username);
        }

        private void IsAdminCmbBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //If user pick IsAdminAccountCmbBx = Yes
            if(IsAdminCmbBx.SelectedIndex == 0)
            {
                SetIndexesForAdmin();
                DisableCmbBxEditing();
            }
            else
            {
                AllowCmbBxEditing();
            }
        }

        void SetIndexesForAdmin()
        {
            //Sets all other comboboxes to yes
            IsAnalyticsAllowedCmbBx.SelectedIndex = 0;
            IsAddDeliveriesAllowedCmbBx.SelectedIndex = 0;
            IsEditTablesAllowedCmbBx.SelectedIndex = 0;
        }
        void DisableCmbBxEditing()
        {
            //prevents editing of combobox choices
            IsAnalyticsAllowedCmbBx.IsHitTestVisible = false;
            IsAnalyticsAllowedCmbBx.Focusable = false;

            IsAddDeliveriesAllowedCmbBx.IsHitTestVisible = false;
            IsAddDeliveriesAllowedCmbBx.Focusable = false;

            IsEditTablesAllowedCmbBx.IsHitTestVisible = false;
            IsEditTablesAllowedCmbBx.Focusable = false;
        }
        void AllowCmbBxEditing()
        {
            //Allowes editing of comoboxes
            IsAnalyticsAllowedCmbBx.IsHitTestVisible = true;
            IsAnalyticsAllowedCmbBx.Focusable = true;

            IsAddDeliveriesAllowedCmbBx.IsHitTestVisible = true;
            IsAddDeliveriesAllowedCmbBx.Focusable = true;

            IsEditTablesAllowedCmbBx.IsHitTestVisible = true;
            IsEditTablesAllowedCmbBx.Focusable = true;
        }
    }
}
