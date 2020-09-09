using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Game {
	public class MatchGame
	{
		internal Party Party { set; get; }
		internal DungeonRoomBuilder DungeonRoomBuilder { private set; get; }
		internal static DungeonRoom ActualDungeonRoom;
		private static MatchGame _matchGame;
		private MatchGame() { }
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

			DungeonRoomBuilder = new DungeonRoomBuilder();
			ActualDungeonRoom = DungeonRoomBuilder.BuildRoom();
			ActualDungeonRoom.Enter();
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
				options += ActualDungeonRoom.Doors[i].optionIcon;

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
		internal void EndGame()
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
