using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Sets the Blend Weight of an Animation. Check Every Frame to update the weight continuously, e.g., if you're manipulating a variable that controls the weight.")]
	public class SetAnimationWeight : BaseAnimationAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Animation)]
		public FsmString animName;

		public FsmFloat weight = 1f;

		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			animName = null;
			weight = 1f;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetAnimationWeight((gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : gameObject.GameObject.Value);
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetAnimationWeight((gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : gameObject.GameObject.Value);
		}

		private void DoSetAnimationWeight(GameObject go)
		{
			if (UpdateCache(go))
			{
				AnimationState animationState = base.animation[animName.Value];
				if (animationState == null)
				{
					LogWarning("Missing animation: " + animName.Value);
				}
				else
				{
					animationState.weight = weight.Value;
				}
			}
		}
	}
}
