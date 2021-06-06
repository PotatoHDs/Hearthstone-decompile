using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;
using Hearthstone.DungeonCrawl;
using UnityEngine;

[CustomEditClass]
public class DungeonCrawlSubDef_VOLines : MonoBehaviour
{
	public enum VOEventType
	{
		INVALID,
		CHARACTER_SELECT,
		BOSS_REVEAL_1,
		BOSS_REVEAL_2,
		BOSS_REVEAL_3,
		BOSS_REVEAL_GENERAL,
		OFFER_TREASURE_1,
		OFFER_TREASURE_GENERAL,
		TAKE_TREASURE_GENERAL,
		OFFER_LOOT_PACKS_1,
		OFFER_LOOT_PACKS_2,
		WELCOME_BANNER,
		COMPLETE_ALL_CLASSES_FIRST_TIME,
		COMPLETE_ALL_CLASSES,
		COMPLETE_FIRST_CLASS,
		COMPLETE_SECOND_CLASS,
		COMPLETE_THIRD_CLASS,
		OFFER_TREASURE_2,
		OFFER_TREASURE_3,
		OFFER_TREASURE_4,
		OFFER_HERO_POWER_1,
		OFFER_DECK_1,
		BOSS_REVEAL_4,
		BOSS_REVEAL_5,
		BOOK_REVEAL,
		BOOK_REVEAL_HEROIC,
		WING_UNLOCK,
		COMPLETE_ALL_WINGS,
		COMPLETE_ALL_WINGS_HEROIC,
		ANOMALY_UNLOCK,
		REWARD_PAGE_REVEAL,
		FINAL_BOSS_REVEAL,
		FINAL_BOSS_LOSS_1,
		FINAL_BOSS_LOSS_2,
		FINAL_BOSS_LOSS_GENERAL,
		COMPLETE_FIRST_WING,
		COMPLETE_SECOND_WING,
		COMPLETE_THIRD_WING,
		COMPLETE_FOURTH_WING,
		COMPLETE_FIFTH_WING,
		BOSS_LOSS_1,
		WING_COMPLETE_GENERAL,
		CHAPTER_PAGE,
		BOSS_LOSS_1_SECOND_BOOK_SECTION,
		COMPLETE_ALL_WINGS_SECOND_BOOK_SECTION,
		COMPLETE_ALL_WINGS_SECOND_BOOK_SECTION_HEROIC,
		CALL_TO_ACTION
	}

	public enum HasSeenDataGameSaveSubkey
	{
		INVALID = 0,
		DUNGEON_CRAWL_HAS_SEEN_CHARACTER_SELECT_VO = 3,
		DUNGEON_CRAWL_HAS_SEEN_WELCOME_BANNER_VO = 4,
		DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_1_VO = 5,
		DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_2_VO = 6,
		DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_3_VO = 7,
		DUNGEON_CRAWL_HAS_SEEN_OFFER_TREASURE_1_VO = 8,
		DUNGEON_CRAWL_HAS_SEEN_OFFER_LOOT_PACKS_1_VO = 9,
		DUNGEON_CRAWL_HAS_SEEN_OFFER_LOOT_PACKS_2_VO = 10,
		DUNGEON_CRAWL_HAS_SEEN_IN_GAME_WIN_VO = 12,
		DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_VO = 13,
		DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_1_VO = 14,
		DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_2_VO = 0xF,
		DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_2_VO = 18,
		DUNGEON_CRAWL_HAS_SEEN_OFFER_TREASURE_2_VO = 19,
		DUNGEON_CRAWL_HAS_SEEN_OFFER_TREASURE_3_VO = 20,
		DUNGEON_CRAWL_HAS_SEEN_OFFER_TREASURE_4_VO = 21,
		DUNGEON_CRAWL_HAS_SEEN_OFFER_HERO_POWER_1_VO = 22,
		DUNGEON_CRAWL_HAS_SEEN_OFFER_DECK_1_VO = 23,
		DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_4_VO = 24,
		DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_5_VO = 25,
		DUNGEON_CRAWL_HAS_SEEN_BOOK_REVEAL_VO = 26,
		DUNGEON_CRAWL_HAS_SEEN_BOOK_REVEAL_HEROIC_VO = 27,
		DUNGEON_CRAWL_HAS_SEEN_WING_UNLOCK_VO = 28,
		DUNGEON_CRAWL_HAS_SEEN_COMPLETE_ALL_WINGS_VO = 29,
		DUNGEON_CRAWL_HAS_SEEN_COMPLETE_ALL_WINGS_HEROIC_VO = 30,
		DUNGEON_CRAWL_HAS_SEEN_ANOMALY_UNLOCK_VO = 0x1F,
		DUNGEON_CRAWL_HAS_SEEN_REWARD_PAGE_REVEAL_VO = 0x20,
		DUNGEON_CRAWL_HAS_SEEN_FINAL_BOSS_LOSS_1_VO = 33,
		DUNGEON_CRAWL_HAS_SEEN_FINAL_BOSS_LOSS_2_VO = 34,
		DUNGEON_CRAWL_HAS_SEEN_FINAL_BOSS_REVEAL_1_VO = 35,
		DUNGEON_CRAWL_HAS_SEEN_BOSS_LOSS_1_VO = 36,
		DUNGEON_CRAWL_HAS_SEEN_CHAPTER_PAGE_VO = 37,
		DUNGEON_CRAWL_HAS_SEEN_BOSS_LOSS_1_SECOND_BOOK_SECTION_VO = 38,
		DUNGEON_CRAWL_HAS_SEEN_COMPLETE_ALL_WINGS_SECOND_BOOK_SECTION_VO = 39,
		DUNGEON_CRAWL_HAS_SEEN_COMPLETE_ALL_WINGS_SECOND_BOOK_SECTION_HEROIC_VO = 40,
		DUNGEON_CRAWL_HAS_SEEN_CALL_TO_ACTION_VO = 41
	}

