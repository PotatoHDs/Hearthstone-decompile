using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPickPlaymakerEvent : MonoBehaviour
{
	[Serializable]
	public class PickEvent
	{
		public PlayMakerFSM m_FSM;

		public string m_StartEvent;

		public string m_EndEvent;

		[HideInInspector]
		public int m_CurrentItemIndex;
	}

	public int m_AwakeStateIndex = -1;

	public bool m_AllowNoneState = true;

	public List<PickEvent> m_State;

	public List<PickEvent> m_AlternateState;

	private bool m_StateActive;

	private PickEvent m_CurrentState;

	private Collider m_Collider;

	private bool m_AlternateEventState;

	private int m_LastEventIndex;

	private int m_LastAlternateIndex;

	private bool m_StartAnimationFinished = true;

	private bool m_EndAnimationFinished = true;

	private void Awake()
	{
		m_Collider = GetComponent<Collider>();
		if (m_AwakeStateIndex > -1)
		{
			m_CurrentState = m_State[m_AwakeStateIndex];
			m_LastEventIndex = m_AwakeStateIndex;
			m_StateActive = true;
		}
	}

	public void RandomPickEvent()
	{
		if (!m_StartAnimationFinished || !m_EndAnimationFinished)
		{
			return;
		}
		if (m_StateActive && m_CurrentState.m_EndEvent != string.Empty && m_CurrentState.m_FSM != null)
		{
			m_CurrentState.m_FSM.SendEvent(m_CurrentState.m_EndEvent);
			m_EndAnimationFinished = false;
			m_StateActive = false;
			StartCoroutine(WaitForEndAnimation());
		}
		else if (m_AlternateState.Count > 0)
		{
			if (m_AlternateEventState)
			{
				SendRandomEvent();
			}
			else
			{
				SendAlternateRandomEvent();
			}
		}
		else
		{
			SendRandomEvent();
		}
	}

	public void StartAnimationFinished()
	{
		m_StartAnimationFinished = true;
	}

	public void EndAnimationFinished()
	{
		m_EndAnimationFinished = true;
	}

	private void SendRandomEvent()
	{
		m_StateActive = true;
		m_AlternateEventState = false;
		List<int> list = new List<int>();
		if (m_State.Count == 1)
		{
			list.Add(0);
		}
		else
		{
			for (int i = 0; i < m_State.Count; i++)
			{
				if (i != m_LastEventIndex)
				{
					list.Add(i);
				}
			}
		}
		int index = UnityEngine.Random.Range(0, list.Count);
		PickEvent pickEvent = (m_CurrentState = m_State[list[index]]);
		m_LastEventIndex = list[index];
		m_StartAnimationFinished = false;
		StartCoroutine(WaitForStartAnimation());
		pickEvent.m_FSM.SendEvent(pickEvent.m_StartEvent);
	}

	private void SendAlternateRandomEvent()
	{
		m_StateActive = true;
		m_AlternateEventState = true;
		List<int> list = new List<int>();
		if (m_AlternateState.Count == 1)
		{
			list.Add(0);
		}
		else
		{
			for (int i = 0; i < m_AlternateState.Count; i++)
			{
				if (i != m_LastAlternateIndex)
				{
					list.Add(i);
				}
			}
		}
		int index = UnityEngine.Random.Range(0, list.Count);
		PickEvent pickEvent = (m_CurrentState = m_AlternateState[list[index]]);
		m_LastAlternateIndex = list[index];
		m_StartAnimationFinished = false;
		StartCoroutine(WaitForStartAnimation());
		pickEvent.m_FSM.SendEvent(pickEvent.m_StartEvent);
	}

	private IEnumerator WaitForStartAnimation()
	{
		while (!m_StartAnimationFinished)
		{
			yield return null;
		}
	}

	private IEnumerator WaitForEndAnimation()
	{
		while (!m_EndAnimationFinished)
		{
			yield return null;
		}
		m_CurrentState = null;
		if (!m_AllowNoneState)
		{
			while (!m_StartAnimationFinished)
			{
				yield return null;
			}
			RandomPickEvent();
		}
	}

	private void EnableCollider()
	{
		if (m_Collider != null)
		{
			m_Collider.enabled = true;
		}
	}

	private void DisableCollider()
	{
		if (m_Collider != null)
		{
			m_Collider.enabled = false;
		}
	}
}
