using System;
using UnityEngine;

// Token: 0x02000029 RID: 41
[CustomEditClass]
public class AdventureBossDef : MonoBehaviour
{
	// Token: 0x06000164 RID: 356 RVA: 0x0000872F File Offset: 0x0000692F
	public virtual string GetIntroLine()
	{
		return this.m_IntroLine;
	}

	// Token: 0x040000EE RID: 238
	[CustomEditField(Sections = "Intro Line")]
	public string m_IntroLine;

	// Token: 0x040000EF RID: 239
	[CustomEditField(Sections = "Intro Line")]
	public AdventureBossDef.IntroLinePlayTime m_IntroLinePlayTime;

	// Token: 0x040000F0 RID: 240
	[CustomEditField(T = EditType.GAME_OBJECT, Sections = "General")]
	public string m_quotePrefabOverride;

	// Token: 0x040000F1 RID: 241
	[CustomEditField(Sections = "General")]
	public MusicPlaylistType m_MissionMusic;

	// Token: 0x040000F2 RID: 242
	public MaterialReference m_CoinPortraitMaterial;

	// Token: 0x0200128D RID: 4749
	public enum IntroLinePlayTime
	{
		// Token: 0x0400A3C5 RID: 41925
		MissionSelect,
		// Token: 0x0400A3C6 RID: 41926
		MissionStart
	}
}
