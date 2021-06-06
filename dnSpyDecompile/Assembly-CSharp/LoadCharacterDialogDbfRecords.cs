using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001B2 RID: 434
public class LoadCharacterDialogDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x060019B4 RID: 6580 RVA: 0x00089624 File Offset: 0x00087824
	public List<CharacterDialogDbfRecord> GetRecords()
	{
		CharacterDialogDbfAsset characterDialogDbfAsset = this.assetBundleRequest.asset as CharacterDialogDbfAsset;
		if (characterDialogDbfAsset != null)
		{
			for (int i = 0; i < characterDialogDbfAsset.Records.Count; i++)
			{
				characterDialogDbfAsset.Records[i].StripUnusedLocales();
			}
			return characterDialogDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x060019B5 RID: 6581 RVA: 0x0008967A File Offset: 0x0008787A
	public LoadCharacterDialogDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CharacterDialogDbfAsset));
	}

	// Token: 0x060019B6 RID: 6582 RVA: 0x0008969D File Offset: 0x0008789D
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000FB3 RID: 4019
	private AssetBundleRequest assetBundleRequest;
}
