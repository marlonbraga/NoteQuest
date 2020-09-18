using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Game {
	public class FactoryMonster {
		private const string Path = "../../../../Game/Config/monsters.json";
		public List<Monster> AllMonsters = new List<Monster>();

		public FactoryMonster() {
			AllMonsters = FillPrototypeList();
		}

		public Monster CreateMonster(string monsterName) {
			Monster monster = new Monster();
			foreach(var m in AllMonsters) {
				if(m.Name == monsterName) {
					monster = m;
					break;
				}
			}
			return monster;
		}
		public Monster CreateMonster(int level) {
			Random random = new Random();
			List<Monster> monstersAtLevel = new List<Monster>();
			foreach(var m in AllMonsters) {
				if(m.Level.Contains(level)) {
					monstersAtLevel.Add(m);
				}
			}
			int index = random.Next(monstersAtLevel.Count);
			return monstersAtLevel[index];
		}

		public List<Monster> FillPrototypeList() {
			List<Monster> Monsters = new List<Monster>();
			string jsonMonsters;
			try
			{
				jsonMonsters = File.ReadAllText(Path);
				Monsters = JsonSerializer.Deserialize<List<Monster>>(jsonMonsters);
			}
			catch(FileNotFoundException e)
			{
				throw new FileNotFoundException(e.Message);
			}
			catch(JsonException e)
			{
				throw new JsonException(e.Message);
			}
			return Monsters;
		}
	}
}
