using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadCharacterDialogDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<CharacterDialogDbfRecord> GetRecords()
	{
		CharacterDialogDbfAsset characterDialogDbfAsset = assetBundleRequest.asset as CharacterDialogDbfAsset;
		if (characterDialogDbfAsset != null)
		{
			for (int i = 0; i < characterDialogDbfAsset.Records.Count; i++)
			{
				characterDialogDbfAsset.Records[i].StripUnusedLocales();
			}
			return characterDialogDbfAsset.Records;
		}
		return null;
	}

	public LoadCharacterDialogDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CharacterDialogDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
