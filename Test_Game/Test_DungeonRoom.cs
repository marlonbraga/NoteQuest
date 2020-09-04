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
	}
}
