using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D4F RID: 3407
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Casts a Ray against all Colliders in the scene. Use either a Game Object or Vector3 world position as the origin of the ray. Use GetRaycastInfo to get more detailed info.")]
	public class Raycast : FsmStateAction
	{
		// Token: 0x0600A391 RID: 41873 RVA: 0x0033F7F4 File Offset: 0x0033D9F4
		public override void Reset()
		{
			this.fromGameObject = null;
			this.fromPosition = new FsmVector3
			{
				UseVariable = true
			};
			this.direction = new FsmVector3
			{
				UseVariable = true
			};
			this.space = Space.Self;
			this.distance = 100f;
			this.hitEvent = null;
			this.storeDidHit = null;
			this.storeHitObject = null;
			this.storeHitPoint = null;
			this.storeHitNormal = null;
			this.storeHitDistance = null;
			this.repeatInterval = 1;
			this.layerMask = new FsmInt[0];
			this.invertMask = false;
			this.debugColor = Color.yellow;
			this.debug = false;
		}

		// Token: 0x0600A392 RID: 41874 RVA: 0x0033F8AD File Offset: 0x0033DAAD
		public override void OnEnter()
		{
			this.DoRaycast();
			if (this.repeatInterval.Value == 0)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A393 RID: 41875 RVA: 0x0033F8C8 File Offset: 0x0033DAC8
		public override void OnUpdate()
		{
			this.repeat--;
			if (this.repeat == 0)
			{
				this.DoRaycast();
			}
		}

		// Token: 0x0600A394 RID: 41876 RVA: 0x0033F8E8 File Offset: 0x0033DAE8
		private void DoRaycast()
		{
			this.repeat = this.repeatInterval.Value;
			if (this.distance.Value == 0f)
			{
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.fromGameObject);
			Vector3 vector = (ownerDefaultTarget != null) ? ownerDefaultTarget.transform.position : this.fromPosition.Value;
			float num = float.PositiveInfinity;
			if (this.distance.Value > 0f)
			{
				num = this.distance.Value;
			}
			Vector3 a = this.direction.Value;
			if (ownerDefaultTarget != null && this.space == Space.Self)
			{
				a = ownerDefaultTarget.transform.TransformDirection(this.direction.Value);
			}
			RaycastHit raycastHitInfo;
			Physics.Raycast(vector, a, out raycastHitInfo, num, ActionHelpers.LayerArrayToLayerMask(this.layerMask, this.invertMask.Value));
			base.Fsm.RaycastHitInfo = raycastHitInfo;
			bool flag = raycastHitInfo.collider != null;
			this.storeDidHit.Value = flag;
			if (flag)
			{
				this.storeHitObject.Value = raycastHitInfo.collider.gameObject;
				this.storeHitPoint.Value = base.Fsm.RaycastHitInfo.point;
				this.storeHitNormal.Value = base.Fsm.RaycastHitInfo.normal;
				this.storeHitDistance.Value = base.Fsm.RaycastHitInfo.distance;
				base.Fsm.Event(this.hitEvent);
			}
			if (this.debug.Value)
			{
				float d = Mathf.Min(num, 1000f);
				Debug.DrawLine(vector, vector + a * d, this.debugColor.Value);
			}
		}

		// Token: 0x040089C3 RID: 35267
		[Tooltip("Start ray at game object position. \nOr use From Position parameter.")]
		public FsmOwnerDefault fromGameObject;

		// Token: 0x040089C4 RID: 35268
		[Tooltip("Start ray at a vector3 world position. \nOr use Game Object parameter.")]
		public FsmVector3 fromPosition;

		// Token: 0x040089C5 RID: 35269
		[Tooltip("A vector3 direction vector")]
		public FsmVector3 direction;

		// Token: 0x040089C6 RID: 35270
		[Tooltip("Cast the ray in world or local space. Note if no Game Object is specified, the direction is in world space.")]
		public Space space;

		// Token: 0x040089C7 RID: 35271
		[Tooltip("The length of the ray. Set to -1 for infinity.")]
		public FsmFloat distance;

		// Token: 0x040089C8 RID: 35272
		[ActionSection("Result")]
		[Tooltip("Event to send if the ray hits an object.")]
		[UIHint(UIHint.Variable)]
		public FsmEvent hitEvent;

		// Token: 0x040089C9 RID: 35273
		[Tooltip("Set a bool variable to true if hit something, otherwise false.")]
		[UIHint(UIHint.Variable)]
		public FsmBool storeDidHit;

		// Token: 0x040089CA RID: 35274
		[Tooltip("Store the game object hit in a variable.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeHitObject;

		// Token: 0x040089CB RID: 35275
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the world position of the ray hit point and store it in a variable.")]
		public FsmVector3 storeHitPoint;

		// Token: 0x040089CC RID: 35276
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the normal at the hit point and store it in a variable.")]
		public FsmVector3 storeHitNormal;

		// Token: 0x040089CD RID: 35277
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the distance along the ray to the hit point and store it in a variable.")]
		public FsmFloat storeHitDistance;

		// Token: 0x040089CE RID: 35278
		[ActionSection("Filter")]
		[Tooltip("Set how often to cast a ray. 0 = once, don't repeat; 1 = everyFrame; 2 = every other frame... \nSince raycasts can get expensive use the highest repeat interval you can get away with.")]
		public FsmInt repeatInterval;

		// Token: 0x040089CF RID: 35279
		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;

		// Token: 0x040089D0 RID: 35280
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;

		// Token: 0x040089D1 RID: 35281
		[ActionSection("Debug")]
		[Tooltip("The color to use for the debug line.")]
		public FsmColor debugColor;

		// Token: 0x040089D2 RID: 35282
		[Tooltip("Draw a debug line. Note: Check Gizmos in the Game View to see it in game.")]
		public FsmBool debug;

		// Token: 0x040089D3 RID: 35283
		private int repeat;
	}
}
