using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009FD RID: 2557
public class TransformUtil
{
	// Token: 0x06008A42 RID: 35394 RVA: 0x002C4C20 File Offset: 0x002C2E20
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

	// Token: 0x06008A43 RID: 35395 RVA: 0x002C5014 File Offset: 0x002C3214
	public static Vector3 ComputeWorldPoint(Bounds bounds, Vector3 selfUnitAnchor)
	{
		return new Vector3
		{
			x = Mathf.Lerp(bounds.min.x, bounds.max.x, selfUnitAnchor.x),
			y = Mathf.Lerp(bounds.min.y, bounds.max.y, selfUnitAnchor.y),
			z = Mathf.Lerp(bounds.min.z, bounds.max.z, selfUnitAnchor.z)
		};
	}

	// Token: 0x06008A44 RID: 35396 RVA: 0x002C50A8 File Offset: 0x002C32A8
	public static Vector2 ComputeUnitAnchor(Bounds bounds, Vector2 worldPoint)
	{
		return new Vector2
		{
			x = (worldPoint.x - bounds.min.x) / bounds.size.x,
			y = (worldPoint.y - bounds.min.y) / bounds.size.y
		};
	}

	// Token: 0x06008A45 RID: 35397 RVA: 0x002C510C File Offset: 0x002C330C
	public static Bounds ComputeSetPointBounds(Component c)
	{
		return TransformUtil.ComputeSetPointBounds(c.gameObject, false);
	}

	// Token: 0x06008A46 RID: 35398 RVA: 0x002C511A File Offset: 0x002C331A
	public static Bounds ComputeSetPointBounds(GameObject go)
	{
		return TransformUtil.ComputeSetPointBounds(go, false);
	}

	// Token: 0x06008A47 RID: 35399 RVA: 0x002C5123 File Offset: 0x002C3323
	public static Bounds ComputeSetPointBounds(Component c, bool includeInactive)
	{
		return TransformUtil.ComputeSetPointBounds(c.gameObject, includeInactive);
	}

