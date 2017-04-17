using System;
using System.Collections.Generic;
using IMS.BL.DataModel;
using System.Linq;

namespace IMS.BL.Repositories
{
    public class ItemTransactionsRepo : Repository<ItemTransaction>, IItemTransactionsRepo
    {
        public ItemTransactionsRepo(InventoryContext context) : base(context)
        {
        }

        public IEnumerable<ItemTransaction> AllItemTransactionFrom(TimeSpan timePeriod)
        {
            //finding out exact DateTime timePeriod into the past
            DateTime pastTime = DateTime.Now.Subtract(timePeriod);

            
            //select all ItemTransactions where the date 
            //was after pastTime
            List<ItemTransaction> validItemTransactions = (from i in Context.Items
                                            from i_t in i.ItemTransactions
                                            where i_t.Transaction.TimeOfTransaction >= pastTime
                                            select i_t).ToList();
            return validItemTransactions;
        }
    }
}
