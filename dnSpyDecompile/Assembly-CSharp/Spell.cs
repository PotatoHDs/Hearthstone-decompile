using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Core;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using PegasusGame;
using UnityEngine;

// Token: 0x0200096B RID: 2411
public class Spell : MonoBehaviour
{
	// Token: 0x06008484 RID: 33924 RVA: 0x002AE1EC File Offset: 0x002AC3EC
	protected virtual void Awake()
	{
		this.BuildSpellStateMap();
		this.m_fsm = base.GetComponent<PlayMakerFSM>();
		if (!string.IsNullOrEmpty(this.m_LocationTransformName))
		{
			this.m_LocationTransformName = this.m_LocationTransformName.Trim();
		}
	}

	// Token: 0x06008485 RID: 33925 RVA: 0x002AE21E File Offset: 0x002AC41E
	protected virtual void OnDestroy()
	{
		if (this.m_ObjectContainer != null)
		{
			UnityEngine.Object.Destroy(this.m_ObjectContainer);
			this.m_ObjectContainer = null;
		}
		if (base.gameObject != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06008486 RID: 33926 RVA: 0x002AE259 File Offset: 0x002AC459
	protected virtual void Start()
	{
		if (this.m_activeStateType == SpellStateType.NONE)
		{
			this.ActivateObjectContainer(false);
			return;
		}
		if (this.m_shown)
		{
			this.ShowImpl();
			return;
		}
		this.HideImpl();
	}

	// Token: 0x06008487 RID: 33927 RVA: 0x002AE280 File Offset: 0x002AC480
	private void Update()
	{
		if (this.m_fsmReady)
		{
			return;
		}
		if (this.m_fsm == null)
		{
			this.m_fsmReady = true;
			return;
		}
		if (!this.m_fsmSkippedFirstFrame)
		{
			this.m_fsmSkippedFirstFrame = true;
			return;
		}
		if (!this.m_fsm.enabled)
		{
			return;
		}
		this.BuildFsmStateMap();
		this.m_fsmReady = true;
	}

	// Token: 0x06008488 RID: 33928 RVA: 0x002AE2D7 File Offset: 0x002AC4D7
	public SpellType GetSpellType()
	{
		return this.m_spellType;
	}

	// Token: 0x06008489 RID: 33929 RVA: 0x002AE2DF File Offset: 0x002AC4DF
	public void SetSpellType(SpellType spellType)
	{
		this.m_spellType = spellType;
	}

	// Token: 0x0600848A RID: 33930 RVA: 0x002AE2E8 File Offset: 0x002AC4E8
	public bool DoesBlockServerEvents()
	{
		return GameState.Get() != null && this.m_BlockServerEvents;
	}

	// Token: 0x0600848B RID: 33931 RVA: 0x002AE2F9 File Offset: 0x002AC4F9
	public SuperSpell GetSuperSpellParent()
	{
		if (base.transform.parent == null)
		{
			return null;
		}
		return base.transform.parent.GetComponent<SuperSpell>();
	}

	// Token: 0x0600848C RID: 33932 RVA: 0x002AE320 File Offset: 0x002AC520
	public PowerTaskList GetPowerTaskList()
	{
		return this.m_taskList;
	}

	// Token: 0x0600848D RID: 33933 RVA: 0x002AE328 File Offset: 0x002AC528
	public global::Entity GetPowerSource()
	{
		if (this.m_taskList == null)
		{
			return null;
		}
		return this.m_taskList.GetSourceEntity(true);
	}

	// Token: 0x0600848E RID: 33934 RVA: 0x002AE340 File Offset: 0x002AC540
	public Card GetPowerSourceCard()
	{
		global::Entity powerSource = this.GetPowerSource();
		if (powerSource != null)
		{
			return powerSource.GetCard();
		}
		return null;
	}

	// Token: 0x0600848F RID: 33935 RVA: 0x002AE35F File Offset: 0x002AC55F
	public global::Entity GetPowerTarget()
	{
		if (this.m_taskList == null)
		{
			return null;
		}
		return this.m_taskList.GetTargetEntity(true);
	}

	// Token: 0x06008490 RID: 33936 RVA: 0x002AE378 File Offset: 0x002AC578
	public Card GetPowerTargetCard()
	{
		global::Entity powerTarget = this.GetPowerTarget();
		if (powerTarget != null)
		{
			return powerTarget.GetCard();
		}
		return null;
	}

	// Token: 0x06008491 RID: 33937 RVA: 0x002AE397 File Offset: 0x002AC597
	public virtual bool CanPurge()
	{
		return !this.IsActive();
	}

	// Token: 0x06008492 RID: 33938 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool ShouldReconnectIfStuck()
	{
		return true;
	}

	// Token: 0x06008493 RID: 33939 RVA: 0x002AE3A4 File Offset: 0x002AC5A4
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

	// Token: 0x06008494 RID: 33940 RVA: 0x002AE3E9 File Offset: 0x002AC5E9
	public SpellLocation GetLocation()
	{
		return this.m_Location;
	}

	// Token: 0x06008495 RID: 33941 RVA: 0x002AE3F1 File Offset: 0x002AC5F1
	public string GetLocationTransformName()
	{
		return this.m_LocationTransformName;
	}

	// Token: 0x06008496 RID: 33942 RVA: 0x002AE3F9 File Offset: 0x002AC5F9
	public SpellFacing GetFacing()
	{
		return this.m_Facing;
	}

	// Token: 0x06008497 RID: 33943 RVA: 0x002AE401 File Offset: 0x002AC601
	public SpellFacingOptions GetFacingOptions()
	{
		return this.m_FacingOptions;
	}

	// Token: 0x06008498 RID: 33944 RVA: 0x002AE409 File Offset: 0x002AC609
	public void ClearPositionDirtyFlag()
	{
		this.m_positionDirty = false;
	}

	// Token: 0x06008499 RID: 33945 RVA: 0x002AE412 File Offset: 0x002AC612
	public void SetPosition(Vector3 position)
	{
		base.transform.position = position;
		this.m_positionDirty = false;
	}

	// Token: 0x0600849A RID: 33946 RVA: 0x002AE427 File Offset: 0x002AC627
	public void SetLocalPosition(Vector3 position)
	{
		base.transform.localPosition = position;
		this.m_positionDirty = false;
	}

	// Token: 0x0600849B RID: 33947 RVA: 0x002AE43C File Offset: 0x002AC63C
	public void SetOrientation(Quaternion orientation)
	{
		base.transform.rotation = orientation;
		this.m_orientationDirty = false;
	}

	// Token: 0x0600849C RID: 33948 RVA: 0x002AE451 File Offset: 0x002AC651
	public void SetLocalOrientation(Quaternion orientation)
	{
		base.transform.localRotation = orientation;
		this.m_orientationDirty = false;
	}

	// Token: 0x0600849D RID: 33949 RVA: 0x002AE466 File Offset: 0x002AC666
	public void ForceUpdateTransform()
	{
		this.m_positionDirty = true;
		this.UpdateTransform();
	}

	// Token: 0x0600849E RID: 33950 RVA: 0x002AE475 File Offset: 0x002AC675
	public void UpdateTransform()
	{
		this.UpdatePosition();
		this.UpdateOrientation();
	}

	// Token: 0x0600849F RID: 33951 RVA: 0x002AE483 File Offset: 0x002AC683
	public void UpdatePosition()
	{
		if (!this.m_positionDirty)
		{
			return;
		}
		SpellUtils.SetPositionFromLocation(this, this.m_SetParentToLocation);
		this.m_positionDirty = false;
	}

	// Token: 0x060084A0 RID: 33952 RVA: 0x002AE4A2 File Offset: 0x002AC6A2
	public void UpdateOrientation()
	{
		if (!this.m_orientationDirty)
		{
			return;
		}
		SpellUtils.SetOrientationFromFacing(this);
		this.m_orientationDirty = false;
	}

	// Token: 0x060084A1 RID: 33953 RVA: 0x002AE4BB File Offset: 0x002AC6BB
	public GameObject GetSource()
	{
		return this.m_source;
	}

	// Token: 0x060084A2 RID: 33954 RVA: 0x002AE4C3 File Offset: 0x002AC6C3
	public virtual void SetSource(GameObject go)
	{
		this.m_source = go;
	}

	// Token: 0x060084A3 RID: 33955 RVA: 0x002AE4CC File Offset: 0x002AC6CC
	public virtual void RemoveSource()
	{
		this.m_source = null;
	}

	// Token: 0x060084A4 RID: 33956 RVA: 0x002AE4D5 File Offset: 0x002AC6D5
	public bool IsSource(GameObject go)
	{
		return this.m_source == go;
	}

	// Token: 0x060084A5 RID: 33957 RVA: 0x002AE4E3 File Offset: 0x002AC6E3
	public Card GetSourceCard()
	{
		if (this.m_source == null)
		{
			return null;
		}
		return this.m_source.GetComponent<Card>();
	}

	// Token: 0x060084A6 RID: 33958 RVA: 0x002AE500 File Offset: 0x002AC700
	public List<GameObject> GetTargets()
	{
		return this.m_targets;
	}

	// Token: 0x060084A7 RID: 33959 RVA: 0x002AE508 File Offset: 0x002AC708
	public GameObject GetTarget()
	{
		if (this.m_targets.Count != 0)
		{
			return this.m_targets[0];
		}
		return null;
	}

	// Token: 0x060084A8 RID: 33960 RVA: 0x002AE525 File Offset: 0x002AC725
	public virtual void AddTarget(GameObject go)
	{
		this.m_targets.Add(go);
	}

	// Token: 0x060084A9 RID: 33961 RVA: 0x002AE533 File Offset: 0x002AC733
	public virtual void AddTargets(List<GameObject> targets)
	{
		this.m_targets.AddRange(targets);
	}

	// Token: 0x060084AA RID: 33962 RVA: 0x002AE541 File Offset: 0x002AC741
	public virtual bool RemoveTarget(GameObject go)
	{
		return this.m_targets.Remove(go);
	}

	// Token: 0x060084AB RID: 33963 RVA: 0x002AE54F File Offset: 0x002AC74F
	public virtual void RemoveAllTargets()
	{
		this.m_targets.Clear();
	}

	// Token: 0x060084AC RID: 33964 RVA: 0x002AE55C File Offset: 0x002AC75C
	public bool IsTarget(GameObject go)
	{
		return this.m_targets.Contains(go);
	}

	// Token: 0x060084AD RID: 33965 RVA: 0x002AE56C File Offset: 0x002AC76C
	public Card GetTargetCard()
	{
		GameObject target = this.GetTarget();
		if (target == null)
		{
			return null;
		}
		return target.GetComponent<Card>();
	}

	// Token: 0x060084AE RID: 33966 RVA: 0x002AE591 File Offset: 0x002AC791
	public virtual List<GameObject> GetVisualTargets()
	{
		return this.GetTargets();
	}

	// Token: 0x060084AF RID: 33967 RVA: 0x002AE599 File Offset: 0x002AC799
	public virtual GameObject GetVisualTarget()
	{
		return this.GetTarget();
	}

	// Token: 0x060084B0 RID: 33968 RVA: 0x002AE5A1 File Offset: 0x002AC7A1
	public virtual void AddVisualTarget(GameObject go)
	{
		this.AddTarget(go);
	}

	// Token: 0x060084B1 RID: 33969 RVA: 0x002AE5AA File Offset: 0x002AC7AA
	public virtual void AddVisualTargets(List<GameObject> targets)
	{
		this.AddTargets(targets);
	}

	// Token: 0x060084B2 RID: 33970 RVA: 0x002AE5B3 File Offset: 0x002AC7B3
	public virtual bool RemoveVisualTarget(GameObject go)
	{
		return this.RemoveTarget(go);
	}

	// Token: 0x060084B3 RID: 33971 RVA: 0x002AE5BC File Offset: 0x002AC7BC
	public virtual void RemoveAllVisualTargets()
	{
		this.RemoveAllTargets();
	}

	// Token: 0x060084B4 RID: 33972 RVA: 0x002AE5C4 File Offset: 0x002AC7C4
	public virtual bool IsVisualTarget(GameObject go)
	{
		return this.IsTarget(go);
	}

	// Token: 0x060084B5 RID: 33973 RVA: 0x002AE5CD File Offset: 0x002AC7CD
	public virtual Card GetVisualTargetCard()
	{
		return this.GetTargetCard();
	}

	// Token: 0x060084B6 RID: 33974 RVA: 0x002AE5D5 File Offset: 0x002AC7D5
	public bool IsValidSpellTarget(global::Entity ent)
	{
		return !ent.IsEnchantment();
	}

	// Token: 0x060084B7 RID: 33975 RVA: 0x002AE5E0 File Offset: 0x002AC7E0
	public bool IsShown()
	{
		return this.m_shown;
	}

	// Token: 0x060084B8 RID: 33976 RVA: 0x002AE5E8 File Offset: 0x002AC7E8
	public void Show()
	{
		if (this.m_shown)
		{
			return;
		}
		this.m_shown = true;
		if (this.m_activeStateType != SpellStateType.NONE)
		{
			this.OnExitedNoneState();
		}
		this.ShowImpl();
	}

	// Token: 0x060084B9 RID: 33977 RVA: 0x002AE60E File Offset: 0x002AC80E
	public void Hide()
	{
		if (!this.m_shown)
		{
			return;
		}
		this.m_shown = false;
		this.HideImpl();
		if (this.m_activeStateType != SpellStateType.NONE)
		{
			this.OnEnteredNoneState();
		}
	}

	// Token: 0x060084BA RID: 33978 RVA: 0x002AE634 File Offset: 0x002AC834
	public void ActivateObjectContainer(bool enable)
	{
		if (this.m_ObjectContainer == null)
		{
			return;
		}
		SceneUtils.EnableRenderers(this.m_ObjectContainer, enable);
	}

	// Token: 0x060084BB RID: 33979 RVA: 0x002AE651 File Offset: 0x002AC851
	public bool IsActive()
	{
		return this.m_activeStateType > SpellStateType.NONE;
	}

	// Token: 0x060084BC RID: 33980 RVA: 0x002AE65C File Offset: 0x002AC85C
	public void Activate()
	{
		SpellStateType spellStateType = this.GuessNextStateType();
		if (spellStateType == SpellStateType.NONE)
		{
			this.Deactivate();
			return;
		}
		this.ChangeState(spellStateType);
	}

	// Token: 0x060084BD RID: 33981 RVA: 0x002AE684 File Offset: 0x002AC884
	public void Reactivate()
	{
		SpellStateType spellStateType = this.GuessNextStateType(SpellStateType.NONE);
		if (spellStateType == SpellStateType.NONE)
		{
			this.Deactivate();
			return;
		}
		this.ChangeState(spellStateType);
	}

	// Token: 0x060084BE RID: 33982 RVA: 0x002AE6AA File Offset: 0x002AC8AA
	public void Deactivate()
	{
		if (this.m_activeStateType == SpellStateType.NONE)
		{
			return;
		}
		this.ForceDeactivate();
	}

	// Token: 0x060084BF RID: 33983 RVA: 0x002AE6BB File Offset: 0x002AC8BB
	public void ForceDeactivate()
	{
		this.ChangeState(SpellStateType.NONE);
	}

	// Token: 0x060084C0 RID: 33984 RVA: 0x002AE6C4 File Offset: 0x002AC8C4
	public void ActivateState(SpellStateType stateType)
	{
		if (!this.HasUsableState(stateType))
		{
			this.Deactivate();
			return;
		}
		this.ChangeState(stateType);
	}

	// Token: 0x060084C1 RID: 33985 RVA: 0x002AE6DD File Offset: 0x002AC8DD
	public void SafeActivateState(SpellStateType stateType)
	{
		if (!this.HasUsableState(stateType))
		{
			this.ForceDeactivate();
			return;
		}
		this.ChangeState(stateType);
	}

	// Token: 0x060084C2 RID: 33986 RVA: 0x002AE6F6 File Offset: 0x002AC8F6
	public virtual bool HasUsableState(SpellStateType stateType)
	{
		return stateType != SpellStateType.NONE && (this.HasStateContent(stateType) || this.HasOverriddenStateMethod(stateType) || (this.m_activeStateType == SpellStateType.NONE && this.m_ZonesToDisable != null && this.m_ZonesToDisable.Count > 0));
	}

	// Token: 0x060084C3 RID: 33987 RVA: 0x002AE734 File Offset: 0x002AC934
	public SpellStateType GetActiveState()
	{
		return this.m_activeStateType;
	}

	// Token: 0x060084C4 RID: 33988 RVA: 0x002AE73C File Offset: 0x002AC93C
	public SpellState GetFirstSpellState(SpellStateType stateType)
	{
		if (this.m_spellStateMap == null)
		{
			return null;
		}
		List<SpellState> list = null;
		if (!this.m_spellStateMap.TryGetValue(stateType, out list))
		{
			return null;
		}
		if (list.Count == 0)
		{
			return null;
		}
		return list[0];
	}

	// Token: 0x060084C5 RID: 33989 RVA: 0x002AE778 File Offset: 0x002AC978
	public List<SpellState> GetActiveStateList()
	{
		if (this.m_spellStateMap == null)
		{
			return null;
		}
		List<SpellState> result = null;
		if (!this.m_spellStateMap.TryGetValue(this.m_activeStateType, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x060084C6 RID: 33990 RVA: 0x002AE7A9 File Offset: 0x002AC9A9
	public bool IsFinished()
	{
		return this.m_finished;
	}

	// Token: 0x060084C7 RID: 33991 RVA: 0x002AE7B1 File Offset: 0x002AC9B1
	public void AddFinishedCallback(Spell.FinishedCallback callback)
	{
		this.AddFinishedCallback(callback, null);
	}

	// Token: 0x060084C8 RID: 33992 RVA: 0x002AE7BC File Offset: 0x002AC9BC
	public void AddFinishedCallback(Spell.FinishedCallback callback, object userData)
	{
		Spell.FinishedListener finishedListener = new Spell.FinishedListener();
		finishedListener.SetCallback(callback);
		finishedListener.SetUserData(userData);
		if (this.m_finishedListeners.Contains(finishedListener))
		{
			return;
		}
		this.m_finishedListeners.Add(finishedListener);
	}

	// Token: 0x060084C9 RID: 33993 RVA: 0x002AE7F8 File Offset: 0x002AC9F8
	public bool RemoveFinishedCallback(Spell.FinishedCallback callback)
	{
		return this.RemoveFinishedCallback(callback, null);
	}

	// Token: 0x060084CA RID: 33994 RVA: 0x002AE804 File Offset: 0x002ACA04
	public bool RemoveFinishedCallback(Spell.FinishedCallback callback, object userData)
	{
		Spell.FinishedListener finishedListener = new Spell.FinishedListener();
		finishedListener.SetCallback(callback);
		finishedListener.SetUserData(userData);
		return this.m_finishedListeners.Remove(finishedListener);
	}

	// Token: 0x060084CB RID: 33995 RVA: 0x002AE831 File Offset: 0x002ACA31
	public void AddStateFinishedCallback(Spell.StateFinishedCallback callback)
	{
		this.AddStateFinishedCallback(callback, null);
	}

	// Token: 0x060084CC RID: 33996 RVA: 0x002AE83C File Offset: 0x002ACA3C
	public void AddStateFinishedCallback(Spell.StateFinishedCallback callback, object userData)
	{
		Spell.StateFinishedListener stateFinishedListener = new Spell.StateFinishedListener();
		stateFinishedListener.SetCallback(callback);
		stateFinishedListener.SetUserData(userData);
		if (this.m_stateFinishedListeners.Contains(stateFinishedListener))
		{
			return;
		}
		this.m_stateFinishedListeners.Add(stateFinishedListener);
	}

	// Token: 0x060084CD RID: 33997 RVA: 0x002AE878 File Offset: 0x002ACA78
	public bool RemoveStateFinishedCallback(Spell.StateFinishedCallback callback)
	{
		return this.RemoveStateFinishedCallback(callback, null);
	}

	// Token: 0x060084CE RID: 33998 RVA: 0x002AE884 File Offset: 0x002ACA84
	public bool RemoveStateFinishedCallback(Spell.StateFinishedCallback callback, object userData)
	{
		Spell.StateFinishedListener stateFinishedListener = new Spell.StateFinishedListener();
		stateFinishedListener.SetCallback(callback);
		stateFinishedListener.SetUserData(userData);
		return this.m_stateFinishedListeners.Remove(stateFinishedListener);
	}

	// Token: 0x060084CF RID: 33999 RVA: 0x002AE8B1 File Offset: 0x002ACAB1
	public void AddStateStartedCallback(Spell.StateStartedCallback callback)
	{
		this.AddStateStartedCallback(callback, null);
	}

	// Token: 0x060084D0 RID: 34000 RVA: 0x002AE8BC File Offset: 0x002ACABC
	public void AddStateStartedCallback(Spell.StateStartedCallback callback, object userData)
	{
		Spell.StateStartedListener stateStartedListener = new Spell.StateStartedListener();
		stateStartedListener.SetCallback(callback);
		stateStartedListener.SetUserData(userData);
		if (this.m_stateStartedListeners.Contains(stateStartedListener))
		{
			return;
		}
		this.m_stateStartedListeners.Add(stateStartedListener);
	}

	// Token: 0x060084D1 RID: 34001 RVA: 0x002AE8F8 File Offset: 0x002ACAF8
	public bool RemoveStateStartedCallback(Spell.StateStartedCallback callback)
	{
		return this.RemoveStateStartedCallback(callback, null);
	}

	// Token: 0x060084D2 RID: 34002 RVA: 0x002AE904 File Offset: 0x002ACB04
	public bool RemoveStateStartedCallback(Spell.StateStartedCallback callback, object userData)
	{
		Spell.StateStartedListener stateStartedListener = new Spell.StateStartedListener();
		stateStartedListener.SetCallback(callback);
		stateStartedListener.SetUserData(userData);
		return this.m_stateStartedListeners.Remove(stateStartedListener);
	}

	// Token: 0x060084D3 RID: 34003 RVA: 0x002AE931 File Offset: 0x002ACB31
	public void AddSpellEventCallback(Spell.SpellEventCallback callback)
	{
		this.AddSpellEventCallback(callback, null);
	}

	// Token: 0x060084D4 RID: 34004 RVA: 0x002AE93C File Offset: 0x002ACB3C
	public void AddSpellEventCallback(Spell.SpellEventCallback callback, object userData)
	{
		Spell.SpellEventListener spellEventListener = new Spell.SpellEventListener();
		spellEventListener.SetCallback(callback);
		spellEventListener.SetUserData(userData);
		if (this.m_spellEventListeners.Contains(spellEventListener))
		{
			return;
		}
		this.m_spellEventListeners.Add(spellEventListener);
	}

	// Token: 0x060084D5 RID: 34005 RVA: 0x002AE978 File Offset: 0x002ACB78
	public bool RemoveSpellEventCallback(Spell.SpellEventCallback callback)
	{
		return this.RemoveSpellEventCallback(callback, null);
	}

	// Token: 0x060084D6 RID: 34006 RVA: 0x002AE984 File Offset: 0x002ACB84
	public bool RemoveSpellEventCallback(Spell.SpellEventCallback callback, object userData)
	{
		Spell.SpellEventListener spellEventListener = new Spell.SpellEventListener();
		spellEventListener.SetCallback(callback);
		spellEventListener.SetUserData(userData);
		return this.m_spellEventListeners.Remove(spellEventListener);
	}

	// Token: 0x060084D7 RID: 34007 RVA: 0x002AE9B1 File Offset: 0x002ACBB1
	public virtual void ChangeState(SpellStateType stateType)
	{
		this.ChangeStateImpl(stateType);
		if (this.m_activeStateType != stateType)
		{
			return;
		}
		this.ChangeFsmState(stateType);
	}

	// Token: 0x060084D8 RID: 34008 RVA: 0x002AE9CB File Offset: 0x002ACBCB
	public SpellStateType GuessNextStateType()
	{
		return this.GuessNextStateType(this.m_activeStateType);
	}

	// Token: 0x060084D9 RID: 34009 RVA: 0x002AE9DC File Offset: 0x002ACBDC
	public SpellStateType GuessNextStateType(SpellStateType stateType)
	{
		switch (stateType)
		{
		case SpellStateType.NONE:
			if (this.HasUsableState(SpellStateType.BIRTH))
			{
				return SpellStateType.BIRTH;
			}
			if (this.HasUsableState(SpellStateType.IDLE))
			{
				return SpellStateType.IDLE;
			}
			if (this.HasUsableState(SpellStateType.ACTION))
			{
				return SpellStateType.ACTION;
			}
			if (this.HasUsableState(SpellStateType.DEATH))
			{
				return SpellStateType.DEATH;
			}
			if (this.HasUsableState(SpellStateType.CANCEL))
			{
				return SpellStateType.CANCEL;
			}
			break;
		case SpellStateType.BIRTH:
			if (this.HasUsableState(SpellStateType.IDLE))
			{
				return SpellStateType.IDLE;
			}
			break;
		case SpellStateType.IDLE:
			if (this.HasUsableState(SpellStateType.ACTION))
			{
				return SpellStateType.ACTION;
			}
			break;
		case SpellStateType.ACTION:
			if (this.HasUsableState(SpellStateType.DEATH))
			{
				return SpellStateType.DEATH;
			}
			break;
		}
		return SpellStateType.NONE;
	}

	// Token: 0x060084DA RID: 34010 RVA: 0x002AEA5C File Offset: 0x002ACC5C
	public virtual bool AttachPowerTaskList(PowerTaskList taskList)
	{
		PowerTaskList taskList2 = this.m_taskList;
		this.m_taskList = taskList;
		this.RemoveAllTargets();
		if (!this.AddPowerTargets())
		{
			this.m_taskList = taskList2;
			return false;
		}
		this.OnAttachPowerTaskList();
		return true;
	}

	// Token: 0x060084DB RID: 34011 RVA: 0x002AEA95 File Offset: 0x002ACC95
	public virtual bool AddPowerTargets()
	{
		return this.CanAddPowerTargets() && this.AddMultiplePowerTargets();
	}

	// Token: 0x060084DC RID: 34012 RVA: 0x002AEAA8 File Offset: 0x002ACCA8
	public PowerTaskList GetLastHandledTaskList(PowerTaskList taskList)
	{
		if (taskList == null)
		{
			return null;
		}
		Spell spell = UnityEngine.Object.Instantiate<Spell>(this);
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

	// Token: 0x060084DD RID: 34013 RVA: 0x002AEAF6 File Offset: 0x002ACCF6
	public bool IsHandlingLastTaskList()
	{
		return this.GetLastHandledTaskList(this.m_taskList) == this.m_taskList;
	}

	// Token: 0x060084DE RID: 34014 RVA: 0x002AEB0C File Offset: 0x002ACD0C
	public virtual void OnStateFinished()
	{
		SpellStateType stateType = this.GuessNextStateType();
		this.ChangeState(stateType);
	}

	// Token: 0x060084DF RID: 34015 RVA: 0x002AEB28 File Offset: 0x002ACD28
	public virtual void OnSpellFinished()
	{
		this.m_finished = true;
		this.m_positionDirty = true;
		this.m_orientationDirty = true;
		if (GameState.Get() != null)
		{
			GameState.Get().RemoveServerBlockingSpell(this);
		}
		this.BlockZones(false);
		if (this.m_UseFastActorTriggers && GameState.Get() != null && this.IsHandlingLastTaskList())
		{
			GameState.Get().SetUsingFastActorTriggers(false);
		}
		this.FireFinishedCallbacks();
	}

	// Token: 0x060084E0 RID: 34016 RVA: 0x002AEB8C File Offset: 0x002ACD8C
	public virtual void OnSpellEvent(string eventName, object eventData)
	{
		this.FireSpellEventCallbacks(eventName, eventData);
	}

	// Token: 0x060084E1 RID: 34017 RVA: 0x002AEB96 File Offset: 0x002ACD96
	public virtual void OnFsmStateStarted(FsmState state, SpellStateType stateType)
	{
		if (this.m_activeStateChange == stateType)
		{
			return;
		}
		this.ChangeStateImpl(stateType);
	}

	// Token: 0x060084E2 RID: 34018 RVA: 0x002AEBA9 File Offset: 0x002ACDA9
	protected virtual void OnAttachPowerTaskList()
	{
		if (this.m_UseFastActorTriggers && this.m_taskList.IsStartOfBlock())
		{
			GameState.Get().SetUsingFastActorTriggers(true);
		}
	}

	// Token: 0x060084E3 RID: 34019 RVA: 0x002AEBCB File Offset: 0x002ACDCB
	protected virtual void OnBirth(SpellStateType prevStateType)
	{
		this.UpdateTransform();
		this.FireStateStartedCallbacks(prevStateType);
	}

	// Token: 0x060084E4 RID: 34020 RVA: 0x002AEBDA File Offset: 0x002ACDDA
	protected virtual void OnIdle(SpellStateType prevStateType)
	{
		this.FireStateStartedCallbacks(prevStateType);
	}

	// Token: 0x060084E5 RID: 34021 RVA: 0x002AEBCB File Offset: 0x002ACDCB
	protected virtual void OnAction(SpellStateType prevStateType)
	{
		this.UpdateTransform();
		this.FireStateStartedCallbacks(prevStateType);
	}

	// Token: 0x060084E6 RID: 34022 RVA: 0x002AEBDA File Offset: 0x002ACDDA
	protected virtual void OnCancel(SpellStateType prevStateType)
	{
		this.FireStateStartedCallbacks(prevStateType);
	}

	// Token: 0x060084E7 RID: 34023 RVA: 0x002AEBDA File Offset: 0x002ACDDA
	protected virtual void OnDeath(SpellStateType prevStateType)
	{
		this.FireStateStartedCallbacks(prevStateType);
	}

	// Token: 0x060084E8 RID: 34024 RVA: 0x002AEBDA File Offset: 0x002ACDDA
	protected virtual void OnNone(SpellStateType prevStateType)
	{
		this.FireStateStartedCallbacks(prevStateType);
	}

	// Token: 0x060084E9 RID: 34025 RVA: 0x002AEBE4 File Offset: 0x002ACDE4
	private void BuildSpellStateMap()
	{
		foreach (object obj in base.transform)
		{
			SpellState component = ((Transform)obj).gameObject.GetComponent<SpellState>();
			if (!(component == null))
			{
				SpellStateType stateType = component.m_StateType;
				if (stateType != SpellStateType.NONE)
				{
					if (this.m_spellStateMap == null)
					{
						this.m_spellStateMap = new Map<SpellStateType, List<SpellState>>();
					}
					List<SpellState> list;
					if (!this.m_spellStateMap.TryGetValue(stateType, out list))
					{
						list = new List<SpellState>();
						this.m_spellStateMap.Add(stateType, list);
					}
					list.Add(component);
				}
			}
		}
	}

	// Token: 0x060084EA RID: 34026 RVA: 0x002AEC94 File Offset: 0x002ACE94
	private void BuildFsmStateMap()
	{
		if (this.m_fsm == null)
		{
			return;
		}
		List<FsmState> list = this.GenerateSpellFsmStateList();
		if (list.Count > 0)
		{
			this.m_fsmStateMap = new Map<SpellStateType, FsmState>();
		}
		Map<SpellStateType, int> map = new Map<SpellStateType, int>();
		foreach (object obj in Enum.GetValues(typeof(SpellStateType)))
		{
			SpellStateType key = (SpellStateType)obj;
			map[key] = 0;
		}
		Map<SpellStateType, int> map2 = new Map<SpellStateType, int>();
		foreach (object obj2 in Enum.GetValues(typeof(SpellStateType)))
		{
			SpellStateType key2 = (SpellStateType)obj2;
			map2[key2] = 0;
		}
		FsmTransition[] fsmGlobalTransitions = this.m_fsm.FsmGlobalTransitions;
		int i = 0;
		while (i < fsmGlobalTransitions.Length)
		{
			FsmTransition fsmTransition = fsmGlobalTransitions[i];
			SpellStateType @enum;
			try
			{
				@enum = EnumUtils.GetEnum<SpellStateType>(fsmTransition.EventName);
			}
			catch (ArgumentException)
			{
				goto IL_191;
			}
			goto IL_FE;
			IL_191:
			i++;
			continue;
			IL_FE:
			Map<SpellStateType, int> map3 = map2;
			SpellStateType key3 = @enum;
			int value = map3[key3] + 1;
			map3[key3] = value;
			foreach (FsmState fsmState in list)
			{
				if (fsmTransition.ToState.Equals(fsmState.Name))
				{
					Map<SpellStateType, int> map4 = map;
					key3 = @enum;
					value = map4[key3] + 1;
					map4[key3] = value;
					if (!this.m_fsmStateMap.ContainsKey(@enum))
					{
						this.m_fsmStateMap.Add(@enum, fsmState);
					}
				}
			}
			goto IL_191;
		}
		foreach (KeyValuePair<SpellStateType, int> keyValuePair in map)
		{
			if (keyValuePair.Value > 1)
			{
				Debug.LogWarning(string.Format("{0}.BuildFsmStateMap() - Found {1} states for SpellStateType {2}. There should be 1.", this, keyValuePair.Value, keyValuePair.Key));
			}
		}
		foreach (KeyValuePair<SpellStateType, int> keyValuePair2 in map2)
		{
			if (keyValuePair2.Value > 1)
			{
				Debug.LogWarning(string.Format("{0}.BuildFsmStateMap() - Found {1} transitions for SpellStateType {2}. There should be 1.", this, keyValuePair2.Value, keyValuePair2.Key));
			}
			if (keyValuePair2.Value > 0 && map[keyValuePair2.Key] == 0)
			{
				Debug.LogWarning(string.Format("{0}.BuildFsmStateMap() - SpellStateType {1} is missing a SpellStateAction.", this, keyValuePair2.Key));
			}
		}
		if (this.m_fsmStateMap != null && this.m_fsmStateMap.Values.Count == 0)
		{
			this.m_fsmStateMap = null;
		}
	}

	// Token: 0x060084EB RID: 34027 RVA: 0x002AEFA4 File Offset: 0x002AD1A4
	private List<FsmState> GenerateSpellFsmStateList()
	{
		List<FsmState> list = new List<FsmState>();
		foreach (FsmState fsmState in this.m_fsm.FsmStates)
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
				Debug.LogWarning(string.Format("{0}.GenerateSpellFsmStateList() - State \"{1}\" has {2} SpellStateActions. There should be 1.", this, fsmState.Name, num));
			}
		}
		return list;
	}

	// Token: 0x060084EC RID: 34028 RVA: 0x002AF044 File Offset: 0x002AD244
	protected void ChangeStateImpl(SpellStateType stateType)
	{
		this.m_activeStateChange = stateType;
		SpellStateType activeStateType = this.m_activeStateType;
		this.m_activeStateType = stateType;
		if (stateType == SpellStateType.NONE)
		{
			this.FinishIfNecessary();
		}
		List<SpellState> list = null;
		if (this.m_spellStateMap != null)
		{
			this.m_spellStateMap.TryGetValue(stateType, out list);
		}
		if (activeStateType != SpellStateType.NONE)
		{
			List<SpellState> list2;
			if (this.m_spellStateMap != null && this.m_spellStateMap.TryGetValue(activeStateType, out list2))
			{
				foreach (SpellState spellState in list2)
				{
					spellState.Stop(list);
				}
			}
			this.FireStateFinishedCallbacks(activeStateType);
		}
		else if (stateType != SpellStateType.NONE)
		{
			this.m_finished = false;
			this.OnExitedNoneState();
		}
		if (list != null)
		{
			foreach (SpellState spellState2 in list)
			{
				spellState2.Play();
			}
		}
		this.CallStateFunction(activeStateType, stateType);
		if (activeStateType != SpellStateType.NONE && stateType == SpellStateType.NONE)
		{
			this.OnEnteredNoneState();
		}
	}

	// Token: 0x060084ED RID: 34029 RVA: 0x002AF14C File Offset: 0x002AD34C
	protected void ChangeFsmState(SpellStateType stateType)
	{
		if (this.m_fsm == null)
		{
			return;
		}
		if (!base.gameObject.activeInHierarchy)
		{
			Log.Spells.PrintWarning("Spell.ChangeFsmState() - WARNING gameObject {0} wants to go into state {1} but is inactive!", new object[]
			{
				base.gameObject,
				stateType
			});
			return;
		}
		base.StartCoroutine(this.WaitThenChangeFsmState(stateType));
	}

	// Token: 0x060084EE RID: 34030 RVA: 0x002AF1AB File Offset: 0x002AD3AB
	private IEnumerator WaitThenChangeFsmState(SpellStateType stateType)
	{
		while (!this.m_fsmReady)
		{
			yield return null;
		}
		if (this.m_activeStateType != stateType)
		{
			yield break;
		}
		this.ChangeFsmStateNow(stateType);
		yield break;
	}

	// Token: 0x060084EF RID: 34031 RVA: 0x002AF1C4 File Offset: 0x002AD3C4
	private void ChangeFsmStateNow(SpellStateType stateType)
	{
		if (this.m_fsmStateMap == null)
		{
			Debug.LogError(string.Format("Spell.ChangeFsmStateNow() - stateType {0} was requested but the m_fsmStateMap is null", stateType));
			return;
		}
		FsmState fsmState = null;
		if (!this.m_fsmStateMap.TryGetValue(stateType, out fsmState))
		{
			return;
		}
		this.m_fsm.SendEvent(EnumUtils.GetString<SpellStateType>(stateType));
	}

	// Token: 0x060084F0 RID: 34032 RVA: 0x002AF213 File Offset: 0x002AD413
	protected void FinishIfNecessary()
	{
		if (this.m_finished)
		{
			return;
		}
		this.OnSpellFinished();
	}

	// Token: 0x060084F1 RID: 34033 RVA: 0x002AF224 File Offset: 0x002AD424
	protected void CallStateFunction(SpellStateType prevStateType, SpellStateType stateType)
	{
		switch (stateType)
		{
		case SpellStateType.BIRTH:
			this.OnBirth(prevStateType);
			return;
		case SpellStateType.IDLE:
			this.OnIdle(prevStateType);
			return;
		case SpellStateType.ACTION:
			this.OnAction(prevStateType);
			return;
		case SpellStateType.CANCEL:
			this.OnCancel(prevStateType);
			return;
		case SpellStateType.DEATH:
			if (this.m_BlockPowerProcessing.m_OnEnterDeathState)
			{
				GameState.Get().AddServerBlockingSpell(this);
			}
			this.OnDeath(prevStateType);
			return;
		default:
			this.OnNone(prevStateType);
			return;
		}
	}

	// Token: 0x060084F2 RID: 34034 RVA: 0x002AF298 File Offset: 0x002AD498
	protected void FireFinishedCallbacks()
	{
		Spell.FinishedListener[] array = this.m_finishedListeners.ToArray();
		this.m_finishedListeners.Clear();
		Spell.FinishedListener[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].Fire(this);
		}
	}

	// Token: 0x060084F3 RID: 34035 RVA: 0x002AF2D4 File Offset: 0x002AD4D4
	protected void FireStateFinishedCallbacks(SpellStateType prevStateType)
	{
		Spell.StateFinishedListener[] array = this.m_stateFinishedListeners.ToArray();
		if (this.m_activeStateType == SpellStateType.NONE)
		{
			this.m_stateFinishedListeners.Clear();
		}
		Spell.StateFinishedListener[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].Fire(this, prevStateType);
		}
	}

	// Token: 0x060084F4 RID: 34036 RVA: 0x002AF318 File Offset: 0x002AD518
	protected void FireStateStartedCallbacks(SpellStateType prevStateType)
	{
		Spell.StateStartedListener[] array = this.m_stateStartedListeners.ToArray();
		if (this.m_activeStateType == SpellStateType.NONE)
		{
			this.m_stateStartedListeners.Clear();
		}
		Spell.StateStartedListener[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].Fire(this, prevStateType);
		}
	}

	// Token: 0x060084F5 RID: 34037 RVA: 0x002AF35C File Offset: 0x002AD55C
	protected void FireSpellEventCallbacks(string eventName, object eventData)
	{
		Spell.SpellEventListener[] array = this.m_spellEventListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(eventName, eventData);
		}
	}

