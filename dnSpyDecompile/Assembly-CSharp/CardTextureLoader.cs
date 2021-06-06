using System;
using System.Collections;
using Blizzard.T5.AssetManager;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x02000875 RID: 2165
public class CardTextureLoader
{
	// Token: 0x060075F9 RID: 30201 RVA: 0x0025DCE4 File Offset: 0x0025BEE4
	public static bool Load(CardDef cardDef, CardPortraitQuality quality, bool prohibitRecursion = false)
	{
		if (cardDef == null)
		{
			return false;
		}
		bool flag = false;
		bool flag2 = false;
		cardDef.UpdateSpecialEvent();
		CardPortraitQuality portraitQuality = cardDef.GetPortraitQuality();
		bool flag3 = portraitQuality.TextureQuality < quality.TextureQuality;
		bool flag4 = !PlatformSettings.ShouldFallbackToLowRes;
		bool flag5 = (quality.LoadPremium || cardDef.m_AlwaysRenderPremiumPortrait) && !portraitQuality.LoadPremium && CardTextureLoader.PremiumAnimationExists(cardDef);
		bool flag6 = quality.TextureQuality == 3 && flag3 && flag4;
		bool flag7 = portraitQuality.TextureQuality == 0;
		if (flag6)
		{
			if (CardTextureLoader.HighQualityAvailable(cardDef))
			{
				CardTextureLoader.LoadHighQuality(cardDef);
				flag = true;
			}
			else if (!prohibitRecursion)
			{
				Processor.RunCoroutine(CardTextureLoader.LoadDeferred(cardDef, new Func<CardDef, bool>(CardTextureLoader.HighQualityAvailable), quality), null);
				prohibitRecursion = true;
			}
		}
		if (flag5)
		{
			if (CardTextureLoader.PremiumAnimationAvailable(cardDef))
			{
				CardTextureLoader.LoadGolden(cardDef);
				flag2 = true;
			}
			else if (!prohibitRecursion)
			{
				Processor.RunCoroutine(CardTextureLoader.LoadDeferred(cardDef, new Func<CardDef, bool>(CardTextureLoader.PremiumAnimationAvailable), quality), null);
				prohibitRecursion = true;
			}
		}
		if (flag7 && !flag)
		{
			CardTextureLoader.LoadLowQuality(cardDef);
			flag = true;
		}
		return flag || flag2;
	}

	// Token: 0x060075FA RID: 30202 RVA: 0x0025DDDB File Offset: 0x0025BFDB
	private static bool HighQualityAvailable(CardDef cardDef)
	{
		return !PlatformSettings.ShouldFallbackToLowRes && AssetLoader.Get().IsAssetAvailable(cardDef.GetPortraitRef());
	}

	// Token: 0x060075FB RID: 30203 RVA: 0x0025DDF8 File Offset: 0x0025BFF8
	public static bool PremiumAnimationAvailable(CardDef cardDef)
	{
		if (!cardDef)
		{
			return false;
		}
		IAssetLoader assetLoader = AssetLoader.Get();
		if (!assetLoader.IsAssetAvailable(cardDef.GetPremiumMaterialRef()))
		{
			return false;
		}
		AssetReference premiumPortraitRef = cardDef.GetPremiumPortraitRef();
		if (premiumPortraitRef != null && !string.IsNullOrEmpty(premiumPortraitRef.guid) && !assetLoader.IsAppropriateVariantAvailable(premiumPortraitRef, CardTextureLoader.GetCardTextureOptions(false)))
		{
			return false;
		}
		AssetReference premiumAnimationRef = cardDef.GetPremiumAnimationRef();
		return premiumAnimationRef == null || string.IsNullOrEmpty(premiumAnimationRef.guid) || assetLoader.IsAssetAvailable(premiumAnimationRef);
	}

	// Token: 0x060075FC RID: 30204 RVA: 0x0025DE7C File Offset: 0x0025C07C
	private static bool PremiumAnimationExists(CardDef cardDef)
	{
		if (!cardDef)
		{
			return false;
		}
		AssetReference premiumMaterialRef = cardDef.GetPremiumMaterialRef();
		return premiumMaterialRef != null && !string.IsNullOrEmpty(premiumMaterialRef.guid);
	}

	// Token: 0x060075FD RID: 30205 RVA: 0x0025DEAD File Offset: 0x0025C0AD
	private static IEnumerator LoadDeferred(CardDef cardDef, Func<CardDef, bool> toWaitFor, CardPortraitQuality quality)
	{
		while (!toWaitFor(cardDef))
		{
			if (!cardDef)
			{
				yield break;
			}
			yield return new WaitForSeconds(0.3f);
		}
		if (CardTextureLoader.Load(cardDef, quality, true))
		{
			foreach (Actor actor in UnityEngine.Object.FindObjectsOfType<Actor>())
			{
				if (actor.HasSameCardDef(cardDef))
				{
					actor.UpdateAllComponents();
				}
			}
		}
		yield break;
	}

