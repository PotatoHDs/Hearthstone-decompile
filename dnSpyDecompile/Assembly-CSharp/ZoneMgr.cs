using System;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

// Token: 0x02000362 RID: 866
public class ZoneMgr : MonoBehaviour
{
	// Token: 0x0600327E RID: 12926 RVA: 0x001015C0 File Offset: 0x000FF7C0
	private void Awake()
	{
		ZoneMgr.s_instance = this;
		foreach (Zone item in base.gameObject.GetComponentsInChildren<Zone>())
		{
			this.m_zones.Add(item);
		}
		if (GameState.Get() != null)
		{
			GameState.Get().RegisterCurrentPlayerChangedListener(new GameState.CurrentPlayerChangedCallback(this.OnCurrentPlayerChanged));
			GameState.Get().RegisterOptionRejectedListener(new GameState.OptionRejectedCallback(this.OnOptionRejected), null);
		}
	}

	// Token: 0x0600327F RID: 12927 RVA: 0x00101634 File Offset: 0x000FF834
	private void Start()
	{
		InputManager inputManager = InputManager.Get();
		if (inputManager != null)
		{
			inputManager.StartWatchingForInput();
		}
	}

	// Token: 0x06003280 RID: 12928 RVA: 0x00101656 File Offset: 0x000FF856
	private void Update()
	{
		this.UpdateLocalChangeLists();
		this.UpdateServerChangeLists();
	}

	// Token: 0x06003281 RID: 12929 RVA: 0x00101664 File Offset: 0x000FF864
	private void OnDestroy()
	{
		if (GameState.Get() != null)
		{
			GameState.Get().UnregisterCurrentPlayerChangedListener(new GameState.CurrentPlayerChangedCallback(this.OnCurrentPlayerChanged));
			GameState.Get().UnregisterOptionRejectedListener(new GameState.OptionRejectedCallback(this.OnOptionRejected), null);
		}
		ZoneMgr.s_instance = null;
	}

	// Token: 0x06003282 RID: 12930 RVA: 0x001016A2 File Offset: 0x000FF8A2
	public static ZoneMgr Get()
	{
		return ZoneMgr.s_instance;
	}

	// Token: 0x06003283 RID: 12931 RVA: 0x001016A9 File Offset: 0x000FF8A9
	public List<Zone> GetZones()
	{
		return this.m_zones;
	}

	// Token: 0x06003284 RID: 12932 RVA: 0x001016B4 File Offset: 0x000FF8B4
	public Zone FindZoneForTags(int controllerId, TAG_ZONE zoneTag, TAG_CARDTYPE cardType, global::Entity entity)
	{
		if (controllerId == 0)
		{
			return null;
		}
		if (zoneTag == TAG_ZONE.INVALID)
		{
			return null;
		}
		foreach (Zone zone in this.m_zones)
		{
			if (zone.CanAcceptTags(controllerId, zoneTag, cardType, entity))
			{
				return zone;
			}
		}
		return null;
	}

	// Token: 0x06003285 RID: 12933 RVA: 0x00101720 File Offset: 0x000FF920
	public Zone FindZoneForEntity(global::Entity entity)
	{
		if (entity.GetZone() == TAG_ZONE.INVALID)
		{
			return null;
		}
		foreach (Zone zone in this.m_zones)
		{
			if (zone.CanAcceptTags(entity.GetControllerId(), entity.GetZone(), entity.GetCardType(), entity))
			{
				return zone;
			}
		}
		return null;
	}

	// Token: 0x06003286 RID: 12934 RVA: 0x00101798 File Offset: 0x000FF998
	public Zone FindZoneForEntityAndZoneTag(global::Entity entity, TAG_ZONE zoneTag)
	{
		if (zoneTag == TAG_ZONE.INVALID)
		{
			return null;
		}
		foreach (Zone zone in this.m_zones)
		{
			if (zone.CanAcceptTags(entity.GetControllerId(), zoneTag, entity.GetCardType(), entity))
			{
				return zone;
			}
		}
		return null;
	}

	// Token: 0x06003287 RID: 12935 RVA: 0x00101808 File Offset: 0x000FFA08
	public Zone FindZoneForEntityAndController(global::Entity entity, int controllerId)
	{
		foreach (Zone zone in this.m_zones)
		{
			if (zone.CanAcceptTags(controllerId, entity.GetZone(), entity.GetCardType(), entity))
			{
				return zone;
			}
		}
		return null;
	}

	// Token: 0x06003288 RID: 12936 RVA: 0x00101874 File Offset: 0x000FFA74
	public Zone FindZoneForFullEntity(Network.HistFullEntity fullEntity)
	{
		int controllerId = 0;
		TAG_ZONE zoneTag = TAG_ZONE.INVALID;
		TAG_CARDTYPE cardType = TAG_CARDTYPE.INVALID;
		foreach (Network.Entity.Tag tag in fullEntity.Entity.Tags)
		{
			GAME_TAG name = (GAME_TAG)tag.Name;
			if (name != GAME_TAG.ZONE)
			{
				if (name != GAME_TAG.CONTROLLER)
				{
					if (name == GAME_TAG.CARDTYPE)
					{
						cardType = (TAG_CARDTYPE)tag.Value;
					}
				}
				else
				{
					controllerId = tag.Value;
				}
			}
			else
			{
				zoneTag = (TAG_ZONE)tag.Value;
			}
		}
		foreach (Zone zone in this.m_zones)
		{
			if (zone.CanAcceptTags(controllerId, zoneTag, cardType, null))
			{
				return zone;
			}
		}
		return null;
	}

	// Token: 0x06003289 RID: 12937 RVA: 0x0010195C File Offset: 0x000FFB5C
	public Zone FindZoneForShowEntity(global::Entity entity, Network.HistShowEntity showEntity)
	{
		int controllerId = entity.GetControllerId();
		TAG_ZONE zoneTag = entity.GetZone();
		TAG_CARDTYPE cardType = entity.GetCardType();
		foreach (Network.Entity.Tag tag in showEntity.Entity.Tags)
		{
			GAME_TAG name = (GAME_TAG)tag.Name;
			if (name != GAME_TAG.ZONE)
			{
				if (name != GAME_TAG.CONTROLLER)
				{
					if (name == GAME_TAG.CARDTYPE)
					{
						cardType = (TAG_CARDTYPE)tag.Value;
					}
				}
				else
				{
					controllerId = tag.Value;
				}
			}
			else
			{
				zoneTag = (TAG_ZONE)tag.Value;
			}
		}
		foreach (Zone zone in this.m_zones)
		{
			if (zone.CanAcceptTags(controllerId, zoneTag, cardType, null))
			{
				return zone;
			}
		}
		return null;
	}

	// Token: 0x0600328A RID: 12938 RVA: 0x00101A54 File Offset: 0x000FFC54
	public T FindZoneOfType<T>(global::Player.Side side) where T : Zone
	{
		Type typeFromHandle = typeof(T);
		foreach (Zone zone in this.m_zones)
		{
			if (!(zone.GetType() != typeFromHandle) && zone.m_Side == side)
			{
				return (T)((object)zone);
			}
		}
		return default(T);
	}

	// Token: 0x0600328B RID: 12939 RVA: 0x00101AD8 File Offset: 0x000FFCD8
	public List<Zone> FindZonesForSide(global::Player.Side playerSide)
	{
		return this.FindZonesOfType<Zone>(playerSide);
	}

	// Token: 0x0600328C RID: 12940 RVA: 0x00101AE1 File Offset: 0x000FFCE1
	public List<T> FindZonesOfType<T>() where T : Zone
	{
		return this.FindZonesOfType<T, T>();
	}

	// Token: 0x0600328D RID: 12941 RVA: 0x00101AEC File Offset: 0x000FFCEC
	public List<ReturnType> FindZonesOfType<ReturnType, ArgType>() where ReturnType : Zone where ArgType : Zone
	{
		List<ReturnType> list = new List<ReturnType>();
		Type typeFromHandle = typeof(ArgType);
		foreach (Zone zone in this.m_zones)
		{
			if (!(zone.GetType() != typeFromHandle))
			{
				list.Add((ReturnType)((object)zone));
			}
		}
		return list;
	}

	// Token: 0x0600328E RID: 12942 RVA: 0x00101B64 File Offset: 0x000FFD64
	public List<T> FindZonesOfType<T>(global::Player.Side side) where T : Zone
	{
		return this.FindZonesOfType<T, T>(side);
	}

	// Token: 0x0600328F RID: 12943 RVA: 0x00101B70 File Offset: 0x000FFD70
	public List<ReturnType> FindZonesOfType<ReturnType, ArgType>(global::Player.Side side) where ReturnType : Zone where ArgType : Zone
	{
		List<ReturnType> list = new List<ReturnType>();
		foreach (Zone zone in this.m_zones)
		{
			if (zone is ArgType && zone.m_Side == side)
			{
				list.Add((ReturnType)((object)zone));
			}
		}
		return list;
	}

	// Token: 0x06003290 RID: 12944 RVA: 0x00101BE0 File Offset: 0x000FFDE0
	public List<Zone> FindZonesForTag(TAG_ZONE zoneTag)
	{
		List<Zone> list = new List<Zone>();
		foreach (Zone zone in this.m_zones)
		{
			if (zone.m_ServerTag == zoneTag)
			{
				list.Add(zone);
			}
		}
		return list;
	}

	// Token: 0x06003291 RID: 12945 RVA: 0x00101C44 File Offset: 0x000FFE44
	public Map<Type, string> GetTweenNames()
	{
		return this.m_tweenNames;
	}

	// Token: 0x06003292 RID: 12946 RVA: 0x00101C4C File Offset: 0x000FFE4C
	public string GetTweenName<T>() where T : Zone
	{
		Type typeFromHandle = typeof(T);
		string result = "";
		this.m_tweenNames.TryGetValue(typeFromHandle, out result);
		return result;
	}

	// Token: 0x06003293 RID: 12947 RVA: 0x00101C7A File Offset: 0x000FFE7A
	public void RequestNextDeathBlockLayoutDelaySec(float sec)
	{
		this.m_nextDeathBlockLayoutDelaySec = Mathf.Max(this.m_nextDeathBlockLayoutDelaySec, sec);
	}

	// Token: 0x06003294 RID: 12948 RVA: 0x00101C8E File Offset: 0x000FFE8E
	public float RemoveNextDeathBlockLayoutDelaySec()
	{
		float nextDeathBlockLayoutDelaySec = this.m_nextDeathBlockLayoutDelaySec;
		this.m_nextDeathBlockLayoutDelaySec = 0f;
		return nextDeathBlockLayoutDelaySec;
	}

	// Token: 0x06003295 RID: 12949 RVA: 0x00101CA4 File Offset: 0x000FFEA4
	public int PredictZonePosition(Zone zone, int pos)
	{
		ZoneMgr.TempZone tempZone = this.BuildTempZone(zone);
		this.PredictZoneFromPowerProcessor(tempZone);
		this.RemoveDraggedMinionsFromTempZone(zone, tempZone);
		int result = this.FindBestInsertionPosition(tempZone, pos - 1, pos);
		this.m_tempZoneMap.Clear();
		this.m_tempEntityMap.Clear();
		return result;
	}

	// Token: 0x06003296 RID: 12950 RVA: 0x00101CEC File Offset: 0x000FFEEC
	private void RemoveDraggedMinionsFromTempZone(Zone originalZone, ZoneMgr.TempZone tempZone)
	{
		foreach (Card card in originalZone.GetCards())
		{
			if (card.IsBeingDragged)
			{
				tempZone.RemoveEntityById(card.GetEntity().GetEntityId());
			}
		}
	}

	// Token: 0x06003297 RID: 12951 RVA: 0x00101D54 File Offset: 0x000FFF54
	public bool HasPredictedCards()
	{
		return this.HasPredictedCards<ZoneSecret>(TAG_ZONE.SECRET) || this.HasPredictedCards<ZoneWeapon>(TAG_ZONE.PLAY) || this.HasPredictedCards<ZoneHero>(TAG_ZONE.PLAY) || this.HasPredictedCards<ZoneGraveyard>(TAG_ZONE.GRAVEYARD);
	}

