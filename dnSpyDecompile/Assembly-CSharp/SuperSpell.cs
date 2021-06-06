using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Core;
using HutongGames.PlayMaker;
using PegasusGame;
using UnityEngine;

// Token: 0x0200098E RID: 2446
public class SuperSpell : Spell
{
	// Token: 0x060085D9 RID: 34265 RVA: 0x002B368C File Offset: 0x002B188C
	public override List<GameObject> GetVisualTargets()
	{
		return this.m_visualTargets;
	}

	// Token: 0x060085DA RID: 34266 RVA: 0x002B3694 File Offset: 0x002B1894
	public override GameObject GetVisualTarget()
	{
		if (this.m_visualTargets.Count != 0)
		{
			return this.m_visualTargets[0];
		}
		return null;
	}

	// Token: 0x060085DB RID: 34267 RVA: 0x002B36B1 File Offset: 0x002B18B1
	public override void AddVisualTarget(GameObject go)
	{
		this.m_visualTargets.Add(go);
	}

	// Token: 0x060085DC RID: 34268 RVA: 0x002B36BF File Offset: 0x002B18BF
	public override void AddVisualTargets(List<GameObject> targets)
	{
		this.m_visualTargets.AddRange(targets);
	}

	// Token: 0x060085DD RID: 34269 RVA: 0x002B36CD File Offset: 0x002B18CD
	public override bool RemoveVisualTarget(GameObject go)
	{
		return this.m_visualTargets.Remove(go);
	}

	// Token: 0x060085DE RID: 34270 RVA: 0x002B36DB File Offset: 0x002B18DB
	public override void RemoveAllVisualTargets()
	{
		this.m_visualTargets.Clear();
	}

	// Token: 0x060085DF RID: 34271 RVA: 0x002B36E8 File Offset: 0x002B18E8
	public override bool IsVisualTarget(GameObject go)
	{
		return this.m_visualTargets.Contains(go);
	}

	// Token: 0x060085E0 RID: 34272 RVA: 0x002B36F8 File Offset: 0x002B18F8
	public override Card GetVisualTargetCard()
	{
		GameObject visualTarget = this.GetVisualTarget();
		if (visualTarget == null)
		{
			return null;
		}
		return visualTarget.GetComponent<Card>();
	}

	// Token: 0x060085E1 RID: 34273 RVA: 0x002B3720 File Offset: 0x002B1920
	protected bool AddPowerTargetsInternal(bool fallbackToStartBlockTarget)
	{
		this.m_visualToTargetIndexMap.Clear();
		this.m_targetToMetaDataMap.Clear();
		if (!base.CanAddPowerTargets())
		{
			return false;
		}
		if (this.HasChain() && !this.AddPrimaryChainTarget())
		{
			return false;
		}
		if (!base.AddMultiplePowerTargets())
		{
			return false;
		}
		if (this.m_targets.Count > 0)
		{
			return true;
		}
		if (!fallbackToStartBlockTarget)
		{
			return true;
		}
		Network.HistBlockStart blockStart = this.m_taskList.GetBlockStart();
		return blockStart == null || blockStart.Target == 0 || base.AddSinglePowerTarget_FromBlockStart(blockStart);
	}

	// Token: 0x060085E2 RID: 34274 RVA: 0x002B379E File Offset: 0x002B199E
	public override bool AddPowerTargets()
	{
		return this.AddPowerTargetsInternal(true);
	}

	// Token: 0x060085E3 RID: 34275 RVA: 0x002B37A8 File Offset: 0x002B19A8
	protected override void AddTargetFromMetaData(int metaDataIndex, Card targetCard)
	{
		int count = this.m_targets.Count;
		this.m_targetToMetaDataMap[count] = metaDataIndex;
		this.AddTarget(targetCard.gameObject);
	}

