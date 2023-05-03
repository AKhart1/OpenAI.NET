using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OpenAI_ImgGenerator.Models
{
	public class DataBaseContext: IdentityDbContext<ApplicationUser>
	{
		public DataBaseContext(DbContextOptions<DataBaseContext> options): base(options)
		{

		}
	}
}
