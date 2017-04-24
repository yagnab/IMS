using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    /// <summary>
    /// Just a wrapper class for StringToIntergerValidation
    /// With more helpful names for class and variables
    /// </summary>
    public class NewQuantityValidation : StringToIntegerValidation
    {
        public bool isNewQuantityValid { get; private set; }
        public NewQuantityValidation(string integer) : base(integer, "Quantity")
        {
            isNewQuantityValid = isStringIntegerValid;
        }
    }
}
