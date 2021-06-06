using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001BB RID: 443
public class LoadClassExclusionsDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001A03 RID: 6659 RVA: 0x0008A688 File Offset: 0x00088888
	public List<ClassExclusionsDbfRecord> GetRecords()
	{
		ClassExclusionsDbfAsset classExclusionsDbfAsset = this.assetBundleRequest.asset as ClassExclusionsDbfAsset;
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

	// Token: 0x06001A04 RID: 6660 RVA: 0x0008A6DE File Offset: 0x000888DE
	public LoadClassExclusionsDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ClassExclusionsDbfAsset));
	}

	// Token: 0x06001A05 RID: 6661 RVA: 0x0008A701 File Offset: 0x00088901
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000FCD RID: 4045
	private AssetBundleRequest assetBundleRequest;
}
