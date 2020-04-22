using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExoEF4
{
	public class User
	{
		public int Id { get; set; }
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public int UserRight { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public List<Article> UserArticles { get; set; } = new List<Article>();
		public User()
		{
			
		}

		public bool IsAdmin()
		{
			return UserRight == 1;
		}

		public override string ToString()
		{
			return $"{ Id } - { UserName } - { FirstName } - { LastName }";
		}
	}
}