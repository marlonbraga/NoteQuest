using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using System.IO;
using Game;
using System.Numerics;

namespace Test_Game {
	[TestFixture]
	class Test_DungeonRoomBuilder {
		DungeonRoomBuilder dungeonRoomBuilder;

		[SetUp]
		public void Init() {
			dungeonRoomBuilder = new DungeonRoomBuilder();
		}

		[TearDown]
		public void Cleanup() { /* ... */ }

		[Test]
		public void BuildRoom_ReturnGatewayRoom() {
			DungeonRoom dungeonRoom;
			DungeonRoom dummyRoom = new DungeonRoom(0);
			List<Enemy> expectedEnemy = new List<Enemy>();
			List<Door> expectedDoors = new List<Door>();
			expectedDoors.Add(new Door(Direction.left, dummyRoom));
			expectedDoors.Add(new Door(Direction.up, dummyRoom));
			expectedDoors.Add(new Door(Direction.right, dummyRoom));
			Vector2 expectedroomSize = new Vector2(15);
			string expectedRoomMap = "" +
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
			
			dungeonRoom = dungeonRoomBuilder.BuildRoom();
			
			Assert.AreEqual(0, dungeonRoom.Enemies.Count);
			Assert.AreEqual(expectedDoors[0].Direction, dungeonRoom.Doors[0].Direction);
			Assert.AreEqual(expectedDoors[1].Direction, dungeonRoom.Doors[1].Direction);
			Assert.AreEqual(expectedDoors[2].Direction, dungeonRoom.Doors[2].Direction);
			Assert.AreEqual(expectedroomSize.X, dungeonRoom.Titles.GetLength(0));
			Assert.AreEqual(expectedroomSize.Y, dungeonRoom.Titles.GetLength(1));
			Assert.AreEqual(expectedRoomMap, dungeonRoom.RoomMap);
		}

		[Test]
		public void GenerateTitles_MaximunSize() {
			string[,] Titles = dungeonRoomBuilder.GenerateTitles();
			int maximunMatrizWidth = 15;
			int maximunMatrizHeigth = 15;

			Assert.LessOrEqual(Titles.GetLength(0), maximunMatrizHeigth);
			Assert.LessOrEqual(Titles.GetLength(1), maximunMatrizWidth);
		}
		[Test]
		public void GenerateTitles_MinimunSize() {
			string[,] Titles = dungeonRoomBuilder.GenerateTitles();
			int minimunMatrizWidth = 3;
			int minimunMatrizHeigth = 3;

			Assert.GreaterOrEqual(Titles.GetLength(0), minimunMatrizHeigth);
			Assert.GreaterOrEqual(Titles.GetLength(1), minimunMatrizWidth);
		}

