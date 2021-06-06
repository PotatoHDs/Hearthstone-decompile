using System;

namespace bgs
{
	public class PartyInvite
	{
		[Flags]
		public enum Flags
		{
			RESERVATION = 0x1,
			REJOIN = 0x1
		}

		public ulong InviteId;

		public PartyId PartyId;

		public PartyType PartyType;

		public string InviterName;

		public string InviteeName;

		public BnetGameAccountId InviterId;

		public BnetGameAccountId InviteeId;

		private Flags InviteFlags;

		public bool IsRejoin => (InviteFlags & Flags.RESERVATION) == Flags.RESERVATION;

		public bool IsReservation => (InviteFlags & Flags.RESERVATION) == Flags.RESERVATION;

		public PartyInvite()
		{
		}

		public PartyInvite(ulong inviteId, PartyId partyId, PartyType type)
		{
			InviteId = inviteId;
			PartyId = partyId;
			PartyType = type;
		}

		public uint GetFlags()
		{
			return (uint)InviteFlags;
		}

		public void SetFlags(uint flagsValue)
		{
			InviteFlags = (Flags)flagsValue;
		}
	}
}
