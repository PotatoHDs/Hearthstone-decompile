using System;
using System.Collections.Generic;

// Token: 0x0200094F RID: 2383
[Serializable]
public class SoundPlaybackLimitDef
{
	// Token: 0x04006E5F RID: 28255
	[CustomEditField(Label = "Playback Limit")]
	public int m_Limit = 1;

	// Token: 0x04006E60 RID: 28256
	[CustomEditField(Label = "Clip Group")]
	public List<SoundPlaybackLimitClipDef> m_ClipDefs = new List<SoundPlaybackLimitClipDef>();
}