		[Test]
		public void GenerateNewDoors__ReturnListAddedDoors() {
			DungeonRoom dummyRoom = new DungeonRoom(0);

			List<Door> Doors = new List<Door>();
			Doors.Add(new Door(Direction.up, dummyRoom));
			List<Enemy> Enemies = new List<Enemy>();
			string[,] Titles = {
				{ "🔳","🚪","🔳"},
				{ "🔳","  ","🔳"},
				{ "🔳","🔳","🔳"}
			};
			string RoomMap = "" +
				"\n🔳🚪🔳" +
				"\n🔳  🔳" +
				"\n🔳🔳🔳";

			DungeonRoom dungeonRoom = new DungeonRoom(0, Doors, Enemies, Titles, RoomMap);

			List<Door> returnedDoors;

			Vector2 roomSize = new Vector2(3);
			//Door enterDoor = new Door(Direction.down, dummyRoom);

			returnedDoors = dungeonRoomBuilder.GenerateNewDoors(dungeonRoom, roomSize, 3);

			Assert.Greater(returnedDoors.Count, 1);
		}
		public void GenerateNewDoors__ReturnListAddedByDownDoor() {
			DungeonRoom dummyRoom = new DungeonRoom(0);

			List<Door> expectedDoors = new List<Door>();
			expectedDoors.Add(new Door(Direction.left, dummyRoom));
			expectedDoors.Add(new Door(Direction.right, dummyRoom));
			List<Door> auxiliarListDoors = new List<Door>();
			auxiliarListDoors.Add(new Door(Direction.left, dummyRoom));
			auxiliarListDoors.Add(new Door(Direction.right, dummyRoom));
			auxiliarListDoors.Add(new Door(Direction.down, dummyRoom));
			List<Door> returnedDoors;
			Vector2 roomSize = new Vector2(5);
			Door enterDoor = new Door(Direction.up, dummyRoom);

			returnedDoors = dungeonRoomBuilder.GenerateNewDoors(dummyRoom, roomSize, 2);

			Assert.AreEqual(expectedDoors, returnedDoors);
		}
		public void GenerateNewDoors__ReturnListAddedByLeftDoor() {
			DungeonRoom dummyRoom = new DungeonRoom(0);

			List<Door> expectedDoors = new List<Door>();
			expectedDoors.Add(new Door(Direction.up, dummyRoom));
			expectedDoors.Add(new Door(Direction.right, dummyRoom));
			List<Door> auxiliarListDoors = new List<Door>();
			auxiliarListDoors.Add(new Door(Direction.up, dummyRoom));
			auxiliarListDoors.Add(new Door(Direction.right, dummyRoom));
			auxiliarListDoors.Add(new Door(Direction.left, dummyRoom));
			List<Door> returnedDoors;
			Vector2 roomSize = new Vector2(5);
			Door enterDoor = new Door(Direction.right, dummyRoom);

			returnedDoors = dungeonRoomBuilder.GenerateNewDoors(dummyRoom, roomSize, 2);

			Assert.AreEqual(expectedDoors, returnedDoors);
		}
		public void GenerateNewDoors__ReturnListAddedByRightDoor() {
			DungeonRoom dummyRoom = new DungeonRoom(0);

			List<Door> expectedDoors = new List<Door>();
			expectedDoors.Add(new Door(Direction.up, dummyRoom));
			expectedDoors.Add(new Door(Direction.left, dummyRoom));
			List<Door> auxiliarListDoors = new List<Door>();
			auxiliarListDoors.Add(new Door(Direction.up, dummyRoom));
			auxiliarListDoors.Add(new Door(Direction.left, dummyRoom));
			auxiliarListDoors.Add(new Door(Direction.right, dummyRoom));
			List<Door> returnedDoors;
			Vector2 roomSize = new Vector2(5);
			Door enterDoor = new Door(Direction.left, dummyRoom);

			returnedDoors = dungeonRoomBuilder.GenerateNewDoors(dummyRoom, roomSize, 2);

			Assert.AreEqual(expectedDoors, returnedDoors);
		}

		//-------------------------------

