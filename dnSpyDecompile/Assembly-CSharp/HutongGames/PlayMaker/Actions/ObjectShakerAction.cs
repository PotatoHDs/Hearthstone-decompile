using System;
using Hearthstone.FX;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F58 RID: 3928
	[ActionCategory("Pegasus")]
	[Tooltip("Shakes an object for a set duration.")]
	public class ObjectShakerAction : FsmStateAction
	{
		// Token: 0x0600ACF0 RID: 44272 RVA: 0x0035F4D8 File Offset: 0x0035D6D8
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_Position = new FsmVector3
			{
				UseVariable = false,
				Value = Vector3.one
			};
			this.m_Rotation = new FsmVector3
			{
				UseVariable = false,
				Value = Vector3.zero
			};
			this.m_Space = Space.Self;
			this.m_Falloff = new FsmAnimationCurve
			{
				curve = new AnimationCurve(new Keyframe[]
				{
					new Keyframe(0f, 1f),
					new Keyframe(1f, 0f)
				})
			};
			this.m_Duration = new FsmFloat
			{
				UseVariable = false,
				Value = 0.25f
			};
			this.m_Interval = new FsmFloat
			{
				UseVariable = false,
				Value = 0.05f
			};
			this.m_TweenType = ObjectShaker.TweenType.DoubleSine;
		}

		// Token: 0x0600ACF1 RID: 44273 RVA: 0x0035F5B8 File Offset: 0x0035D7B8
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget != null)
			{
				ObjectShaker.Shake(ownerDefaultTarget, this.m_Position.Value, this.m_Rotation.Value, this.m_Space, this.m_Falloff.curve, this.m_Duration.Value, this.m_Interval.Value, this.m_TweenType, false);
			}
			base.Finish();
		}

		// Token: 0x040093CF RID: 37839
		[RequiredField]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x040093D0 RID: 37840
		[Tooltip("The shake's base translation.")]
		public FsmVector3 m_Position;

		// Token: 0x040093D1 RID: 37841
		[Tooltip("The shake's base rotation.")]
		public FsmVector3 m_Rotation;

		// Token: 0x040093D2 RID: 37842
		[Tooltip("Whether movement is in local or world space.")]
		public Space m_Space;

		// Token: 0x040093D3 RID: 37843
		[Tooltip("The curve by which to scale movement over time.")]
		public FsmAnimationCurve m_Falloff;

		// Token: 0x040093D4 RID: 37844
		[Tooltip("The shake's duration.\n\n(Note that the shake does not use the PlayMaker action's duration.)")]
		public FsmFloat m_Duration;

		// Token: 0x040093D5 RID: 37845
		[Tooltip("The time between individual bounces.")]
		public FsmFloat m_Interval;

		// Token: 0x040093D6 RID: 37846
		[Tooltip("The tween type for each bounce.")]
		public ObjectShaker.TweenType m_TweenType = ObjectShaker.TweenType.DoubleSine;
	}
}
