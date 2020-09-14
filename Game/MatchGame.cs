using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Game {
	public class MatchGame
	{
		internal Party Party { set; get; }
		internal DungeonRoomBuilder DungeonRoomBuilder { private set; get; }
		internal static Room ActualDungeonRoom;
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
			CheckMonstersInRoom(ActualDungeonRoom);
		}
		private void CheckMonstersInRoom(Room ActualRoom) {
			if(ActualRoom.Monsters.Count > 0){
				Battle battle = new Battle(ActualRoom);
				BattleStatus status = battle.Start();
				switch(status) {
					case BattleStatus.Defeat:
						isGameOver();
						break;
					case BattleStatus.Stopped:
						ActualDungeonRoom.Doors[0].PassDoor();
						break;
					case BattleStatus.Victory:
						ActualDungeonRoom.RemoveMonsterOf(MatchGame.ActualDungeonRoom.Titles);
						ActualDungeonRoom.Enter(); 
						break;
					case BattleStatus.Ready:
						Console.WriteLine("ERROR: [Battle doesn't change state] 1");
						break;
					case BattleStatus.Started:
						Console.WriteLine("ERROR: [Battle doesn't change state] 2");
						break;
					default:
						Console.WriteLine("ERROR: [Battle doesn't change state] 3");
						break;
				}
			}
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
