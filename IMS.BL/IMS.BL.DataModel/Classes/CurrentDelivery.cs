using System;

namespace IMS.BL.DataModel
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
