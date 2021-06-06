using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadKeywordTextDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<KeywordTextDbfRecord> GetRecords()
	{
		KeywordTextDbfAsset keywordTextDbfAsset = assetBundleRequest.asset as KeywordTextDbfAsset;
		if (keywordTextDbfAsset != null)
		{
			for (int i = 0; i < keywordTextDbfAsset.Records.Count; i++)
			{
				keywordTextDbfAsset.Records[i].StripUnusedLocales();
			}
			return keywordTextDbfAsset.Records;
		}
		return null;
	}

	public LoadKeywordTextDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(KeywordTextDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
