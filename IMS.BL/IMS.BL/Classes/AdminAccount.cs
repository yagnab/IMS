using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL
{
    public class AdminAccount : UserAccount
    {
        public readonly bool IsAddDeliveryAllowed = true;
        public readonly bool IsEditTablesAllowed = true;
        public readonly bool IsAnalyticsAllowed = true;
        /// <summary>
        /// Will return a new AdminAccount object
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static AdminAccount CreateNewAdminAccount(string username, string password)
        {
            var admin = new AdminAccount()
            {
                Username = username,
                Password = password,
                IsAdmin = true
            };
            return admin;
        }
    }
}
