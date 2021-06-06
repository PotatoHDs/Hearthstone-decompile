using System;
using UnityEngine;

// Token: 0x02000139 RID: 313
public class RarityGem : MonoBehaviour
{
	// Token: 0x06001476 RID: 5238 RVA: 0x0007576C File Offset: 0x0007396C
	public void SetRarityGem(TAG_RARITY rarity, TAG_CARD_SET cardSet)
	{
		Renderer component = base.GetComponent<Renderer>();
		if (rarity == TAG_RARITY.FREE)
		{
			component.enabled = false;
			return;
		}
		component.enabled = true;
		switch (rarity)
		{
		default:
			component.GetMaterial().mainTextureOffset = new Vector2(0f, 0f);
			return;
		case TAG_RARITY.RARE:
			component.GetMaterial().mainTextureOffset = new Vector2(0.118f, 0f);
			return;
		case TAG_RARITY.EPIC:
			component.GetMaterial().mainTextureOffset = new Vector2(0.239f, 0f);
			return;
		case TAG_RARITY.LEGENDARY:
			component.GetMaterial().mainTextureOffset = new Vector2(0.3575f, 0f);
			return;
		}
	}
}
