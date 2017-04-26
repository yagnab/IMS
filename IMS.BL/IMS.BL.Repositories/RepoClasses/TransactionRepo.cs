using System;
using IMS.BL.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace IMS.BL.Repositories
{
    public class TransactionRepo : Repository<Transaction>, ITransactionRepo
    {
        public TransactionRepo(InventoryContext context) : base(context)
        {
        }
        /// <summary>
        /// Assumes quantity already validated
        /// </summary>
        /// <param name="itemsToQuantity"></param>
        public void AddNewTransaction(Dictionary<Item, int> itemsToQuantity)
        {
            var now = DateTime.Now;
            //creating transaction
            var newTransaction = new Transaction()
            {
                TimeOfTransaction = now
            };

            using (var dbContext = new InventoryContext())
            {
                dbContext.Transactions.Add(newTransaction);
                dbContext.SaveChanges();
            }

            using (var dbContext = new InventoryContext())
            {
                foreach (KeyValuePair<Item, int> i_q in itemsToQuantity)
                {
                    var newi_t = new ItemTransaction()
                    {
                        Item = dbContext.Items.Where(i => i.ItemID == i_q.Key.ItemID).First(),
                        Transaction = dbContext.Transactions
                            .Where(t => t.TransactionID == newTransaction.TransactionID)
                            .First(),
                        Quantity = i_q.Value
                    };
                    dbContext.ItemTransactions.Add(newi_t);
                }

                dbContext.SaveChanges();
            }

            //setting totalValue
            decimal totalValue = 0m;
            foreach(KeyValuePair<Item, int> i_q in itemsToQuantity)
            {
                //totalValue += item.RRP * itemTransaction.Quantity
                totalValue += i_q.Key.RRP * i_q.Value;
            }

            //setting new total value
            using (var dbContext = new InventoryContext())
            {
                var targetTransaction = dbContext.Transactions
                    .Where(t => t.TransactionID == newTransaction.TransactionID)
                    .First();
                targetTransaction.TotalValue = totalValue;
                dbContext.SaveChanges();
            }

                //Reduce QuantityStockLevel of items by itemsToQuantity.value
            ReduceItemQuantity(itemsToQuantity);
        }

        void ReduceItemQuantity(Dictionary<Item, int> itemsToQuantity)
        {
            using (var dbContext = new InventoryContext())
            {
                foreach (KeyValuePair<Item, int> i_q in itemsToQuantity)
                {

                    var item = dbContext.Items.Where(i => i.ItemID == i_q.Key.ItemID).First();
                    item.QuantityStockLevel -= i_q.Value;
                }

                dbContext.SaveChanges();
            }
        }

        public List<Transaction> AllTransactionsIncludeItemTransaction()
        {
            using (var dbContext = new InventoryContext())
            {
                return dbContext.Transactions.Include("ItemTransactions").ToList();
            }
        }

        public List<Transaction> AllTransactionsIncludeItemTransactionItem()
        {
            using (var dbContext = new InventoryContext())
            {
                    return dbContext.Transactions
                    .Include("ItemTransactions")
                    //.Include("Item")
                    .ToList();
            }
        }
    }
}
