using System;
using System.Collections.Generic;
using IMS.BL.DataModel;

namespace IMS.BL.Repositories
{
    interface ITransactionRepo : IRepository<Transaction>
    {
        void AddNewTransaction(Dictionary<Item, int> itemsToQuantity);
    }
}
