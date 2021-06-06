using System;

[Serializable]
public class SpecialEventVisualDef
{
	[CustomEditField]
	public SpecialEventType m_EventType;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_Prefab;
}
