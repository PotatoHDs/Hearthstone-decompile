using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000AC0 RID: 2752
public class UberTextMgr : MonoBehaviour
{
	// Token: 0x0600930E RID: 37646 RVA: 0x002FB088 File Offset: 0x002F9288
	private void Awake()
	{
		UberTextMgr.s_Instance = this;
	}

	// Token: 0x0600930F RID: 37647 RVA: 0x002FB090 File Offset: 0x002F9290
	private void Start()
	{
		this.m_AtlasCharacters = this.BuildCharacterSet();
		Log.UberText.Print("Updating Atlas to include: {0}", new object[]
		{
			this.m_AtlasCharacters
		});
	}

	// Token: 0x06009310 RID: 37648 RVA: 0x002FB0BC File Offset: 0x002F92BC
	private void Update()
	{
		if (!this.m_Active)
		{
			return;
		}
		this.ForceEnglishCharactersInAtlas();
	}

	// Token: 0x06009311 RID: 37649 RVA: 0x002FB0CD File Offset: 0x002F92CD
	public static UberTextMgr Get()
	{
		return UberTextMgr.s_Instance;
	}

	// Token: 0x06009312 RID: 37650 RVA: 0x002FB0D4 File Offset: 0x002F92D4
	public void StartAtlasUpdate()
	{
		Log.UberText.Print("UberTextMgr.StartAtlasUpdate()", Array.Empty<object>());
		this.m_BelweFont = this.GetLocalizedFont(this.m_BelweFont);
		this.m_BelweOutlineFont = this.GetLocalizedFont(this.m_BelweOutlineFont);
		this.m_FranklinGothicFont = this.GetLocalizedFont(this.m_FranklinGothicFont);
		this.m_Active = true;
		Font.textureRebuilt += this.LogFontAtlasUpdate;
	}

	// Token: 0x06009313 RID: 37651 RVA: 0x002FB144 File Offset: 0x002F9344
	private void ForceEnglishCharactersInAtlas()
	{
		if (this.m_FranklinGothicFont == null)
		{
			Debug.LogError("UberTextMgr: m_FranklinGothicFont == null");
			return;
		}
		this.m_FranklinGothicFont.RequestCharactersInTexture(this.m_AtlasCharacters, 40, FontStyle.Normal);
		this.m_FranklinGothicFont.RequestCharactersInTexture(this.m_AtlasCharacters, 40, FontStyle.Italic);
		if (this.m_BelweOutlineFont == null)
		{
			Debug.LogError("UberTextMgr: m_BelweOutlineFont == null");
			return;
		}
		this.m_BelweOutlineFont.RequestCharactersInTexture(this.m_AtlasCharacters, 40, FontStyle.Normal);
		this.m_BelweOutlineFont.RequestCharactersInTexture(this.m_AtlasNumbers, 65, FontStyle.Normal);
	}

	// Token: 0x06009314 RID: 37652 RVA: 0x002FB1D4 File Offset: 0x002F93D4
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

	// Token: 0x06009315 RID: 37653 RVA: 0x002FB224 File Offset: 0x002F9424
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

	// Token: 0x06009316 RID: 37654 RVA: 0x002FB274 File Offset: 0x002F9474
	private void LogFontAtlasUpdate(Font font)
	{
		if (font == this.m_BelweFont)
		{
			this.LogBelweAtlasUpdate();
			return;
		}
		if (font == this.m_BelweOutlineFont)
		{
			this.LogBelweOutlineAtlasUpdate();
			return;
		}
		if (font == this.m_FranklinGothicFont)
		{
			this.LogFranklinGothicAtlasUpdate();
			return;
		}
		if (font == this.m_BlizzardGlobal)
		{
			this.LogBlizzardGlobalAtlasUpdate();
		}
	}

	// Token: 0x06009317 RID: 37655 RVA: 0x002FB2D4 File Offset: 0x002F94D4
	private void LogBelweAtlasUpdate()
	{
		int width = this.m_BelweFont.material.mainTexture.width;
		int height = this.m_BelweFont.material.mainTexture.height;
		Log.UberText.Print("Belwe Atlas Updated: {0}, {1}", new object[]
		{
			width,
			height
		});
	}

	// Token: 0x06009318 RID: 37656 RVA: 0x002FB334 File Offset: 0x002F9534
	private void LogBelweOutlineAtlasUpdate()
	{
		int width = this.m_BelweOutlineFont.material.mainTexture.width;
		int height = this.m_BelweOutlineFont.material.mainTexture.height;
		Log.UberText.Print("BelweOutline Atlas Updated: {0}, {1}", new object[]
		{
			width,
			height
		});
	}

	// Token: 0x06009319 RID: 37657 RVA: 0x002FB394 File Offset: 0x002F9594
	private void LogFranklinGothicAtlasUpdate()
	{
		int width = this.m_FranklinGothicFont.material.mainTexture.width;
		int height = this.m_FranklinGothicFont.material.mainTexture.height;
		Log.UberText.Print("Franklin Gothic Atlas Updated: {0}, {1}", new object[]
		{
			width,
			height
		});
	}

	// Token: 0x0600931A RID: 37658 RVA: 0x002FB3F4 File Offset: 0x002F95F4
	private void LogBlizzardGlobalAtlasUpdate()
	{
		int width = this.m_BlizzardGlobal.material.mainTexture.width;
		int height = this.m_BlizzardGlobal.material.mainTexture.height;
		Log.UberText.Print("Blizzard Global Atlas Updated: {0}, {1}", new object[]
		{
			width,
			height
		});
	}

	// Token: 0x04007B34 RID: 31540
	public Font m_BelweFont;

	// Token: 0x04007B35 RID: 31541
	public Font m_BelweOutlineFont;

	// Token: 0x04007B36 RID: 31542
	public Font m_FranklinGothicFont;

	// Token: 0x04007B37 RID: 31543
	public Font m_BlizzardGlobal;

	// Token: 0x04007B38 RID: 31544
	private bool m_Active;

	// Token: 0x04007B39 RID: 31545
	private List<Font> m_Fonts;

	// Token: 0x04007B3A RID: 31546
	private Locale m_Locale;

	// Token: 0x04007B3B RID: 31547
	private string m_AtlasCharacters;

	// Token: 0x04007B3C RID: 31548
	private string m_AtlasNumbers = "0123456789.";

	// Token: 0x04007B3D RID: 31549
	private static UberTextMgr s_Instance;
}
