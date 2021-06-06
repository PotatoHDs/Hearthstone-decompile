using System;
using UnityEngine;

// Token: 0x0200011A RID: 282
public class CraftCardCountTab : MonoBehaviour
{
	// Token: 0x06001294 RID: 4756 RVA: 0x00069C98 File Offset: 0x00067E98
	public void UpdateText(int numCopies, TAG_PREMIUM premium)
	{
		if (premium == TAG_PREMIUM.DIAMOND)
		{
			base.gameObject.SetActive(false);
		}
		if (numCopies > 9)
		{
			this.m_count.Text = "9";
			this.m_plus.gameObject.SetActive(true);
			return;
		}
		if (numCopies >= 2)
		{
			this.m_shadow.SetActive(true);
			this.m_shadow.GetComponent<Animation>().Play("Crafting2ndCardShadow");
		}
		else
		{
			this.m_shadow.SetActive(false);
		}
		this.m_count.TextColor = this.m_normalColor;
		this.m_plus.TextColor = this.m_normalColor;
		this.m_x.TextColor = this.m_normalColor;
		this.m_countTab.SetMaterial(this.m_normalMaterial);
		this.m_count.Text = numCopies.ToString();
		this.m_plus.gameObject.SetActive(false);
	}

	// Token: 0x04000BE0 RID: 3040
	public UberText m_count;

	// Token: 0x04000BE1 RID: 3041
	public UberText m_x;

	// Token: 0x04000BE2 RID: 3042
	public UberText m_plus;

	// Token: 0x04000BE3 RID: 3043
	public GameObject m_shadow;

	// Token: 0x04000BE4 RID: 3044
	public Color m_normalColor;

	// Token: 0x04000BE5 RID: 3045
	public Color m_goldenColor;

	// Token: 0x04000BE6 RID: 3046
	public Material m_normalMaterial;

	// Token: 0x04000BE7 RID: 3047
	public Material m_goldenMaterial;

	// Token: 0x04000BE8 RID: 3048
	public MeshRenderer m_countTab;
}
