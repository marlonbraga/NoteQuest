using Game.Heroes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game {
	public class FactoryHero {

		public Hero CreateHero(HeroClass heroClass) {
			Hero hero;
			switch(heroClass) {
				case HeroClass.Warrior:
					hero = new Warrior();
					break;
				case HeroClass.Mage:
					hero = new Mage();
					break;
				case HeroClass.Thief:
					hero = new Thief();
					break;
				case HeroClass.Cleric:
					hero = new Cleric();
					break;
				default:
					hero = new Hero();
					break;
			}
			
			return hero;
		}
	}
}
