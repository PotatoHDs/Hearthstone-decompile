using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D16 RID: 3350
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Casts a Ray against all Colliders in the scene. A raycast is conceptually like a laser beam that is fired from a point in space along a particular direction. Any object making contact with the beam can be detected and reported. Use GetRaycastHit2dInfo to get more detailed info.")]
	public class RayCast2d : FsmStateAction
	{
		// Token: 0x0600A26B RID: 41579 RVA: 0x0033BF5C File Offset: 0x0033A15C
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
			this.distance = 100f;
			this.hitEvent = null;
			this.storeDidHit = null;
			this.storeHitObject = null;
			this.storeHitPoint = null;
			this.storeHitNormal = null;
			this.storeHitDistance = null;
			this.storeHitFraction = null;
			this.repeatInterval = 1;
			this.layerMask = new FsmInt[0];
			this.invertMask = false;
			this.debugColor = Color.yellow;
			this.debug = false;
		}

		// Token: 0x0600A26C RID: 41580 RVA: 0x0033C040 File Offset: 0x0033A240
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.fromGameObject);
			if (ownerDefaultTarget != null)
			{
				this._transform = ownerDefaultTarget.transform;
			}
			this.DoRaycast();
			if (this.repeatInterval.Value == 0)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A26D RID: 41581 RVA: 0x0033C08D File Offset: 0x0033A28D
		public override void OnUpdate()
		{
			this.repeat--;
			if (this.repeat == 0)
			{
				this.DoRaycast();
			}
		}

		// Token: 0x0600A26E RID: 41582 RVA: 0x0033C0AC File Offset: 0x0033A2AC
		private void DoRaycast()
		{
			this.repeat = this.repeatInterval.Value;
			if (Math.Abs(this.distance.Value) < Mathf.Epsilon)
			{
				return;
			}
			Vector2 value = this.fromPosition.Value;
			if (this._transform != null)
			{
				value.x += this._transform.position.x;
				value.y += this._transform.position.y;
			}
			float a = float.PositiveInfinity;
			if (this.distance.Value > 0f)
			{
				a = this.distance.Value;
			}
			Vector2 normalized = this.direction.Value.normalized;
			if (this._transform != null && this.space == Space.Self)
			{
				Vector3 vector = this._transform.TransformDirection(new Vector3(this.direction.Value.x, this.direction.Value.y, 0f));
				normalized.x = vector.x;
				normalized.y = vector.y;
			}
			RaycastHit2D info;
			if (this.minDepth.IsNone && this.maxDepth.IsNone)
			{
				info = Physics2D.Raycast(value, normalized, a, ActionHelpers.LayerArrayToLayerMask(this.layerMask, this.invertMask.Value));
			}
			else
			{
				float num = this.minDepth.IsNone ? float.NegativeInfinity : ((float)this.minDepth.Value);
				float num2 = this.maxDepth.IsNone ? float.PositiveInfinity : ((float)this.maxDepth.Value);
				info = Physics2D.Raycast(value, normalized, a, ActionHelpers.LayerArrayToLayerMask(this.layerMask, this.invertMask.Value), num, num2);
			}
			Fsm.RecordLastRaycastHit2DInfo(base.Fsm, info);
			bool flag = info.collider != null;
			this.storeDidHit.Value = flag;
			if (flag)
			{
				this.storeHitObject.Value = info.collider.gameObject;
				this.storeHitPoint.Value = info.point;
				this.storeHitNormal.Value = info.normal;
				this.storeHitDistance.Value = info.distance;
				this.storeHitFraction.Value = info.fraction;
				base.Fsm.Event(this.hitEvent);
			}
			if (this.debug.Value)
			{
				float d = Mathf.Min(a, 1000f);
				Vector3 vector2 = new Vector3(value.x, value.y, 0f);
				Vector3 a2 = new Vector3(normalized.x, normalized.y, 0f);
				Vector3 end = vector2 + a2 * d;
				Debug.DrawLine(vector2, end, this.debugColor.Value);
			}
		}

		// Token: 0x040088C0 RID: 35008
		[ActionSection("Setup")]
		[Tooltip("Start ray at game object position. \nOr use From Position parameter.")]
		public FsmOwnerDefault fromGameObject;

		// Token: 0x040088C1 RID: 35009
		[Tooltip("Start ray at a vector2 world position. \nOr use Game Object parameter.")]
		public FsmVector2 fromPosition;

		// Token: 0x040088C2 RID: 35010
		[Tooltip("A vector2 direction vector")]
		public FsmVector2 direction;

		// Token: 0x040088C3 RID: 35011
		[Tooltip("Cast the ray in world or local space. Note if no Game Object is specified, the direction is in world space.")]
		public Space space;

		// Token: 0x040088C4 RID: 35012
		[Tooltip("The length of the ray. Set to -1 for infinity.")]
		public FsmFloat distance;

		// Token: 0x040088C5 RID: 35013
		[Tooltip("Only include objects with a Z coordinate (depth) greater than this value. leave to none for no effect")]
		public FsmInt minDepth;

		// Token: 0x040088C6 RID: 35014
		[Tooltip("Only include objects with a Z coordinate (depth) less than this value. leave to none")]
		public FsmInt maxDepth;

		// Token: 0x040088C7 RID: 35015
		[ActionSection("Result")]
		[Tooltip("Event to send if the ray hits an object.")]
		[UIHint(UIHint.Variable)]
		public FsmEvent hitEvent;

		// Token: 0x040088C8 RID: 35016
		[Tooltip("Set a bool variable to true if hit something, otherwise false.")]
		[UIHint(UIHint.Variable)]
		public FsmBool storeDidHit;

		// Token: 0x040088C9 RID: 35017
		[Tooltip("Store the game object hit in a variable.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeHitObject;

		// Token: 0x040088CA RID: 35018
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the 2d position of the ray hit point and store it in a variable.")]
		public FsmVector2 storeHitPoint;

		// Token: 0x040088CB RID: 35019
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the 2d normal at the hit point and store it in a variable.")]
		public FsmVector2 storeHitNormal;

		// Token: 0x040088CC RID: 35020
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the distance along the ray to the hit point and store it in a variable.")]
		public FsmFloat storeHitDistance;

		// Token: 0x040088CD RID: 35021
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the fraction along the ray to the hit point and store it in a variable. If the ray's direction vector is normalized then this value is simply the distance between the origin and the hit point. If the direction is not normalized then this distance is expressed as a 'fraction' (which could be greater than 1) of the vector's magnitude.")]
		public FsmFloat storeHitFraction;

		// Token: 0x040088CE RID: 35022
		[ActionSection("Filter")]
		[Tooltip("Set how often to cast a ray. 0 = once, don't repeat; 1 = everyFrame; 2 = every other frame... \nSince raycasts can get expensive use the highest repeat interval you can get away with.")]
		public FsmInt repeatInterval;

		// Token: 0x040088CF RID: 35023
		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;

		// Token: 0x040088D0 RID: 35024
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;

		// Token: 0x040088D1 RID: 35025
		[ActionSection("Debug")]
		[Tooltip("The color to use for the debug line.")]
		public FsmColor debugColor;

		// Token: 0x040088D2 RID: 35026
		[Tooltip("Draw a debug line. Note: Check Gizmos in the Game View to see it in game.")]
		public FsmBool debug;

		// Token: 0x040088D3 RID: 35027
		private Transform _transform;

		// Token: 0x040088D4 RID: 35028
		private int repeat;
	}
}
