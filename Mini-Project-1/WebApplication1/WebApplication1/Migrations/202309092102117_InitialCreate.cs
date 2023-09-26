namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OtoparkAracLists",
                c => new
                {
                    ArabaId = c.Int(nullable: false, identity: true),
                    ArabaSinif = c.Int(),
                    Renk = c.String(),
                    Plaka = c.String(),
                    ModelYili = c.Int(),
                    ModelAdi = c.String(),
                    BeygirGucu = c.Int(),
                    OtomatikPilot = c.Boolean(),
                    ArabaFiyat = c.Decimal(),
                    GirisZamani = c.DateTime(),
                    CikisZamani = c.DateTime(),
                    BagajHacmi = c.Int(),
                    YedekLastik = c.Boolean(),
                    OtoparkUcreti = c.Decimal(),
                })
                .PrimaryKey(t => t.ArabaId);
        }

        public override void Down()
        {
            DropTable("dbo.OtoparkAracLists");
        }
    }
}
