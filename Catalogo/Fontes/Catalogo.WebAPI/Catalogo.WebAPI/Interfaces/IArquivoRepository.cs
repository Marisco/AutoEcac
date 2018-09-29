using Catalogo.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo.WebAPI.Interfaces
{
    public interface IArquivoRepository
    {
        Arquivo GetArquivo(int arquivoID);
        int SalvarArquivo(Arquivo model);
    }
}
