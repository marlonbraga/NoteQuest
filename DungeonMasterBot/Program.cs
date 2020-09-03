using System;

namespace DungeonMasterBot {
	class Program {
		static void Enable()
		{
			Console.OutputEncoding = System.Text.Encoding.Unicode;
		}
		static void Main(string[] args)
		{
			Enable();
			MatchGame.GetInstance().Start();
		}
	}
}
