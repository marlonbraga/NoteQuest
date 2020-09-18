using NUnit.Framework;
using Game;
using Game.Heroes;
using NuGet.Frameworks;
using System.Collections.Generic;
using Castle.Core.Internal;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Test_Game {
	[TestFixture]
	class Test_Monster {
		private FactoryMonster factory;
		private Monster monster;

		[SetUp]
		public void Init() {
			factory = new FactoryMonster();
		}

		[Test]
		public void Monster_getJson_returnAMonster() {
			Monster dummyMonster = new Monster(){
				Name = "Goblin",
			    Icon = "👺",
			    Level = new int[] { 2, 3, 4 },
			    Strength = 4,

				Dextrity = 0,
				Contitution = 0,
				Inteligence = 0,
				Wisdow = 0,
				Charisma = 0,

				Defense = 12,
			    Damage = 8,
			    HeathPoint = 8,
			    SkillPoint = 0
			};
			string json = JsonSerializer.Serialize(dummyMonster);
			monster = new Monster(json);

			Assert.True(monster is Monster);
			Assert.AreEqual(monster.Name, dummyMonster.Name);
			Assert.AreEqual(monster.Icon, dummyMonster.Icon);
			Assert.AreEqual(monster.Level, dummyMonster.Level);//BUG
			Assert.AreEqual(monster.Strength, dummyMonster.Strength);
			Assert.AreEqual(monster.Defense, dummyMonster.Defense);
			Assert.AreEqual(monster.Damage, dummyMonster.Damage);
			Assert.AreEqual(monster.HeathPoint, dummyMonster.HeathPoint);
			Assert.AreEqual(monster.SkillPoint, dummyMonster.SkillPoint);
		}

		
	}
}
