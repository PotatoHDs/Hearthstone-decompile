using System;

// Token: 0x02000709 RID: 1801
public interface IStorePackDef
{
	// Token: 0x060064C8 RID: 25800
	string GetSelectorButtonPrefab();

	// Token: 0x060064C9 RID: 25801
	string GetLowPolyPrefab();

	// Token: 0x060064CA RID: 25802
	string GetLowPolyDustPrefab();

	// Token: 0x060064CB RID: 25803
	string GetLogoTextureName();

	// Token: 0x060064CC RID: 25804
	string GetLogoTextureGlowName();

	// Token: 0x060064CD RID: 25805
	string GetAccentTextureName();

	// Token: 0x060064CE RID: 25806
	string GetBackgroundMaterial();

	// Token: 0x060064CF RID: 25807
	string GetBackgroundTexture();

	// Token: 0x060064D0 RID: 25808
	MusicPlaylistType GetPlaylist();

	// Token: 0x060064D1 RID: 25809
	string GetPreorderAvailableDateString();

	// Token: 0x060064D2 RID: 25810
	string GetPreorderDustAvailableDateString();
}
