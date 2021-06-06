using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using PegasusGame;
using UnityEngine;

// Token: 0x02000361 RID: 865
public class ZoneChangeList
{
	// Token: 0x0600324E RID: 12878 RVA: 0x00100E47 File Offset: 0x000FF047
	public int GetId()
	{
		return this.m_id;
	}

	// Token: 0x0600324F RID: 12879 RVA: 0x00100E4F File Offset: 0x000FF04F
	public void SetId(int id)
	{
		this.m_id = id;
	}

	// Token: 0x06003250 RID: 12880 RVA: 0x00100E58 File Offset: 0x000FF058
	public bool IsLocal()
	{
		return this.m_taskList == null;
	}

	// Token: 0x06003251 RID: 12881 RVA: 0x00100E63 File Offset: 0x000FF063
	public int GetPredictedPosition()
	{
		return this.m_predictedPosition;
	}

	// Token: 0x06003252 RID: 12882 RVA: 0x00100E6B File Offset: 0x000FF06B
	public void SetPredictedPosition(int pos)
	{
		this.m_predictedPosition = pos;
	}

	// Token: 0x06003253 RID: 12883 RVA: 0x00100E74 File Offset: 0x000FF074
	public void SetIgnoreCardZoneChanges(bool ignore)
	{
		this.m_ignoreCardZoneChanges = ignore;
	}

	// Token: 0x06003254 RID: 12884 RVA: 0x00100E7D File Offset: 0x000FF07D
	public bool IsCanceledChangeList()
	{
		return this.m_canceledChangeList;
	}

	// Token: 0x06003255 RID: 12885 RVA: 0x00100E85 File Offset: 0x000FF085
	public void SetCanceledChangeList(bool canceledChangeList)
	{
		this.m_canceledChangeList = canceledChangeList;
	}

