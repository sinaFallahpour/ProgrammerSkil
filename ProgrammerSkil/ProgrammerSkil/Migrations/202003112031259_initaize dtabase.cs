namespace ProgrammerSkil.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initaizedtabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBL_Cooperation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        ResumeLink = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBL_User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TBL_User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        RoleType = c.String(nullable: false),
                        RegDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TBL_FeedBacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        RegDate = c.DateTime(nullable: false),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBL_User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TBL_Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Photo = c.String(),
                        SamplesId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBL_Samples", t => t.SamplesId)
                .Index(t => t.SamplesId);
            
            CreateTable(
                "dbo.TBL_Samples",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Link = c.String(),
                        Description = c.String(nullable: false),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBL_User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TBL_Setting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Theme = c.String(),
                        Photo = c.String(),
                        AboutUs = c.String(),
                        SocialMedia = c.String(),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBL_User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TBL_Skill",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TBL_SkillUserProfile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SkillId = c.Int(),
                        UserProfileId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBL_Skill", t => t.SkillId)
                .ForeignKey("dbo.TBL_UserProfile", t => t.UserProfileId)
                .Index(t => t.SkillId)
                .Index(t => t.UserProfileId);
            
            CreateTable(
                "dbo.TBL_UserProfile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Biography = c.String(),
                        Photo = c.String(),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBL_User", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBL_UserProfile", "UserId", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_SkillUserProfile", "UserProfileId", "dbo.TBL_UserProfile");
            DropForeignKey("dbo.TBL_SkillUserProfile", "SkillId", "dbo.TBL_Skill");
            DropForeignKey("dbo.TBL_Setting", "UserId", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_Images", "SamplesId", "dbo.TBL_Samples");
            DropForeignKey("dbo.TBL_Samples", "UserId", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_FeedBacks", "UserId", "dbo.TBL_User");
            DropForeignKey("dbo.TBL_Cooperation", "UserId", "dbo.TBL_User");
            DropIndex("dbo.TBL_UserProfile", new[] { "UserId" });
            DropIndex("dbo.TBL_SkillUserProfile", new[] { "UserProfileId" });
            DropIndex("dbo.TBL_SkillUserProfile", new[] { "SkillId" });
            DropIndex("dbo.TBL_Setting", new[] { "UserId" });
            DropIndex("dbo.TBL_Samples", new[] { "UserId" });
            DropIndex("dbo.TBL_Images", new[] { "SamplesId" });
            DropIndex("dbo.TBL_FeedBacks", new[] { "UserId" });
            DropIndex("dbo.TBL_Cooperation", new[] { "UserId" });
            DropTable("dbo.TBL_UserProfile");
            DropTable("dbo.TBL_SkillUserProfile");
            DropTable("dbo.TBL_Skill");
            DropTable("dbo.TBL_Setting");
            DropTable("dbo.TBL_Samples");
            DropTable("dbo.TBL_Images");
            DropTable("dbo.TBL_FeedBacks");
            DropTable("dbo.TBL_User");
            DropTable("dbo.TBL_Cooperation");
        }
    }
}
