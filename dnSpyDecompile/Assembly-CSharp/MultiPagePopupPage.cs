using System;
using UnityEngine;

// Token: 0x02000B22 RID: 2850
[CustomEditClass]
public class MultiPagePopupPage : MonoBehaviour
{
	// Token: 0x04007EA4 RID: 32420
	[CustomEditField(Sections = "Button")]
	public UIBButton m_button;

	// Token: 0x04007EA5 RID: 32421
	[CustomEditField(Sections = "Button")]
	public UberText m_buttonText;

	// Token: 0x04007EA6 RID: 32422
	[CustomEditField(Sections = "Text Fields")]
	public UberText m_headerText;

	// Token: 0x04007EA7 RID: 32423
	[CustomEditField(Sections = "Text Fields")]
	public UberText m_bodyText;

	// Token: 0x04007EA8 RID: 32424
	[CustomEditField(Sections = "Text Fields")]
	public UberText m_footerText;
}
