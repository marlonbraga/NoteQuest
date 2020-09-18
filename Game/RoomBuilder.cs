using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Game {
	public class RoomBuilder {
		private Room dungeonRoom;
		private static int numbersOfRooms = 0;

		public Room BuildRoom() {
			numbersOfRooms++;
			dungeonRoom = new Room(numbersOfRooms);

			dungeonRoom.Monsters = new List<Monster>();
			dungeonRoom.Doors = new List<Door>();

			Vector2 roomSize = new Vector2(15);

			dungeonRoom.Titles = new string[(int)roomSize.X, (int)roomSize.Y];
			for(int i = 0; i < roomSize.X; i++) {
				for(int j = 0; j < roomSize.Y; j++) {
					if(i == 0 || j == 0 || i == roomSize.X - 1 || j == roomSize.Y - 1) {
						dungeonRoom.Titles[i, j] = "🔳";
					} else {
						dungeonRoom.Titles[i, j] = "  ";
					}

					if((i == 0 && j == 7) || (i == 7 && j == 0) || (i == 7 && j == roomSize.Y - 1)) {
						dungeonRoom.Titles[i, j] = "🚪";
					}
				}
			}
			dungeonRoom.RoomMap = "" +
								  "\n🔳🔳🔳🔳🔳🔳🔳🚪🔳🔳🔳🔳🔳🔳🔳" +
								  "\n🔳            ░░            🔳" +
								  "\n🔳  🔳🔳      ░░      🔳🔳  🔳" +
								  "\n🔳  🔳🔳      ░░      🔳🔳  🔳" +
								  "\n🔳            ░░            🔳" +
								  "\n🔳        ░░░░░░░░░░        🔳" +
								  "\n🔳       ░░░░░░░░░░░░       🔳" +
								  "\n🚪░░░░░░░░░░░░░░░░░░░░░░░░░░🚪" +
								  "\n🔳       ░░░░░░░░░░░░       🔳" +
								  "\n🔳        ░░░░░░░░░░        🔳" +
								  "\n🔳            ░░            🔳" +
								  "\n🔳  🔳🔳      ░░      🔳🔳  🔳" +
								  "\n🔳  🔳🔳     ░░░░     🔳🔳  🔳" +
								  "\n🔳          ░░░░░░          🔳" +
								  "\n🔳🔳🔳🔳🔳░░░░░░░░░░🔳🔳🔳🔳🔳";

			dungeonRoom.Doors = new List<Door>();
			dungeonRoom.Doors.Add(new Door(Direction.left, dungeonRoom));
			dungeonRoom.Doors.Add(new Door(Direction.up, dungeonRoom));
			dungeonRoom.Doors.Add(new Door(Direction.right, dungeonRoom));

			return dungeonRoom;
		}

		public Room BuildRoom(Door enterDoor) {
			dungeonRoom = new Room(++numbersOfRooms);
			dungeonRoom.RoomMap = "";
			dungeonRoom.Titles = GenerateTitles();
			dungeonRoom.Doors = GenerateDoors(dungeonRoom, enterDoor);
			dungeonRoom.Monsters = GenerateMonsters(dungeonRoom);

			return dungeonRoom;
		}

		public Room ObtainDungeonRoom(Door enterDoor) {
			Room dungeonRoom = BuildRoom(enterDoor);

			return dungeonRoom;
		}

		public string[,] GenerateTitles() {
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
			return titles;
		}

		private List<Door> GenerateDoors(Room room, Door enterDoor) {

			var random = new Random();
			int doorsQuantity = random.Next(3);

			Vector2 roomSize = new Vector2();
			roomSize.X = room.Titles.GetLength(0);
			roomSize.Y = room.Titles.GetLength(1);

			room.Doors.Add(GenerateGateway(room, enterDoor));
			room.Titles = AddGatewayToMap(room, roomSize);
			room.Doors = GenerateNewDoors(room, roomSize, doorsQuantity);
			return room.Doors;
		}
		
		public string[,] AddGatewayToMap(Room room, Vector2 RoomSize) {
			Door enterDoor = room.Doors[0];
			string[,] Titles = room.Titles;
			var random = new Random();
			Vector2 doorPosition = new Vector2();
			doorPosition.X = random.Next((int)RoomSize.X - 2) + 1;
			doorPosition.Y = random.Next((int)RoomSize.Y - 2) + 1;
			switch(enterDoor.Direction) {
				case Direction.down:
					Titles[(int)RoomSize.X - 1, (int)doorPosition.Y] = enterDoor.mapIcon;
					break;
				case Direction.right:
					Titles[(int)doorPosition.X, (int)RoomSize.Y - 1] = enterDoor.mapIcon;
					break;
				case Direction.up:
					Titles[0, (int)doorPosition.Y] = enterDoor.mapIcon;
					break;
				case Direction.left:
					Titles[(int)doorPosition.X, 0] = enterDoor.mapIcon;
					break;
			}
			return Titles;
		}
		public Door GenerateGateway(Room room, Door enterDoor) {
			Room previousRoom = enterDoor.previousRoom;
			Direction direction = enterDoor.InvertDoor().Direction;
			Door backDoor = new Door(direction, room);
			backDoor.FrontRoom = previousRoom;
			return backDoor;
		}
		
		public List<Door> GenerateNewDoors(Room room, Vector2 RoomSize, int DoorsQuantity) {
			var random = new Random();
			Vector2 DoorPosition = new Vector2();
			DoorPosition.X = random.Next((int)RoomSize.X - 2) + 1;
			DoorPosition.Y = random.Next((int)RoomSize.Y - 2) + 1;

			for(int i = 0; i < DoorsQuantity; i++) {

				Direction direction = (Direction)random.Next(4);
				while(IsDoorAble(room.Doors, direction) == false) {
					direction = (Direction)random.Next(4);
				}

				Door newDoor = new Door(direction, room);//???
				room.Doors.Add(newDoor);

				switch((int)newDoor.Direction) {
					case 0:
						room.Titles[0, (int)DoorPosition.Y] = newDoor.mapIcon;
						break;
					case 1:
						room.Titles[(int)DoorPosition.X, 0] = newDoor.mapIcon;
						break;
					case 2:
						room.Titles[(int)RoomSize.X - 1, (int)DoorPosition.Y] = newDoor.mapIcon;
						break;
					case 3:
						room.Titles[(int)DoorPosition.X, (int)RoomSize.Y - 1] = newDoor.mapIcon;
						break;
				}

			}
			return room.Doors;
		}

		public bool IsDoorAble(List<Door> Doors, Direction direction) {
			bool thereIsDoor = true;
			foreach(var door in Doors) {
				if(door.Direction == direction) {
					thereIsDoor = false;
				}
			}

			return thereIsDoor;
		}

		private List<Monster> GenerateMonsters(Room room) {
			FactoryMonster factory = new FactoryMonster();
			List<Monster> Monsters = new List<Monster>();
			var maxX = room.Titles.GetLength(0);
			var maxY = room.Titles.GetLength(1);

			var random = new Random();
			if(random.Next(100) > 50) {
				Monster monster = factory.CreateMonster(MatchGame.GetInstance().GameLevel);
				Monsters.Add(monster);

				int positionX = random.Next(maxX - 2) + 1;
				int positionY = random.Next(maxY - 2) + 1;

				room.Titles[positionX, positionY] = Monsters[0].Icon;
			}

			return Monsters;
		}

	}
}
