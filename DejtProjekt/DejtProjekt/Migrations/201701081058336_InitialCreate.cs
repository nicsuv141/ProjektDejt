namespace DejtProjekt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.File",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false),
                        ContentType = c.String(),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.UserModel", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserModel",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        NewPassword = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Hidden = c.Boolean(),
                        Gender = c.Boolean(),
                        Phone = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        LookingFor = c.Int(nullable: false),
                        PersonalNumber = c.String(),
                        ReturnUrl = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Friend",
                c => new
                    {
                        FriendId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FId = c.Int(nullable: false),
                        UserModel_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.FriendId)
                .ForeignKey("dbo.UserModel", t => t.FId)
                .ForeignKey("dbo.UserModel", t => t.UserId)
                .ForeignKey("dbo.UserModel", t => t.UserModel_UserID)
                .Index(t => t.UserId)
                .Index(t => t.FId)
                .Index(t => t.UserModel_UserID);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        AuthorId = c.Int(nullable: false),
                        WallId = c.Int(nullable: false),
                        UserModel_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.UserModel", t => t.AuthorId)
                .ForeignKey("dbo.UserModel", t => t.WallId)
                .ForeignKey("dbo.UserModel", t => t.UserModel_UserID)
                .Index(t => t.AuthorId)
                .Index(t => t.WallId)
                .Index(t => t.UserModel_UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Post", "UserModel_UserID", "dbo.UserModel");
            DropForeignKey("dbo.Post", "WallId", "dbo.UserModel");
            DropForeignKey("dbo.Post", "AuthorId", "dbo.UserModel");
            DropForeignKey("dbo.Friend", "UserModel_UserID", "dbo.UserModel");
            DropForeignKey("dbo.Friend", "UserId", "dbo.UserModel");
            DropForeignKey("dbo.Friend", "FId", "dbo.UserModel");
            DropForeignKey("dbo.File", "UserId", "dbo.UserModel");
            DropIndex("dbo.Post", new[] { "UserModel_UserID" });
            DropIndex("dbo.Post", new[] { "WallId" });
            DropIndex("dbo.Post", new[] { "AuthorId" });
            DropIndex("dbo.Friend", new[] { "UserModel_UserID" });
            DropIndex("dbo.Friend", new[] { "FId" });
            DropIndex("dbo.Friend", new[] { "UserId" });
            DropIndex("dbo.File", new[] { "UserId" });
            DropTable("dbo.Post");
            DropTable("dbo.Friend");
            DropTable("dbo.UserModel");
            DropTable("dbo.File");
        }
    }
}
