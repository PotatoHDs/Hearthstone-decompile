using System;
using UnityEngine;

// Token: 0x020000D6 RID: 214
public class CardBack : MonoBehaviour
{
	// Token: 0x040008C3 RID: 2243
	public Mesh m_CardBackMesh;

	// Token: 0x040008C4 RID: 2244
	public Material m_CardBackMaterial;

	// Token: 0x040008C5 RID: 2245
	public Material m_CardBackMaterial1;

	// Token: 0x040008C6 RID: 2246
	public Texture2D m_CardBackTexture;

	// Token: 0x040008C7 RID: 2247
	public Texture2D m_HiddenCardEchoTexture;

	// Token: 0x040008C8 RID: 2248
	public GameObject m_DragEffect;

	// Token: 0x040008C9 RID: 2249
	public float m_EffectMinVelocity = 2f;

	// Token: 0x040008CA RID: 2250
	public float m_EffectMaxVelocity = 40f;

	// Token: 0x040008CB RID: 2251
	public CardBack.cardBackHelpers cardBackHelper;

	// Token: 0x020013E2 RID: 5090
	public enum cardBackHelpers
	{
		// Token: 0x0400A828 RID: 43048
		None,
		// Token: 0x0400A829 RID: 43049
		CardBackHelperBubbleLevel,
		// Token: 0x0400A82A RID: 43050
		CardBackHelperFlipbook
	}
}
