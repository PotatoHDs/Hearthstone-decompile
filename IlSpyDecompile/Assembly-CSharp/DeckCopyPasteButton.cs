using UnityEngine;

public class DeckCopyPasteButton : PegUIElement
{
	public UberText ButtonText;

	private bool m_clickEnabled;

	public string TooltipMessage { get; set; }

	public string TooltipHeaderString { get; set; }

	private void Start()
	{
		AddEventListener(UIEventType.ROLLOVER, OnButtonOver);
		AddEventListener(UIEventType.ROLLOUT, OnButtonOut);
	}

	public override void SetEnabled(bool enabled, bool isInternal = false)
	{
		m_clickEnabled = enabled;
		GetComponent<Renderer>().GetMaterial().SetFloat("_Desaturate", (!enabled) ? 1 : 0);
	}

	public bool IsClickEnabled()
	{
		return m_clickEnabled;
	}

	public override void TriggerPress()
	{
		if (m_clickEnabled)
		{
			base.TriggerPress();
		}
	}

	private void OnButtonOver(UIEvent e)
	{
		TooltipZone component = GetComponent<TooltipZone>();
		if (!(component == null) && !string.IsNullOrEmpty(TooltipMessage))
		{
			component.ShowTooltip(TooltipHeaderString, TooltipMessage, 4f);
		}
	}

	private void OnButtonOut(UIEvent e)
	{
		TooltipZone component = GetComponent<TooltipZone>();
		if (component != null)
		{
			component.HideTooltip();
		}
	}
}
