using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Core;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using PegasusGame;
using UnityEngine;

public class Spell : MonoBehaviour
{
	public delegate void FinishedCallback(Spell spell, object userData);

	public delegate void StateFinishedCallback(Spell spell, SpellStateType prevStateType, object userData);

	public delegate void StateStartedCallback(Spell spell, SpellStateType prevStateType, object userData);

	public delegate void SpellEventCallback(string eventName, object eventData, object userData);

	private class FinishedListener : EventListener<FinishedCallback>
	{
		public void Fire(Spell spell)
		{
			m_callback(spell, m_userData);
		}
	}

	private class StateFinishedListener : EventListener<StateFinishedCallback>
	{
		public void Fire(Spell spell, SpellStateType prevStateType)
		{
			m_callback(spell, prevStateType, m_userData);
		}
	}

	private class StateStartedListener : EventListener<StateStartedCallback>
	{
		public void Fire(Spell spell, SpellStateType prevStateType)
		{
			m_callback(spell, prevStateType, m_userData);
		}
	}

	private class SpellEventListener : EventListener<SpellEventCallback>
	{
		public void Fire(string eventName, object eventData)
		{
			m_callback(eventName, eventData, m_userData);
		}
	}

	[UnityEngine.Tooltip("If checked, this spell will block power history processing when the spell leaves the None state.")]
	public bool m_BlockServerEvents;

	[UnityEngine.Tooltip("Additional configuration on when this spell should block power history processing")]
	public PowerProcessorBlockingBehavior m_BlockPowerProcessing;

	public GameObject m_ObjectContainer;

	public SpellLocation m_Location = SpellLocation.SOURCE_AUTO;

	public string m_LocationTransformName;

	public bool m_SetParentToLocation;

	public SpellFacing m_Facing;

	public SpellFacingOptions m_FacingOptions;

	public TARGET_RETICLE_TYPE m_TargetReticle;

	public List<SpellZoneTag> m_ZonesToDisable;

	[UnityEngine.Tooltip("Delay (in seconds) to wait before sorting a zone after processing entity death. This is often used in CustomDeath spells, in order to wait for the custom death animation to play through before sorting the Play zone.")]
	public float m_ZoneLayoutDelayForDeaths;

	public bool m_UseFastActorTriggers;

	public bool m_ExclusivelyUseMetadataForTargeting;

	protected SpellType m_spellType;

	private Map<SpellStateType, List<SpellState>> m_spellStateMap;

	protected SpellStateType m_activeStateType;

	protected SpellStateType m_activeStateChange;

	private List<FinishedListener> m_finishedListeners = new List<FinishedListener>();

	private List<StateFinishedListener> m_stateFinishedListeners = new List<StateFinishedListener>();

	private List<StateStartedListener> m_stateStartedListeners = new List<StateStartedListener>();

	private List<SpellEventListener> m_spellEventListeners = new List<SpellEventListener>();

	protected GameObject m_source;

	protected List<GameObject> m_targets = new List<GameObject>();

	protected PowerTaskList m_taskList;

	protected bool m_shown = true;

	protected PlayMakerFSM m_fsm;

	private Map<SpellStateType, FsmState> m_fsmStateMap;

	private bool m_fsmSkippedFirstFrame;

	private bool m_fsmReady;

	protected bool m_positionDirty = true;

	protected bool m_orientationDirty = true;

	protected bool m_finished;

	protected virtual void Awake()
	{
		BuildSpellStateMap();
		m_fsm = GetComponent<PlayMakerFSM>();
		if (!string.IsNullOrEmpty(m_LocationTransformName))
		{
			m_LocationTransformName = m_LocationTransformName.Trim();
		}
	}

