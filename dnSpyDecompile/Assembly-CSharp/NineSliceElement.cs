using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AD5 RID: 2773
[ExecuteAlways]
[CustomEditClass]
public class NineSliceElement : MonoBehaviour
{
	// Token: 0x060093B4 RID: 37812 RVA: 0x002FF204 File Offset: 0x002FD404
	public void SetEntireWidth(float width)
	{
		int widthDirection = (int)this.m_WidthDirection;
		OrientedBounds sliceBounds = this.GetSliceBounds(this.m_topLeft);
		OrientedBounds sliceBounds2 = this.GetSliceBounds(this.m_left);
		OrientedBounds sliceBounds3 = this.GetSliceBounds(this.m_bottomLeft);
		OrientedBounds sliceBounds4 = this.GetSliceBounds(this.m_topRight);
		OrientedBounds sliceBounds5 = this.GetSliceBounds(this.m_right);
		OrientedBounds sliceBounds6 = this.GetSliceBounds(this.m_bottomRight);
		float num = Mathf.Max(new float[]
		{
			sliceBounds.Extents[widthDirection].magnitude,
			sliceBounds2.Extents[widthDirection].magnitude,
			sliceBounds3.Extents[widthDirection].magnitude
		}) * 2f;
		float num2 = Mathf.Max(new float[]
		{
			sliceBounds4.Extents[widthDirection].magnitude,
			sliceBounds5.Extents[widthDirection].magnitude,
			sliceBounds6.Extents[widthDirection].magnitude
		}) * 2f;
		this.SetWidth(width - num - num2);
	}

	// Token: 0x060093B5 RID: 37813 RVA: 0x002FF338 File Offset: 0x002FD538
	public void SetEntireHeight(float height)
	{
		int heightDirection = (int)this.m_HeightDirection;
		OrientedBounds sliceBounds = this.GetSliceBounds(this.m_topLeft);
		OrientedBounds sliceBounds2 = this.GetSliceBounds(this.m_top);
		OrientedBounds sliceBounds3 = this.GetSliceBounds(this.m_topRight);
		OrientedBounds sliceBounds4 = this.GetSliceBounds(this.m_bottomLeft);
		OrientedBounds sliceBounds5 = this.GetSliceBounds(this.m_bottom);
		OrientedBounds sliceBounds6 = this.GetSliceBounds(this.m_bottomRight);
		float num = Mathf.Max(new float[]
		{
			sliceBounds.Extents[heightDirection].magnitude,
			sliceBounds2.Extents[heightDirection].magnitude,
			sliceBounds3.Extents[heightDirection].magnitude
		}) * 2f;
		float num2 = Mathf.Max(new float[]
		{
			sliceBounds4.Extents[heightDirection].magnitude,
			sliceBounds5.Extents[heightDirection].magnitude,
			sliceBounds6.Extents[heightDirection].magnitude
		}) * 2f;
		this.SetHeight(height - num - num2);
	}

	// Token: 0x060093B6 RID: 37814 RVA: 0x002FF469 File Offset: 0x002FD669
	public void SetEntireSize(Vector2 size)
	{
		this.SetEntireSize(size.x, size.y);
	}

