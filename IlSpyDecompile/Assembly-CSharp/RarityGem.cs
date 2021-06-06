using UnityEngine;

public class RarityGem : MonoBehaviour
{
	public void SetRarityGem(TAG_RARITY rarity, TAG_CARD_SET cardSet)
	{
		Renderer component = GetComponent<Renderer>();
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
			break;
		case TAG_RARITY.RARE:
			component.GetMaterial().mainTextureOffset = new Vector2(0.118f, 0f);
			break;
		case TAG_RARITY.EPIC:
			component.GetMaterial().mainTextureOffset = new Vector2(0.239f, 0f);
			break;
		case TAG_RARITY.LEGENDARY:
			component.GetMaterial().mainTextureOffset = new Vector2(0.3575f, 0f);
			break;
		}
	}
}
