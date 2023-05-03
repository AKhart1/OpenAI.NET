using Microsoft.AspNetCore.Identity;

namespace OpenAI_ImgGenerator.Models
{
	public class ApplicationUser: IdentityUser
	{
		public string Name { get; set; }
		public string? ProfilePic { get; set; }

	}
}
