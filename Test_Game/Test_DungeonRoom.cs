using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Game;
using System.IO;

namespace Test_Game {
	[TestFixture]
	class Test_DungeonRoom {
		private DungeonRoom dummyRoom;

		[SetUp]
		public void Init() {
			dummyRoom = new DungeonRoom(0); 
		}

		[TearDown]
		public void Cleanup() { /* ... */ }

		[Test]
		public void DungeonRoom_1parameter() {
			int expectedIdRoom = 0;
			DungeonRoom dungeonRoom;

			dungeonRoom = new DungeonRoom(0);

			Assert.AreEqual(expectedIdRoom, dungeonRoom.IdRoom);
		}
		[Test]
		public void DungeonRoom_5parameteres() {
			List<Door> Doors = new List<Door>();
			Doors.Add(new Door(Direction.left, dummyRoom));
			Doors.Add(new Door(Direction.up, dummyRoom));
			Doors.Add(new Door(Direction.right, dummyRoom));
			
			List<Enemy> Enemies = new List<Enemy>();
			Enemies.Add(new Rat());
			Enemies.Add(new Rat());

			string[,] Titles = {
				{ "🔳","🚪","🔳"},
				{ "🚪","  ","🚪"},
				{ "🔳","🚪","🔳"}
			};
			string RoomMap = "" +
				"\n🔳🚪🔳" +
				"\n🚪  🚪" +
				"\n🔳🚪🔳";

			DungeonRoom dungeonRoom = new DungeonRoom(0, Doors,Enemies,Titles,RoomMap);

			Assert.AreEqual(0, dungeonRoom.IdRoom);
			Assert.AreEqual(Doors, dungeonRoom.Doors);
			Assert.AreEqual(Enemies, dungeonRoom.Enemies);
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
		public void Hostil_ReturnSomething() {
			List<Hero> Heroes = new List<Hero>();
			List<Enemy> Enemies = new List<Enemy>();
			FactoryHero factory = new FactoryHero();
			Hero hero = factory.CreateHero(HeroClass.Warrior);
			Enemy enemy = new VoidEnemy();
			Heroes.Add(hero);
			Enemies.Add(enemy);
			dummyRoom.Enemies = Enemies;
			Party.GetInstance().Heroes = Heroes;
			var input = new StringReader("1");
			Console.SetIn(input);
			Console.SetIn(input);

			Assert.IsNotNull(dummyRoom.Hostil());
		}
		[Test]
		public void Hostil_GetBattle() {
			List<Hero> Heroes = new List<Hero>();
			List<Enemy> Enemies = new List<Enemy>();
			FactoryHero factory = new FactoryHero();
			Hero hero = factory.CreateHero(HeroClass.Warrior);
			Enemy enemy = new VoidEnemy();
			Heroes.Add(hero);
			Enemies.Add(enemy);
			dummyRoom.Enemies = Enemies;
			Party.GetInstance().Heroes = Heroes;
			var input = new StringReader("1");
			Console.SetIn(input);
			Console.SetIn(input);

			Type t = dummyRoom.Hostil().GetType();

			Assert.AreEqual(t, typeof(Battle));
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

			string[,] newDrawMap = dummyRoom.RemoveEnemyOf(drawMap);

			Assert.AreEqual(map, newDrawMap);
		}
	}
}
