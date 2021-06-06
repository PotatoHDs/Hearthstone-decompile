using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D07 RID: 3335
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Iterate through a list of all colliders that fall within a circular area.The colliders iterated are sorted in order of increasing Z coordinate. No iteration will take place if there are no colliders within the area.")]
	public class GetNextOverlapCircle2d : FsmStateAction
	{
		// Token: 0x0600A220 RID: 41504 RVA: 0x0033A814 File Offset: 0x00338A14
		public override void Reset()
		{
			this.fromGameObject = null;
			this.fromPosition = new FsmVector2
			{
				UseVariable = true
			};
			this.radius = 10f;
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

		// Token: 0x0600A221 RID: 41505 RVA: 0x0033A8AC File Offset: 0x00338AAC
		public override void OnEnter()
		{
			if (this.colliders == null || this.resetFlag.Value)
			{
				this.nextColliderIndex = 0;
				this.colliders = this.GetOverlapCircleAll();
				this.colliderCount = this.colliders.Length;
				this.collidersCount.Value = this.colliderCount;
				this.resetFlag.Value = false;
			}
			this.DoGetNextCollider();
			base.Finish();
		}

		// Token: 0x0600A222 RID: 41506 RVA: 0x0033A918 File Offset: 0x00338B18
		private void DoGetNextCollider()
		{
			if (this.nextColliderIndex >= this.colliderCount)
			{
				this.nextColliderIndex = 0;
				this.colliders = null;
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

		// Token: 0x0600A223 RID: 41507 RVA: 0x0033A9C8 File Offset: 0x00338BC8
		private Collider2D[] GetOverlapCircleAll()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.fromGameObject);
			Vector2 value = this.fromPosition.Value;
			if (ownerDefaultTarget != null)
			{
				value.x += ownerDefaultTarget.transform.position.x;
				value.y += ownerDefaultTarget.transform.position.y;
			}
			if (this.minDepth.IsNone && this.maxDepth.IsNone)
			{
				return Physics2D.OverlapCircleAll(value, this.radius.Value, ActionHelpers.LayerArrayToLayerMask(this.layerMask, this.invertMask.Value));
			}
			float num = this.minDepth.IsNone ? float.NegativeInfinity : ((float)this.minDepth.Value);
			float num2 = this.maxDepth.IsNone ? float.PositiveInfinity : ((float)this.maxDepth.Value);
			return Physics2D.OverlapCircleAll(value, this.radius.Value, ActionHelpers.LayerArrayToLayerMask(this.layerMask, this.invertMask.Value), num, num2);
		}

		// Token: 0x0400883D RID: 34877
		[ActionSection("Setup")]
		[Tooltip("Center of the circle area. \nOr use From Position parameter.")]
		public FsmOwnerDefault fromGameObject;

		// Token: 0x0400883E RID: 34878
		[Tooltip("CEnter of the circle area as a world position. \nOr use fromGameObject parameter. If both define, will add fromPosition to the fromGameObject position")]
		public FsmVector2 fromPosition;

		// Token: 0x0400883F RID: 34879
		[Tooltip("The circle radius")]
		public FsmFloat radius;

		// Token: 0x04008840 RID: 34880
		[Tooltip("Only include objects with a Z coordinate (depth) greater than this value. leave to none for no effect")]
		public FsmInt minDepth;

		// Token: 0x04008841 RID: 34881
		[Tooltip("Only include objects with a Z coordinate (depth) less than this value. leave to none")]
		public FsmInt maxDepth;

		// Token: 0x04008842 RID: 34882
		[Tooltip("If you want to reset the iteration, raise this flag to true when you enter the state, it will indicate you want to start from the beginning again")]
		[UIHint(UIHint.Variable)]
		public FsmBool resetFlag;

		// Token: 0x04008843 RID: 34883
		[ActionSection("Filter")]
		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;

		// Token: 0x04008844 RID: 34884
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;

		// Token: 0x04008845 RID: 34885
		[ActionSection("Result")]
		[Tooltip("Store the number of colliders found for this overlap.")]
		[UIHint(UIHint.Variable)]
		public FsmInt collidersCount;

		// Token: 0x04008846 RID: 34886
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the next collider in a GameObject variable.")]
		public FsmGameObject storeNextCollider;

		// Token: 0x04008847 RID: 34887
		[Tooltip("Event to send to get the next collider.")]
		public FsmEvent loopEvent;

		// Token: 0x04008848 RID: 34888
		[Tooltip("Event to send when there are no more colliders to iterate.")]
		public FsmEvent finishedEvent;

		// Token: 0x04008849 RID: 34889
		private Collider2D[] colliders;

		// Token: 0x0400884A RID: 34890
		private int colliderCount;

		// Token: 0x0400884B RID: 34891
		private int nextColliderIndex;
	}
}
