namespace MySocNet.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPasswordSaltMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PasswodHash", c => c.String());
            AddColumn("dbo.Users", "PasswodSalt", c => c.String());
            AlterColumn("dbo.Notifications", "NotificationMessage", c => c.String());
            DropColumn("dbo.Users", "Passwod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Passwod", c => c.String());
            AlterColumn("dbo.Notifications", "NotificationMessage", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "PasswodSalt");
            DropColumn("dbo.Users", "PasswodHash");
        }
    }
}
