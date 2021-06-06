using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the layer's current weight")]
	public class GetAnimatorLayerWeight : FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;

		[ActionSection("Results")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The layer's current weight")]
		public FsmFloat layerWeight;

		private Animator _animator;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			layerIndex = null;
			layerWeight = null;
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
			GetLayerWeight();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			GetLayerWeight();
		}

		private void GetLayerWeight()
		{
			if (_animator != null)
			{
				layerWeight.Value = _animator.GetLayerWeight(layerIndex.Value);
			}
		}
	}
}
