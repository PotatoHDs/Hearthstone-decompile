using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns the name of a layer from its index")]
	public class GetAnimatorLayerName : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The layer index")]
		public FsmInt layerIndex;

		[ActionSection("Results")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The layer name")]
		public FsmString layerName;

		private Animator _animator;

		public override void Reset()
		{
			gameObject = null;
			layerIndex = null;
			layerName = null;
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
			DoGetLayerName();
			Finish();
		}

		private void DoGetLayerName()
		{
			if (!(_animator == null))
			{
				layerName.Value = _animator.GetLayerName(layerIndex.Value);
			}
		}
	}
}
