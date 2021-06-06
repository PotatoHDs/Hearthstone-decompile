using System;
using System.Collections.Generic;

// Token: 0x02000872 RID: 2162
[Serializable]
public class CardEffectDef
{
	// Token: 0x04005D22 RID: 23842
	[CustomEditField(T = EditType.SPELL)]
	public string m_SpellPath;

	// Token: 0x04005D23 RID: 23843
	[CustomEditField(T = EditType.CARD_SOUND_SPELL)]
	public List<string> m_SoundSpellPaths = new List<string>();
}
