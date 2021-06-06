using System;

// Token: 0x020000CE RID: 206
[Serializable]
public class BoxSpecialEvent
{
	// Token: 0x040008A5 RID: 2213
	public SpecialEventType EventType;

	// Token: 0x040008A6 RID: 2214
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string Prefab;

	// Token: 0x040008A7 RID: 2215
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string PrefabPhoneOverride;

	// Token: 0x040008A8 RID: 2216
	[CustomEditField(T = EditType.TEXTURE)]
	public string BoxTexture;

	// Token: 0x040008A9 RID: 2217
	[CustomEditField(T = EditType.TEXTURE)]
	public string BoxTexturePhone;

	// Token: 0x040008AA RID: 2218
	[CustomEditField(T = EditType.TEXTURE)]
	public string TableTexture;

	// Token: 0x040008AB RID: 2219
	public bool showToReturningPlayers;

	// Token: 0x040008AC RID: 2220
	public bool showToNewPlayers;
}
