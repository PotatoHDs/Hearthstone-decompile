using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Rewinds the named animation.")]
	public class RewindAnimation : BaseAnimationAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Animation)]
		public FsmString animName;

		public override void Reset()
		{
			gameObject = null;
			animName = null;
		}

		public override void OnEnter()
		{
			DoRewindAnimation();
			Finish();
		}

		private void DoRewindAnimation()
		{
			if (!string.IsNullOrEmpty(animName.Value))
			{
				GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
				if (UpdateCache(ownerDefaultTarget))
				{
					base.animation.Rewind(animName.Value);
				}
			}
		}
	}
}
