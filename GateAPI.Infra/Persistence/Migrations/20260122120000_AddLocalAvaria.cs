using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GateAPI.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddLocalAvaria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LOCAL_AVARIA",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LOCAL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DESCRICAO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    STATUS = table.Column<int>(type: "int", nullable: false),
                    CRIADO_EM = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CRIADO_POR = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    ATUALIZADO_EM = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ATUALIZADO_POR = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    DELETADO_EM = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DELETADO_POR = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOCAL_AVARIA", x => x.ID);
                    table.UniqueConstraint("UQ_LOCAL_AVARIA_LOCAL", x => x.LOCAL);
                });

            // Criar índices para otimização
            migrationBuilder.CreateIndex(
                name: "IX_LOCAL_AVARIA_STATUS",
                table: "LOCAL_AVARIA",
                column: "STATUS");

            migrationBuilder.CreateIndex(
                name: "IX_LOCAL_AVARIA_CRIADO_EM",
                table: "LOCAL_AVARIA",
                column: "CRIADO_EM");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LOCAL_AVARIA");
        }
    }
}
