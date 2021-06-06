using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadProductClientDataDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<ProductClientDataDbfRecord> GetRecords()
	{
		ProductClientDataDbfAsset productClientDataDbfAsset = assetBundleRequest.asset as ProductClientDataDbfAsset;
		if (productClientDataDbfAsset != null)
		{
			for (int i = 0; i < productClientDataDbfAsset.Records.Count; i++)
			{
				productClientDataDbfAsset.Records[i].StripUnusedLocales();
			}
			return productClientDataDbfAsset.Records;
		}
		return null;
	}

	public LoadProductClientDataDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ProductClientDataDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
