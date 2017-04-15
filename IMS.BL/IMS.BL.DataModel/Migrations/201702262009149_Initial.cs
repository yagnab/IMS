namespace IMS.BL.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Deliveries",
                c => new
                    {
                        DeliveryID = c.Int(nullable: false, identity: true),
                        SupplierID = c.Int(nullable: false),
                        IsArrived = c.Boolean(nullable: false),
                        ExpectedArrivalDate = c.DateTime(),
                        ActualArrivalDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.DeliveryID)
                .ForeignKey("dbo.Suppliers", t => t.SupplierID, cascadeDelete: true)
                .Index(t => t.SupplierID);
            
            CreateTable(
                "dbo.ItemDeliveries",
                c => new
                    {
                        ItemID = c.Int(nullable: false),
                        DeliveryID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ItemID, t.DeliveryID })
                .ForeignKey("dbo.Deliveries", t => t.DeliveryID, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.ItemID, cascadeDelete: true)
                .Index(t => t.ItemID)
                .Index(t => t.DeliveryID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        Barcode = c.String(nullable: false),
                        Description = c.String(),
                        RRP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantityStockLevel = c.Int(nullable: false),
                        QuantityWeaklySaleRate = c.Int(nullable: false),
                        Catagory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID);
            
            CreateTable(
                "dbo.ItemReservations",
                c => new
                    {
                        ItemID = c.Int(nullable: false),
                        ReservationID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ItemID, t.ReservationID })
                .ForeignKey("dbo.Items", t => t.ItemID, cascadeDelete: true)
                .ForeignKey("dbo.Reservations", t => t.ReservationID, cascadeDelete: true)
                .Index(t => t.ItemID)
                .Index(t => t.ReservationID);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationID = c.Int(nullable: false, identity: true),
                        IsPickedUp = c.Boolean(nullable: false),
                        ExpectedPickUpDate = c.DateTime(),
                        ActualPickUpDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ReservationID);
            
            CreateTable(
                "dbo.ItemTransactions",
                c => new
                    {
                        ItemID = c.Int(nullable: false),
                        TransactionID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ItemID, t.TransactionID })
                .ForeignKey("dbo.Items", t => t.ItemID, cascadeDelete: true)
                .ForeignKey("dbo.Transactions", t => t.TransactionID, cascadeDelete: true)
                .Index(t => t.ItemID)
                .Index(t => t.TransactionID);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false, identity: true),
                        TotalValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TimeOfTransaction = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionID);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierID = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SupplierID);
            
            CreateTable(
                "dbo.UserAccounts",
                c => new
                    {
                        UserAccountID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.Binary(),
                        IsAdmin = c.Boolean(nullable: false),
                        IsAddDeliveryAllowed = c.Boolean(),
                        IsEditTablesAllowed = c.Boolean(),
                        IsAnalyticsAllowed = c.Boolean(),
                    })
                .PrimaryKey(t => t.UserAccountID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deliveries", "SupplierID", "dbo.Suppliers");
            DropForeignKey("dbo.ItemTransactions", "TransactionID", "dbo.Transactions");
            DropForeignKey("dbo.ItemTransactions", "ItemID", "dbo.Items");
            DropForeignKey("dbo.ItemReservations", "ReservationID", "dbo.Reservations");
            DropForeignKey("dbo.ItemReservations", "ItemID", "dbo.Items");
            DropForeignKey("dbo.ItemDeliveries", "ItemID", "dbo.Items");
            DropForeignKey("dbo.ItemDeliveries", "DeliveryID", "dbo.Deliveries");
            DropIndex("dbo.ItemTransactions", new[] { "TransactionID" });
            DropIndex("dbo.ItemTransactions", new[] { "ItemID" });
            DropIndex("dbo.ItemReservations", new[] { "ReservationID" });
            DropIndex("dbo.ItemReservations", new[] { "ItemID" });
            DropIndex("dbo.ItemDeliveries", new[] { "DeliveryID" });
            DropIndex("dbo.ItemDeliveries", new[] { "ItemID" });
            DropIndex("dbo.Deliveries", new[] { "SupplierID" });
            DropTable("dbo.UserAccounts");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Transactions");
            DropTable("dbo.ItemTransactions");
            DropTable("dbo.Reservations");
            DropTable("dbo.ItemReservations");
            DropTable("dbo.Items");
            DropTable("dbo.ItemDeliveries");
            DropTable("dbo.Deliveries");
        }
    }
}
