using Hearthstone.UI;
using UnityEngine;

[CustomEditClass]
public class AdventureChooserDescription : MonoBehaviour
{
	[SerializeField]
	private Color32 m_WarningTextColor = new Color32(byte.MaxValue, 210, 23, byte.MaxValue);

	[CustomEditField(Sections = "Description")]
	public UberText m_DescriptionObject;

	public AsyncReference m_WidgetElement;

	private string m_RequiredText;

	private string m_DescText;

	[CustomEditField(Sections = "Description")]
	public Color WarningTextColor
	{
		get
		{
			return m_WarningTextColor;
		}
		set
		{
			m_WarningTextColor = value;
			RefreshText();
		}
	}

	public string GetText()
	{
		return m_DescriptionObject.Text;
	}

	public void SetText(string requiredText, string descText)
	{
		m_RequiredText = requiredText;
		m_DescText = descText;
		RefreshText();
	}

	private void RefreshText()
	{
		string text = null;
		if (!string.IsNullOrEmpty(m_RequiredText))
		{
			string text2 = m_WarningTextColor.r.ToString("X2") + m_WarningTextColor.g.ToString("X2") + m_WarningTextColor.b.ToString("X2");
			text = "<color=#" + text2 + ">" + m_RequiredText + "</color>\n" + m_DescText;
		}
		else
		{
			text = m_DescText;
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_DescriptionObject.CharacterSize = 70f;
		}
		m_DescriptionObject.Text = text;
	}
}
