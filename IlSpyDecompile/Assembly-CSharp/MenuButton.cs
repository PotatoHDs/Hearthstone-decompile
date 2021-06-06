using UnityEngine;

public class MenuButton : PegUIElement
{
	public TextMesh m_label;

	public void SetText(string s)
	{
		m_label.text = s;
	}
}
