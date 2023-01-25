using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Test.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Payroll = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Forename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "Address2", "DateOfBirth", "Email", "Forename", "Mobile", "Payroll", "PostCode", "StartDate", "Surname", "Telephone" },
                values: new object[,]
                {
                    { new Guid("5d4e6479-6a42-485b-bec9-aa560fa31cdd"), "115 Spinney Road", "Luton", "11/5/1974", "gerry.jackson@bt.com", "Jerry", "6987457", "JACK13", "LU33DF", "18/04/2013", "Jackson", "2050508" },
                    { new Guid("a815e489-caab-42de-86a5-5d2a5b7c298c"), "12 Foreman road", "London", "26/01/1955", "nomadic20@hotmail.co.uk", "John ", "987654231", "COOP08", "GU12 6JW", "18/04/2013", "William", "12345678" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
