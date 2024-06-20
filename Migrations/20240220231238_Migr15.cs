using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToFEA.Migrations
{
    /// <inheritdoc />
    public partial class Migr15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TitanStats_Equipments_SlotId",
                table: "TitanStats");

            migrationBuilder.RenameColumn(
                name: "SlotId",
                table: "TitanStats",
                newName: "EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_TitanStats_SlotId",
                table: "TitanStats",
                newName: "IX_TitanStats_EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TitanStats_Equipments_EquipmentId",
                table: "TitanStats",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TitanStats_Equipments_EquipmentId",
                table: "TitanStats");

            migrationBuilder.RenameColumn(
                name: "EquipmentId",
                table: "TitanStats",
                newName: "SlotId");

            migrationBuilder.RenameIndex(
                name: "IX_TitanStats_EquipmentId",
                table: "TitanStats",
                newName: "IX_TitanStats_SlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_TitanStats_Equipments_SlotId",
                table: "TitanStats",
                column: "SlotId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
