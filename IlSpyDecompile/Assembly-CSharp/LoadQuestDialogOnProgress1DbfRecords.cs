using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadQuestDialogOnProgress1DbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<QuestDialogOnProgress1DbfRecord> GetRecords()
	{
		QuestDialogOnProgress1DbfAsset questDialogOnProgress1DbfAsset = assetBundleRequest.asset as QuestDialogOnProgress1DbfAsset;
		if (questDialogOnProgress1DbfAsset != null)
		{
			for (int i = 0; i < questDialogOnProgress1DbfAsset.Records.Count; i++)
			{
				questDialogOnProgress1DbfAsset.Records[i].StripUnusedLocales();
			}
			return questDialogOnProgress1DbfAsset.Records;
		}
		return null;
	}

	public LoadQuestDialogOnProgress1DbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(QuestDialogOnProgress1DbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
