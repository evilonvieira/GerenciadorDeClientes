using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorDeClientes.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class sp_delete_endereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Cria a stored procedure
            migrationBuilder.Sql(
                @"
CREATE PROCEDURE sp_delete_endereco
    @EnderecoId INT
AS
BEGIN
    -- Início de uma transação para garantir consistência
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verifica se o registro existe antes de tentar deletar
        IF EXISTS (SELECT 1 FROM ClientesEnderecos WHERE Id = @EnderecoId)
        BEGIN
            -- Deleta o registro de forma segura
            DELETE FROM ClientesEnderecos WHERE Id = @EnderecoId;
            
            -- Confirma a transação
            COMMIT TRANSACTION;
        END
        ELSE
        BEGIN
            -- Se o registro não existir, desfaz a transação e retorna um erro
            ROLLBACK TRANSACTION;
            RAISERROR('Registro não encontrado.', 16, 1);
        END
    END TRY
    BEGIN CATCH
        -- Desfaz a transação em caso de erro
        ROLLBACK TRANSACTION;

        -- Lança o erro capturado
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove a stored procedure no downgrade da migration
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_delete_endereco");
        }
    }
}
