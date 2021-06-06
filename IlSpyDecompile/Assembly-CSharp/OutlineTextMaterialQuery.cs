using UnityEngine;

public sealed class OutlineTextMaterialQuery : UberTextMaterialQuery
{
	private MainTextureMaterialProperty m_textureProperty = new MainTextureMaterialProperty();

	private MainColorMaterialProperty m_colorProperty = new MainColorMaterialProperty();

	private FloatMaterialProperty m_lightingBlendProperty = new FloatMaterialProperty("_LightingBlend");

	private ColorMaterialProperty m_outlineColorProperty = new ColorMaterialProperty("_OutlineColor");

	private FloatMaterialProperty m_outlineOffsetX = new FloatMaterialProperty("_OutlineOffsetX");

	private FloatMaterialProperty m_outlineOffsetY = new FloatMaterialProperty("_OutlineOffsetY");

	private FloatMaterialProperty m_texelSizeX = new FloatMaterialProperty("_TexelSizeX");

	private FloatMaterialProperty m_texelSizeY = new FloatMaterialProperty("_TexelSizeY");

	public bool RichTextEnabled { get; private set; }

	public Locale Locale { get; private set; }

	public OutlineTextMaterialQuery()
		: base(8)
	{
		base.Type = UberTextMaterialManager.MaterialType.TEXT_OUTLINE;
		m_materialProperties.Add(m_textureProperty);
		m_materialProperties.Add(m_colorProperty);
		m_materialProperties.Add(m_lightingBlendProperty);
		m_materialProperties.Add(m_outlineColorProperty);
		m_materialProperties.Add(m_outlineOffsetX);
		m_materialProperties.Add(m_outlineOffsetY);
		m_materialProperties.Add(m_texelSizeX);
		m_materialProperties.Add(m_texelSizeY);
	}

	public override UberTextMaterialQuery Clone()
	{
		OutlineTextMaterialQuery outlineTextMaterialQuery = new OutlineTextMaterialQuery();
		CopyPropertiesTo(outlineTextMaterialQuery);
		outlineTextMaterialQuery.RichTextEnabled = RichTextEnabled;
		outlineTextMaterialQuery.Locale = Locale;
		return outlineTextMaterialQuery;
	}

	public override bool Equals(UberTextMaterialQuery query)
	{
		OutlineTextMaterialQuery outlineTextMaterialQuery = query as OutlineTextMaterialQuery;
		if (outlineTextMaterialQuery != null && (outlineTextMaterialQuery.RichTextEnabled != RichTextEnabled || outlineTextMaterialQuery.Locale != Locale))
		{
			return false;
		}
		return base.Equals(query);
	}

	public OutlineTextMaterialQuery WithTexture(Texture texture)
	{
		m_textureProperty.SetValue(texture);
		return this;
	}

	public OutlineTextMaterialQuery WithColor(Color color)
	{
		m_colorProperty.SetValue(color);
		return this;
	}

	public OutlineTextMaterialQuery WithLightingBlend(float blend)
	{
		m_lightingBlendProperty.SetValue(blend);
		return this;
	}

	public OutlineTextMaterialQuery WithOutlineColor(Color color)
	{
		m_outlineColorProperty.SetValue(color);
		return this;
	}

	public OutlineTextMaterialQuery WithOutlineOffsetX(float x)
	{
		m_outlineOffsetX.SetValue(x);
		return this;
	}

	public OutlineTextMaterialQuery WithOutlineOffsetY(float y)
	{
		m_outlineOffsetY.SetValue(y);
		return this;
	}

	public OutlineTextMaterialQuery WithTexelSizeX(float x)
	{
		m_texelSizeX.SetValue(x);
		return this;
	}

	public OutlineTextMaterialQuery WithTexelSizeY(float y)
	{
		m_texelSizeY.SetValue(y);
		return this;
	}

	public OutlineTextMaterialQuery WithRichText(bool richText)
	{
		RichTextEnabled = richText;
		return this;
	}

	public OutlineTextMaterialQuery WithLocale(Locale locale)
	{
		Locale = locale;
		return this;
	}
}
