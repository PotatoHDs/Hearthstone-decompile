using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001A6 RID: 422
public class LoadCardSetSpellOverrideDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x0600196B RID: 6507 RVA: 0x00088C48 File Offset: 0x00086E48
	public List<CardSetSpellOverrideDbfRecord> GetRecords()
	{
		CardSetSpellOverrideDbfAsset cardSetSpellOverrideDbfAsset = this.assetBundleRequest.asset as CardSetSpellOverrideDbfAsset;
		if (cardSetSpellOverrideDbfAsset != null)
		{
			for (int i = 0; i < cardSetSpellOverrideDbfAsset.Records.Count; i++)
			{
				cardSetSpellOverrideDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardSetSpellOverrideDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x0600196C RID: 6508 RVA: 0x00088C9E File Offset: 0x00086E9E
	public LoadCardSetSpellOverrideDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardSetSpellOverrideDbfAsset));
	}

	// Token: 0x0600196D RID: 6509 RVA: 0x00088CC1 File Offset: 0x00086EC1
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000F9F RID: 3999
	private AssetBundleRequest assetBundleRequest;
}
