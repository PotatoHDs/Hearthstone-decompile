using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Interrupts the automatic target matching. CompleteMatch will make the gameobject match the target completely at the next frame.")]
	public class AnimatorInterruptMatchTarget : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Will make the gameobject match the target completely at the next frame")]
		public FsmBool completeMatch;

		public override void Reset()
		{
			gameObject = null;
			completeMatch = true;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget == null)
			{
				Finish();
				return;
			}
			Animator component = ownerDefaultTarget.GetComponent<Animator>();
			if (component != null)
			{
				component.InterruptMatchTarget(completeMatch.Value);
			}
			Finish();
		}
	}
}
