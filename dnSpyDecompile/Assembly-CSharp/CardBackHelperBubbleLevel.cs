using System;
using UnityEngine;

// Token: 0x020000DA RID: 218
public class CardBackHelperBubbleLevel : MonoBehaviour
{
	// Token: 0x06000CAE RID: 3246 RVA: 0x00049B6C File Offset: 0x00047D6C
	public void Awake()
	{
		this.m_material2 = base.GetComponent<Renderer>().GetMaterial(this.TargetMaterialIndex);
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x00049B88 File Offset: 0x00047D88
	private void Update()
	{
		float num = CardBackHelperBubbleLevel.NormalizeRotation(base.gameObject.transform.eulerAngles.x);
		float num2 = CardBackHelperBubbleLevel.NormalizeRotation(base.gameObject.transform.eulerAngles.y - 180f);
		float num3 = CardBackHelperBubbleLevel.NormalizeRotation(base.gameObject.transform.eulerAngles.z);
		this.m_objectTilt = num + num2 + num3;
		this.m_objectTilt = Mathf.Clamp(this.m_objectTilt / this.TiltSensitivity, this.m_tiltRangeMin, this.m_tiltRangeMax);
		this.m_objectTilt = (this.m_objectTilt + 2f) * 0.5f;
		this.m_bubbleLiteralPosition = this.m_objectTilt * 0.5f;
		this.m_bubbleDistanceFromLiteral = (this.m_bubblePosition - this.m_bubbleLiteralPosition) * -1f;
		this.m_bubbleMomentum = this.m_bubbleDistanceFromLiteral * this.BubbleMomentumBase;
		this.m_bubblePosition = Mathf.Clamp(this.m_bubblePosition + this.m_bubbleMomentum, 0f, 1f);
		this.m_material2.SetTextureOffset(this.TexturePropertyName, new Vector2(this.m_bubblePosition * -2f, this.BubbleOffsetY));
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x00049CBB File Offset: 0x00047EBB
	private static float NormalizeRotation(float inputRotation)
	{
		if (inputRotation > 180f)
		{
			return inputRotation - 360f;
		}
		return inputRotation;
	}

	// Token: 0x040008DC RID: 2268
	private Material m_material2;

	// Token: 0x040008DD RID: 2269
	private float m_bubbleLiteralPosition;

	// Token: 0x040008DE RID: 2270
	private float m_objectTilt;

	// Token: 0x040008DF RID: 2271
	private float m_tiltRangeMin = -2f;

	// Token: 0x040008E0 RID: 2272
	private float m_tiltRangeMax = 2f;

	// Token: 0x040008E1 RID: 2273
	private float m_bubbleMomentum;

	// Token: 0x040008E2 RID: 2274
	private float m_bubblePosition;

	// Token: 0x040008E3 RID: 2275
	private float m_bubbleDistanceFromLiteral;

	// Token: 0x040008E4 RID: 2276
	public int TargetMaterialIndex;

	// Token: 0x040008E5 RID: 2277
	public float TiltSensitivity = 5f;

	// Token: 0x040008E6 RID: 2278
	public float BubbleMomentumBase = 0.05f;

	// Token: 0x040008E7 RID: 2279
	public float BubbleOffsetY = -1.22f;

	// Token: 0x040008E8 RID: 2280
	public string TexturePropertyName = "_AddTex";
}
