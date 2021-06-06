using System;

// Token: 0x02000961 RID: 2401
[Serializable]
public class SpecialEventVisualDef
{
	// Token: 0x04006F22 RID: 28450
	[CustomEditField]
	public SpecialEventType m_EventType;

	// Token: 0x04006F23 RID: 28451
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_Prefab;
}
