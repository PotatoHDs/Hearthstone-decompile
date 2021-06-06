using System;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x0200083F RID: 2111
public class FontTable : IService
{
	// Token: 0x1700067B RID: 1659
	// (get) Token: 0x0600709B RID: 28827 RVA: 0x002450AA File Offset: 0x002432AA
	// (set) Token: 0x0600709C RID: 28828 RVA: 0x002450B2 File Offset: 0x002432B2
	private FontTableData Data { get; set; }

	// Token: 0x0600709D RID: 28829 RVA: 0x002450BB File Offset: 0x002432BB
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		LoadResource loadData = new LoadResource("ServiceData/FontTableData", LoadResourceFlags.FailOnError);
		yield return loadData;
		this.Data = (loadData.LoadedAsset as FontTableData);
		yield return new JobDefinition("FontTable.LoadFontDefs", this.Job_LoadFontDefs(), new IJobDependency[]
		{
			new WaitForGameDownloadManagerState()
		});
		HearthstoneApplication.Get().WillReset += this.WillReset;
		yield break;
	}

	// Token: 0x0600709E RID: 28830 RVA: 0x002450CA File Offset: 0x002432CA
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(IAssetLoader)
		};
	}

	// Token: 0x0600709F RID: 28831 RVA: 0x002450DF File Offset: 0x002432DF
	public void Shutdown()
	{
		HearthstoneApplication.Get().WillReset -= this.WillReset;
		this.m_defs.DisposeValuesAndClear<string, AssetHandle<FontDefinition>>();
	}

	// Token: 0x060070A0 RID: 28832 RVA: 0x00245102 File Offset: 0x00243302
	public static FontTable Get()
	{
		return HearthstoneServices.Get<FontTable>();
	}

	// Token: 0x060070A1 RID: 28833 RVA: 0x00245109 File Offset: 0x00243309
	public IEnumerator<IAsyncJobResult> Job_LoadFontDefs()
	{
		JobResultCollection loadFontDefJobs = new JobResultCollection(Array.Empty<IAsyncJobResult>());
		foreach (FontTableData.FontTableEntry fontTableEntry in this.Data.m_Entries)
		{
			loadFontDefJobs.Add(new LoadFontDef(string.Format("{0}:{1}", fontTableEntry.m_FontName, fontTableEntry.m_FontGuid)));
		}
		yield return loadFontDefJobs;
		int num;
		for (int i = 0; i < loadFontDefJobs.Results.Count; i = num)
		{
			LoadFontDef loadFontDefJob = loadFontDefJobs.Results[i] as LoadFontDef;
			string assetName = loadFontDefJob.AssetRef.GetLegacyAssetName();
			Log.Font.Print("OnFontDefLoaded {0}", new object[]
			{
				assetName
			});
			if (loadFontDefJob.loadedAsset == null)
			{
				Error.AddFatal(FatalErrorReason.ASSET_INCORRECT_DATA, "GLOBAL_ERROR_ASSET_INCORRECT_DATA", new object[]
				{
					assetName
				});
				string text = string.Format("FontTable.OnFontDefLoaded() - name={0} message={1}", assetName, GameStrings.Format("GLOBAL_ERROR_ASSET_INCORRECT_DATA", new object[]
				{
					assetName
				}));
				Debug.LogError(text);
				yield return new JobFailedResult(text, Array.Empty<object>());
			}
			this.m_defs.SetOrReplaceDisposable(assetName, loadFontDefJob.loadedAsset);
			loadFontDefJob = null;
			assetName = null;
			num = i + 1;
		}
		yield break;
	}

	// Token: 0x060070A2 RID: 28834 RVA: 0x00245118 File Offset: 0x00243318
	public FontDefinition GetFontDef(Font enUSFont)
	{
		string fontDefName = this.GetFontDefName(enUSFont);
		return this.GetFontDef(fontDefName);
	}

	// Token: 0x060070A3 RID: 28835 RVA: 0x00245134 File Offset: 0x00243334
	public FontDefinition GetFontDef(string name)
	{
		AssetHandle<FontDefinition> handle;
		this.m_defs.TryGetValue(name, out handle);
		return handle;
	}

	// Token: 0x060070A4 RID: 28836 RVA: 0x00245156 File Offset: 0x00243356
	public void Reset()
	{
		this.m_defs.DisposeValuesAndClear<string, AssetHandle<FontDefinition>>();
	}

	// Token: 0x060070A5 RID: 28837 RVA: 0x00245163 File Offset: 0x00243363
	private string GetFontDefName(Font font)
	{
		if (font == null)
		{
			return null;
		}
		return this.GetFontDefName(font.name);
	}

	// Token: 0x060070A6 RID: 28838 RVA: 0x0024517C File Offset: 0x0024337C
	private string GetFontDefName(string fontName)
	{
		foreach (FontTableData.FontTableEntry fontTableEntry in this.Data.m_Entries)
		{
			if (fontTableEntry.m_FontName == fontName)
			{
				return fontTableEntry.m_FontDefName;
			}
		}
		return null;
	}

	// Token: 0x060070A7 RID: 28839 RVA: 0x002451E8 File Offset: 0x002433E8
	private void WillReset()
	{
		this.Reset();
	}

	// Token: 0x04005A71 RID: 23153
	private Map<string, AssetHandle<FontDefinition>> m_defs = new Map<string, AssetHandle<FontDefinition>>();
}
