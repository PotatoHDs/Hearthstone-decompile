using System;
using UnityEngine;

// Token: 0x02000B0E RID: 2830
public class DropdownMenuItem : PegUIElement
{
	// Token: 0x06009697 RID: 38551 RVA: 0x0030BD03 File Offset: 0x00309F03
	public object GetValue()
	{
		return this.m_value;
	}

	// Token: 0x06009698 RID: 38552 RVA: 0x0030BD0B File Offset: 0x00309F0B
	public void SetValue(object val, string text)
	{
		this.m_value = val;
		this.m_text.Text = text;
	}

	// Token: 0x06009699 RID: 38553 RVA: 0x0030BD20 File Offset: 0x00309F20
	public void SetSelected(bool selected)
	{
		if (this.m_selected == null)
		{
			return;
		}
		this.m_selected.SetActive(selected);
	}

	// Token: 0x0600969A RID: 38554 RVA: 0x0030BD3D File Offset: 0x00309F3D
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		this.m_text.TextColor = Color.white;
	}

	// Token: 0x0600969B RID: 38555 RVA: 0x0030BD4F File Offset: 0x00309F4F
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		this.m_text.TextColor = Color.black;
	}

	// Token: 0x04007E26 RID: 32294
	public GameObject m_selected;

	// Token: 0x04007E27 RID: 32295
	public UberText m_text;

	// Token: 0x04007E28 RID: 32296
	private object m_value;
}
