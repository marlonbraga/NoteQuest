using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using System.Threading;
using System.Transactions;

namespace DungeonMasterBot {
	enum Direction:int {
		up = 0,//🔼
		left = 1,
		down = 2,
		right = 3
	}
	class DungeonRoom
	{
		public int idRoom;
		private static int numbersOfRooms = 0;
		public List<Door> Doors { set; get; }
		public List<Enemy> Enemy { set; get; }
		private string[,] titles;
		private int maxX;
		private int maxY;

		public DungeonRoom() {
			numbersOfRooms++;
			idRoom = numbersOfRooms;

			Enemy = new List<Enemy>();
			Doors = new List<Door>();

			maxX = 15;
			maxY = 15;
			titles = new string[maxX, maxY];
			int floor;
			for(int i = 0; i < maxX; i++) {
				for(int j = 0; j < maxY; j++) {
					if(i == 0 || j == 0 || i == maxX - 1 || j == maxY - 1) {
						titles[i, j] = "🔳";
					} else {
						titles[i, j] = "  ";
					}
					
					if ((i == 0 && j == 7)|| (i == 7 && j == 0)|| (i == 7 && j == maxY-1))
					{
						titles[i, j] = "🚪";
					} 
				}
			}
			DrawRoom();
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
			Doors = new List<Door>();
			Doors.Add(new Door(Direction.left,this));
			Doors.Add(new Door(Direction.up,this));
			Doors.Add(new Door(Direction.right,this));
		}
		public DungeonRoom(Door enterDoor, DungeonRoom backRoom)
		{
			numbersOfRooms++;
			idRoom = numbersOfRooms;

			GenerteRoom(enterDoor, backRoom);
		}
		public void GenerteRoom(Door enterDoor, DungeonRoom backRoom)
		{
			var random = new Random();
			maxX = random.Next(12) + 3;
			maxY = random.Next(12) + 3;
			titles = new string[maxX, maxY];
			int floor;
			for(int i = 0; i < maxX; i++) {
				for(int j = 0; j < maxY; j++) {
					if(i == 0 || j == 0 || i == maxX - 1 || j == maxY - 1) {
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
			MakeDoors(enterDoor, backRoom);
			GenerateEnemy();
		}
		public void Hostil()
		{
			Battle battle = new Battle(Party.GetInstance().Heroes, Enemy);
		}

		private void GenerateEnemy()
		{
			var random = new Random();
			int enemyType = random.Next(5);
			Enemy = new List<Enemy>();
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
			Enemy.Add(enemy);
			int positionX = random.Next(maxX - 2) +1;
			int positionY = random.Next(maxY - 2) +1;

			titles[positionX, positionY] = Enemy[0].Icon;

			if (enemyType == 0)
			{
				Enemy.RemoveRange(0, Enemy.Count);
			}
		}

		public void RemoveEnemy()
		{
			for(int i = 0; i < maxX; i++) {
				for(int j = 0; j < maxY; j++) {
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
		}
		private void MakeDoors(Door enterDoor, DungeonRoom backRoom) {
			var random = new Random();
			int DoorsQuantity = random.Next(3);
			int DoorsPosition = random.Next(3);
			int doorPositionX = random.Next(maxX - 2) + 1;
			int doorPositionY = random.Next(maxY - 2) + 1;
			Doors = new List<Door>();
			string doorIcon = "🚪";
			switch(enterDoor.Direction) {
				case Direction.up:
					titles[maxX - 1, doorPositionY] = doorIcon;
					break;
				case Direction.left:
					titles[doorPositionX, maxY - 1] = doorIcon;
					break;
				case Direction.down:
					titles[0, doorPositionY] = doorIcon;
					break;
				case Direction.right:
					titles[doorPositionX, 0] = doorIcon;
					break;
			}

			Direction direction = InvertDoor(enterDoor).Direction;
			Door backDoor = new Door(direction, this);
			backDoor.FrontRoom = backRoom;
			Doors.Add(backDoor);
			bool[] ablePositions={ true, true, true, true };
			ablePositions[(int) enterDoor.Direction] = false;


			//Others Doors
			for(int i = 0; i < DoorsQuantity; i++) {
				
				DoorsPosition = random.Next(3);
				while(ablePositions[DoorsPosition] == false)
				//while (enterDoor.Direction.ToString() == Direction.down.ToString())
				{
					DoorsPosition = random.Next(3);
				}
				ablePositions[DoorsPosition] = false;
				
				switch(DoorsPosition) {
					case 0:
						titles[maxX - 1, doorPositionY] = "🚪";
						direction = Direction.down;
						break;
					case 1:
						titles[doorPositionX, 0] = "🚪";
						direction = Direction.left;
						break;
					case 2:
						titles[0, doorPositionY] = "🚪";
						direction = Direction.up;
						break;
					case 3:
						titles[doorPositionX, maxY - 1] = "🚪";
						direction = Direction.right;
						break;
				}
				Doors.Add(new Door(direction, this));
			}
		}
		public void Enter()
		{
			DrawRoom();
		}
		private string DrawRoom() {
			string room = $"\n\nROOM:{idRoom}\n";
			for(int i = 0; i < maxX; i++) {
				for(int j = 0; j < maxY; j++) {
					room += titles[i, j];
				}
				room += "\n";
			}
			Console.WriteLine(room);
			return room;
		}
		private Door InvertDoor(Door door)
		{
			Direction direction = door.Direction;
			if(direction == Direction.up) {
				direction = Direction.down;
			} else if(direction == Direction.left) {
				direction = Direction.right;
			} else if(direction == Direction.down) {
				direction = Direction.up;
			} else if(direction == Direction.right) {
				direction = Direction.left;
			}
			door = new Door(direction, door.BackRoom);
			return door;
		}
	}
}