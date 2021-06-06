using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadProductDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<ProductDbfRecord> GetRecords()
	{
		ProductDbfAsset productDbfAsset = assetBundleRequest.asset as ProductDbfAsset;
		if (productDbfAsset != null)
		{
			for (int i = 0; i < productDbfAsset.Records.Count; i++)
			{
				productDbfAsset.Records[i].StripUnusedLocales();
			}
			return productDbfAsset.Records;
		}
		return null;
	}

	public LoadProductDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ProductDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
