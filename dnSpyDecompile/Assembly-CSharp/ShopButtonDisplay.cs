using System;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020006B8 RID: 1720
public class ShopButtonDisplay : MonoBehaviour
{
	// Token: 0x170005CF RID: 1487
	// (get) Token: 0x06006084 RID: 24708 RVA: 0x001F7226 File Offset: 0x001F5426
	// (set) Token: 0x06006085 RID: 24709 RVA: 0x001F722E File Offset: 0x001F542E
	[Overridable]
	public ShopButtonDisplay.ProductIndex Index
	{
		get
		{
			return this.m_index;
		}
		set
		{
			this.m_index = value;
		}
	}

	// Token: 0x06006086 RID: 24710 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x06006087 RID: 24711 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Update()
	{
	}

	// Token: 0x06006088 RID: 24712 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Reload()
	{
	}

	// Token: 0x040050C5 RID: 20677
	[SerializeField]
	protected ShopButtonDisplay.DisplayType displayType;

	// Token: 0x040050C6 RID: 20678
	[SerializeField]
	protected ShopButtonDisplay.ProductIndex m_index;

	// Token: 0x020021FF RID: 8703
	public enum DisplayType
	{
		// Token: 0x0400E210 RID: 57872
		BOOSTER,
		// Token: 0x0400E211 RID: 57873
		HERO,
		// Token: 0x0400E212 RID: 57874
		CARDBACK
	}

	// Token: 0x02002200 RID: 8704
	public enum ProductIndex
	{
		// Token: 0x0400E214 RID: 57876
		AUTO,
		// Token: 0x0400E215 RID: 57877
		FIRST,
		// Token: 0x0400E216 RID: 57878
		SECOND,
		// Token: 0x0400E217 RID: 57879
		THIRD
	}
}
