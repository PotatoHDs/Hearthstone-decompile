using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns true if the current rig is humanoid, false if it is generic. Can also sends events")]
	public class GetAnimatorIsHuman : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("True if the current rig is humanoid, False if it is generic")]
		public FsmBool isHuman;

		[Tooltip("Event send if rig is humanoid")]
		public FsmEvent isHumanEvent;

		[Tooltip("Event send if rig is generic")]
		public FsmEvent isGenericEvent;

		private Animator _animator;

		public override void Reset()
		{
			gameObject = null;
			isHuman = null;
			isHumanEvent = null;
			isGenericEvent = null;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget == null)
			{
				Finish();
				return;
			}
			_animator = ownerDefaultTarget.GetComponent<Animator>();
			if (_animator == null)
			{
				Finish();
				return;
			}
			DoCheckIsHuman();
			Finish();
		}

		private void DoCheckIsHuman()
		{
			if (!(_animator == null))
			{
				bool flag = _animator.isHuman;
				if (!isHuman.IsNone)
				{
					isHuman.Value = flag;
				}
				if (flag)
				{
					base.Fsm.Event(isHumanEvent);
				}
				else
				{
					base.Fsm.Event(isGenericEvent);
				}
			}
		}
	}
}
