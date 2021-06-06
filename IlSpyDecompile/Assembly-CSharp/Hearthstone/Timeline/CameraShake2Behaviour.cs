using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	[Serializable]
	public class CameraShake2Behaviour : TimelineEffectBehaviour<CameraShaker2Helper>
	{
		[SerializeField]
		[Tooltip("Max distance the shake can move (scaled by the intensity curve).")]
		private Vector3 m_Amount = new Vector3(0.1f, 0.1f, 0f);

		[SerializeField]
		[Tooltip("Build or falloff over time.")]
		private AnimationCurve m_IntensityCurve = new AnimationCurve(new Keyframe(0f, 1f, 0f, -2f), new Keyframe(1f, 0f, 0f, 0f));

		[SerializeField]
		[Range(0f, 0.25f)]
		[Tooltip("Time between shakes (zero for every frame).")]
		private float m_interval = 0.075f;

		protected override object[] GetHelperInitializationData()
		{
			return new object[3] { m_Amount, m_IntensityCurve, m_interval };
		}

		protected override void InitializeFrame(Playable playable, FrameData info, object playerData)
		{
			if (!Application.isPlaying && playerData != null && (base.Helper == null || (playerData as Camera).gameObject != base.Helper.gameObject))
			{
				base.Helper?.Kill();
				SpawnHelper((playerData as Camera).gameObject, (float)playable.GetDuration());
			}
		}

		protected override void UpdateFrame(Playable playable, FrameData info, object playerData, float normalizedTime)
		{
			base.Helper.UpdateEffect((float)playable.GetTime(), !playable.GetGraph().IsPlaying());
		}
	}
}
