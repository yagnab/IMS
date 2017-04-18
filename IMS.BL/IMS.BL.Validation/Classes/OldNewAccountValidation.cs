using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    /*
    public class NewAccountValidation
    {
        /// <summary>
        /// Will validate if string values can converted to bool
        /// If so, return new StaffAccount or AdminAccount
        /// Passwords must be hashed
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="IsAdmin"></param>
        /// <param name="IsAnalyticsAllowed"></param>
        /// <param name="IsEditTablesAllowed"></param>
        /// <param name="IsAddNewDeliveryAllowed"></param>
        /// <returns></returns>
        public static UserAccount ValidateNewAccount(string username, string password, string IsAdmin, string IsAnalyticsAllowed, string IsEditTablesAllowed, string IsAddNewDeliveriesAllowed)
        {
            string[] inputs = new string[]
            {
                username, 
                password,
                IsAdmin,
                IsAnalyticsAllowed,
                IsEditTablesAllowed,
                IsAddNewDeliveriesAllowed
            };
            
            //If inputs are valid
            if (AreInputsValid(inputs))
            {
                //If account is AdminAccount
                if (Convert.ToBoolean(IsAdmin))
                {
                    return GetNewAdminAccount(inputs);
                }
                else
                {
                    return GetNewStaffAccount(inputs);
                }
            }
            else
            {
                //Incorrect inputs
                return null;
            }
        }
        #region Create new account
        
        public static UserAccount CreateNewAccount(string username, string password, bool IsAdmin, bool IsAnalyticsAllowed, bool IsEditTablesAllowed, bool IsAddNewDeliveriesAllowed)
        {
            string[] inputs = new String[]
            {
                username,
                password,
                IsAdmin,
                IsAnalyticsAllowed,
                IsEditTablesAllowed,
                IsAddNewDeliveriesAllowed
            };

            if (IsAdmin)
            {
                GetNewAdminAccount(inputs)
            }
        }
        public static StaffAccount GetNewStaffAccount(string[] inputs)
        {
            var newStaff = new StaffAccount()
            {
                Username = inputs[0],
                Password = inputs[1],
                IsAdmin = Convert.ToBoolean(inputs[2]),
                IsAnalyticsAllowed = Convert.ToBoolean(inputs[3]),
                IsEditTablesAllowed = Convert.ToBoolean(inputs[4]),
                IsAddDeliveryAllowed = Convert.ToBoolean(inputs[5])
            };
            return newStaff;
        }
        public static AdminAccount GetNewAdminAccount(string[] inputs)
        {
            var newAdmin = new AdminAccount()
            {
                Username = inputs[0],
                Password = inputs[1],
                IsAdmin = Convert.ToBoolean(inputs[2]),
                IsAnalyticsAllowed = Convert.ToBoolean(inputs[3]),
                IsEditTablesAllowed = Convert.ToBoolean(inputs[4]),
                IsAddDeliveryAllowed = Convert.ToBoolean(inputs[5])
            };
            return newAdmin;
        }
        #endregion

        #region Validate inputs
        static bool AreInputsValid(string[] inputs)
        {
            //Will loop through last 4 inputs
            //Check if they can be converted to bool
            for(int i = 2; i < 6; i++) 
            {
                //If the input is invalid
                if(IsInputValid(inputs[i]) == null || StringValidation.IsStringNullOrWhiteSpace(inputs[i]))
                {
                    return false;
                }
            }
            return true;
        }

        static bool? IsInputValid(string input)
        {
            try
            {
                bool boolVal = Convert.ToBoolean(input);

                //If conversion worked
                return boolVal;
            }
            catch
            {
                //If conversion didn't work
                return null;
            }
            

        }
        #endregion
    }*/
}
