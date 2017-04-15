using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.DataModel
{
    public abstract class DisplayBase
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        
        public DisplayBase(int _ID, DateTime _Date, int _Quantity)
        {
            ID = _ID;
            Date = _Date;
            Quantity = _Quantity;
            
        }
    }
}