	// Token: 0x06008A48 RID: 35400 RVA: 0x002C5134 File Offset: 0x002C3334
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
		return TransformUtil.GetBoundsOfChildren(go, includeInactive);
	}

	// Token: 0x06008A49 RID: 35401 RVA: 0x002C522E File Offset: 0x002C342E
	public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, bool includeAllChildren = true)
	{
		return TransformUtil.ComputeOrientedWorldBounds(go, true, includeAllChildren);
	}

	// Token: 0x06008A4A RID: 35402 RVA: 0x002C5238 File Offset: 0x002C3438
	public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, List<GameObject> ignoreMeshes, bool includeAllChildren = true)
	{
		return TransformUtil.ComputeOrientedWorldBounds(go, true, ignoreMeshes, includeAllChildren);
	}

	// Token: 0x06008A4B RID: 35403 RVA: 0x002C5243 File Offset: 0x002C3443
	public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, Vector3 minLocalPadding, Vector3 maxLocalPadding, bool includeAllChildren = true)
	{
		return TransformUtil.ComputeOrientedWorldBounds(go, true, minLocalPadding, maxLocalPadding, includeAllChildren);
	}

	// Token: 0x06008A4C RID: 35404 RVA: 0x002C524F File Offset: 0x002C344F
	public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, Vector3 minLocalPadding, Vector3 maxLocalPadding, List<GameObject> ignoreMeshes, bool includeAllChildren = true)
	{
		return TransformUtil.ComputeOrientedWorldBounds(go, true, minLocalPadding, maxLocalPadding, ignoreMeshes, includeAllChildren);
	}

	// Token: 0x06008A4D RID: 35405 RVA: 0x002C525D File Offset: 0x002C345D
	public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, bool includeUberText, bool includeAllChildren = true)
	{
		return TransformUtil.ComputeOrientedWorldBounds(go, includeUberText, Vector3.zero, Vector3.zero, null, includeAllChildren);
	}

	// Token: 0x06008A4E RID: 35406 RVA: 0x002C5272 File Offset: 0x002C3472
	public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, bool includeUberText, List<GameObject> ignoreMeshes, bool includeAllChildren = true)
	{
		return TransformUtil.ComputeOrientedWorldBounds(go, includeUberText, Vector3.zero, Vector3.zero, ignoreMeshes, includeAllChildren);
	}

	// Token: 0x06008A4F RID: 35407 RVA: 0x002C5287 File Offset: 0x002C3487
	public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, bool includeUberText, Vector3 minLocalPadding, Vector3 maxLocalPadding, bool includeAllChildren = true)
	{
		return TransformUtil.ComputeOrientedWorldBounds(go, includeUberText, minLocalPadding, maxLocalPadding, null, includeAllChildren);
	}

	// Token: 0x06008A50 RID: 35408 RVA: 0x002C5298 File Offset: 0x002C3498
	public static OrientedBounds ComputeOrientedWorldBounds(GameObject go, bool includeUberText, Vector3 minLocalPadding, Vector3 maxLocalPadding, List<GameObject> ignoreMeshes, bool includeAllChildren = true)
	{
		if (go == null || !go.activeSelf)
		{
			return null;
		}
		List<MeshFilter> componentsWithIgnore = TransformUtil.GetComponentsWithIgnore<MeshFilter>(go, ignoreMeshes, includeAllChildren);
		List<UberText> list = null;
		if (includeUberText)
		{
			list = TransformUtil.GetComponentsWithIgnore<UberText>(go, ignoreMeshes, includeAllChildren);
		}
		if ((componentsWithIgnore == null || componentsWithIgnore.Count == 0) && (list == null || list.Count == 0))
		{
			return null;
		}
		Matrix4x4 worldToLocalMatrix = go.transform.worldToLocalMatrix;
		Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		Vector3 vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
		if (componentsWithIgnore != null)
		{
			foreach (MeshFilter meshFilter in componentsWithIgnore)
			{
				if (meshFilter.gameObject.activeSelf && !(meshFilter.sharedMesh == null))
				{
					Matrix4x4 localToWorldMatrix = meshFilter.transform.localToWorldMatrix;
					Bounds bounds = meshFilter.sharedMesh.bounds;
					Matrix4x4 lhs = worldToLocalMatrix * localToWorldMatrix;
					Vector3[] array = new Vector3[]
					{
						lhs * new Vector3(bounds.extents.x, 0f, 0f),
						lhs * new Vector3(0f, bounds.extents.y, 0f),
						lhs * new Vector3(0f, 0f, bounds.extents.z)
					};
					Vector3 b = localToWorldMatrix * meshFilter.sharedMesh.bounds.center;
					TransformUtil.GetBoundsMinMax(worldToLocalMatrix * (meshFilter.transform.position + b), array[0], array[1], array[2], ref vector, ref vector2);
				}
			}
		}
		if (list != null)
		{
			foreach (UberText uberText in list)
			{
				if (uberText.gameObject.activeSelf)
				{
					Matrix4x4 localToWorldMatrix2 = uberText.transform.localToWorldMatrix;
					Matrix4x4 lhs2 = worldToLocalMatrix * localToWorldMatrix2;
					Vector3[] array2 = new Vector3[]
					{
						lhs2 * new Vector3(uberText.Width * 0.5f, 0f, 0f),
						lhs2 * new Vector3(0f, uberText.Height * 0.5f),
						lhs2 * new Vector3(0f, 0f, 0.01f)
					};
					TransformUtil.GetBoundsMinMax(worldToLocalMatrix * uberText.transform.position, array2[0], array2[1], array2[2], ref vector, ref vector2);
				}
			}
		}
		if (minLocalPadding.sqrMagnitude > 0f)
		{
			vector -= minLocalPadding;
		}
		if (maxLocalPadding.sqrMagnitude > 0f)
		{
			vector2 += maxLocalPadding;
		}
		Matrix4x4 localToWorldMatrix3 = go.transform.localToWorldMatrix;
		Matrix4x4 lhs3 = localToWorldMatrix3;
		lhs3.SetColumn(3, Vector4.zero);
		Vector3 vector3 = (localToWorldMatrix3 * vector2 + localToWorldMatrix3 * vector) * 0.5f;
		Vector3 vector4 = (vector2 - vector) * 0.5f;
		return new OrientedBounds
		{
			Extents = new Vector3[]
			{
				lhs3 * new Vector3(vector4.x, 0f, 0f),
				lhs3 * new Vector3(0f, vector4.y, 0f),
				lhs3 * new Vector3(0f, 0f, vector4.z)
			},
			Origin = vector3,
			CenterOffset = go.transform.position - vector3
		};
	}

	// Token: 0x06008A51 RID: 35409 RVA: 0x002C5770 File Offset: 0x002C3970
	public static bool CanComputeOrientedWorldBounds(GameObject go, bool includeAllChildren = true)
	{
		return TransformUtil.CanComputeOrientedWorldBounds(go, true, includeAllChildren);
	}

	// Token: 0x06008A52 RID: 35410 RVA: 0x002C577A File Offset: 0x002C397A
	public static bool CanComputeOrientedWorldBounds(GameObject go, List<GameObject> ignoreMeshes, bool includeAllChildren = true)
	{
		return TransformUtil.CanComputeOrientedWorldBounds(go, true, ignoreMeshes, includeAllChildren);
	}

	// Token: 0x06008A53 RID: 35411 RVA: 0x002C5785 File Offset: 0x002C3985
	public static bool CanComputeOrientedWorldBounds(GameObject go, bool includeUberText, bool includeAllChildren = true)
	{
		return TransformUtil.CanComputeOrientedWorldBounds(go, includeUberText, null, includeAllChildren);
	}

	// Token: 0x06008A54 RID: 35412 RVA: 0x002C5790 File Offset: 0x002C3990
	public static bool CanComputeOrientedWorldBounds(GameObject go, bool includeUberText, List<GameObject> ignoreMeshes, bool includeAllChildren = true)
	{
		if (go == null || !go.activeSelf)
		{
			return false;
		}
		List<MeshFilter> componentsWithIgnore = TransformUtil.GetComponentsWithIgnore<MeshFilter>(go, ignoreMeshes, includeAllChildren);
		if (componentsWithIgnore != null && componentsWithIgnore.Count > 0)
		{
			return true;
		}
		if (includeUberText)
		{
			List<UberText> componentsWithIgnore2 = TransformUtil.GetComponentsWithIgnore<UberText>(go, ignoreMeshes, includeAllChildren);
			return componentsWithIgnore2 != null && componentsWithIgnore2.Count > 0;
		}
		return false;
	}

	// Token: 0x06008A55 RID: 35413 RVA: 0x002C57E4 File Offset: 0x002C39E4
	public static List<T> GetComponentsWithIgnore<T>(GameObject obj, List<GameObject> ignoreObjects, bool includeAllChildren = true) where T : Component
	{
		List<T> list = new List<T>();
		if (includeAllChildren)
		{
			obj.GetComponentsInChildren<T>(list);
		}
		T component = obj.GetComponent<T>();
		if (component != null)
		{
			list.Add(component);
		}
		if (ignoreObjects != null && ignoreObjects.Count > 0)
		{
			T[] array = list.ToArray();
			list.Clear();
			foreach (T t in array)
			{
				bool flag = true;
				foreach (GameObject gameObject in ignoreObjects)
				{
					if (gameObject == null || t.transform == gameObject.transform || t.transform.IsChildOf(gameObject.transform))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					list.Add(t);
				}
			}
		}
		return list;
	}

	// Token: 0x06008A56 RID: 35414 RVA: 0x002C58E8 File Offset: 0x002C3AE8
	public static Vector3[] GetBoundCorners(Vector3 origin, Vector3 xExtent, Vector3 yExtent, Vector3 zExtent)
	{
		Vector3 a = origin + xExtent;
		Vector3 a2 = origin - xExtent;
		Vector3 b = yExtent + zExtent;
		Vector3 b2 = yExtent - zExtent;
		Vector3 b3 = -yExtent + zExtent;
		Vector3 b4 = -yExtent - zExtent;
		return new Vector3[]
		{
			a + b,
			a + b2,
			a + b3,
			a + b4,
			a2 - b,
			a2 - b2,
			a2 - b3,
			a2 - b4
		};
	}

	// Token: 0x06008A57 RID: 35415 RVA: 0x002C59AC File Offset: 0x002C3BAC
	public static void GetBoundsMinMax(Vector3 origin, Vector3 xExtent, Vector3 yExtent, Vector3 zExtent, ref Vector3 min, ref Vector3 max)
	{
		Vector3[] boundCorners = TransformUtil.GetBoundCorners(origin, xExtent, yExtent, zExtent);
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

	// Token: 0x06008A58 RID: 35416 RVA: 0x002C5A91 File Offset: 0x002C3C91
	public static void SetLocalScaleToWorldDimension(GameObject obj, params WorldDimensionIndex[] dimensions)
	{
		TransformUtil.SetLocalScaleToWorldDimension(obj, null, dimensions);
	}

	// Token: 0x06008A59 RID: 35417 RVA: 0x002C5A9C File Offset: 0x002C3C9C
	public static void SetLocalScaleToWorldDimension(GameObject obj, List<GameObject> ignoreMeshes, params WorldDimensionIndex[] dimensions)
	{
		Vector3 localScale = obj.transform.localScale;
		OrientedBounds orientedBounds = TransformUtil.ComputeOrientedWorldBounds(obj, ignoreMeshes, true);
		for (int i = 0; i < dimensions.Length; i++)
		{
			float num = orientedBounds.Extents[dimensions[i].Index].magnitude * 2f;
			ref Vector3 ptr = ref localScale;
			int index = dimensions[i].Index;
			ptr[index] *= ((num <= Mathf.Epsilon) ? 0.001f : (dimensions[i].Dimension / num));
			if (Mathf.Abs(localScale[dimensions[i].Index]) < 0.001f)
			{
				localScale[dimensions[i].Index] = 0.001f;
			}
		}
		obj.transform.localScale = localScale;
	}

	// Token: 0x06008A5A RID: 35418 RVA: 0x002C5B7A File Offset: 0x002C3D7A
	public static void SetPoint(Component src, Anchor srcAnchor, Component dst, Anchor dstAnchor)
	{
		TransformUtil.SetPoint(src.gameObject, TransformUtil.GetUnitAnchor(srcAnchor), dst.gameObject, TransformUtil.GetUnitAnchor(dstAnchor), Vector3.zero, false);
	}

	// Token: 0x06008A5B RID: 35419 RVA: 0x002C5B9F File Offset: 0x002C3D9F
	public static void SetPoint(Component src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor)
	{
		TransformUtil.SetPoint(src.gameObject, TransformUtil.GetUnitAnchor(srcAnchor), dst, TransformUtil.GetUnitAnchor(dstAnchor), Vector3.zero, false);
	}

	// Token: 0x06008A5C RID: 35420 RVA: 0x002C5BBF File Offset: 0x002C3DBF
	public static void SetPoint(GameObject src, Anchor srcAnchor, Component dst, Anchor dstAnchor)
	{
		TransformUtil.SetPoint(src, TransformUtil.GetUnitAnchor(srcAnchor), dst.gameObject, TransformUtil.GetUnitAnchor(dstAnchor), Vector3.zero, false);
	}

	// Token: 0x06008A5D RID: 35421 RVA: 0x002C5BDF File Offset: 0x002C3DDF
	public static void SetPoint(GameObject src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor)
	{
		TransformUtil.SetPoint(src, TransformUtil.GetUnitAnchor(srcAnchor), dst, TransformUtil.GetUnitAnchor(dstAnchor), Vector3.zero, false);
	}

	// Token: 0x06008A5E RID: 35422 RVA: 0x002C5BFA File Offset: 0x002C3DFA
	public static void SetPoint(Component src, Anchor srcAnchor, Component dst, Anchor dstAnchor, bool includeInactive)
	{
		TransformUtil.SetPoint(src.gameObject, TransformUtil.GetUnitAnchor(srcAnchor), dst.gameObject, TransformUtil.GetUnitAnchor(dstAnchor), Vector3.zero, includeInactive);
	}

	// Token: 0x06008A5F RID: 35423 RVA: 0x002C5C20 File Offset: 0x002C3E20
	public static void SetPoint(Component src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor, bool includeInactive)
	{
		TransformUtil.SetPoint(src.gameObject, TransformUtil.GetUnitAnchor(srcAnchor), dst, TransformUtil.GetUnitAnchor(dstAnchor), Vector3.zero, includeInactive);
	}

	// Token: 0x06008A60 RID: 35424 RVA: 0x002C5C41 File Offset: 0x002C3E41
	public static void SetPoint(GameObject src, Anchor srcAnchor, Component dst, Anchor dstAnchor, bool includeInactive)
	{
		TransformUtil.SetPoint(src, TransformUtil.GetUnitAnchor(srcAnchor), dst.gameObject, TransformUtil.GetUnitAnchor(dstAnchor), Vector3.zero, includeInactive);
	}

	// Token: 0x06008A61 RID: 35425 RVA: 0x002C5C62 File Offset: 0x002C3E62
	public static void SetPoint(GameObject src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor, bool includeInactive)
	{
		TransformUtil.SetPoint(src, TransformUtil.GetUnitAnchor(srcAnchor), dst, TransformUtil.GetUnitAnchor(dstAnchor), Vector3.zero, includeInactive);
	}

	// Token: 0x06008A62 RID: 35426 RVA: 0x002C5C7E File Offset: 0x002C3E7E
	public static void SetPoint(Component src, Anchor srcAnchor, Component dst, Anchor dstAnchor, Vector3 offset)
	{
		TransformUtil.SetPoint(src.gameObject, TransformUtil.GetUnitAnchor(srcAnchor), dst.gameObject, TransformUtil.GetUnitAnchor(dstAnchor), offset, false);
	}

	// Token: 0x06008A63 RID: 35427 RVA: 0x002C5CA0 File Offset: 0x002C3EA0
	public static void SetPoint(Component src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor, Vector3 offset)
	{
		TransformUtil.SetPoint(src.gameObject, TransformUtil.GetUnitAnchor(srcAnchor), dst, TransformUtil.GetUnitAnchor(dstAnchor), offset, false);
	}

	// Token: 0x06008A64 RID: 35428 RVA: 0x002C5CBD File Offset: 0x002C3EBD
	public static void SetPoint(GameObject src, Anchor srcAnchor, Component dst, Anchor dstAnchor, Vector3 offset)
	{
		TransformUtil.SetPoint(src, TransformUtil.GetUnitAnchor(srcAnchor), dst.gameObject, TransformUtil.GetUnitAnchor(dstAnchor), offset, false);
	}

	// Token: 0x06008A65 RID: 35429 RVA: 0x002C5CDA File Offset: 0x002C3EDA
	public static void SetPoint(GameObject src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor, Vector3 offset)
	{
		TransformUtil.SetPoint(src, TransformUtil.GetUnitAnchor(srcAnchor), dst, TransformUtil.GetUnitAnchor(dstAnchor), offset, false);
	}

	// Token: 0x06008A66 RID: 35430 RVA: 0x002C5CF2 File Offset: 0x002C3EF2
	public static void SetPoint(Component src, Anchor srcAnchor, Component dst, Anchor dstAnchor, Vector3 offset, bool includeInactive)
	{
		TransformUtil.SetPoint(src.gameObject, TransformUtil.GetUnitAnchor(srcAnchor), dst.gameObject, TransformUtil.GetUnitAnchor(dstAnchor), offset, includeInactive);
	}

	// Token: 0x06008A67 RID: 35431 RVA: 0x002C5D15 File Offset: 0x002C3F15
	public static void SetPoint(Component src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor, Vector3 offset, bool includeInactive)
	{
		TransformUtil.SetPoint(src.gameObject, TransformUtil.GetUnitAnchor(srcAnchor), dst, TransformUtil.GetUnitAnchor(dstAnchor), offset, includeInactive);
	}

	// Token: 0x06008A68 RID: 35432 RVA: 0x002C5D33 File Offset: 0x002C3F33
	public static void SetPoint(GameObject src, Anchor srcAnchor, Component dst, Anchor dstAnchor, Vector3 offset, bool includeInactive)
	{
		TransformUtil.SetPoint(src, TransformUtil.GetUnitAnchor(srcAnchor), dst.gameObject, TransformUtil.GetUnitAnchor(dstAnchor), offset, includeInactive);
	}

	// Token: 0x06008A69 RID: 35433 RVA: 0x002C5D51 File Offset: 0x002C3F51
	public static void SetPoint(GameObject src, Anchor srcAnchor, GameObject dst, Anchor dstAnchor, Vector3 offset, bool includeInactive)
	{
		TransformUtil.SetPoint(src, TransformUtil.GetUnitAnchor(srcAnchor), dst, TransformUtil.GetUnitAnchor(dstAnchor), offset, includeInactive);
	}

	// Token: 0x06008A6A RID: 35434 RVA: 0x002C5D6A File Offset: 0x002C3F6A
	public static void SetPoint(Component self, Vector3 selfUnitAnchor, Component relative, Vector3 relativeUnitAnchor)
	{
		TransformUtil.SetPoint(self.gameObject, selfUnitAnchor, relative.gameObject, relativeUnitAnchor, Vector3.zero, false);
	}

	// Token: 0x06008A6B RID: 35435 RVA: 0x002C5D85 File Offset: 0x002C3F85
	public static void SetPoint(Component self, Vector3 selfUnitAnchor, GameObject relative, Vector3 relativeUnitAnchor)
	{
		TransformUtil.SetPoint(self.gameObject, selfUnitAnchor, relative, relativeUnitAnchor, Vector3.zero, false);
	}

	// Token: 0x06008A6C RID: 35436 RVA: 0x002C5D9B File Offset: 0x002C3F9B
	public static void SetPoint(GameObject self, Vector3 selfUnitAnchor, Component relative, Vector3 relativeUnitAnchor)
	{
		TransformUtil.SetPoint(self, selfUnitAnchor, relative.gameObject, relativeUnitAnchor, Vector3.zero, false);
	}

	// Token: 0x06008A6D RID: 35437 RVA: 0x002C5DB1 File Offset: 0x002C3FB1
	public static void SetPoint(GameObject self, Vector3 selfUnitAnchor, GameObject relative, Vector3 relativeUnitAnchor)
	{
		TransformUtil.SetPoint(self, selfUnitAnchor, relative, relativeUnitAnchor, Vector3.zero, false);
	}

	// Token: 0x06008A6E RID: 35438 RVA: 0x002C5DC2 File Offset: 0x002C3FC2
	public static void SetPoint(Component self, Vector3 selfUnitAnchor, Component relative, Vector3 relativeUnitAnchor, Vector3 offset)
	{
		TransformUtil.SetPoint(self.gameObject, selfUnitAnchor, relative.gameObject, relativeUnitAnchor, offset, false);
	}

	// Token: 0x06008A6F RID: 35439 RVA: 0x002C5DDA File Offset: 0x002C3FDA
	public static void SetPoint(Component self, Vector3 selfUnitAnchor, GameObject relative, Vector3 relativeUnitAnchor, Vector3 offset)
	{
		TransformUtil.SetPoint(self.gameObject, selfUnitAnchor, relative, relativeUnitAnchor, offset, false);
	}

	// Token: 0x06008A70 RID: 35440 RVA: 0x002C5DED File Offset: 0x002C3FED
	public static void SetPoint(GameObject self, Vector3 selfUnitAnchor, Component relative, Vector3 relativeUnitAnchor, Vector3 offset)
	{
		TransformUtil.SetPoint(self, selfUnitAnchor, relative.gameObject, relativeUnitAnchor, offset, false);
	}

	// Token: 0x06008A71 RID: 35441 RVA: 0x002C5E00 File Offset: 0x002C4000
	public static void SetPoint(GameObject self, Vector3 selfUnitAnchor, GameObject relative, Vector3 relativeUnitAnchor, Vector3 offset)
	{
		TransformUtil.SetPoint(self, selfUnitAnchor, relative, relativeUnitAnchor, offset, false);
	}

	// Token: 0x06008A72 RID: 35442 RVA: 0x002C5E0E File Offset: 0x002C400E
	public static void SetPoint(Component self, Vector3 selfUnitAnchor, Component relative, Vector3 relativeUnitAnchor, Vector3 offset, bool includeInactive)
	{
		TransformUtil.SetPoint(self.gameObject, selfUnitAnchor, relative.gameObject, relativeUnitAnchor, offset, includeInactive);
	}

	// Token: 0x06008A73 RID: 35443 RVA: 0x002C5E27 File Offset: 0x002C4027
	public static void SetPoint(Component self, Vector3 selfUnitAnchor, GameObject relative, Vector3 relativeUnitAnchor, Vector3 offset, bool includeInactive)
	{
		TransformUtil.SetPoint(self.gameObject, selfUnitAnchor, relative, relativeUnitAnchor, offset, includeInactive);
	}

	// Token: 0x06008A74 RID: 35444 RVA: 0x002C5E3B File Offset: 0x002C403B
	public static void SetPoint(GameObject self, Vector3 selfUnitAnchor, Component relative, Vector3 relativeUnitAnchor, Vector3 offset, bool includeInactive)
	{
		TransformUtil.SetPoint(self, selfUnitAnchor, relative.gameObject, relativeUnitAnchor, offset, includeInactive);
	}

	// Token: 0x06008A75 RID: 35445 RVA: 0x002C5E50 File Offset: 0x002C4050
	public static void SetPoint(GameObject self, Vector3 selfUnitAnchor, GameObject relative, Vector3 relativeUnitAnchor, Vector3 offset, bool includeInactive)
	{
		if (!self || !relative)
		{
			return;
		}
		Bounds bounds = TransformUtil.ComputeSetPointBounds(self, includeInactive);
		Bounds bounds2 = TransformUtil.ComputeSetPointBounds(relative, includeInactive);
		Vector3 vector = TransformUtil.ComputeWorldPoint(bounds, selfUnitAnchor);
		Vector3 vector2 = TransformUtil.ComputeWorldPoint(bounds2, relativeUnitAnchor);
		Vector3 translation = new Vector3(vector2.x - vector.x + offset.x, vector2.y - vector.y + offset.y, vector2.z - vector.z + offset.z);
		self.transform.Translate(translation, Space.World);
	}

	// Token: 0x06008A76 RID: 35446 RVA: 0x002C5EE1 File Offset: 0x002C40E1
	public static Bounds GetBoundsOfChildren(Component c)
	{
		return TransformUtil.GetBoundsOfChildren(c.gameObject, false);
	}

	// Token: 0x06008A77 RID: 35447 RVA: 0x002C5EEF File Offset: 0x002C40EF
	public static Bounds GetBoundsOfChildren(GameObject go)
	{
		return TransformUtil.GetBoundsOfChildren(go, false);
	}

	// Token: 0x06008A78 RID: 35448 RVA: 0x002C5EF8 File Offset: 0x002C40F8
	public static Bounds GetBoundsOfChildren(Component c, bool includeInactive)
	{
		return TransformUtil.GetBoundsOfChildren(c.gameObject, includeInactive);
	}

	// Token: 0x06008A79 RID: 35449 RVA: 0x002C5F08 File Offset: 0x002C4108
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

	// Token: 0x06008A7A RID: 35450 RVA: 0x002C5F8D File Offset: 0x002C418D
	public static Vector3 Divide(Vector3 v1, Vector3 v2)
	{
		return new Vector3(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
	}

	// Token: 0x06008A7B RID: 35451 RVA: 0x002C5FBB File Offset: 0x002C41BB
	public static Vector3 Multiply(Vector3 v1, Vector3 v2)
	{
		return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
	}

	// Token: 0x06008A7C RID: 35452 RVA: 0x002C5FEC File Offset: 0x002C41EC
	public static void SetLocalPosX(GameObject go, float x)
	{
		Transform transform = go.transform;
		transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
	}

	// Token: 0x06008A7D RID: 35453 RVA: 0x002C6024 File Offset: 0x002C4224
	public static void SetLocalPosX(Component component, float x)
	{
		Transform transform = component.transform;
		transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
	}

	// Token: 0x06008A7E RID: 35454 RVA: 0x002C605C File Offset: 0x002C425C
	public static void SetLocalPosY(GameObject go, float y)
	{
		Transform transform = go.transform;
		transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
	}

	// Token: 0x06008A7F RID: 35455 RVA: 0x002C6094 File Offset: 0x002C4294
	public static void SetLocalPosY(Component component, float y)
	{
		Transform transform = component.transform;
		transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
	}

	// Token: 0x06008A80 RID: 35456 RVA: 0x002C60CC File Offset: 0x002C42CC
	public static void SetLocalPosZ(GameObject go, float z)
	{
		Transform transform = go.transform;
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
	}

	// Token: 0x06008A81 RID: 35457 RVA: 0x002C6104 File Offset: 0x002C4304
	public static void SetLocalPosZ(Component component, float z)
	{
		Transform transform = component.transform;
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
	}

	// Token: 0x06008A82 RID: 35458 RVA: 0x002C613C File Offset: 0x002C433C
	public static void SetPosX(GameObject go, float x)
	{
		Transform transform = go.transform;
		transform.position = new Vector3(x, transform.position.y, transform.position.z);
	}

	// Token: 0x06008A83 RID: 35459 RVA: 0x002C6174 File Offset: 0x002C4374
	public static void SetPosX(Component component, float x)
	{
		Transform transform = component.transform;
		transform.position = new Vector3(x, transform.position.y, transform.position.z);
	}

	// Token: 0x06008A84 RID: 35460 RVA: 0x002C61AC File Offset: 0x002C43AC
	public static void SetPosY(GameObject go, float y)
	{
		Transform transform = go.transform;
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}

	// Token: 0x06008A85 RID: 35461 RVA: 0x002C61E4 File Offset: 0x002C43E4
	public static void SetPosY(Component component, float y)
	{
		Transform transform = component.transform;
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}

	// Token: 0x06008A86 RID: 35462 RVA: 0x002C621C File Offset: 0x002C441C
	public static void SetPosZ(GameObject go, float z)
	{
		Transform transform = go.transform;
		transform.position = new Vector3(transform.position.x, transform.position.y, z);
	}

	// Token: 0x06008A87 RID: 35463 RVA: 0x002C6254 File Offset: 0x002C4454
	public static void SetPosZ(Component component, float z)
	{
		Transform transform = component.transform;
		transform.position = new Vector3(transform.position.x, transform.position.y, z);
	}

	// Token: 0x06008A88 RID: 35464 RVA: 0x002C628C File Offset: 0x002C448C
	public static void SetLocalEulerAngleX(GameObject go, float x)
	{
		Transform transform = go.transform;
		transform.localEulerAngles = new Vector3(x, transform.localEulerAngles.y, transform.localEulerAngles.z);
	}

	// Token: 0x06008A89 RID: 35465 RVA: 0x002C62C4 File Offset: 0x002C44C4
	public static void SetLocalEulerAngleX(Component c, float x)
	{
		Transform transform = c.transform;
		transform.localEulerAngles = new Vector3(x, transform.localEulerAngles.y, transform.localEulerAngles.z);
	}

	// Token: 0x06008A8A RID: 35466 RVA: 0x002C62FC File Offset: 0x002C44FC
	public static void SetLocalEulerAngleY(GameObject go, float y)
	{
		Transform transform = go.transform;
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, y, transform.localEulerAngles.z);
	}

	// Token: 0x06008A8B RID: 35467 RVA: 0x002C6334 File Offset: 0x002C4534
	public static void SetLocalEulerAngleY(Component c, float y)
	{
		Transform transform = c.transform;
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, y, transform.localEulerAngles.z);
	}

	// Token: 0x06008A8C RID: 35468 RVA: 0x002C636C File Offset: 0x002C456C
	public static void SetLocalEulerAngleZ(GameObject go, float z)
	{
		Transform transform = go.transform;
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, z);
	}

	// Token: 0x06008A8D RID: 35469 RVA: 0x002C63A4 File Offset: 0x002C45A4
	public static void SetLocalEulerAngleZ(Component c, float z)
	{
		Transform transform = c.transform;
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, z);
	}

	// Token: 0x06008A8E RID: 35470 RVA: 0x002C63DC File Offset: 0x002C45DC
	public static void SetEulerAngleX(GameObject go, float x)
	{
		Transform transform = go.transform;
		transform.eulerAngles = new Vector3(x, transform.eulerAngles.y, transform.eulerAngles.z);
	}

	// Token: 0x06008A8F RID: 35471 RVA: 0x002C6414 File Offset: 0x002C4614
	public static void SetEulerAngleX(Component c, float x)
	{
		Transform transform = c.transform;
		transform.eulerAngles = new Vector3(x, transform.eulerAngles.y, transform.eulerAngles.z);
	}

	// Token: 0x06008A90 RID: 35472 RVA: 0x002C644C File Offset: 0x002C464C
	public static void SetEulerAngleY(GameObject go, float y)
	{
		Transform transform = go.transform;
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, y, transform.eulerAngles.z);
	}

	// Token: 0x06008A91 RID: 35473 RVA: 0x002C6484 File Offset: 0x002C4684
	public static void SetEulerAngleY(Component c, float y)
	{
		Transform transform = c.transform;
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, y, transform.eulerAngles.z);
	}

	// Token: 0x06008A92 RID: 35474 RVA: 0x002C64BC File Offset: 0x002C46BC
	public static void SetEulerAngleZ(GameObject go, float z)
	{
		Transform transform = go.transform;
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, z);
	}

	// Token: 0x06008A93 RID: 35475 RVA: 0x002C64F4 File Offset: 0x002C46F4
	public static void SetEulerAngleZ(Component c, float z)
	{
		Transform transform = c.transform;
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, z);
	}

	// Token: 0x06008A94 RID: 35476 RVA: 0x002C652C File Offset: 0x002C472C
	public static void SetLocalScaleX(Component component, float x)
	{
		Transform transform = component.transform;
		transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
	}

	// Token: 0x06008A95 RID: 35477 RVA: 0x002C6564 File Offset: 0x002C4764
	public static void SetLocalScaleX(GameObject go, float x)
	{
		Transform transform = go.transform;
		transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
	}

	// Token: 0x06008A96 RID: 35478 RVA: 0x002C659C File Offset: 0x002C479C
	public static void SetLocalScaleY(Component component, float y)
	{
		Transform transform = component.transform;
		transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
	}

	// Token: 0x06008A97 RID: 35479 RVA: 0x002C65D4 File Offset: 0x002C47D4
	public static void SetLocalScaleY(GameObject go, float y)
	{
		Transform transform = go.transform;
		transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
	}

	// Token: 0x06008A98 RID: 35480 RVA: 0x002C660C File Offset: 0x002C480C
	public static void SetLocalScaleZ(Component component, float z)
	{
		Transform transform = component.transform;
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
	}

	// Token: 0x06008A99 RID: 35481 RVA: 0x002C6644 File Offset: 0x002C4844
	public static void SetLocalScaleZ(GameObject go, float z)
	{
		Transform transform = go.transform;
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
	}

	// Token: 0x06008A9A RID: 35482 RVA: 0x002C667C File Offset: 0x002C487C
	public static void SetLocalScaleXY(Component component, float x, float y)
	{
		Transform transform = component.transform;
		transform.localScale = new Vector3(x, y, transform.localScale.z);
	}

	// Token: 0x06008A9B RID: 35483 RVA: 0x002C66A8 File Offset: 0x002C48A8
	public static void SetLocalScaleXY(GameObject go, float x, float y)
	{
		Transform transform = go.transform;
		transform.localScale = new Vector3(x, y, transform.localScale.z);
	}

	// Token: 0x06008A9C RID: 35484 RVA: 0x002C66D4 File Offset: 0x002C48D4
	public static void SetLocalScaleXY(Component component, Vector2 v)
	{
		Transform transform = component.transform;
		transform.localScale = new Vector3(v.x, v.y, transform.localScale.z);
	}

	// Token: 0x06008A9D RID: 35485 RVA: 0x002C670C File Offset: 0x002C490C
	public static void SetLocalScaleXY(GameObject go, Vector2 v)
	{
		Transform transform = go.transform;
		transform.localScale = new Vector3(v.x, v.y, transform.localScale.z);
	}

	// Token: 0x06008A9E RID: 35486 RVA: 0x002C6744 File Offset: 0x002C4944
	public static void SetLocalScaleXZ(Component component, float x, float z)
	{
		Transform transform = component.transform;
		transform.localScale = new Vector3(x, transform.localScale.y, z);
	}

	// Token: 0x06008A9F RID: 35487 RVA: 0x002C6770 File Offset: 0x002C4970
	public static void SetLocalScaleXZ(GameObject go, float x, float z)
	{
		Transform transform = go.transform;
		transform.localScale = new Vector3(x, transform.localScale.y, z);
	}

	// Token: 0x06008AA0 RID: 35488 RVA: 0x002C679C File Offset: 0x002C499C
	public static void SetLocalScaleXZ(Component component, Vector2 v)
	{
		Transform transform = component.transform;
		transform.localScale = new Vector3(v.x, transform.localScale.y, v.y);
	}

	// Token: 0x06008AA1 RID: 35489 RVA: 0x002C67D4 File Offset: 0x002C49D4
	public static void SetLocalScaleXZ(GameObject go, Vector2 v)
	{
		Transform transform = go.transform;
		transform.localScale = new Vector3(v.x, transform.localScale.y, v.y);
	}

	// Token: 0x06008AA2 RID: 35490 RVA: 0x002C680A File Offset: 0x002C4A0A
	public static void SetLocalScaleYZ(Component component, float y, float z)
	{
		Transform transform = component.transform;
		transform.localScale = new Vector3(transform.localScale.x, y, z);
	}

	// Token: 0x06008AA3 RID: 35491 RVA: 0x002C6829 File Offset: 0x002C4A29
	public static void SetLocalScaleYZ(GameObject go, float y, float z)
	{
		Transform transform = go.transform;
		transform.localScale = new Vector3(transform.localScale.x, y, z);
	}

	// Token: 0x06008AA4 RID: 35492 RVA: 0x002C6848 File Offset: 0x002C4A48
	public static void SetLocalScaleYZ(Component component, Vector2 v)
	{
		Transform transform = component.transform;
		transform.localScale = new Vector3(transform.localScale.x, v.x, v.y);
	}

	// Token: 0x06008AA5 RID: 35493 RVA: 0x002C6871 File Offset: 0x002C4A71
	public static void SetLocalScaleYZ(GameObject go, Vector2 v)
	{
		Transform transform = go.transform;
		transform.localScale = new Vector3(transform.localScale.x, v.x, v.y);
	}

	// Token: 0x06008AA6 RID: 35494 RVA: 0x002C689A File Offset: 0x002C4A9A
	public static void Identity(Component c)
	{
		c.transform.localScale = Vector3.one;
		c.transform.localRotation = Quaternion.identity;
		c.transform.localPosition = Vector3.zero;
	}

	// Token: 0x06008AA7 RID: 35495 RVA: 0x002C68CC File Offset: 0x002C4ACC
	public static void Identity(GameObject go)
	{
		go.transform.localScale = Vector3.one;
		go.transform.localRotation = Quaternion.identity;
		go.transform.localPosition = Vector3.zero;
	}

	// Token: 0x06008AA8 RID: 35496 RVA: 0x002C68FE File Offset: 0x002C4AFE
	public static void CopyLocal(Component destination, Component source)
	{
		TransformUtil.CopyLocal(destination.gameObject, source.gameObject);
	}

	// Token: 0x06008AA9 RID: 35497 RVA: 0x002C6911 File Offset: 0x002C4B11
	public static void CopyLocal(Component destination, GameObject source)
	{
		TransformUtil.CopyLocal(destination.gameObject, source);
	}

	// Token: 0x06008AAA RID: 35498 RVA: 0x002C691F File Offset: 0x002C4B1F
	public static void CopyLocal(GameObject destination, Component source)
	{
		TransformUtil.CopyLocal(destination, source.gameObject);
	}

	// Token: 0x06008AAB RID: 35499 RVA: 0x002C6930 File Offset: 0x002C4B30
	public static void CopyLocal(GameObject destination, GameObject source)
	{
		destination.transform.localScale = source.transform.localScale;
		destination.transform.localRotation = source.transform.localRotation;
		destination.transform.localPosition = source.transform.localPosition;
	}

	// Token: 0x06008AAC RID: 35500 RVA: 0x002C697F File Offset: 0x002C4B7F
	public static void CopyLocal(Component destination, TransformProps source)
	{
		TransformUtil.CopyLocal(destination.gameObject, source);
	}

	// Token: 0x06008AAD RID: 35501 RVA: 0x002C698D File Offset: 0x002C4B8D
	public static void CopyLocal(GameObject destination, TransformProps source)
	{
		destination.transform.localScale = source.scale;
		destination.transform.localRotation = source.rotation;
		destination.transform.localPosition = source.position;
	}

	// Token: 0x06008AAE RID: 35502 RVA: 0x002C69C2 File Offset: 0x002C4BC2
	public static void CopyLocal(TransformProps destination, Component source)
	{
		TransformUtil.CopyLocal(destination, source.gameObject);
	}

	// Token: 0x06008AAF RID: 35503 RVA: 0x002C69D0 File Offset: 0x002C4BD0
	public static void CopyLocal(TransformProps destination, GameObject source)
	{
		destination.scale = source.transform.localScale;
		destination.rotation = source.transform.localRotation;
		destination.position = source.transform.localPosition;
	}

	// Token: 0x06008AB0 RID: 35504 RVA: 0x002C6A05 File Offset: 0x002C4C05
	public static void CopyWorld(Component destination, Component source)
	{
		TransformUtil.CopyWorld(destination.gameObject, source);
	}

	// Token: 0x06008AB1 RID: 35505 RVA: 0x002C6A13 File Offset: 0x002C4C13
	public static void CopyWorld(Component destination, GameObject source)
	{
		TransformUtil.CopyWorld(destination.gameObject, source);
	}

	// Token: 0x06008AB2 RID: 35506 RVA: 0x002C6A21 File Offset: 0x002C4C21
	public static void CopyWorld(GameObject destination, Component source)
	{
		TransformUtil.CopyWorld(destination, source.gameObject);
	}

	// Token: 0x06008AB3 RID: 35507 RVA: 0x002C6A2F File Offset: 0x002C4C2F
	public static void CopyWorld(GameObject destination, GameObject source)
	{
		TransformUtil.CopyWorldScale(destination, source);
		destination.transform.rotation = source.transform.rotation;
		destination.transform.position = source.transform.position;
	}

	// Token: 0x06008AB4 RID: 35508 RVA: 0x002C6A64 File Offset: 0x002C4C64
	public static void CopyWorld(Component destination, TransformProps source)
	{
		TransformUtil.CopyWorld(destination.gameObject, source);
	}

	// Token: 0x06008AB5 RID: 35509 RVA: 0x002C6A72 File Offset: 0x002C4C72
	public static void CopyWorld(GameObject destination, TransformProps source)
	{
		TransformUtil.SetWorldScale(destination, source.scale);
		destination.transform.rotation = source.rotation;
		destination.transform.position = source.position;
	}

	// Token: 0x06008AB6 RID: 35510 RVA: 0x002C6AA2 File Offset: 0x002C4CA2
	public static void CopyWorld(TransformProps destination, Component source)
	{
		TransformUtil.CopyWorld(destination, source.gameObject);
	}

	// Token: 0x06008AB7 RID: 35511 RVA: 0x002C6AB0 File Offset: 0x002C4CB0
	public static void CopyWorld(TransformProps destination, GameObject source)
	{
		destination.scale = TransformUtil.ComputeWorldScale(source);
		destination.rotation = source.transform.rotation;
		destination.position = source.transform.position;
	}

	// Token: 0x06008AB8 RID: 35512 RVA: 0x002C6AE0 File Offset: 0x002C4CE0
	public static void CopyWorldScale(Component destination, Component source)
	{
		TransformUtil.CopyWorldScale(destination.gameObject, source.gameObject);
	}

	// Token: 0x06008AB9 RID: 35513 RVA: 0x002C6AF3 File Offset: 0x002C4CF3
	public static void CopyWorldScale(Component destination, GameObject source)
	{
		TransformUtil.CopyWorldScale(destination.gameObject, source);
	}

	// Token: 0x06008ABA RID: 35514 RVA: 0x002C6B01 File Offset: 0x002C4D01
	public static void CopyWorldScale(GameObject destination, Component source)
	{
		TransformUtil.CopyWorldScale(destination, source.gameObject);
	}

	// Token: 0x06008ABB RID: 35515 RVA: 0x002C6B10 File Offset: 0x002C4D10
	public static void CopyWorldScale(GameObject destination, GameObject source)
	{
		Vector3 scale = TransformUtil.ComputeWorldScale(source);
		TransformUtil.SetWorldScale(destination, scale);
	}

	// Token: 0x06008ABC RID: 35516 RVA: 0x002C6B2B File Offset: 0x002C4D2B
	public static void SetWorldScale(Component destination, Vector3 scale)
	{
		TransformUtil.SetWorldScale(destination.gameObject, scale);
	}

	// Token: 0x06008ABD RID: 35517 RVA: 0x002C6B3C File Offset: 0x002C4D3C
	public static void SetWorldScale(GameObject destination, Vector3 scale)
	{
		if (destination.transform.parent != null)
		{
			Transform parent = destination.transform.parent;
			while (parent != null)
			{
				scale.Scale(TransformUtil.Vector3Reciprocal(parent.localScale));
				parent = parent.parent;
			}
		}
		destination.transform.localScale = scale;
	}

	// Token: 0x06008ABE RID: 35518 RVA: 0x002C6B98 File Offset: 0x002C4D98
	public static Vector3 ComputeWorldScale(Component c)
	{
		return TransformUtil.ComputeWorldScale(c.gameObject);
	}

	// Token: 0x06008ABF RID: 35519 RVA: 0x002C6BA8 File Offset: 0x002C4DA8
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

	// Token: 0x06008AC0 RID: 35520 RVA: 0x002C6C00 File Offset: 0x002C4E00
	public static Vector3 Vector3Reciprocal(Vector3 source)
	{
		Vector3 vector = source;
		if (vector.x != 0f)
		{
			vector.x = 1f / vector.x;
		}
		if (vector.y != 0f)
		{
			vector.y = 1f / vector.y;
		}
		if (vector.z != 0f)
		{
			vector.z = 1f / vector.z;
		}
		return vector;
	}

	// Token: 0x06008AC1 RID: 35521 RVA: 0x002C6C70 File Offset: 0x002C4E70
	public static void OrientTo(GameObject source, GameObject target)
	{
		TransformUtil.OrientTo(source.transform, target.transform);
	}

	// Token: 0x06008AC2 RID: 35522 RVA: 0x002C6C83 File Offset: 0x002C4E83
	public static void OrientTo(GameObject source, Component target)
	{
		TransformUtil.OrientTo(source.transform, target.transform);
	}

	// Token: 0x06008AC3 RID: 35523 RVA: 0x002C6C96 File Offset: 0x002C4E96
	public static void OrientTo(Component source, GameObject target)
	{
		TransformUtil.OrientTo(source.transform, target.transform);
	}

	// Token: 0x06008AC4 RID: 35524 RVA: 0x002C6CA9 File Offset: 0x002C4EA9
	public static void OrientTo(Component source, Component target)
	{
		TransformUtil.OrientTo(source.transform, target.transform);
	}

	// Token: 0x06008AC5 RID: 35525 RVA: 0x002C6CBC File Offset: 0x002C4EBC
	public static void OrientTo(Transform source, Transform target)
	{
		TransformUtil.OrientTo(source, source.transform.position, target.transform.position);
	}

	// Token: 0x06008AC6 RID: 35526 RVA: 0x002C6CDC File Offset: 0x002C4EDC
	public static void OrientTo(Transform source, Vector3 sourcePosition, Vector3 targetPosition)
	{
		Vector3 forward = targetPosition - sourcePosition;
		if (forward.sqrMagnitude > Mathf.Epsilon)
		{
			source.rotation = Quaternion.LookRotation(forward);
		}
	}

	// Token: 0x06008AC7 RID: 35527 RVA: 0x002C6D0C File Offset: 0x002C4F0C
	public static Vector3 RandomVector3(Vector3 min, Vector3 max)
	{
		return new Vector3
		{
			x = UnityEngine.Random.Range(min.x, max.x),
			y = UnityEngine.Random.Range(min.y, max.y),
			z = UnityEngine.Random.Range(min.z, max.z)
		};
	}

	// Token: 0x06008AC8 RID: 35528 RVA: 0x002C6D6C File Offset: 0x002C4F6C
	public static void AttachAndPreserveLocalTransform(Transform child, Transform parent)
	{
		TransformProps transformProps = new TransformProps();
		TransformUtil.CopyLocal(transformProps, child);
		child.parent = parent;
		TransformUtil.CopyLocal(child, transformProps);
	}

	// Token: 0x06008AC9 RID: 35529 RVA: 0x002C6D94 File Offset: 0x002C4F94
	public static float GetAspectRatioValue(TransformUtil.PhoneAspectRatio aspectRatio)
	{
		switch (aspectRatio)
		{
		case TransformUtil.PhoneAspectRatio.Minimum:
			return 1.5f;
		case TransformUtil.PhoneAspectRatio.Wide:
			return 1.7777778f;
		case TransformUtil.PhoneAspectRatio.ExtraWide:
			return 2.04f;
		default:
			return 0f;
		}
	}

	// Token: 0x06008ACA RID: 35530 RVA: 0x002C6DC1 File Offset: 0x002C4FC1
	public static Vector3 GetAspectRatioDependentPosition(Vector3 aspectSmall, Vector3 aspectWide, Vector3 aspectExtraWide)
	{
		return TransformUtil.GetAspectRatioDependentValue<Vector3>(new Func<Vector3, Vector3, float, Vector3>(Vector3.Lerp), aspectSmall, aspectWide, aspectExtraWide);
	}

	// Token: 0x06008ACB RID: 35531 RVA: 0x002C6DD7 File Offset: 0x002C4FD7
	public static float GetAspectRatioDependentValue(float aspectSmall, float aspectWide, float aspectExtraWide)
	{
		return TransformUtil.GetAspectRatioDependentValue<float>(new Func<float, float, float, float>(Mathf.Lerp), aspectSmall, aspectWide, aspectExtraWide);
	}

	// Token: 0x06008ACC RID: 35532 RVA: 0x002C6DF0 File Offset: 0x002C4FF0
	private static T GetAspectRatioDependentValue<T>(Func<T, T, float, T> interpolator, T small, T wide, T extraWide)
	{
		Dictionary<TransformUtil.PhoneAspectRatio, T> dictionary = new Dictionary<TransformUtil.PhoneAspectRatio, T>
		{
			{
				TransformUtil.PhoneAspectRatio.Minimum,
				small
			},
			{
				TransformUtil.PhoneAspectRatio.Wide,
				wide
			},
			{
				TransformUtil.PhoneAspectRatio.ExtraWide,
				extraWide
			}
		};
		TransformUtil.PhoneAspectRatio key;
		TransformUtil.PhoneAspectRatio key2;
		float arg = TransformUtil.PhoneAspectRatioScale(out key, out key2);
		return interpolator(dictionary[key], dictionary[key2], arg);
	}

	// Token: 0x06008ACD RID: 35533 RVA: 0x002C6E3A File Offset: 0x002C503A
	public static bool IsExtraWideAspectRatio()
	{
		return TransformUtil.GetAspectRatioDependentValue(0f, 1f, 2f) > 1.2f;
	}

	// Token: 0x06008ACE RID: 35534 RVA: 0x002C6E58 File Offset: 0x002C5058
	private static float PhoneAspectRatioScale(out TransformUtil.PhoneAspectRatio lowerRatio, out TransformUtil.PhoneAspectRatio upperRatio)
	{
		float num = (float)Screen.width / (float)Screen.height;
		lowerRatio = TransformUtil.PhoneAspectRatio.Minimum;
		upperRatio = TransformUtil.PhoneAspectRatio.ExtraWide;
		int num2 = EnumUtils.Length<TransformUtil.PhoneAspectRatio>();
		for (int i = 0; i < num2; i++)
		{
			TransformUtil.PhoneAspectRatio phoneAspectRatio = (TransformUtil.PhoneAspectRatio)i;
			if (TransformUtil.GetAspectRatioValue(phoneAspectRatio) > num)
			{
				lowerRatio = (TransformUtil.PhoneAspectRatio)((i > 0) ? (i - 1) : 0);
				upperRatio = ((i == 0) ? (i + TransformUtil.PhoneAspectRatio.Wide) : phoneAspectRatio);
				break;
			}
		}
		float aspectRatioValue = TransformUtil.GetAspectRatioValue(lowerRatio);
		float aspectRatioValue2 = TransformUtil.GetAspectRatioValue(upperRatio);
		float num3 = aspectRatioValue2 - aspectRatioValue;
		num = Mathf.Clamp(num, aspectRatioValue, aspectRatioValue2);
		return (num - aspectRatioValue) / num3;
	}

	// Token: 0x06008ACF RID: 35535 RVA: 0x002C6EE0 File Offset: 0x002C50E0
	public static void ConstrainToScreen(GameObject go, int layer)
	{
		Camera camera = CameraUtils.FindFirstByLayer(layer);
		if (camera == null)
		{
			Log.All.PrintError("TransformUtil.ConstrainToScreen - No camera found for indicated layer.", Array.Empty<object>());
			return;
		}
		Bounds bounds = TransformUtil.ComputeSetPointBounds(go);
		Vector3[] array = new Vector3[4];
		Vector3 vector = camera.transform.InverseTransformPoint(bounds.center);
		camera.CalculateFrustumCorners(new Rect(0f, 0f, 1f, 1f), vector.z, Camera.MonoOrStereoscopicEye.Mono, array);
		Bounds bounds2 = new Bounds(camera.transform.TransformPoint(array[0]), default(Vector3));
		for (int i = 1; i < 4; i++)
		{
			Vector3 point = camera.transform.TransformPoint(array[i]);
			bounds2.Encapsulate(point);
		}
		Vector3 position = go.transform.position;
		bounds2.SetMinMax(bounds2.min - (bounds.min - position), bounds2.max - (bounds.max - position));
		Vector3 vector2 = bounds2.ClosestPoint(position) - position;
		vector2 -= camera.transform.forward * Vector3.Dot(camera.transform.forward, vector2);
		go.transform.position += vector2;
	}

	// Token: 0x02002687 RID: 9863
	public enum PhoneAspectRatio
	{
		// Token: 0x0400F0E9 RID: 61673
		Minimum,
		// Token: 0x0400F0EA RID: 61674
		Wide,
		// Token: 0x0400F0EB RID: 61675
		ExtraWide,
		// Token: 0x0400F0EC RID: 61676
		Maximum = 2
	}
}
