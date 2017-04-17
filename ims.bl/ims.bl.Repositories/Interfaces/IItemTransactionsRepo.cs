using System;
using IMS.BL.DataModel;
using System.Collections;
using System.Collections.Generic;

namespace IMS.BL.Repositories
{
    public interface IItemTransactionsRepo : IRepository<ItemTransaction>
    {
        IEnumerable<ItemTransaction> AllItemTransactionFrom(TimeSpan timePeriod);

    }
}