using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToFEA.Migrations
{
    /// <inheritdoc />
    public partial class Migr6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseStats_EquipmentTypes_SlotId",
                table: "BaseStats");

            migrationBuilder.DropForeignKey(
                name: "FK_PossibleStats_EquipmentTypes_EquipmentTypeId",
                table: "PossibleStats");

            migrationBuilder.DropForeignKey(
                name: "FK_PossibleTitanStats_EquipmentTypes_EquipmentTypeId",
                table: "PossibleTitanStats");

            migrationBuilder.DropIndex(
                name: "IX_PossibleTitanStats_EquipmentTypeId",
                table: "PossibleTitanStats");

            migrationBuilder.DropIndex(
                name: "IX_PossibleStats_EquipmentTypeId",
                table: "PossibleStats");

            migrationBuilder.DropIndex(
                name: "IX_BaseStats_SlotId",
                table: "BaseStats");

            migrationBuilder.DropColumn(
                name: "EquipmentTypeId",
                table: "PossibleTitanStats");

            migrationBuilder.DropColumn(
                name: "EquipmentTypeId",
                table: "PossibleStats");

            migrationBuilder.DropColumn(
                name: "SlotId",
                table: "BaseStats");

            migrationBuilder.AddColumn<int>(
                name: "BaseStatId",
                table: "EquipmentTypes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EquipmentTypePossibleStat",
                columns: table => new
                {
                    EquipmentTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    PossibleStatId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTypePossibleStat", x => new { x.EquipmentTypeId, x.PossibleStatId });
                    table.ForeignKey(
                        name: "FK_EquipmentTypePossibleStat_EquipmentTypes_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalTable: "EquipmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentTypePossibleStat_PossibleStats_PossibleStatId",
                        column: x => x.PossibleStatId,
                        principalTable: "PossibleStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentTypePossibleTitanStat",
                columns: table => new
                {
                    EquipmentTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    PossibleTitanStatId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTypePossibleTitanStat", x => new { x.EquipmentTypeId, x.PossibleTitanStatId });
                    table.ForeignKey(
                        name: "FK_EquipmentTypePossibleTitanStat_EquipmentTypes_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
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
                name: "IX_EquipmentTypes_BaseStatId",
                table: "EquipmentTypes",
                column: "BaseStatId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTypePossibleStat_PossibleStatId",
                table: "EquipmentTypePossibleStat",
                column: "PossibleStatId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTypePossibleTitanStat_PossibleTitanStatId",
                table: "EquipmentTypePossibleTitanStat",
                column: "PossibleTitanStatId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTypes_BaseStats_BaseStatId",
                table: "EquipmentTypes",
                column: "BaseStatId",
                principalTable: "BaseStats",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTypes_BaseStats_BaseStatId",
                table: "EquipmentTypes");

            migrationBuilder.DropTable(
                name: "EquipmentTypePossibleStat");

            migrationBuilder.DropTable(
                name: "EquipmentTypePossibleTitanStat");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentTypes_BaseStatId",
                table: "EquipmentTypes");

            migrationBuilder.DropColumn(
                name: "BaseStatId",
                table: "EquipmentTypes");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentTypeId",
                table: "PossibleTitanStats",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentTypeId",
                table: "PossibleStats",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SlotId",
                table: "BaseStats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PossibleTitanStats_EquipmentTypeId",
                table: "PossibleTitanStats",
                column: "EquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PossibleStats_EquipmentTypeId",
                table: "PossibleStats",
                column: "EquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseStats_SlotId",
                table: "BaseStats",
                column: "SlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseStats_EquipmentTypes_SlotId",
                table: "BaseStats",
                column: "SlotId",
                principalTable: "EquipmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PossibleStats_EquipmentTypes_EquipmentTypeId",
                table: "PossibleStats",
                column: "EquipmentTypeId",
                principalTable: "EquipmentTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PossibleTitanStats_EquipmentTypes_EquipmentTypeId",
                table: "PossibleTitanStats",
                column: "EquipmentTypeId",
                principalTable: "EquipmentTypes",
                principalColumn: "Id");
        }
    }
}
