using System;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x0200002C RID: 44
[CustomEditClass]
public class AdventureChooserDescription : MonoBehaviour
{
	// Token: 0x17000031 RID: 49
	// (get) Token: 0x0600016D RID: 365 RVA: 0x0000890B File Offset: 0x00006B0B
	// (set) Token: 0x0600016E RID: 366 RVA: 0x00008918 File Offset: 0x00006B18
	[CustomEditField(Sections = "Description")]
	public Color WarningTextColor
	{
		get
		{
			return this.m_WarningTextColor;
		}
		set
		{
			this.m_WarningTextColor = value;
			this.RefreshText();
		}
	}

	// Token: 0x0600016F RID: 367 RVA: 0x0000892C File Offset: 0x00006B2C
	public string GetText()
	{
		return this.m_DescriptionObject.Text;
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00008939 File Offset: 0x00006B39
	public void SetText(string requiredText, string descText)
	{
		this.m_RequiredText = requiredText;
		this.m_DescText = descText;
		this.RefreshText();
	}

	// Token: 0x06000171 RID: 369 RVA: 0x00008950 File Offset: 0x00006B50
	private void RefreshText()
	{
		string text2;
		if (!string.IsNullOrEmpty(this.m_RequiredText))
		{
			string text = this.m_WarningTextColor.r.ToString("X2") + this.m_WarningTextColor.g.ToString("X2") + this.m_WarningTextColor.b.ToString("X2");
			text2 = string.Concat(new string[]
			{
				"<color=#",
				text,
				">",
				this.m_RequiredText,
				"</color>\n",
				this.m_DescText
			});
		}
		else
		{
			text2 = this.m_DescText;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_DescriptionObject.CharacterSize = 70f;
		}
		this.m_DescriptionObject.Text = text2;
	}

	// Token: 0x040000F7 RID: 247
	[SerializeField]
	private Color32 m_WarningTextColor = new Color32(byte.MaxValue, 210, 23, byte.MaxValue);

	// Token: 0x040000F8 RID: 248
	[CustomEditField(Sections = "Description")]
	public UberText m_DescriptionObject;

	// Token: 0x040000F9 RID: 249
	public AsyncReference m_WidgetElement;

	// Token: 0x040000FA RID: 250
	private string m_RequiredText;

	// Token: 0x040000FB RID: 251
	private string m_DescText;
}
