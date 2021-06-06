using System;
using UnityEngine;

// Token: 0x02000AD4 RID: 2772
[ExecuteAlways]
public class NewThreeSliceElement : MonoBehaviour
{
	// Token: 0x060093AD RID: 37805 RVA: 0x002FED11 File Offset: 0x002FCF11
	private void Awake()
	{
		this.SetSize(this.m_middleScale);
	}

	// Token: 0x060093AE RID: 37806 RVA: 0x002FED1F File Offset: 0x002FCF1F
	private void OnDestroy()
	{
		if (this.m_identity != null && this.m_identity.gameObject != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_identity.gameObject);
		}
	}

	// Token: 0x060093AF RID: 37807 RVA: 0x002FED54 File Offset: 0x002FCF54
	public virtual void SetSize(Vector3 size)
	{
		this.m_middle.transform.localScale = size;
		if (this.m_identity == null)
		{
			this.m_identity = new GameObject().transform;
		}
		this.m_identity.position = Vector3.zero;
		if (this.m_planeAxis == NewThreeSliceElement.PlaneAxis.XZ)
		{
			this.m_leftAnchor = new Vector3(0f, 0f, 0.5f);
			this.m_rightAnchor = new Vector3(1f, 0f, 0.5f);
			this.m_topAnchor = new Vector3(0.5f, 0f, 1f);
			this.m_bottomAnchor = new Vector3(0.5f, 0f, 0f);
		}
		else
		{
			this.m_leftAnchor = new Vector3(0f, 0.5f, 0f);
			this.m_rightAnchor = new Vector3(1f, 0.5f, 0f);
			this.m_topAnchor = new Vector3(0.5f, 0f, 0f);
			this.m_bottomAnchor = new Vector3(0.5f, 1f, 0f);
		}
		switch (this.m_direction)
		{
		case NewThreeSliceElement.Direction.X:
			this.DisplayOnXAxis();
			return;
		case NewThreeSliceElement.Direction.Y:
			break;
		case NewThreeSliceElement.Direction.Z:
			this.DisplayOnZAxis();
			break;
		default:
			return;
		}
	}

	// Token: 0x060093B0 RID: 37808 RVA: 0x002FEEA4 File Offset: 0x002FD0A4
	private void DisplayOnXAxis()
	{
		switch (this.m_pinnedPoint)
		{
		case NewThreeSliceElement.PinnedPoint.LEFT:
			this.m_leftOrTop.transform.localPosition = this.m_pinnedPointOffset;
			TransformUtil.SetPoint(this.m_middle, this.m_leftAnchor, this.m_leftOrTop, this.m_rightAnchor, this.m_identity.transform.TransformPoint(this.m_middleOffset));
			TransformUtil.SetPoint(this.m_rightOrBottom, this.m_leftAnchor, this.m_middle, this.m_rightAnchor, this.m_identity.transform.TransformPoint(this.m_rightOffset));
			return;
		case NewThreeSliceElement.PinnedPoint.MIDDLE:
			this.m_middle.transform.localPosition = this.m_pinnedPointOffset;
			TransformUtil.SetPoint(this.m_leftOrTop, this.m_rightAnchor, this.m_middle, this.m_leftAnchor, this.m_identity.transform.TransformPoint(this.m_leftOffset));
			TransformUtil.SetPoint(this.m_rightOrBottom, this.m_leftAnchor, this.m_middle, this.m_rightAnchor, this.m_identity.transform.TransformPoint(this.m_rightOffset));
			return;
		case NewThreeSliceElement.PinnedPoint.RIGHT:
			this.m_rightOrBottom.transform.localPosition = this.m_pinnedPointOffset;
			TransformUtil.SetPoint(this.m_middle, this.m_rightAnchor, this.m_rightOrBottom, this.m_leftAnchor, this.m_identity.transform.TransformPoint(this.m_middleOffset));
			TransformUtil.SetPoint(this.m_leftOrTop, this.m_rightAnchor, this.m_middle, this.m_leftAnchor, this.m_identity.transform.TransformPoint(this.m_leftOffset));
			return;
		default:
			return;
		}
	}

	// Token: 0x060093B1 RID: 37809 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void DisplayOnYAxis()
	{
	}

	// Token: 0x060093B2 RID: 37810 RVA: 0x002FF044 File Offset: 0x002FD244
	private void DisplayOnZAxis()
	{
		switch (this.m_pinnedPoint)
		{
		case NewThreeSliceElement.PinnedPoint.MIDDLE:
			this.m_middle.transform.localPosition = this.m_pinnedPointOffset;
			TransformUtil.SetPoint(this.m_leftOrTop, this.m_bottomAnchor, this.m_middle, this.m_topAnchor, this.m_identity.transform.TransformPoint(this.m_leftOffset));
			TransformUtil.SetPoint(this.m_rightOrBottom, this.m_topAnchor, this.m_middle, this.m_bottomAnchor, this.m_identity.transform.TransformPoint(this.m_rightOffset));
			break;
		case NewThreeSliceElement.PinnedPoint.RIGHT:
			break;
		case NewThreeSliceElement.PinnedPoint.TOP:
			this.m_leftOrTop.transform.localPosition = this.m_pinnedPointOffset;
			TransformUtil.SetPoint(this.m_middle, this.m_topAnchor, this.m_leftOrTop, this.m_bottomAnchor, this.m_identity.transform.TransformPoint(this.m_middleOffset));
			TransformUtil.SetPoint(this.m_rightOrBottom, this.m_topAnchor, this.m_middle, this.m_bottomAnchor, this.m_identity.transform.TransformPoint(this.m_rightOffset));
			return;
		case NewThreeSliceElement.PinnedPoint.BOTTOM:
			this.m_rightOrBottom.transform.localPosition = this.m_pinnedPointOffset;
			TransformUtil.SetPoint(this.m_middle, this.m_bottomAnchor, this.m_rightOrBottom, this.m_topAnchor, this.m_identity.transform.TransformPoint(this.m_middleOffset));
			TransformUtil.SetPoint(this.m_leftOrTop, this.m_bottomAnchor, this.m_middle, this.m_topAnchor, this.m_identity.transform.TransformPoint(this.m_leftOffset));
			return;
		default:
			return;
		}
	}

	// Token: 0x04007BC4 RID: 31684
	public GameObject m_leftOrTop;

	// Token: 0x04007BC5 RID: 31685
	public GameObject m_middle;

	// Token: 0x04007BC6 RID: 31686
	public GameObject m_rightOrBottom;

	// Token: 0x04007BC7 RID: 31687
	public NewThreeSliceElement.PinnedPoint m_pinnedPoint;

	// Token: 0x04007BC8 RID: 31688
	public NewThreeSliceElement.PlaneAxis m_planeAxis = NewThreeSliceElement.PlaneAxis.XZ;

	// Token: 0x04007BC9 RID: 31689
	public Vector3 m_pinnedPointOffset;

	// Token: 0x04007BCA RID: 31690
	public NewThreeSliceElement.Direction m_direction;

	// Token: 0x04007BCB RID: 31691
	public Vector3 m_middleScale = Vector3.one;

	// Token: 0x04007BCC RID: 31692
	public Vector3 m_leftOffset;

	// Token: 0x04007BCD RID: 31693
	public Vector3 m_middleOffset;

	// Token: 0x04007BCE RID: 31694
	public Vector3 m_rightOffset;

	// Token: 0x04007BCF RID: 31695
	private Vector3 m_leftAnchor;

	// Token: 0x04007BD0 RID: 31696
	private Vector3 m_rightAnchor;

	// Token: 0x04007BD1 RID: 31697
	private Vector3 m_topAnchor;

	// Token: 0x04007BD2 RID: 31698
	private Vector3 m_bottomAnchor;

	// Token: 0x04007BD3 RID: 31699
	private Transform m_identity;

	// Token: 0x0200270D RID: 9997
	public enum PinnedPoint
	{
		// Token: 0x0400F332 RID: 62258
		LEFT,
		// Token: 0x0400F333 RID: 62259
		MIDDLE,
		// Token: 0x0400F334 RID: 62260
		RIGHT,
		// Token: 0x0400F335 RID: 62261
		TOP,
		// Token: 0x0400F336 RID: 62262
		BOTTOM
	}

	// Token: 0x0200270E RID: 9998
	public enum Direction
	{
		// Token: 0x0400F338 RID: 62264
		X,
		// Token: 0x0400F339 RID: 62265
		Y,
		// Token: 0x0400F33A RID: 62266
		Z
	}

	// Token: 0x0200270F RID: 9999
	public enum PlaneAxis
	{
		// Token: 0x0400F33C RID: 62268
		XY,
		// Token: 0x0400F33D RID: 62269
		XZ
	}
}
