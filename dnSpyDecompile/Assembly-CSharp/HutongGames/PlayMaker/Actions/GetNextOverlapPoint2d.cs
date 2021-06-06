using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D08 RID: 3336
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Iterate through a list of all colliders that overlap a point in space.The colliders iterated are sorted in order of increasing Z coordinate. No iteration will take place if there are no colliders overlap this point.")]
	public class GetNextOverlapPoint2d : FsmStateAction
	{
		// Token: 0x0600A225 RID: 41509 RVA: 0x0033AAE0 File Offset: 0x00338CE0
		public override void Reset()
		{
			this.gameObject = null;
			this.position = new FsmVector2
			{
				UseVariable = true
			};
			this.minDepth = new FsmInt
			{
				UseVariable = true
			};
			this.maxDepth = new FsmInt
			{
				UseVariable = true
			};
			this.layerMask = new FsmInt[0];
			this.invertMask = false;
			this.resetFlag = null;
			this.collidersCount = null;
			this.storeNextCollider = null;
			this.loopEvent = null;
			this.finishedEvent = null;
		}

		// Token: 0x0600A226 RID: 41510 RVA: 0x0033AB68 File Offset: 0x00338D68
		public override void OnEnter()
		{
			if (this.colliders == null || this.resetFlag.Value)
			{
				this.nextColliderIndex = 0;
				this.colliders = this.GetOverlapPointAll();
				this.colliderCount = this.colliders.Length;
				this.collidersCount.Value = this.colliderCount;
				this.resetFlag.Value = false;
			}
			this.DoGetNextCollider();
			base.Finish();
		}

		// Token: 0x0600A227 RID: 41511 RVA: 0x0033ABD4 File Offset: 0x00338DD4
		private void DoGetNextCollider()
		{
			if (this.nextColliderIndex >= this.colliderCount)
			{
				this.nextColliderIndex = 0;
				base.Fsm.Event(this.finishedEvent);
				return;
			}
			this.storeNextCollider.Value = this.colliders[this.nextColliderIndex].gameObject;
			if (this.nextColliderIndex >= this.colliderCount)
			{
				this.colliders = null;
				this.nextColliderIndex = 0;
				base.Fsm.Event(this.finishedEvent);
				return;
			}
			this.nextColliderIndex++;
			if (this.loopEvent != null)
			{
				base.Fsm.Event(this.loopEvent);
			}
		}

		// Token: 0x0600A228 RID: 41512 RVA: 0x0033AC7C File Offset: 0x00338E7C
		private Collider2D[] GetOverlapPointAll()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			Vector2 value = this.position.Value;
			if (ownerDefaultTarget != null)
			{
				value.x += ownerDefaultTarget.transform.position.x;
				value.y += ownerDefaultTarget.transform.position.y;
			}
			if (this.minDepth.IsNone && this.maxDepth.IsNone)
			{
				return Physics2D.OverlapPointAll(value, ActionHelpers.LayerArrayToLayerMask(this.layerMask, this.invertMask.Value));
			}
			float num = this.minDepth.IsNone ? float.NegativeInfinity : ((float)this.minDepth.Value);
			float num2 = this.maxDepth.IsNone ? float.PositiveInfinity : ((float)this.maxDepth.Value);
			return Physics2D.OverlapPointAll(value, ActionHelpers.LayerArrayToLayerMask(this.layerMask, this.invertMask.Value), num, num2);
		}

		// Token: 0x0400884C RID: 34892
		[ActionSection("Setup")]
		[Tooltip("Point using the gameObject position. \nOr use From Position parameter.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400884D RID: 34893
		[Tooltip("Point as a world position. \nOr use gameObject parameter. If both define, will add position to the gameObject position")]
		public FsmVector2 position;

		// Token: 0x0400884E RID: 34894
		[Tooltip("Only include objects with a Z coordinate (depth) greater than this value. leave to none for no effect")]
		public FsmInt minDepth;

		// Token: 0x0400884F RID: 34895
		[Tooltip("Only include objects with a Z coordinate (depth) less than this value. leave to none")]
		public FsmInt maxDepth;

		// Token: 0x04008850 RID: 34896
		[Tooltip("If you want to reset the iteration, raise this flag to true when you enter the state, it will indicate you want to start from the beginning again")]
		[UIHint(UIHint.Variable)]
		public FsmBool resetFlag;

		// Token: 0x04008851 RID: 34897
		[ActionSection("Filter")]
		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;

		// Token: 0x04008852 RID: 34898
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;

		// Token: 0x04008853 RID: 34899
		[ActionSection("Result")]
		[Tooltip("Store the number of colliders found for this overlap.")]
		[UIHint(UIHint.Variable)]
		public FsmInt collidersCount;

		// Token: 0x04008854 RID: 34900
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the next collider in a GameObject variable.")]
		public FsmGameObject storeNextCollider;

		// Token: 0x04008855 RID: 34901
		[Tooltip("Event to send to get the next collider.")]
		public FsmEvent loopEvent;

		// Token: 0x04008856 RID: 34902
		[Tooltip("Event to send when there are no more colliders to iterate.")]
		public FsmEvent finishedEvent;

		// Token: 0x04008857 RID: 34903
		private Collider2D[] colliders;

		// Token: 0x04008858 RID: 34904
		private int colliderCount;

		// Token: 0x04008859 RID: 34905
		private int nextColliderIndex;
	}
}
