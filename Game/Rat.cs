using System;
using System.Collections.Generic;
using System.Text;

namespace Game {
	public class Rat:Monster {
		public Rat()
		{
			this.Name = "Rat";
			this.Icon = "🐀";

			this.Strength = 2;
			this.Dextrity = 11;
			this.Contitution = 9;
			this.Inteligence = 2;
			this.Wisdow = 10;
			this.Charisma = 4;

			this.Defense = 10;
			this.Damage = 8;

			this.HeathPoint = new Random().Next(6) + 1;
		}
	}
}
