using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using IMS.BL;
using IMS.BL.DataModel;
using MahApps.Metro.Controls;

namespace IMS.UI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        
        //when user click on usernameTxt, it removes text
        private void usernameTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            var usernameTxt = (TextBox)sender;
            //make text blank
            //then remove this event
            usernameTxt.Text = "";
            usernameTxt.GotFocus -= usernameTxt_GotFocus;
        }
        
        //when user click on passwordTxt remove the text
        private void passwordTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            var passwordTxt = (TextBox)sender;
            
            //remove the textbox, and focus on password box behind
            passwordTxt.Visibility = Visibility.Hidden;
            passwordBox.Focus();
        }

        //when user pressed login
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            TryLogin();
        }
        
        void DefaultInputFields()
        {
            usernameTxt.Text = "";
            passwordBox.Password = "";

            usernameTxt.Text = "Username";
            passwordTxt.Visibility = Visibility.Visible;
            
            usernameTxt.GotFocus += usernameTxt_GotFocus;
        }
        private void newAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            //Create instance of singleton CreateNewAccountService
            //Or does nothing if window already exists
            CreateNewAccountService.CreateInstance();
            
            //Display window it stores
            //Diplay window currently showing if a window is already stored
            CreateNewAccountService.Instance.Window.Show();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(CreateNewAccountService.Instance != null)
            {
                CreateNewAccountService.Instance.Window.Close();
            }
        }

        void TryLogin()
        {
            string username = usernameTxt.Text;
            //string hash of plaintext password
            string password = UserAccount.stringToHashString(passwordBox.Password);

            var user = UICreatePage.GetUserFromUsernamePassword(username, password);
            if (user == null)
            {
                this.errorBlock.Text = "That username or password is invalid";
                DefaultInputFields();
            }
            else
            {
                //stores this current user
                //store ui window, with user menu page
                LoginService.getFirstInstance(user);
                UIWindow ui = LoginService.Instance.currentUIWindow;
                ui.Show();

                //close all windows
                this.Close();
                if (CreateNewAccountService.Instance != null)
                {
                    CreateNewAccountService.Instance.Window.Close();
                }
            }
        }
    }
}