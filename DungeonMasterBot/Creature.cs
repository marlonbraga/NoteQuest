using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DungeonMasterBot {
	class Creature
	{
		public string Name { get; set; }
		public string Icon { get; set; }

		public int Strength { get; set; }
		public int Dextrity { get; set; }
		public int Contitution { get; set; }
		public int Inteligence { get; set; }
		public int Wisdow { get; set; }
		public int Charisma { get; set; }

		public int Defense { get; set; }

		public int Damage { get; set; }
		public int HeathPoint { get; set; }
		public int SkillPoint { get; set; }
		private bool isDead = false;
		public bool isDefending = false;

		public bool Attack(Creature creature)
		{
			var random = new Random();
			int test = random.Next(20)+1;
			test += this.Modify(Strength);
			Thread.Sleep(1500);
			Console.WriteLine($"{this.Icon}{this.Name} attacks[{test}]...");
			Thread.Sleep(750);
			if(test >= creature.Defense) {
				Console.WriteLine($"{this.Icon}{this.Name} hits! 💥");
				isDead = creature.Hit(random.Next(this.Damage)+1);
			}
			else
			{
				Console.WriteLine($"{this.Icon}{this.Name} missed!");
				isDead = false;
			}
			return isDead;
		}

		public bool Hit(int damage)
		{
			if (isDefending == true)
			{
				Double aux = damage/2;
				damage = Convert.ToInt16(Math.Floor(aux));
				Console.WriteLine($"{this.Icon}{this.Name} blocks🛡 [{damage}] of damage!");
			}
			Thread.Sleep(750);
			Console.WriteLine($"{this.Icon}{this.Name} lost [{damage}]HP🩸");
			this.HeathPoint -= damage;
			if (this.HeathPoint <= 0)
			{
				return this.Die();
			}
			else
			{
				return false;
			}
		}

		public bool Die()
		{
			Console.WriteLine($" 💀 {this.Name} died!");
			return true;
		}

		public int Modify(int atribute)
		{
			Double aux = (atribute - 10)/2;
			return Convert.ToInt16(Math.Floor(aux));
		}
	}
}
