using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DungeonMasterBot {
	class MatchGame
	{
		public Party Party { set; get; }
		public static DungeonRoom ActualDungeonRoom;
		private static MatchGame _matchGame;
		public static MatchGame GetInstance()
		{
			if(_matchGame == null) {
				_matchGame = new MatchGame();
			}
			return _matchGame;
		}
		public void Start()
		{
			Console.WriteLine("\n\n\n      🎲🎲🎲  WELCOME TO DUNGEON  🎲🎲🎲\n");

			Party = Party.GetInstance();

			
			ActualDungeonRoom = new DungeonRoom();
			do
			{
				MatchRound();
			} while (!isGameOver());

			EndGame();
		}

		private void MatchRound()
		{
			string options = "[";
			for(int i = 0; i < ActualDungeonRoom.Doors.Count; i++) {
				options += $"{i}-";
				options += ActualDungeonRoom.Doors[i].icon;

				if(i < ActualDungeonRoom.Doors.Count - 1) {
					options += "|";
				}
			}
			options += "]";
			Console.WriteLine(options);
			int option = (int)int.Parse(Console.ReadLine());
			ActualDungeonRoom = ActualDungeonRoom.Doors[option].PassDoor();
			ActualDungeonRoom.Hostil();
		}

		private bool isGameOver()
		{
			if(Party.GetInstance().Heroes.Count > 0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		public void EndGame()
		{
			Console.WriteLine("     \n💀💀💀 GAME OVER 💀💀💀\n");
			_matchGame = null;
			Thread.Sleep(2000);
			Console.Write(".");
			Thread.Sleep(2000);
			Console.Write(".");
			Thread.Sleep(2000);
			Console.Write(".\n");
			Thread.Sleep(2000);
			Start();
		}
	}
}
