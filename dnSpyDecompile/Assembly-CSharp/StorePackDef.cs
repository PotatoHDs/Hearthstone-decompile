using System;
using UnityEngine;

// Token: 0x02000728 RID: 1832
[CustomEditClass]
public class StorePackDef : MonoBehaviour, IStorePackDef
{
	// Token: 0x060066E7 RID: 26343 RVA: 0x00218F0B File Offset: 0x0021710B
	public string GetSelectorButtonPrefab()
	{
		return this.m_buttonPrefab;
	}

	// Token: 0x060066E8 RID: 26344 RVA: 0x00218F13 File Offset: 0x00217113
	public string GetLowPolyPrefab()
	{
		return this.m_lowPolyPrefab;
	}

	// Token: 0x060066E9 RID: 26345 RVA: 0x00218F1B File Offset: 0x0021711B
	public string GetLowPolyDustPrefab()
	{
		return this.m_lowPolyPrefabDust;
	}

	// Token: 0x060066EA RID: 26346 RVA: 0x00218F23 File Offset: 0x00217123
	public string GetLogoTextureName()
	{
		return this.m_logoTextureName;
	}

	// Token: 0x060066EB RID: 26347 RVA: 0x00218F2B File Offset: 0x0021712B
	public string GetLogoTextureGlowName()
	{
		return this.m_logoTextureGlowName;
	}

	// Token: 0x060066EC RID: 26348 RVA: 0x00218F33 File Offset: 0x00217133
	public string GetAccentTextureName()
	{
		return this.m_accentTextureName;
	}

	// Token: 0x060066ED RID: 26349 RVA: 0x00218F3B File Offset: 0x0021713B
	public string GetBackgroundMaterial()
	{
		return this.m_background;
	}

	// Token: 0x060066EE RID: 26350 RVA: 0x000D5239 File Offset: 0x000D3439
	public string GetBackgroundTexture()
	{
		return "";
	}

	// Token: 0x060066EF RID: 26351 RVA: 0x00218F43 File Offset: 0x00217143
	public MusicPlaylistType GetPlaylist()
	{
		return this.m_playlist;
	}

	// Token: 0x060066F0 RID: 26352 RVA: 0x00218F4B File Offset: 0x0021714B
	public MusicPlaylistType GetMiniSetPlaylist()
	{
		return this.m_miniSetPlaylist;
	}

	// Token: 0x060066F1 RID: 26353 RVA: 0x00218F53 File Offset: 0x00217153
	public string GetPreorderAvailableDateString()
	{
		return this.m_preorderAvailableDateString;
	}

	// Token: 0x060066F2 RID: 26354 RVA: 0x00218F5B File Offset: 0x0021715B
	public string GetPreorderDustAvailableDateString()
	{
		return this.m_preorderDustAvailableDateString;
	}

	// Token: 0x060066F3 RID: 26355 RVA: 0x00218F63 File Offset: 0x00217163
	public string GetMiniSetTextureName()
	{
		return this.m_miniSetLogoTextureName;
	}

	// Token: 0x060066F4 RID: 26356 RVA: 0x00218F6B File Offset: 0x0021716B
	public string GetMiniSetAccentTextureName()
	{
		return this.m_miniSetAccentTextureName;
	}

	// Token: 0x040054B6 RID: 21686
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_buttonPrefab;

	// Token: 0x040054B7 RID: 21687
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_lowPolyPrefab;

	// Token: 0x040054B8 RID: 21688
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_lowPolyPrefabDust;

	// Token: 0x040054B9 RID: 21689
	[CustomEditField(T = EditType.TEXTURE)]
	public string m_logoTextureName;

	// Token: 0x040054BA RID: 21690
	[CustomEditField(T = EditType.TEXTURE)]
	public string m_logoTextureGlowName;

	// Token: 0x040054BB RID: 21691
	[CustomEditField(T = EditType.TEXTURE)]
	public string m_accentTextureName;

	// Token: 0x040054BC RID: 21692
	[CustomEditField(T = EditType.TEXTURE)]
	public string m_miniSetLogoTextureName;

	// Token: 0x040054BD RID: 21693
	[CustomEditField(T = EditType.TEXTURE)]
	public string m_miniSetAccentTextureName;

	// Token: 0x040054BE RID: 21694
	[CustomEditField(T = EditType.MATERIAL)]
	public string m_background;

	// Token: 0x040054BF RID: 21695
	public MusicPlaylistType m_playlist;

	// Token: 0x040054C0 RID: 21696
	public MusicPlaylistType m_miniSetPlaylist;

	// Token: 0x040054C1 RID: 21697
	public string m_preorderAvailableDateString;

	// Token: 0x040054C2 RID: 21698
	public string m_preorderDustAvailableDateString;
}
