using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    public class RRPValidation : StringToDecimalValidation
    {
        /// <summary>
        /// This class is empty for now.
        /// It provides a more readable interface for validation
        /// Than StringToDecimalValidation and can be extended
        /// In future if need be.
        /// </summary>
        /// <param name="rrp"></param>
        /// <param name="rrpFieldName"></param>
        public RRPValidation(string rrp, string rrpFieldName = "RRP") : base(rrp, rrpFieldName)
        {
        }
    }
}
