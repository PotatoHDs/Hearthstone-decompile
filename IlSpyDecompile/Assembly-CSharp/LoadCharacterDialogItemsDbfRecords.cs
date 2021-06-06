using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadCharacterDialogItemsDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<CharacterDialogItemsDbfRecord> GetRecords()
	{
		CharacterDialogItemsDbfAsset characterDialogItemsDbfAsset = assetBundleRequest.asset as CharacterDialogItemsDbfAsset;
		if (characterDialogItemsDbfAsset != null)
		{
			for (int i = 0; i < characterDialogItemsDbfAsset.Records.Count; i++)
			{
				characterDialogItemsDbfAsset.Records[i].StripUnusedLocales();
			}
			return characterDialogItemsDbfAsset.Records;
		}
		return null;
	}

	public LoadCharacterDialogItemsDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CharacterDialogItemsDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