		[Test]
		public void IsDoorAble_EmptyDoorList_ReturnTrue() {
			List<Door> Doors = new List<Door>();
			Direction direction = Direction.right;
			bool isDoorAble = false;

			isDoorAble = dungeonRoomBuilder.IsDoorAble(Doors, direction);

			Assert.IsTrue(isDoorAble);
		}
		[Test]
		public void IsDoorAble_FullDoorList_ReturnFalse() {
			DungeonRoom dummyRoom = new DungeonRoom(0);
			List<Door> Doors = new List<Door>();
			Doors.Add(new Door(Direction.right, dummyRoom));
			Doors.Add(new Door(Direction.left, dummyRoom));
			Doors.Add(new Door(Direction.up, dummyRoom));
			Doors.Add(new Door(Direction.down, dummyRoom));
			Direction direction = Direction.right;
			bool isDoorAble = false;

			isDoorAble = dungeonRoomBuilder.IsDoorAble(Doors, direction);

			Assert.IsFalse(isDoorAble);
		}
		[Test]
		public void IsDoorAble_PartialEmptyDoorList_ReturnFalse() {
			DungeonRoom dummyRoom = new DungeonRoom(0);
			List<Door> Doors = new List<Door>();
			Doors.Add(new Door(Direction.right, dummyRoom));
			Direction direction = Direction.right;
			bool isDoorAble = false;

			isDoorAble = dungeonRoomBuilder.IsDoorAble(Doors, direction);

			Assert.IsFalse(isDoorAble);
		}
		[Test]
		public void IsDoorAble_PartialEmptyDoorList_ReturnTrue() {
			DungeonRoom dummyRoom = new DungeonRoom(0);
			List<Door> Doors = new List<Door>();
			Doors.Add(new Door(Direction.up, dummyRoom));
			Doors.Add(new Door(Direction.down, dummyRoom));
			Doors.Add(new Door(Direction.left, dummyRoom));
			Direction direction = Direction.right;
			bool isDoorAble = false;

			isDoorAble = dungeonRoomBuilder.IsDoorAble(Doors, direction);

			Assert.IsTrue(isDoorAble);
		}

		[Test]
		public void GenerateGateway_SettingUpEnterdoorAndBackdoor_CreateUpEnterDoor() {
			DungeonRoom dummyNewRoom = new DungeonRoom(0);
			DungeonRoom dummyBackRoom = new DungeonRoom(0);
			Door enterDoor = new Door(Direction.up, dummyNewRoom);
			Door door;

			door = dungeonRoomBuilder.GenerateGateway(dummyNewRoom, enterDoor, dummyBackRoom);

			Assert.AreEqual(Direction.down, door.Direction);
			Assert.AreEqual(dummyBackRoom, door.FrontRoom);
			Assert.AreEqual("⏬", door.optionIcon);
		}
		[Test]
		public void GenerateGateway_SettingLeftEnterdoorAndBackdoor_CreateLeftEnterDoor() {
			DungeonRoom dummyNewRoom = new DungeonRoom(0);
			DungeonRoom dummyBackRoom = new DungeonRoom(0);
			Door enterDoor = new Door(Direction.left, dummyBackRoom);
			Door door;

			door = dungeonRoomBuilder.GenerateGateway(dummyNewRoom, enterDoor, dummyBackRoom);

			Assert.AreEqual(Direction.right, door.Direction);
			Assert.AreEqual(dummyBackRoom, door.FrontRoom);
			Assert.AreEqual("⏩", door.optionIcon);
		}
		[Test]
		public void GenerateGateway_SettingDownEnterdoorAndBackdoor_CreateDownEnterDoor() {
			DungeonRoom dummyNewRoom = new DungeonRoom(0);
			DungeonRoom dummyBackRoom = new DungeonRoom(0);
			Door enterDoor = new Door(Direction.down, dummyBackRoom);
			Door door;

			door = dungeonRoomBuilder.GenerateGateway(dummyNewRoom, enterDoor, dummyBackRoom);

			Assert.AreEqual(Direction.up, door.Direction);
			Assert.AreEqual(dummyBackRoom, door.FrontRoom);
			Assert.AreEqual("⏫", door.optionIcon);
		}
		[Test]
		public void GenerateGateway_SettingRightEnterdoorAndBackdoor_CreateRightEnterDoor() {
			DungeonRoom dummyNewRoom = new DungeonRoom(0);
			DungeonRoom dummyBackRoom = new DungeonRoom(0);
			Door enterDoor = new Door(Direction.right, dummyBackRoom);
			Door door;

			door = dungeonRoomBuilder.GenerateGateway(dummyNewRoom, enterDoor, dummyBackRoom);

			Assert.AreEqual(Direction.left, door.Direction);
			Assert.AreEqual(dummyBackRoom, door.FrontRoom);
			Assert.AreEqual("⏪", door.optionIcon);
		}
	}
}
