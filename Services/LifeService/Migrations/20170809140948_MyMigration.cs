using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LifeService.Migrations
{
    public partial class MyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MR0003_COMPANY",
                columns: table => new
                {
                    MR0003_PK = table.Column<Guid>(maxLength: 50, nullable: false),
                    MR0003_Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MR0003_COMPANY", x => x.MR0003_PK);
                });

            migrationBuilder.CreateTable(
                name: "MR0001_USER_MSTR",
                columns: table => new
                {
                    MR0001_PK = table.Column<Guid>(maxLength: 50, nullable: false),
                    MR0001_Company_RK = table.Column<Guid>(nullable: false),
                    MR0001_Email = table.Column<string>(nullable: true),
                    MR0001_ID = table.Column<int>(type: "int", nullable: false),
                    MR0001_Name = table.Column<string>(nullable: true),
                    MR0001_PassWord = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MR0001_USER_MSTR", x => x.MR0001_PK);
                    table.ForeignKey(
                        name: "FK_MR0001_USER_MSTR_MR0003_COMPANY_MR0001_Company_RK",
                        column: x => x.MR0001_Company_RK,
                        principalTable: "MR0003_COMPANY",
                        principalColumn: "MR0003_PK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MR0001_USER_MSTR_MR0001_Company_RK",
                table: "MR0001_USER_MSTR",
                column: "MR0001_Company_RK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MR0001_USER_MSTR");

            migrationBuilder.DropTable(
                name: "MR0003_COMPANY");
        }
    }
}
