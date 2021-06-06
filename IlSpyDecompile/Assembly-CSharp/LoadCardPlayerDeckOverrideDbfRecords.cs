using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadCardPlayerDeckOverrideDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<CardPlayerDeckOverrideDbfRecord> GetRecords()
	{
		CardPlayerDeckOverrideDbfAsset cardPlayerDeckOverrideDbfAsset = assetBundleRequest.asset as CardPlayerDeckOverrideDbfAsset;
		if (cardPlayerDeckOverrideDbfAsset != null)
		{
			for (int i = 0; i < cardPlayerDeckOverrideDbfAsset.Records.Count; i++)
			{
				cardPlayerDeckOverrideDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardPlayerDeckOverrideDbfAsset.Records;
		}
		return null;
	}

	public LoadCardPlayerDeckOverrideDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardPlayerDeckOverrideDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
