﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace IMS.BL.DataModel
{
    public class Item
    {
        //Attributes
        public int ItemID { get; set; }
        [Required]
        public string Barcode { get; set; }
        [Required]
        public string Description { get; set; }

        //make sure it can only be to 2dp 
        //this is how curreny works
        private decimal _RRP;
        public decimal RRP
        {
            get
            {
                return _RRP;
            }
            set
            {
                //round to 2 dp
                _RRP = Math.Round(value, 2);
            }
        }

        public int QuantityStockLevel { get; set; }
        public int QuantityWeaklySaleRate { get; set; }
        public ItemCatagory Catagory { get; set; }
        
        //Relatationships
        public ICollection<ItemDelivery> ItemDeliveries { get; set; }
        public ICollection<ItemReservation> ItemReservations { get; set; }
        public ICollection<ItemTransaction> ItemTransactions { get; set; }
        
        //prevent .ToString() from returning "IMS.BL.DataModel.Item"
        public override string ToString()
        {
            return "Item";
        }
    }
}
