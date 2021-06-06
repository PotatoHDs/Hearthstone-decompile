using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000240 RID: 576
public class LoadQuestDialogOnReceivedDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001E9F RID: 7839 RVA: 0x0009AEAC File Offset: 0x000990AC
	public List<QuestDialogOnReceivedDbfRecord> GetRecords()
	{
		QuestDialogOnReceivedDbfAsset questDialogOnReceivedDbfAsset = this.assetBundleRequest.asset as QuestDialogOnReceivedDbfAsset;
		if (questDialogOnReceivedDbfAsset != null)
		{
			for (int i = 0; i < questDialogOnReceivedDbfAsset.Records.Count; i++)
			{
				questDialogOnReceivedDbfAsset.Records[i].StripUnusedLocales();
			}
			return questDialogOnReceivedDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001EA0 RID: 7840 RVA: 0x0009AF02 File Offset: 0x00099102
	public LoadQuestDialogOnReceivedDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(QuestDialogOnReceivedDbfAsset));
	}

	// Token: 0x06001EA1 RID: 7841 RVA: 0x0009AF25 File Offset: 0x00099125
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040011A9 RID: 4521
	private AssetBundleRequest assetBundleRequest;
}
