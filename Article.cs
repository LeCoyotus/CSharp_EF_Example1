using System.ComponentModel.DataAnnotations.Schema;

namespace ExoEF4
{
	public class Article
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public int UserId { get; set; }
		[ForeignKey("UserId")]
		virtual public User Author { get; set; }

		public Article()
		{
			
		}

		public override string ToString()
		{
			return $" { Id } - { Title } - Created by { Author }";
		}
	}
}