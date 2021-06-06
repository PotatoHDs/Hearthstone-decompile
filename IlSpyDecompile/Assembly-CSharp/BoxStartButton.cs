using System.Collections;

public class BoxStartButton : PegUIElement
{
	public enum State
	{
		SHOWN,
		HIDDEN
	}

	public UberText m_Text;

	private Box m_parent;

	private BoxStartButtonStateInfo m_info;

	private State m_state;

	public Box GetParent()
	{
		return m_parent;
	}

	public void SetParent(Box parent)
	{
		m_parent = parent;
	}

	public BoxStartButtonStateInfo GetInfo()
	{
		return m_info;
	}

	public void SetInfo(BoxStartButtonStateInfo info)
	{
		m_info = info;
	}

	public string GetText()
	{
		return m_Text.Text;
	}

	public void SetText(string text)
	{
		m_Text.Text = text;
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
			base.gameObject.SetActive(value: true);
			Hashtable args2 = iTween.Hash("amount", m_info.m_ShownAlpha, "delay", m_info.m_ShownDelaySec, "time", m_info.m_ShownFadeSec, "easeType", m_info.m_ShownFadeEaseType, "oncomplete", "OnShownAnimFinished", "oncompletetarget", base.gameObject);
			iTween.FadeTo(base.gameObject, args2);
			break;
		}
		case State.HIDDEN:
		{
			m_parent.OnAnimStarted();
			Hashtable args = iTween.Hash("amount", m_info.m_HiddenAlpha, "delay", m_info.m_HiddenDelaySec, "time", m_info.m_HiddenFadeSec, "easeType", m_info.m_HiddenFadeEaseType, "oncomplete", "OnHiddenAnimFinished", "oncompletetarget", base.gameObject);
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
			base.gameObject.SetActive(value: true);
			break;
		case State.HIDDEN:
			RenderUtils.SetAlpha(base.gameObject, m_info.m_HiddenAlpha);
			base.gameObject.SetActive(value: false);
			break;
		}
	}

	private void OnShownAnimFinished()
	{
		m_parent.OnAnimFinished();
	}

	private void OnHiddenAnimFinished()
	{
		base.gameObject.SetActive(value: false);
		m_parent.OnAnimFinished();
	}
}
