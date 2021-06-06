using System;
using System.Collections.Generic;

[Serializable]
public class CardEffectDef
{
	[CustomEditField(T = EditType.SPELL)]
	public string m_SpellPath;

	[CustomEditField(T = EditType.CARD_SOUND_SPELL)]
	public List<string> m_SoundSpellPaths = new List<string>();
}
