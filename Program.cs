using System;
using System.Collections.Generic;

namespace candy_market
{
	class Program
	{
		static void Main(string[] args)
		{
			// Creates a database context and customizes the console colors.
			var app = new App();
			var db = new Database();

			var exit = false;
			while (!exit)
			{
				// Display the main menu and return the user menu selection.
				var menuOptions = new List<string>
				{
						"Did you just get some new candy? Add it here.",
						"Do you want to eat a specific candy? Take it here.",
						"Do you want to eat a random candy? Spin the wheel.",
						"Would you like to trade with someone?"
				};
				var userInput = app.MainMenu(menuOptions);

				if (userInput.Key == ConsoleKey.Escape)
					break;
				
				var userSelection = int.Parse(userInput.KeyChar.ToString());
				
				// Takes an action based on the user menu selection.
				// The action taken determines if "exit" changes to true.
				exit = DoSomething(app, db, userSelection);
			}
		}

		private static bool DoSomething(App app, Database db, int userSelection)
		{
			// there seems to be a bug in this switch statement. the app closes after every action.
			switch (userSelection)
			{
				case 1: app.AddNewCandy(db);
					break;
				case 2: app.EatCandy(db);
					break;
				case 3: app.EatRandomCandy(db);
					break;
				case 4: app.TradeCandy(db);
					break;
				default: return false;
			}
			return false;
		}
	}
}
