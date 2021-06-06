using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class StoreAdventureDef : MonoBehaviour
{
	[CustomEditField(T = EditType.TEXTURE, Label = "Logo Main Texture")]
	public string m_logoTextureName;

	[CustomEditField(T = EditType.TEXTURE, Label = "Logo Shadow Texture")]
	public string m_logoShadowTextureName;

	[CustomEditField(T = EditType.TEXTURE, Label = "Logo Glow Texture")]
	public string m_logoTextureGlowName;

	[CustomEditField(T = EditType.TEXTURE, Label = "Accent Texture")]
	public string m_accentTextureName;

	[CustomEditField(Label = "Music Playlist")]
	public MusicPlaylistType m_playlist;

	[CustomEditField(Label = "Preview Cards")]
	public List<string> m_previewCards = new List<string>();

	[CustomEditField(T = EditType.GAME_OBJECT, Sections = "Deprecated (Might be removed eventually.)")]
	public string m_storeButtonPrefab;

	[CustomEditField(Sections = "Deprecated (Might be removed eventually.)")]
	public Material m_keyArt;

	[CustomEditField(Sections = "Deprecated (Might be removed eventually.)")]
	public int m_preorderCardBackId;

	[CustomEditField(Sections = "Deprecated (Might be removed eventually.)")]
	public string m_preorderCardBackTextName;

	public string GetLogoTextureName()
	{
		return m_logoTextureName;
	}

	public string GetLogoShadowTextureName()
	{
		return m_logoShadowTextureName;
	}

	public string GetAccentTextureName()
	{
		return m_accentTextureName;
	}

	public MusicPlaylistType GetPlaylist()
	{
		return m_playlist;
	}
}