	// Token: 0x06003256 RID: 12886 RVA: 0x00100E90 File Offset: 0x000FF090
	public void SetZoneInputBlocking(bool block)
	{
		for (int i = 0; i < this.m_changes.Count; i++)
		{
			ZoneChange zoneChange = this.m_changes[i];
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

	// Token: 0x06003257 RID: 12887 RVA: 0x00100EEC File Offset: 0x000FF0EC
	public bool IsComplete()
	{
		return this.m_complete;
	}

	// Token: 0x06003258 RID: 12888 RVA: 0x00100EF4 File Offset: 0x000FF0F4
	public ZoneMgr.ChangeCompleteCallback GetCompleteCallback()
	{
		return this.m_completeCallback;
	}

	// Token: 0x06003259 RID: 12889 RVA: 0x00100EFC File Offset: 0x000FF0FC
	public void SetCompleteCallback(ZoneMgr.ChangeCompleteCallback callback)
	{
		this.m_completeCallback = callback;
	}

	// Token: 0x0600325A RID: 12890 RVA: 0x00100F05 File Offset: 0x000FF105
	public object GetCompleteCallbackUserData()
	{
		return this.m_completeCallbackUserData;
	}

	// Token: 0x0600325B RID: 12891 RVA: 0x00100F0D File Offset: 0x000FF10D
	public void SetCompleteCallbackUserData(object userData)
	{
		this.m_completeCallbackUserData = userData;
	}

	// Token: 0x0600325C RID: 12892 RVA: 0x00100F18 File Offset: 0x000FF118
	public void FireCompleteCallback()
	{
		this.DebugPrint("ZoneChangeList.FireCompleteCallback() - m_id={0} m_taskList={1} m_changes.Count={2} m_complete={3} m_completeCallback={4}", new object[]
		{
			this.m_id,
			(this.m_taskList == null) ? "(null)" : this.m_taskList.GetId().ToString(),
			this.m_changes.Count,
			this.m_complete,
			(this.m_completeCallback == null) ? "(null)" : "(not null)"
		});
		if (this.m_completeCallback == null)
		{
			return;
		}
		this.m_completeCallback(this, this.m_completeCallbackUserData);
	}

	// Token: 0x0600325D RID: 12893 RVA: 0x00100FBC File Offset: 0x000FF1BC
	public PowerTaskList GetTaskList()
	{
		return this.m_taskList;
	}

	// Token: 0x0600325E RID: 12894 RVA: 0x00100FC4 File Offset: 0x000FF1C4
	public void SetTaskList(PowerTaskList taskList)
	{
		this.m_taskList = taskList;
	}

	// Token: 0x0600325F RID: 12895 RVA: 0x00100FCD File Offset: 0x000FF1CD
	public List<ZoneChange> GetChanges()
	{
		return this.m_changes;
	}

	// Token: 0x06003260 RID: 12896 RVA: 0x00100FD5 File Offset: 0x000FF1D5
	public ZoneChange GetChange(int index)
	{
		return this.m_changes[index];
	}

	// Token: 0x06003261 RID: 12897 RVA: 0x00100FE3 File Offset: 0x000FF1E3
	public ZoneChange GetLocalTriggerChange()
	{
		if (!this.IsLocal())
		{
			return null;
		}
		if (this.m_changes.Count <= 0)
		{
			return null;
		}
		return this.m_changes[0];
	}

	// Token: 0x06003262 RID: 12898 RVA: 0x0010100C File Offset: 0x000FF20C
	public Card GetLocalTriggerCard()
	{
		ZoneChange localTriggerChange = this.GetLocalTriggerChange();
		if (localTriggerChange == null)
		{
			return null;
		}
		return localTriggerChange.GetEntity().GetCard();
	}

	// Token: 0x06003263 RID: 12899 RVA: 0x00101030 File Offset: 0x000FF230
	public void AddChange(ZoneChange change)
	{
		this.m_changes.Add(change);
	}

	// Token: 0x06003264 RID: 12900 RVA: 0x0010103E File Offset: 0x000FF23E
	public void RemoveChange(ZoneChange change)
	{
		this.m_changes.Remove(change);
	}

	// Token: 0x06003265 RID: 12901 RVA: 0x0010104D File Offset: 0x000FF24D
	public void InsertChange(int index, ZoneChange change)
	{
		this.m_changes.Insert(index, change);
	}

	// Token: 0x06003266 RID: 12902 RVA: 0x0010105C File Offset: 0x000FF25C
	public void ClearChanges()
	{
		this.m_changes.Clear();
	}

	// Token: 0x06003267 RID: 12903 RVA: 0x00101069 File Offset: 0x000FF269
	public IEnumerator ProcessChanges()
	{
		this.DebugPrint("ZoneChangeList.ProcessChanges() - m_id={0} m_taskList={1} m_changes.Count={2}", new object[]
		{
			this.m_id,
			(this.m_taskList == null) ? "(null)" : this.m_taskList.GetId().ToString(),
			this.m_changes.Count
		});
		while (GameState.Get().MustWaitForChoices())
		{
			yield return null;
		}
		HashSet<global::Entity> loadingEntities = new HashSet<global::Entity>();
		Map<global::Player, DyingSecretGroup> dyingSecretMap = null;
		int i = 0;
		while (i < this.m_changes.Count)
		{
			ZoneChange change = this.m_changes[i];
			this.DebugPrint("ZoneChangeList.ProcessChanges() - processing index={0} change={1}", new object[]
			{
				i,
				change
			});
			global::Entity entity = change.GetEntity();
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
			if (powerTask == null)
			{
				goto IL_2FA;
			}
			if (!powerTask.IsCompleted())
			{
				if (loadingEntities.Contains(entity))
				{
					this.DebugPrint("ZoneChangeList.ProcessChanges() - START waiting for {0} to load (powerTask=(not null))", new object[]
					{
						card
					});
					yield return ZoneMgr.Get().StartCoroutine(this.WaitForAndRemoveLoadingEntity(loadingEntities, entity, card));
					this.DebugPrint("ZoneChangeList.ProcessChanges() - END waiting for {0} to load (powerTask=(not null))", new object[]
					{
						card
					});
				}
				while (!GameState.Get().GetPowerProcessor().CanDoTask(powerTask))
				{
					yield return null;
				}
				while (this.ShouldWaitForOldHero(entity))
				{
					yield return null;
				}
				powerTask.DoTask();
				if (entity.IsLoadingAssets())
				{
					loadingEntities.Add(entity);
					goto IL_2FA;
				}
				goto IL_2FA;
			}
			IL_8F4:
			int num = i + 1;
			i = num;
			continue;
			IL_2FA:
			if (!this.ShouldIgnoreZoneChange(entity))
			{
				bool zoneChanged = dstZoneTag != TAG_ZONE.INVALID && srcZone != dstZone;
				bool controllerChanged = dstControllerId != 0 && srcControllerId != dstControllerId;
				bool posChanged = zoneChanged || (dstPos != 0 && srcPos != dstPos);
				bool revealed = powerTask != null && powerTask.GetPower().Type == Network.PowerType.SHOW_ENTITY;
				if (UniversalInputManager.UsePhoneUI && this.IsDisplayableDyingSecret(entity, card, srcZone, dstZone))
				{
					if (dyingSecretMap == null)
					{
						dyingSecretMap = new Map<global::Player, DyingSecretGroup>();
					}
					global::Player controller = card.GetController();
					DyingSecretGroup dyingSecretGroup;
					if (!dyingSecretMap.TryGetValue(controller, out dyingSecretGroup))
					{
						dyingSecretGroup = new DyingSecretGroup();
						dyingSecretMap.Add(controller, dyingSecretGroup);
					}
					dyingSecretGroup.AddCard(card);
				}
				if (zoneChanged || controllerChanged || revealed)
				{
					bool transitionedZones = zoneChanged || controllerChanged;
					bool flag = revealed && entity.GetZone() == TAG_ZONE.SECRET;
					if (transitionedZones || !flag)
					{
						if (srcZone != null)
						{
							this.m_dirtyZones.Add(srcZone);
						}
						if (dstZone != null)
						{
							this.m_dirtyZones.Add(dstZone);
						}
						this.DebugPrint("ZoneChangeList.ProcessChanges() - TRANSITIONING card {0} to {1}", new object[]
						{
							card,
							dstZone
						});
					}
					if (loadingEntities.Contains(entity))
					{
						this.DebugPrint("ZoneChangeList.ProcessChanges() - START waiting for {0} to load (zoneChanged={1} controllerChanged={2} powerTask=(not null))", new object[]
						{
							card,
							zoneChanged,
							controllerChanged
						});
						yield return ZoneMgr.Get().StartCoroutine(this.WaitForAndRemoveLoadingEntity(loadingEntities, entity, card));
						this.DebugPrint("ZoneChangeList.ProcessChanges() - END waiting for {0} to load (zoneChanged={1} controllerChanged={2} powerTask=(not null))", new object[]
						{
							card,
							zoneChanged,
							controllerChanged
						});
					}
					if (!card.IsActorReady() || card.IsBeingDrawnByOpponent())
					{
						this.DebugPrint("ZoneChangeList.ProcessChanges() - START waiting for {0} to become ready (zoneChanged={1} controllerChanged={2} powerTask=(not null))", new object[]
						{
							card,
							zoneChanged,
							controllerChanged
						});
						if (card.GetPrevZone() is ZoneDeck && card.GetZone() is ZoneHand && card.GetPrevZone().GetController() == card.GetZone().GetController() && TurnStartManager.Get().IsCardDrawHandled(card))
						{
							TurnStartManager.Get().DrawCardImmediately(card);
						}
						while (!card.IsActorReady() || card.IsBeingDrawnByOpponent())
						{
							yield return null;
						}
						this.DebugPrint("ZoneChangeList.ProcessChanges() - END waiting for {0} to become ready (zoneChanged={1} controllerChanged={2} powerTask=(not null))", new object[]
						{
							card,
							zoneChanged,
							controllerChanged
						});
					}
					Log.Zone.Print("ZoneChangeList.ProcessChanges() - id={0} local={1} {2} zone from {3} -> {4}", new object[]
					{
						this.m_id,
						this.IsLocal(),
						card,
						srcZone,
						dstZone
					});
					if (transitionedZones)
					{
						if (srcZone is ZonePlay && srcZone.m_Side == global::Player.Side.OPPOSING && dstZone is ZoneHand && dstZone.m_Side == global::Player.Side.OPPOSING)
						{
							Log.FaceDownCard.Print("ZoneChangeList.ProcessChanges() - id={0} {1}.TransitionToZone(): {2} -> {3}", new object[]
							{
								this.m_id,
								card,
								srcZone,
								dstZone
							});
							this.m_taskList.DebugDump(Log.FaceDownCard);
						}
						card.SetZonePosition(0);
						card.TransitionToZone(dstZone, change);
					}
					else if (revealed)
					{
						card.UpdateActor(false, null);
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
						this.m_dirtyZones.Add(srcZone);
					}
					if (dstZone != null)
					{
						this.m_dirtyZones.Add(dstZone);
					}
					if (card.m_minionWasMovedFromSrcToDst != null && !this.IsLocal())
					{
						this.GenerateLocalChangelistForMovedMinionWhileProcessingServerChangelist(card);
					}
					else
					{
						Log.Zone.Print("ZoneChangeList.ProcessChanges() - id={0} local={1} {2} pos from {3} -> {4}", new object[]
						{
							this.m_id,
							this.IsLocal(),
							card,
							srcPos,
							dstPos
						});
						card.SetZonePosition(dstPos);
					}
				}
				change = null;
				entity = null;
				card = null;
				powerTask = null;
				srcZone = null;
				dstZone = null;
				goto IL_8F4;
			}
			goto IL_8F4;
		}
		while (this.ShowNewHeroStats())
		{
			yield return null;
		}
		if (this.IsCanceledChangeList())
		{
			this.SetZoneInputBlocking(false);
		}
		this.ProcessDyingSecrets(dyingSecretMap);
		ZoneMgr.Get().ProcessGeneratedLocalChangeLists(this.m_generatedLocalChangeLists);
		ZoneMgr.Get().StartCoroutine(this.UpdateDirtyZones(loadingEntities));
		yield break;
	}

	// Token: 0x06003268 RID: 12904 RVA: 0x00101078 File Offset: 0x000FF278
	private void GenerateLocalChangelistForMovedMinionWhileProcessingServerChangelist(Card card)
	{
		if (card == null || card.m_minionWasMovedFromSrcToDst == null)
		{
			return;
		}
		ZoneChangeList zoneChangeList = new ZoneChangeList();
		ZoneChange zoneChange = new ZoneChange();
		zoneChange.SetEntity(card.GetEntity());
		zoneChange.SetSourcePosition(card.GetZonePosition());
		zoneChange.SetDestinationPosition(card.GetEntity().GetRealTimeZonePosition());
		Log.Zone.Print("ZoneMgr.GenerateLocalChangelistForMovedMinionWhileProcessingServerChangelist() - AddChange() changeList: {0}, change: {1}", new object[]
		{
			zoneChangeList,
			zoneChange
		});
		zoneChangeList.AddChange(zoneChange);
		this.m_generatedLocalChangeLists.Add(zoneChangeList);
	}

	// Token: 0x06003269 RID: 12905 RVA: 0x001010FC File Offset: 0x000FF2FC
	private bool IsCardMove(int zoneChangeIndex, TAG_ZONE fromZone, TAG_ZONE toZone)
	{
		if (zoneChangeIndex < 0 || zoneChangeIndex >= this.m_changes.Count)
		{
			return false;
		}
		if (this.m_changes[zoneChangeIndex].GetDestinationZoneTag() != toZone)
		{
			return false;
		}
		if (this.m_changes[zoneChangeIndex].GetSourceZoneTag() == fromZone)
		{
			return true;
		}
		ZoneChange zoneChange = null;
		int num = zoneChangeIndex - 1;
		while (zoneChangeIndex >= 0)
		{
			zoneChange = this.m_changes[num];
			if (zoneChange.HasDestinationZoneTag())
			{
				break;
			}
			num--;
		}
		return zoneChange.GetDestinationZoneTag() == fromZone;
	}

	// Token: 0x0600326A RID: 12906 RVA: 0x00101177 File Offset: 0x000FF377
	public bool IsCardDraw(int zoneChangeIndex)
	{
		return this.IsCardMove(zoneChangeIndex, TAG_ZONE.DECK, TAG_ZONE.HAND);
	}

	// Token: 0x0600326B RID: 12907 RVA: 0x00101182 File Offset: 0x000FF382
	public bool IsCardMill(int zoneChangeIndex)
	{
		return this.IsCardMove(zoneChangeIndex, TAG_ZONE.DECK, TAG_ZONE.GRAVEYARD);
	}

	// Token: 0x0600326C RID: 12908 RVA: 0x00101190 File Offset: 0x000FF390
	public override string ToString()
	{
		return string.Format("id={0} changes={1} complete={2} local={3} localTrigger=[{4}]", new object[]
		{
			this.m_id,
			this.m_changes.Count,
			this.m_complete,
			this.IsLocal(),
			this.GetLocalTriggerChange()
		});
	}

	// Token: 0x0600326D RID: 12909 RVA: 0x001011F3 File Offset: 0x000FF3F3
	private bool IsDisplayableDyingSecret(global::Entity entity, Card card, Zone srcZone, Zone dstZone)
	{
		return entity.IsSecret() && srcZone is ZoneSecret && dstZone is ZoneGraveyard;
	}

	// Token: 0x0600326E RID: 12910 RVA: 0x00101218 File Offset: 0x000FF418
	private void ProcessDyingSecrets(Map<global::Player, DyingSecretGroup> dyingSecretMap)
	{
		if (dyingSecretMap == null)
		{
			return;
		}
		Map<global::Player, DeadSecretGroup> map = null;
		foreach (KeyValuePair<global::Player, DyingSecretGroup> keyValuePair in dyingSecretMap)
		{
			global::Player key = keyValuePair.Key;
			DyingSecretGroup value = keyValuePair.Value;
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
				}
				else
				{
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
						map = new Map<global::Player, DeadSecretGroup>();
					}
					DeadSecretGroup deadSecretGroup;
					if (!map.TryGetValue(key, out deadSecretGroup))
					{
						deadSecretGroup = new DeadSecretGroup();
						deadSecretGroup.SetMainCard(mainCard);
						map.Add(key, deadSecretGroup);
					}
					deadSecretGroup.AddCard(card);
				}
			}
		}
		BigCard.Get().ShowSecretDeaths(map);
	}

	// Token: 0x0600326F RID: 12911 RVA: 0x0010133C File Offset: 0x000FF53C
	private IEnumerator WaitForAndRemoveLoadingEntity(HashSet<global::Entity> loadingEntities, global::Entity entity, Card card)
	{
		while (this.IsEntityLoading(entity, card))
		{
			yield return null;
		}
		loadingEntities.Remove(entity);
		yield break;
	}

	// Token: 0x06003270 RID: 12912 RVA: 0x00101360 File Offset: 0x000FF560
	private bool IsEntityLoading(global::Entity entity, Card card)
	{
		return entity.IsLoadingAssets() || (card != null && card.IsActorLoading());
	}

	// Token: 0x06003271 RID: 12913 RVA: 0x00101380 File Offset: 0x000FF580
	private IEnumerator UpdateDirtyZones(HashSet<global::Entity> loadingEntities)
	{
		this.DebugPrint("ZoneChangeList.UpdateDirtyZones() - m_id={0} loadingEntities.Count={1} m_dirtyZones.Count={2}", new object[]
		{
			this.m_id,
			loadingEntities.Count,
			this.m_dirtyZones.Count
		});
		foreach (global::Entity entity in loadingEntities)
		{
			Card card = entity.GetCard();
			this.DebugPrint("ZoneChangeList.UpdateDirtyZones() - m_id={0} START waiting for {1} to load (card={2})", new object[]
			{
				this.m_id,
				entity,
				card
			});
			while (this.IsEntityLoading(entity, card))
			{
				yield return null;
			}
			this.DebugPrint("ZoneChangeList.UpdateDirtyZones() - m_id={0} END waiting for {1} to load (card={2})", new object[]
			{
				this.m_id,
				entity,
				card
			});
			card = null;
			entity = null;
		}
		HashSet<global::Entity>.Enumerator enumerator = default(HashSet<global::Entity>.Enumerator);
		if (this.IsDeathBlock())
		{
			float num = ZoneMgr.Get().RemoveNextDeathBlockLayoutDelaySec();
			if (num >= 0f)
			{
				yield return new WaitForSeconds(num);
			}
			foreach (Zone zone in this.m_dirtyZones)
			{
				zone.UpdateLayout();
			}
			this.m_dirtyZones.Clear();
		}
		else
		{
			Zone[] array = new Zone[this.m_dirtyZones.Count];
			this.m_dirtyZones.CopyTo(array);
			foreach (Zone zone2 in array)
			{
				this.DebugPrint("ZoneChangeList.UpdateDirtyZones() - m_id={0} START waiting for zone {1}", new object[]
				{
					this.m_id,
					zone2
				});
				if (zone2 is ZoneHand)
				{
					ZoneMgr.Get().StartCoroutine(this.ZoneHand_UpdateLayout((ZoneHand)zone2));
				}
				else
				{
					zone2.AddUpdateLayoutCompleteCallback(new Zone.UpdateLayoutCompleteCallback(this.OnUpdateLayoutComplete));
					zone2.UpdateLayout();
				}
			}
		}
		ZoneMgr.Get().StartCoroutine(this.FinishWhenPossible());
		yield break;
		yield break;
	}

	// Token: 0x06003272 RID: 12914 RVA: 0x00101396 File Offset: 0x000FF596
	private bool IsDeathBlock()
	{
		return this.m_taskList != null && this.m_taskList.IsDeathBlock();
	}

	// Token: 0x06003273 RID: 12915 RVA: 0x001013AD File Offset: 0x000FF5AD
	private IEnumerator ZoneHand_UpdateLayout(ZoneHand zoneHand)
	{
		for (;;)
		{
			if (zoneHand.GetCards().Find((Card card) => (!(TurnStartManager.Get() != null) || !TurnStartManager.Get().IsCardDrawHandled(card)) && !card.IsDoNotSort() && !card.IsActorReady()) == null)
			{
				break;
			}
			yield return null;
		}
		zoneHand.AddUpdateLayoutCompleteCallback(new Zone.UpdateLayoutCompleteCallback(this.OnUpdateLayoutComplete));
		zoneHand.UpdateLayout();
		yield break;
	}

	// Token: 0x06003274 RID: 12916 RVA: 0x001013C3 File Offset: 0x000FF5C3
	private void OnUpdateLayoutComplete(Zone zone, object userData)
	{
		this.DebugPrint("ZoneChangeList.OnUpdateLayoutComplete() - m_id={0} END waiting for zone {1}", new object[]
		{
			this.m_id,
			zone
		});
		this.m_dirtyZones.Remove(zone);
	}

	// Token: 0x06003275 RID: 12917 RVA: 0x001013F8 File Offset: 0x000FF5F8
	private global::Entity GetNewHeroPlayedFromPowerTaskList()
	{
		PowerTaskList taskList = this.GetTaskList();
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
		global::Entity sourceEntity = taskList.GetSourceEntity(true);
		if (sourceEntity == null)
		{
			Log.Zone.PrintWarning("ZoneChangelist.GetNewHeroPlayedFromPowerTaskList() - source is null.", Array.Empty<object>());
			return null;
		}
		if (!sourceEntity.IsHero())
		{
			return null;
		}
		return sourceEntity;
	}

	// Token: 0x06003276 RID: 12918 RVA: 0x00101454 File Offset: 0x000FF654
	private bool ShowNewHeroStats()
	{
		global::Entity newHeroPlayedFromPowerTaskList = this.GetNewHeroPlayedFromPowerTaskList();
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

	// Token: 0x06003277 RID: 12919 RVA: 0x001014C0 File Offset: 0x000FF6C0
	private bool ShouldWaitForOldHero(global::Entity entity)
	{
		if (!entity.IsHero())
		{
			return false;
		}
		global::Entity newHeroPlayedFromPowerTaskList = this.GetNewHeroPlayedFromPowerTaskList();
		return newHeroPlayedFromPowerTaskList != null && newHeroPlayedFromPowerTaskList.GetEntityId() != entity.GetEntityId() && !newHeroPlayedFromPowerTaskList.GetCard().IsActorReady();
	}

	// Token: 0x06003278 RID: 12920 RVA: 0x00101501 File Offset: 0x000FF701
	private bool ShouldIgnoreZoneChange(global::Entity entity)
	{
		return entity.GetCard() == null || (!this.IsOldHero(entity) && this.m_ignoreCardZoneChanges);
	}

	// Token: 0x06003279 RID: 12921 RVA: 0x00101524 File Offset: 0x000FF724
	private bool IsOldHero(global::Entity entity)
	{
		global::Entity newHeroPlayedFromPowerTaskList = this.GetNewHeroPlayedFromPowerTaskList();
		return newHeroPlayedFromPowerTaskList != null && entity.IsHero() && newHeroPlayedFromPowerTaskList.GetEntityId() != entity.GetEntityId();
	}

	// Token: 0x0600327A RID: 12922 RVA: 0x00101558 File Offset: 0x000FF758
	private IEnumerator FinishWhenPossible()
	{
		while (this.m_dirtyZones.Count > 0)
		{
			yield return null;
		}
		while (GameState.Get().IsBusy())
		{
			yield return null;
		}
		this.Finish();
		yield break;
	}

	// Token: 0x0600327B RID: 12923 RVA: 0x00101567 File Offset: 0x000FF767
	private void Finish()
	{
		this.m_complete = true;
		Log.Zone.Print("ZoneChangeList.Finish() - {0}", new object[]
		{
			this
		});
	}

	// Token: 0x0600327C RID: 12924 RVA: 0x00101589 File Offset: 0x000FF789
	[Conditional("ZONE_CHANGE_DEBUG")]
	private void DebugPrint(string format, params object[] args)
	{
		Log.Zone.Print(format, args);
	}

	// Token: 0x04001BE8 RID: 7144
	private int m_id;

	// Token: 0x04001BE9 RID: 7145
	private int m_predictedPosition;

	// Token: 0x04001BEA RID: 7146
	private bool m_ignoreCardZoneChanges;

	// Token: 0x04001BEB RID: 7147
	private bool m_canceledChangeList;

	// Token: 0x04001BEC RID: 7148
	private PowerTaskList m_taskList;

	// Token: 0x04001BED RID: 7149
	private List<ZoneChange> m_changes = new List<ZoneChange>();

	// Token: 0x04001BEE RID: 7150
	private HashSet<Zone> m_dirtyZones = new HashSet<Zone>();

	// Token: 0x04001BEF RID: 7151
	private List<ZoneChangeList> m_generatedLocalChangeLists = new List<ZoneChangeList>();

	// Token: 0x04001BF0 RID: 7152
	private bool m_complete;

	// Token: 0x04001BF1 RID: 7153
	private ZoneMgr.ChangeCompleteCallback m_completeCallback;

	// Token: 0x04001BF2 RID: 7154
	private object m_completeCallbackUserData;
}
