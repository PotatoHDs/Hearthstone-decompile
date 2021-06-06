using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000AD2 RID: 2770
[ExecuteAlways]
[CustomEditClass]
public class MultiSliceElement : MonoBehaviour
{
	// Token: 0x060093A2 RID: 37794 RVA: 0x002FDE15 File Offset: 0x002FC015
	public void AddSlice(GameObject obj)
	{
		this.AddSlice(obj, Vector3.zero, Vector3.zero, false);
	}

	// Token: 0x060093A3 RID: 37795 RVA: 0x002FDE29 File Offset: 0x002FC029
	public void AddSlice(GameObject obj, Vector3 minLocalPadding, Vector3 maxLocalPadding, bool reverse = false)
	{
		this.m_slices.Add(new MultiSliceElement.Slice
		{
			m_slice = obj,
			m_minLocalPadding = minLocalPadding,
			m_maxLocalPadding = maxLocalPadding,
			m_reverse = reverse
		});
	}

	// Token: 0x060093A4 RID: 37796 RVA: 0x002FDE58 File Offset: 0x002FC058
	public void ClearSlices()
	{
		this.m_slices.Clear();
	}

	// Token: 0x060093A5 RID: 37797 RVA: 0x002FDE68 File Offset: 0x002FC068
	public void UpdateSlices()
	{
		if (this.m_sliceWeld != null)
		{
			this.m_sliceWeld.Unweld();
		}
		MultiSliceElement.PositionSlices(base.transform, this.m_slices, this.m_reverse, this.m_direction, this.m_useUberText, this.m_localSliceSpacing, this.m_localPinnedPointOffset, this.m_XAlign, this.m_YAlign, this.m_ZAlign, this.m_ignore);
		if (this.m_weldable)
		{
			this.m_sliceWeld = new GeometryWeld(base.gameObject, (from x in this.m_slices
			select x.m_slice).ToArray<GameObject>());
		}
	}

