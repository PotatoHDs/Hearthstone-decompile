using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000246 RID: 582
public class LoadQuestPoolDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001ED1 RID: 7889 RVA: 0x0009B8B0 File Offset: 0x00099AB0
	public List<QuestPoolDbfRecord> GetRecords()
	{
		QuestPoolDbfAsset questPoolDbfAsset = this.assetBundleRequest.asset as QuestPoolDbfAsset;
		if (questPoolDbfAsset != null)
		{
			for (int i = 0; i < questPoolDbfAsset.Records.Count; i++)
			{
				questPoolDbfAsset.Records[i].StripUnusedLocales();
			}
			return questPoolDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001ED2 RID: 7890 RVA: 0x0009B906 File Offset: 0x00099B06
	public LoadQuestPoolDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(QuestPoolDbfAsset));
	}

	// Token: 0x06001ED3 RID: 7891 RVA: 0x0009B929 File Offset: 0x00099B29
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040011BA RID: 4538
	private AssetBundleRequest assetBundleRequest;
}
