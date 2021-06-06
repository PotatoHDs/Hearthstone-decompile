using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200071F RID: 1823
[CustomEditClass]
public class StoreAdventureDef : MonoBehaviour
{
	// Token: 0x060065A8 RID: 26024 RVA: 0x00211581 File Offset: 0x0020F781
	public string GetLogoTextureName()
	{
		return this.m_logoTextureName;
	}

	// Token: 0x060065A9 RID: 26025 RVA: 0x00211589 File Offset: 0x0020F789
	public string GetLogoShadowTextureName()
	{
		return this.m_logoShadowTextureName;
	}

	// Token: 0x060065AA RID: 26026 RVA: 0x00211591 File Offset: 0x0020F791
	public string GetAccentTextureName()
	{
		return this.m_accentTextureName;
	}

	// Token: 0x060065AB RID: 26027 RVA: 0x00211599 File Offset: 0x0020F799
	public MusicPlaylistType GetPlaylist()
	{
		return this.m_playlist;
	}

	// Token: 0x0400544B RID: 21579
	[CustomEditField(T = EditType.TEXTURE, Label = "Logo Main Texture")]
	public string m_logoTextureName;

	// Token: 0x0400544C RID: 21580
	[CustomEditField(T = EditType.TEXTURE, Label = "Logo Shadow Texture")]
	public string m_logoShadowTextureName;

	// Token: 0x0400544D RID: 21581
	[CustomEditField(T = EditType.TEXTURE, Label = "Logo Glow Texture")]
	public string m_logoTextureGlowName;

	// Token: 0x0400544E RID: 21582
	[CustomEditField(T = EditType.TEXTURE, Label = "Accent Texture")]
	public string m_accentTextureName;

	// Token: 0x0400544F RID: 21583
	[CustomEditField(Label = "Music Playlist")]
	public MusicPlaylistType m_playlist;

	// Token: 0x04005450 RID: 21584
	[CustomEditField(Label = "Preview Cards")]
	public List<string> m_previewCards = new List<string>();

	// Token: 0x04005451 RID: 21585
	[CustomEditField(T = EditType.GAME_OBJECT, Sections = "Deprecated (Might be removed eventually.)")]
	public string m_storeButtonPrefab;

	// Token: 0x04005452 RID: 21586
	[CustomEditField(Sections = "Deprecated (Might be removed eventually.)")]
	public Material m_keyArt;

	// Token: 0x04005453 RID: 21587
	[CustomEditField(Sections = "Deprecated (Might be removed eventually.)")]
	public int m_preorderCardBackId;

	// Token: 0x04005454 RID: 21588
	[CustomEditField(Sections = "Deprecated (Might be removed eventually.)")]
	public string m_preorderCardBackTextName;
}
