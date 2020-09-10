using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Game;
using Moq;
using System.IO;

namespace Test_Game {

    [TestFixture]
    class Test_Battle {
        [SetUp]
        public void Init() { /* ... */ }

        [TearDown]
        public void Cleanup() { /* ... */ }

        [Test]
		public void Battle_1HeroAnd1Enemy_BattleRuns() {
            string expectedOutput1 = "This room has [*] 💀s!";
            string expectedOutput2 = "💀 died!";
            FactoryHero factory = new FactoryHero();
                
            List<Hero> Heroes = new List<Hero>();
            List<Enemy> Enemies = new List<Enemy>();
            Hero hero = factory.CreateHero(HeroClass.Warrior);
            Enemy enemy = new VoidEnemy();
            Heroes.Add(hero);
            Enemies.Add(enemy);
            
            var input = new StringReader("1");
            Console.SetIn(input);
            var output = new StringWriter();
            Console.SetOut(output);

            Battle battle = new Battle(Heroes, Enemies);

            //Assert.That(output.ToString(), Is.EqualTo(string.Format("", Environment.NewLine)));
            Assert.True(output.ToString().Contains(expectedOutput1));
            Assert.True(output.ToString().Contains(expectedOutput2));
        }
	}
}
