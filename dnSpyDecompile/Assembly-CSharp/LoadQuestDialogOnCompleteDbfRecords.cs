using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000237 RID: 567
public class LoadQuestDialogOnCompleteDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001E4B RID: 7755 RVA: 0x00099B68 File Offset: 0x00097D68
	public List<QuestDialogOnCompleteDbfRecord> GetRecords()
	{
		QuestDialogOnCompleteDbfAsset questDialogOnCompleteDbfAsset = this.assetBundleRequest.asset as QuestDialogOnCompleteDbfAsset;
		if (questDialogOnCompleteDbfAsset != null)
		{
			for (int i = 0; i < questDialogOnCompleteDbfAsset.Records.Count; i++)
			{
				questDialogOnCompleteDbfAsset.Records[i].StripUnusedLocales();
			}
			return questDialogOnCompleteDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001E4C RID: 7756 RVA: 0x00099BBE File Offset: 0x00097DBE
	public LoadQuestDialogOnCompleteDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(QuestDialogOnCompleteDbfAsset));
	}

	// Token: 0x06001E4D RID: 7757 RVA: 0x00099BE1 File Offset: 0x00097DE1
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x0400118B RID: 4491
	private AssetBundleRequest assetBundleRequest;
}
