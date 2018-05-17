namespace MySocNet.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedNotification_AddedUsersRelationIsViewed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "UserId", "dbo.Users");
            DropIndex("dbo.Notifications", new[] { "UserId" });
            AddColumn("dbo.Users", "PasswordHash", c => c.String());
            AddColumn("dbo.Users", "PasswordSalt", c => c.String());
            AddColumn("dbo.UsersRelations", "IsViewed", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "PasswodHash");
            DropColumn("dbo.Users", "PasswodSalt");
            DropTable("dbo.Notifications");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        NotificationMessage = c.String(),
                        IsRead = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "PasswodSalt", c => c.String());
            AddColumn("dbo.Users", "PasswodHash", c => c.String());
            DropColumn("dbo.UsersRelations", "IsViewed");
            DropColumn("dbo.Users", "PasswordSalt");
            DropColumn("dbo.Users", "PasswordHash");
            CreateIndex("dbo.Notifications", "UserId");
            AddForeignKey("dbo.Notifications", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
