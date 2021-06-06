#define ZONE_CHANGE_DEBUG
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using PegasusGame;
using UnityEngine;

public class ZoneChangeList
{
	private int m_id;

	private int m_predictedPosition;

	private bool m_ignoreCardZoneChanges;

	private bool m_canceledChangeList;

	private PowerTaskList m_taskList;

	private List<ZoneChange> m_changes = new List<ZoneChange>();

	private HashSet<Zone> m_dirtyZones = new HashSet<Zone>();

	private List<ZoneChangeList> m_generatedLocalChangeLists = new List<ZoneChangeList>();

	private bool m_complete;

	private ZoneMgr.ChangeCompleteCallback m_completeCallback;

	private object m_completeCallbackUserData;

	public int GetId()
	{
		return m_id;
	}

	public void SetId(int id)
	{
		m_id = id;
	}

	public bool IsLocal()
	{
		return m_taskList == null;
	}

	public int GetPredictedPosition()
	{
		return m_predictedPosition;
	}

	public void SetPredictedPosition(int pos)
	{
		m_predictedPosition = pos;
	}

	public void SetIgnoreCardZoneChanges(bool ignore)
	{
		m_ignoreCardZoneChanges = ignore;
	}

	public bool IsCanceledChangeList()
	{
		return m_canceledChangeList;
	}

	public void SetCanceledChangeList(bool canceledChangeList)
	{
		m_canceledChangeList = canceledChangeList;
	}

	public void SetZoneInputBlocking(bool block)
	{
		for (int i = 0; i < m_changes.Count; i++)
		{
			ZoneChange zoneChange = m_changes[i];
			Zone sourceZone = zoneChange.GetSourceZone();
			if (sourceZone != null)
			{
				sourceZone.BlockInput(block);
			}
			Zone destinationZone = zoneChange.GetDestinationZone();
			if (destinationZone != null)
			{
				destinationZone.BlockInput(block);
			}
		}
	}

	public bool IsComplete()
	{
		return m_complete;
	}

	public ZoneMgr.ChangeCompleteCallback GetCompleteCallback()
	{
		return m_completeCallback;
	}

	public void SetCompleteCallback(ZoneMgr.ChangeCompleteCallback callback)
	{
		m_completeCallback = callback;
	}

	public object GetCompleteCallbackUserData()
	{
		return m_completeCallbackUserData;
	}

	public void SetCompleteCallbackUserData(object userData)
	{
		m_completeCallbackUserData = userData;
	}

	public void FireCompleteCallback()
	{
		DebugPrint("ZoneChangeList.FireCompleteCallback() - m_id={0} m_taskList={1} m_changes.Count={2} m_complete={3} m_completeCallback={4}", m_id, (m_taskList == null) ? "(null)" : m_taskList.GetId().ToString(), m_changes.Count, m_complete, (m_completeCallback == null) ? "(null)" : "(not null)");
		if (m_completeCallback != null)
		{
			m_completeCallback(this, m_completeCallbackUserData);
		}
	}

	public PowerTaskList GetTaskList()
	{
		return m_taskList;
	}

	public void SetTaskList(PowerTaskList taskList)
	{
		m_taskList = taskList;
	}

	public List<ZoneChange> GetChanges()
	{
		return m_changes;
	}

	public ZoneChange GetChange(int index)
	{
		return m_changes[index];
	}

	public ZoneChange GetLocalTriggerChange()
	{
		if (!IsLocal())
		{
			return null;
		}
		if (m_changes.Count <= 0)
		{
			return null;
		}
		return m_changes[0];
	}

	public Card GetLocalTriggerCard()
	{
		return GetLocalTriggerChange()?.GetEntity().GetCard();
	}

	public void AddChange(ZoneChange change)
	{
		m_changes.Add(change);
	}

	public void RemoveChange(ZoneChange change)
	{
		m_changes.Remove(change);
	}

	public void InsertChange(int index, ZoneChange change)
	{
		m_changes.Insert(index, change);
	}

	public void ClearChanges()
	{
		m_changes.Clear();
	}

