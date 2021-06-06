using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Check the active Transition name on a specified layer. Format is 'CURRENT_STATE -> NEXT_STATE'.")]
	public class GetAnimatorCurrentTransitionInfoIsName : FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;

		[Tooltip("The name to check the transition against.")]
		public FsmString name;

		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("True if name matches")]
		public FsmBool nameMatch;

		[Tooltip("Event send if name matches")]
		public FsmEvent nameMatchEvent;

		[Tooltip("Event send if name doesn't match")]
		public FsmEvent nameDoNotMatchEvent;

		private Animator _animator;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			layerIndex = null;
			name = null;
			nameMatch = null;
			nameMatchEvent = null;
			nameDoNotMatchEvent = null;
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
			IsName();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			IsName();
		}

		private void IsName()
		{
			if (_animator != null)
			{
				if (_animator.GetAnimatorTransitionInfo(layerIndex.Value).IsName(name.Value))
				{
					nameMatch.Value = true;
					base.Fsm.Event(nameMatchEvent);
				}
				else
				{
					nameMatch.Value = false;
					base.Fsm.Event(nameDoNotMatchEvent);
				}
			}
		}
	}
}
