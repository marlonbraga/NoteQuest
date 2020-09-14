using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Game {
	public enum HeroClass:int {
		Warrior = 1,
		Mage = 2,
		Thief = 3,
		Cleric = 4
	}
	public class Hero : Creature
	{
		public string heroClass;
		private int ModifyStrenght = 0;
		public Hero() {

		}
		public void Defend() {
			isDefending = true;
		}
		public int Skills() {
			return 0;
		}
	}
}