	public IEnumerator ProcessChanges()
	{
		DebugPrint("ZoneChangeList.ProcessChanges() - m_id={0} m_taskList={1} m_changes.Count={2}", m_id, (m_taskList == null) ? "(null)" : m_taskList.GetId().ToString(), m_changes.Count);
		while (GameState.Get().MustWaitForChoices())
		{
			yield return null;
		}
		HashSet<Entity> loadingEntities = new HashSet<Entity>();
		Map<Player, DyingSecretGroup> dyingSecretMap = null;
		int num;
		for (int i = 0; i < m_changes.Count; num = i + 1, i = num)
		{
			ZoneChange change = m_changes[i];
			DebugPrint("ZoneChangeList.ProcessChanges() - processing index={0} change={1}", i, change);
			Entity entity = change.GetEntity();
			Card card = entity.GetCard();
			PowerTask powerTask = change.GetPowerTask();
			int srcControllerId = entity.GetControllerId();
			int srcPos = 0;
			Zone srcZone = null;
			if (card != null)
			{
				srcPos = card.GetZonePosition();
				srcZone = card.GetZone();
			}
			int dstControllerId = change.GetDestinationControllerId();
			int dstPos = change.GetDestinationPosition();
			Zone dstZone = change.GetDestinationZone();
			TAG_ZONE dstZoneTag = change.GetDestinationZoneTag();
			if (powerTask != null)
			{
				if (powerTask.IsCompleted())
				{
					continue;
				}
				if (loadingEntities.Contains(entity))
				{
					DebugPrint("ZoneChangeList.ProcessChanges() - START waiting for {0} to load (powerTask=(not null))", card);
					yield return ZoneMgr.Get().StartCoroutine(WaitForAndRemoveLoadingEntity(loadingEntities, entity, card));
					DebugPrint("ZoneChangeList.ProcessChanges() - END waiting for {0} to load (powerTask=(not null))", card);
				}
				while (!GameState.Get().GetPowerProcessor().CanDoTask(powerTask))
				{
					yield return null;
				}
				while (ShouldWaitForOldHero(entity))
				{
					yield return null;
				}
				powerTask.DoTask();
				if (entity.IsLoadingAssets())
				{
					loadingEntities.Add(entity);
				}
			}
			if (ShouldIgnoreZoneChange(entity))
			{
				continue;
			}
			bool zoneChanged = dstZoneTag != 0 && srcZone != dstZone;
			bool controllerChanged = dstControllerId != 0 && srcControllerId != dstControllerId;
			bool posChanged = zoneChanged || (dstPos != 0 && srcPos != dstPos);
			bool revealed = powerTask != null && powerTask.GetPower().Type == Network.PowerType.SHOW_ENTITY;
			if ((bool)UniversalInputManager.UsePhoneUI && IsDisplayableDyingSecret(entity, card, srcZone, dstZone))
			{
				if (dyingSecretMap == null)
				{
					dyingSecretMap = new Map<Player, DyingSecretGroup>();
				}
				Player controller = card.GetController();
				if (!dyingSecretMap.TryGetValue(controller, out var value))
				{
					value = new DyingSecretGroup();
					dyingSecretMap.Add(controller, value);
				}
				value.AddCard(card);
			}
			if (zoneChanged || controllerChanged || revealed)
			{
				bool transitionedZones = zoneChanged || controllerChanged;
				bool flag = revealed && entity.GetZone() == TAG_ZONE.SECRET;
				if (transitionedZones || !flag)
				{
					if (srcZone != null)
					{
						m_dirtyZones.Add(srcZone);
					}
					if (dstZone != null)
					{
						m_dirtyZones.Add(dstZone);
					}
					DebugPrint("ZoneChangeList.ProcessChanges() - TRANSITIONING card {0} to {1}", card, dstZone);
				}
				if (loadingEntities.Contains(entity))
				{
					DebugPrint("ZoneChangeList.ProcessChanges() - START waiting for {0} to load (zoneChanged={1} controllerChanged={2} powerTask=(not null))", card, zoneChanged, controllerChanged);
					yield return ZoneMgr.Get().StartCoroutine(WaitForAndRemoveLoadingEntity(loadingEntities, entity, card));
					DebugPrint("ZoneChangeList.ProcessChanges() - END waiting for {0} to load (zoneChanged={1} controllerChanged={2} powerTask=(not null))", card, zoneChanged, controllerChanged);
				}
				if (!card.IsActorReady() || card.IsBeingDrawnByOpponent())
				{
					DebugPrint("ZoneChangeList.ProcessChanges() - START waiting for {0} to become ready (zoneChanged={1} controllerChanged={2} powerTask=(not null))", card, zoneChanged, controllerChanged);
					if (card.GetPrevZone() is ZoneDeck && card.GetZone() is ZoneHand && card.GetPrevZone().GetController() == card.GetZone().GetController() && TurnStartManager.Get().IsCardDrawHandled(card))
					{
						TurnStartManager.Get().DrawCardImmediately(card);
					}
					while (!card.IsActorReady() || card.IsBeingDrawnByOpponent())
					{
						yield return null;
					}
					DebugPrint("ZoneChangeList.ProcessChanges() - END waiting for {0} to become ready (zoneChanged={1} controllerChanged={2} powerTask=(not null))", card, zoneChanged, controllerChanged);
				}
				Log.Zone.Print("ZoneChangeList.ProcessChanges() - id={0} local={1} {2} zone from {3} -> {4}", m_id, IsLocal(), card, srcZone, dstZone);
				if (transitionedZones)
				{
					if (srcZone is ZonePlay && srcZone.m_Side == Player.Side.OPPOSING && dstZone is ZoneHand && dstZone.m_Side == Player.Side.OPPOSING)
					{
						Log.FaceDownCard.Print("ZoneChangeList.ProcessChanges() - id={0} {1}.TransitionToZone(): {2} -> {3}", m_id, card, srcZone, dstZone);
						m_taskList.DebugDump(Log.FaceDownCard);
					}
					card.SetZonePosition(0);
					card.TransitionToZone(dstZone, change);
				}
				else if (revealed)
				{
					card.UpdateActor();
				}
				if (card.IsActorLoading())
				{
					loadingEntities.Add(entity);
				}
			}
			if (posChanged)
			{
				if (srcZone != null && !zoneChanged && !controllerChanged)
				{
					m_dirtyZones.Add(srcZone);
				}
				if (dstZone != null)
				{
					m_dirtyZones.Add(dstZone);
				}
				if (card.m_minionWasMovedFromSrcToDst != null && !IsLocal())
				{
					GenerateLocalChangelistForMovedMinionWhileProcessingServerChangelist(card);
					continue;
				}
				Log.Zone.Print("ZoneChangeList.ProcessChanges() - id={0} local={1} {2} pos from {3} -> {4}", m_id, IsLocal(), card, srcPos, dstPos);
				card.SetZonePosition(dstPos);
			}
		}
		while (ShowNewHeroStats())
		{
			yield return null;
		}
		if (IsCanceledChangeList())
		{
			SetZoneInputBlocking(block: false);
		}
		ProcessDyingSecrets(dyingSecretMap);
		ZoneMgr.Get().ProcessGeneratedLocalChangeLists(m_generatedLocalChangeLists);
		ZoneMgr.Get().StartCoroutine(UpdateDirtyZones(loadingEntities));
	}

