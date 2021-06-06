using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Check the current State name on a specified layer, this is more than the layer name, it holds the current state as well.")]
	public class GetAnimatorCurrentStateInfoIsName : FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component and a PlayMakerAnimatorProxy component are required")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;

		[Tooltip("The name to check the layer against.")]
		public FsmString name;

		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("True if name matches")]
		public FsmBool isMatching;

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
			nameMatchEvent = null;
			nameDoNotMatchEvent = null;
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
				AnimatorStateInfo currentAnimatorStateInfo = _animator.GetCurrentAnimatorStateInfo(layerIndex.Value);
				if (!isMatching.IsNone)
				{
					isMatching.Value = currentAnimatorStateInfo.IsName(name.Value);
				}
				if (currentAnimatorStateInfo.IsName(name.Value))
				{
					base.Fsm.Event(nameMatchEvent);
				}
				else
				{
					base.Fsm.Event(nameDoNotMatchEvent);
				}
			}
		}
	}
}
