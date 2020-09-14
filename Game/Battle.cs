using System;
using System.Collections.Generic;
using System.Text;

namespace Game {
	public enum BattleStatus:int {
		Ready = 0,//🔼
		Started = 1,
		Stopped = 2,
		Victory = 3,
		Defeat = 4
	}
	public class Battle {
		public List<Hero> Heroes{ get;  private set; }
		public List<Monster> Monsters { get; private set; }
		public BattleStatus Status { get; private set; }
		public Battle(List<Hero> Heroes, List<Monster> Enemies) {
			this.Heroes = Heroes;
			this.Monsters = Enemies;
			Status = BattleStatus.Ready;
		}
		public Battle(Room dungeonRoom) {
			this.Heroes = Party.GetInstance().Heroes;
			this.Monsters = dungeonRoom.Monsters;
			Status = BattleStatus.Ready;
		}
		public BattleStatus Start() {
			Status = BattleStatus.Started;
			Console.WriteLine(PrintMonsters());
			while(!IsBattleFinished()) {
				Turn();
			}

			return Status;
		}

		private bool IsBattleFinished() {
			if ((Monsters.Count > 0) && (Heroes.Count > 0)){
				return false;
			}
			if(Heroes.Count <= 0) {
				Status = BattleStatus.Defeat;
				MatchGame.GetInstance().EndGame();
			}
			Status = BattleStatus.Victory;
			return true;
		}
		private string PrintMonsters() {
			string enemies = "[";
			for(int i = 0; i < Monsters.Count; i++) {
				enemies += Monsters[i].Icon;
			}
			enemies += "] " + Monsters[0].Name + "s!";
			return $"This room has {enemies}\n";
		}
		private void Turn() {
			RoundHeroes();
			RoundMonsters();
		}
		private void RoundHeroes()
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
						killedEnemy = hero.Attack(Monsters[0]);
						if(killedEnemy)
						{
							Monsters.Remove(Monsters[0]);
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
		private void RoundMonsters()
		{
			bool killedHero = false;
			for(int i = 0; i < Monsters.Count; i++) {
				killedHero = Monsters[i].Attack(Heroes[i]);
				if(killedHero) {
					Heroes.Remove(Heroes[i]);
				}
			}
			Console.WriteLine(" ");
		}
	}
}
