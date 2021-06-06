using System;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

public class ZoneMgr : MonoBehaviour
{
	public delegate void ChangeCompleteCallback(ZoneChangeList changeList, object userData);

	private class TempZone
	{
		private Zone m_zone;

		private bool m_modified;

		private List<Entity> m_prevEntities = new List<Entity>();

		private List<Entity> m_entities = new List<Entity>();

		private Map<int, int> m_replacedEntities = new Map<int, int>();

		public Zone GetZone()
		{
			return m_zone;
		}

		public void SetZone(Zone zone)
		{
			m_zone = zone;
		}

		public bool IsModified()
		{
			return m_modified;
		}

		public int GetEntityCount()
		{
			return m_entities.Count;
		}

		public List<Entity> GetEntities()
		{
			return m_entities;
		}

		public Entity GetEntityAtIndex(int index)
		{
			if (index < 0)
			{
				return null;
			}
			if (index >= m_entities.Count)
			{
				return null;
			}
			return m_entities[index];
		}

		public Entity GetEntityAtPos(int pos)
		{
			return GetEntityAtIndex(pos - 1);
		}

		public void ClearEntities()
		{
			m_entities.Clear();
		}

		public void AddInitialEntity(Entity entity)
		{
			m_entities.Add(entity);
		}

		public bool CanAcceptEntity(Entity entity)
		{
			return Get().FindZoneForEntityAndZoneTag(entity, m_zone.m_ServerTag) == m_zone;
		}

		public void AddEntity(Entity entity)
		{
			if (CanAcceptEntity(entity) && !m_entities.Contains(entity))
			{
				m_entities.Add(entity);
				m_modified = true;
			}
		}

		public void InsertEntityAtIndex(int index, Entity entity)
		{
			if (CanAcceptEntity(entity) && index >= 0 && index <= m_entities.Count && (index >= m_entities.Count || m_entities[index] != entity))
			{
				m_entities.Insert(index, entity);
				m_modified = true;
			}
		}

		public void InsertEntityAtPos(int pos, Entity entity)
		{
			int index = pos - 1;
			InsertEntityAtIndex(index, entity);
		}

		public bool RemoveEntity(Entity entity)
		{
			if (!m_entities.Remove(entity))
			{
				return false;
			}
			m_modified = true;
			return true;
		}

		public bool RemoveEntityById(int entityId)
		{
			Entity entity = null;
			foreach (Entity entity2 in m_entities)
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
			m_entities.Remove(entity);
			m_modified = true;
			return true;
		}

		public int GetLastPos()
		{
			return m_entities.Count + 1;
		}

		public int FindEntityPos(Entity entity)
		{
			return 1 + m_entities.FindIndex((Entity currEntity) => currEntity == entity);
		}

		public bool ContainsEntity(Entity entity)
		{
			return FindEntityPos(entity) > 0;
		}

		public int FindEntityPos(int entityId)
		{
			return 1 + m_entities.FindIndex((Entity currEntity) => currEntity.GetEntityId() == entityId);
		}

		public bool ContainsEntity(int entityId)
		{
			return FindEntityPos(entityId) > 0;
		}

		public int FindEntityPosWithReplacements(int entityId)
		{
			while (entityId != 0)
			{
				int num = 1 + m_entities.FindIndex((Entity currEntity) => currEntity.GetEntityId() == entityId);
				if (num > 0)
				{
					return num;
				}
				m_replacedEntities.TryGetValue(entityId, out entityId);
			}
			return 0;
		}

		public void Sort()
		{
			if (m_modified)
			{
				m_entities.Sort(SortComparison);
				return;
			}
			Entity[] array = m_entities.ToArray();
			m_entities.Sort(SortComparison);
			for (int i = 0; i < m_entities.Count; i++)
			{
				if (array[i] != m_entities[i])
				{
					m_modified = true;
					break;
				}
			}
		}

		public void PreprocessChanges()
		{
			m_prevEntities.Clear();
			for (int i = 0; i < m_entities.Count; i++)
			{
				m_prevEntities.Add(m_entities[i]);
			}
		}

		public void PostprocessChanges()
		{
			for (int i = 0; i < m_prevEntities.Count; i++)
			{
				if (i >= m_entities.Count)
				{
					break;
				}
				Entity prevEntity = m_prevEntities[i];
				if (m_entities.FindIndex((Entity currEntity) => currEntity == prevEntity) < 0)
				{
					Entity entity = m_entities[i];
					if (!m_prevEntities.Contains(entity))
					{
						m_replacedEntities[prevEntity.GetEntityId()] = entity.GetEntityId();
					}
				}
			}
		}

		public override string ToString()
		{
			return $"{m_zone} ({m_entities.Count} entities)";
		}

		private int SortComparison(Entity entity1, Entity entity2)
		{
			int zonePosition = entity1.GetZonePosition();
			int zonePosition2 = entity2.GetZonePosition();
			return zonePosition - zonePosition2;
		}
	}

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

	private static ZoneMgr s_instance;

	private List<Zone> m_zones = new List<Zone>();

	private int m_nextLocalChangeListId = 1;

	private int m_nextServerChangeListId = 1;

	private Queue<ZoneChangeList> m_pendingServerChangeLists = new Queue<ZoneChangeList>();

	private ZoneChangeList m_activeServerChangeList;

	private Map<int, Entity> m_tempEntityMap = new Map<int, Entity>();

	private Map<Zone, TempZone> m_tempZoneMap = new Map<Zone, TempZone>();

	private List<ZoneChangeList> m_activeLocalChangeLists = new List<ZoneChangeList>();

	private List<ZoneChangeList> m_pendingLocalChangeLists = new List<ZoneChangeList>();

	private QueueList<ZoneChangeList> m_localChangeListHistory = new QueueList<ZoneChangeList>();

	private float m_nextDeathBlockLayoutDelaySec;

	private void Awake()
	{
		s_instance = this;
		Zone[] componentsInChildren = base.gameObject.GetComponentsInChildren<Zone>();
		foreach (Zone item in componentsInChildren)
		{
			m_zones.Add(item);
		}
		if (GameState.Get() != null)
		{
			GameState.Get().RegisterCurrentPlayerChangedListener(OnCurrentPlayerChanged);
			GameState.Get().RegisterOptionRejectedListener(OnOptionRejected);
		}
	}

	private void Start()
	{
		InputManager inputManager = InputManager.Get();
		if (inputManager != null)
		{
			inputManager.StartWatchingForInput();
		}
	}

	private void Update()
	{
		UpdateLocalChangeLists();
		UpdateServerChangeLists();
	}

	private void OnDestroy()
	{
		if (GameState.Get() != null)
		{
			GameState.Get().UnregisterCurrentPlayerChangedListener(OnCurrentPlayerChanged);
			GameState.Get().UnregisterOptionRejectedListener(OnOptionRejected);
		}
		s_instance = null;
	}

	public static ZoneMgr Get()
	{
		return s_instance;
	}

	public List<Zone> GetZones()
	{
		return m_zones;
	}

	public Zone FindZoneForTags(int controllerId, TAG_ZONE zoneTag, TAG_CARDTYPE cardType, Entity entity)
	{
		if (controllerId == 0)
		{
			return null;
		}
		if (zoneTag == TAG_ZONE.INVALID)
		{
			return null;
		}
		foreach (Zone zone in m_zones)
		{
			if (zone.CanAcceptTags(controllerId, zoneTag, cardType, entity))
			{
				return zone;
			}
		}
		return null;
	}

	public Zone FindZoneForEntity(Entity entity)
	{
		if (entity.GetZone() == TAG_ZONE.INVALID)
		{
			return null;
		}
		foreach (Zone zone in m_zones)
		{
			if (zone.CanAcceptTags(entity.GetControllerId(), entity.GetZone(), entity.GetCardType(), entity))
			{
				return zone;
			}
		}
		return null;
	}

	public Zone FindZoneForEntityAndZoneTag(Entity entity, TAG_ZONE zoneTag)
	{
		if (zoneTag == TAG_ZONE.INVALID)
		{
			return null;
		}
		foreach (Zone zone in m_zones)
		{
			if (zone.CanAcceptTags(entity.GetControllerId(), zoneTag, entity.GetCardType(), entity))
			{
				return zone;
			}
		}
		return null;
	}

	public Zone FindZoneForEntityAndController(Entity entity, int controllerId)
	{
		foreach (Zone zone in m_zones)
		{
			if (zone.CanAcceptTags(controllerId, entity.GetZone(), entity.GetCardType(), entity))
			{
				return zone;
			}
		}
		return null;
	}

