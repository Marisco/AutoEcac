﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutoEcac
{
    public class kestraaUpload
    {
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

		//body:
		//fileToUpload: arquivo que será enviado

		//fileType:Import Declaration - DI, Receipt of Import - CI
		//fileReference:NUMERODI - Extrato DI ou NUMERODI - XML Acompanhamento ou NUMERODI - XML Acompanhamento ou NUMERODI - Comprovante
		//issueDate:data de hoje
		//userId:99999999


		public void enviarArquivosws()
        {
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create("https://kestraa-upload-qa.azurewebsites.net/upload/Shipments");
            webrequest.ContentType = "application/json";
            webrequest.Method = "POST";
            webrequest.Headers.Add("Authorization", "eyJhbGciOiJIUzI1NiJ9.S2VzdHJhYVVwbG9hZFNlcnZpY2VBcGlLZXk.ybCz7EEcTl8Hjf9lx3zjQdbd1qPcJGn_LBR3z0OGyGI");
            var httpResponse = (HttpWebResponse)webrequest.GetResponse();

        }


    }
}
