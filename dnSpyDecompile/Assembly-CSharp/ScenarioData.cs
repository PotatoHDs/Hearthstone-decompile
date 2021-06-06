using System;
using UnityEngine;

// Token: 0x02000014 RID: 20
[CustomEditClass]
public class ScenarioData : ScriptableObject
{
	// Token: 0x1700001D RID: 29
	// (get) Token: 0x0600008A RID: 138 RVA: 0x0000383C File Offset: 0x00001A3C
	// (set) Token: 0x0600008B RID: 139 RVA: 0x00003844 File Offset: 0x00001A44
	[CustomEditField(Sections = "Phone", Label = "Use Bottom Image")]
	public bool m_bottom
	{
		get
		{
			return this._bottom;
		}
		set
		{
			this._bottom = value;
			this.m_Texture_Phone_offsetY = (value ? this.m_phoneoffset : 0f);
		}
	}

	// Token: 0x04000058 RID: 88
	private bool _bottom;

	// Token: 0x04000059 RID: 89
	private float m_phoneoffset = -0.389f;

	// Token: 0x0400005A RID: 90
	[CustomEditField(T = EditType.TEXTURE)]
	public string m_Texture;

	// Token: 0x0400005B RID: 91
	[CustomEditField(Sections = "Phone", T = EditType.TEXTURE)]
	public string m_Texture_Phone;

	// Token: 0x0400005C RID: 92
	[CustomEditField(Hide = true)]
	public float m_Texture_Phone_offsetY;
}
