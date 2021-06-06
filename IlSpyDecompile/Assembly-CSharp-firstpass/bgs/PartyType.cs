using System.ComponentModel;

namespace bgs
{
	public enum PartyType
	{
		[Description("default")]
		DEFAULT,
		[Description("FriendlyGame")]
		FRIENDLY_CHALLENGE,
		[Description("SpectatorParty")]
		SPECTATOR_PARTY,
		[Description("BattlegroundsParty")]
		BATTLEGROUNDS_PARTY
	}
}
