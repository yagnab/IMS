using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.UI.ViewModels;

namespace IMS.UI
{

    //lazy loading singleton pattern
    //allow for global values but with lazy loading
    //as initalised after user logs into their session  
    public class LoginService
    {
        public static LoginService  Instance { get; set; }
        //dynamic as it can be AdminAccount or StaffAccount
        //these types enable full functionality e.g. IsEnabled properties
        public dynamic currentUser { get; set; }
        public UIWindow currentUIWindow { get; set; }
        public CreateNewAccountWindow currentNewAccountWindow { get; set; }
        private LoginService(dynamic _currentUser)
        {
            this.currentUser = _currentUser;
            this.currentUIWindow = UICreatePage.GetUIWindow(this.currentUser);
            
        }

        public static LoginService getFirstInstance(dynamic _user)
        {
            if(Instance == null)
            {
                Instance = new LoginService(_user);
            }
            return Instance;
        }
        
        public static void NullCurrentInstance()
        {
            Instance = null;
        }
    }
}
