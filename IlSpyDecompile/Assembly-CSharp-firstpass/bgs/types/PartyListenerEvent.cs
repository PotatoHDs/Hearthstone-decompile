namespace bgs.types
{
	public struct PartyListenerEvent
	{
		public enum AttributeChangeEvent_AttrType
		{
			ATTR_TYPE_NULL,
			ATTR_TYPE_LONG,
			ATTR_TYPE_STRING,
			ATTR_TYPE_BLOB
		}

		public PartyListenerEventType Type;

		public PartyId PartyId;

		public BnetGameAccountId SubjectMemberId;

		public BnetGameAccountId TargetMemberId;

		public uint UintData;

		public ulong UlongData;

		public string StringData;

		public string StringData2;

		public byte[] BlobData;

		public PartyError ToPartyError()
		{
			PartyError result = default(PartyError);
			result.IsOperationCallback = Type == PartyListenerEventType.OPERATION_CALLBACK;
			result.DebugContext = StringData;
			result.ErrorCode = (BattleNetErrors)UintData;
			result.Feature = (BnetFeature)(UlongData >> 32);
			result.FeatureEvent = (BnetFeatureEvent)(UlongData & 0xFFFFFFFFu);
			result.PartyId = PartyId;
			result.szPartyType = StringData2;
			result.StringData = StringData;
			return result;
		}
	}
}
