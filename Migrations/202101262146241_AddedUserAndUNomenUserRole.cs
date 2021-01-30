namespace API_HM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserAndUNomenUserRole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NomenUserRole",
                c => new
                    {
                        NomenUserRoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.NomenUserRoleId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserName = c.String(),
                        NomenUserRoleId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.NomenUserRole", t => t.NomenUserRoleId)
                .Index(t => t.NomenUserRoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "NomenUserRoleId", "dbo.NomenUserRole");
            DropIndex("dbo.User", new[] { "NomenUserRoleId" });
            DropTable("dbo.User");
            DropTable("dbo.NomenUserRole");
        }
    }
}
