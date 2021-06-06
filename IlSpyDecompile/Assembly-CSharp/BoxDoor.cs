using System.Collections;
using UnityEngine;

public class BoxDoor : MonoBehaviour
{
	public enum State
	{
		CLOSED,
		OPENED
	}

	private const float BOX_SLIDE_PERCENTAGE_PHONE = 1.038f;

	private Box m_parent;

	private BoxDoorStateInfo m_info;

	private State m_state;

	private bool m_main;

	private Vector3 m_startingPosition;

	private void Awake()
	{
		m_startingPosition = base.gameObject.transform.localPosition;
	}

	public Box GetParent()
	{
		return m_parent;
	}

	public void SetParent(Box parent)
	{
		m_parent = parent;
	}

	public BoxDoorStateInfo GetInfo()
	{
		return m_info;
	}

	public void SetInfo(BoxDoorStateInfo info)
	{
		m_info = info;
	}

	public void EnableMain(bool enable)
	{
		m_main = enable;
	}

	private bool IsMain()
	{
		return m_main;
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
		case State.CLOSED:
		{
			m_parent.OnAnimStarted();
			Vector3 vector2 = m_info.m_ClosedRotation - m_info.m_OpenedRotation;
			Hashtable args2 = iTween.Hash("amount", vector2, "delay", m_info.m_ClosedDelaySec, "time", m_info.m_ClosedRotateSec, "easeType", m_info.m_ClosedRotateEaseType, "space", Space.Self, "oncomplete", "OnAnimFinished", "oncompletetarget", m_parent.gameObject);
			iTween.RotateAdd(base.gameObject, args2);
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				args2 = iTween.Hash("position", m_startingPosition, "isLocal", true, "delay", m_info.m_ClosedDelaySec, "time", m_info.m_ClosedRotateSec, "easeType", m_info.m_ClosedRotateEaseType);
				iTween.MoveTo(base.gameObject, args2);
			}
			if (IsMain())
			{
				m_parent.GetEventSpell(BoxEventType.DOORS_CLOSE).Activate();
				m_parent.GetEventSpell(BoxEventType.SHADOW_FADE_IN).ActivateState(SpellStateType.BIRTH);
			}
			break;
		}
		case State.OPENED:
		{
			m_parent.OnAnimStarted();
			Vector3 vector = m_info.m_OpenedRotation - m_info.m_ClosedRotation;
			Hashtable args = iTween.Hash("amount", vector, "delay", m_info.m_OpenedDelaySec, "time", m_info.m_OpenedRotateSec, "easeType", m_info.m_OpenedRotateEaseType, "space", Space.Self, "oncomplete", "OnAnimFinished", "oncompletetarget", m_parent.gameObject);
			iTween.RotateAdd(base.gameObject, args);
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				Vector3 startingPosition = m_startingPosition;
				startingPosition.x *= 1.038f;
				args = iTween.Hash("position", startingPosition, "isLocal", true, "delay", m_info.m_ClosedDelaySec, "time", m_info.m_ClosedRotateSec, "easeType", m_info.m_ClosedRotateEaseType);
				iTween.MoveTo(base.gameObject, args);
			}
			if (IsMain())
			{
				m_parent.GetEventSpell(BoxEventType.DOORS_OPEN).Activate();
				m_parent.GetEventSpell(BoxEventType.SHADOW_FADE_OUT).ActivateState(SpellStateType.BIRTH);
			}
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
			base.transform.localRotation = Quaternion.Euler(m_info.m_ClosedRotation);
			m_parent.GetEventSpell(BoxEventType.SHADOW_FADE_IN).ActivateState(SpellStateType.ACTION);
			break;
		case State.OPENED:
			base.transform.localRotation = Quaternion.Euler(m_info.m_OpenedRotation);
			m_parent.GetEventSpell(BoxEventType.SHADOW_FADE_OUT).ActivateState(SpellStateType.ACTION);
			break;
		}
	}
}
