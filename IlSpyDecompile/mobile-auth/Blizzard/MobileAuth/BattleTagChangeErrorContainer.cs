using System;
using System.Collections.Generic;

namespace Blizzard.MobileAuth
{
	[Serializable]
	public struct BattleTagChangeErrorContainer
	{
		public List<BattleTagChangeError> battleTagChangeErrors;

		public BattleTagChangeErrorContainer(List<BattleTagChangeError> battleTagChangeErrors)
		{
			this.battleTagChangeErrors = battleTagChangeErrors;
		}
	}
}