	// Token: 0x060093B7 RID: 37815 RVA: 0x002FF480 File Offset: 0x002FD680
	public void SetEntireSize(float width, float height)
	{
		int widthDirection = (int)this.m_WidthDirection;
		int heightDirection = (int)this.m_HeightDirection;
		OrientedBounds sliceBounds = this.GetSliceBounds(this.m_topLeft);
		OrientedBounds sliceBounds2 = this.GetSliceBounds(this.m_top);
		OrientedBounds sliceBounds3 = this.GetSliceBounds(this.m_topRight);
		OrientedBounds sliceBounds4 = this.GetSliceBounds(this.m_left);
		OrientedBounds sliceBounds5 = this.GetSliceBounds(this.m_right);
		OrientedBounds sliceBounds6 = this.GetSliceBounds(this.m_bottomLeft);
		OrientedBounds sliceBounds7 = this.GetSliceBounds(this.m_bottom);
		OrientedBounds sliceBounds8 = this.GetSliceBounds(this.m_bottomRight);
		float num = Mathf.Max(new float[]
		{
			sliceBounds.Extents[widthDirection].magnitude,
			sliceBounds4.Extents[widthDirection].magnitude,
			sliceBounds6.Extents[widthDirection].magnitude
		}) * 2f;
		float num2 = Mathf.Max(new float[]
		{
			sliceBounds3.Extents[widthDirection].magnitude,
			sliceBounds5.Extents[widthDirection].magnitude,
			sliceBounds8.Extents[widthDirection].magnitude
		}) * 2f;
		float num3 = Mathf.Max(new float[]
		{
			sliceBounds.Extents[heightDirection].magnitude,
			sliceBounds2.Extents[heightDirection].magnitude,
			sliceBounds3.Extents[heightDirection].magnitude
		}) * 2f;
		float num4 = Mathf.Max(new float[]
		{
			sliceBounds6.Extents[heightDirection].magnitude,
			sliceBounds7.Extents[heightDirection].magnitude,
			sliceBounds8.Extents[heightDirection].magnitude
		}) * 2f;
		this.SetSize(width - num - num2, height - num3 - num4);
	}

	// Token: 0x060093B8 RID: 37816 RVA: 0x002FF68C File Offset: 0x002FD88C
	public void SetWidth(float width)
	{
		width = Mathf.Max(width, 0f);
		int widthDirection = (int)this.m_WidthDirection;
		this.SetSliceSize(this.m_top, new WorldDimensionIndex[]
		{
			new WorldDimensionIndex(width, widthDirection)
		});
		this.SetSliceSize(this.m_bottom, new WorldDimensionIndex[]
		{
			new WorldDimensionIndex(width, widthDirection)
		});
		this.SetSliceSize(this.m_middle, new WorldDimensionIndex[]
		{
			new WorldDimensionIndex(width, widthDirection)
		});
		this.UpdateAllSlices();
	}

	// Token: 0x060093B9 RID: 37817 RVA: 0x002FF724 File Offset: 0x002FD924
	public void SetHeight(float height)
	{
		height = Mathf.Max(height, 0f);
		int heightDirection = (int)this.m_HeightDirection;
		this.SetSliceSize(this.m_left, new WorldDimensionIndex[]
		{
			new WorldDimensionIndex(height, heightDirection)
		});
		this.SetSliceSize(this.m_right, new WorldDimensionIndex[]
		{
			new WorldDimensionIndex(height, heightDirection)
		});
		this.SetSliceSize(this.m_middle, new WorldDimensionIndex[]
		{
			new WorldDimensionIndex(height, heightDirection)
		});
		this.UpdateAllSlices();
	}

	// Token: 0x060093BA RID: 37818 RVA: 0x002FF7BA File Offset: 0x002FD9BA
	public void SetSize(Vector2 size)
	{
		this.SetSize(size.x, size.y);
	}

	// Token: 0x060093BB RID: 37819 RVA: 0x002FF7D0 File Offset: 0x002FD9D0
	public void SetSize(float width, float height)
	{
		width = Mathf.Max(width, 0f);
		height = Mathf.Max(height, 0f);
		int widthDirection = (int)this.m_WidthDirection;
		int heightDirection = (int)this.m_HeightDirection;
		this.SetSliceSize(this.m_top, new WorldDimensionIndex[]
		{
			new WorldDimensionIndex(width, widthDirection)
		});
		this.SetSliceSize(this.m_bottom, new WorldDimensionIndex[]
		{
			new WorldDimensionIndex(width, widthDirection)
		});
		this.SetSliceSize(this.m_left, new WorldDimensionIndex[]
		{
			new WorldDimensionIndex(height, heightDirection)
		});
		this.SetSliceSize(this.m_right, new WorldDimensionIndex[]
		{
			new WorldDimensionIndex(height, heightDirection)
		});
		this.SetSliceSize(this.m_middle, new WorldDimensionIndex[]
		{
			new WorldDimensionIndex(width, widthDirection),
			new WorldDimensionIndex(height, heightDirection)
		});
		this.UpdateAllSlices();
	}

