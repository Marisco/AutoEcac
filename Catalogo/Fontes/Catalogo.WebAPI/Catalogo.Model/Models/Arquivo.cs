using System;
using System.Collections.Generic;

namespace Catalogo.Model.Models
{
    public partial class Arquivo
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public byte[] Imagem { get; set; }
        public long Tam { get; set; }
    }
}
