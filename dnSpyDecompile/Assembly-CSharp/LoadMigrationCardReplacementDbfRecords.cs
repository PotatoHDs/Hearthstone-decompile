using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000213 RID: 531
public class LoadMigrationCardReplacementDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001D03 RID: 7427 RVA: 0x0009587C File Offset: 0x00093A7C
	public List<MigrationCardReplacementDbfRecord> GetRecords()
	{
		MigrationCardReplacementDbfAsset migrationCardReplacementDbfAsset = this.assetBundleRequest.asset as MigrationCardReplacementDbfAsset;
		if (migrationCardReplacementDbfAsset != null)
		{
			for (int i = 0; i < migrationCardReplacementDbfAsset.Records.Count; i++)
			{
				migrationCardReplacementDbfAsset.Records[i].StripUnusedLocales();
			}
			return migrationCardReplacementDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001D04 RID: 7428 RVA: 0x000958D2 File Offset: 0x00093AD2
	public LoadMigrationCardReplacementDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(MigrationCardReplacementDbfAsset));
	}

	// Token: 0x06001D05 RID: 7429 RVA: 0x000958F5 File Offset: 0x00093AF5
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001126 RID: 4390
	private AssetBundleRequest assetBundleRequest;
}
