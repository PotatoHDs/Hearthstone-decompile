using Hearthstone.FX;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Shakes an object for a set duration.")]
	public class ObjectShakerAction : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault m_GameObject;

		[Tooltip("The shake's base translation.")]
		public FsmVector3 m_Position;

		[Tooltip("The shake's base rotation.")]
		public FsmVector3 m_Rotation;

		[Tooltip("Whether movement is in local or world space.")]
		public Space m_Space;

		[Tooltip("The curve by which to scale movement over time.")]
		public FsmAnimationCurve m_Falloff;

		[Tooltip("The shake's duration.\n\n(Note that the shake does not use the PlayMaker action's duration.)")]
		public FsmFloat m_Duration;

		[Tooltip("The time between individual bounces.")]
		public FsmFloat m_Interval;

		[Tooltip("The tween type for each bounce.")]
		public ObjectShaker.TweenType m_TweenType = ObjectShaker.TweenType.DoubleSine;

		public override void Reset()
		{
			m_GameObject = null;
			m_Position = new FsmVector3
			{
				UseVariable = false,
				Value = Vector3.one
			};
			m_Rotation = new FsmVector3
			{
				UseVariable = false,
				Value = Vector3.zero
			};
			m_Space = Space.Self;
			m_Falloff = new FsmAnimationCurve
			{
				curve = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 0f))
			};
			m_Duration = new FsmFloat
			{
				UseVariable = false,
				Value = 0.25f
			};
			m_Interval = new FsmFloat
			{
				UseVariable = false,
				Value = 0.05f
			};
			m_TweenType = ObjectShaker.TweenType.DoubleSine;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (ownerDefaultTarget != null)
			{
				ObjectShaker.Shake(ownerDefaultTarget, m_Position.Value, m_Rotation.Value, m_Space, m_Falloff.curve, m_Duration.Value, m_Interval.Value, m_TweenType);
			}
			Finish();
		}
	}
}
