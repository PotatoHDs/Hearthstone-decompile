using System;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020006B7 RID: 1719
public class ShopBrowserElement : MonoBehaviour
{
	// Token: 0x170005C6 RID: 1478
	// (get) Token: 0x0600606A RID: 24682 RVA: 0x001F6E68 File Offset: 0x001F5068
	// (set) Token: 0x0600606B RID: 24683 RVA: 0x001F6E75 File Offset: 0x001F5075
	[Overridable]
	public float BoundsX
	{
		get
		{
			return this.Bounds.x;
		}
		set
		{
			this.Bounds.x = value;
			this.OnElementBoundsChanged();
		}
	}

	// Token: 0x170005C7 RID: 1479
	// (get) Token: 0x0600606C RID: 24684 RVA: 0x001F6E89 File Offset: 0x001F5089
	// (set) Token: 0x0600606D RID: 24685 RVA: 0x001F6E96 File Offset: 0x001F5096
	[Overridable]
	public float BoundsY
	{
		get
		{
			return this.Bounds.y;
		}
		set
		{
			this.Bounds.y = value;
			this.OnElementBoundsChanged();
		}
	}

	// Token: 0x170005C8 RID: 1480
	// (get) Token: 0x0600606E RID: 24686 RVA: 0x001F6EAA File Offset: 0x001F50AA
	// (set) Token: 0x0600606F RID: 24687 RVA: 0x001F6EB7 File Offset: 0x001F50B7
	[Overridable]
	public float Width
	{
		get
		{
			return this.Bounds.width;
		}
		set
		{
			this.Bounds.width = value;
			this.OnElementBoundsChanged();
		}
	}

	// Token: 0x170005C9 RID: 1481
	// (get) Token: 0x06006070 RID: 24688 RVA: 0x001F6ECB File Offset: 0x001F50CB
	// (set) Token: 0x06006071 RID: 24689 RVA: 0x001F6ED8 File Offset: 0x001F50D8
	[Overridable]
	public float Height
	{
		get
		{
			return this.Bounds.height;
		}
		set
		{
			this.Bounds.height = value;
			this.OnElementBoundsChanged();
		}
	}

	// Token: 0x170005CA RID: 1482
	// (get) Token: 0x06006072 RID: 24690 RVA: 0x001F6EEC File Offset: 0x001F50EC
	// (set) Token: 0x06006073 RID: 24691 RVA: 0x001F6F0C File Offset: 0x001F510C
	public float Top
	{
		get
		{
			return this.GetDisplayTransform().localPosition.z + this.Bounds.yMax;
		}
		set
		{
			Transform displayTransform = this.GetDisplayTransform();
			Vector3 localPosition = displayTransform.localPosition;
			displayTransform.localPosition = new Vector3(localPosition.x, localPosition.y, value - this.Bounds.yMax);
		}
	}

	// Token: 0x170005CB RID: 1483
	// (get) Token: 0x06006074 RID: 24692 RVA: 0x001F6F49 File Offset: 0x001F5149
	// (set) Token: 0x06006075 RID: 24693 RVA: 0x001F6F68 File Offset: 0x001F5168
	public float Left
	{
		get
		{
			return this.GetDisplayTransform().localPosition.x + this.Bounds.xMin;
		}
		set
		{
			Transform displayTransform = this.GetDisplayTransform();
			Vector3 localPosition = displayTransform.localPosition;
			displayTransform.localPosition = new Vector3(value - this.Bounds.xMin, localPosition.y, localPosition.z);
		}
	}

	// Token: 0x170005CC RID: 1484
	// (get) Token: 0x06006076 RID: 24694 RVA: 0x001F6FA5 File Offset: 0x001F51A5
	// (set) Token: 0x06006077 RID: 24695 RVA: 0x001F6FC4 File Offset: 0x001F51C4
	public float Right
	{
		get
		{
			return this.GetDisplayTransform().localPosition.x + this.Bounds.xMax;
		}
		set
		{
			Transform displayTransform = this.GetDisplayTransform();
			Vector3 localPosition = displayTransform.localPosition;
			displayTransform.localPosition = new Vector3(value - this.Bounds.xMax, localPosition.y, localPosition.z);
		}
	}

