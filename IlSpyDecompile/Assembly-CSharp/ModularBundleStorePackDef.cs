using System;

public class ModularBundleStorePackDef : IStorePackDef
{
	private ModularBundleDbfRecord m_modularBundleRecord;

	public ModularBundleStorePackDef(ModularBundleDbfRecord modularBundleRecord)
	{
		m_modularBundleRecord = modularBundleRecord;
	}

	public string GetSelectorButtonPrefab()
	{
		return m_modularBundleRecord.SelectorPrefab;
	}

	public string GetLowPolyPrefab()
	{
		return "";
	}

	public string GetLowPolyDustPrefab()
	{
		return "";
	}

	public string GetLogoTextureName()
	{
		return m_modularBundleRecord.LogoTexture;
	}

	public string GetLogoTextureGlowName()
	{
		return m_modularBundleRecord.LogoTextureGlow;
	}

	public string GetAccentTextureName()
	{
		return "";
	}

	public string GetBackgroundMaterial()
	{
		return "";
	}

	public string GetBackgroundTexture()
	{
		return m_modularBundleRecord.Background;
	}

	public MusicPlaylistType GetPlaylist()
	{
		object obj = Enum.Parse(typeof(MusicPlaylistType), m_modularBundleRecord.Playlist, ignoreCase: true);
		if (obj == null)
		{
			return MusicPlaylistType.Invalid;
		}
		return (MusicPlaylistType)obj;
	}

	public string GetPreorderAvailableDateString()
	{
		return "";
	}

	public string GetPreorderDustAvailableDateString()
	{
		return "";
	}
}
