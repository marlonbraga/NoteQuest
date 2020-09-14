using System;
using System.Collections.Generic;
using System.Numerics;

namespace Game {
	public enum Direction:int {
		up = 0,//🔼
		left = 1,
		down = 2,
		right = 3
	}
	public class Room
	{
		public int IdRoom { get; }
		public List<Door> Doors { set; get; }
		public List<Monster> Monsters { set; get; }
		public string[,] Titles { set; get; }
		public string RoomMap { set; get; }

		public Room(int idRoom) {
			this.IdRoom = idRoom;
			this.Doors = new List<Door>();
			this.Monsters = new List<Monster>();
			this.Titles = Titles;
			this.RoomMap = "";
		}
		public Room(int idRoom, List<Door> Doors, List<Monster> Monsters, string[,] Titles, string RoomMap="") {
			this.IdRoom = idRoom;
			this.Doors = Doors;
			this.Monsters = Monsters;
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
		
		public string[,] RemoveMonsterOf(string[,] titles)
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