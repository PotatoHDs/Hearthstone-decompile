using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C05 RID: 3077
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Draw Gizmos in the Scene View.")]
	public class DebugDrawShape : FsmStateAction
	{
		// Token: 0x06009DB4 RID: 40372 RVA: 0x003299DC File Offset: 0x00327BDC
		public override void Reset()
		{
			this.gameObject = null;
			this.shape = DebugDrawShape.ShapeType.Sphere;
			this.color = Color.grey;
			this.radius = 1f;
			this.size = new Vector3(1f, 1f, 1f);
		}

		// Token: 0x06009DB5 RID: 40373 RVA: 0x00329A38 File Offset: 0x00327C38
		public override void OnDrawActionGizmos()
		{
			Transform transform = base.Fsm.GetOwnerDefaultTarget(this.gameObject).transform;
			if (transform == null)
			{
				return;
			}
			Gizmos.color = this.color.Value;
			switch (this.shape)
			{
			case DebugDrawShape.ShapeType.Sphere:
				Gizmos.DrawSphere(transform.position, this.radius.Value);
				return;
			case DebugDrawShape.ShapeType.Cube:
				Gizmos.DrawCube(transform.position, this.size.Value);
				return;
			case DebugDrawShape.ShapeType.WireSphere:
				Gizmos.DrawWireSphere(transform.position, this.radius.Value);
				return;
			case DebugDrawShape.ShapeType.WireCube:
				Gizmos.DrawWireCube(transform.position, this.size.Value);
				return;
			default:
				return;
			}
		}

		// Token: 0x0400831D RID: 33565
		[RequiredField]
		[Tooltip("Draw the Gizmo at a GameObject's position.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400831E RID: 33566
		[Tooltip("The type of Gizmo to draw:\nSphere, Cube, WireSphere, or WireCube.")]
		public DebugDrawShape.ShapeType shape;

		// Token: 0x0400831F RID: 33567
		[Tooltip("The color to use.")]
		public FsmColor color;

		// Token: 0x04008320 RID: 33568
		[Tooltip("Use this for sphere gizmos")]
		public FsmFloat radius;

		// Token: 0x04008321 RID: 33569
		[Tooltip("Use this for cube gizmos")]
		public FsmVector3 size;

		// Token: 0x02002796 RID: 10134
		public enum ShapeType
		{
			// Token: 0x0400F4B6 RID: 62646
			Sphere,
			// Token: 0x0400F4B7 RID: 62647
			Cube,
			// Token: 0x0400F4B8 RID: 62648
			WireSphere,
			// Token: 0x0400F4B9 RID: 62649
			WireCube
		}
	}
}
