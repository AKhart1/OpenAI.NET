using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace OpenAI_ImgGenerator.Models.DTO
{
	public class RegistrationModel
	{
		[Required]
		public string Name { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string Username { get; set; }
		[Required]
		[RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$",ErrorMessage ="Minimum LENGTH 6 and must contain 1 Uppercase, 1 Lowercase, 1 special character, 1 digit")]
		public string Password { get; set; }
		[Required]
		[Compare("Password")]
		public string PasswordConfirm { get; set; }
		public string ? Role { get; set; }
	}
}
