using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000076 RID: 118
public class BnetBarKeyboard : PegUIElement
{
	// Token: 0x060006C0 RID: 1728 RVA: 0x000273C4 File Offset: 0x000255C4
	public void ShowHighlight(bool show)
	{
		Color value = this.m_origColor;
		if (show)
		{
			value = this.m_highlight;
		}
		base.gameObject.GetComponent<Renderer>().GetMaterial().SetColor("_Color", value);
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x00027400 File Offset: 0x00025600
	protected override void OnPress()
	{
		HearthstoneServices.Get<ITouchScreenService>().ShowKeyboard();
		OnKeyboardPressed[] array = this.m_keyboardPressedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x00027439 File Offset: 0x00025639
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		this.ShowHighlight(true);
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x00027442 File Offset: 0x00025642
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		this.ShowHighlight(false);
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x0002744B File Offset: 0x0002564B
	public void RegisterKeyboardPressedListener(OnKeyboardPressed listener)
	{
		if (this.m_keyboardPressedListeners.Contains(listener))
		{
			return;
		}
		this.m_keyboardPressedListeners.Add(listener);
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x00027468 File Offset: 0x00025668
	public void UnregisterKeyboardPressedListener(OnKeyboardPressed listener)
	{
		this.m_keyboardPressedListeners.Remove(listener);
	}

	// Token: 0x040004A8 RID: 1192
	public Color m_highlight;

	// Token: 0x040004A9 RID: 1193
	public Color m_origColor;

	// Token: 0x040004AA RID: 1194
	private List<OnKeyboardPressed> m_keyboardPressedListeners = new List<OnKeyboardPressed>();
}
