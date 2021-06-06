using System;
using System.ComponentModel;

namespace bgs
{
	// Token: 0x02000240 RID: 576
	public enum PartyType
	{
		// Token: 0x04000EBC RID: 3772
		[Description("default")]
		DEFAULT,
		// Token: 0x04000EBD RID: 3773
		[Description("FriendlyGame")]
		FRIENDLY_CHALLENGE,
		// Token: 0x04000EBE RID: 3774
		[Description("SpectatorParty")]
		SPECTATOR_PARTY,
		// Token: 0x04000EBF RID: 3775
		[Description("BattlegroundsParty")]
		BATTLEGROUNDS_PARTY
	}
}
