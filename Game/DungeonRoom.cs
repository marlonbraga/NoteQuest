using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Transactions;

namespace Game {
	public enum Direction:int {
		up = 0,//🔼
		left = 1,
		down = 2,
		right = 3
	}
	public class DungeonRoom
	{
		public int IdRoom { get; }
		public List<Door> Doors { set; get; }
		public List<Enemy> Enemies { set; get; }
		public string[,] Titles { set; get; }
		public string RoomMap { set; get; }

		public DungeonRoom(int idRoom) {
			this.IdRoom = idRoom;
			this.Doors = new List<Door>();
			this.Enemies = new List<Enemy>();
			this.Titles = Titles;
			this.RoomMap = "";
		}
		public DungeonRoom(int idRoom, List<Door> Doors, List<Enemy> Enemies, string[,] Titles, string RoomMap="") {
			this.IdRoom = idRoom;
			this.Doors = Doors;
			this.Enemies = Enemies;
			this.Titles = Titles;
			this.RoomMap = RoomMap;
		}
		
		public string Enter()
		{
			return DrawRoom(this.RoomMap);
		}
		
		private string DrawRoom(string titles) {
			if(titles == "") {
				titles = DrawRoom(this.Titles);
			}
			Console.WriteLine(titles);
			return titles;
		}
		private string DrawRoom(string[,] titles) {
			Vector2 roomSize = new Vector2();
			roomSize.X = titles.GetLength(0);
			roomSize.Y = titles.GetLength(1);
			string room = $"\n\nROOM:{IdRoom}\n";
			for(int i = 0; i < roomSize.X; i++) {
				for(int j = 0; j < roomSize.Y; j++) {
					room += Titles[i, j];
				}
				room += "\n";
			}
			return room;
		}

		public Battle Hostil() {
			Battle battle = new Battle(Party.GetInstance().Heroes, this.Enemies);
			return battle;
		}
		
		public string[,] RemoveEnemyOf(string[,] titles)
		{
			Vector2 roomSize = new Vector2();
			roomSize.X = titles.GetLength(0);
			roomSize.Y = titles.GetLength(1);
			for(int i = 0; i < roomSize.X; i++) {
				for(int j = 0; j < roomSize.Y; j++) {
					if((titles[i, j] != "🌫 ") &&
							(titles[i, j] != " 🌫") &&
							(titles[i, j] != "  ") &&
							(titles[i, j] != " 🕸") &&
							(titles[i, j] != "🚪") &&
							(titles[i, j] != "🔳"))
					{
						titles[i, j] = "💀";
					}
				}
			}
			this.Titles = titles;
			return Titles;
		}
		
	}
}