using System;

namespace bgs.types
{
	// Token: 0x0200027A RID: 634
	public struct PartyListenerEvent
	{
		// Token: 0x060025D2 RID: 9682 RVA: 0x00086108 File Offset: 0x00084308
		public PartyError ToPartyError()
		{
			return new PartyError
			{
				IsOperationCallback = (this.Type == PartyListenerEventType.OPERATION_CALLBACK),
				DebugContext = this.StringData,
				ErrorCode = (BattleNetErrors)this.UintData,
				Feature = (BnetFeature)(this.UlongData >> 32),
				FeatureEvent = (BnetFeatureEvent)((uint)(this.UlongData & (ulong)-1)),
				PartyId = this.PartyId,
				szPartyType = this.StringData2,
				StringData = this.StringData
			};
		}

		// Token: 0x0400100C RID: 4108
		public PartyListenerEventType Type;

		// Token: 0x0400100D RID: 4109
		public PartyId PartyId;

		// Token: 0x0400100E RID: 4110
		public BnetGameAccountId SubjectMemberId;

		// Token: 0x0400100F RID: 4111
		public BnetGameAccountId TargetMemberId;

		// Token: 0x04001010 RID: 4112
		public uint UintData;

		// Token: 0x04001011 RID: 4113
		public ulong UlongData;

		// Token: 0x04001012 RID: 4114
		public string StringData;

		// Token: 0x04001013 RID: 4115
		public string StringData2;

		// Token: 0x04001014 RID: 4116
		public byte[] BlobData;

		// Token: 0x020006F4 RID: 1780
		public enum AttributeChangeEvent_AttrType
		{
			// Token: 0x040022C5 RID: 8901
			ATTR_TYPE_NULL,
			// Token: 0x040022C6 RID: 8902
			ATTR_TYPE_LONG,
			// Token: 0x040022C7 RID: 8903
			ATTR_TYPE_STRING,
			// Token: 0x040022C8 RID: 8904
			ATTR_TYPE_BLOB
		}
	}
}
