using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonaCompleta.Migrations
{
    /// <inheritdoc />
    public partial class alimentartabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Documento",
                columns: new[] { "id", "LastUpdated", "apellido", "documento", "edad", "fecha", "imagen", "nacionalidad", "nombre", "segundonombre", "sexo" },
                values: new object[] { 1, new DateTime(2024, 1, 26, 15, 6, 29, 774, DateTimeKind.Local).AddTicks(2626), "Acuña", 44120148, 20, new DateTime(2024, 1, 26, 15, 6, 29, 774, DateTimeKind.Local).AddTicks(2617), "", "ARG", "Julián", "Martin", "M" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Documento",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
