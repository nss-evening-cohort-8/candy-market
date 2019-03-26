using System;
using System.Collections.Generic;
using System.Linq;

namespace candy_market
{
	public class App
	{
		public App()
		{
			Console.Title = "Cross Confectioneries Incorporated";
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
		}

		public ConsoleKeyInfo MainMenu(List<string> options)
		{
			// View is a helper class for building different menus.
			View mainMenu = new View()
					.AddMenuText("What would you like to do with your candy?")
					.AddMenuOptions(options)
					.AddMenuText("Press Esc to exit.")
					.PrintMenu();
			var userOption = Console.ReadKey();
			return userOption;
		}

		public void AddNewCandy(Database db)
		{
			// Get some information about the candy from the user.
			new View("What is the name of the candy?");
			var candyName = Console.ReadLine();
			
			new View("What is the flavor of the candy?");
			var candyFlavor = Console.ReadLine();
			
			// Get a user and create the candy I'm about to store.
			var ownerOfTheCandy = db.GetMe();
			var newCandy = new Candy(db, ownerOfTheCandy.Id)
			{
				Name = candyName,
				Flavor = candyFlavor,
				ManufacturedDate = DateTime.Now
			};
			
			var savedCandy = db.SaveNewCandy(newCandy);
			
			var confirmations = new List<string>
			{
				$"You now own a {savedCandy.Name}",
				"Press the any key to return to the main menu"
			};
			new View(confirmations);
			Console.ReadKey();
		}

		public void EatCandy(Database db)
		{
			var me = db.GetMe();
			var candyNames = me.Candy
				.Select(candy => candy.Name)
				.Distinct().ToList();

			// ask the user about the candy they want to eat.
			var eatCandySelection = new View()
				.AddMenuText("What candy would you like to eat?")
				.AddMenuOptions(candyNames)
				.PrintMenu()
				.NumberedSelection();

			// find the candy based on the information the user asked for.
			var selectedCandy = me.Candy
				.Where(candy => candy.Name == candyNames[eatCandySelection - 1])
				.OrderBy(candy => candy.ManufacturedDate)
				.First();

			// delete the candy... because you ate it.
			db.EatCandy(selectedCandy);
			
			var confirmations = new List<string>
			{
				$"You just ate a {selectedCandy.Name}",
				"Press the any key to return to the main menu"
			};
			new View(confirmations);
			Console.ReadKey();
		}

		public void EatRandomCandy(Database db)
		{ // use Humanize for enum of types?
			var me = db.GetMe();
			var candyFlavors = me.Candy
				.Select(candy => candy.Flavor)
				.Distinct().ToList();
			
			var eatRandomCandySelection = new View()
				.AddMenuText("What candy flavor would you like?")
				.AddMenuOptions(candyFlavors)
				.PrintMenu()
				.NumberedSelection();
				
			var selectedCandy = me.Candy
				.Where(candy => candy.Flavor == candyFlavors[eatRandomCandySelection - 1])
				.OrderBy(candy => candy.ManufacturedDate)
				.First();
				
			db.EatCandy(selectedCandy);
			
			var confirmations = new List<string>
			{
				$"You just ate a {selectedCandy.Name}",
				"Press the any key to return to the main menu"
			};
			new View(confirmations);
			Console.ReadKey();
		}
		
		public void TradeCandy(Database db)
		{
			// show users to select
			User selectedUser = SelectOtherUser(db);

			// pick a random candy of the other user to trade
			Candy randomCandy = GetRandomCandy(selectedUser);
			new View($"{selectedUser.Name} is offering to trade a {randomCandy.Name} that tastes {randomCandy.Flavor}");
			Console.ReadKey();

			// show my candies to select
			var myCandy = db.GetAllMyCandy();
			var candyNames = myCandy.Select(candy => candy.Name).ToList();
			var candyToTrade = new View()
				.AddMenuText("What candy would you like to trade?")
				.AddMenuOptions(candyNames)
				.PrintMenu()
				.NumberedSelection();

			var selectedCandy = myCandy
				.Where(candy => candy.Name == candyNames[candyToTrade - 1])
				.OrderBy(candy => candy.ManufacturedDate)
				.First();

			// put random candy in my bag
			myCandy.Add(randomCandy);
			selectedUser.Candy.Remove(randomCandy);

			// put selected candy in other users bag
			selectedUser.Candy.Add(selectedCandy);
			myCandy.Remove(selectedCandy);

		}

		private static Candy GetRandomCandy(User selectedUser)
		{
			var rand = new Random().Next(selectedUser.Candy.Count);
			var randomCandy = selectedUser.Candy[rand];
			return randomCandy;
		}

		private static User SelectOtherUser(Database db)
		{
			var users = db.GetOtherUsers();
			
			var userNames = users
				.Select(user => user.Name)
				.ToList();

			var userToTradeWith = new View()
				.AddMenuOptions(userNames)
				.PrintMenu()
				.NumberedSelection();

			var selectedUser = users
				.First(user => user.Id == userToTradeWith + 1);
			return selectedUser;
		}
	}
}
