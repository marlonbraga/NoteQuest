using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Heroes {
	public class Mage:Hero {
		public Mage() {
			this.Name = "Mage";
			this.Icon = "🧙‍";

			this.Strength = 11;
			this.Dextrity = 11;
			this.Contitution = 11;
			this.Inteligence = 11;
			this.Wisdow = 11;
			this.Charisma = 11;

			this.Defense = 10;
			this.Damage = 4;
			this.HeathPoint = 6;
		}
	}
}