	// Token: 0x060093A6 RID: 37798 RVA: 0x002FDF18 File Offset: 0x002FC118
	public static void PositionSlices(Transform root, List<MultiSliceElement.Slice> slices, bool reverseDir, MultiSliceElement.Direction dir, bool useUberText, Vector3 localSliceSpacing, Vector3 localPinnedPointOffset, MultiSliceElement.XAxisAlign xAlign, MultiSliceElement.YAxisAlign yAlign, MultiSliceElement.ZAxisAlign zAlign, List<GameObject> ignoreObjects = null)
	{
		if (slices.Count == 0)
		{
			return;
		}
		float num = reverseDir ? -1f : 1f;
		MultiSliceElement.Slice[] array = slices.FindAll((MultiSliceElement.Slice s) => TransformUtil.CanComputeOrientedWorldBounds(s.m_slice, useUberText, ignoreObjects, true)).ToArray();
		if (array.Length == 0)
		{
			return;
		}
		Vector3 vector = Vector3.zero;
		Matrix4x4 worldToLocalMatrix = root.worldToLocalMatrix;
		Vector3 vector2 = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		Vector3 vector3 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
		MultiSliceElement.Slice slice = array[0];
		GameObject slice2 = slice.m_slice;
		slice2.transform.localPosition = Vector3.zero;
		OrientedBounds orientedBounds = TransformUtil.ComputeOrientedWorldBounds(slice2, useUberText, slice.m_minLocalPadding, slice.m_maxLocalPadding, ignoreObjects, true);
		float d = num * (slice.m_reverse ? -1f : 1f);
		Vector3 b = (orientedBounds.Extents[0] + orientedBounds.Extents[1] + orientedBounds.Extents[2]) * d;
		slice2.transform.position += orientedBounds.CenterOffset + b;
		vector = orientedBounds.Extents[(int)dir] * d + b;
		TransformUtil.GetBoundsMinMax(worldToLocalMatrix * (slice2.transform.position - orientedBounds.CenterOffset), worldToLocalMatrix * orientedBounds.Extents[0], worldToLocalMatrix * orientedBounds.Extents[1], worldToLocalMatrix * orientedBounds.Extents[2], ref vector2, ref vector3);
		Vector3 v = localSliceSpacing * num;
		for (int i = 1; i < array.Length; i++)
		{
			MultiSliceElement.Slice slice3 = array[i];
			GameObject slice4 = slice3.m_slice;
			float d2 = num * (slice3.m_reverse ? -1f : 1f);
			slice4.transform.localPosition = Vector3.zero;
			OrientedBounds orientedBounds2 = TransformUtil.ComputeOrientedWorldBounds(slice4, useUberText, slice3.m_minLocalPadding, slice3.m_maxLocalPadding, ignoreObjects, true);
			Vector3 b2 = slice4.transform.localToWorldMatrix * v;
			Vector3 vector4 = orientedBounds2.Extents[(int)dir] * d2;
			slice4.transform.position += orientedBounds2.CenterOffset + vector + vector4 + b2;
			vector += vector4 * 2f + b2;
			TransformUtil.GetBoundsMinMax(worldToLocalMatrix * (slice4.transform.position - orientedBounds2.CenterOffset), worldToLocalMatrix * orientedBounds2.Extents[0], worldToLocalMatrix * orientedBounds2.Extents[1], worldToLocalMatrix * orientedBounds2.Extents[2], ref vector2, ref vector3);
		}
		Vector3 vector5 = new Vector3(vector2.x, vector3.y, vector2.z);
		Vector3 a = new Vector3(vector3.x, vector2.y, vector3.z);
		Vector3 b3 = root.localToWorldMatrix * (vector5 + MultiSliceElement.GetAlignmentVector(a - vector5, xAlign, yAlign, zAlign));
		Vector3 a2 = root.localToWorldMatrix * localPinnedPointOffset * num;
		Vector3 b4 = root.position - b3;
		Vector3 b5 = a2 + b4;
		MultiSliceElement.Slice[] array2 = array;
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].m_slice.transform.position += b5;
		}
	}

	// Token: 0x060093A7 RID: 37799 RVA: 0x002FE377 File Offset: 0x002FC577
	private static Vector3 GetAlignmentVector(Vector3 interpolate, MultiSliceElement.XAxisAlign x, MultiSliceElement.YAxisAlign y, MultiSliceElement.ZAxisAlign z)
	{
		return new Vector3(interpolate.x * ((float)x * 0.5f), interpolate.y * ((float)y * 0.5f), interpolate.z * ((float)z * 0.5f));
	}

	// Token: 0x04007BA8 RID: 31656
	[CustomEditField(ListTable = true)]
	public List<MultiSliceElement.Slice> m_slices = new List<MultiSliceElement.Slice>();

	// Token: 0x04007BA9 RID: 31657
	public List<GameObject> m_ignore = new List<GameObject>();

	// Token: 0x04007BAA RID: 31658
	public Vector3 m_localPinnedPointOffset = Vector3.zero;

	// Token: 0x04007BAB RID: 31659
	public MultiSliceElement.XAxisAlign m_XAlign;

	// Token: 0x04007BAC RID: 31660
	public MultiSliceElement.YAxisAlign m_YAlign = MultiSliceElement.YAxisAlign.BOTTOM;

	// Token: 0x04007BAD RID: 31661
	public MultiSliceElement.ZAxisAlign m_ZAlign = MultiSliceElement.ZAxisAlign.BACK;

	// Token: 0x04007BAE RID: 31662
	public Vector3 m_localSliceSpacing = Vector3.zero;

	// Token: 0x04007BAF RID: 31663
	public MultiSliceElement.Direction m_direction;

	// Token: 0x04007BB0 RID: 31664
	public bool m_reverse;

	// Token: 0x04007BB1 RID: 31665
	public bool m_useUberText;

	// Token: 0x04007BB2 RID: 31666
	public bool m_weldable;

	// Token: 0x04007BB3 RID: 31667
	private GeometryWeld m_sliceWeld;

	// Token: 0x02002703 RID: 9987
	public enum Direction
	{
		// Token: 0x0400F30A RID: 62218
		X,
		// Token: 0x0400F30B RID: 62219
		Y,
		// Token: 0x0400F30C RID: 62220
		Z
	}

	// Token: 0x02002704 RID: 9988
	public enum XAxisAlign
	{
		// Token: 0x0400F30E RID: 62222
		LEFT,
		// Token: 0x0400F30F RID: 62223
		MIDDLE,
		// Token: 0x0400F310 RID: 62224
		RIGHT
	}

	// Token: 0x02002705 RID: 9989
	public enum YAxisAlign
	{
		// Token: 0x0400F312 RID: 62226
		TOP,
		// Token: 0x0400F313 RID: 62227
		MIDDLE,
		// Token: 0x0400F314 RID: 62228
		BOTTOM
	}

	// Token: 0x02002706 RID: 9990
	public enum ZAxisAlign
	{
		// Token: 0x0400F316 RID: 62230
		FRONT,
		// Token: 0x0400F317 RID: 62231
		MIDDLE,
		// Token: 0x0400F318 RID: 62232
		BACK
	}

	// Token: 0x02002707 RID: 9991
	[Serializable]
	public class Slice
	{
		// Token: 0x060138CC RID: 80076 RVA: 0x00537C90 File Offset: 0x00535E90
		public static implicit operator GameObject(MultiSliceElement.Slice slice)
		{
			if (slice == null)
			{
				return null;
			}
			return slice.m_slice;
		}

		// Token: 0x0400F319 RID: 62233
		public GameObject m_slice;

		// Token: 0x0400F31A RID: 62234
		public Vector3 m_minLocalPadding;

		// Token: 0x0400F31B RID: 62235
		public Vector3 m_maxLocalPadding;

		// Token: 0x0400F31C RID: 62236
		public bool m_reverse;
	}
}
