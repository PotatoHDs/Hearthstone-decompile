using System;

// Token: 0x02000950 RID: 2384
[Serializable]
public class SoundPlaybackLimitClipDef
{
	// Token: 0x1700077D RID: 1917
	// (get) Token: 0x0600832B RID: 33579 RVA: 0x002A8504 File Offset: 0x002A6704
	public string LegacyName
	{
		get
		{
			if (this.m_legacyName == null)
			{
				this.m_legacyName = new AssetReference(this.m_Path).GetLegacyAssetName();
			}
			return this.m_legacyName;
		}
	}

	// Token: 0x04006E61 RID: 28257
	[CustomEditField(Label = "Clip", T = EditType.AUDIO_CLIP)]
	public string m_Path;

	// Token: 0x04006E62 RID: 28258
	public int m_Priority;

	// Token: 0x04006E63 RID: 28259
	[CustomEditField(Range = "0.0-1.0")]
	public float m_ExclusivePlaybackThreshold = 0.1f;

	// Token: 0x04006E64 RID: 28260
	[NonSerialized]
	private string m_legacyName;
}
