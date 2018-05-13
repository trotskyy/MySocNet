namespace MySocNet.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromId = c.Int(nullable: false),
                        ToId = c.Int(nullable: false),
                        Text = c.String(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        Sent = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.FromId)
                .ForeignKey("dbo.Users", t => t.ToId)
                .Index(t => t.FromId)
                .Index(t => t.ToId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Passwod = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CurrentState = c.String(),
                        CurrentCity = c.String(),
                        StateOfBirth = c.String(),
                        CityOfBirth = c.String(),
                        AboutSelf = c.String(),
                        DateOfBirth = c.DateTime(nullable: false, storeType: "date"),
                        IsMale = c.Boolean(),
                        AvatarPath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        ThreadId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        Published = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.ConvThreads", t => t.ThreadId, cascadeDelete: true)
                .Index(t => t.ThreadId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.ConvThreads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Topic = c.String(),
                        Description = c.String(),
                        AvatarPath = c.String(),
                        ModeratorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ModeratorId)
                .Index(t => t.ModeratorId);
            
            CreateTable(
                "dbo.UsersRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PublisherId = c.Int(nullable: false),
                        SubscriberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.PublisherId)
                .ForeignKey("dbo.Users", t => t.SubscriberId)
                .Index(t => t.PublisherId)
                .Index(t => t.SubscriberId);
            
            CreateTable(
                "dbo.ConvThreadUsers",
                c => new
                    {
                        ConvThread_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ConvThread_Id, t.User_Id })
                .ForeignKey("dbo.ConvThreads", t => t.ConvThread_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.ConvThread_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "ToId", "dbo.Users");
            DropForeignKey("dbo.Messages", "FromId", "dbo.Users");
            DropForeignKey("dbo.UsersRelations", "SubscriberId", "dbo.Users");
            DropForeignKey("dbo.UsersRelations", "PublisherId", "dbo.Users");
            DropForeignKey("dbo.ConvThreadUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.ConvThreadUsers", "ConvThread_Id", "dbo.ConvThreads");
            DropForeignKey("dbo.Posts", "ThreadId", "dbo.ConvThreads");
            DropForeignKey("dbo.ConvThreads", "ModeratorId", "dbo.Users");
            DropForeignKey("dbo.Posts", "AuthorId", "dbo.Users");
            DropIndex("dbo.ConvThreadUsers", new[] { "User_Id" });
            DropIndex("dbo.ConvThreadUsers", new[] { "ConvThread_Id" });
            DropIndex("dbo.UsersRelations", new[] { "SubscriberId" });
            DropIndex("dbo.UsersRelations", new[] { "PublisherId" });
            DropIndex("dbo.ConvThreads", new[] { "ModeratorId" });
            DropIndex("dbo.Posts", new[] { "AuthorId" });
            DropIndex("dbo.Posts", new[] { "ThreadId" });
            DropIndex("dbo.Messages", new[] { "ToId" });
            DropIndex("dbo.Messages", new[] { "FromId" });
            DropTable("dbo.ConvThreadUsers");
            DropTable("dbo.UsersRelations");
            DropTable("dbo.ConvThreads");
            DropTable("dbo.Posts");
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
        }
    }
}
