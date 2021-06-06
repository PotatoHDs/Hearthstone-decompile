using System;
using UnityEngine;

// Token: 0x02000B25 RID: 2853
[Serializable]
public class CanvasAnchors
{
	// Token: 0x0600975C RID: 38748 RVA: 0x0030EB08 File Offset: 0x0030CD08
	public Transform GetAnchor(CanvasAnchor type)
	{
		if (type == CanvasAnchor.CENTER)
		{
			return this.m_Center;
		}
		if (type == CanvasAnchor.LEFT)
		{
			return this.m_Left;
		}
		if (type == CanvasAnchor.RIGHT)
		{
			return this.m_Right;
		}
		if (type == CanvasAnchor.BOTTOM)
		{
			return this.m_Bottom;
		}
		if (type == CanvasAnchor.TOP)
		{
			return this.m_Top;
		}
		if (type == CanvasAnchor.BOTTOM_LEFT)
		{
			return this.m_BottomLeft;
		}
		if (type == CanvasAnchor.BOTTOM_RIGHT)
		{
			return this.m_BottomRight;
		}
		if (type == CanvasAnchor.TOP_LEFT)
		{
			return this.m_TopLeft;
		}
		if (type == CanvasAnchor.TOP_RIGHT)
		{
			return this.m_TopRight;
		}
		return this.m_Center;
	}

	// Token: 0x0600975D RID: 38749 RVA: 0x0030EB80 File Offset: 0x0030CD80
	public void WillReset()
	{
		foreach (object obj in this.m_Center)
		{
			UnityEngine.Object.Destroy(((Transform)obj).gameObject);
		}
		foreach (object obj2 in this.m_Left)
		{
			UnityEngine.Object.Destroy(((Transform)obj2).gameObject);
		}
		foreach (object obj3 in this.m_Right)
		{
			UnityEngine.Object.Destroy(((Transform)obj3).gameObject);
		}
		foreach (object obj4 in this.m_Bottom)
		{
			UnityEngine.Object.Destroy(((Transform)obj4).gameObject);
		}
		foreach (object obj5 in this.m_Top)
		{
			UnityEngine.Object.Destroy(((Transform)obj5).gameObject);
		}
		foreach (object obj6 in this.m_BottomLeft)
		{
			UnityEngine.Object.Destroy(((Transform)obj6).gameObject);
		}
		foreach (object obj7 in this.m_BottomRight)
		{
			UnityEngine.Object.Destroy(((Transform)obj7).gameObject);
		}
		foreach (object obj8 in this.m_TopLeft)
		{
			UnityEngine.Object.Destroy(((Transform)obj8).gameObject);
		}
		foreach (object obj9 in this.m_TopRight)
		{
			UnityEngine.Object.Destroy(((Transform)obj9).gameObject);
		}
	}

	// Token: 0x04007EB6 RID: 32438
	public Transform m_Center;

	// Token: 0x04007EB7 RID: 32439
	public Transform m_Left;

	// Token: 0x04007EB8 RID: 32440
	public Transform m_Right;

	// Token: 0x04007EB9 RID: 32441
	public Transform m_Bottom;

	// Token: 0x04007EBA RID: 32442
	public Transform m_Top;

	// Token: 0x04007EBB RID: 32443
	public Transform m_BottomLeft;

	// Token: 0x04007EBC RID: 32444
	public Transform m_BottomRight;

	// Token: 0x04007EBD RID: 32445
	public Transform m_TopLeft;

	// Token: 0x04007EBE RID: 32446
	public Transform m_TopRight;
}
