using System.Collections;
using UnityEngine;

public class TutorialKeywordTooltip : MonoBehaviour
{
	public UberText m_name;

	public UberText m_body;

	public PlayMakerFSM playMakerComponent;

	public void Initialize(string keywordName, string keywordText)
	{
		SetName(keywordName);
		SetBodyText(keywordText);
		StartCoroutine(WaitAFrameBeforeSendingEvent());
	}

	private IEnumerator WaitAFrameBeforeSendingEvent()
	{
		RenderUtils.SetAlpha(base.gameObject, 0f);
		yield return null;
		playMakerComponent.SendEvent("Birth");
		iTween.FadeTo(base.gameObject, 1f, 0.5f);
	}

	public void SetName(string s)
	{
		m_name.Text = s;
	}

	public void SetBodyText(string s)
	{
		m_body.Text = s;
	}

	public float GetHeight()
	{
		return GetComponent<Renderer>().bounds.size.z;
	}

	public float GetWidth()
	{
		return GetComponent<Renderer>().bounds.size.x;
	}
}
