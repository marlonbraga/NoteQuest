using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Game {
	public class Monster:Creature {
		public int[] Level = { 1, 2, 3 };
		public Monster() {
		
		}
		public Monster(string jsonString) {
			Monster monster = JsonSerializer.Deserialize<Monster>(jsonString);
			this.Name = monster.Name;
			this.Icon = monster.Icon;
			this.Level = monster.Level;
			this.Strength = monster.Strength;
			this.Defense = monster.Defense;
			this.Damage = monster.Damage;
			this.HeathPoint = monster.HeathPoint;
			this.SkillPoint = monster.SkillPoint;
		}
	}
}
