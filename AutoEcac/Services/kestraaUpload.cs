using System;
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

        // Definir os parâmetros para o post provavelmente numero da di e arquivo.
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
