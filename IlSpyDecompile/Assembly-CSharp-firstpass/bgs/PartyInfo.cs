namespace bgs
{
	public class PartyInfo
	{
		public PartyId Id;

		public PartyType Type;

		public PartyInfo()
		{
		}

		public PartyInfo(PartyId partyId, PartyType type)
		{
			Id = partyId;
			Type = type;
		}

		public override string ToString()
		{
			return $"{Type.ToString()}:{Id.ToString()}";
		}
	}
}
