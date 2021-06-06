using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C8 RID: 200
[Serializable]
public class BoxLightState
{
	// Token: 0x04000889 RID: 2185
	public BoxLightStateType m_Type;

	// Token: 0x0400088A RID: 2186
	public float m_DelaySec;

	// Token: 0x0400088B RID: 2187
	public float m_TransitionSec = 0.5f;

	// Token: 0x0400088C RID: 2188
	public iTween.EaseType m_TransitionEaseType = iTween.EaseType.linear;

	// Token: 0x0400088D RID: 2189
	public Spell m_Spell;

	// Token: 0x0400088E RID: 2190
	public Color m_AmbientColor = new Color(0.5058824f, 0.4745098f, 0.4745098f, 1f);

	// Token: 0x0400088F RID: 2191
	public List<BoxLightInfo> m_LightInfos;
}
