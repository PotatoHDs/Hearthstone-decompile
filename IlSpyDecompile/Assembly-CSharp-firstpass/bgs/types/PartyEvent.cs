namespace bgs.types
{
	public struct PartyEvent
	{
		public string eventName;

		public string eventData;

		public EntityId partyId;

		public EntityId otherMemberId;

		public BnetErrorInfo errorInfo;
	}
}
