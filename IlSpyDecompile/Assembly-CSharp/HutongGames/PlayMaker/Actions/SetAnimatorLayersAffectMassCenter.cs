using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("If true, additional layers affects the mass center")]
	public class SetAnimatorLayersAffectMassCenter : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[Tooltip("If true, additional layers affects the mass center")]
		public FsmBool affectMassCenter;

		private Animator _animator;

		public override void Reset()
		{
			gameObject = null;
			affectMassCenter = null;
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
			SetAffectMassCenter();
			Finish();
		}

		private void SetAffectMassCenter()
		{
			if (!(_animator == null))
			{
				_animator.layersAffectMassCenter = affectMassCenter.Value;
			}
		}
	}
}
