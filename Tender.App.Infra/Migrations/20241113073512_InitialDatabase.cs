using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tender.App.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MinAmount = table.Column<long>(type: "bigint", nullable: false),
                    MaxAmount = table.Column<long>(type: "bigint", nullable: false),
                    CreatedTimeUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedTimeUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BidDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    IsWinner = table.Column<bool>(type: "bit", nullable: false),
                    BidId = table.Column<int>(type: "int", nullable: true),
                    CreatedTimeUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedTimeUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BidDetails_Bids_BidId",
                        column: x => x.BidId,
                        principalTable: "Bids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BidDetails_BidId",
                table: "BidDetails",
                column: "BidId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BidDetails");

            migrationBuilder.DropTable(
                name: "Bids");
        }
    }
}
