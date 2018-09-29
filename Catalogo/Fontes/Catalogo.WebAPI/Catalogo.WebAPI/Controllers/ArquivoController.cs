using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Catalogo.Model.Models;
using Catalogo.WebAPI.ApiModel;
using Catalogo.WebAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.WebAPI.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class ArquivoController : ControllerBase
    {
        private readonly IArquivoRepository _arquivoRepository;
        private readonly IMapper _mapper;

        public ArquivoController(IArquivoRepository arquivoRepository, IMapper mapper)
        {
            _arquivoRepository = arquivoRepository;
            _mapper = mapper;
        }

        [HttpGet("GetArquivo/{id}")]
        public ActionResult<string> GetArquivo(int id)
        {
            ArquivoApiModel apiModel = null;

            var model = _arquivoRepository.GetArquivo(id);

            if (model != null)
            {
                apiModel = _mapper.Map<Arquivo, ArquivoApiModel>(model);
            }

            return new JsonResult(apiModel);
        }

        [HttpPost("PostArquivo")]
        public ActionResult<string> PostArquivo(ArquivoApiModel arquivo)
        {
            var viewModel = _mapper.Map<ArquivoApiModel, Arquivo>(arquivo);

            int documentoId = _arquivoRepository.SalvarArquivo(viewModel);

            return new JsonResult(documentoId);
        }



    }
}