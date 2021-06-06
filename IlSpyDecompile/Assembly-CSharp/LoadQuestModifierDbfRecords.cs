using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadQuestModifierDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<QuestModifierDbfRecord> GetRecords()
	{
		QuestModifierDbfAsset questModifierDbfAsset = assetBundleRequest.asset as QuestModifierDbfAsset;
		if (questModifierDbfAsset != null)
		{
			for (int i = 0; i < questModifierDbfAsset.Records.Count; i++)
			{
				questModifierDbfAsset.Records[i].StripUnusedLocales();
			}
			return questModifierDbfAsset.Records;
		}
		return null;
	}

	public LoadQuestModifierDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(QuestModifierDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
