using Assets;

namespace FixedReward
{
	public static class FixedRewardUtils
	{
		public static bool ShouldSkipRewardVisual(Achieve.RewardTiming timing, Reward reward)
		{
			if (Achieve.RewardTiming.NEVER == timing)
			{
				return true;
			}
			if (Achieve.RewardTiming.OUT_OF_BAND == timing && SceneMgr.Get().GetMode() != SceneMgr.Mode.LOGIN)
			{
				return true;
			}
			if (IsNewVaniallyHeroUnlock(reward))
			{
				return true;
			}
			return false;
		}

		private static bool IsNewVaniallyHeroUnlock(Reward reward)
		{
			if (!RankMgr.Get().DidPromoteSelfThisSession)
			{
				return false;
			}
			if (reward.FixedCardRewardData != null)
			{
				return GameUtils.IsVanillaHero(reward.FixedCardRewardData.CardID);
			}
			return false;
		}
	}
}
