using System;
using System.Collections.Generic;
using System.Linq;
using IMS.BL.DataModel;
using System.Data.Entity;

namespace IMS.UI
{
    public static class UIUtility
    {
        #region Updating sale rate utility
        public static void UpdateAllItemSaleRate()
        {
            using (var dbContext = new InventoryContext())
            {
                var allItems = dbContext.Items;
                foreach (Item item in allItems)
                {
                    item.QuantityWeaklySaleRate = 0;
                }

                dbContext.SaveChanges();
                
                List<ItemTransaction> validI_Ts = AllItemTransactionsPastWeek();

                //each time an item is sold, increment its sale rate
                //by quantity sold
                foreach (ItemTransaction i_t in validI_Ts)
                {
                    var tempItem = (from i in dbContext.Items
                                    where i.ItemID == i_t.ItemID
                                    select i).First();
                    tempItem.QuantityWeaklySaleRate += i_t.Quantity;
                    

                }
                dbContext.SaveChanges();
            }

        }

        public static List<ItemTransaction> AllItemTransactionsPastHour()
        {
            //finding out exact DateTime a hour ago
            //which is stored as hourBeforeNow
            DateTime hourBeforeNow = DateTime.Now.AddHours(-1);

            using (var dbContext = new InventoryContext())
            {
                //select all ItemTransactions where a date 
                //was after weekBeforeNow
                List<ItemTransaction> i_ts = (from i in dbContext.Items
                                              from i_t in i.ItemTransactions
                                              where i_t.Transaction.TimeOfTransaction >= hourBeforeNow
                                              select i_t).ToList();

                return i_ts;
            }

        }

        public static List<ItemTransaction> AllItemTransactionsPastWeek()
        {
            //finding out exact DateTime a week ago
            //which is stored as weekBeforeNow
            TimeSpan week = TimeSpan.FromDays(7 * 1);
            DateTime weekBeforeNow = DateTime.Now.Date.Subtract(week);

            using (var dbContext = new InventoryContext())
            {
                //select all ItemTransactions where a date 
                //was after weekBeforeNow
                List<ItemTransaction> i_ts = (from i in dbContext.Items
                                              from i_t in i.ItemTransactions
                                              where i_t.Transaction.TimeOfTransaction >= weekBeforeNow
                                              select i_t).ToList();

                return i_ts;
            }

        }
        #endregion

        #region Analytics Get graphs utility
        public static Dictionary<DateTime, int> GetQuantityData(GraphTimePeriod timePeriod)
        {
            switch (timePeriod)
            {
                case GraphTimePeriod.PastHour:
                    return GetQuantityDataPastHour();
                case GraphTimePeriod.PastDay:
                    return GetQuantityDataPastDay();
                case GraphTimePeriod.PastWeek:
                    return GetQuantityDataPastWeek();
                case GraphTimePeriod.PastYear:
                    return GetQuantityDataPastYear();
                    
                default:
                    return null;
            }
        }

        public static Dictionary<DateTime, int> GetQuantityData(GraphTimePeriod timePeriod, Item item)
        {
            switch (timePeriod)
            {
                case GraphTimePeriod.PastHour:
                    return GetQuantityDataPastHour(item);
                case GraphTimePeriod.PastDay:
                    return GetQuantityDataPastDay(item);
                case GraphTimePeriod.PastWeek:
                    return GetQuantityDataPastWeek(item);
                case GraphTimePeriod.PastYear:
                    return GetQuantityDataPastYear(item);
                default:
                    return null;
            }
        }

        public static Dictionary<DateTime, decimal> GetRevenueData(GraphTimePeriod timePeriod)
        {
            switch (timePeriod)
            {
                case GraphTimePeriod.PastHour:
                    return GetRevenueDataPastHour();
                case GraphTimePeriod.PastDay:
                    return GetRevenueDataPastDay();
                case GraphTimePeriod.PastWeek:
                    return GetRevenueDataPastWeek();
                case GraphTimePeriod.PastYear:
                    return GetRevenueDataPastYear();
                default:
                    return null;
            }
        }

