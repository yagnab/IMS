namespace IMS.BL.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTPTToDelivery : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Deliveries", newName: "Delivery");
            CreateTable(
                "dbo.CurrentDelivery",
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
        }
        
        public override void Down()
        {
            AddColumn("dbo.Delivery", "ActualArrivalDate", c => c.DateTime());
            AddColumn("dbo.Delivery", "ExpectedArrivalDate", c => c.DateTime());
            DropForeignKey("dbo.PastDelivery", "DeliveryID", "dbo.Delivery");
            DropForeignKey("dbo.CurrentDelivery", "DeliveryID", "dbo.Delivery");
            DropIndex("dbo.PastDelivery", new[] { "DeliveryID" });
            DropIndex("dbo.CurrentDelivery", new[] { "DeliveryID" });
            DropTable("dbo.PastDelivery");
            DropTable("dbo.CurrentDelivery");
            RenameTable(name: "dbo.Delivery", newName: "Deliveries");
        }
    }
}
