using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.DataModel
{
    public class ItemTransactionDisplay
    {
        public string Barcode { get; private set; }
        public string Description { get; private set; }
        public decimal RRP { get; private set; }
        public int Quantity { get; private set; }

        public ItemTransactionDisplay(ItemTransaction itemTransaction)
        {
            //getting itemTransaction with item
            using (var dbContext = new InventoryContext())
            {
                itemTransaction = (from it in dbContext.ItemTransactions
                                  where it.TransactionID == itemTransaction.TransactionID && it.ItemID == itemTransaction.ItemID
                                  select it).First();

                Barcode = itemTransaction.Item.Barcode;
                Description = itemTransaction.Item.Description;
                RRP = itemTransaction.Item.RRP;
                Quantity = itemTransaction.Quantity;
            }
        }

        public static List<ItemTransactionDisplay> GetRange(List<ItemTransaction> itemTransactions)
        {
            List<ItemTransactionDisplay> range = new List<ItemTransactionDisplay>();

            //adding to range
            using (var dbContext = new InventoryContext())
            {
                foreach (ItemTransaction i_t in itemTransactions)
                {
                    var tempI_T = (from it in dbContext.ItemTransactions
                                  where it.TransactionID == i_t.TransactionID && it.ItemID == i_t.ItemID
                                  select it).First();
                    range.Add(new ItemTransactionDisplay(tempI_T));
                }
            }

            return range;
        }
    }
}
