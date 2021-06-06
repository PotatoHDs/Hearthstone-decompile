using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadScoreLabelDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<ScoreLabelDbfRecord> GetRecords()
	{
		ScoreLabelDbfAsset scoreLabelDbfAsset = assetBundleRequest.asset as ScoreLabelDbfAsset;
		if (scoreLabelDbfAsset != null)
		{
			for (int i = 0; i < scoreLabelDbfAsset.Records.Count; i++)
			{
				scoreLabelDbfAsset.Records[i].StripUnusedLocales();
			}
			return scoreLabelDbfAsset.Records;
		}
		return null;
	}

	public LoadScoreLabelDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ScoreLabelDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
