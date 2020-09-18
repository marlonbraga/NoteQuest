using System;
using System.Collections.Generic;
using System.Text;

namespace Game {
	public class Party {
		public List<Hero> Heroes { set; get; }

		private static Party _party;
		public static Party GetInstance() {
			if(_party == null) {
				_party = new Party();
			}
			return _party;
		}

		private Party()
		{
			ChoseClass();
		}

		private void ChoseClass()
		{
			InputOutputData.GetInstance().Write(":: Chose your class\n1-Warrior🧔🏽\n2-‍Mage🧙\n3-Thief👲🏼\n4-Cleric👴🏼\n :: ");
			string line = InputOutputData.GetInstance().Read();
			FactoryHero factoryHero = new FactoryHero();
			if (line == "1" || line == "2" || line == "3" || line == "4")
			{
				Heroes = new List<Hero>();
				Hero newHero = factoryHero.CreateHero((HeroClass)Int32.Parse(line));
				Heroes.Add(newHero);
				InputOutputData.GetInstance().Write($"A {Heroes[Heroes.Count - 1].Icon}{Heroes[Heroes.Count-1].Name} joins to party!");
			}
		}
	}
}