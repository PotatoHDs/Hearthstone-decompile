using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200023A RID: 570
public class LoadQuestDialogOnProgress1DbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001E67 RID: 7783 RVA: 0x0009A1D4 File Offset: 0x000983D4
	public List<QuestDialogOnProgress1DbfRecord> GetRecords()
	{
		QuestDialogOnProgress1DbfAsset questDialogOnProgress1DbfAsset = this.assetBundleRequest.asset as QuestDialogOnProgress1DbfAsset;
		if (questDialogOnProgress1DbfAsset != null)
		{
			for (int i = 0; i < questDialogOnProgress1DbfAsset.Records.Count; i++)
			{
				questDialogOnProgress1DbfAsset.Records[i].StripUnusedLocales();
			}
			return questDialogOnProgress1DbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001E68 RID: 7784 RVA: 0x0009A22A File Offset: 0x0009842A
	public LoadQuestDialogOnProgress1DbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(QuestDialogOnProgress1DbfAsset));
	}

	// Token: 0x06001E69 RID: 7785 RVA: 0x0009A24D File Offset: 0x0009844D
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001195 RID: 4501
	private AssetBundleRequest assetBundleRequest;
}
