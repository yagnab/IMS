namespace IMS.BL.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeValidNullToNotNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.UserAccounts", "Username", c => c.String(nullable: false));
            AlterColumn("dbo.UserAccounts", "Password", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserAccounts", "Password", c => c.Binary());
            AlterColumn("dbo.UserAccounts", "Username", c => c.String());
            AlterColumn("dbo.Items", "Description", c => c.String());
        }
    }
}
