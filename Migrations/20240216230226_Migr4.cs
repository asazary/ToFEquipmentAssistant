using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToFEA.Migrations
{
    /// <inheritdoc />
    public partial class Migr4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseStats_Equipments_SlotId",
                table: "BaseStats");

            migrationBuilder.DropTable(
                name: "EquipmentTypePossibleTitanStat");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "BaseStats",
                newName: "Value3");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentTypeId",
                table: "PossibleTitanStats",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "MinUpgradeValue",
                table: "PossibleStats",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<float>(
                name: "MaxUpgradeValue",
                table: "PossibleStats",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<float>(
                name: "InitValue",
                table: "PossibleStats",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentTypeId",
                table: "PossibleStats",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "BaseStats",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatId",
                table: "BaseStats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Value1",
                table: "BaseStats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Value2",
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
                name: "IX_BaseStats_EquipmentId",
                table: "BaseStats",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseStats_StatId",
                table: "BaseStats",
                column: "StatId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseStats_EquipmentTypes_SlotId",
                table: "BaseStats",
                column: "SlotId",
                principalTable: "EquipmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseStats_Equipments_EquipmentId",
                table: "BaseStats",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseStats_PossibleStats_StatId",
                table: "BaseStats",
                column: "StatId",
                principalTable: "PossibleStats",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseStats_EquipmentTypes_SlotId",
                table: "BaseStats");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseStats_Equipments_EquipmentId",
                table: "BaseStats");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseStats_PossibleStats_StatId",
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
                name: "IX_BaseStats_EquipmentId",
                table: "BaseStats");

            migrationBuilder.DropIndex(
                name: "IX_BaseStats_StatId",
                table: "BaseStats");

            migrationBuilder.DropColumn(
                name: "EquipmentTypeId",
                table: "PossibleTitanStats");

            migrationBuilder.DropColumn(
                name: "EquipmentTypeId",
                table: "PossibleStats");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "BaseStats");

            migrationBuilder.DropColumn(
                name: "StatId",
                table: "BaseStats");

            migrationBuilder.DropColumn(
                name: "Value1",
                table: "BaseStats");

            migrationBuilder.DropColumn(
                name: "Value2",
                table: "BaseStats");

            migrationBuilder.RenameColumn(
                name: "Value3",
                table: "BaseStats",
                newName: "Value");

            migrationBuilder.AlterColumn<int>(
                name: "MinUpgradeValue",
                table: "PossibleStats",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AlterColumn<int>(
                name: "MaxUpgradeValue",
                table: "PossibleStats",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AlterColumn<int>(
                name: "InitValue",
                table: "PossibleStats",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BaseStats_Equipments_SlotId",
                table: "BaseStats",
                column: "SlotId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
