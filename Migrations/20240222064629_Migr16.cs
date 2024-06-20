using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToFEA.Migrations
{
    /// <inheritdoc />
    public partial class Migr16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTypePossibleStat_PossibleStats_PossibleStatId",
                table: "EquipmentTypePossibleStat");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTypePossibleTitanStat_EquipmentTypes_EquipmentTypeId",
                table: "EquipmentTypePossibleTitanStat");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTypePossibleTitanStat_PossibleTitanStats_PossibleTitanStatId",
                table: "EquipmentTypePossibleTitanStat");

            migrationBuilder.DropTable(
                name: "BaseStatEquipmentType");

            migrationBuilder.RenameColumn(
                name: "PossibleTitanStatId",
                table: "EquipmentTypePossibleTitanStat",
                newName: "PossibleTitanStatsId");

            migrationBuilder.RenameColumn(
                name: "EquipmentTypeId",
                table: "EquipmentTypePossibleTitanStat",
                newName: "EquipmentTypesId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentTypePossibleTitanStat_PossibleTitanStatId",
                table: "EquipmentTypePossibleTitanStat",
                newName: "IX_EquipmentTypePossibleTitanStat_PossibleTitanStatsId");

            migrationBuilder.RenameColumn(
                name: "PossibleStatId",
                table: "EquipmentTypePossibleStat",
                newName: "PossibleStatsId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentTypePossibleStat_PossibleStatId",
                table: "EquipmentTypePossibleStat",
                newName: "IX_EquipmentTypePossibleStat_PossibleStatsId");

            migrationBuilder.AddColumn<int>(
                name: "SlotId",
                table: "BaseStats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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
                name: "FK_EquipmentTypePossibleStat_PossibleStats_PossibleStatsId",
                table: "EquipmentTypePossibleStat",
                column: "PossibleStatsId",
                principalTable: "PossibleStats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTypePossibleTitanStat_EquipmentTypes_EquipmentTypesId",
                table: "EquipmentTypePossibleTitanStat",
                column: "EquipmentTypesId",
                principalTable: "EquipmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTypePossibleTitanStat_PossibleTitanStats_PossibleTitanStatsId",
                table: "EquipmentTypePossibleTitanStat",
                column: "PossibleTitanStatsId",
                principalTable: "PossibleTitanStats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseStats_EquipmentTypes_SlotId",
                table: "BaseStats");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTypePossibleStat_PossibleStats_PossibleStatsId",
                table: "EquipmentTypePossibleStat");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTypePossibleTitanStat_EquipmentTypes_EquipmentTypesId",
                table: "EquipmentTypePossibleTitanStat");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTypePossibleTitanStat_PossibleTitanStats_PossibleTitanStatsId",
                table: "EquipmentTypePossibleTitanStat");

            migrationBuilder.DropIndex(
                name: "IX_BaseStats_SlotId",
                table: "BaseStats");

            migrationBuilder.DropColumn(
                name: "SlotId",
                table: "BaseStats");

            migrationBuilder.RenameColumn(
                name: "PossibleTitanStatsId",
                table: "EquipmentTypePossibleTitanStat",
                newName: "PossibleTitanStatId");

            migrationBuilder.RenameColumn(
                name: "EquipmentTypesId",
                table: "EquipmentTypePossibleTitanStat",
                newName: "EquipmentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentTypePossibleTitanStat_PossibleTitanStatsId",
                table: "EquipmentTypePossibleTitanStat",
                newName: "IX_EquipmentTypePossibleTitanStat_PossibleTitanStatId");

            migrationBuilder.RenameColumn(
                name: "PossibleStatsId",
                table: "EquipmentTypePossibleStat",
                newName: "PossibleStatId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentTypePossibleStat_PossibleStatsId",
                table: "EquipmentTypePossibleStat",
                newName: "IX_EquipmentTypePossibleStat_PossibleStatId");

            migrationBuilder.CreateTable(
                name: "BaseStatEquipmentType",
                columns: table => new
                {
                    BaseStatId = table.Column<int>(type: "INTEGER", nullable: false),
                    SlotId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseStatEquipmentType", x => new { x.BaseStatId, x.SlotId });
                    table.ForeignKey(
                        name: "FK_BaseStatEquipmentType_BaseStats_BaseStatId",
                        column: x => x.BaseStatId,
                        principalTable: "BaseStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseStatEquipmentType_EquipmentTypes_SlotId",
                        column: x => x.SlotId,
                        principalTable: "EquipmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseStatEquipmentType_SlotId",
                table: "BaseStatEquipmentType",
                column: "SlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTypePossibleStat_PossibleStats_PossibleStatId",
                table: "EquipmentTypePossibleStat",
                column: "PossibleStatId",
                principalTable: "PossibleStats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTypePossibleTitanStat_EquipmentTypes_EquipmentTypeId",
                table: "EquipmentTypePossibleTitanStat",
                column: "EquipmentTypeId",
                principalTable: "EquipmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTypePossibleTitanStat_PossibleTitanStats_PossibleTitanStatId",
                table: "EquipmentTypePossibleTitanStat",
                column: "PossibleTitanStatId",
                principalTable: "PossibleTitanStats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
