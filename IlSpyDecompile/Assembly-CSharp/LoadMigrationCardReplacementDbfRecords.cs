using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadMigrationCardReplacementDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<MigrationCardReplacementDbfRecord> GetRecords()
	{
		MigrationCardReplacementDbfAsset migrationCardReplacementDbfAsset = assetBundleRequest.asset as MigrationCardReplacementDbfAsset;
		if (migrationCardReplacementDbfAsset != null)
		{
			for (int i = 0; i < migrationCardReplacementDbfAsset.Records.Count; i++)
			{
				migrationCardReplacementDbfAsset.Records[i].StripUnusedLocales();
			}
			return migrationCardReplacementDbfAsset.Records;
		}
		return null;
	}

	public LoadMigrationCardReplacementDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(MigrationCardReplacementDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