	public Zone FindZoneForFullEntity(Network.HistFullEntity fullEntity)
	{
		int controllerId = 0;
		TAG_ZONE zoneTag = TAG_ZONE.INVALID;
		TAG_CARDTYPE cardType = TAG_CARDTYPE.INVALID;
		foreach (Network.Entity.Tag tag in fullEntity.Entity.Tags)
		{
			switch (tag.Name)
			{
			case 49:
				zoneTag = (TAG_ZONE)tag.Value;
				break;
			case 50:
				controllerId = tag.Value;
				break;
			case 202:
				cardType = (TAG_CARDTYPE)tag.Value;
				break;
			}
		}
		foreach (Zone zone in m_zones)
		{
			if (zone.CanAcceptTags(controllerId, zoneTag, cardType, null))
			{
				return zone;
			}
		}
		return null;
	}

	public Zone FindZoneForShowEntity(Entity entity, Network.HistShowEntity showEntity)
	{
		int controllerId = entity.GetControllerId();
		TAG_ZONE zoneTag = entity.GetZone();
		TAG_CARDTYPE cardType = entity.GetCardType();
		foreach (Network.Entity.Tag tag in showEntity.Entity.Tags)
		{
			switch (tag.Name)
			{
			case 49:
				zoneTag = (TAG_ZONE)tag.Value;
				break;
			case 50:
				controllerId = tag.Value;
				break;
			case 202:
				cardType = (TAG_CARDTYPE)tag.Value;
				break;
			}
		}
		foreach (Zone zone in m_zones)
		{
			if (zone.CanAcceptTags(controllerId, zoneTag, cardType, null))
			{
				return zone;
			}
		}
		return null;
	}

	public T FindZoneOfType<T>(Player.Side side) where T : Zone
	{
		Type typeFromHandle = typeof(T);
		foreach (Zone zone in m_zones)
		{
			if (!(zone.GetType() != typeFromHandle) && zone.m_Side == side)
			{
				return (T)zone;
			}
		}
		return null;
	}

	public List<Zone> FindZonesForSide(Player.Side playerSide)
	{
		return FindZonesOfType<Zone>(playerSide);
	}

	public List<T> FindZonesOfType<T>() where T : Zone
	{
		return FindZonesOfType<T, T>();
	}

	public List<ReturnType> FindZonesOfType<ReturnType, ArgType>() where ReturnType : Zone where ArgType : Zone
	{
		List<ReturnType> list = new List<ReturnType>();
		Type typeFromHandle = typeof(ArgType);
		foreach (Zone zone in m_zones)
		{
			if (!(zone.GetType() != typeFromHandle))
			{
				list.Add((ReturnType)zone);
			}
		}
		return list;
	}

	public List<T> FindZonesOfType<T>(Player.Side side) where T : Zone
	{
		return FindZonesOfType<T, T>(side);
	}

	public List<ReturnType> FindZonesOfType<ReturnType, ArgType>(Player.Side side) where ReturnType : Zone where ArgType : Zone
	{
		List<ReturnType> list = new List<ReturnType>();
		foreach (Zone zone in m_zones)
		{
			if (zone is ArgType && zone.m_Side == side)
			{
				list.Add((ReturnType)zone);
			}
		}
		return list;
	}

	public List<Zone> FindZonesForTag(TAG_ZONE zoneTag)
	{
		List<Zone> list = new List<Zone>();
		foreach (Zone zone in m_zones)
		{
			if (zone.m_ServerTag == zoneTag)
			{
				list.Add(zone);
			}
		}
		return list;
	}

	public Map<Type, string> GetTweenNames()
	{
		return m_tweenNames;
	}

	public string GetTweenName<T>() where T : Zone
	{
		Type typeFromHandle = typeof(T);
		string value = "";
		m_tweenNames.TryGetValue(typeFromHandle, out value);
		return value;
	}

	public void RequestNextDeathBlockLayoutDelaySec(float sec)
	{
		m_nextDeathBlockLayoutDelaySec = Mathf.Max(m_nextDeathBlockLayoutDelaySec, sec);
	}

	public float RemoveNextDeathBlockLayoutDelaySec()
	{
		float nextDeathBlockLayoutDelaySec = m_nextDeathBlockLayoutDelaySec;
		m_nextDeathBlockLayoutDelaySec = 0f;
		return nextDeathBlockLayoutDelaySec;
	}

	public int PredictZonePosition(Zone zone, int pos)
	{
		TempZone tempZone = BuildTempZone(zone);
		PredictZoneFromPowerProcessor(tempZone);
		RemoveDraggedMinionsFromTempZone(zone, tempZone);
		int result = FindBestInsertionPosition(tempZone, pos - 1, pos);
		m_tempZoneMap.Clear();
		m_tempEntityMap.Clear();
		return result;
	}

	private void RemoveDraggedMinionsFromTempZone(Zone originalZone, TempZone tempZone)
	{
		foreach (Card card in originalZone.GetCards())
		{
			if (card.IsBeingDragged)
			{
				tempZone.RemoveEntityById(card.GetEntity().GetEntityId());
			}
		}
	}

	public bool HasPredictedCards()
	{
		if (HasPredictedCards<ZoneSecret>(TAG_ZONE.SECRET))
		{
			return true;
		}
		if (HasPredictedCards<ZoneWeapon>(TAG_ZONE.PLAY))
		{
			return true;
		}
		if (HasPredictedCards<ZoneHero>(TAG_ZONE.PLAY))
		{
			return true;
		}
		if (HasPredictedCards<ZoneGraveyard>(TAG_ZONE.GRAVEYARD))
		{
			return true;
		}
		return false;
	}

