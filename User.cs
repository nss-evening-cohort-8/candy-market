using System.Collections.Generic;

namespace candy_market
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Candy> Candy { get; set; } = new List<Candy>();
	}
}
