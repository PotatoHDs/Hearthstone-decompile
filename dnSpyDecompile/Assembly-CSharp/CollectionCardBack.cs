using System;
using UnityEngine;

// Token: 0x020000F8 RID: 248
public class CollectionCardBack : MonoBehaviour
{
	// Token: 0x06000E66 RID: 3686 RVA: 0x00051074 File Offset: 0x0004F274
	public void Awake()
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_nameFrame.SetActive(false);
		}
	}

	// Token: 0x06000E67 RID: 3687 RVA: 0x0005108E File Offset: 0x0004F28E
	public void SetCardBackId(int id)
	{
		this.m_cardBackId = id;
	}

	// Token: 0x06000E68 RID: 3688 RVA: 0x00051097 File Offset: 0x0004F297
	public int GetCardBackId()
	{
		return this.m_cardBackId;
	}

	// Token: 0x06000E69 RID: 3689 RVA: 0x0005109F File Offset: 0x0004F29F
	public void SetSeasonId(int seasonId)
	{
		this.m_seasonId = seasonId;
	}

	// Token: 0x06000E6A RID: 3690 RVA: 0x000510A8 File Offset: 0x0004F2A8
	public int GetSeasonId()
	{
		return this.m_seasonId;
	}

	// Token: 0x06000E6B RID: 3691 RVA: 0x000510B0 File Offset: 0x0004F2B0
	public void SetCardBackName(string name)
	{
		if (this.m_name == null)
		{
			return;
		}
		this.m_name.Text = name;
	}

	// Token: 0x06000E6C RID: 3692 RVA: 0x000510CD File Offset: 0x0004F2CD
	public void ShowFavoriteBanner(bool show)
	{
		if (this.m_favoriteBanner == null)
		{
			return;
		}
		this.m_favoriteBanner.SetActive(show);
	}

	// Token: 0x040009E5 RID: 2533
	public UberText m_name;

	// Token: 0x040009E6 RID: 2534
	public GameObject m_favoriteBanner;

	// Token: 0x040009E7 RID: 2535
	public GameObject m_nameFrame;

	// Token: 0x040009E8 RID: 2536
	private int m_cardBackId = -1;

	// Token: 0x040009E9 RID: 2537
	private int m_seasonId = -1;
}
