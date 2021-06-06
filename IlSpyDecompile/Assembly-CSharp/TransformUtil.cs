using System;
using System.Collections.Generic;
using UnityEngine;

public class TransformUtil
{
	public enum PhoneAspectRatio
	{
		Minimum = 0,
		Wide = 1,
		ExtraWide = 2,
		Maximum = 2
	}

	public static Vector3 GetUnitAnchor(Anchor anchor)
	{
		Vector3 result = default(Vector3);
		switch (anchor)
		{
		case Anchor.TOP_LEFT:
			result.x = 0f;
			result.y = 1f;
			result.z = 0f;
			break;
		case Anchor.TOP:
			result.x = 0.5f;
			result.y = 1f;
			result.z = 0f;
			break;
		case Anchor.TOP_RIGHT:
			result.x = 1f;
			result.y = 1f;
			result.z = 0f;
			break;
		case Anchor.LEFT:
			result.x = 0f;
			result.y = 0.5f;
			result.z = 0f;
			break;
		case Anchor.CENTER:
			result.x = 0.5f;
			result.y = 0.5f;
			result.z = 0f;
			break;
		case Anchor.RIGHT:
			result.x = 1f;
			result.y = 0.5f;
			result.z = 0f;
			break;
		case Anchor.BOTTOM_LEFT:
			result.x = 0f;
			result.y = 0f;
			result.z = 0f;
			break;
		case Anchor.BOTTOM:
			result.x = 0.5f;
			result.y = 0f;
			result.z = 0f;
			break;
		case Anchor.BOTTOM_RIGHT:
			result.x = 1f;
			result.y = 0f;
			result.z = 0f;
			break;
		case Anchor.FRONT:
			result.x = 0.5f;
			result.y = 0f;
			result.z = 1f;
			break;
		case Anchor.BACK:
			result.x = 0.5f;
			result.y = 0f;
			result.z = 0f;
			break;
		case Anchor.TOP_LEFT_XZ:
			result.x = 0f;
			result.z = 1f;
			result.y = 0f;
			break;
		case Anchor.TOP_XZ:
			result.x = 0.5f;
			result.z = 1f;
			result.y = 0f;
			break;
		case Anchor.TOP_RIGHT_XZ:
			result.x = 1f;
			result.z = 1f;
			result.y = 0f;
			break;
		case Anchor.LEFT_XZ:
			result.x = 0f;
			result.z = 0.5f;
			result.y = 0f;
			break;
		case Anchor.CENTER_XZ:
			result.x = 0.5f;
			result.z = 0.5f;
			result.y = 0f;
			break;
		case Anchor.RIGHT_XZ:
			result.x = 1f;
			result.z = 0.5f;
			result.y = 0f;
			break;
		case Anchor.BOTTOM_LEFT_XZ:
			result.x = 0f;
			result.z = 0f;
			result.y = 0f;
			break;
		case Anchor.BOTTOM_XZ:
			result.x = 0.5f;
			result.z = 0f;
			result.y = 0f;
			break;
		case Anchor.BOTTOM_RIGHT_XZ:
			result.x = 1f;
			result.z = 0f;
			result.y = 0f;
			break;
		case Anchor.FRONT_XZ:
			result.x = 0.5f;
			result.z = 0f;
			result.y = 1f;
			break;
		case Anchor.BACK_XZ:
			result.x = 0.5f;
			result.z = 0f;
			result.y = 0f;
			break;
		}
		return result;
	}

	public static Vector3 ComputeWorldPoint(Bounds bounds, Vector3 selfUnitAnchor)
	{
		Vector3 result = default(Vector3);
		result.x = Mathf.Lerp(bounds.min.x, bounds.max.x, selfUnitAnchor.x);
		result.y = Mathf.Lerp(bounds.min.y, bounds.max.y, selfUnitAnchor.y);
		result.z = Mathf.Lerp(bounds.min.z, bounds.max.z, selfUnitAnchor.z);
		return result;
	}

	public static Vector2 ComputeUnitAnchor(Bounds bounds, Vector2 worldPoint)
	{
		Vector2 result = default(Vector2);
		result.x = (worldPoint.x - bounds.min.x) / bounds.size.x;
		result.y = (worldPoint.y - bounds.min.y) / bounds.size.y;
		return result;
	}

	public static Bounds ComputeSetPointBounds(Component c)
	{
		return ComputeSetPointBounds(c.gameObject, includeInactive: false);
	}

	public static Bounds ComputeSetPointBounds(GameObject go)
	{
		return ComputeSetPointBounds(go, includeInactive: false);
	}

	public static Bounds ComputeSetPointBounds(Component c, bool includeInactive)
	{
		return ComputeSetPointBounds(c.gameObject, includeInactive);
	}

	public static Bounds ComputeSetPointBounds(GameObject go, bool includeInactive)
	{
		UberText component = go.GetComponent<UberText>();
		if (component != null)
		{
			return component.GetTextWorldSpaceBounds();
		}
		Renderer component2 = go.GetComponent<Renderer>();
		if (component2 != null)
		{
			return component2.bounds;
		}
		BoundsOverride component3 = go.GetComponent<BoundsOverride>();
		if (component3 != null)
		{
			return component3.bounds;
		}
		Collider component4 = go.GetComponent<Collider>();
		if (component4 != null)
		{
			Bounds bounds;
			if (component4.enabled)
			{
				bounds = component4.bounds;
			}
			else
			{
				component4.enabled = true;
				bounds = component4.bounds;
				component4.enabled = false;
			}
			MobileHitBox component5 = go.GetComponent<MobileHitBox>();
			if (component5 != null && component5.HasExecuted())
			{
				bounds.size = new Vector3(bounds.size.x / component5.m_scaleX, bounds.size.y / component5.m_scaleY, bounds.size.z / component5.m_scaleY);
			}
			return bounds;
		}
		return GetBoundsOfChildren(go, includeInactive);
	}

