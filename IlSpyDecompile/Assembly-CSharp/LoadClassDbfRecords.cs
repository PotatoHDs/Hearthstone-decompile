using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadClassDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<ClassDbfRecord> GetRecords()
	{
		ClassDbfAsset classDbfAsset = assetBundleRequest.asset as ClassDbfAsset;
		if (classDbfAsset != null)
		{
			for (int i = 0; i < classDbfAsset.Records.Count; i++)
			{
				classDbfAsset.Records[i].StripUnusedLocales();
			}
			return classDbfAsset.Records;
		}
		return null;
	}

	public LoadClassDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ClassDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
