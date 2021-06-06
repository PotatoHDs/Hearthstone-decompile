using System;

namespace bgs.types
{
	// Token: 0x02000277 RID: 631
	public struct PartyEvent
	{
		// Token: 0x04000FF0 RID: 4080
		public string eventName;

		// Token: 0x04000FF1 RID: 4081
		public string eventData;

		// Token: 0x04000FF2 RID: 4082
		public EntityId partyId;

		// Token: 0x04000FF3 RID: 4083
		public EntityId otherMemberId;

		// Token: 0x04000FF4 RID: 4084
		public BnetErrorInfo errorInfo;
	}
}
