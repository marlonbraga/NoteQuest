using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Game;
using System.IO;

namespace Test_Game {
	[TestFixture]
	class Test_Room {
		private Room dummyRoom;

		[SetUp]
		public void Init() {
			dummyRoom = new Room(0); 
		}

		[TearDown]
		public void Cleanup() { /* ... */ }

		[Test]
		public void DungeonRoom_1parameter() {
			int expectedIdRoom = 0;
			Room dungeonRoom;

			dungeonRoom = new Room(0);

			Assert.AreEqual(expectedIdRoom, dungeonRoom.IdRoom);
		}
		[Test]
		public void DungeonRoom_5parameteres() {
			List<Door> Doors = new List<Door>();
			Doors.Add(new Door(Direction.left, dummyRoom));
			Doors.Add(new Door(Direction.up, dummyRoom));
			Doors.Add(new Door(Direction.right, dummyRoom));
			
			List<Monster> Monsters = new List<Monster>();
			Monsters.Add(new Monster(){Name = "Rat" });
			Monsters.Add(new Monster(){Name = "Rat" });

			string[,] Titles = {
				{ "🔳","🚪","🔳"},
				{ "🚪","  ","🚪"},
				{ "🔳","🚪","🔳"}
			};
			string RoomMap = "" +
				"\n🔳🚪🔳" +
				"\n🚪  🚪" +
				"\n🔳🚪🔳";

			Room dungeonRoom = new Room(0, Doors,Monsters,Titles,RoomMap);

			Assert.AreEqual(0, dungeonRoom.IdRoom);
			Assert.AreEqual(Doors, dungeonRoom.Doors);
			Assert.AreEqual(Monsters, dungeonRoom.Monsters);
			Assert.AreEqual(Titles, dungeonRoom.Titles);
			Assert.AreEqual(RoomMap, dungeonRoom.RoomMap);
		}

		[Test]
		public void Enter_RoomMapIsBlank_ReturnString() {
			string[,] title = {
				{ "🔳","🚪","🔳"},
				{ "🚪","  ","🚪"},
				{ "🔳","🚪","🔳"}
			};
			dummyRoom.Titles = title;
			dummyRoom.RoomMap = "";
			string drawMap;

			drawMap = dummyRoom.Enter();

			Assert.IsNotNull(drawMap);
		}
		[Test]
		public void Enter_RoomMapIsNotNull_ReturnString() {
			string map = dummyRoom.RoomMap = "" +
				"\n🔳🚪🔳" +
				"\n🚪  🚪" +
				"\n🔳🚪🔳";
			string drawMap;

			drawMap = dummyRoom.Enter();

			Assert.AreEqual(map, drawMap);
		}
		[Test]
		public void RemoveMonster_SettingMonsterOn_CreateMonster() {
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

			string[,] newDrawMap = dummyRoom.RemoveMonsterOf(drawMap);

			Assert.AreEqual(map, newDrawMap);
		}
	}
}
