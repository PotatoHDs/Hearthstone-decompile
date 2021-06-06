using UnityEngine;

[CustomEditClass]
public class MultiPagePopupPage : MonoBehaviour
{
	[CustomEditField(Sections = "Button")]
	public UIBButton m_button;

	[CustomEditField(Sections = "Button")]
	public UberText m_buttonText;

	[CustomEditField(Sections = "Text Fields")]
	public UberText m_headerText;

	[CustomEditField(Sections = "Text Fields")]
	public UberText m_bodyText;

	[CustomEditField(Sections = "Text Fields")]
	public UberText m_footerText;
}
