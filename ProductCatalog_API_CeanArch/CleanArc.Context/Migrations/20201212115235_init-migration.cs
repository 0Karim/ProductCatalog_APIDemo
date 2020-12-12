using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArc.Context.Migrations
{
    public partial class initmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "LastUpdate", "Name", "Photo", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 12, 12, 13, 52, 34, 940, DateTimeKind.Local).AddTicks(5467), "Product 1", null, 1000.0 },
                    { 18, new DateTime(2020, 12, 12, 13, 52, 34, 941, DateTimeKind.Local).AddTicks(9514), "Product 18", null, 18000.0 },
                    { 17, new DateTime(2020, 12, 12, 13, 52, 34, 941, DateTimeKind.Local).AddTicks(9512), "Product 17", null, 17000.0 },
                    { 16, new DateTime(2020, 12, 12, 13, 52, 34, 941, DateTimeKind.Local).AddTicks(9510), "Product 16", null, 16000.0 },
                    { 15, new DateTime(2020, 12, 12, 13, 52, 34, 941, DateTimeKind.Local).AddTicks(9508), "Product 15", null, 15000.0 },
                    { 14, new DateTime(2020, 12, 12, 13, 52, 34, 941, DateTimeKind.Local).AddTicks(9506), "Product 14", null, 14000.0 },
                    { 13, new DateTime(2020, 12, 12, 13, 52, 34, 941, DateTimeKind.Local).AddTicks(9504), "Product 13", null, 13000.0 },
                    { 12, new DateTime(2020, 12, 12, 13, 52, 34, 941, DateTimeKind.Local).AddTicks(9502), "Product 12    ", null, 12000.0 },
                    { 11, new DateTime(2020, 12, 12, 13, 52, 34, 941, DateTimeKind.Local).AddTicks(9500), "Product 11", null, 11000.0 },
                    { 10, new DateTime(2020, 12, 12, 13, 52, 34, 941, DateTimeKind.Local).AddTicks(9498), "Product 10", null, 10000.0 },
                    { 9, new DateTime(2020, 12, 12, 13, 52, 34, 941, DateTimeKind.Local).AddTicks(9496), "Product 9", null, 9000.0 },
                    { 8, new DateTime(2020, 12, 12, 13, 52, 34, 941, DateTimeKind.Local).AddTicks(9494), "Product 8", null, 8000.0 },
                    { 7, new DateTime(2020, 12, 12, 13, 52, 34, 941, DateTimeKind.Local).AddTicks(9492), "Product 7", null, 7000.0 },
                    { 6, new DateTime(2020, 12, 12, 13, 52, 34, 941, DateTimeKind.Local).AddTicks(9490), "Product 6", null, 6000.0 },
                    { 5, new DateTime(2020, 12, 12, 13, 52, 34, 941, DateTimeKind.Local).AddTicks(9489), "Product 5", null, 5000.0 },
                    { 4, new DateTime(2020, 12, 12, 13, 52, 34, 941, DateTimeKind.Local).AddTicks(9487), "Product 4", null, 4000.0 },
                    { 3, new DateTime(2020, 12, 12, 13, 52, 34, 941, DateTimeKind.Local).AddTicks(9483), "Product 3", null, 3000.0 },
                    { 2, new DateTime(2020, 12, 12, 13, 52, 34, 941, DateTimeKind.Local).AddTicks(9437), "Product 2", null, 2000.0 },
                    { 19, new DateTime(2020, 12, 12, 13, 52, 34, 941, DateTimeKind.Local).AddTicks(9516), "Product 19", null, 19000.0 },
                    { 20, new DateTime(2020, 12, 12, 13, 52, 34, 941, DateTimeKind.Local).AddTicks(9518), "Product 20", null, 20000.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
