using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class AdventureDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private DbfLocValue m_name;

	[SerializeField]
	private int m_sortOrder;

	[SerializeField]
	private DbfLocValue m_storeBuyButtonLabel;

	[SerializeField]
	private DbfLocValue m_storeBuyWings1Headline;

	[SerializeField]
	private DbfLocValue m_storeBuyWings2Headline;

	[SerializeField]
	private DbfLocValue m_storeBuyWings3Headline;

	[SerializeField]
	private DbfLocValue m_storeBuyWings4Headline;

	[SerializeField]
	private DbfLocValue m_storeBuyWings5Headline;

	[SerializeField]
	private DbfLocValue m_storeOwnedHeadline;

	[SerializeField]
	private DbfLocValue m_storePreorderHeadline;

	[SerializeField]
	private DbfLocValue m_storeBuyWings1Desc;

	[SerializeField]
	private DbfLocValue m_storeBuyWings2Desc;

	[SerializeField]
	private DbfLocValue m_storeBuyWings3Desc;

	[SerializeField]
	private DbfLocValue m_storeBuyWings4Desc;

	[SerializeField]
	private DbfLocValue m_storeBuyWings5Desc;

	[SerializeField]
	private DbfLocValue m_storeBuyRemainingWingsDescTimelockedTrue;

	[SerializeField]
	private DbfLocValue m_storeBuyRemainingWingsDescTimelockedFalse;

	[SerializeField]
	private DbfLocValue m_storeOwnedDesc;

	[SerializeField]
	private DbfLocValue m_storePreorderWings1Desc;

	[SerializeField]
	private DbfLocValue m_storePreorderWings2Desc;

	[SerializeField]
	private DbfLocValue m_storePreorderWings3Desc;

	[SerializeField]
	private DbfLocValue m_storePreorderWings4Desc;

	[SerializeField]
	private DbfLocValue m_storePreorderWings5Desc;

	[SerializeField]
	private DbfLocValue m_storePreorderRadioText;

	[SerializeField]
	private DbfLocValue m_storePreviewRewardsText;

	[SerializeField]
	private string m_adventureDefPrefab;

	[SerializeField]
	private string m_storePrefab;

	[SerializeField]
	private bool m_leavingSoon;

	[SerializeField]
	private DbfLocValue m_leavingSoonText;

	[SerializeField]
	private string m_gameModeIcon;

	[SerializeField]
	private string m_productStringKey;

	[SerializeField]
	private string m_standardEvent = "always";

	[SerializeField]
	private string m_comingSoonEvent = "never";

	[SerializeField]
	private DbfLocValue m_comingSoonText;

	[SerializeField]
	private bool m_mapPageHasButtonsToChapters;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("NAME")]
	public DbfLocValue Name => m_name;

	[DbfField("SORT_ORDER")]
	public int SortOrder => m_sortOrder;

	[DbfField("STORE_BUY_BUTTON_LABEL")]
	public DbfLocValue StoreBuyButtonLabel => m_storeBuyButtonLabel;

	[DbfField("STORE_BUY_WINGS_1_HEADLINE")]
	public DbfLocValue StoreBuyWings1Headline => m_storeBuyWings1Headline;

	[DbfField("STORE_BUY_WINGS_2_HEADLINE")]
	public DbfLocValue StoreBuyWings2Headline => m_storeBuyWings2Headline;

	[DbfField("STORE_BUY_WINGS_3_HEADLINE")]
	public DbfLocValue StoreBuyWings3Headline => m_storeBuyWings3Headline;

	[DbfField("STORE_BUY_WINGS_4_HEADLINE")]
	public DbfLocValue StoreBuyWings4Headline => m_storeBuyWings4Headline;

	[DbfField("STORE_BUY_WINGS_5_HEADLINE")]
	public DbfLocValue StoreBuyWings5Headline => m_storeBuyWings5Headline;

	[DbfField("STORE_OWNED_HEADLINE")]
	public DbfLocValue StoreOwnedHeadline => m_storeOwnedHeadline;

	[DbfField("STORE_PREORDER_HEADLINE")]
	public DbfLocValue StorePreorderHeadline => m_storePreorderHeadline;

	[DbfField("STORE_BUY_WINGS_1_DESC")]
	public DbfLocValue StoreBuyWings1Desc => m_storeBuyWings1Desc;

	[DbfField("STORE_BUY_WINGS_2_DESC")]
	public DbfLocValue StoreBuyWings2Desc => m_storeBuyWings2Desc;

	[DbfField("STORE_BUY_WINGS_3_DESC")]
	public DbfLocValue StoreBuyWings3Desc => m_storeBuyWings3Desc;

	[DbfField("STORE_BUY_WINGS_4_DESC")]
	public DbfLocValue StoreBuyWings4Desc => m_storeBuyWings4Desc;

	[DbfField("STORE_BUY_WINGS_5_DESC")]
	public DbfLocValue StoreBuyWings5Desc => m_storeBuyWings5Desc;

	[DbfField("STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_TRUE")]
	public DbfLocValue StoreBuyRemainingWingsDescTimelockedTrue => m_storeBuyRemainingWingsDescTimelockedTrue;

	[DbfField("STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_FALSE")]
	public DbfLocValue StoreBuyRemainingWingsDescTimelockedFalse => m_storeBuyRemainingWingsDescTimelockedFalse;

	[DbfField("STORE_OWNED_DESC")]
	public DbfLocValue StoreOwnedDesc => m_storeOwnedDesc;

	[DbfField("STORE_PREORDER_WINGS_1_DESC")]
	public DbfLocValue StorePreorderWings1Desc => m_storePreorderWings1Desc;

	[DbfField("STORE_PREORDER_WINGS_2_DESC")]
	public DbfLocValue StorePreorderWings2Desc => m_storePreorderWings2Desc;

	[DbfField("STORE_PREORDER_WINGS_3_DESC")]
	public DbfLocValue StorePreorderWings3Desc => m_storePreorderWings3Desc;

	[DbfField("STORE_PREORDER_WINGS_4_DESC")]
	public DbfLocValue StorePreorderWings4Desc => m_storePreorderWings4Desc;

	[DbfField("STORE_PREORDER_WINGS_5_DESC")]
	public DbfLocValue StorePreorderWings5Desc => m_storePreorderWings5Desc;

	[DbfField("STORE_PREORDER_RADIO_TEXT")]
	public DbfLocValue StorePreorderRadioText => m_storePreorderRadioText;

	[DbfField("STORE_PREVIEW_REWARDS_TEXT")]
	public DbfLocValue StorePreviewRewardsText => m_storePreviewRewardsText;

	[DbfField("ADVENTURE_DEF_PREFAB")]
	public string AdventureDefPrefab => m_adventureDefPrefab;

	[DbfField("STORE_PREFAB")]
	public string StorePrefab => m_storePrefab;

	[DbfField("LEAVING_SOON")]
	public bool LeavingSoon => m_leavingSoon;

	[DbfField("LEAVING_SOON_TEXT")]
	public DbfLocValue LeavingSoonText => m_leavingSoonText;

	[DbfField("GAME_MODE_ICON")]
	public string GameModeIcon => m_gameModeIcon;

	[DbfField("PRODUCT_STRING_KEY")]
	public string ProductStringKey => m_productStringKey;

	[DbfField("STANDARD_EVENT")]
	public string StandardEvent => m_standardEvent;

	[DbfField("COMING_SOON_EVENT")]
	public string ComingSoonEvent => m_comingSoonEvent;

	[DbfField("COMING_SOON_TEXT")]
	public DbfLocValue ComingSoonText => m_comingSoonText;

	[DbfField("MAP_PAGE_HAS_BUTTONS_TO_CHAPTERS")]
	public bool MapPageHasButtonsToChapters => m_mapPageHasButtonsToChapters;

	public List<AdventureDataDbfRecord> AdventureData => GameDbf.AdventureData.GetRecords((AdventureDataDbfRecord r) => r.AdventureId == base.ID);

	public List<AdventureDeckDbfRecord> AdventureDecks => GameDbf.AdventureDeck.GetRecords((AdventureDeckDbfRecord r) => r.AdventureId == base.ID);

	public List<AdventureGuestHeroesDbfRecord> GuestHeroes => GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.AdventureId == base.ID);

	public List<AdventureHeroPowerDbfRecord> AdventureHeroPowers => GameDbf.AdventureHeroPower.GetRecords((AdventureHeroPowerDbfRecord r) => r.AdventureId == base.ID);

	public List<AdventureLoadoutTreasuresDbfRecord> AdventureLoadoutTreasures => GameDbf.AdventureLoadoutTreasures.GetRecords((AdventureLoadoutTreasuresDbfRecord r) => r.AdventureId == base.ID);

	public List<WingDbfRecord> Wings => GameDbf.Wing.GetRecords((WingDbfRecord r) => r.AdventureId == base.ID);

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetName(DbfLocValue v)
	{
		m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	public void SetSortOrder(int v)
	{
		m_sortOrder = v;
	}

	public void SetStoreBuyButtonLabel(DbfLocValue v)
	{
		m_storeBuyButtonLabel = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_BUTTON_LABEL");
	}

	public void SetStoreBuyWings1Headline(DbfLocValue v)
	{
		m_storeBuyWings1Headline = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_1_HEADLINE");
	}

	public void SetStoreBuyWings2Headline(DbfLocValue v)
	{
		m_storeBuyWings2Headline = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_2_HEADLINE");
	}

	public void SetStoreBuyWings3Headline(DbfLocValue v)
	{
		m_storeBuyWings3Headline = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_3_HEADLINE");
	}

	public void SetStoreBuyWings4Headline(DbfLocValue v)
	{
		m_storeBuyWings4Headline = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_4_HEADLINE");
	}

	public void SetStoreBuyWings5Headline(DbfLocValue v)
	{
		m_storeBuyWings5Headline = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_5_HEADLINE");
	}

	public void SetStoreOwnedHeadline(DbfLocValue v)
	{
		m_storeOwnedHeadline = v;
		v.SetDebugInfo(base.ID, "STORE_OWNED_HEADLINE");
	}

	public void SetStorePreorderHeadline(DbfLocValue v)
	{
		m_storePreorderHeadline = v;
		v.SetDebugInfo(base.ID, "STORE_PREORDER_HEADLINE");
	}

	public void SetStoreBuyWings1Desc(DbfLocValue v)
	{
		m_storeBuyWings1Desc = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_1_DESC");
	}

	public void SetStoreBuyWings2Desc(DbfLocValue v)
	{
		m_storeBuyWings2Desc = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_2_DESC");
	}

	public void SetStoreBuyWings3Desc(DbfLocValue v)
	{
		m_storeBuyWings3Desc = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_3_DESC");
	}

	public void SetStoreBuyWings4Desc(DbfLocValue v)
	{
		m_storeBuyWings4Desc = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_4_DESC");
	}

	public void SetStoreBuyWings5Desc(DbfLocValue v)
	{
		m_storeBuyWings5Desc = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WINGS_5_DESC");
	}

	public void SetStoreBuyRemainingWingsDescTimelockedTrue(DbfLocValue v)
	{
		m_storeBuyRemainingWingsDescTimelockedTrue = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_TRUE");
	}

	public void SetStoreBuyRemainingWingsDescTimelockedFalse(DbfLocValue v)
	{
		m_storeBuyRemainingWingsDescTimelockedFalse = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_FALSE");
	}

	public void SetStoreOwnedDesc(DbfLocValue v)
	{
		m_storeOwnedDesc = v;
		v.SetDebugInfo(base.ID, "STORE_OWNED_DESC");
	}

	public void SetStorePreorderWings1Desc(DbfLocValue v)
	{
		m_storePreorderWings1Desc = v;
		v.SetDebugInfo(base.ID, "STORE_PREORDER_WINGS_1_DESC");
	}

	public void SetStorePreorderWings2Desc(DbfLocValue v)
	{
		m_storePreorderWings2Desc = v;
		v.SetDebugInfo(base.ID, "STORE_PREORDER_WINGS_2_DESC");
	}

	public void SetStorePreorderWings3Desc(DbfLocValue v)
	{
		m_storePreorderWings3Desc = v;
		v.SetDebugInfo(base.ID, "STORE_PREORDER_WINGS_3_DESC");
	}

	public void SetStorePreorderWings4Desc(DbfLocValue v)
	{
		m_storePreorderWings4Desc = v;
		v.SetDebugInfo(base.ID, "STORE_PREORDER_WINGS_4_DESC");
	}

	public void SetStorePreorderWings5Desc(DbfLocValue v)
	{
		m_storePreorderWings5Desc = v;
		v.SetDebugInfo(base.ID, "STORE_PREORDER_WINGS_5_DESC");
	}

	public void SetStorePreorderRadioText(DbfLocValue v)
	{
		m_storePreorderRadioText = v;
		v.SetDebugInfo(base.ID, "STORE_PREORDER_RADIO_TEXT");
	}

	public void SetStorePreviewRewardsText(DbfLocValue v)
	{
		m_storePreviewRewardsText = v;
		v.SetDebugInfo(base.ID, "STORE_PREVIEW_REWARDS_TEXT");
	}

	public void SetAdventureDefPrefab(string v)
	{
		m_adventureDefPrefab = v;
	}

	public void SetStorePrefab(string v)
	{
		m_storePrefab = v;
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

	public void SetGameModeIcon(string v)
	{
		m_gameModeIcon = v;
	}

	public void SetProductStringKey(string v)
	{
		m_productStringKey = v;
	}

	public void SetStandardEvent(string v)
	{
		m_standardEvent = v;
	}

	public void SetComingSoonEvent(string v)
	{
		m_comingSoonEvent = v;
	}

	public void SetComingSoonText(DbfLocValue v)
	{
		m_comingSoonText = v;
		v.SetDebugInfo(base.ID, "COMING_SOON_TEXT");
	}

	public void SetMapPageHasButtonsToChapters(bool v)
	{
		m_mapPageHasButtonsToChapters = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_DESC" => m_noteDesc, 
			"NAME" => m_name, 
			"SORT_ORDER" => m_sortOrder, 
			"STORE_BUY_BUTTON_LABEL" => m_storeBuyButtonLabel, 
			"STORE_BUY_WINGS_1_HEADLINE" => m_storeBuyWings1Headline, 
			"STORE_BUY_WINGS_2_HEADLINE" => m_storeBuyWings2Headline, 
			"STORE_BUY_WINGS_3_HEADLINE" => m_storeBuyWings3Headline, 
			"STORE_BUY_WINGS_4_HEADLINE" => m_storeBuyWings4Headline, 
			"STORE_BUY_WINGS_5_HEADLINE" => m_storeBuyWings5Headline, 
			"STORE_OWNED_HEADLINE" => m_storeOwnedHeadline, 
			"STORE_PREORDER_HEADLINE" => m_storePreorderHeadline, 
			"STORE_BUY_WINGS_1_DESC" => m_storeBuyWings1Desc, 
			"STORE_BUY_WINGS_2_DESC" => m_storeBuyWings2Desc, 
			"STORE_BUY_WINGS_3_DESC" => m_storeBuyWings3Desc, 
			"STORE_BUY_WINGS_4_DESC" => m_storeBuyWings4Desc, 
			"STORE_BUY_WINGS_5_DESC" => m_storeBuyWings5Desc, 
			"STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_TRUE" => m_storeBuyRemainingWingsDescTimelockedTrue, 
			"STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_FALSE" => m_storeBuyRemainingWingsDescTimelockedFalse, 
			"STORE_OWNED_DESC" => m_storeOwnedDesc, 
			"STORE_PREORDER_WINGS_1_DESC" => m_storePreorderWings1Desc, 
			"STORE_PREORDER_WINGS_2_DESC" => m_storePreorderWings2Desc, 
			"STORE_PREORDER_WINGS_3_DESC" => m_storePreorderWings3Desc, 
			"STORE_PREORDER_WINGS_4_DESC" => m_storePreorderWings4Desc, 
			"STORE_PREORDER_WINGS_5_DESC" => m_storePreorderWings5Desc, 
			"STORE_PREORDER_RADIO_TEXT" => m_storePreorderRadioText, 
			"STORE_PREVIEW_REWARDS_TEXT" => m_storePreviewRewardsText, 
			"ADVENTURE_DEF_PREFAB" => m_adventureDefPrefab, 
			"STORE_PREFAB" => m_storePrefab, 
			"LEAVING_SOON" => m_leavingSoon, 
			"LEAVING_SOON_TEXT" => m_leavingSoonText, 
			"GAME_MODE_ICON" => m_gameModeIcon, 
			"PRODUCT_STRING_KEY" => m_productStringKey, 
			"STANDARD_EVENT" => m_standardEvent, 
			"COMING_SOON_EVENT" => m_comingSoonEvent, 
			"COMING_SOON_TEXT" => m_comingSoonText, 
			"MAP_PAGE_HAS_BUTTONS_TO_CHAPTERS" => m_mapPageHasButtonsToChapters, 
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
		case "NAME":
			m_name = (DbfLocValue)val;
			break;
		case "SORT_ORDER":
			m_sortOrder = (int)val;
			break;
		case "STORE_BUY_BUTTON_LABEL":
			m_storeBuyButtonLabel = (DbfLocValue)val;
			break;
		case "STORE_BUY_WINGS_1_HEADLINE":
			m_storeBuyWings1Headline = (DbfLocValue)val;
			break;
		case "STORE_BUY_WINGS_2_HEADLINE":
			m_storeBuyWings2Headline = (DbfLocValue)val;
			break;
		case "STORE_BUY_WINGS_3_HEADLINE":
			m_storeBuyWings3Headline = (DbfLocValue)val;
			break;
		case "STORE_BUY_WINGS_4_HEADLINE":
			m_storeBuyWings4Headline = (DbfLocValue)val;
			break;
		case "STORE_BUY_WINGS_5_HEADLINE":
			m_storeBuyWings5Headline = (DbfLocValue)val;
			break;
		case "STORE_OWNED_HEADLINE":
			m_storeOwnedHeadline = (DbfLocValue)val;
			break;
		case "STORE_PREORDER_HEADLINE":
			m_storePreorderHeadline = (DbfLocValue)val;
			break;
		case "STORE_BUY_WINGS_1_DESC":
			m_storeBuyWings1Desc = (DbfLocValue)val;
			break;
		case "STORE_BUY_WINGS_2_DESC":
			m_storeBuyWings2Desc = (DbfLocValue)val;
			break;
		case "STORE_BUY_WINGS_3_DESC":
			m_storeBuyWings3Desc = (DbfLocValue)val;
			break;
		case "STORE_BUY_WINGS_4_DESC":
			m_storeBuyWings4Desc = (DbfLocValue)val;
			break;
		case "STORE_BUY_WINGS_5_DESC":
			m_storeBuyWings5Desc = (DbfLocValue)val;
			break;
		case "STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_TRUE":
			m_storeBuyRemainingWingsDescTimelockedTrue = (DbfLocValue)val;
			break;
		case "STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_FALSE":
			m_storeBuyRemainingWingsDescTimelockedFalse = (DbfLocValue)val;
			break;
		case "STORE_OWNED_DESC":
			m_storeOwnedDesc = (DbfLocValue)val;
			break;
		case "STORE_PREORDER_WINGS_1_DESC":
			m_storePreorderWings1Desc = (DbfLocValue)val;
			break;
		case "STORE_PREORDER_WINGS_2_DESC":
			m_storePreorderWings2Desc = (DbfLocValue)val;
			break;
		case "STORE_PREORDER_WINGS_3_DESC":
			m_storePreorderWings3Desc = (DbfLocValue)val;
			break;
		case "STORE_PREORDER_WINGS_4_DESC":
			m_storePreorderWings4Desc = (DbfLocValue)val;
			break;
		case "STORE_PREORDER_WINGS_5_DESC":
			m_storePreorderWings5Desc = (DbfLocValue)val;
			break;
		case "STORE_PREORDER_RADIO_TEXT":
			m_storePreorderRadioText = (DbfLocValue)val;
			break;
		case "STORE_PREVIEW_REWARDS_TEXT":
			m_storePreviewRewardsText = (DbfLocValue)val;
			break;
		case "ADVENTURE_DEF_PREFAB":
			m_adventureDefPrefab = (string)val;
			break;
		case "STORE_PREFAB":
			m_storePrefab = (string)val;
			break;
		case "LEAVING_SOON":
			m_leavingSoon = (bool)val;
			break;
		case "LEAVING_SOON_TEXT":
			m_leavingSoonText = (DbfLocValue)val;
			break;
		case "GAME_MODE_ICON":
			m_gameModeIcon = (string)val;
			break;
		case "PRODUCT_STRING_KEY":
			m_productStringKey = (string)val;
			break;
		case "STANDARD_EVENT":
			m_standardEvent = (string)val;
			break;
		case "COMING_SOON_EVENT":
			m_comingSoonEvent = (string)val;
			break;
		case "COMING_SOON_TEXT":
			m_comingSoonText = (DbfLocValue)val;
			break;
		case "MAP_PAGE_HAS_BUTTONS_TO_CHAPTERS":
			m_mapPageHasButtonsToChapters = (bool)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"NAME" => typeof(DbfLocValue), 
			"SORT_ORDER" => typeof(int), 
			"STORE_BUY_BUTTON_LABEL" => typeof(DbfLocValue), 
			"STORE_BUY_WINGS_1_HEADLINE" => typeof(DbfLocValue), 
			"STORE_BUY_WINGS_2_HEADLINE" => typeof(DbfLocValue), 
			"STORE_BUY_WINGS_3_HEADLINE" => typeof(DbfLocValue), 
			"STORE_BUY_WINGS_4_HEADLINE" => typeof(DbfLocValue), 
			"STORE_BUY_WINGS_5_HEADLINE" => typeof(DbfLocValue), 
			"STORE_OWNED_HEADLINE" => typeof(DbfLocValue), 
			"STORE_PREORDER_HEADLINE" => typeof(DbfLocValue), 
			"STORE_BUY_WINGS_1_DESC" => typeof(DbfLocValue), 
			"STORE_BUY_WINGS_2_DESC" => typeof(DbfLocValue), 
			"STORE_BUY_WINGS_3_DESC" => typeof(DbfLocValue), 
			"STORE_BUY_WINGS_4_DESC" => typeof(DbfLocValue), 
			"STORE_BUY_WINGS_5_DESC" => typeof(DbfLocValue), 
			"STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_TRUE" => typeof(DbfLocValue), 
			"STORE_BUY_REMAINING_WINGS_DESC_TIMELOCKED_FALSE" => typeof(DbfLocValue), 
			"STORE_OWNED_DESC" => typeof(DbfLocValue), 
			"STORE_PREORDER_WINGS_1_DESC" => typeof(DbfLocValue), 
			"STORE_PREORDER_WINGS_2_DESC" => typeof(DbfLocValue), 
			"STORE_PREORDER_WINGS_3_DESC" => typeof(DbfLocValue), 
			"STORE_PREORDER_WINGS_4_DESC" => typeof(DbfLocValue), 
			"STORE_PREORDER_WINGS_5_DESC" => typeof(DbfLocValue), 
			"STORE_PREORDER_RADIO_TEXT" => typeof(DbfLocValue), 
			"STORE_PREVIEW_REWARDS_TEXT" => typeof(DbfLocValue), 
			"ADVENTURE_DEF_PREFAB" => typeof(string), 
			"STORE_PREFAB" => typeof(string), 
			"LEAVING_SOON" => typeof(bool), 
			"LEAVING_SOON_TEXT" => typeof(DbfLocValue), 
			"GAME_MODE_ICON" => typeof(string), 
			"PRODUCT_STRING_KEY" => typeof(string), 
			"STANDARD_EVENT" => typeof(string), 
			"COMING_SOON_EVENT" => typeof(string), 
			"COMING_SOON_TEXT" => typeof(DbfLocValue), 
			"MAP_PAGE_HAS_BUTTONS_TO_CHAPTERS" => typeof(bool), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAdventureDbfRecords loadRecords = new LoadAdventureDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AdventureDbfAsset adventureDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AdventureDbfAsset)) as AdventureDbfAsset;
		if (adventureDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"AdventureDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < adventureDbfAsset.Records.Count; i++)
		{
			adventureDbfAsset.Records[i].StripUnusedLocales();
		}
		records = adventureDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_name.StripUnusedLocales();
		m_storeBuyButtonLabel.StripUnusedLocales();
		m_storeBuyWings1Headline.StripUnusedLocales();
		m_storeBuyWings2Headline.StripUnusedLocales();
		m_storeBuyWings3Headline.StripUnusedLocales();
		m_storeBuyWings4Headline.StripUnusedLocales();
		m_storeBuyWings5Headline.StripUnusedLocales();
		m_storeOwnedHeadline.StripUnusedLocales();
		m_storePreorderHeadline.StripUnusedLocales();
		m_storeBuyWings1Desc.StripUnusedLocales();
		m_storeBuyWings2Desc.StripUnusedLocales();
		m_storeBuyWings3Desc.StripUnusedLocales();
		m_storeBuyWings4Desc.StripUnusedLocales();
		m_storeBuyWings5Desc.StripUnusedLocales();
		m_storeBuyRemainingWingsDescTimelockedTrue.StripUnusedLocales();
		m_storeBuyRemainingWingsDescTimelockedFalse.StripUnusedLocales();
		m_storeOwnedDesc.StripUnusedLocales();
		m_storePreorderWings1Desc.StripUnusedLocales();
		m_storePreorderWings2Desc.StripUnusedLocales();
		m_storePreorderWings3Desc.StripUnusedLocales();
		m_storePreorderWings4Desc.StripUnusedLocales();
		m_storePreorderWings5Desc.StripUnusedLocales();
		m_storePreorderRadioText.StripUnusedLocales();
		m_storePreviewRewardsText.StripUnusedLocales();
		m_leavingSoonText.StripUnusedLocales();
		m_comingSoonText.StripUnusedLocales();
	}
}
