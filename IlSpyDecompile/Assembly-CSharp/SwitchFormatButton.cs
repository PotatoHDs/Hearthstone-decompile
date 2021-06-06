using System.Collections;
using Hearthstone.UI;
using UnityEngine;

public class SwitchFormatButton : UIBButton
{
	public MeshRenderer m_buttonRenderer;

	public HighlightState m_highlight;

	public GameObject m_coverObject;

	public UIBHighlight m_uibHighlight;

	public VisualController m_visualController;

	private VisualsFormatType m_visualsFormatType = VisualsFormatType.VFT_STANDARD;

	private bool m_isCovered;

	private const string STANDARD_STATE = "STANDARD";

	private const string CLASSIC_STATE = "CLASSIC";

	private const string WILD_STATE = "WILD";

	private const string CASUAL_STATE = "CASUAL";

	private const string COVERED_STATE = "COVERED";

	private void UpdateIcon()
	{
		if (m_isCovered)
		{
			m_visualController.SetState("COVERED");
			return;
		}
		switch (m_visualsFormatType)
		{
		case VisualsFormatType.VFT_STANDARD:
			m_visualController.SetState("STANDARD");
			break;
		case VisualsFormatType.VFT_CLASSIC:
			m_visualController.SetState("CLASSIC");
			break;
		case VisualsFormatType.VFT_WILD:
			m_visualController.SetState("WILD");
			break;
		case VisualsFormatType.VFT_CASUAL:
			m_visualController.SetState("CASUAL");
			break;
		}
	}

	public void SetVisualsFormatType(VisualsFormatType newVisualsFormatType)
	{
		if (m_visualsFormatType != newVisualsFormatType)
		{
			m_visualsFormatType = newVisualsFormatType;
			UpdateIcon();
		}
	}

	public void Disable()
	{
		m_uibHighlight.Reset();
		SetEnabled(enabled: false);
	}

	public void Enable()
	{
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(value: true);
		}
		SetEnabled(enabled: true);
		UpdateIcon();
	}

	public IEnumerator EnableWithDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(value: true);
		}
		SetEnabled(enabled: true);
		UpdateIcon();
	}

	public void Cover()
	{
		m_isCovered = true;
		UpdateIcon();
	}

	public void Uncover()
	{
		m_isCovered = false;
		UpdateIcon();
	}

	public bool IsCovered()
	{
		return m_isCovered;
	}

	public void EnableHighlight(bool enabled)
	{
		EnableHighlightImpl(enabled);
	}

	private void EnableHighlightImpl(bool enabled)
	{
		if (enabled)
		{
			m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
		else
		{
			m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		}
	}
}
