namespace IMS.BL.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTransactionToTruncateSafely : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ItemTransactions", "ItemID", "dbo.Items");
            DropForeignKey("dbo.ItemTransactions", "TransactionID", "dbo.Transactions");
            DropIndex("dbo.ItemTransactions", new[] { "ItemID" });
            DropIndex("dbo.ItemTransactions", new[] { "TransactionID" });
            DropTable("dbo.ItemTransactions");
            DropTable("dbo.Transactions");
        }
        
        public override void Down()
        {
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
                "dbo.ItemTransactions",
                c => new
                    {
                        ItemID = c.Int(nullable: false),
                        TransactionID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ItemID, t.TransactionID });
            
            CreateIndex("dbo.ItemTransactions", "TransactionID");
            CreateIndex("dbo.ItemTransactions", "ItemID");
            AddForeignKey("dbo.ItemTransactions", "TransactionID", "dbo.Transactions", "TransactionID", cascadeDelete: true);
            AddForeignKey("dbo.ItemTransactions", "ItemID", "dbo.Items", "ItemID", cascadeDelete: true);
        }
    }
}
