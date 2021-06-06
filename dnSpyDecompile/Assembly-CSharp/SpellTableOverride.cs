using System;

// Token: 0x0200086E RID: 2158
[Serializable]
public class SpellTableOverride
{
	// Token: 0x04005CBC RID: 23740
	public SpellType m_Type;

	// Token: 0x04005CBD RID: 23741
	[CustomEditField(T = EditType.SPELL)]
	public string m_SpellPrefabName = "";
}
