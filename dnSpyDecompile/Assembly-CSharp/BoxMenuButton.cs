using System;

// Token: 0x020000CD RID: 205
public class BoxMenuButton : PegUIElement
{
	// Token: 0x06000C6E RID: 3182 RVA: 0x000491F1 File Offset: 0x000473F1
	public string GetText()
	{
		return this.m_TextMesh.Text;
	}

	// Token: 0x06000C6F RID: 3183 RVA: 0x000491FE File Offset: 0x000473FE
	public void SetText(string text)
	{
		this.m_TextMesh.Text = text;
	}

	// Token: 0x06000C70 RID: 3184 RVA: 0x0004920C File Offset: 0x0004740C
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		if (this.m_Spell == null)
		{
			return;
		}
		this.m_Spell.ActivateState(SpellStateType.BIRTH);
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x00049229 File Offset: 0x00047429
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		if (this.m_Spell == null)
		{
			return;
		}
		this.m_Spell.ActivateState(SpellStateType.DEATH);
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x00049246 File Offset: 0x00047446
	protected override void OnPress()
	{
		if (this.m_Spell == null)
		{
			return;
		}
		if (DialogManager.Get().ShowingDialog())
		{
			return;
		}
		this.m_Spell.ActivateState(SpellStateType.IDLE);
	}

	// Token: 0x06000C73 RID: 3187 RVA: 0x00049270 File Offset: 0x00047470
	protected override void OnRelease()
	{
		if (this.m_Spell == null)
		{
			return;
		}
		this.m_Spell.ActivateState(SpellStateType.ACTION);
	}

	// Token: 0x040008A2 RID: 2210
	public UberText m_TextMesh;

	// Token: 0x040008A3 RID: 2211
	public Spell m_Spell;

	// Token: 0x040008A4 RID: 2212
	public HighlightState m_HighlightState;
}
