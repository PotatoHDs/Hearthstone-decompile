using System;

// Token: 0x02000876 RID: 2166
public struct CharacterDialog
{
	// Token: 0x04005D2E RID: 23854
	public int dbfRecordId;

	// Token: 0x04005D2F RID: 23855
	public int playOrder;

	// Token: 0x04005D30 RID: 23856
	public bool useInnkeeperQuote;

	// Token: 0x04005D31 RID: 23857
	public string prefabName;

	// Token: 0x04005D32 RID: 23858
	public string audioName;

	// Token: 0x04005D33 RID: 23859
	public bool useAltSpeechBubble;

	// Token: 0x04005D34 RID: 23860
	public float waitBefore;

	// Token: 0x04005D35 RID: 23861
	public float waitAfter;

	// Token: 0x04005D36 RID: 23862
	public bool persistPrefab;

	// Token: 0x04005D37 RID: 23863
	public bool useAltPosition;

	// Token: 0x04005D38 RID: 23864
	public float minimumDurationSeconds;

	// Token: 0x04005D39 RID: 23865
	public float localeExtraSeconds;

	// Token: 0x04005D3A RID: 23866
	public DbfLocValue bubbleText;
}