	// Token: 0x060084F6 RID: 34038 RVA: 0x002AF390 File Offset: 0x002AD590
	protected bool HasStateContent(SpellStateType stateType)
	{
		if (this.m_spellStateMap != null && this.m_spellStateMap.ContainsKey(stateType))
		{
			return true;
		}
		if (!this.m_fsmReady)
		{
			if (this.m_fsm != null && this.m_fsm.Fsm.HasEvent(EnumUtils.GetString<SpellStateType>(stateType)))
			{
				return true;
			}
		}
		else if (this.m_fsmStateMap != null && this.m_fsmStateMap.ContainsKey(stateType))
		{
			return true;
		}
		return false;
	}

	// Token: 0x060084F7 RID: 34039 RVA: 0x002AF400 File Offset: 0x002AD600
	protected bool HasOverriddenStateMethod(SpellStateType stateType)
	{
		string stateMethodName = this.GetStateMethodName(stateType);
		if (stateMethodName == null)
		{
			return false;
		}
		Type type = base.GetType();
		Type typeFromHandle = typeof(Spell);
		return GeneralUtils.IsOverriddenMethod(type, typeFromHandle, stateMethodName);
	}

	// Token: 0x060084F8 RID: 34040 RVA: 0x002AF432 File Offset: 0x002AD632
	protected string GetStateMethodName(SpellStateType stateType)
	{
		switch (stateType)
		{
		case SpellStateType.BIRTH:
			return "OnBirth";
		case SpellStateType.IDLE:
			return "OnIdle";
		case SpellStateType.ACTION:
			return "OnAction";
		case SpellStateType.CANCEL:
			return "OnCancel";
		case SpellStateType.DEATH:
			return "OnDeath";
		default:
			return null;
		}
	}

