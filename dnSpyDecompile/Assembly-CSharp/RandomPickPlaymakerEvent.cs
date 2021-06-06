using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A68 RID: 2664
public class RandomPickPlaymakerEvent : MonoBehaviour
{
	// Token: 0x06008F00 RID: 36608 RVA: 0x002E384C File Offset: 0x002E1A4C
	private void Awake()
	{
		this.m_Collider = base.GetComponent<Collider>();
		if (this.m_AwakeStateIndex > -1)
		{
			this.m_CurrentState = this.m_State[this.m_AwakeStateIndex];
			this.m_LastEventIndex = this.m_AwakeStateIndex;
			this.m_StateActive = true;
		}
	}

	// Token: 0x06008F01 RID: 36609 RVA: 0x002E3898 File Offset: 0x002E1A98
	public void RandomPickEvent()
	{
		if (!this.m_StartAnimationFinished || !this.m_EndAnimationFinished)
		{
			return;
		}
		if (this.m_StateActive && this.m_CurrentState.m_EndEvent != string.Empty && this.m_CurrentState.m_FSM != null)
		{
			this.m_CurrentState.m_FSM.SendEvent(this.m_CurrentState.m_EndEvent);
			this.m_EndAnimationFinished = false;
			this.m_StateActive = false;
			base.StartCoroutine(this.WaitForEndAnimation());
			return;
		}
		if (this.m_AlternateState.Count <= 0)
		{
			this.SendRandomEvent();
			return;
		}
		if (this.m_AlternateEventState)
		{
			this.SendRandomEvent();
			return;
		}
		this.SendAlternateRandomEvent();
	}

	// Token: 0x06008F02 RID: 36610 RVA: 0x002E3949 File Offset: 0x002E1B49
	public void StartAnimationFinished()
	{
		this.m_StartAnimationFinished = true;
	}

	// Token: 0x06008F03 RID: 36611 RVA: 0x002E3952 File Offset: 0x002E1B52
	public void EndAnimationFinished()
	{
		this.m_EndAnimationFinished = true;
	}

	// Token: 0x06008F04 RID: 36612 RVA: 0x002E395C File Offset: 0x002E1B5C
	private void SendRandomEvent()
	{
		this.m_StateActive = true;
		this.m_AlternateEventState = false;
		List<int> list = new List<int>();
		if (this.m_State.Count == 1)
		{
			list.Add(0);
		}
		else
		{
			for (int i = 0; i < this.m_State.Count; i++)
			{
				if (i != this.m_LastEventIndex)
				{
					list.Add(i);
				}
			}
		}
		int index = UnityEngine.Random.Range(0, list.Count);
		RandomPickPlaymakerEvent.PickEvent pickEvent = this.m_State[list[index]];
		this.m_CurrentState = pickEvent;
		this.m_LastEventIndex = list[index];
		this.m_StartAnimationFinished = false;
		base.StartCoroutine(this.WaitForStartAnimation());
		pickEvent.m_FSM.SendEvent(pickEvent.m_StartEvent);
	}

	// Token: 0x06008F05 RID: 36613 RVA: 0x002E3A14 File Offset: 0x002E1C14
	private void SendAlternateRandomEvent()
	{
		this.m_StateActive = true;
		this.m_AlternateEventState = true;
		List<int> list = new List<int>();
		if (this.m_AlternateState.Count == 1)
		{
			list.Add(0);
		}
		else
		{
			for (int i = 0; i < this.m_AlternateState.Count; i++)
			{
				if (i != this.m_LastAlternateIndex)
				{
					list.Add(i);
				}
			}
		}
		int index = UnityEngine.Random.Range(0, list.Count);
		RandomPickPlaymakerEvent.PickEvent pickEvent = this.m_AlternateState[list[index]];
		this.m_CurrentState = pickEvent;
		this.m_LastAlternateIndex = list[index];
		this.m_StartAnimationFinished = false;
		base.StartCoroutine(this.WaitForStartAnimation());
		pickEvent.m_FSM.SendEvent(pickEvent.m_StartEvent);
	}

	// Token: 0x06008F06 RID: 36614 RVA: 0x002E3ACB File Offset: 0x002E1CCB
	private IEnumerator WaitForStartAnimation()
	{
		while (!this.m_StartAnimationFinished)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06008F07 RID: 36615 RVA: 0x002E3ADA File Offset: 0x002E1CDA
	private IEnumerator WaitForEndAnimation()
	{
		while (!this.m_EndAnimationFinished)
		{
			yield return null;
		}
		this.m_CurrentState = null;
		if (!this.m_AllowNoneState)
		{
			while (!this.m_StartAnimationFinished)
			{
				yield return null;
			}
			this.RandomPickEvent();
		}
		yield break;
	}

	// Token: 0x06008F08 RID: 36616 RVA: 0x002E3AE9 File Offset: 0x002E1CE9
	private void EnableCollider()
	{
		if (this.m_Collider != null)
		{
			this.m_Collider.enabled = true;
		}
	}

	// Token: 0x06008F09 RID: 36617 RVA: 0x002E3B05 File Offset: 0x002E1D05
	private void DisableCollider()
	{
		if (this.m_Collider != null)
		{
			this.m_Collider.enabled = false;
		}
	}

	// Token: 0x0400777D RID: 30589
	public int m_AwakeStateIndex = -1;

	// Token: 0x0400777E RID: 30590
	public bool m_AllowNoneState = true;

	// Token: 0x0400777F RID: 30591
	public List<RandomPickPlaymakerEvent.PickEvent> m_State;

	// Token: 0x04007780 RID: 30592
	public List<RandomPickPlaymakerEvent.PickEvent> m_AlternateState;

	// Token: 0x04007781 RID: 30593
	private bool m_StateActive;

	// Token: 0x04007782 RID: 30594
	private RandomPickPlaymakerEvent.PickEvent m_CurrentState;

	// Token: 0x04007783 RID: 30595
	private Collider m_Collider;

	// Token: 0x04007784 RID: 30596
	private bool m_AlternateEventState;

	// Token: 0x04007785 RID: 30597
	private int m_LastEventIndex;

	// Token: 0x04007786 RID: 30598
	private int m_LastAlternateIndex;

	// Token: 0x04007787 RID: 30599
	private bool m_StartAnimationFinished = true;

	// Token: 0x04007788 RID: 30600
	private bool m_EndAnimationFinished = true;

	// Token: 0x020026BF RID: 9919
	[Serializable]
	public class PickEvent
	{
		// Token: 0x0400F1E6 RID: 61926
		public PlayMakerFSM m_FSM;

		// Token: 0x0400F1E7 RID: 61927
		public string m_StartEvent;

		// Token: 0x0400F1E8 RID: 61928
		public string m_EndEvent;

		// Token: 0x0400F1E9 RID: 61929
		[HideInInspector]
		public int m_CurrentItemIndex;
	}
}
