using UnityEngine;

public class FriendListUIElement : PegUIElement
{
	public FriendListUIElement m_ParentElement;

	public GameObject m_Highlight;

	private bool m_selected;

	protected override void Awake()
	{
		base.Awake();
		UpdateHighlight();
	}

	public bool IsSelected()
	{
		return m_selected;
	}

	public void SetSelected(bool enable)
	{
		if (enable != m_selected)
		{
			m_selected = enable;
			UpdateHighlight();
		}
	}

	protected virtual bool ShouldBeHighlighted()
	{
		if (!m_selected)
		{
			return GetInteractionState() == InteractionState.Over;
		}
		return true;
	}

	protected void UpdateHighlight()
	{
		bool flag = ShouldBeHighlighted();
		if (!flag)
		{
			flag = ShouldChildBeHighlighted();
		}
		UpdateSelfHighlight(flag);
		if (m_ParentElement != null)
		{
			m_ParentElement.UpdateHighlight();
		}
	}

	protected bool ShouldChildBeHighlighted()
	{
		FriendListUIElement[] componentsInChildrenOnly = SceneUtils.GetComponentsInChildrenOnly<FriendListUIElement>(this, includeInactive: true);
		for (int i = 0; i < componentsInChildrenOnly.Length; i++)
		{
			if (componentsInChildrenOnly[i].ShouldBeHighlighted())
			{
				return true;
			}
		}
		return false;
	}

	protected void UpdateSelfHighlight(bool shouldHighlight)
	{
		if (!(m_Highlight == null) && m_Highlight.activeSelf != shouldHighlight)
		{
			m_Highlight.SetActive(shouldHighlight);
		}
	}

	protected override void OnOver(InteractionState oldState)
	{
		UpdateHighlight();
	}

	protected override void OnOut(InteractionState oldState)
	{
		UpdateHighlight();
	}
}
