using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TOP.API.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    company = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    teacher = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VocationalQualificationUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    vocationalQualificationUnit = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VocationalQualificationUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TOP",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 30, nullable: false),
                    Reserved = table.Column<bool>(nullable: false),
                    ReservationEnds = table.Column<DateTime>(nullable: false),
                    Accepted = table.Column<bool>(nullable: false),
                    Info = table.Column<string>(maxLength: 1000, nullable: true),
                    Company = table.Column<Guid>(nullable: false),
                    Teacher = table.Column<Guid>(nullable: false),
                    Address = table.Column<Guid>(nullable: false),
                    VocationalQualificationUnit = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TOP_Addresses_Address",
                        column: x => x.Address,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TOP_Companys_Company",
                        column: x => x.Company,
                        principalTable: "Companys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TOP_Teachers_Teacher",
                        column: x => x.Teacher,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TOP_VocationalQualificationUnits_VocationalQualificationUnit",
                        column: x => x.VocationalQualificationUnit,
                        principalTable: "VocationalQualificationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TOP_Address",
                table: "TOP",
                column: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_TOP_Company",
                table: "TOP",
                column: "Company");

            migrationBuilder.CreateIndex(
                name: "IX_TOP_Teacher",
                table: "TOP",
                column: "Teacher");

            migrationBuilder.CreateIndex(
                name: "IX_TOP_VocationalQualificationUnit",
                table: "TOP",
                column: "VocationalQualificationUnit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "TOP");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Companys");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "VocationalQualificationUnits");
        }
    }
}
