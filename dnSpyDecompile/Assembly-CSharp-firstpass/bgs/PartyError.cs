using System;

namespace bgs
{
	// Token: 0x02000241 RID: 577
	public struct PartyError
	{
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06002409 RID: 9225 RVA: 0x0007EF32 File Offset: 0x0007D132
		public PartyType PartyType
		{
			get
			{
				return BnetParty.GetPartyTypeFromString(this.szPartyType);
			}
		}

		// Token: 0x04000EC0 RID: 3776
		public bool IsOperationCallback;

		// Token: 0x04000EC1 RID: 3777
		public string DebugContext;

		// Token: 0x04000EC2 RID: 3778
		public Error ErrorCode;

		// Token: 0x04000EC3 RID: 3779
		public BnetFeature Feature;

		// Token: 0x04000EC4 RID: 3780
		public BnetFeatureEvent FeatureEvent;

		// Token: 0x04000EC5 RID: 3781
		public PartyId PartyId;

		// Token: 0x04000EC6 RID: 3782
		public string szPartyType;

		// Token: 0x04000EC7 RID: 3783
		public string StringData;
	}
}
