using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000243 RID: 579
public class LoadQuestModifierDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001EBB RID: 7867 RVA: 0x0009B518 File Offset: 0x00099718
	public List<QuestModifierDbfRecord> GetRecords()
	{
		QuestModifierDbfAsset questModifierDbfAsset = this.assetBundleRequest.asset as QuestModifierDbfAsset;
		if (questModifierDbfAsset != null)
		{
			for (int i = 0; i < questModifierDbfAsset.Records.Count; i++)
			{
				questModifierDbfAsset.Records[i].StripUnusedLocales();
			}
			return questModifierDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001EBC RID: 7868 RVA: 0x0009B56E File Offset: 0x0009976E
	public LoadQuestModifierDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(QuestModifierDbfAsset));
	}

	// Token: 0x06001EBD RID: 7869 RVA: 0x0009B591 File Offset: 0x00099791
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040011B3 RID: 4531
	private AssetBundleRequest assetBundleRequest;
}
