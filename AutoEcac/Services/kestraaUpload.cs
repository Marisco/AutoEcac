using AutoEcac.model;
using Newtonsoft.Json;
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
		private readonly string urlBase = "https://kestraaintegration-dev.cfapps.us10.hana.ondemand.com/file/upload/ImportDeclaration/";

        // instanciar a classe para chamar o servico
        //kestraaUpload enviarAquivo = new kestraaUpload();
        //envio.enviarArquivosws();

        // Definir os parâmetros para o post. Segue abaixo.
        //Post dos arquivos
        //Post: https://kestraa-upload-qa.azurewebsites.net/upload/Shipments/{ :idShipment }    “idShipment é igual a nr_processo”
        //Exemplo de Shipment Ids: 2522243 , 2522245 , 2492940 , 2507504 , 2425127 para testar os arquivos

        //Headers:
        //Authorization:KestraaUploadServiceApiKey 247fd8c6-d87a-4ddd-9a36-b21dd6809cfd
        //Content-Type:application/json

        //body:{body:{fileToUpload:132, }}
        //fileToUpload: arquivo que será enviado

        //fileType:Import Declaration - DI, Receipt of Import - CI
        //fileReference:NUMERODI - Extrato DI ou NUMERODI - XML Acompanhamento ou NUMERODI - XML Acompanhamento ou NUMERODI - Comprovante
        //issueDate:data de hoje
        //userId:99999999


        public async void enviarArquivosws(KestraaUploadRequest requestData, Int64 ?nrProcesso)
        {
            

			try
			{
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Add("Authorization", "Apikey 247fd8c6-d87a-4ddd-9a36-b21dd6809cfd");
                client.DefaultRequestHeaders.Add("Origin", "https://kestraa.com");                

            } catch(Exception e)
			{
				//ja possui o header
			}   

           
            //HttpResponseMessage response = await client.PostAsync(nrProcesso.ToString(), requestData.getFormContent());
            //string contents = await response.Content.ReadAsStringAsync();

            //Console.Write(response.StatusCode);
        }

		
    }
}
