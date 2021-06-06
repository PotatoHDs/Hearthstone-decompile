using System;
using UnityEngine;

namespace Hearthstone.Timeline
{
	public class CameraShaker2Helper : TimelineEffectHelper
	{
		private AnimationCurve m_falloff;

		private Vector3 m_amount;

		private float m_interval;

		private Vector3 m_originalPosition;

		private Vector3[] m_positions = new Vector3[0];

		protected override void Initialize(params object[] values)
		{
			if (values.Length >= 3)
			{
				Vector3 vector = (Vector3)values[0];
				m_amount = new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
				m_falloff = (AnimationCurve)values[1];
				m_interval = Convert.ToSingle(values[2]);
				if (!base.ReceivedOriginalValues)
				{
					m_originalPosition = base.transform.position;
				}
				m_positions = new Vector3[30];
				float[] array = new float[30];
				for (int i = 0; i < m_positions.Length; i++)
				{
					array[i] = ((i == 0) ? UnityEngine.Random.Range(0f, 6.283f) : UnityEngine.Random.Range(array[i - 1] + 1.745f, array[i - 1] + 4.538f));
					float x = Mathf.Cos(array[i]) * m_amount.x;
					float y = Mathf.Sin(array[i]) * m_amount.y;
					m_positions[i] = new Vector3
					{
						x = x,
						y = y,
						z = ((i == 0 || m_positions[i - 1].z < 0f) ? UnityEngine.Random.Range(0f, m_amount.z) : UnityEngine.Random.Range(0f - m_amount.z, 0f))
					};
				}
			}
		}

		protected override void CopyOriginalValuesFrom<T>(T other)
		{
			CameraShaker2Helper cameraShaker2Helper = other as CameraShaker2Helper;
			m_originalPosition = cameraShaker2Helper.m_originalPosition;
		}

		protected override void OnKill()
		{
		}

		protected override void ResetTarget()
		{
			base.transform.position = m_originalPosition;
		}

		protected override void UpdateTarget(float normalizedTime)
		{
			float num = ((m_interval > 0f) ? (base.Duration / m_interval) : 500f);
			float num2 = normalizedTime * num;
			int num3 = Mathf.FloorToInt(num2);
			float num4 = Mathf.Cos((num2 - (float)num3) * (float)Math.PI);
			num4 *= 0.5f;
			num4 += 0.5f;
			num4 = 1f - num4;
			num4 = Mathf.Cos(num4 * (float)Math.PI);
			num4 *= 0.5f;
			num4 += 0.5f;
			num4 = 1f - num4;
			Vector3 vector = Vector3.Lerp(m_positions[num3 % m_positions.Length], m_positions[(num3 + 1) % m_positions.Length], num4);
			vector *= m_falloff.Evaluate(normalizedTime);
			base.transform.position = m_originalPosition + base.transform.right * vector.x + base.transform.up * vector.y + base.transform.forward * vector.z;
		}
	}
}
