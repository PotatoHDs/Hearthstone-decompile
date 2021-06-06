using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B15 RID: 2837
public class GameModeIcon : PegUIElement
{
	// Token: 0x060096D7 RID: 38615 RVA: 0x00036768 File Offset: 0x00034968
	public void Show(bool show)
	{
		base.gameObject.SetActive(show);
	}

	// Token: 0x060096D8 RID: 38616 RVA: 0x0030CE0D File Offset: 0x0030B00D
	public void SetText(string text)
	{
		if (this.m_text == null)
		{
			return;
		}
		this.m_text.Text = text;
	}

	// Token: 0x060096D9 RID: 38617 RVA: 0x0030CE2C File Offset: 0x0030B02C
	public void ShowXMarks(uint numberOfMarks)
	{
		if (this.m_Xmarks.Count == 0)
		{
			return;
		}
		int num = 0;
		while ((long)num < (long)((ulong)numberOfMarks))
		{
			this.m_Xmarks[num].SetActive(true);
			num++;
		}
	}

	// Token: 0x060096DA RID: 38618 RVA: 0x0030CE67 File Offset: 0x0030B067
	public void ShowFriendlyChallengeBanner(bool showBanner)
	{
		if (this.m_friendlyChallengeBanner == null)
		{
			return;
		}
		this.m_friendlyChallengeBanner.SetActive(showBanner);
	}

	// Token: 0x060096DB RID: 38619 RVA: 0x0030CE84 File Offset: 0x0030B084
	public void ShowWildVines(bool showVines)
	{
		if (this.m_wildVines == null)
		{
			return;
		}
		this.m_wildVines.SetActive(showVines);
	}

	// Token: 0x04007E55 RID: 32341
	public UberText m_text;

	// Token: 0x04007E56 RID: 32342
	public List<GameObject> m_Xmarks = new List<GameObject>();

	// Token: 0x04007E57 RID: 32343
	public GameObject m_friendlyChallengeBanner;

	// Token: 0x04007E58 RID: 32344
	public GameObject m_wildVines;
}
