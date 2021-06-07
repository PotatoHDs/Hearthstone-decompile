using System;

namespace Blizzard.MobileAuth
{
	[Serializable]
	public struct BattleTagChangeValue
	{
		public string battleTag;

		public BattleTagInfo.AccountInfo.BattleTag battleTagInformation;

		public BattleTagChangeValue(string battleTag, BattleTagInfo.AccountInfo.BattleTag battleTagInformation)
		{
			this.battleTag = battleTag;
			this.battleTagInformation = battleTagInformation;
		}
	}
}
