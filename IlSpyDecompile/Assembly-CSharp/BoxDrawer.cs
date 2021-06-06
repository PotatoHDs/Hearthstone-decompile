using System.Collections;
using UnityEngine;

public class BoxDrawer : MonoBehaviour
{
	public enum State
	{
		CLOSED,
		CLOSED_BOX_OPENED,
		OPENED
	}

	private Box m_parent;

	private BoxDrawerStateInfo m_info;

	private State m_state;

	public Box GetParent()
	{
		return m_parent;
	}

	public void SetParent(Box parent)
	{
		m_parent = parent;
	}

	public BoxDrawerStateInfo GetInfo()
	{
		return m_info;
	}

	public void SetInfo(BoxDrawerStateInfo info)
	{
		m_info = info;
	}

	public bool ChangeState(State state)
	{
		if (DemoMgr.Get().GetMode() == DemoMode.PAX_EAST_2013)
		{
			return true;
		}
		if (DemoMgr.Get().GetMode() == DemoMode.BLIZZCON_2013)
		{
			return true;
		}
		if (m_state == state)
		{
			return false;
		}
		State state2 = m_state;
		m_state = state;
		if (IsInactiveState(state2) && IsInactiveState(m_state))
		{
			return true;
		}
		base.gameObject.SetActive(value: true);
		switch (state)
		{
		case State.CLOSED:
		{
			m_parent.OnAnimStarted();
			Hashtable args3 = iTween.Hash("position", m_info.m_ClosedBone.transform.position, "delay", m_info.m_ClosedDelaySec, "time", m_info.m_ClosedMoveSec, "easeType", m_info.m_ClosedMoveEaseType, "oncomplete", "OnClosedAnimFinished", "oncompletetarget", base.gameObject);
			iTween.MoveTo(base.gameObject, args3);
			m_parent.GetEventSpell(BoxEventType.DRAWER_CLOSE).Activate();
			break;
		}
		case State.CLOSED_BOX_OPENED:
		{
			m_parent.OnAnimStarted();
			Hashtable args2 = iTween.Hash("position", m_info.m_ClosedBoxOpenedBone.transform.position, "delay", m_info.m_ClosedBoxOpenedDelaySec, "time", m_info.m_ClosedBoxOpenedMoveSec, "easeType", m_info.m_ClosedBoxOpenedMoveEaseType, "oncomplete", "OnClosedBoxOpenedAnimFinished", "oncompletetarget", base.gameObject);
			iTween.MoveTo(base.gameObject, args2);
			m_parent.GetEventSpell(BoxEventType.DRAWER_CLOSE).Activate();
			break;
		}
		case State.OPENED:
		{
			m_parent.OnAnimStarted();
			Hashtable args = iTween.Hash("position", m_info.m_OpenedBone.transform.position, "delay", m_info.m_OpenedDelaySec, "time", m_info.m_OpenedMoveSec, "easeType", m_info.m_OpenedMoveEaseType, "oncomplete", "OnOpenedAnimFinished", "oncompletetarget", base.gameObject);
			iTween.MoveTo(base.gameObject, args);
			m_parent.GetEventSpell(BoxEventType.DRAWER_OPEN).Activate();
			break;
		}
		}
		return true;
	}

	public void UpdateState(State state)
	{
		m_state = state;
		switch (state)
		{
		case State.CLOSED:
			base.transform.position = m_info.m_ClosedBone.transform.position;
			base.gameObject.SetActive(value: false);
			break;
		case State.CLOSED_BOX_OPENED:
			base.transform.position = m_info.m_ClosedBoxOpenedBone.transform.position;
			base.gameObject.SetActive(value: false);
			break;
		case State.OPENED:
			base.transform.position = m_info.m_OpenedBone.transform.position;
			base.gameObject.SetActive(value: true);
			break;
		}
	}

	private bool IsInactiveState(State state)
	{
		if (state != 0)
		{
			return state == State.CLOSED_BOX_OPENED;
		}
		return true;
	}

	private void OnClosedAnimFinished()
	{
		base.gameObject.SetActive(value: false);
		m_parent.OnAnimFinished();
	}

	private void OnClosedBoxOpenedAnimFinished()
	{
		base.gameObject.SetActive(value: false);
		m_parent.OnAnimFinished();
	}

	private void OnOpenedAnimFinished()
	{
		base.gameObject.SetActive(value: true);
		m_parent.OnAnimFinished();
	}
}
