using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenAI_ImgGenerator.Models.DTO;
using OpenAI_ImgGenerator.Repositories.Abstract;

namespace OpenAI_ImgGenerator.Controllers
{
	public class UserAuthentication : Controller
	{
		private readonly IUserAuthenticationService _service;

		public UserAuthentication(IUserAuthenticationService service)
		{
			this._service = service;
		}

		public IActionResult Registration()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Registration(RegistrationModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);				
			}

			model.Role = "user";

			var result = await _service.RegistrationAsync(model);

			TempData["msg"] = result.Message;

			return RedirectToAction(nameof(Registration));
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result = await _service.LoginAsync(model);
			if (result.StatusCode == 1)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				TempData["msg"] = result.Message;
				return RedirectToAction(nameof(Login));
			}
		}

		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await _service.LogoutAsync();
			return RedirectToAction(nameof(Login));
		}


		//Create Admin User, comment cause we don't need more than one admin

		//public async Task<IActionResult> Reg()
		//{
		//	var model = new RegistrationModel
		//	{
		//		Username = "admin1",
		//		Name = "Aleksandr Hard",
		//		Email = "hard@gmail.com",
		//		Password = "Admin@12345#"
		//	};

		//	model.Role = "admin";

		//	var result = await _service.RegistrationAsync(model);

		//	TempData["msg"] = result.Message;

		//	return Ok(result);
		//}
	}
}
