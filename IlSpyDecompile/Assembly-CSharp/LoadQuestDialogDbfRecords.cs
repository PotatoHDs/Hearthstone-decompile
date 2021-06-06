using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadQuestDialogDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<QuestDialogDbfRecord> GetRecords()
	{
		QuestDialogDbfAsset questDialogDbfAsset = assetBundleRequest.asset as QuestDialogDbfAsset;
		if (questDialogDbfAsset != null)
		{
			for (int i = 0; i < questDialogDbfAsset.Records.Count; i++)
			{
				questDialogDbfAsset.Records[i].StripUnusedLocales();
			}
			return questDialogDbfAsset.Records;
		}
		return null;
	}

	public LoadQuestDialogDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(QuestDialogDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
