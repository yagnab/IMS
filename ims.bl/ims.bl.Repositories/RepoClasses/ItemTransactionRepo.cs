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
        /// <summary>
        /// Finds all itemTransactions within now and -timePeriod
        /// from provided itemTransactionsPool
        /// </summary>
        /// <param name="timePeriod"></param>
        /// <param name="itemTransactionPool"></param>
        /// <returns></returns>
        public List<ItemTransaction> AllItemTransactionFrom(TimeSpan timePeriod, IEnumerable<Item> itemsPool) 
        {
            //finding out exact DateTime timePeriod into the past
            DateTime pastTime = DateTime.Now.Subtract(timePeriod);
            
            //itemsPool -> itemTransactionsPool
            //subtracting again as GetItemTransactionPool uses > time instead of >= time
            List<ItemTransaction> validItemTransactions = GetItemTransactionPool(itemsPool, pastTime.Subtract(new TimeSpan(0,0,0) ) );

            return validItemTransactions;
        }

        public List<ItemTransaction> AllItemTransactionBetween(DateTime afterThis, DateTime beforeOrEqualThis, IEnumerable<ItemTransaction> itemTransactionsPool)
        {
            List<ItemTransaction> validItemTransactions = (from i_t in itemTransactionsPool
                                    where i_t.Transaction.TimeOfTransaction <= beforeOrEqualThis
                                        && i_t.Transaction.TimeOfTransaction > afterThis
                                    select i_t).ToList();
            return validItemTransactions;
        }
        /// <summary>
        /// This will do > afterThis 
        /// not >= afterThis
        /// </summary>
        /// <param name="afterThis"></param>
        /// <returns></returns>
        public List<ItemTransaction> AllItemTransactionsAfter(DateTime afterThis)
        {
            List<ItemTransaction> validItemTransactions = (from i_t in Context.ItemTransactions
                                                           where i_t.Transaction.TimeOfTransaction > afterThis
                                                           select i_t).ToList();
            return validItemTransactions;
        }
        /// <summary>
        /// This will do > afterThis 
        /// not >= afterThis
        /// </summary>
        /// <param name="afterThis"></param>
        /// <returns></returns>
        public List<ItemTransaction> AllItemTransactionsAfter(DateTime afterThis, List<ItemTransaction> itemTransactionsPool)
        {
            List<ItemTransaction> validItemTransactions = (from i_t in itemTransactionsPool
                                                           where i_t.Transaction.TimeOfTransaction > afterThis
                                                           select i_t).ToList();
            return validItemTransactions;
        }
        public Dictionary<DateTime, decimal> RevenueDataFrom(GraphTimePeriod graphTimePeriod, IEnumerable<Item> itemsPool)
        {
            List<DateTime> XAxis = GetXAxis(graphTimePeriod);
            List<decimal> YAxis = GetYAxis(XAxis, itemsPool);
            Dictionary<DateTime, decimal> XYAxis = XAxis.Zip(YAxis, (k, v) => new { k, v })
                                                    .ToDictionary(row => row.k, row => row.v);
            return XYAxis;
        }

        public Dictionary<DateTime, int> QuantityDataFrom(GraphTimePeriod graphTimePeriod, IEnumerable<Item> itemsPool)
        {
            List<DateTime> XAxis = GetXAxis(graphTimePeriod);
            List<decimal> DecimalYAxis = GetYAxis(XAxis, itemsPool);

            //converting decimals to integers
            List<int> YAxis = new List<int>();
            DecimalYAxis.ForEach(dec => YAxis.Add(Convert.ToInt32(dec)));

            Dictionary<DateTime, int> XYAxis = XAxis.Zip(YAxis, (k, v) => new { k, v })
                                                    .ToDictionary(row => row.k, row => row.v);
            return XYAxis;
        }
        List<ItemTransaction> GetItemTransactionPool(IEnumerable<Item> itemsPool)
        {
            List<ItemTransaction> itemTransactionsPool = new List<ItemTransaction>();
            foreach (Item item in itemsPool)
            {
                foreach (ItemTransaction itemTransaction in item.ItemTransactions)
                {
                    itemTransactionsPool.Add(itemTransaction);
                }
            }
            return itemTransactionsPool;
        }
        List<ItemTransaction> GetItemTransactionPool(IEnumerable<Item> itemsPool, DateTime afterThis)
        {
            List<ItemTransaction> itemTransactionsPool = new List<ItemTransaction>();
            foreach (Item item in itemsPool)
            {
                foreach (ItemTransaction itemTransaction in item.ItemTransactions)
                {
                    
                    itemTransactionsPool.Add(itemTransaction);
                    
                }
            }
            return AllItemTransactionsAfter(afterThis, itemTransactionsPool);
        }
        /// <summary>
        /// Takes in a time period and 
        /// return a list of DateTimes's
        /// between DateTime.Now and DateTime.Now - graphTimePeriod
        /// with regular intervals declared in the function
        /// </summary>
        /// <param name="graphTimePeriod"></param>
        /// <returns></returns>
        List<DateTime> GetXAxis(GraphTimePeriod graphTimePeriod)
        {
            int xIntervalSetting = 0;
            int xStartSetting = 0;
            //interval in for loop
            switch (graphTimePeriod)
            {
                //Unit for x axis is minutes
                case GraphTimePeriod.PastHour:
                    xIntervalSetting = 5;
                    xStartSetting = -60;
                    break;
                case GraphTimePeriod.PastDay:
                    xIntervalSetting = 2 * 60;
                    xStartSetting = -24 * 60;
                    break;
                case GraphTimePeriod.PastWeek:
                    xIntervalSetting = 24 * 60;
                    xStartSetting = -7 * 24 * 60;
                    break;
                case GraphTimePeriod.PastYear:
                    xIntervalSetting = 30 * 24 * 60;
                    xStartSetting = -365 * 24 * 60;
                    break;
                default:
                    //some error found with graphTimePeriod
                    return null;
            }

            //creating the xAxis

            //unit = minutes
            DateTime now = DateTime.Now;
            DateTime xStart = now.Add(new TimeSpan(0, xStartSetting, 0));
            TimeSpan xInterval = new TimeSpan(0, xIntervalSetting, 0);

            List<DateTime> xAxis = new List<DateTime>();
            for (DateTime currTime = xStart; currTime <= now; currTime = currTime.Add(xInterval))
            {
                xAxis.Add(currTime);
            }

            xAxis.ForEach(dt => System.Console.WriteLine(dt));
            return null;
        }
        /// <summary>
        /// If want quantity YAxis make third
        /// argument = False
        /// </summary>
        /// <returns>YAxis where YAxis[0] == 0</returns>
        List<decimal> GetYAxis(List<DateTime> XAxis, IEnumerable<Item> itemsPool, bool isForRevenue = true)
        {
            //itemsPool -> itemTransactionsPool where only itemTransactions after XAxis[0]
            List<ItemTransaction> itemTransactionsPool = GetItemTransactionPool(itemsPool, XAxis[0]);

            //Creates YAxis list and initalised first value as 0
            List<decimal> YAxis = new List<decimal>()
            {
                0
            };

            //adding revenue/quantity
            for (int index = 1; index < XAxis.Count; index++)
            {
                IEnumerable<ItemTransaction> validi_ts = AllItemTransactionBetween(XAxis[index - 1], XAxis[index], itemTransactionsPool);
                decimal currTotal = 0;
                foreach (ItemTransaction i_t in validi_ts)
                {
                    if(isForRevenue)
                    {
                        currTotal += i_t.Quantity * i_t.Item.RRP;
                    }
                    else
                    {
                        currTotal += i_t.Quantity;
                    }
                }
                YAxis.Add(currTotal);
            }
            return YAxis;
        }
    }
}
