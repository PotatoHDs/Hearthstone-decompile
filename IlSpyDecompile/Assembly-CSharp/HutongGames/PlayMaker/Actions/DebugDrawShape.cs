using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Draw Gizmos in the Scene View.")]
	public class DebugDrawShape : FsmStateAction
	{
		public enum ShapeType
		{
			Sphere,
			Cube,
			WireSphere,
			WireCube
		}

		[RequiredField]
		[Tooltip("Draw the Gizmo at a GameObject's position.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The type of Gizmo to draw:\nSphere, Cube, WireSphere, or WireCube.")]
		public ShapeType shape;

		[Tooltip("The color to use.")]
		public FsmColor color;

		[Tooltip("Use this for sphere gizmos")]
		public FsmFloat radius;

		[Tooltip("Use this for cube gizmos")]
		public FsmVector3 size;

		public override void Reset()
		{
			gameObject = null;
			shape = ShapeType.Sphere;
			color = Color.grey;
			radius = 1f;
			size = new Vector3(1f, 1f, 1f);
		}

		public override void OnDrawActionGizmos()
		{
			Transform transform = base.Fsm.GetOwnerDefaultTarget(gameObject).transform;
			if (!(transform == null))
			{
				Gizmos.color = color.Value;
				switch (shape)
				{
				case ShapeType.Sphere:
					Gizmos.DrawSphere(transform.position, radius.Value);
					break;
				case ShapeType.WireSphere:
					Gizmos.DrawWireSphere(transform.position, radius.Value);
					break;
				case ShapeType.Cube:
					Gizmos.DrawCube(transform.position, size.Value);
					break;
				case ShapeType.WireCube:
					Gizmos.DrawWireCube(transform.position, size.Value);
					break;
				}
			}
		}
	}
}
