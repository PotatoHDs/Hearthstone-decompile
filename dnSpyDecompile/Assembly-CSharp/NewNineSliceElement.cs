using System;
using UnityEngine;

// Token: 0x02000AD3 RID: 2771
[ExecuteAlways]
public class NewNineSliceElement : MonoBehaviour
{
	// Token: 0x060093A9 RID: 37801 RVA: 0x002FE3FC File Offset: 0x002FC5FC
	public virtual void SetSize(float width, float height)
	{
		if (this.m_mode == NewNineSliceElement.Mode.UseSize)
		{
			this.m_size = new Vector2(width, height);
			Vector3 size = this.m_topLeft.GetComponent<Renderer>().bounds.size;
			Vector3 size2 = this.m_bottomLeft.GetComponent<Renderer>().bounds.size;
			width = Mathf.Max(width - (size.x + size2.x), 1f);
			height = Mathf.Max(height - (size.y + size2.y), 1f);
			this.SetPieceWidth(this.m_middle, width);
			this.SetPieceHeight(this.m_middle, height);
			this.SetPieceWidth(this.m_top, width);
			this.SetPieceWidth(this.m_bottom, width);
			this.SetPieceHeight(this.m_left, height);
			this.SetPieceHeight(this.m_right, height);
		}
		else
		{
			TransformUtil.SetLocalScaleXZ(this.m_middle, new Vector2(width, height));
			TransformUtil.SetLocalScaleX(this.m_top, width);
			TransformUtil.SetLocalScaleX(this.m_bottom, width);
			TransformUtil.SetLocalScaleZ(this.m_left, height);
			TransformUtil.SetLocalScaleZ(this.m_right, height);
		}
		Vector3 vector;
		Vector3 vector2;
		Vector3 vector3;
		Vector3 vector4;
		Vector3 vector5;
		if (this.m_planeAxis == NewNineSliceElement.PlaneAxis.XZ)
		{
			vector = new Vector3(0f, 0f, 1f);
			vector2 = new Vector3(0f, 0f, 0f);
			vector3 = new Vector3(0f, 0f, 0.5f);
			vector4 = new Vector3(1f, 0f, 0.5f);
			vector5 = new Vector3(0.5f, 0f, 0.5f);
		}
		else
		{
			vector = new Vector3(0f, 1f, 0f);
			vector2 = new Vector3(0f, 0f, 0f);
			vector3 = new Vector3(0f, 0.5f, 0f);
			vector4 = new Vector3(1f, 0.5f, 0f);
			vector5 = new Vector3(0.5f, 0.5f, 0f);
		}
		switch (this.m_pinnedPoint)
		{
		case NewNineSliceElement.PinnedPoint.TOPLEFT:
			TransformUtil.SetPoint(this.m_topLeft, Anchor.TOP_LEFT, this.m_anchorBone, Anchor.TOP_LEFT);
			TransformUtil.SetPoint(this.m_top, Anchor.LEFT, this.m_topLeft, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_topRight, Anchor.LEFT, this.m_top, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_left, vector, this.m_topLeft, vector2);
			TransformUtil.SetPoint(this.m_middle, Anchor.LEFT, this.m_left, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_right, Anchor.LEFT, this.m_middle, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_bottomLeft, vector, this.m_left, vector2);
			TransformUtil.SetPoint(this.m_bottom, Anchor.LEFT, this.m_bottomLeft, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_bottomRight, Anchor.LEFT, this.m_bottom, Anchor.RIGHT);
			return;
		case NewNineSliceElement.PinnedPoint.TOP:
			TransformUtil.SetPoint(this.m_top, Anchor.TOP, this.m_anchorBone, Anchor.TOP);
			TransformUtil.SetPoint(this.m_topLeft, Anchor.RIGHT, this.m_top, Anchor.LEFT);
			TransformUtil.SetPoint(this.m_topRight, Anchor.LEFT, this.m_top, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_left, vector, this.m_topLeft, vector2);
			TransformUtil.SetPoint(this.m_middle, Anchor.LEFT, this.m_left, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_right, Anchor.LEFT, this.m_middle, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_bottomLeft, vector, this.m_left, vector2);
			TransformUtil.SetPoint(this.m_bottom, Anchor.LEFT, this.m_bottomLeft, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_bottomRight, Anchor.LEFT, this.m_bottom, Anchor.RIGHT);
			return;
		case NewNineSliceElement.PinnedPoint.TOPRIGHT:
			TransformUtil.SetPoint(this.m_topRight, Anchor.TOP_RIGHT, this.m_anchorBone, Anchor.TOP_RIGHT);
			TransformUtil.SetPoint(this.m_top, Anchor.RIGHT, this.m_topRight, Anchor.LEFT);
			TransformUtil.SetPoint(this.m_topLeft, Anchor.RIGHT, this.m_top, Anchor.LEFT);
			TransformUtil.SetPoint(this.m_left, vector, this.m_topLeft, vector2);
			TransformUtil.SetPoint(this.m_middle, Anchor.LEFT, this.m_left, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_right, Anchor.LEFT, this.m_middle, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_bottomLeft, vector, this.m_left, vector2);
			TransformUtil.SetPoint(this.m_bottom, Anchor.LEFT, this.m_bottomLeft, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_bottomRight, Anchor.LEFT, this.m_bottom, Anchor.RIGHT);
			return;
		case NewNineSliceElement.PinnedPoint.LEFT:
			TransformUtil.SetPoint(this.m_left, vector3, this.m_anchorBone, vector3);
			TransformUtil.SetPoint(this.m_middle, Anchor.LEFT, this.m_left, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_right, Anchor.LEFT, this.m_middle, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_topLeft, vector2, this.m_left, vector);
			TransformUtil.SetPoint(this.m_top, Anchor.LEFT, this.m_topLeft, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_topRight, Anchor.LEFT, this.m_top, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_bottomLeft, vector, this.m_left, vector2);
			TransformUtil.SetPoint(this.m_bottom, Anchor.LEFT, this.m_bottomLeft, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_bottomRight, Anchor.LEFT, this.m_bottom, Anchor.RIGHT);
			return;
		case NewNineSliceElement.PinnedPoint.MIDDLE:
			TransformUtil.SetPoint(this.m_middle, vector5, this.m_anchorBone, vector5);
			TransformUtil.SetPoint(this.m_left, Anchor.RIGHT, this.m_middle, Anchor.LEFT);
			TransformUtil.SetPoint(this.m_right, Anchor.LEFT, this.m_middle, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_topLeft, vector2, this.m_left, vector);
			TransformUtil.SetPoint(this.m_top, Anchor.LEFT, this.m_topLeft, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_topRight, Anchor.LEFT, this.m_top, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_bottomLeft, vector, this.m_left, vector2);
			TransformUtil.SetPoint(this.m_bottom, Anchor.LEFT, this.m_bottomLeft, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_bottomRight, Anchor.LEFT, this.m_bottom, Anchor.RIGHT);
			return;
		case NewNineSliceElement.PinnedPoint.RIGHT:
			TransformUtil.SetPoint(this.m_right, vector4, this.m_anchorBone, vector4);
			TransformUtil.SetPoint(this.m_middle, Anchor.RIGHT, this.m_right, Anchor.LEFT);
			TransformUtil.SetPoint(this.m_left, Anchor.RIGHT, this.m_middle, Anchor.LEFT);
			TransformUtil.SetPoint(this.m_topLeft, vector2, this.m_left, vector);
			TransformUtil.SetPoint(this.m_top, Anchor.LEFT, this.m_topLeft, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_topRight, Anchor.LEFT, this.m_top, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_bottomLeft, vector, this.m_left, vector2);
			TransformUtil.SetPoint(this.m_bottom, Anchor.LEFT, this.m_bottomLeft, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_bottomRight, Anchor.LEFT, this.m_bottom, Anchor.RIGHT);
			return;
		case NewNineSliceElement.PinnedPoint.BOTTOMLEFT:
			TransformUtil.SetPoint(this.m_bottomLeft, Anchor.BOTTOM_LEFT, this.m_anchorBone, Anchor.BOTTOM_LEFT);
			TransformUtil.SetPoint(this.m_bottom, Anchor.LEFT, this.m_bottomLeft, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_bottomRight, Anchor.LEFT, this.m_bottom, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_left, vector2, this.m_bottomLeft, vector);
			TransformUtil.SetPoint(this.m_middle, Anchor.LEFT, this.m_left, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_right, Anchor.LEFT, this.m_middle, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_topLeft, vector2, this.m_left, vector);
			TransformUtil.SetPoint(this.m_top, Anchor.LEFT, this.m_topLeft, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_topRight, Anchor.LEFT, this.m_top, Anchor.RIGHT);
			return;
		case NewNineSliceElement.PinnedPoint.BOTTOM:
			TransformUtil.SetPoint(this.m_bottom, Anchor.BOTTOM, this.m_anchorBone, Anchor.BOTTOM);
			TransformUtil.SetPoint(this.m_bottomLeft, Anchor.RIGHT, this.m_bottom, Anchor.LEFT);
			TransformUtil.SetPoint(this.m_bottomRight, Anchor.LEFT, this.m_bottom, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_left, vector2, this.m_bottomLeft, vector);
			TransformUtil.SetPoint(this.m_middle, Anchor.LEFT, this.m_left, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_right, Anchor.LEFT, this.m_middle, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_topLeft, vector2, this.m_left, vector);
			TransformUtil.SetPoint(this.m_top, Anchor.LEFT, this.m_topLeft, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_topRight, Anchor.LEFT, this.m_top, Anchor.RIGHT);
			return;
		case NewNineSliceElement.PinnedPoint.BOTTOMRIGHT:
			TransformUtil.SetPoint(this.m_bottomRight, Anchor.BOTTOM_RIGHT, this.m_anchorBone, Anchor.BOTTOM_RIGHT);
			TransformUtil.SetPoint(this.m_bottom, Anchor.RIGHT, this.m_bottomRight, Anchor.LEFT);
			TransformUtil.SetPoint(this.m_bottomLeft, Anchor.RIGHT, this.m_bottom, Anchor.LEFT);
			TransformUtil.SetPoint(this.m_left, vector2, this.m_bottomLeft, vector);
			TransformUtil.SetPoint(this.m_middle, Anchor.LEFT, this.m_left, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_right, Anchor.LEFT, this.m_middle, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_topLeft, vector2, this.m_left, vector);
			TransformUtil.SetPoint(this.m_top, Anchor.LEFT, this.m_topLeft, Anchor.RIGHT);
			TransformUtil.SetPoint(this.m_topRight, Anchor.LEFT, this.m_top, Anchor.RIGHT);
			return;
		default:
			return;
		}
	}

