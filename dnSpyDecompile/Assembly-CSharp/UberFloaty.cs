using System;
using UnityEngine;

// Token: 0x02000AA4 RID: 2724
public class UberFloaty : MonoBehaviour
{
	// Token: 0x0600912D RID: 37165 RVA: 0x002F1666 File Offset: 0x002EF866
	private void Start()
	{
		this.Init();
	}

	// Token: 0x0600912E RID: 37166 RVA: 0x002F166E File Offset: 0x002EF86E
	private void OnEnable()
	{
		this.InitTransforms();
	}

	// Token: 0x0600912F RID: 37167 RVA: 0x002F1678 File Offset: 0x002EF878
	private void Update()
	{
		float num = Time.time * this.speed;
		Vector3 b;
		b.x = Mathf.Sin(num * this.m_interval.x + this.m_offset.x) * this.magnitude.x * this.m_interval.x;
		b.y = Mathf.Sin(num * this.m_interval.y + this.m_offset.y) * this.magnitude.y * this.m_interval.y;
		b.z = Mathf.Sin(num * this.m_interval.z + this.m_offset.z) * this.magnitude.z * this.m_interval.z;
		Vector3 vector = Vector3.Lerp(this.m_startingPosition, this.m_startingPosition + b, this.positionBlend);
		if (this.localSpace)
		{
			base.transform.localPosition = vector;
		}
		else
		{
			base.transform.position = vector;
		}
		Vector3 b2;
		b2.x = Mathf.Sin(num * this.m_rotationInterval.x + this.m_offset.x) * this.magnitudeRot.x * this.m_rotationInterval.x;
		b2.y = Mathf.Sin(num * this.m_rotationInterval.y + this.m_offset.y) * this.magnitudeRot.y * this.m_rotationInterval.y;
		b2.z = Mathf.Sin(num * this.m_rotationInterval.z + this.m_offset.z) * this.magnitudeRot.z * this.m_rotationInterval.z;
		base.transform.eulerAngles = Vector3.Lerp(this.m_startingRotation, this.m_startingRotation + b2, this.rotationBlend);
	}

	// Token: 0x06009130 RID: 37168 RVA: 0x002F1868 File Offset: 0x002EFA68
	private void InitTransforms()
	{
		if (this.localSpace)
		{
			this.m_startingPosition = base.transform.localPosition;
		}
		else
		{
			this.m_startingPosition = base.transform.position;
		}
		this.m_startingRotation = base.transform.eulerAngles;
	}

	// Token: 0x06009131 RID: 37169 RVA: 0x002F18A8 File Offset: 0x002EFAA8
	private void Init()
	{
		this.InitTransforms();
		this.m_interval.x = UnityEngine.Random.Range(this.frequencyMin, this.frequencyMax);
		this.m_interval.y = UnityEngine.Random.Range(this.frequencyMin, this.frequencyMax);
		this.m_interval.z = UnityEngine.Random.Range(this.frequencyMin, this.frequencyMax);
		this.m_offset.x = 0.5f * UnityEngine.Random.Range(-this.m_interval.x, this.m_interval.x);
		this.m_offset.y = 0.5f * UnityEngine.Random.Range(-this.m_interval.y, this.m_interval.y);
		this.m_offset.z = 0.5f * UnityEngine.Random.Range(-this.m_interval.z, this.m_interval.z);
		this.m_rotationInterval.x = UnityEngine.Random.Range(this.frequencyMinRot, this.frequencyMaxRot);
		this.m_rotationInterval.y = UnityEngine.Random.Range(this.frequencyMinRot, this.frequencyMaxRot);
		this.m_rotationInterval.z = UnityEngine.Random.Range(this.frequencyMinRot, this.frequencyMaxRot);
	}

	// Token: 0x040079FF RID: 31231
	public float speed = 1f;

	// Token: 0x04007A00 RID: 31232
	public float positionBlend = 1f;

	// Token: 0x04007A01 RID: 31233
	public float frequencyMin = 1f;

	// Token: 0x04007A02 RID: 31234
	public float frequencyMax = 3f;

	// Token: 0x04007A03 RID: 31235
	public bool localSpace = true;

	// Token: 0x04007A04 RID: 31236
	public Vector3 magnitude = new Vector3(0.001f, 0.001f, 0.001f);

	// Token: 0x04007A05 RID: 31237
	public float rotationBlend = 1f;

	// Token: 0x04007A06 RID: 31238
	public float frequencyMinRot = 1f;

	// Token: 0x04007A07 RID: 31239
	public float frequencyMaxRot = 3f;

	// Token: 0x04007A08 RID: 31240
	public Vector3 magnitudeRot = new Vector3(0f, 0f, 0f);

	// Token: 0x04007A09 RID: 31241
	private Vector3 m_interval;

	// Token: 0x04007A0A RID: 31242
	private Vector3 m_offset;

	// Token: 0x04007A0B RID: 31243
	private Vector3 m_rotationInterval;

	// Token: 0x04007A0C RID: 31244
	private Vector3 m_startingPosition;

	// Token: 0x04007A0D RID: 31245
	private Vector3 m_startingRotation;
}
