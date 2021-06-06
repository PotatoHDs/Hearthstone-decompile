using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns the Animator controller layer count")]
	public class GetAnimatorLayerCount : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[ActionSection("Results")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Animator controller layer count")]
		public FsmInt layerCount;

		private Animator _animator;

		public override void Reset()
		{
			gameObject = null;
			layerCount = null;
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
			DoGetLayerCount();
			Finish();
		}

		private void DoGetLayerCount()
		{
			if (!(_animator == null))
			{
				layerCount.Value = _animator.layerCount;
			}
		}
	}
}
