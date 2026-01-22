using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GateAPI.Infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableNameTipoAvaria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoAvaria",
                table: "TipoAvaria");

            migrationBuilder.RenameTable(
                name: "TipoAvaria",
                newName: "tipo_avaria");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "TipoLacre",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tipo_avaria",
                table: "tipo_avaria",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tipo_avaria",
                table: "tipo_avaria");

            migrationBuilder.RenameTable(
                name: "tipo_avaria",
                newName: "TipoAvaria");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "TipoLacre",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoAvaria",
                table: "TipoAvaria",
                column: "Id");
        }
    }
}
