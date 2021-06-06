using System;
using Assets;

namespace FixedReward
{
	// Token: 0x02000B4F RID: 2895
	public class Reward
	{
		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06009A64 RID: 39524 RVA: 0x0031B0E8 File Offset: 0x003192E8
		// (set) Token: 0x06009A65 RID: 39525 RVA: 0x0031B0F0 File Offset: 0x003192F0
		public FixedReward.Type Type { get; set; }

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06009A66 RID: 39526 RVA: 0x0031B0F9 File Offset: 0x003192F9
		// (set) Token: 0x06009A67 RID: 39527 RVA: 0x0031B101 File Offset: 0x00319301
		public CardRewardData FixedCardRewardData { get; set; }

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06009A68 RID: 39528 RVA: 0x0031B10A File Offset: 0x0031930A
		// (set) Token: 0x06009A69 RID: 39529 RVA: 0x0031B112 File Offset: 0x00319312
		public CardBackRewardData FixedCardBackRewardData { get; set; }

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06009A6A RID: 39530 RVA: 0x0031B11B File Offset: 0x0031931B
		// (set) Token: 0x06009A6B RID: 39531 RVA: 0x0031B123 File Offset: 0x00319323
		public NetCache.CardDefinition FixedCraftableCardRewardData { get; set; }

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06009A6C RID: 39532 RVA: 0x0031B12C File Offset: 0x0031932C
		// (set) Token: 0x06009A6D RID: 39533 RVA: 0x0031B134 File Offset: 0x00319334
		public MetaAction MetaActionData { get; set; }

		// Token: 0x06009A6E RID: 39534 RVA: 0x0031B13D File Offset: 0x0031933D
		public Reward()
		{
			this.Type = FixedReward.Type.UNKNOWN;
			this.FixedCardRewardData = null;
			this.FixedCardBackRewardData = null;
			this.FixedCraftableCardRewardData = null;
			this.MetaActionData = null;
		}
	}
}
