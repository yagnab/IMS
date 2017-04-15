using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL
{
    public abstract class Reservation
    {
        public int ReservationID { get; set; }
        public ICollection<ItemReservation> ItemReservations { get; set; }
        public bool IsPickedUp { get; set; }

        //read only version of all
        //associated item ids
        public IEnumerable<int> ReservedItemIDs
        {
            get
            {
                return this.ItemReservations.Select(i_r => i_r.ItemID);
            }
        }
    }
}
