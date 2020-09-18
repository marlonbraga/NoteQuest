using System;
using System.Collections.Generic;
using System.Text;

namespace Game {
	public class InputOutputConsole:InputOutputData {
		public override string Write(string mensage) {
			Console.Write(mensage);
			return mensage;
		}

		public override string Read() {
			string mensage;
			mensage = Console.ReadLine();
			return mensage;
		}
	}
}
