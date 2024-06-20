using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToFEA.Migrations
{
    /// <inheritdoc />
    public partial class Migr7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTypes_BaseStats_BaseStatId",
                table: "EquipmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentTypes_BaseStatId",
                table: "EquipmentTypes");

            migrationBuilder.DropColumn(
                name: "BaseStatId",
                table: "EquipmentTypes");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseStatEquipmentType");

            migrationBuilder.AddColumn<int>(
                name: "BaseStatId",
                table: "EquipmentTypes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTypes_BaseStatId",
                table: "EquipmentTypes",
                column: "BaseStatId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTypes_BaseStats_BaseStatId",
                table: "EquipmentTypes",
                column: "BaseStatId",
                principalTable: "BaseStats",
                principalColumn: "Id");
        }
    }
}