	[Serializable]
	public class VOEventData
	{
		public VOEventType m_EventType;

		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string m_QuotePrefab;

		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string m_QuoteVOSoundPrefab;

		public HasSeenDataGameSaveSubkey m_EventSeenOption;

		public int m_AssociatedCardID;

		public int m_HeroCardID;

		public float m_ChanceToPlay = -1f;

		public int m_MinimumRequiredBossesDefeated;

		public bool m_BlockAllOtherInput;

		public Vector3 m_QuotePosition = NotificationManager.DEFAULT_CHARACTER_POS;

		[CustomEditField(T = EditType.GAME_OBJECT)]
		public List<CharacterQuoteVOObject> m_MultiQuoteVO;

		[CustomEditField(T = EditType.GAME_OBJECT)]
		public List<CharacterQuoteVOObject> m_RandomQuoteVO;

		[CustomEditField(T = EditType.GAME_OBJECT)]
		public List<VOConstraintObject> m_QuoteConstraints;
	}

	[Serializable]
	public class CharacterQuoteVOObject
	{
		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string SoundPrefab;

		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string CharacterPrefab;
	}

	[Serializable]
	public class VOConstraintObject
	{
		public enum ConstraintType
		{
			WingIsUnlocked,
			WingIsLocked,
			WingIsCompleted
		}

		public ConstraintType Constraint;

		public int Value;
	}

	private class VOData
	{
		public DungeonCrawlSubDef_VOLines m_VOLines;

		public VOEventData m_EventData;
	}

	[CustomEditField(Sections = "Defaults", T = EditType.GAME_OBJECT)]
	public string m_DefaultQuotePrefab;

	[CustomEditField(Sections = "Defaults")]
	public float m_DefaultChanceToPlay = 1f;

	public List<VOEventType> m_TutorialEventTypes;

	public List<VOEventData> m_VOEventDataList = new List<VOEventData>();

	public static readonly VOEventType[] BOSS_REVEAL_EVENTS = new VOEventType[6]
	{
		VOEventType.BOSS_REVEAL_1,
		VOEventType.BOSS_REVEAL_2,
		VOEventType.BOSS_REVEAL_3,
		VOEventType.BOSS_REVEAL_4,
		VOEventType.BOSS_REVEAL_5,
		VOEventType.BOSS_REVEAL_GENERAL
	};

	public static readonly VOEventType[] FINAL_BOSS_LOSS_EVENTS = new VOEventType[3]
	{
		VOEventType.FINAL_BOSS_LOSS_1,
		VOEventType.FINAL_BOSS_LOSS_2,
		VOEventType.FINAL_BOSS_LOSS_GENERAL
	};

	public static readonly VOEventType[] OFFER_TREASURE_EVENTS = new VOEventType[5]
	{
		VOEventType.OFFER_TREASURE_1,
		VOEventType.OFFER_TREASURE_2,
		VOEventType.OFFER_TREASURE_3,
		VOEventType.OFFER_TREASURE_4,
		VOEventType.OFFER_TREASURE_GENERAL
	};

