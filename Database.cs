using System.Collections.Generic;
using System.Linq;

namespace candy_market
{
	public class Database
	{
		private static List<User> _database = new List<User>{ new User { Id = 1, Name = "Martin" } };
		
		private User _me = _database.First();
		
		public Database()
		{
			var seedCandy = new List<Candy>
			{
				new Candy(this, 1)
				{
					Name = "Whatchamacallit",
					Flavor = "Crispy"
				},
				new Candy(this, 1)
				{
					Name = "Snickers",
					Flavor = "Savory"
				},
				new Candy(this, 1)
				{
					Name = "3 Muskateers",
					Flavor = "Nougaty"
				},
				new Candy(this, 1)
				{
					Name = "Twix",
					Flavor = "Crispy"
				},
				new Candy(this, 1)
				{
					Name = "Whatchamacallit",
					Flavor = "Crispy"
				},
				new Candy(this, 1)
				{
					Name = "Whatchamacallit",
					Flavor = "Crispy"
				}
			};
			_me.Candy.AddRange(seedCandy);
			
			var otherUser = new User
			{
				Id = 2,
				Name = "Nathan",
				Candy = new List<Candy>
				{
					new Candy(this, 2)
					{
						Name = "Broken Glass",
						Flavor = "Crunchy"
					},
					new Candy(this, 2)
					{
						Name = "Used Syringe",
						Flavor = "Sweet & Sour"
					},
					new Candy(this, 2)
					{
						Name = "Razor Blade",
						Flavor = "Sharp"
					},
					new Candy(this, 2)
					{
						Name = "Tequila Worm",
						Flavor = "Spicy"
					},
					new Candy(this, 2)
					{
						Name = "Rusty Nickel",
						Flavor = "Metallic"
					}
				}
			};
			_database.Add(otherUser);
		}
		
		public List<Candy> GetAllMyCandy()
		{
			return _me.Candy;
		}
		
		public User GetMe()
		{
			return _me;
		}
		
		public List<User> GetOtherUsers()
		{
			return _database.Where(user => user.Id != 1).ToList();
		}

		public Candy SaveNewCandy(Candy newCandy)
		{
			_me.Candy.Add(newCandy);
			return _me.Candy.Single(candy => candy.Id == newCandy.Id);
		}

		public bool EatCandy(Candy candyToEat)
		{
			return _me.Candy.Remove(candyToEat);
		}
	}
}
