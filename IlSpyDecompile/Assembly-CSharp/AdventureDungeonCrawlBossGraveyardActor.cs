using System;
using System.Collections.Generic;
using Hearthstone.DungeonCrawl;
using UnityEngine;

public class AdventureDungeonCrawlBossGraveyardActor : Actor
{
	[Serializable]
	public class BossGraveyardActorVisualStyle
	{
		public DungeonRunVisualStyle VisualStyle;

		public Material BossBackerMaterial;
	}

	public MeshRenderer m_BossBackerRenderer;

	public List<BossGraveyardActorVisualStyle> m_BossGraveyardActorStyle;

	public void SetStyle(IDungeonCrawlData data)
	{
		DungeonRunVisualStyle visualStyle = data.VisualStyle;
		foreach (BossGraveyardActorVisualStyle item in m_BossGraveyardActorStyle)
		{
			if (visualStyle == item.VisualStyle)
			{
				m_BossBackerRenderer.SetMaterial(item.BossBackerMaterial);
				break;
			}
		}
	}
}
