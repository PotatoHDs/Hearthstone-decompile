using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000231 RID: 561
public class LoadQuestDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001E0F RID: 7695 RVA: 0x00099020 File Offset: 0x00097220
	public List<QuestDbfRecord> GetRecords()
	{
		QuestDbfAsset questDbfAsset = this.assetBundleRequest.asset as QuestDbfAsset;
		if (questDbfAsset != null)
		{
			for (int i = 0; i < questDbfAsset.Records.Count; i++)
			{
				questDbfAsset.Records[i].StripUnusedLocales();
			}
			return questDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001E10 RID: 7696 RVA: 0x00099076 File Offset: 0x00097276
	public LoadQuestDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(QuestDbfAsset));
	}

	// Token: 0x06001E11 RID: 7697 RVA: 0x00099099 File Offset: 0x00097299
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x0400117B RID: 4475
	private AssetBundleRequest assetBundleRequest;
}
