using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Does tag match the tag of the active state in the statemachine")]
	public class GetAnimatorCurrentStateInfoIsTag : FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;

		[Tooltip("The tag to check the layer against.")]
		public FsmString tag;

		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("True if tag matches")]
		public FsmBool tagMatch;

		[Tooltip("Event send if tag matches")]
		public FsmEvent tagMatchEvent;

		[Tooltip("Event send if tag matches")]
		public FsmEvent tagDoNotMatchEvent;

		private Animator _animator;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			layerIndex = null;
			tag = null;
			tagMatch = null;
			tagMatchEvent = null;
			tagDoNotMatchEvent = null;
			everyFrame = false;
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
			IsTag();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			IsTag();
		}

		private void IsTag()
		{
			if (_animator != null)
			{
				if (_animator.GetCurrentAnimatorStateInfo(layerIndex.Value).IsTag(tag.Value))
				{
					tagMatch.Value = true;
					base.Fsm.Event(tagMatchEvent);
				}
				else
				{
					tagMatch.Value = false;
					base.Fsm.Event(tagDoNotMatchEvent);
				}
			}
		}
	}
}
