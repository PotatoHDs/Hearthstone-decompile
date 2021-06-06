using System.Collections;
using UnityEngine;

[CustomEditClass]
public abstract class ChooserSubButton : AdventureGenericButton
{
	protected const string s_EventFlash = "Flash";

	public GameObject m_NewModePopupBone;

	[CustomEditField(Sections = "Event Table")]
	public StateEventTable m_StateTable;

	public float m_NewModePopupAutomaticHideTime = 1f;

	protected bool m_Glow;

	private Notification m_NewModePopup;

	public void SetHighlight(bool enable)
	{
		UIBHighlightStateControl component = GetComponent<UIBHighlightStateControl>();
		if (component != null)
		{
			if (m_Glow)
			{
				component.Select(selected: true, primary: true);
			}
			else
			{
				component.Select(enable);
			}
		}
		UIBHighlight component2 = GetComponent<UIBHighlight>();
		if (component2 != null)
		{
			if (enable)
			{
				component2.Select();
			}
			else
			{
				component2.Reset();
			}
		}
	}

	public void SetNewGlow(bool enable)
	{
		m_Glow = enable;
		UIBHighlightStateControl component = GetComponent<UIBHighlightStateControl>();
		if (component != null)
		{
			component.Select(enable, primary: true);
		}
	}

	public void ShowNewModePopup(string message)
	{
		if (!(m_NewModePopupBone == null))
		{
			m_NewModePopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, m_NewModePopupBone.transform.position, m_NewModePopupBone.transform.localScale, message);
			m_NewModePopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
		}
	}

	public void HideNewModePopupAfterDelay()
	{
		StartCoroutine(HideNewModePopupAfterDelayCoroutine());
	}

	public void Flash()
	{
		m_StateTable.TriggerState("Flash");
	}

	public bool IsReady()
	{
		UIBHighlightStateControl component = GetComponent<UIBHighlightStateControl>();
		if (component != null)
		{
			return component.IsReady();
		}
		return false;
	}

	protected override void OnDestroy()
	{
		if (m_NewModePopup != null)
		{
			m_NewModePopup.Shrink();
		}
		base.OnDestroy();
	}

	public void OnDisable()
	{
		if (m_NewModePopup != null)
		{
			m_NewModePopup.Shrink();
		}
	}

	private IEnumerator HideNewModePopupAfterDelayCoroutine()
	{
		float timer = m_NewModePopupAutomaticHideTime;
		while (timer > 0f)
		{
			timer -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		if (m_NewModePopup != null)
		{
			m_NewModePopup.Shrink();
		}
	}
}
