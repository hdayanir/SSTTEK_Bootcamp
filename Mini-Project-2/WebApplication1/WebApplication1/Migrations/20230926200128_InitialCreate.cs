using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OtoparkAracLists",
                columns: table => new
                {
                    ArabaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArabaSinif = table.Column<int>(type: "int", nullable: false),
                    Renk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plaka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelYili = table.Column<int>(type: "int", nullable: false),
                    ModelAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeygirGucu = table.Column<int>(type: "int", nullable: false),
                    OtomatikPilot = table.Column<bool>(type: "bit", nullable: false),
                    ArabaFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GirisZamani = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CikisZamani = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BagajHacmi = table.Column<int>(type: "int", nullable: true),
                    YedekLastik = table.Column<bool>(type: "bit", nullable: false),
                    OtoparkUcreti = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtoparkAracLists", x => x.ArabaId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtoparkAracLists");
        }
    }
}
