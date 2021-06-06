using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteAlways]
[CustomEditClass]
public class MultiSliceElement : MonoBehaviour
{
	public enum Direction
	{
		X,
		Y,
		Z
	}

	public enum XAxisAlign
	{
		LEFT,
		MIDDLE,
		RIGHT
	}

	public enum YAxisAlign
	{
		TOP,
		MIDDLE,
		BOTTOM
	}

	public enum ZAxisAlign
	{
		FRONT,
		MIDDLE,
		BACK
	}

	[Serializable]
	public class Slice
	{
		public GameObject m_slice;

		public Vector3 m_minLocalPadding;

		public Vector3 m_maxLocalPadding;

		public bool m_reverse;

		public static implicit operator GameObject(Slice slice)
		{
			return slice?.m_slice;
		}
	}

	[CustomEditField(ListTable = true)]
	public List<Slice> m_slices = new List<Slice>();

	public List<GameObject> m_ignore = new List<GameObject>();

	public Vector3 m_localPinnedPointOffset = Vector3.zero;

	public XAxisAlign m_XAlign;

	public YAxisAlign m_YAlign = YAxisAlign.BOTTOM;

	public ZAxisAlign m_ZAlign = ZAxisAlign.BACK;

	public Vector3 m_localSliceSpacing = Vector3.zero;

	public Direction m_direction;

	public bool m_reverse;

	public bool m_useUberText;

	public bool m_weldable;

	private GeometryWeld m_sliceWeld;

	public void AddSlice(GameObject obj)
	{
		AddSlice(obj, Vector3.zero, Vector3.zero);
	}

	public void AddSlice(GameObject obj, Vector3 minLocalPadding, Vector3 maxLocalPadding, bool reverse = false)
	{
		m_slices.Add(new Slice
		{
			m_slice = obj,
			m_minLocalPadding = minLocalPadding,
			m_maxLocalPadding = maxLocalPadding,
			m_reverse = reverse
		});
	}

	public void ClearSlices()
	{
		m_slices.Clear();
	}

	public void UpdateSlices()
	{
		if (m_sliceWeld != null)
		{
			m_sliceWeld.Unweld();
		}
		PositionSlices(base.transform, m_slices, m_reverse, m_direction, m_useUberText, m_localSliceSpacing, m_localPinnedPointOffset, m_XAlign, m_YAlign, m_ZAlign, m_ignore);
		if (m_weldable)
		{
			m_sliceWeld = new GeometryWeld(base.gameObject, m_slices.Select((Slice x) => x.m_slice).ToArray());
		}
	}

	public static void PositionSlices(Transform root, List<Slice> slices, bool reverseDir, Direction dir, bool useUberText, Vector3 localSliceSpacing, Vector3 localPinnedPointOffset, XAxisAlign xAlign, YAxisAlign yAlign, ZAxisAlign zAlign, List<GameObject> ignoreObjects = null)
	{
		if (slices.Count == 0)
		{
			return;
		}
		float num = (reverseDir ? (-1f) : 1f);
		Slice[] array = slices.FindAll((Slice s) => TransformUtil.CanComputeOrientedWorldBounds(s.m_slice, useUberText, ignoreObjects)).ToArray();
		if (array.Length != 0)
		{
			Vector3 zero = Vector3.zero;
			Matrix4x4 worldToLocalMatrix = root.worldToLocalMatrix;
			Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			Slice slice = array[0];
			GameObject slice2 = slice.m_slice;
			slice2.transform.localPosition = Vector3.zero;
			OrientedBounds orientedBounds = TransformUtil.ComputeOrientedWorldBounds(slice2, useUberText, slice.m_minLocalPadding, slice.m_maxLocalPadding, ignoreObjects);
			float num2 = num * (slice.m_reverse ? (-1f) : 1f);
			Vector3 vector = (orientedBounds.Extents[0] + orientedBounds.Extents[1] + orientedBounds.Extents[2]) * num2;
			slice2.transform.position += orientedBounds.CenterOffset + vector;
			zero = orientedBounds.Extents[(int)dir] * num2 + vector;
			TransformUtil.GetBoundsMinMax(worldToLocalMatrix * (slice2.transform.position - orientedBounds.CenterOffset), worldToLocalMatrix * orientedBounds.Extents[0], worldToLocalMatrix * orientedBounds.Extents[1], worldToLocalMatrix * orientedBounds.Extents[2], ref min, ref max);
			Vector3 vector2 = localSliceSpacing * num;
			for (int i = 1; i < array.Length; i++)
			{
				Slice slice3 = array[i];
				GameObject slice4 = slice3.m_slice;
				float num3 = num * (slice3.m_reverse ? (-1f) : 1f);
				slice4.transform.localPosition = Vector3.zero;
				OrientedBounds orientedBounds2 = TransformUtil.ComputeOrientedWorldBounds(slice4, useUberText, slice3.m_minLocalPadding, slice3.m_maxLocalPadding, ignoreObjects);
				Vector3 vector3 = slice4.transform.localToWorldMatrix * vector2;
				Vector3 vector4 = orientedBounds2.Extents[(int)dir] * num3;
				slice4.transform.position += orientedBounds2.CenterOffset + zero + vector4 + vector3;
				zero += vector4 * 2f + vector3;
				TransformUtil.GetBoundsMinMax(worldToLocalMatrix * (slice4.transform.position - orientedBounds2.CenterOffset), worldToLocalMatrix * orientedBounds2.Extents[0], worldToLocalMatrix * orientedBounds2.Extents[1], worldToLocalMatrix * orientedBounds2.Extents[2], ref min, ref max);
			}
			Vector3 vector5 = new Vector3(min.x, max.y, min.z);
			Vector3 vector6 = new Vector3(max.x, min.y, max.z);
			Vector3 vector7 = root.localToWorldMatrix * (vector5 + GetAlignmentVector(vector6 - vector5, xAlign, yAlign, zAlign));
			Vector3 vector8 = root.localToWorldMatrix * localPinnedPointOffset * num;
			Vector3 vector9 = root.position - vector7;
			Vector3 vector10 = vector8 + vector9;
			Slice[] array2 = array;
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j].m_slice.transform.position += vector10;
			}
		}
	}

	private static Vector3 GetAlignmentVector(Vector3 interpolate, XAxisAlign x, YAxisAlign y, ZAxisAlign z)
	{
		return new Vector3(interpolate.x * ((float)x * 0.5f), interpolate.y * ((float)y * 0.5f), interpolate.z * ((float)z * 0.5f));
	}
}
