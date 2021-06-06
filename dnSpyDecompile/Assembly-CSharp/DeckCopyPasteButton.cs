using System;
using UnityEngine;

// Token: 0x02000124 RID: 292
public class DeckCopyPasteButton : PegUIElement
{
	// Token: 0x170000DD RID: 221
	// (get) Token: 0x06001344 RID: 4932 RVA: 0x0006EA6E File Offset: 0x0006CC6E
	// (set) Token: 0x06001345 RID: 4933 RVA: 0x0006EA76 File Offset: 0x0006CC76
	public string TooltipMessage { get; set; }

	// Token: 0x170000DE RID: 222
	// (get) Token: 0x06001346 RID: 4934 RVA: 0x0006EA7F File Offset: 0x0006CC7F
	// (set) Token: 0x06001347 RID: 4935 RVA: 0x0006EA87 File Offset: 0x0006CC87
	public string TooltipHeaderString { get; set; }

	// Token: 0x06001348 RID: 4936 RVA: 0x0006EA90 File Offset: 0x0006CC90
	private void Start()
	{
		this.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnButtonOver));
		this.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnButtonOut));
	}

	// Token: 0x06001349 RID: 4937 RVA: 0x0006EABA File Offset: 0x0006CCBA
	public override void SetEnabled(bool enabled, bool isInternal = false)
	{
		this.m_clickEnabled = enabled;
		base.GetComponent<Renderer>().GetMaterial().SetFloat("_Desaturate", (float)(enabled ? 0 : 1));
	}

	// Token: 0x0600134A RID: 4938 RVA: 0x0006EAE0 File Offset: 0x0006CCE0
	public bool IsClickEnabled()
	{
		return this.m_clickEnabled;
	}

	// Token: 0x0600134B RID: 4939 RVA: 0x0006EAE8 File Offset: 0x0006CCE8
	public override void TriggerPress()
	{
		if (!this.m_clickEnabled)
		{
			return;
		}
		base.TriggerPress();
	}

	// Token: 0x0600134C RID: 4940 RVA: 0x0006EAFC File Offset: 0x0006CCFC
	private void OnButtonOver(UIEvent e)
	{
		TooltipZone component = base.GetComponent<TooltipZone>();
		if (component == null)
		{
			return;
		}
		if (string.IsNullOrEmpty(this.TooltipMessage))
		{
			return;
		}
		component.ShowTooltip(this.TooltipHeaderString, this.TooltipMessage, 4f, 0);
	}

	// Token: 0x0600134D RID: 4941 RVA: 0x0006EB44 File Offset: 0x0006CD44
	private void OnButtonOut(UIEvent e)
	{
		TooltipZone component = base.GetComponent<TooltipZone>();
		if (component != null)
		{
			component.HideTooltip();
		}
	}

	// Token: 0x04000C8C RID: 3212
	public UberText ButtonText;

	// Token: 0x04000C8F RID: 3215
	private bool m_clickEnabled;
}
