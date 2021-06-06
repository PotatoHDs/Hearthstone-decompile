using UnityEngine;

public class PlayerLeaderboardIcon : MonoBehaviour
{
	public GameObject m_icon;

	public UberText m_text;

	private const string SHOW_PLAYMAKER_STATE = "Show";

	public void SetText(string text)
	{
		if (text == "")
		{
			ClearText();
		}
		else
		{
			m_text.gameObject.SetActive(value: true);
		}
		m_text.Text = text;
	}

	public void ClearText()
	{
		m_text.Text = "";
		m_text.gameObject.SetActive(value: false);
	}

	public void SetPlaymakerValue(string name, int value)
	{
		PlayMakerFSM component = base.gameObject.GetComponent<PlayMakerFSM>();
		if (component != null && component.FsmVariables.GetFsmInt(name) != null)
		{
			component.FsmVariables.GetFsmInt(name).Value = value;
			component.SendEvent("Action");
		}
	}

	public void PlaymakerShow()
	{
		PlayMakerFSM component = base.gameObject.GetComponent<PlayMakerFSM>();
		if (component != null)
		{
			component.SetState("Show");
		}
		else
		{
			base.gameObject.SetActive(value: false);
		}
	}

	public bool PlaymakerIsShowing()
	{
		PlayMakerFSM component = base.gameObject.GetComponent<PlayMakerFSM>();
		if (component != null)
		{
			return component.ActiveStateName == "Show";
		}
		return false;
	}
}