	// Token: 0x060084F9 RID: 34041 RVA: 0x002AF471 File Offset: 0x002AD671
	protected bool CanAddPowerTargets()
	{
		return SpellUtils.CanAddPowerTargets(this.m_taskList);
	}

	// Token: 0x060084FA RID: 34042 RVA: 0x002AF480 File Offset: 0x002AD680
	protected bool AddSinglePowerTarget()
	{
		Card sourceCard = this.GetSourceCard();
		if (sourceCard == null)
		{
			Log.Power.PrintWarning("{0}.AddSinglePowerTarget() - a source card was never added", new object[]
			{
				this
			});
			return false;
		}
		Network.HistBlockStart blockStart = this.m_taskList.GetBlockStart();
		if (blockStart == null)
		{
			Log.Power.PrintError("{0}.AddSinglePowerTarget() - got a task list with no block start", new object[]
			{
				this
			});
			return false;
		}
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		return this.AddSinglePowerTarget_FromBlockStart(blockStart) || this.AddSinglePowerTarget_FromMetaData(taskList) || this.AddSinglePowerTarget_FromAnyPower(sourceCard, taskList);
	}

	// Token: 0x060084FB RID: 34043 RVA: 0x002AF514 File Offset: 0x002AD714
	protected bool AddSinglePowerTarget_FromBlockStart(Network.HistBlockStart blockStart)
	{
		global::Entity entity = GameState.Get().GetEntity(blockStart.Target);
		if (entity == null)
		{
			return false;
		}
		Card card = entity.GetCard();
		if (card == null)
		{
			Log.Power.Print("{0}.AddSinglePowerTarget_FromSourceAction() - FAILED Target {1} in blockStart has no Card", new object[]
			{
				this,
				blockStart.Target
			});
			return false;
		}
		this.AddTarget(card.gameObject);
		return true;
	}

