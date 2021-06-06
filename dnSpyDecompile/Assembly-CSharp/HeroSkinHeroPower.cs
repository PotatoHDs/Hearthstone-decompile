using System;
using System.Collections;
using UnityEngine;

// Token: 0x020008DE RID: 2270
public class HeroSkinHeroPower : MonoBehaviour
{
	// Token: 0x06007DBD RID: 32189 RVA: 0x0028CA6B File Offset: 0x0028AC6B
	private void Start()
	{
		if (SceneMgr.Get().IsInGame())
		{
			base.StartCoroutine(this.HeroSkinCustomHeroPowerTextures());
		}
	}

	// Token: 0x06007DBE RID: 32190 RVA: 0x0028CA86 File Offset: 0x0028AC86
	public void SetFrontTexture(Texture tex)
	{
		base.GetComponent<Renderer>().GetMaterial().mainTexture = tex;
	}

	// Token: 0x06007DBF RID: 32191 RVA: 0x0028CA99 File Offset: 0x0028AC99
	public void SetBackTexture(Texture tex)
	{
		Renderer component = base.GetComponent<Renderer>();
		component.GetMaterial(1).SetTexture("_SecondTex", tex);
		component.GetMaterial(2).mainTexture = tex;
	}

	// Token: 0x06007DC0 RID: 32192 RVA: 0x0028CABF File Offset: 0x0028ACBF
	private IEnumerator HeroSkinCustomHeroPowerTextures()
	{
		Card card = this.m_Actor.GetCard();
		while (card == null)
		{
			card = this.m_Actor.GetCard();
			yield return 0;
		}
		Card heroCard = card.GetHeroCard();
		while (heroCard == null)
		{
			heroCard = card.GetHeroCard();
			yield return 0;
		}
		if (!heroCard.HasCardDef)
		{
			Debug.LogWarning("HeroSkinHeroPower: heroCardDef is null!");
			yield break;
		}
		yield break;
	}

	// Token: 0x06007DC1 RID: 32193 RVA: 0x0028CAD0 File Offset: 0x0028ACD0
	private void OnFrontTextureLoaded(AssetReference assetRef, UnityEngine.Object asset, object callbackData)
	{
		Texture frontTexture = asset as Texture2D;
		this.SetFrontTexture(frontTexture);
	}

	// Token: 0x06007DC2 RID: 32194 RVA: 0x0028CAEC File Offset: 0x0028ACEC
	private void OnBackTextureLoaded(AssetReference assetRef, UnityEngine.Object asset, object callbackData)
	{
		Texture backTexture = asset as Texture2D;
		this.SetBackTexture(backTexture);
	}

	// Token: 0x040065DD RID: 26077
	public Actor m_Actor;

	// Token: 0x040065DE RID: 26078
	public Texture m_OriginalFrontTexture;

	// Token: 0x040065DF RID: 26079
	public Texture m_OriginalBackTexture;
}
