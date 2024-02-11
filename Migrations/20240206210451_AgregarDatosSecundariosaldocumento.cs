using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonaCompleta.Migrations
{
    /// <inheritdoc />
    public partial class AgregarDatosSecundariosaldocumento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Secundario",
                columns: table => new
                {
                    idCantdoc = table.Column<int>(type: "int", nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    documento = table.Column<int>(type: "int", nullable: true),
                    religion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    barrio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    region = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secundario", x => x.idCantdoc);
                    table.ForeignKey(
                        name: "FK_Secundario_Documento_PersonaID",
                        column: x => x.documento,
                        principalTable: "Documento",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "Documento",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "LastUpdated", "fecha" },
                values: new object[] { new DateTime(2024, 2, 6, 18, 4, 50, 432, DateTimeKind.Local).AddTicks(6803), new DateTime(2024, 2, 6, 18, 4, 50, 432, DateTimeKind.Local).AddTicks(6792) });

            migrationBuilder.CreateIndex(
                name: "IX_Secundario_PersonaID",
                table: "Secundario",
                column: "PersonaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Secundario");

            migrationBuilder.UpdateData(
                table: "Documento",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "LastUpdated", "fecha" },
                values: new object[] { new DateTime(2024, 1, 26, 15, 6, 29, 774, DateTimeKind.Local).AddTicks(2626), new DateTime(2024, 1, 26, 15, 6, 29, 774, DateTimeKind.Local).AddTicks(2617) });
        }
    }
}
