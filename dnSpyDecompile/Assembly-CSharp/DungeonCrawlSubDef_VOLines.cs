using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;
using Hearthstone.DungeonCrawl;
using UnityEngine;

// Token: 0x02000065 RID: 101
[CustomEditClass]
public class DungeonCrawlSubDef_VOLines : MonoBehaviour
{
	// Token: 0x060005B0 RID: 1456 RVA: 0x00020C34 File Offset: 0x0001EE34
	private void Awake()
	{
		AdventureWingDef component = base.GetComponent<AdventureWingDef>();
		this.m_isWingVO = (component != null);
		if (this.m_isWingVO && this.m_TutorialEventTypes.Count > 0)
		{
			Debug.LogErrorFormat("Tutorial VO events on wing defs ({0}) are not supported and they will not be considered when deciding to play a VO line.", new object[]
			{
				base.gameObject.name
			});
			this.m_TutorialEventTypes.Clear();
		}
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x00020C94 File Offset: 0x0001EE94
	private void Start()
	{
		foreach (DungeonCrawlSubDef_VOLines.VOEventData voeventData in this.m_VOEventDataList)
		{
			if (!this.m_VOEventDataMap.ContainsKey(voeventData.m_EventType))
			{
				this.m_VOEventDataMap.Add(voeventData.m_EventType, new Map<int, Map<int, DungeonCrawlSubDef_VOLines.VOEventData>>());
			}
			if (!this.m_VOEventDataMap[voeventData.m_EventType].ContainsKey(voeventData.m_HeroCardID))
			{
				this.m_VOEventDataMap[voeventData.m_EventType].Add(voeventData.m_HeroCardID, new Map<int, DungeonCrawlSubDef_VOLines.VOEventData>());
			}
			if (this.m_VOEventDataMap[voeventData.m_EventType][voeventData.m_HeroCardID].ContainsKey(voeventData.m_AssociatedCardID))
			{
				Debug.LogWarningFormat("DungeonCrawlSubDef_VOLines - Tried to add AssociatedCardID ({0}) with HeroCardID ({1}) for VOEventType ({2}) twice to the m_VOEventDataList. Using latest...", new object[]
				{
					voeventData.m_AssociatedCardID,
					voeventData.m_HeroCardID,
					voeventData.m_EventType
				});
				this.m_VOEventDataMap[voeventData.m_EventType][voeventData.m_HeroCardID][voeventData.m_AssociatedCardID] = voeventData;
			}
			else
			{
				this.m_VOEventDataMap[voeventData.m_EventType][voeventData.m_HeroCardID].Add(voeventData.m_AssociatedCardID, voeventData);
			}
		}
		foreach (DungeonCrawlSubDef_VOLines.VOEventType voeventType in this.m_TutorialEventTypes)
		{
			if (this.m_VOEventDataMap.ContainsKey(voeventType) && this.m_VOEventDataMap[voeventType].ContainsKey(0))
			{
				if (this.m_VOTutorialEventRefIdMap.ContainsKey(voeventType))
				{
					Debug.LogWarningFormat("DungeonCrawlSubDef_VOLines - Tried to add VOEventType ({0}) twice to the m_VOTutorialEventRefIdMap for {1}. Skipping...", new object[]
					{
						voeventType,
						base.gameObject.name
					});
				}
				else
				{
					this.m_VOTutorialEventRefIdMap.Add(voeventType, this.m_VOEventDataMap[voeventType][0].Keys.ToList<int>());
				}
			}
		}
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x00020EE0 File Offset: 0x0001F0E0
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

	// Token: 0x060005B3 RID: 1459 RVA: 0x00020F44 File Offset: 0x0001F144
	public DungeonCrawlSubDef_VOLines.VOEventData GetVOEventData(DungeonCrawlSubDef_VOLines.VOEventType voEventType, int heroDbId, int referenceID = 0)
	{
		if (!this.m_VOEventDataMap.ContainsKey(voEventType))
		{
			return null;
		}
		int key = 0;
		if (this.m_VOEventDataMap[voEventType].ContainsKey(heroDbId) && this.m_VOEventDataMap[voEventType][heroDbId].ContainsKey(referenceID))
		{
			key = heroDbId;
		}
		if (!this.m_VOEventDataMap[voEventType].ContainsKey(key) || !this.m_VOEventDataMap[voEventType][key].ContainsKey(referenceID))
		{
			return null;
		}
		return this.m_VOEventDataMap[voEventType][key][referenceID];
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x00020FDC File Offset: 0x0001F1DC
	private static DungeonCrawlSubDef_VOLines GetAdventureModeVOLines(AdventureDbId adventureId)
	{
		AdventureModeDbId modeBasedOnCurrentScene = DungeonCrawlSubDef_VOLines.GetModeBasedOnCurrentScene();
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		AdventureDef adventureDef;
		if (mode != SceneMgr.Mode.ADVENTURE)
		{
			if (mode != SceneMgr.Mode.TAVERN_BRAWL)
			{
				if (mode != SceneMgr.Mode.PVP_DUNGEON_RUN)
				{
					return null;
				}
				PvPDungeonRunScene pvPDungeonRunScene = PvPDungeonRunScene.Get();
				if (pvPDungeonRunScene == null)
				{
					return null;
				}
				adventureDef = pvPDungeonRunScene.GetAdventureDef(adventureId);
			}
			else
			{
				TavernBrawlDisplay tavernBrawlDisplay = TavernBrawlDisplay.Get();
				if (tavernBrawlDisplay == null)
				{
					return null;
				}
				adventureDef = tavernBrawlDisplay.GetAdventureDef(adventureId);
			}
		}
		else
		{
			AdventureScene adventureScene = AdventureScene.Get();
			if (adventureScene == null)
			{
				return null;
			}
			adventureDef = adventureScene.GetAdventureDef(adventureId);
		}
		if (adventureDef == null)
		{
			Debug.LogErrorFormat("No AdventureDef for AdventureDbId {0}!", new object[]
			{
				adventureId
			});
			return null;
		}
		AdventureSubDef subDef = adventureDef.GetSubDef(modeBasedOnCurrentScene);
		if (subDef == null)
		{
			Debug.LogErrorFormat("No AdventureSubDef for AdventureDbId {0} and AdventureModeDbId {1}!", new object[]
			{
				adventureId,
				modeBasedOnCurrentScene
			});
			return null;
		}
		return subDef.GetComponent<DungeonCrawlSubDef_VOLines>();
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x000210C8 File Offset: 0x0001F2C8
	private static DungeonCrawlSubDef_VOLines GetAdventureWingVOLines(WingDbId wingId)
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		AdventureWingDef adventureWingDef;
		if (mode != SceneMgr.Mode.ADVENTURE)
		{
			if (mode != SceneMgr.Mode.TAVERN_BRAWL)
			{
				if (mode != SceneMgr.Mode.PVP_DUNGEON_RUN)
				{
					return null;
				}
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
			}
			else
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
			}
		}
		else
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
		}
		return adventureWingDef.GetComponent<DungeonCrawlSubDef_VOLines>();
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x00021168 File Offset: 0x0001F368
	private static bool HasEventDataBeenSeen(AdventureDbId adventureId, WingDbId wingId, DungeonCrawlSubDef_VOLines.VOEventData eventData, bool isWingVO)
	{
		AdventureModeDbId modeBasedOnCurrentScene = DungeonCrawlSubDef_VOLines.GetModeBasedOnCurrentScene();
		if (eventData == null)
		{
			return false;
		}
		GameSaveKeyId gameSaveDataClientKey = (GameSaveKeyId)GameUtils.GetAdventureDataRecord((int)adventureId, (int)modeBasedOnCurrentScene).GameSaveDataClientKey;
		GameSaveKeySubkeyId subkeyFromHasSeenOption = DungeonCrawlSubDef_VOLines.GetSubkeyFromHasSeenOption(eventData.m_EventSeenOption);
		List<long> list;
		if (!GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataClientKey, subkeyFromHasSeenOption, out list))
		{
			return false;
		}
		int num = 0;
		if (isWingVO)
		{
			WingDbfRecord record = GameDbf.Wing.GetRecord((int)wingId);
			num = ((record != null) ? (GameUtils.GetSortedWingUnlockIndex(record) + 1) : 0);
		}
		return list.Count > num && (list[num] & (long)DungeonCrawlSubDef_VOLines.GetGSDFlagFromHeroCardDbID(adventureId, eventData.m_HeroCardID)) != 0L;
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x000211F8 File Offset: 0x0001F3F8
	private bool IsEventDataValid(AdventureDbId adventureId, WingDbId wingId, int heroDbId, DungeonCrawlSubDef_VOLines.VOEventData eventData)
	{
		AdventureModeDbId modeBasedOnCurrentScene = DungeonCrawlSubDef_VOLines.GetModeBasedOnCurrentScene();
		if (eventData == null)
		{
			return false;
		}
		if (DungeonCrawlSubDef_VOLines.HasEventDataBeenSeen(adventureId, wingId, eventData, this.m_isWingVO))
		{
			return false;
		}
		if (eventData.m_MinimumRequiredBossesDefeated > 0)
		{
			AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)modeBasedOnCurrentScene);
			if (adventureDataRecord.GameSaveDataServerKey == 0)
			{
				Debug.LogWarningFormat("DungeonCrawlSubDef_VOLines - Event {0} has MinimumRequiredBossesDefeated set, but Adventure {1} Wing {2} has no GameSaveDataServerKey!", new object[]
				{
					eventData.m_EventType,
					adventureId,
					wingId
				});
				return false;
			}
			GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
			List<long> list;
			GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSSES_DEFEATED, out list);
			int num = (list != null) ? list.Count : 0;
			if (!DungeonCrawlUtil.IsDungeonRunActive(gameSaveDataServerKey) || num < eventData.m_MinimumRequiredBossesDefeated)
			{
				return false;
			}
		}
		if (!this.m_isWingVO && !this.IsEventPartOfTutorial(eventData.m_EventType, heroDbId) && !this.IsVOEventTutorialComplete(adventureId))
		{
			return false;
		}
		bool flag = eventData.m_MultiQuoteVO.Count == 0 && eventData.m_RandomQuoteVO.Count == 0;
		return (!string.IsNullOrEmpty(eventData.m_QuotePrefab) || !string.IsNullOrEmpty(this.m_DefaultQuotePrefab) || !flag) && (!string.IsNullOrEmpty(eventData.m_QuoteVOSoundPrefab) || !flag);
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x0002132A File Offset: 0x0001F52A
	private bool IsEventPartOfTutorial(DungeonCrawlSubDef_VOLines.VOEventType eventType, int heroDbId)
	{
		return heroDbId <= 0 && !this.m_isWingVO && this.m_TutorialEventTypes.Contains(eventType);
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x00021350 File Offset: 0x0001F550
	private bool IsVOEventTutorialComplete(AdventureDbId adventureId)
	{
		if (this.m_isWingVO)
		{
			return true;
		}
		int heroDbId = 0;
		foreach (DungeonCrawlSubDef_VOLines.VOEventType voeventType in this.m_TutorialEventTypes)
		{
			if (!this.m_VOTutorialEventRefIdMap.ContainsKey(voeventType))
			{
				Debug.LogWarningFormat("DungeonCrawlSubDef_VOLines.IsVOEventTutorialComplete - TutorialEventType ({0}) in Adventure ({1}) was not found in the Ref ID map in {3}. Ensure that this event does not require a specific hero since hero specific tutorial events are not supported. Ignoring...", new object[]
				{
					voeventType,
					adventureId,
					base.gameObject.name
				});
			}
			else
			{
				foreach (int referenceID in this.m_VOTutorialEventRefIdMap[voeventType])
				{
					DungeonCrawlSubDef_VOLines.VOEventData voeventData = this.GetVOEventData(voeventType, heroDbId, referenceID);
					if (!DungeonCrawlSubDef_VOLines.HasEventDataBeenSeen(adventureId, WingDbId.INVALID, voeventData, this.m_isWingVO))
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x00021458 File Offset: 0x0001F658
	public static bool PlayVOLine(AdventureDbId adventureId, WingDbId wingId, int heroDbId, DungeonCrawlSubDef_VOLines.VOEventType voEvent, int referenceID = 0, bool allowRepeatDuringSession = true)
	{
		return DungeonCrawlSubDef_VOLines.PlayVOLine(adventureId, wingId, heroDbId, new DungeonCrawlSubDef_VOLines.VOEventType[]
		{
			voEvent
		}, referenceID, allowRepeatDuringSession);
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x00021470 File Offset: 0x0001F670
	public static bool PlayVOLine(AdventureDbId adventureId, WingDbId wingId, int heroDbId, DungeonCrawlSubDef_VOLines.VOEventType[] voEvents, int referenceID = 0, bool allowRepeatDuringSession = true)
	{
		AdventureModeDbId modeBasedOnCurrentScene = DungeonCrawlSubDef_VOLines.GetModeBasedOnCurrentScene();
		DungeonCrawlSubDef_VOLines.VOData nextValidVOData = DungeonCrawlSubDef_VOLines.GetNextValidVOData(adventureId, wingId, heroDbId, voEvents, referenceID);
		DungeonCrawlSubDef_VOLines volines = nextValidVOData.m_VOLines;
		DungeonCrawlSubDef_VOLines.VOEventData eventData = nextValidVOData.m_EventData;
		if (volines == null)
		{
			Debug.LogErrorFormat("No DungeonCrawlSubDef_VOLines Component found on AdventureDbId {0}'s AdventureSubDef or on WingDbId {1}'s AdventureWingSubDef!", new object[]
			{
				adventureId,
				wingId
			});
			return false;
		}
		if (eventData == null)
		{
			return false;
		}
		if (!DungeonCrawlSubDef_VOLines.EventConstraintsMet(eventData))
		{
			return false;
		}
		float num = UnityEngine.Random.Range(0f, 1f);
		float num2 = eventData.m_ChanceToPlay;
		if (num2 < 0f)
		{
			num2 = volines.m_DefaultChanceToPlay;
		}
		if (num2 < 1f && Cheats.VOChanceOverride >= 0f && HearthstoneApplication.IsInternal())
		{
			num2 = Cheats.VOChanceOverride;
		}
		if (num > num2)
		{
			return false;
		}
		string text = string.IsNullOrEmpty(eventData.m_QuotePrefab) ? volines.m_DefaultQuotePrefab : eventData.m_QuotePrefab;
		if (eventData.m_MultiQuoteVO.Count > 0 && !string.IsNullOrEmpty(eventData.m_QuoteVOSoundPrefab))
		{
			Debug.LogErrorFormat("Playing a quote for eventType {0} and have both MultiQuotes and a VO Sound prefab.  Playing MultiQuotes", new object[]
			{
				eventData.m_EventType
			});
		}
		else if (eventData.m_RandomQuoteVO.Count > 0 && !string.IsNullOrEmpty(eventData.m_QuoteVOSoundPrefab))
		{
			Debug.LogErrorFormat("Playing a quote for eventType {0} and have both RandomQuotes and a VO Sound prefab.  Playing RandomQuotes", new object[]
			{
				eventData.m_EventType
			});
		}
		if (eventData.m_MultiQuoteVO.Count > 0)
		{
			DungeonCrawlSubDef_VOLines.PlayMultiLines(0, eventData.m_MultiQuoteVO.ToArray(), text, eventData.m_QuotePosition, eventData.m_BlockAllOtherInput, allowRepeatDuringSession);
		}
		else if (eventData.m_RandomQuoteVO.Count > 0)
		{
			DungeonCrawlSubDef_VOLines.PlayRandomLine(eventData.m_RandomQuoteVO.ToArray(), text, eventData.m_QuotePosition, eventData.m_BlockAllOtherInput);
		}
		else
		{
			string legacyAssetName = new AssetReference(eventData.m_QuoteVOSoundPrefab).GetLegacyAssetName();
			NotificationManager.Get().CreateCharacterQuote(text, eventData.m_QuotePosition, GameStrings.Get(legacyAssetName), eventData.m_QuoteVOSoundPrefab, allowRepeatDuringSession, 0f, null, CanvasAnchor.BOTTOM_LEFT, eventData.m_BlockAllOtherInput);
		}
		GameSaveKeyId gameSaveDataClientKey = (GameSaveKeyId)GameUtils.GetAdventureDataRecord((int)adventureId, (int)modeBasedOnCurrentScene).GameSaveDataClientKey;
		GameSaveKeySubkeyId subkeyFromHasSeenOption = DungeonCrawlSubDef_VOLines.GetSubkeyFromHasSeenOption(eventData.m_EventSeenOption);
		if (subkeyFromHasSeenOption != GameSaveKeySubkeyId.INVALID)
		{
			List<long> list = null;
			if (!GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataClientKey, subkeyFromHasSeenOption, out list))
			{
				list = new List<long>
				{
					0L
				};
			}
			long num3 = (long)DungeonCrawlSubDef_VOLines.GetGSDFlagFromHeroCardDbID(adventureId, eventData.m_HeroCardID);
			int num4 = 0;
			if (volines.m_isWingVO)
			{
				WingDbfRecord record = GameDbf.Wing.GetRecord((int)wingId);
				int numWingsInAdventure = GameUtils.GetNumWingsInAdventure(adventureId);
				num4 = ((record != null) ? (GameUtils.GetSortedWingUnlockIndex(record) + 1) : 0);
				if (list.Count < numWingsInAdventure)
				{
					list.AddRange(Enumerable.Repeat<long>(0L, numWingsInAdventure + 1 - list.Count));
				}
			}
			List<long> list2 = list;
			int index = num4;
			list2[index] |= num3;
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(gameSaveDataClientKey, subkeyFromHasSeenOption, list.ToArray()), null);
		}
		return true;
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x0002173C File Offset: 0x0001F93C
	private static void PlayMultiLines(int index, DungeonCrawlSubDef_VOLines.CharacterQuoteVOObject[] lines, string prefab, Vector3 quotePosition, bool blockAllOtherInput, bool allowRepeatDuringSession)
	{
		Action<int> action = null;
		if (index < lines.Length - 1)
		{
			action = delegate(int groupId)
			{
				DungeonCrawlSubDef_VOLines.PlayMultiLines(index + 1, lines, prefab, quotePosition, blockAllOtherInput, allowRepeatDuringSession);
			};
		}
		string legacyAssetName = new AssetReference(lines[index].SoundPrefab).GetLegacyAssetName();
		string text = (!string.IsNullOrEmpty(lines[index].CharacterPrefab)) ? lines[index].CharacterPrefab : prefab;
		NotificationManager notificationManager = NotificationManager.Get();
		string prefabPath = text;
		Vector3 quotePosition2 = quotePosition;
		string text2 = GameStrings.Get(legacyAssetName);
		string soundPrefab = lines[index].SoundPrefab;
		Action<int> finishCallback = action;
		notificationManager.CreateCharacterQuote(prefabPath, quotePosition2, text2, soundPrefab, allowRepeatDuringSession, 0f, finishCallback, CanvasAnchor.BOTTOM_LEFT, blockAllOtherInput);
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x00021830 File Offset: 0x0001FA30
	private static void PlayRandomLine(DungeonCrawlSubDef_VOLines.CharacterQuoteVOObject[] lines, string prefab, Vector3 quotePosition, bool blockAllOtherInput)
	{
		int num = UnityEngine.Random.Range(0, lines.Length);
		string legacyAssetName = new AssetReference(lines[num].SoundPrefab).GetLegacyAssetName();
		string prefabPath = (!string.IsNullOrEmpty(lines[num].CharacterPrefab)) ? lines[num].CharacterPrefab : prefab;
		NotificationManager.Get().CreateCharacterQuote(prefabPath, quotePosition, GameStrings.Get(legacyAssetName), lines[num].SoundPrefab, true, 0f, null, CanvasAnchor.BOTTOM_LEFT, blockAllOtherInput);
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x0002189C File Offset: 0x0001FA9C
	public static DungeonCrawlSubDef_VOLines.VOEventType GetNextValidEventType(AdventureDbId adventureId, WingDbId wingId, int heroDbId, DungeonCrawlSubDef_VOLines.VOEventType[] events, int referenceID = 0)
	{
		DungeonCrawlSubDef_VOLines.VOData nextValidVOData = DungeonCrawlSubDef_VOLines.GetNextValidVOData(adventureId, wingId, heroDbId, events, referenceID);
		if (nextValidVOData.m_EventData != null)
		{
			return nextValidVOData.m_EventData.m_EventType;
		}
		return DungeonCrawlSubDef_VOLines.VOEventType.INVALID;
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x000218CC File Offset: 0x0001FACC
	private static DungeonCrawlSubDef_VOLines.VOData GetNextValidVOData(AdventureDbId adventureId, WingDbId wingId, int heroDbId, DungeonCrawlSubDef_VOLines.VOEventType[] events, int referenceID = 0)
	{
		DungeonCrawlSubDef_VOLines.VOData vodata = new DungeonCrawlSubDef_VOLines.VOData();
		DungeonCrawlSubDef_VOLines adventureWingVOLines = DungeonCrawlSubDef_VOLines.GetAdventureWingVOLines(wingId);
		DungeonCrawlSubDef_VOLines adventureModeVOLines = DungeonCrawlSubDef_VOLines.GetAdventureModeVOLines(adventureId);
		bool flag = adventureModeVOLines == null || adventureModeVOLines.IsVOEventTutorialComplete(adventureId);
		vodata.m_VOLines = adventureWingVOLines;
		if (vodata.m_VOLines != null && flag)
		{
			vodata.m_EventData = vodata.m_VOLines.GetNextValidEventData(adventureId, wingId, heroDbId, events, referenceID);
			if (vodata.m_EventData != null && vodata.m_EventData.m_EventType != DungeonCrawlSubDef_VOLines.VOEventType.INVALID)
			{
				return vodata;
			}
		}
		vodata.m_VOLines = adventureModeVOLines;
		if (vodata.m_VOLines != null)
		{
			vodata.m_EventData = vodata.m_VOLines.GetNextValidEventData(adventureId, wingId, heroDbId, events, referenceID);
			if (vodata.m_EventData != null)
			{
				DungeonCrawlSubDef_VOLines.VOEventType eventType = vodata.m_EventData.m_EventType;
				return vodata;
			}
		}
		return vodata;
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x0002198C File Offset: 0x0001FB8C
	private DungeonCrawlSubDef_VOLines.VOEventData GetNextValidEventData(AdventureDbId adventureId, WingDbId wingId, int heroDbId, DungeonCrawlSubDef_VOLines.VOEventType[] events, int referenceID = 0)
	{
		List<int> list = new List<int>
		{
			heroDbId,
			0
		};
		foreach (DungeonCrawlSubDef_VOLines.VOEventType voEventType in events)
		{
			foreach (int heroDbId2 in list)
			{
				DungeonCrawlSubDef_VOLines.VOEventData voeventData = this.GetVOEventData(voEventType, heroDbId2, referenceID);
				if (voeventData == null)
				{
					voeventData = this.GetVOEventData(voEventType, heroDbId2, 0);
				}
				if (this.IsEventDataValid(adventureId, wingId, heroDbId2, voeventData))
				{
					return voeventData;
				}
			}
		}
		return null;
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x00021A34 File Offset: 0x0001FC34
	private static GameSaveKeySubkeyId GetSubkeyFromHasSeenOption(DungeonCrawlSubDef_VOLines.HasSeenDataGameSaveSubkey hasSeenSubkey)
	{
		if (!Enum.IsDefined(typeof(DungeonCrawlSubDef_VOLines.HasSeenDataGameSaveSubkey), hasSeenSubkey))
		{
			Debug.LogErrorFormat("HasSeenDataGameSaveSubkey {0} is not a valid value!", new object[]
			{
				hasSeenSubkey
			});
			return GameSaveKeySubkeyId.INVALID;
		}
		string text = hasSeenSubkey.ToString();
		object obj = Enum.Parse(typeof(GameSaveKeySubkeyId), text, true);
		if (obj == null)
		{
			Debug.LogError("Unable to parse subkey from Dungeon Crawl HasSeenDataGameSaveSubkey: " + text);
			return GameSaveKeySubkeyId.INVALID;
		}
		return (GameSaveKeySubkeyId)obj;
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x00021AB0 File Offset: 0x0001FCB0
	private static int GetGSDFlagFromHeroCardDbID(AdventureDbId adventureId, int heroDbId)
	{
		DungeonCrawlSubDef_VOLines adventureModeVOLines = DungeonCrawlSubDef_VOLines.GetAdventureModeVOLines(adventureId);
		if (adventureModeVOLines == null)
		{
			Debug.LogErrorFormat("GetGSDFlagFromHeroCardDbID - unable to get the VO Lines component from the Adventure ({0}) sub def.", new object[]
			{
				adventureId
			});
			return -1;
		}
		List<int> sortedHeroDbIds = adventureModeVOLines.m_sortedHeroDbIds;
		if (sortedHeroDbIds.Count <= 0)
		{
			List<AdventureGuestHeroesDbfRecord> records = GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)adventureId, -1);
			records.Sort((AdventureGuestHeroesDbfRecord a, AdventureGuestHeroesDbfRecord b) => a.ID - b.ID);
			foreach (AdventureGuestHeroesDbfRecord adventureGuestHeroesDbfRecord in records)
			{
				GuestHeroDbfRecord record = GameDbf.GuestHero.GetRecord(adventureGuestHeroesDbfRecord.GuestHeroId);
				if (record != null)
				{
					sortedHeroDbIds.Add(record.CardId);
				}
			}
		}
		return 1 << sortedHeroDbIds.IndexOf(heroDbId) + 1;
	}

	// Token: 0x060005C3 RID: 1475 RVA: 0x00021BBC File Offset: 0x0001FDBC
	private static bool EventConstraintsMet(DungeonCrawlSubDef_VOLines.VOEventData eventData)
	{
		if (eventData.m_QuoteConstraints.Count == 0)
		{
			return true;
		}
		foreach (DungeonCrawlSubDef_VOLines.VOConstraintObject voconstraintObject in eventData.m_QuoteConstraints)
		{
			DungeonCrawlSubDef_VOLines.VOConstraintObject.ConstraintType constraint = voconstraintObject.Constraint;
			if (constraint > DungeonCrawlSubDef_VOLines.VOConstraintObject.ConstraintType.WingIsCompleted)
			{
				Debug.LogWarningFormat("DungeonCrawlSubDef_VOLines.EventConstraintsMet did not have a case to handle the passed constraint of type {0}.", new object[]
				{
					voconstraintObject.Constraint.ToString()
				});
			}
			else if (!DungeonCrawlSubDef_VOLines.EvaluateWingConstraint(voconstraintObject))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x00021C58 File Offset: 0x0001FE58
	private static bool EvaluateWingConstraint(DungeonCrawlSubDef_VOLines.VOConstraintObject quoteConstraint)
	{
		WingDbfRecord record = GameDbf.Wing.GetRecord(quoteConstraint.Value);
		if (record == null)
		{
			Debug.LogWarningFormat("DungeonCrawlSubDef_VOLines.EvaluateWingConstraint was called with invalid wing ID: {0}.", new object[]
			{
				quoteConstraint.Value
			});
			return false;
		}
		AdventureChapterState adventureChapterState = AdventureProgressMgr.Get().AdventureBookChapterStateForWing(record, AdventureConfig.Get().GetSelectedMode());
		switch (quoteConstraint.Constraint)
		{
		case DungeonCrawlSubDef_VOLines.VOConstraintObject.ConstraintType.WingIsUnlocked:
			return adventureChapterState == AdventureChapterState.UNLOCKED;
		case DungeonCrawlSubDef_VOLines.VOConstraintObject.ConstraintType.WingIsLocked:
			return adventureChapterState == AdventureChapterState.LOCKED;
		case DungeonCrawlSubDef_VOLines.VOConstraintObject.ConstraintType.WingIsCompleted:
			return adventureChapterState == AdventureChapterState.COMPLETED;
		default:
			Debug.LogWarningFormat("DungeonCrawlSubDef_VOLines.EvaluateWingConstraint was called with unsupported Constraint Type: {0}.", new object[]
			{
				quoteConstraint.Constraint.ToString()
			});
			return false;
		}
	}

	// Token: 0x04000408 RID: 1032
	[CustomEditField(Sections = "Defaults", T = EditType.GAME_OBJECT)]
	public string m_DefaultQuotePrefab;

	// Token: 0x04000409 RID: 1033
	[CustomEditField(Sections = "Defaults")]
	public float m_DefaultChanceToPlay = 1f;

	// Token: 0x0400040A RID: 1034
	public List<DungeonCrawlSubDef_VOLines.VOEventType> m_TutorialEventTypes;

	// Token: 0x0400040B RID: 1035
	public List<DungeonCrawlSubDef_VOLines.VOEventData> m_VOEventDataList = new List<DungeonCrawlSubDef_VOLines.VOEventData>();

	// Token: 0x0400040C RID: 1036
	public static readonly DungeonCrawlSubDef_VOLines.VOEventType[] BOSS_REVEAL_EVENTS = new DungeonCrawlSubDef_VOLines.VOEventType[]
	{
		DungeonCrawlSubDef_VOLines.VOEventType.BOSS_REVEAL_1,
		DungeonCrawlSubDef_VOLines.VOEventType.BOSS_REVEAL_2,
		DungeonCrawlSubDef_VOLines.VOEventType.BOSS_REVEAL_3,
		DungeonCrawlSubDef_VOLines.VOEventType.BOSS_REVEAL_4,
		DungeonCrawlSubDef_VOLines.VOEventType.BOSS_REVEAL_5,
		DungeonCrawlSubDef_VOLines.VOEventType.BOSS_REVEAL_GENERAL
	};

	// Token: 0x0400040D RID: 1037
	public static readonly DungeonCrawlSubDef_VOLines.VOEventType[] FINAL_BOSS_LOSS_EVENTS = new DungeonCrawlSubDef_VOLines.VOEventType[]
	{
		DungeonCrawlSubDef_VOLines.VOEventType.FINAL_BOSS_LOSS_1,
		DungeonCrawlSubDef_VOLines.VOEventType.FINAL_BOSS_LOSS_2,
		DungeonCrawlSubDef_VOLines.VOEventType.FINAL_BOSS_LOSS_GENERAL
	};

	// Token: 0x0400040E RID: 1038
	public static readonly DungeonCrawlSubDef_VOLines.VOEventType[] OFFER_TREASURE_EVENTS = new DungeonCrawlSubDef_VOLines.VOEventType[]
	{
		DungeonCrawlSubDef_VOLines.VOEventType.OFFER_TREASURE_1,
		DungeonCrawlSubDef_VOLines.VOEventType.OFFER_TREASURE_2,
		DungeonCrawlSubDef_VOLines.VOEventType.OFFER_TREASURE_3,
		DungeonCrawlSubDef_VOLines.VOEventType.OFFER_TREASURE_4,
		DungeonCrawlSubDef_VOLines.VOEventType.OFFER_TREASURE_GENERAL
	};

	// Token: 0x0400040F RID: 1039
	public static readonly DungeonCrawlSubDef_VOLines.VOEventType[] OFFER_LOOT_PACKS_EVENTS = new DungeonCrawlSubDef_VOLines.VOEventType[]
	{
		DungeonCrawlSubDef_VOLines.VOEventType.OFFER_LOOT_PACKS_1,
		DungeonCrawlSubDef_VOLines.VOEventType.OFFER_LOOT_PACKS_2
	};

	// Token: 0x04000410 RID: 1040
	public static readonly DungeonCrawlSubDef_VOLines.VOEventType[] OFFER_HERO_POWER_EVENTS = new DungeonCrawlSubDef_VOLines.VOEventType[]
	{
		DungeonCrawlSubDef_VOLines.VOEventType.OFFER_HERO_POWER_1
	};

	// Token: 0x04000411 RID: 1041
	public static readonly DungeonCrawlSubDef_VOLines.VOEventType[] OFFER_DECK_EVENTS = new DungeonCrawlSubDef_VOLines.VOEventType[]
	{
		DungeonCrawlSubDef_VOLines.VOEventType.OFFER_DECK_1
	};

	// Token: 0x04000412 RID: 1042
	public static readonly DungeonCrawlSubDef_VOLines.VOEventType[] WING_COMPLETE_EVENTS = new DungeonCrawlSubDef_VOLines.VOEventType[]
	{
		DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_FIRST_WING,
		DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_SECOND_WING,
		DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_THIRD_WING,
		DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_FOURTH_WING,
		DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_FIFTH_WING
	};

	// Token: 0x04000413 RID: 1043
	public static readonly DungeonCrawlSubDef_VOLines.VOEventType[] CLASS_COMPLETE_EVENTS = new DungeonCrawlSubDef_VOLines.VOEventType[]
	{
		DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_FIRST_CLASS,
		DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_SECOND_CLASS,
		DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_THIRD_CLASS
	};

	// Token: 0x04000414 RID: 1044
	private List<int> m_sortedHeroDbIds = new List<int>();

	// Token: 0x04000415 RID: 1045
	private bool m_isWingVO;

	// Token: 0x04000416 RID: 1046
	private Map<DungeonCrawlSubDef_VOLines.VOEventType, Map<int, Map<int, DungeonCrawlSubDef_VOLines.VOEventData>>> m_VOEventDataMap = new Map<DungeonCrawlSubDef_VOLines.VOEventType, Map<int, Map<int, DungeonCrawlSubDef_VOLines.VOEventData>>>();

	// Token: 0x04000417 RID: 1047
	private Map<DungeonCrawlSubDef_VOLines.VOEventType, List<int>> m_VOTutorialEventRefIdMap = new Map<DungeonCrawlSubDef_VOLines.VOEventType, List<int>>();

	// Token: 0x02001358 RID: 4952
	public enum VOEventType
	{
		// Token: 0x0400A61C RID: 42524
		INVALID,
		// Token: 0x0400A61D RID: 42525
		CHARACTER_SELECT,
		// Token: 0x0400A61E RID: 42526
		BOSS_REVEAL_1,
		// Token: 0x0400A61F RID: 42527
		BOSS_REVEAL_2,
		// Token: 0x0400A620 RID: 42528
		BOSS_REVEAL_3,
		// Token: 0x0400A621 RID: 42529
		BOSS_REVEAL_GENERAL,
		// Token: 0x0400A622 RID: 42530
		OFFER_TREASURE_1,
		// Token: 0x0400A623 RID: 42531
		OFFER_TREASURE_GENERAL,
		// Token: 0x0400A624 RID: 42532
		TAKE_TREASURE_GENERAL,
		// Token: 0x0400A625 RID: 42533
		OFFER_LOOT_PACKS_1,
		// Token: 0x0400A626 RID: 42534
		OFFER_LOOT_PACKS_2,
		// Token: 0x0400A627 RID: 42535
		WELCOME_BANNER,
		// Token: 0x0400A628 RID: 42536
		COMPLETE_ALL_CLASSES_FIRST_TIME,
		// Token: 0x0400A629 RID: 42537
		COMPLETE_ALL_CLASSES,
		// Token: 0x0400A62A RID: 42538
		COMPLETE_FIRST_CLASS,
		// Token: 0x0400A62B RID: 42539
		COMPLETE_SECOND_CLASS,
		// Token: 0x0400A62C RID: 42540
		COMPLETE_THIRD_CLASS,
		// Token: 0x0400A62D RID: 42541
		OFFER_TREASURE_2,
		// Token: 0x0400A62E RID: 42542
		OFFER_TREASURE_3,
		// Token: 0x0400A62F RID: 42543
		OFFER_TREASURE_4,
		// Token: 0x0400A630 RID: 42544
		OFFER_HERO_POWER_1,
		// Token: 0x0400A631 RID: 42545
		OFFER_DECK_1,
		// Token: 0x0400A632 RID: 42546
		BOSS_REVEAL_4,
		// Token: 0x0400A633 RID: 42547
		BOSS_REVEAL_5,
		// Token: 0x0400A634 RID: 42548
		BOOK_REVEAL,
		// Token: 0x0400A635 RID: 42549
		BOOK_REVEAL_HEROIC,
		// Token: 0x0400A636 RID: 42550
		WING_UNLOCK,
		// Token: 0x0400A637 RID: 42551
		COMPLETE_ALL_WINGS,
		// Token: 0x0400A638 RID: 42552
		COMPLETE_ALL_WINGS_HEROIC,
		// Token: 0x0400A639 RID: 42553
		ANOMALY_UNLOCK,
		// Token: 0x0400A63A RID: 42554
		REWARD_PAGE_REVEAL,
		// Token: 0x0400A63B RID: 42555
		FINAL_BOSS_REVEAL,
		// Token: 0x0400A63C RID: 42556
		FINAL_BOSS_LOSS_1,
		// Token: 0x0400A63D RID: 42557
		FINAL_BOSS_LOSS_2,
		// Token: 0x0400A63E RID: 42558
		FINAL_BOSS_LOSS_GENERAL,
		// Token: 0x0400A63F RID: 42559
		COMPLETE_FIRST_WING,
		// Token: 0x0400A640 RID: 42560
		COMPLETE_SECOND_WING,
		// Token: 0x0400A641 RID: 42561
		COMPLETE_THIRD_WING,
		// Token: 0x0400A642 RID: 42562
		COMPLETE_FOURTH_WING,
		// Token: 0x0400A643 RID: 42563
		COMPLETE_FIFTH_WING,
		// Token: 0x0400A644 RID: 42564
		BOSS_LOSS_1,
		// Token: 0x0400A645 RID: 42565
		WING_COMPLETE_GENERAL,
		// Token: 0x0400A646 RID: 42566
		CHAPTER_PAGE,
		// Token: 0x0400A647 RID: 42567
		BOSS_LOSS_1_SECOND_BOOK_SECTION,
		// Token: 0x0400A648 RID: 42568
		COMPLETE_ALL_WINGS_SECOND_BOOK_SECTION,
		// Token: 0x0400A649 RID: 42569
		COMPLETE_ALL_WINGS_SECOND_BOOK_SECTION_HEROIC,
		// Token: 0x0400A64A RID: 42570
		CALL_TO_ACTION
	}

	// Token: 0x02001359 RID: 4953
	public enum HasSeenDataGameSaveSubkey
	{
		// Token: 0x0400A64C RID: 42572
		INVALID,
		// Token: 0x0400A64D RID: 42573
		DUNGEON_CRAWL_HAS_SEEN_CHARACTER_SELECT_VO = 3,
		// Token: 0x0400A64E RID: 42574
		DUNGEON_CRAWL_HAS_SEEN_WELCOME_BANNER_VO,
		// Token: 0x0400A64F RID: 42575
		DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_1_VO,
		// Token: 0x0400A650 RID: 42576
		DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_2_VO,
		// Token: 0x0400A651 RID: 42577
		DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_3_VO,
		// Token: 0x0400A652 RID: 42578
		DUNGEON_CRAWL_HAS_SEEN_OFFER_TREASURE_1_VO,
		// Token: 0x0400A653 RID: 42579
		DUNGEON_CRAWL_HAS_SEEN_OFFER_LOOT_PACKS_1_VO,
		// Token: 0x0400A654 RID: 42580
		DUNGEON_CRAWL_HAS_SEEN_OFFER_LOOT_PACKS_2_VO,
		// Token: 0x0400A655 RID: 42581
		DUNGEON_CRAWL_HAS_SEEN_IN_GAME_WIN_VO = 12,
		// Token: 0x0400A656 RID: 42582
		DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_VO,
		// Token: 0x0400A657 RID: 42583
		DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_1_VO,
		// Token: 0x0400A658 RID: 42584
		DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_2_VO,
		// Token: 0x0400A659 RID: 42585
		DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_2_VO = 18,
		// Token: 0x0400A65A RID: 42586
		DUNGEON_CRAWL_HAS_SEEN_OFFER_TREASURE_2_VO,
		// Token: 0x0400A65B RID: 42587
		DUNGEON_CRAWL_HAS_SEEN_OFFER_TREASURE_3_VO,
		// Token: 0x0400A65C RID: 42588
		DUNGEON_CRAWL_HAS_SEEN_OFFER_TREASURE_4_VO,
		// Token: 0x0400A65D RID: 42589
		DUNGEON_CRAWL_HAS_SEEN_OFFER_HERO_POWER_1_VO,
		// Token: 0x0400A65E RID: 42590
		DUNGEON_CRAWL_HAS_SEEN_OFFER_DECK_1_VO,
		// Token: 0x0400A65F RID: 42591
		DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_4_VO,
		// Token: 0x0400A660 RID: 42592
		DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_5_VO,
		// Token: 0x0400A661 RID: 42593
		DUNGEON_CRAWL_HAS_SEEN_BOOK_REVEAL_VO,
		// Token: 0x0400A662 RID: 42594
		DUNGEON_CRAWL_HAS_SEEN_BOOK_REVEAL_HEROIC_VO,
		// Token: 0x0400A663 RID: 42595
		DUNGEON_CRAWL_HAS_SEEN_WING_UNLOCK_VO,
		// Token: 0x0400A664 RID: 42596
		DUNGEON_CRAWL_HAS_SEEN_COMPLETE_ALL_WINGS_VO,
		// Token: 0x0400A665 RID: 42597
		DUNGEON_CRAWL_HAS_SEEN_COMPLETE_ALL_WINGS_HEROIC_VO,
		// Token: 0x0400A666 RID: 42598
		DUNGEON_CRAWL_HAS_SEEN_ANOMALY_UNLOCK_VO,
		// Token: 0x0400A667 RID: 42599
		DUNGEON_CRAWL_HAS_SEEN_REWARD_PAGE_REVEAL_VO,
		// Token: 0x0400A668 RID: 42600
		DUNGEON_CRAWL_HAS_SEEN_FINAL_BOSS_LOSS_1_VO,
		// Token: 0x0400A669 RID: 42601
		DUNGEON_CRAWL_HAS_SEEN_FINAL_BOSS_LOSS_2_VO,
		// Token: 0x0400A66A RID: 42602
		DUNGEON_CRAWL_HAS_SEEN_FINAL_BOSS_REVEAL_1_VO,
		// Token: 0x0400A66B RID: 42603
		DUNGEON_CRAWL_HAS_SEEN_BOSS_LOSS_1_VO,
		// Token: 0x0400A66C RID: 42604
		DUNGEON_CRAWL_HAS_SEEN_CHAPTER_PAGE_VO,
		// Token: 0x0400A66D RID: 42605
		DUNGEON_CRAWL_HAS_SEEN_BOSS_LOSS_1_SECOND_BOOK_SECTION_VO,
		// Token: 0x0400A66E RID: 42606
		DUNGEON_CRAWL_HAS_SEEN_COMPLETE_ALL_WINGS_SECOND_BOOK_SECTION_VO,
		// Token: 0x0400A66F RID: 42607
		DUNGEON_CRAWL_HAS_SEEN_COMPLETE_ALL_WINGS_SECOND_BOOK_SECTION_HEROIC_VO,
		// Token: 0x0400A670 RID: 42608
		DUNGEON_CRAWL_HAS_SEEN_CALL_TO_ACTION_VO
	}

	// Token: 0x0200135A RID: 4954
	[Serializable]
	public class VOEventData
	{
		// Token: 0x0400A671 RID: 42609
		public DungeonCrawlSubDef_VOLines.VOEventType m_EventType;

		// Token: 0x0400A672 RID: 42610
		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string m_QuotePrefab;

		// Token: 0x0400A673 RID: 42611
		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string m_QuoteVOSoundPrefab;

		// Token: 0x0400A674 RID: 42612
		public DungeonCrawlSubDef_VOLines.HasSeenDataGameSaveSubkey m_EventSeenOption;

		// Token: 0x0400A675 RID: 42613
		public int m_AssociatedCardID;

		// Token: 0x0400A676 RID: 42614
		public int m_HeroCardID;

		// Token: 0x0400A677 RID: 42615
		public float m_ChanceToPlay = -1f;

		// Token: 0x0400A678 RID: 42616
		public int m_MinimumRequiredBossesDefeated;

		// Token: 0x0400A679 RID: 42617
		public bool m_BlockAllOtherInput;

		// Token: 0x0400A67A RID: 42618
		public Vector3 m_QuotePosition = NotificationManager.DEFAULT_CHARACTER_POS;

		// Token: 0x0400A67B RID: 42619
		[CustomEditField(T = EditType.GAME_OBJECT)]
		public List<DungeonCrawlSubDef_VOLines.CharacterQuoteVOObject> m_MultiQuoteVO;

		// Token: 0x0400A67C RID: 42620
		[CustomEditField(T = EditType.GAME_OBJECT)]
		public List<DungeonCrawlSubDef_VOLines.CharacterQuoteVOObject> m_RandomQuoteVO;

		// Token: 0x0400A67D RID: 42621
		[CustomEditField(T = EditType.GAME_OBJECT)]
		public List<DungeonCrawlSubDef_VOLines.VOConstraintObject> m_QuoteConstraints;
	}

	// Token: 0x0200135B RID: 4955
	[Serializable]
	public class CharacterQuoteVOObject
	{
		// Token: 0x0400A67E RID: 42622
		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string SoundPrefab;

		// Token: 0x0400A67F RID: 42623
		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string CharacterPrefab;
	}

	// Token: 0x0200135C RID: 4956
	[Serializable]
	public class VOConstraintObject
	{
		// Token: 0x0400A680 RID: 42624
		public DungeonCrawlSubDef_VOLines.VOConstraintObject.ConstraintType Constraint;

		// Token: 0x0400A681 RID: 42625
		public int Value;

		// Token: 0x02002976 RID: 10614
		public enum ConstraintType
		{
			// Token: 0x0400FCD6 RID: 64726
			WingIsUnlocked,
			// Token: 0x0400FCD7 RID: 64727
			WingIsLocked,
			// Token: 0x0400FCD8 RID: 64728
			WingIsCompleted
		}
	}

	// Token: 0x0200135D RID: 4957
	private class VOData
	{
		// Token: 0x0400A682 RID: 42626
		public DungeonCrawlSubDef_VOLines m_VOLines;

		// Token: 0x0400A683 RID: 42627
		public DungeonCrawlSubDef_VOLines.VOEventData m_EventData;
	}
}
