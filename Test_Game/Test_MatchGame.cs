using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Game;

namespace Test_Game {
	[TestFixture]
	class Test_MatchGame {

		[Test]
		[Ignore("Ignore a test")]
		public void Starts_NormalConditions_CallParty () {
			MatchGame.GetInstance().Start();

			Assert.IsTrue(true);
		}

		[Test]
		public void GetInstance_MatchGameReturnsInstance() {
			MatchGame match;

			match = MatchGame.GetInstance();

			Assert.IsNotNull(match);
		}
	}
}
