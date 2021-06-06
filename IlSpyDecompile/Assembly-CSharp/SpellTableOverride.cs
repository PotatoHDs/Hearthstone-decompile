using System;

[Serializable]
public class SpellTableOverride
{
	public SpellType m_Type;

	[CustomEditField(T = EditType.SPELL)]
	public string m_SpellPrefabName = "";
}
