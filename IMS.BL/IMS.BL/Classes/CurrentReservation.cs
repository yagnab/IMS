using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL
{
    public class CurrentReservation : Reservation
    {
        public DateTime ExpectedPickUpDate { get; set; }
    }
}