	// Token: 0x060075FE RID: 30206 RVA: 0x0025DECC File Offset: 0x0025C0CC
	private static void LoadLowQuality(CardDef cardDef)
	{
		AssetReference portraitRef = cardDef.GetPortraitRef();
		if (portraitRef == null)
		{
			return;
		}
		using (AssetHandle<Texture> assetHandle = AssetLoader.Get().LoadAsset<Texture>(portraitRef, CardTextureLoader.GetCardTextureOptions(true)))
		{
			if (!assetHandle)
			{
				Error.AddDevFatalUnlessWorkarounds("CardTextureLoader.LoadLowQuality - Failed to load asset for card {0}.  Portrait: {1}", new object[]
				{
					cardDef.name,
					(assetHandle == null) ? "missing" : "loaded"
				});
			}
			else
			{
				cardDef.OnPortraitLoaded(assetHandle, 1);
			}
		}
	}

	// Token: 0x060075FF RID: 30207 RVA: 0x0025DF50 File Offset: 0x0025C150
	private static bool LoadHighQuality(CardDef cardDef)
	{
		AssetReference portraitRef = cardDef.GetPortraitRef();
		if (portraitRef == null)
		{
			return false;
		}
		using (AssetHandle<Texture> assetHandle = AssetLoader.Get().LoadAsset<Texture>(portraitRef, CardTextureLoader.GetCardTextureOptions(false)))
		{
			if (assetHandle)
			{
				cardDef.OnPortraitLoaded(assetHandle, 3);
				return true;
			}
		}
		using (AssetHandle<Texture> assetHandle2 = AssetLoader.Get().LoadAsset<Texture>(portraitRef, CardTextureLoader.GetCardTextureOptions(true)))
		{
			if (assetHandle2)
			{
				cardDef.OnPortraitLoaded(assetHandle2, 1);
			}
			else
			{
				Error.AddDevFatalUnlessWorkarounds("CardTextureLoader.LoadHighQuality - Failed to load asset for card {0}.  Portrait: {1}", new object[]
				{
					cardDef.name,
					"missing"
				});
			}
		}
		return false;
	}

	// Token: 0x06007600 RID: 30208 RVA: 0x0025E00C File Offset: 0x0025C20C
	private static void LoadGolden(CardDef cardDef)
	{
		if (cardDef == null)
		{
			return;
		}
		AssetReference premiumMaterialRef = cardDef.GetPremiumMaterialRef();
		AssetReference premiumPortraitRef = cardDef.GetPremiumPortraitRef();
		AssetReference premiumAnimationRef = cardDef.GetPremiumAnimationRef();
		if (premiumMaterialRef == null)
		{
			return;
		}
		using (AssetHandle<Material> assetHandle = AssetLoader.Get().LoadAsset<Material>(premiumMaterialRef, AssetLoadingOptions.None))
		{
			using (AssetHandle<UberShaderAnimation> assetHandle2 = (premiumAnimationRef != null) ? AssetLoader.Get().LoadAsset<UberShaderAnimation>(premiumAnimationRef, AssetLoadingOptions.None) : null)
			{
				using (AssetHandle<Texture> assetHandle3 = (premiumPortraitRef != null) ? AssetLoader.Get().LoadAsset<Texture>(premiumPortraitRef, CardTextureLoader.GetCardTextureOptions(false)) : null)
				{
					if (!assetHandle)
					{
						Error.AddDevFatalUnlessWorkarounds("CardTextureLoader.LoadHighQuality - Failed to load asset for card {0}.  Material: {1}, Premium Portrait: {2}, Animation: {3}", new object[]
						{
							cardDef.name,
							(assetHandle == null) ? "missing" : "loaded",
							(assetHandle3 == null) ? "missing" : "loaded",
							(assetHandle2 == null) ? "missing" : "loaded"
						});
					}
					else
					{
						cardDef.OnPremiumMaterialLoaded(assetHandle, assetHandle3, assetHandle2);
					}
				}
			}
		}
	}

	// Token: 0x06007601 RID: 30209 RVA: 0x0025E128 File Offset: 0x0025C328
	private static AssetLoadingOptions GetCardTextureOptions(bool forceLowRes)
	{
		if (forceLowRes || PlatformSettings.ShouldFallbackToLowRes)
		{
			return AssetLoadingOptions.UseLowQuality;
		}
		return AssetLoadingOptions.None;
	}

	// Token: 0x04005D2D RID: 23853
	public const float POLL_INTERVAL = 0.3f;
}
