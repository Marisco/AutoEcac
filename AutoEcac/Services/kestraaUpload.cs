using AutoEcac.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace AutoEcac
{
    public class KestraaUpload
    {

		private HttpClient client = new HttpClient();
		private readonly string urlBase = "https://kestraa-upload-qa.azurewebsites.net/upload/Shipments/";

		// instanciar a classe para chamar o servico
		//kestraaUpload enviarAquivo = new kestraaUpload();
		//envio.enviarArquivosws();

		// Definir os parâmetros para o post. Segue abaixo.
		//Post dos arquivos
		//Post: https://kestraa-upload-qa.azurewebsites.net/upload/Shipments/{ :idShipment }    “idShipment é igual a nr_processo”
		//Exemplo de Shipment Ids: 2522243 , 2522245 , 2492940 , 2507504 , 2425127 para testar os arquivos

		//Headers:
		//Authorization:KestraaUploadServiceApiKey eyJhbGciOiJIUzI1NiJ9.S2VzdHJhYVVwbG9hZFNlcnZpY2VBcGlLZXk.ybCz7EEcTl8Hjf9lx3zjQdbd1qPcJGn_LBR3z0OGyGI
		//Content-Type:application/json

		//body:{body:{fileToUpload:132, }}
		//fileToUpload: arquivo que será enviado

		//fileType:Import Declaration - DI, Receipt of Import - CI
		//fileReference:NUMERODI - Extrato DI ou NUMERODI - XML Acompanhamento ou NUMERODI - XML Acompanhamento ou NUMERODI - Comprovante
		//issueDate:data de hoje
		//userId:99999999


		public async void enviarArquivosws(KestraaUploadRequest requestData, String nrProcesso)
        {

			try
			{
				client.DefaultRequestHeaders.Add("Authorization", "KestraaUploadServiceApiKey eyJhbGciOiJIUzI1NiJ9.S2VzdHJhYVVwbG9hZFNlcnZpY2VBcGlLZXk.ybCz7EEcTl8Hjf9lx3zjQdbd1qPcJGn_LBR3z0OGyGI");
			} catch(Exception e)
			{
				//ja possui o header
			}
			

			var response = await client.PostAsync(urlBase + nrProcesso, requestData.getFormContent());

			Console.Write("teste");
        }

		
    }
}