	// Token: 0x06003298 RID: 12952 RVA: 0x00101D84 File Offset: 0x000FFF84
	public bool HasPredictedMovedMinion()
	{
		foreach (Zone zone in this.FindZonesOfType<Zone>(global::Player.Side.FRIENDLY))
		{
			using (List<Card>.Enumerator enumerator2 = zone.GetCards().GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.m_minionWasMovedFromSrcToDst != null)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06003299 RID: 12953 RVA: 0x00101E18 File Offset: 0x00100018
	public bool HasPredictedPositions()
	{
		foreach (Zone zone in this.FindZonesOfType<Zone>(global::Player.Side.FRIENDLY))
		{
			using (List<Card>.Enumerator enumerator2 = zone.GetCards().GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.GetPredictedZonePosition() != 0)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x0600329A RID: 12954 RVA: 0x00101EAC File Offset: 0x001000AC
	public bool HasPredictedCards<T>(TAG_ZONE predictedZone) where T : Zone
	{
		foreach (T t in this.FindZonesOfType<T>(global::Player.Side.FRIENDLY))
		{
			using (List<Card>.Enumerator enumerator2 = t.GetCards().GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.GetEntity().GetZone() != predictedZone)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x0600329B RID: 12955 RVA: 0x00101F4C File Offset: 0x0010014C
	public bool HasActiveLocalChange()
	{
		return this.m_activeLocalChangeLists.Count > 0;
	}

	// Token: 0x0600329C RID: 12956 RVA: 0x00101F5C File Offset: 0x0010015C
	public bool HasPendingLocalChange()
	{
		return this.m_pendingLocalChangeLists.Count > 0;
	}

	// Token: 0x0600329D RID: 12957 RVA: 0x00101F6C File Offset: 0x0010016C
	public bool HasUnresolvedLocalChange()
	{
		return this.m_localChangeListHistory.Count > 0;
	}

	// Token: 0x0600329E RID: 12958 RVA: 0x00101F7C File Offset: 0x0010017C
	public bool HasTriggeredActiveLocalChange(Card card)
	{
		return this.FindTriggeredActiveLocalChangeIndex(card) >= 0;
	}

	// Token: 0x0600329F RID: 12959 RVA: 0x00101F8C File Offset: 0x0010018C
	public ZoneChangeList AddLocalZoneChange(Card triggerCard, TAG_ZONE zoneTag)
	{
		global::Entity entity = triggerCard.GetEntity();
		Zone destinationZone = this.FindZoneForEntityAndZoneTag(entity, zoneTag);
		return this.AddLocalZoneChange(triggerCard, destinationZone, zoneTag, 0, null, null);
	}

	// Token: 0x060032A0 RID: 12960 RVA: 0x00101FB5 File Offset: 0x001001B5
	public ZoneChangeList AddLocalZoneChange(Card triggerCard, Zone destinationZone, int destinationPos)
	{
		if (destinationZone == null)
		{
			Debug.LogWarning(string.Format("ZoneMgr.AddLocalZoneChange() - illegal zone change to null zone for card {0}", triggerCard));
			return null;
		}
		return this.AddLocalZoneChange(triggerCard, destinationZone, destinationZone.m_ServerTag, destinationPos, null, null);
	}

	// Token: 0x060032A1 RID: 12961 RVA: 0x00101FE4 File Offset: 0x001001E4
	public ZoneChangeList AddLocalZoneChange(Card triggerCard, Zone destinationZone, TAG_ZONE destinationZoneTag, int destinationPos, ZoneMgr.ChangeCompleteCallback callback, object userData)
	{
		if (destinationZoneTag == TAG_ZONE.INVALID)
		{
			Debug.LogWarning(string.Format("ZoneMgr.AddLocalZoneChange() - illegal zone change to {0} for card {1}", destinationZoneTag, triggerCard));
			return null;
		}
		if ((destinationZone is ZonePlay || destinationZone is ZoneHand) && destinationPos <= 0)
		{
			Debug.LogWarning(string.Format("ZoneMgr.AddLocalZoneChange() - destinationPos {0} is too small for zone {1}, min is 1", destinationPos, destinationZone));
			return null;
		}
		ZoneChangeList zoneChangeList = this.CreateLocalChangeList(triggerCard, destinationZone, destinationZoneTag, destinationPos, callback, userData);
		this.ProcessOrEnqueueLocalChangeList(zoneChangeList);
		this.m_localChangeListHistory.Enqueue(zoneChangeList);
		return zoneChangeList;
	}

	// Token: 0x060032A2 RID: 12962 RVA: 0x00102068 File Offset: 0x00100268
	public ZoneChangeList AddPredictedLocalZoneChange(Card triggerCard, Zone destinationZone, int destinationPos, int predictedPos)
	{
		if (triggerCard == null)
		{
			Debug.LogWarning(string.Format("ZoneMgr.AddPredictedLocalZoneChange() - triggerCard is null", Array.Empty<object>()));
			return null;
		}
		ZoneChangeList zoneChangeList = this.AddLocalZoneChange(triggerCard, destinationZone, destinationPos);
		if (zoneChangeList == null)
		{
			return null;
		}
		triggerCard.SetPredictedZonePosition(predictedPos);
		zoneChangeList.SetPredictedPosition(predictedPos);
		return zoneChangeList;
	}

	// Token: 0x060032A3 RID: 12963 RVA: 0x001020B4 File Offset: 0x001002B4
	public ZoneChangeList CancelLocalZoneChange(ZoneChangeList changeList, ZoneMgr.ChangeCompleteCallback callback = null, object userData = null)
	{
		if (changeList == null)
		{
			Debug.LogWarning(string.Format("ZoneMgr.CancelLocalZoneChange() - changeList is null", Array.Empty<object>()));
			return null;
		}
		if (!this.m_localChangeListHistory.Remove(changeList))
		{
			Debug.LogWarning(string.Format("ZoneMgr.CancelLocalZoneChange() - changeList {0} is not in history", changeList.GetId()));
			return null;
		}
		ZoneChange localTriggerChange = changeList.GetLocalTriggerChange();
		global::Entity entity = localTriggerChange.GetEntity();
		Card card = entity.GetCard();
		Zone sourceZone = localTriggerChange.GetSourceZone();
		int sourcePosition = localTriggerChange.GetSourcePosition();
		ZoneChangeList zoneChangeList = this.CreateLocalChangeList(card, sourceZone, sourceZone.m_ServerTag, sourcePosition, callback, userData);
		if (entity.IsHero())
		{
			this.AddOldHeroCanceledChange(zoneChangeList, card);
		}
		zoneChangeList.SetCanceledChangeList(true);
		zoneChangeList.SetZoneInputBlocking(true);
		this.ProcessOrEnqueueLocalChangeList(zoneChangeList);
		return zoneChangeList;
	}

	// Token: 0x060032A4 RID: 12964 RVA: 0x00102164 File Offset: 0x00100364
	private void AddOldHeroCanceledChange(ZoneChangeList canceledChangeList, Card triggerCard)
	{
		global::Player controller = triggerCard.GetController();
		Card heroCard = controller.GetHeroCard();
		ZoneChange zoneChange = new ZoneChange();
		zoneChange.SetParentList(canceledChangeList);
		zoneChange.SetEntity(heroCard.GetEntity());
		zoneChange.SetDestinationZone(controller.GetHeroZone());
		zoneChange.SetDestinationZoneTag(controller.GetHeroZone().m_ServerTag);
		zoneChange.SetDestinationPosition(0);
		Log.Zone.Print("ZoneMgr.CreateLocalChangesFromTrigger() - AddChange() canceledChangeList: {0},  triggerChange: {1}", new object[]
		{
			canceledChangeList,
			zoneChange
		});
		canceledChangeList.AddChange(zoneChange);
	}

	// Token: 0x060032A5 RID: 12965 RVA: 0x001021E0 File Offset: 0x001003E0
	public static bool IsHandledPower(Network.PowerHistory power)
	{
		switch (power.Type)
		{
		case Network.PowerType.FULL_ENTITY:
		{
			Network.HistFullEntity histFullEntity = power as Network.HistFullEntity;
			bool result = false;
			foreach (Network.Entity.Tag tag in histFullEntity.Entity.Tags)
			{
				if (tag.Name == 202)
				{
					if (tag.Value == 1)
					{
						return false;
					}
					if (tag.Value == 2)
					{
						return false;
					}
				}
				else if (tag.Name == 49 || tag.Name == 263 || tag.Name == 50)
				{
					result = true;
				}
			}
			return result;
		}
		case Network.PowerType.SHOW_ENTITY:
			return true;
		case Network.PowerType.HIDE_ENTITY:
			return true;
		case Network.PowerType.TAG_CHANGE:
		{
			Network.HistTagChange histTagChange = power as Network.HistTagChange;
			if (histTagChange.Tag == 49 || histTagChange.Tag == 263 || histTagChange.Tag == 50)
			{
				global::Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
				if (entity != null)
				{
					if (entity.IsPlayer())
					{
						return false;
					}
					if (entity.IsGame())
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}
		default:
			return false;
		}
	}

	// Token: 0x060032A6 RID: 12966 RVA: 0x00102318 File Offset: 0x00100518
	public bool HasActiveServerChange()
	{
		return this.m_activeServerChangeList != null;
	}

	// Token: 0x060032A7 RID: 12967 RVA: 0x00102323 File Offset: 0x00100523
	public bool HasPendingServerChange()
	{
		return this.m_pendingServerChangeLists.Count > 0;
	}

	// Token: 0x060032A8 RID: 12968 RVA: 0x00102334 File Offset: 0x00100534
	public ZoneChangeList AddServerZoneChanges(PowerTaskList taskList, int taskStartIndex, int taskEndIndex, ZoneMgr.ChangeCompleteCallback callback, object userData)
	{
		int nextServerChangeListId = this.GetNextServerChangeListId();
		ZoneChangeList zoneChangeList = new ZoneChangeList();
		zoneChangeList.SetId(nextServerChangeListId);
		zoneChangeList.SetTaskList(taskList);
		zoneChangeList.SetCompleteCallback(callback);
		zoneChangeList.SetCompleteCallbackUserData(userData);
		Log.Zone.Print("ZoneMgr.AddServerZoneChanges() - taskListId={0} changeListId={1} taskStart={2} taskEnd={3}", new object[]
		{
			taskList.GetId(),
			nextServerChangeListId,
			taskStartIndex,
			taskEndIndex
		});
		List<PowerTask> taskList2 = taskList.GetTaskList();
		int i = taskStartIndex;
		while (i <= taskEndIndex)
		{
			PowerTask powerTask = taskList2[i];
			Network.PowerHistory power = powerTask.GetPower();
			Network.PowerType type = power.Type;
			ZoneChange zoneChange;
			switch (type)
			{
			case Network.PowerType.FULL_ENTITY:
			{
				Network.HistFullEntity fullEntity = (Network.HistFullEntity)power;
				zoneChange = this.CreateZoneChangeFromFullEntity(fullEntity);
				break;
			}
			case Network.PowerType.SHOW_ENTITY:
			{
				Network.HistShowEntity histShowEntity = (Network.HistShowEntity)power;
				zoneChange = this.CreateZoneChangeFromEntity(histShowEntity.Entity);
				break;
			}
			case Network.PowerType.HIDE_ENTITY:
			{
				Network.HistHideEntity hideEntity = (Network.HistHideEntity)power;
				zoneChange = this.CreateZoneChangeFromHideEntity(hideEntity);
				break;
			}
			case Network.PowerType.TAG_CHANGE:
			{
				Network.HistTagChange tagChange = (Network.HistTagChange)power;
				zoneChange = this.CreateZoneChangeFromTagChange(tagChange);
				break;
			}
			case Network.PowerType.BLOCK_START:
			case Network.PowerType.BLOCK_END:
				goto IL_177;
			case Network.PowerType.CREATE_GAME:
			case Network.PowerType.RESET_GAME:
			case Network.PowerType.SUB_SPELL_START:
			case Network.PowerType.SUB_SPELL_END:
			case Network.PowerType.VO_SPELL:
			case Network.PowerType.CACHED_TAG_FOR_DORMANT_CHANGE:
			case Network.PowerType.SHUFFLE_DECK:
				zoneChange = this.CreateZoneChangeForNonZoneTask();
				break;
			case Network.PowerType.META_DATA:
			{
				Network.HistMetaData metaData = (Network.HistMetaData)power;
				zoneChange = this.CreateZoneChangeFromMetaData(metaData);
				break;
			}
			case Network.PowerType.CHANGE_ENTITY:
			{
				Network.HistChangeEntity histChangeEntity = (Network.HistChangeEntity)power;
				zoneChange = this.CreateZoneChangeFromEntity(histChangeEntity.Entity);
				break;
			}
			default:
				goto IL_177;
			}
			if (zoneChange != null)
			{
				zoneChange.SetParentList(zoneChangeList);
				zoneChange.SetPowerTask(powerTask);
				Log.Zone.Print("ZoneMgr.AddServerZoneChanges() - AddChange() changeList: {0},  change: {1}", new object[]
				{
					zoneChangeList,
					zoneChange
				});
				zoneChangeList.AddChange(zoneChange);
			}
			i++;
			continue;
			IL_177:
			Debug.LogError(string.Format("ZoneMgr.AddServerZoneChanges() - id={0} received unhandled power of type {1}", zoneChangeList.GetId(), type));
			return null;
		}
		for (int j = 0; j < zoneChangeList.GetChanges().Count; j++)
		{
			ZoneChange zoneChange2 = zoneChangeList.GetChanges()[j];
			Network.HistMetaData histMetaData = zoneChange2.GetPowerTask().GetPower() as Network.HistMetaData;
			if (histMetaData != null && histMetaData.MetaType == HistoryMeta.Type.CONTROLLER_AND_ZONE_CHANGE)
			{
				if (histMetaData.Info.Count != 5)
				{
					Log.Zone.PrintError("CONTROLLER_AND_ZONE_CHANGE MetaData task found ({0}), but info array isn't of size 5!", Array.Empty<object>());
				}
				ZoneChange zoneChange3 = null;
				ZoneChange zoneChange4 = null;
				int num = histMetaData.Info[1];
				int num2 = histMetaData.Info[2];
				TAG_ZONE zoneTag = (TAG_ZONE)histMetaData.Info[3];
				TAG_ZONE tag_ZONE = (TAG_ZONE)histMetaData.Info[4];
				for (int k = j + 1; k < zoneChangeList.GetChanges().Count; k++)
				{
					ZoneChange zoneChange5 = zoneChangeList.GetChanges()[k];
					if (zoneChange5.GetEntity() == zoneChange2.GetEntity())
					{
						if (zoneChange5.HasDestinationControllerId() && zoneChange5.GetDestinationControllerId() == num2 && zoneChange5.GetDestinationZoneTag() != tag_ZONE)
						{
							zoneChange3 = zoneChange5;
						}
						else if (zoneChange5.HasDestinationControllerId() && zoneChange5.GetDestinationControllerId() == num2 && zoneChange5.HasDestinationZoneChange() && zoneChange5.GetDestinationZoneTag() == tag_ZONE)
						{
							zoneChange3 = zoneChange5;
							zoneChange4 = zoneChange5;
						}
						else if (!zoneChange5.HasDestinationControllerId() && zoneChange5.HasDestinationZoneChange() && zoneChange5.GetDestinationZoneTag() == tag_ZONE)
						{
							zoneChange4 = zoneChange5;
						}
						if (zoneChange3 != null && zoneChange4 != null)
						{
							break;
						}
					}
				}
				if (zoneChange3 != null && zoneChange4 != null)
				{
					global::Entity entity = zoneChange3.GetEntity();
					Zone sourceZone = this.FindZoneForTags(num, zoneTag, entity.GetCardType(), entity);
					zoneChange4.SetSourceZone(sourceZone);
					zoneChange4.SetDestinationControllerId(zoneChange3.GetDestinationControllerId());
					zoneChange4.SetSourceControllerId(num);
					if (zoneChange4 != zoneChange3)
					{
						zoneChange3.ClearDestinationControllerId();
						zoneChange3.SetDestinationZone(null);
					}
				}
				else
				{
					Log.Zone.PrintError("CONTROLLER_AND_ZONE_CHANGE MetaData task found ({0}), but couldn't find both controller ({1}) and zone ({2}) changes in tasklist!", new object[]
					{
						zoneChange2,
						zoneChange3,
						zoneChange4
					});
				}
			}
		}
		this.m_tempEntityMap.Clear();
		this.m_pendingServerChangeLists.Enqueue(zoneChangeList);
		return zoneChangeList;
	}

	// Token: 0x060032A9 RID: 12969 RVA: 0x0010273C File Offset: 0x0010093C
	private void UpdateLocalChangeLists()
	{
		List<ZoneChangeList> list = null;
		int i = 0;
		while (i < this.m_activeLocalChangeLists.Count)
		{
			ZoneChangeList zoneChangeList = this.m_activeLocalChangeLists[i];
			if (!zoneChangeList.IsComplete())
			{
				i++;
			}
			else
			{
				zoneChangeList.FireCompleteCallback();
				this.m_activeLocalChangeLists.RemoveAt(i);
				if (list == null)
				{
					list = new List<ZoneChangeList>();
				}
				list.Add(zoneChangeList);
			}
		}
		if (list == null)
		{
			return;
		}
		foreach (ZoneChangeList zoneChangeList2 in list)
		{
			ZoneChange localTriggerChange = zoneChangeList2.GetLocalTriggerChange();
			Card card = localTriggerChange.GetEntity().GetCard();
			if (zoneChangeList2.IsCanceledChangeList())
			{
				card.SetPredictedZonePosition(0);
				if (card.m_minionWasMovedFromSrcToDst != null && card.m_minionWasMovedFromSrcToDst.m_destinationZonePosition == localTriggerChange.GetDestinationPosition())
				{
					card.m_minionWasMovedFromSrcToDst = null;
				}
			}
			int num = this.FindTriggeredPendingLocalChangeIndex(card);
			if (num >= 0)
			{
				ZoneChangeList zoneChangeList3 = this.m_pendingLocalChangeLists[num];
				this.m_pendingLocalChangeLists.RemoveAt(num);
				this.CreateLocalChangesFromTrigger(zoneChangeList3, zoneChangeList3.GetLocalTriggerChange());
				this.ProcessLocalChangeList(zoneChangeList3);
			}
		}
	}

	// Token: 0x060032AA RID: 12970 RVA: 0x0010286C File Offset: 0x00100A6C
	private void UpdateServerChangeLists()
	{
		if (this.m_activeServerChangeList != null && this.m_activeServerChangeList.IsComplete())
		{
			this.m_activeServerChangeList.FireCompleteCallback();
			this.m_activeServerChangeList = null;
			this.AutoCorrectZonesAfterServerChange();
		}
		if (this.HasPendingServerChange() && !this.HasActiveServerChange())
		{
			this.m_activeServerChangeList = this.m_pendingServerChangeLists.Dequeue();
			this.PostProcessServerChangeList(this.m_activeServerChangeList);
			base.StartCoroutine(this.m_activeServerChangeList.ProcessChanges());
		}
	}

	// Token: 0x060032AB RID: 12971 RVA: 0x001028E5 File Offset: 0x00100AE5
	private bool HasLocalChangeExitingZone(global::Entity entity, Zone zone)
	{
		return this.HasLocalChangeExitingZone(entity, zone, this.m_activeLocalChangeLists) || this.HasLocalChangeExitingZone(entity, zone, this.m_pendingLocalChangeLists);
	}

	// Token: 0x060032AC RID: 12972 RVA: 0x0010290C File Offset: 0x00100B0C
	private bool HasLocalChangeExitingZone(global::Entity entity, Zone zone, List<ZoneChangeList> changeLists)
	{
		TAG_ZONE serverTag = zone.m_ServerTag;
		foreach (ZoneChangeList zoneChangeList in changeLists)
		{
			foreach (ZoneChange zoneChange in zoneChangeList.GetChanges())
			{
				if (entity == zoneChange.GetEntity() && serverTag == zoneChange.GetSourceZoneTag() && serverTag != zoneChange.GetDestinationZoneTag())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060032AD RID: 12973 RVA: 0x001029B8 File Offset: 0x00100BB8
	private void PredictZoneFromPowerProcessor(ZoneMgr.TempZone tempZone)
	{
		PowerProcessor powerProcessor = GameState.Get().GetPowerProcessor();
		tempZone.PreprocessChanges();
		powerProcessor.ForEachTaskList(delegate(int queueIndex, PowerTaskList taskList)
		{
			this.PredictZoneFromPowerTaskList(tempZone, taskList);
		});
		tempZone.Sort();
		tempZone.PostprocessChanges();
	}

	// Token: 0x060032AE RID: 12974 RVA: 0x00102A18 File Offset: 0x00100C18
	private void PredictZoneFromPowerTaskList(ZoneMgr.TempZone tempZone, PowerTaskList taskList)
	{
		List<PowerTask> taskList2 = taskList.GetTaskList();
		for (int i = 0; i < taskList2.Count; i++)
		{
			Network.PowerHistory power = taskList2[i].GetPower();
			this.PredictZoneFromPower(tempZone, power);
		}
	}

	// Token: 0x060032AF RID: 12975 RVA: 0x00102A54 File Offset: 0x00100C54
	private void PredictZoneFromPower(ZoneMgr.TempZone tempZone, Network.PowerHistory power)
	{
		switch (power.Type)
		{
		case Network.PowerType.FULL_ENTITY:
			this.PredictZoneFromFullEntity(tempZone, (Network.HistFullEntity)power);
			return;
		case Network.PowerType.SHOW_ENTITY:
			this.PredictZoneFromShowEntity(tempZone, (Network.HistShowEntity)power);
			return;
		case Network.PowerType.HIDE_ENTITY:
			this.PredictZoneFromHideEntity(tempZone, (Network.HistHideEntity)power);
			return;
		case Network.PowerType.TAG_CHANGE:
			this.PredictZoneFromTagChange(tempZone, (Network.HistTagChange)power);
			return;
		default:
			return;
		}
	}

	// Token: 0x060032B0 RID: 12976 RVA: 0x00102AB8 File Offset: 0x00100CB8
	private void PredictZoneFromFullEntity(ZoneMgr.TempZone tempZone, Network.HistFullEntity fullEntity)
	{
		global::Entity entity = this.RegisterTempEntity(fullEntity.Entity);
		if (entity == null)
		{
			return;
		}
		Zone zone = tempZone.GetZone();
		bool flag = entity.GetZone() == zone.m_ServerTag;
		bool flag2 = entity.GetControllerId() == zone.GetControllerId();
		if (!flag)
		{
			return;
		}
		if (!flag2)
		{
			return;
		}
		tempZone.AddEntity(entity);
	}

	// Token: 0x060032B1 RID: 12977 RVA: 0x00102B08 File Offset: 0x00100D08
	private void PredictZoneFromShowEntity(ZoneMgr.TempZone tempZone, Network.HistShowEntity showEntity)
	{
		global::Entity tempEntity = this.RegisterTempEntity(showEntity.Entity);
		foreach (Network.Entity.Tag tag in showEntity.Entity.Tags)
		{
			this.PredictZoneByApplyingTag(tempZone, tempEntity, (GAME_TAG)tag.Name, tag.Value);
		}
	}

	// Token: 0x060032B2 RID: 12978 RVA: 0x00102B7C File Offset: 0x00100D7C
	private void PredictZoneFromHideEntity(ZoneMgr.TempZone tempZone, Network.HistHideEntity hideEntity)
	{
		global::Entity tempEntity = this.RegisterTempEntity(hideEntity.Entity);
		this.PredictZoneByApplyingTag(tempZone, tempEntity, GAME_TAG.ZONE, hideEntity.Zone);
	}

	// Token: 0x060032B3 RID: 12979 RVA: 0x00102BA8 File Offset: 0x00100DA8
	private void PredictZoneFromTagChange(ZoneMgr.TempZone tempZone, Network.HistTagChange tagChange)
	{
		global::Entity tempEntity = this.RegisterTempEntity(tagChange.Entity);
		this.PredictZoneByApplyingTag(tempZone, tempEntity, (GAME_TAG)tagChange.Tag, tagChange.Value);
	}

	// Token: 0x060032B4 RID: 12980 RVA: 0x00102BD8 File Offset: 0x00100DD8
	private void PredictZoneByApplyingTag(ZoneMgr.TempZone tempZone, global::Entity tempEntity, GAME_TAG tag, int val)
	{
		if (tempEntity == null)
		{
			return;
		}
		if (tag != GAME_TAG.ZONE && tag != GAME_TAG.CONTROLLER)
		{
			tempEntity.SetTag(tag, val);
			return;
		}
		Zone zone = tempZone.GetZone();
		bool flag = tempEntity.GetZone() == zone.m_ServerTag;
		bool flag2 = tempEntity.GetControllerId() == zone.GetControllerId();
		if (flag && flag2)
		{
			tempZone.RemoveEntity(tempEntity);
		}
		tempEntity.SetTag(tag, val);
		bool flag3 = tempEntity.GetZone() == zone.m_ServerTag;
		flag2 = (tempEntity.GetControllerId() == zone.GetControllerId());
		if (flag3 && flag2)
		{
			tempZone.AddEntity(tempEntity);
		}
	}

	// Token: 0x060032B5 RID: 12981 RVA: 0x00102C64 File Offset: 0x00100E64
	private ZoneChangeList CreateLocalChangeList(Card triggerCard, Zone destinationZone, TAG_ZONE destinationZoneTag, int destinationPos, ZoneMgr.ChangeCompleteCallback callback, object userData)
	{
		int nextLocalChangeListId = this.GetNextLocalChangeListId();
		Log.Zone.Print("ZoneMgr.CreateLocalChangeList() - changeListId={0}", new object[]
		{
			nextLocalChangeListId
		});
		ZoneChangeList zoneChangeList = new ZoneChangeList();
		zoneChangeList.SetId(nextLocalChangeListId);
		zoneChangeList.SetCompleteCallback(callback);
		zoneChangeList.SetCompleteCallbackUserData(userData);
		global::Entity entity = triggerCard.GetEntity();
		Zone zone = triggerCard.GetZone();
		TAG_ZONE sourceZoneTag = (zone == null) ? TAG_ZONE.INVALID : zone.m_ServerTag;
		int zonePosition = triggerCard.GetZonePosition();
		ZoneChange zoneChange = new ZoneChange();
		zoneChange.SetParentList(zoneChangeList);
		zoneChange.SetEntity(entity);
		zoneChange.SetSourceZone(zone);
		zoneChange.SetSourceZoneTag(sourceZoneTag);
		zoneChange.SetSourcePosition(zonePosition);
		zoneChange.SetDestinationZone(destinationZone);
		zoneChange.SetDestinationZoneTag(destinationZoneTag);
		zoneChange.SetDestinationPosition(destinationPos);
		Log.Zone.Print("ZoneMgr.CreateLocalChangeList() - AddChange() changeList: {0}, triggerChange: {1}", new object[]
		{
			zoneChangeList,
			zoneChange
		});
		zoneChangeList.AddChange(zoneChange);
		return zoneChangeList;
	}

	// Token: 0x060032B6 RID: 12982 RVA: 0x00102D50 File Offset: 0x00100F50
	private void ProcessOrEnqueueLocalChangeList(ZoneChangeList changeList)
	{
		ZoneChange localTriggerChange = changeList.GetLocalTriggerChange();
		Card card = localTriggerChange.GetEntity().GetCard();
		if (this.HasTriggeredActiveLocalChange(card))
		{
			this.m_pendingLocalChangeLists.Add(changeList);
			return;
		}
		this.CreateLocalChangesFromTrigger(changeList, localTriggerChange);
		this.ProcessLocalChangeList(changeList);
	}

	// Token: 0x060032B7 RID: 12983 RVA: 0x00102D98 File Offset: 0x00100F98
	private void CreateLocalChangesFromTrigger(ZoneChangeList changeList, ZoneChange triggerChange)
	{
		Log.Zone.Print(string.Format("ZoneMgr.CreateLocalChangesFromTrigger() - {0}", changeList), Array.Empty<object>());
		global::Entity entity = triggerChange.GetEntity();
		Zone sourceZone = triggerChange.GetSourceZone();
		int sourcePosition = triggerChange.GetSourcePosition();
		Zone destinationZone = triggerChange.GetDestinationZone();
		int destinationPosition = triggerChange.GetDestinationPosition();
		if (sourceZone != destinationZone)
		{
			TAG_ZONE sourceZoneTag = triggerChange.GetSourceZoneTag();
			TAG_ZONE destinationZoneTag = triggerChange.GetDestinationZoneTag();
			this.CreateLocalChangesFromTrigger(changeList, entity, sourceZone, sourceZoneTag, sourcePosition, destinationZone, destinationZoneTag, destinationPosition);
			return;
		}
		if (sourcePosition != destinationPosition)
		{
			this.CreateLocalPosOnlyChangesFromTrigger(changeList, entity, sourceZone, sourcePosition, destinationPosition);
		}
	}

	// Token: 0x060032B8 RID: 12984 RVA: 0x00102E20 File Offset: 0x00101020
	private void CreateLocalChangesFromTrigger(ZoneChangeList changeList, global::Entity triggerEntity, Zone sourceZone, TAG_ZONE sourceZoneTag, int sourcePos, Zone destinationZone, TAG_ZONE destinationZoneTag, int destinationPos)
	{
		Log.Zone.Print("ZoneMgr.CreateLocalChangesFromTrigger() - triggerEntity={0} srcZone={1} srcPos={2} dstZone={3} dstPos={4}", new object[]
		{
			triggerEntity,
			sourceZoneTag,
			sourcePos,
			destinationZoneTag,
			destinationPos
		});
		if (sourcePos != destinationPos)
		{
			Log.Zone.Print("ZoneMgr.CreateLocalChangesFromTrigger() - srcPos={0} destPos={1}", new object[]
			{
				sourcePos,
				destinationPos
			});
		}
		if (sourceZone != null && !(sourceZone is ZoneHero))
		{
			foreach (Card card in sourceZone.GetCards())
			{
				int zonePosition = card.GetZonePosition();
				if (zonePosition > sourcePos)
				{
					global::Entity entity = card.GetEntity();
					ZoneChange zoneChange = new ZoneChange();
					zoneChange.SetParentList(changeList);
					zoneChange.SetEntity(entity);
					int num = zonePosition - 1;
					zoneChange.SetSourcePosition(zonePosition);
					zoneChange.SetDestinationPosition(num);
					Log.Zone.Print(string.Format("ZoneMgr.CreateLocalChangesFromTrigger() - srcZone card {0} zonePos {1} -> {2}", card, card.GetZonePosition(), num), Array.Empty<object>());
					Log.Zone.Print(string.Format("ZoneMgr.CreateLocalChangesFromTrigger() 3 - AddChange() changeList: {0}, change: {1}", changeList, zoneChange), Array.Empty<object>());
					changeList.AddChange(zoneChange);
				}
			}
		}
		if (destinationZone != null && !(destinationZone is ZoneSecret))
		{
			if (destinationZone is ZoneWeapon)
			{
				List<Card> cards = destinationZone.GetCards();
				if (cards.Count > 0)
				{
					global::Entity entity2 = cards[0].GetEntity();
					ZoneChange zoneChange2 = new ZoneChange();
					zoneChange2.SetParentList(changeList);
					zoneChange2.SetEntity(entity2);
					zoneChange2.SetDestinationZone(this.FindZoneOfType<ZoneGraveyard>(destinationZone.m_Side));
					zoneChange2.SetDestinationZoneTag(TAG_ZONE.GRAVEYARD);
					Log.Zone.Print("ZoneMgr.CreateLocalChangesFromTrigger() 4 - AddChange() changeList: {0}, change: {1}", new object[]
					{
						changeList,
						zoneChange2
					});
					changeList.AddChange(zoneChange2);
					return;
				}
			}
			else
			{
				if (destinationZone is ZonePlay || destinationZone is ZoneHand)
				{
					List<Card> cards2 = destinationZone.GetCards();
					for (int i = destinationPos - 1; i < cards2.Count; i++)
					{
						Card card2 = cards2[i];
						global::Entity entity3 = card2.GetEntity();
						int num2 = i + 2;
						ZoneChange zoneChange3 = new ZoneChange();
						zoneChange3.SetParentList(changeList);
						zoneChange3.SetEntity(entity3);
						zoneChange3.SetDestinationPosition(num2);
						Log.Zone.Print("ZoneMgr.CreateLocalChangesFromTrigger() - dstZone card {0} zonePos {1} -> {2}", new object[]
						{
							card2,
							entity3.GetZonePosition(),
							num2
						});
						Log.Zone.Print("ZoneMgr.CreateLocalChangesFromTrigger() 5 - AddChange() changeList: {0}, change: {1}", new object[]
						{
							changeList,
							zoneChange3
						});
						changeList.AddChange(zoneChange3);
					}
					return;
				}
				if (!(destinationZone is ZoneHero))
				{
					Debug.LogError(string.Format("ZoneMgr.CreateLocalChangesFromTrigger() - don't know how to predict zone position changes for zone {0}", destinationZone));
				}
			}
		}
	}

	// Token: 0x060032B9 RID: 12985 RVA: 0x00103120 File Offset: 0x00101320
	private void CreateLocalPosOnlyChangesFromTrigger(ZoneChangeList changeList, global::Entity triggerEntity, Zone sourceZone, int sourcePos, int destinationPos)
	{
		List<Card> cards = sourceZone.GetCards();
		if (sourcePos < destinationPos)
		{
			for (int i = 0; i < cards.Count; i++)
			{
				Card card = cards[i];
				global::Entity entity = card.GetEntity();
				int zonePosition = card.GetZonePosition();
				if (zonePosition <= destinationPos && zonePosition >= sourcePos)
				{
					int destinationPosition = zonePosition - 1;
					if (entity == triggerEntity)
					{
						destinationPosition = destinationPos;
					}
					ZoneChange zoneChange = new ZoneChange();
					zoneChange.SetParentList(changeList);
					zoneChange.SetEntity(entity);
					zoneChange.SetSourcePosition(card.GetZonePosition());
					zoneChange.SetDestinationPosition(destinationPosition);
					Log.Zone.Print("ZoneMgr.CreateLocalPosOnlyChangesFromTrigger() 1 - AddChange() changeList: {0}, change: {1}", new object[]
					{
						changeList,
						zoneChange
					});
					changeList.AddChange(zoneChange);
				}
			}
			return;
		}
		for (int j = 0; j < cards.Count; j++)
		{
			Card card2 = cards[j];
			global::Entity entity2 = card2.GetEntity();
			int zonePosition2 = card2.GetZonePosition();
			if (zonePosition2 <= sourcePos && zonePosition2 >= destinationPos)
			{
				int destinationPosition2 = zonePosition2 + 1;
				if (entity2 == triggerEntity)
				{
					destinationPosition2 = destinationPos;
				}
				ZoneChange zoneChange2 = new ZoneChange();
				zoneChange2.SetParentList(changeList);
				zoneChange2.SetEntity(entity2);
				zoneChange2.SetSourcePosition(card2.GetZonePosition());
				zoneChange2.SetDestinationPosition(destinationPosition2);
				Log.Zone.Print("ZoneMgr.CreateLocalPosOnlyChangesFromTrigger() 2 - AddChange() changeList: {0}, change: {1}", new object[]
				{
					changeList,
					zoneChange2
				});
				changeList.AddChange(zoneChange2);
			}
		}
	}

	// Token: 0x060032BA RID: 12986 RVA: 0x00103280 File Offset: 0x00101480
	private void ProcessLocalChangeList(ZoneChangeList changeList)
	{
		Log.Zone.Print("ZoneMgr.ProcessLocalChangeList() - [{0}]", new object[]
		{
			changeList
		});
		this.m_activeLocalChangeLists.Add(changeList);
		base.StartCoroutine(changeList.ProcessChanges());
	}

	// Token: 0x060032BB RID: 12987 RVA: 0x001032B4 File Offset: 0x001014B4
	private void OnCurrentPlayerChanged(global::Player player, object userData)
	{
		if (player.IsLocalUser())
		{
			this.m_localChangeListHistory.Clear();
		}
	}

	// Token: 0x060032BC RID: 12988 RVA: 0x001032CC File Offset: 0x001014CC
	private void OnOptionRejected(Network.Options.Option option, object userData)
	{
		if (option.Type != Network.Options.Option.OptionType.POWER)
		{
			return;
		}
		global::Entity entity = GameState.Get().GetEntity(option.Main.ID);
		ZoneChangeList zoneChangeList = this.FindRejectedLocalZoneChange(entity);
		if (zoneChangeList == null)
		{
			Log.Zone.Print("ZoneMgr.RejectLocalZoneChange() - did not find a zone change to reject for {0}", new object[]
			{
				entity
			});
			return;
		}
		Card card = entity.GetCard();
		card.SetPredictedZonePosition(0);
		ZoneChange localTriggerChange = zoneChangeList.GetLocalTriggerChange();
		if (card.m_minionWasMovedFromSrcToDst != null && card.m_minionWasMovedFromSrcToDst.m_destinationZonePosition == localTriggerChange.GetDestinationPosition())
		{
			card.m_minionWasMovedFromSrcToDst = null;
		}
		this.CancelLocalZoneChange(zoneChangeList, null, null);
	}

	// Token: 0x060032BD RID: 12989 RVA: 0x00103360 File Offset: 0x00101560
	private ZoneChangeList FindRejectedLocalZoneChange(global::Entity triggerEntity)
	{
		List<ZoneChangeList> list = this.m_localChangeListHistory.GetList();
		for (int i = 0; i < list.Count; i++)
		{
			ZoneChangeList zoneChangeList = list[i];
			List<ZoneChange> changes = zoneChangeList.GetChanges();
			for (int j = 0; j < changes.Count; j++)
			{
				ZoneChange zoneChange = changes[j];
				if (zoneChange.GetEntity() == triggerEntity && zoneChange.GetDestinationZoneTag() == TAG_ZONE.PLAY)
				{
					return zoneChangeList;
				}
			}
		}
		return null;
	}

	// Token: 0x060032BE RID: 12990 RVA: 0x001033CF File Offset: 0x001015CF
	private ZoneChange CreateZoneChangeForNonZoneTask()
	{
		ZoneChange zoneChange = new ZoneChange();
		zoneChange.SetEntity(GameState.Get().GetGameEntity());
		return zoneChange;
	}

	// Token: 0x060032BF RID: 12991 RVA: 0x001033E8 File Offset: 0x001015E8
	private ZoneChange CreateZoneChangeFromFullEntity(Network.HistFullEntity fullEntity)
	{
		Network.Entity entity = fullEntity.Entity;
		global::Entity entity2 = GameState.Get().GetEntity(entity.ID);
		if (entity2 == null)
		{
			Debug.LogWarning(string.Format("ZoneMgr.CreateZoneChangeFromFullEntity() - WARNING entity {0} DOES NOT EXIST!", entity.ID));
			return null;
		}
		ZoneChange zoneChange = new ZoneChange();
		zoneChange.SetEntity(entity2);
		if (entity2.GetCard() == null)
		{
			return zoneChange;
		}
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		foreach (Network.Entity.Tag tag in entity.Tags)
		{
			GAME_TAG name = (GAME_TAG)tag.Name;
			if (name != GAME_TAG.ZONE)
			{
				if (name != GAME_TAG.CONTROLLER)
				{
					if (name == GAME_TAG.ZONE_POSITION)
					{
						zoneChange.SetDestinationPosition(tag.Value);
						flag2 = true;
					}
				}
				else
				{
					zoneChange.SetDestinationControllerId(tag.Value);
					flag3 = true;
				}
			}
			else
			{
				zoneChange.SetDestinationZoneTag((TAG_ZONE)tag.Value);
				flag = true;
			}
			if (flag && flag2 && flag3)
			{
				break;
			}
		}
		if (flag || flag3)
		{
			zoneChange.SetDestinationZone(this.FindZoneForEntity(entity2));
		}
		return zoneChange;
	}

	// Token: 0x060032C0 RID: 12992 RVA: 0x00103504 File Offset: 0x00101704
	private ZoneChange CreateZoneChangeFromEntity(Network.Entity netEnt)
	{
		global::Entity entity = GameState.Get().GetEntity(netEnt.ID);
		if (entity == null)
		{
			if (!GameState.Get().EntityRemovedFromGame(netEnt.ID))
			{
				Debug.LogWarning(string.Format("ZoneMgr.CreateZoneChangeFromEntity() - WARNING entity {0} DOES NOT EXIST!", netEnt.ID));
			}
			return null;
		}
		ZoneChange zoneChange = new ZoneChange();
		zoneChange.SetEntity(entity);
		if (entity.GetCard() == null)
		{
			return zoneChange;
		}
		global::Entity entity2 = this.RegisterTempEntity(netEnt.ID, entity);
		if (entity2 == null)
		{
			return zoneChange;
		}
		foreach (Network.Entity.Tag tag in netEnt.Tags)
		{
			entity2.SetTag(tag.Name, tag.Value);
		}
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		foreach (Network.Entity.Tag tag2 in netEnt.Tags)
		{
			GAME_TAG name = (GAME_TAG)tag2.Name;
			if (name != GAME_TAG.ZONE)
			{
				if (name != GAME_TAG.CONTROLLER)
				{
					if (name == GAME_TAG.ZONE_POSITION)
					{
						zoneChange.SetDestinationPosition(tag2.Value);
						flag2 = true;
					}
				}
				else
				{
					zoneChange.SetDestinationControllerId(tag2.Value);
					flag3 = true;
				}
			}
			else
			{
				zoneChange.SetDestinationZoneTag((TAG_ZONE)tag2.Value);
				flag = true;
			}
			if (flag && flag2 && flag3)
			{
				break;
			}
		}
		if (flag || flag3)
		{
			zoneChange.SetDestinationZone(this.FindZoneForEntity(entity2));
		}
		return zoneChange;
	}

	// Token: 0x060032C1 RID: 12993 RVA: 0x00103690 File Offset: 0x00101890
	private ZoneChange CreateZoneChangeFromHideEntity(Network.HistHideEntity hideEntity)
	{
		global::Entity entity = GameState.Get().GetEntity(hideEntity.Entity);
		if (entity == null)
		{
			if (!GameState.Get().EntityRemovedFromGame(hideEntity.Entity))
			{
				Debug.LogWarning(string.Format("ZoneMgr.CreateZoneChangeFromHideEntity() - WARNING entity {0} DOES NOT EXIST! zone={1}", hideEntity.Entity, hideEntity.Zone));
			}
			return null;
		}
		ZoneChange zoneChange = new ZoneChange();
		zoneChange.SetEntity(entity);
		if (entity.GetCard() == null)
		{
			return zoneChange;
		}
		global::Entity entity2 = this.RegisterTempEntity(hideEntity.Entity, entity);
		if (entity2 == null)
		{
			return zoneChange;
		}
		entity2.SetTag(GAME_TAG.ZONE, hideEntity.Zone);
		TAG_ZONE zone = (TAG_ZONE)hideEntity.Zone;
		zoneChange.SetDestinationZoneTag(zone);
		zoneChange.SetDestinationZone(this.FindZoneForEntity(entity2));
		return zoneChange;
	}

	// Token: 0x060032C2 RID: 12994 RVA: 0x00103744 File Offset: 0x00101944
	private ZoneChange CreateZoneChangeFromTagChange(Network.HistTagChange tagChange)
	{
		global::Entity entity = GameState.Get().GetEntity(tagChange.Entity);
		if (entity == null)
		{
			if (!GameState.Get().EntityRemovedFromGame(tagChange.Entity))
			{
				Debug.LogError(string.Format("ZoneMgr.CreateZoneChangeFromTagChange() - Entity {0} does not exist", tagChange.Entity));
			}
			return null;
		}
		ZoneChange zoneChange = new ZoneChange();
		zoneChange.SetEntity(entity);
		if (entity.GetCard() == null)
		{
			return zoneChange;
		}
		global::Entity entity2 = this.RegisterTempEntity(tagChange.Entity, entity);
		if (entity2 == null)
		{
			return zoneChange;
		}
		entity2.SetTag(tagChange.Tag, tagChange.Value);
		GAME_TAG tag = (GAME_TAG)tagChange.Tag;
		if (tag != GAME_TAG.ZONE)
		{
			if (tag != GAME_TAG.CONTROLLER)
			{
				if (tag == GAME_TAG.ZONE_POSITION)
				{
					zoneChange.SetDestinationPosition(tagChange.Value);
				}
			}
			else
			{
				int value = tagChange.Value;
				zoneChange.SetDestinationControllerId(value);
				zoneChange.SetDestinationZone(this.FindZoneForEntity(entity2));
			}
		}
		else
		{
			TAG_ZONE value2 = (TAG_ZONE)tagChange.Value;
			zoneChange.SetDestinationZoneTag(value2);
			zoneChange.SetDestinationZone(this.FindZoneForEntity(entity2));
		}
		return zoneChange;
	}

	// Token: 0x060032C3 RID: 12995 RVA: 0x0010383C File Offset: 0x00101A3C
	private ZoneChange CreateZoneChangeFromMetaData(Network.HistMetaData metaData)
	{
		if (metaData.Info.Count <= 0)
		{
			return null;
		}
		global::Entity entity = GameState.Get().GetEntity(metaData.Info[0]);
		if (entity == null)
		{
			Debug.LogError(string.Format("ZoneMgr.CreateZoneChangeFromMetaData() - Entity {0} does not exist", metaData.Info[0]));
			return null;
		}
		ZoneChange zoneChange = new ZoneChange();
		zoneChange.SetEntity(entity);
		return zoneChange;
	}

	// Token: 0x060032C4 RID: 12996 RVA: 0x001038A4 File Offset: 0x00101AA4
	private global::Entity RegisterTempEntity(int id)
	{
		global::Entity entity = GameState.Get().GetEntity(id);
		return this.RegisterTempEntity(id, entity);
	}

	// Token: 0x060032C5 RID: 12997 RVA: 0x001038C8 File Offset: 0x00101AC8
	private global::Entity RegisterTempEntity(Network.Entity netEnt)
	{
		global::Entity entity = GameState.Get().GetEntity(netEnt.ID);
		return this.RegisterTempEntity(netEnt.ID, entity);
	}

	// Token: 0x060032C6 RID: 12998 RVA: 0x001038F4 File Offset: 0x00101AF4
	private global::Entity RegisterTempEntity(global::Entity entity)
	{
		int id = (entity == null) ? -1 : entity.GetEntityId();
		return this.RegisterTempEntity(id, entity);
	}

	// Token: 0x060032C7 RID: 12999 RVA: 0x00103918 File Offset: 0x00101B18
	private global::Entity RegisterTempEntity(int id, global::Entity entity)
	{
		if (entity == null)
		{
			string text = string.Format("{0}.RegisterTempEntity(): Attempting to register an invalid entity! No dbid {1} exists.", this, id);
			TelemetryManager.Client().SendLiveIssue("Gameplay_ZoneManager", text);
			Log.Zone.PrintWarning(text, Array.Empty<object>());
		}
		global::Entity entity2 = null;
		if (!this.m_tempEntityMap.TryGetValue(id, out entity2) && entity != null)
		{
			entity2 = entity.CloneForZoneMgr();
			this.m_tempEntityMap.Add(id, entity2);
		}
		return entity2;
	}

	// Token: 0x060032C8 RID: 13000 RVA: 0x00103984 File Offset: 0x00101B84
	private void PostProcessServerChangeList(ZoneChangeList serverChangeList)
	{
		if (!this.ShouldPostProcessServerChangeList(serverChangeList))
		{
			return;
		}
		if (this.CheckAndIgnoreServerChangeList(serverChangeList))
		{
			return;
		}
		if (this.ReplaceRemoteWeaponInServerChangeList(serverChangeList))
		{
			return;
		}
		this.MergeServerChangeList(serverChangeList);
	}

	// Token: 0x060032C9 RID: 13001 RVA: 0x001039AC File Offset: 0x00101BAC
	private bool ShouldPostProcessServerChangeList(ZoneChangeList changeList)
	{
		List<ZoneChange> changes = changeList.GetChanges();
		for (int i = 0; i < changes.Count; i++)
		{
			if (changes[i].HasDestinationData())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060032CA RID: 13002 RVA: 0x001039E4 File Offset: 0x00101BE4
	private bool CheckAndIgnoreServerChangeList(ZoneChangeList serverChangeList)
	{
		Network.HistBlockStart blockStart = serverChangeList.GetTaskList().GetBlockStart();
		if (blockStart == null)
		{
			return false;
		}
		if (blockStart.BlockType != HistoryBlock.Type.PLAY && blockStart.BlockType != HistoryBlock.Type.MOVE_MINION)
		{
			return false;
		}
		ZoneChangeList zoneChangeList = this.FindLocalChangeListMatchingServerChangeList(serverChangeList);
		if (zoneChangeList == null)
		{
			return false;
		}
		serverChangeList.SetIgnoreCardZoneChanges(true);
		Card localTriggerCard = zoneChangeList.GetLocalTriggerCard();
		if (blockStart.BlockType != HistoryBlock.Type.MOVE_MINION || !(localTriggerCard != null) || localTriggerCard.m_minionWasMovedFromSrcToDst == null)
		{
			goto IL_DE;
		}
		using (List<ZoneChange>.Enumerator enumerator = zoneChangeList.GetChanges().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ZoneChange zoneChange = enumerator.Current;
				if (zoneChange.GetDestinationPosition() == localTriggerCard.m_minionWasMovedFromSrcToDst.m_destinationZonePosition && zoneChange.GetEntity() == localTriggerCard.GetEntity())
				{
					localTriggerCard.m_minionWasMovedFromSrcToDst = null;
					break;
				}
			}
			goto IL_DE;
		}
		IL_BE:
		ZoneChangeList zoneChangeList2 = this.m_localChangeListHistory.Dequeue();
		if (zoneChangeList == zoneChangeList2)
		{
			zoneChangeList.GetLocalTriggerCard().SetPredictedZonePosition(0);
			return true;
		}
		IL_DE:
		if (this.m_localChangeListHistory.Count > 0)
		{
			goto IL_BE;
		}
		return true;
	}

	// Token: 0x060032CB RID: 13003 RVA: 0x00103AF0 File Offset: 0x00101CF0
	private ZoneChangeList FindLocalChangeListMatchingServerChangeList(ZoneChangeList serverChangeList)
	{
		foreach (ZoneChangeList zoneChangeList in this.m_localChangeListHistory)
		{
			int predictedPosition = zoneChangeList.GetPredictedPosition();
			foreach (ZoneChange zoneChange in zoneChangeList.GetChanges())
			{
				global::Entity entity = zoneChange.GetEntity();
				TAG_ZONE destinationZoneTag = zoneChange.GetDestinationZoneTag();
				TAG_ZONE sourceZoneTag = zoneChange.GetSourceZoneTag();
				if (destinationZoneTag != TAG_ZONE.INVALID)
				{
					bool flag = sourceZoneTag != destinationZoneTag;
					List<ZoneChange> changes = serverChangeList.GetChanges();
					for (int i = 0; i < changes.Count; i++)
					{
						ZoneChange zoneChange2 = changes[i];
						global::Entity entity2 = zoneChange2.GetEntity();
						if (entity == entity2)
						{
							if (flag)
							{
								TAG_ZONE destinationZoneTag2 = zoneChange2.GetDestinationZoneTag();
								if (destinationZoneTag != destinationZoneTag2)
								{
									goto IL_12E;
								}
								if (destinationZoneTag == TAG_ZONE.PLAY && entity.HasTag(GAME_TAG.TRANSFORMED_FROM_CARD) && entity.GetTag(GAME_TAG.TRANSFORMED_FROM_CARD) != entity.GetTag(GAME_TAG.DATABASE_ID))
								{
									int tag = entity.GetTag(GAME_TAG.LAST_AFFECTED_BY);
									global::Entity entity3 = GameState.Get().GetEntity(tag);
									if (entity3 != null && GameUtils.TranslateCardIdToDbId(entity3.GetCardId(), false) == 61187)
									{
										goto IL_12E;
									}
								}
							}
							ZoneChange zoneChange3 = this.FindNextDstPosChange(serverChangeList, i, entity2);
							int num = (zoneChange3 == null) ? entity2.GetZonePosition() : zoneChange3.GetDestinationPosition();
							if (predictedPosition == num)
							{
								return zoneChangeList;
							}
						}
						IL_12E:;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x060032CC RID: 13004 RVA: 0x00103CAC File Offset: 0x00101EAC
	private ZoneChange FindNextDstPosChange(ZoneChangeList changeList, int index, global::Entity entity)
	{
		List<ZoneChange> changes = changeList.GetChanges();
		int i = index;
		while (i < changes.Count)
		{
			ZoneChange zoneChange = changes[i];
			if (zoneChange.HasDestinationZoneChange() && i != index)
			{
				return null;
			}
			if (zoneChange.HasDestinationPosition())
			{
				if (zoneChange.GetEntity() != entity)
				{
					return null;
				}
				return zoneChange;
			}
			else
			{
				i++;
			}
		}
		return null;
	}

	// Token: 0x060032CD RID: 13005 RVA: 0x00103D00 File Offset: 0x00101F00
	private bool ReplaceRemoteWeaponInServerChangeList(ZoneChangeList serverChangeList)
	{
		List<ZoneChange> changes = serverChangeList.GetChanges();
		List<ZoneChange> list = changes.FindAll(delegate(ZoneChange change)
		{
			if (!(change.GetDestinationZone() is ZoneWeapon))
			{
				return false;
			}
			PowerTask powerTask2 = change.GetPowerTask();
			return powerTask2 == null || !powerTask2.IsCompleted();
		});
		bool result = false;
		foreach (ZoneChange zoneChange in list)
		{
			Zone destinationZone = zoneChange.GetDestinationZone();
			if (destinationZone.GetCardCount() != 0)
			{
				global::Entity entity = destinationZone.GetCardAtIndex(0).GetEntity();
				bool flag = false;
				foreach (ZoneChange zoneChange2 in changes)
				{
					PowerTask powerTask = zoneChange2.GetPowerTask();
					if (powerTask != null)
					{
						Network.HistTagChange histTagChange = powerTask.GetPower() as Network.HistTagChange;
						if (histTagChange != null && histTagChange.Entity == entity.GetEntityId() && histTagChange.Tag == 360 && histTagChange.Value > 0)
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					int controllerId = entity.GetControllerId();
					Zone destinationZone2 = this.FindZoneForTags(controllerId, TAG_ZONE.GRAVEYARD, TAG_CARDTYPE.WEAPON, entity);
					ZoneChange zoneChange3 = new ZoneChange();
					zoneChange3.SetEntity(entity);
					zoneChange3.SetDestinationZone(destinationZone2);
					zoneChange3.SetDestinationZoneTag(TAG_ZONE.GRAVEYARD);
					zoneChange3.SetDestinationPosition(0);
					zoneChange3.SetParentList(serverChangeList);
					Log.Zone.Print("ZoneMgr.ReplaceRemoteWeaponInServerChangeList() - AddChange() serverChangeList: {0}, graveyardChange: {1}", new object[]
					{
						serverChangeList,
						zoneChange3
					});
					serverChangeList.AddChange(zoneChange3);
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x060032CE RID: 13006 RVA: 0x00103EA8 File Offset: 0x001020A8
	private bool MergeServerChangeList(ZoneChangeList serverChangeList)
	{
		Log.Zone.Print("ZoneMgr.MergeServerChangeList() Start - serverChangeList: {0}, m_tempZoneMap.Count: {1}, m_tempEntityMap.Count: {2}", new object[]
		{
			serverChangeList,
			this.m_tempZoneMap.Count,
			this.m_tempEntityMap.Count
		});
		foreach (Zone zone in this.m_zones)
		{
			if (this.IsZoneInLocalHistory(zone))
			{
				ZoneMgr.TempZone tempZone = this.BuildTempZone(zone);
				this.m_tempZoneMap[zone] = tempZone;
				tempZone.PreprocessChanges();
			}
		}
		List<ZoneChange> changes = serverChangeList.GetChanges();
		for (int i = 0; i < changes.Count; i++)
		{
			ZoneChange change = changes[i];
			this.TempApplyZoneChange(change);
		}
		bool result = false;
		foreach (ZoneMgr.TempZone tempZone2 in this.m_tempZoneMap.Values)
		{
			tempZone2.Sort();
			tempZone2.PostprocessChanges();
			Zone zone2 = tempZone2.GetZone();
			Log.Zone.Print("ZoneMgr.MergeServerChangeList() zone: {0}", new object[]
			{
				zone2
			});
			foreach (Card card in zone2.GetCards())
			{
				Log.Zone.Print("\tzone card: {0}", new object[]
				{
					card
				});
			}
			Log.Zone.Print("ZoneMgr.MergeServerChangeList() tempZone: {0}", new object[]
			{
				tempZone2
			});
			foreach (global::Entity entity in tempZone2.GetEntities())
			{
				Log.Zone.Print("\ttempZone entity: {0}", new object[]
				{
					entity
				});
			}
			for (int j = 1; j < zone2.GetLastPos(); j++)
			{
				Card cardAtPos = zone2.GetCardAtPos(j);
				global::Entity entity2 = cardAtPos.GetEntity();
				if (cardAtPos.GetPredictedZonePosition() != 0)
				{
					int num = this.FindBestInsertionPosition(tempZone2, j - 1, j + 1);
					Log.Zone.Print("ZoneMgr.MergeServerChangeList() InsertEntityAtPos() - tempZone: {0}, insertionPos: {1}, entity: {2}", new object[]
					{
						tempZone2,
						num,
						entity2
					});
					tempZone2.InsertEntityAtPos(num, entity2);
				}
			}
			if (tempZone2.IsModified())
			{
				result = true;
				for (int k = 1; k < tempZone2.GetLastPos(); k++)
				{
					global::Entity entity3 = tempZone2.GetEntityAtPos(k).GetCard().GetEntity();
					ZoneChange zoneChange = new ZoneChange();
					zoneChange.SetEntity(entity3);
					zoneChange.SetDestinationZone(zone2);
					zoneChange.SetDestinationZoneTag(zone2.m_ServerTag);
					zoneChange.SetDestinationPosition(k);
					zoneChange.SetParentList(serverChangeList);
					Log.Zone.Print("ZoneMgr.MergeServerChangeList() - AddChange() tempZone:{0}, serverChangeList: {1}, graveyardChange: {2}", new object[]
					{
						tempZone2,
						serverChangeList,
						zoneChange
					});
					serverChangeList.AddChange(zoneChange);
				}
			}
		}
		this.m_tempZoneMap.Clear();
		this.m_tempEntityMap.Clear();
		return result;
	}

	// Token: 0x060032CF RID: 13007 RVA: 0x00104228 File Offset: 0x00102428
	private bool IsZoneInLocalHistory(Zone zone)
	{
		foreach (ZoneChangeList zoneChangeList in this.m_localChangeListHistory)
		{
			foreach (ZoneChange zoneChange in zoneChangeList.GetChanges())
			{
				Zone sourceZone = zoneChange.GetSourceZone();
				Zone destinationZone = zoneChange.GetDestinationZone();
				if (zone == sourceZone || zone == destinationZone)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060032D0 RID: 13008 RVA: 0x001042D0 File Offset: 0x001024D0
	private void TempApplyZoneChange(ZoneChange change)
	{
		Log.Zone.Print("ZoneMgr.TempApplyZoneChange() - change: {0}, changeList: {1}", new object[]
		{
			change,
			change.GetParentList()
		});
		Network.PowerHistory power = change.GetPowerTask().GetPower();
		global::Entity entity = change.GetEntity();
		global::Entity entity2 = this.RegisterTempEntity(entity);
		if (entity2 == null)
		{
			return;
		}
		if (!change.HasDestinationZoneChange())
		{
			GameUtils.ApplyPower(entity2, power);
			return;
		}
		Zone zone = change.HasSourceZone() ? change.GetSourceZone() : this.FindZoneForEntity(entity2);
		ZoneMgr.TempZone tempZone = this.FindTempZoneForZone(zone);
		if (tempZone != null)
		{
			bool flag = tempZone.RemoveEntity(entity2);
			Log.Zone.Print("ZoneMgr.TempApplyZoneChange() - RemoveEntity() srcTempZone: {0}, tempEntity: {1}, result: {2}", new object[]
			{
				tempZone,
				entity2,
				flag
			});
		}
		GameUtils.ApplyPower(entity2, power);
		Zone destinationZone = change.GetDestinationZone();
		ZoneMgr.TempZone tempZone2 = this.FindTempZoneForZone(destinationZone);
		if (tempZone2 != null)
		{
			tempZone2.AddEntity(entity2);
			Log.Zone.Print("ZoneMgr.TempApplyZoneChange() - AddEntity() dstTempZone: {0}, tempEntity: {1}", new object[]
			{
				tempZone2,
				entity2
			});
		}
	}

	// Token: 0x060032D1 RID: 13009 RVA: 0x001043C8 File Offset: 0x001025C8
	private ZoneMgr.TempZone BuildTempZone(Zone zone)
	{
		ZoneMgr.TempZone tempZone = new ZoneMgr.TempZone();
		tempZone.SetZone(zone);
		List<Card> cards = zone.GetCards();
		for (int i = 0; i < cards.Count; i++)
		{
			Card card = cards[i];
			if (card.GetPredictedZonePosition() == 0 && (!card.IsBeingDragged || zone is ZoneHand))
			{
				global::Entity entity = card.GetEntity();
				global::Entity entity2 = this.RegisterTempEntity(entity);
				if (entity2 != null)
				{
					tempZone.AddInitialEntity(entity2);
				}
			}
		}
		return tempZone;
	}

	// Token: 0x060032D2 RID: 13010 RVA: 0x0010443C File Offset: 0x0010263C
	private ZoneMgr.TempZone FindTempZoneForZone(Zone zone)
	{
		if (zone == null)
		{
			return null;
		}
		ZoneMgr.TempZone result = null;
		this.m_tempZoneMap.TryGetValue(zone, out result);
		return result;
	}

	// Token: 0x060032D3 RID: 13011 RVA: 0x00104468 File Offset: 0x00102668
	private int FindBestInsertionPosition(ZoneMgr.TempZone tempZone, int leftPos, int rightPos)
	{
		Zone zone = tempZone.GetZone();
		int num = 0;
		for (int i = leftPos - 1; i >= 0; i--)
		{
			Card cardAtIndex = zone.GetCardAtIndex(i);
			if (cardAtIndex == null)
			{
				Log.Zone.PrintWarning("{0}.FindBestInsertionPosition(): Bad leftPos value! No card at index {1} currently in Zone {2}.", new object[]
				{
					this,
					i,
					zone
				});
			}
			else
			{
				global::Entity entity = cardAtIndex.GetEntity();
				num = tempZone.FindEntityPosWithReplacements(entity.GetEntityId());
				if (num != 0)
				{
					break;
				}
			}
		}
		int j;
		if (num == 0)
		{
			j = 1;
		}
		else
		{
			int entityId = tempZone.GetEntityAtPos(num).GetEntityId();
			for (j = num + 1; j < tempZone.GetLastPos(); j++)
			{
				global::Entity entityAtPos = tempZone.GetEntityAtPos(j);
				if (entityAtPos.GetCreatorId() != entityId || zone.ContainsCard(entityAtPos.GetCard()))
				{
					break;
				}
			}
		}
		int num2 = 0;
		for (int k = rightPos - 1; k < zone.GetCardCount(); k++)
		{
			Card cardAtIndex2 = zone.GetCardAtIndex(k);
			if (cardAtIndex2 == null)
			{
				Log.Zone.PrintWarning("{0}.FindBestInsertionPosition(): Bad rightPos value! No card at index {1} currently in Zone {2}.", new object[]
				{
					this,
					k,
					zone
				});
			}
			else
			{
				global::Entity entity2 = cardAtIndex2.GetEntity();
				num2 = tempZone.FindEntityPosWithReplacements(entity2.GetEntityId());
				if (num2 != 0)
				{
					break;
				}
			}
		}
		int l;
		if (num2 == 0)
		{
			l = tempZone.GetLastPos();
		}
		else
		{
			int entityId2 = tempZone.GetEntityAtPos(num2).GetEntityId();
			for (l = num2 - 1; l > 0; l--)
			{
				global::Entity entityAtPos2 = tempZone.GetEntityAtPos(l);
				if (entityAtPos2.GetCreatorId() != entityId2 || zone.ContainsCard(entityAtPos2.GetCard()))
				{
					break;
				}
			}
			l++;
		}
		return Mathf.CeilToInt(0.5f * (float)(j + l));
	}

	// Token: 0x060032D4 RID: 13012 RVA: 0x00104609 File Offset: 0x00102809
	private int GetNextLocalChangeListId()
	{
		int nextLocalChangeListId = this.m_nextLocalChangeListId;
		this.m_nextLocalChangeListId = ((this.m_nextLocalChangeListId == int.MaxValue) ? 1 : (this.m_nextLocalChangeListId + 1));
		return nextLocalChangeListId;
	}

	// Token: 0x060032D5 RID: 13013 RVA: 0x0010462F File Offset: 0x0010282F
	private int GetNextServerChangeListId()
	{
		int nextServerChangeListId = this.m_nextServerChangeListId;
		this.m_nextServerChangeListId = ((this.m_nextServerChangeListId == int.MaxValue) ? 1 : (this.m_nextServerChangeListId + 1));
		return nextServerChangeListId;
	}

	// Token: 0x060032D6 RID: 13014 RVA: 0x00104658 File Offset: 0x00102858
	private int FindTriggeredActiveLocalChangeIndex(Card card)
	{
		for (int i = 0; i < this.m_activeLocalChangeLists.Count; i++)
		{
			if (this.m_activeLocalChangeLists[i].GetLocalTriggerCard() == card)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060032D7 RID: 13015 RVA: 0x00104698 File Offset: 0x00102898
	private int FindTriggeredPendingLocalChangeIndex(Card card)
	{
		for (int i = 0; i < this.m_pendingLocalChangeLists.Count; i++)
		{
			if (this.m_pendingLocalChangeLists[i].GetLocalTriggerCard() == card)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060032D8 RID: 13016 RVA: 0x001046D8 File Offset: 0x001028D8
	private void AutoCorrectZonesAfterServerChange()
	{
		if (this.HasActiveLocalChange())
		{
			Log.Zone.Print("ZoneMgr.AutoCorrectZonesAfterServerChange() - HasActiveLocalChange()", Array.Empty<object>());
			return;
		}
		if (this.HasPendingLocalChange())
		{
			Log.Zone.Print("ZoneMgr.AutoCorrectZonesAfterServerChange() - HasPendingLocalChange()", Array.Empty<object>());
			return;
		}
		if (this.HasActiveServerChange())
		{
			Log.Zone.Print("ZoneMgr.AutoCorrectZonesAfterServerChange() - HasActiveServerChange()", Array.Empty<object>());
			return;
		}
		if (this.HasPendingServerChange())
		{
			Log.Zone.Print("ZoneMgr.AutoCorrectZonesAfterServerChange() - HasPendingServerChange()", Array.Empty<object>());
			return;
		}
		if (this.HasPredictedPositions())
		{
			Log.Zone.Print("ZoneMgr.AutoCorrectZonesAfterServerChange() - HasPredictedPositions()", Array.Empty<object>());
			return;
		}
		if (this.HasPredictedCards())
		{
			Log.Zone.Print("ZoneMgr.AutoCorrectZonesAfterServerChange() - HasPredictedCards()", Array.Empty<object>());
			return;
		}
		if (this.HasPredictedMovedMinion())
		{
			Log.Zone.Print("ZoneMgr.AutoCorrectZonesAfterServerChange() - HasPredictedMovedMinion()", Array.Empty<object>());
			return;
		}
		Log.Zone.Print("ZoneMgr.AutoCorrectZonesAfterServerChange()", Array.Empty<object>());
		this.AutoCorrectZones();
	}

	// Token: 0x060032D9 RID: 13017 RVA: 0x001047CC File Offset: 0x001029CC
	private void AutoCorrectZones()
	{
		ZoneChangeList zoneChangeList = null;
		foreach (Zone zone in this.FindZonesOfType<Zone>(global::Player.Side.FRIENDLY))
		{
			foreach (Card card in zone.GetCards())
			{
				global::Entity entity = card.GetEntity();
				TAG_ZONE zone2 = entity.GetZone();
				int controllerId = entity.GetControllerId();
				int zonePosition = entity.GetZonePosition();
				TAG_ZONE serverTag = zone.m_ServerTag;
				int controllerId2 = zone.GetControllerId();
				int zonePosition2 = card.GetZonePosition();
				bool flag = zone2 == serverTag;
				bool flag2 = controllerId == controllerId2;
				bool flag3 = zonePosition == 0 || zonePosition == zonePosition2;
				if (!flag || !flag2 || !flag3)
				{
					if (zoneChangeList == null)
					{
						int nextLocalChangeListId = this.GetNextLocalChangeListId();
						Log.Zone.Print("ZoneMgr.AutoCorrectZones() CreateLocalChangeList - changeListId={0}", new object[]
						{
							nextLocalChangeListId
						});
						zoneChangeList = new ZoneChangeList();
						zoneChangeList.SetId(nextLocalChangeListId);
					}
					ZoneChange zoneChange = new ZoneChange();
					zoneChange.SetEntity(entity);
					zoneChange.SetSourcePosition(zonePosition2);
					zoneChange.SetDestinationZoneTag(zone2);
					zoneChange.SetDestinationZone(this.FindZoneForEntity(entity));
					zoneChange.SetDestinationControllerId(controllerId);
					zoneChange.SetDestinationPosition(zonePosition);
					Log.Zone.Print("ZoneMgr.AutoCorrectZones() - AddChange() changeList: {0}, change: {1}", new object[]
					{
						zoneChangeList,
						zoneChange
					});
					zoneChangeList.AddChange(zoneChange);
				}
			}
		}
		if (zoneChangeList == null)
		{
			return;
		}
		this.ProcessLocalChangeList(zoneChangeList);
	}

	// Token: 0x060032DA RID: 13018 RVA: 0x00104988 File Offset: 0x00102B88
	public void ProcessGeneratedLocalChangeLists(List<ZoneChangeList> generatedChangeLists)
	{
		foreach (ZoneChangeList zoneChangeList in generatedChangeLists)
		{
			int nextLocalChangeListId = this.GetNextLocalChangeListId();
			zoneChangeList.SetId(nextLocalChangeListId);
			this.ProcessLocalChangeList(zoneChangeList);
		}
	}

	// Token: 0x060032DB RID: 13019 RVA: 0x001049E4 File Offset: 0x00102BE4
	public void OnHealingDoesDamageEntityMousedOver()
	{
		foreach (Zone zone in this.FindZonesForSide(global::Player.Side.FRIENDLY))
		{
			zone.OnHealingDoesDamageEntityMousedOver();
		}
	}

	// Token: 0x060032DC RID: 13020 RVA: 0x00104A38 File Offset: 0x00102C38
	public void OnHealingDoesDamageEntityMousedOut()
	{
		foreach (Zone zone in this.FindZonesForSide(global::Player.Side.FRIENDLY))
		{
			zone.OnHealingDoesDamageEntityMousedOut();
		}
	}

	// Token: 0x060032DD RID: 13021 RVA: 0x00104A8C File Offset: 0x00102C8C
	public void OnLifestealDoesDamageEntityMousedOver()
	{
		foreach (Zone zone in this.FindZonesForSide(global::Player.Side.FRIENDLY))
		{
			zone.OnLifestealDoesDamageEntityMousedOver();
		}
	}

	// Token: 0x060032DE RID: 13022 RVA: 0x00104AE0 File Offset: 0x00102CE0
	public void OnLifestealDoesDamageEntityMousedOut()
	{
		foreach (Zone zone in this.FindZonesForSide(global::Player.Side.FRIENDLY))
		{
			zone.OnLifestealDoesDamageEntityMousedOut();
		}
	}

	// Token: 0x060032DF RID: 13023 RVA: 0x00104B34 File Offset: 0x00102D34
	public void OnHealingDoesDamageEntityEnteredPlay()
	{
		foreach (Zone zone in this.FindZonesForSide(global::Player.Side.FRIENDLY))
		{
			zone.OnHealingDoesDamageEntityEnteredPlay();
		}
	}

	// Token: 0x060032E0 RID: 13024 RVA: 0x00104B88 File Offset: 0x00102D88
	public void OnLifestealDoesDamageEntityEnteredPlay()
	{
		foreach (Zone zone in this.FindZonesForSide(global::Player.Side.FRIENDLY))
		{
			zone.OnLifestealDoesDamageEntityEnteredPlay();
		}
	}

	// Token: 0x060032E1 RID: 13025 RVA: 0x00104BDC File Offset: 0x00102DDC
	public void OnSpellPowerEntityMousedOver(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
		foreach (Zone zone in this.FindZonesForSide(global::Player.Side.FRIENDLY))
		{
			zone.OnSpellPowerEntityMousedOver(spellSchool);
		}
	}

	// Token: 0x060032E2 RID: 13026 RVA: 0x00104C30 File Offset: 0x00102E30
	public void OnSpellPowerEntityMousedOut(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
		foreach (Zone zone in this.FindZonesForSide(global::Player.Side.FRIENDLY))
		{
			zone.OnSpellPowerEntityMousedOut(spellSchool);
		}
	}

	// Token: 0x060032E3 RID: 13027 RVA: 0x00104C84 File Offset: 0x00102E84
	public void OnSpellPowerEntityEnteredPlay(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
		foreach (Zone zone in this.FindZonesForSide(global::Player.Side.FRIENDLY))
		{
			zone.OnSpellPowerEntityEnteredPlay(spellSchool);
		}
	}

	// Token: 0x04001BF3 RID: 7155
	private Map<Type, string> m_tweenNames = new Map<Type, string>
	{
		{
			typeof(ZoneHand),
			"ZoneHandUpdateLayout"
		},
		{
			typeof(ZonePlay),
			"ZonePlayUpdateLayout"
		},
		{
			typeof(ZoneWeapon),
			"ZoneWeaponUpdateLayout"
		}
	};

	// Token: 0x04001BF4 RID: 7156
	private static ZoneMgr s_instance;

	// Token: 0x04001BF5 RID: 7157
	private List<Zone> m_zones = new List<Zone>();

	// Token: 0x04001BF6 RID: 7158
	private int m_nextLocalChangeListId = 1;

	// Token: 0x04001BF7 RID: 7159
	private int m_nextServerChangeListId = 1;

	// Token: 0x04001BF8 RID: 7160
	private Queue<ZoneChangeList> m_pendingServerChangeLists = new Queue<ZoneChangeList>();

	// Token: 0x04001BF9 RID: 7161
	private ZoneChangeList m_activeServerChangeList;

	// Token: 0x04001BFA RID: 7162
	private Map<int, global::Entity> m_tempEntityMap = new Map<int, global::Entity>();

	// Token: 0x04001BFB RID: 7163
	private Map<Zone, ZoneMgr.TempZone> m_tempZoneMap = new Map<Zone, ZoneMgr.TempZone>();

	// Token: 0x04001BFC RID: 7164
	private List<ZoneChangeList> m_activeLocalChangeLists = new List<ZoneChangeList>();

	// Token: 0x04001BFD RID: 7165
	private List<ZoneChangeList> m_pendingLocalChangeLists = new List<ZoneChangeList>();

	// Token: 0x04001BFE RID: 7166
	private QueueList<ZoneChangeList> m_localChangeListHistory = new QueueList<ZoneChangeList>();

	// Token: 0x04001BFF RID: 7167
	private float m_nextDeathBlockLayoutDelaySec;

	// Token: 0x02001706 RID: 5894
	// (Invoke) Token: 0x0600E69B RID: 59035
	public delegate void ChangeCompleteCallback(ZoneChangeList changeList, object userData);

	// Token: 0x02001707 RID: 5895
	private class TempZone
	{
		// Token: 0x0600E69E RID: 59038 RVA: 0x00412599 File Offset: 0x00410799
		public Zone GetZone()
		{
			return this.m_zone;
		}

		// Token: 0x0600E69F RID: 59039 RVA: 0x004125A1 File Offset: 0x004107A1
		public void SetZone(Zone zone)
		{
			this.m_zone = zone;
		}

		// Token: 0x0600E6A0 RID: 59040 RVA: 0x004125AA File Offset: 0x004107AA
		public bool IsModified()
		{
			return this.m_modified;
		}

		// Token: 0x0600E6A1 RID: 59041 RVA: 0x004125B2 File Offset: 0x004107B2
		public int GetEntityCount()
		{
			return this.m_entities.Count;
		}

		// Token: 0x0600E6A2 RID: 59042 RVA: 0x004125BF File Offset: 0x004107BF
		public List<global::Entity> GetEntities()
		{
			return this.m_entities;
		}

		// Token: 0x0600E6A3 RID: 59043 RVA: 0x004125C7 File Offset: 0x004107C7
		public global::Entity GetEntityAtIndex(int index)
		{
			if (index < 0)
			{
				return null;
			}
			if (index >= this.m_entities.Count)
			{
				return null;
			}
			return this.m_entities[index];
		}

		// Token: 0x0600E6A4 RID: 59044 RVA: 0x004125EB File Offset: 0x004107EB
		public global::Entity GetEntityAtPos(int pos)
		{
			return this.GetEntityAtIndex(pos - 1);
		}

		// Token: 0x0600E6A5 RID: 59045 RVA: 0x004125F6 File Offset: 0x004107F6
		public void ClearEntities()
		{
			this.m_entities.Clear();
		}

		// Token: 0x0600E6A6 RID: 59046 RVA: 0x00412603 File Offset: 0x00410803
		public void AddInitialEntity(global::Entity entity)
		{
			this.m_entities.Add(entity);
		}

		// Token: 0x0600E6A7 RID: 59047 RVA: 0x00412611 File Offset: 0x00410811
		public bool CanAcceptEntity(global::Entity entity)
		{
			return ZoneMgr.Get().FindZoneForEntityAndZoneTag(entity, this.m_zone.m_ServerTag) == this.m_zone;
		}

		// Token: 0x0600E6A8 RID: 59048 RVA: 0x00412634 File Offset: 0x00410834
		public void AddEntity(global::Entity entity)
		{
			if (!this.CanAcceptEntity(entity))
			{
				return;
			}
			if (this.m_entities.Contains(entity))
			{
				return;
			}
			this.m_entities.Add(entity);
			this.m_modified = true;
		}

		// Token: 0x0600E6A9 RID: 59049 RVA: 0x00412664 File Offset: 0x00410864
		public void InsertEntityAtIndex(int index, global::Entity entity)
		{
			if (!this.CanAcceptEntity(entity))
			{
				return;
			}
			if (index < 0)
			{
				return;
			}
			if (index > this.m_entities.Count)
			{
				return;
			}
			if (index < this.m_entities.Count && this.m_entities[index] == entity)
			{
				return;
			}
			this.m_entities.Insert(index, entity);
			this.m_modified = true;
		}

		// Token: 0x0600E6AA RID: 59050 RVA: 0x004126C4 File Offset: 0x004108C4
		public void InsertEntityAtPos(int pos, global::Entity entity)
		{
			int index = pos - 1;
			this.InsertEntityAtIndex(index, entity);
		}

		// Token: 0x0600E6AB RID: 59051 RVA: 0x004126DD File Offset: 0x004108DD
		public bool RemoveEntity(global::Entity entity)
		{
			if (!this.m_entities.Remove(entity))
			{
				return false;
			}
			this.m_modified = true;
			return true;
		}

		// Token: 0x0600E6AC RID: 59052 RVA: 0x004126F8 File Offset: 0x004108F8
		public bool RemoveEntityById(int entityId)
		{
			global::Entity entity = null;
			foreach (global::Entity entity2 in this.m_entities)
			{
				if (entity2.GetEntityId() == entityId)
				{
					entity = entity2;
					break;
				}
			}
			if (entity == null)
			{
				return false;
			}
			this.m_entities.Remove(entity);
			this.m_modified = true;
			return true;
		}

		// Token: 0x0600E6AD RID: 59053 RVA: 0x00412770 File Offset: 0x00410970
		public int GetLastPos()
		{
			return this.m_entities.Count + 1;
		}

		// Token: 0x0600E6AE RID: 59054 RVA: 0x00412780 File Offset: 0x00410980
		public int FindEntityPos(global::Entity entity)
		{
			return 1 + this.m_entities.FindIndex((global::Entity currEntity) => currEntity == entity);
		}

		// Token: 0x0600E6AF RID: 59055 RVA: 0x004127B3 File Offset: 0x004109B3
		public bool ContainsEntity(global::Entity entity)
		{
			return this.FindEntityPos(entity) > 0;
		}

		// Token: 0x0600E6B0 RID: 59056 RVA: 0x004127C0 File Offset: 0x004109C0
		public int FindEntityPos(int entityId)
		{
			return 1 + this.m_entities.FindIndex((global::Entity currEntity) => currEntity.GetEntityId() == entityId);
		}

		// Token: 0x0600E6B1 RID: 59057 RVA: 0x004127F3 File Offset: 0x004109F3
		public bool ContainsEntity(int entityId)
		{
			return this.FindEntityPos(entityId) > 0;
		}

		// Token: 0x0600E6B2 RID: 59058 RVA: 0x00412800 File Offset: 0x00410A00
		public int FindEntityPosWithReplacements(int entityId)
		{
			Predicate<global::Entity> <>9__0;
			while (entityId != 0)
			{
				int num = 1;
				List<global::Entity> entities = this.m_entities;
				Predicate<global::Entity> match;
				if ((match = <>9__0) == null)
				{
					match = (<>9__0 = ((global::Entity currEntity) => currEntity.GetEntityId() == entityId));
				}
				int num2 = num + entities.FindIndex(match);
				if (num2 > 0)
				{
					return num2;
				}
				this.m_replacedEntities.TryGetValue(entityId, out entityId);
			}
			return 0;
		}

		// Token: 0x0600E6B3 RID: 59059 RVA: 0x00412870 File Offset: 0x00410A70
		public void Sort()
		{
			if (this.m_modified)
			{
				this.m_entities.Sort(new Comparison<global::Entity>(this.SortComparison));
				return;
			}
			global::Entity[] array = this.m_entities.ToArray();
			this.m_entities.Sort(new Comparison<global::Entity>(this.SortComparison));
			for (int i = 0; i < this.m_entities.Count; i++)
			{
				if (array[i] != this.m_entities[i])
				{
					this.m_modified = true;
					return;
				}
			}
		}

		// Token: 0x0600E6B4 RID: 59060 RVA: 0x004128F0 File Offset: 0x00410AF0
		public void PreprocessChanges()
		{
			this.m_prevEntities.Clear();
			for (int i = 0; i < this.m_entities.Count; i++)
			{
				this.m_prevEntities.Add(this.m_entities[i]);
			}
		}

		// Token: 0x0600E6B5 RID: 59061 RVA: 0x00412938 File Offset: 0x00410B38
		public void PostprocessChanges()
		{
			for (int i = 0; i < this.m_prevEntities.Count; i++)
			{
				if (i >= this.m_entities.Count)
				{
					break;
				}
				global::Entity prevEntity = this.m_prevEntities[i];
				if (this.m_entities.FindIndex((global::Entity currEntity) => currEntity == prevEntity) < 0)
				{
					global::Entity entity = this.m_entities[i];
					if (!this.m_prevEntities.Contains(entity))
					{
						this.m_replacedEntities[prevEntity.GetEntityId()] = entity.GetEntityId();
					}
				}
			}
		}

		// Token: 0x0600E6B6 RID: 59062 RVA: 0x004129D5 File Offset: 0x00410BD5
		public override string ToString()
		{
			return string.Format("{0} ({1} entities)", this.m_zone, this.m_entities.Count);
		}

		// Token: 0x0600E6B7 RID: 59063 RVA: 0x004129F8 File Offset: 0x00410BF8
		private int SortComparison(global::Entity entity1, global::Entity entity2)
		{
			int zonePosition = entity1.GetZonePosition();
			int zonePosition2 = entity2.GetZonePosition();
			return zonePosition - zonePosition2;
		}

		// Token: 0x0400B35D RID: 45917
		private Zone m_zone;

		// Token: 0x0400B35E RID: 45918
		private bool m_modified;

		// Token: 0x0400B35F RID: 45919
		private List<global::Entity> m_prevEntities = new List<global::Entity>();

		// Token: 0x0400B360 RID: 45920
		private List<global::Entity> m_entities = new List<global::Entity>();

		// Token: 0x0400B361 RID: 45921
		private Map<int, int> m_replacedEntities = new Map<int, int>();
	}
}
