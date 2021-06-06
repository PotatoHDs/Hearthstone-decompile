using System;
using UnityEngine;

// Token: 0x02000313 RID: 787
public class HeroChoiceActor : Actor
{
	// Token: 0x06002C48 RID: 11336 RVA: 0x000DE80F File Offset: 0x000DCA0F
	public void SetNameText(string text)
	{
		if (this.m_nameText != null)
		{
			this.m_nameText.Text = text;
		}
	}

	// Token: 0x06002C49 RID: 11337 RVA: 0x000DE82B File Offset: 0x000DCA2B
	public void SetNameTextActive(bool active)
	{
		if (this.m_nameText != null)
		{
			this.m_nameText.gameObject.SetActive(active);
		}
	}

	// Token: 0x06002C4A RID: 11338 RVA: 0x000DE84C File Offset: 0x000DCA4C
	protected override void ShowImpl(bool ignoreSpells)
	{
		base.ShowImpl(ignoreSpells);
		if (this.m_nameTextMesh != null)
		{
			this.m_nameTextMesh.gameObject.SetActive(false);
			if (this.m_nameTextMesh.RenderOnObject)
			{
				this.m_nameTextMesh.RenderOnObject.GetComponent<Renderer>().enabled = false;
			}
		}
	}

	// Token: 0x0400185D RID: 6237
	public UberText m_nameText;
}
