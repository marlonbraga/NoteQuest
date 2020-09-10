using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Game;
using Moq;
using System.IO;
using Game.Heroes;

namespace Test_Game {
	[TestFixture]
	class Test_FactoryHero {
		[SetUp]
		public void Init() { /* ... */ }

		[TearDown]
		public void Cleanup() { /* ... */ }

		[Test]
		public void CreateHero_PassWarriorHeroClass_WarriorInstance() {
			FactoryHero factoryHero = new FactoryHero();
			Hero hero = new Hero();

			hero = factoryHero.CreateHero(HeroClass.Warrior);

			Assert.IsTrue(hero is Warrior);
		}
		[Test]
		public void CreateHero_PassMageHeroClass_MageInstance() {
			FactoryHero factoryHero = new FactoryHero();
			Hero hero = new Hero();

			hero = factoryHero.CreateHero(HeroClass.Mage);

			Assert.IsTrue(hero is Mage);
		}
		[Test]
		public void CreateHero_PassThiefHeroClass_ThiefInstance() {
			FactoryHero factoryHero = new FactoryHero();
			Hero hero = new Hero();

			hero = factoryHero.CreateHero(HeroClass.Thief);

			Assert.IsTrue(hero is Thief);
		}
		[Test]
		public void CreateHero_PassClericHeroClass_ClericInstance() {
			FactoryHero factoryHero = new FactoryHero();
			Hero hero = new Hero();

			hero = factoryHero.CreateHero(HeroClass.Cleric);

			Assert.IsTrue(hero is Cleric);
		}
	}
}
