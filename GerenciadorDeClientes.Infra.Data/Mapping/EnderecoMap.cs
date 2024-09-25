using GerenciadorDeClientes.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.Infra.Data.Mapping
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("ClientesEnderecos");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Logradouro).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Numero).IsRequired().HasMaxLength(10);
            builder.Property(c => c.Complemento).HasMaxLength(100);
            builder.Property(c => c.Bairro).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Cidade).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Uf).IsRequired();


            // Configurando a chave estrangeira
            builder.Property(l => l.IdCliente).HasColumnName("IdCliente")
                .IsRequired();

            builder.HasOne(l => l.Cliente)
                .WithMany(c => c.Enderecos)
                .HasForeignKey(l => l.IdCliente);

        }
    }
}