	private void GenerateLocalChangelistForMovedMinionWhileProcessingServerChangelist(Card card)
	{
		if (!(card == null) && card.m_minionWasMovedFromSrcToDst != null)
		{
			ZoneChangeList zoneChangeList = new ZoneChangeList();
			ZoneChange zoneChange = new ZoneChange();
			zoneChange.SetEntity(card.GetEntity());
			zoneChange.SetSourcePosition(card.GetZonePosition());
			zoneChange.SetDestinationPosition(card.GetEntity().GetRealTimeZonePosition());
			Log.Zone.Print("ZoneMgr.GenerateLocalChangelistForMovedMinionWhileProcessingServerChangelist() - AddChange() changeList: {0}, change: {1}", zoneChangeList, zoneChange);
			zoneChangeList.AddChange(zoneChange);
			m_generatedLocalChangeLists.Add(zoneChangeList);
		}
	}

	private bool IsCardMove(int zoneChangeIndex, TAG_ZONE fromZone, TAG_ZONE toZone)
	{
		if (zoneChangeIndex < 0 || zoneChangeIndex >= m_changes.Count)
		{
			return false;
		}
		if (m_changes[zoneChangeIndex].GetDestinationZoneTag() != toZone)
		{
			return false;
		}
		if (m_changes[zoneChangeIndex].GetSourceZoneTag() == fromZone)
		{
			return true;
		}
		ZoneChange zoneChange = null;
		int num = zoneChangeIndex - 1;
		while (zoneChangeIndex >= 0)
		{
			zoneChange = m_changes[num];
			if (zoneChange.HasDestinationZoneTag())
			{
				break;
			}
			num--;
		}
		return zoneChange.GetDestinationZoneTag() == fromZone;
	}

	public bool IsCardDraw(int zoneChangeIndex)
	{
		return IsCardMove(zoneChangeIndex, TAG_ZONE.DECK, TAG_ZONE.HAND);
	}

