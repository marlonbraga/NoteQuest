using NUnit.Framework;
using Game;
using Game.Heroes;
using NuGet.Frameworks;
using System.Collections.Generic;
using Castle.Core.Internal;
using System;
using System.Linq;

namespace Test_Game {
	[TestFixture]
	class Test_FactoryMonster {
		private FactoryMonster factory;
		private Monster monster;

		[SetUp]
		public void Init() {
			factory = new FactoryMonster();
		}
		[Test]
		public void CreateMonster_MonsterNameByArg_returnMatchedMonster() {
			string monsterName = "Rat";
			
			monster = factory.CreateMonster(monsterName);

			Assert.AreEqual(monster.Name, monsterName);
		}
		[Test]
		public void CreateMonster_LevelByArg_returnAnyMatchedMonster() {
			int level = 1;
			
			monster = factory.CreateMonster(level);

			Assert.True(monster.Level.Contains(level));
		}
		[Test]
		public void FillPrototypeList_returnMonstersList() {
			List<Monster> Monsters;

			Monsters = factory.FillPrototypeList();

			Assert.GreaterOrEqual(Monsters.Count,1);
		}
		[Test]
		public void FillPrototypeList_Error() {
			List<Monster> Monsters;

			Monsters = factory.FillPrototypeList();

			//Assert.Throws();
		}
	}
}