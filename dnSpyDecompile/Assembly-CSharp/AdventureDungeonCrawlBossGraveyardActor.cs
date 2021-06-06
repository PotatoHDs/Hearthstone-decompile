using System;
using System.Collections.Generic;
using Hearthstone.DungeonCrawl;
using UnityEngine;

// Token: 0x02000037 RID: 55
public class AdventureDungeonCrawlBossGraveyardActor : Actor
{
	// Token: 0x060001FA RID: 506 RVA: 0x0000B574 File Offset: 0x00009774
	public void SetStyle(IDungeonCrawlData data)
	{
		DungeonRunVisualStyle visualStyle = data.VisualStyle;
		foreach (AdventureDungeonCrawlBossGraveyardActor.BossGraveyardActorVisualStyle bossGraveyardActorVisualStyle in this.m_BossGraveyardActorStyle)
		{
			if (visualStyle == bossGraveyardActorVisualStyle.VisualStyle)
			{
				this.m_BossBackerRenderer.SetMaterial(bossGraveyardActorVisualStyle.BossBackerMaterial);
				break;
			}
		}
	}

	// Token: 0x04000170 RID: 368
	public MeshRenderer m_BossBackerRenderer;

	// Token: 0x04000171 RID: 369
	public List<AdventureDungeonCrawlBossGraveyardActor.BossGraveyardActorVisualStyle> m_BossGraveyardActorStyle;

	// Token: 0x020012AA RID: 4778
	[Serializable]
	public class BossGraveyardActorVisualStyle
	{
		// Token: 0x0400A439 RID: 42041
		public DungeonRunVisualStyle VisualStyle;

		// Token: 0x0400A43A RID: 42042
		public Material BossBackerMaterial;
	}
}
