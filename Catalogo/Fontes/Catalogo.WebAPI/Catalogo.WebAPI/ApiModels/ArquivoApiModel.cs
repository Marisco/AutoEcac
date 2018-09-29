using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo.WebAPI.ApiModel
{
    public class ArquivoApiModel
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public byte[] Imagem { get; set; }
        public long Tam { get; set; }
    }
}
