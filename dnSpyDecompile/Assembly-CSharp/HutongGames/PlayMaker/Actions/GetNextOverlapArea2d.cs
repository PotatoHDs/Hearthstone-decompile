using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D06 RID: 3334
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Iterate through a list of all colliders that fall within a rectangular area.The colliders iterated are sorted in order of increasing Z coordinate. No iteration will take place if there are no colliders within the area.")]
	public class GetNextOverlapArea2d : FsmStateAction
	{
		// Token: 0x0600A21B RID: 41499 RVA: 0x0033A500 File Offset: 0x00338700
		public override void Reset()
		{
			this.firstCornerGameObject = null;
			this.firstCornerPosition = new FsmVector2
			{
				UseVariable = true
			};
			this.secondCornerGameObject = null;
			this.secondCornerPosition = new FsmVector2
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

		// Token: 0x0600A21C RID: 41500 RVA: 0x0033A5A0 File Offset: 0x003387A0
		public override void OnEnter()
		{
			if (this.colliders == null || this.resetFlag.Value)
			{
				this.nextColliderIndex = 0;
				this.colliders = this.GetOverlapAreaAll();
				this.colliderCount = this.colliders.Length;
				this.collidersCount.Value = this.colliderCount;
				this.resetFlag.Value = false;
			}
			this.DoGetNextCollider();
			base.Finish();
		}

		// Token: 0x0600A21D RID: 41501 RVA: 0x0033A60C File Offset: 0x0033880C
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

		// Token: 0x0600A21E RID: 41502 RVA: 0x0033A6B4 File Offset: 0x003388B4
		private Collider2D[] GetOverlapAreaAll()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.firstCornerGameObject);
			Vector2 value = this.firstCornerPosition.Value;
			if (ownerDefaultTarget != null)
			{
				value.x += ownerDefaultTarget.transform.position.x;
				value.y += ownerDefaultTarget.transform.position.y;
			}
			GameObject value2 = this.secondCornerGameObject.Value;
			Vector2 value3 = this.secondCornerPosition.Value;
			if (value2 != null)
			{
				value3.x += value2.transform.position.x;
				value3.y += value2.transform.position.y;
			}
			if (this.minDepth.IsNone && this.maxDepth.IsNone)
			{
				return Physics2D.OverlapAreaAll(value, value3, ActionHelpers.LayerArrayToLayerMask(this.layerMask, this.invertMask.Value));
			}
			float num = this.minDepth.IsNone ? float.NegativeInfinity : ((float)this.minDepth.Value);
			float num2 = this.maxDepth.IsNone ? float.PositiveInfinity : ((float)this.maxDepth.Value);
			return Physics2D.OverlapAreaAll(value, value3, ActionHelpers.LayerArrayToLayerMask(this.layerMask, this.invertMask.Value), num, num2);
		}

		// Token: 0x0400882D RID: 34861
		[ActionSection("Setup")]
		[Tooltip("First corner of the rectangle area using the game object position. \nOr use firstCornerPosition parameter.")]
		public FsmOwnerDefault firstCornerGameObject;

		// Token: 0x0400882E RID: 34862
		[Tooltip("First Corner of the rectangle area as a world position. \nOr use FirstCornerGameObject parameter. If both define, will add firstCornerPosition to the FirstCornerGameObject position")]
		public FsmVector2 firstCornerPosition;

		// Token: 0x0400882F RID: 34863
		[Tooltip("Second corner of the rectangle area using the game object position. \nOr use secondCornerPosition parameter.")]
		public FsmGameObject secondCornerGameObject;

		// Token: 0x04008830 RID: 34864
		[Tooltip("Second Corner rectangle area as a world position. \nOr use SecondCornerGameObject parameter. If both define, will add secondCornerPosition to the SecondCornerGameObject position")]
		public FsmVector2 secondCornerPosition;

		// Token: 0x04008831 RID: 34865
		[Tooltip("Only include objects with a Z coordinate (depth) greater than this value. leave to none for no effect")]
		public FsmInt minDepth;

		// Token: 0x04008832 RID: 34866
		[Tooltip("Only include objects with a Z coordinate (depth) less than this value. leave to none")]
		public FsmInt maxDepth;

		// Token: 0x04008833 RID: 34867
		[Tooltip("If you want to reset the iteration, raise this flag to true when you enter the state, it will indicate you want to start from the beginning again")]
		[UIHint(UIHint.Variable)]
		public FsmBool resetFlag;

		// Token: 0x04008834 RID: 34868
		[ActionSection("Filter")]
		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;

		// Token: 0x04008835 RID: 34869
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;

		// Token: 0x04008836 RID: 34870
		[ActionSection("Result")]
		[Tooltip("Store the number of colliders found for this overlap.")]
		[UIHint(UIHint.Variable)]
		public FsmInt collidersCount;

		// Token: 0x04008837 RID: 34871
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the next collider in a GameObject variable.")]
		public FsmGameObject storeNextCollider;

		// Token: 0x04008838 RID: 34872
		[Tooltip("Event to send to get the next collider.")]
		public FsmEvent loopEvent;

		// Token: 0x04008839 RID: 34873
		[Tooltip("Event to send when there are no more colliders to iterate.")]
		public FsmEvent finishedEvent;

		// Token: 0x0400883A RID: 34874
		private Collider2D[] colliders;

		// Token: 0x0400883B RID: 34875
		private int colliderCount;

		// Token: 0x0400883C RID: 34876
		private int nextColliderIndex;
	}
}
