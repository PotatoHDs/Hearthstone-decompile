using System;

[Serializable]
public class EmoteEntryDef
{
	public EmoteType m_emoteType;

	[CustomEditField(T = EditType.SPELL)]
	public string m_emoteSpellPath;

	[CustomEditField(T = EditType.CARD_SOUND_SPELL)]
	public string m_emoteSoundSpellPath;

	public string m_emoteGameStringKey;
}
