using Microsoft.AspNetCore.Mvc;

namespace OpenAI_ImgGenerator.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult About()
        {
            return View();
        }
    }
}
