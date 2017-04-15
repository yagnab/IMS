using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.DataModel
{
    public class PastReservation : Reservation
    {
        public DateTime ActualPickUpDate { get; set; }

        public override string ToString()
        {
            return "Past Reservation";
        }
    }
}
