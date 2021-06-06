namespace FixedReward
{
	public class MetaAction
	{
		public int MetaActionID { get; }

		public ulong MetaActionFlags { get; private set; }

		public MetaAction(int metaActionID)
		{
			MetaActionID = metaActionID;
			MetaActionFlags = 0uL;
		}

		public void UpdateFlags(ulong addFlags, ulong removeFlags)
		{
			MetaActionFlags |= addFlags;
			MetaActionFlags &= ~removeFlags;
		}

		public bool HasAllRequiredFlags(ulong requiredFlags)
		{
			return (MetaActionFlags & requiredFlags) == requiredFlags;
		}
	}
}
