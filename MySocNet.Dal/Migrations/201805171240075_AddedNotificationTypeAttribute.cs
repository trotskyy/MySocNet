namespace MySocNet.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNotificationTypeAttribute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "NotificationType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "NotificationType");
        }
    }
}
