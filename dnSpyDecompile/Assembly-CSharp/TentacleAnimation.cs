using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A9D RID: 2717
[ExecuteAlways]
public class TentacleAnimation : MonoBehaviour
{
	// Token: 0x06009109 RID: 37129 RVA: 0x002F05F0 File Offset: 0x002EE7F0
	private void Start()
	{
		this.Init();
	}

	// Token: 0x0600910A RID: 37130 RVA: 0x002F05F8 File Offset: 0x002EE7F8
	private void Update()
	{
		if (this.m_Bones == null)
		{
			return;
		}
		this.CalculateBoneAngles();
		if (this.m_ControlBones.Count > 0)
		{
			for (int i = 0; i < this.m_Bones.Count; i++)
			{
				this.m_Bones[i].localRotation = this.m_ControlBones[i].localRotation;
				this.m_Bones[i].localPosition = this.m_ControlBones[i].localPosition;
				this.m_Bones[i].localScale = this.m_ControlBones[i].localScale;
				this.m_Bones[i].Rotate(this.m_angleX[i], this.m_angleY[i], this.m_angleZ[i]);
			}
		}
		else
		{
			for (int j = 0; j < this.m_Bones.Count; j++)
			{
				this.m_Bones[j].rotation = this.m_orgRotation[j];
				this.m_Bones[j].Rotate(this.m_angleX[j], this.m_angleY[j], this.m_angleZ[j]);
			}
		}
		this.m_secondaryAnim = this.m_Secondary * 0.01f;
		this.m_smoothing = this.m_Smooth * 0.1f;
	}

	// Token: 0x0600910B RID: 37131 RVA: 0x002F0754 File Offset: 0x002EE954
	private void Init()
	{
		if (this.m_Bones == null)
		{
			return;
		}
		if (this.m_IntensityCurve == null || this.m_IntensityCurve.length < 1)
		{
			this.m_IntensityCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
		}
		this.m_randomNumbers = new int[513];
		for (int i = 0; i < 5; i++)
		{
			this.m_randomNumbers[i] = UnityEngine.Random.Range(0, 255);
		}
		this.m_randomCount = 4;
		this.m_secondaryAnim = this.m_Secondary * 0.01f;
		this.m_smoothing = 0f;
		this.m_jointStep = 1f / (float)this.m_Bones.Count;
		this.m_timeSeed = (float)UnityEngine.Random.Range(1, 100);
		this.m_seedX = (float)UnityEngine.Random.Range(1, 10);
		this.m_seedY = (float)UnityEngine.Random.Range(1, 10);
		this.m_seedZ = (float)UnityEngine.Random.Range(1, 10);
		this.m_intensityValues = new float[this.m_Bones.Count];
		this.m_angleX = new float[this.m_Bones.Count];
		this.m_angleY = new float[this.m_Bones.Count];
		this.m_angleZ = new float[this.m_Bones.Count];
		this.m_velocityX = new float[this.m_Bones.Count];
		this.m_velocityY = new float[this.m_Bones.Count];
		this.m_velocityZ = new float[this.m_Bones.Count];
		this.m_lastX = new float[this.m_Bones.Count];
		this.m_lastY = new float[this.m_Bones.Count];
		this.m_lastZ = new float[this.m_Bones.Count];
		this.InitBones();
	}

	// Token: 0x0600910C RID: 37132 RVA: 0x002F0928 File Offset: 0x002EEB28
	private void InitBones()
	{
		if (this.m_ControlBones.Count < this.m_Bones.Count)
		{
			this.m_orgRotation = new Quaternion[this.m_Bones.Count];
			for (int i = 0; i < this.m_Bones.Count; i++)
			{
				this.m_orgRotation[i] = this.m_Bones[i].rotation;
			}
		}
		else
		{
			for (int j = 0; j < this.m_Bones.Count; j++)
			{
				this.m_Bones[j].rotation = this.m_ControlBones[j].rotation;
			}
		}
		for (int k = 0; k < this.m_Bones.Count; k++)
		{
			this.m_lastX[k] = this.m_Bones[k].eulerAngles.x;
			this.m_lastY[k] = this.m_Bones[k].eulerAngles.y;
			this.m_lastZ[k] = this.m_Bones[k].eulerAngles.z;
			this.m_velocityX[k] = 0f;
			this.m_velocityY[k] = 0f;
			this.m_velocityZ[k] = 0f;
			this.m_intensityValues[k] = this.m_IntensityCurve.Evaluate((float)k * this.m_jointStep);
		}
	}

	// Token: 0x0600910D RID: 37133 RVA: 0x002F0A8C File Offset: 0x002EEC8C
	private void CalculateBoneAngles()
	{
		for (int i = 0; i < this.m_Bones.Count; i++)
		{
			this.m_angleX[i] = this.CalculateAngle(i, this.m_lastX, this.m_velocityX, this.m_seedX) * this.m_X_Intensity;
			this.m_angleY[i] = this.CalculateAngle(i, this.m_lastY, this.m_velocityY, this.m_seedY) * this.m_Y_Intensity;
			this.m_angleZ[i] = this.CalculateAngle(i, this.m_lastZ, this.m_velocityZ, this.m_seedZ) * this.m_Z_Intensity;
		}
	}

