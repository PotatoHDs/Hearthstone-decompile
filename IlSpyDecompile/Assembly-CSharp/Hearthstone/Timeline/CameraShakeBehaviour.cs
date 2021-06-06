using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	[Serializable]
	[Obsolete("Use CameraShaker2 instead.", false)]
	public class CameraShakeBehaviour : PlayableBehaviour
	{
		public Vector3 m_Amount;

		public AnimationCurve m_IntensityCurve;

		public Vector3 m_cameraOrigin;

		private bool m_needsReset;

		private Camera m_camera;

		public override void OnGraphStart(Playable playable)
		{
			m_needsReset = true;
			if (Application.isPlaying)
			{
				m_camera = Camera.main;
			}
		}

		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			if (!Application.isPlaying)
			{
				m_camera = playerData as Camera;
			}
			if (m_camera != null && m_IntensityCurve != null)
			{
				if (m_needsReset)
				{
					m_needsReset = false;
					m_cameraOrigin = m_camera.transform.position;
				}
				float time = (float)playable.GetTime() / (float)playable.GetDuration();
				float num = m_IntensityCurve.Evaluate(time);
				Vector3 vector = default(Vector3);
				vector.x = UnityEngine.Random.Range((0f - m_Amount.x) * num, m_Amount.x * num);
				vector.y = UnityEngine.Random.Range((0f - m_Amount.y) * num, m_Amount.y * num);
				vector.z = UnityEngine.Random.Range((0f - m_Amount.z) * num, m_Amount.z * num);
				m_camera.transform.position = m_cameraOrigin + vector;
			}
		}
	}
}