        public static Dictionary<DateTime, decimal> GetRevenueData(GraphTimePeriod timePeriod, Item item)
        {
            switch (timePeriod)
            {
                case GraphTimePeriod.PastHour:
                    return GetRevenueDataPastHour(item);
                case GraphTimePeriod.PastDay:
                    return GetRevenueDataPastDay(item);
                case GraphTimePeriod.PastWeek:
                    return GetRevenueDataPastWeek(item);
                case GraphTimePeriod.PastYear:
                    return GetRevenueDataPastYear(item);
                default:
                    return null;
            }
        }
        #endregion

        #region Past hour utility
        static Dictionary<DateTime, int> GetQuantityDataPastHour()
        {
            Dictionary<DateTime, int> quantityDataPoints = new Dictionary<DateTime, int>();
            var now = DateTime.Now;

            //first dp
            quantityDataPoints.Add(now.AddHours(-1), 0);

            //create other dps
            int total = 0;
            using (var dbContext = new InventoryContext())
            {
                //every 5 mins
                for (int min = 1; min < 61; min = min + 5)
                {
                    total = 0;
                    var validItemTransactions = from i_t in dbContext.ItemTransactions
                                                where i_t.Transaction.TimeOfTransaction >= DbFunctions.AddMinutes(now, (-1) * min)
                                                    && i_t.Transaction.TimeOfTransaction < DbFunctions.AddMinutes(now, ((-1) * min) + 5)
                                                select i_t;

                    if (validItemTransactions != null)
                    {
                        //find total for transactions
                        foreach (ItemTransaction itemTransaction in validItemTransactions)
                        {
                            total += itemTransaction.Quantity;
                        }
                    }
                    else
                    {
                        total = 0;
                    }

                    quantityDataPoints.Add(now.AddMinutes((-1) * min), total);

                }
            }
            return quantityDataPoints;
        }

        //using in "single item mode"
        static Dictionary<DateTime, int> GetQuantityDataPastHour(Item item)
        {
            Dictionary<DateTime, int> quantityDataPoints = new Dictionary<DateTime, int>();
            var now = DateTime.Now;

            //first dp
            quantityDataPoints.Add(now.AddHours(-1), 0);

            //create other dps
            int total = 0;
            using (var dbContext = new InventoryContext())
            {
                //every 5 mins
                for (int min = 1; min < 61; min = min + 5)
                {
                    total = 0;
                    var validItemTransactions = from i_t in dbContext.ItemTransactions
                                                where i_t.Transaction.TimeOfTransaction >= DbFunctions.AddMinutes(now, (-1) * min)
                                                    && i_t.Transaction.TimeOfTransaction < DbFunctions.AddMinutes(now, ((-1) * min) + 5)
                                                    && i_t.ItemID == item.ItemID
                                                select i_t;

                    if (validItemTransactions != null)
                    {
                        //find total for transactions
                        foreach (ItemTransaction itemTransaction in validItemTransactions)
                        {
                            total += itemTransaction.Quantity;
                        }
                    }
                    else
                    {
                        total = 0;
                    }

                    quantityDataPoints.Add(now.AddMinutes((-1) * min), total);

                }
            }
            return quantityDataPoints;
        }