	protected virtual void OnDestroy()
	{
		if (m_ObjectContainer != null)
		{
			UnityEngine.Object.Destroy(m_ObjectContainer);
			m_ObjectContainer = null;
		}
		if (base.gameObject != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	protected virtual void Start()
	{
		if (m_activeStateType == SpellStateType.NONE)
		{
			ActivateObjectContainer(enable: false);
		}
		else if (m_shown)
		{
			ShowImpl();
		}
		else
		{
			HideImpl();
		}
	}

	private void Update()
	{
		if (!m_fsmReady)
		{
			if (m_fsm == null)
			{
				m_fsmReady = true;
			}
			else if (!m_fsmSkippedFirstFrame)
			{
				m_fsmSkippedFirstFrame = true;
			}
			else if (m_fsm.enabled)
			{
				BuildFsmStateMap();
				m_fsmReady = true;
			}
		}
	}

	public SpellType GetSpellType()
	{
		return m_spellType;
	}

	public void SetSpellType(SpellType spellType)
	{
		m_spellType = spellType;
	}

	public bool DoesBlockServerEvents()
	{
		if (GameState.Get() == null)
		{
			return false;
		}
		return m_BlockServerEvents;
	}

	public SuperSpell GetSuperSpellParent()
	{
		if (base.transform.parent == null)
		{
			return null;
		}
		return base.transform.parent.GetComponent<SuperSpell>();
	}

	public PowerTaskList GetPowerTaskList()
	{
		return m_taskList;
	}

	public Entity GetPowerSource()
	{
		if (m_taskList == null)
		{
			return null;
		}
		return m_taskList.GetSourceEntity();
	}

	public Card GetPowerSourceCard()
	{
		return GetPowerSource()?.GetCard();
	}

	public Entity GetPowerTarget()
	{
		if (m_taskList == null)
		{
			return null;
		}
		return m_taskList.GetTargetEntity();
	}

	public Card GetPowerTargetCard()
	{
		return GetPowerTarget()?.GetCard();
	}

	public virtual bool CanPurge()
	{
		return !IsActive();
	}

	public virtual bool ShouldReconnectIfStuck()
	{
		return true;
	}

	public void UpdateParentActorComponents()
	{
		if (base.transform.parent != null)
		{
			Actor component = base.transform.parent.gameObject.GetComponent<Actor>();
			if (component != null)
			{
				component.UpdateAllComponents();
			}
		}
	}

	public SpellLocation GetLocation()
	{
		return m_Location;
	}

	public string GetLocationTransformName()
	{
		return m_LocationTransformName;
	}

	public SpellFacing GetFacing()
	{
		return m_Facing;
	}

	public SpellFacingOptions GetFacingOptions()
	{
		return m_FacingOptions;
	}

	public void ClearPositionDirtyFlag()
	{
		m_positionDirty = false;
	}

	public void SetPosition(Vector3 position)
	{
		base.transform.position = position;
		m_positionDirty = false;
	}

	public void SetLocalPosition(Vector3 position)
	{
		base.transform.localPosition = position;
		m_positionDirty = false;
	}

	public void SetOrientation(Quaternion orientation)
	{
		base.transform.rotation = orientation;
		m_orientationDirty = false;
	}

	public void SetLocalOrientation(Quaternion orientation)
	{
		base.transform.localRotation = orientation;
		m_orientationDirty = false;
	}

	public void ForceUpdateTransform()
	{
		m_positionDirty = true;
		UpdateTransform();
	}

	public void UpdateTransform()
	{
		UpdatePosition();
		UpdateOrientation();
	}

	public void UpdatePosition()
	{
		if (m_positionDirty)
		{
			SpellUtils.SetPositionFromLocation(this, m_SetParentToLocation);
			m_positionDirty = false;
		}
	}

	public void UpdateOrientation()
	{
		if (m_orientationDirty)
		{
			SpellUtils.SetOrientationFromFacing(this);
			m_orientationDirty = false;
		}
	}

	public GameObject GetSource()
	{
		return m_source;
	}

	public virtual void SetSource(GameObject go)
	{
		m_source = go;
	}

	public virtual void RemoveSource()
	{
		m_source = null;
	}

	public bool IsSource(GameObject go)
	{
		return m_source == go;
	}

	public Card GetSourceCard()
	{
		if (m_source == null)
		{
			return null;
		}
		return m_source.GetComponent<Card>();
	}

	public List<GameObject> GetTargets()
	{
		return m_targets;
	}

	public GameObject GetTarget()
	{
		if (m_targets.Count != 0)
		{
			return m_targets[0];
		}
		return null;
	}

	public virtual void AddTarget(GameObject go)
	{
		m_targets.Add(go);
	}

	public virtual void AddTargets(List<GameObject> targets)
	{
		m_targets.AddRange(targets);
	}

	public virtual bool RemoveTarget(GameObject go)
	{
		return m_targets.Remove(go);
	}

	public virtual void RemoveAllTargets()
	{
		m_targets.Clear();
	}

	public bool IsTarget(GameObject go)
	{
		return m_targets.Contains(go);
	}

	public Card GetTargetCard()
	{
		GameObject target = GetTarget();
		if (target == null)
		{
			return null;
		}
		return target.GetComponent<Card>();
	}

	public virtual List<GameObject> GetVisualTargets()
	{
		return GetTargets();
	}

	public virtual GameObject GetVisualTarget()
	{
		return GetTarget();
	}

	public virtual void AddVisualTarget(GameObject go)
	{
		AddTarget(go);
	}

	public virtual void AddVisualTargets(List<GameObject> targets)
	{
		AddTargets(targets);
	}

	public virtual bool RemoveVisualTarget(GameObject go)
	{
		return RemoveTarget(go);
	}

	public virtual void RemoveAllVisualTargets()
	{
		RemoveAllTargets();
	}

	public virtual bool IsVisualTarget(GameObject go)
	{
		return IsTarget(go);
	}

	public virtual Card GetVisualTargetCard()
	{
		return GetTargetCard();
	}

	public bool IsValidSpellTarget(Entity ent)
	{
		return !ent.IsEnchantment();
	}

	public bool IsShown()
	{
		return m_shown;
	}

	public void Show()
	{
		if (!m_shown)
		{
			m_shown = true;
			if (m_activeStateType != 0)
			{
				OnExitedNoneState();
			}
			ShowImpl();
		}
	}

	public void Hide()
	{
		if (m_shown)
		{
			m_shown = false;
			HideImpl();
			if (m_activeStateType != 0)
			{
				OnEnteredNoneState();
			}
		}
	}

	public void ActivateObjectContainer(bool enable)
	{
		if (!(m_ObjectContainer == null))
		{
			SceneUtils.EnableRenderers(m_ObjectContainer, enable);
		}
	}

	public bool IsActive()
	{
		return m_activeStateType != SpellStateType.NONE;
	}

	public void Activate()
	{
		SpellStateType spellStateType = GuessNextStateType();
		if (spellStateType == SpellStateType.NONE)
		{
			Deactivate();
		}
		else
		{
			ChangeState(spellStateType);
		}
	}

	public void Reactivate()
	{
		SpellStateType spellStateType = GuessNextStateType(SpellStateType.NONE);
		if (spellStateType == SpellStateType.NONE)
		{
			Deactivate();
		}
		else
		{
			ChangeState(spellStateType);
		}
	}

	public void Deactivate()
	{
		if (m_activeStateType != 0)
		{
			ForceDeactivate();
		}
	}

	public void ForceDeactivate()
	{
		ChangeState(SpellStateType.NONE);
	}

	public void ActivateState(SpellStateType stateType)
	{
		if (!HasUsableState(stateType))
		{
			Deactivate();
		}
		else
		{
			ChangeState(stateType);
		}
	}

	public void SafeActivateState(SpellStateType stateType)
	{
		if (!HasUsableState(stateType))
		{
			ForceDeactivate();
		}
		else
		{
			ChangeState(stateType);
		}
	}

	public virtual bool HasUsableState(SpellStateType stateType)
	{
		if (stateType == SpellStateType.NONE)
		{
			return false;
		}
		if (HasStateContent(stateType))
		{
			return true;
		}
		if (HasOverriddenStateMethod(stateType))
		{
			return true;
		}
		if (m_activeStateType == SpellStateType.NONE && m_ZonesToDisable != null && m_ZonesToDisable.Count > 0)
		{
			return true;
		}
		return false;
	}

	public SpellStateType GetActiveState()
	{
		return m_activeStateType;
	}

	public SpellState GetFirstSpellState(SpellStateType stateType)
	{
		if (m_spellStateMap == null)
		{
			return null;
		}
		List<SpellState> value = null;
		if (!m_spellStateMap.TryGetValue(stateType, out value))
		{
			return null;
		}
		if (value.Count == 0)
		{
			return null;
		}
		return value[0];
	}

	public List<SpellState> GetActiveStateList()
	{
		if (m_spellStateMap == null)
		{
			return null;
		}
		List<SpellState> value = null;
		if (!m_spellStateMap.TryGetValue(m_activeStateType, out value))
		{
			return null;
		}
		return value;
	}

	public bool IsFinished()
	{
		return m_finished;
	}

	public void AddFinishedCallback(FinishedCallback callback)
	{
		AddFinishedCallback(callback, null);
	}

	public void AddFinishedCallback(FinishedCallback callback, object userData)
	{
		FinishedListener finishedListener = new FinishedListener();
		finishedListener.SetCallback(callback);
		finishedListener.SetUserData(userData);
		if (!m_finishedListeners.Contains(finishedListener))
		{
			m_finishedListeners.Add(finishedListener);
		}
	}

	public bool RemoveFinishedCallback(FinishedCallback callback)
	{
		return RemoveFinishedCallback(callback, null);
	}

	public bool RemoveFinishedCallback(FinishedCallback callback, object userData)
	{
		FinishedListener finishedListener = new FinishedListener();
		finishedListener.SetCallback(callback);
		finishedListener.SetUserData(userData);
		return m_finishedListeners.Remove(finishedListener);
	}

	public void AddStateFinishedCallback(StateFinishedCallback callback)
	{
		AddStateFinishedCallback(callback, null);
	}

	public void AddStateFinishedCallback(StateFinishedCallback callback, object userData)
	{
		StateFinishedListener stateFinishedListener = new StateFinishedListener();
		stateFinishedListener.SetCallback(callback);
		stateFinishedListener.SetUserData(userData);
		if (!m_stateFinishedListeners.Contains(stateFinishedListener))
		{
			m_stateFinishedListeners.Add(stateFinishedListener);
		}
	}

	public bool RemoveStateFinishedCallback(StateFinishedCallback callback)
	{
		return RemoveStateFinishedCallback(callback, null);
	}

	public bool RemoveStateFinishedCallback(StateFinishedCallback callback, object userData)
	{
		StateFinishedListener stateFinishedListener = new StateFinishedListener();
		stateFinishedListener.SetCallback(callback);
		stateFinishedListener.SetUserData(userData);
		return m_stateFinishedListeners.Remove(stateFinishedListener);
	}

	public void AddStateStartedCallback(StateStartedCallback callback)
	{
		AddStateStartedCallback(callback, null);
	}

	public void AddStateStartedCallback(StateStartedCallback callback, object userData)
	{
		StateStartedListener stateStartedListener = new StateStartedListener();
		stateStartedListener.SetCallback(callback);
		stateStartedListener.SetUserData(userData);
		if (!m_stateStartedListeners.Contains(stateStartedListener))
		{
			m_stateStartedListeners.Add(stateStartedListener);
		}
	}

	public bool RemoveStateStartedCallback(StateStartedCallback callback)
	{
		return RemoveStateStartedCallback(callback, null);
	}

	public bool RemoveStateStartedCallback(StateStartedCallback callback, object userData)
	{
		StateStartedListener stateStartedListener = new StateStartedListener();
		stateStartedListener.SetCallback(callback);
		stateStartedListener.SetUserData(userData);
		return m_stateStartedListeners.Remove(stateStartedListener);
	}

	public void AddSpellEventCallback(SpellEventCallback callback)
	{
		AddSpellEventCallback(callback, null);
	}

	public void AddSpellEventCallback(SpellEventCallback callback, object userData)
	{
		SpellEventListener spellEventListener = new SpellEventListener();
		spellEventListener.SetCallback(callback);
		spellEventListener.SetUserData(userData);
		if (!m_spellEventListeners.Contains(spellEventListener))
		{
			m_spellEventListeners.Add(spellEventListener);
		}
	}

	public bool RemoveSpellEventCallback(SpellEventCallback callback)
	{
		return RemoveSpellEventCallback(callback, null);
	}

	public bool RemoveSpellEventCallback(SpellEventCallback callback, object userData)
	{
		SpellEventListener spellEventListener = new SpellEventListener();
		spellEventListener.SetCallback(callback);
		spellEventListener.SetUserData(userData);
		return m_spellEventListeners.Remove(spellEventListener);
	}

	public virtual void ChangeState(SpellStateType stateType)
	{
		ChangeStateImpl(stateType);
		if (m_activeStateType == stateType)
		{
			ChangeFsmState(stateType);
		}
	}

	public SpellStateType GuessNextStateType()
	{
		return GuessNextStateType(m_activeStateType);
	}

	public SpellStateType GuessNextStateType(SpellStateType stateType)
	{
		switch (stateType)
		{
		case SpellStateType.NONE:
			if (HasUsableState(SpellStateType.BIRTH))
			{
				return SpellStateType.BIRTH;
			}
			if (HasUsableState(SpellStateType.IDLE))
			{
				return SpellStateType.IDLE;
			}
			if (HasUsableState(SpellStateType.ACTION))
			{
				return SpellStateType.ACTION;
			}
			if (HasUsableState(SpellStateType.DEATH))
			{
				return SpellStateType.DEATH;
			}
			if (HasUsableState(SpellStateType.CANCEL))
			{
				return SpellStateType.CANCEL;
			}
			break;
		case SpellStateType.BIRTH:
			if (HasUsableState(SpellStateType.IDLE))
			{
				return SpellStateType.IDLE;
			}
			break;
		case SpellStateType.IDLE:
			if (HasUsableState(SpellStateType.ACTION))
			{
				return SpellStateType.ACTION;
			}
			break;
		case SpellStateType.ACTION:
			if (HasUsableState(SpellStateType.DEATH))
			{
				return SpellStateType.DEATH;
			}
			break;
		}
		return SpellStateType.NONE;
	}

	public virtual bool AttachPowerTaskList(PowerTaskList taskList)
	{
		PowerTaskList taskList2 = m_taskList;
		m_taskList = taskList;
		RemoveAllTargets();
		if (!AddPowerTargets())
		{
			m_taskList = taskList2;
			return false;
		}
		OnAttachPowerTaskList();
		return true;
	}

	public virtual bool AddPowerTargets()
	{
		if (!CanAddPowerTargets())
		{
			return false;
		}
		return AddMultiplePowerTargets();
	}

	public PowerTaskList GetLastHandledTaskList(PowerTaskList taskList)
	{
		if (taskList == null)
		{
			return null;
		}
		Spell spell = UnityEngine.Object.Instantiate(this);
		PowerTaskList result = null;
		for (PowerTaskList powerTaskList = taskList.GetLast(); powerTaskList != null; powerTaskList = powerTaskList.GetPrevious())
		{
			spell.m_taskList = powerTaskList;
			spell.RemoveAllTargets();
			if (spell.AddPowerTargets())
			{
				result = powerTaskList;
				break;
			}
		}
		UnityEngine.Object.Destroy(spell);
		return result;
	}

	public bool IsHandlingLastTaskList()
	{
		return GetLastHandledTaskList(m_taskList) == m_taskList;
	}

	public virtual void OnStateFinished()
	{
		SpellStateType stateType = GuessNextStateType();
		ChangeState(stateType);
	}

	public virtual void OnSpellFinished()
	{
		m_finished = true;
		m_positionDirty = true;
		m_orientationDirty = true;
		if (GameState.Get() != null)
		{
			GameState.Get().RemoveServerBlockingSpell(this);
		}
		BlockZones(block: false);
		if (m_UseFastActorTriggers && GameState.Get() != null && IsHandlingLastTaskList())
		{
			GameState.Get().SetUsingFastActorTriggers(enable: false);
		}
		FireFinishedCallbacks();
	}

	public virtual void OnSpellEvent(string eventName, object eventData)
	{
		FireSpellEventCallbacks(eventName, eventData);
	}

	public virtual void OnFsmStateStarted(FsmState state, SpellStateType stateType)
	{
		if (m_activeStateChange != stateType)
		{
			ChangeStateImpl(stateType);
		}
	}

	protected virtual void OnAttachPowerTaskList()
	{
		if (m_UseFastActorTriggers && m_taskList.IsStartOfBlock())
		{
			GameState.Get().SetUsingFastActorTriggers(enable: true);
		}
	}

	protected virtual void OnBirth(SpellStateType prevStateType)
	{
		UpdateTransform();
		FireStateStartedCallbacks(prevStateType);
	}

	protected virtual void OnIdle(SpellStateType prevStateType)
	{
		FireStateStartedCallbacks(prevStateType);
	}

	protected virtual void OnAction(SpellStateType prevStateType)
	{
		UpdateTransform();
		FireStateStartedCallbacks(prevStateType);
	}

	protected virtual void OnCancel(SpellStateType prevStateType)
	{
		FireStateStartedCallbacks(prevStateType);
	}

	protected virtual void OnDeath(SpellStateType prevStateType)
	{
		FireStateStartedCallbacks(prevStateType);
	}

	protected virtual void OnNone(SpellStateType prevStateType)
	{
		FireStateStartedCallbacks(prevStateType);
	}

	private void BuildSpellStateMap()
	{
		foreach (Transform item in base.transform)
		{
			SpellState component = item.gameObject.GetComponent<SpellState>();
			if (component == null)
			{
				continue;
			}
			SpellStateType stateType = component.m_StateType;
			if (stateType != 0)
			{
				if (m_spellStateMap == null)
				{
					m_spellStateMap = new Map<SpellStateType, List<SpellState>>();
				}
				if (!m_spellStateMap.TryGetValue(stateType, out var value))
				{
					value = new List<SpellState>();
					m_spellStateMap.Add(stateType, value);
				}
				value.Add(component);
			}
		}
	}

	private void BuildFsmStateMap()
	{
		if (m_fsm == null)
		{
			return;
		}
		List<FsmState> list = GenerateSpellFsmStateList();
		if (list.Count > 0)
		{
			m_fsmStateMap = new Map<SpellStateType, FsmState>();
		}
		Map<SpellStateType, int> map = new Map<SpellStateType, int>();
		foreach (SpellStateType value in Enum.GetValues(typeof(SpellStateType)))
		{
			map[value] = 0;
		}
		Map<SpellStateType, int> map2 = new Map<SpellStateType, int>();
		foreach (SpellStateType value2 in Enum.GetValues(typeof(SpellStateType)))
		{
			map2[value2] = 0;
		}
		FsmTransition[] fsmGlobalTransitions = m_fsm.FsmGlobalTransitions;
		foreach (FsmTransition fsmTransition in fsmGlobalTransitions)
		{
			SpellStateType @enum;
			try
			{
				@enum = EnumUtils.GetEnum<SpellStateType>(fsmTransition.EventName);
			}
			catch (ArgumentException)
			{
				continue;
			}
			map2[@enum]++;
			foreach (FsmState item in list)
			{
				if (fsmTransition.ToState.Equals(item.Name))
				{
					map[@enum]++;
					if (!m_fsmStateMap.ContainsKey(@enum))
					{
						m_fsmStateMap.Add(@enum, item);
					}
				}
			}
		}
		foreach (KeyValuePair<SpellStateType, int> item2 in map)
		{
			if (item2.Value > 1)
			{
				Debug.LogWarning($"{this}.BuildFsmStateMap() - Found {item2.Value} states for SpellStateType {item2.Key}. There should be 1.");
			}
		}
		foreach (KeyValuePair<SpellStateType, int> item3 in map2)
		{
			if (item3.Value > 1)
			{
				Debug.LogWarning($"{this}.BuildFsmStateMap() - Found {item3.Value} transitions for SpellStateType {item3.Key}. There should be 1.");
			}
			if (item3.Value > 0 && map[item3.Key] == 0)
			{
				Debug.LogWarning($"{this}.BuildFsmStateMap() - SpellStateType {item3.Key} is missing a SpellStateAction.");
			}
		}
		if (m_fsmStateMap != null && m_fsmStateMap.Values.Count == 0)
		{
			m_fsmStateMap = null;
		}
	}

	private List<FsmState> GenerateSpellFsmStateList()
	{
		List<FsmState> list = new List<FsmState>();
		FsmState[] fsmStates = m_fsm.FsmStates;
		foreach (FsmState fsmState in fsmStates)
		{
			SpellStateAction spellStateAction = null;
			int num = 0;
			for (int j = 0; j < fsmState.Actions.Length; j++)
			{
				SpellStateAction spellStateAction2 = fsmState.Actions[j] as SpellStateAction;
				if (spellStateAction2 != null)
				{
					num++;
					if (spellStateAction == null)
					{
						spellStateAction = spellStateAction2;
					}
				}
			}
			if (spellStateAction != null)
			{
				list.Add(fsmState);
			}
			if (num > 1)
			{
				Debug.LogWarning($"{this}.GenerateSpellFsmStateList() - State \"{fsmState.Name}\" has {num} SpellStateActions. There should be 1.");
			}
		}
		return list;
	}

	protected void ChangeStateImpl(SpellStateType stateType)
	{
		m_activeStateChange = stateType;
		SpellStateType activeStateType = m_activeStateType;
		m_activeStateType = stateType;
		if (stateType == SpellStateType.NONE)
		{
			FinishIfNecessary();
		}
		List<SpellState> value = null;
		if (m_spellStateMap != null)
		{
			m_spellStateMap.TryGetValue(stateType, out value);
		}
		if (activeStateType != 0)
		{
			if (m_spellStateMap != null && m_spellStateMap.TryGetValue(activeStateType, out var value2))
			{
				foreach (SpellState item in value2)
				{
					item.Stop(value);
				}
			}
			FireStateFinishedCallbacks(activeStateType);
		}
		else if (stateType != 0)
		{
			m_finished = false;
			OnExitedNoneState();
		}
		if (value != null)
		{
			foreach (SpellState item2 in value)
			{
				item2.Play();
			}
		}
		CallStateFunction(activeStateType, stateType);
		if (activeStateType != 0 && stateType == SpellStateType.NONE)
		{
			OnEnteredNoneState();
		}
	}

	protected void ChangeFsmState(SpellStateType stateType)
	{
		if (!(m_fsm == null))
		{
			if (!base.gameObject.activeInHierarchy)
			{
				Log.Spells.PrintWarning("Spell.ChangeFsmState() - WARNING gameObject {0} wants to go into state {1} but is inactive!", base.gameObject, stateType);
			}
			else
			{
				StartCoroutine(WaitThenChangeFsmState(stateType));
			}
		}
	}

	private IEnumerator WaitThenChangeFsmState(SpellStateType stateType)
	{
		while (!m_fsmReady)
		{
			yield return null;
		}
		if (m_activeStateType == stateType)
		{
			ChangeFsmStateNow(stateType);
		}
	}

	private void ChangeFsmStateNow(SpellStateType stateType)
	{
		if (m_fsmStateMap == null)
		{
			Debug.LogError($"Spell.ChangeFsmStateNow() - stateType {stateType} was requested but the m_fsmStateMap is null");
			return;
		}
		FsmState value = null;
		if (m_fsmStateMap.TryGetValue(stateType, out value))
		{
			m_fsm.SendEvent(EnumUtils.GetString(stateType));
		}
	}

	protected void FinishIfNecessary()
	{
		if (!m_finished)
		{
			OnSpellFinished();
		}
	}

	protected void CallStateFunction(SpellStateType prevStateType, SpellStateType stateType)
	{
		switch (stateType)
		{
		case SpellStateType.BIRTH:
			OnBirth(prevStateType);
			break;
		case SpellStateType.IDLE:
			OnIdle(prevStateType);
			break;
		case SpellStateType.ACTION:
			OnAction(prevStateType);
			break;
		case SpellStateType.CANCEL:
			OnCancel(prevStateType);
			break;
		case SpellStateType.DEATH:
			if (m_BlockPowerProcessing.m_OnEnterDeathState)
			{
				GameState.Get().AddServerBlockingSpell(this);
			}
			OnDeath(prevStateType);
			break;
		default:
			OnNone(prevStateType);
			break;
		}
	}

	protected void FireFinishedCallbacks()
	{
		FinishedListener[] array = m_finishedListeners.ToArray();
		m_finishedListeners.Clear();
		FinishedListener[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].Fire(this);
		}
	}

	protected void FireStateFinishedCallbacks(SpellStateType prevStateType)
	{
		StateFinishedListener[] array = m_stateFinishedListeners.ToArray();
		if (m_activeStateType == SpellStateType.NONE)
		{
			m_stateFinishedListeners.Clear();
		}
		StateFinishedListener[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].Fire(this, prevStateType);
		}
	}

