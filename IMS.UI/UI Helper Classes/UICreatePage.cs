
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using IMS.BL.DataModel;

namespace IMS.UI
{
    public static class UICreatePage
    {
        #region Login Utility
        //will return a Staff or Useracccount
        public static dynamic GetUserFromUsernamePassword(string username, string password)
        {
            using (var dbContext = new InventoryContext())
            {
                //if username valid
                //return the object 
                //else return null
                try
                {
                    UserAccount dbReturnedValue = (from usr in dbContext.UserAccounts
                                          where usr.Username == username
                                          select usr).First();
                    return GetUserWithTypeFromPassword(dbReturnedValue, password);
                }
                catch (System.InvalidOperationException)
                {
                    return null;
                }
                
            }
        }

        public static dynamic GetUserWithTypeFromPassword(UserAccount userAccount, string password)
        {
            if (userAccount.Password == password)
            {
                using (var dbContext = new InventoryContext())
                {
                    if (userAccount.IsAdmin)
                    {
                        
                        
                        AdminAccount user = (from admin in dbContext.UserAccounts.OfType<AdminAccount>()
                                                where admin.Username == userAccount.Username
                                                select admin).First();
                        return user;
                        

                    }
                    else
                    {
                        StaffAccount user = (from staff in dbContext.UserAccounts.OfType<StaffAccount>()
                                             where staff.Username == userAccount.Username
                                             select staff).First();

                        return user;
                    }
                }
                    
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region UIWindow utility
        public static UIWindow GetUIWindow(dynamic user)
        {
            
            UIWindow pageHolder = new UIWindow();

            var menuPage = UICreatePage.GetUserMenu(user);
            pageHolder.pageHolder.Content = menuPage;

            return pageHolder;
        }
        #endregion

        #region UserMenu utility
        //gets getsermenu but removes buttons depending on permission
        public static UserMenu GetUserMenu(dynamic user)
        {
            
            if (!user.IsAdmin)
            {
                var ui = new UserMenu();

                Dictionary<Button, bool> buttonToIsEnabled = new Dictionary<Button, bool>();
                buttonToIsEnabled.Add(ui.viewTransactionsBtn, user.IsAnalyticsAllowed);
                buttonToIsEnabled.Add(ui.editTblsBtn, user.IsEditTablesAllowed);
                buttonToIsEnabled.Add(ui.analyticsBtn, user.IsAnalyticsAllowed);

                foreach(KeyValuePair<Button, bool> row in buttonToIsEnabled)
                {
                    //if IsEnabled is false, hide button
                    if(!row.Value)
                    {
                        row.Key.Visibility = Visibility.Hidden;
                    }
                }

                return ui;
            }
            else
            {
                //user is admin, so allow all features
                return new UserMenu();
            }
        }
        #endregion

        #region ViewTables utility
        //gets viewtblpage but implicity removes options from combobox
        //depending on permission levels
        public static ViewTblPage GetViewTblPage()
        {
            dynamic user = LoginService.Instance.currentUser;

            //datasource for tables combobox
            List<object> tblComboBoxObjs = ViewTblGetComboBoxObjects(user);

            ViewTblPage ui = new ViewTblPage(tblComboBoxObjs);
            return ui;
           
        }

        public static List<object> ViewTblGetComboBoxObjects(dynamic user)
        {
            var Transaction = new Transaction();
            var PastReservation = new PastReservation();
            var CurrentDelivery = new CurrentDelivery();
            var PastDelivery = new PastDelivery();
            var Item = new Item();
            var Supplier = new Supplier();

            var datasource = new List<object>
            {
                Transaction,
                PastReservation,
                CurrentDelivery,
                PastDelivery,
                Item,
                Supplier
            };
            
            //if user isn't an admin and isnt allowed to see analytics
            //then remove first item from datasouce (transactions)
            if (!(user.IsAnalyticsAllowed))
            {
                //if the user isnt an admi
                datasource.RemoveAt(0);
            }

            return datasource;
        }
        #endregion

        #region ViewTransactionPage Utility

        //this is a very small method
        //however it reduces complexity by
        //leaving all constructions of new pages
        //in one place
        public static ViewTransactionsPage GetViewTransactionsPage()
        {
            return new ViewTransactionsPage();
        }

        #endregion

        #region TillModePage utility
        public static TillModePage GetTillModePage()
        {
            return new TillModePage();
        }

        #endregion

        #region AnalyticsPage utility
        public static AnalyticsPage GetAnalyticsPage()
        {
            AnalyticsPage ui = new AnalyticsPage();
            return ui;
        }
        #endregion

        #region Items page utility

        public static ItemsPage GetItemsPage()
        {
            return new ItemsPage();
        }

        #endregion

        #region CurrentDelivery page utility

        public static ViewCurrentDeliveriesPage GetCurrentDeliveryPage()
        {
            var ui = new ViewCurrentDeliveriesPage();
            return ui;
        }

        #endregion

        #region PastDeliveries page utility

        public static ViewPastDeliveriesPage GetNewPastDeliveriesPage()
        {
            var ui = new ViewPastDeliveriesPage();

            //If user isn't admin and doesnt have permission to add deliveries
            if(!LoginService.Instance.currentUser.IsAdmin && !LoginService.Instance.currentUser.IsAddDeliveryAllowed)
            {
                ui.AddNewDelBtn.Visibility = Visibility.Hidden;
            }

            return ui;
        }
        #endregion

        #region CreateNewAccount utility
        public static AdminDetailsPage GetAdminDetailsPage()
        {
            return new AdminDetailsPage();
        }

        public static AddUserDetailsPage GetAddUserDetailsPage()
        {
            return new AddUserDetailsPage();
        }

        public static CreateNewAccountWindow GetNewAccountWindow()
        {
            var ui = new CreateNewAccountWindow();
            ui.ContentFrame.Content = GetAddUserDetailsPage();
            return ui;
        }

        public static SetNewAccountAccessPage CreateNewSetAccessPage()
        {
            return new SetNewAccountAccessPage();
        }
        #endregion
        public static LowStockPage GetNewLowStockPage()
        {
            return new LowStockPage();
        }
    }
}
