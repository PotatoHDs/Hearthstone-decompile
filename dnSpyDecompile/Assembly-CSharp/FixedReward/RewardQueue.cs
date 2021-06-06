using System;
using System.Collections.Generic;
using System.Linq;
using Assets;

namespace FixedReward
{
	// Token: 0x02000B51 RID: 2897
	public class RewardQueue
	{
		// Token: 0x06009A73 RID: 39539 RVA: 0x0031B1CC File Offset: 0x003193CC
		public bool HasRewardsToShow(IEnumerable<Achieve.RewardTiming> timings)
		{
			return (from pair in this.m_rewardMap
			where timings.Contains(pair.Key)
			select pair).SelectMany((KeyValuePair<Achieve.RewardTiming, HashSet<RewardMapIDToShow>> pair) => pair.Value).Any<RewardMapIDToShow>();
		}

		// Token: 0x06009A74 RID: 39540 RVA: 0x0031B226 File Offset: 0x00319426
		public bool TryGetRewards(Achieve.RewardTiming timing, out HashSet<RewardMapIDToShow> rewards)
		{
			return this.m_rewardMap.TryGetValue(timing, out rewards);
		}

		// Token: 0x06009A75 RID: 39541 RVA: 0x0031B235 File Offset: 0x00319435
		public void Add(Achieve.RewardTiming timing, RewardMapIDToShow reward)
		{
			if (!this.m_rewardMap.ContainsKey(timing))
			{
				this.m_rewardMap[timing] = new HashSet<RewardMapIDToShow>();
			}
			this.m_rewardMap[timing].Add(reward);
		}

		// Token: 0x06009A76 RID: 39542 RVA: 0x0031B269 File Offset: 0x00319469
		public void Clear()
		{
			this.m_rewardMap.Clear();
		}

		// Token: 0x06009A77 RID: 39543 RVA: 0x0031B278 File Offset: 0x00319478
		public void Clear(Achieve.RewardTiming timing)
		{
			HashSet<RewardMapIDToShow> hashSet;
			if (this.m_rewardMap.TryGetValue(timing, out hashSet))
			{
				hashSet.Clear();
			}
		}

		// Token: 0x04008018 RID: 32792
		private readonly Map<Achieve.RewardTiming, HashSet<RewardMapIDToShow>> m_rewardMap = new Map<Achieve.RewardTiming, HashSet<RewardMapIDToShow>>();
	}
}
