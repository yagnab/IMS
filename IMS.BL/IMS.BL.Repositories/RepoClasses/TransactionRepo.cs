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
            using (var tRepo = new TransactionRepo(new InventoryContext()))
            {
                tRepo.Add(newTransaction);
                tRepo.Complete();
            }

            //getting transaction from db
            using (var tRepo = new TransactionRepo(new InventoryContext()))
            {
                newTransaction = tRepo.Find(t => t.TimeOfTransaction == now).First();
                tRepo.Complete();
            }

            //creating itemTransactions
            foreach (KeyValuePair<Item, int> i_q in itemsToQuantity)
            {
                using (var itRepo = new ItemTransactionsRepo(new InventoryContext()))
                {
                    var newi_t = new ItemTransaction()
                    {
                        Item = i_q.Key,
                        Transaction = newTransaction,
                        Quantity = i_q.Value
                    };
                    itRepo.Add(newi_t);

                    itRepo.Complete();
                }
            }

            //setting totalValue
            decimal totalValue = 0m;
            foreach(KeyValuePair<Item, int> i_q in itemsToQuantity)
            {
                //totalValue += item.RRP * itemTransaction.Quantity
                totalValue += i_q.Key.RRP * i_q.Value;
            }
            newTransaction.TotalValue = totalValue;

            //Reduce QuantityStockLevel of items by itemsToQuantity.value
            ReduceItemQuantity(itemsToQuantity);
        }

        void ReduceItemQuantity(Dictionary<Item, int> itemsToQuantity)
        {
            foreach(KeyValuePair<Item, int> i_q in itemsToQuantity)
            {
                using (var iRepo = new ItemRepository(new InventoryContext()))
                {
                    var item = iRepo.GetByID(i_q.Key.ItemID);
                    item.QuantityStockLevel -= i_q.Value;
                    iRepo.Complete();
                }
            }
        }
    }
}