	// Token: 0x060093AA RID: 37802 RVA: 0x002FEC4C File Offset: 0x002FCE4C
	private void SetPieceWidth(GameObject piece, float width)
	{
		Vector3 localScale = piece.transform.localScale;
		localScale.x = width * piece.transform.localScale.x / piece.GetComponent<Renderer>().bounds.size.x;
		piece.transform.localScale = localScale;
	}

	// Token: 0x060093AB RID: 37803 RVA: 0x002FECA4 File Offset: 0x002FCEA4
	private void SetPieceHeight(GameObject piece, float height)
	{
		Vector3 localScale = piece.transform.localScale;
		localScale.z = height * piece.transform.localScale.z / piece.GetComponent<Renderer>().bounds.size.y;
		piece.transform.localScale = localScale;
	}

	// Token: 0x04007BB4 RID: 31668
	public GameObject m_topLeft;

	// Token: 0x04007BB5 RID: 31669
	public GameObject m_top;

	// Token: 0x04007BB6 RID: 31670
	public GameObject m_topRight;

	// Token: 0x04007BB7 RID: 31671
	public GameObject m_left;

	// Token: 0x04007BB8 RID: 31672
	public GameObject m_right;

	// Token: 0x04007BB9 RID: 31673
	public GameObject m_middle;

