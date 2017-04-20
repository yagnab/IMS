using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.Repositories;
using IMS.BL.DataModel;

namespace IMS.BL.Validation
{
    public class OldAccountValidation : ValidationBase
    {
        public bool isOldUsernameValid { get; private set; }
        //this is nullable because if username is invalid
        //it cant be known if password is in valid
        public bool? isOldPasswordValid { get; private set; }
        public bool isOldAccountValid { get; private set; }
        public UserAccount account { get; private set; }

        public OldAccountValidation(string username, string password) : base()
        {
            var ouV = new OldUsernameValidation(username);
            isOldUsernameValid = ouV.isOldUsernameValid;

            //if username is invalid
            if(!isOldUsernameValid)
            {
                //unknown if username is valid
                isOldPasswordValid = null;
                ErrorMessage += ouV.ErrorMessage;
            }
            else
            {
                var opV = new OldPasswordValidation(username, password);
                isOldPasswordValid = opV.isOldPasswordValid;

                //cast to bool now as it cant be null at this point
                if(!(bool)isOldPasswordValid)
                {
                    //if password is invalid with correct username
                    ErrorMessage += opV.ErrorMessage;
                    isOldAccountValid = false;
                }
                else
                {
                    //valid details
                    isOldAccountValid = true;
                    using (var uaRepo = new UserAccountRepo(new InventoryContext()))
                    {
                        account = uaRepo.UserAccountByUsername(username).First();
                        uaRepo.Complete();
                    }
                }
            }
        }
        /// <summary>
        /// Must remove account to reuse
        /// The object
        /// </summary>
        public override void Complete()
        {
            ErrorMessage = "";
            account = null;
        }
    }
}
