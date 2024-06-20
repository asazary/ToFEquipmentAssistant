using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToFEA.Migrations
{
    /// <inheritdoc />
    public partial class Migr17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AugmentationStats_Equipments_EquipmentId",
                table: "AugmentationStats");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Equipments_EquipmentId",
                table: "Stats");

            migrationBuilder.AlterColumn<int>(
                name: "EquipmentId",
                table: "Stats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EquipmentId",
                table: "AugmentationStats",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AugmentationStats_Equipments_EquipmentId",
                table: "AugmentationStats",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Equipments_EquipmentId",
                table: "Stats",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AugmentationStats_Equipments_EquipmentId",
                table: "AugmentationStats");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Equipments_EquipmentId",
                table: "Stats");

            migrationBuilder.AlterColumn<int>(
                name: "EquipmentId",
                table: "Stats",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "EquipmentId",
                table: "AugmentationStats",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_AugmentationStats_Equipments_EquipmentId",
                table: "AugmentationStats",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Equipments_EquipmentId",
                table: "Stats",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id");
        }
    }
}
