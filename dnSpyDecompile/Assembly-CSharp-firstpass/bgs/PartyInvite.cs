using System;

namespace bgs
{
	// Token: 0x02000247 RID: 583
	public class PartyInvite
	{
		// Token: 0x0600247F RID: 9343 RVA: 0x00002654 File Offset: 0x00000854
		public PartyInvite()
		{
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x00081032 File Offset: 0x0007F232
		public PartyInvite(ulong inviteId, PartyId partyId, PartyType type)
		{
			this.InviteId = inviteId;
			this.PartyId = partyId;
			this.PartyType = type;
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x0008104F File Offset: 0x0007F24F
		public uint GetFlags()
		{
			return (uint)this.InviteFlags;
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x00081057 File Offset: 0x0007F257
		public void SetFlags(uint flagsValue)
		{
			this.InviteFlags = (PartyInvite.Flags)flagsValue;
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06002483 RID: 9347 RVA: 0x00081060 File Offset: 0x0007F260
		public bool IsRejoin
		{
			get
			{
				return (this.InviteFlags & PartyInvite.Flags.RESERVATION) == PartyInvite.Flags.RESERVATION;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x00081060 File Offset: 0x0007F260
		public bool IsReservation
		{
			get
			{
				return (this.InviteFlags & PartyInvite.Flags.RESERVATION) == PartyInvite.Flags.RESERVATION;
			}
		}

		// Token: 0x04000EE8 RID: 3816
		public ulong InviteId;

		// Token: 0x04000EE9 RID: 3817
		public PartyId PartyId;

		// Token: 0x04000EEA RID: 3818
		public PartyType PartyType;

		// Token: 0x04000EEB RID: 3819
		public string InviterName;

		// Token: 0x04000EEC RID: 3820
		public string InviteeName;

		// Token: 0x04000EED RID: 3821
		public BnetGameAccountId InviterId;

		// Token: 0x04000EEE RID: 3822
		public BnetGameAccountId InviteeId;

		// Token: 0x04000EEF RID: 3823
		private PartyInvite.Flags InviteFlags;

		// Token: 0x020006DB RID: 1755
		[Flags]
		public enum Flags
		{
			// Token: 0x04002256 RID: 8790
			RESERVATION = 1,
			// Token: 0x04002257 RID: 8791
			REJOIN = 1
		}
	}
}
