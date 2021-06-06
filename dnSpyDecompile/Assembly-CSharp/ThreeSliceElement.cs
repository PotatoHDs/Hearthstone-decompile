using System;
using UnityEngine;

// Token: 0x02000AE3 RID: 2787
[ExecuteAlways]
public class ThreeSliceElement : MonoBehaviour
{
	// Token: 0x06009479 RID: 38009 RVA: 0x0030228A File Offset: 0x0030048A
	private void Awake()
	{
		if (this.m_middle)
		{
			this.SetInitialValues();
		}
	}

	// Token: 0x0600947A RID: 38010 RVA: 0x003022A0 File Offset: 0x003004A0
	public void UpdateDisplay()
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.m_initialMiddleBounds.size == Vector3.zero)
		{
			this.m_initialMiddleBounds = this.m_middle.GetComponent<Renderer>().bounds;
		}
		float num = this.m_width - (this.m_left.GetComponent<Renderer>().bounds.size.x + this.m_right.GetComponent<Renderer>().bounds.size.x);
		switch (this.m_direction)
		{
		case ThreeSliceElement.Direction.X:
		{
			Vector3 scale = TransformUtil.ComputeWorldScale(this.m_middle.transform);
			scale.x = this.m_initialScale.x * num / this.m_initialMiddleBounds.size.x;
			TransformUtil.SetWorldScale(this.m_middle.transform, scale);
			break;
		}
		}
		switch (this.m_pinnedPoint)
		{
		case ThreeSliceElement.PinnedPoint.LEFT:
			this.m_left.transform.localPosition = this.m_pinnedPointOffset;
			TransformUtil.SetPoint(this.m_middle, Anchor.LEFT, this.m_left, Anchor.RIGHT, this.m_middleOffset);
			TransformUtil.SetPoint(this.m_right, Anchor.LEFT, this.m_middle, Anchor.RIGHT, this.m_rightOffset);
			return;
		case ThreeSliceElement.PinnedPoint.MIDDLE:
			this.m_middle.transform.localPosition = this.m_pinnedPointOffset;
			TransformUtil.SetPoint(this.m_left, Anchor.RIGHT, this.m_middle, Anchor.LEFT, this.m_leftOffset);
			TransformUtil.SetPoint(this.m_right, Anchor.LEFT, this.m_middle, Anchor.RIGHT, this.m_rightOffset);
			return;
		case ThreeSliceElement.PinnedPoint.RIGHT:
			this.m_right.transform.localPosition = this.m_pinnedPointOffset;
			TransformUtil.SetPoint(this.m_middle, Anchor.RIGHT, this.m_right, Anchor.LEFT, this.m_middleOffset);
			TransformUtil.SetPoint(this.m_left, Anchor.RIGHT, this.m_middle, Anchor.LEFT, this.m_leftOffset);
			return;
		default:
			return;
		}
	}

	// Token: 0x0600947B RID: 38011 RVA: 0x0030247E File Offset: 0x0030067E
	public void SetWidth(float globalWidth)
	{
		this.m_width = globalWidth;
		this.UpdateDisplay();
	}

	// Token: 0x0600947C RID: 38012 RVA: 0x00302490 File Offset: 0x00300690
	public void SetMiddleWidth(float globalWidth)
	{
		this.m_width = globalWidth + this.m_left.GetComponent<Renderer>().bounds.size.x + this.m_right.GetComponent<Renderer>().bounds.size.x;
		this.UpdateDisplay();
	}

	// Token: 0x0600947D RID: 38013 RVA: 0x003024E8 File Offset: 0x003006E8
	public Vector3 GetMiddleSize()
	{
		return this.m_middle.GetComponent<Renderer>().bounds.size;
	}

	// Token: 0x0600947E RID: 38014 RVA: 0x0030250D File Offset: 0x0030070D
	public Vector3 GetSize()
	{
		return this.GetSize(true);
	}

	// Token: 0x0600947F RID: 38015 RVA: 0x00302518 File Offset: 0x00300718
	public Vector3 GetSize(bool zIsHeight)
	{
		Vector3 size = this.m_left.GetComponent<Renderer>().bounds.size;
		Vector3 size2 = this.m_middle.GetComponent<Renderer>().bounds.size;
		Vector3 size3 = this.m_right.GetComponent<Renderer>().bounds.size;
		float x = size.x + size3.x + size2.x;
		float num = Mathf.Max(Mathf.Max(size.z, size2.z), size3.z);
		float num2 = Mathf.Max(Mathf.Max(size.y, size2.y), size3.y);
		if (zIsHeight)
		{
			return new Vector3(x, num, num2);
		}
		return new Vector3(x, num2, num);
	}

	// Token: 0x06009480 RID: 38016 RVA: 0x003025D8 File Offset: 0x003007D8
	public void SetInitialValues()
	{
		this.m_initialMiddleBounds = this.m_middle.GetComponent<Renderer>().bounds;
		this.m_initialScale = this.m_middle.transform.lossyScale;
		this.m_width = this.m_middle.GetComponent<Renderer>().bounds.size.x + this.m_left.GetComponent<Renderer>().bounds.size.x + this.m_right.GetComponent<Renderer>().bounds.size.x;
	}

	// Token: 0x04007C6E RID: 31854
	public GameObject m_left;

	// Token: 0x04007C6F RID: 31855
	public GameObject m_middle;

	// Token: 0x04007C70 RID: 31856
	public GameObject m_right;

	// Token: 0x04007C71 RID: 31857
	public ThreeSliceElement.PinnedPoint m_pinnedPoint;

	// Token: 0x04007C72 RID: 31858
	public Vector3 m_pinnedPointOffset;

	// Token: 0x04007C73 RID: 31859
	public ThreeSliceElement.Direction m_direction;

	// Token: 0x04007C74 RID: 31860
	public float m_width;

	// Token: 0x04007C75 RID: 31861
	public float m_middleScale = 1f;

	// Token: 0x04007C76 RID: 31862
	public Vector3 m_leftOffset;

	// Token: 0x04007C77 RID: 31863
	public Vector3 m_middleOffset;

	// Token: 0x04007C78 RID: 31864
	public Vector3 m_rightOffset;

	// Token: 0x04007C79 RID: 31865
	private Bounds m_initialMiddleBounds;

	// Token: 0x04007C7A RID: 31866
	private Vector3 m_initialScale = Vector3.zero;

	// Token: 0x0200271C RID: 10012
	public enum PinnedPoint
	{
		// Token: 0x0400F36A RID: 62314
		LEFT,
		// Token: 0x0400F36B RID: 62315
		MIDDLE,
		// Token: 0x0400F36C RID: 62316
		RIGHT,
		// Token: 0x0400F36D RID: 62317
		TOP,
		// Token: 0x0400F36E RID: 62318
		BOTTOM
	}

	// Token: 0x0200271D RID: 10013
	public enum Direction
	{
		// Token: 0x0400F370 RID: 62320
		X,
		// Token: 0x0400F371 RID: 62321
		Y,
		// Token: 0x0400F372 RID: 62322
		Z
	}
}
