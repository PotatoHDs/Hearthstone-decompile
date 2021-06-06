using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001B8 RID: 440
public class LoadClassDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x060019F5 RID: 6645 RVA: 0x0008A4B4 File Offset: 0x000886B4
	public List<ClassDbfRecord> GetRecords()
	{
		ClassDbfAsset classDbfAsset = this.assetBundleRequest.asset as ClassDbfAsset;
		if (classDbfAsset != null)
		{
			for (int i = 0; i < classDbfAsset.Records.Count; i++)
			{
				classDbfAsset.Records[i].StripUnusedLocales();
			}
			return classDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x060019F6 RID: 6646 RVA: 0x0008A50A File Offset: 0x0008870A
	public LoadClassDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ClassDbfAsset));
	}

	// Token: 0x060019F7 RID: 6647 RVA: 0x0008A52D File Offset: 0x0008872D
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000FCA RID: 4042
	private AssetBundleRequest assetBundleRequest;
}
