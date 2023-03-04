using DALLE_webapp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using static DALLE_webapp.Models.ResponseModel;

namespace DALLE_webapp.Controllers
{

    public class HomeController : Controller
    {
        string APIKEY = string.Empty;

        public HomeController(IConfiguration conf)
        {
            APIKEY = conf.GetSection("OPENAI_API_KEY").Value;
        }


        public IActionResult Index()
        {
            return View();
        }


        //Home/GenerateImage 
        [HttpPost]
        public async Task<IActionResult> GenerateImage([FromBody] Input input)
        {
            var resp = new ReponseModel();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", APIKEY);
                var Message = await client.
                    PostAsync("https://api.openai.com/v1/images/generations",
                    new StringContent(JsonConvert.SerializeObject(input),
                    Encoding.UTF8,
                    "application/json"
                    ));

                if (Message.IsSuccessStatusCode)
                {
                    var content = await Message.Content.ReadAsStringAsync();
                    resp = JsonConvert.DeserializeObject<ReponseModel>(content);
                }
            }
            return Json(resp);
        }
    }
}