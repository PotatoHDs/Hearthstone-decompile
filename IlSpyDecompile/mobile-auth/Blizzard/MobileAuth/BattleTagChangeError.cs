using System;

namespace Blizzard.MobileAuth
{
	[Serializable]
	public struct BattleTagChangeError
	{
		public string code;

		public string message;

		public BattleTagChangeError(string code, string message)
		{
			this.code = code;
			this.message = message;
		}
	}
}
