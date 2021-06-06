using System;
using System.Collections;
using Blizzard.T5.AssetManager;
using Hearthstone.Core;
using UnityEngine;

public class CardTextureLoader
{
	public const float POLL_INTERVAL = 0.3f;

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
		bool num = (quality.LoadPremium || cardDef.m_AlwaysRenderPremiumPortrait) && !portraitQuality.LoadPremium && PremiumAnimationExists(cardDef);
		bool num2 = quality.TextureQuality == 3 && flag3 && flag4;
		bool flag5 = portraitQuality.TextureQuality == 0;
		if (num2)
		{
			if (HighQualityAvailable(cardDef))
			{
				LoadHighQuality(cardDef);
				flag = true;
			}
			else if (!prohibitRecursion)
			{
				Processor.RunCoroutine(LoadDeferred(cardDef, HighQualityAvailable, quality));
				prohibitRecursion = true;
			}
		}
		if (num)
		{
			if (PremiumAnimationAvailable(cardDef))
			{
				LoadGolden(cardDef);
				flag2 = true;
			}
			else if (!prohibitRecursion)
			{
				Processor.RunCoroutine(LoadDeferred(cardDef, PremiumAnimationAvailable, quality));
				prohibitRecursion = true;
			}
		}
		if (flag5 && !flag)
		{
			LoadLowQuality(cardDef);
			flag = true;
		}
		return flag || flag2;
	}

	private static bool HighQualityAvailable(CardDef cardDef)
	{
		if (!PlatformSettings.ShouldFallbackToLowRes)
		{
			return AssetLoader.Get().IsAssetAvailable(cardDef.GetPortraitRef());
		}
		return false;
	}

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
		if (premiumPortraitRef != null && !string.IsNullOrEmpty(premiumPortraitRef.guid) && !assetLoader.IsAppropriateVariantAvailable(premiumPortraitRef, GetCardTextureOptions(forceLowRes: false)))
		{
			return false;
		}
		AssetReference premiumAnimationRef = cardDef.GetPremiumAnimationRef();
		if (premiumAnimationRef != null && !string.IsNullOrEmpty(premiumAnimationRef.guid) && !assetLoader.IsAssetAvailable(premiumAnimationRef))
		{
			return false;
		}
		return true;
	}

	private static bool PremiumAnimationExists(CardDef cardDef)
	{
		if (!cardDef)
		{
			return false;
		}
		AssetReference premiumMaterialRef = cardDef.GetPremiumMaterialRef();
		if (premiumMaterialRef != null)
		{
			return !string.IsNullOrEmpty(premiumMaterialRef.guid);
		}
		return false;
	}

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
		if (!Load(cardDef, quality, prohibitRecursion: true))
		{
			yield break;
		}
		Actor[] array = UnityEngine.Object.FindObjectsOfType<Actor>();
		foreach (Actor actor in array)
		{
			if (actor.HasSameCardDef(cardDef))
			{
				actor.UpdateAllComponents();
			}
		}
	}

	private static void LoadLowQuality(CardDef cardDef)
	{
		AssetReference portraitRef = cardDef.GetPortraitRef();
		if (portraitRef == null)
		{
			return;
		}
		using AssetHandle<Texture> assetHandle = AssetLoader.Get().LoadAsset<Texture>(portraitRef, GetCardTextureOptions(forceLowRes: true));
		if (!assetHandle)
		{
			Error.AddDevFatalUnlessWorkarounds("CardTextureLoader.LoadLowQuality - Failed to load asset for card {0}.  Portrait: {1}", cardDef.name, (assetHandle == null) ? "missing" : "loaded");
		}
		else
		{
			cardDef.OnPortraitLoaded(assetHandle, 1);
		}
	}

	private static bool LoadHighQuality(CardDef cardDef)
	{
		AssetReference portraitRef = cardDef.GetPortraitRef();
		if (portraitRef == null)
		{
			return false;
		}
		using (AssetHandle<Texture> assetHandle = AssetLoader.Get().LoadAsset<Texture>(portraitRef, GetCardTextureOptions(forceLowRes: false)))
		{
			if ((bool)assetHandle)
			{
				cardDef.OnPortraitLoaded(assetHandle, 3);
				return true;
			}
		}
		using (AssetHandle<Texture> assetHandle2 = AssetLoader.Get().LoadAsset<Texture>(portraitRef, GetCardTextureOptions(forceLowRes: true)))
		{
			if ((bool)assetHandle2)
			{
				cardDef.OnPortraitLoaded(assetHandle2, 1);
			}
			else
			{
				Error.AddDevFatalUnlessWorkarounds("CardTextureLoader.LoadHighQuality - Failed to load asset for card {0}.  Portrait: {1}", cardDef.name, "missing");
			}
		}
		return false;
	}

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
		using AssetHandle<Material> assetHandle = AssetLoader.Get().LoadAsset<Material>(premiumMaterialRef);
		using AssetHandle<UberShaderAnimation> assetHandle3 = ((premiumAnimationRef != null) ? AssetLoader.Get().LoadAsset<UberShaderAnimation>(premiumAnimationRef) : null);
		using AssetHandle<Texture> assetHandle2 = ((premiumPortraitRef != null) ? AssetLoader.Get().LoadAsset<Texture>(premiumPortraitRef, GetCardTextureOptions(forceLowRes: false)) : null);
		if (!assetHandle)
		{
			Error.AddDevFatalUnlessWorkarounds("CardTextureLoader.LoadHighQuality - Failed to load asset for card {0}.  Material: {1}, Premium Portrait: {2}, Animation: {3}", cardDef.name, (assetHandle == null) ? "missing" : "loaded", (assetHandle2 == null) ? "missing" : "loaded", (assetHandle3 == null) ? "missing" : "loaded");
		}
		else
		{
			cardDef.OnPremiumMaterialLoaded(assetHandle, assetHandle2, assetHandle3);
		}
	}

	private static AssetLoadingOptions GetCardTextureOptions(bool forceLowRes)
	{
		if (forceLowRes || PlatformSettings.ShouldFallbackToLowRes)
		{
			return AssetLoadingOptions.UseLowQuality;
		}
		return AssetLoadingOptions.None;
	}
}
