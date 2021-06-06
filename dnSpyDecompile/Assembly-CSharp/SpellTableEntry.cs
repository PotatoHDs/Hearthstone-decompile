using System;

// Token: 0x02000978 RID: 2424
[Serializable]
public class SpellTableEntry
{
	// Token: 0x04006FEF RID: 28655
	public SpellType m_Type;

	// Token: 0x04006FF0 RID: 28656
	[CustomEditField(Hide = true)]
	public Spell m_Spell;

	// Token: 0x04006FF1 RID: 28657
	[CustomEditField(T = EditType.SPELL)]
	public string m_SpellPrefabName = "";
}
