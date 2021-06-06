using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UberTextMgr : MonoBehaviour
{
	public Font m_BelweFont;

	public Font m_BelweOutlineFont;

	public Font m_FranklinGothicFont;

	public Font m_BlizzardGlobal;

	private bool m_Active;

	private List<Font> m_Fonts;

	private Locale m_Locale;

	private string m_AtlasCharacters;

	private string m_AtlasNumbers = "0123456789.";

	private static UberTextMgr s_Instance;

	private void Awake()
	{
		s_Instance = this;
	}

	private void Start()
	{
		m_AtlasCharacters = BuildCharacterSet();
		Log.UberText.Print("Updating Atlas to include: {0}", m_AtlasCharacters);
	}

	private void Update()
	{
		if (m_Active)
		{
			ForceEnglishCharactersInAtlas();
		}
	}

	public static UberTextMgr Get()
	{
		return s_Instance;
	}

	public void StartAtlasUpdate()
	{
		Log.UberText.Print("UberTextMgr.StartAtlasUpdate()");
		m_BelweFont = GetLocalizedFont(m_BelweFont);
		m_BelweOutlineFont = GetLocalizedFont(m_BelweOutlineFont);
		m_FranklinGothicFont = GetLocalizedFont(m_FranklinGothicFont);
		m_Active = true;
		Font.textureRebuilt += LogFontAtlasUpdate;
	}

	private void ForceEnglishCharactersInAtlas()
	{
		if (m_FranklinGothicFont == null)
		{
			Debug.LogError("UberTextMgr: m_FranklinGothicFont == null");
			return;
		}
		m_FranklinGothicFont.RequestCharactersInTexture(m_AtlasCharacters, 40, FontStyle.Normal);
		m_FranklinGothicFont.RequestCharactersInTexture(m_AtlasCharacters, 40, FontStyle.Italic);
		if (m_BelweOutlineFont == null)
		{
			Debug.LogError("UberTextMgr: m_BelweOutlineFont == null");
			return;
		}
		m_BelweOutlineFont.RequestCharactersInTexture(m_AtlasCharacters, 40, FontStyle.Normal);
		m_BelweOutlineFont.RequestCharactersInTexture(m_AtlasNumbers, 65, FontStyle.Normal);
	}

	private string BuildCharacterSet()
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 33; i < 127; i++)
		{
			stringBuilder.Append((char)i);
		}
		for (int j = 192; j < 256; j++)
		{
			stringBuilder.Append((char)j);
		}
		return stringBuilder.ToString();
	}

	private Font GetLocalizedFont(Font font)
	{
		FontTable fontTable = FontTable.Get();
		if (fontTable == null)
		{
			Debug.LogError("UberTextMgr: Error loading FontTable");
			return null;
		}
		FontDefinition fontDef = fontTable.GetFontDef(font);
		if (fontDef == null)
		{
			Debug.LogError("UberTextMgr: Error loading fontDef for: " + font.name);
			return null;
		}
		return fontDef.m_Font;
	}

	private void LogFontAtlasUpdate(Font font)
	{
		if (font == m_BelweFont)
		{
			LogBelweAtlasUpdate();
		}
		else if (font == m_BelweOutlineFont)
		{
			LogBelweOutlineAtlasUpdate();
		}
		else if (font == m_FranklinGothicFont)
		{
			LogFranklinGothicAtlasUpdate();
		}
		else if (font == m_BlizzardGlobal)
		{
			LogBlizzardGlobalAtlasUpdate();
		}
	}

	private void LogBelweAtlasUpdate()
	{
		int width = m_BelweFont.material.mainTexture.width;
		int height = m_BelweFont.material.mainTexture.height;
		Log.UberText.Print("Belwe Atlas Updated: {0}, {1}", width, height);
	}

	private void LogBelweOutlineAtlasUpdate()
	{
		int width = m_BelweOutlineFont.material.mainTexture.width;
		int height = m_BelweOutlineFont.material.mainTexture.height;
		Log.UberText.Print("BelweOutline Atlas Updated: {0}, {1}", width, height);
	}

	private void LogFranklinGothicAtlasUpdate()
	{
		int width = m_FranklinGothicFont.material.mainTexture.width;
		int height = m_FranklinGothicFont.material.mainTexture.height;
		Log.UberText.Print("Franklin Gothic Atlas Updated: {0}, {1}", width, height);
	}

	private void LogBlizzardGlobalAtlasUpdate()
	{
		int width = m_BlizzardGlobal.material.mainTexture.width;
		int height = m_BlizzardGlobal.material.mainTexture.height;
		Log.UberText.Print("Blizzard Global Atlas Updated: {0}, {1}", width, height);
	}
}
