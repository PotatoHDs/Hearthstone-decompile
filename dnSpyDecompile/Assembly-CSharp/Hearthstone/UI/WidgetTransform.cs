using System;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001033 RID: 4147
	[ExecuteAlways]
	[DisallowMultipleComponent]
	public sealed class WidgetTransform : MonoBehaviour
	{
		// Token: 0x0600B44B RID: 46155 RVA: 0x00377ABF File Offset: 0x00375CBF
		public static Quaternion GetRotationFromZNegativeToDesiredFacing(WidgetTransform.FacingDirection facing)
		{
			return WidgetTransform.s_RotationZNegativeToFacingDirection[(int)facing];
		}

		// Token: 0x0600B44C RID: 46156 RVA: 0x00377ACC File Offset: 0x00375CCC
		public static Matrix4x4 GetRotationMatrixFromZNegativeToDesiredFacing(WidgetTransform.FacingDirection endFacing)
		{
			return Matrix4x4.Rotate(WidgetTransform.s_RotationZNegativeToFacingDirection[(int)endFacing]);
		}

		// Token: 0x0600B44D RID: 46157 RVA: 0x00377ADE File Offset: 0x00375CDE
		public static Bounds GetLocalBoundsOfWidgetTransform(Transform transform)
		{
			return WidgetTransform.GetBoundsOfWidgetTransform(transform, Matrix4x4.identity);
		}

		// Token: 0x0600B44E RID: 46158 RVA: 0x00377AEC File Offset: 0x00375CEC
		public static Bounds GetBoundsOfWidgetTransform(Transform transform, Matrix4x4 localToTargetMatrix)
		{
			Bounds result = new Bounds(Vector3.zero, Vector3.zero);
			WidgetTransform component = transform.GetComponent<WidgetTransform>();
			if (component == null || component.BoundsMode == WidgetTransform.LayoutBoundsMode.Excluded)
			{
				return result;
			}
			Matrix4x4 rotationMatrixFromZNegativeToDesiredFacing = WidgetTransform.GetRotationMatrixFromZNegativeToDesiredFacing(component.Facing);
			Matrix4x4 matrix4x = localToTargetMatrix * rotationMatrixFromZNegativeToDesiredFacing;
			result.size = matrix4x.MultiplyPoint(new Vector3(component.Rect.width, component.Rect.height, 0f));
			float x = (component.Rect.xMax + component.Rect.xMin) * 0.5f;
			float y = (component.Rect.yMax + component.Rect.yMin) * 0.5f;
			result.center = matrix4x.MultiplyPoint(new Vector3(x, y, 0f));
			return result;
		}

		// Token: 0x0600B44F RID: 46159 RVA: 0x00377BDA File Offset: 0x00375DDA
		public static Bounds GetWorldBoundsOfWidgetTransforms(Transform transform)
		{
			return WidgetTransform.GetBoundsOfWidgetTransforms(transform, Matrix4x4.identity);
		}

		// Token: 0x0600B450 RID: 46160 RVA: 0x00377BE8 File Offset: 0x00375DE8
		public static Bounds GetBoundsOfWidgetTransforms(Transform transform, Matrix4x4 worldToTargetMatrix)
		{
			Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
			bool hasInitBounds = false;
			SceneUtils.WalkSelfAndChildren(transform, delegate(Transform current)
			{
				WidgetTransform component = current.GetComponent<WidgetTransform>();
				if (component == null || component.BoundsMode == WidgetTransform.LayoutBoundsMode.Excluded)
				{
					return true;
				}
				if (!component.enabled || !component.gameObject.activeInHierarchy)
				{
					return false;
				}
				Matrix4x4 rotationMatrixFromZNegativeToDesiredFacing = WidgetTransform.GetRotationMatrixFromZNegativeToDesiredFacing(component.Facing);
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
				return component.BoundsMode != WidgetTransform.LayoutBoundsMode.NonRecursive;
			});
			return bounds;
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x0600B451 RID: 46161 RVA: 0x00377C36 File Offset: 0x00375E36
		public Rect Rect
		{
			get
			{
				return this.m_rect;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x0600B453 RID: 46163 RVA: 0x00377C4C File Offset: 0x00375E4C
		// (set) Token: 0x0600B452 RID: 46162 RVA: 0x00377C3E File Offset: 0x00375E3E
		[Overridable]
		public float Left
		{
			get
			{
				return this.m_rect.xMin;
			}
			set
			{
				this.m_rect.xMin = value;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x0600B455 RID: 46165 RVA: 0x00377C67 File Offset: 0x00375E67
		// (set) Token: 0x0600B454 RID: 46164 RVA: 0x00377C59 File Offset: 0x00375E59
		[Overridable]
		public float Right
		{
			get
			{
				return this.m_rect.xMax;
			}
			set
			{
				this.m_rect.xMax = value;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x0600B457 RID: 46167 RVA: 0x00377C82 File Offset: 0x00375E82
		// (set) Token: 0x0600B456 RID: 46166 RVA: 0x00377C74 File Offset: 0x00375E74
		[Overridable]
		public float Top
		{
			get
			{
				return this.m_rect.yMax;
			}
			set
			{
				this.m_rect.yMax = value;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x0600B459 RID: 46169 RVA: 0x00377C9D File Offset: 0x00375E9D
		// (set) Token: 0x0600B458 RID: 46168 RVA: 0x00377C8F File Offset: 0x00375E8F
		[Overridable]
		public float Bottom
		{
			get
			{
				return this.m_rect.yMin;
			}
			set
			{
				this.m_rect.yMin = value;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x0600B45A RID: 46170 RVA: 0x00377CAA File Offset: 0x00375EAA
		public bool HasBounds
		{
			get
			{
				return this.m_hasBounds;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x0600B45B RID: 46171 RVA: 0x00377CB2 File Offset: 0x00375EB2
		// (set) Token: 0x0600B45C RID: 46172 RVA: 0x00377CBA File Offset: 0x00375EBA
		public WidgetTransform.FacingDirection Facing
		{
			get
			{
				return this.m_facingDirection;
			}
			set
			{
				this.m_facingDirection = value;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x0600B45D RID: 46173 RVA: 0x00377CC3 File Offset: 0x00375EC3
		// (set) Token: 0x0600B45E RID: 46174 RVA: 0x00377CCB File Offset: 0x00375ECB
		public WidgetTransform.LayoutBoundsMode BoundsMode
		{
			get
			{
				return this.m_layoutBoundsMode;
			}
			set
			{
				this.m_layoutBoundsMode = value;
			}
		}

		// Token: 0x0600B45F RID: 46175 RVA: 0x00377CD4 File Offset: 0x00375ED4
		private void OnDrawGizmosSelected()
		{
			this.DrawGizmos(true);
		}

		// Token: 0x0600B460 RID: 46176 RVA: 0x00377CDD File Offset: 0x00375EDD
		private void OnDrawGizmos()
		{
			this.DrawGizmos(false);
		}

		// Token: 0x0600B461 RID: 46177 RVA: 0x00377CE8 File Offset: 0x00375EE8
		private void DrawGizmos(bool selected)
		{
			if (this.m_hasBounds && base.transform.gameObject.activeInHierarchy)
			{
				Gizmos.matrix = base.transform.localToWorldMatrix;
				Color color = (base.transform.GetComponent<Clickable>() != null) ? Color.magenta : Color.cyan;
				Gizmos.color = (selected ? color : (color * 0.5f));
				Matrix4x4 rotationMatrixFromZNegativeToDesiredFacing = WidgetTransform.GetRotationMatrixFromZNegativeToDesiredFacing(this.m_facingDirection);
				Gizmos.DrawWireCube(rotationMatrixFromZNegativeToDesiredFacing * this.m_rect.center, rotationMatrixFromZNegativeToDesiredFacing * this.m_rect.size);
			}
		}

		// Token: 0x0600B462 RID: 46178 RVA: 0x00377DA4 File Offset: 0x00375FA4
		public BoxCollider CreateBoxCollider(GameObject target)
		{
			Matrix4x4 rotationMatrixFromZNegativeToDesiredFacing = WidgetTransform.GetRotationMatrixFromZNegativeToDesiredFacing(this.m_facingDirection);
			BoxCollider boxCollider = target.AddComponent<BoxCollider>();
			boxCollider.hideFlags = HideFlags.DontSave;
			boxCollider.center = rotationMatrixFromZNegativeToDesiredFacing * this.Rect.center;
			boxCollider.size = rotationMatrixFromZNegativeToDesiredFacing * this.Rect.size;
			return boxCollider;
		}

		// Token: 0x040096D2 RID: 38610
		private static readonly Quaternion[] s_RotationZNegativeToFacingDirection = new Quaternion[]
		{
			Quaternion.Euler(90f, 0f, 0f),
			Quaternion.identity
		};

		// Token: 0x040096D3 RID: 38611
		[SerializeField]
		private Rect m_rect;

		// Token: 0x040096D4 RID: 38612
		[SerializeField]
		private bool m_hasBounds;

		// Token: 0x040096D5 RID: 38613
		[SerializeField]
		private WidgetTransform.FacingDirection m_facingDirection;

		// Token: 0x040096D6 RID: 38614
		[SerializeField]
		private WidgetTransform.LayoutBoundsMode m_layoutBoundsMode;

		// Token: 0x02002856 RID: 10326
		public enum FacingDirection
		{
			// Token: 0x0400F935 RID: 63797
			YPositive,
			// Token: 0x0400F936 RID: 63798
			ZNegative
		}

		// Token: 0x02002857 RID: 10327
		public enum LayoutBoundsMode
		{
			// Token: 0x0400F938 RID: 63800
			Recursive,
			// Token: 0x0400F939 RID: 63801
			NonRecursive,
			// Token: 0x0400F93A RID: 63802
			Excluded
		}
	}
}
