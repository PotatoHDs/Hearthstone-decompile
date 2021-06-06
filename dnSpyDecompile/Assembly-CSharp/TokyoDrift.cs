using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000AA1 RID: 2721
public class TokyoDrift : MonoBehaviour
{
	// Token: 0x0600911A RID: 37146 RVA: 0x002F0E58 File Offset: 0x002EF058
	private void Start()
	{
		this.m_originalPosition = base.transform.localPosition;
		this.m_newPosition = default(Vector3);
		this.m_posSeedX = (float)UnityEngine.Random.Range(1, 10);
		this.m_posSeedY = (float)UnityEngine.Random.Range(1, 10);
		this.m_posSeedZ = (float)UnityEngine.Random.Range(1, 10);
		this.m_posOffsetX = UnityEngine.Random.Range(0.6f, 0.99f);
		this.m_posOffsetY = UnityEngine.Random.Range(0.6f, 0.99f);
		this.m_posOffsetZ = UnityEngine.Random.Range(0.6f, 0.99f);
	}

	// Token: 0x0600911B RID: 37147 RVA: 0x002F0EF0 File Offset: 0x002EF0F0
	private void OnDisable()
	{
		if (this.m_blend == 0f)
		{
			return;
		}
		if (!base.gameObject.activeInHierarchy)
		{
			this.m_blend = 0f;
			this.m_blendOut = false;
			base.transform.localPosition = this.m_originalPosition;
			return;
		}
		if (this.m_blend > 1f)
		{
			this.m_blend = 1f;
		}
		base.StartCoroutine(this.BlendOut());
	}

	// Token: 0x0600911C RID: 37148 RVA: 0x002F0F64 File Offset: 0x002EF164
	private void Update()
	{
		if (this.m_blendOut)
		{
			return;
		}
		float num = Time.timeSinceLevelLoad * this.m_DriftSpeed;
		float num2 = this.m_originalPosition.x;
		float num3 = this.m_originalPosition.y;
		float num4 = this.m_originalPosition.z;
		if (this.m_DriftPositionAxisX)
		{
			num2 = Mathf.Sin(num + this.m_posSeedX + Mathf.Cos(num * this.m_posOffsetX)) * this.m_DriftPositionAmount;
		}
		if (this.m_DriftPositionAxisY)
		{
			num3 = Mathf.Sin(num + this.m_posSeedY + Mathf.Cos(num * this.m_posOffsetY)) * this.m_DriftPositionAmount;
		}
		if (this.m_DriftPositionAxisZ)
		{
			num4 = Mathf.Sin(num + this.m_posSeedZ + Mathf.Cos(num * this.m_posOffsetZ)) * this.m_DriftPositionAmount;
		}
		this.m_newPosition.x = this.m_originalPosition.x + num2;
		this.m_newPosition.y = this.m_originalPosition.y + num3;
		this.m_newPosition.z = this.m_originalPosition.z + num4;
		if (this.m_blend < 1f)
		{
			base.transform.localPosition = Vector3.Lerp(this.m_originalPosition, this.m_newPosition, this.m_blend);
			this.m_blend += Time.deltaTime * this.m_DriftSpeed;
			return;
		}
		base.transform.localPosition = this.m_newPosition;
	}

	// Token: 0x0600911D RID: 37149 RVA: 0x002F10CE File Offset: 0x002EF2CE
	private IEnumerator BlendOut()
	{
		this.m_blendOut = true;
		this.m_blend = 0f;
		while (this.m_blend < 1f)
		{
			base.transform.localPosition = Vector3.Lerp(this.m_newPosition, this.m_originalPosition, this.m_blend);
			this.m_blend += Time.deltaTime * this.m_DriftSpeed;
			yield return null;
		}
		this.m_blend = 0f;
		this.m_blendOut = false;
		yield break;
	}

	// Token: 0x040079E4 RID: 31204
	public float m_DriftPositionAmount = 1f;

	// Token: 0x040079E5 RID: 31205
	public bool m_DriftPositionAxisX = true;

	// Token: 0x040079E6 RID: 31206
	public bool m_DriftPositionAxisY = true;

	// Token: 0x040079E7 RID: 31207
	public bool m_DriftPositionAxisZ = true;

	// Token: 0x040079E8 RID: 31208
	public float m_DriftSpeed = 0.1f;

	// Token: 0x040079E9 RID: 31209
	private Vector3 m_originalPosition;

	// Token: 0x040079EA RID: 31210
	private Vector3 m_newPosition;

	// Token: 0x040079EB RID: 31211
	private float m_posSeedX;

	// Token: 0x040079EC RID: 31212
	private float m_posSeedY;

	// Token: 0x040079ED RID: 31213
	private float m_posSeedZ;

	// Token: 0x040079EE RID: 31214
	private float m_posOffsetX;

	// Token: 0x040079EF RID: 31215
	private float m_posOffsetY;

	// Token: 0x040079F0 RID: 31216
	private float m_posOffsetZ;

	// Token: 0x040079F1 RID: 31217
	private float m_blend;

	// Token: 0x040079F2 RID: 31218
	private bool m_blendOut;
}
