using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadBoardDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<BoardDbfRecord> GetRecords()
	{
		BoardDbfAsset boardDbfAsset = assetBundleRequest.asset as BoardDbfAsset;
		if (boardDbfAsset != null)
		{
			for (int i = 0; i < boardDbfAsset.Records.Count; i++)
			{
				boardDbfAsset.Records[i].StripUnusedLocales();
			}
			return boardDbfAsset.Records;
		}
		return null;
	}

	public LoadBoardDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(BoardDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
