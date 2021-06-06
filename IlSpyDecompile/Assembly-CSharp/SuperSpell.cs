using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Core;
using HutongGames.PlayMaker;
using PegasusGame;
using UnityEngine;

public class SuperSpell : Spell
{
	public bool m_MakeClones = true;

	public SpellTargetInfo m_TargetInfo = new SpellTargetInfo();

	public SpellStartInfo m_StartInfo;

	public SpellActionInfo m_ActionInfo;

	public SpellMissileInfo m_MissileInfo;

	public SpellImpactInfo m_ImpactInfo;

	public SpellAreaEffectInfo m_FriendlyAreaEffectInfo;

	public SpellAreaEffectInfo m_OpponentAreaEffectInfo;

	[HideInInspector]
	public SpellChainInfo m_ChainInfo;

	protected Spell m_startSpell;

	protected List<GameObject> m_visualTargets = new List<GameObject>();

	protected int m_currentTargetIndex;

	protected int m_effectsPendingFinish;

	protected bool m_pendingNoneStateChange;

	protected bool m_pendingSpellFinish;

	protected List<Spell> m_activeClonedSpells = new List<Spell>();

	protected Map<int, int> m_visualToTargetIndexMap = new Map<int, int>();

	protected Map<int, int> m_targetToMetaDataMap = new Map<int, int>();

	protected bool m_settingUpAction;

	protected Spell m_activeAreaEffectSpell;

	protected Action<Spell> OnBeforeActivateAreaEffectSpell { get; set; }

	public override List<GameObject> GetVisualTargets()
	{
		return m_visualTargets;
	}

	public override GameObject GetVisualTarget()
	{
		if (m_visualTargets.Count != 0)
		{
			return m_visualTargets[0];
		}
		return null;
	}

	public override void AddVisualTarget(GameObject go)
	{
		m_visualTargets.Add(go);
	}

	public override void AddVisualTargets(List<GameObject> targets)
	{
		m_visualTargets.AddRange(targets);
	}

	public override bool RemoveVisualTarget(GameObject go)
	{
		return m_visualTargets.Remove(go);
	}

	public override void RemoveAllVisualTargets()
	{
		m_visualTargets.Clear();
	}

	public override bool IsVisualTarget(GameObject go)
	{
		return m_visualTargets.Contains(go);
	}

	public override Card GetVisualTargetCard()
	{
		GameObject visualTarget = GetVisualTarget();
		if (visualTarget == null)
		{
			return null;
		}
		return visualTarget.GetComponent<Card>();
	}

	protected bool AddPowerTargetsInternal(bool fallbackToStartBlockTarget)
	{
		m_visualToTargetIndexMap.Clear();
		m_targetToMetaDataMap.Clear();
		if (!CanAddPowerTargets())
		{
			return false;
		}
		if (HasChain() && !AddPrimaryChainTarget())
		{
			return false;
		}
		if (!AddMultiplePowerTargets())
		{
			return false;
		}
		if (m_targets.Count > 0)
		{
			return true;
		}
		if (!fallbackToStartBlockTarget)
		{
			return true;
		}
		Network.HistBlockStart blockStart = m_taskList.GetBlockStart();
		if (blockStart == null || blockStart.Target == 0)
		{
			return true;
		}
		return AddSinglePowerTarget_FromBlockStart(blockStart);
	}

	public override bool AddPowerTargets()
	{
		return AddPowerTargetsInternal(fallbackToStartBlockTarget: true);
	}

	protected override void AddTargetFromMetaData(int metaDataIndex, Card targetCard)
	{
		int count = m_targets.Count;
		m_targetToMetaDataMap[count] = metaDataIndex;
		AddTarget(targetCard.gameObject);
	}

	protected override void OnBirth(SpellStateType prevStateType)
	{
		UpdatePosition();
		UpdateOrientation();
		m_currentTargetIndex = 0;
		if (HasStart())
		{
			SpawnStart();
			m_startSpell.SafeActivateState(SpellStateType.BIRTH);
			if (m_startSpell.GetActiveState() == SpellStateType.NONE)
			{
				m_startSpell = null;
			}
		}
		base.OnBirth(prevStateType);
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		m_settingUpAction = true;
		UpdateTargets();
		if (m_Location == SpellLocation.CHOSEN_TARGET)
		{
			m_positionDirty = true;
		}
		UpdatePosition();
		if (m_Facing == SpellFacing.TOWARDS_CHOSEN_TARGET)
		{
			m_orientationDirty = true;
		}
		UpdateOrientation();
		m_currentTargetIndex = GetPrimaryTargetIndex();
		UpdatePendingStateChangeFlags(SpellStateType.ACTION);
		DoAction();
		base.OnAction(prevStateType);
		m_settingUpAction = false;
		FinishIfPossible();
	}

	protected override void OnCancel(SpellStateType prevStateType)
	{
		UpdatePendingStateChangeFlags(SpellStateType.CANCEL);
		if (m_startSpell != null)
		{
			m_startSpell.SafeActivateState(SpellStateType.CANCEL);
			m_startSpell = null;
		}
		base.OnCancel(prevStateType);
		FinishIfPossible();
	}

	public override void OnStateFinished()
	{
		if (GuessNextStateType() == SpellStateType.NONE && AreEffectsActive())
		{
			m_pendingNoneStateChange = true;
		}
		else
		{
			base.OnStateFinished();
		}
	}

	public override void OnSpellFinished()
	{
		if (AreEffectsActive())
		{
			m_pendingSpellFinish = true;
		}
		else
		{
			base.OnSpellFinished();
		}
	}

