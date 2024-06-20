using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToFEA.Migrations
{
    /// <inheritdoc />
    public partial class Migr13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TitanLevel",
                table: "TitanStats",
                newName: "StatLevel");

            migrationBuilder.AddColumn<int>(
                name: "AugmentationLevel",
                table: "Equipments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AugmentationLevel",
                table: "Equipments");

            migrationBuilder.RenameColumn(
                name: "StatLevel",
                table: "TitanStats",
                newName: "TitanLevel");
        }
    }
}