        static Dictionary<DateTime, decimal> GetRevenueDataPastHour()
        {
            Dictionary<DateTime, decimal> revDataPoints = new Dictionary<DateTime, decimal>();
            var now = DateTime.Now;

            //first dp
            revDataPoints.Add(now.AddHours(-1), 0m);

            //create other dps
            decimal total = 0;
            using (var dbContext = new InventoryContext())
            {
                //every 5 mins
                for (int min = 1; min < 61; min = min + 5)
                {
                    total = 0;
                    var validItemTransactions = from i_t in dbContext.ItemTransactions
                                                where i_t.Transaction.TimeOfTransaction >= DbFunctions.AddMinutes(now, (-1) * min)
                                                    && i_t.Transaction.TimeOfTransaction < DbFunctions.AddMinutes(now, ((-1) * min) + 5)
                                                select i_t;

                    if (validItemTransactions != null)
                    {
                        //find total for transactions
                        foreach (ItemTransaction itemTransaction in validItemTransactions)
                        {
                            total += itemTransaction.Item.RRP * itemTransaction.Quantity;
                        }
                    }
                    else
                    {
                        total = 0;
                    }

                    revDataPoints.Add(now.AddMinutes((-1) * min), total);

                }
            }
            return revDataPoints;
        }
        //Use for "single item" option
        static Dictionary<DateTime, decimal> GetRevenueDataPastHour(Item item)
        {
            Dictionary<DateTime, decimal> revDataPoints = new Dictionary<DateTime, decimal>();
            var now = DateTime.Now;

            //first dp
            revDataPoints.Add(now.AddHours(-1), 0m);

            //create other dps
            decimal total = 0;
            using (var dbContext = new InventoryContext())
            {
                //every 5 mins
                for (int min = 1; min < 61; min=min+5)
                {
                    total = 0;
                    var validItemTransactions = from i_t in dbContext.ItemTransactions
                                                where i_t.Transaction.TimeOfTransaction >= DbFunctions.AddMinutes(now, (-1) * min)
                                                    && i_t.Transaction.TimeOfTransaction < DbFunctions.AddMinutes(now, ((-1) * min) + 5 )
                                                    && (i_t.ItemID == item.ItemID) 
                                                select i_t;

                    if (validItemTransactions != null)
                    {
                        //find total for transactions
                        foreach (ItemTransaction itemTransaction in validItemTransactions)
                        {
                            total += itemTransaction.Item.RRP * itemTransaction.Quantity;
                        }
                    }
                    else
                    {
                        total = 0;
                    }

                    revDataPoints.Add( now.AddMinutes( (-1)*min ) , total);

                }
            }
            return revDataPoints;
        }
        #endregion

        #region Past day utility
        
        static Dictionary<DateTime, int> GetQuantityDataPastDay()
        {
            Dictionary<DateTime, int> quantityDataPoints = new Dictionary<DateTime, int>();
            var now = DateTime.Now;

            //first dp
            quantityDataPoints.Add(now.AddDays(-1), 0);

            //create other dps
            int total = 0;
            using (var dbContext = new InventoryContext())
            {
                //every 5 mins
                for (int hour = 1; hour < 25; hour = hour+2)
                {
                    total = 0;
                    var validItemTransactions = from i_t in dbContext.ItemTransactions
                                                where i_t.Transaction.TimeOfTransaction >= DbFunctions.AddHours(now, (-1) * hour)
                                                    && i_t.Transaction.TimeOfTransaction < DbFunctions.AddHours(now, ((-1) * hour) + 2)
                                                select i_t;

                    if (validItemTransactions != null)
                    {
                        //find total for transactions
                        foreach (ItemTransaction itemTransaction in validItemTransactions)
                        {
                            total += itemTransaction.Quantity;
                        }
                    }
                    else
                    {
                        total = 0;
                    }

                    quantityDataPoints.Add(now.AddHours( (-1)*hour ), total) ;

                }
            }
            return quantityDataPoints;
        }
        static Dictionary<DateTime, decimal> GetRevenueDataPastDay()
        {
            Dictionary<DateTime, decimal> revenueDataPoints = new Dictionary<DateTime, decimal>();
            var now = DateTime.Now;

            //first dp
            revenueDataPoints.Add(now.AddDays(-1), 0);

            //create other dps
            decimal total = 0;

            using (var dbContext = new InventoryContext())
            {
                //every 5 mins
                for (int hour = 1; hour < 25; hour = hour + 2)
                {
                    total = 0;
                    var validItemTransactions = from i_t in dbContext.ItemTransactions
                                                where i_t.Transaction.TimeOfTransaction >= DbFunctions.AddHours(now, (-1) * hour)
                                                    && i_t.Transaction.TimeOfTransaction < DbFunctions.AddHours(now, ((-1) * hour) + 2)
                                                select i_t;

                    if (validItemTransactions != null)
                    {
                        //find total for transactions
                        foreach (ItemTransaction itemTransaction in validItemTransactions)
                        {
                            total += itemTransaction.Quantity * itemTransaction.Item.RRP;
                        }
                    }
                    else
                    {
                        total = 0;
                    }

                    revenueDataPoints.Add(now.AddHours((-1) * hour), total);

                }
            }
            return revenueDataPoints;
        }