	public static readonly VOEventType[] OFFER_LOOT_PACKS_EVENTS = new VOEventType[2]
	{
		VOEventType.OFFER_LOOT_PACKS_1,
		VOEventType.OFFER_LOOT_PACKS_2
	};

	public static readonly VOEventType[] OFFER_HERO_POWER_EVENTS = new VOEventType[1] { VOEventType.OFFER_HERO_POWER_1 };

	public static readonly VOEventType[] OFFER_DECK_EVENTS = new VOEventType[1] { VOEventType.OFFER_DECK_1 };

	public static readonly VOEventType[] WING_COMPLETE_EVENTS = new VOEventType[5]
	{
		VOEventType.COMPLETE_FIRST_WING,
		VOEventType.COMPLETE_SECOND_WING,
		VOEventType.COMPLETE_THIRD_WING,
		VOEventType.COMPLETE_FOURTH_WING,
		VOEventType.COMPLETE_FIFTH_WING
	};

	public static readonly VOEventType[] CLASS_COMPLETE_EVENTS = new VOEventType[3]
	{
		VOEventType.COMPLETE_FIRST_CLASS,
		VOEventType.COMPLETE_SECOND_CLASS,
		VOEventType.COMPLETE_THIRD_CLASS
	};

	private List<int> m_sortedHeroDbIds = new List<int>();

	private bool m_isWingVO;

	private Map<VOEventType, Map<int, Map<int, VOEventData>>> m_VOEventDataMap = new Map<VOEventType, Map<int, Map<int, VOEventData>>>();

	private Map<VOEventType, List<int>> m_VOTutorialEventRefIdMap = new Map<VOEventType, List<int>>();

	private void Awake()
	{
		AdventureWingDef component = GetComponent<AdventureWingDef>();
		m_isWingVO = component != null;
		if (m_isWingVO && m_TutorialEventTypes.Count > 0)
		{
			Debug.LogErrorFormat("Tutorial VO events on wing defs ({0}) are not supported and they will not be considered when deciding to play a VO line.", base.gameObject.name);
			m_TutorialEventTypes.Clear();
		}
	}

	private void Start()
	{
		foreach (VOEventData vOEventData in m_VOEventDataList)
		{
			if (!m_VOEventDataMap.ContainsKey(vOEventData.m_EventType))
			{
				m_VOEventDataMap.Add(vOEventData.m_EventType, new Map<int, Map<int, VOEventData>>());
			}
			if (!m_VOEventDataMap[vOEventData.m_EventType].ContainsKey(vOEventData.m_HeroCardID))
			{
				m_VOEventDataMap[vOEventData.m_EventType].Add(vOEventData.m_HeroCardID, new Map<int, VOEventData>());
			}
			if (m_VOEventDataMap[vOEventData.m_EventType][vOEventData.m_HeroCardID].ContainsKey(vOEventData.m_AssociatedCardID))
			{
				Debug.LogWarningFormat("DungeonCrawlSubDef_VOLines - Tried to add AssociatedCardID ({0}) with HeroCardID ({1}) for VOEventType ({2}) twice to the m_VOEventDataList. Using latest...", vOEventData.m_AssociatedCardID, vOEventData.m_HeroCardID, vOEventData.m_EventType);
				m_VOEventDataMap[vOEventData.m_EventType][vOEventData.m_HeroCardID][vOEventData.m_AssociatedCardID] = vOEventData;
			}
			else
			{
				m_VOEventDataMap[vOEventData.m_EventType][vOEventData.m_HeroCardID].Add(vOEventData.m_AssociatedCardID, vOEventData);
			}
		}
		foreach (VOEventType tutorialEventType in m_TutorialEventTypes)
		{
			if (m_VOEventDataMap.ContainsKey(tutorialEventType) && m_VOEventDataMap[tutorialEventType].ContainsKey(0))
			{
				if (m_VOTutorialEventRefIdMap.ContainsKey(tutorialEventType))
				{
					Debug.LogWarningFormat("DungeonCrawlSubDef_VOLines - Tried to add VOEventType ({0}) twice to the m_VOTutorialEventRefIdMap for {1}. Skipping...", tutorialEventType, base.gameObject.name);
				}
				else
				{
					m_VOTutorialEventRefIdMap.Add(tutorialEventType, m_VOEventDataMap[tutorialEventType][0].Keys.ToList());
				}
			}
		}
	}

