using UnityEngine;

namespace Hearthstone.UI
{
	[ExecuteAlways]
	[DisallowMultipleComponent]
	public sealed class WidgetTransform : MonoBehaviour
	{
		public enum FacingDirection
		{
			YPositive,
			ZNegative
		}

		public enum LayoutBoundsMode
		{
			Recursive,
			NonRecursive,
			Excluded
		}

		private static readonly Quaternion[] s_RotationZNegativeToFacingDirection = new Quaternion[2]
		{
			Quaternion.Euler(90f, 0f, 0f),
			Quaternion.identity
		};

		[SerializeField]
		private Rect m_rect;

		[SerializeField]
		private bool m_hasBounds;

		[SerializeField]
		private FacingDirection m_facingDirection;

		[SerializeField]
		private LayoutBoundsMode m_layoutBoundsMode;

		public Rect Rect => m_rect;

		[Overridable]
		public float Left
		{
			get
			{
				return m_rect.xMin;
			}
			set
			{
				m_rect.xMin = value;
			}
		}

		[Overridable]
		public float Right
		{
			get
			{
				return m_rect.xMax;
			}
			set
			{
				m_rect.xMax = value;
			}
		}

		[Overridable]
		public float Top
		{
			get
			{
				return m_rect.yMax;
			}
			set
			{
				m_rect.yMax = value;
			}
		}

		[Overridable]
		public float Bottom
		{
			get
			{
				return m_rect.yMin;
			}
			set
			{
				m_rect.yMin = value;
			}
		}

		public bool HasBounds => m_hasBounds;

		public FacingDirection Facing
		{
			get
			{
				return m_facingDirection;
			}
			set
			{
				m_facingDirection = value;
			}
		}

		public LayoutBoundsMode BoundsMode
		{
			get
			{
				return m_layoutBoundsMode;
			}
			set
			{
				m_layoutBoundsMode = value;
			}
		}

		public static Quaternion GetRotationFromZNegativeToDesiredFacing(FacingDirection facing)
		{
			return s_RotationZNegativeToFacingDirection[(int)facing];
		}

		public static Matrix4x4 GetRotationMatrixFromZNegativeToDesiredFacing(FacingDirection endFacing)
		{
			return Matrix4x4.Rotate(s_RotationZNegativeToFacingDirection[(int)endFacing]);
		}

		public static Bounds GetLocalBoundsOfWidgetTransform(Transform transform)
		{
			return GetBoundsOfWidgetTransform(transform, Matrix4x4.identity);
		}

		public static Bounds GetBoundsOfWidgetTransform(Transform transform, Matrix4x4 localToTargetMatrix)
		{
			Bounds result = new Bounds(Vector3.zero, Vector3.zero);
			WidgetTransform component = transform.GetComponent<WidgetTransform>();
			if (component == null || component.BoundsMode == LayoutBoundsMode.Excluded)
			{
				return result;
			}
			Matrix4x4 rotationMatrixFromZNegativeToDesiredFacing = GetRotationMatrixFromZNegativeToDesiredFacing(component.Facing);
			Matrix4x4 matrix4x = localToTargetMatrix * rotationMatrixFromZNegativeToDesiredFacing;
			result.size = matrix4x.MultiplyPoint(new Vector3(component.Rect.width, component.Rect.height, 0f));
			float x = (component.Rect.xMax + component.Rect.xMin) * 0.5f;
			float y = (component.Rect.yMax + component.Rect.yMin) * 0.5f;
			result.center = matrix4x.MultiplyPoint(new Vector3(x, y, 0f));
			return result;
		}

		public static Bounds GetWorldBoundsOfWidgetTransforms(Transform transform)
		{
			return GetBoundsOfWidgetTransforms(transform, Matrix4x4.identity);
		}

		public static Bounds GetBoundsOfWidgetTransforms(Transform transform, Matrix4x4 worldToTargetMatrix)
		{
			Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
			bool hasInitBounds = false;
			SceneUtils.WalkSelfAndChildren(transform, delegate(Transform current)
			{
				WidgetTransform component = current.GetComponent<WidgetTransform>();
				if (component == null || component.BoundsMode == LayoutBoundsMode.Excluded)
				{
					return true;
				}
				if (!component.enabled || !component.gameObject.activeInHierarchy)
				{
					return false;
				}
				Matrix4x4 rotationMatrixFromZNegativeToDesiredFacing = GetRotationMatrixFromZNegativeToDesiredFacing(component.Facing);
				Matrix4x4 localToWorldMatrix = component.gameObject.transform.localToWorldMatrix;
				Matrix4x4 matrix4x = worldToTargetMatrix * localToWorldMatrix * rotationMatrixFromZNegativeToDesiredFacing;
				Vector3 vector = matrix4x.MultiplyPoint(new Vector3(component.Rect.xMin, component.Rect.yMin, 0f));
				Vector3 point = matrix4x.MultiplyPoint(new Vector3(component.Rect.xMax, component.Rect.yMin, 0f));
				Vector3 point2 = matrix4x.MultiplyPoint(new Vector3(component.Rect.xMin, component.Rect.yMax, 0f));
				Vector3 point3 = matrix4x.MultiplyPoint(new Vector3(component.Rect.xMax, component.Rect.yMax, 0f));
				if (!hasInitBounds)
				{
					bounds = new Bounds(vector, Vector3.zero);
					hasInitBounds = true;
				}
				bounds.Encapsulate(vector);
				bounds.Encapsulate(point);
				bounds.Encapsulate(point2);
				bounds.Encapsulate(point3);
				return component.BoundsMode != LayoutBoundsMode.NonRecursive;
			});
			return bounds;
		}

		private void OnDrawGizmosSelected()
		{
			DrawGizmos(selected: true);
		}

		private void OnDrawGizmos()
		{
			DrawGizmos(selected: false);
		}

		private void DrawGizmos(bool selected)
		{
			if (m_hasBounds && base.transform.gameObject.activeInHierarchy)
			{
				Gizmos.matrix = base.transform.localToWorldMatrix;
				Color color = ((base.transform.GetComponent<Clickable>() != null) ? Color.magenta : Color.cyan);
				Gizmos.color = (selected ? color : (color * 0.5f));
				Matrix4x4 rotationMatrixFromZNegativeToDesiredFacing = GetRotationMatrixFromZNegativeToDesiredFacing(m_facingDirection);
				Gizmos.DrawWireCube(rotationMatrixFromZNegativeToDesiredFacing * m_rect.center, rotationMatrixFromZNegativeToDesiredFacing * m_rect.size);
			}
		}

		public BoxCollider CreateBoxCollider(GameObject target)
		{
			Matrix4x4 rotationMatrixFromZNegativeToDesiredFacing = GetRotationMatrixFromZNegativeToDesiredFacing(m_facingDirection);
			BoxCollider boxCollider = target.AddComponent<BoxCollider>();
			boxCollider.hideFlags = HideFlags.DontSave;
			boxCollider.center = rotationMatrixFromZNegativeToDesiredFacing * Rect.center;
			boxCollider.size = rotationMatrixFromZNegativeToDesiredFacing * Rect.size;
			return boxCollider;
		}
	}
}
