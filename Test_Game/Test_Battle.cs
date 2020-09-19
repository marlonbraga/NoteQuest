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
        Monster monster;
        string expectedOutput1;
        string expectedOutput2;

        [SetUp]
        public void Init() {
            expectedOutput1 = "This room has [🐀] Rats!";
            expectedOutput2 = "Rat died!";
            monster = new Monster() { Name = "Rat", Icon = "🐀", HeathPoint = 0, Defense = 0 };
        }

        [Test]
        public void StartBattle_1HeroAnd1Monster_BattleRuns() {
            FactoryHero factory = new FactoryHero();
            List<Hero> Heroes = new List<Hero>();
            List<Monster> Monsters = new List<Monster>();
            Hero hero = factory.CreateHero(HeroClass.Warrior);
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
            Assert.True(output.ToString().Contains(expectedOutput1));
            Assert.True(output.ToString().Contains(expectedOutput2));
        }
        [Test]
        public void StartBattle_1Room_BattleRuns() {
            RoomBuilder roomBuilder = new RoomBuilder();
            Room room = roomBuilder.BuildRoom();

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
