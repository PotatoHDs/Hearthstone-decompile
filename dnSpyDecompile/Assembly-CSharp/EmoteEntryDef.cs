using System;

// Token: 0x0200089F RID: 2207
[Serializable]
public class EmoteEntryDef
{
	// Token: 0x04005E9C RID: 24220
	public EmoteType m_emoteType;

	// Token: 0x04005E9D RID: 24221
	[CustomEditField(T = EditType.SPELL)]
	public string m_emoteSpellPath;

	// Token: 0x04005E9E RID: 24222
	[CustomEditField(T = EditType.CARD_SOUND_SPELL)]
	public string m_emoteSoundSpellPath;

	// Token: 0x04005E9F RID: 24223
	public string m_emoteGameStringKey;
}
