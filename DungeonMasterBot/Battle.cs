using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonMasterBot {
	class Battle {
		public Battle(List<Hero> Heroes, List<Enemy> Enemies) {
			if(!IsBattleFinished(Heroes, Enemies)) {
				Console.WriteLine(PrintEnemies(Enemies));
				while(!IsBattleFinished(Heroes, Enemies)) {
					Turn(Heroes, Enemies);
				}

				MatchGame.ActualDungeonRoom.RemoveEnemy();
				MatchGame.ActualDungeonRoom.Enter();
			}
		}

		private bool IsBattleFinished(List<Hero> Heroes, List<Enemy> Enemies) {
			if ((Enemies.Count > 0) && (Heroes.Count > 0)){
				return false;
			}
			if(Heroes.Count <= 0) {
					MatchGame.GetInstance().EndGame();
			}
			return true;
		}
		private string PrintEnemies(List<Enemy> Enemies) {
			string enemies = "[";
			for(int i = 0; i < Enemies.Count; i++) {
				enemies += Enemies[i].Icon;
			}
			enemies += "] " + Enemies[0].Name + "s!";
			return $"This room has {enemies}\n";
			}
		private void Turn(List<Hero> Heroes, List<Enemy> Enemies) {
			RoundHeroes(Heroes, Enemies);
			RoundEnemies(Heroes, Enemies);
		}
		private void RoundHeroes(List<Hero> Heroes, List<Enemy> Enemies)
		{
			Hero hero;
			int response = 0;
			bool killedEnemy = false;
			for(int i = 0; i < Heroes.Count; i++) {
				hero = Heroes[i];
				hero.isDefending = false;
				Console.Write($"{hero.Icon}{hero.Name}({hero.HeathPoint}❤)\n[🗡-1|🛡-2|💼-3|🌟-4]: ");
				response = (int)int.Parse(Console.ReadLine());
				switch(response) {
					case 1:
						killedEnemy = hero.Attack(Enemies[0]);
						if(killedEnemy)
						{
							Enemies.Remove(Enemies[0]);
						}
						break;
					case 2:
						hero.Defend();
						break;
					case 3:
						break;
					case 4:
						break;
				}
			}
		}
		private void RoundEnemies(List<Hero> Heroes, List<Enemy> Enemies)
		{
			bool killedHero = false;
			for(int i = 0; i < Enemies.Count; i++) {
				killedHero = Enemies[i].Attack(Heroes[i]);
				if(killedHero) {
					Heroes.Remove(Heroes[i]);
				}
			}
			Console.WriteLine(" ");
		}
	}
}