	// Token: 0x060085E4 RID: 34276 RVA: 0x002B37DC File Offset: 0x002B19DC
	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.UpdatePosition();
		base.UpdateOrientation();
		this.m_currentTargetIndex = 0;
		if (this.HasStart())
		{
			this.SpawnStart();
			this.m_startSpell.SafeActivateState(SpellStateType.BIRTH);
			if (this.m_startSpell.GetActiveState() == SpellStateType.NONE)
			{
				this.m_startSpell = null;
			}
		}
		base.OnBirth(prevStateType);
	}

	// Token: 0x060085E5 RID: 34277 RVA: 0x002B3834 File Offset: 0x002B1A34
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.m_settingUpAction = true;
		this.UpdateTargets();
		if (this.m_Location == SpellLocation.CHOSEN_TARGET)
		{
			this.m_positionDirty = true;
		}
		base.UpdatePosition();
		if (this.m_Facing == SpellFacing.TOWARDS_CHOSEN_TARGET)
		{
			this.m_orientationDirty = true;
		}
		base.UpdateOrientation();
		this.m_currentTargetIndex = this.GetPrimaryTargetIndex();
		this.UpdatePendingStateChangeFlags(SpellStateType.ACTION);
		this.DoAction();
		base.OnAction(prevStateType);
		this.m_settingUpAction = false;
		this.FinishIfPossible();
	}

	// Token: 0x060085E6 RID: 34278 RVA: 0x002B38A9 File Offset: 0x002B1AA9
	protected override void OnCancel(SpellStateType prevStateType)
	{
		this.UpdatePendingStateChangeFlags(SpellStateType.CANCEL);
		if (this.m_startSpell != null)
		{
			this.m_startSpell.SafeActivateState(SpellStateType.CANCEL);
			this.m_startSpell = null;
		}
		base.OnCancel(prevStateType);
		this.FinishIfPossible();
	}

	// Token: 0x060085E7 RID: 34279 RVA: 0x002B38E0 File Offset: 0x002B1AE0
	public override void OnStateFinished()
	{
		if (base.GuessNextStateType() == SpellStateType.NONE && this.AreEffectsActive())
		{
			this.m_pendingNoneStateChange = true;
			return;
		}
		base.OnStateFinished();
	}

	// Token: 0x060085E8 RID: 34280 RVA: 0x002B3900 File Offset: 0x002B1B00
	public override void OnSpellFinished()
	{
		if (this.AreEffectsActive())
		{
			this.m_pendingSpellFinish = true;
			return;
		}
		base.OnSpellFinished();
	}

	// Token: 0x060085E9 RID: 34281 RVA: 0x002B3918 File Offset: 0x002B1B18
	public override void OnFsmStateStarted(FsmState state, SpellStateType stateType)
	{
		if (this.m_activeStateChange == stateType)
		{
			return;
		}
		if (stateType == SpellStateType.NONE && this.AreEffectsActive())
		{
			this.m_pendingSpellFinish = true;
			this.m_pendingNoneStateChange = true;
			return;
		}
		base.OnFsmStateStarted(state, stateType);
	}

	// Token: 0x060085EA RID: 34282 RVA: 0x002B3946 File Offset: 0x002B1B46
	public override bool CanPurge()
	{
		return this.m_activeClonedSpells.Count <= 0 && base.CanPurge();
	}

	// Token: 0x060085EB RID: 34283 RVA: 0x002B395E File Offset: 0x002B1B5E
	private void DoAction()
	{
		if (this.CheckAndWaitForGameEventsThenDoAction())
		{
			return;
		}
		if (this.CheckAndWaitForStartDelayThenDoAction())
		{
			return;
		}
		if (this.CheckAndWaitForStartPrefabThenDoAction())
		{
			return;
		}
		this.DoActionNow();
	}

	// Token: 0x060085EC RID: 34284 RVA: 0x002B3981 File Offset: 0x002B1B81
	private bool CheckAndWaitForGameEventsThenDoAction()
	{
		if (this.m_taskList == null)
		{
			return false;
		}
		if (this.m_ActionInfo.m_ShowSpellVisuals == SpellVisualShowTime.DURING_GAME_EVENTS)
		{
			return this.DoActionDuringGameEvents();
		}
		if (this.m_ActionInfo.m_ShowSpellVisuals == SpellVisualShowTime.AFTER_GAME_EVENTS)
		{
			this.DoActionAfterGameEvents();
			return true;
		}
		return false;
	}

	// Token: 0x060085ED RID: 34285 RVA: 0x002B39BC File Offset: 0x002B1BBC
	private bool DoActionDuringGameEvents()
	{
		this.m_taskList.DoAllTasks();
		if (this.m_taskList.IsComplete())
		{
			return false;
		}
		QueueList<PowerTask> queueList = this.DetermineTasksToWaitFor(0, this.m_taskList.GetTaskList().Count);
		if (queueList.Count == 0)
		{
			return false;
		}
		base.StartCoroutine(this.DoDelayedActionDuringGameEvents(queueList));
		return true;
	}

	// Token: 0x060085EE RID: 34286 RVA: 0x002B3A14 File Offset: 0x002B1C14
	private IEnumerator DoDelayedActionDuringGameEvents(QueueList<PowerTask> tasksToWaitFor)
	{
		this.m_effectsPendingFinish++;
		yield return base.StartCoroutine(this.WaitForTasks(tasksToWaitFor));
		this.m_effectsPendingFinish--;
		if (this.CheckAndWaitForStartDelayThenDoAction())
		{
			yield break;
		}
		if (this.CheckAndWaitForStartPrefabThenDoAction())
		{
			yield break;
		}
		this.DoActionNow();
		yield break;
	}

	// Token: 0x060085EF RID: 34287 RVA: 0x002B3A2C File Offset: 0x002B1C2C
	private global::Entity GetEntityFromZoneChangePowerTask(PowerTask task)
	{
		global::Entity result;
		int num;
		this.GetZoneChangeFromPowerTask(task, out result, out num);
		return result;
	}

	// Token: 0x060085F0 RID: 34288 RVA: 0x002B3A48 File Offset: 0x002B1C48
	private bool GetZoneChangeFromPowerTask(PowerTask task, out global::Entity entity, out int zoneTag)
	{
		entity = null;
		zoneTag = 0;
		Network.PowerHistory power = task.GetPower();
		global::Entity entity2;
		switch (power.Type)
		{
		case Network.PowerType.FULL_ENTITY:
		{
			Network.HistFullEntity histFullEntity = (Network.HistFullEntity)power;
			entity2 = GameState.Get().GetEntity(histFullEntity.Entity.ID);
			if (entity2.GetCard() == null)
			{
				return false;
			}
			using (List<Network.Entity.Tag>.Enumerator enumerator = histFullEntity.Entity.Tags.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Network.Entity.Tag tag = enumerator.Current;
					if (tag.Name == 49)
					{
						entity = entity2;
						zoneTag = tag.Value;
						return true;
					}
				}
				return false;
			}
			break;
		}
		case Network.PowerType.SHOW_ENTITY:
			break;
		case Network.PowerType.HIDE_ENTITY:
			return false;
		case Network.PowerType.TAG_CHANGE:
			goto IL_13A;
		default:
			return false;
		}
		Network.HistShowEntity histShowEntity = (Network.HistShowEntity)power;
		entity2 = GameState.Get().GetEntity(histShowEntity.Entity.ID);
		if (entity2.GetCard() == null)
		{
			return false;
		}
		using (List<Network.Entity.Tag>.Enumerator enumerator = histShowEntity.Entity.Tags.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Network.Entity.Tag tag2 = enumerator.Current;
				if (tag2.Name == 49)
				{
					entity = entity2;
					zoneTag = tag2.Value;
					return true;
				}
			}
			return false;
		}
		IL_13A:
		Network.HistTagChange histTagChange = (Network.HistTagChange)power;
		entity2 = GameState.Get().GetEntity(histTagChange.Entity);
		if (entity2.GetCard() == null)
		{
			return false;
		}
		if (histTagChange.Tag == 49)
		{
			entity = entity2;
			zoneTag = histTagChange.Value;
			return true;
		}
		return false;
	}

	// Token: 0x060085F1 RID: 34289 RVA: 0x002B3BF4 File Offset: 0x002B1DF4
	private void DoActionAfterGameEvents()
	{
		this.m_effectsPendingFinish++;
		PowerTaskList.CompleteCallback callback = delegate(PowerTaskList taskList, int startIndex, int count, object userData)
		{
			this.m_effectsPendingFinish--;
			if (this.CheckAndWaitForStartDelayThenDoAction())
			{
				return;
			}
			if (this.CheckAndWaitForStartPrefabThenDoAction())
			{
				return;
			}
			this.DoActionNow();
		};
		this.m_taskList.DoAllTasks(callback);
	}

	// Token: 0x060085F2 RID: 34290 RVA: 0x002B3C28 File Offset: 0x002B1E28
	private bool CheckAndWaitForStartDelayThenDoAction()
	{
		if (Mathf.Min(this.m_ActionInfo.m_StartDelayMax, this.m_ActionInfo.m_StartDelayMin) <= Mathf.Epsilon)
		{
			return false;
		}
		this.m_effectsPendingFinish++;
		base.StartCoroutine(this.WaitForStartDelayThenDoAction());
		return true;
	}

	// Token: 0x060085F3 RID: 34291 RVA: 0x002B3C75 File Offset: 0x002B1E75
	private IEnumerator WaitForStartDelayThenDoAction()
	{
		float seconds = UnityEngine.Random.Range(this.m_ActionInfo.m_StartDelayMin, this.m_ActionInfo.m_StartDelayMax);
		yield return new WaitForSeconds(seconds);
		this.m_effectsPendingFinish--;
		if (this.CheckAndWaitForStartPrefabThenDoAction())
		{
			yield break;
		}
		this.DoActionNow();
		yield break;
	}

	// Token: 0x060085F4 RID: 34292 RVA: 0x002B3C84 File Offset: 0x002B1E84
	private bool CheckAndWaitForStartPrefabThenDoAction()
	{
		if (!this.HasStart())
		{
			return false;
		}
		if (this.m_startSpell != null && this.m_startSpell.GetActiveState() == SpellStateType.IDLE)
		{
			return false;
		}
		if (this.m_startSpell == null)
		{
			this.SpawnStart();
		}
		this.m_startSpell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnStartSpellBirthStateFinished));
		if (this.m_startSpell.GetActiveState() != SpellStateType.BIRTH)
		{
			this.m_startSpell.SafeActivateState(SpellStateType.BIRTH);
			if (this.m_startSpell.GetActiveState() == SpellStateType.NONE)
			{
				this.m_startSpell = null;
				return false;
			}
		}
		return true;
	}

	// Token: 0x060085F5 RID: 34293 RVA: 0x002B3D15 File Offset: 0x002B1F15
	private void OnStartSpellBirthStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (prevStateType != SpellStateType.BIRTH)
		{
			return;
		}
		spell.RemoveStateFinishedCallback(new Spell.StateFinishedCallback(this.OnStartSpellBirthStateFinished), userData);
		this.DoActionNow();
	}

	// Token: 0x060085F6 RID: 34294 RVA: 0x002B3D38 File Offset: 0x002B1F38
	protected virtual void DoActionNow()
	{
		SpellAreaEffectInfo spellAreaEffectInfo = this.DetermineAreaEffectInfo();
		if (spellAreaEffectInfo != null)
		{
			this.SpawnAreaEffect(spellAreaEffectInfo);
		}
		bool flag = this.HasMissile();
		bool flag2 = this.HasImpact();
		bool flag3 = this.HasChain();
		if (this.GetVisualTargetCount() > 0 && (flag || flag2 || flag3))
		{
			if (flag)
			{
				if (flag3)
				{
					this.SpawnChainMissile();
				}
				else if (this.m_MissileInfo.m_SpawnInSequence)
				{
					this.SpawnMissileInSequence();
				}
				else
				{
					this.SpawnAllMissiles();
				}
			}
			else
			{
				if (flag2)
				{
					if (flag3)
					{
						this.SpawnImpact(this.m_currentTargetIndex);
					}
					else
					{
						this.SpawnAllImpacts();
					}
				}
				if (flag3)
				{
					this.SpawnChain();
				}
				this.DoStartSpellAction();
			}
		}
		else
		{
			this.DoStartSpellAction();
		}
		this.FinishIfPossible();
	}

	// Token: 0x060085F7 RID: 34295 RVA: 0x002B3DDD File Offset: 0x002B1FDD
	private bool HasStart()
	{
		return this.m_StartInfo != null && this.m_StartInfo.m_Enabled && this.m_StartInfo.m_Prefab != null;
	}

	// Token: 0x060085F8 RID: 34296 RVA: 0x002B3E08 File Offset: 0x002B2008
	private void SpawnStart()
	{
		this.m_effectsPendingFinish++;
		this.m_startSpell = this.CloneSpell(this.m_StartInfo.m_Prefab, null, null);
		this.m_startSpell.SetSource(base.GetSource());
		this.m_startSpell.AddTargets(base.GetTargets());
		if (this.m_StartInfo.m_UseSuperSpellLocation)
		{
			this.m_startSpell.SetPosition(base.transform.position);
		}
	}

	// Token: 0x060085F9 RID: 34297 RVA: 0x002B3E8C File Offset: 0x002B208C
	private void DoStartSpellAction()
	{
		if (this.m_startSpell == null)
		{
			return;
		}
		if (!this.m_startSpell.HasUsableState(SpellStateType.ACTION))
		{
			this.m_startSpell.UpdateTransform();
			this.m_startSpell.SafeActivateState(SpellStateType.DEATH);
		}
		else
		{
			this.m_startSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnStartSpellActionFinished));
			this.m_startSpell.ActivateState(SpellStateType.ACTION);
		}
		this.m_startSpell = null;
	}

	// Token: 0x060085FA RID: 34298 RVA: 0x002B3EF9 File Offset: 0x002B20F9
	private void OnStartSpellActionFinished(Spell spell, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.ACTION)
		{
			return;
		}
		spell.SafeActivateState(SpellStateType.DEATH);
	}

	// Token: 0x060085FB RID: 34299 RVA: 0x002B3F0C File Offset: 0x002B210C
	private bool HasMissile()
	{
		return this.m_MissileInfo != null && this.m_MissileInfo.m_Enabled && (this.m_MissileInfo.m_Prefab != null || this.m_MissileInfo.m_ReversePrefab != null);
	}

	// Token: 0x060085FC RID: 34300 RVA: 0x002B3F4B File Offset: 0x002B214B
	private void SpawnChainMissile()
	{
		this.SpawnMissile(this.GetPrimaryTargetIndex());
		this.DoStartSpellAction();
	}

	// Token: 0x060085FD RID: 34301 RVA: 0x002B3F60 File Offset: 0x002B2160
	private void SpawnMissileInSequence()
	{
		if (this.m_currentTargetIndex >= this.GetVisualTargetCount())
		{
			return;
		}
		this.SpawnMissile(this.m_currentTargetIndex);
		this.m_currentTargetIndex++;
		if (this.m_startSpell == null)
		{
			return;
		}
		if (this.m_StartInfo.m_DeathAfterAllMissilesFire)
		{
			if (this.m_currentTargetIndex >= this.GetVisualTargetCount())
			{
				this.DoStartSpellAction();
				return;
			}
			if (this.m_startSpell.HasUsableState(SpellStateType.ACTION))
			{
				this.m_startSpell.ActivateState(SpellStateType.ACTION);
				return;
			}
		}
		else
		{
			this.DoStartSpellAction();
		}
	}

	// Token: 0x060085FE RID: 34302 RVA: 0x002B3FE8 File Offset: 0x002B21E8
	private void SpawnAllMissiles()
	{
		for (int i = 0; i < this.GetVisualTargetCount(); i++)
		{
			this.SpawnMissile(i);
		}
		this.DoStartSpellAction();
	}

	// Token: 0x060085FF RID: 34303 RVA: 0x002B4013 File Offset: 0x002B2213
	private void SpawnMissile(int targetIndex)
	{
		this.m_effectsPendingFinish++;
		base.StartCoroutine(this.WaitAndSpawnMissile(targetIndex));
	}

	// Token: 0x06008600 RID: 34304 RVA: 0x002B4031 File Offset: 0x002B2231
	private IEnumerator WaitAndSpawnMissile(int targetIndex)
	{
		float seconds = UnityEngine.Random.Range(this.m_MissileInfo.m_SpawnDelaySecMin, this.m_MissileInfo.m_SpawnDelaySecMax);
		if (!this.m_MissileInfo.m_SpawnInSequence || targetIndex == 0)
		{
			yield return new WaitForSeconds(seconds);
		}
		if (this.m_MissileInfo.m_SpawnOffset > 0f && targetIndex > 0)
		{
			yield return new WaitForSeconds(this.m_MissileInfo.m_SpawnOffset * (float)targetIndex);
		}
		int metaDataIndexForTarget = this.GetMetaDataIndexForTarget(targetIndex);
		if (this.ShouldCompleteTasksUntilMetaData(metaDataIndexForTarget))
		{
			yield return base.StartCoroutine(this.CompleteTasksUntilMetaData(metaDataIndexForTarget));
		}
		if (this.m_visualTargets.Count <= targetIndex || this.m_visualTargets[targetIndex] == null)
		{
			this.m_effectsPendingFinish--;
			yield break;
		}
		GameObject source = base.GetSource();
		GameObject gameObject = this.m_visualTargets[targetIndex];
		if (this.m_MissileInfo.m_Prefab != null)
		{
			Spell spell;
			if (this.m_MissileInfo.m_UseSuperSpellLocation)
			{
				spell = this.CloneSpell(this.m_MissileInfo.m_Prefab, new Vector3?(base.transform.position), null);
				spell.ClearPositionDirtyFlag();
			}
			else
			{
				spell = this.CloneSpell(this.m_MissileInfo.m_Prefab, null, null);
			}
			spell.SetSource(source);
			spell.AddTarget(gameObject);
			spell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnMissileSpellStateFinished), targetIndex);
			spell.ActivateState(SpellStateType.BIRTH);
		}
		else
		{
			this.m_effectsPendingFinish--;
		}
		if (this.m_MissileInfo.m_ReversePrefab != null)
		{
			this.m_effectsPendingFinish++;
			base.StartCoroutine(this.SpawnReverseMissile(this.m_MissileInfo.m_ReversePrefab, source, gameObject, this.m_MissileInfo.m_reverseDelay));
		}
		yield break;
	}

	// Token: 0x06008601 RID: 34305 RVA: 0x002B4047 File Offset: 0x002B2247
	private IEnumerator SpawnReverseMissile(Spell cloneSpell, GameObject sourceObject, GameObject targetObject, float delay)
	{
		yield return new WaitForSeconds(delay);
		Spell spell = this.CloneSpell(cloneSpell, null, null);
		spell.SetSource(targetObject);
		spell.AddTarget(sourceObject);
		spell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnMissileSpellStateFinished), -1);
		spell.ActivateState(SpellStateType.BIRTH);
		yield break;
	}

	// Token: 0x06008602 RID: 34306 RVA: 0x002B4074 File Offset: 0x002B2274
	private void OnMissileSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (prevStateType != SpellStateType.BIRTH)
		{
			return;
		}
		spell.RemoveStateFinishedCallback(new Spell.StateFinishedCallback(this.OnMissileSpellStateFinished), userData);
		int num = (int)userData;
		bool reverse = num < 0;
		this.FireMissileOnPath(spell, num, reverse);
	}

	// Token: 0x06008603 RID: 34307 RVA: 0x002B40B0 File Offset: 0x002B22B0
	private void FireMissileOnPath(Spell missile, int targetIndex, bool reverse)
	{
		Vector3[] array = this.GenerateMissilePath(missile);
		float num = UnityEngine.Random.Range(this.m_MissileInfo.m_PathDurationMin, this.m_MissileInfo.m_PathDurationMax);
		Hashtable hashtable = iTween.Hash(new object[]
		{
			"path",
			array,
			"time",
			num,
			"easetype",
			this.m_MissileInfo.m_PathEaseType,
			"oncompletetarget",
			base.gameObject
		});
		if (reverse)
		{
			hashtable.Add("oncomplete", "OnReverseMissileTargetReached");
			hashtable.Add("oncompleteparams", missile);
		}
		else
		{
			Hashtable value = iTween.Hash(new object[]
			{
				"missile",
				missile,
				"targetIndex",
				targetIndex
			});
			hashtable.Add("oncomplete", "OnMissileTargetReached");
			hashtable.Add("oncompleteparams", value);
		}
		if (!object.Equals(array[0], array[2]))
		{
			hashtable.Add("orienttopath", this.m_MissileInfo.m_OrientToPath);
		}
		if (this.m_MissileInfo.m_TargetJoint.Length > 0)
		{
			GameObject gameObject = SceneUtils.FindChildBySubstring(missile.gameObject, this.m_MissileInfo.m_TargetJoint);
			if (gameObject != null)
			{
				missile.transform.LookAt(missile.GetTarget().transform, this.m_MissileInfo.m_JointUpVector);
				Vector3[] array2 = array;
				int num2 = 2;
				array2[num2].y = array2[num2].y + this.m_MissileInfo.m_TargetHeightOffset;
				iTween.MoveTo(gameObject, hashtable);
				return;
			}
		}
		iTween.MoveTo(missile.gameObject, hashtable);
	}

	// Token: 0x06008604 RID: 34308 RVA: 0x002B425C File Offset: 0x002B245C
	private Vector3[] GenerateMissilePath(Spell missile)
	{
		Vector3[] array = new Vector3[3];
		array[0] = missile.transform.position;
		Card targetCard = missile.GetTargetCard();
		if (targetCard != null && targetCard.GetZone() is ZoneHand && !this.m_MissileInfo.m_UseTargetCardPositionInsteadOfHandSlot)
		{
			ZoneHand zoneHand = targetCard.GetZone() as ZoneHand;
			array[2] = zoneHand.GetCardPosition(zoneHand.GetCardSlot(targetCard), -1);
		}
		else
		{
			array[2] = missile.GetTarget().transform.position;
		}
		array[1] = this.GenerateMissilePathCenterPoint(array);
		return array;
	}

	// Token: 0x06008605 RID: 34309 RVA: 0x002B42F8 File Offset: 0x002B24F8
	private Vector3 GenerateMissilePathCenterPoint(Vector3[] path)
	{
		Vector3 vector = path[0];
		Vector3 vector2 = path[2];
		Vector3 a = vector2 - vector;
		float magnitude = a.magnitude;
		Vector3 result = vector;
		bool flag = magnitude <= Mathf.Epsilon;
		if (!flag)
		{
			result = vector + a * (this.m_MissileInfo.m_CenterOffsetPercent * 0.01f);
		}
		float num = magnitude / this.m_MissileInfo.m_DistanceScaleFactor;
		if (flag)
		{
			if (this.m_MissileInfo.m_CenterPointHeightMin <= Mathf.Epsilon && this.m_MissileInfo.m_CenterPointHeightMax <= Mathf.Epsilon)
			{
				result.y += 2f;
			}
			else
			{
				result.y += UnityEngine.Random.Range(this.m_MissileInfo.m_CenterPointHeightMin, this.m_MissileInfo.m_CenterPointHeightMax);
			}
		}
		else
		{
			result.y += num * UnityEngine.Random.Range(this.m_MissileInfo.m_CenterPointHeightMin, this.m_MissileInfo.m_CenterPointHeightMax);
		}
		float num2 = 1f;
		if (vector.z > vector2.z)
		{
			num2 = -1f;
		}
		bool flag2 = GeneralUtils.RandomBool();
		if (this.m_MissileInfo.m_RightMin == 0f && this.m_MissileInfo.m_RightMax == 0f)
		{
			flag2 = false;
		}
		if (this.m_MissileInfo.m_LeftMin == 0f && this.m_MissileInfo.m_LeftMax == 0f)
		{
			flag2 = true;
		}
		if (flag2)
		{
			if (this.m_MissileInfo.m_RightMin == this.m_MissileInfo.m_RightMax || this.m_MissileInfo.m_DebugForceMax)
			{
				result.x += this.m_MissileInfo.m_RightMax * num * num2;
			}
			else
			{
				result.x += UnityEngine.Random.Range(this.m_MissileInfo.m_RightMin * num, this.m_MissileInfo.m_RightMax * num) * num2;
			}
		}
		else if (this.m_MissileInfo.m_LeftMin == this.m_MissileInfo.m_LeftMax || this.m_MissileInfo.m_DebugForceMax)
		{
			result.x -= this.m_MissileInfo.m_LeftMax * num * num2;
		}
		else
		{
			result.x -= UnityEngine.Random.Range(this.m_MissileInfo.m_LeftMin * num, this.m_MissileInfo.m_LeftMax * num) * num2;
		}
		return result;
	}

	// Token: 0x06008606 RID: 34310 RVA: 0x002B4550 File Offset: 0x002B2750
	private void OnMissileTargetReached(Hashtable args)
	{
		Spell spell = (Spell)args["missile"];
		int targetIndex = (int)args["targetIndex"];
		if (this.HasImpact())
		{
			this.SpawnImpact(targetIndex);
		}
		if (this.HasChain())
		{
			this.SpawnChain();
		}
		else if (this.m_MissileInfo.m_SpawnInSequence)
		{
			this.SpawnMissileInSequence();
		}
		spell.ActivateState(SpellStateType.DEATH);
	}

	// Token: 0x06008607 RID: 34311 RVA: 0x002B45B6 File Offset: 0x002B27B6
	private void OnReverseMissileTargetReached(Spell missile)
	{
		missile.ActivateState(SpellStateType.DEATH);
	}

	// Token: 0x06008608 RID: 34312 RVA: 0x002B45BF File Offset: 0x002B27BF
	private bool HasImpact()
	{
		return this.m_ImpactInfo != null && this.m_ImpactInfo.m_Enabled && (this.m_ImpactInfo.m_Prefab != null || this.m_ImpactInfo.m_damageAmountImpactSpells.Length != 0);
	}

	// Token: 0x06008609 RID: 34313 RVA: 0x002B45FC File Offset: 0x002B27FC
	private void SpawnAllImpacts()
	{
		for (int i = 0; i < this.GetVisualTargetCount(); i++)
		{
			this.SpawnImpact(i);
		}
	}

	// Token: 0x0600860A RID: 34314 RVA: 0x002B4621 File Offset: 0x002B2821
	private void SpawnImpact(int targetIndex)
	{
		this.m_effectsPendingFinish++;
		base.StartCoroutine(this.WaitAndSpawnImpact(targetIndex));
	}

	// Token: 0x0600860B RID: 34315 RVA: 0x002B463F File Offset: 0x002B283F
	private IEnumerator WaitAndSpawnImpact(int targetIndex)
	{
		float seconds = UnityEngine.Random.Range(this.m_ImpactInfo.m_SpawnDelaySecMin, this.m_ImpactInfo.m_SpawnDelaySecMax);
		yield return new WaitForSeconds(seconds);
		if (this.m_ImpactInfo.m_SpawnOffset > 0f && targetIndex > 0)
		{
			yield return new WaitForSeconds(this.m_ImpactInfo.m_SpawnOffset * (float)targetIndex);
		}
		int metaDataIndex = this.GetMetaDataIndexForTarget(targetIndex);
		if (metaDataIndex >= 0)
		{
			if (this.ShouldCompleteTasksUntilMetaData(metaDataIndex))
			{
				yield return base.StartCoroutine(this.CompleteTasksUntilMetaData(metaDataIndex));
			}
			float delaySec = UnityEngine.Random.Range(this.m_ImpactInfo.m_GameDelaySecMin, this.m_ImpactInfo.m_GameDelaySecMax);
			base.StartCoroutine(this.CompleteTasksFromMetaData(metaDataIndex, delaySec));
		}
		if (this.m_visualTargets.Count <= targetIndex || this.m_visualTargets[targetIndex] == null)
		{
			this.m_effectsPendingFinish--;
			yield break;
		}
		GameObject source = base.GetSource();
		GameObject gameObject = this.m_visualTargets[targetIndex];
		Spell prefab = this.DetermineImpactPrefab(gameObject);
		Spell spell = this.CloneSpell(prefab, null, null);
		spell.SetSource(source);
		spell.AddTarget(gameObject);
		if (this.m_ImpactInfo.m_UseSuperSpellLocation)
		{
			spell.SetPosition(base.transform.position);
		}
		else
		{
			if (this.IsMakingClones())
			{
				spell.m_Location = this.m_ImpactInfo.m_Location;
				spell.m_SetParentToLocation = this.m_ImpactInfo.m_SetParentToLocation;
			}
			spell.UpdatePosition();
		}
		spell.UpdateOrientation();
		spell.Activate();
		yield break;
	}

	// Token: 0x0600860C RID: 34316 RVA: 0x002B4658 File Offset: 0x002B2858
	private Spell DetermineImpactPrefab(GameObject targetObject)
	{
		if (this.m_ImpactInfo.m_damageAmountImpactSpells.Length == 0)
		{
			return this.m_ImpactInfo.m_Prefab;
		}
		Spell result = this.m_ImpactInfo.m_Prefab;
		if (this.m_taskList == null)
		{
			return result;
		}
		Card component = targetObject.GetComponent<Card>();
		if (component == null)
		{
			return result;
		}
		PowerTaskList.DamageInfo damageInfo = this.m_taskList.GetDamageInfo(component.GetEntity());
		if (damageInfo == null)
		{
			return result;
		}
		SpellValueRange appropriateElementAccordingToRanges = SpellUtils.GetAppropriateElementAccordingToRanges<SpellValueRange>(this.m_ImpactInfo.m_damageAmountImpactSpells, (SpellValueRange x) => x.m_range, damageInfo.m_damage);
		if (appropriateElementAccordingToRanges != null && appropriateElementAccordingToRanges.m_spellPrefab != null)
		{
			result = appropriateElementAccordingToRanges.m_spellPrefab;
		}
		return result;
	}

	// Token: 0x0600860D RID: 34317 RVA: 0x002B470D File Offset: 0x002B290D
	private bool HasChain()
	{
		return this.m_ChainInfo != null && this.m_ChainInfo.m_Enabled && this.m_ChainInfo.m_Prefab != null;
	}

	// Token: 0x0600860E RID: 34318 RVA: 0x002B4737 File Offset: 0x002B2937
	private void SpawnChain()
	{
		if (this.GetVisualTargetCount() <= 1)
		{
			return;
		}
		this.m_effectsPendingFinish++;
		base.StartCoroutine(this.WaitAndSpawnChain());
	}

	// Token: 0x0600860F RID: 34319 RVA: 0x002B475E File Offset: 0x002B295E
	private IEnumerator WaitAndSpawnChain()
	{
		float seconds = UnityEngine.Random.Range(this.m_ChainInfo.m_SpawnDelayMin, this.m_ChainInfo.m_SpawnDelayMax);
		yield return new WaitForSeconds(seconds);
		Spell spell = this.CloneSpell(this.m_ChainInfo.m_Prefab, null, null);
		GameObject primaryTarget = this.GetPrimaryTarget();
		spell.SetSource(primaryTarget);
		foreach (GameObject gameObject in this.m_visualTargets)
		{
			if (!(gameObject == primaryTarget))
			{
				spell.AddTarget(gameObject);
			}
		}
		spell.ActivateState(SpellStateType.ACTION);
		yield break;
	}

	// Token: 0x06008610 RID: 34320 RVA: 0x002B4770 File Offset: 0x002B2970
	private SpellAreaEffectInfo DetermineAreaEffectInfo()
	{
		Card sourceCard = base.GetSourceCard();
		if (sourceCard != null)
		{
			global::Player controller = sourceCard.GetController();
			if (controller != null)
			{
				if (controller.IsFriendlySide() && this.HasFriendlyAreaEffect())
				{
					return this.m_FriendlyAreaEffectInfo;
				}
				if (!controller.IsFriendlySide() && this.HasOpponentAreaEffect())
				{
					return this.m_OpponentAreaEffectInfo;
				}
			}
		}
		if (this.HasFriendlyAreaEffect())
		{
			return this.m_FriendlyAreaEffectInfo;
		}
		if (this.HasOpponentAreaEffect())
		{
			return this.m_OpponentAreaEffectInfo;
		}
		return null;
	}

	// Token: 0x06008611 RID: 34321 RVA: 0x002B47E4 File Offset: 0x002B29E4
	private bool HasAreaEffect()
	{
		return this.HasFriendlyAreaEffect() || this.HasOpponentAreaEffect();
	}

	// Token: 0x06008612 RID: 34322 RVA: 0x002B47F6 File Offset: 0x002B29F6
	private bool HasFriendlyAreaEffect()
	{
		return this.m_FriendlyAreaEffectInfo != null && this.m_FriendlyAreaEffectInfo.m_Enabled && this.m_FriendlyAreaEffectInfo.m_Prefab != null;
	}

	// Token: 0x06008613 RID: 34323 RVA: 0x002B4820 File Offset: 0x002B2A20
	private bool HasOpponentAreaEffect()
	{
		return this.m_OpponentAreaEffectInfo != null && this.m_OpponentAreaEffectInfo.m_Enabled && this.m_OpponentAreaEffectInfo.m_Prefab != null;
	}

	// Token: 0x06008614 RID: 34324 RVA: 0x002B484A File Offset: 0x002B2A4A
	private void SpawnAreaEffect(SpellAreaEffectInfo info)
	{
		this.m_effectsPendingFinish++;
		base.StartCoroutine(this.WaitAndSpawnAreaEffect(info));
	}

	// Token: 0x06008615 RID: 34325 RVA: 0x002B4868 File Offset: 0x002B2A68
	private IEnumerator WaitAndSpawnAreaEffect(SpellAreaEffectInfo info)
	{
		float num = UnityEngine.Random.Range(info.m_SpawnDelaySecMin, info.m_SpawnDelaySecMax);
		if (num > 0f)
		{
			yield return new WaitForSeconds(num);
		}
		Spell spell = this.CloneSpell(info.m_Prefab, null, null);
		spell.SetSource(base.GetSource());
		spell.AttachPowerTaskList(this.m_taskList);
		if (info.m_UseSuperSpellLocation)
		{
			spell.SetPosition(base.transform.position);
		}
		else if (this.IsMakingClones() && info.m_Location != SpellLocation.NONE)
		{
			spell.m_Location = info.m_Location;
			spell.m_SetParentToLocation = info.m_SetParentToLocation;
			spell.UpdatePosition();
		}
		if (this.IsMakingClones() && info.m_Facing != SpellFacing.NONE)
		{
			spell.m_Facing = info.m_Facing;
			spell.m_FacingOptions = info.m_FacingOptions;
			spell.UpdateOrientation();
		}
		if (this.OnBeforeActivateAreaEffectSpell != null)
		{
			this.OnBeforeActivateAreaEffectSpell(spell);
		}
		spell.Activate();
		this.m_activeAreaEffectSpell = spell;
		yield break;
	}

	// Token: 0x17000784 RID: 1924
	// (get) Token: 0x06008616 RID: 34326 RVA: 0x002B487E File Offset: 0x002B2A7E
	// (set) Token: 0x06008617 RID: 34327 RVA: 0x002B4886 File Offset: 0x002B2A86
	protected Action<Spell> OnBeforeActivateAreaEffectSpell { get; set; }

	// Token: 0x06008618 RID: 34328 RVA: 0x002B4890 File Offset: 0x002B2A90
	private bool AddPrimaryChainTarget()
	{
		Network.HistBlockStart blockStart = this.m_taskList.GetBlockStart();
		return blockStart != null && base.AddSinglePowerTarget_FromBlockStart(blockStart);
	}

	// Token: 0x06008619 RID: 34329 RVA: 0x0001FA65 File Offset: 0x0001DC65
	private int GetPrimaryTargetIndex()
	{
		return 0;
	}

	// Token: 0x0600861A RID: 34330 RVA: 0x002B48B5 File Offset: 0x002B2AB5
	private GameObject GetPrimaryTarget()
	{
		return this.m_visualTargets[this.GetPrimaryTargetIndex()];
	}

	// Token: 0x0600861B RID: 34331 RVA: 0x002B48C8 File Offset: 0x002B2AC8
	protected virtual void UpdateTargets()
	{
		this.UpdateVisualTargets();
		this.SuppressPlaySoundsOnVisualTargets();
	}

	// Token: 0x0600861C RID: 34332 RVA: 0x002B48D6 File Offset: 0x002B2AD6
	private int GetVisualTargetCount()
	{
		if (this.IsMakingClones())
		{
			return this.m_visualTargets.Count;
		}
		return Mathf.Min(1, this.m_visualTargets.Count);
	}

	// Token: 0x0600861D RID: 34333 RVA: 0x002B4900 File Offset: 0x002B2B00
	protected virtual void UpdateVisualTargets()
	{
		SpellTargetBehavior behavior = this.m_TargetInfo.m_Behavior;
		if (behavior == SpellTargetBehavior.FRIENDLY_PLAY_ZONE_CENTER)
		{
			ZonePlay zonePlay = SpellUtils.FindFriendlyPlayZone(this);
			this.AddVisualTarget(zonePlay.gameObject);
			return;
		}
		if (behavior == SpellTargetBehavior.FRIENDLY_PLAY_ZONE_RANDOM)
		{
			ZonePlay zonePlay2 = SpellUtils.FindFriendlyPlayZone(this);
			this.GenerateRandomPlayZoneVisualTargets(zonePlay2);
			return;
		}
		if (behavior == SpellTargetBehavior.OPPONENT_PLAY_ZONE_CENTER)
		{
			ZonePlay zonePlay3 = SpellUtils.FindOpponentPlayZone(this);
			this.AddVisualTarget(zonePlay3.gameObject);
			return;
		}
		if (behavior == SpellTargetBehavior.OPPONENT_PLAY_ZONE_RANDOM)
		{
			ZonePlay zonePlay4 = SpellUtils.FindOpponentPlayZone(this);
			this.GenerateRandomPlayZoneVisualTargets(zonePlay4);
			return;
		}
		if (behavior == SpellTargetBehavior.BOARD_CENTER)
		{
			Board board = Board.Get();
			this.AddVisualTarget(board.FindBone("CenterPointBone").gameObject);
			return;
		}
		if (behavior == SpellTargetBehavior.UNTARGETED)
		{
			this.AddVisualTarget(base.GetSource());
			return;
		}
		if (behavior == SpellTargetBehavior.CHOSEN_TARGET_ONLY)
		{
			this.AddChosenTargetAsVisualTarget();
			return;
		}
		if (behavior == SpellTargetBehavior.BOARD_RANDOM)
		{
			this.GenerateRandomBoardVisualTargets();
			return;
		}
		if (behavior == SpellTargetBehavior.TARGET_ZONE_CENTER)
		{
			Zone zone = SpellUtils.FindTargetZone(this);
			this.AddVisualTarget(zone.gameObject);
			return;
		}
		if (behavior == SpellTargetBehavior.NEW_CREATED_CARDS)
		{
			this.GenerateCreatedCardsTargets(TAG_ZONE.INVALID);
			return;
		}
		if (behavior == SpellTargetBehavior.NEW_CREATED_CARDS_IN_PLAY)
		{
			this.GenerateCreatedCardsTargets(TAG_ZONE.PLAY);
			return;
		}
		this.AddAllTargetsAsVisualTargets();
	}

	// Token: 0x0600861E RID: 34334 RVA: 0x002B49F8 File Offset: 0x002B2BF8
	protected void GenerateRandomBoardVisualTargets()
	{
		ZonePlay zonePlay = SpellUtils.FindFriendlyPlayZone(this);
		Component component = SpellUtils.FindOpponentPlayZone(this);
		Bounds bounds = zonePlay.GetComponent<Collider>().bounds;
		Bounds bounds2 = component.GetComponent<Collider>().bounds;
		Vector3 b = Vector3.Min(bounds.min, bounds2.min);
		Vector3 a = Vector3.Max(bounds.max, bounds2.max);
		Vector3 center = 0.5f * (a + b);
		Vector3 vector = a - b;
		Vector3 size = new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
		Bounds bounds3 = new Bounds(center, size);
		this.GenerateRandomVisualTargets(bounds3);
	}

	// Token: 0x0600861F RID: 34335 RVA: 0x002B4AAE File Offset: 0x002B2CAE
	protected void GenerateRandomPlayZoneVisualTargets(ZonePlay zonePlay)
	{
		this.GenerateRandomVisualTargets(zonePlay.GetComponent<Collider>().bounds);
	}

	// Token: 0x06008620 RID: 34336 RVA: 0x002B4AC4 File Offset: 0x002B2CC4
	private void GenerateRandomVisualTargets(Bounds bounds)
	{
		int num = UnityEngine.Random.Range(this.m_TargetInfo.m_RandomTargetCountMin, this.m_TargetInfo.m_RandomTargetCountMax + 1);
		if (num == 0)
		{
			return;
		}
		float x = bounds.min.x;
		float z = bounds.max.z;
		float z2 = bounds.min.z;
		float num2 = bounds.size.x / (float)num;
		int[] array = new int[num];
		int[] array2 = new int[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = 0;
			array2[i] = -1;
		}
		for (int j = 0; j < num; j++)
		{
			float num3 = UnityEngine.Random.Range(0f, 1f);
			int max = 0;
			for (int k = 0; k < num; k++)
			{
				if (this.ComputeBoxPickChance(array, k) >= num3)
				{
					array2[max++] = k;
				}
			}
			int num4 = array2[UnityEngine.Random.Range(0, max)];
			array[num4]++;
			float num5 = x + (float)num4 * num2;
			float max2 = num5 + num2;
			this.GenerateVisualTarget(new Vector3
			{
				x = UnityEngine.Random.Range(num5, max2),
				y = bounds.center.y,
				z = UnityEngine.Random.Range(z2, z)
			}, j, num4);
		}
	}

	// Token: 0x06008621 RID: 34337 RVA: 0x002B4C1C File Offset: 0x002B2E1C
	private void GenerateVisualTarget(Vector3 position, int index, int boxIndex)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = string.Format("{0} Target {1} (box {2})", this, index, boxIndex);
		gameObject.transform.position = position;
		gameObject.AddComponent<SpellGeneratedTarget>();
		this.AddVisualTarget(gameObject);
	}

	// Token: 0x06008622 RID: 34338 RVA: 0x002B4C68 File Offset: 0x002B2E68
	private float ComputeBoxPickChance(int[] boxUsageCounts, int index)
	{
		float num = (float)boxUsageCounts[index];
		float num2 = (float)boxUsageCounts.Length * 0.25f;
		float num3 = num / num2;
		return 1f - num3;
	}

	// Token: 0x06008623 RID: 34339 RVA: 0x002B4C90 File Offset: 0x002B2E90
	private void GenerateCreatedCardsTargets(TAG_ZONE onlyAffectZone = TAG_ZONE.INVALID)
	{
		if (this.m_taskList == null)
		{
			return;
		}
		foreach (PowerTask powerTask in this.m_taskList.GetTaskList())
		{
			Network.PowerHistory power = powerTask.GetPower();
			if (power.Type == Network.PowerType.FULL_ENTITY)
			{
				int id = (power as Network.HistFullEntity).Entity.ID;
				global::Entity entity = GameState.Get().GetEntity(id);
				if ((onlyAffectZone == TAG_ZONE.INVALID || entity.GetTag(GAME_TAG.ZONE) == (int)onlyAffectZone) && entity.GetTag(GAME_TAG.ZONE) != 6)
				{
					if (entity == null)
					{
						Debug.LogWarning(string.Format("{0}.GenerateCreatedCardsTargets() - WARNING trying to target entity with id {1} but there is no entity with that id", this, id));
					}
					else
					{
						Card card = entity.GetCard();
						if (card == null)
						{
							Debug.LogWarning(string.Format("{0}.GenerateCreatedCardsTargets() - WARNING trying to target entity.GetCard() with id {1} but there is no card with that id", this, id));
						}
						else
						{
							this.m_visualTargets.Add(card.gameObject);
						}
					}
				}
			}
		}
	}

	// Token: 0x06008624 RID: 34340 RVA: 0x002B4D90 File Offset: 0x002B2F90
	private void AddChosenTargetAsVisualTarget()
	{
		Card powerTargetCard = base.GetPowerTargetCard();
		if (powerTargetCard == null)
		{
			Debug.LogWarning(string.Format("{0}.AddChosenTargetAsVisualTarget() - there is no chosen target", this));
			return;
		}
		this.AddVisualTarget(powerTargetCard.gameObject);
	}

	// Token: 0x06008625 RID: 34341 RVA: 0x002B4DCC File Offset: 0x002B2FCC
	private void AddAllTargetsAsVisualTargets()
	{
		for (int i = 0; i < this.m_targets.Count; i++)
		{
			int count = this.m_visualTargets.Count;
			this.m_visualToTargetIndexMap[count] = i;
			this.AddVisualTarget(this.m_targets[i]);
		}
	}

	// Token: 0x06008626 RID: 34342 RVA: 0x002B4E1C File Offset: 0x002B301C
	private void SuppressPlaySoundsOnVisualTargets()
	{
		if (!this.m_TargetInfo.m_SuppressPlaySounds)
		{
			return;
		}
		for (int i = 0; i < this.m_visualTargets.Count; i++)
		{
			Card component = this.m_visualTargets[i].GetComponent<Card>();
			if (!(component == null))
			{
				component.SuppressPlaySounds(true);
			}
		}
	}

	// Token: 0x06008627 RID: 34343 RVA: 0x002B4E70 File Offset: 0x002B3070
	protected virtual void CleanUp()
	{
		foreach (GameObject gameObject in this.m_visualTargets)
		{
			if (gameObject == null)
			{
				Debug.LogWarning(string.Format("{0}.CleanUp() - found a null GameObject in m_visualTargets", this));
			}
			else if (!(gameObject.GetComponent<SpellGeneratedTarget>() == null))
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		this.m_visualTargets.Clear();
	}

	// Token: 0x06008628 RID: 34344 RVA: 0x002B4EF8 File Offset: 0x002B30F8
	protected bool HasMetaDataTargets()
	{
		return this.m_targetToMetaDataMap.Count > 0;
	}

	// Token: 0x06008629 RID: 34345 RVA: 0x002B4F08 File Offset: 0x002B3108
	protected int GetMetaDataIndexForTarget(int visualTargetIndex)
	{
		int key;
		if (!this.m_visualToTargetIndexMap.TryGetValue(visualTargetIndex, out key))
		{
			return -1;
		}
		int result;
		if (!this.m_targetToMetaDataMap.TryGetValue(key, out result))
		{
			return -1;
		}
		return result;
	}

	// Token: 0x0600862A RID: 34346 RVA: 0x002B4F3A File Offset: 0x002B313A
	protected bool ShouldCompleteTasksUntilMetaData(int metaDataIndex)
	{
		return this.m_taskList != null && !this.IsBatchedTargetInfo(metaDataIndex) && this.m_taskList.HasEarlierIncompleteTask(metaDataIndex);
	}

	// Token: 0x0600862B RID: 34347 RVA: 0x002B4F60 File Offset: 0x002B3160
	private bool IsBatchedTargetInfo(int metaDataIndex)
	{
		if (this.m_taskList.GetTaskList().Count >= metaDataIndex)
		{
			return false;
		}
		Network.HistMetaData histMetaData = this.m_taskList.GetTaskList()[metaDataIndex].GetPower() as Network.HistMetaData;
		return histMetaData != null && histMetaData.MetaType == HistoryMeta.Type.TARGET && histMetaData.Data != 0;
	}

	// Token: 0x0600862C RID: 34348 RVA: 0x002B4FB4 File Offset: 0x002B31B4
	protected IEnumerator CompleteTasksUntilMetaData(int metaDataIndex)
	{
		this.m_effectsPendingFinish++;
		this.m_taskList.DoTasks(0, metaDataIndex);
		QueueList<PowerTask> queueList = this.DetermineTasksToWaitFor(0, metaDataIndex);
		if (queueList != null && queueList.Count > 0)
		{
			yield return base.StartCoroutine(this.WaitForTasks(queueList));
		}
		this.m_effectsPendingFinish--;
		yield break;
	}

	// Token: 0x0600862D RID: 34349 RVA: 0x002B4FCC File Offset: 0x002B31CC
	protected QueueList<PowerTask> DetermineTasksToWaitFor(int startIndex, int count)
	{
		if (count == 0)
		{
			return null;
		}
		int num = startIndex + count;
		QueueList<PowerTask> queueList = new QueueList<PowerTask>();
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		for (int i = startIndex; i < num; i++)
		{
			PowerTask powerTask = taskList[i];
			global::Entity entity = this.GetEntityFromZoneChangePowerTask(powerTask);
			if (entity != null && !(this.m_visualTargets.Find(delegate(GameObject currTargetObject)
			{
				Card component = currTargetObject.GetComponent<Card>();
				return entity.GetCard() == component;
			}) == null))
			{
				for (int j = 0; j < queueList.Count; j++)
				{
					PowerTask task = queueList[j];
					global::Entity entityFromZoneChangePowerTask = this.GetEntityFromZoneChangePowerTask(task);
					if (entity == entityFromZoneChangePowerTask)
					{
						queueList.RemoveAt(j);
						break;
					}
				}
				queueList.Enqueue(powerTask);
			}
		}
		return queueList;
	}

	// Token: 0x0600862E RID: 34350 RVA: 0x002B5097 File Offset: 0x002B3297
	protected IEnumerator WaitForTasks(QueueList<PowerTask> tasksToWaitFor)
	{
		while (tasksToWaitFor.Count > 0)
		{
			PowerTask powerTask = tasksToWaitFor.Peek();
			if (!powerTask.IsCompleted())
			{
				yield return null;
			}
			else
			{
				global::Entity entity;
				int zoneTag;
				this.GetZoneChangeFromPowerTask(powerTask, out entity, out zoneTag);
				Card card = entity.GetCard();
				Zone zone = ZoneMgr.Get().FindZoneForEntityAndZoneTag(entity, (TAG_ZONE)zoneTag);
				while (card.GetZone() != zone)
				{
					yield return null;
				}
				while (card.IsActorLoading())
				{
					yield return null;
				}
				tasksToWaitFor.Dequeue();
				card = null;
				zone = null;
			}
		}
		yield break;
	}

	// Token: 0x0600862F RID: 34351 RVA: 0x002B50AD File Offset: 0x002B32AD
	protected IEnumerator CompleteTasksFromMetaData(int metaDataIndex, float delaySec)
	{
		this.m_effectsPendingFinish++;
		yield return new WaitForSeconds(delaySec);
		base.CompleteMetaDataTasks(metaDataIndex, new PowerTaskList.CompleteCallback(this.OnMetaDataTasksComplete));
		yield break;
	}

	// Token: 0x06008630 RID: 34352 RVA: 0x0023B85D File Offset: 0x00239A5D
	protected void OnMetaDataTasksComplete(PowerTaskList taskList, int startIndex, int count, object userData)
	{
		this.m_effectsPendingFinish--;
		this.FinishIfPossible();
	}

	// Token: 0x06008631 RID: 34353 RVA: 0x000052EC File Offset: 0x000034EC
	protected bool IsMakingClones()
	{
		return true;
	}

	// Token: 0x06008632 RID: 34354 RVA: 0x002B50CA File Offset: 0x002B32CA
	protected bool AreEffectsActive()
	{
		return this.m_effectsPendingFinish > 0;
	}

	// Token: 0x06008633 RID: 34355 RVA: 0x002B50D8 File Offset: 0x002B32D8
	protected Spell CloneSpell(Spell prefab, Vector3? position = null, Spell.FinishedCallback finishedCallback = null)
	{
		Spell spell;
		if (this.IsMakingClones())
		{
			if (position != null)
			{
				spell = UnityEngine.Object.Instantiate<Spell>(prefab, position.Value, Quaternion.identity);
			}
			else
			{
				spell = UnityEngine.Object.Instantiate<Spell>(prefab);
			}
			spell.AddStateStartedCallback(new Spell.StateStartedCallback(this.OnCloneSpellStateStarted));
			spell.transform.parent = base.transform;
			this.m_activeClonedSpells.Add(spell);
		}
		else
		{
			spell = prefab;
			spell.RemoveAllTargets();
		}
		Spell.FinishedCallback callback = (finishedCallback == null) ? new Spell.FinishedCallback(this.OnCloneSpellFinished) : finishedCallback;
		spell.AddFinishedCallback(callback);
		return spell;
	}

	// Token: 0x06008634 RID: 34356 RVA: 0x0023B85D File Offset: 0x00239A5D
	private void OnCloneSpellFinished(Spell spell, object userData)
	{
		this.m_effectsPendingFinish--;
		this.FinishIfPossible();
	}

	// Token: 0x06008635 RID: 34357 RVA: 0x002B5166 File Offset: 0x002B3366
	private void OnCloneSpellStateStarted(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		this.m_activeClonedSpells.Remove(spell);
		UnityEngine.Object.Destroy(spell.gameObject);
	}

	// Token: 0x06008636 RID: 34358 RVA: 0x002B5189 File Offset: 0x002B3389
	private void UpdatePendingStateChangeFlags(SpellStateType stateType)
	{
		if (!base.HasStateContent(stateType))
		{
			this.m_pendingNoneStateChange = true;
			this.m_pendingSpellFinish = true;
			return;
		}
		this.m_pendingNoneStateChange = false;
		this.m_pendingSpellFinish = false;
	}

	// Token: 0x06008637 RID: 34359 RVA: 0x002B51B4 File Offset: 0x002B33B4
	protected void FinishIfPossible()
	{
		if (this.m_settingUpAction)
		{
			return;
		}
		if (this.AreEffectsActive())
		{
			return;
		}
		if (this.m_pendingSpellFinish)
		{
			this.OnSpellFinished();
			this.m_pendingSpellFinish = false;
		}
		if (this.m_pendingNoneStateChange)
		{
			this.OnStateFinished();
			this.m_pendingNoneStateChange = false;
		}
		this.CleanUp();
	}

	// Token: 0x04007164 RID: 29028
	public bool m_MakeClones = true;

	// Token: 0x04007165 RID: 29029
	public SpellTargetInfo m_TargetInfo = new SpellTargetInfo();

	// Token: 0x04007166 RID: 29030
	public SpellStartInfo m_StartInfo;

	// Token: 0x04007167 RID: 29031
	public SpellActionInfo m_ActionInfo;

	// Token: 0x04007168 RID: 29032
	public SpellMissileInfo m_MissileInfo;

	// Token: 0x04007169 RID: 29033
	public SpellImpactInfo m_ImpactInfo;

	// Token: 0x0400716A RID: 29034
	public SpellAreaEffectInfo m_FriendlyAreaEffectInfo;

	// Token: 0x0400716B RID: 29035
	public SpellAreaEffectInfo m_OpponentAreaEffectInfo;

	// Token: 0x0400716C RID: 29036
	[HideInInspector]
	public SpellChainInfo m_ChainInfo;

	// Token: 0x0400716D RID: 29037
	protected Spell m_startSpell;

	// Token: 0x0400716E RID: 29038
	protected List<GameObject> m_visualTargets = new List<GameObject>();

	// Token: 0x0400716F RID: 29039
	protected int m_currentTargetIndex;

	// Token: 0x04007170 RID: 29040
	protected int m_effectsPendingFinish;

	// Token: 0x04007171 RID: 29041
	protected bool m_pendingNoneStateChange;

	// Token: 0x04007172 RID: 29042
	protected bool m_pendingSpellFinish;

	// Token: 0x04007173 RID: 29043
	protected List<Spell> m_activeClonedSpells = new List<Spell>();

	// Token: 0x04007174 RID: 29044
	protected Map<int, int> m_visualToTargetIndexMap = new Map<int, int>();

	// Token: 0x04007175 RID: 29045
	protected Map<int, int> m_targetToMetaDataMap = new Map<int, int>();

	// Token: 0x04007176 RID: 29046
	protected bool m_settingUpAction;

	// Token: 0x04007177 RID: 29047
	protected Spell m_activeAreaEffectSpell;
}
