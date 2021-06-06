using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadCardSetSpellOverrideDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<CardSetSpellOverrideDbfRecord> GetRecords()
	{
		CardSetSpellOverrideDbfAsset cardSetSpellOverrideDbfAsset = assetBundleRequest.asset as CardSetSpellOverrideDbfAsset;
		if (cardSetSpellOverrideDbfAsset != null)
		{
			for (int i = 0; i < cardSetSpellOverrideDbfAsset.Records.Count; i++)
			{
				cardSetSpellOverrideDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardSetSpellOverrideDbfAsset.Records;
		}
		return null;
	}

	public LoadCardSetSpellOverrideDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardSetSpellOverrideDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
