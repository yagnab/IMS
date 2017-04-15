using System;

namespace IMS.BL
{
    public class PastDelivery : Delivery
    {
        public DateTime ActualArrivalDate { get; set; }

        public override string ToString()
        {
            return "Past Delivery";
        }
    }

    
}
