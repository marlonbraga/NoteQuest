using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Game;

namespace Test_Game {
	[TestFixture]
	class Test_DungeonRoom {
		private DungeonRoom dungeonRoom;

		[SetUp]
		public void Init() {
			dungeonRoom = new DungeonRoom(); 
		}

		[TearDown]
		public void Cleanup() { /* ... */ }

		[Test]
		[Ignore("Ignore a test")]
		public void ReturnDungeonRoom() {
			//dungeonRoom;

			Assert.Pass();
		}
		[Test]
		[Ignore("Ignore a test")]
		public void ReturnGenertedRoom() {
			Assert.Pass();
		}
		[Test]
		[Ignore("Ignore a test")]
		public void ReturnRoomWithoutEnemies() {
			//RemoveEnemy();
			Assert.Pass();
		}
		[Test]
		public void IsDoorAble_EmptyDoorList_ReturnTrue() {
			List<Door> Doors = new List<Door>();
			Direction direction = Direction.right;
			bool isDoorAble = false;

			isDoorAble = dungeonRoom.IsDoorAble(Doors, direction);

			Assert.IsTrue(isDoorAble);
		}
		[Test]
		public void IsDoorAble_FullDoorList_ReturnFalse() {
			List<Door> Doors = new List<Door>();
			Doors.Add(new Door(Direction.right, dungeonRoom));
			Doors.Add(new Door(Direction.left, dungeonRoom));
			Doors.Add(new Door(Direction.up, dungeonRoom));
			Doors.Add(new Door(Direction.down, dungeonRoom));
			Direction direction = Direction.right;
			bool isDoorAble = false;

			isDoorAble = dungeonRoom.IsDoorAble(Doors, direction);

			Assert.IsFalse(isDoorAble);
		}
		[Test]
		public void IsDoorAble_PartialEmptyDoorList_ReturnFalse() {
			List<Door> Doors = new List<Door>();
			Doors.Add(new Door(Direction.right, dungeonRoom));
			Direction direction = Direction.right;
			bool isDoorAble = false;

			isDoorAble = dungeonRoom.IsDoorAble(Doors, direction);

			Assert.IsFalse(isDoorAble);
		}
		[Test]
		public void IsDoorAble_PartialEmptyDoorList_ReturnTrue() {
			List<Door> Doors = new List<Door>();
			Doors.Add(new Door(Direction.up, dungeonRoom));
			Doors.Add(new Door(Direction.down, dungeonRoom));
			Doors.Add(new Door(Direction.left, dungeonRoom));
			Direction direction = Direction.right;
			bool isDoorAble = false;

			isDoorAble = dungeonRoom.IsDoorAble(Doors, direction);

			Assert.IsTrue(isDoorAble);
		}
		[Test]
		public void GenerateTitles_MaximunSize() {
			string[,] Titles = dungeonRoom.GenerateTitles();
			int maximunMatrizWidth = 15;
			int maximunMatrizHeigth = 15;

			Assert.LessOrEqual(Titles.GetLength(0), maximunMatrizHeigth);
			Assert.LessOrEqual(Titles.GetLength(1), maximunMatrizWidth);
		}
		[Test]
		public void GenerateTitles_MinimunSize() {
			string[,] Titles = dungeonRoom.GenerateTitles();
			int minimunMatrizWidth = 3;
			int minimunMatrizHeigth = 3;

			Assert.GreaterOrEqual(Titles.GetLength(0), minimunMatrizHeigth);
			Assert.GreaterOrEqual(Titles.GetLength(1), minimunMatrizWidth);
		}
		[Test]
		public void Battle_ReturnSomething() {
			Assert.IsNotNull(dungeonRoom.Hostil());
		}
		[Test]
		public void Battle_GetBattle() {
			Type t = dungeonRoom.Hostil().GetType();

			Assert.AreEqual(t, typeof(Battle));
		}
		[Test]
		public void Enter_RoomMapIsBlank_ReturnString() {
			string drawMap;

			drawMap = new DungeonRoom().Enter();

			Assert.IsNotNull(drawMap);
		}
		[Test]
		public void Enter_RoomMapIsNotNull_ReturnString() {
			string map = dungeonRoom.RoomMap = ""+
				"\n🔳🚪🔳" +
				"\n🚪  🚪" +
				"\n🔳🚪🔳";
			string drawMap;

			drawMap = dungeonRoom.Enter();

			Assert.AreEqual(map, drawMap);
		}
		[Test]
		public void DungeonRoom_SettingEnterdoorAndBackdoor_CreateTitles() {
			Door enterDoor = new Door(Direction.up, dungeonRoom);
			DungeonRoom newDungeonRoom = new DungeonRoom(enterDoor, dungeonRoom);

			Assert.IsNotNull(newDungeonRoom.Titles);
		}

