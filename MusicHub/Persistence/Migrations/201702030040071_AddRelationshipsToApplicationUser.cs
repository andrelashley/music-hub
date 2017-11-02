namespace MusicHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationshipsToApplicationUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Relationships", "FolloweeId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Relationships", "FolloweeId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Relationships", "FolloweeId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Relationships", "FolloweeId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
