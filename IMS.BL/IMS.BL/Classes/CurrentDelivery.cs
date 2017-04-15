using System;

namespace IMS.BL
{
    public class CurrentDelivery : Delivery
    {
        public DateTime ExpectedArrivalDate { get; set; }

        public override string ToString()
        {
            return "Current Delivery";
        }
    }
}
