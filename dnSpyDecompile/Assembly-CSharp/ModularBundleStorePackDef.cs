using System;

// Token: 0x02000712 RID: 1810
public class ModularBundleStorePackDef : IStorePackDef
{
	// Token: 0x06006511 RID: 25873 RVA: 0x0020FB30 File Offset: 0x0020DD30
	public ModularBundleStorePackDef(ModularBundleDbfRecord modularBundleRecord)
	{
		this.m_modularBundleRecord = modularBundleRecord;
	}

	// Token: 0x06006512 RID: 25874 RVA: 0x0020FB3F File Offset: 0x0020DD3F
	public string GetSelectorButtonPrefab()
	{
		return this.m_modularBundleRecord.SelectorPrefab;
	}

	// Token: 0x06006513 RID: 25875 RVA: 0x000D5239 File Offset: 0x000D3439
	public string GetLowPolyPrefab()
	{
		return "";
	}

	// Token: 0x06006514 RID: 25876 RVA: 0x000D5239 File Offset: 0x000D3439
	public string GetLowPolyDustPrefab()
	{
		return "";
	}

	// Token: 0x06006515 RID: 25877 RVA: 0x0020FB4C File Offset: 0x0020DD4C
	public string GetLogoTextureName()
	{
		return this.m_modularBundleRecord.LogoTexture;
	}

	// Token: 0x06006516 RID: 25878 RVA: 0x0020FB59 File Offset: 0x0020DD59
	public string GetLogoTextureGlowName()
	{
		return this.m_modularBundleRecord.LogoTextureGlow;
	}

	// Token: 0x06006517 RID: 25879 RVA: 0x000D5239 File Offset: 0x000D3439
	public string GetAccentTextureName()
	{
		return "";
	}

	// Token: 0x06006518 RID: 25880 RVA: 0x000D5239 File Offset: 0x000D3439
	public string GetBackgroundMaterial()
	{
		return "";
	}

	// Token: 0x06006519 RID: 25881 RVA: 0x0020FB66 File Offset: 0x0020DD66
	public string GetBackgroundTexture()
	{
		return this.m_modularBundleRecord.Background;
	}

	// Token: 0x0600651A RID: 25882 RVA: 0x0020FB74 File Offset: 0x0020DD74
	public MusicPlaylistType GetPlaylist()
	{
		object obj = Enum.Parse(typeof(MusicPlaylistType), this.m_modularBundleRecord.Playlist, true);
		if (obj == null)
		{
			return MusicPlaylistType.Invalid;
		}
		return (MusicPlaylistType)obj;
	}

	// Token: 0x0600651B RID: 25883 RVA: 0x000D5239 File Offset: 0x000D3439
	public string GetPreorderAvailableDateString()
	{
		return "";
	}

	// Token: 0x0600651C RID: 25884 RVA: 0x000D5239 File Offset: 0x000D3439
	public string GetPreorderDustAvailableDateString()
	{
		return "";
	}

	// Token: 0x040053E2 RID: 21474
	private ModularBundleDbfRecord m_modularBundleRecord;
}
