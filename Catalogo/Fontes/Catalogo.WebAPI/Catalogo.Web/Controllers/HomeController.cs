using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Catalogo.Web.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Microsoft.AspNetCore.Http;
using System.Text;
using NPOI.HSSF.UserModel;
using Microsoft.Extensions.Configuration;
using Catalogo.Web.ViewModels.Arquivo;
using System.Net.Http.Headers;

namespace Catalogo.Web.Controllers
{  

    public class HomeController : MainController
    {
        private IHostingEnvironment _hostingEnvironment;

        public HomeController(IConfiguration configuration, IHostingEnvironment hostingEnvironment) : base(configuration["HostWebAPI"]) 
        {
            _hostingEnvironment = hostingEnvironment;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadArquivo(ArquivoViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Request.Form.Files.Count > 0)
                {
                    var fileUpload = HttpContext.Request.Form.Files[0];

                    var fileName = ContentDispositionHeaderValue.Parse(fileUpload.ContentDisposition).FileName.Trim('"');

                    using (var fileStream = fileUpload.OpenReadStream())
                    {
                        using (var ms = new MemoryStream())
                        {
                            fileStream.CopyTo(ms);
                            model.Imagem = ms.ToArray();
                            model.Tipo = fileUpload.Name.Substring(fileUpload.Name.LastIndexOf(".") + 1 , fileUpload.Name.Length - fileUpload.Name.LastIndexOf(".") - 1);
                            model.Nome = fileName;
                            model.Tam = fileUpload.Length;
                        }
                    }
                }

                var idArquivo = base.PostAsync<int>("Arquivo/PostArquivo", model);

                return this.Content("Salvo com sucesso!");
            }
            else
            {
                return this.Content("Erro no upload!");
            }
        }

        public FileResult DownloadArquivo(int arquivoID)
        {
            var retorno = base.GetAsync<ArquivoViewModel>(string.Format("Arquivo/GetArquivo/{0}", arquivoID));

            return File(retorno.Imagem, "application/x-msdownload", retorno.Nome);
        }

        

        [HttpPost]
        public ActionResult ExtrairExcelParaHtml()
        {
            IFormFile file = Request.Form.Files[0];
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            StringBuilder sb = new StringBuilder();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); 
                        sheet = hssfwb.GetSheetAt(0); 
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream);
                        sheet = hssfwb.GetSheetAt(0);   
                    }
                    IRow headerRow = sheet.GetRow(0); 
                    int cellCount = headerRow.LastCellNum;
                    sb.Append("<table class='table'><tr>");
                    for (int j = 0; j < cellCount; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        sb.Append("<th>" + cell.ToString() + "</th>");
                    }
                    sb.Append("</tr>");
                    sb.AppendLine("<tr>");
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) 
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                                sb.Append("<td>" + row.GetCell(j).ToString() + "</td>");
                        }
                        sb.AppendLine("</tr>");
                    }
                    sb.Append("</table>");
                }
            }
            return this.Content(sb.ToString());
        }
    }
}
