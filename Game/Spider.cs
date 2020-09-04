using System;
using System.Collections.Generic;
using System.Text;

namespace Game {
	class Spider:Enemy {
		public Spider() {
			this.Name = "Spider";
			this.Icon = "🕷🕷";
			this.Strength = 2;
			this.Dextrity = 11;
			this.Contitution = 9;
			this.Inteligence = 2;
			this.Wisdow = 10;
			this.Charisma = 4;

			this.Defense = 10;
			this.Damage = 8;

			this.HeathPoint = new Random().Next(10) + 1;
		}
	}
}