	// Token: 0x060093BC RID: 37820 RVA: 0x002FF8D4 File Offset: 0x002FDAD4
	public void SetMiddleScale(float scaleWidth, float scaleHeight)
	{
		Vector3 localScale = this.m_middle.m_slice.transform.localScale;
		localScale[(int)this.m_WidthDirection] = scaleWidth;
		localScale[(int)this.m_HeightDirection] = scaleHeight;
		this.m_middle.m_slice.transform.localScale = localScale;
		this.UpdateSegmentsToMatchMiddle();
		this.UpdateAllSlices();
	}

	// Token: 0x060093BD RID: 37821 RVA: 0x002FF938 File Offset: 0x002FDB38
	public Vector2 GetWorldDimensions()
	{
		OrientedBounds orientedBounds = TransformUtil.ComputeOrientedWorldBounds(this.m_middle, this.m_ignore, true);
		return new Vector2(orientedBounds.Extents[(int)this.m_WidthDirection].magnitude * 2f, orientedBounds.Extents[(int)this.m_HeightDirection].magnitude * 2f);
	}

	// Token: 0x060093BE RID: 37822 RVA: 0x002FF99C File Offset: 0x002FDB9C
	private OrientedBounds GetSliceBounds(GameObject slice)
	{
		if (slice != null)
		{
			return TransformUtil.ComputeOrientedWorldBounds(slice, this.m_ignore, true);
		}
		return new OrientedBounds
		{
			Extents = new Vector3[]
			{
				Vector3.zero,
				Vector3.zero,
				Vector3.zero
			},
			Origin = Vector3.zero,
			CenterOffset = Vector3.zero
		};
	}

	// Token: 0x060093BF RID: 37823 RVA: 0x002FFA0D File Offset: 0x002FDC0D
	private void SetSliceSize(GameObject slice, params WorldDimensionIndex[] dimensions)
	{
		if (slice != null)
		{
			TransformUtil.SetLocalScaleToWorldDimension(slice, this.m_ignore, dimensions);
		}
	}

	// Token: 0x060093C0 RID: 37824 RVA: 0x002FFA28 File Offset: 0x002FDC28
	private void UpdateAllSlices()
	{
		this.UpdateRowSlices(new List<MultiSliceElement.Slice>
		{
			this.m_topLeft,
			this.m_top,
			this.m_topRight
		}, this.m_WidthDirection);
		this.UpdateRowSlices(new List<MultiSliceElement.Slice>
		{
			this.m_left,
			this.m_middle,
			this.m_right
		}, this.m_WidthDirection);
		this.UpdateRowSlices(new List<MultiSliceElement.Slice>
		{
			this.m_bottomLeft,
			this.m_bottom,
			this.m_bottomRight
		}, this.m_WidthDirection);
		this.UpdateRowSlices(new List<MultiSliceElement.Slice>
		{
			this.m_topRow,
			this.m_midRow,
			this.m_btmRow
		}, this.m_HeightDirection);
	}

	// Token: 0x060093C1 RID: 37825 RVA: 0x002FFB0C File Offset: 0x002FDD0C
	private void UpdateRowSlices(List<MultiSliceElement.Slice> slices, MultiSliceElement.Direction direction)
	{
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		zero[(int)direction] = this.m_localSliceSpacing[(int)direction];
		zero2[(int)direction] = this.m_localPinnedPointOffset[(int)direction];
		MultiSliceElement.PositionSlices(base.transform, slices, this.m_reverse, direction, this.m_useUberText, zero, zero2, this.m_XAlign, this.m_YAlign, this.m_ZAlign, this.m_ignore);
	}

