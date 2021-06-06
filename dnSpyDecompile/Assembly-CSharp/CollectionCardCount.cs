using System;
using PegasusShared;
using UnityEngine;

// Token: 0x020000F9 RID: 249
public class CollectionCardCount : MonoBehaviour
{
	// Token: 0x06000E6E RID: 3694 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Update()
	{
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Awake()
	{
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x00051100 File Offset: 0x0004F300
	public void SetCount(int normalCount, int goldenCount, int diamondCount, TAG_PREMIUM premium)
	{
		this.m_normalCount = normalCount;
		this.m_goldenCount = goldenCount;
		this.m_diamondCount = diamondCount;
		this.m_premium = premium;
		this.UpdateVisibility();
	}

	// Token: 0x06000E72 RID: 3698 RVA: 0x00051125 File Offset: 0x0004F325
	public int GetCount(TAG_PREMIUM premium)
	{
		switch (premium)
		{
		case TAG_PREMIUM.NORMAL:
			return this.m_normalCount;
		case TAG_PREMIUM.GOLDEN:
			return this.m_goldenCount;
		case TAG_PREMIUM.DIAMOND:
			return this.m_diamondCount;
		default:
			return 0;
		}
	}

	// Token: 0x06000E73 RID: 3699 RVA: 0x00051151 File Offset: 0x0004F351
	public void Show()
	{
		this.UpdateVisibility();
	}

	// Token: 0x06000E74 RID: 3700 RVA: 0x00028167 File Offset: 0x00026367
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x0005115C File Offset: 0x0004F35C
	private void UpdateVisibility()
	{
		int count = this.GetCount(this.m_premium);
		if (count <= 1)
		{
			this.Hide();
			return;
		}
		base.gameObject.SetActive(true);
		this.m_normalTab.SetActive(false);
		this.m_normalBorder.SetActive(false);
		this.m_normalWideBorder.SetActive(false);
		this.m_goldenTab.SetActive(false);
		this.m_goldenBorder.SetActive(false);
		this.m_goldenWideBorder.SetActive(false);
		FormatType themeShowing = CollectionManager.Get().GetThemeShowing(null);
		Color textColor;
		if (new Map<FormatType, Color>
		{
			{
				FormatType.FT_STANDARD,
				this.m_standardTextColor
			},
			{
				FormatType.FT_WILD,
				this.m_wildTextColor
			},
			{
				FormatType.FT_CLASSIC,
				this.m_classicTextColor
			}
		}.TryGetValue(themeShowing, out textColor))
		{
			this.m_normalCountText.TextColor = textColor;
		}
		else
		{
			Debug.LogWarning("CollectionCardCount.UpdateVisibility failed to find text color for format" + themeShowing.ToString());
		}
		Material material;
		if (new Map<FormatType, Material>
		{
			{
				FormatType.FT_STANDARD,
				this.m_standardBorderMaterial
			},
			{
				FormatType.FT_WILD,
				this.m_wildBorderMaterial
			},
			{
				FormatType.FT_CLASSIC,
				this.m_classicBorderMaterial
			}
		}.TryGetValue(themeShowing, out material))
		{
			this.m_normalWideBorder.GetComponent<Renderer>().SetMaterial(material);
			this.m_normalBorder.GetComponent<Renderer>().SetMaterial(material);
		}
		else
		{
			Debug.LogWarning("CollectionCardCount.UpdateVisibility failed to find material for format" + themeShowing.ToString());
		}
		this.m_goldenCountText.TextColor = this.m_standardGoldTextColor;
		this.m_goldenWideBorder.GetComponent<Renderer>().SetMaterial(this.m_standardGoldBorderMaterial);
		this.m_goldenBorder.GetComponent<Renderer>().SetMaterial(this.m_standardGoldBorderMaterial);
		if (count < 10)
		{
			this.m_normalBorder.SetActive(true);
			this.m_normalCountText.Text = GameStrings.Format("GLUE_COLLECTION_CARD_COUNT", new object[]
			{
				count
			});
		}
		else if (count > 0)
		{
			this.m_normalWideBorder.SetActive(true);
			this.m_normalCountText.Text = GameStrings.Get("GLUE_COLLECTION_CARD_COUNT_LARGE");
		}
		if (count > 0)
		{
			this.m_normalTab.SetActive(true);
			this.m_normalTab.transform.position = this.m_centerBone.transform.position;
		}
	}

	// Token: 0x040009EA RID: 2538
	public GameObject m_normalTab;

	// Token: 0x040009EB RID: 2539
	public UberText m_normalCountText;

	// Token: 0x040009EC RID: 2540
	public GameObject m_normalBorder;

	// Token: 0x040009ED RID: 2541
	public GameObject m_normalWideBorder;

	// Token: 0x040009EE RID: 2542
	public GameObject m_normalBone;

	// Token: 0x040009EF RID: 2543
	public GameObject m_goldenTab;

	// Token: 0x040009F0 RID: 2544
	public UberText m_goldenCountText;

	// Token: 0x040009F1 RID: 2545
	public GameObject m_goldenBorder;

	// Token: 0x040009F2 RID: 2546
	public GameObject m_goldenWideBorder;

	// Token: 0x040009F3 RID: 2547
	public GameObject m_goldenBone;

	// Token: 0x040009F4 RID: 2548
	public GameObject m_centerBone;

	// Token: 0x040009F5 RID: 2549
	public Color m_standardTextColor;

	// Token: 0x040009F6 RID: 2550
	public Color m_wildTextColor;

	// Token: 0x040009F7 RID: 2551
	public Color m_classicTextColor;

	// Token: 0x040009F8 RID: 2552
	public Material m_standardBorderMaterial;

	// Token: 0x040009F9 RID: 2553
	public Material m_wildBorderMaterial;

	// Token: 0x040009FA RID: 2554
	public Material m_classicBorderMaterial;

	// Token: 0x040009FB RID: 2555
	public Color m_standardGoldTextColor;

	// Token: 0x040009FC RID: 2556
	public Material m_standardGoldBorderMaterial;

	// Token: 0x040009FD RID: 2557
	private TAG_PREMIUM m_premium;

	// Token: 0x040009FE RID: 2558
	private int m_normalCount;

	// Token: 0x040009FF RID: 2559
	private int m_goldenCount;

	// Token: 0x04000A00 RID: 2560
	private int m_diamondCount;
}
