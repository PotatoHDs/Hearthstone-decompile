using System;

namespace bgs
{
	// Token: 0x02000244 RID: 580
	public class PartyInfo
	{
		// Token: 0x06002476 RID: 9334 RVA: 0x00002654 File Offset: 0x00000854
		public PartyInfo()
		{
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x00080F3F File Offset: 0x0007F13F
		public PartyInfo(PartyId partyId, PartyType type)
		{
			this.Id = partyId;
			this.Type = type;
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x00080F55 File Offset: 0x0007F155
		public override string ToString()
		{
			return string.Format("{0}:{1}", this.Type.ToString(), this.Id.ToString());
		}

		// Token: 0x04000EE2 RID: 3810
		public PartyId Id;

		// Token: 0x04000EE3 RID: 3811
		public PartyType Type;
	}
}
