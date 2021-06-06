using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000966 RID: 2406
public class ActorStateMgr : MonoBehaviour
{
	// Token: 0x0600844B RID: 33867 RVA: 0x002ACFA4 File Offset: 0x002AB1A4
	private void Start()
	{
		this.m_HighlightState = this.FindHightlightObject();
		if (this.m_HighlightState != null)
		{
			this.m_initialHighlightRenderQueue = this.m_HighlightState.m_RenderQueue;
		}
		this.BuildStateMap();
		if (this.m_activeStateType == ActorStateType.NONE)
		{
			this.HideImpl();
			return;
		}
		if (this.m_shown)
		{
			this.ShowImpl();
			return;
		}
		this.HideImpl();
	}

	// Token: 0x0600844C RID: 33868 RVA: 0x002AD006 File Offset: 0x002AB206
	public Map<ActorStateType, List<ActorState>> GetStateMap()
	{
		return this.m_actorStateMap;
	}

	// Token: 0x0600844D RID: 33869 RVA: 0x002AD00E File Offset: 0x002AB20E
	public ActorStateType GetActiveStateType()
	{
		return this.m_activeStateType;
	}

	// Token: 0x0600844E RID: 33870 RVA: 0x002AD018 File Offset: 0x002AB218
	public List<ActorState> GetActiveStateList()
	{
		List<ActorState> result = null;
		if (!this.m_actorStateMap.TryGetValue(this.m_activeStateType, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x0600844F RID: 33871 RVA: 0x002AD040 File Offset: 0x002AB240
	public float GetMaximumAnimationTimeOfActiveStates()
	{
		if (this.GetActiveStateList() == null)
		{
			return 0f;
		}
		float num = 0f;
		foreach (ActorState actorState in this.GetActiveStateList())
		{
			num = Mathf.Max(actorState.GetAnimationDuration(), num);
		}
		return num;
	}

	// Token: 0x06008450 RID: 33872 RVA: 0x002AD0AC File Offset: 0x002AB2AC
	public bool ChangeState(ActorStateType stateType)
	{
		return this.ChangeState_NewState(stateType) || this.ChangeState_LegacyState(stateType);
	}

	// Token: 0x06008451 RID: 33873 RVA: 0x002AD0C5 File Offset: 0x002AB2C5
	public bool ChangeState_NewState(ActorStateType stateType)
	{
		if (!this.m_HighlightState)
		{
			return false;
		}
		ActorStateType activeStateType = this.m_activeStateType;
		this.m_activeStateType = stateType;
		return activeStateType == stateType || this.m_HighlightState.ChangeState(stateType);
	}

	// Token: 0x06008452 RID: 33874 RVA: 0x002AD0F4 File Offset: 0x002AB2F4
	public bool ChangeState_LegacyState(ActorStateType stateType)
	{
		List<ActorState> list = null;
		this.m_actorStateMap.TryGetValue(stateType, out list);
		ActorStateType activeStateType = this.m_activeStateType;
		this.m_activeStateType = stateType;
		if (activeStateType != ActorStateType.NONE)
		{
			List<ActorState> list2;
			if (!this.m_actorStateMap.TryGetValue(activeStateType, out list2))
			{
				goto IL_7E;
			}
			using (List<ActorState>.Enumerator enumerator = list2.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ActorState actorState = enumerator.Current;
					actorState.Stop(list);
				}
				goto IL_7E;
			}
		}
		if (stateType != ActorStateType.NONE && this.m_ObjectContainer != null)
		{
			this.m_ObjectContainer.SetActive(true);
		}
		IL_7E:
		if (stateType == ActorStateType.NONE)
		{
			if (activeStateType != ActorStateType.NONE && this.m_ObjectContainer != null)
			{
				this.m_ObjectContainer.SetActive(false);
			}
			return true;
		}
		if (list != null)
		{
			foreach (ActorState actorState2 in list)
			{
				actorState2.Play();
			}
			return true;
		}
		return false;
	}

	// Token: 0x06008453 RID: 33875 RVA: 0x002AD1F4 File Offset: 0x002AB3F4
	public void ShowStateMgr()
	{
		if (this.m_shown)
		{
			return;
		}
		this.m_shown = true;
		this.ShowImpl();
	}

	// Token: 0x06008454 RID: 33876 RVA: 0x002AD20C File Offset: 0x002AB40C
	public void HideStateMgr()
	{
		if (!this.m_shown)
		{
			return;
		}
		this.m_shown = false;
		this.HideImpl();
	}

	// Token: 0x06008455 RID: 33877 RVA: 0x002AD224 File Offset: 0x002AB424
	public void RefreshStateMgr()
	{
		if (this.m_HighlightState)
		{
			this.m_HighlightState.ContinuousUpdate(0.1f);
		}
	}

	// Token: 0x06008456 RID: 33878 RVA: 0x002AD243 File Offset: 0x002AB443
	public bool SetStateRenderQueue(bool reset, int renderQueue)
	{
		if (this.m_HighlightState == null)
		{
			return false;
		}
		this.m_HighlightState.m_RenderQueue = (reset ? this.m_initialHighlightRenderQueue : renderQueue);
		return true;
	}

	// Token: 0x06008457 RID: 33879 RVA: 0x002AD270 File Offset: 0x002AB470
	private HighlightState FindHightlightObject()
	{
		foreach (object obj in base.transform)
		{
			HighlightState component = ((Transform)obj).gameObject.GetComponent<HighlightState>();
			if (component)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x06008458 RID: 33880 RVA: 0x002AD2DC File Offset: 0x002AB4DC
	private void BuildStateMap()
	{
		foreach (object obj in base.transform)
		{
			ActorState component = ((Transform)obj).gameObject.GetComponent<ActorState>();
			if (!(component == null))
			{
				ActorStateType stateType = component.m_StateType;
				if (stateType != ActorStateType.NONE)
				{
					List<ActorState> list;
					if (!this.m_actorStateMap.TryGetValue(stateType, out list))
					{
						list = new List<ActorState>();
						this.m_actorStateMap.Add(stateType, list);
					}
					list.Add(component);
				}
			}
		}
	}

	// Token: 0x06008459 RID: 33881 RVA: 0x002AD378 File Offset: 0x002AB578
	private void ShowImpl()
	{
		if (this.m_HighlightState)
		{
			this.m_HighlightState.ChangeState(this.m_activeStateType);
		}
		if (this.m_activeStateType != ActorStateType.NONE && this.m_ObjectContainer != null)
		{
			this.m_ObjectContainer.SetActive(true);
		}
		List<ActorState> activeStateList = this.GetActiveStateList();
		if (activeStateList != null)
		{
			foreach (ActorState actorState in activeStateList)
			{
				actorState.ShowState();
			}
		}
	}

	// Token: 0x0600845A RID: 33882 RVA: 0x002AD410 File Offset: 0x002AB610
	private void HideImpl()
	{
		if (this.m_HighlightState)
		{
			this.m_HighlightState.ChangeState(ActorStateType.NONE);
		}
		List<ActorState> activeStateList = this.GetActiveStateList();
		if (activeStateList != null)
		{
			foreach (ActorState actorState in activeStateList)
			{
				actorState.HideState();
			}
		}
		if (this.m_activeStateType != ActorStateType.NONE && this.m_ObjectContainer != null)
		{
			this.m_ObjectContainer.SetActive(false);
		}
	}

	// Token: 0x04006F63 RID: 28515
	public GameObject m_ObjectContainer;

	// Token: 0x04006F64 RID: 28516
	private Map<ActorStateType, List<ActorState>> m_actorStateMap = new Map<ActorStateType, List<ActorState>>();

	// Token: 0x04006F65 RID: 28517
	private ActorStateType m_activeStateType;

	// Token: 0x04006F66 RID: 28518
	private bool m_shown = true;

	// Token: 0x04006F67 RID: 28519
	private HighlightState m_HighlightState;

	// Token: 0x04006F68 RID: 28520
	private int m_initialHighlightRenderQueue;
}
