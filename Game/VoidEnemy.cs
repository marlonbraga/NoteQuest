using System;
using System.Collections.Generic;
using System.Text;

namespace Game {
	class VoidEnemy:Enemy {
		public VoidEnemy() {
			this.Name = "💀";
			this.Icon = "  ";
			this.Strength = 0;
			this.Dextrity = 0;
			this.Contitution = 0;
			this.Inteligence = 0;
			this.Wisdow = 0;
			this.Charisma = 0;

			this.HeathPoint = 0;
		}
	}
}
