using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.DataModel;
using IMS.BL.Repositories;

namespace IMS.BL.Validation
{
    public class AdminValidation : OldAccountValidation
    {
        public bool isAccountAdmin { get; private set; }
        public AdminAccount adminAccount { get; private set; }
        /// <summary>
        /// Arguement "password" should be hashed.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public AdminValidation(string username, string password) : base(username, password)
        {
            //account exists
            if(isOldAccountValid)
            {
                //setting properties
                //existing account is admin
                if(account.IsAdmin)
                {
                    isAccountAdmin = true;
                }
                else
                {
                    isAccountAdmin = false;
                    adminAccount = null;
                }

                //building error message
                if(!isAccountAdmin)
                {
                    ErrorMessage += "Those details don't belong to an admin.\n";
                }
            }
            else
            {
                isAccountAdmin = false;
            }
        }
    }
}
