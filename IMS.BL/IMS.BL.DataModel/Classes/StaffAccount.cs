using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IMS.BL.DataModel
{
    public class StaffAccount : UserAccount
    {
        public bool IsAddDeliveryAllowed { get; set; }
        public bool IsEditTablesAllowed { get; set; }
        public bool IsAnalyticsAllowed { get; set; }
        /// <summary>
        /// Will return a new StaffAccount object
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public static StaffAccount GetNewStaffAccount(string username, string password, bool isAnalyticsAllowed, bool isEditTablesAllowed, bool isAddDeliveryAllowed)
        {
            var staff = new StaffAccount()
            {
                Username = username,
                Password = password,
                IsAdmin = false,
                IsAnalyticsAllowed = isAnalyticsAllowed,
                IsEditTablesAllowed = isEditTablesAllowed,
                IsAddDeliveryAllowed = isAddDeliveryAllowed
            };
            return staff;
        }
    }
}
