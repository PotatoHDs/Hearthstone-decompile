using System;
using UnityEngine;

// Token: 0x0200009B RID: 155
public class MobileChatLogMessageFrame : MonoBehaviour, ITouchListItem
{
	// Token: 0x17000088 RID: 136
	// (get) Token: 0x060009B3 RID: 2483 RVA: 0x000381A6 File Offset: 0x000363A6
	// (set) Token: 0x060009B4 RID: 2484 RVA: 0x000381B3 File Offset: 0x000363B3
	public string Message
	{
		get
		{
			return this.text.Text;
		}
		set
		{
			this.text.Text = value;
			this.text.UpdateNow(false);
			this.UpdateLocalBounds();
		}
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public bool IsHeader
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x060009B6 RID: 2486 RVA: 0x000052EC File Offset: 0x000034EC
	// (set) Token: 0x060009B7 RID: 2487 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public bool Visible
	{
		get
		{
			return true;
		}
		set
		{
		}
	}

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x060009B8 RID: 2488 RVA: 0x000381D3 File Offset: 0x000363D3
	// (set) Token: 0x060009B9 RID: 2489 RVA: 0x000381E0 File Offset: 0x000363E0
	public Color Color
	{
		get
		{
			return this.text.TextColor;
		}
		set
		{
			this.text.TextColor = value;
		}
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x060009BA RID: 2490 RVA: 0x000381EE File Offset: 0x000363EE
	// (set) Token: 0x060009BB RID: 2491 RVA: 0x000381FC File Offset: 0x000363FC
	public float Width
	{
		get
		{
			return this.text.Width;
		}
		set
		{
			this.text.Width = value;
			if (this.m_Background != null)
			{
				float x = this.m_Background.GetComponent<MeshFilter>().mesh.bounds.size.x;
				this.m_Background.transform.localScale = new Vector3(value / x, this.m_Background.transform.localScale.y, 1f);
				this.m_Background.transform.localPosition = new Vector3(-value / (0.5f * x), 0f, 0f);
			}
		}
	}

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x060009BC RID: 2492 RVA: 0x000382A1 File Offset: 0x000364A1
	public Bounds LocalBounds
	{
		get
		{
			return this.localBounds;
		}
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x000382A9 File Offset: 0x000364A9
	public new T GetComponent<T>() where T : Component
	{
		return base.GetComponent<T>();
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x000382B1 File Offset: 0x000364B1
	public void RebuildUberText()
	{
		this.text.UpdateNow(true);
		this.UpdateLocalBounds();
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void OnScrollOutOfView()
	{
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x000382C8 File Offset: 0x000364C8
	private void UpdateLocalBounds()
	{
		Bounds textBounds = this.text.GetTextBounds();
		Vector3 size = textBounds.size;
		this.localBounds.center = base.transform.InverseTransformPoint(textBounds.center) + 10f * Vector3.up;
		this.localBounds.size = size;
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x00036786 File Offset: 0x00034986
	GameObject ITouchListItem.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x0003678E File Offset: 0x0003498E
	Transform ITouchListItem.get_transform()
	{
		return base.transform;
	}

	// Token: 0x0400066F RID: 1647
	public UberText text;

	// Token: 0x04000670 RID: 1648
	public GameObject m_Background;

	// Token: 0x04000671 RID: 1649
	private Bounds localBounds;
}
