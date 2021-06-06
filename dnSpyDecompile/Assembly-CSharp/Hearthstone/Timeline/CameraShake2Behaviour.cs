using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	// Token: 0x020010F0 RID: 4336
	[Serializable]
	public class CameraShake2Behaviour : TimelineEffectBehaviour<CameraShaker2Helper>
	{
		// Token: 0x0600BE39 RID: 48697 RVA: 0x003A0433 File Offset: 0x0039E633
		protected override object[] GetHelperInitializationData()
		{
			return new object[]
			{
				this.m_Amount,
				this.m_IntensityCurve,
				this.m_interval
			};
		}

		// Token: 0x0600BE3A RID: 48698 RVA: 0x003A0460 File Offset: 0x0039E660
		protected override void InitializeFrame(Playable playable, FrameData info, object playerData)
		{
			if (!Application.isPlaying && playerData != null && (base.Helper == null || (playerData as Camera).gameObject != base.Helper.gameObject))
			{
				CameraShaker2Helper helper = base.Helper;
				if (helper != null)
				{
					helper.Kill();
				}
				base.SpawnHelper((playerData as Camera).gameObject, (float)playable.GetDuration<Playable>());
			}
		}

		// Token: 0x0600BE3B RID: 48699 RVA: 0x003A04CC File Offset: 0x0039E6CC
		protected override void UpdateFrame(Playable playable, FrameData info, object playerData, float normalizedTime)
		{
			base.Helper.UpdateEffect((float)playable.GetTime<Playable>(), !playable.GetGraph<Playable>().IsPlaying());
		}

		// Token: 0x04009AF3 RID: 39667
		[SerializeField]
		[Tooltip("Max distance the shake can move (scaled by the intensity curve).")]
		private Vector3 m_Amount = new Vector3(0.1f, 0.1f, 0f);

		// Token: 0x04009AF4 RID: 39668
		[SerializeField]
		[Tooltip("Build or falloff over time.")]
		private AnimationCurve m_IntensityCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f, 0f, -2f),
			new Keyframe(1f, 0f, 0f, 0f)
		});

		// Token: 0x04009AF5 RID: 39669
		[SerializeField]
		[Range(0f, 0.25f)]
		[Tooltip("Time between shakes (zero for every frame).")]
		private float m_interval = 0.075f;
	}
}