	public override void OnFsmStateStarted(FsmState state, SpellStateType stateType)
	{
		if (m_activeStateChange != stateType)
		{
			if (stateType == SpellStateType.NONE && AreEffectsActive())
			{
				m_pendingSpellFinish = true;
				m_pendingNoneStateChange = true;
			}
			else
			{
				base.OnFsmStateStarted(state, stateType);
			}
		}
	}

	public override bool CanPurge()
	{
		if (m_activeClonedSpells.Count > 0)
		{
			return false;
		}
		return base.CanPurge();
	}

	private void DoAction()
	{
		if (!CheckAndWaitForGameEventsThenDoAction() && !CheckAndWaitForStartDelayThenDoAction() && !CheckAndWaitForStartPrefabThenDoAction())
		{
			DoActionNow();
		}
	}

	private bool CheckAndWaitForGameEventsThenDoAction()
	{
		if (m_taskList == null)
		{
			return false;
		}
		if (m_ActionInfo.m_ShowSpellVisuals == SpellVisualShowTime.DURING_GAME_EVENTS)
		{
			return DoActionDuringGameEvents();
		}
		if (m_ActionInfo.m_ShowSpellVisuals == SpellVisualShowTime.AFTER_GAME_EVENTS)
		{
			DoActionAfterGameEvents();
			return true;
		}
		return false;
	}

	private bool DoActionDuringGameEvents()
	{
		m_taskList.DoAllTasks();
		if (m_taskList.IsComplete())
		{
			return false;
		}
		QueueList<PowerTask> queueList = DetermineTasksToWaitFor(0, m_taskList.GetTaskList().Count);
		if (queueList.Count == 0)
		{
			return false;
		}
		StartCoroutine(DoDelayedActionDuringGameEvents(queueList));
		return true;
	}

	private IEnumerator DoDelayedActionDuringGameEvents(QueueList<PowerTask> tasksToWaitFor)
	{
		m_effectsPendingFinish++;
		yield return StartCoroutine(WaitForTasks(tasksToWaitFor));
		m_effectsPendingFinish--;
		if (!CheckAndWaitForStartDelayThenDoAction() && !CheckAndWaitForStartPrefabThenDoAction())
		{
			DoActionNow();
		}
	}

	private Entity GetEntityFromZoneChangePowerTask(PowerTask task)
	{
		GetZoneChangeFromPowerTask(task, out var entity, out var _);
		return entity;
	}

