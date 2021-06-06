using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Sets the current Time of an Animation, Normalize time means 0 (start) to 1 (end); useful if you don't care about the exact time. Check Every Frame to update the time continuously.")]
	public class SetAnimationTime : BaseAnimationAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Animation)]
		public FsmString animName;

		public FsmFloat time;

		public bool normalized;

		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			animName = null;
			time = null;
			normalized = false;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetAnimationTime((gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : gameObject.GameObject.Value);
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetAnimationTime((gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : gameObject.GameObject.Value);
		}

		private void DoSetAnimationTime(GameObject go)
		{
			if (!UpdateCache(go))
			{
				return;
			}
			base.animation.Play(animName.Value);
			AnimationState animationState = base.animation[animName.Value];
			if (animationState == null)
			{
				LogWarning("Missing animation: " + animName.Value);
				return;
			}
			if (normalized)
			{
				animationState.normalizedTime = time.Value;
			}
			else
			{
				animationState.time = time.Value;
			}
			if (everyFrame)
			{
				animationState.speed = 0f;
			}
		}
	}
}
