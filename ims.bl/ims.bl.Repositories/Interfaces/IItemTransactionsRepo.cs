using System;
using IMS.BL.DataModel;
using System.Collections;
using System.Collections.Generic;

namespace IMS.BL.Repositories
{
    public interface IItemTransactionsRepo : IRepository<ItemTransaction>
    {
        IEnumerable<ItemTransaction> AllItemTransactionFrom(TimeSpan timePeriod);
        List<ItemTransaction> AllItemTransactionFrom(TimeSpan timePeriod, IEnumerable<Item> itemsPool);
        Dictionary<DateTime, decimal> RevenueDataFrom(GraphTimePeriod graphTimePeriod, IEnumerable<Item> itemsPool);
        Dictionary<DateTime, int> QuantityDataFrom(GraphTimePeriod graphTimePeriod, IEnumerable<Item> itemsPool);
    }
}