using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DungeonMasterBot {
	class Hero : Creature
	{
		public string heroClass;
		private int ModifyStrenght = 0;
		public Hero(string heroClass)
		{
			this.heroClass = heroClass;
			if (heroClass == "1")
			{
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
			} else if (heroClass == "2")
			{
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
			} else if (heroClass == "3")
			{
				this.Name = "Thief";
				this.Icon = "👲🏼";

				this.Strength = 11;
				this.Dextrity = 11;
				this.Contitution = 11;
				this.Inteligence = 11;
				this.Wisdow = 11;
				this.Charisma = 11;

				this.Defense = 10;
				this.Damage = 6;
				this.HeathPoint = 10;
			} else if (heroClass == "4")
			{
				this.Name = "Cleric";
				this.Icon = "👴🏼";
				
				this.Strength = 11;
				this.Dextrity = 11;
				this.Contitution = 11;
				this.Inteligence = 11;
				this.Wisdow = 11;
				this.Charisma = 11;

				this.Defense = 10;
				this.Damage = 6;
				this.HeathPoint = 10;
			}
		}

		public void Defend() {
			isDefending = true;
		}
		public int Skills() {
			return 0;
		}
	}
}