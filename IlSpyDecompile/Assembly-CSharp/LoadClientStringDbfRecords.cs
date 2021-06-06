using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadClientStringDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<ClientStringDbfRecord> GetRecords()
	{
		ClientStringDbfAsset clientStringDbfAsset = assetBundleRequest.asset as ClientStringDbfAsset;
		if (clientStringDbfAsset != null)
		{
			for (int i = 0; i < clientStringDbfAsset.Records.Count; i++)
			{
				clientStringDbfAsset.Records[i].StripUnusedLocales();
			}
			return clientStringDbfAsset.Records;
		}
		return null;
	}

	public LoadClientStringDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ClientStringDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
