using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D09 RID: 3337
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Iterate through a list of all colliders detected by a RayCastThe colliders iterated are sorted in order of increasing Z coordinate. No iteration will take place if there are no colliders within the area.")]
	public class GetNextRayCast2d : FsmStateAction
	{
		// Token: 0x0600A22A RID: 41514 RVA: 0x0033AD80 File Offset: 0x00338F80
		public override void Reset()
		{
			this.fromGameObject = null;
			this.fromPosition = new FsmVector2
			{
				UseVariable = true
			};
			this.direction = new FsmVector2
			{
				UseVariable = true
			};
			this.space = Space.Self;
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
			this.storeNextHitPoint = null;
			this.storeNextHitNormal = null;
			this.storeNextHitDistance = null;
			this.storeNextHitFraction = null;
			this.loopEvent = null;
			this.finishedEvent = null;
		}

		// Token: 0x0600A22B RID: 41515 RVA: 0x0033AE3C File Offset: 0x0033903C
		public override void OnEnter()
		{
			if (this.hits == null || this.resetFlag.Value)
			{
				this.nextColliderIndex = 0;
				this.hits = this.GetRayCastAll();
				this.colliderCount = this.hits.Length;
				this.collidersCount.Value = this.colliderCount;
				this.resetFlag.Value = false;
			}
			this.DoGetNextCollider();
			base.Finish();
		}

		// Token: 0x0600A22C RID: 41516 RVA: 0x0033AEA8 File Offset: 0x003390A8
		private void DoGetNextCollider()
		{
			if (this.nextColliderIndex >= this.colliderCount)
			{
				this.hits = null;
				this.nextColliderIndex = 0;
				base.Fsm.Event(this.finishedEvent);
				return;
			}
			Fsm.RecordLastRaycastHit2DInfo(base.Fsm, this.hits[this.nextColliderIndex]);
			this.storeNextCollider.Value = this.hits[this.nextColliderIndex].collider.gameObject;
			this.storeNextHitPoint.Value = this.hits[this.nextColliderIndex].point;
			this.storeNextHitNormal.Value = this.hits[this.nextColliderIndex].normal;
			this.storeNextHitDistance.Value = this.hits[this.nextColliderIndex].distance;
			this.storeNextHitFraction.Value = this.hits[this.nextColliderIndex].fraction;
			if (this.nextColliderIndex >= this.colliderCount)
			{
				this.hits = new RaycastHit2D[0];
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

		// Token: 0x0600A22D RID: 41517 RVA: 0x0033B004 File Offset: 0x00339204
		private RaycastHit2D[] GetRayCastAll()
		{
			if (Math.Abs(this.distance.Value) < Mathf.Epsilon)
			{
				return new RaycastHit2D[0];
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.fromGameObject);
			Vector2 value = this.fromPosition.Value;
			if (ownerDefaultTarget != null)
			{
				value.x += ownerDefaultTarget.transform.position.x;
				value.y += ownerDefaultTarget.transform.position.y;
			}
			float num = float.PositiveInfinity;
			if (this.distance.Value > 0f)
			{
				num = this.distance.Value;
			}
			Vector2 normalized = this.direction.Value.normalized;
			if (ownerDefaultTarget != null && this.space == Space.Self)
			{
				Vector3 vector = ownerDefaultTarget.transform.TransformDirection(new Vector3(this.direction.Value.x, this.direction.Value.y, 0f));
				normalized.x = vector.x;
				normalized.y = vector.y;
			}
			if (this.minDepth.IsNone && this.maxDepth.IsNone)
			{
				return Physics2D.RaycastAll(value, normalized, num, ActionHelpers.LayerArrayToLayerMask(this.layerMask, this.invertMask.Value));
			}
			float num2 = this.minDepth.IsNone ? float.NegativeInfinity : ((float)this.minDepth.Value);
			float num3 = this.maxDepth.IsNone ? float.PositiveInfinity : ((float)this.maxDepth.Value);
			return Physics2D.RaycastAll(value, normalized, num, ActionHelpers.LayerArrayToLayerMask(this.layerMask, this.invertMask.Value), num2, num3);
		}

		// Token: 0x0400885A RID: 34906
		[ActionSection("Setup")]
		[Tooltip("Start ray at game object position. \nOr use From Position parameter.")]
		public FsmOwnerDefault fromGameObject;

		// Token: 0x0400885B RID: 34907
		[Tooltip("Start ray at a vector2 world position. \nOr use Game Object parameter.")]
		public FsmVector2 fromPosition;

		// Token: 0x0400885C RID: 34908
		[Tooltip("A vector2 direction vector")]
		public FsmVector2 direction;

		// Token: 0x0400885D RID: 34909
		[Tooltip("Cast the ray in world or local space. Note if no Game Object is specified, the direction is in world space.")]
		public Space space;

		// Token: 0x0400885E RID: 34910
		[Tooltip("The length of the ray. Set to -1 for infinity.")]
		public FsmFloat distance;

		// Token: 0x0400885F RID: 34911
		[Tooltip("Only include objects with a Z coordinate (depth) greater than this value. leave to none for no effect")]
		public FsmInt minDepth;

		// Token: 0x04008860 RID: 34912
		[Tooltip("Only include objects with a Z coordinate (depth) less than this value. leave to none")]
		public FsmInt maxDepth;

		// Token: 0x04008861 RID: 34913
		[Tooltip("If you want to reset the iteration, raise this flag to true when you enter the state, it will indicate you want to start from the beginning again")]
		[UIHint(UIHint.Variable)]
		public FsmBool resetFlag;

		// Token: 0x04008862 RID: 34914
		[ActionSection("Filter")]
		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;

		// Token: 0x04008863 RID: 34915
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;

		// Token: 0x04008864 RID: 34916
		[ActionSection("Result")]
		[Tooltip("Store the number of colliders found for this overlap.")]
		[UIHint(UIHint.Variable)]
		public FsmInt collidersCount;

		// Token: 0x04008865 RID: 34917
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the next collider in a GameObject variable.")]
		public FsmGameObject storeNextCollider;

		// Token: 0x04008866 RID: 34918
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the 2d position of the next ray hit point and store it in a variable.")]
		public FsmVector2 storeNextHitPoint;

		// Token: 0x04008867 RID: 34919
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the 2d normal at the next hit point and store it in a variable.")]
		public FsmVector2 storeNextHitNormal;

		// Token: 0x04008868 RID: 34920
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the distance along the ray to the next hit point and store it in a variable.")]
		public FsmFloat storeNextHitDistance;

		// Token: 0x04008869 RID: 34921
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the fraction along the ray to the hit point and store it in a variable. If the ray's direction vector is normalized then this value is simply the distance between the origin and the hit point. If the direction is not normalized then this distance is expressed as a 'fraction' (which could be greater than 1) of the vector's magnitude.")]
		public FsmFloat storeNextHitFraction;

		// Token: 0x0400886A RID: 34922
		[Tooltip("Event to send to get the next collider.")]
		public FsmEvent loopEvent;

		// Token: 0x0400886B RID: 34923
		[Tooltip("Event to send when there are no more colliders to iterate.")]
		public FsmEvent finishedEvent;

		// Token: 0x0400886C RID: 34924
		private RaycastHit2D[] hits;

		// Token: 0x0400886D RID: 34925
		private int colliderCount;

		// Token: 0x0400886E RID: 34926
		private int nextColliderIndex;
	}
}