	private static AdventureModeDbId GetModeBasedOnCurrentScene()
	{
		AdventureModeDbId modeId = AdventureModeDbId.DUNGEON_CRAWL;
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.ADVENTURE)
		{
			AdventureModeDbId selectedMode = AdventureConfig.Get().GetSelectedMode();
			modeId = GameUtils.GetNormalModeFromHeroicMode(selectedMode);
			if (GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == (int)AdventureConfig.Get().GetSelectedAdventure() && r.ModeId == (int)modeId) == null)
			{
				modeId = selectedMode;
			}
		}
		return modeId;
	}

	public VOEventData GetVOEventData(VOEventType voEventType, int heroDbId, int referenceID = 0)
	{
		if (!m_VOEventDataMap.ContainsKey(voEventType))
		{
			return null;
		}
		int key = 0;
		if (m_VOEventDataMap[voEventType].ContainsKey(heroDbId) && m_VOEventDataMap[voEventType][heroDbId].ContainsKey(referenceID))
		{
			key = heroDbId;
		}
		if (!m_VOEventDataMap[voEventType].ContainsKey(key) || !m_VOEventDataMap[voEventType][key].ContainsKey(referenceID))
		{
			return null;
		}
		return m_VOEventDataMap[voEventType][key][referenceID];
	}

	private static DungeonCrawlSubDef_VOLines GetAdventureModeVOLines(AdventureDbId adventureId)
	{
		AdventureModeDbId modeBasedOnCurrentScene = GetModeBasedOnCurrentScene();
		AdventureDef adventureDef;
		switch (SceneMgr.Get().GetMode())
		{
		case SceneMgr.Mode.ADVENTURE:
		{
			AdventureScene adventureScene = AdventureScene.Get();
			if (adventureScene == null)
			{
				return null;
			}
			adventureDef = adventureScene.GetAdventureDef(adventureId);
			break;
		}
		case SceneMgr.Mode.TAVERN_BRAWL:
		{
			TavernBrawlDisplay tavernBrawlDisplay = TavernBrawlDisplay.Get();
			if (tavernBrawlDisplay == null)
			{
				return null;
			}
			adventureDef = tavernBrawlDisplay.GetAdventureDef(adventureId);
			break;
		}
		case SceneMgr.Mode.PVP_DUNGEON_RUN:
		{
			PvPDungeonRunScene pvPDungeonRunScene = PvPDungeonRunScene.Get();
			if (pvPDungeonRunScene == null)
			{
				return null;
			}
			adventureDef = pvPDungeonRunScene.GetAdventureDef(adventureId);
			break;
		}
		default:
			return null;
		}
		if (adventureDef == null)
		{
			Debug.LogErrorFormat("No AdventureDef for AdventureDbId {0}!", adventureId);
			return null;
		}
		AdventureSubDef subDef = adventureDef.GetSubDef(modeBasedOnCurrentScene);
		if (subDef == null)
		{
			Debug.LogErrorFormat("No AdventureSubDef for AdventureDbId {0} and AdventureModeDbId {1}!", adventureId, modeBasedOnCurrentScene);
			return null;
		}
		return subDef.GetComponent<DungeonCrawlSubDef_VOLines>();
	}

	private static DungeonCrawlSubDef_VOLines GetAdventureWingVOLines(WingDbId wingId)
	{
		AdventureWingDef adventureWingDef;
		switch (SceneMgr.Get().GetMode())
		{
		case SceneMgr.Mode.ADVENTURE:
		{
			AdventureScene adventureScene = AdventureScene.Get();
			if (adventureScene == null)
			{
				return null;
			}
			adventureWingDef = adventureScene.GetWingDef(wingId);
			if (adventureWingDef == null)
			{
				return null;
			}
			break;
		}
		case SceneMgr.Mode.TAVERN_BRAWL:
		{
			TavernBrawlDisplay tavernBrawlDisplay = TavernBrawlDisplay.Get();
			if (tavernBrawlDisplay == null)
			{
				return null;
			}
			adventureWingDef = tavernBrawlDisplay.GetAdventureWingDef(wingId);
			if (adventureWingDef == null)
			{
				return null;
			}
			break;
		}
		case SceneMgr.Mode.PVP_DUNGEON_RUN:
		{
			PvPDungeonRunScene pvPDungeonRunScene = PvPDungeonRunScene.Get();
			if (pvPDungeonRunScene == null)
			{
				return null;
			}
			adventureWingDef = pvPDungeonRunScene.GetWingDef(wingId);
			if (adventureWingDef == null)
			{
				return null;
			}
			break;
		}
		default:
			return null;
		}
		return adventureWingDef.GetComponent<DungeonCrawlSubDef_VOLines>();
	}

	private static bool HasEventDataBeenSeen(AdventureDbId adventureId, WingDbId wingId, VOEventData eventData, bool isWingVO)
	{
		AdventureModeDbId modeBasedOnCurrentScene = GetModeBasedOnCurrentScene();
		if (eventData == null)
		{
			return false;
		}
		GameSaveKeyId gameSaveDataClientKey = (GameSaveKeyId)GameUtils.GetAdventureDataRecord((int)adventureId, (int)modeBasedOnCurrentScene).GameSaveDataClientKey;
		GameSaveKeySubkeyId subkeyFromHasSeenOption = GetSubkeyFromHasSeenOption(eventData.m_EventSeenOption);
		if (!GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataClientKey, subkeyFromHasSeenOption, out List<long> values))
		{
			return false;
		}
		int num = 0;
		if (isWingVO)
		{
			WingDbfRecord record = GameDbf.Wing.GetRecord((int)wingId);
			num = ((record != null) ? (GameUtils.GetSortedWingUnlockIndex(record) + 1) : 0);
		}
		if (values.Count > num && (values[num] & GetGSDFlagFromHeroCardDbID(adventureId, eventData.m_HeroCardID)) != 0L)
		{
			return true;
		}
		return false;
	}

	private bool IsEventDataValid(AdventureDbId adventureId, WingDbId wingId, int heroDbId, VOEventData eventData)
	{
		AdventureModeDbId modeBasedOnCurrentScene = GetModeBasedOnCurrentScene();
		if (eventData == null)
		{
			return false;
		}
		if (HasEventDataBeenSeen(adventureId, wingId, eventData, m_isWingVO))
		{
			return false;
		}
		if (eventData.m_MinimumRequiredBossesDefeated > 0)
		{
			AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)modeBasedOnCurrentScene);
			if (adventureDataRecord.GameSaveDataServerKey == 0)
			{
				Debug.LogWarningFormat("DungeonCrawlSubDef_VOLines - Event {0} has MinimumRequiredBossesDefeated set, but Adventure {1} Wing {2} has no GameSaveDataServerKey!", eventData.m_EventType, adventureId, wingId);
				return false;
			}
			GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
			GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSSES_DEFEATED, out List<long> values);
			int num = values?.Count ?? 0;
			if (!DungeonCrawlUtil.IsDungeonRunActive(gameSaveDataServerKey) || num < eventData.m_MinimumRequiredBossesDefeated)
			{
				return false;
			}
		}
		if (!m_isWingVO && !IsEventPartOfTutorial(eventData.m_EventType, heroDbId) && !IsVOEventTutorialComplete(adventureId))
		{
			return false;
		}
		bool flag = eventData.m_MultiQuoteVO.Count == 0 && eventData.m_RandomQuoteVO.Count == 0;
		if (string.IsNullOrEmpty(eventData.m_QuotePrefab) && string.IsNullOrEmpty(m_DefaultQuotePrefab) && flag)
		{
			return false;
		}
		if (string.IsNullOrEmpty(eventData.m_QuoteVOSoundPrefab) && flag)
		{
			return false;
		}
		return true;
	}

	private bool IsEventPartOfTutorial(VOEventType eventType, int heroDbId)
	{
		if (heroDbId > 0)
		{
			return false;
		}
		if (m_isWingVO)
		{
			return false;
		}
		if (!m_TutorialEventTypes.Contains(eventType))
		{
			return false;
		}
		return true;
	}

	private bool IsVOEventTutorialComplete(AdventureDbId adventureId)
	{
		if (m_isWingVO)
		{
			return true;
		}
		int heroDbId = 0;
		foreach (VOEventType tutorialEventType in m_TutorialEventTypes)
		{
			if (!m_VOTutorialEventRefIdMap.ContainsKey(tutorialEventType))
			{
				Debug.LogWarningFormat("DungeonCrawlSubDef_VOLines.IsVOEventTutorialComplete - TutorialEventType ({0}) in Adventure ({1}) was not found in the Ref ID map in {3}. Ensure that this event does not require a specific hero since hero specific tutorial events are not supported. Ignoring...", tutorialEventType, adventureId, base.gameObject.name);
				continue;
			}
			foreach (int item in m_VOTutorialEventRefIdMap[tutorialEventType])
			{
				VOEventData vOEventData = GetVOEventData(tutorialEventType, heroDbId, item);
				if (!HasEventDataBeenSeen(adventureId, WingDbId.INVALID, vOEventData, m_isWingVO))
				{
					return false;
				}
			}
		}
		return true;
	}

	public static bool PlayVOLine(AdventureDbId adventureId, WingDbId wingId, int heroDbId, VOEventType voEvent, int referenceID = 0, bool allowRepeatDuringSession = true)
	{
		return PlayVOLine(adventureId, wingId, heroDbId, new VOEventType[1] { voEvent }, referenceID, allowRepeatDuringSession);
	}

	public static bool PlayVOLine(AdventureDbId adventureId, WingDbId wingId, int heroDbId, VOEventType[] voEvents, int referenceID = 0, bool allowRepeatDuringSession = true)
	{
		AdventureModeDbId modeBasedOnCurrentScene = GetModeBasedOnCurrentScene();
		VOData nextValidVOData = GetNextValidVOData(adventureId, wingId, heroDbId, voEvents, referenceID);
		DungeonCrawlSubDef_VOLines vOLines = nextValidVOData.m_VOLines;
		VOEventData eventData = nextValidVOData.m_EventData;
		if (vOLines == null)
		{
			Debug.LogErrorFormat("No DungeonCrawlSubDef_VOLines Component found on AdventureDbId {0}'s AdventureSubDef or on WingDbId {1}'s AdventureWingSubDef!", adventureId, wingId);
			return false;
		}
		if (eventData == null)
		{
			return false;
		}
		if (!EventConstraintsMet(eventData))
		{
			return false;
		}
		float num = UnityEngine.Random.Range(0f, 1f);
		float num2 = eventData.m_ChanceToPlay;
		if (num2 < 0f)
		{
			num2 = vOLines.m_DefaultChanceToPlay;
		}
		if (num2 < 1f && Cheats.VOChanceOverride >= 0f && HearthstoneApplication.IsInternal())
		{
			num2 = Cheats.VOChanceOverride;
		}
		if (num > num2)
		{
			return false;
		}
		string text = (string.IsNullOrEmpty(eventData.m_QuotePrefab) ? vOLines.m_DefaultQuotePrefab : eventData.m_QuotePrefab);
		if (eventData.m_MultiQuoteVO.Count > 0 && !string.IsNullOrEmpty(eventData.m_QuoteVOSoundPrefab))
		{
			Debug.LogErrorFormat("Playing a quote for eventType {0} and have both MultiQuotes and a VO Sound prefab.  Playing MultiQuotes", eventData.m_EventType);
		}
		else if (eventData.m_RandomQuoteVO.Count > 0 && !string.IsNullOrEmpty(eventData.m_QuoteVOSoundPrefab))
		{
			Debug.LogErrorFormat("Playing a quote for eventType {0} and have both RandomQuotes and a VO Sound prefab.  Playing RandomQuotes", eventData.m_EventType);
		}
		if (eventData.m_MultiQuoteVO.Count > 0)
		{
			PlayMultiLines(0, eventData.m_MultiQuoteVO.ToArray(), text, eventData.m_QuotePosition, eventData.m_BlockAllOtherInput, allowRepeatDuringSession);
		}
		else if (eventData.m_RandomQuoteVO.Count > 0)
		{
			PlayRandomLine(eventData.m_RandomQuoteVO.ToArray(), text, eventData.m_QuotePosition, eventData.m_BlockAllOtherInput);
		}
		else
		{
			string legacyAssetName = new AssetReference(eventData.m_QuoteVOSoundPrefab).GetLegacyAssetName();
			NotificationManager.Get().CreateCharacterQuote(text, eventData.m_QuotePosition, GameStrings.Get(legacyAssetName), eventData.m_QuoteVOSoundPrefab, allowRepeatDuringSession, 0f, null, CanvasAnchor.BOTTOM_LEFT, eventData.m_BlockAllOtherInput);
		}
		GameSaveKeyId gameSaveDataClientKey = (GameSaveKeyId)GameUtils.GetAdventureDataRecord((int)adventureId, (int)modeBasedOnCurrentScene).GameSaveDataClientKey;
		GameSaveKeySubkeyId subkeyFromHasSeenOption = GetSubkeyFromHasSeenOption(eventData.m_EventSeenOption);
		if (subkeyFromHasSeenOption != GameSaveKeySubkeyId.INVALID)
		{
			List<long> values = null;
			if (!GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataClientKey, subkeyFromHasSeenOption, out values))
			{
				values = new List<long> { 0L };
			}
			long num3 = GetGSDFlagFromHeroCardDbID(adventureId, eventData.m_HeroCardID);
			int index = 0;
			if (vOLines.m_isWingVO)
			{
				WingDbfRecord record = GameDbf.Wing.GetRecord((int)wingId);
				int numWingsInAdventure = GameUtils.GetNumWingsInAdventure(adventureId);
				index = ((record != null) ? (GameUtils.GetSortedWingUnlockIndex(record) + 1) : 0);
				if (values.Count < numWingsInAdventure)
				{
					values.AddRange(Enumerable.Repeat(0L, numWingsInAdventure + 1 - values.Count));
				}
			}
			values[index] |= num3;
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(gameSaveDataClientKey, subkeyFromHasSeenOption, values.ToArray()));
		}
		return true;
	}

	private static void PlayMultiLines(int index, CharacterQuoteVOObject[] lines, string prefab, Vector3 quotePosition, bool blockAllOtherInput, bool allowRepeatDuringSession)
	{
		Action<int> action = null;
		if (index < lines.Length - 1)
		{
			action = delegate
			{
				PlayMultiLines(index + 1, lines, prefab, quotePosition, blockAllOtherInput, allowRepeatDuringSession);
			};
		}
		string legacyAssetName = new AssetReference(lines[index].SoundPrefab).GetLegacyAssetName();
		string prefabPath = ((!string.IsNullOrEmpty(lines[index].CharacterPrefab)) ? lines[index].CharacterPrefab : prefab);
		NotificationManager notificationManager = NotificationManager.Get();
		Vector3 position = quotePosition;
		string text = GameStrings.Get(legacyAssetName);
		string soundPrefab = lines[index].SoundPrefab;
		Action<int> finishCallback = action;
		notificationManager.CreateCharacterQuote(prefabPath, position, text, soundPrefab, allowRepeatDuringSession, 0f, finishCallback, CanvasAnchor.BOTTOM_LEFT, blockAllOtherInput);
	}

	private static void PlayRandomLine(CharacterQuoteVOObject[] lines, string prefab, Vector3 quotePosition, bool blockAllOtherInput)
	{
		int num = UnityEngine.Random.Range(0, lines.Length);
		string legacyAssetName = new AssetReference(lines[num].SoundPrefab).GetLegacyAssetName();
		string prefabPath = ((!string.IsNullOrEmpty(lines[num].CharacterPrefab)) ? lines[num].CharacterPrefab : prefab);
		NotificationManager.Get().CreateCharacterQuote(prefabPath, quotePosition, GameStrings.Get(legacyAssetName), lines[num].SoundPrefab, allowRepeatDuringSession: true, 0f, null, CanvasAnchor.BOTTOM_LEFT, blockAllOtherInput);
	}

	public static VOEventType GetNextValidEventType(AdventureDbId adventureId, WingDbId wingId, int heroDbId, VOEventType[] events, int referenceID = 0)
	{
		VOData nextValidVOData = GetNextValidVOData(adventureId, wingId, heroDbId, events, referenceID);
		if (nextValidVOData.m_EventData != null)
		{
			return nextValidVOData.m_EventData.m_EventType;
		}
		return VOEventType.INVALID;
	}

	private static VOData GetNextValidVOData(AdventureDbId adventureId, WingDbId wingId, int heroDbId, VOEventType[] events, int referenceID = 0)
	{
		VOData vOData = new VOData();
		DungeonCrawlSubDef_VOLines adventureWingVOLines = GetAdventureWingVOLines(wingId);
		DungeonCrawlSubDef_VOLines adventureModeVOLines = GetAdventureModeVOLines(adventureId);
		bool flag = adventureModeVOLines == null || adventureModeVOLines.IsVOEventTutorialComplete(adventureId);
		vOData.m_VOLines = adventureWingVOLines;
		if (vOData.m_VOLines != null && flag)
		{
			vOData.m_EventData = vOData.m_VOLines.GetNextValidEventData(adventureId, wingId, heroDbId, events, referenceID);
			if (vOData.m_EventData != null && vOData.m_EventData.m_EventType != 0)
			{
				return vOData;
			}
		}
		vOData.m_VOLines = adventureModeVOLines;
		if (vOData.m_VOLines != null)
		{
			vOData.m_EventData = vOData.m_VOLines.GetNextValidEventData(adventureId, wingId, heroDbId, events, referenceID);
			if (vOData.m_EventData != null)
			{
				_ = vOData.m_EventData.m_EventType;
				return vOData;
			}
		}
		return vOData;
	}

	private VOEventData GetNextValidEventData(AdventureDbId adventureId, WingDbId wingId, int heroDbId, VOEventType[] events, int referenceID = 0)
	{
		List<int> list = new List<int> { heroDbId, 0 };
		foreach (VOEventType voEventType in events)
		{
			foreach (int item in list)
			{
				VOEventData vOEventData = GetVOEventData(voEventType, item, referenceID);
				if (vOEventData == null)
				{
					vOEventData = GetVOEventData(voEventType, item);
				}
				if (IsEventDataValid(adventureId, wingId, item, vOEventData))
				{
					return vOEventData;
				}
			}
		}
		return null;
	}

	private static GameSaveKeySubkeyId GetSubkeyFromHasSeenOption(HasSeenDataGameSaveSubkey hasSeenSubkey)
	{
		if (!Enum.IsDefined(typeof(HasSeenDataGameSaveSubkey), hasSeenSubkey))
		{
			Debug.LogErrorFormat("HasSeenDataGameSaveSubkey {0} is not a valid value!", hasSeenSubkey);
			return GameSaveKeySubkeyId.INVALID;
		}
		string text = hasSeenSubkey.ToString();
		object obj = Enum.Parse(typeof(GameSaveKeySubkeyId), text, ignoreCase: true);
		if (obj == null)
		{
			Debug.LogError("Unable to parse subkey from Dungeon Crawl HasSeenDataGameSaveSubkey: " + text);
			return GameSaveKeySubkeyId.INVALID;
		}
		return (GameSaveKeySubkeyId)obj;
	}

	private static int GetGSDFlagFromHeroCardDbID(AdventureDbId adventureId, int heroDbId)
	{
		DungeonCrawlSubDef_VOLines adventureModeVOLines = GetAdventureModeVOLines(adventureId);
		if (adventureModeVOLines == null)
		{
			Debug.LogErrorFormat("GetGSDFlagFromHeroCardDbID - unable to get the VO Lines component from the Adventure ({0}) sub def.", adventureId);
			return -1;
		}
		List<int> sortedHeroDbIds = adventureModeVOLines.m_sortedHeroDbIds;
		if (sortedHeroDbIds.Count <= 0)
		{
			List<AdventureGuestHeroesDbfRecord> records = GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)adventureId);
			records.Sort((AdventureGuestHeroesDbfRecord a, AdventureGuestHeroesDbfRecord b) => a.ID - b.ID);
			foreach (AdventureGuestHeroesDbfRecord item in records)
			{
				GuestHeroDbfRecord record = GameDbf.GuestHero.GetRecord(item.GuestHeroId);
				if (record != null)
				{
					sortedHeroDbIds.Add(record.CardId);
				}
			}
		}
		return 1 << sortedHeroDbIds.IndexOf(heroDbId) + 1;
	}

	private static bool EventConstraintsMet(VOEventData eventData)
	{
		if (eventData.m_QuoteConstraints.Count == 0)
		{
			return true;
		}
		foreach (VOConstraintObject quoteConstraint in eventData.m_QuoteConstraints)
		{
			VOConstraintObject.ConstraintType constraint = quoteConstraint.Constraint;
			if ((uint)constraint > 2u)
			{
				Debug.LogWarningFormat("DungeonCrawlSubDef_VOLines.EventConstraintsMet did not have a case to handle the passed constraint of type {0}.", quoteConstraint.Constraint.ToString());
			}
			else if (!EvaluateWingConstraint(quoteConstraint))
			{
				return false;
			}
		}
		return true;
	}

	private static bool EvaluateWingConstraint(VOConstraintObject quoteConstraint)
	{
		WingDbfRecord record = GameDbf.Wing.GetRecord(quoteConstraint.Value);
		if (record == null)
		{
			Debug.LogWarningFormat("DungeonCrawlSubDef_VOLines.EvaluateWingConstraint was called with invalid wing ID: {0}.", quoteConstraint.Value);
			return false;
		}
		AdventureChapterState adventureChapterState = AdventureProgressMgr.Get().AdventureBookChapterStateForWing(record, AdventureConfig.Get().GetSelectedMode());
		switch (quoteConstraint.Constraint)
		{
		default:
			Debug.LogWarningFormat("DungeonCrawlSubDef_VOLines.EvaluateWingConstraint was called with unsupported Constraint Type: {0}.", quoteConstraint.Constraint.ToString());
			return false;
		case VOConstraintObject.ConstraintType.WingIsLocked:
			return adventureChapterState == AdventureChapterState.LOCKED;
		case VOConstraintObject.ConstraintType.WingIsUnlocked:
			return adventureChapterState == AdventureChapterState.UNLOCKED;
		case VOConstraintObject.ConstraintType.WingIsCompleted:
			return adventureChapterState == AdventureChapterState.COMPLETED;
		}
	}
}
