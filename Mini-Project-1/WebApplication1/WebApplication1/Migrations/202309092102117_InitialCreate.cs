namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailListDetails",
                c => new
                    {
                        EmailListDetailId = c.Int(nullable: false, identity: true),
                        FromAdress = c.String(),
                        ToAdress = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        SentTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EmailListDetailId);
            
            CreateTable(
                "dbo.EmailLists",
                c => new
                    {
                        EmailListId = c.Int(nullable: false, identity: true),
                        ToAdress = c.String(),
                    })
                .PrimaryKey(t => t.EmailListId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmailLists");
            DropTable("dbo.EmailListDetails");
        }
    }
}
