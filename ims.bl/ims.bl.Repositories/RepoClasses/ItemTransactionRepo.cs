using System;
using System.Collections.Generic;
using IMS.BL.DataModel;

namespace IMS.BL.Repositories
{
    public class ItemTransactionsRepo : Repository<ItemTransaction>, IItemTransactionsRepo
    {
        public ItemTransactionsRepo(InventoryContext context) : base(context)
        {
        }

        public IEnumerable<ItemTransaction> AllPastItemTransactionFrom(DateTime time)
        {
            return null;
        }
    }
}
