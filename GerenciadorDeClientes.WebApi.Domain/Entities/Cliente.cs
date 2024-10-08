﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.WebApi.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public byte[]? Logo { get; set; }
        public string Logotipo { get; set; }

        // 1:N
        public ICollection<Endereco> Enderecos { get; set; }
    }
}
