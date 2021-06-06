using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[CustomEditClass]
public class NineSliceElement : MonoBehaviour
{
	[CustomEditField(Sections = "Top Row")]
	public MultiSliceElement.Slice m_topRow;

	[CustomEditField(Sections = "Middle Row")]
	public MultiSliceElement.Slice m_midRow;

	[CustomEditField(Sections = "Bottom Row")]
	public MultiSliceElement.Slice m_btmRow;

	[CustomEditField(Sections = "Top Row")]
	public MultiSliceElement.Slice m_topLeft;

	[CustomEditField(Sections = "Top Row")]
	public MultiSliceElement.Slice m_top;

	[CustomEditField(Sections = "Top Row")]
	public MultiSliceElement.Slice m_topRight;

	[CustomEditField(Sections = "Middle Row")]
	public MultiSliceElement.Slice m_left;

	[CustomEditField(Sections = "Middle Row")]
	public MultiSliceElement.Slice m_middle;

	[CustomEditField(Sections = "Middle Row")]
	public MultiSliceElement.Slice m_right;

	[CustomEditField(Sections = "Bottom Row")]
	public MultiSliceElement.Slice m_bottomLeft;

	[CustomEditField(Sections = "Bottom Row")]
	public MultiSliceElement.Slice m_bottom;

	[CustomEditField(Sections = "Bottom Row")]
	public MultiSliceElement.Slice m_bottomRight;

	public List<GameObject> m_ignore = new List<GameObject>();

	public MultiSliceElement.Direction m_WidthDirection;

	public MultiSliceElement.Direction m_HeightDirection = MultiSliceElement.Direction.Z;

	public Vector3 m_localPinnedPointOffset = Vector3.zero;

	public MultiSliceElement.XAxisAlign m_XAlign;

	public MultiSliceElement.YAxisAlign m_YAlign = MultiSliceElement.YAxisAlign.BOTTOM;

	public MultiSliceElement.ZAxisAlign m_ZAlign = MultiSliceElement.ZAxisAlign.BACK;

	public Vector3 m_localSliceSpacing = Vector3.zero;

	public bool m_reverse;

	public bool m_useUberText;

	public void SetEntireWidth(float width)
	{
		int widthDirection = (int)m_WidthDirection;
		OrientedBounds sliceBounds = GetSliceBounds(m_topLeft);
		OrientedBounds sliceBounds2 = GetSliceBounds(m_left);
		OrientedBounds sliceBounds3 = GetSliceBounds(m_bottomLeft);
		OrientedBounds sliceBounds4 = GetSliceBounds(m_topRight);
		OrientedBounds sliceBounds5 = GetSliceBounds(m_right);
		OrientedBounds sliceBounds6 = GetSliceBounds(m_bottomRight);
		float num = Mathf.Max(sliceBounds.Extents[widthDirection].magnitude, sliceBounds2.Extents[widthDirection].magnitude, sliceBounds3.Extents[widthDirection].magnitude) * 2f;
		float num2 = Mathf.Max(sliceBounds4.Extents[widthDirection].magnitude, sliceBounds5.Extents[widthDirection].magnitude, sliceBounds6.Extents[widthDirection].magnitude) * 2f;
		SetWidth(width - num - num2);
	}

	public void SetEntireHeight(float height)
	{
		int heightDirection = (int)m_HeightDirection;
		OrientedBounds sliceBounds = GetSliceBounds(m_topLeft);
		OrientedBounds sliceBounds2 = GetSliceBounds(m_top);
		OrientedBounds sliceBounds3 = GetSliceBounds(m_topRight);
		OrientedBounds sliceBounds4 = GetSliceBounds(m_bottomLeft);
		OrientedBounds sliceBounds5 = GetSliceBounds(m_bottom);
		OrientedBounds sliceBounds6 = GetSliceBounds(m_bottomRight);
		float num = Mathf.Max(sliceBounds.Extents[heightDirection].magnitude, sliceBounds2.Extents[heightDirection].magnitude, sliceBounds3.Extents[heightDirection].magnitude) * 2f;
		float num2 = Mathf.Max(sliceBounds4.Extents[heightDirection].magnitude, sliceBounds5.Extents[heightDirection].magnitude, sliceBounds6.Extents[heightDirection].magnitude) * 2f;
		SetHeight(height - num - num2);
	}

	public void SetEntireSize(Vector2 size)
	{
		SetEntireSize(size.x, size.y);
	}

	public void SetEntireSize(float width, float height)
	{
		int widthDirection = (int)m_WidthDirection;
		int heightDirection = (int)m_HeightDirection;
		OrientedBounds sliceBounds = GetSliceBounds(m_topLeft);
		OrientedBounds sliceBounds2 = GetSliceBounds(m_top);
		OrientedBounds sliceBounds3 = GetSliceBounds(m_topRight);
		OrientedBounds sliceBounds4 = GetSliceBounds(m_left);
		OrientedBounds sliceBounds5 = GetSliceBounds(m_right);
		OrientedBounds sliceBounds6 = GetSliceBounds(m_bottomLeft);
		OrientedBounds sliceBounds7 = GetSliceBounds(m_bottom);
		OrientedBounds sliceBounds8 = GetSliceBounds(m_bottomRight);
		float num = Mathf.Max(sliceBounds.Extents[widthDirection].magnitude, sliceBounds4.Extents[widthDirection].magnitude, sliceBounds6.Extents[widthDirection].magnitude) * 2f;
		float num2 = Mathf.Max(sliceBounds3.Extents[widthDirection].magnitude, sliceBounds5.Extents[widthDirection].magnitude, sliceBounds8.Extents[widthDirection].magnitude) * 2f;
		float num3 = Mathf.Max(sliceBounds.Extents[heightDirection].magnitude, sliceBounds2.Extents[heightDirection].magnitude, sliceBounds3.Extents[heightDirection].magnitude) * 2f;
		float num4 = Mathf.Max(sliceBounds6.Extents[heightDirection].magnitude, sliceBounds7.Extents[heightDirection].magnitude, sliceBounds8.Extents[heightDirection].magnitude) * 2f;
		SetSize(width - num - num2, height - num3 - num4);
	}

