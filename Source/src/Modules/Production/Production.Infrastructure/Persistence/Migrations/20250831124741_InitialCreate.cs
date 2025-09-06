using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Production.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PolisherAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolisherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolisherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolisherAssignments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PolisherAssignmentItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BagTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BagTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BagWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Dozens = table.Column<int>(type: "int", nullable: false),
                    TotalWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AvgWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductAvgWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToleranceDiff = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolisherAssignmentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolisherAssignmentItems_PolisherAssignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "PolisherAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PolisherAssignmentItems_AssignmentId",
                table: "PolisherAssignmentItems",
                column: "AssignmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PolisherAssignmentItems");

            migrationBuilder.DropTable(
                name: "PolisherAssignments");
        }
    }
}
