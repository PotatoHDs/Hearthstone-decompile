using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadClassExclusionsDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<ClassExclusionsDbfRecord> GetRecords()
	{
		ClassExclusionsDbfAsset classExclusionsDbfAsset = assetBundleRequest.asset as ClassExclusionsDbfAsset;
		if (classExclusionsDbfAsset != null)
		{
			for (int i = 0; i < classExclusionsDbfAsset.Records.Count; i++)
			{
				classExclusionsDbfAsset.Records[i].StripUnusedLocales();
			}
			return classExclusionsDbfAsset.Records;
		}
		return null;
	}

	public LoadClassExclusionsDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ClassExclusionsDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