	// Token: 0x0600910E RID: 37134 RVA: 0x002F0B2C File Offset: 0x002EED2C
	private float CalculateAngle(int index, float[] last, float[] velocity, float offset)
	{
		float num = Time.timeSinceLevelLoad * this.m_AnimSpeed + this.m_timeSeed - (float)index * this.m_secondaryAnim;
		float num2 = this.Simplex1D(num + offset) * this.m_intensityValues[index] * this.m_MaxAngle;
		float num3 = velocity[index];
		num2 = Mathf.SmoothDamp(last[index], num2, ref num3, this.m_smoothing);
		velocity[index] = num3;
		last[index] = num2;
		return num2;
	}

	// Token: 0x0600910F RID: 37135 RVA: 0x002F0B94 File Offset: 0x002EED94
	private float Simplex1D(float x)
	{
		int num = (int)Mathf.Floor(x);
		int num2 = num + 1;
		float num3 = x - (float)num;
		float num4 = num3 - 1f;
		float num5 = Mathf.Pow(1f - num3 * num3, 4f) * this.Interpolate(this.GetRandomNumber(num & 255), num3);
		float num6 = Mathf.Pow(1f - num4 * num4, 4f) * this.Interpolate(this.GetRandomNumber(num2 & 255), num4);
		return (num5 + num6) * 0.395f;
	}

	// Token: 0x06009110 RID: 37136 RVA: 0x002F0C18 File Offset: 0x002EEE18
	private float Interpolate(int h, float x)
	{
		h &= 15;
		float num = 1f + (float)(h & 7);
		if ((h & 8) != 0)
		{
			return -num * x;
		}
		return num * x;
	}

	// Token: 0x06009111 RID: 37137 RVA: 0x002F0C44 File Offset: 0x002EEE44
	private int GetRandomNumber(int index)
	{
		if (index > this.m_randomCount)
		{
			for (int i = this.m_randomCount + 1; i <= index + 1; i++)
			{
				this.m_randomNumbers[i] = UnityEngine.Random.Range(0, 255);
			}
			this.m_randomCount = index + 1;
		}
		return this.m_randomNumbers[index];
	}

	// Token: 0x040079BC RID: 31164
	private const int RANDOM_INIT_COUNT = 5;

	// Token: 0x040079BD RID: 31165
	public float m_MaxAngle = 45f;

	// Token: 0x040079BE RID: 31166
	public float m_AnimSpeed = 0.5f;

	// Token: 0x040079BF RID: 31167
	public float m_Secondary = 10f;

	// Token: 0x040079C0 RID: 31168
	public float m_Smooth = 3f;

	// Token: 0x040079C1 RID: 31169
	[Range(0f, 1f)]
	public float m_X_Intensity = 1f;

	// Token: 0x040079C2 RID: 31170
	[Range(0f, 1f)]
	public float m_Y_Intensity = 1f;

	// Token: 0x040079C3 RID: 31171
	[Range(0f, 1f)]
	public float m_Z_Intensity = 1f;

	// Token: 0x040079C4 RID: 31172
	public AnimationCurve m_IntensityCurve;

	// Token: 0x040079C5 RID: 31173
	public List<Transform> m_Bones;

	// Token: 0x040079C6 RID: 31174
	public List<Transform> m_ControlBones;

	// Token: 0x040079C7 RID: 31175
	private float[] m_intensityValues;

	// Token: 0x040079C8 RID: 31176
	private float[] m_angleX;

	// Token: 0x040079C9 RID: 31177
	private float[] m_angleY;

	// Token: 0x040079CA RID: 31178
	private float[] m_angleZ;

	// Token: 0x040079CB RID: 31179
	private float[] m_velocityX;

	// Token: 0x040079CC RID: 31180
	private float[] m_velocityY;

	// Token: 0x040079CD RID: 31181
	private float[] m_velocityZ;

	// Token: 0x040079CE RID: 31182
	private float[] m_lastX;

	// Token: 0x040079CF RID: 31183
	private float[] m_lastY;

	// Token: 0x040079D0 RID: 31184
	private float[] m_lastZ;

	// Token: 0x040079D1 RID: 31185
	private Quaternion[] m_orgRotation;

	// Token: 0x040079D2 RID: 31186
	private float m_jointStep;

	// Token: 0x040079D3 RID: 31187
	private float m_secondaryAnim = 0.05f;

	// Token: 0x040079D4 RID: 31188
	private float m_smoothing = 0.3f;

	// Token: 0x040079D5 RID: 31189
	private float m_seedX;

	// Token: 0x040079D6 RID: 31190
	private float m_seedY;

	// Token: 0x040079D7 RID: 31191
	private float m_seedZ;

	// Token: 0x040079D8 RID: 31192
	private float m_timeSeed;

	// Token: 0x040079D9 RID: 31193
	private int[] m_randomNumbers;

	// Token: 0x040079DA RID: 31194
	private int m_randomCount;
}