	// Token: 0x060084FC RID: 34044 RVA: 0x002AF580 File Offset: 0x002AD780
	protected bool AddSinglePowerTarget_FromMetaData(List<PowerTask> tasks)
	{
		GameState gameState = GameState.Get();
		for (int i = 0; i < tasks.Count; i++)
		{
			Network.PowerHistory power = tasks[i].GetPower();
			if (power.Type == Network.PowerType.META_DATA)
			{
				Network.HistMetaData histMetaData = (Network.HistMetaData)power;
				if (histMetaData.MetaType == HistoryMeta.Type.TARGET)
				{
					if (histMetaData.Info == null || histMetaData.Info.Count == 0)
					{
						Debug.LogError(string.Format("{0}.AddSinglePowerTarget_FromMetaData() - META_DATA at index {1} has no Info", this, i));
					}
					else
					{
						for (int j = 0; j < histMetaData.Info.Count; j++)
						{
							global::Entity entity = gameState.GetEntity(histMetaData.Info[j]);
							if (entity != null)
							{
								Card card = entity.GetCard();
								this.AddTargetFromMetaData(i, card);
								return true;
							}
							Debug.LogError(string.Format("{0}.AddSinglePowerTarget_FromMetaData() - Entity is null for META_DATA at index {1} Info index {2}", this, i, j));
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060084FD RID: 34045 RVA: 0x002AF66C File Offset: 0x002AD86C
	protected bool AddSinglePowerTarget_FromAnyPower(Card sourceCard, List<PowerTask> tasks)
	{
		for (int i = 0; i < tasks.Count; i++)
		{
			PowerTask task = tasks[i];
			Card targetCardFromPowerTask = this.GetTargetCardFromPowerTask(i, task);
			if (!(targetCardFromPowerTask == null) && !(sourceCard == targetCardFromPowerTask) && this.IsValidSpellTarget(targetCardFromPowerTask.GetEntity()))
			{
				this.AddTarget(targetCardFromPowerTask.gameObject);
				return true;
			}
		}
		return false;
	}

	// Token: 0x060084FE RID: 34046 RVA: 0x002AF6CC File Offset: 0x002AD8CC
	protected bool AddMultiplePowerTargets()
	{
		Card sourceCard = this.GetSourceCard();
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		if (this.AddMultiplePowerTargets_FromMetaData(taskList) || this.m_ExclusivelyUseMetadataForTargeting)
		{
			return true;
		}
		this.AddMultiplePowerTargets_FromAnyPower(sourceCard, taskList);
		return true;
	}

	// Token: 0x060084FF RID: 34047 RVA: 0x002AF708 File Offset: 0x002AD908
	protected bool AddMultiplePowerTargets_FromMetaData(List<PowerTask> tasks)
	{
		int count = this.m_targets.Count;
		GameState gameState = GameState.Get();
		for (int i = 0; i < tasks.Count; i++)
		{
			Network.PowerHistory power = tasks[i].GetPower();
			if (power.Type == Network.PowerType.META_DATA)
			{
				Network.HistMetaData histMetaData = (Network.HistMetaData)power;
				if (histMetaData.MetaType == HistoryMeta.Type.TARGET)
				{
					if (histMetaData.Info == null || histMetaData.Info.Count == 0)
					{
						Debug.LogError(string.Format("{0}.AddMultiplePowerTargets_FromMetaData() - META_DATA at index {1} has no Info", this, i));
					}
					else
					{
						int data = histMetaData.Data;
						if (data == 0 || data == this.GetSourceCard().GetEntity().GetEntityId())
						{
							for (int j = 0; j < histMetaData.Info.Count; j++)
							{
								global::Entity entity = gameState.GetEntity(histMetaData.Info[j]);
								if (entity == null)
								{
									Debug.LogError(string.Format("{0}.AddMultiplePowerTargets_FromMetaData() - Entity is null for META_DATA at index {1} Info index {2}", this, i, j));
								}
								else
								{
									Card card = entity.GetCard();
									this.AddTargetFromMetaData(i, card);
								}
							}
						}
					}
				}
			}
		}
		return this.m_targets.Count != count;
	}

	// Token: 0x06008500 RID: 34048 RVA: 0x002AF838 File Offset: 0x002ADA38
	protected void AddMultiplePowerTargets_FromAnyPower(Card sourceCard, List<PowerTask> tasks)
	{
		for (int i = 0; i < tasks.Count; i++)
		{
			PowerTask task = tasks[i];
			Card targetCardFromPowerTask = this.GetTargetCardFromPowerTask(i, task);
			if (!(targetCardFromPowerTask == null) && !(sourceCard == targetCardFromPowerTask) && !this.IsTarget(targetCardFromPowerTask.gameObject) && this.IsValidSpellTarget(targetCardFromPowerTask.GetEntity()))
			{
				this.AddTarget(targetCardFromPowerTask.gameObject);
			}
		}
	}

	// Token: 0x06008501 RID: 34049 RVA: 0x002AF8A4 File Offset: 0x002ADAA4
	protected virtual Card GetTargetCardFromPowerTask(int index, PowerTask task)
	{
		Network.PowerHistory power = task.GetPower();
		if (power.Type != Network.PowerType.TAG_CHANGE)
		{
			return null;
		}
		Network.HistTagChange histTagChange = power as Network.HistTagChange;
		global::Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
		if (entity == null)
		{
			Debug.LogWarning(string.Format("{0}.GetTargetCardFromPowerTask() - WARNING trying to target entity with id {1} but there is no entity with that id", this, histTagChange.Entity));
			return null;
		}
		return entity.GetCard();
	}

	// Token: 0x06008502 RID: 34050 RVA: 0x002AF901 File Offset: 0x002ADB01
	protected virtual void AddTargetFromMetaData(int metaDataIndex, Card targetCard)
	{
		this.AddTarget(targetCard.gameObject);
	}

	// Token: 0x06008503 RID: 34051 RVA: 0x002AF90F File Offset: 0x002ADB0F
	protected bool CompleteMetaDataTasks(int metaDataIndex)
	{
		return this.CompleteMetaDataTasks(metaDataIndex, null, null);
	}

	// Token: 0x06008504 RID: 34052 RVA: 0x002AF91A File Offset: 0x002ADB1A
	protected bool CompleteMetaDataTasks(int metaDataIndex, PowerTaskList.CompleteCallback completeCallback)
	{
		return this.CompleteMetaDataTasks(metaDataIndex, completeCallback, null);
	}

	// Token: 0x06008505 RID: 34053 RVA: 0x002AF928 File Offset: 0x002ADB28
	protected bool CompleteMetaDataTasks(int metaDataIndex, PowerTaskList.CompleteCallback completeCallback, object callbackData)
	{
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
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
			Debug.LogError(string.Format("{0}.CompleteMetaDataTasks() - there are no tasks to complete for meta data {1}", this, metaDataIndex));
			return false;
		}
		this.m_taskList.DoTasks(metaDataIndex, num, completeCallback, callbackData);
		return true;
	}

	// Token: 0x06008506 RID: 34054 RVA: 0x002AF9A8 File Offset: 0x002ADBA8
	protected virtual void ShowImpl()
	{
		List<SpellState> activeStateList = this.GetActiveStateList();
		if (activeStateList != null)
		{
			foreach (SpellState spellState in activeStateList)
			{
				spellState.ShowState();
			}
		}
	}

	// Token: 0x06008507 RID: 34055 RVA: 0x002AFA00 File Offset: 0x002ADC00
	protected virtual void HideImpl()
	{
		List<SpellState> activeStateList = this.GetActiveStateList();
		if (activeStateList != null)
		{
			foreach (SpellState spellState in activeStateList)
			{
				spellState.HideState();
			}
		}
	}

	// Token: 0x06008508 RID: 34056 RVA: 0x002AFA58 File Offset: 0x002ADC58
	protected void OnExitedNoneState()
	{
		if (this.DoesBlockServerEvents())
		{
			GameState.Get().AddServerBlockingSpell(this);
		}
		this.ActivateObjectContainer(true);
		this.BlockZones(true);
		if (ZoneMgr.Get() != null)
		{
			ZoneMgr.Get().RequestNextDeathBlockLayoutDelaySec(this.m_ZoneLayoutDelayForDeaths);
		}
	}

	// Token: 0x06008509 RID: 34057 RVA: 0x002AFA98 File Offset: 0x002ADC98
	protected void OnEnteredNoneState()
	{
		if (GameState.Get() != null)
		{
			GameState.Get().RemoveServerBlockingSpell(this);
		}
		this.ActivateObjectContainer(false);
	}

	// Token: 0x0600850A RID: 34058 RVA: 0x002AFAB4 File Offset: 0x002ADCB4
	protected void BlockZones(bool block)
	{
		if (this.m_ZonesToDisable == null)
		{
			return;
		}
		foreach (SpellZoneTag zoneTag in this.m_ZonesToDisable)
		{
			List<Zone> list = SpellUtils.FindZonesFromTag(zoneTag);
			if (list != null)
			{
				foreach (Zone zone in list)
				{
					zone.BlockInput(block);
				}
			}
		}
	}

	// Token: 0x0600850B RID: 34059 RVA: 0x002AFB4C File Offset: 0x002ADD4C
	public void OnLoad()
	{
		foreach (object obj in base.transform)
		{
			SpellState component = ((Transform)obj).gameObject.GetComponent<SpellState>();
			if (!(component == null))
			{
				component.OnLoad();
			}
		}
	}

	// Token: 0x04006F85 RID: 28549
	[UnityEngine.Tooltip("If checked, this spell will block power history processing when the spell leaves the None state.")]
	public bool m_BlockServerEvents;

	// Token: 0x04006F86 RID: 28550
	[UnityEngine.Tooltip("Additional configuration on when this spell should block power history processing")]
	public PowerProcessorBlockingBehavior m_BlockPowerProcessing;

	// Token: 0x04006F87 RID: 28551
	public GameObject m_ObjectContainer;

	// Token: 0x04006F88 RID: 28552
	public SpellLocation m_Location = SpellLocation.SOURCE_AUTO;

	// Token: 0x04006F89 RID: 28553
	public string m_LocationTransformName;

	// Token: 0x04006F8A RID: 28554
	public bool m_SetParentToLocation;

	// Token: 0x04006F8B RID: 28555
	public SpellFacing m_Facing;

	// Token: 0x04006F8C RID: 28556
	public SpellFacingOptions m_FacingOptions;

	// Token: 0x04006F8D RID: 28557
	public TARGET_RETICLE_TYPE m_TargetReticle;

	// Token: 0x04006F8E RID: 28558
	public List<SpellZoneTag> m_ZonesToDisable;

	// Token: 0x04006F8F RID: 28559
	[UnityEngine.Tooltip("Delay (in seconds) to wait before sorting a zone after processing entity death. This is often used in CustomDeath spells, in order to wait for the custom death animation to play through before sorting the Play zone.")]
	public float m_ZoneLayoutDelayForDeaths;

	// Token: 0x04006F90 RID: 28560
	public bool m_UseFastActorTriggers;

	// Token: 0x04006F91 RID: 28561
	public bool m_ExclusivelyUseMetadataForTargeting;

	// Token: 0x04006F92 RID: 28562
	protected SpellType m_spellType;

	// Token: 0x04006F93 RID: 28563
	private Map<SpellStateType, List<SpellState>> m_spellStateMap;

	// Token: 0x04006F94 RID: 28564
	protected SpellStateType m_activeStateType;

	// Token: 0x04006F95 RID: 28565
	protected SpellStateType m_activeStateChange;

	// Token: 0x04006F96 RID: 28566
	private List<Spell.FinishedListener> m_finishedListeners = new List<Spell.FinishedListener>();

	// Token: 0x04006F97 RID: 28567
	private List<Spell.StateFinishedListener> m_stateFinishedListeners = new List<Spell.StateFinishedListener>();

	// Token: 0x04006F98 RID: 28568
	private List<Spell.StateStartedListener> m_stateStartedListeners = new List<Spell.StateStartedListener>();

	// Token: 0x04006F99 RID: 28569
	private List<Spell.SpellEventListener> m_spellEventListeners = new List<Spell.SpellEventListener>();

	// Token: 0x04006F9A RID: 28570
	protected GameObject m_source;

	// Token: 0x04006F9B RID: 28571
	protected List<GameObject> m_targets = new List<GameObject>();

	// Token: 0x04006F9C RID: 28572
	protected PowerTaskList m_taskList;

	// Token: 0x04006F9D RID: 28573
	protected bool m_shown = true;

	// Token: 0x04006F9E RID: 28574
	protected PlayMakerFSM m_fsm;

	// Token: 0x04006F9F RID: 28575
	private Map<SpellStateType, FsmState> m_fsmStateMap;

	// Token: 0x04006FA0 RID: 28576
	private bool m_fsmSkippedFirstFrame;

	// Token: 0x04006FA1 RID: 28577
	private bool m_fsmReady;

	// Token: 0x04006FA2 RID: 28578
	protected bool m_positionDirty = true;

	// Token: 0x04006FA3 RID: 28579
	protected bool m_orientationDirty = true;

	// Token: 0x04006FA4 RID: 28580
	protected bool m_finished;

	// Token: 0x0200262F RID: 9775
	// (Invoke) Token: 0x06013612 RID: 79378
	public delegate void FinishedCallback(Spell spell, object userData);

	// Token: 0x02002630 RID: 9776
	// (Invoke) Token: 0x06013616 RID: 79382
	public delegate void StateFinishedCallback(Spell spell, SpellStateType prevStateType, object userData);

	// Token: 0x02002631 RID: 9777
	// (Invoke) Token: 0x0601361A RID: 79386
	public delegate void StateStartedCallback(Spell spell, SpellStateType prevStateType, object userData);

	// Token: 0x02002632 RID: 9778
	// (Invoke) Token: 0x0601361E RID: 79390
	public delegate void SpellEventCallback(string eventName, object eventData, object userData);

	// Token: 0x02002633 RID: 9779
	private class FinishedListener : EventListener<Spell.FinishedCallback>
	{
		// Token: 0x06013621 RID: 79393 RVA: 0x005323B6 File Offset: 0x005305B6
		public void Fire(Spell spell)
		{
			this.m_callback(spell, this.m_userData);
		}
	}

	// Token: 0x02002634 RID: 9780
	private class StateFinishedListener : EventListener<Spell.StateFinishedCallback>
	{
		// Token: 0x06013623 RID: 79395 RVA: 0x005323D2 File Offset: 0x005305D2
		public void Fire(Spell spell, SpellStateType prevStateType)
		{
			this.m_callback(spell, prevStateType, this.m_userData);
		}
	}

	// Token: 0x02002635 RID: 9781
	private class StateStartedListener : EventListener<Spell.StateStartedCallback>
	{
		// Token: 0x06013625 RID: 79397 RVA: 0x005323EF File Offset: 0x005305EF
		public void Fire(Spell spell, SpellStateType prevStateType)
		{
			this.m_callback(spell, prevStateType, this.m_userData);
		}
	}

	// Token: 0x02002636 RID: 9782
	private class SpellEventListener : EventListener<Spell.SpellEventCallback>
	{
		// Token: 0x06013627 RID: 79399 RVA: 0x0053240C File Offset: 0x0053060C
		public void Fire(string eventName, object eventData)
		{
			this.m_callback(eventName, eventData, this.m_userData);
		}
	}
}
