using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_api.Migrations
{
    public partial class migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataFiles = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "varchar(50)", nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false),
                    Nivel = table.Column<short>(type: "smallint", nullable: false),
                    RelNF = table.Column<short>(type: "smallint", nullable: false),
                    RelNFICMS = table.Column<short>(type: "smallint", nullable: false),
                    RelNFICMSST = table.Column<short>(type: "smallint", nullable: false),
                    RelNFUF = table.Column<short>(type: "smallint", nullable: false),
                    RelNFQTD = table.Column<short>(type: "smallint", nullable: false),
                    RelNFQTDUF = table.Column<short>(type: "smallint", nullable: false),
                    RelCF = table.Column<short>(type: "smallint", nullable: false),
                    RelCFTOT = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