	// Token: 0x04007BBA RID: 31674
	public GameObject m_bottomLeft;

	// Token: 0x04007BBB RID: 31675
	public GameObject m_bottom;

	// Token: 0x04007BBC RID: 31676
	public GameObject m_bottomRight;

	// Token: 0x04007BBD RID: 31677
	public GameObject m_anchorBone;

	// Token: 0x04007BBE RID: 31678
	public NewNineSliceElement.PinnedPoint m_pinnedPoint = NewNineSliceElement.PinnedPoint.TOP;

	// Token: 0x04007BBF RID: 31679
	public NewNineSliceElement.PlaneAxis m_planeAxis = NewNineSliceElement.PlaneAxis.XZ;

	// Token: 0x04007BC0 RID: 31680
	public Vector3 m_pinnedPointOffset;

	// Token: 0x04007BC1 RID: 31681
	public Vector2 m_middleScale;

	// Token: 0x04007BC2 RID: 31682
	public Vector2 m_size;

	// Token: 0x04007BC3 RID: 31683
	public NewNineSliceElement.Mode m_mode;

	// Token: 0x0200270A RID: 9994
	public enum PinnedPoint
	{
		// Token: 0x0400F322 RID: 62242
		TOPLEFT,
		// Token: 0x0400F323 RID: 62243
		TOP,
		// Token: 0x0400F324 RID: 62244
		TOPRIGHT,
		// Token: 0x0400F325 RID: 62245
		LEFT,
		// Token: 0x0400F326 RID: 62246
		MIDDLE,
		// Token: 0x0400F327 RID: 62247
		RIGHT,
		// Token: 0x0400F328 RID: 62248
		BOTTOMLEFT,
		// Token: 0x0400F329 RID: 62249
		BOTTOM,
		// Token: 0x0400F32A RID: 62250
		BOTTOMRIGHT
	}

	// Token: 0x0200270B RID: 9995
	public enum PlaneAxis
	{
		// Token: 0x0400F32C RID: 62252
		XY,
		// Token: 0x0400F32D RID: 62253
		XZ
	}

	// Token: 0x0200270C RID: 9996
	public enum Mode
	{
		// Token: 0x0400F32F RID: 62255
		UseMiddleScale,
		// Token: 0x0400F330 RID: 62256
		UseSize
	}
}