		[Test]
		public void DungeonRoom_SettingUpEnterdoorAndBackdoor_CreateUpEnterDoor() {
			Door enterDoor = new Door(Direction.up, dungeonRoom);
			DungeonRoom newDungeonRoom = new DungeonRoom(enterDoor, dungeonRoom);

			Assert.AreEqual(Direction.down, newDungeonRoom.Doors[0].Direction);
			Assert.AreEqual(dungeonRoom, newDungeonRoom.Doors[0].FrontRoom);
			Assert.AreEqual("⏬", newDungeonRoom.Doors[0].optionIcon);
		}
		[Test]
		public void DungeonRoom_SettingLeftEnterdoorAndBackdoor_CreateLeftEnterDoor() {
			Door enterDoor = new Door(Direction.left, dungeonRoom);
			DungeonRoom newDungeonRoom = new DungeonRoom(enterDoor, dungeonRoom);

			Assert.AreEqual(Direction.right, newDungeonRoom.Doors[0].Direction);
			Assert.AreEqual(dungeonRoom, newDungeonRoom.Doors[0].FrontRoom);
			Assert.AreEqual("⏩", newDungeonRoom.Doors[0].optionIcon);
		}
		[Test]
		public void DungeonRoom_SettingDownEnterdoorAndBackdoor_CreateDownEnterDoor() {
			Door enterDoor = new Door(Direction.down, dungeonRoom);
			DungeonRoom newDungeonRoom = new DungeonRoom(enterDoor, dungeonRoom);

			Assert.AreEqual(Direction.up, newDungeonRoom.Doors[0].Direction);
			Assert.AreEqual(dungeonRoom, newDungeonRoom.Doors[0].FrontRoom);
			Assert.AreEqual("⏫", newDungeonRoom.Doors[0].optionIcon);
		}
		[Test]
		public void DungeonRoom_SettingRightEnterdoorAndBackdoor_CreateRightEnterDoor() {
			Door enterDoor = new Door(Direction.right, dungeonRoom);
			DungeonRoom newDungeonRoom = new DungeonRoom(enterDoor, dungeonRoom);

			Assert.AreEqual(Direction.left	, newDungeonRoom.Doors[0].Direction	);
			Assert.AreEqual(dungeonRoom		, newDungeonRoom.Doors[0].FrontRoom);
			Assert.AreEqual("⏪", newDungeonRoom.Doors[0].optionIcon);
		}


		[Test]
		public void DungeonRoom_SettingEnterdoorAndBackdoor_CreateDoors() {
			Door enterDoor = new Door(Direction.up, dungeonRoom);
			DungeonRoom newDungeonRoom = new DungeonRoom(enterDoor, dungeonRoom);

			Assert.IsNotNull(newDungeonRoom.Doors);
		}
		[Test]
		public void DungeonRoom_SettingEnterdoorAndBackdoor_CreateEnemy() {
			Door enterDoor = new Door(Direction.up, dungeonRoom);
			DungeonRoom newDungeonRoom = new DungeonRoom(enterDoor, dungeonRoom);

			Assert.IsNotNull(newDungeonRoom.Enemy);
		}
		[Test]
		public void RemoveEnemy_SettingEnemyOn_CreateEnemy() {
			string[,] map = {
				{ "🔳","🚪","🔳"},
				{ "🚪","💀","🚪"},
				{ "🔳","🚪","🔳"}
			};
			string[,] drawMap = {
				{"🔳","🚪","🔳"},
				{"🚪","🐀","🚪"},
				{"🔳","🚪","🔳"}
			};

			string[,] newDrawMap = dungeonRoom.RemoveEnemy(drawMap);

			Assert.AreEqual(map, newDrawMap);
		}
	}
}
