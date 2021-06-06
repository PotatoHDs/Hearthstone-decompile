namespace bgs
{
	public struct PartyError
	{
		public bool IsOperationCallback;

		public string DebugContext;

		public Error ErrorCode;

		public BnetFeature Feature;

		public BnetFeatureEvent FeatureEvent;

		public PartyId PartyId;

		public string szPartyType;

		public string StringData;

		public PartyType PartyType => BnetParty.GetPartyTypeFromString(szPartyType);
	}
}
