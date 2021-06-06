using System;

// Token: 0x020006D6 RID: 1750
public class ArmorSpell : Spell
{
	// Token: 0x060061F1 RID: 25073 RVA: 0x001FFBB8 File Offset: 0x001FDDB8
	public int GetArmor()
	{
		return this.m_armor;
	}

	// Token: 0x060061F2 RID: 25074 RVA: 0x001FFBC0 File Offset: 0x001FDDC0
	public void SetArmor(int armor)
	{
		this.m_armor = armor;
		this.UpdateArmorText();
	}

	// Token: 0x060061F3 RID: 25075 RVA: 0x001FFBD0 File Offset: 0x001FDDD0
	private void UpdateArmorText()
	{
		if (this.m_ArmorText == null)
		{
			return;
		}
		string text = this.m_armor.ToString();
		if (this.m_armor == 0)
		{
			text = "";
		}
		this.m_ArmorText.Text = text;
	}

	// Token: 0x0400517F RID: 20863
	public UberText m_ArmorText;

	// Token: 0x04005180 RID: 20864
	private int m_armor;
}
