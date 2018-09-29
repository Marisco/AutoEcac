using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.Model.Models
{
    public class Items
    {
        public int seq { get; set; }
        public object codigo { get; set; }
        public object descricao { get; set; }
        public string cnpjRaiz { get; set; }
        public string situacao { get; set; }
        public string modalidade { get; set; }
        public string ncm { get; set; }
        public object codigoNaladi { get; set; }
        public object codigoGPC { get; set; }
        public object codigoGPCBrick { get; set; }
        public object codigoUNSPSC { get; set; }
        public string paisOrigem { get; set; }
        public string cpfCnpjFabricante { get; set; }
        public bool fabricanteConhecido { get; set; }
        public int codigoOperadorEstrangeiro { get; set; }
        public List<Atributo> atributos { get; set; }
        public object codigosInterno { get; set; }
    }
}
