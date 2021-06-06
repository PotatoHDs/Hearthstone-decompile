using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	// Token: 0x020010ED RID: 4333
	[Obsolete("Use CameraShaker2 instead.", false)]
	[Serializable]
	public class CameraShakeBehaviour : PlayableBehaviour
	{
		// Token: 0x0600BE33 RID: 48691 RVA: 0x003A02E5 File Offset: 0x0039E4E5
		public override void OnGraphStart(Playable playable)
		{
			this.m_needsReset = true;
			if (Application.isPlaying)
			{
				this.m_camera = Camera.main;
			}
		}

		// Token: 0x0600BE34 RID: 48692 RVA: 0x003A0300 File Offset: 0x0039E500
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			if (!Application.isPlaying)
			{
				this.m_camera = (playerData as Camera);
			}
			if (this.m_camera != null && this.m_IntensityCurve != null)
			{
				if (this.m_needsReset)
				{
					this.m_needsReset = false;
					this.m_cameraOrigin = this.m_camera.transform.position;
				}
				float time = (float)playable.GetTime<Playable>() / (float)playable.GetDuration<Playable>();
				float num = this.m_IntensityCurve.Evaluate(time);
				Vector3 b = default(Vector3);
				b.x = UnityEngine.Random.Range(-this.m_Amount.x * num, this.m_Amount.x * num);
				b.y = UnityEngine.Random.Range(-this.m_Amount.y * num, this.m_Amount.y * num);
				b.z = UnityEngine.Random.Range(-this.m_Amount.z * num, this.m_Amount.z * num);
				this.m_camera.transform.position = this.m_cameraOrigin + b;
			}
		}

		// Token: 0x04009AED RID: 39661
		public Vector3 m_Amount;

		// Token: 0x04009AEE RID: 39662
		public AnimationCurve m_IntensityCurve;

		// Token: 0x04009AEF RID: 39663
		public Vector3 m_cameraOrigin;

		// Token: 0x04009AF0 RID: 39664
		private bool m_needsReset;

		// Token: 0x04009AF1 RID: 39665
		private Camera m_camera;
	}
}
