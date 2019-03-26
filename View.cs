using System;
using System.Collections.Generic;

namespace candy_market
{
	public class View
	{
		// http://patorjk.com/software/taag/#p=display&f=ANSI%20Shadow&t=candy%20market
		private string companyName = @"
           ██████╗██████╗  ██████╗ ███████╗███████╗
          ██╔════╝██╔══██╗██╔═══██╗██╔════╝██╔════╝
          ██║     ██████╔╝██║   ██║███████╗███████╗
          ██║     ██╔══██╗██║   ██║╚════██║╚════██║
          ╚██████╗██║  ██║╚██████╔╝███████║███████║
           ╚═════╝╚═╝  ╚═╝ ╚═════╝ ╚══════╝╚══════╝
  
     ██████╗ █████╗ ███╗   ██╗██████╗ ██╗███████╗███████╗
    ██╔════╝██╔══██╗████╗  ██║██╔══██╗██║██╔════╝██╔════╝
    ██║     ███████║██╔██╗ ██║██║  ██║██║█████╗  ███████╗
    ██║     ██╔══██║██║╚██╗██║██║  ██║██║██╔══╝  ╚════██║
    ╚██████╗██║  ██║██║ ╚████║██████╔╝██║███████╗███████║
     ╚═════╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═════╝ ╚═╝╚══════╝╚══════╝
";

		private List<string> _menuItems;
		private int itemNumber = 0;

		public View()
		{
			_menuItems = new List<string> { companyName };
		}

		public View(string question)
		{
			_menuItems = new List<string> { companyName, question };
			PrintMenu();
		}
		
		public View(List<string> questions)
		{
			_menuItems = new List<string>(questions);
			_menuItems.Insert(0, companyName);
			PrintMenu();
		}

		public View AddMenuText(string text)
		{
			var menuText = $"{Environment.NewLine}{text}{Environment.NewLine}";
			_menuItems.Add(menuText);
			return this;
		}

		public View AddMenuOption(string menuItem)
		{
			++itemNumber;
			var menuEntry = $"{itemNumber}. {menuItem}";
			_menuItems.Add(menuEntry);
			return this;
		}

		public View AddMenuOptions(List<string> menuItems)
		{
			foreach (var menuItem in menuItems)
			{
				AddMenuOption(menuItem);
			}

			return this;
		}

		public View PrintMenu()
		{
			Console.Clear();
			var menu = string.Join(Environment.NewLine, _menuItems);
			Console.Write($"{menu}{Environment.NewLine}> ");
			return this;
		}

		public int NumberedSelection()
		{
			var userSelection = Console.ReadKey().KeyChar.ToString();
			return int.Parse(userSelection);
		}
	}
}
