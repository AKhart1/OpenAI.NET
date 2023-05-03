using OpenAI_ImgGenerator.Models.DTO;

namespace OpenAI_ImgGenerator.Repositories.Abstract
{
	public interface IUserAuthenticationService
	{
		Task<Status> LoginAsync(LoginModel model);
		Task<Status> RegistrationAsync(RegistrationModel model);
		Task LogoutAsync();
	}
}
