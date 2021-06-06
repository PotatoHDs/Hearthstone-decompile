using System;
using Assets;

namespace FixedReward
{
	// Token: 0x02000B4D RID: 2893
	public static class FixedRewardUtils
	{
		// Token: 0x06009A5C RID: 39516 RVA: 0x0031B03C File Offset: 0x0031923C
		public static bool ShouldSkipRewardVisual(Achieve.RewardTiming timing, Reward reward)
		{
			return Achieve.RewardTiming.NEVER == timing || (Achieve.RewardTiming.OUT_OF_BAND == timing && SceneMgr.Get().GetMode() != SceneMgr.Mode.LOGIN) || FixedRewardUtils.IsNewVaniallyHeroUnlock(reward);
		}

		// Token: 0x06009A5D RID: 39517 RVA: 0x0031B062 File Offset: 0x00319262
		private static bool IsNewVaniallyHeroUnlock(Reward reward)
		{
			return RankMgr.Get().DidPromoteSelfThisSession && reward.FixedCardRewardData != null && GameUtils.IsVanillaHero(reward.FixedCardRewardData.CardID);
		}
	}
}