        static Dictionary<DateTime, int> GetQuantityDataPastDay(Item item)
        {
            Dictionary<DateTime, int> quantityDataPoints = new Dictionary<DateTime, int>();
            var now = DateTime.Now;

            //first dp
            quantityDataPoints.Add(now.AddDays(-1), 0);

            //create other dps
            int total = 0;
            using (var dbContext = new InventoryContext())
            {
                //every 5 mins
                for (int hour = 1; hour < 25; hour = hour + 2)
                {
                    total = 0;
                    var validItemTransactions = from i_t in dbContext.ItemTransactions
                                                where i_t.Transaction.TimeOfTransaction >= DbFunctions.AddHours(now, (-1) * hour)
                                                    && i_t.Transaction.TimeOfTransaction < DbFunctions.AddHours(now, ((-1) * hour) + 2)
                                                    && i_t.ItemID == item.ItemID
                                                select i_t;

                    if (validItemTransactions != null)
                    {
                        //find total for transactions
                        foreach (ItemTransaction itemTransaction in validItemTransactions)
                        {
                            total += itemTransaction.Quantity;
                        }
                    }
                    else
                    {
                        total = 0;
                    }

                    quantityDataPoints.Add(now.AddHours((-1) * hour), total);

                }
            }
            return quantityDataPoints;
        }

        static Dictionary<DateTime, decimal> GetRevenueDataPastDay(Item item)
        {
            Dictionary<DateTime, decimal> revenueDataPoints = new Dictionary<DateTime, decimal>();
            var now = DateTime.Now;

            //first dp
            revenueDataPoints.Add(now.AddDays(-1), 0);

            //create other dps
            decimal total = 0;

            using (var dbContext = new InventoryContext())
            {
                //every 5 mins
                for (int hour = 1; hour < 25; hour = hour + 2)
                {
                    total = 0;
                    var validItemTransactions = from i_t in dbContext.ItemTransactions
                                                where i_t.Transaction.TimeOfTransaction >= DbFunctions.AddHours(now, (-1) * hour)
                                                    && i_t.Transaction.TimeOfTransaction < DbFunctions.AddHours(now, ((-1) * hour) + 2)
                                                    && i_t.ItemID == item.ItemID
                                                select i_t;

                    if (validItemTransactions != null)
                    {
                        //find total for transactions
                        foreach (ItemTransaction itemTransaction in validItemTransactions)
                        {
                            total += itemTransaction.Quantity * itemTransaction.Item.RRP;
                        }
                    }
                    else
                    {
                        total = 0;
                    }

                    revenueDataPoints.Add(now.AddHours((-1) * hour), total);

                }
            }
            return revenueDataPoints;
        }
        #endregion

        #region Past week utility

