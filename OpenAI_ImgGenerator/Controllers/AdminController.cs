using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OpenAI_ImgGenerator.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		public IActionResult Display()
		{
			return View();
		}
	}
}
