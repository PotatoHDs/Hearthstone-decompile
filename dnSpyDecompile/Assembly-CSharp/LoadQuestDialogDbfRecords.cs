using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000234 RID: 564
public class LoadQuestDialogDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001E33 RID: 7731 RVA: 0x00099864 File Offset: 0x00097A64
	public List<QuestDialogDbfRecord> GetRecords()
	{
		QuestDialogDbfAsset questDialogDbfAsset = this.assetBundleRequest.asset as QuestDialogDbfAsset;
		if (questDialogDbfAsset != null)
		{
			for (int i = 0; i < questDialogDbfAsset.Records.Count; i++)
			{
				questDialogDbfAsset.Records[i].StripUnusedLocales();
			}
			return questDialogDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001E34 RID: 7732 RVA: 0x000998BA File Offset: 0x00097ABA
	public LoadQuestDialogDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(QuestDialogDbfAsset));
	}

	// Token: 0x06001E35 RID: 7733 RVA: 0x000998DD File Offset: 0x00097ADD
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001187 RID: 4487
	private AssetBundleRequest assetBundleRequest;
}
