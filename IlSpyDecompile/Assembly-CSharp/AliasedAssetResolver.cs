using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

public class AliasedAssetResolver : IAliasedAssetResolver, IService
{
	private readonly AssetBundlesAliasedAssetResolver m_assetBundleResolver = new AssetBundlesAliasedAssetResolver();

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(IAssetLoader) };
	}

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		if (AssetLoaderPrefs.AssetLoadingMethod == AssetLoaderPrefs.ASSET_LOADING_METHOD.ASSET_BUNDLES)
		{
			m_assetBundleResolver.LoadFromBundle();
		}
		yield break;
	}

	public void Shutdown()
	{
		m_assetBundleResolver.Shutdown();
	}

	public AssetReference GetCardDefAssetRefFromCardId(string cardId)
	{
		AssetReference cardDefAssetRefFromCardId = m_assetBundleResolver.GetCardDefAssetRefFromCardId(cardId);
		if (cardDefAssetRefFromCardId == null)
		{
			SendMissingCardDefTelemetry(cardId);
		}
		return cardDefAssetRefFromCardId;
	}

	private static void SendMissingCardDefTelemetry(string cardId)
	{
		if (Application.isEditor)
		{
			Log.Telemetry.Print("Missing CardDef found in editor - not sending missing asset telemetry for cardId={0}, extension=prefab, filepath=unknown", cardId);
		}
		else
		{
			TelemetryManager.Client().SendAssetNotFound("CardDef", string.Empty, string.Empty, $"{cardId}.prefab");
		}
	}
}
