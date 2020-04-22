using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoEF4
{
	class Program
	{
		static ApplicationContext db = new ApplicationContext();
		private static User LoggedUser = null;
		static void Main(string[] args)
		{
			Menu();
		}

		static void Menu()
		{
			
			string choix;

			do
			{
				Console.WriteLine("Que voulez vous faire ?");
				Console.WriteLine("display / select / connect");
				choix = Console.ReadLine();

				switch (choix)
				{
					case "display":
						DisplayAllArticle();
						break;
					case "select":
						break;
					case "connect":
						User connectUser = Connect();
						if (connectUser != null)
						{
							Console.WriteLine("Vous êtes connecté !");

							LoggedUser = connectUser;

							if (connectUser.IsAdmin())
							{
								AdminMenu();
								choix = "quit";
							}
							else
							{
								UserMenu();
								choix = "quit";
							}
						}
						else
						{
							Console.WriteLine("Echec de connexion");
						}

						break;
				}
			} while (choix != "quit");
		}

		static void AdminMenu()
		{
			string choix;

			do
			{
				Console.WriteLine("Que voulez vous faire ?");
				Console.WriteLine("display / select / adduser / modifyuser / addarticle / deletearticle");
				choix = Console.ReadLine();

				switch (choix)
				{
					case "display":
						DisplayAllArticle();
						break;
					case "displayuser":
						DisplayAllUser();
						break;
					case "select":
						Console.WriteLine(SelectArticle());
						break;
					case "adduser":
						AddUser();
						break;
					case "removeuser":
						RemoveUser();
						break;
					case "modifyuser":
						ModifyUser();
						break;
					case "addarticle":
						AddArticle();
						break;
					case "deletearticle":
						RemoveArticle();
						break;
				}

			} while (choix != "quit");
		}

		static void UserMenu()
		{
			
			string choix;

			do
			{
				Console.WriteLine("Que voulez vous faire ?");
				Console.WriteLine("display / select / addarticle / deletearticle");
				choix = Console.ReadLine();

				switch (choix)
				{
					case "display":
						DisplayAllArticle();
						break;
					case "select":
						Console.WriteLine(SelectArticle());
						break;
					case "addarticle":
						AddArticle();
						break;
					case "deletearticle":
						RemoveArticle();
						break;
				}
			} while (choix != "quit");
		}

		static User Connect()
		{
			User toReturn = null;
			Console.WriteLine("Username :");
			string username = Console.ReadLine();
			Console.WriteLine("Password :");
			string password = Console.ReadLine();

			if (db.Users.Any(x => x.UserName == username && x.Password == password))
			{
				toReturn = db.Users.FirstOrDefault(x => x.UserName == username);
			}
			return toReturn;
		}

		static void AddUser()
		{
			Console.WriteLine("Veuillez renseigner le nouvel utilisateur : UserName / PassWord / FirstName / LastName");

			User userToAdd = new User
			{
				UserName = Console.ReadLine(),
				Password = Console.ReadLine(),
				FirstName = Console.ReadLine(),
				LastName = Console.ReadLine()
			};

			db.Users.Add(userToAdd);
			db.SaveChanges();
		}

		static void ModifyUser()
		{
			int choixIdUser = int.Parse(Console.ReadLine());
			User userToModify = db.Users.FirstOrDefault(x => x.Id == choixIdUser);

			Console.WriteLine($"Que voulez vous modifier sur l'utilisateur { userToModify.UserName } ?");
		}

		static void RemoveUser()
		{
			int choixIdUser = int.Parse(Console.ReadLine());
			User userToRemove = db.Users.FirstOrDefault(x => x.Id == choixIdUser);

			db.Users.Remove(userToRemove);
			db.SaveChanges();
		}

		static void DisplayAllUser()
		{
			var query = db.Users.Select(x => x).ToList();

			foreach (var user in query)
			{
				Console.WriteLine(user);
			}
		}

		static void DisplayAllArticle()
		{
			var query = db.Articles.Select(x => x).ToList();

			foreach (var article in query)
			{
				Console.WriteLine(article);
			}
		}

		static Article SelectArticle()
		{
			DisplayAllArticle();
			Console.WriteLine("Id de l'article a selectionner :");
			int choiceIdArticle = int.Parse(Console.ReadLine());

			return db.Articles.FirstOrDefault(x => x.Id == choiceIdArticle);
		}

		static void AddArticle()
		{
			Console.WriteLine("Nouvel article : Title - Content");
			Article artToAdd = new Article
			{
				Author = LoggedUser,
				Title = Console.ReadLine(),
				Content = Console.ReadLine()
			};

			LoggedUser.UserArticles.Add(artToAdd);
			db.Articles.Add(artToAdd);
			db.SaveChanges();
		}

		static void RemoveArticle()
		{
			Article artToRemove = SelectArticle();
			db.Articles.Remove(artToRemove);
			db.SaveChanges();
		}
	}

	class ApplicationContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Article> Articles { get; set; }
	}
}
