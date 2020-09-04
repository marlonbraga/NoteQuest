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
		public int idRoom;
		private static int numbersOfRooms = 0;
		public List<Door> Doors { set; get; }
		public List<Enemy> Enemy { set; get; }
		public string[,] Titles { set; get; }

		public DungeonRoom() {
			numbersOfRooms++;
			idRoom = numbersOfRooms;

			this.Enemy = new List<Enemy>();
			this.Doors = new List<Door>();

			Vector2 roomSize = new Vector2(15);

			this.Titles = new string[(int)roomSize.X, (int)roomSize.Y];
			int floor;
			for(int i = 0; i < roomSize.X; i++) {
				for(int j = 0; j < roomSize.Y; j++) {
					if(i == 0 || j == 0 || i == roomSize.X - 1 || j == roomSize.Y - 1) {
						this.Titles[i, j] = "🔳";
					} else {
						this.Titles[i, j] = "  ";
					}
					
					if ((i == 0 && j == 7)|| (i == 7 && j == 0)|| (i == 7 && j == roomSize.Y -1))
					{
						this.Titles[i, j] = "🚪";
					} 
				}
			}
			DrawRoom(this.Titles);
			string entranceRoom = ""+
			                      "\n🔳🔳🔳🔳🔳🔳🔳🚪🔳🔳🔳🔳🔳🔳🔳" +
			                      "\n🔳               ░░               🔳" +
			                      "\n🔳   🔳🔳       ░░       🔳🔳   🔳" +
			                      "\n🔳   🔳🔳       ░░       🔳🔳   🔳" +
			                      "\n🔳               ░░               🔳" +
			                      "\n🔳          ░░░░░░░░░░░░          🔳" +
			                      "\n🔳         ░░░░░░░░░░░░░░         🔳" +
			                      "\n🚪░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░🚪" +
			                      "\n🔳         ░░░░░░░░░░░░░░         🔳" +
			                      "\n🔳          ░░░░░░░░░░░░          🔳" +
			                      "\n🔳               ░░               🔳" +
			                      "\n🔳   🔳🔳       ░░       🔳🔳   🔳" +
			                      "\n🔳   🔳🔳      ░░░░      🔳🔳   🔳" +
			                      "\n🔳             ░░░░░░             🔳" +
			                      "\n🔳🔳🔳🔳🔳🔳░░░░░░░🔳🔳🔳🔳🔳🔳";
			//Console.WriteLine(entranceRoom);
			this.Doors = new List<Door>();
			this.Doors.Add(new Door(Direction.left,this));
			this.Doors.Add(new Door(Direction.up,this));
			this.Doors.Add(new Door(Direction.right,this));
		}

		public DungeonRoom(Door enterDoor, DungeonRoom backRoom)
		{
			numbersOfRooms++;
			idRoom = numbersOfRooms;

			//TODO: Set Asyncronic programming;
			this.Titles = GenerateTitles(); //public
			this.Doors = GenerateDoors(this, enterDoor, backRoom); //private
			this.Enemy = GenerateEnemy(this); //private
		}
		
		public string[,] GenerateTitles()
		{
			var random = new Random();
			Vector2 roomSize = new Vector2();
			roomSize.X = random.Next(12) + 3;
			roomSize.Y = random.Next(12) + 3;
			string[,] titles;
			titles = new string[(int)roomSize.X, (int)roomSize.Y];
			int floor;
			for(int i = 0; i < (int)roomSize.X; i++) {
				for(int j = 0; j < (int)roomSize.Y; j++) {
					if(i == 0 || j == 0 || i == (int)roomSize.X - 1 || j == (int)roomSize.Y - 1) {
						titles[i, j] = "🔳";
					} else {
						floor = random.Next(100);
						if(floor <= 10) {
							titles[i, j] = "🌫 ";
						} else if(floor <= 20) {
							titles[i, j] = " 🌫";
						} else if(floor < 99) {
							titles[i, j] = "  ";
						} else if(floor == 99) {
							titles[i, j] = " 🕸";
						}
					}
				}
			}
			//GenerateDoors(enterDoor, backRoom);
			//GenerateEnemy();
			return titles;
		}

		private List<Door> GenerateDoors(DungeonRoom room, Door enterDoor, DungeonRoom backRoom) {
			
			var random = new Random();
			int doorsQuantity = random.Next(3);
			
			Vector2 roomSize = new Vector2();
			roomSize.X = room.Titles.GetLength(0);
			roomSize.Y = room.Titles.GetLength(1);

			var doors = new List<Door>();

			var doorsParams = new{
				RoomSize = roomSize,
				DoorsQuantity = doorsQuantity,
				Doors = doors
			};

			room.Doors = GenerateGateway(room, enterDoor, backRoom, doorsParams);
			return GenerateNewDoors(doorsParams);
		}
		private List<Door> GenerateGateway(DungeonRoom room, Door enterDoor, DungeonRoom backRoom, dynamic doorsParams) {
			var random = new Random();
			Vector2 doorPosition = new Vector2();
			doorPosition.X = random.Next((int)doorsParams.RoomSize.X - 2) + 1;
			doorPosition.Y = random.Next((int)doorsParams.RoomSize.Y - 2) + 1;
			List<Door> Doors = doorsParams.Doors;
			switch(enterDoor.Direction) {
				case Direction.up:
					room.Titles[(int)doorsParams.RoomSize.X - 1, (int)doorPosition.Y] = enterDoor.mapIcon;
					break;
				case Direction.left:
					room.Titles[(int)doorPosition.X, (int)doorsParams.RoomSize.Y - 1] = enterDoor.mapIcon;
					break;
				case Direction.down:
					room.Titles[0, (int)doorPosition.Y] = enterDoor.mapIcon;
					break;
				case Direction.right:
					room.Titles[(int)doorPosition.X, 0] = enterDoor.mapIcon;
					break;
			}

			Direction direction = enterDoor.InvertDoor().Direction;
			Door backDoor = new Door(direction, this);
			backDoor.FrontRoom = backRoom;
			Doors.Add(backDoor);

			return Doors;
		}

		private List<Door> GenerateNewDoors(dynamic doorsParams) {
			var random = new Random();
			Vector2 DoorPosition = new Vector2();
			DoorPosition.X = random.Next((int)doorsParams.RoomSize.X - 2) + 1;
			DoorPosition.Y = random.Next((int)doorsParams.RoomSize.Y - 2) + 1;
			DungeonRoom room = this;
			for(int i = 0; i < doorsParams.DoorsQuantity; i++) {

				Direction direction = (Direction)random.Next(3);
				while(IsDoorAble(room.Doors, direction) == false)
				{
					direction = (Direction)random.Next(3);
				}

				Door newDoor = new Door(direction, this);//???
				room.Doors.Add(newDoor);

				//Draw new door on Titles
				switch((int)newDoor.Direction) {
					case 0:
						room.Titles[/*(int)doorsParams.RoomSize.X - 1*/0, (int)DoorPosition.Y] = newDoor.mapIcon;
						//doorsParams.direction = Direction.down;
						break;
					case 1:
						room.Titles[(int)DoorPosition.X, 0] = newDoor.mapIcon;
						//doorsParams.direction = Direction.left;
						break;
					case 2:
						room.Titles[/*0*/(int)doorsParams.RoomSize.X - 1, (int)DoorPosition.Y] = newDoor.mapIcon;
						//doorsParams.direction = Direction.up;
						break;
					case 3:
						room.Titles[(int)DoorPosition.X, (int)doorsParams.RoomSize.Y - 1] = newDoor.mapIcon;
						//doorsParams.direction = Direction.right;
						break;

					//case 0:
					//	room.Titles[doorsParams.MaxX - 1, doorsParams.doorPositionY] = "🚪";
					//	doorsParams.direction = Direction.down;
					//	break;
					//case 1:
					//	room.Titles[doorsParams.doorPositionX, 0] = "🚪";
					//	doorsParams.direction = Direction.left;
					//	break;
					//case 2:
					//	room.Titles[0, doorsParams.doorPositionY] = "🚪";
					//	doorsParams.direction = Direction.up;
					//	break;
					//case 3:
					//	room.Titles[doorsParams.DoorPositionX, doorsParams.MaxY - 1] = "🚪";
					//	doorsParams.direction = Direction.right;
					//	break;
				}
				
			}
			return Doors;
		}
		public bool IsDoorAble(List<Door> Doors, Direction direction){
			bool thereIsDoor = true;
			foreach(var door in Doors) {
				if(door.Direction == direction) {
					thereIsDoor = false;
				}
			}

			return thereIsDoor;
		}

		private List<Enemy> GenerateEnemy(DungeonRoom room)
		{
			var maxX = room.Titles.GetLength(0);
			var maxY = room.Titles.GetLength(1);
			var random = new Random();
			int enemyType = random.Next(5);
			List<Enemy> Enemies = new List<Enemy>();
			Enemy enemy = null;
			switch (enemyType)
			{
				case 0:
					enemy = new VoidEnemy();
					break;
				case 1:
					enemy = new Rat();
					break;
				case 2:
					enemy = new Bat();
					break;
				case 3:
					enemy = new Spider();
					break;
				case 4:
					enemy = new VoidEnemy();
					break;
				case 5:
					enemy = new VoidEnemy();
					break;
			}
			Enemies.Add(enemy);
			int positionX = random.Next(maxX - 2) +1;
			int positionY = random.Next(maxY - 2) +1;

			room.Titles[positionX, positionY] = Enemies[0].Icon;

			if (enemyType == 0)
			{
				Enemies.RemoveRange(0, Enemies.Count);
			}

			return Enemies;
		}
		
		public Battle Hostil() {
			Battle battle = new Battle(Party.GetInstance().Heroes, Enemy);
			return battle;
		}
		
		public string[,] RemoveEnemy()
		{
			string[,] titles = this.Titles;
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
		
		public string Enter()
		{
			return DrawRoom(this.Titles);
		}
		
		private string DrawRoom(string[,] titles) {
			Vector2 roomSize = new Vector2();
			roomSize.X = titles.GetLength(0);
			roomSize.Y = titles.GetLength(1);
			string room = $"\n\nROOM:{idRoom}\n";
			for(int i = 0; i < roomSize.X; i++) {
				for(int j = 0; j < roomSize.Y; j++) {
					room += Titles[i, j];
				}
				room += "\n";
			}
			Console.WriteLine(room);
			return room;
		}
		
		
	}
}