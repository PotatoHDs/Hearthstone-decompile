using System;

namespace bgs
{
	// Token: 0x02000246 RID: 582
	public class PartyMember : OnlinePlayer
	{
		// Token: 0x0600247A RID: 9338 RVA: 0x00080F80 File Offset: 0x0007F180
		public bool HasRole(Enum role)
		{
			uint roleId = Convert.ToUInt32(role);
			return this.HasRole(roleId);
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x00080F9C File Offset: 0x0007F19C
		public bool HasRole(uint roleId)
		{
			if (this.RoleIds == null)
			{
				return false;
			}
			for (int i = 0; i < this.RoleIds.Length; i++)
			{
				if (this.RoleIds[i] == roleId)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x00080FD4 File Offset: 0x0007F1D4
		public bool IsLeader(PartyType partyType)
		{
			uint leaderRoleId = PartyMember.GetLeaderRoleId(partyType);
			return this.HasRole(leaderRoleId);
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x00080FEF File Offset: 0x0007F1EF
		public static uint GetLeaderRoleId(PartyType partyType)
		{
			switch (partyType)
			{
			case PartyType.SPECTATOR_PARTY:
				return Convert.ToUInt32(BnetParty.SpectatorPartyRoleSet.Leader);
			}
			return Convert.ToUInt32(BnetParty.FriendlyGameRoleSet.Inviter);
		}

		// Token: 0x04000EE5 RID: 3813
		public uint[] RoleIds = new uint[0];

		// Token: 0x04000EE6 RID: 3814
		public string BattleTag;

		// Token: 0x04000EE7 RID: 3815
		public string VoiceId;
	}
}
