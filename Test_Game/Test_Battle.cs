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
        public void StartBattle_1HeroAnd1Monster_BattleRuns() {
            string expectedOutput1 = "This room has [*] 💀s!";
            string expectedOutput2 = "💀 died!";
            FactoryHero factory = new FactoryHero();

            List<Hero> Heroes = new List<Hero>();
            List<Monster> Monsters = new List<Monster>();
            Hero hero = factory.CreateHero(HeroClass.Warrior);
            Monster monster = new VoidEnemy();
            Heroes.Add(hero);
            Monsters.Add(monster);
            BattleStatus result;

            var input = new StringReader("1");
            Console.SetIn(input);
            var output = new StringWriter();
            Console.SetOut(output);

            Battle battle = new Battle(Heroes, Monsters);
            result = battle.Start();

            Assert.AreEqual(BattleStatus.Victory, result);
            //Assert.That(output.ToString(), Is.EqualTo(string.Format("", Environment.NewLine)));
            Assert.True(output.ToString().Contains(expectedOutput1));
            Assert.True(output.ToString().Contains(expectedOutput2));
        }
        [Test]
        public void StartBattle_1Room_BattleRuns() {
            string expectedOutput1 = "This room has [*] 💀s!";
            string expectedOutput2 = "💀 died!";
            FactoryHero factory = new FactoryHero();
            DungeonRoomBuilder roomBuilder = new DungeonRoomBuilder();
            Room room = roomBuilder.BuildRoom();

            //Hero hero = factory.CreateHero(HeroClass.Warrior);
            Monster monster = new VoidEnemy();
            room.Monsters.Add(monster);
            BattleStatus result;

            var input = new StringReader("1\n1");
            Console.SetIn(input);
            var output = new StringWriter();
            Console.SetOut(output);

            Party.GetInstance();
            Battle battle = new Battle(room);
            result = battle.Start();

            Assert.AreEqual(BattleStatus.Victory, result);
            Assert.True(output.ToString().Contains(expectedOutput1));
            Assert.True(output.ToString().Contains(expectedOutput2));
        }
    }
}
