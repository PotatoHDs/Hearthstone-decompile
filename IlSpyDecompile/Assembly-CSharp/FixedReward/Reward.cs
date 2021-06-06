using Assets;

namespace FixedReward
{
	public class Reward
	{
		public Assets.FixedReward.Type Type { get; set; }

		public CardRewardData FixedCardRewardData { get; set; }

		public CardBackRewardData FixedCardBackRewardData { get; set; }

		public NetCache.CardDefinition FixedCraftableCardRewardData { get; set; }

		public MetaAction MetaActionData { get; set; }

		public Reward()
		{
			Type = Assets.FixedReward.Type.UNKNOWN;
			FixedCardRewardData = null;
			FixedCardBackRewardData = null;
			FixedCraftableCardRewardData = null;
			MetaActionData = null;
		}
	}
}
