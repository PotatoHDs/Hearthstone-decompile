using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001BE RID: 446
public class LoadClientStringDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001A14 RID: 6676 RVA: 0x0008A900 File Offset: 0x00088B00
	public List<ClientStringDbfRecord> GetRecords()
	{
		ClientStringDbfAsset clientStringDbfAsset = this.assetBundleRequest.asset as ClientStringDbfAsset;
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

	// Token: 0x06001A15 RID: 6677 RVA: 0x0008A956 File Offset: 0x00088B56
	public LoadClientStringDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ClientStringDbfAsset));
	}

	// Token: 0x06001A16 RID: 6678 RVA: 0x0008A979 File Offset: 0x00088B79
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000FD1 RID: 4049
	private AssetBundleRequest assetBundleRequest;
}
