using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.Repositories;

namespace IMS.BL.Validation.Classes
{
    public class NewUsernameValidation : StringValidation
    {
        public bool doesUsernameExist { get; private set; }
        public NewUsernameValidation(string username, string usernameFieldName = "Username") : base(username, usernameFieldName)
        {

        }

        bool DoesUsernameExist(string username)
        {
            
        }
    }
}