        static Dictionary<DateTime, int> GetQuantityDataPastWeek()
        {
            Dictionary<DateTime, int> quantityDataPoints = new Dictionary<DateTime, int>();
            var now = DateTime.Now;

            //first dp
            quantityDataPoints.Add(now.AddDays(-7), 0);

            //create other dps
            int total = 0;
            using (var dbContext = new InventoryContext())
            {
                //every 5 mins
                for (int day = 1; day < 8; day++)
                {
                    total = 0;
                    var validItemTransactions = from i_t in dbContext.ItemTransactions
                                                where i_t.Transaction.TimeOfTransaction >= DbFunctions.AddDays(now, (-1) * day)
                                                    && i_t.Transaction.TimeOfTransaction < DbFunctions.AddDays(now, ((-1) * day) + 1)
                                                select i_t;

                    if (validItemTransactions != null)
                    {
                        //find total for transactions
                        foreach (ItemTransaction itemTransaction in validItemTransactions)
                        {
                            total += itemTransaction.Quantity;
                        }
                    }
                    else
                    {
                        total = 0;
                    }

                    quantityDataPoints.Add(now.AddHours((-1) * day), total);

                }
            }
            return quantityDataPoints;
        }

        static Dictionary<DateTime, decimal> GetRevenueDataPastWeek()
        {
            Dictionary<DateTime, decimal> quantityDataPoints = new Dictionary<DateTime, decimal>();
            var now = DateTime.Now;

            //first dp
            quantityDataPoints.Add( now.AddDays(-7), 0 );

            //create other dps
            decimal total = 0;
            using (var dbContext = new InventoryContext())
            {
                //every 5 mins
                for (int day = 1; day < 8; day++)
                {
                    total = 0;
                    var validItemTransactions = from i_t in dbContext.ItemTransactions
                                                where i_t.Transaction.TimeOfTransaction >= DbFunctions.AddDays(now, (-1) * day)
                                                    && i_t.Transaction.TimeOfTransaction < DbFunctions.AddDays(now, ((-1) * day) + 1)
                                                select i_t;

                    if (validItemTransactions != null)
                    {
                        //find total for transactions
                        foreach (ItemTransaction itemTransaction in validItemTransactions)
                        {
                            total += itemTransaction.Quantity * itemTransaction.Item.RRP;
                        }
                    }
                    else
                    {
                        total = 0;
                    }

                    quantityDataPoints.Add(now.AddHours((-1) * day), total);

                }
            }
            return quantityDataPoints;
        }

        static Dictionary<DateTime, int> GetQuantityDataPastWeek(Item item)
        {
            Dictionary<DateTime, int> quantityDataPoints = new Dictionary<DateTime, int>();
            var now = DateTime.Now;

            //first dp
            quantityDataPoints.Add(now.AddDays(-7), 0);

            //create other dps
            int total = 0;
            using (var dbContext = new InventoryContext())
            {
                //every 5 mins
                for (int day = 1; day < 8; day++)
                {
                    total = 0;
                    var validItemTransactions = from i_t in dbContext.ItemTransactions
                                                where i_t.Transaction.TimeOfTransaction >= DbFunctions.AddDays(now, (-1) * day)
                                                    && i_t.Transaction.TimeOfTransaction < DbFunctions.AddDays(now, ((-1) * day) + 1)
                                                    && i_t.ItemID == item.ItemID
                                                select i_t;

                    if (validItemTransactions != null)
                    {
                        //find total for transactions
                        foreach (ItemTransaction itemTransaction in validItemTransactions)
                        {
                            total += itemTransaction.Quantity;
                        }
                    }
                    else
                    {
                        total = 0;
                    }

                    quantityDataPoints.Add(now.AddHours((-1) * day), total);

                }
            }
            return quantityDataPoints;
        }

        static Dictionary<DateTime, decimal> GetRevenueDataPastWeek(Item item)
        {
            Dictionary<DateTime, decimal> quantityDataPoints = new Dictionary<DateTime, decimal>();
            var now = DateTime.Now;

            //first dp
            quantityDataPoints.Add(now.AddDays(-7), 0);

            //create other dps
            decimal total = 0;
            using (var dbContext = new InventoryContext())
            {
                //every 5 mins
                for (int day = 1; day < 8; day++)
                {
                    total = 0;
                    var validItemTransactions = from i_t in dbContext.ItemTransactions
                                                where i_t.Transaction.TimeOfTransaction >= DbFunctions.AddDays(now, (-1) * day)
                                                    && i_t.Transaction.TimeOfTransaction < DbFunctions.AddDays(now, ((-1) * day) + 1)
                                                    && i_t.ItemID == item.ItemID
                                                select i_t;

                    if (validItemTransactions != null)
                    {
                        //find total for transactions
                        foreach (ItemTransaction itemTransaction in validItemTransactions)
                        {
                            total += itemTransaction.Quantity * itemTransaction.Item.RRP;
                        }
                    }
                    else
                    {
                        total = 0;
                    }

                    quantityDataPoints.Add(now.AddHours((-1) * day), total);

                }
            }
            return quantityDataPoints;
        }
        #endregion

