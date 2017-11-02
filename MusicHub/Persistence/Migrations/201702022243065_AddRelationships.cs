namespace MusicHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationships : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Relationships",
                c => new
                    {
                        FollowerId = c.String(nullable: false, maxLength: 128),
                        FolloweeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FollowerId, t.FolloweeId })
                .ForeignKey("dbo.AspNetUsers", t => t.FolloweeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.FollowerId)
                .Index(t => t.FollowerId)
                .Index(t => t.FolloweeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Relationships", "FollowerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Relationships", "FolloweeId", "dbo.AspNetUsers");
            DropIndex("dbo.Relationships", new[] { "FolloweeId" });
            DropIndex("dbo.Relationships", new[] { "FollowerId" });
            DropTable("dbo.Relationships");
        }
    }
}