	// Token: 0x170005CD RID: 1485
	// (get) Token: 0x06006078 RID: 24696 RVA: 0x001F7001 File Offset: 0x001F5201
	// (set) Token: 0x06006079 RID: 24697 RVA: 0x001F7020 File Offset: 0x001F5220
	public float Bottom
	{
		get
		{
			return this.GetDisplayTransform().localPosition.z + this.Bounds.yMin;
		}
		set
		{
			Transform displayTransform = this.GetDisplayTransform();
			Vector3 localPosition = displayTransform.localPosition;
			displayTransform.localPosition = new Vector3(localPosition.x, value - this.Bounds.yMin, localPosition.z);
		}
	}

	// Token: 0x170005CE RID: 1486
	// (get) Token: 0x0600607A RID: 24698 RVA: 0x001F705D File Offset: 0x001F525D
	// (set) Token: 0x0600607B RID: 24699 RVA: 0x001F7065 File Offset: 0x001F5265
	[Overridable]
	public bool IsElementEnabled
	{
		get
		{
			return this.m_isElementEnabled;
		}
		set
		{
			this.m_isElementEnabled = value;
			this.OnElementEnabled();
		}
	}

	// Token: 0x0600607C RID: 24700 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnElementBoundsChanged()
	{
	}

	// Token: 0x0600607D RID: 24701 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnElementEnabled()
	{
	}

	// Token: 0x0600607E RID: 24702 RVA: 0x001F7074 File Offset: 0x001F5274
	protected Transform GetDisplayTransform()
	{
		if (!(base.GetComponent<WidgetTemplate>() != null))
		{
			return base.transform;
		}
		return base.transform.parent;
	}

	// Token: 0x0600607F RID: 24703 RVA: 0x001F7098 File Offset: 0x001F5298
	public static int ComparePosition(ShopBrowserElement A, ShopBrowserElement B)
	{
		if (Mathf.Approximately(A.Top, B.Top))
		{
			if (Mathf.Approximately(A.Left, B.Left))
			{
				return 0;
			}
			if (A.Left >= B.Left)
			{
				return 1;
			}
			return -1;
		}
		else
		{
			if (A.Top <= B.Top)
			{
				return 1;
			}
			return -1;
		}
	}

	// Token: 0x06006080 RID: 24704 RVA: 0x001F70F0 File Offset: 0x001F52F0
	private void OnDrawGizmosSelected()
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		this.DrawRegion(this.m_boundsColor, 0f);
	}

	// Token: 0x06006081 RID: 24705 RVA: 0x001F710C File Offset: 0x001F530C
	private void OnDrawGizmos()
	{
		if (!base.isActiveAndEnabled && !this.m_previewBounds)
		{
			return;
		}
		this.DrawRegion(this.m_boundsColor, 0f);
	}

	// Token: 0x06006082 RID: 24706 RVA: 0x001F7130 File Offset: 0x001F5330
	protected void DrawRegion(Color color, float padding = 0f)
	{
		Vector3 size = new Vector3(this.Width, this.Thickness, this.Height);
		size.x += padding;
		size.z += padding;
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.color = color;
		if (this.m_previewOutline)
		{
			Gizmos.DrawWireCube(new Vector3(this.Bounds.center.x, 0f, this.Bounds.center.y), size);
			return;
		}
		Gizmos.DrawCube(new Vector3(this.Bounds.center.x, 0f, this.Bounds.center.y), size);
	}

	// Token: 0x040050BF RID: 20671
	public Rect Bounds;

	// Token: 0x040050C0 RID: 20672
	protected bool m_isElementEnabled = true;

	// Token: 0x040050C1 RID: 20673
	public bool m_previewBounds;

	// Token: 0x040050C2 RID: 20674
	public bool m_previewOutline;

	// Token: 0x040050C3 RID: 20675
	public Color m_boundsColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

	// Token: 0x040050C4 RID: 20676
	public float Thickness = 0.1f;

	// Token: 0x020021FE RID: 8702
	public enum Side
	{
		// Token: 0x0400E20B RID: 57867
		TOP,
		// Token: 0x0400E20C RID: 57868
		BOTTOM,
		// Token: 0x0400E20D RID: 57869
		LEFT,
		// Token: 0x0400E20E RID: 57870
		RIGHT
	}
}