        #region Past year utility
        static Dictionary<DateTime, int> GetQuantityDataPastYear()
        {
            Dictionary<DateTime, int> quantityDataPoints = new Dictionary<DateTime, int>();
            var now = DateTime.Now;

            //first dp
            quantityDataPoints.Add(now.AddYears(-1), 0);

            //create other dps
            int total = 0;
            using (var dbContext = new InventoryContext())
            {
                //every 5 mins
                for (int month = 1; month < 13; month++)
                {
                    total = 0;
                    var validItemTransactions = from i_t in dbContext.ItemTransactions
                                                where i_t.Transaction.TimeOfTransaction >= DbFunctions.AddDays(now, (-1) * month)
                                                    && i_t.Transaction.TimeOfTransaction < DbFunctions.AddDays(now, ((-1) * month) + 1)
                                                select i_t;

                    if (validItemTransactions != null)
                    {
                        //find total for transactions
                        foreach (ItemTransaction itemTransaction in validItemTransactions)
                        {
                            total += itemTransaction.Quantity;
                        }
                    }
                    else
                    {
                        total = 0;
                    }

                    quantityDataPoints.Add(now.AddHours((-1) * month), total);

                }
            }
            return quantityDataPoints;
        }

        static Dictionary<DateTime, decimal> GetRevenueDataPastYear()
        {
            Dictionary<DateTime, decimal> quantityDataPoints = new Dictionary<DateTime, decimal>();
            var now = DateTime.Now;

            //first dp
            quantityDataPoints.Add(now.AddYears(-1), 0);

            //create other dps
            decimal total = 0;
            using (var dbContext = new InventoryContext())
            {
                //every 5 mins
                for (int month = 1; month < 13; month++)
                {
                    total = 0;
                    var validItemTransactions = from i_t in dbContext.ItemTransactions
                                                where i_t.Transaction.TimeOfTransaction >= DbFunctions.AddDays(now, (-1) * month)
                                                    && i_t.Transaction.TimeOfTransaction < DbFunctions.AddDays(now, ((-1) * month) + 1)
                                                select i_t;

                    if (validItemTransactions != null)
                    {
                        //find total for transactions
                        foreach (ItemTransaction itemTransaction in validItemTransactions)
                        {
                            total += itemTransaction.Quantity * itemTransaction.Item.RRP;
                        }
                    }
                    else
                    {
                        total = 0;
                    }

                    quantityDataPoints.Add(now.AddHours((-1) * month), total);

                }
            }
            return quantityDataPoints;
        }

        static Dictionary<DateTime, int> GetQuantityDataPastYear(Item item)
        {
            Dictionary<DateTime, int> quantityDataPoints = new Dictionary<DateTime, int>();
            var now = DateTime.Now;

            //first dp
            quantityDataPoints.Add(now.AddYears(-1), 0);

            //create other dps
            int total = 0;
            using (var dbContext = new InventoryContext())
            {
                //every 5 mins
                for (int month = 1; month < 13; month++)
                {
                    total = 0;
                    var validItemTransactions = from i_t in dbContext.ItemTransactions
                                                where i_t.Transaction.TimeOfTransaction >= DbFunctions.AddDays(now, (-1) * month)
                                                    && i_t.Transaction.TimeOfTransaction < DbFunctions.AddDays(now, ((-1) * month) + 1)
                                                    && i_t.ItemID == item.ItemID
                                                select i_t;

                    if (validItemTransactions != null)
                    {
                        //find total for transactions
                        foreach (ItemTransaction itemTransaction in validItemTransactions)
                        {
                            total += itemTransaction.Quantity;
                        }
                    }
                    else
                    {
                        total = 0;
                    }

                    quantityDataPoints.Add(now.AddHours((-1) * month), total);

                }
            }
            return quantityDataPoints;
        }

