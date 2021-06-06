using UnityEngine;

[CustomEditClass]
public class StorePackDef : MonoBehaviour, IStorePackDef
{
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_buttonPrefab;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_lowPolyPrefab;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_lowPolyPrefabDust;

	[CustomEditField(T = EditType.TEXTURE)]
	public string m_logoTextureName;

	[CustomEditField(T = EditType.TEXTURE)]
	public string m_logoTextureGlowName;

	[CustomEditField(T = EditType.TEXTURE)]
	public string m_accentTextureName;

	[CustomEditField(T = EditType.TEXTURE)]
	public string m_miniSetLogoTextureName;

	[CustomEditField(T = EditType.TEXTURE)]
	public string m_miniSetAccentTextureName;

	[CustomEditField(T = EditType.MATERIAL)]
	public string m_background;

	public MusicPlaylistType m_playlist;

	public MusicPlaylistType m_miniSetPlaylist;

	public string m_preorderAvailableDateString;

	public string m_preorderDustAvailableDateString;

	public string GetSelectorButtonPrefab()
	{
		return m_buttonPrefab;
	}

	public string GetLowPolyPrefab()
	{
		return m_lowPolyPrefab;
	}

	public string GetLowPolyDustPrefab()
	{
		return m_lowPolyPrefabDust;
	}

	public string GetLogoTextureName()
	{
		return m_logoTextureName;
	}

	public string GetLogoTextureGlowName()
	{
		return m_logoTextureGlowName;
	}

	public string GetAccentTextureName()
	{
		return m_accentTextureName;
	}

	public string GetBackgroundMaterial()
	{
		return m_background;
	}

	public string GetBackgroundTexture()
	{
		return "";
	}

	public MusicPlaylistType GetPlaylist()
	{
		return m_playlist;
	}

	public MusicPlaylistType GetMiniSetPlaylist()
	{
		return m_miniSetPlaylist;
	}

	public string GetPreorderAvailableDateString()
	{
		return m_preorderAvailableDateString;
	}

	public string GetPreorderDustAvailableDateString()
	{
		return m_preorderDustAvailableDateString;
	}

	public string GetMiniSetTextureName()
	{
		return m_miniSetLogoTextureName;
	}

	public string GetMiniSetAccentTextureName()
	{
		return m_miniSetAccentTextureName;
	}
}
