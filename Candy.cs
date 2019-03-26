using System;
using System.Linq;

namespace candy_market
{
	public class Candy
	{
		public Candy(Database db, int userId)
		{
			OwnerId = userId;
			Id = GenerateNewCandyId(db);
		}
		public int Id { get; }
		public string Name { get; set; }
		public int OwnerId { get; }
		public DateTime ManufacturedDate { get; set; }
		public string Flavor { get; set; }

		private int GenerateNewCandyId(Database db)
		{
			var candies = db.GetAllMyCandy();
			if (candies.Count == 0) return 1;
			return candies.Max(candy => candy.Id) + 1;
		}
	}
}
