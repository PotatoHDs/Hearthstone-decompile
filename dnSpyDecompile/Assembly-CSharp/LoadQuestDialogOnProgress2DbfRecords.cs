using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200023D RID: 573
public class LoadQuestDialogOnProgress2DbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001E83 RID: 7811 RVA: 0x0009A840 File Offset: 0x00098A40
	public List<QuestDialogOnProgress2DbfRecord> GetRecords()
	{
		QuestDialogOnProgress2DbfAsset questDialogOnProgress2DbfAsset = this.assetBundleRequest.asset as QuestDialogOnProgress2DbfAsset;
		if (questDialogOnProgress2DbfAsset != null)
		{
			for (int i = 0; i < questDialogOnProgress2DbfAsset.Records.Count; i++)
			{
				questDialogOnProgress2DbfAsset.Records[i].StripUnusedLocales();
			}
			return questDialogOnProgress2DbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001E84 RID: 7812 RVA: 0x0009A896 File Offset: 0x00098A96
	public LoadQuestDialogOnProgress2DbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(QuestDialogOnProgress2DbfAsset));
	}

	// Token: 0x06001E85 RID: 7813 RVA: 0x0009A8B9 File Offset: 0x00098AB9
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x0400119F RID: 4511
	private AssetBundleRequest assetBundleRequest;
}
