using System;
using UnityEngine;

// Token: 0x02000AFB RID: 2811
[CustomEditClass]
public class ButtonListMenuDef : MonoBehaviour
{
	// Token: 0x04007D79 RID: 32121
	[CustomEditField(Sections = "Header")]
	public UberText m_headerText;

	// Token: 0x04007D7A RID: 32122
	[CustomEditField(Sections = "Header")]
	public MultiSliceElement m_header;

	// Token: 0x04007D7B RID: 32123
	[CustomEditField(Sections = "Header")]
	public GameObject m_headerMiddle;

	// Token: 0x04007D7C RID: 32124
	[CustomEditField(Sections = "Layout")]
	public NineSliceElement m_background;

	// Token: 0x04007D7D RID: 32125
	[CustomEditField(Sections = "Layout")]
	public NineSliceElement m_border;

	// Token: 0x04007D7E RID: 32126
	[CustomEditField(Sections = "Layout")]
	public MultiSliceElement m_buttonContainer;

	// Token: 0x04007D7F RID: 32127
	[CustomEditField(Sections = "Template Items")]
	public UIBButton m_templateButton;

	// Token: 0x04007D80 RID: 32128
	[CustomEditField(Sections = "Template Items")]
	public UIBButton m_templateDownloadButton;

	// Token: 0x04007D81 RID: 32129
	[CustomEditField(Sections = "Template Items")]
	public UIBButton m_templateSignUpButton;

	// Token: 0x04007D82 RID: 32130
	[CustomEditField(Sections = "Template Items")]
	public GameObject m_templateHorizontalDivider;

	// Token: 0x04007D83 RID: 32131
	[CustomEditField(Sections = "Template Items")]
	public Vector3 m_horizontalDividerMinPadding = new Vector3(0f, 0f, 0.18f);
}
