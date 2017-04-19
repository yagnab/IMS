using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.Validation
{
    public class ValidationBase 
    {
        public string ErrorMessage { get; protected set; }

        public ValidationBase()
        {
        }
        /// <summary>
        /// Call this after one validation operation done
        /// Allow use of one object for multiple validation.
        /// This method can be overridden for more complex child
        /// Classes.
        /// </summary>
        public virtual void Complete()
        {
            ErrorMessage = "";
        }
    }
}