	public bool IsCardMill(int zoneChangeIndex)
	{
		return IsCardMove(zoneChangeIndex, TAG_ZONE.DECK, TAG_ZONE.GRAVEYARD);
	}

	public override string ToString()
	{
		return $"id={m_id} changes={m_changes.Count} complete={m_complete} local={IsLocal()} localTrigger=[{GetLocalTriggerChange()}]";
	}

	private bool IsDisplayableDyingSecret(Entity entity, Card card, Zone srcZone, Zone dstZone)
	{
		if (!entity.IsSecret())
		{
			return false;
		}
		if (!(srcZone is ZoneSecret))
		{
			return false;
		}
		if (!(dstZone is ZoneGraveyard))
		{
			return false;
		}
		return true;
	}

	private void ProcessDyingSecrets(Map<Player, DyingSecretGroup> dyingSecretMap)
	{
		if (dyingSecretMap == null)
		{
			return;
		}
		Map<Player, DeadSecretGroup> map = null;
		foreach (KeyValuePair<Player, DyingSecretGroup> item in dyingSecretMap)
		{
			Player key = item.Key;
			DyingSecretGroup value = item.Value;
			Card mainCard = value.GetMainCard();
			List<Card> cards = value.GetCards();
			List<Actor> actors = value.GetActors();
			for (int i = 0; i < cards.Count; i++)
			{
				Card card = cards[i];
				Actor actor = actors[i];
				if (card.WasSecretTriggered())
				{
					actor.Destroy();
					continue;
				}
				if (card == mainCard && card.CanShowSecretDeath())
				{
					card.ShowSecretDeath(actor);
				}
				else
				{
					actor.Destroy();
				}
				if (map == null)
				{
					map = new Map<Player, DeadSecretGroup>();
				}
				if (!map.TryGetValue(key, out var value2))
				{
					value2 = new DeadSecretGroup();
					value2.SetMainCard(mainCard);
					map.Add(key, value2);
				}
				value2.AddCard(card);
			}
		}
		BigCard.Get().ShowSecretDeaths(map);
	}

	private IEnumerator WaitForAndRemoveLoadingEntity(HashSet<Entity> loadingEntities, Entity entity, Card card)
	{
		while (IsEntityLoading(entity, card))
		{
			yield return null;
		}
		loadingEntities.Remove(entity);
	}

	private bool IsEntityLoading(Entity entity, Card card)
	{
		if (entity.IsLoadingAssets())
		{
			return true;
		}
		if (card != null && card.IsActorLoading())
		{
			return true;
		}
		return false;
	}

	private IEnumerator UpdateDirtyZones(HashSet<Entity> loadingEntities)
	{
		DebugPrint("ZoneChangeList.UpdateDirtyZones() - m_id={0} loadingEntities.Count={1} m_dirtyZones.Count={2}", m_id, loadingEntities.Count, m_dirtyZones.Count);
		foreach (Entity entity in loadingEntities)
		{
			Card card = entity.GetCard();
			DebugPrint("ZoneChangeList.UpdateDirtyZones() - m_id={0} START waiting for {1} to load (card={2})", m_id, entity, card);
			while (IsEntityLoading(entity, card))
			{
				yield return null;
			}
			DebugPrint("ZoneChangeList.UpdateDirtyZones() - m_id={0} END waiting for {1} to load (card={2})", m_id, entity, card);
		}
		if (IsDeathBlock())
		{
			float num = ZoneMgr.Get().RemoveNextDeathBlockLayoutDelaySec();
			if (num >= 0f)
			{
				yield return new WaitForSeconds(num);
			}
			foreach (Zone dirtyZone in m_dirtyZones)
			{
				dirtyZone.UpdateLayout();
			}
			m_dirtyZones.Clear();
		}
		else
		{
			Zone[] array = new Zone[m_dirtyZones.Count];
			m_dirtyZones.CopyTo(array);
			Zone[] array2 = array;
			foreach (Zone zone in array2)
			{
				DebugPrint("ZoneChangeList.UpdateDirtyZones() - m_id={0} START waiting for zone {1}", m_id, zone);
				if (zone is ZoneHand)
				{
					ZoneMgr.Get().StartCoroutine(ZoneHand_UpdateLayout((ZoneHand)zone));
					continue;
				}
				zone.AddUpdateLayoutCompleteCallback(OnUpdateLayoutComplete);
				zone.UpdateLayout();
			}
		}
		ZoneMgr.Get().StartCoroutine(FinishWhenPossible());
	}

