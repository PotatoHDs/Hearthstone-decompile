using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadQuestPoolDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<QuestPoolDbfRecord> GetRecords()
	{
		QuestPoolDbfAsset questPoolDbfAsset = assetBundleRequest.asset as QuestPoolDbfAsset;
		if (questPoolDbfAsset != null)
		{
			for (int i = 0; i < questPoolDbfAsset.Records.Count; i++)
			{
				questPoolDbfAsset.Records[i].StripUnusedLocales();
			}
			return questPoolDbfAsset.Records;
		}
		return null;
	}

	public LoadQuestPoolDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(QuestPoolDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
