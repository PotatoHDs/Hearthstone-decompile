using System;

namespace bgs
{
	public class PartyMember : OnlinePlayer
	{
		public uint[] RoleIds = new uint[0];

		public string BattleTag;

		public string VoiceId;

		public bool HasRole(Enum role)
		{
			uint roleId = Convert.ToUInt32(role);
			return HasRole(roleId);
		}

		public bool HasRole(uint roleId)
		{
			if (RoleIds == null)
			{
				return false;
			}
			for (int i = 0; i < RoleIds.Length; i++)
			{
				if (RoleIds[i] == roleId)
				{
					return true;
				}
			}
			return false;
		}

		public bool IsLeader(PartyType partyType)
		{
			uint leaderRoleId = GetLeaderRoleId(partyType);
			return HasRole(leaderRoleId);
		}

		public static uint GetLeaderRoleId(PartyType partyType)
		{
			return partyType switch
			{
				PartyType.SPECTATOR_PARTY => Convert.ToUInt32(BnetParty.SpectatorPartyRoleSet.Leader), 
				_ => Convert.ToUInt32(BnetParty.FriendlyGameRoleSet.Inviter), 
			};
		}
	}
}