        static Dictionary<DateTime, decimal> GetRevenueDataPastYear(Item item)
        {
            Dictionary<DateTime, decimal> revenueDataPoints = new Dictionary<DateTime, decimal>();
            var now = DateTime.Now;

            //first dp
            revenueDataPoints.Add(now.AddYears(-1), 0);

            //create other dps
            decimal total = 0;
            using (var dbContext = new InventoryContext())
            {
                //every 5 mins
                for (int month = 1; month < 13; month++)
                {
                    total = 0;
                    var validItemTransactions = from i_t in dbContext.ItemTransactions
                                                where i_t.Transaction.TimeOfTransaction >= DbFunctions.AddDays(now, (-1) * month)
                                                    && i_t.Transaction.TimeOfTransaction < DbFunctions.AddDays(now, ((-1) * month) + 1)
                                                    && i_t.ItemID == item.ItemID
                                                select i_t;

                    if (validItemTransactions != null)
                    {
                        //find total for transactions
                        foreach (ItemTransaction itemTransaction in validItemTransactions)
                        {
                            total += itemTransaction.Quantity * itemTransaction.Item.RRP;
                        }
                    }
                    else
                    {
                        total = 0;
                    }

                    revenueDataPoints.Add(now.AddHours((-1) * month), total);

                }
            }
            return revenueDataPoints;
        }

        #endregion
            
        #region ItemsPage utility
        /// <summary>
        /// Takes in an ItemID
        /// Retrieves corrisponding entity framework object
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static Item GetItemFromItemID(int ItemID, InventoryContext dbContext)
        {
            //Gets an item and includes corrisponding
            //ItemDeliveries + ItemTransactions
            Item item = dbContext.Items
                        .Include("ItemDeliveries")
                        .Include("ItemReservations")
                        .Where(i => i.ItemID == ItemID)
                        .First();

            return item;
        }
        /// <summary>
        /// Make sure barcode is validated
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public static Item GetItemFromBarcode(string barcode)
        {
            using(var dbContext = new InventoryContext())
            {
                var newItems = from i in dbContext.Items
                               where i.Barcode == barcode
                               select i;

                //Barcode doesn't exist
                if (newItems == null)
                {
                    return null;
                }
                else
                {
                    return newItems.First();
                }
            }
        }
        /// <summary>
        /// Will create ItemReservationDisplay 
        /// for a given ItemReservation object
        /// </summary>
        /// <param name="item"></param>
        /// <param name="i_d"></param>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static ItemReservationDisplay CreateNewItemReservationDisplay(ItemReservation i_r, InventoryContext dbContext)
        {
            
            DateTime expectedPickUpDate = dbContext.Reservations.OfType<CurrentReservation>()
                .Where(r => r.ReservationID == i_r.ReservationID)
                .Select(d => d.ExpectedPickUpDate)
                .First();
            return new ItemReservationDisplay(i_r.ReservationID, expectedPickUpDate, i_r.Quantity);
        }
        #endregion

        #region BL utility

        //This method cannot be used inside Delivery class
        //As it requires InventoryContext object
        //Referencing IMS.BL.DataModel inside IMS.BL would create 
        //A circular dependency, which isn't allowed
        public static List<ItemDelivery> GetItemDeliveries(Delivery _Delivery, InventoryContext dbContext)
        {
            var i_ds = (from i_d in dbContext.ItemDeliveries
                        where i_d.DeliveryID == _Delivery.DeliveryID
                        select i_d).ToList();

            return i_ds;
        }

        #endregion
    }
}