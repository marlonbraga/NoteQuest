using System;
using System.Collections.Generic;
using System.Text;

namespace Game {
	public abstract class InputOutputData{
		private static InputOutputData _inputOutputData;
		public InputOutputData() {	}
		public static InputOutputData GetInstance() {
			if(_inputOutputData == null) {
				_inputOutputData = new InputOutputConsole();
			}
			return _inputOutputData;
		}
		abstract public string Write(string mensage);
		abstract public string Read();
	}
}
