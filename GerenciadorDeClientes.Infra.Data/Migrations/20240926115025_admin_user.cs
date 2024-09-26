using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorDeClientes.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class admin_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
INSERT INTO Usuarios (Nome, Email, Senha) VALUES ('Administrador','admin@admin.com','123')
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.Sql("DELETE FROM Usuarios WHERE Email = 'admin@admin.com'");
        }
    }
}
