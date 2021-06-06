using System;
using UnityEngine;

// Token: 0x02000B1E RID: 2846
public class MenuButton : PegUIElement
{
	// Token: 0x06009737 RID: 38711 RVA: 0x0030E0AC File Offset: 0x0030C2AC
	public void SetText(string s)
	{
		this.m_label.text = s;
	}

	// Token: 0x04007E8B RID: 32395
	public TextMesh m_label;
}
