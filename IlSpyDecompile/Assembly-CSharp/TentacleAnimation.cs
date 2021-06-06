using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TentacleAnimation : MonoBehaviour
{
	private const int RANDOM_INIT_COUNT = 5;

	public float m_MaxAngle = 45f;

	public float m_AnimSpeed = 0.5f;

	public float m_Secondary = 10f;

	public float m_Smooth = 3f;

	[Range(0f, 1f)]
	public float m_X_Intensity = 1f;

	[Range(0f, 1f)]
	public float m_Y_Intensity = 1f;

	[Range(0f, 1f)]
	public float m_Z_Intensity = 1f;

	public AnimationCurve m_IntensityCurve;

	public List<Transform> m_Bones;

	public List<Transform> m_ControlBones;

	private float[] m_intensityValues;

	private float[] m_angleX;

	private float[] m_angleY;

	private float[] m_angleZ;

	private float[] m_velocityX;

	private float[] m_velocityY;

	private float[] m_velocityZ;

	private float[] m_lastX;

	private float[] m_lastY;

	private float[] m_lastZ;

	private Quaternion[] m_orgRotation;

	private float m_jointStep;

	private float m_secondaryAnim = 0.05f;

	private float m_smoothing = 0.3f;

	private float m_seedX;

	private float m_seedY;

	private float m_seedZ;

	private float m_timeSeed;

	private int[] m_randomNumbers;

	private int m_randomCount;

	private void Start()
	{
		Init();
	}

	private void Update()
	{
		if (m_Bones == null)
		{
			return;
		}
		CalculateBoneAngles();
		if (m_ControlBones.Count > 0)
		{
			for (int i = 0; i < m_Bones.Count; i++)
			{
				m_Bones[i].localRotation = m_ControlBones[i].localRotation;
				m_Bones[i].localPosition = m_ControlBones[i].localPosition;
				m_Bones[i].localScale = m_ControlBones[i].localScale;
				m_Bones[i].Rotate(m_angleX[i], m_angleY[i], m_angleZ[i]);
			}
		}
		else
		{
			for (int j = 0; j < m_Bones.Count; j++)
			{
				m_Bones[j].rotation = m_orgRotation[j];
				m_Bones[j].Rotate(m_angleX[j], m_angleY[j], m_angleZ[j]);
			}
		}
		m_secondaryAnim = m_Secondary * 0.01f;
		m_smoothing = m_Smooth * 0.1f;
	}

	private void Init()
	{
		if (m_Bones != null)
		{
			if (m_IntensityCurve == null || m_IntensityCurve.length < 1)
			{
				m_IntensityCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
			}
			m_randomNumbers = new int[513];
			for (int i = 0; i < 5; i++)
			{
				m_randomNumbers[i] = Random.Range(0, 255);
			}
			m_randomCount = 4;
			m_secondaryAnim = m_Secondary * 0.01f;
			m_smoothing = 0f;
			m_jointStep = 1f / (float)m_Bones.Count;
			m_timeSeed = Random.Range(1, 100);
			m_seedX = Random.Range(1, 10);
			m_seedY = Random.Range(1, 10);
			m_seedZ = Random.Range(1, 10);
			m_intensityValues = new float[m_Bones.Count];
			m_angleX = new float[m_Bones.Count];
			m_angleY = new float[m_Bones.Count];
			m_angleZ = new float[m_Bones.Count];
			m_velocityX = new float[m_Bones.Count];
			m_velocityY = new float[m_Bones.Count];
			m_velocityZ = new float[m_Bones.Count];
			m_lastX = new float[m_Bones.Count];
			m_lastY = new float[m_Bones.Count];
			m_lastZ = new float[m_Bones.Count];
			InitBones();
		}
	}

	private void InitBones()
	{
		if (m_ControlBones.Count < m_Bones.Count)
		{
			m_orgRotation = new Quaternion[m_Bones.Count];
			for (int i = 0; i < m_Bones.Count; i++)
			{
				m_orgRotation[i] = m_Bones[i].rotation;
			}
		}
		else
		{
			for (int j = 0; j < m_Bones.Count; j++)
			{
				m_Bones[j].rotation = m_ControlBones[j].rotation;
			}
		}
		for (int k = 0; k < m_Bones.Count; k++)
		{
			m_lastX[k] = m_Bones[k].eulerAngles.x;
			m_lastY[k] = m_Bones[k].eulerAngles.y;
			m_lastZ[k] = m_Bones[k].eulerAngles.z;
			m_velocityX[k] = 0f;
			m_velocityY[k] = 0f;
			m_velocityZ[k] = 0f;
			m_intensityValues[k] = m_IntensityCurve.Evaluate((float)k * m_jointStep);
		}
	}

	private void CalculateBoneAngles()
	{
		for (int i = 0; i < m_Bones.Count; i++)
		{
			m_angleX[i] = CalculateAngle(i, m_lastX, m_velocityX, m_seedX) * m_X_Intensity;
			m_angleY[i] = CalculateAngle(i, m_lastY, m_velocityY, m_seedY) * m_Y_Intensity;
			m_angleZ[i] = CalculateAngle(i, m_lastZ, m_velocityZ, m_seedZ) * m_Z_Intensity;
		}
	}

	private float CalculateAngle(int index, float[] last, float[] velocity, float offset)
	{
		float num = Time.timeSinceLevelLoad * m_AnimSpeed + m_timeSeed - (float)index * m_secondaryAnim;
		float target = Simplex1D(num + offset) * m_intensityValues[index] * m_MaxAngle;
		float currentVelocity = velocity[index];
		target = Mathf.SmoothDamp(last[index], target, ref currentVelocity, m_smoothing);
		velocity[index] = currentVelocity;
		last[index] = target;
		return target;
	}

	private float Simplex1D(float x)
	{
		int num = (int)Mathf.Floor(x);
		int num2 = num + 1;
		float num3 = x - (float)num;
		float num4 = num3 - 1f;
		float num5 = Mathf.Pow(1f - num3 * num3, 4f) * Interpolate(GetRandomNumber(num & 0xFF), num3);
		float num6 = Mathf.Pow(1f - num4 * num4, 4f) * Interpolate(GetRandomNumber(num2 & 0xFF), num4);
		return (num5 + num6) * 0.395f;
	}

	private float Interpolate(int h, float x)
	{
		h &= 0xF;
		float num = 1f + (float)(h & 7);
		if (((uint)h & 8u) != 0)
		{
			return (0f - num) * x;
		}
		return num * x;
	}

	private int GetRandomNumber(int index)
	{
		if (index > m_randomCount)
		{
			for (int i = m_randomCount + 1; i <= index + 1; i++)
			{
				m_randomNumbers[i] = Random.Range(0, 255);
			}
			m_randomCount = index + 1;
		}
		return m_randomNumbers[index];
	}
}
