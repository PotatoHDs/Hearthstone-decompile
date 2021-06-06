using System;
using UnityEngine;

namespace Hearthstone.Timeline
{
	// Token: 0x020010F1 RID: 4337
	public class CameraShaker2Helper : TimelineEffectHelper
	{
		// Token: 0x0600BE3D RID: 48701 RVA: 0x003A0588 File Offset: 0x0039E788
		protected override void Initialize(params object[] values)
		{
			if (values.Length < 3)
			{
				return;
			}
			Vector3 vector = (Vector3)values[0];
			this.m_amount = new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
			this.m_falloff = (AnimationCurve)values[1];
			this.m_interval = Convert.ToSingle(values[2]);
			if (!base.ReceivedOriginalValues)
			{
				this.m_originalPosition = base.transform.position;
			}
			this.m_positions = new Vector3[30];
			float[] array = new float[30];
			for (int i = 0; i < this.m_positions.Length; i++)
			{
				array[i] = ((i == 0) ? UnityEngine.Random.Range(0f, 6.283f) : UnityEngine.Random.Range(array[i - 1] + 1.745f, array[i - 1] + 4.538f));
				float x = Mathf.Cos(array[i]) * this.m_amount.x;
				float y = Mathf.Sin(array[i]) * this.m_amount.y;
				this.m_positions[i] = new Vector3
				{
					x = x,
					y = y,
					z = ((i == 0 || this.m_positions[i - 1].z < 0f) ? UnityEngine.Random.Range(0f, this.m_amount.z) : UnityEngine.Random.Range(-this.m_amount.z, 0f))
				};
			}
		}

		// Token: 0x0600BE3E RID: 48702 RVA: 0x003A0714 File Offset: 0x0039E914
		protected override void CopyOriginalValuesFrom<T>(T other)
		{
			CameraShaker2Helper cameraShaker2Helper = other as CameraShaker2Helper;
			this.m_originalPosition = cameraShaker2Helper.m_originalPosition;
		}

		// Token: 0x0600BE3F RID: 48703 RVA: 0x00003BE8 File Offset: 0x00001DE8
		protected override void OnKill()
		{
		}

		// Token: 0x0600BE40 RID: 48704 RVA: 0x003A0739 File Offset: 0x0039E939
		protected override void ResetTarget()
		{
			base.transform.position = this.m_originalPosition;
		}

		// Token: 0x0600BE41 RID: 48705 RVA: 0x003A074C File Offset: 0x0039E94C
		protected override void UpdateTarget(float normalizedTime)
		{
			float num = (this.m_interval > 0f) ? (base.Duration / this.m_interval) : 500f;
			float num2 = normalizedTime * num;
			int num3 = Mathf.FloorToInt(num2);
			float num4 = Mathf.Cos((num2 - (float)num3) * 3.1415927f);
			num4 *= 0.5f;
			num4 += 0.5f;
			num4 = 1f - num4;
			num4 = Mathf.Cos(num4 * 3.1415927f);
			num4 *= 0.5f;
			num4 += 0.5f;
			num4 = 1f - num4;
			Vector3 vector = Vector3.Lerp(this.m_positions[num3 % this.m_positions.Length], this.m_positions[(num3 + 1) % this.m_positions.Length], num4);
			vector *= this.m_falloff.Evaluate(normalizedTime);
			base.transform.position = this.m_originalPosition + base.transform.right * vector.x + base.transform.up * vector.y + base.transform.forward * vector.z;
		}

		// Token: 0x04009AF6 RID: 39670
		private AnimationCurve m_falloff;

		// Token: 0x04009AF7 RID: 39671
		private Vector3 m_amount;

		// Token: 0x04009AF8 RID: 39672
		private float m_interval;

		// Token: 0x04009AF9 RID: 39673
		private Vector3 m_originalPosition;

		// Token: 0x04009AFA RID: 39674
		private Vector3[] m_positions = new Vector3[0];
	}
}
