using System;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

public class FontTable : IService
{
	private Map<string, AssetHandle<FontDefinition>> m_defs = new Map<string, AssetHandle<FontDefinition>>();

	private FontTableData Data { get; set; }

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		LoadResource loadData = new LoadResource("ServiceData/FontTableData", LoadResourceFlags.FailOnError);
		yield return loadData;
		Data = loadData.LoadedAsset as FontTableData;
		yield return new JobDefinition("FontTable.LoadFontDefs", Job_LoadFontDefs(), new WaitForGameDownloadManagerState());
		HearthstoneApplication.Get().WillReset += WillReset;
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(IAssetLoader) };
	}

	public void Shutdown()
	{
		HearthstoneApplication.Get().WillReset -= WillReset;
		m_defs.DisposeValuesAndClear();
	}

	public static FontTable Get()
	{
		return HearthstoneServices.Get<FontTable>();
	}

	public IEnumerator<IAsyncJobResult> Job_LoadFontDefs()
	{
		JobResultCollection loadFontDefJobs = new JobResultCollection();
		foreach (FontTableData.FontTableEntry entry in Data.m_Entries)
		{
			loadFontDefJobs.Add(new LoadFontDef($"{entry.m_FontName}:{entry.m_FontGuid}"));
		}
		yield return loadFontDefJobs;
		int i = 0;
		while (i < loadFontDefJobs.Results.Count)
		{
			LoadFontDef loadFontDefJob = loadFontDefJobs.Results[i] as LoadFontDef;
			string assetName = loadFontDefJob.AssetRef.GetLegacyAssetName();
			Log.Font.Print("OnFontDefLoaded {0}", assetName);
			if (loadFontDefJob.loadedAsset == null)
			{
				Error.AddFatal(FatalErrorReason.ASSET_INCORRECT_DATA, "GLOBAL_ERROR_ASSET_INCORRECT_DATA", assetName);
				string text = string.Format("FontTable.OnFontDefLoaded() - name={0} message={1}", assetName, GameStrings.Format("GLOBAL_ERROR_ASSET_INCORRECT_DATA", assetName));
				Debug.LogError(text);
				yield return new JobFailedResult(text);
			}
			m_defs.SetOrReplaceDisposable(assetName, loadFontDefJob.loadedAsset);
			int num = i + 1;
			i = num;
		}
	}

	public FontDefinition GetFontDef(Font enUSFont)
	{
		string fontDefName = GetFontDefName(enUSFont);
		return GetFontDef(fontDefName);
	}

	public FontDefinition GetFontDef(string name)
	{
		m_defs.TryGetValue(name, out var value);
		return value;
	}

	public void Reset()
	{
		m_defs.DisposeValuesAndClear();
	}

	private string GetFontDefName(Font font)
	{
		if (font == null)
		{
			return null;
		}
		return GetFontDefName(font.name);
	}

	private string GetFontDefName(string fontName)
	{
		foreach (FontTableData.FontTableEntry entry in Data.m_Entries)
		{
			if (entry.m_FontName == fontName)
			{
				return entry.m_FontDefName;
			}
		}
		return null;
	}

	private void WillReset()
	{
		Reset();
	}
}
