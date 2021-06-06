using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns if additional layers affects the mass center")]
	public class GetAnimatorLayersAffectMassCenter : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[ActionSection("Results")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("If true, additional layers affects the mass center")]
		public FsmBool affectMassCenter;

		[Tooltip("Event send if additional layers affects the mass center")]
		public FsmEvent affectMassCenterEvent;

		[Tooltip("Event send if additional layers do no affects the mass center")]
		public FsmEvent doNotAffectMassCenterEvent;

		private Animator _animator;

		public override void Reset()
		{
			gameObject = null;
			affectMassCenter = null;
			affectMassCenterEvent = null;
			doNotAffectMassCenterEvent = null;
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
			CheckAffectMassCenter();
			Finish();
		}

		private void CheckAffectMassCenter()
		{
			if (!(_animator == null))
			{
				bool layersAffectMassCenter = _animator.layersAffectMassCenter;
				affectMassCenter.Value = layersAffectMassCenter;
				if (layersAffectMassCenter)
				{
					base.Fsm.Event(affectMassCenterEvent);
				}
				else
				{
					base.Fsm.Event(doNotAffectMassCenterEvent);
				}
			}
		}
	}
}
