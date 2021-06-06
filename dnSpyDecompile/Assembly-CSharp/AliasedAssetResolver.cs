using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

// Token: 0x0200084C RID: 2124
public class AliasedAssetResolver : IAliasedAssetResolver, IService
{
	// Token: 0x0600731F RID: 29471 RVA: 0x002450CA File Offset: 0x002432CA
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(IAssetLoader)
		};
	}

	// Token: 0x06007320 RID: 29472 RVA: 0x00250A55 File Offset: 0x0024EC55
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		if (AssetLoaderPrefs.AssetLoadingMethod == AssetLoaderPrefs.ASSET_LOADING_METHOD.ASSET_BUNDLES)
		{
			this.m_assetBundleResolver.LoadFromBundle();
		}
		yield break;
	}

	// Token: 0x06007321 RID: 29473 RVA: 0x00250A64 File Offset: 0x0024EC64
	public void Shutdown()
	{
		this.m_assetBundleResolver.Shutdown();
	}

	// Token: 0x06007322 RID: 29474 RVA: 0x00250A71 File Offset: 0x0024EC71
	public AssetReference GetCardDefAssetRefFromCardId(string cardId)
	{
		AssetReference cardDefAssetRefFromCardId = this.m_assetBundleResolver.GetCardDefAssetRefFromCardId(cardId);
		if (cardDefAssetRefFromCardId == null)
		{
			AliasedAssetResolver.SendMissingCardDefTelemetry(cardId);
		}
		return cardDefAssetRefFromCardId;
	}

	// Token: 0x06007323 RID: 29475 RVA: 0x00250A88 File Offset: 0x0024EC88
	private static void SendMissingCardDefTelemetry(string cardId)
	{
		if (Application.isEditor)
		{
			Log.Telemetry.Print("Missing CardDef found in editor - not sending missing asset telemetry for cardId={0}, extension=prefab, filepath=unknown", new object[]
			{
				cardId
			});
			return;
		}
		TelemetryManager.Client().SendAssetNotFound("CardDef", string.Empty, string.Empty, string.Format("{0}.prefab", cardId));
	}

	// Token: 0x04005BB8 RID: 23480
	private readonly AssetBundlesAliasedAssetResolver m_assetBundleResolver = new AssetBundlesAliasedAssetResolver();
}
