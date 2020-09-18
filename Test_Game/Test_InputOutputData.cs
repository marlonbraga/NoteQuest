using System;
using System.IO;
using Game;
using NUnit.Framework;

namespace Test_Game {
	[TestFixture]
	class Test_InputOutputData {
		[SetUp]
		public void Init() { /* ... */ }

		[TearDown]
		public void Cleanup() { /* ... */ }

		[Test]
		public void Write_PutString_GetConsole() {
			string dummyString = "dummyString";
			var output = new StringWriter();
			Console.SetOut(output);

			Console.Write(dummyString);

			Assert.True(output.ToString().Contains(dummyString));
		}
		[Test]
		public void Read_GetString_ByConsole() {
			string dummyString = "dummyString";
			var input = new StringReader(dummyString);

			Console.SetIn(input);

			Assert.AreEqual(dummyString, Console.ReadLine());
		}
	}
}
