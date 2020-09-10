using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Heroes {
	public class Warrior:Hero {
		public Warrior() {
			this.Name = "Warrior";
			this.Icon = "🧔🏽";

			this.Strength = 11;
			this.Dextrity = 11;
			this.Contitution = 11;
			this.Inteligence = 11;
			this.Wisdow = 11;
			this.Charisma = 11;

			this.Defense = 10;
			this.Damage = 8;
			this.HeathPoint = 14;
		}
	}
}
