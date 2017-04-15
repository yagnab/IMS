namespace IMS.BL.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingRecentUpdates : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Deliveries", newName: "Delivery");
            CreateTable(
                "dbo.DurrentDelivery",
                c => new
                    {
                        DeliveryID = c.Int(nullable: false),
                        ExpectedArrivalDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DeliveryID)
                .ForeignKey("dbo.Delivery", t => t.DeliveryID)
                .Index(t => t.DeliveryID);
            
            CreateTable(
                "dbo.PastDelivery",
                c => new
                    {
                        DeliveryID = c.Int(nullable: false),
                        ActualArrivalDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DeliveryID)
                .ForeignKey("dbo.Delivery", t => t.DeliveryID)
                .Index(t => t.DeliveryID);
            
            DropColumn("dbo.Delivery", "ExpectedArrivalDate");
            DropColumn("dbo.Delivery", "ActualArrivalDate");
            DropColumn("dbo.Delivery", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Delivery", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Delivery", "ActualArrivalDate", c => c.DateTime());
            AddColumn("dbo.Delivery", "ExpectedArrivalDate", c => c.DateTime());
            DropForeignKey("dbo.PastDelivery", "DeliveryID", "dbo.Delivery");
            DropForeignKey("dbo.DurrentDelivery", "DeliveryID", "dbo.Delivery");
            DropIndex("dbo.PastDelivery", new[] { "DeliveryID" });
            DropIndex("dbo.DurrentDelivery", new[] { "DeliveryID" });
            DropTable("dbo.PastDelivery");
            DropTable("dbo.DurrentDelivery");
            RenameTable(name: "dbo.Delivery", newName: "Deliveries");
        }
    }
}