	private bool GetZoneChangeFromPowerTask(PowerTask task, out Entity entity, out int zoneTag)
	{
		entity = null;
		zoneTag = 0;
		Entity entity2 = null;
		Network.PowerHistory power = task.GetPower();
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
			foreach (Network.Entity.Tag tag in histFullEntity.Entity.Tags)
			{
				if (tag.Name == 49)
				{
					entity = entity2;
					zoneTag = tag.Value;
					return true;
				}
			}
			break;
		}
		case Network.PowerType.SHOW_ENTITY:
		{
			Network.HistShowEntity histShowEntity = (Network.HistShowEntity)power;
			entity2 = GameState.Get().GetEntity(histShowEntity.Entity.ID);
			if (entity2.GetCard() == null)
			{
				return false;
			}
			foreach (Network.Entity.Tag tag2 in histShowEntity.Entity.Tags)
			{
				if (tag2.Name == 49)
				{
					entity = entity2;
					zoneTag = tag2.Value;
					return true;
				}
			}
			break;
		}
		case Network.PowerType.TAG_CHANGE:
		{
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
			break;
		}
		}
		return false;
	}

	private void DoActionAfterGameEvents()
	{
		m_effectsPendingFinish++;
		PowerTaskList.CompleteCallback callback = delegate
		{
			m_effectsPendingFinish--;
			if (!CheckAndWaitForStartDelayThenDoAction() && !CheckAndWaitForStartPrefabThenDoAction())
			{
				DoActionNow();
			}
		};
		m_taskList.DoAllTasks(callback);
	}

	private bool CheckAndWaitForStartDelayThenDoAction()
	{
		if (Mathf.Min(m_ActionInfo.m_StartDelayMax, m_ActionInfo.m_StartDelayMin) <= Mathf.Epsilon)
		{
			return false;
		}
		m_effectsPendingFinish++;
		StartCoroutine(WaitForStartDelayThenDoAction());
		return true;
	}

	private IEnumerator WaitForStartDelayThenDoAction()
	{
		float seconds = UnityEngine.Random.Range(m_ActionInfo.m_StartDelayMin, m_ActionInfo.m_StartDelayMax);
		yield return new WaitForSeconds(seconds);
		m_effectsPendingFinish--;
		if (!CheckAndWaitForStartPrefabThenDoAction())
		{
			DoActionNow();
		}
	}

	private bool CheckAndWaitForStartPrefabThenDoAction()
	{
		if (!HasStart())
		{
			return false;
		}
		if (m_startSpell != null && m_startSpell.GetActiveState() == SpellStateType.IDLE)
		{
			return false;
		}
		if (m_startSpell == null)
		{
			SpawnStart();
		}
		m_startSpell.AddStateFinishedCallback(OnStartSpellBirthStateFinished);
		if (m_startSpell.GetActiveState() != SpellStateType.BIRTH)
		{
			m_startSpell.SafeActivateState(SpellStateType.BIRTH);
			if (m_startSpell.GetActiveState() == SpellStateType.NONE)
			{
				m_startSpell = null;
				return false;
			}
		}
		return true;
	}

	private void OnStartSpellBirthStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (prevStateType == SpellStateType.BIRTH)
		{
			spell.RemoveStateFinishedCallback(OnStartSpellBirthStateFinished, userData);
			DoActionNow();
		}
	}

	protected virtual void DoActionNow()
	{
		SpellAreaEffectInfo spellAreaEffectInfo = DetermineAreaEffectInfo();
		if (spellAreaEffectInfo != null)
		{
			SpawnAreaEffect(spellAreaEffectInfo);
		}
		bool flag = HasMissile();
		bool flag2 = HasImpact();
		bool flag3 = HasChain();
		if (GetVisualTargetCount() > 0 && (flag || flag2 || flag3))
		{
			if (flag)
			{
				if (flag3)
				{
					SpawnChainMissile();
				}
				else if (m_MissileInfo.m_SpawnInSequence)
				{
					SpawnMissileInSequence();
				}
				else
				{
					SpawnAllMissiles();
				}
			}
			else
			{
				if (flag2)
				{
					if (flag3)
					{
						SpawnImpact(m_currentTargetIndex);
					}
					else
					{
						SpawnAllImpacts();
					}
				}
				if (flag3)
				{
					SpawnChain();
				}
				DoStartSpellAction();
			}
		}
		else
		{
			DoStartSpellAction();
		}
		FinishIfPossible();
	}

	private bool HasStart()
	{
		if (m_StartInfo != null && m_StartInfo.m_Enabled)
		{
			return m_StartInfo.m_Prefab != null;
		}
		return false;
	}

	private void SpawnStart()
	{
		m_effectsPendingFinish++;
		m_startSpell = CloneSpell(m_StartInfo.m_Prefab);
		m_startSpell.SetSource(GetSource());
		m_startSpell.AddTargets(GetTargets());
		if (m_StartInfo.m_UseSuperSpellLocation)
		{
			m_startSpell.SetPosition(base.transform.position);
		}
	}

	private void DoStartSpellAction()
	{
		if (!(m_startSpell == null))
		{
			if (!m_startSpell.HasUsableState(SpellStateType.ACTION))
			{
				m_startSpell.UpdateTransform();
				m_startSpell.SafeActivateState(SpellStateType.DEATH);
			}
			else
			{
				m_startSpell.AddFinishedCallback(OnStartSpellActionFinished);
				m_startSpell.ActivateState(SpellStateType.ACTION);
			}
			m_startSpell = null;
		}
	}

	private void OnStartSpellActionFinished(Spell spell, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.ACTION)
		{
			spell.SafeActivateState(SpellStateType.DEATH);
		}
	}

	private bool HasMissile()
	{
		if (m_MissileInfo != null && m_MissileInfo.m_Enabled)
		{
			if (!(m_MissileInfo.m_Prefab != null))
			{
				return m_MissileInfo.m_ReversePrefab != null;
			}
			return true;
		}
		return false;
	}

	private void SpawnChainMissile()
	{
		SpawnMissile(GetPrimaryTargetIndex());
		DoStartSpellAction();
	}

	private void SpawnMissileInSequence()
	{
		if (m_currentTargetIndex >= GetVisualTargetCount())
		{
			return;
		}
		SpawnMissile(m_currentTargetIndex);
		m_currentTargetIndex++;
		if (m_startSpell == null)
		{
			return;
		}
		if (m_StartInfo.m_DeathAfterAllMissilesFire)
		{
			if (m_currentTargetIndex < GetVisualTargetCount())
			{
				if (m_startSpell.HasUsableState(SpellStateType.ACTION))
				{
					m_startSpell.ActivateState(SpellStateType.ACTION);
				}
			}
			else
			{
				DoStartSpellAction();
			}
		}
		else
		{
			DoStartSpellAction();
		}
	}

	private void SpawnAllMissiles()
	{
		for (int i = 0; i < GetVisualTargetCount(); i++)
		{
			SpawnMissile(i);
		}
		DoStartSpellAction();
	}

	private void SpawnMissile(int targetIndex)
	{
		m_effectsPendingFinish++;
		StartCoroutine(WaitAndSpawnMissile(targetIndex));
	}

	private IEnumerator WaitAndSpawnMissile(int targetIndex)
	{
		float seconds = UnityEngine.Random.Range(m_MissileInfo.m_SpawnDelaySecMin, m_MissileInfo.m_SpawnDelaySecMax);
		if (!m_MissileInfo.m_SpawnInSequence || targetIndex == 0)
		{
			yield return new WaitForSeconds(seconds);
		}
		if (m_MissileInfo.m_SpawnOffset > 0f && targetIndex > 0)
		{
			yield return new WaitForSeconds(m_MissileInfo.m_SpawnOffset * (float)targetIndex);
		}
		int metaDataIndexForTarget = GetMetaDataIndexForTarget(targetIndex);
		if (ShouldCompleteTasksUntilMetaData(metaDataIndexForTarget))
		{
			yield return StartCoroutine(CompleteTasksUntilMetaData(metaDataIndexForTarget));
		}
		if (m_visualTargets.Count <= targetIndex || m_visualTargets[targetIndex] == null)
		{
			m_effectsPendingFinish--;
			yield break;
		}
		GameObject source = GetSource();
		GameObject gameObject = m_visualTargets[targetIndex];
		if (m_MissileInfo.m_Prefab != null)
		{
			Spell spell;
			if (m_MissileInfo.m_UseSuperSpellLocation)
			{
				spell = CloneSpell(m_MissileInfo.m_Prefab, base.transform.position);
				spell.ClearPositionDirtyFlag();
			}
			else
			{
				spell = CloneSpell(m_MissileInfo.m_Prefab);
			}
			spell.SetSource(source);
			spell.AddTarget(gameObject);
			spell.AddStateFinishedCallback(OnMissileSpellStateFinished, targetIndex);
			spell.ActivateState(SpellStateType.BIRTH);
		}
		else
		{
			m_effectsPendingFinish--;
		}
		if (m_MissileInfo.m_ReversePrefab != null)
		{
			m_effectsPendingFinish++;
			StartCoroutine(SpawnReverseMissile(m_MissileInfo.m_ReversePrefab, source, gameObject, m_MissileInfo.m_reverseDelay));
		}
	}

	private IEnumerator SpawnReverseMissile(Spell cloneSpell, GameObject sourceObject, GameObject targetObject, float delay)
	{
		yield return new WaitForSeconds(delay);
		Spell spell = CloneSpell(cloneSpell);
		spell.SetSource(targetObject);
		spell.AddTarget(sourceObject);
		spell.AddStateFinishedCallback(OnMissileSpellStateFinished, -1);
		spell.ActivateState(SpellStateType.BIRTH);
	}

	private void OnMissileSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (prevStateType == SpellStateType.BIRTH)
		{
			spell.RemoveStateFinishedCallback(OnMissileSpellStateFinished, userData);
			int num = (int)userData;
			bool reverse = num < 0;
			FireMissileOnPath(spell, num, reverse);
		}
	}

	private void FireMissileOnPath(Spell missile, int targetIndex, bool reverse)
	{
		Vector3[] array = GenerateMissilePath(missile);
		float num = UnityEngine.Random.Range(m_MissileInfo.m_PathDurationMin, m_MissileInfo.m_PathDurationMax);
		Hashtable hashtable = iTween.Hash("path", array, "time", num, "easetype", m_MissileInfo.m_PathEaseType, "oncompletetarget", base.gameObject);
		if (reverse)
		{
			hashtable.Add("oncomplete", "OnReverseMissileTargetReached");
			hashtable.Add("oncompleteparams", missile);
		}
		else
		{
			Hashtable value = iTween.Hash("missile", missile, "targetIndex", targetIndex);
			hashtable.Add("oncomplete", "OnMissileTargetReached");
			hashtable.Add("oncompleteparams", value);
		}
		if (!object.Equals(array[0], array[2]))
		{
			hashtable.Add("orienttopath", m_MissileInfo.m_OrientToPath);
		}
		if (m_MissileInfo.m_TargetJoint.Length > 0)
		{
			GameObject gameObject = SceneUtils.FindChildBySubstring(missile.gameObject, m_MissileInfo.m_TargetJoint);
			if (gameObject != null)
			{
				missile.transform.LookAt(missile.GetTarget().transform, m_MissileInfo.m_JointUpVector);
				array[2].y += m_MissileInfo.m_TargetHeightOffset;
				iTween.MoveTo(gameObject, hashtable);
				return;
			}
		}
		iTween.MoveTo(missile.gameObject, hashtable);
	}

	private Vector3[] GenerateMissilePath(Spell missile)
	{
		Vector3[] array = new Vector3[3]
		{
			missile.transform.position,
			default(Vector3),
			default(Vector3)
		};
		Card targetCard = missile.GetTargetCard();
		if (targetCard != null && targetCard.GetZone() is ZoneHand && !m_MissileInfo.m_UseTargetCardPositionInsteadOfHandSlot)
		{
			ZoneHand zoneHand = targetCard.GetZone() as ZoneHand;
			array[2] = zoneHand.GetCardPosition(zoneHand.GetCardSlot(targetCard), -1);
		}
		else
		{
			array[2] = missile.GetTarget().transform.position;
		}
		array[1] = GenerateMissilePathCenterPoint(array);
		return array;
	}

	private Vector3 GenerateMissilePathCenterPoint(Vector3[] path)
	{
		Vector3 vector = path[0];
		Vector3 vector2 = path[2];
		Vector3 vector3 = vector2 - vector;
		float magnitude = vector3.magnitude;
		Vector3 result = vector;
		bool flag = magnitude <= Mathf.Epsilon;
		if (!flag)
		{
			result = vector + vector3 * (m_MissileInfo.m_CenterOffsetPercent * 0.01f);
		}
		float num = magnitude / m_MissileInfo.m_DistanceScaleFactor;
		if (flag)
		{
			if (m_MissileInfo.m_CenterPointHeightMin <= Mathf.Epsilon && m_MissileInfo.m_CenterPointHeightMax <= Mathf.Epsilon)
			{
				result.y += 2f;
			}
			else
			{
				result.y += UnityEngine.Random.Range(m_MissileInfo.m_CenterPointHeightMin, m_MissileInfo.m_CenterPointHeightMax);
			}
		}
		else
		{
			result.y += num * UnityEngine.Random.Range(m_MissileInfo.m_CenterPointHeightMin, m_MissileInfo.m_CenterPointHeightMax);
		}
		float num2 = 1f;
		if (vector.z > vector2.z)
		{
			num2 = -1f;
		}
		bool flag2 = GeneralUtils.RandomBool();
		if (m_MissileInfo.m_RightMin == 0f && m_MissileInfo.m_RightMax == 0f)
		{
			flag2 = false;
		}
		if (m_MissileInfo.m_LeftMin == 0f && m_MissileInfo.m_LeftMax == 0f)
		{
			flag2 = true;
		}
		if (flag2)
		{
			if (m_MissileInfo.m_RightMin == m_MissileInfo.m_RightMax || m_MissileInfo.m_DebugForceMax)
			{
				result.x += m_MissileInfo.m_RightMax * num * num2;
			}
			else
			{
				result.x += UnityEngine.Random.Range(m_MissileInfo.m_RightMin * num, m_MissileInfo.m_RightMax * num) * num2;
			}
		}
		else if (m_MissileInfo.m_LeftMin == m_MissileInfo.m_LeftMax || m_MissileInfo.m_DebugForceMax)
		{
			result.x -= m_MissileInfo.m_LeftMax * num * num2;
		}
		else
		{
			result.x -= UnityEngine.Random.Range(m_MissileInfo.m_LeftMin * num, m_MissileInfo.m_LeftMax * num) * num2;
		}
		return result;
	}

	private void OnMissileTargetReached(Hashtable args)
	{
		Spell obj = (Spell)args["missile"];
		int targetIndex = (int)args["targetIndex"];
		if (HasImpact())
		{
			SpawnImpact(targetIndex);
		}
		if (HasChain())
		{
			SpawnChain();
		}
		else if (m_MissileInfo.m_SpawnInSequence)
		{
			SpawnMissileInSequence();
		}
		obj.ActivateState(SpellStateType.DEATH);
	}

	private void OnReverseMissileTargetReached(Spell missile)
	{
		missile.ActivateState(SpellStateType.DEATH);
	}

	private bool HasImpact()
	{
		if (m_ImpactInfo != null && m_ImpactInfo.m_Enabled)
		{
			if (!(m_ImpactInfo.m_Prefab != null))
			{
				return m_ImpactInfo.m_damageAmountImpactSpells.Length != 0;
			}
			return true;
		}
		return false;
	}

	private void SpawnAllImpacts()
	{
		for (int i = 0; i < GetVisualTargetCount(); i++)
		{
			SpawnImpact(i);
		}
	}

	private void SpawnImpact(int targetIndex)
	{
		m_effectsPendingFinish++;
		StartCoroutine(WaitAndSpawnImpact(targetIndex));
	}

	private IEnumerator WaitAndSpawnImpact(int targetIndex)
	{
		float seconds = UnityEngine.Random.Range(m_ImpactInfo.m_SpawnDelaySecMin, m_ImpactInfo.m_SpawnDelaySecMax);
		yield return new WaitForSeconds(seconds);
		if (m_ImpactInfo.m_SpawnOffset > 0f && targetIndex > 0)
		{
			yield return new WaitForSeconds(m_ImpactInfo.m_SpawnOffset * (float)targetIndex);
		}
		int metaDataIndex = GetMetaDataIndexForTarget(targetIndex);
		if (metaDataIndex >= 0)
		{
			if (ShouldCompleteTasksUntilMetaData(metaDataIndex))
			{
				yield return StartCoroutine(CompleteTasksUntilMetaData(metaDataIndex));
			}
			float delaySec = UnityEngine.Random.Range(m_ImpactInfo.m_GameDelaySecMin, m_ImpactInfo.m_GameDelaySecMax);
			StartCoroutine(CompleteTasksFromMetaData(metaDataIndex, delaySec));
		}
		if (m_visualTargets.Count <= targetIndex || m_visualTargets[targetIndex] == null)
		{
			m_effectsPendingFinish--;
			yield break;
		}
		GameObject source = GetSource();
		GameObject gameObject = m_visualTargets[targetIndex];
		Spell prefab = DetermineImpactPrefab(gameObject);
		Spell spell = CloneSpell(prefab);
		spell.SetSource(source);
		spell.AddTarget(gameObject);
		if (m_ImpactInfo.m_UseSuperSpellLocation)
		{
			spell.SetPosition(base.transform.position);
		}
		else
		{
			if (IsMakingClones())
			{
				spell.m_Location = m_ImpactInfo.m_Location;
				spell.m_SetParentToLocation = m_ImpactInfo.m_SetParentToLocation;
			}
			spell.UpdatePosition();
		}
		spell.UpdateOrientation();
		spell.Activate();
	}

	private Spell DetermineImpactPrefab(GameObject targetObject)
	{
		if (m_ImpactInfo.m_damageAmountImpactSpells.Length == 0)
		{
			return m_ImpactInfo.m_Prefab;
		}
		Spell result = m_ImpactInfo.m_Prefab;
		if (m_taskList == null)
		{
			return result;
		}
		Card component = targetObject.GetComponent<Card>();
		if (component == null)
		{
			return result;
		}
		PowerTaskList.DamageInfo damageInfo = m_taskList.GetDamageInfo(component.GetEntity());
		if (damageInfo == null)
		{
			return result;
		}
		SpellValueRange appropriateElementAccordingToRanges = SpellUtils.GetAppropriateElementAccordingToRanges(m_ImpactInfo.m_damageAmountImpactSpells, (SpellValueRange x) => x.m_range, damageInfo.m_damage);
		if (appropriateElementAccordingToRanges != null && appropriateElementAccordingToRanges.m_spellPrefab != null)
		{
			result = appropriateElementAccordingToRanges.m_spellPrefab;
		}
		return result;
	}

	private bool HasChain()
	{
		if (m_ChainInfo != null && m_ChainInfo.m_Enabled)
		{
			return m_ChainInfo.m_Prefab != null;
		}
		return false;
	}

	private void SpawnChain()
	{
		if (GetVisualTargetCount() > 1)
		{
			m_effectsPendingFinish++;
			StartCoroutine(WaitAndSpawnChain());
		}
	}

	private IEnumerator WaitAndSpawnChain()
	{
		float seconds = UnityEngine.Random.Range(m_ChainInfo.m_SpawnDelayMin, m_ChainInfo.m_SpawnDelayMax);
		yield return new WaitForSeconds(seconds);
		Spell spell = CloneSpell(m_ChainInfo.m_Prefab);
		GameObject primaryTarget = GetPrimaryTarget();
		spell.SetSource(primaryTarget);
		foreach (GameObject visualTarget in m_visualTargets)
		{
			if (!(visualTarget == primaryTarget))
			{
				spell.AddTarget(visualTarget);
			}
		}
		spell.ActivateState(SpellStateType.ACTION);
	}

	private SpellAreaEffectInfo DetermineAreaEffectInfo()
	{
		Card sourceCard = GetSourceCard();
		if (sourceCard != null)
		{
			Player controller = sourceCard.GetController();
			if (controller != null)
			{
				if (controller.IsFriendlySide() && HasFriendlyAreaEffect())
				{
					return m_FriendlyAreaEffectInfo;
				}
				if (!controller.IsFriendlySide() && HasOpponentAreaEffect())
				{
					return m_OpponentAreaEffectInfo;
				}
			}
		}
		if (HasFriendlyAreaEffect())
		{
			return m_FriendlyAreaEffectInfo;
		}
		if (HasOpponentAreaEffect())
		{
			return m_OpponentAreaEffectInfo;
		}
		return null;
	}

	private bool HasAreaEffect()
	{
		if (!HasFriendlyAreaEffect())
		{
			return HasOpponentAreaEffect();
		}
		return true;
	}

	private bool HasFriendlyAreaEffect()
	{
		if (m_FriendlyAreaEffectInfo != null && m_FriendlyAreaEffectInfo.m_Enabled)
		{
			return m_FriendlyAreaEffectInfo.m_Prefab != null;
		}
		return false;
	}

	private bool HasOpponentAreaEffect()
	{
		if (m_OpponentAreaEffectInfo != null && m_OpponentAreaEffectInfo.m_Enabled)
		{
			return m_OpponentAreaEffectInfo.m_Prefab != null;
		}
		return false;
	}

	private void SpawnAreaEffect(SpellAreaEffectInfo info)
	{
		m_effectsPendingFinish++;
		StartCoroutine(WaitAndSpawnAreaEffect(info));
	}

	private IEnumerator WaitAndSpawnAreaEffect(SpellAreaEffectInfo info)
	{
		float num = UnityEngine.Random.Range(info.m_SpawnDelaySecMin, info.m_SpawnDelaySecMax);
		if (num > 0f)
		{
			yield return new WaitForSeconds(num);
		}
		Spell spell = CloneSpell(info.m_Prefab);
		spell.SetSource(GetSource());
		spell.AttachPowerTaskList(m_taskList);
		if (info.m_UseSuperSpellLocation)
		{
			spell.SetPosition(base.transform.position);
		}
		else if (IsMakingClones() && info.m_Location != 0)
		{
			spell.m_Location = info.m_Location;
			spell.m_SetParentToLocation = info.m_SetParentToLocation;
			spell.UpdatePosition();
		}
		if (IsMakingClones() && info.m_Facing != 0)
		{
			spell.m_Facing = info.m_Facing;
			spell.m_FacingOptions = info.m_FacingOptions;
			spell.UpdateOrientation();
		}
		if (OnBeforeActivateAreaEffectSpell != null)
		{
			OnBeforeActivateAreaEffectSpell(spell);
		}
		spell.Activate();
		m_activeAreaEffectSpell = spell;
	}

	private bool AddPrimaryChainTarget()
	{
		Network.HistBlockStart blockStart = m_taskList.GetBlockStart();
		if (blockStart == null)
		{
			return false;
		}
		return AddSinglePowerTarget_FromBlockStart(blockStart);
	}

	private int GetPrimaryTargetIndex()
	{
		return 0;
	}

	private GameObject GetPrimaryTarget()
	{
		return m_visualTargets[GetPrimaryTargetIndex()];
	}

	protected virtual void UpdateTargets()
	{
		UpdateVisualTargets();
		SuppressPlaySoundsOnVisualTargets();
	}

	private int GetVisualTargetCount()
	{
		if (IsMakingClones())
		{
			return m_visualTargets.Count;
		}
		return Mathf.Min(1, m_visualTargets.Count);
	}

	protected virtual void UpdateVisualTargets()
	{
		switch (m_TargetInfo.m_Behavior)
		{
		case SpellTargetBehavior.FRIENDLY_PLAY_ZONE_CENTER:
		{
			ZonePlay zonePlay4 = SpellUtils.FindFriendlyPlayZone(this);
			AddVisualTarget(zonePlay4.gameObject);
			break;
		}
		case SpellTargetBehavior.FRIENDLY_PLAY_ZONE_RANDOM:
		{
			ZonePlay zonePlay3 = SpellUtils.FindFriendlyPlayZone(this);
			GenerateRandomPlayZoneVisualTargets(zonePlay3);
			break;
		}
		case SpellTargetBehavior.OPPONENT_PLAY_ZONE_CENTER:
		{
			ZonePlay zonePlay2 = SpellUtils.FindOpponentPlayZone(this);
			AddVisualTarget(zonePlay2.gameObject);
			break;
		}
		case SpellTargetBehavior.OPPONENT_PLAY_ZONE_RANDOM:
		{
			ZonePlay zonePlay = SpellUtils.FindOpponentPlayZone(this);
			GenerateRandomPlayZoneVisualTargets(zonePlay);
			break;
		}
		case SpellTargetBehavior.BOARD_CENTER:
		{
			Board board = Board.Get();
			AddVisualTarget(board.FindBone("CenterPointBone").gameObject);
			break;
		}
		case SpellTargetBehavior.UNTARGETED:
			AddVisualTarget(GetSource());
			break;
		case SpellTargetBehavior.CHOSEN_TARGET_ONLY:
			AddChosenTargetAsVisualTarget();
			break;
		case SpellTargetBehavior.BOARD_RANDOM:
			GenerateRandomBoardVisualTargets();
			break;
		case SpellTargetBehavior.TARGET_ZONE_CENTER:
		{
			Zone zone = SpellUtils.FindTargetZone(this);
			AddVisualTarget(zone.gameObject);
			break;
		}
		case SpellTargetBehavior.NEW_CREATED_CARDS:
			GenerateCreatedCardsTargets();
			break;
		case SpellTargetBehavior.NEW_CREATED_CARDS_IN_PLAY:
			GenerateCreatedCardsTargets(TAG_ZONE.PLAY);
			break;
		default:
			AddAllTargetsAsVisualTargets();
			break;
		}
	}

	protected void GenerateRandomBoardVisualTargets()
	{
		ZonePlay zonePlay = SpellUtils.FindFriendlyPlayZone(this);
		ZonePlay zonePlay2 = SpellUtils.FindOpponentPlayZone(this);
		Bounds bounds = zonePlay.GetComponent<Collider>().bounds;
		Bounds bounds2 = zonePlay2.GetComponent<Collider>().bounds;
		Vector3 vector = Vector3.Min(bounds.min, bounds2.min);
		Vector3 vector2 = Vector3.Max(bounds.max, bounds2.max);
		Vector3 center = 0.5f * (vector2 + vector);
		Vector3 vector3 = vector2 - vector;
		Vector3 size = new Vector3(Mathf.Abs(vector3.x), Mathf.Abs(vector3.y), Mathf.Abs(vector3.z));
		Bounds bounds3 = new Bounds(center, size);
		GenerateRandomVisualTargets(bounds3);
	}

	protected void GenerateRandomPlayZoneVisualTargets(ZonePlay zonePlay)
	{
		GenerateRandomVisualTargets(zonePlay.GetComponent<Collider>().bounds);
	}

	private void GenerateRandomVisualTargets(Bounds bounds)
	{
		int num = UnityEngine.Random.Range(m_TargetInfo.m_RandomTargetCountMin, m_TargetInfo.m_RandomTargetCountMax + 1);
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
				if (ComputeBoxPickChance(array, k) >= num3)
				{
					array2[max++] = k;
				}
			}
			int num4 = array2[UnityEngine.Random.Range(0, max)];
			array[num4]++;
			float num5 = x + (float)num4 * num2;
			float max2 = num5 + num2;
			Vector3 position = default(Vector3);
			position.x = UnityEngine.Random.Range(num5, max2);
			position.y = bounds.center.y;
			position.z = UnityEngine.Random.Range(z2, z);
			GenerateVisualTarget(position, j, num4);
		}
	}

	private void GenerateVisualTarget(Vector3 position, int index, int boxIndex)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = $"{this} Target {index} (box {boxIndex})";
		gameObject.transform.position = position;
		gameObject.AddComponent<SpellGeneratedTarget>();
		AddVisualTarget(gameObject);
	}

	private float ComputeBoxPickChance(int[] boxUsageCounts, int index)
	{
		int num = boxUsageCounts[index];
		float num2 = (float)boxUsageCounts.Length * 0.25f;
		float num3 = (float)num / num2;
		return 1f - num3;
	}

	private void GenerateCreatedCardsTargets(TAG_ZONE onlyAffectZone = TAG_ZONE.INVALID)
	{
		if (m_taskList == null)
		{
			return;
		}
		foreach (PowerTask task in m_taskList.GetTaskList())
		{
			Network.PowerHistory power = task.GetPower();
			if (power.Type != Network.PowerType.FULL_ENTITY)
			{
				continue;
			}
			int iD = (power as Network.HistFullEntity).Entity.ID;
			Entity entity = GameState.Get().GetEntity(iD);
			if ((onlyAffectZone != 0 && entity.GetTag(GAME_TAG.ZONE) != (int)onlyAffectZone) || entity.GetTag(GAME_TAG.ZONE) == 6)
			{
				continue;
			}
			if (entity == null)
			{
				Debug.LogWarning($"{this}.GenerateCreatedCardsTargets() - WARNING trying to target entity with id {iD} but there is no entity with that id");
				continue;
			}
			Card card = entity.GetCard();
			if (card == null)
			{
				Debug.LogWarning($"{this}.GenerateCreatedCardsTargets() - WARNING trying to target entity.GetCard() with id {iD} but there is no card with that id");
			}
			else
			{
				m_visualTargets.Add(card.gameObject);
			}
		}
	}

	private void AddChosenTargetAsVisualTarget()
	{
		Card powerTargetCard = GetPowerTargetCard();
		if (powerTargetCard == null)
		{
			Debug.LogWarning($"{this}.AddChosenTargetAsVisualTarget() - there is no chosen target");
		}
		else
		{
			AddVisualTarget(powerTargetCard.gameObject);
		}
	}

	private void AddAllTargetsAsVisualTargets()
	{
		for (int i = 0; i < m_targets.Count; i++)
		{
			int count = m_visualTargets.Count;
			m_visualToTargetIndexMap[count] = i;
			AddVisualTarget(m_targets[i]);
		}
	}

	private void SuppressPlaySoundsOnVisualTargets()
	{
		if (!m_TargetInfo.m_SuppressPlaySounds)
		{
			return;
		}
		for (int i = 0; i < m_visualTargets.Count; i++)
		{
			Card component = m_visualTargets[i].GetComponent<Card>();
			if (!(component == null))
			{
				component.SuppressPlaySounds(suppress: true);
			}
		}
	}

	protected virtual void CleanUp()
	{
		foreach (GameObject visualTarget in m_visualTargets)
		{
			if (visualTarget == null)
			{
				Debug.LogWarning($"{this}.CleanUp() - found a null GameObject in m_visualTargets");
			}
			else if (!(visualTarget.GetComponent<SpellGeneratedTarget>() == null))
			{
				UnityEngine.Object.Destroy(visualTarget);
			}
		}
		m_visualTargets.Clear();
	}

	protected bool HasMetaDataTargets()
	{
		return m_targetToMetaDataMap.Count > 0;
	}

	protected int GetMetaDataIndexForTarget(int visualTargetIndex)
	{
		if (!m_visualToTargetIndexMap.TryGetValue(visualTargetIndex, out var value))
		{
			return -1;
		}
		if (!m_targetToMetaDataMap.TryGetValue(value, out var value2))
		{
			return -1;
		}
		return value2;
	}

	protected bool ShouldCompleteTasksUntilMetaData(int metaDataIndex)
	{
		if (m_taskList == null || IsBatchedTargetInfo(metaDataIndex) || !m_taskList.HasEarlierIncompleteTask(metaDataIndex))
		{
			return false;
		}
		return true;
	}

	private bool IsBatchedTargetInfo(int metaDataIndex)
	{
		if (m_taskList.GetTaskList().Count >= metaDataIndex)
		{
			return false;
		}
		Network.HistMetaData histMetaData = m_taskList.GetTaskList()[metaDataIndex].GetPower() as Network.HistMetaData;
		if (histMetaData != null && histMetaData.MetaType == HistoryMeta.Type.TARGET && histMetaData.Data != 0)
		{
			return true;
		}
		return false;
	}

	protected IEnumerator CompleteTasksUntilMetaData(int metaDataIndex)
	{
		m_effectsPendingFinish++;
		m_taskList.DoTasks(0, metaDataIndex);
		QueueList<PowerTask> queueList = DetermineTasksToWaitFor(0, metaDataIndex);
		if (queueList != null && queueList.Count > 0)
		{
			yield return StartCoroutine(WaitForTasks(queueList));
		}
		m_effectsPendingFinish--;
	}

	protected QueueList<PowerTask> DetermineTasksToWaitFor(int startIndex, int count)
	{
		if (count == 0)
		{
			return null;
		}
		int num = startIndex + count;
		QueueList<PowerTask> queueList = new QueueList<PowerTask>();
		List<PowerTask> taskList = m_taskList.GetTaskList();
		for (int i = startIndex; i < num; i++)
		{
			PowerTask powerTask = taskList[i];
			Entity entity = GetEntityFromZoneChangePowerTask(powerTask);
			if (entity == null || m_visualTargets.Find(delegate(GameObject currTargetObject)
			{
				Card component = currTargetObject.GetComponent<Card>();
				return entity.GetCard() == component;
			}) == null)
			{
				continue;
			}
			for (int j = 0; j < queueList.Count; j++)
			{
				PowerTask task = queueList[j];
				Entity entityFromZoneChangePowerTask = GetEntityFromZoneChangePowerTask(task);
				if (entity == entityFromZoneChangePowerTask)
				{
					queueList.RemoveAt(j);
					break;
				}
			}
			queueList.Enqueue(powerTask);
		}
		return queueList;
	}

	protected IEnumerator WaitForTasks(QueueList<PowerTask> tasksToWaitFor)
	{
		while (tasksToWaitFor.Count > 0)
		{
			PowerTask powerTask = tasksToWaitFor.Peek();
			if (!powerTask.IsCompleted())
			{
				yield return null;
				continue;
			}
			GetZoneChangeFromPowerTask(powerTask, out var entity, out var zoneTag);
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
		}
	}

	protected IEnumerator CompleteTasksFromMetaData(int metaDataIndex, float delaySec)
	{
		m_effectsPendingFinish++;
		yield return new WaitForSeconds(delaySec);
		CompleteMetaDataTasks(metaDataIndex, OnMetaDataTasksComplete);
	}

	protected void OnMetaDataTasksComplete(PowerTaskList taskList, int startIndex, int count, object userData)
	{
		m_effectsPendingFinish--;
		FinishIfPossible();
	}

	protected bool IsMakingClones()
	{
		return true;
	}

	protected bool AreEffectsActive()
	{
		return m_effectsPendingFinish > 0;
	}

	protected Spell CloneSpell(Spell prefab, Vector3? position = null, FinishedCallback finishedCallback = null)
	{
		Spell spell;
		if (IsMakingClones())
		{
			spell = ((!position.HasValue) ? UnityEngine.Object.Instantiate(prefab) : UnityEngine.Object.Instantiate(prefab, position.Value, Quaternion.identity));
			spell.AddStateStartedCallback(OnCloneSpellStateStarted);
			spell.transform.parent = base.transform;
			m_activeClonedSpells.Add(spell);
		}
		else
		{
			spell = prefab;
			spell.RemoveAllTargets();
		}
		FinishedCallback callback = ((finishedCallback == null) ? new FinishedCallback(OnCloneSpellFinished) : finishedCallback);
		spell.AddFinishedCallback(callback);
		return spell;
	}

	private void OnCloneSpellFinished(Spell spell, object userData)
	{
		m_effectsPendingFinish--;
		FinishIfPossible();
	}

	private void OnCloneSpellStateStarted(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			m_activeClonedSpells.Remove(spell);
			UnityEngine.Object.Destroy(spell.gameObject);
		}
	}

	private void UpdatePendingStateChangeFlags(SpellStateType stateType)
	{
		if (!HasStateContent(stateType))
		{
			m_pendingNoneStateChange = true;
			m_pendingSpellFinish = true;
		}
		else
		{
			m_pendingNoneStateChange = false;
			m_pendingSpellFinish = false;
		}
	}

	protected void FinishIfPossible()
	{
		if (!m_settingUpAction && !AreEffectsActive())
		{
			if (m_pendingSpellFinish)
			{
				OnSpellFinished();
				m_pendingSpellFinish = false;
			}
			if (m_pendingNoneStateChange)
			{
				OnStateFinished();
				m_pendingNoneStateChange = false;
			}
			CleanUp();
		}
	}
}
