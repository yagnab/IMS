using System;

namespace IMS.BL.DataModel
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
