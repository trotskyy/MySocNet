namespace MySocNet.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewFeatures : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsModerator", c => c.Boolean(nullable: false));
            AddColumn("dbo.Notifications", "NotificationMessage", c => c.String());
            AlterColumn("dbo.Users", "DateOfBirth", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "DateOfBirth", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.Notifications", "NotificationMessage");
            DropColumn("dbo.Users", "IsModerator");
        }
    }
}
