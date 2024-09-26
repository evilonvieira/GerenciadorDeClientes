﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.Infra.CrossCutting.Extensions
{
    public static class UfExtension
    {
        public static string SiglaUf(this int codigoUf) {
            return codigoUf switch { 
                0  => "UF",
                12 => "AC",
                27 => "AL",
                16 => "AP",
                13 => "AM",
                29 => "BA",
                23 => "CE",
                53 => "DF",
                32 => "ES",
                52 => "GO",
                21 => "MA",
                51 => "MT",
                50 => "MS",
                31 => "MG",
                15 => "PA",
                25 => "PB",
                41 => "PR",
                26 => "PE",
                22 => "PI",
                33 => "RJ",
                24 => "RN",
                43 => "RS",
                11 => "RO",
                14 => "RR",
                42 => "SC",
                35 => "SP",
                28 => "SE",
                17 => "TO",
                _ => "--"
            };
        }
    }
}
