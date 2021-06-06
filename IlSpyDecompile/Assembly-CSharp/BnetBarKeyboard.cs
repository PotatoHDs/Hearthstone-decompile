using System.Collections.Generic;
using UnityEngine;

public class BnetBarKeyboard : PegUIElement
{
	public Color m_highlight;

	public Color m_origColor;

	private List<OnKeyboardPressed> m_keyboardPressedListeners = new List<OnKeyboardPressed>();

	public void ShowHighlight(bool show)
	{
		Color value = m_origColor;
		if (show)
		{
			value = m_highlight;
		}
		base.gameObject.GetComponent<Renderer>().GetMaterial().SetColor("_Color", value);
	}

	protected override void OnPress()
	{
		HearthstoneServices.Get<ITouchScreenService>().ShowKeyboard();
		OnKeyboardPressed[] array = m_keyboardPressedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	protected override void OnOver(InteractionState oldState)
	{
		ShowHighlight(show: true);
	}

	protected override void OnOut(InteractionState oldState)
	{
		ShowHighlight(show: false);
	}

	public void RegisterKeyboardPressedListener(OnKeyboardPressed listener)
	{
		if (!m_keyboardPressedListeners.Contains(listener))
		{
			m_keyboardPressedListeners.Add(listener);
		}
	}

	public void UnregisterKeyboardPressedListener(OnKeyboardPressed listener)
	{
		m_keyboardPressedListeners.Remove(listener);
	}
}
