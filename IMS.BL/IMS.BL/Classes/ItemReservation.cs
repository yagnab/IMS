using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.BL
{
    public class ItemReservation
    {
        [Key, Column(Order = 0)]
        public int ItemID { get; set; }
        [Key, Column(Order = 1)]
        public int ReservationID { get; set; }

        public virtual Item Item { get; set; }
        public virtual Reservation Reservation { get; set; }

        public int Quantity { get; set; }
    }
}
