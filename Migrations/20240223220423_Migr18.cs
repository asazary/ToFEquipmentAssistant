using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToFEA.Migrations
{
    /// <inheritdoc />
    public partial class Migr18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseStats_Equipments_EquipmentId",
                table: "BaseStats");

            migrationBuilder.DropIndex(
                name: "IX_BaseStats_EquipmentId",
                table: "BaseStats");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "BaseStats");

            migrationBuilder.CreateTable(
                name: "BaseStatEquipment",
                columns: table => new
                {
                    BaseStatsId = table.Column<int>(type: "INTEGER", nullable: false),
                    EquipmentsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseStatEquipment", x => new { x.BaseStatsId, x.EquipmentsId });
                    table.ForeignKey(
                        name: "FK_BaseStatEquipment_BaseStats_BaseStatsId",
                        column: x => x.BaseStatsId,
                        principalTable: "BaseStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseStatEquipment_Equipments_EquipmentsId",
                        column: x => x.EquipmentsId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseStatEquipment_EquipmentsId",
                table: "BaseStatEquipment",
                column: "EquipmentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseStatEquipment");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "BaseStats",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseStats_EquipmentId",
                table: "BaseStats",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseStats_Equipments_EquipmentId",
                table: "BaseStats",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id");
        }
    }
}
