using System;
using UnityEngine;

// Token: 0x02000B47 RID: 2887
public class PopUpController : MonoBehaviour
{
	// Token: 0x060098FB RID: 39163 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x060098FC RID: 39164 RVA: 0x00317C9C File Offset: 0x00315E9C
	private void Update()
	{
		if (this.LifeTime >= 0f)
		{
			this.LifeTime -= Time.deltaTime;
			if (this.LifeTime <= 0f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x060098FD RID: 39165 RVA: 0x00317CD8 File Offset: 0x00315ED8
	public void Populate(int currentValue, int totalValue, int cardID)
	{
		DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(cardID);
		if (cardDef == null)
		{
			return;
		}
		Texture portraitTexture = cardDef.CardDef.GetPortraitTexture();
		if (portraitTexture != null)
		{
			this.Portrait.GetComponent<Renderer>().GetMaterial().SetTexture("_MainTex", portraitTexture);
		}
		this.Banner.GetComponent<RewardBanner>().SetText(currentValue + " out of " + totalValue, "Entity: " + DefLoader.Get().GetEntityDef(cardID, true).GetName(), "");
	}

	// Token: 0x04007FF3 RID: 32755
	public float LifeTime = 4f;

	// Token: 0x04007FF4 RID: 32756
	public GameObject Portrait;

	// Token: 0x04007FF5 RID: 32757
	public GameObject Banner;
}
