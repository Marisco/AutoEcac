using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.Model.Models
{
    public class OperadorEstrangeiro
    {
        public int seq { get; set; }
        public string cnpjRaiz { get; set; }
        public string codigo { get; set; }
        public string nome { get; set; }
        public string logradouro { get; set; }
        public string nomeCidade { get; set; }
        public string codigoSubdivisaoPais { get; set; }
        public string codigoPais { get; set; }
        public string cep { get; set; }
    }
}