	private bool IsDeathBlock()
	{
		if (m_taskList == null)
		{
			return false;
		}
		return m_taskList.IsDeathBlock();
	}

	private IEnumerator ZoneHand_UpdateLayout(ZoneHand zoneHand)
	{
		while (!(zoneHand.GetCards().Find(delegate(Card card)
		{
			if (TurnStartManager.Get() != null && TurnStartManager.Get().IsCardDrawHandled(card))
			{
				return false;
			}
			return !card.IsDoNotSort() && !card.IsActorReady();
		}) == null))
		{
			yield return null;
		}
		zoneHand.AddUpdateLayoutCompleteCallback(OnUpdateLayoutComplete);
		zoneHand.UpdateLayout();
	}

	private void OnUpdateLayoutComplete(Zone zone, object userData)
	{
		DebugPrint("ZoneChangeList.OnUpdateLayoutComplete() - m_id={0} END waiting for zone {1}", m_id, zone);
		m_dirtyZones.Remove(zone);
	}

	private Entity GetNewHeroPlayedFromPowerTaskList()
	{
		PowerTaskList taskList = GetTaskList();
		if (taskList == null)
		{
			return null;
		}
		Network.HistBlockStart blockStart = taskList.GetBlockStart();
		if (blockStart == null)
		{
			return null;
		}
		if (blockStart.BlockType != HistoryBlock.Type.PLAY)
		{
			return null;
		}
		Entity sourceEntity = taskList.GetSourceEntity();
		if (sourceEntity == null)
		{
			Log.Zone.PrintWarning("ZoneChangelist.GetNewHeroPlayedFromPowerTaskList() - source is null.");
			return null;
		}
		if (!sourceEntity.IsHero())
		{
			return null;
		}
		return sourceEntity;
	}

	private bool ShowNewHeroStats()
	{
		Entity newHeroPlayedFromPowerTaskList = GetNewHeroPlayedFromPowerTaskList();
		if (newHeroPlayedFromPowerTaskList != null)
		{
			if (!newHeroPlayedFromPowerTaskList.GetCard().IsActorReady())
			{
				return true;
			}
			Actor actor = newHeroPlayedFromPowerTaskList.GetCard().GetActor();
			actor.EnableArmorSpellAfterTransition();
			actor.ShowArmorSpell();
			actor.GetHealthObject().Show();
			actor.GetAttackObject().Show();
			if (newHeroPlayedFromPowerTaskList.GetATK() <= 0)
			{
				actor.GetAttackObject().ImmediatelyScaleToZero();
			}
		}
		return false;
	}

	private bool ShouldWaitForOldHero(Entity entity)
	{
		if (!entity.IsHero())
		{
			return false;
		}
		Entity newHeroPlayedFromPowerTaskList = GetNewHeroPlayedFromPowerTaskList();
		if (newHeroPlayedFromPowerTaskList == null)
		{
			return false;
		}
		if (newHeroPlayedFromPowerTaskList.GetEntityId() == entity.GetEntityId())
		{
			return false;
		}
		return !newHeroPlayedFromPowerTaskList.GetCard().IsActorReady();
	}

	private bool ShouldIgnoreZoneChange(Entity entity)
	{
		if (entity.GetCard() == null)
		{
			return true;
		}
		if (IsOldHero(entity))
		{
			return false;
		}
		return m_ignoreCardZoneChanges;
	}

	private bool IsOldHero(Entity entity)
	{
		Entity newHeroPlayedFromPowerTaskList = GetNewHeroPlayedFromPowerTaskList();
		if (newHeroPlayedFromPowerTaskList == null)
		{
			return false;
		}
		if (!entity.IsHero())
		{
			return false;
		}
		return newHeroPlayedFromPowerTaskList.GetEntityId() != entity.GetEntityId();
	}

	private IEnumerator FinishWhenPossible()
	{
		while (m_dirtyZones.Count > 0)
		{
			yield return null;
		}
		while (GameState.Get().IsBusy())
		{
			yield return null;
		}
		Finish();
	}

	private void Finish()
	{
		m_complete = true;
		Log.Zone.Print("ZoneChangeList.Finish() - {0}", this);
	}

	[Conditional("ZONE_CHANGE_DEBUG")]
	private void DebugPrint(string format, params object[] args)
	{
		Log.Zone.Print(format, args);
	}
}