	// Token: 0x060093C2 RID: 37826 RVA: 0x002FFB80 File Offset: 0x002FDD80
	private void UpdateSegmentsToMatchMiddle()
	{
		OrientedBounds orientedBounds = TransformUtil.ComputeOrientedWorldBounds(this.m_middle, this.m_ignore, true);
		if (orientedBounds == null)
		{
			return;
		}
		float dimension = orientedBounds.Extents[(int)this.m_WidthDirection].magnitude * 2f;
		float dimension2 = orientedBounds.Extents[(int)this.m_HeightDirection].magnitude * 2f;
		int widthDirection = (int)this.m_WidthDirection;
		int heightDirection = (int)this.m_HeightDirection;
		this.SetSliceSize(this.m_top, new WorldDimensionIndex[]
		{
			new WorldDimensionIndex(dimension, widthDirection)
		});
		this.SetSliceSize(this.m_bottom, new WorldDimensionIndex[]
		{
			new WorldDimensionIndex(dimension, widthDirection)
		});
		this.SetSliceSize(this.m_left, new WorldDimensionIndex[]
		{
			new WorldDimensionIndex(dimension2, heightDirection)
		});
		this.SetSliceSize(this.m_right, new WorldDimensionIndex[]
		{
			new WorldDimensionIndex(dimension2, heightDirection)
		});
	}

	// Token: 0x04007BD4 RID: 31700
	[CustomEditField(Sections = "Top Row")]
	public MultiSliceElement.Slice m_topRow;

	// Token: 0x04007BD5 RID: 31701
	[CustomEditField(Sections = "Middle Row")]
	public MultiSliceElement.Slice m_midRow;

	// Token: 0x04007BD6 RID: 31702
	[CustomEditField(Sections = "Bottom Row")]
	public MultiSliceElement.Slice m_btmRow;

	// Token: 0x04007BD7 RID: 31703
	[CustomEditField(Sections = "Top Row")]
	public MultiSliceElement.Slice m_topLeft;

	// Token: 0x04007BD8 RID: 31704
	[CustomEditField(Sections = "Top Row")]
	public MultiSliceElement.Slice m_top;

	// Token: 0x04007BD9 RID: 31705
	[CustomEditField(Sections = "Top Row")]
	public MultiSliceElement.Slice m_topRight;

	// Token: 0x04007BDA RID: 31706
	[CustomEditField(Sections = "Middle Row")]
	public MultiSliceElement.Slice m_left;

	// Token: 0x04007BDB RID: 31707
	[CustomEditField(Sections = "Middle Row")]
	public MultiSliceElement.Slice m_middle;

	// Token: 0x04007BDC RID: 31708
	[CustomEditField(Sections = "Middle Row")]
	public MultiSliceElement.Slice m_right;

	// Token: 0x04007BDD RID: 31709
	[CustomEditField(Sections = "Bottom Row")]
	public MultiSliceElement.Slice m_bottomLeft;

	// Token: 0x04007BDE RID: 31710
	[CustomEditField(Sections = "Bottom Row")]
	public MultiSliceElement.Slice m_bottom;

	// Token: 0x04007BDF RID: 31711
	[CustomEditField(Sections = "Bottom Row")]
	public MultiSliceElement.Slice m_bottomRight;

	// Token: 0x04007BE0 RID: 31712
	public List<GameObject> m_ignore = new List<GameObject>();

	// Token: 0x04007BE1 RID: 31713
	public MultiSliceElement.Direction m_WidthDirection;

	// Token: 0x04007BE2 RID: 31714
	public MultiSliceElement.Direction m_HeightDirection = MultiSliceElement.Direction.Z;

	// Token: 0x04007BE3 RID: 31715
	public Vector3 m_localPinnedPointOffset = Vector3.zero;

	// Token: 0x04007BE4 RID: 31716
	public MultiSliceElement.XAxisAlign m_XAlign;

	// Token: 0x04007BE5 RID: 31717
	public MultiSliceElement.YAxisAlign m_YAlign = MultiSliceElement.YAxisAlign.BOTTOM;

	// Token: 0x04007BE6 RID: 31718
	public MultiSliceElement.ZAxisAlign m_ZAlign = MultiSliceElement.ZAxisAlign.BACK;

	// Token: 0x04007BE7 RID: 31719
	public Vector3 m_localSliceSpacing = Vector3.zero;

	// Token: 0x04007BE8 RID: 31720
	public bool m_reverse;

	// Token: 0x04007BE9 RID: 31721
	public bool m_useUberText;
}
