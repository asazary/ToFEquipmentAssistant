using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToFEA.Migrations
{
    /// <inheritdoc />
    public partial class migr22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.CreateTable(
                name: "StatLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ElemStatId = table.Column<int>(type: "INTEGER", nullable: false),
                    CommonStatId = table.Column<int>(type: "INTEGER", nullable: false),
                    PercentStatId = table.Column<int>(type: "INTEGER", nullable: true),
                    DamageStatId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatLinks_PossibleStats_CommonStatId",
                        column: x => x.CommonStatId,
                        principalTable: "PossibleStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatLinks_PossibleStats_DamageStatId",
                        column: x => x.DamageStatId,
                        principalTable: "PossibleStats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StatLinks_PossibleStats_ElemStatId",
                        column: x => x.ElemStatId,
                        principalTable: "PossibleStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatLinks_PossibleStats_PercentStatId",
                        column: x => x.PercentStatId,
                        principalTable: "PossibleStats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatLinks_CommonStatId",
                table: "StatLinks",
                column: "CommonStatId");

            migrationBuilder.CreateIndex(
                name: "IX_StatLinks_DamageStatId",
                table: "StatLinks",
                column: "DamageStatId");

            migrationBuilder.CreateIndex(
                name: "IX_StatLinks_ElemStatId",
                table: "StatLinks",
                column: "ElemStatId");

            migrationBuilder.CreateIndex(
                name: "IX_StatLinks_PercentStatId",
                table: "StatLinks",
                column: "PercentStatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatLinks");

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Values = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }
    }
}
