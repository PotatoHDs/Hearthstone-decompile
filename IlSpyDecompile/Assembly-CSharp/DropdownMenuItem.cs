using UnityEngine;

public class DropdownMenuItem : PegUIElement
{
	public GameObject m_selected;

	public UberText m_text;

	private object m_value;

	public object GetValue()
	{
		return m_value;
	}

	public void SetValue(object val, string text)
	{
		m_value = val;
		m_text.Text = text;
	}

	public void SetSelected(bool selected)
	{
		if (!(m_selected == null))
		{
			m_selected.SetActive(selected);
		}
	}

	protected override void OnOver(InteractionState oldState)
	{
		m_text.TextColor = Color.white;
	}

	protected override void OnOut(InteractionState oldState)
	{
		m_text.TextColor = Color.black;
	}
}
