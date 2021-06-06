using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class WingDbfRecord : DbfRecord
{
	[SerializeField]
	private string m_noteDesc;

	[SerializeField]
	private int m_adventureId;

	[SerializeField]
	private int m_sortOrder;

	[SerializeField]
	private int m_unlockOrder;

	[SerializeField]
	private string m_requiredEvent = "none";

	[SerializeField]
	private int m_ownershipPrereqWingId;

	[SerializeField]
	private DbfLocValue m_name;

	[SerializeField]
	private DbfLocValue m_nameShort;

	[SerializeField]
	private DbfLocValue m_description;

	[SerializeField]
	private DbfLocValue m_classChallengeRewardSource;

	[SerializeField]
	private string m_adventureWingDefPrefab;

	[SerializeField]
	private DbfLocValue m_comingSoonLabel;

	[SerializeField]
	private DbfLocValue m_requiresLabel;

	[SerializeField]
	private int m_openPrereqWingId;

	[SerializeField]
	private DbfLocValue m_openDiscouragedLabel;

	[SerializeField]
	private DbfLocValue m_openDiscouragedWarning;

	[SerializeField]
	private bool m_mustCompleteOpenPrereq;

	[SerializeField]
	private bool m_unlocksAutomatically;

	[SerializeField]
	private bool m_useUnlockCountdown;

	[SerializeField]
	private DbfLocValue m_storeBuyWingButtonLabel;

	[SerializeField]
	private DbfLocValue m_storeBuyWingDesc;

	[SerializeField]
	private int m_dungeonCrawlBosses = 8;

	[SerializeField]
	private string m_visualStateName;

	[SerializeField]
	private int m_plotTwistCardId;

	[SerializeField]
	private bool m_displayRaidBossHealth;

	[SerializeField]
	private int m_raidBossCardId;

	[SerializeField]
	private bool m_allowsAnomaly = true;

	[SerializeField]
	private int m_pmtProductIdForSingleWingPurchase;

	[SerializeField]
	private int m_pmtProductIdForThisAndRestOfAdventure;

	[SerializeField]
	private int m_bookSection;

	[DbfField("NOTE_DESC")]
	public string NoteDesc => m_noteDesc;

	[DbfField("ADVENTURE_ID")]
	public int AdventureId => m_adventureId;

	[DbfField("SORT_ORDER")]
	public int SortOrder => m_sortOrder;

	[DbfField("UNLOCK_ORDER")]
	public int UnlockOrder => m_unlockOrder;

	[DbfField("REQUIRED_EVENT")]
	public string RequiredEvent => m_requiredEvent;

	[DbfField("OWNERSHIP_PREREQ_WING_ID")]
	public int OwnershipPrereqWingId => m_ownershipPrereqWingId;

	public WingDbfRecord OwnershipPrereqWingRecord => GameDbf.Wing.GetRecord(m_ownershipPrereqWingId);

	[DbfField("NAME")]
	public DbfLocValue Name => m_name;

	[DbfField("NAME_SHORT")]
	public DbfLocValue NameShort => m_nameShort;

	[DbfField("DESCRIPTION")]
	public DbfLocValue Description => m_description;

	[DbfField("CLASS_CHALLENGE_REWARD_SOURCE")]
	public DbfLocValue ClassChallengeRewardSource => m_classChallengeRewardSource;

	[DbfField("ADVENTURE_WING_DEF_PREFAB")]
	public string AdventureWingDefPrefab => m_adventureWingDefPrefab;

	[DbfField("COMING_SOON_LABEL")]
	public DbfLocValue ComingSoonLabel => m_comingSoonLabel;

	[DbfField("REQUIRES_LABEL")]
	public DbfLocValue RequiresLabel => m_requiresLabel;

	[DbfField("OPEN_PREREQ_WING_ID")]
	public int OpenPrereqWingId => m_openPrereqWingId;

	public WingDbfRecord OpenPrereqWingRecord => GameDbf.Wing.GetRecord(m_openPrereqWingId);

	[DbfField("OPEN_DISCOURAGED_LABEL")]
	public DbfLocValue OpenDiscouragedLabel => m_openDiscouragedLabel;

	[DbfField("OPEN_DISCOURAGED_WARNING")]
	public DbfLocValue OpenDiscouragedWarning => m_openDiscouragedWarning;

	[DbfField("MUST_COMPLETE_OPEN_PREREQ")]
	public bool MustCompleteOpenPrereq => m_mustCompleteOpenPrereq;

	[DbfField("UNLOCKS_AUTOMATICALLY")]
	public bool UnlocksAutomatically => m_unlocksAutomatically;

	[DbfField("USE_UNLOCK_COUNTDOWN")]
	public bool UseUnlockCountdown => m_useUnlockCountdown;

	[DbfField("STORE_BUY_WING_BUTTON_LABEL")]
	public DbfLocValue StoreBuyWingButtonLabel => m_storeBuyWingButtonLabel;

	[DbfField("STORE_BUY_WING_DESC")]
	public DbfLocValue StoreBuyWingDesc => m_storeBuyWingDesc;

	[DbfField("DUNGEON_CRAWL_BOSSES")]
	public int DungeonCrawlBosses => m_dungeonCrawlBosses;

	[DbfField("VISUAL_STATE_NAME")]
	public string VisualStateName => m_visualStateName;

	[DbfField("PLOT_TWIST_CARD_ID")]
	public int PlotTwistCardId => m_plotTwistCardId;

	public CardDbfRecord PlotTwistCardRecord => GameDbf.Card.GetRecord(m_plotTwistCardId);

	[DbfField("DISPLAY_RAID_BOSS_HEALTH")]
	public bool DisplayRaidBossHealth => m_displayRaidBossHealth;

	[DbfField("RAID_BOSS_CARD_ID")]
	public int RaidBossCardId => m_raidBossCardId;

	public CardDbfRecord RaidBossCardRecord => GameDbf.Card.GetRecord(m_raidBossCardId);

	[DbfField("ALLOWS_ANOMALY")]
	public bool AllowsAnomaly => m_allowsAnomaly;

	[DbfField("PMT_PRODUCT_ID_FOR_SINGLE_WING_PURCHASE")]
	public int PmtProductIdForSingleWingPurchase => m_pmtProductIdForSingleWingPurchase;

	[DbfField("PMT_PRODUCT_ID_FOR_THIS_AND_REST_OF_ADVENTURE")]
	public int PmtProductIdForThisAndRestOfAdventure => m_pmtProductIdForThisAndRestOfAdventure;

	[DbfField("BOOK_SECTION")]
	public int BookSection => m_bookSection;

	public void SetNoteDesc(string v)
	{
		m_noteDesc = v;
	}

	public void SetAdventureId(int v)
	{
		m_adventureId = v;
	}

	public void SetSortOrder(int v)
	{
		m_sortOrder = v;
	}

	public void SetUnlockOrder(int v)
	{
		m_unlockOrder = v;
	}

	public void SetRequiredEvent(string v)
	{
		m_requiredEvent = v;
	}

	public void SetOwnershipPrereqWingId(int v)
	{
		m_ownershipPrereqWingId = v;
	}

	public void SetName(DbfLocValue v)
	{
		m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	public void SetNameShort(DbfLocValue v)
	{
		m_nameShort = v;
		v.SetDebugInfo(base.ID, "NAME_SHORT");
	}

	public void SetDescription(DbfLocValue v)
	{
		m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	public void SetClassChallengeRewardSource(DbfLocValue v)
	{
		m_classChallengeRewardSource = v;
		v.SetDebugInfo(base.ID, "CLASS_CHALLENGE_REWARD_SOURCE");
	}

	public void SetAdventureWingDefPrefab(string v)
	{
		m_adventureWingDefPrefab = v;
	}

	public void SetComingSoonLabel(DbfLocValue v)
	{
		m_comingSoonLabel = v;
		v.SetDebugInfo(base.ID, "COMING_SOON_LABEL");
	}

	public void SetRequiresLabel(DbfLocValue v)
	{
		m_requiresLabel = v;
		v.SetDebugInfo(base.ID, "REQUIRES_LABEL");
	}

	public void SetOpenPrereqWingId(int v)
	{
		m_openPrereqWingId = v;
	}

	public void SetOpenDiscouragedLabel(DbfLocValue v)
	{
		m_openDiscouragedLabel = v;
		v.SetDebugInfo(base.ID, "OPEN_DISCOURAGED_LABEL");
	}

	public void SetOpenDiscouragedWarning(DbfLocValue v)
	{
		m_openDiscouragedWarning = v;
		v.SetDebugInfo(base.ID, "OPEN_DISCOURAGED_WARNING");
	}

	public void SetMustCompleteOpenPrereq(bool v)
	{
		m_mustCompleteOpenPrereq = v;
	}

	public void SetUnlocksAutomatically(bool v)
	{
		m_unlocksAutomatically = v;
	}

	public void SetUseUnlockCountdown(bool v)
	{
		m_useUnlockCountdown = v;
	}

	public void SetStoreBuyWingButtonLabel(DbfLocValue v)
	{
		m_storeBuyWingButtonLabel = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WING_BUTTON_LABEL");
	}

	public void SetStoreBuyWingDesc(DbfLocValue v)
	{
		m_storeBuyWingDesc = v;
		v.SetDebugInfo(base.ID, "STORE_BUY_WING_DESC");
	}

	public void SetDungeonCrawlBosses(int v)
	{
		m_dungeonCrawlBosses = v;
	}

	public void SetVisualStateName(string v)
	{
		m_visualStateName = v;
	}

	public void SetPlotTwistCardId(int v)
	{
		m_plotTwistCardId = v;
	}

	public void SetDisplayRaidBossHealth(bool v)
	{
		m_displayRaidBossHealth = v;
	}

	public void SetRaidBossCardId(int v)
	{
		m_raidBossCardId = v;
	}

	public void SetAllowsAnomaly(bool v)
	{
		m_allowsAnomaly = v;
	}

	public void SetPmtProductIdForSingleWingPurchase(int v)
	{
		m_pmtProductIdForSingleWingPurchase = v;
	}

	public void SetPmtProductIdForThisAndRestOfAdventure(int v)
	{
		m_pmtProductIdForThisAndRestOfAdventure = v;
	}

	public void SetBookSection(int v)
	{
		m_bookSection = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NOTE_DESC" => m_noteDesc, 
			"ADVENTURE_ID" => m_adventureId, 
			"SORT_ORDER" => m_sortOrder, 
			"UNLOCK_ORDER" => m_unlockOrder, 
			"REQUIRED_EVENT" => m_requiredEvent, 
			"OWNERSHIP_PREREQ_WING_ID" => m_ownershipPrereqWingId, 
			"NAME" => m_name, 
			"NAME_SHORT" => m_nameShort, 
			"DESCRIPTION" => m_description, 
			"CLASS_CHALLENGE_REWARD_SOURCE" => m_classChallengeRewardSource, 
			"ADVENTURE_WING_DEF_PREFAB" => m_adventureWingDefPrefab, 
			"COMING_SOON_LABEL" => m_comingSoonLabel, 
			"REQUIRES_LABEL" => m_requiresLabel, 
			"OPEN_PREREQ_WING_ID" => m_openPrereqWingId, 
			"OPEN_DISCOURAGED_LABEL" => m_openDiscouragedLabel, 
			"OPEN_DISCOURAGED_WARNING" => m_openDiscouragedWarning, 
			"MUST_COMPLETE_OPEN_PREREQ" => m_mustCompleteOpenPrereq, 
			"UNLOCKS_AUTOMATICALLY" => m_unlocksAutomatically, 
			"USE_UNLOCK_COUNTDOWN" => m_useUnlockCountdown, 
			"STORE_BUY_WING_BUTTON_LABEL" => m_storeBuyWingButtonLabel, 
			"STORE_BUY_WING_DESC" => m_storeBuyWingDesc, 
			"DUNGEON_CRAWL_BOSSES" => m_dungeonCrawlBosses, 
			"VISUAL_STATE_NAME" => m_visualStateName, 
			"PLOT_TWIST_CARD_ID" => m_plotTwistCardId, 
			"DISPLAY_RAID_BOSS_HEALTH" => m_displayRaidBossHealth, 
			"RAID_BOSS_CARD_ID" => m_raidBossCardId, 
			"ALLOWS_ANOMALY" => m_allowsAnomaly, 
			"PMT_PRODUCT_ID_FOR_SINGLE_WING_PURCHASE" => m_pmtProductIdForSingleWingPurchase, 
			"PMT_PRODUCT_ID_FOR_THIS_AND_REST_OF_ADVENTURE" => m_pmtProductIdForThisAndRestOfAdventure, 
			"BOOK_SECTION" => m_bookSection, 
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
		case "ADVENTURE_ID":
			m_adventureId = (int)val;
			break;
		case "SORT_ORDER":
			m_sortOrder = (int)val;
			break;
		case "UNLOCK_ORDER":
			m_unlockOrder = (int)val;
			break;
		case "REQUIRED_EVENT":
			m_requiredEvent = (string)val;
			break;
		case "OWNERSHIP_PREREQ_WING_ID":
			m_ownershipPrereqWingId = (int)val;
			break;
		case "NAME":
			m_name = (DbfLocValue)val;
			break;
		case "NAME_SHORT":
			m_nameShort = (DbfLocValue)val;
			break;
		case "DESCRIPTION":
			m_description = (DbfLocValue)val;
			break;
		case "CLASS_CHALLENGE_REWARD_SOURCE":
			m_classChallengeRewardSource = (DbfLocValue)val;
			break;
		case "ADVENTURE_WING_DEF_PREFAB":
			m_adventureWingDefPrefab = (string)val;
			break;
		case "COMING_SOON_LABEL":
			m_comingSoonLabel = (DbfLocValue)val;
			break;
		case "REQUIRES_LABEL":
			m_requiresLabel = (DbfLocValue)val;
			break;
		case "OPEN_PREREQ_WING_ID":
			m_openPrereqWingId = (int)val;
			break;
		case "OPEN_DISCOURAGED_LABEL":
			m_openDiscouragedLabel = (DbfLocValue)val;
			break;
		case "OPEN_DISCOURAGED_WARNING":
			m_openDiscouragedWarning = (DbfLocValue)val;
			break;
		case "MUST_COMPLETE_OPEN_PREREQ":
			m_mustCompleteOpenPrereq = (bool)val;
			break;
		case "UNLOCKS_AUTOMATICALLY":
			m_unlocksAutomatically = (bool)val;
			break;
		case "USE_UNLOCK_COUNTDOWN":
			m_useUnlockCountdown = (bool)val;
			break;
		case "STORE_BUY_WING_BUTTON_LABEL":
			m_storeBuyWingButtonLabel = (DbfLocValue)val;
			break;
		case "STORE_BUY_WING_DESC":
			m_storeBuyWingDesc = (DbfLocValue)val;
			break;
		case "DUNGEON_CRAWL_BOSSES":
			m_dungeonCrawlBosses = (int)val;
			break;
		case "VISUAL_STATE_NAME":
			m_visualStateName = (string)val;
			break;
		case "PLOT_TWIST_CARD_ID":
			m_plotTwistCardId = (int)val;
			break;
		case "DISPLAY_RAID_BOSS_HEALTH":
			m_displayRaidBossHealth = (bool)val;
			break;
		case "RAID_BOSS_CARD_ID":
			m_raidBossCardId = (int)val;
			break;
		case "ALLOWS_ANOMALY":
			m_allowsAnomaly = (bool)val;
			break;
		case "PMT_PRODUCT_ID_FOR_SINGLE_WING_PURCHASE":
			m_pmtProductIdForSingleWingPurchase = (int)val;
			break;
		case "PMT_PRODUCT_ID_FOR_THIS_AND_REST_OF_ADVENTURE":
			m_pmtProductIdForThisAndRestOfAdventure = (int)val;
			break;
		case "BOOK_SECTION":
			m_bookSection = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NOTE_DESC" => typeof(string), 
			"ADVENTURE_ID" => typeof(int), 
			"SORT_ORDER" => typeof(int), 
			"UNLOCK_ORDER" => typeof(int), 
			"REQUIRED_EVENT" => typeof(string), 
			"OWNERSHIP_PREREQ_WING_ID" => typeof(int), 
			"NAME" => typeof(DbfLocValue), 
			"NAME_SHORT" => typeof(DbfLocValue), 
			"DESCRIPTION" => typeof(DbfLocValue), 
			"CLASS_CHALLENGE_REWARD_SOURCE" => typeof(DbfLocValue), 
			"ADVENTURE_WING_DEF_PREFAB" => typeof(string), 
			"COMING_SOON_LABEL" => typeof(DbfLocValue), 
			"REQUIRES_LABEL" => typeof(DbfLocValue), 
			"OPEN_PREREQ_WING_ID" => typeof(int), 
			"OPEN_DISCOURAGED_LABEL" => typeof(DbfLocValue), 
			"OPEN_DISCOURAGED_WARNING" => typeof(DbfLocValue), 
			"MUST_COMPLETE_OPEN_PREREQ" => typeof(bool), 
			"UNLOCKS_AUTOMATICALLY" => typeof(bool), 
			"USE_UNLOCK_COUNTDOWN" => typeof(bool), 
			"STORE_BUY_WING_BUTTON_LABEL" => typeof(DbfLocValue), 
			"STORE_BUY_WING_DESC" => typeof(DbfLocValue), 
			"DUNGEON_CRAWL_BOSSES" => typeof(int), 
			"VISUAL_STATE_NAME" => typeof(string), 
			"PLOT_TWIST_CARD_ID" => typeof(int), 
			"DISPLAY_RAID_BOSS_HEALTH" => typeof(bool), 
			"RAID_BOSS_CARD_ID" => typeof(int), 
			"ALLOWS_ANOMALY" => typeof(bool), 
			"PMT_PRODUCT_ID_FOR_SINGLE_WING_PURCHASE" => typeof(int), 
			"PMT_PRODUCT_ID_FOR_THIS_AND_REST_OF_ADVENTURE" => typeof(int), 
			"BOOK_SECTION" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadWingDbfRecords loadRecords = new LoadWingDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		WingDbfAsset wingDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(WingDbfAsset)) as WingDbfAsset;
		if (wingDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"WingDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < wingDbfAsset.Records.Count; i++)
		{
			wingDbfAsset.Records[i].StripUnusedLocales();
		}
		records = wingDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_name.StripUnusedLocales();
		m_nameShort.StripUnusedLocales();
		m_description.StripUnusedLocales();
		m_classChallengeRewardSource.StripUnusedLocales();
		m_comingSoonLabel.StripUnusedLocales();
		m_requiresLabel.StripUnusedLocales();
		m_openDiscouragedLabel.StripUnusedLocales();
		m_openDiscouragedWarning.StripUnusedLocales();
		m_storeBuyWingButtonLabel.StripUnusedLocales();
		m_storeBuyWingDesc.StripUnusedLocales();
	}
}
