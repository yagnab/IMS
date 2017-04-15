using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UI
{
    public class CreateNewAccountService
    {
        public static CreateNewAccountService Instance { get; private set; }
        public CreateNewAccountWindow Window { get; private set; }
        public string username { get; set; }
        public string password { get; set; }
        private CreateNewAccountService(CreateNewAccountWindow _Window)
        {
            Window = UICreatePage.GetNewAccountWindow();
        }

        public static CreateNewAccountService CreateInstance()
        {
            if(Instance == null)
            {
                Instance = new CreateNewAccountService(new CreateNewAccountWindow());
            }
            return Instance;
        }

        public static void NullCurrentWindow()
        {
            if(Instance != null)
            {
                Instance = null;
            }
        }
    }
}
