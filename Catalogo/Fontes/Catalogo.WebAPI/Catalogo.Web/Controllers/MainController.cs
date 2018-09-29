using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Web.Controllers
{
    public class MainController : Controller
    {
        private HttpClient client;
        private readonly string _urlAPI;

        public MainController(string urlAPI)
        {
            _urlAPI = urlAPI;
        }

        protected T PostAsync<T>(string urlMethod, object objGenerico)
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(_urlAPI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync(urlMethod, objGenerico).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
                }
                else
                {
                    return default(T);
                }
            }
        }

        protected T GetAsync<T>(string urlMethod)
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(_urlAPI);
                client.DefaultRequestHeaders.Accept.Clear();

                var response = client.GetAsync(urlMethod).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;

                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseString);
                }

                return default(T);
            }
        }
    }
}