	public void SetWidth(float width)
	{
		width = Mathf.Max(width, 0f);
		int widthDirection = (int)m_WidthDirection;
		SetSliceSize(m_top, new WorldDimensionIndex(width, widthDirection));
		SetSliceSize(m_bottom, new WorldDimensionIndex(width, widthDirection));
		SetSliceSize(m_middle, new WorldDimensionIndex(width, widthDirection));
		UpdateAllSlices();
	}

	public void SetHeight(float height)
	{
		height = Mathf.Max(height, 0f);
		int heightDirection = (int)m_HeightDirection;
		SetSliceSize(m_left, new WorldDimensionIndex(height, heightDirection));
		SetSliceSize(m_right, new WorldDimensionIndex(height, heightDirection));
		SetSliceSize(m_middle, new WorldDimensionIndex(height, heightDirection));
		UpdateAllSlices();
	}

	public void SetSize(Vector2 size)
	{
		SetSize(size.x, size.y);
	}

	public void SetSize(float width, float height)
	{
		width = Mathf.Max(width, 0f);
		height = Mathf.Max(height, 0f);
		int widthDirection = (int)m_WidthDirection;
		int heightDirection = (int)m_HeightDirection;
		SetSliceSize(m_top, new WorldDimensionIndex(width, widthDirection));
		SetSliceSize(m_bottom, new WorldDimensionIndex(width, widthDirection));
		SetSliceSize(m_left, new WorldDimensionIndex(height, heightDirection));
		SetSliceSize(m_right, new WorldDimensionIndex(height, heightDirection));
		SetSliceSize(m_middle, new WorldDimensionIndex(width, widthDirection), new WorldDimensionIndex(height, heightDirection));
		UpdateAllSlices();
	}

	public void SetMiddleScale(float scaleWidth, float scaleHeight)
	{
		Vector3 localScale = m_middle.m_slice.transform.localScale;
		localScale[(int)m_WidthDirection] = scaleWidth;
		localScale[(int)m_HeightDirection] = scaleHeight;
		m_middle.m_slice.transform.localScale = localScale;
		UpdateSegmentsToMatchMiddle();
		UpdateAllSlices();
	}

	public Vector2 GetWorldDimensions()
	{
		OrientedBounds orientedBounds = TransformUtil.ComputeOrientedWorldBounds(m_middle, m_ignore);
		return new Vector2(orientedBounds.Extents[(int)m_WidthDirection].magnitude * 2f, orientedBounds.Extents[(int)m_HeightDirection].magnitude * 2f);
	}

	private OrientedBounds GetSliceBounds(GameObject slice)
	{
		if (slice != null)
		{
			return TransformUtil.ComputeOrientedWorldBounds(slice, m_ignore);
		}
		OrientedBounds orientedBounds = new OrientedBounds();
		orientedBounds.Extents = new Vector3[3]
		{
			Vector3.zero,
			Vector3.zero,
			Vector3.zero
		};
		orientedBounds.Origin = Vector3.zero;
		orientedBounds.CenterOffset = Vector3.zero;
		return orientedBounds;
	}

	private void SetSliceSize(GameObject slice, params WorldDimensionIndex[] dimensions)
	{
		if (slice != null)
		{
			TransformUtil.SetLocalScaleToWorldDimension(slice, m_ignore, dimensions);
		}
	}

	private void UpdateAllSlices()
	{
		UpdateRowSlices(new List<MultiSliceElement.Slice> { m_topLeft, m_top, m_topRight }, m_WidthDirection);
		UpdateRowSlices(new List<MultiSliceElement.Slice> { m_left, m_middle, m_right }, m_WidthDirection);
		UpdateRowSlices(new List<MultiSliceElement.Slice> { m_bottomLeft, m_bottom, m_bottomRight }, m_WidthDirection);
		UpdateRowSlices(new List<MultiSliceElement.Slice> { m_topRow, m_midRow, m_btmRow }, m_HeightDirection);
	}

	private void UpdateRowSlices(List<MultiSliceElement.Slice> slices, MultiSliceElement.Direction direction)
	{
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		zero[(int)direction] = m_localSliceSpacing[(int)direction];
		zero2[(int)direction] = m_localPinnedPointOffset[(int)direction];
		MultiSliceElement.PositionSlices(base.transform, slices, m_reverse, direction, m_useUberText, zero, zero2, m_XAlign, m_YAlign, m_ZAlign, m_ignore);
	}

	private void UpdateSegmentsToMatchMiddle()
	{
		OrientedBounds orientedBounds = TransformUtil.ComputeOrientedWorldBounds(m_middle, m_ignore);
		if (orientedBounds != null)
		{
			float dimension = orientedBounds.Extents[(int)m_WidthDirection].magnitude * 2f;
			float dimension2 = orientedBounds.Extents[(int)m_HeightDirection].magnitude * 2f;
			int widthDirection = (int)m_WidthDirection;
			int heightDirection = (int)m_HeightDirection;
			SetSliceSize(m_top, new WorldDimensionIndex(dimension, widthDirection));
			SetSliceSize(m_bottom, new WorldDimensionIndex(dimension, widthDirection));
			SetSliceSize(m_left, new WorldDimensionIndex(dimension2, heightDirection));
			SetSliceSize(m_right, new WorldDimensionIndex(dimension2, heightDirection));
		}
	}
}
