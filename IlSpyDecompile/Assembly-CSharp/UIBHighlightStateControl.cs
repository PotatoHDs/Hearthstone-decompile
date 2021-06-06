using UnityEngine;

[CustomEditClass]
public class UIBHighlightStateControl : MonoBehaviour
{
	[CustomEditField(Sections = "Highlight State Reference")]
	public HighlightState m_HighlightState;

	[CustomEditField(Sections = "Highlight State Type")]
	public ActorStateType m_MouseOverStateType = ActorStateType.HIGHLIGHT_MOUSE_OVER;

	[CustomEditField(Sections = "Highlight State Type")]
	public ActorStateType m_PrimarySelectedStateType = ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE;

	[CustomEditField(Sections = "Highlight State Type")]
	public ActorStateType m_SecondarySelectedStateType = ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE;

	[CustomEditField(Sections = "Behavior Settings")]
	public bool m_UseMouseOver;

	[CustomEditField(Sections = "Behavior Settings")]
	public bool m_AllowSelection;

	[CustomEditField(Sections = "Behavior Settings")]
	public bool m_EnableResponse = true;

	private PegUIElement m_PegUIElement;

	private bool m_MouseOver;

	private void Awake()
	{
		PegUIElement component = base.gameObject.GetComponent<PegUIElement>();
		if (!(component != null))
		{
			return;
		}
		component.AddEventListener(UIEventType.ROLLOVER, delegate
		{
			if (m_EnableResponse)
			{
				OnRollOver();
			}
		});
		component.AddEventListener(UIEventType.ROLLOUT, delegate
		{
			if (m_EnableResponse)
			{
				OnRollOut();
			}
		});
		component.AddEventListener(UIEventType.RELEASE, delegate
		{
			if (m_EnableResponse)
			{
				OnRelease();
			}
		});
	}

	public void Select(bool selected, bool primary = false)
	{
		if (selected)
		{
			m_HighlightState.ChangeState(primary ? m_PrimarySelectedStateType : m_SecondarySelectedStateType);
		}
		else if (m_MouseOver)
		{
			m_HighlightState.ChangeState(m_MouseOverStateType);
		}
		else
		{
			m_HighlightState.ChangeState(ActorStateType.NONE);
		}
	}

	public bool IsReady()
	{
		return m_HighlightState.IsReady();
	}

	private void OnRollOver()
	{
		if (m_UseMouseOver)
		{
			m_MouseOver = true;
			m_HighlightState.ChangeState(m_MouseOverStateType);
		}
	}

	private void OnRollOut()
	{
		if (m_UseMouseOver)
		{
			m_MouseOver = false;
			if (!m_AllowSelection)
			{
				m_HighlightState.ChangeState(ActorStateType.NONE);
			}
		}
	}

	private void OnRelease()
	{
		if (m_AllowSelection)
		{
			Select(selected: true);
		}
		else if (m_MouseOver)
		{
			m_HighlightState.ChangeState(m_MouseOverStateType);
		}
		else
		{
			m_HighlightState.ChangeState(ActorStateType.NONE);
		}
	}
}
