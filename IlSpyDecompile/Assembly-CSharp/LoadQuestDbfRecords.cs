using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadQuestDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<QuestDbfRecord> GetRecords()
	{
		QuestDbfAsset questDbfAsset = assetBundleRequest.asset as QuestDbfAsset;
		if (questDbfAsset != null)
		{
			for (int i = 0; i < questDbfAsset.Records.Count; i++)
			{
				questDbfAsset.Records[i].StripUnusedLocales();
			}
			return questDbfAsset.Records;
		}
		return null;
	}

	public LoadQuestDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(QuestDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
