using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.DataModel
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        //only to 2 dp
        public decimal TotalValue { get; set; }
        
        public DateTime TimeOfTransaction { get; set; }
        
        //needs a solution
        public List<ItemTransaction> ItemTransactions { get; set; }

        //all associated ItemIDs
        //readonly
        public IEnumerable<int> ItemIDs
        {
            get
            {
                return this.ItemTransactions.Select(i_t => i_t.ItemID);
            }
        }
        
        public override string ToString()
        {
            return "Transaction";
        }

    }
}
