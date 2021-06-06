using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadCardAdditonalSearchTermsDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<CardAdditonalSearchTermsDbfRecord> GetRecords()
	{
		CardAdditonalSearchTermsDbfAsset cardAdditonalSearchTermsDbfAsset = assetBundleRequest.asset as CardAdditonalSearchTermsDbfAsset;
		if (cardAdditonalSearchTermsDbfAsset != null)
		{
			for (int i = 0; i < cardAdditonalSearchTermsDbfAsset.Records.Count; i++)
			{
				cardAdditonalSearchTermsDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardAdditonalSearchTermsDbfAsset.Records;
		}
		return null;
	}

	public LoadCardAdditonalSearchTermsDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardAdditonalSearchTermsDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
