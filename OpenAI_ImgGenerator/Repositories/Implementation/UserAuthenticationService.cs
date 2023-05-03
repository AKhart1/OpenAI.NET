using Microsoft.AspNetCore.Identity;
using OpenAI_ImgGenerator.Models;
using OpenAI_ImgGenerator.Models.DTO;
using OpenAI_ImgGenerator.Repositories.Abstract;
using System.Security.Claims;

namespace OpenAI_ImgGenerator.Repositories.Implementation
{
	public class UserAuthenticationService : IUserAuthenticationService
	{
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly UserManager<ApplicationUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public UserAuthenticationService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.roleManager = roleManager;
		}

		public async Task<Status> LoginAsync(LoginModel model)
		{
			var status = new Status();
			var user = await userManager.FindByNameAsync(model.Username);
			if(user == null)
			{
				status.StatusCode = 0;
				status.Message = "Invalid UserName";
				return status;
			}
			//we will match pass
			if (!await userManager.CheckPasswordAsync(user, model.Password))
			{
				status.StatusCode = 0;
				status.Message = "Invalid password";

				return status;
			}

			var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
			if(signInResult.Succeeded)
			{
				var userRoles = await userManager.GetRolesAsync(user);
				var authClaims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.UserName)
				};

				foreach (var UserRole in userRoles)
				{
					authClaims.Add(new Claim(ClaimTypes.Role, UserRole));
				}

				status.StatusCode = 1;
				status.Message = "Logged in Successfully";

				return status;

			}else if (signInResult.IsLockedOut)
			{
				status.StatusCode = 0;
				status.Message = "User locked out";

				return status;
			}
			else
			{
				status.StatusCode = 0;
				status.Message = "Error on Login in";

				return status;
			}
		}

		public async Task LogoutAsync()
		{
			await signInManager.SignOutAsync();
		}

		public async Task<Status> RegistrationAsync(RegistrationModel model)
		{
			var status = new Status();			
			var userExcist = await userManager.FindByNameAsync(model.Username);
			if (userExcist != null) 
			{
				status.StatusCode = 0;
				status.Message = "User already exsists";

				return status;
			}

			ApplicationUser user = new ApplicationUser
			{
				SecurityStamp = Guid.NewGuid().ToString(),
				Name = model.Name,
				Email = model.Email,
				UserName = model.Username,
				EmailConfirmed = true
			};

			var result = await userManager.CreateAsync(user,model.Password);
			if (!result.Succeeded) 
			{
				status.StatusCode = 1;
				status.Message = "User creation failed";

				return status;
			}


			//Role management
			if(!await roleManager.RoleExistsAsync(model.Role))
			{
				await roleManager.CreateAsync(new IdentityRole(model.Role));
			}

			if(await roleManager.RoleExistsAsync(model.Role))
			{
				await userManager.AddToRoleAsync(user,model.Role);
			}

			status.StatusCode = 1;
			status.Message = "User has registered successfully";

			return status;
		}
	}
}
