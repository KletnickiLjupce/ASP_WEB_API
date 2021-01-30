namespace API_HM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNomenArticleLevel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NomenArticleLevel",
                c => new
                    {
                        NomenArticleLevelId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.NomenArticleLevelId);
            
            AddColumn("dbo.Article", "NomenArticleLevelId", c => c.Int());
            CreateIndex("dbo.Article", "NomenArticleLevelId");
            AddForeignKey("dbo.Article", "NomenArticleLevelId", "dbo.NomenArticleLevel", "NomenArticleLevelId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Article", "NomenArticleLevelId", "dbo.NomenArticleLevel");
            DropIndex("dbo.Article", new[] { "NomenArticleLevelId" });
            DropColumn("dbo.Article", "NomenArticleLevelId");
            DropTable("dbo.NomenArticleLevel");
        }
    }
}