	public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, bool includeAllChildren = true)
	{
		return ComputeOrientedWorldBounds(go, includeUberText: true, includeAllChildren);
	}

	public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, List<GameObject> ignoreMeshes, bool includeAllChildren = true)
	{
		return ComputeOrientedWorldBounds(go, includeUberText: true, ignoreMeshes, includeAllChildren);
	}

	public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, Vector3 minLocalPadding, Vector3 maxLocalPadding, bool includeAllChildren = true)
	{
		return ComputeOrientedWorldBounds(go, includeUberText: true, minLocalPadding, maxLocalPadding, includeAllChildren);
	}

	public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, Vector3 minLocalPadding, Vector3 maxLocalPadding, List<GameObject> ignoreMeshes, bool includeAllChildren = true)
	{
		return ComputeOrientedWorldBounds(go, includeUberText: true, minLocalPadding, maxLocalPadding, ignoreMeshes, includeAllChildren);
	}

	public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, bool includeUberText, bool includeAllChildren = true)
	{
		return ComputeOrientedWorldBounds(go, includeUberText, Vector3.zero, Vector3.zero, null, includeAllChildren);
	}

	public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, bool includeUberText, List<GameObject> ignoreMeshes, bool includeAllChildren = true)
	{
		return ComputeOrientedWorldBounds(go, includeUberText, Vector3.zero, Vector3.zero, ignoreMeshes, includeAllChildren);
	}

	public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, bool includeUberText, Vector3 minLocalPadding, Vector3 maxLocalPadding, bool includeAllChildren = true)
	{
		return ComputeOrientedWorldBounds(go, includeUberText, minLocalPadding, maxLocalPadding, null, includeAllChildren);
	}

	public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, bool includeUberText, Vector3 minLocalPadding, Vector3 maxLocalPadding, List<GameObject> ignoreMeshes, bool includeAllChildren = true)
	{
		if (go == null || !go.activeSelf)
		{
			return null;
		}
		List<MeshFilter> componentsWithIgnore = GetComponentsWithIgnore<MeshFilter>(go, ignoreMeshes, includeAllChildren);
		List<UberText> list = null;
		if (includeUberText)
		{
			list = GetComponentsWithIgnore<UberText>(go, ignoreMeshes, includeAllChildren);
		}
		if ((componentsWithIgnore == null || componentsWithIgnore.Count == 0) && (list == null || list.Count == 0))
		{
			return null;
		}
		Matrix4x4 worldToLocalMatrix = go.transform.worldToLocalMatrix;
		Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
		if (componentsWithIgnore != null)
		{
			foreach (MeshFilter item in componentsWithIgnore)
			{
				if (item.gameObject.activeSelf && !(item.sharedMesh == null))
				{
					Matrix4x4 localToWorldMatrix = item.transform.localToWorldMatrix;
					Bounds bounds = item.sharedMesh.bounds;
					Matrix4x4 matrix4x = worldToLocalMatrix * localToWorldMatrix;
					Vector3[] array = new Vector3[3]
					{
						matrix4x * new Vector3(bounds.extents.x, 0f, 0f),
						matrix4x * new Vector3(0f, bounds.extents.y, 0f),
						matrix4x * new Vector3(0f, 0f, bounds.extents.z)
					};
					Vector3 vector = localToWorldMatrix * item.sharedMesh.bounds.center;
					GetBoundsMinMax(worldToLocalMatrix * (item.transform.position + vector), array[0], array[1], array[2], ref min, ref max);
				}
			}
		}
		if (list != null)
		{
			foreach (UberText item2 in list)
			{
				if (item2.gameObject.activeSelf)
				{
					Matrix4x4 localToWorldMatrix2 = item2.transform.localToWorldMatrix;
					Matrix4x4 matrix4x2 = worldToLocalMatrix * localToWorldMatrix2;
					Vector3[] array2 = new Vector3[3]
					{
						matrix4x2 * new Vector3(item2.Width * 0.5f, 0f, 0f),
						matrix4x2 * new Vector3(0f, item2.Height * 0.5f),
						matrix4x2 * new Vector3(0f, 0f, 0.01f)
					};
					GetBoundsMinMax(worldToLocalMatrix * item2.transform.position, array2[0], array2[1], array2[2], ref min, ref max);
				}
			}
		}
		if (minLocalPadding.sqrMagnitude > 0f)
		{
			min -= minLocalPadding;
		}
		if (maxLocalPadding.sqrMagnitude > 0f)
		{
			max += maxLocalPadding;
		}
		Matrix4x4 localToWorldMatrix3 = go.transform.localToWorldMatrix;
		Matrix4x4 matrix4x3 = localToWorldMatrix3;
		matrix4x3.SetColumn(3, Vector4.zero);
		Vector3 vector2 = (localToWorldMatrix3 * max + localToWorldMatrix3 * min) * 0.5f;
		Vector3 vector3 = (max - min) * 0.5f;
		OrientedBounds orientedBounds = new OrientedBounds();
		orientedBounds.Extents = new Vector3[3]
		{
			matrix4x3 * new Vector3(vector3.x, 0f, 0f),
			matrix4x3 * new Vector3(0f, vector3.y, 0f),
			matrix4x3 * new Vector3(0f, 0f, vector3.z)
		};
		orientedBounds.Origin = vector2;
		orientedBounds.CenterOffset = go.transform.position - vector2;
		return orientedBounds;
	}

	public static bool CanComputeOrientedWorldBounds(GameObject go, bool includeAllChildren = true)
	{
		return CanComputeOrientedWorldBounds(go, includeUberText: true, includeAllChildren);
	}

	public static bool CanComputeOrientedWorldBounds(GameObject go, List<GameObject> ignoreMeshes, bool includeAllChildren = true)
	{
		return CanComputeOrientedWorldBounds(go, includeUberText: true, ignoreMeshes, includeAllChildren);
	}

	public static bool CanComputeOrientedWorldBounds(GameObject go, bool includeUberText, bool includeAllChildren = true)
	{
		return CanComputeOrientedWorldBounds(go, includeUberText, null, includeAllChildren);
	}

	public static bool CanComputeOrientedWorldBounds(GameObject go, bool includeUberText, List<GameObject> ignoreMeshes, bool includeAllChildren = true)
	{
		if (go == null || !go.activeSelf)
		{
			return false;
		}
		List<MeshFilter> componentsWithIgnore = GetComponentsWithIgnore<MeshFilter>(go, ignoreMeshes, includeAllChildren);
		if (componentsWithIgnore != null && componentsWithIgnore.Count > 0)
		{
			return true;
		}
		if (includeUberText)
		{
			List<UberText> componentsWithIgnore2 = GetComponentsWithIgnore<UberText>(go, ignoreMeshes, includeAllChildren);
			if (componentsWithIgnore2 != null)
			{
				return componentsWithIgnore2.Count > 0;
			}
			return false;
		}
		return false;
	}

	public static List<T> GetComponentsWithIgnore<T>(GameObject obj, List<GameObject> ignoreObjects, bool includeAllChildren = true) where T : Component
	{
		List<T> list = new List<T>();
		if (includeAllChildren)
		{
			obj.GetComponentsInChildren(list);
		}
		T component = obj.GetComponent<T>();
		if ((UnityEngine.Object)component != (UnityEngine.Object)null)
		{
			list.Add(component);
		}
		if (ignoreObjects != null && ignoreObjects.Count > 0)
		{
			T[] array = list.ToArray();
			list.Clear();
			T[] array2 = array;
			foreach (T val in array2)
			{
				bool flag = true;
				foreach (GameObject ignoreObject in ignoreObjects)
				{
					if (ignoreObject == null || val.transform == ignoreObject.transform || val.transform.IsChildOf(ignoreObject.transform))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					list.Add(val);
				}
			}
		}
		return list;
	}

	public static Vector3[] GetBoundCorners(Vector3 origin, Vector3 xExtent, Vector3 yExtent, Vector3 zExtent)
	{
		Vector3 vector = origin + xExtent;
		Vector3 vector2 = origin - xExtent;
		Vector3 vector3 = yExtent + zExtent;
		Vector3 vector4 = yExtent - zExtent;
		Vector3 vector5 = -yExtent + zExtent;
		Vector3 vector6 = -yExtent - zExtent;
		return new Vector3[8]
		{
			vector + vector3,
			vector + vector4,
			vector + vector5,
			vector + vector6,
			vector2 - vector3,
			vector2 - vector4,
			vector2 - vector5,
			vector2 - vector6
		};
	}

	public static void GetBoundsMinMax(Vector3 origin, Vector3 xExtent, Vector3 yExtent, Vector3 zExtent, ref Vector3 min, ref Vector3 max)
	{
		Vector3[] boundCorners = GetBoundCorners(origin, xExtent, yExtent, zExtent);
		for (int i = 0; i < boundCorners.Length; i++)
		{
			min.x = Mathf.Min(boundCorners[i].x, min.x);
			min.y = Mathf.Min(boundCorners[i].y, min.y);
			min.z = Mathf.Min(boundCorners[i].z, min.z);
			max.x = Mathf.Max(boundCorners[i].x, max.x);
			max.y = Mathf.Max(boundCorners[i].y, max.y);
			max.z = Mathf.Max(boundCorners[i].z, max.z);
		}
	}

	public static void SetLocalScaleToWorldDimension(GameObject obj, params WorldDimensionIndex[] dimensions)
	{
		SetLocalScaleToWorldDimension(obj, null, dimensions);
	}

	public static void SetLocalScaleToWorldDimension(GameObject obj, List<GameObject> ignoreMeshes, params WorldDimensionIndex[] dimensions)
	{
		Vector3 localScale = obj.transform.localScale;
		OrientedBounds orientedBounds = ComputeOrientedWorldBounds(obj, ignoreMeshes);
		for (int i = 0; i < dimensions.Length; i++)
		{
			float num = orientedBounds.Extents[dimensions[i].Index].magnitude * 2f;
			localScale[dimensions[i].Index] *= ((num <= Mathf.Epsilon) ? 0.001f : (dimensions[i].Dimension / num));
			if (Mathf.Abs(localScale[dimensions[i].Index]) < 0.001f)
			{
				localScale[dimensions[i].Index] = 0.001f;
			}
		}
		obj.transform.localScale = localScale;
	}

	public static void SetPoint(Component src, Anchor srcAnchor, Component dst, Anchor dstAnchor)
	{
		SetPoint(src.gameObject, GetUnitAnchor(srcAnchor), dst.gameObject, GetUnitAnchor(dstAnchor), Vector3.zero, includeInactive: false);
	}

	public static void SetPoint(Component src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor)
	{
		SetPoint(src.gameObject, GetUnitAnchor(srcAnchor), dst, GetUnitAnchor(dstAnchor), Vector3.zero, includeInactive: false);
	}

	public static void SetPoint(GameObject src, Anchor srcAnchor, Component dst, Anchor dstAnchor)
	{
		SetPoint(src, GetUnitAnchor(srcAnchor), dst.gameObject, GetUnitAnchor(dstAnchor), Vector3.zero, includeInactive: false);
	}

	public static void SetPoint(GameObject src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor)
	{
		SetPoint(src, GetUnitAnchor(srcAnchor), dst, GetUnitAnchor(dstAnchor), Vector3.zero, includeInactive: false);
	}

	public static void SetPoint(Component src, Anchor srcAnchor, Component dst, Anchor dstAnchor, bool includeInactive)
	{
		SetPoint(src.gameObject, GetUnitAnchor(srcAnchor), dst.gameObject, GetUnitAnchor(dstAnchor), Vector3.zero, includeInactive);
	}

	public static void SetPoint(Component src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor, bool includeInactive)
	{
		SetPoint(src.gameObject, GetUnitAnchor(srcAnchor), dst, GetUnitAnchor(dstAnchor), Vector3.zero, includeInactive);
	}

	public static void SetPoint(GameObject src, Anchor srcAnchor, Component dst, Anchor dstAnchor, bool includeInactive)
	{
		SetPoint(src, GetUnitAnchor(srcAnchor), dst.gameObject, GetUnitAnchor(dstAnchor), Vector3.zero, includeInactive);
	}

	public static void SetPoint(GameObject src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor, bool includeInactive)
	{
		SetPoint(src, GetUnitAnchor(srcAnchor), dst, GetUnitAnchor(dstAnchor), Vector3.zero, includeInactive);
	}

	public static void SetPoint(Component src, Anchor srcAnchor, Component dst, Anchor dstAnchor, Vector3 offset)
	{
		SetPoint(src.gameObject, GetUnitAnchor(srcAnchor), dst.gameObject, GetUnitAnchor(dstAnchor), offset, includeInactive: false);
	}

	public static void SetPoint(Component src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor, Vector3 offset)
	{
		SetPoint(src.gameObject, GetUnitAnchor(srcAnchor), dst, GetUnitAnchor(dstAnchor), offset, includeInactive: false);
	}

	public static void SetPoint(GameObject src, Anchor srcAnchor, Component dst, Anchor dstAnchor, Vector3 offset)
	{
		SetPoint(src, GetUnitAnchor(srcAnchor), dst.gameObject, GetUnitAnchor(dstAnchor), offset, includeInactive: false);
	}

	public static void SetPoint(GameObject src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor, Vector3 offset)
	{
		SetPoint(src, GetUnitAnchor(srcAnchor), dst, GetUnitAnchor(dstAnchor), offset, includeInactive: false);
	}

	public static void SetPoint(Component src, Anchor srcAnchor, Component dst, Anchor dstAnchor, Vector3 offset, bool includeInactive)
	{
		SetPoint(src.gameObject, GetUnitAnchor(srcAnchor), dst.gameObject, GetUnitAnchor(dstAnchor), offset, includeInactive);
	}

	public static void SetPoint(Component src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor, Vector3 offset, bool includeInactive)
	{
		SetPoint(src.gameObject, GetUnitAnchor(srcAnchor), dst, GetUnitAnchor(dstAnchor), offset, includeInactive);
	}

	public static void SetPoint(GameObject src, Anchor srcAnchor, Component dst, Anchor dstAnchor, Vector3 offset, bool includeInactive)
	{
		SetPoint(src, GetUnitAnchor(srcAnchor), dst.gameObject, GetUnitAnchor(dstAnchor), offset, includeInactive);
	}

	public static void SetPoint(GameObject src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor, Vector3 offset, bool includeInactive)
	{
		SetPoint(src, GetUnitAnchor(srcAnchor), dst, GetUnitAnchor(dstAnchor), offset, includeInactive);
	}

	public static void SetPoint(Component self, Vector3 selfUnitAnchor, Component relative, Vector3 relativeUnitAnchor)
	{
		SetPoint(self.gameObject, selfUnitAnchor, relative.gameObject, relativeUnitAnchor, Vector3.zero, includeInactive: false);
	}

	public static void SetPoint(Component self, Vector3 selfUnitAnchor, GameObject relative, Vector3 relativeUnitAnchor)
	{
		SetPoint(self.gameObject, selfUnitAnchor, relative, relativeUnitAnchor, Vector3.zero, includeInactive: false);
	}

	public static void SetPoint(GameObject self, Vector3 selfUnitAnchor, Component relative, Vector3 relativeUnitAnchor)
	{
		SetPoint(self, selfUnitAnchor, relative.gameObject, relativeUnitAnchor, Vector3.zero, includeInactive: false);
	}

	public static void SetPoint(GameObject self, Vector3 selfUnitAnchor, GameObject relative, Vector3 relativeUnitAnchor)
	{
		SetPoint(self, selfUnitAnchor, relative, relativeUnitAnchor, Vector3.zero, includeInactive: false);
	}

	public static void SetPoint(Component self, Vector3 selfUnitAnchor, Component relative, Vector3 relativeUnitAnchor, Vector3 offset)
	{
		SetPoint(self.gameObject, selfUnitAnchor, relative.gameObject, relativeUnitAnchor, offset, includeInactive: false);
	}

	public static void SetPoint(Component self, Vector3 selfUnitAnchor, GameObject relative, Vector3 relativeUnitAnchor, Vector3 offset)
	{
		SetPoint(self.gameObject, selfUnitAnchor, relative, relativeUnitAnchor, offset, includeInactive: false);
	}

	public static void SetPoint(GameObject self, Vector3 selfUnitAnchor, Component relative, Vector3 relativeUnitAnchor, Vector3 offset)
	{
		SetPoint(self, selfUnitAnchor, relative.gameObject, relativeUnitAnchor, offset, includeInactive: false);
	}

	public static void SetPoint(GameObject self, Vector3 selfUnitAnchor, GameObject relative, Vector3 relativeUnitAnchor, Vector3 offset)
	{
		SetPoint(self, selfUnitAnchor, relative, relativeUnitAnchor, offset, includeInactive: false);
	}

	public static void SetPoint(Component self, Vector3 selfUnitAnchor, Component relative, Vector3 relativeUnitAnchor, Vector3 offset, bool includeInactive)
	{
		SetPoint(self.gameObject, selfUnitAnchor, relative.gameObject, relativeUnitAnchor, offset, includeInactive);
	}

	public static void SetPoint(Component self, Vector3 selfUnitAnchor, GameObject relative, Vector3 relativeUnitAnchor, Vector3 offset, bool includeInactive)
	{
		SetPoint(self.gameObject, selfUnitAnchor, relative, relativeUnitAnchor, offset, includeInactive);
	}

	public static void SetPoint(GameObject self, Vector3 selfUnitAnchor, Component relative, Vector3 relativeUnitAnchor, Vector3 offset, bool includeInactive)
	{
		SetPoint(self, selfUnitAnchor, relative.gameObject, relativeUnitAnchor, offset, includeInactive);
	}

	public static void SetPoint(GameObject self, Vector3 selfUnitAnchor, GameObject relative, Vector3 relativeUnitAnchor, Vector3 offset, bool includeInactive)
	{
		if ((bool)self && (bool)relative)
		{
			Bounds bounds = ComputeSetPointBounds(self, includeInactive);
			Bounds bounds2 = ComputeSetPointBounds(relative, includeInactive);
			Vector3 vector = ComputeWorldPoint(bounds, selfUnitAnchor);
			Vector3 vector2 = ComputeWorldPoint(bounds2, relativeUnitAnchor);
			Vector3 translation = new Vector3(vector2.x - vector.x + offset.x, vector2.y - vector.y + offset.y, vector2.z - vector.z + offset.z);
			self.transform.Translate(translation, Space.World);
		}
	}

	public static Bounds GetBoundsOfChildren(Component c)
	{
		return GetBoundsOfChildren(c.gameObject, includeInactive: false);
	}

	public static Bounds GetBoundsOfChildren(GameObject go)
	{
		return GetBoundsOfChildren(go, includeInactive: false);
	}

	public static Bounds GetBoundsOfChildren(Component c, bool includeInactive)
	{
		return GetBoundsOfChildren(c.gameObject, includeInactive);
	}

	public static Bounds GetBoundsOfChildren(GameObject go, bool includeInactive)
	{
		Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>(includeInactive);
		if (componentsInChildren.Length == 0)
		{
			return new Bounds(go.transform.position, Vector3.zero);
		}
		Bounds bounds = componentsInChildren[0].bounds;
		for (int i = 1; i < componentsInChildren.Length; i++)
		{
			Bounds bounds2 = componentsInChildren[i].bounds;
			Vector3 max = Vector3.Max(bounds2.max, bounds.max);
			Vector3 min = Vector3.Min(bounds2.min, bounds.min);
			bounds.SetMinMax(min, max);
		}
		return bounds;
	}

	public static Vector3 Divide(Vector3 v1, Vector3 v2)
	{
		return new Vector3(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
	}

	public static Vector3 Multiply(Vector3 v1, Vector3 v2)
	{
		return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
	}

	public static void SetLocalPosX(GameObject go, float x)
	{
		Transform transform = go.transform;
		transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
	}

	public static void SetLocalPosX(Component component, float x)
	{
		Transform transform = component.transform;
		transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
	}

	public static void SetLocalPosY(GameObject go, float y)
	{
		Transform transform = go.transform;
		transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
	}

	public static void SetLocalPosY(Component component, float y)
	{
		Transform transform = component.transform;
		transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
	}

	public static void SetLocalPosZ(GameObject go, float z)
	{
		Transform transform = go.transform;
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
	}

	public static void SetLocalPosZ(Component component, float z)
	{
		Transform transform = component.transform;
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
	}

	public static void SetPosX(GameObject go, float x)
	{
		Transform transform = go.transform;
		transform.position = new Vector3(x, transform.position.y, transform.position.z);
	}

	public static void SetPosX(Component component, float x)
	{
		Transform transform = component.transform;
		transform.position = new Vector3(x, transform.position.y, transform.position.z);
	}

	public static void SetPosY(GameObject go, float y)
	{
		Transform transform = go.transform;
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}

	public static void SetPosY(Component component, float y)
	{
		Transform transform = component.transform;
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}

	public static void SetPosZ(GameObject go, float z)
	{
		Transform transform = go.transform;
		transform.position = new Vector3(transform.position.x, transform.position.y, z);
	}

	public static void SetPosZ(Component component, float z)
	{
		Transform transform = component.transform;
		transform.position = new Vector3(transform.position.x, transform.position.y, z);
	}

	public static void SetLocalEulerAngleX(GameObject go, float x)
	{
		Transform transform = go.transform;
		transform.localEulerAngles = new Vector3(x, transform.localEulerAngles.y, transform.localEulerAngles.z);
	}

	public static void SetLocalEulerAngleX(Component c, float x)
	{
		Transform transform = c.transform;
		transform.localEulerAngles = new Vector3(x, transform.localEulerAngles.y, transform.localEulerAngles.z);
	}

	public static void SetLocalEulerAngleY(GameObject go, float y)
	{
		Transform transform = go.transform;
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, y, transform.localEulerAngles.z);
	}

	public static void SetLocalEulerAngleY(Component c, float y)
	{
		Transform transform = c.transform;
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, y, transform.localEulerAngles.z);
	}

	public static void SetLocalEulerAngleZ(GameObject go, float z)
	{
		Transform transform = go.transform;
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, z);
	}

	public static void SetLocalEulerAngleZ(Component c, float z)
	{
		Transform transform = c.transform;
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, z);
	}

	public static void SetEulerAngleX(GameObject go, float x)
	{
		Transform transform = go.transform;
		transform.eulerAngles = new Vector3(x, transform.eulerAngles.y, transform.eulerAngles.z);
	}

	public static void SetEulerAngleX(Component c, float x)
	{
		Transform transform = c.transform;
		transform.eulerAngles = new Vector3(x, transform.eulerAngles.y, transform.eulerAngles.z);
	}

	public static void SetEulerAngleY(GameObject go, float y)
	{
		Transform transform = go.transform;
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, y, transform.eulerAngles.z);
	}

	public static void SetEulerAngleY(Component c, float y)
	{
		Transform transform = c.transform;
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, y, transform.eulerAngles.z);
	}

	public static void SetEulerAngleZ(GameObject go, float z)
	{
		Transform transform = go.transform;
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, z);
	}

	public static void SetEulerAngleZ(Component c, float z)
	{
		Transform transform = c.transform;
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, z);
	}

	public static void SetLocalScaleX(Component component, float x)
	{
		Transform transform = component.transform;
		transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
	}

	public static void SetLocalScaleX(GameObject go, float x)
	{
		Transform transform = go.transform;
		transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
	}

	public static void SetLocalScaleY(Component component, float y)
	{
		Transform transform = component.transform;
		transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
	}

	public static void SetLocalScaleY(GameObject go, float y)
	{
		Transform transform = go.transform;
		transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
	}

	public static void SetLocalScaleZ(Component component, float z)
	{
		Transform transform = component.transform;
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
	}

	public static void SetLocalScaleZ(GameObject go, float z)
	{
		Transform transform = go.transform;
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
	}

	public static void SetLocalScaleXY(Component component, float x, float y)
	{
		Transform transform = component.transform;
		transform.localScale = new Vector3(x, y, transform.localScale.z);
	}

	public static void SetLocalScaleXY(GameObject go, float x, float y)
	{
		Transform transform = go.transform;
		transform.localScale = new Vector3(x, y, transform.localScale.z);
	}

	public static void SetLocalScaleXY(Component component, Vector2 v)
	{
		Transform transform = component.transform;
		transform.localScale = new Vector3(v.x, v.y, transform.localScale.z);
	}

	public static void SetLocalScaleXY(GameObject go, Vector2 v)
	{
		Transform transform = go.transform;
		transform.localScale = new Vector3(v.x, v.y, transform.localScale.z);
	}

	public static void SetLocalScaleXZ(Component component, float x, float z)
	{
		Transform transform = component.transform;
		transform.localScale = new Vector3(x, transform.localScale.y, z);
	}

	public static void SetLocalScaleXZ(GameObject go, float x, float z)
	{
		Transform transform = go.transform;
		transform.localScale = new Vector3(x, transform.localScale.y, z);
	}

	public static void SetLocalScaleXZ(Component component, Vector2 v)
	{
		Transform transform = component.transform;
		transform.localScale = new Vector3(v.x, transform.localScale.y, v.y);
	}

	public static void SetLocalScaleXZ(GameObject go, Vector2 v)
	{
		Transform transform = go.transform;
		transform.localScale = new Vector3(v.x, transform.localScale.y, v.y);
	}

	public static void SetLocalScaleYZ(Component component, float y, float z)
	{
		Transform transform = component.transform;
		transform.localScale = new Vector3(transform.localScale.x, y, z);
	}

	public static void SetLocalScaleYZ(GameObject go, float y, float z)
	{
		Transform transform = go.transform;
		transform.localScale = new Vector3(transform.localScale.x, y, z);
	}

	public static void SetLocalScaleYZ(Component component, Vector2 v)
	{
		Transform transform = component.transform;
		transform.localScale = new Vector3(transform.localScale.x, v.x, v.y);
	}

	public static void SetLocalScaleYZ(GameObject go, Vector2 v)
	{
		Transform transform = go.transform;
		transform.localScale = new Vector3(transform.localScale.x, v.x, v.y);
	}

	public static void Identity(Component c)
	{
		c.transform.localScale = Vector3.one;
		c.transform.localRotation = Quaternion.identity;
		c.transform.localPosition = Vector3.zero;
	}

	public static void Identity(GameObject go)
	{
		go.transform.localScale = Vector3.one;
		go.transform.localRotation = Quaternion.identity;
		go.transform.localPosition = Vector3.zero;
	}

	public static void CopyLocal(Component destination, Component source)
	{
		CopyLocal(destination.gameObject, source.gameObject);
	}

	public static void CopyLocal(Component destination, GameObject source)
	{
		CopyLocal(destination.gameObject, source);
	}

	public static void CopyLocal(GameObject destination, Component source)
	{
		CopyLocal(destination, source.gameObject);
	}

	public static void CopyLocal(GameObject destination, GameObject source)
	{
		destination.transform.localScale = source.transform.localScale;
		destination.transform.localRotation = source.transform.localRotation;
		destination.transform.localPosition = source.transform.localPosition;
	}

	public static void CopyLocal(Component destination, TransformProps source)
	{
		CopyLocal(destination.gameObject, source);
	}

	public static void CopyLocal(GameObject destination, TransformProps source)
	{
		destination.transform.localScale = source.scale;
		destination.transform.localRotation = source.rotation;
		destination.transform.localPosition = source.position;
	}

	public static void CopyLocal(TransformProps destination, Component source)
	{
		CopyLocal(destination, source.gameObject);
	}

	public static void CopyLocal(TransformProps destination, GameObject source)
	{
		destination.scale = source.transform.localScale;
		destination.rotation = source.transform.localRotation;
		destination.position = source.transform.localPosition;
	}

	public static void CopyWorld(Component destination, Component source)
	{
		CopyWorld(destination.gameObject, source);
	}

	public static void CopyWorld(Component destination, GameObject source)
	{
		CopyWorld(destination.gameObject, source);
	}

	public static void CopyWorld(GameObject destination, Component source)
	{
		CopyWorld(destination, source.gameObject);
	}

	public static void CopyWorld(GameObject destination, GameObject source)
	{
		CopyWorldScale(destination, source);
		destination.transform.rotation = source.transform.rotation;
		destination.transform.position = source.transform.position;
	}

	public static void CopyWorld(Component destination, TransformProps source)
	{
		CopyWorld(destination.gameObject, source);
	}

	public static void CopyWorld(GameObject destination, TransformProps source)
	{
		SetWorldScale(destination, source.scale);
		destination.transform.rotation = source.rotation;
		destination.transform.position = source.position;
	}

	public static void CopyWorld(TransformProps destination, Component source)
	{
		CopyWorld(destination, source.gameObject);
	}

	public static void CopyWorld(TransformProps destination, GameObject source)
	{
		destination.scale = ComputeWorldScale(source);
		destination.rotation = source.transform.rotation;
		destination.position = source.transform.position;
	}

	public static void CopyWorldScale(Component destination, Component source)
	{
		CopyWorldScale(destination.gameObject, source.gameObject);
	}

	public static void CopyWorldScale(Component destination, GameObject source)
	{
		CopyWorldScale(destination.gameObject, source);
	}

	public static void CopyWorldScale(GameObject destination, Component source)
	{
		CopyWorldScale(destination, source.gameObject);
	}

	public static void CopyWorldScale(GameObject destination, GameObject source)
	{
		Vector3 scale = ComputeWorldScale(source);
		SetWorldScale(destination, scale);
	}

	public static void SetWorldScale(Component destination, Vector3 scale)
	{
		SetWorldScale(destination.gameObject, scale);
	}

	public static void SetWorldScale(GameObject destination, Vector3 scale)
	{
		if (destination.transform.parent != null)
		{
			Transform parent = destination.transform.parent;
			while (parent != null)
			{
				scale.Scale(Vector3Reciprocal(parent.localScale));
				parent = parent.parent;
			}
		}
		destination.transform.localScale = scale;
	}

	public static Vector3 ComputeWorldScale(Component c)
	{
		return ComputeWorldScale(c.gameObject);
	}

	public static Vector3 ComputeWorldScale(GameObject go)
	{
		Vector3 localScale = go.transform.localScale;
		if (go.transform.parent != null)
		{
			Transform parent = go.transform.parent;
			while (parent != null)
			{
				localScale.Scale(parent.localScale);
				parent = parent.parent;
			}
		}
		return localScale;
	}

	public static Vector3 Vector3Reciprocal(Vector3 source)
	{
		Vector3 result = source;
		if (result.x != 0f)
		{
			result.x = 1f / result.x;
		}
		if (result.y != 0f)
		{
			result.y = 1f / result.y;
		}
		if (result.z != 0f)
		{
			result.z = 1f / result.z;
		}
		return result;
	}

	public static void OrientTo(GameObject source, GameObject target)
	{
		OrientTo(source.transform, target.transform);
	}

	public static void OrientTo(GameObject source, Component target)
	{
		OrientTo(source.transform, target.transform);
	}

	public static void OrientTo(Component source, GameObject target)
	{
		OrientTo(source.transform, target.transform);
	}

	public static void OrientTo(Component source, Component target)
	{
		OrientTo(source.transform, target.transform);
	}

	public static void OrientTo(Transform source, Transform target)
	{
		OrientTo(source, source.transform.position, target.transform.position);
	}

	public static void OrientTo(Transform source, Vector3 sourcePosition, Vector3 targetPosition)
	{
		Vector3 forward = targetPosition - sourcePosition;
		if (forward.sqrMagnitude > Mathf.Epsilon)
		{
			source.rotation = Quaternion.LookRotation(forward);
		}
	}

	public static Vector3 RandomVector3(Vector3 min, Vector3 max)
	{
		Vector3 result = default(Vector3);
		result.x = UnityEngine.Random.Range(min.x, max.x);
		result.y = UnityEngine.Random.Range(min.y, max.y);
		result.z = UnityEngine.Random.Range(min.z, max.z);
		return result;
	}

	public static void AttachAndPreserveLocalTransform(Transform child, Transform parent)
	{
		TransformProps transformProps = new TransformProps();
		CopyLocal(transformProps, child);
		child.parent = parent;
		CopyLocal(child, transformProps);
	}

	public static float GetAspectRatioValue(PhoneAspectRatio aspectRatio)
	{
		return aspectRatio switch
		{
			PhoneAspectRatio.Minimum => 1.5f, 
			PhoneAspectRatio.Wide => 1.77777779f, 
			PhoneAspectRatio.ExtraWide => 2.04f, 
			_ => 0f, 
		};
	}

	public static Vector3 GetAspectRatioDependentPosition(Vector3 aspectSmall, Vector3 aspectWide, Vector3 aspectExtraWide)
	{
		return GetAspectRatioDependentValue(Vector3.Lerp, aspectSmall, aspectWide, aspectExtraWide);
	}

	public static float GetAspectRatioDependentValue(float aspectSmall, float aspectWide, float aspectExtraWide)
	{
		return GetAspectRatioDependentValue(Mathf.Lerp, aspectSmall, aspectWide, aspectExtraWide);
	}

	private static T GetAspectRatioDependentValue<T>(Func<T, T, float, T> interpolator, T small, T wide, T extraWide)
	{
		Dictionary<PhoneAspectRatio, T> dictionary = new Dictionary<PhoneAspectRatio, T>
		{
			{
				PhoneAspectRatio.Minimum,
				small
			},
			{
				PhoneAspectRatio.Wide,
				wide
			},
			{
				PhoneAspectRatio.ExtraWide,
				extraWide
			}
		};
		PhoneAspectRatio lowerRatio;
		PhoneAspectRatio upperRatio;
		float arg = PhoneAspectRatioScale(out lowerRatio, out upperRatio);
		return interpolator(dictionary[lowerRatio], dictionary[upperRatio], arg);
	}

	public static bool IsExtraWideAspectRatio()
	{
		return GetAspectRatioDependentValue(0f, 1f, 2f) > 1.2f;
	}

	private static float PhoneAspectRatioScale(out PhoneAspectRatio lowerRatio, out PhoneAspectRatio upperRatio)
	{
		float num = (float)Screen.width / (float)Screen.height;
		lowerRatio = PhoneAspectRatio.Minimum;
		upperRatio = PhoneAspectRatio.ExtraWide;
		int num2 = EnumUtils.Length<PhoneAspectRatio>();
		for (int i = 0; i < num2; i++)
		{
			PhoneAspectRatio phoneAspectRatio = (PhoneAspectRatio)i;
			if (GetAspectRatioValue(phoneAspectRatio) > num)
			{
				lowerRatio = ((i > 0) ? ((PhoneAspectRatio)(i - 1)) : PhoneAspectRatio.Minimum);
				upperRatio = ((i == 0) ? ((PhoneAspectRatio)(i + 1)) : phoneAspectRatio);
				break;
			}
		}
		float aspectRatioValue = GetAspectRatioValue(lowerRatio);
		float aspectRatioValue2 = GetAspectRatioValue(upperRatio);
		float num3 = aspectRatioValue2 - aspectRatioValue;
		num = Mathf.Clamp(num, aspectRatioValue, aspectRatioValue2);
		return (num - aspectRatioValue) / num3;
	}

	public static void ConstrainToScreen(GameObject go, int layer)
	{
		Camera camera = CameraUtils.FindFirstByLayer(layer);
		if (camera == null)
		{
			Log.All.PrintError("TransformUtil.ConstrainToScreen - No camera found for indicated layer.");
			return;
		}
		Bounds bounds = ComputeSetPointBounds(go);
		Vector3[] array = new Vector3[4];
		camera.CalculateFrustumCorners(z: camera.transform.InverseTransformPoint(bounds.center).z, viewport: new Rect(0f, 0f, 1f, 1f), eye: Camera.MonoOrStereoscopicEye.Mono, outCorners: array);
		Bounds bounds2 = new Bounds(camera.transform.TransformPoint(array[0]), default(Vector3));
		for (int i = 1; i < 4; i++)
		{
			Vector3 point = camera.transform.TransformPoint(array[i]);
			bounds2.Encapsulate(point);
		}
		Vector3 position = go.transform.position;
		bounds2.SetMinMax(bounds2.min - (bounds.min - position), bounds2.max - (bounds.max - position));
		Vector3 vector = bounds2.ClosestPoint(position) - position;
		vector -= camera.transform.forward * Vector3.Dot(camera.transform.forward, vector);
		go.transform.position += vector;
	}
}
