using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadQuestDialogOnProgress2DbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<QuestDialogOnProgress2DbfRecord> GetRecords()
	{
		QuestDialogOnProgress2DbfAsset questDialogOnProgress2DbfAsset = assetBundleRequest.asset as QuestDialogOnProgress2DbfAsset;
		if (questDialogOnProgress2DbfAsset != null)
		{
			for (int i = 0; i < questDialogOnProgress2DbfAsset.Records.Count; i++)
			{
				questDialogOnProgress2DbfAsset.Records[i].StripUnusedLocales();
			}
			return questDialogOnProgress2DbfAsset.Records;
		}
		return null;
	}

	public LoadQuestDialogOnProgress2DbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(QuestDialogOnProgress2DbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
