using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001B5 RID: 437
public class LoadCharacterDialogItemsDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x060019CD RID: 6605 RVA: 0x000899EC File Offset: 0x00087BEC
	public List<CharacterDialogItemsDbfRecord> GetRecords()
	{
		CharacterDialogItemsDbfAsset characterDialogItemsDbfAsset = this.assetBundleRequest.asset as CharacterDialogItemsDbfAsset;
		if (characterDialogItemsDbfAsset != null)
		{
			for (int i = 0; i < characterDialogItemsDbfAsset.Records.Count; i++)
			{
				characterDialogItemsDbfAsset.Records[i].StripUnusedLocales();
			}
			return characterDialogItemsDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x060019CE RID: 6606 RVA: 0x00089A42 File Offset: 0x00087C42
	public LoadCharacterDialogItemsDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CharacterDialogItemsDbfAsset));
	}

	// Token: 0x060019CF RID: 6607 RVA: 0x00089A65 File Offset: 0x00087C65
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000FBA RID: 4026
	private AssetBundleRequest assetBundleRequest;
}
