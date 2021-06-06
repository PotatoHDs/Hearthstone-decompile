using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Sets the Parent of a Game Object.")]
	public class SetParent : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to parent.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The new parent for the Game Object.")]
		public FsmGameObject parent;

		[Tooltip("Set the local position to 0,0,0 after parenting.")]
		public FsmBool resetLocalPosition;

		[Tooltip("Set the local rotation to 0,0,0 after parenting.")]
		public FsmBool resetLocalRotation;

		[Tooltip("Keep the local position of the child object, instead of keeping the world position.")]
		public FsmBool localPositionStays;

		[Tooltip("Sets the game object's layer to the same as the parent's if true")]
		public FsmBool changeToParentLayer;

		public override void Reset()
		{
			gameObject = null;
			parent = null;
			resetLocalPosition = null;
			resetLocalRotation = null;
			changeToParentLayer = null;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				ownerDefaultTarget.transform.SetParent((parent.Value == null) ? null : parent.Value.transform, !localPositionStays.Value);
				if (changeToParentLayer.Value)
				{
					int layer = parent.Value.layer;
					SceneUtils.SetLayer(ownerDefaultTarget, layer);
				}
				if (resetLocalPosition.Value)
				{
					ownerDefaultTarget.transform.localPosition = Vector3.zero;
				}
				if (resetLocalRotation.Value)
				{
					ownerDefaultTarget.transform.localRotation = Quaternion.identity;
				}
			}
			Finish();
		}
	}
}
