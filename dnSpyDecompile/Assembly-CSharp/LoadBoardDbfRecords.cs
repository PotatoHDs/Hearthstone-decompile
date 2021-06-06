using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000185 RID: 389
public class LoadBoardDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001820 RID: 6176 RVA: 0x00084218 File Offset: 0x00082418
	public List<BoardDbfRecord> GetRecords()
	{
		BoardDbfAsset boardDbfAsset = this.assetBundleRequest.asset as BoardDbfAsset;
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

	// Token: 0x06001821 RID: 6177 RVA: 0x0008426E File Offset: 0x0008246E
	public LoadBoardDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(BoardDbfAsset));
	}

	// Token: 0x06001822 RID: 6178 RVA: 0x00084291 File Offset: 0x00082491
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000F2E RID: 3886
	private AssetBundleRequest assetBundleRequest;
}
