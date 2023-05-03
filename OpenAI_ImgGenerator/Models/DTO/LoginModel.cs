using Microsoft.Build.Framework;

namespace OpenAI_ImgGenerator.Models.DTO
{
	public class LoginModel
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
