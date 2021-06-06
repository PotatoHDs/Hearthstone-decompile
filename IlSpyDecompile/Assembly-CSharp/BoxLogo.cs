using System.Collections;
using UnityEngine;

public class BoxLogo : MonoBehaviour
{
	public enum State
	{
		SHOWN,
		HIDDEN
	}

	private Box m_parent;

	private BoxLogoStateInfo m_info;

	private State m_state;

	public Box GetParent()
	{
		return m_parent;
	}

	public void SetParent(Box parent)
	{
		m_parent = parent;
	}

	public BoxLogoStateInfo GetInfo()
	{
		return m_info;
	}

	public void SetInfo(BoxLogoStateInfo info)
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
		case State.SHOWN:
		{
			m_parent.OnAnimStarted();
			Hashtable args2 = iTween.Hash("amount", m_info.m_ShownAlpha, "delay", m_info.m_ShownDelaySec, "time", m_info.m_ShownFadeSec, "easeType", m_info.m_ShownFadeEaseType, "oncomplete", "OnAnimFinished", "oncompletetarget", m_parent.gameObject);
			iTween.FadeTo(base.gameObject, args2);
			break;
		}
		case State.HIDDEN:
		{
			m_parent.OnAnimStarted();
			Hashtable args = iTween.Hash("amount", m_info.m_HiddenAlpha, "delay", m_info.m_HiddenDelaySec, "time", m_info.m_HiddenFadeSec, "easeType", m_info.m_HiddenFadeEaseType, "oncomplete", "OnAnimFinished", "oncompletetarget", m_parent.gameObject);
			iTween.FadeTo(base.gameObject, args);
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
		case State.SHOWN:
			RenderUtils.SetAlpha(base.gameObject, m_info.m_ShownAlpha);
			break;
		case State.HIDDEN:
			RenderUtils.SetAlpha(base.gameObject, m_info.m_HiddenAlpha);
			break;
		}
	}
}
