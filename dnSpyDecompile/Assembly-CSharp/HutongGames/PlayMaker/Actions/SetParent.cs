using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DEF RID: 3567
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Sets the Parent of a Game Object.")]
	public class SetParent : FsmStateAction
	{
		// Token: 0x0600A679 RID: 42617 RVA: 0x0034949B File Offset: 0x0034769B
		public override void Reset()
		{
			this.gameObject = null;
			this.parent = null;
			this.resetLocalPosition = null;
			this.resetLocalRotation = null;
			this.changeToParentLayer = null;
		}

		// Token: 0x0600A67A RID: 42618 RVA: 0x003494C0 File Offset: 0x003476C0
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				ownerDefaultTarget.transform.SetParent((this.parent.Value == null) ? null : this.parent.Value.transform, !this.localPositionStays.Value);
				if (this.changeToParentLayer.Value)
				{
					int layer = this.parent.Value.layer;
					SceneUtils.SetLayer(ownerDefaultTarget, layer, null);
				}
				if (this.resetLocalPosition.Value)
				{
					ownerDefaultTarget.transform.localPosition = Vector3.zero;
				}
				if (this.resetLocalRotation.Value)
				{
					ownerDefaultTarget.transform.localRotation = Quaternion.identity;
				}
			}
			base.Finish();
		}

		// Token: 0x04008CF9 RID: 36089
		[RequiredField]
		[Tooltip("The Game Object to parent.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CFA RID: 36090
		[Tooltip("The new parent for the Game Object.")]
		public FsmGameObject parent;

		// Token: 0x04008CFB RID: 36091
		[Tooltip("Set the local position to 0,0,0 after parenting.")]
		public FsmBool resetLocalPosition;

		// Token: 0x04008CFC RID: 36092
		[Tooltip("Set the local rotation to 0,0,0 after parenting.")]
		public FsmBool resetLocalRotation;

		// Token: 0x04008CFD RID: 36093
		[Tooltip("Keep the local position of the child object, instead of keeping the world position.")]
		public FsmBool localPositionStays;

		// Token: 0x04008CFE RID: 36094
		[Tooltip("Sets the game object's layer to the same as the parent's if true")]
		public FsmBool changeToParentLayer;
	}
}
