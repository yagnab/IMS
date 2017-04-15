namespace IMS.BL.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingBackTransactionToRepopulate : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemTransactions", "TransactionID", "dbo.Transactions");
            DropForeignKey("dbo.ItemTransactions", "ItemID", "dbo.Items");
            DropIndex("dbo.ItemTransactions", new[] { "TransactionID" });
            DropIndex("dbo.ItemTransactions", new[] { "ItemID" });
            DropTable("dbo.Transactions");
            DropTable("dbo.ItemTransactions");
        }
    }
}
