using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class BoosterDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private int m_latestExpansionOrder;

	[SerializeField]
	private int m_listDisplayOrder;

	[SerializeField]
	private int m_listDisplayOrderCategory;

	[SerializeField]
	private string m_openPackEvent = "none";

	[SerializeField]
	private string m_deprecatedOpenPackEvent = "never";

	[SerializeField]
	private string m_prereleaseOpenPackEvent = "never";

	[SerializeField]
	private string m_buyWithGoldEvent = "never";

	[SerializeField]
	private string m_rewardableEvent = "none";

	[SerializeField]
	private DbfLocValue m_name;

	[SerializeField]
	private string m_packOpeningPrefab;

	[SerializeField]
	private string m_packOpeningFxPrefab;

	[SerializeField]
	private string m_storePrefab;

	[SerializeField]
	private string m_arenaPrefab;

	[SerializeField]
	private bool m_leavingSoon;

	[SerializeField]
	private DbfLocValue m_leavingSoonText;

	[SerializeField]
	private string m_standardEvent = "always";

	[SerializeField]
	private bool m_showInStore;

	[SerializeField]
	private int m_rankedRewardInitialSeason;

	[SerializeField]
	private string m_questIconPath;

	[SerializeField]
	private double m_questIconOffsetX;

	[SerializeField]
	private double m_questIconOffsetY;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("LATEST_EXPANSION_ORDER")]
	public int LatestExpansionOrder => m_latestExpansionOrder;

	[DbfField("LIST_DISPLAY_ORDER")]
	public int ListDisplayOrder => m_listDisplayOrder;

	[DbfField("LIST_DISPLAY_ORDER_CATEGORY")]
	public int ListDisplayOrderCategory => m_listDisplayOrderCategory;

	[DbfField("OPEN_PACK_EVENT")]
	public string OpenPackEvent => m_openPackEvent;

	[Obsolete("renamed to PRERELEASE_OPEN_PACK_EVENT")]
	[DbfField("DEPRECATED_OPEN_PACK_EVENT")]
	public string DeprecatedOpenPackEvent => m_deprecatedOpenPackEvent;

	[DbfField("PRERELEASE_OPEN_PACK_EVENT")]
	public string PrereleaseOpenPackEvent => m_prereleaseOpenPackEvent;

	[DbfField("BUY_WITH_GOLD_EVENT")]
	public string BuyWithGoldEvent => m_buyWithGoldEvent;

	[DbfField("REWARDABLE_EVENT")]
	public string RewardableEvent => m_rewardableEvent;

	[DbfField("NAME")]
	public DbfLocValue Name => m_name;

	[DbfField("PACK_OPENING_PREFAB")]
	public string PackOpeningPrefab => m_packOpeningPrefab;

	[DbfField("PACK_OPENING_FX_PREFAB")]
	public string PackOpeningFxPrefab => m_packOpeningFxPrefab;

	[DbfField("STORE_PREFAB")]
	public string StorePrefab => m_storePrefab;

	[DbfField("ARENA_PREFAB")]
	public string ArenaPrefab => m_arenaPrefab;

	[DbfField("LEAVING_SOON")]
	public bool LeavingSoon => m_leavingSoon;

	[DbfField("LEAVING_SOON_TEXT")]
	public DbfLocValue LeavingSoonText => m_leavingSoonText;

	[DbfField("STANDARD_EVENT")]
	public string StandardEvent => m_standardEvent;

	[Obsolete("DEPRECATED. Hasn't been used in a long time.")]
	[DbfField("SHOW_IN_STORE")]
	public bool ShowInStore => m_showInStore;

	[DbfField("RANKED_REWARD_INITIAL_SEASON")]
	public int RankedRewardInitialSeason => m_rankedRewardInitialSeason;

	[DbfField("QUEST_ICON_PATH")]
	public string QuestIconPath => m_questIconPath;

	[DbfField("QUEST_ICON_OFFSET_X")]
	public double QuestIconOffsetX => m_questIconOffsetX;

	[DbfField("QUEST_ICON_OFFSET_Y")]
	public double QuestIconOffsetY => m_questIconOffsetY;

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetLatestExpansionOrder(int v)
	{
		m_latestExpansionOrder = v;
	}

	public void SetListDisplayOrder(int v)
	{
		m_listDisplayOrder = v;
	}

	public void SetListDisplayOrderCategory(int v)
	{
		m_listDisplayOrderCategory = v;
	}

	public void SetOpenPackEvent(string v)
	{
		m_openPackEvent = v;
	}

	[Obsolete("renamed to PRERELEASE_OPEN_PACK_EVENT")]
	public void SetDeprecatedOpenPackEvent(string v)
	{
		m_deprecatedOpenPackEvent = v;
	}

	public void SetPrereleaseOpenPackEvent(string v)
	{
		m_prereleaseOpenPackEvent = v;
	}

	public void SetBuyWithGoldEvent(string v)
	{
		m_buyWithGoldEvent = v;
	}

	public void SetRewardableEvent(string v)
	{
		m_rewardableEvent = v;
	}

	public void SetName(DbfLocValue v)
	{
		m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	public void SetPackOpeningPrefab(string v)
	{
		m_packOpeningPrefab = v;
	}

	public void SetPackOpeningFxPrefab(string v)
	{
		m_packOpeningFxPrefab = v;
	}

	public void SetStorePrefab(string v)
	{
		m_storePrefab = v;
	}

	public void SetArenaPrefab(string v)
	{
		m_arenaPrefab = v;
	}

	public void SetLeavingSoon(bool v)
	{
		m_leavingSoon = v;
	}

	public void SetLeavingSoonText(DbfLocValue v)
	{
		m_leavingSoonText = v;
		v.SetDebugInfo(base.ID, "LEAVING_SOON_TEXT");
	}

	public void SetStandardEvent(string v)
	{
		m_standardEvent = v;
	}

	[Obsolete("DEPRECATED. Hasn't been used in a long time.")]
	public void SetShowInStore(bool v)
	{
		m_showInStore = v;
	}

	public void SetRankedRewardInitialSeason(int v)
	{
		m_rankedRewardInitialSeason = v;
	}

	public void SetQuestIconPath(string v)
	{
		m_questIconPath = v;
	}

	public void SetQuestIconOffsetX(double v)
	{
		m_questIconOffsetX = v;
	}

	public void SetQuestIconOffsetY(double v)
	{
		m_questIconOffsetY = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_DESC" => m_noteDesc, 
			"LATEST_EXPANSION_ORDER" => m_latestExpansionOrder, 
			"LIST_DISPLAY_ORDER" => m_listDisplayOrder, 
			"LIST_DISPLAY_ORDER_CATEGORY" => m_listDisplayOrderCategory, 
			"OPEN_PACK_EVENT" => m_openPackEvent, 
			"DEPRECATED_OPEN_PACK_EVENT" => m_deprecatedOpenPackEvent, 
			"PRERELEASE_OPEN_PACK_EVENT" => m_prereleaseOpenPackEvent, 
			"BUY_WITH_GOLD_EVENT" => m_buyWithGoldEvent, 
			"REWARDABLE_EVENT" => m_rewardableEvent, 
			"NAME" => m_name, 
			"PACK_OPENING_PREFAB" => m_packOpeningPrefab, 
			"PACK_OPENING_FX_PREFAB" => m_packOpeningFxPrefab, 
			"STORE_PREFAB" => m_storePrefab, 
			"ARENA_PREFAB" => m_arenaPrefab, 
			"LEAVING_SOON" => m_leavingSoon, 
			"LEAVING_SOON_TEXT" => m_leavingSoonText, 
			"STANDARD_EVENT" => m_standardEvent, 
			"SHOW_IN_STORE" => m_showInStore, 
			"RANKED_REWARD_INITIAL_SEASON" => m_rankedRewardInitialSeason, 
			"QUEST_ICON_PATH" => m_questIconPath, 
			"QUEST_ICON_OFFSET_X" => m_questIconOffsetX, 
			"QUEST_ICON_OFFSET_Y" => m_questIconOffsetY, 
			_ => null, 
		};
	}

	public override void SetVar(string name, object val)
	{
		switch (name)
		{
		case "ID":
			SetID((int)val);
			break;
		case "NOTE_DESC":
			m_noteDesc = (string)val;
			break;
		case "LATEST_EXPANSION_ORDER":
			m_latestExpansionOrder = (int)val;
			break;
		case "LIST_DISPLAY_ORDER":
			m_listDisplayOrder = (int)val;
			break;
		case "LIST_DISPLAY_ORDER_CATEGORY":
			m_listDisplayOrderCategory = (int)val;
			break;
		case "OPEN_PACK_EVENT":
			m_openPackEvent = (string)val;
			break;
		case "DEPRECATED_OPEN_PACK_EVENT":
			m_deprecatedOpenPackEvent = (string)val;
			break;
		case "PRERELEASE_OPEN_PACK_EVENT":
			m_prereleaseOpenPackEvent = (string)val;
			break;
		case "BUY_WITH_GOLD_EVENT":
			m_buyWithGoldEvent = (string)val;
			break;
		case "REWARDABLE_EVENT":
			m_rewardableEvent = (string)val;
			break;
		case "NAME":
			m_name = (DbfLocValue)val;
			break;
		case "PACK_OPENING_PREFAB":
			m_packOpeningPrefab = (string)val;
			break;
		case "PACK_OPENING_FX_PREFAB":
			m_packOpeningFxPrefab = (string)val;
			break;
		case "STORE_PREFAB":
			m_storePrefab = (string)val;
			break;
		case "ARENA_PREFAB":
			m_arenaPrefab = (string)val;
			break;
		case "LEAVING_SOON":
			m_leavingSoon = (bool)val;
			break;
		case "LEAVING_SOON_TEXT":
			m_leavingSoonText = (DbfLocValue)val;
			break;
		case "STANDARD_EVENT":
			m_standardEvent = (string)val;
			break;
		case "SHOW_IN_STORE":
			m_showInStore = (bool)val;
			break;
		case "RANKED_REWARD_INITIAL_SEASON":
			m_rankedRewardInitialSeason = (int)val;
			break;
		case "QUEST_ICON_PATH":
			m_questIconPath = (string)val;
			break;
		case "QUEST_ICON_OFFSET_X":
			m_questIconOffsetX = (double)val;
			break;
		case "QUEST_ICON_OFFSET_Y":
			m_questIconOffsetY = (double)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"LATEST_EXPANSION_ORDER" => typeof(int), 
			"LIST_DISPLAY_ORDER" => typeof(int), 
			"LIST_DISPLAY_ORDER_CATEGORY" => typeof(int), 
			"OPEN_PACK_EVENT" => typeof(string), 
			"DEPRECATED_OPEN_PACK_EVENT" => typeof(string), 
			"PRERELEASE_OPEN_PACK_EVENT" => typeof(string), 
			"BUY_WITH_GOLD_EVENT" => typeof(string), 
			"REWARDABLE_EVENT" => typeof(string), 
			"NAME" => typeof(DbfLocValue), 
			"PACK_OPENING_PREFAB" => typeof(string), 
			"PACK_OPENING_FX_PREFAB" => typeof(string), 
			"STORE_PREFAB" => typeof(string), 
			"ARENA_PREFAB" => typeof(string), 
			"LEAVING_SOON" => typeof(bool), 
			"LEAVING_SOON_TEXT" => typeof(DbfLocValue), 
			"STANDARD_EVENT" => typeof(string), 
			"SHOW_IN_STORE" => typeof(bool), 
			"RANKED_REWARD_INITIAL_SEASON" => typeof(int), 
			"QUEST_ICON_PATH" => typeof(string), 
			"QUEST_ICON_OFFSET_X" => typeof(double), 
			"QUEST_ICON_OFFSET_Y" => typeof(double), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadBoosterDbfRecords loadRecords = new LoadBoosterDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		BoosterDbfAsset boosterDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(BoosterDbfAsset)) as BoosterDbfAsset;
		if (boosterDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"BoosterDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < boosterDbfAsset.Records.Count; i++)
		{
			boosterDbfAsset.Records[i].StripUnusedLocales();
		}
		records = boosterDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_name.StripUnusedLocales();
		m_leavingSoonText.StripUnusedLocales();
	}
}
