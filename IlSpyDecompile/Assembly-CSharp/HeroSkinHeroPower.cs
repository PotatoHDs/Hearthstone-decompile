using System.Collections;
using UnityEngine;

public class HeroSkinHeroPower : MonoBehaviour
{
	public Actor m_Actor;

	public Texture m_OriginalFrontTexture;

	public Texture m_OriginalBackTexture;

	private void Start()
	{
		if (SceneMgr.Get().IsInGame())
		{
			StartCoroutine(HeroSkinCustomHeroPowerTextures());
		}
	}

	public void SetFrontTexture(Texture tex)
	{
		GetComponent<Renderer>().GetMaterial().mainTexture = tex;
	}

	public void SetBackTexture(Texture tex)
	{
		Renderer component = GetComponent<Renderer>();
		component.GetMaterial(1).SetTexture("_SecondTex", tex);
		component.GetMaterial(2).mainTexture = tex;
	}

	private IEnumerator HeroSkinCustomHeroPowerTextures()
	{
		Card card = m_Actor.GetCard();
		while (card == null)
		{
			card = m_Actor.GetCard();
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
		}
	}

	private void OnFrontTextureLoaded(AssetReference assetRef, Object asset, object callbackData)
	{
		Texture frontTexture = asset as Texture2D;
		SetFrontTexture(frontTexture);
	}

	private void OnBackTextureLoaded(AssetReference assetRef, Object asset, object callbackData)
	{
		Texture backTexture = asset as Texture2D;
		SetBackTexture(backTexture);
	}
}
