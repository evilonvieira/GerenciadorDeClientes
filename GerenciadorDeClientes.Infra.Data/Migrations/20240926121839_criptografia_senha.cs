using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorDeClientes.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class criptografia_senha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
UPDATE Usuarios SET senha = 'PjvWU3QDyhnk4aEOErm45A==' WHERE Email = 'admin@admin.com'
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Usuarios SET senha = '123' WHERE Email = 'admin@admin.com'");
        }
    }
}
