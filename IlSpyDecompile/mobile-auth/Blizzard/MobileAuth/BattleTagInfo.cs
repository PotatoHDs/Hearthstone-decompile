using System;
using System.Collections.Generic;

namespace Blizzard.MobileAuth
{
	[Serializable]
	public struct BattleTagInfo
	{
		[Serializable]
		public struct AccountInfo
		{
			[Serializable]
			public struct BattleTag
			{
				public string name;

				public string code;

				public BattleTag(string name, string code)
				{
					this.name = name;
					this.code = code;
				}

				public override string ToString()
				{
					return "BattleTag { " + name + ", " + code + " }";
				}
			}

			public bool hasBattleTag;

			public BattleTag battleTag;

			public bool hasFreeChange;

			public bool isGenerated;

			public AccountInfo(bool hasBattleTag, BattleTag battleTag, bool hasFreeChange, bool isGenerated)
			{
				this.hasBattleTag = hasBattleTag;
				this.battleTag = battleTag;
				this.hasFreeChange = hasFreeChange;
				this.isGenerated = isGenerated;
			}

			public override string ToString()
			{
				return "AccountInfo { " + hasBattleTag + ", " + battleTag.ToString() + ", " + hasFreeChange + ", " + isGenerated + " }";
			}
		}

		[Serializable]
		public struct ChangeRules
		{
			[Serializable]
			public struct Characters
			{
				public string start;

				public string end;

				public Characters(string start, string end)
				{
					this.start = start;
					this.end = end;
				}

				public override string ToString()
				{
					return "Chatacers { " + start + ", " + end + " }";
				}
			}

			public string name;

			public int minLength;

			public int maxLength;

			public List<Characters> characters;

			public ChangeRules(string name, int minLength, int maxLength, List<Characters> characters)
			{
				this.name = name;
				this.minLength = minLength;
				this.maxLength = maxLength;
				this.characters = characters;
			}

			public override string ToString()
			{
				return "ChangeRules { " + name + ", " + minLength + ", " + maxLength + ", " + characters.ToString() + " }";
			}
		}

		public AccountInfo accountInfo;

		public List<ChangeRules> changeRules;

		public BattleTagInfo(AccountInfo accountInfo, List<ChangeRules> changeRules)
		{
			this.accountInfo = accountInfo;
			this.changeRules = changeRules;
		}

		public override string ToString()
		{
			return "BattleTagInfo { " + accountInfo.ToString() + ", " + changeRules.ToString() + " }";
		}
	}
}
