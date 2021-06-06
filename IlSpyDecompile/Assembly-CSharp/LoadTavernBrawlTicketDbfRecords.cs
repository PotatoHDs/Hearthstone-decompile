using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadTavernBrawlTicketDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<TavernBrawlTicketDbfRecord> GetRecords()
	{
		TavernBrawlTicketDbfAsset tavernBrawlTicketDbfAsset = assetBundleRequest.asset as TavernBrawlTicketDbfAsset;
		if (tavernBrawlTicketDbfAsset != null)
		{
			for (int i = 0; i < tavernBrawlTicketDbfAsset.Records.Count; i++)
			{
				tavernBrawlTicketDbfAsset.Records[i].StripUnusedLocales();
			}
			return tavernBrawlTicketDbfAsset.Records;
		}
		return null;
	}

	public LoadTavernBrawlTicketDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(TavernBrawlTicketDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
