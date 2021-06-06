using System.Collections;
using UnityEngine;

public class BoxDisk : MonoBehaviour
{
	public enum State
	{
		LOADING,
		MAINMENU
	}

	private Box m_parent;

	private BoxDiskStateInfo m_info;

	private State m_state;

	public void SetParent(Box parent)
	{
		m_parent = parent;
	}

	public Box GetParent()
	{
		return m_parent;
	}

	public BoxDiskStateInfo GetInfo()
	{
		return m_info;
	}

	public void SetInfo(BoxDiskStateInfo info)
	{
		m_info = info;
	}

	public bool ChangeState(State state)
	{
		if (m_state == state)
		{
			return false;
		}
		m_state = state;
		switch (state)
		{
		case State.LOADING:
		{
			m_parent.OnAnimStarted();
			Vector3 vector2 = m_info.m_LoadingRotation - base.transform.localRotation.eulerAngles;
			Hashtable args2 = iTween.Hash("amount", vector2, "delay", m_info.m_LoadingDelaySec, "time", m_info.m_LoadingRotateSec, "easeType", m_info.m_LoadingRotateEaseType, "space", Space.Self, "oncomplete", "OnAnimFinished", "oncompletetarget", m_parent.gameObject);
			iTween.RotateAdd(base.gameObject, args2);
			m_parent.GetEventSpell(BoxEventType.DISK_LOADING).ActivateState(SpellStateType.BIRTH);
			break;
		}
		case State.MAINMENU:
		{
			m_parent.OnAnimStarted();
			Vector3 vector = m_info.m_MainMenuRotation - base.transform.localRotation.eulerAngles;
			Hashtable args = iTween.Hash("amount", vector, "delay", m_info.m_MainMenuDelaySec, "time", m_info.m_MainMenuRotateSec, "easeType", m_info.m_MainMenuRotateEaseType, "space", Space.Self, "oncomplete", "OnAnimFinished", "oncompletetarget", m_parent.gameObject);
			iTween.RotateAdd(base.gameObject, args);
			m_parent.GetEventSpell(BoxEventType.DISK_MAIN_MENU).ActivateState(SpellStateType.BIRTH);
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
		case State.LOADING:
			base.transform.localRotation = Quaternion.Euler(m_info.m_LoadingRotation);
			m_parent.GetEventSpell(BoxEventType.DISK_LOADING).ActivateState(SpellStateType.ACTION);
			break;
		case State.MAINMENU:
			base.transform.localRotation = Quaternion.Euler(m_info.m_MainMenuRotation);
			m_parent.GetEventSpell(BoxEventType.DISK_MAIN_MENU).ActivateState(SpellStateType.ACTION);
			break;
		}
	}
}
