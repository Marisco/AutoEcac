using Catalogo.Model.Models;
using Catalogo.WebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo.WebAPI.Repository
{
    public class ArquivoRepository : IArquivoRepository
    {
        private readonly catalogoContext _db;

        public ArquivoRepository(catalogoContext contexto)
        {
            _db = contexto;
        }

        public Arquivo GetArquivo(int arquivoID)
        {
            return _db.Arquivo.Find(arquivoID);
        }

        public int SalvarArquivo(Arquivo model)
        {
            try
            {
                _db.Arquivo.Add(model);

                if (_db.SaveChanges() > 0)
                {
                    return model.Id;
                }

                return 0;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

    }
}
