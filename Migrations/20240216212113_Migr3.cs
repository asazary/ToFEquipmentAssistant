using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToFEA.Migrations
{
    /// <inheritdoc />
    public partial class Migr3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PossibleTitanStats_EquipmentTypes_SlotId",
                table: "PossibleTitanStats");

            migrationBuilder.DropIndex(
                name: "IX_PossibleTitanStats_SlotId",
                table: "PossibleTitanStats");

            migrationBuilder.DropColumn(
                name: "SlotId",
                table: "PossibleTitanStats");

            migrationBuilder.AddColumn<int>(
                name: "InitValue",
                table: "PossibleStats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EquipmentTypePossibleTitanStat",
                columns: table => new
                {
                    PossibleTitanStatId = table.Column<int>(type: "INTEGER", nullable: false),
                    SlotId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTypePossibleTitanStat", x => new { x.PossibleTitanStatId, x.SlotId });
                    table.ForeignKey(
                        name: "FK_EquipmentTypePossibleTitanStat_EquipmentTypes_SlotId",
                        column: x => x.SlotId,
                        principalTable: "EquipmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentTypePossibleTitanStat_PossibleTitanStats_PossibleTitanStatId",
                        column: x => x.PossibleTitanStatId,
                        principalTable: "PossibleTitanStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTypePossibleTitanStat_SlotId",
                table: "EquipmentTypePossibleTitanStat",
                column: "SlotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentTypePossibleTitanStat");

            migrationBuilder.DropColumn(
                name: "InitValue",
                table: "PossibleStats");

            migrationBuilder.AddColumn<int>(
                name: "SlotId",
                table: "PossibleTitanStats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PossibleTitanStats_SlotId",
                table: "PossibleTitanStats",
                column: "SlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_PossibleTitanStats_EquipmentTypes_SlotId",
                table: "PossibleTitanStats",
                column: "SlotId",
                principalTable: "EquipmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
