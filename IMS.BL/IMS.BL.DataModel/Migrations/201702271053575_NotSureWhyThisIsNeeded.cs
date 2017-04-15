namespace IMS.BL.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotSureWhyThisIsNeeded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserAccounts", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserAccounts", "Password", c => c.Binary(nullable: false));
        }
    }
}