	public bool HasPredictedMovedMinion()
	{
		foreach (Zone item in FindZonesOfType<Zone>(Player.Side.FRIENDLY))
		{
			foreach (Card card in item.GetCards())
			{
				if (card.m_minionWasMovedFromSrcToDst != null)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool HasPredictedPositions()
	{
		foreach (Zone item in FindZonesOfType<Zone>(Player.Side.FRIENDLY))
		{
			foreach (Card card in item.GetCards())
			{
				if (card.GetPredictedZonePosition() != 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool HasPredictedCards<T>(TAG_ZONE predictedZone) where T : Zone
	{
		foreach (T item in FindZonesOfType<T>(Player.Side.FRIENDLY))
		{
			foreach (Card card in item.GetCards())
			{
				if (card.GetEntity().GetZone() != predictedZone)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool HasActiveLocalChange()
	{
		return m_activeLocalChangeLists.Count > 0;
	}

	public bool HasPendingLocalChange()
	{
		return m_pendingLocalChangeLists.Count > 0;
	}

	public bool HasUnresolvedLocalChange()
	{
		return m_localChangeListHistory.Count > 0;
	}

	public bool HasTriggeredActiveLocalChange(Card card)
	{
		return FindTriggeredActiveLocalChangeIndex(card) >= 0;
	}

	public ZoneChangeList AddLocalZoneChange(Card triggerCard, TAG_ZONE zoneTag)
	{
		Entity entity = triggerCard.GetEntity();
		Zone destinationZone = FindZoneForEntityAndZoneTag(entity, zoneTag);
		return AddLocalZoneChange(triggerCard, destinationZone, zoneTag, 0, null, null);
	}

	public ZoneChangeList AddLocalZoneChange(Card triggerCard, Zone destinationZone, int destinationPos)
	{
		if (destinationZone == null)
		{
			Debug.LogWarning($"ZoneMgr.AddLocalZoneChange() - illegal zone change to null zone for card {triggerCard}");
			return null;
		}
		return AddLocalZoneChange(triggerCard, destinationZone, destinationZone.m_ServerTag, destinationPos, null, null);
	}

	public ZoneChangeList AddLocalZoneChange(Card triggerCard, Zone destinationZone, TAG_ZONE destinationZoneTag, int destinationPos, ChangeCompleteCallback callback, object userData)
	{
		if (destinationZoneTag == TAG_ZONE.INVALID)
		{
			Debug.LogWarning($"ZoneMgr.AddLocalZoneChange() - illegal zone change to {destinationZoneTag} for card {triggerCard}");
			return null;
		}
		if ((destinationZone is ZonePlay || destinationZone is ZoneHand) && destinationPos <= 0)
		{
			Debug.LogWarning($"ZoneMgr.AddLocalZoneChange() - destinationPos {destinationPos} is too small for zone {destinationZone}, min is 1");
			return null;
		}
		ZoneChangeList zoneChangeList = CreateLocalChangeList(triggerCard, destinationZone, destinationZoneTag, destinationPos, callback, userData);
		ProcessOrEnqueueLocalChangeList(zoneChangeList);
		m_localChangeListHistory.Enqueue(zoneChangeList);
		return zoneChangeList;
	}

	public ZoneChangeList AddPredictedLocalZoneChange(Card triggerCard, Zone destinationZone, int destinationPos, int predictedPos)
	{
		if (triggerCard == null)
		{
			Debug.LogWarning($"ZoneMgr.AddPredictedLocalZoneChange() - triggerCard is null");
			return null;
		}
		ZoneChangeList zoneChangeList = AddLocalZoneChange(triggerCard, destinationZone, destinationPos);
		if (zoneChangeList == null)
		{
			return null;
		}
		triggerCard.SetPredictedZonePosition(predictedPos);
		zoneChangeList.SetPredictedPosition(predictedPos);
		return zoneChangeList;
	}

	public ZoneChangeList CancelLocalZoneChange(ZoneChangeList changeList, ChangeCompleteCallback callback = null, object userData = null)
	{
		if (changeList == null)
		{
			Debug.LogWarning($"ZoneMgr.CancelLocalZoneChange() - changeList is null");
			return null;
		}
		if (!m_localChangeListHistory.Remove(changeList))
		{
			Debug.LogWarning($"ZoneMgr.CancelLocalZoneChange() - changeList {changeList.GetId()} is not in history");
			return null;
		}
		ZoneChange localTriggerChange = changeList.GetLocalTriggerChange();
		Entity entity = localTriggerChange.GetEntity();
		Card card = entity.GetCard();
		Zone sourceZone = localTriggerChange.GetSourceZone();
		int sourcePosition = localTriggerChange.GetSourcePosition();
		ZoneChangeList zoneChangeList = CreateLocalChangeList(card, sourceZone, sourceZone.m_ServerTag, sourcePosition, callback, userData);
		if (entity.IsHero())
		{
			AddOldHeroCanceledChange(zoneChangeList, card);
		}
		zoneChangeList.SetCanceledChangeList(canceledChangeList: true);
		zoneChangeList.SetZoneInputBlocking(block: true);
		ProcessOrEnqueueLocalChangeList(zoneChangeList);
		return zoneChangeList;
	}

	private void AddOldHeroCanceledChange(ZoneChangeList canceledChangeList, Card triggerCard)
	{
		Player controller = triggerCard.GetController();
		Card heroCard = controller.GetHeroCard();
		ZoneChange zoneChange = new ZoneChange();
		zoneChange.SetParentList(canceledChangeList);
		zoneChange.SetEntity(heroCard.GetEntity());
		zoneChange.SetDestinationZone(controller.GetHeroZone());
		zoneChange.SetDestinationZoneTag(controller.GetHeroZone().m_ServerTag);
		zoneChange.SetDestinationPosition(0);
		Log.Zone.Print("ZoneMgr.CreateLocalChangesFromTrigger() - AddChange() canceledChangeList: {0},  triggerChange: {1}", canceledChangeList, zoneChange);
		canceledChangeList.AddChange(zoneChange);
	}

	public static bool IsHandledPower(Network.PowerHistory power)
	{
		switch (power.Type)
		{
		case Network.PowerType.FULL_ENTITY:
		{
			Network.HistFullEntity obj = power as Network.HistFullEntity;
			bool result = false;
			{
				foreach (Network.Entity.Tag tag in obj.Entity.Tags)
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
				Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
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

	public bool HasActiveServerChange()
	{
		return m_activeServerChangeList != null;
	}

	public bool HasPendingServerChange()
	{
		return m_pendingServerChangeLists.Count > 0;
	}

	public ZoneChangeList AddServerZoneChanges(PowerTaskList taskList, int taskStartIndex, int taskEndIndex, ChangeCompleteCallback callback, object userData)
	{
		int nextServerChangeListId = GetNextServerChangeListId();
		ZoneChangeList zoneChangeList = new ZoneChangeList();
		zoneChangeList.SetId(nextServerChangeListId);
		zoneChangeList.SetTaskList(taskList);
		zoneChangeList.SetCompleteCallback(callback);
		zoneChangeList.SetCompleteCallbackUserData(userData);
		Log.Zone.Print("ZoneMgr.AddServerZoneChanges() - taskListId={0} changeListId={1} taskStart={2} taskEnd={3}", taskList.GetId(), nextServerChangeListId, taskStartIndex, taskEndIndex);
		List<PowerTask> taskList2 = taskList.GetTaskList();
		for (int i = taskStartIndex; i <= taskEndIndex; i++)
		{
			PowerTask powerTask = taskList2[i];
			Network.PowerHistory power = powerTask.GetPower();
			Network.PowerType type = power.Type;
			ZoneChange zoneChange = null;
			switch (type)
			{
			case Network.PowerType.FULL_ENTITY:
			{
				Network.HistFullEntity fullEntity = (Network.HistFullEntity)power;
				zoneChange = CreateZoneChangeFromFullEntity(fullEntity);
				break;
			}
			case Network.PowerType.SHOW_ENTITY:
			{
				Network.HistShowEntity histShowEntity = (Network.HistShowEntity)power;
				zoneChange = CreateZoneChangeFromEntity(histShowEntity.Entity);
				break;
			}
			case Network.PowerType.CHANGE_ENTITY:
			{
				Network.HistChangeEntity histChangeEntity = (Network.HistChangeEntity)power;
				zoneChange = CreateZoneChangeFromEntity(histChangeEntity.Entity);
				break;
			}
			case Network.PowerType.HIDE_ENTITY:
			{
				Network.HistHideEntity hideEntity = (Network.HistHideEntity)power;
				zoneChange = CreateZoneChangeFromHideEntity(hideEntity);
				break;
			}
			case Network.PowerType.TAG_CHANGE:
			{
				Network.HistTagChange tagChange = (Network.HistTagChange)power;
				zoneChange = CreateZoneChangeFromTagChange(tagChange);
				break;
			}
			case Network.PowerType.META_DATA:
			{
				Network.HistMetaData metaData = (Network.HistMetaData)power;
				zoneChange = CreateZoneChangeFromMetaData(metaData);
				break;
			}
			case Network.PowerType.CREATE_GAME:
			case Network.PowerType.RESET_GAME:
			case Network.PowerType.SUB_SPELL_START:
			case Network.PowerType.SUB_SPELL_END:
			case Network.PowerType.VO_SPELL:
			case Network.PowerType.CACHED_TAG_FOR_DORMANT_CHANGE:
			case Network.PowerType.SHUFFLE_DECK:
				zoneChange = CreateZoneChangeForNonZoneTask();
				break;
			default:
				Debug.LogError($"ZoneMgr.AddServerZoneChanges() - id={zoneChangeList.GetId()} received unhandled power of type {type}");
				return null;
			}
			if (zoneChange != null)
			{
				zoneChange.SetParentList(zoneChangeList);
				zoneChange.SetPowerTask(powerTask);
				Log.Zone.Print("ZoneMgr.AddServerZoneChanges() - AddChange() changeList: {0},  change: {1}", zoneChangeList, zoneChange);
				zoneChangeList.AddChange(zoneChange);
			}
		}
		for (int j = 0; j < zoneChangeList.GetChanges().Count; j++)
		{
			ZoneChange zoneChange2 = zoneChangeList.GetChanges()[j];
			Network.HistMetaData histMetaData = zoneChange2.GetPowerTask().GetPower() as Network.HistMetaData;
			if (histMetaData == null || histMetaData.MetaType != HistoryMeta.Type.CONTROLLER_AND_ZONE_CHANGE)
			{
				continue;
			}
			if (histMetaData.Info.Count != 5)
			{
				Log.Zone.PrintError("CONTROLLER_AND_ZONE_CHANGE MetaData task found ({0}), but info array isn't of size 5!");
			}
			ZoneChange zoneChange3 = null;
			ZoneChange zoneChange4 = null;
			int num = histMetaData.Info[1];
			int num2 = histMetaData.Info[2];
			TAG_ZONE zoneTag = (TAG_ZONE)histMetaData.Info[3];
			TAG_ZONE tAG_ZONE = (TAG_ZONE)histMetaData.Info[4];
			for (int k = j + 1; k < zoneChangeList.GetChanges().Count; k++)
			{
				ZoneChange zoneChange5 = zoneChangeList.GetChanges()[k];
				if (zoneChange5.GetEntity() == zoneChange2.GetEntity())
				{
					if (zoneChange5.HasDestinationControllerId() && zoneChange5.GetDestinationControllerId() == num2 && zoneChange5.GetDestinationZoneTag() != tAG_ZONE)
					{
						zoneChange3 = zoneChange5;
					}
					else if (zoneChange5.HasDestinationControllerId() && zoneChange5.GetDestinationControllerId() == num2 && zoneChange5.HasDestinationZoneChange() && zoneChange5.GetDestinationZoneTag() == tAG_ZONE)
					{
						zoneChange3 = zoneChange5;
						zoneChange4 = zoneChange5;
					}
					else if (!zoneChange5.HasDestinationControllerId() && zoneChange5.HasDestinationZoneChange() && zoneChange5.GetDestinationZoneTag() == tAG_ZONE)
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
				Entity entity = zoneChange3.GetEntity();
				Zone sourceZone = FindZoneForTags(num, zoneTag, entity.GetCardType(), entity);
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
				Log.Zone.PrintError("CONTROLLER_AND_ZONE_CHANGE MetaData task found ({0}), but couldn't find both controller ({1}) and zone ({2}) changes in tasklist!", zoneChange2, zoneChange3, zoneChange4);
			}
		}
		m_tempEntityMap.Clear();
		m_pendingServerChangeLists.Enqueue(zoneChangeList);
		return zoneChangeList;
	}

	private void UpdateLocalChangeLists()
	{
		List<ZoneChangeList> list = null;
		int num = 0;
		while (num < m_activeLocalChangeLists.Count)
		{
			ZoneChangeList zoneChangeList = m_activeLocalChangeLists[num];
			if (!zoneChangeList.IsComplete())
			{
				num++;
				continue;
			}
			zoneChangeList.FireCompleteCallback();
			m_activeLocalChangeLists.RemoveAt(num);
			if (list == null)
			{
				list = new List<ZoneChangeList>();
			}
			list.Add(zoneChangeList);
		}
		if (list == null)
		{
			return;
		}
		foreach (ZoneChangeList item in list)
		{
			ZoneChange localTriggerChange = item.GetLocalTriggerChange();
			Card card = localTriggerChange.GetEntity().GetCard();
			if (item.IsCanceledChangeList())
			{
				card.SetPredictedZonePosition(0);
				if (card.m_minionWasMovedFromSrcToDst != null && card.m_minionWasMovedFromSrcToDst.m_destinationZonePosition == localTriggerChange.GetDestinationPosition())
				{
					card.m_minionWasMovedFromSrcToDst = null;
				}
			}
			int num2 = FindTriggeredPendingLocalChangeIndex(card);
			if (num2 >= 0)
			{
				ZoneChangeList zoneChangeList2 = m_pendingLocalChangeLists[num2];
				m_pendingLocalChangeLists.RemoveAt(num2);
				CreateLocalChangesFromTrigger(zoneChangeList2, zoneChangeList2.GetLocalTriggerChange());
				ProcessLocalChangeList(zoneChangeList2);
			}
		}
	}

	private void UpdateServerChangeLists()
	{
		if (m_activeServerChangeList != null && m_activeServerChangeList.IsComplete())
		{
			m_activeServerChangeList.FireCompleteCallback();
			m_activeServerChangeList = null;
			AutoCorrectZonesAfterServerChange();
		}
		if (HasPendingServerChange() && !HasActiveServerChange())
		{
			m_activeServerChangeList = m_pendingServerChangeLists.Dequeue();
			PostProcessServerChangeList(m_activeServerChangeList);
			StartCoroutine(m_activeServerChangeList.ProcessChanges());
		}
	}

	private bool HasLocalChangeExitingZone(Entity entity, Zone zone)
	{
		if (HasLocalChangeExitingZone(entity, zone, m_activeLocalChangeLists))
		{
			return true;
		}
		if (HasLocalChangeExitingZone(entity, zone, m_pendingLocalChangeLists))
		{
			return true;
		}
		return false;
	}

	private bool HasLocalChangeExitingZone(Entity entity, Zone zone, List<ZoneChangeList> changeLists)
	{
		TAG_ZONE serverTag = zone.m_ServerTag;
		foreach (ZoneChangeList changeList in changeLists)
		{
			foreach (ZoneChange change in changeList.GetChanges())
			{
				if (entity == change.GetEntity() && serverTag == change.GetSourceZoneTag() && serverTag != change.GetDestinationZoneTag())
				{
					return true;
				}
			}
		}
		return false;
	}

	private void PredictZoneFromPowerProcessor(TempZone tempZone)
	{
		PowerProcessor powerProcessor = GameState.Get().GetPowerProcessor();
		tempZone.PreprocessChanges();
		powerProcessor.ForEachTaskList(delegate(int queueIndex, PowerTaskList taskList)
		{
			PredictZoneFromPowerTaskList(tempZone, taskList);
		});
		tempZone.Sort();
		tempZone.PostprocessChanges();
	}

	private void PredictZoneFromPowerTaskList(TempZone tempZone, PowerTaskList taskList)
	{
		List<PowerTask> taskList2 = taskList.GetTaskList();
		for (int i = 0; i < taskList2.Count; i++)
		{
			Network.PowerHistory power = taskList2[i].GetPower();
			PredictZoneFromPower(tempZone, power);
		}
	}

	private void PredictZoneFromPower(TempZone tempZone, Network.PowerHistory power)
	{
		switch (power.Type)
		{
		case Network.PowerType.FULL_ENTITY:
			PredictZoneFromFullEntity(tempZone, (Network.HistFullEntity)power);
			break;
		case Network.PowerType.SHOW_ENTITY:
			PredictZoneFromShowEntity(tempZone, (Network.HistShowEntity)power);
			break;
		case Network.PowerType.HIDE_ENTITY:
			PredictZoneFromHideEntity(tempZone, (Network.HistHideEntity)power);
			break;
		case Network.PowerType.TAG_CHANGE:
			PredictZoneFromTagChange(tempZone, (Network.HistTagChange)power);
			break;
		}
	}

	private void PredictZoneFromFullEntity(TempZone tempZone, Network.HistFullEntity fullEntity)
	{
		Entity entity = RegisterTempEntity(fullEntity.Entity);
		if (entity != null)
		{
			Zone zone = tempZone.GetZone();
			bool num = entity.GetZone() == zone.m_ServerTag;
			bool flag = entity.GetControllerId() == zone.GetControllerId();
			if (num && flag)
			{
				tempZone.AddEntity(entity);
			}
		}
	}

	private void PredictZoneFromShowEntity(TempZone tempZone, Network.HistShowEntity showEntity)
	{
		Entity tempEntity = RegisterTempEntity(showEntity.Entity);
		foreach (Network.Entity.Tag tag in showEntity.Entity.Tags)
		{
			PredictZoneByApplyingTag(tempZone, tempEntity, (GAME_TAG)tag.Name, tag.Value);
		}
	}

	private void PredictZoneFromHideEntity(TempZone tempZone, Network.HistHideEntity hideEntity)
	{
		Entity tempEntity = RegisterTempEntity(hideEntity.Entity);
		PredictZoneByApplyingTag(tempZone, tempEntity, GAME_TAG.ZONE, hideEntity.Zone);
	}

	private void PredictZoneFromTagChange(TempZone tempZone, Network.HistTagChange tagChange)
	{
		Entity tempEntity = RegisterTempEntity(tagChange.Entity);
		PredictZoneByApplyingTag(tempZone, tempEntity, (GAME_TAG)tagChange.Tag, tagChange.Value);
	}

	private void PredictZoneByApplyingTag(TempZone tempZone, Entity tempEntity, GAME_TAG tag, int val)
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
		bool num = tempEntity.GetZone() == zone.m_ServerTag;
		bool flag = tempEntity.GetControllerId() == zone.GetControllerId();
		if (num && flag)
		{
			tempZone.RemoveEntity(tempEntity);
		}
		tempEntity.SetTag(tag, val);
		bool num2 = tempEntity.GetZone() == zone.m_ServerTag;
		flag = tempEntity.GetControllerId() == zone.GetControllerId();
		if (num2 && flag)
		{
			tempZone.AddEntity(tempEntity);
		}
	}

	private ZoneChangeList CreateLocalChangeList(Card triggerCard, Zone destinationZone, TAG_ZONE destinationZoneTag, int destinationPos, ChangeCompleteCallback callback, object userData)
	{
		int nextLocalChangeListId = GetNextLocalChangeListId();
		Log.Zone.Print("ZoneMgr.CreateLocalChangeList() - changeListId={0}", nextLocalChangeListId);
		ZoneChangeList zoneChangeList = new ZoneChangeList();
		zoneChangeList.SetId(nextLocalChangeListId);
		zoneChangeList.SetCompleteCallback(callback);
		zoneChangeList.SetCompleteCallbackUserData(userData);
		Entity entity = triggerCard.GetEntity();
		Zone zone = triggerCard.GetZone();
		TAG_ZONE sourceZoneTag = ((!(zone == null)) ? zone.m_ServerTag : TAG_ZONE.INVALID);
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
		Log.Zone.Print("ZoneMgr.CreateLocalChangeList() - AddChange() changeList: {0}, triggerChange: {1}", zoneChangeList, zoneChange);
		zoneChangeList.AddChange(zoneChange);
		return zoneChangeList;
	}

	private void ProcessOrEnqueueLocalChangeList(ZoneChangeList changeList)
	{
		ZoneChange localTriggerChange = changeList.GetLocalTriggerChange();
		Card card = localTriggerChange.GetEntity().GetCard();
		if (HasTriggeredActiveLocalChange(card))
		{
			m_pendingLocalChangeLists.Add(changeList);
			return;
		}
		CreateLocalChangesFromTrigger(changeList, localTriggerChange);
		ProcessLocalChangeList(changeList);
	}

	private void CreateLocalChangesFromTrigger(ZoneChangeList changeList, ZoneChange triggerChange)
	{
		Log.Zone.Print($"ZoneMgr.CreateLocalChangesFromTrigger() - {changeList}");
		Entity entity = triggerChange.GetEntity();
		Zone sourceZone = triggerChange.GetSourceZone();
		int sourcePosition = triggerChange.GetSourcePosition();
		Zone destinationZone = triggerChange.GetDestinationZone();
		int destinationPosition = triggerChange.GetDestinationPosition();
		if (sourceZone != destinationZone)
		{
			TAG_ZONE sourceZoneTag = triggerChange.GetSourceZoneTag();
			TAG_ZONE destinationZoneTag = triggerChange.GetDestinationZoneTag();
			CreateLocalChangesFromTrigger(changeList, entity, sourceZone, sourceZoneTag, sourcePosition, destinationZone, destinationZoneTag, destinationPosition);
		}
		else if (sourcePosition != destinationPosition)
		{
			CreateLocalPosOnlyChangesFromTrigger(changeList, entity, sourceZone, sourcePosition, destinationPosition);
		}
	}

	private void CreateLocalChangesFromTrigger(ZoneChangeList changeList, Entity triggerEntity, Zone sourceZone, TAG_ZONE sourceZoneTag, int sourcePos, Zone destinationZone, TAG_ZONE destinationZoneTag, int destinationPos)
	{
		Log.Zone.Print("ZoneMgr.CreateLocalChangesFromTrigger() - triggerEntity={0} srcZone={1} srcPos={2} dstZone={3} dstPos={4}", triggerEntity, sourceZoneTag, sourcePos, destinationZoneTag, destinationPos);
		if (sourcePos != destinationPos)
		{
			Log.Zone.Print("ZoneMgr.CreateLocalChangesFromTrigger() - srcPos={0} destPos={1}", sourcePos, destinationPos);
		}
		if (sourceZone != null && !(sourceZone is ZoneHero))
		{
			foreach (Card card2 in sourceZone.GetCards())
			{
				int zonePosition = card2.GetZonePosition();
				if (zonePosition > sourcePos)
				{
					Entity entity = card2.GetEntity();
					ZoneChange zoneChange = new ZoneChange();
					zoneChange.SetParentList(changeList);
					zoneChange.SetEntity(entity);
					int num = zonePosition - 1;
					zoneChange.SetSourcePosition(zonePosition);
					zoneChange.SetDestinationPosition(num);
					Log.Zone.Print($"ZoneMgr.CreateLocalChangesFromTrigger() - srcZone card {card2} zonePos {card2.GetZonePosition()} -> {num}");
					Log.Zone.Print($"ZoneMgr.CreateLocalChangesFromTrigger() 3 - AddChange() changeList: {changeList}, change: {zoneChange}");
					changeList.AddChange(zoneChange);
				}
			}
		}
		if (!(destinationZone != null) || destinationZone is ZoneSecret)
		{
			return;
		}
		if (destinationZone is ZoneWeapon)
		{
			List<Card> cards = destinationZone.GetCards();
			if (cards.Count > 0)
			{
				Entity entity2 = cards[0].GetEntity();
				ZoneChange zoneChange2 = new ZoneChange();
				zoneChange2.SetParentList(changeList);
				zoneChange2.SetEntity(entity2);
				zoneChange2.SetDestinationZone(FindZoneOfType<ZoneGraveyard>(destinationZone.m_Side));
				zoneChange2.SetDestinationZoneTag(TAG_ZONE.GRAVEYARD);
				Log.Zone.Print("ZoneMgr.CreateLocalChangesFromTrigger() 4 - AddChange() changeList: {0}, change: {1}", changeList, zoneChange2);
				changeList.AddChange(zoneChange2);
			}
		}
		else if (destinationZone is ZonePlay || destinationZone is ZoneHand)
		{
			List<Card> cards2 = destinationZone.GetCards();
			for (int i = destinationPos - 1; i < cards2.Count; i++)
			{
				Card card = cards2[i];
				Entity entity3 = card.GetEntity();
				int num2 = i + 2;
				ZoneChange zoneChange3 = new ZoneChange();
				zoneChange3.SetParentList(changeList);
				zoneChange3.SetEntity(entity3);
				zoneChange3.SetDestinationPosition(num2);
				Log.Zone.Print("ZoneMgr.CreateLocalChangesFromTrigger() - dstZone card {0} zonePos {1} -> {2}", card, entity3.GetZonePosition(), num2);
				Log.Zone.Print("ZoneMgr.CreateLocalChangesFromTrigger() 5 - AddChange() changeList: {0}, change: {1}", changeList, zoneChange3);
				changeList.AddChange(zoneChange3);
			}
		}
		else if (!(destinationZone is ZoneHero))
		{
			Debug.LogError($"ZoneMgr.CreateLocalChangesFromTrigger() - don't know how to predict zone position changes for zone {destinationZone}");
		}
	}

	private void CreateLocalPosOnlyChangesFromTrigger(ZoneChangeList changeList, Entity triggerEntity, Zone sourceZone, int sourcePos, int destinationPos)
	{
		List<Card> cards = sourceZone.GetCards();
		if (sourcePos < destinationPos)
		{
			for (int i = 0; i < cards.Count; i++)
			{
				Card card = cards[i];
				Entity entity = card.GetEntity();
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
					Log.Zone.Print("ZoneMgr.CreateLocalPosOnlyChangesFromTrigger() 1 - AddChange() changeList: {0}, change: {1}", changeList, zoneChange);
					changeList.AddChange(zoneChange);
				}
			}
			return;
		}
		for (int j = 0; j < cards.Count; j++)
		{
			Card card2 = cards[j];
			Entity entity2 = card2.GetEntity();
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
				Log.Zone.Print("ZoneMgr.CreateLocalPosOnlyChangesFromTrigger() 2 - AddChange() changeList: {0}, change: {1}", changeList, zoneChange2);
				changeList.AddChange(zoneChange2);
			}
		}
	}

	private void ProcessLocalChangeList(ZoneChangeList changeList)
	{
		Log.Zone.Print("ZoneMgr.ProcessLocalChangeList() - [{0}]", changeList);
		m_activeLocalChangeLists.Add(changeList);
		StartCoroutine(changeList.ProcessChanges());
	}

	private void OnCurrentPlayerChanged(Player player, object userData)
	{
		if (player.IsLocalUser())
		{
			m_localChangeListHistory.Clear();
		}
	}

	private void OnOptionRejected(Network.Options.Option option, object userData)
	{
		if (option.Type != Network.Options.Option.OptionType.POWER)
		{
			return;
		}
		Entity entity = GameState.Get().GetEntity(option.Main.ID);
		ZoneChangeList zoneChangeList = FindRejectedLocalZoneChange(entity);
		if (zoneChangeList == null)
		{
			Log.Zone.Print("ZoneMgr.RejectLocalZoneChange() - did not find a zone change to reject for {0}", entity);
			return;
		}
		Card card = entity.GetCard();
		card.SetPredictedZonePosition(0);
		ZoneChange localTriggerChange = zoneChangeList.GetLocalTriggerChange();
		if (card.m_minionWasMovedFromSrcToDst != null && card.m_minionWasMovedFromSrcToDst.m_destinationZonePosition == localTriggerChange.GetDestinationPosition())
		{
			card.m_minionWasMovedFromSrcToDst = null;
		}
		CancelLocalZoneChange(zoneChangeList);
	}

	private ZoneChangeList FindRejectedLocalZoneChange(Entity triggerEntity)
	{
		List<ZoneChangeList> list = m_localChangeListHistory.GetList();
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

	private ZoneChange CreateZoneChangeForNonZoneTask()
	{
		ZoneChange zoneChange = new ZoneChange();
		zoneChange.SetEntity(GameState.Get().GetGameEntity());
		return zoneChange;
	}

	private ZoneChange CreateZoneChangeFromFullEntity(Network.HistFullEntity fullEntity)
	{
		Network.Entity entity = fullEntity.Entity;
		Entity entity2 = GameState.Get().GetEntity(entity.ID);
		if (entity2 == null)
		{
			Debug.LogWarning($"ZoneMgr.CreateZoneChangeFromFullEntity() - WARNING entity {entity.ID} DOES NOT EXIST!");
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
			switch (tag.Name)
			{
			case 49:
				zoneChange.SetDestinationZoneTag((TAG_ZONE)tag.Value);
				flag = true;
				break;
			case 263:
				zoneChange.SetDestinationPosition(tag.Value);
				flag2 = true;
				break;
			case 50:
				zoneChange.SetDestinationControllerId(tag.Value);
				flag3 = true;
				break;
			}
			if (flag && flag2 && flag3)
			{
				break;
			}
		}
		if (flag || flag3)
		{
			zoneChange.SetDestinationZone(FindZoneForEntity(entity2));
		}
		return zoneChange;
	}

	private ZoneChange CreateZoneChangeFromEntity(Network.Entity netEnt)
	{
		Entity entity = GameState.Get().GetEntity(netEnt.ID);
		if (entity == null)
		{
			if (!GameState.Get().EntityRemovedFromGame(netEnt.ID))
			{
				Debug.LogWarning($"ZoneMgr.CreateZoneChangeFromEntity() - WARNING entity {netEnt.ID} DOES NOT EXIST!");
			}
			return null;
		}
		ZoneChange zoneChange = new ZoneChange();
		zoneChange.SetEntity(entity);
		if (entity.GetCard() == null)
		{
			return zoneChange;
		}
		Entity entity2 = RegisterTempEntity(netEnt.ID, entity);
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
			switch (tag2.Name)
			{
			case 49:
				zoneChange.SetDestinationZoneTag((TAG_ZONE)tag2.Value);
				flag = true;
				break;
			case 263:
				zoneChange.SetDestinationPosition(tag2.Value);
				flag2 = true;
				break;
			case 50:
				zoneChange.SetDestinationControllerId(tag2.Value);
				flag3 = true;
				break;
			}
			if (flag && flag2 && flag3)
			{
				break;
			}
		}
		if (flag || flag3)
		{
			zoneChange.SetDestinationZone(FindZoneForEntity(entity2));
		}
		return zoneChange;
	}

	private ZoneChange CreateZoneChangeFromHideEntity(Network.HistHideEntity hideEntity)
	{
		Entity entity = GameState.Get().GetEntity(hideEntity.Entity);
		if (entity == null)
		{
			if (!GameState.Get().EntityRemovedFromGame(hideEntity.Entity))
			{
				Debug.LogWarning($"ZoneMgr.CreateZoneChangeFromHideEntity() - WARNING entity {hideEntity.Entity} DOES NOT EXIST! zone={hideEntity.Zone}");
			}
			return null;
		}
		ZoneChange zoneChange = new ZoneChange();
		zoneChange.SetEntity(entity);
		if (entity.GetCard() == null)
		{
			return zoneChange;
		}
		Entity entity2 = RegisterTempEntity(hideEntity.Entity, entity);
		if (entity2 == null)
		{
			return zoneChange;
		}
		entity2.SetTag(GAME_TAG.ZONE, hideEntity.Zone);
		TAG_ZONE zone = (TAG_ZONE)hideEntity.Zone;
		zoneChange.SetDestinationZoneTag(zone);
		zoneChange.SetDestinationZone(FindZoneForEntity(entity2));
		return zoneChange;
	}

	private ZoneChange CreateZoneChangeFromTagChange(Network.HistTagChange tagChange)
	{
		Entity entity = GameState.Get().GetEntity(tagChange.Entity);
		if (entity == null)
		{
			if (!GameState.Get().EntityRemovedFromGame(tagChange.Entity))
			{
				Debug.LogError($"ZoneMgr.CreateZoneChangeFromTagChange() - Entity {tagChange.Entity} does not exist");
			}
			return null;
		}
		ZoneChange zoneChange = new ZoneChange();
		zoneChange.SetEntity(entity);
		if (entity.GetCard() == null)
		{
			return zoneChange;
		}
		Entity entity2 = RegisterTempEntity(tagChange.Entity, entity);
		if (entity2 == null)
		{
			return zoneChange;
		}
		entity2.SetTag(tagChange.Tag, tagChange.Value);
		switch (tagChange.Tag)
		{
		case 49:
		{
			TAG_ZONE value2 = (TAG_ZONE)tagChange.Value;
			zoneChange.SetDestinationZoneTag(value2);
			zoneChange.SetDestinationZone(FindZoneForEntity(entity2));
			break;
		}
		case 263:
			zoneChange.SetDestinationPosition(tagChange.Value);
			break;
		case 50:
		{
			int value = tagChange.Value;
			zoneChange.SetDestinationControllerId(value);
			zoneChange.SetDestinationZone(FindZoneForEntity(entity2));
			break;
		}
		}
		return zoneChange;
	}

	private ZoneChange CreateZoneChangeFromMetaData(Network.HistMetaData metaData)
	{
		if (metaData.Info.Count <= 0)
		{
			return null;
		}
		Entity entity = GameState.Get().GetEntity(metaData.Info[0]);
		if (entity == null)
		{
			Debug.LogError($"ZoneMgr.CreateZoneChangeFromMetaData() - Entity {metaData.Info[0]} does not exist");
			return null;
		}
		ZoneChange zoneChange = new ZoneChange();
		zoneChange.SetEntity(entity);
		return zoneChange;
	}

	private Entity RegisterTempEntity(int id)
	{
		Entity entity = GameState.Get().GetEntity(id);
		return RegisterTempEntity(id, entity);
	}

	private Entity RegisterTempEntity(Network.Entity netEnt)
	{
		Entity entity = GameState.Get().GetEntity(netEnt.ID);
		return RegisterTempEntity(netEnt.ID, entity);
	}

	private Entity RegisterTempEntity(Entity entity)
	{
		int id = entity?.GetEntityId() ?? (-1);
		return RegisterTempEntity(id, entity);
	}

	private Entity RegisterTempEntity(int id, Entity entity)
	{
		if (entity == null)
		{
			string text = $"{this}.RegisterTempEntity(): Attempting to register an invalid entity! No dbid {id} exists.";
			TelemetryManager.Client().SendLiveIssue("Gameplay_ZoneManager", text);
			Log.Zone.PrintWarning(text);
		}
		Entity value = null;
		if (!m_tempEntityMap.TryGetValue(id, out value) && entity != null)
		{
			value = entity.CloneForZoneMgr();
			m_tempEntityMap.Add(id, value);
		}
		return value;
	}

	private void PostProcessServerChangeList(ZoneChangeList serverChangeList)
	{
		if (ShouldPostProcessServerChangeList(serverChangeList) && !CheckAndIgnoreServerChangeList(serverChangeList) && !ReplaceRemoteWeaponInServerChangeList(serverChangeList))
		{
			MergeServerChangeList(serverChangeList);
		}
	}

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
		ZoneChangeList zoneChangeList = FindLocalChangeListMatchingServerChangeList(serverChangeList);
		if (zoneChangeList == null)
		{
			return false;
		}
		serverChangeList.SetIgnoreCardZoneChanges(ignore: true);
		Card localTriggerCard = zoneChangeList.GetLocalTriggerCard();
		if (blockStart.BlockType == HistoryBlock.Type.MOVE_MINION && localTriggerCard != null && localTriggerCard.m_minionWasMovedFromSrcToDst != null)
		{
			foreach (ZoneChange change in zoneChangeList.GetChanges())
			{
				if (change.GetDestinationPosition() == localTriggerCard.m_minionWasMovedFromSrcToDst.m_destinationZonePosition && change.GetEntity() == localTriggerCard.GetEntity())
				{
					localTriggerCard.m_minionWasMovedFromSrcToDst = null;
					break;
				}
			}
		}
		while (m_localChangeListHistory.Count > 0)
		{
			ZoneChangeList zoneChangeList2 = m_localChangeListHistory.Dequeue();
			if (zoneChangeList == zoneChangeList2)
			{
				zoneChangeList.GetLocalTriggerCard().SetPredictedZonePosition(0);
				break;
			}
		}
		return true;
	}

	private ZoneChangeList FindLocalChangeListMatchingServerChangeList(ZoneChangeList serverChangeList)
	{
		foreach (ZoneChangeList item in m_localChangeListHistory)
		{
			int predictedPosition = item.GetPredictedPosition();
			foreach (ZoneChange change in item.GetChanges())
			{
				Entity entity = change.GetEntity();
				TAG_ZONE destinationZoneTag = change.GetDestinationZoneTag();
				TAG_ZONE sourceZoneTag = change.GetSourceZoneTag();
				if (destinationZoneTag == TAG_ZONE.INVALID)
				{
					continue;
				}
				bool flag = sourceZoneTag != destinationZoneTag;
				List<ZoneChange> changes = serverChangeList.GetChanges();
				for (int i = 0; i < changes.Count; i++)
				{
					ZoneChange zoneChange = changes[i];
					Entity entity2 = zoneChange.GetEntity();
					if (entity != entity2)
					{
						continue;
					}
					if (flag)
					{
						TAG_ZONE destinationZoneTag2 = zoneChange.GetDestinationZoneTag();
						if (destinationZoneTag != destinationZoneTag2)
						{
							continue;
						}
						if (destinationZoneTag == TAG_ZONE.PLAY && entity.HasTag(GAME_TAG.TRANSFORMED_FROM_CARD) && entity.GetTag(GAME_TAG.TRANSFORMED_FROM_CARD) != entity.GetTag(GAME_TAG.DATABASE_ID))
						{
							int id = entity.GetTag(GAME_TAG.LAST_AFFECTED_BY);
							Entity entity3 = GameState.Get().GetEntity(id);
							if (entity3 != null && GameUtils.TranslateCardIdToDbId(entity3.GetCardId()) == 61187)
							{
								continue;
							}
						}
					}
					int num = FindNextDstPosChange(serverChangeList, i, entity2)?.GetDestinationPosition() ?? entity2.GetZonePosition();
					if (predictedPosition == num)
					{
						return item;
					}
				}
			}
		}
		return null;
	}

	private ZoneChange FindNextDstPosChange(ZoneChangeList changeList, int index, Entity entity)
	{
		List<ZoneChange> changes = changeList.GetChanges();
		for (int i = index; i < changes.Count; i++)
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
		}
		return null;
	}

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
			return (powerTask2 == null || !powerTask2.IsCompleted()) ? true : false;
		});
		bool result = false;
		foreach (ZoneChange item in list)
		{
			Zone destinationZone = item.GetDestinationZone();
			if (destinationZone.GetCardCount() == 0)
			{
				continue;
			}
			Entity entity = destinationZone.GetCardAtIndex(0).GetEntity();
			bool flag = false;
			foreach (ZoneChange item2 in changes)
			{
				PowerTask powerTask = item2.GetPowerTask();
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
				Zone destinationZone2 = FindZoneForTags(controllerId, TAG_ZONE.GRAVEYARD, TAG_CARDTYPE.WEAPON, entity);
				ZoneChange zoneChange = new ZoneChange();
				zoneChange.SetEntity(entity);
				zoneChange.SetDestinationZone(destinationZone2);
				zoneChange.SetDestinationZoneTag(TAG_ZONE.GRAVEYARD);
				zoneChange.SetDestinationPosition(0);
				zoneChange.SetParentList(serverChangeList);
				Log.Zone.Print("ZoneMgr.ReplaceRemoteWeaponInServerChangeList() - AddChange() serverChangeList: {0}, graveyardChange: {1}", serverChangeList, zoneChange);
				serverChangeList.AddChange(zoneChange);
				result = true;
			}
		}
		return result;
	}

	private bool MergeServerChangeList(ZoneChangeList serverChangeList)
	{
		Log.Zone.Print("ZoneMgr.MergeServerChangeList() Start - serverChangeList: {0}, m_tempZoneMap.Count: {1}, m_tempEntityMap.Count: {2}", serverChangeList, m_tempZoneMap.Count, m_tempEntityMap.Count);
		foreach (Zone zone2 in m_zones)
		{
			if (IsZoneInLocalHistory(zone2))
			{
				TempZone tempZone = BuildTempZone(zone2);
				m_tempZoneMap[zone2] = tempZone;
				tempZone.PreprocessChanges();
			}
		}
		List<ZoneChange> changes = serverChangeList.GetChanges();
		for (int i = 0; i < changes.Count; i++)
		{
			ZoneChange change = changes[i];
			TempApplyZoneChange(change);
		}
		bool result = false;
		foreach (TempZone value in m_tempZoneMap.Values)
		{
			value.Sort();
			value.PostprocessChanges();
			Zone zone = value.GetZone();
			Log.Zone.Print("ZoneMgr.MergeServerChangeList() zone: {0}", zone);
			foreach (Card card in zone.GetCards())
			{
				Log.Zone.Print("\tzone card: {0}", card);
			}
			Log.Zone.Print("ZoneMgr.MergeServerChangeList() tempZone: {0}", value);
			foreach (Entity entity3 in value.GetEntities())
			{
				Log.Zone.Print("\ttempZone entity: {0}", entity3);
			}
			for (int j = 1; j < zone.GetLastPos(); j++)
			{
				Card cardAtPos = zone.GetCardAtPos(j);
				Entity entity = cardAtPos.GetEntity();
				if (cardAtPos.GetPredictedZonePosition() != 0)
				{
					int num = FindBestInsertionPosition(value, j - 1, j + 1);
					Log.Zone.Print("ZoneMgr.MergeServerChangeList() InsertEntityAtPos() - tempZone: {0}, insertionPos: {1}, entity: {2}", value, num, entity);
					value.InsertEntityAtPos(num, entity);
				}
			}
			if (value.IsModified())
			{
				result = true;
				for (int k = 1; k < value.GetLastPos(); k++)
				{
					Entity entity2 = value.GetEntityAtPos(k).GetCard().GetEntity();
					ZoneChange zoneChange = new ZoneChange();
					zoneChange.SetEntity(entity2);
					zoneChange.SetDestinationZone(zone);
					zoneChange.SetDestinationZoneTag(zone.m_ServerTag);
					zoneChange.SetDestinationPosition(k);
					zoneChange.SetParentList(serverChangeList);
					Log.Zone.Print("ZoneMgr.MergeServerChangeList() - AddChange() tempZone:{0}, serverChangeList: {1}, graveyardChange: {2}", value, serverChangeList, zoneChange);
					serverChangeList.AddChange(zoneChange);
				}
			}
		}
		m_tempZoneMap.Clear();
		m_tempEntityMap.Clear();
		return result;
	}

	private bool IsZoneInLocalHistory(Zone zone)
	{
		foreach (ZoneChangeList item in m_localChangeListHistory)
		{
			foreach (ZoneChange change in item.GetChanges())
			{
				Zone sourceZone = change.GetSourceZone();
				Zone destinationZone = change.GetDestinationZone();
				if (zone == sourceZone || zone == destinationZone)
				{
					return true;
				}
			}
		}
		return false;
	}

	private void TempApplyZoneChange(ZoneChange change)
	{
		Log.Zone.Print("ZoneMgr.TempApplyZoneChange() - change: {0}, changeList: {1}", change, change.GetParentList());
		Network.PowerHistory power = change.GetPowerTask().GetPower();
		Entity entity = change.GetEntity();
		Entity entity2 = RegisterTempEntity(entity);
		if (entity2 == null)
		{
			return;
		}
		if (!change.HasDestinationZoneChange())
		{
			GameUtils.ApplyPower(entity2, power);
			return;
		}
		Zone zone = (change.HasSourceZone() ? change.GetSourceZone() : FindZoneForEntity(entity2));
		TempZone tempZone = FindTempZoneForZone(zone);
		if (tempZone != null)
		{
			bool flag = tempZone.RemoveEntity(entity2);
			Log.Zone.Print("ZoneMgr.TempApplyZoneChange() - RemoveEntity() srcTempZone: {0}, tempEntity: {1}, result: {2}", tempZone, entity2, flag);
		}
		GameUtils.ApplyPower(entity2, power);
		Zone destinationZone = change.GetDestinationZone();
		TempZone tempZone2 = FindTempZoneForZone(destinationZone);
		if (tempZone2 != null)
		{
			tempZone2.AddEntity(entity2);
			Log.Zone.Print("ZoneMgr.TempApplyZoneChange() - AddEntity() dstTempZone: {0}, tempEntity: {1}", tempZone2, entity2);
		}
	}

	private TempZone BuildTempZone(Zone zone)
	{
		TempZone tempZone = new TempZone();
		tempZone.SetZone(zone);
		List<Card> cards = zone.GetCards();
		for (int i = 0; i < cards.Count; i++)
		{
			Card card = cards[i];
			if (card.GetPredictedZonePosition() == 0 && (!card.IsBeingDragged || zone is ZoneHand))
			{
				Entity entity = card.GetEntity();
				Entity entity2 = RegisterTempEntity(entity);
				if (entity2 != null)
				{
					tempZone.AddInitialEntity(entity2);
				}
			}
		}
		return tempZone;
	}

	private TempZone FindTempZoneForZone(Zone zone)
	{
		if (zone == null)
		{
			return null;
		}
		TempZone value = null;
		m_tempZoneMap.TryGetValue(zone, out value);
		return value;
	}

	private int FindBestInsertionPosition(TempZone tempZone, int leftPos, int rightPos)
	{
		Zone zone = tempZone.GetZone();
		int num = 0;
		for (int num2 = leftPos - 1; num2 >= 0; num2--)
		{
			Card cardAtIndex = zone.GetCardAtIndex(num2);
			if (cardAtIndex == null)
			{
				Log.Zone.PrintWarning("{0}.FindBestInsertionPosition(): Bad leftPos value! No card at index {1} currently in Zone {2}.", this, num2, zone);
			}
			else
			{
				Entity entity = cardAtIndex.GetEntity();
				num = tempZone.FindEntityPosWithReplacements(entity.GetEntityId());
				if (num != 0)
				{
					break;
				}
			}
		}
		int i;
		if (num == 0)
		{
			i = 1;
		}
		else
		{
			int entityId = tempZone.GetEntityAtPos(num).GetEntityId();
			for (i = num + 1; i < tempZone.GetLastPos(); i++)
			{
				Entity entityAtPos = tempZone.GetEntityAtPos(i);
				if (entityAtPos.GetCreatorId() != entityId || zone.ContainsCard(entityAtPos.GetCard()))
				{
					break;
				}
			}
		}
		int num3 = 0;
		for (int j = rightPos - 1; j < zone.GetCardCount(); j++)
		{
			Card cardAtIndex2 = zone.GetCardAtIndex(j);
			if (cardAtIndex2 == null)
			{
				Log.Zone.PrintWarning("{0}.FindBestInsertionPosition(): Bad rightPos value! No card at index {1} currently in Zone {2}.", this, j, zone);
				continue;
			}
			Entity entity2 = cardAtIndex2.GetEntity();
			num3 = tempZone.FindEntityPosWithReplacements(entity2.GetEntityId());
			if (num3 != 0)
			{
				break;
			}
		}
		int num4;
		if (num3 == 0)
		{
			num4 = tempZone.GetLastPos();
		}
		else
		{
			int entityId2 = tempZone.GetEntityAtPos(num3).GetEntityId();
			for (num4 = num3 - 1; num4 > 0; num4--)
			{
				Entity entityAtPos2 = tempZone.GetEntityAtPos(num4);
				if (entityAtPos2.GetCreatorId() != entityId2 || zone.ContainsCard(entityAtPos2.GetCard()))
				{
					break;
				}
			}
			num4++;
		}
		return Mathf.CeilToInt(0.5f * (float)(i + num4));
	}

	private int GetNextLocalChangeListId()
	{
		int nextLocalChangeListId = m_nextLocalChangeListId;
		m_nextLocalChangeListId = ((m_nextLocalChangeListId == int.MaxValue) ? 1 : (m_nextLocalChangeListId + 1));
		return nextLocalChangeListId;
	}

	private int GetNextServerChangeListId()
	{
		int nextServerChangeListId = m_nextServerChangeListId;
		m_nextServerChangeListId = ((m_nextServerChangeListId == int.MaxValue) ? 1 : (m_nextServerChangeListId + 1));
		return nextServerChangeListId;
	}

	private int FindTriggeredActiveLocalChangeIndex(Card card)
	{
		for (int i = 0; i < m_activeLocalChangeLists.Count; i++)
		{
			if (m_activeLocalChangeLists[i].GetLocalTriggerCard() == card)
			{
				return i;
			}
		}
		return -1;
	}

	private int FindTriggeredPendingLocalChangeIndex(Card card)
	{
		for (int i = 0; i < m_pendingLocalChangeLists.Count; i++)
		{
			if (m_pendingLocalChangeLists[i].GetLocalTriggerCard() == card)
			{
				return i;
			}
		}
		return -1;
	}

	private void AutoCorrectZonesAfterServerChange()
	{
		if (HasActiveLocalChange())
		{
			Log.Zone.Print("ZoneMgr.AutoCorrectZonesAfterServerChange() - HasActiveLocalChange()");
			return;
		}
		if (HasPendingLocalChange())
		{
			Log.Zone.Print("ZoneMgr.AutoCorrectZonesAfterServerChange() - HasPendingLocalChange()");
			return;
		}
		if (HasActiveServerChange())
		{
			Log.Zone.Print("ZoneMgr.AutoCorrectZonesAfterServerChange() - HasActiveServerChange()");
			return;
		}
		if (HasPendingServerChange())
		{
			Log.Zone.Print("ZoneMgr.AutoCorrectZonesAfterServerChange() - HasPendingServerChange()");
			return;
		}
		if (HasPredictedPositions())
		{
			Log.Zone.Print("ZoneMgr.AutoCorrectZonesAfterServerChange() - HasPredictedPositions()");
			return;
		}
		if (HasPredictedCards())
		{
			Log.Zone.Print("ZoneMgr.AutoCorrectZonesAfterServerChange() - HasPredictedCards()");
			return;
		}
		if (HasPredictedMovedMinion())
		{
			Log.Zone.Print("ZoneMgr.AutoCorrectZonesAfterServerChange() - HasPredictedMovedMinion()");
			return;
		}
		Log.Zone.Print("ZoneMgr.AutoCorrectZonesAfterServerChange()");
		AutoCorrectZones();
	}

	private void AutoCorrectZones()
	{
		ZoneChangeList zoneChangeList = null;
		foreach (Zone item in FindZonesOfType<Zone>(Player.Side.FRIENDLY))
		{
			foreach (Card card in item.GetCards())
			{
				Entity entity = card.GetEntity();
				TAG_ZONE zone = entity.GetZone();
				int controllerId = entity.GetControllerId();
				int zonePosition = entity.GetZonePosition();
				TAG_ZONE serverTag = item.m_ServerTag;
				int controllerId2 = item.GetControllerId();
				int zonePosition2 = card.GetZonePosition();
				bool num = zone == serverTag;
				bool flag = controllerId == controllerId2;
				bool flag2 = zonePosition == 0 || zonePosition == zonePosition2;
				if (!(num && flag && flag2))
				{
					if (zoneChangeList == null)
					{
						int nextLocalChangeListId = GetNextLocalChangeListId();
						Log.Zone.Print("ZoneMgr.AutoCorrectZones() CreateLocalChangeList - changeListId={0}", nextLocalChangeListId);
						zoneChangeList = new ZoneChangeList();
						zoneChangeList.SetId(nextLocalChangeListId);
					}
					ZoneChange zoneChange = new ZoneChange();
					zoneChange.SetEntity(entity);
					zoneChange.SetSourcePosition(zonePosition2);
					zoneChange.SetDestinationZoneTag(zone);
					zoneChange.SetDestinationZone(FindZoneForEntity(entity));
					zoneChange.SetDestinationControllerId(controllerId);
					zoneChange.SetDestinationPosition(zonePosition);
					Log.Zone.Print("ZoneMgr.AutoCorrectZones() - AddChange() changeList: {0}, change: {1}", zoneChangeList, zoneChange);
					zoneChangeList.AddChange(zoneChange);
				}
			}
		}
		if (zoneChangeList != null)
		{
			ProcessLocalChangeList(zoneChangeList);
		}
	}

	public void ProcessGeneratedLocalChangeLists(List<ZoneChangeList> generatedChangeLists)
	{
		foreach (ZoneChangeList generatedChangeList in generatedChangeLists)
		{
			int nextLocalChangeListId = GetNextLocalChangeListId();
			generatedChangeList.SetId(nextLocalChangeListId);
			ProcessLocalChangeList(generatedChangeList);
		}
	}

	public void OnHealingDoesDamageEntityMousedOver()
	{
		foreach (Zone item in FindZonesForSide(Player.Side.FRIENDLY))
		{
			item.OnHealingDoesDamageEntityMousedOver();
		}
	}

	public void OnHealingDoesDamageEntityMousedOut()
	{
		foreach (Zone item in FindZonesForSide(Player.Side.FRIENDLY))
		{
			item.OnHealingDoesDamageEntityMousedOut();
		}
	}

	public void OnLifestealDoesDamageEntityMousedOver()
	{
		foreach (Zone item in FindZonesForSide(Player.Side.FRIENDLY))
		{
			item.OnLifestealDoesDamageEntityMousedOver();
		}
	}

	public void OnLifestealDoesDamageEntityMousedOut()
	{
		foreach (Zone item in FindZonesForSide(Player.Side.FRIENDLY))
		{
			item.OnLifestealDoesDamageEntityMousedOut();
		}
	}

	public void OnHealingDoesDamageEntityEnteredPlay()
	{
		foreach (Zone item in FindZonesForSide(Player.Side.FRIENDLY))
		{
			item.OnHealingDoesDamageEntityEnteredPlay();
		}
	}

	public void OnLifestealDoesDamageEntityEnteredPlay()
	{
		foreach (Zone item in FindZonesForSide(Player.Side.FRIENDLY))
		{
			item.OnLifestealDoesDamageEntityEnteredPlay();
		}
	}

	public void OnSpellPowerEntityMousedOver(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
		foreach (Zone item in FindZonesForSide(Player.Side.FRIENDLY))
		{
			item.OnSpellPowerEntityMousedOver(spellSchool);
		}
	}

	public void OnSpellPowerEntityMousedOut(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
		foreach (Zone item in FindZonesForSide(Player.Side.FRIENDLY))
		{
			item.OnSpellPowerEntityMousedOut(spellSchool);
		}
	}

	public void OnSpellPowerEntityEnteredPlay(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
		foreach (Zone item in FindZonesForSide(Player.Side.FRIENDLY))
		{
			item.OnSpellPowerEntityEnteredPlay(spellSchool);
		}
	}
}
