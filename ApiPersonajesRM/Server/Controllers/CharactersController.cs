using ApiPersonajesRM.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiPersonajesRM.Server.Controllers
{
    [Route("api/characters")]
    public class CharactersController : Controller
    {
        private HttpClient _httpClient;
        public CharactersController()
        {
            _httpClient = new HttpClient();
            
        }

        [HttpGet]
        [Route("all")]
        public async Task<Characters> GetAllCharacters(int page, string name = "")
        {
            try
            {
                Characters characters = null;

                //Construye la url de la Api con los parametros de la pagina y nombre
                string url = $"https://rickandmortyapi.com/api/character?page={page}";

                if (!string.IsNullOrEmpty(name))
                {
                    url += $"&name={name}";
                }

                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(url);
                httpResponseMessage.EnsureSuccessStatusCode();

                string responseBody = await
                    httpResponseMessage.Content.ReadAsStringAsync();


                characters = JsonConvert.DeserializeObject<Characters>(responseBody);

                return characters;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