	protected void FireStateStartedCallbacks(SpellStateType prevStateType)
	{
		StateStartedListener[] array = m_stateStartedListeners.ToArray();
		if (m_activeStateType == SpellStateType.NONE)
		{
			m_stateStartedListeners.Clear();
		}
		StateStartedListener[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].Fire(this, prevStateType);
		}
	}

	protected void FireSpellEventCallbacks(string eventName, object eventData)
	{
		SpellEventListener[] array = m_spellEventListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(eventName, eventData);
		}
	}

	protected bool HasStateContent(SpellStateType stateType)
	{
		if (m_spellStateMap != null && m_spellStateMap.ContainsKey(stateType))
		{
			return true;
		}
		if (!m_fsmReady)
		{
			if (m_fsm != null && m_fsm.Fsm.HasEvent(EnumUtils.GetString(stateType)))
			{
				return true;
			}
		}
		else if (m_fsmStateMap != null && m_fsmStateMap.ContainsKey(stateType))
		{
			return true;
		}
		return false;
	}

	protected bool HasOverriddenStateMethod(SpellStateType stateType)
	{
		string stateMethodName = GetStateMethodName(stateType);
		if (stateMethodName == null)
		{
			return false;
		}
		Type type = GetType();
		Type typeFromHandle = typeof(Spell);
		return GeneralUtils.IsOverriddenMethod(type, typeFromHandle, stateMethodName);
	}

	protected string GetStateMethodName(SpellStateType stateType)
	{
		return stateType switch
		{
			SpellStateType.BIRTH => "OnBirth", 
			SpellStateType.IDLE => "OnIdle", 
			SpellStateType.ACTION => "OnAction", 
			SpellStateType.CANCEL => "OnCancel", 
			SpellStateType.DEATH => "OnDeath", 
			_ => null, 
		};
	}

	protected bool CanAddPowerTargets()
	{
		return SpellUtils.CanAddPowerTargets(m_taskList);
	}

	protected bool AddSinglePowerTarget()
	{
		Card sourceCard = GetSourceCard();
		if (sourceCard == null)
		{
			Log.Power.PrintWarning("{0}.AddSinglePowerTarget() - a source card was never added", this);
			return false;
		}
		Network.HistBlockStart blockStart = m_taskList.GetBlockStart();
		if (blockStart == null)
		{
			Log.Power.PrintError("{0}.AddSinglePowerTarget() - got a task list with no block start", this);
			return false;
		}
		List<PowerTask> taskList = m_taskList.GetTaskList();
		if (AddSinglePowerTarget_FromBlockStart(blockStart))
		{
			return true;
		}
		if (AddSinglePowerTarget_FromMetaData(taskList))
		{
			return true;
		}
		if (AddSinglePowerTarget_FromAnyPower(sourceCard, taskList))
		{
			return true;
		}
		return false;
	}

	protected bool AddSinglePowerTarget_FromBlockStart(Network.HistBlockStart blockStart)
	{
		Entity entity = GameState.Get().GetEntity(blockStart.Target);
		if (entity == null)
		{
			return false;
		}
		Card card = entity.GetCard();
		if (card == null)
		{
			Log.Power.Print("{0}.AddSinglePowerTarget_FromSourceAction() - FAILED Target {1} in blockStart has no Card", this, blockStart.Target);
			return false;
		}
		AddTarget(card.gameObject);
		return true;
	}

	protected bool AddSinglePowerTarget_FromMetaData(List<PowerTask> tasks)
	{
		GameState gameState = GameState.Get();
		for (int i = 0; i < tasks.Count; i++)
		{
			Network.PowerHistory power = tasks[i].GetPower();
			if (power.Type != Network.PowerType.META_DATA)
			{
				continue;
			}
			Network.HistMetaData histMetaData = (Network.HistMetaData)power;
			if (histMetaData.MetaType != 0)
			{
				continue;
			}
			if (histMetaData.Info == null || histMetaData.Info.Count == 0)
			{
				Debug.LogError($"{this}.AddSinglePowerTarget_FromMetaData() - META_DATA at index {i} has no Info");
				continue;
			}
			for (int j = 0; j < histMetaData.Info.Count; j++)
			{
				Entity entity = gameState.GetEntity(histMetaData.Info[j]);
				if (entity == null)
				{
					Debug.LogError($"{this}.AddSinglePowerTarget_FromMetaData() - Entity is null for META_DATA at index {i} Info index {j}");
					continue;
				}
				Card card = entity.GetCard();
				AddTargetFromMetaData(i, card);
				return true;
			}
		}
		return false;
	}

	protected bool AddSinglePowerTarget_FromAnyPower(Card sourceCard, List<PowerTask> tasks)
	{
		for (int i = 0; i < tasks.Count; i++)
		{
			PowerTask task = tasks[i];
			Card targetCardFromPowerTask = GetTargetCardFromPowerTask(i, task);
			if (!(targetCardFromPowerTask == null) && !(sourceCard == targetCardFromPowerTask) && IsValidSpellTarget(targetCardFromPowerTask.GetEntity()))
			{
				AddTarget(targetCardFromPowerTask.gameObject);
				return true;
			}
		}
		return false;
	}

	protected bool AddMultiplePowerTargets()
	{
		Card sourceCard = GetSourceCard();
		List<PowerTask> taskList = m_taskList.GetTaskList();
		if (AddMultiplePowerTargets_FromMetaData(taskList) || m_ExclusivelyUseMetadataForTargeting)
		{
			return true;
		}
		AddMultiplePowerTargets_FromAnyPower(sourceCard, taskList);
		return true;
	}

	protected bool AddMultiplePowerTargets_FromMetaData(List<PowerTask> tasks)
	{
		int count = m_targets.Count;
		GameState gameState = GameState.Get();
		for (int i = 0; i < tasks.Count; i++)
		{
			Network.PowerHistory power = tasks[i].GetPower();
			if (power.Type != Network.PowerType.META_DATA)
			{
				continue;
			}
			Network.HistMetaData histMetaData = (Network.HistMetaData)power;
			if (histMetaData.MetaType != 0)
			{
				continue;
			}
			if (histMetaData.Info == null || histMetaData.Info.Count == 0)
			{
				Debug.LogError($"{this}.AddMultiplePowerTargets_FromMetaData() - META_DATA at index {i} has no Info");
				continue;
			}
			int data = histMetaData.Data;
			if (data != 0 && data != GetSourceCard().GetEntity().GetEntityId())
			{
				continue;
			}
			for (int j = 0; j < histMetaData.Info.Count; j++)
			{
				Entity entity = gameState.GetEntity(histMetaData.Info[j]);
				if (entity == null)
				{
					Debug.LogError($"{this}.AddMultiplePowerTargets_FromMetaData() - Entity is null for META_DATA at index {i} Info index {j}");
					continue;
				}
				Card card = entity.GetCard();
				AddTargetFromMetaData(i, card);
			}
		}
		return m_targets.Count != count;
	}

	protected void AddMultiplePowerTargets_FromAnyPower(Card sourceCard, List<PowerTask> tasks)
	{
		for (int i = 0; i < tasks.Count; i++)
		{
			PowerTask task = tasks[i];
			Card targetCardFromPowerTask = GetTargetCardFromPowerTask(i, task);
			if (!(targetCardFromPowerTask == null) && !(sourceCard == targetCardFromPowerTask) && !IsTarget(targetCardFromPowerTask.gameObject) && IsValidSpellTarget(targetCardFromPowerTask.GetEntity()))
			{
				AddTarget(targetCardFromPowerTask.gameObject);
			}
		}
	}

	protected virtual Card GetTargetCardFromPowerTask(int index, PowerTask task)
	{
		Network.PowerHistory power = task.GetPower();
		if (power.Type != Network.PowerType.TAG_CHANGE)
		{
			return null;
		}
		Network.HistTagChange histTagChange = power as Network.HistTagChange;
		Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
		if (entity == null)
		{
			Debug.LogWarning($"{this}.GetTargetCardFromPowerTask() - WARNING trying to target entity with id {histTagChange.Entity} but there is no entity with that id");
			return null;
		}
		return entity.GetCard();
	}

	protected virtual void AddTargetFromMetaData(int metaDataIndex, Card targetCard)
	{
		AddTarget(targetCard.gameObject);
	}

	protected bool CompleteMetaDataTasks(int metaDataIndex)
	{
		return CompleteMetaDataTasks(metaDataIndex, null, null);
	}

	protected bool CompleteMetaDataTasks(int metaDataIndex, PowerTaskList.CompleteCallback completeCallback)
	{
		return CompleteMetaDataTasks(metaDataIndex, completeCallback, null);
	}

	protected bool CompleteMetaDataTasks(int metaDataIndex, PowerTaskList.CompleteCallback completeCallback, object callbackData)
	{
		List<PowerTask> taskList = m_taskList.GetTaskList();
		int num = 1;
		for (int i = metaDataIndex + 1; i < taskList.Count; i++)
		{
			Network.PowerHistory power = taskList[i].GetPower();
			if (power.Type == Network.PowerType.META_DATA && ((Network.HistMetaData)power).MetaType == HistoryMeta.Type.TARGET)
			{
				break;
			}
			num++;
		}
		if (num == 0)
		{
			Debug.LogError($"{this}.CompleteMetaDataTasks() - there are no tasks to complete for meta data {metaDataIndex}");
			return false;
		}
		m_taskList.DoTasks(metaDataIndex, num, completeCallback, callbackData);
		return true;
	}

	protected virtual void ShowImpl()
	{
		List<SpellState> activeStateList = GetActiveStateList();
		if (activeStateList == null)
		{
			return;
		}
		foreach (SpellState item in activeStateList)
		{
			item.ShowState();
		}
	}

	protected virtual void HideImpl()
	{
		List<SpellState> activeStateList = GetActiveStateList();
		if (activeStateList == null)
		{
			return;
		}
		foreach (SpellState item in activeStateList)
		{
			item.HideState();
		}
	}

	protected void OnExitedNoneState()
	{
		if (DoesBlockServerEvents())
		{
			GameState.Get().AddServerBlockingSpell(this);
		}
		ActivateObjectContainer(enable: true);
		BlockZones(block: true);
		if (ZoneMgr.Get() != null)
		{
			ZoneMgr.Get().RequestNextDeathBlockLayoutDelaySec(m_ZoneLayoutDelayForDeaths);
		}
	}

	protected void OnEnteredNoneState()
	{
		if (GameState.Get() != null)
		{
			GameState.Get().RemoveServerBlockingSpell(this);
		}
		ActivateObjectContainer(enable: false);
	}

	protected void BlockZones(bool block)
	{
		if (m_ZonesToDisable == null)
		{
			return;
		}
		foreach (SpellZoneTag item in m_ZonesToDisable)
		{
			List<Zone> list = SpellUtils.FindZonesFromTag(item);
			if (list == null)
			{
				continue;
			}
			foreach (Zone item2 in list)
			{
				item2.BlockInput(block);
			}
		}
	}

	public void OnLoad()
	{
		foreach (Transform item in base.transform)
		{
			SpellState component = item.gameObject.GetComponent<SpellState>();
			if (!(component == null))
			{
				component.OnLoad();
			}
		}
	}
}
