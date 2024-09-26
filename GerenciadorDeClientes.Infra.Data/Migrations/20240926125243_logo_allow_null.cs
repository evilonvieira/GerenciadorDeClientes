using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorDeClientes.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class logo_allow_null : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Logo",
                table: "Clientes",
                type: "VARBINARY(MAX)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "VARBINARY(MAX)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Logo",
                table: "Clientes",
                type: "VARBINARY(MAX)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "VARBINARY(MAX)",
                oldNullable: true);
        }
    }
}
