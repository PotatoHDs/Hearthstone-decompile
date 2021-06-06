using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Blizzard.T5.Jobs;
using Hearthstone.UI;
using Hearthstone.UI.Internal;
using UnityEngine;

[ExecuteAlways]
[CustomEditClass]
public class UberText : MonoBehaviour, IPopupRendering
{
	[Serializable]
	public class LocalizationSettings
	{
		[Serializable]
		public class LocaleAdjustment
		{
			public Locale m_Locale;

			public float m_LineSpaceModifier = 1f;

			public float m_SingleLineAdjustment;

			public float m_Width;

			public float m_Height;

			public float m_FontSizeModifier = 1f;

			public float m_UnderwearWidth;

			public float m_UnderwearHeight;

			public float m_OutlineModifier = 1f;

			public float m_UnboundCharacterSizeModifier = 1f;

			public float m_ResizeToFitWidthModifier = 1f;

			public Vector3 m_PositionOffset = Vector3.zero;

			public LocaleAdjustment()
			{
				m_Locale = Locale.enUS;
			}

			public LocaleAdjustment(Locale locale)
			{
				m_Locale = locale;
			}
		}

		public List<LocaleAdjustment> m_LocaleAdjustments;

		public LocalizationSettings()
		{
			m_LocaleAdjustments = new List<LocaleAdjustment>();
		}

		public bool HasLocale(Locale locale)
		{
			if (GetLocale(locale) != null)
			{
				return true;
			}
			return false;
		}

		public LocaleAdjustment GetLocale(Locale locale)
		{
			foreach (LocaleAdjustment localeAdjustment in m_LocaleAdjustments)
			{
				if (localeAdjustment.m_Locale == locale)
				{
					return localeAdjustment;
				}
			}
			return null;
		}

		public LocaleAdjustment AddLocale(Locale locale)
		{
			LocaleAdjustment locale2 = GetLocale(locale);
			if (locale2 != null)
			{
				return locale2;
			}
			locale2 = new LocaleAdjustment(locale);
			m_LocaleAdjustments.Add(locale2);
			return locale2;
		}

		public void RemoveLocale(Locale locale)
		{
			for (int i = 0; i < m_LocaleAdjustments.Count; i++)
			{
				if (m_LocaleAdjustments[i].m_Locale == locale)
				{
					m_LocaleAdjustments.RemoveAt(i);
					break;
				}
			}
		}
	}

	private class CachedTextKeyData
	{
		public string m_Text;

		public int m_FontSize;

		public float m_CharSize;

		public float m_Width;

		public float m_Height;

		public Font m_Font;

		public float m_LineSpacing;

		public override int GetHashCode()
		{
			return m_Text.Length + m_FontSize + m_Text.GetHashCode() + m_FontSize.GetHashCode() - m_CharSize.GetHashCode() + m_Width.GetHashCode() - m_Height.GetHashCode() + m_Font.GetHashCode() - m_LineSpacing.GetHashCode();
		}
	}

	[Serializable]
	private class CachedTextValues
	{
		public string m_Text;

		public float m_CharSize;

		public int m_OriginalTextHash;
	}

	public enum AlignmentOptions
	{
		Left,
		Center,
		Right
	}

	public enum AnchorOptions
	{
		Upper,
		Middle,
		Lower
	}

	private enum TextRenderMaterial
	{
		Text,
		Bold,
		Outline,
		InlineImages
	}

	private enum Fonts
	{
		BlizzardGlobal,
		Belwe,
		BelweOutline,
		FranklinGothic
	}

	private enum ThaiGlyphType
	{
		BASE,
		BASE_ASCENDER,
		BASE_DESCENDER,
		TONE_MARK,
		UPPER,
		LOWER
	}

	private const int CACHE_FILE_VERSION_TEMP = 2;

	private const int CACHE_FILE_MAX_SIZE = 50000;

	private const string FONT_NAME_BLIZZARD_GLOBAL = "BlizzardGlobal";

	private const string FONT_NAME_BELWE_OUTLINE = "Belwe_Outline";

	private const string FONT_NAME_BELWE = "Belwe";

	private const string FONT_NAME_FRANKLIN_GOTHIC = "FranklinGothic";

	private const float CHARACTER_SIZE_SCALE = 0.01f;

	private const float BOLD_MAX_SIZE = 10f;

	public const int WORD_CHARACTER_BUFFER = 3;

	private const int MAX_RESIZE_TEXT_ITERATION_COUNT = 40;

	[SerializeField]
	private string m_Text = "Uber Text";

	[SerializeField]
	private bool m_GameStringLookup;

	[SerializeField]
	private bool m_UseEditorText;

	[SerializeField]
	private float m_Width = 1f;

	[SerializeField]
	private float m_Height = 1f;

	[SerializeField]
	private float m_LineSpacing = 1f;

	[SerializeField]
	private Font m_Font;

	[SerializeField]
	private int m_FontSize = 35;

	[SerializeField]
	private int m_MinFontSize = 10;

	[SerializeField]
	private float m_CharacterSize = 5f;

	[SerializeField]
	private float m_MinCharacterSize = 1f;

	[SerializeField]
	private bool m_RichText = true;

	[SerializeField]
	private Color m_TextColor = Color.white;

	[SerializeField]
	private float m_BoldSize;

	[SerializeField]
	private bool m_WordWrap;

	[SerializeField]
	private bool m_ForceWrapLargeWords;

	[SerializeField]
	private bool m_ResizeToFit;

	[SerializeField]
	private bool m_ResizeToFitAndGrow;

	[SerializeField]
	private float m_ResizeToGrowStep = 0.05f;

	[SerializeField]
	private bool m_Underwear;

	[SerializeField]
	private bool m_UnderwearFlip;

	[SerializeField]
	private float m_UnderwearWidth = 0.2f;

	[SerializeField]
	private float m_UnderwearHeight = 0.2f;

	[SerializeField]
	private AlignmentOptions m_Alignment = AlignmentOptions.Center;

	[SerializeField]
	private AnchorOptions m_Anchor = AnchorOptions.Middle;

	[SerializeField]
	private bool m_RenderToTexture;

	[SerializeField]
	private GameObject m_RenderOnObject;

	[SerializeField]
	private int m_Resolution = 256;

	[SerializeField]
	private bool m_Outline;

	[SerializeField]
	private float m_OutlineSize = 1f;

	[SerializeField]
	private Color m_OutlineColor = Color.black;

	[SerializeField]
	private bool m_AntiAlias;

	[SerializeField]
	private float m_AntiAliasAmount = 0.5f;

	[SerializeField]
	private float m_AntiAliasEdge = 0.5f;

	[SerializeField]
	private bool m_Shadow;

	[SerializeField]
	private float m_ShadowOffset = 1f;

	[SerializeField]
	private float m_ShadowDepthOffset;

	[SerializeField]
	private Color m_ShadowColor = new Color(0.1f, 0.1f, 0.1f, 0.333f);

	[SerializeField]
	private float m_ShadowBlur = 1.5f;

	[SerializeField]
	private int m_ShadowRenderQueueOffset = -1;

	[SerializeField]
	private int m_RenderQueue;

	[SerializeField]
	private float m_AmbientLightBlend;

	[SerializeField]
	private List<Material> m_AdditionalMaterials = new List<Material>();

	[SerializeField]
	private Color m_GradientUpperColor = Color.white;

	[SerializeField]
	private Color m_GradientLowerColor = Color.white;

	[SerializeField]
	private bool m_Cache = true;

	[SerializeField]
	private LocalizationSettings m_LocalizedSettings;

	private bool m_isFontDefLoaded;

	private Font m_LocalizedFont;

	private float m_LineSpaceModifier = 1f;

	private float m_SingleLineAdjustment;

	private float m_FontSizeModifier = 1f;

	private float m_CharacterSizeModifier = 1f;

	private float m_UnboundCharacterSizeModifier = 1f;

	private float m_OutlineModifier = 1f;

	private float m_WorldWidth;

	private float m_WorldHeight;

	private bool m_updated;

	private string m_PreviousText = string.Empty;

	private int m_LineCount;

	private Vector2 m_PreviousTexelSize;

	private Vector2Int m_PreviousFontSize;

	private bool m_Ellipsized;

	private int m_CacheHash;

	private bool m_Hidden;

	private bool m_TextSet;

	private FlagStateTracker m_meshReadyTracker;

	private Bounds m_UnderwearLeftBounds;

	private Bounds m_UnderwearRightBounds;

	private WidgetTransform m_widgetTransform;

	private bool m_HadWidgetTransformLastCheck;

	private UberTextRendering m_UberTextRendering;

	private UberTextLineWrapper m_UberTextLineWrapper;

	private UberTextRenderToTexture m_UberTextRenderToTexture;

	private bool m_HasBoldText;

	private int m_CurrentLayer;

	private static float s_offset = -3000f;

	private float m_Offset;

	private readonly StringBuilder m_forceLargeWrapRichTextString = new StringBuilder();

	private readonly StringBuilder m_forceLargeWrapTestString = new StringBuilder();

	private WordWrapSettings m_wordWrapSettings;

	private static bool s_disableCache = false;

	private static Map<int, CachedTextValues> s_CachedText = new Map<int, CachedTextValues>();

	private static Map<Font, int> s_TexelUpdateFrame = new Map<Font, int>();

	private static Map<Font, Vector2> s_TexelUpdateData = new Map<Font, Vector2>();

	private PopupRoot m_popupRoot;

	private int m_originalLayerBeforePopupRendering;

	private bool m_popupRenderingEnabled;

	private static readonly char[] STRIP_CHARS_INDEX_OF_ANY = new char[8] { '<', '[', '\\', ' ', '\t', '\r', '\n', '_' };

	private bool IsPartOfWidget => m_widgetTransform != null;

	protected float Offset
	{
		get
		{
			if (m_Offset == 0f)
			{
				s_offset -= 100f;
				m_Offset = s_offset;
			}
			return m_Offset;
		}
	}

	[Overridable]
	[CustomEditField(Sections = "Text", T = EditType.TEXT_AREA)]
	public string Text
	{
		get
		{
			return m_Text;
		}
		set
		{
			m_TextSet = true;
			m_TextSet = true;
			if (value == m_Text)
			{
				return;
			}
			m_Text = value ?? string.Empty;
			if (m_Text.Any((char c) => char.IsSurrogate(c)))
			{
				IEnumerable<char> source = from c in m_Text
					where !char.IsLowSurrogate(c)
					select (!char.IsHighSurrogate(c)) ? c : '\ufffd';
				m_Text = new string(source.ToArray());
			}
			if (!(m_Text == m_PreviousText))
			{
				UpdateNow();
			}
		}
	}

	[CustomEditField(Sections = "Text", HidePredicate = "IsPartOfWidget")]
	public bool GameStringLookup
	{
		get
		{
			return m_GameStringLookup;
		}
		set
		{
			if (value != m_GameStringLookup)
			{
				m_GameStringLookup = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Text", HidePredicate = "IsPartOfWidget")]
	public bool UseEditorText
	{
		get
		{
			return m_UseEditorText;
		}
		set
		{
			if (value != m_UseEditorText)
			{
				m_UseEditorText = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Text", HidePredicate = "IsPartOfWidget")]
	public bool Cache
	{
		get
		{
			return m_Cache;
		}
		set
		{
			m_Cache = value;
		}
	}

	[CustomEditField(Sections = "Size", HidePredicate = "IsPartOfWidget")]
	public float Width
	{
		get
		{
			return m_Width;
		}
		set
		{
			if (value != m_Width)
			{
				m_Width = value;
				if (m_Width < 0.01f)
				{
					m_Width = 0.01f;
				}
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Size", HidePredicate = "IsPartOfWidget")]
	public float Height
	{
		get
		{
			return m_Height;
		}
		set
		{
			if (value != m_Height)
			{
				m_Height = value;
				if (m_Height < 0.01f)
				{
					m_Height = 0.01f;
				}
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Size")]
	public float LineSpacing
	{
		get
		{
			return m_LineSpacing;
		}
		set
		{
			if (value != m_LineSpacing)
			{
				m_LineSpacing = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Style")]
	public Font TrueTypeFont
	{
		get
		{
			return m_Font;
		}
		set
		{
			if (!(value == m_Font))
			{
				m_Font = value;
				m_isFontDefLoaded = false;
				SetFont();
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Style")]
	public int FontSize
	{
		get
		{
			return m_FontSize;
		}
		set
		{
			if (value != m_FontSize)
			{
				m_FontSize = value;
				if (m_FontSize < 1)
				{
					m_FontSize = 1;
				}
				if (m_FontSize > 120)
				{
					m_FontSize = 120;
				}
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Style")]
	public int MinFontSize
	{
		get
		{
			return m_MinFontSize;
		}
		set
		{
			if (value != m_MinFontSize)
			{
				m_MinFontSize = value;
				if (m_MinFontSize < 1)
				{
					m_MinFontSize = 1;
				}
				if (m_MinFontSize > m_FontSize)
				{
					m_MinFontSize = m_FontSize;
				}
				UpdateText();
			}
		}
	}

	[Overridable]
	[CustomEditField(Sections = "Style")]
	public float CharacterSize
	{
		get
		{
			return m_CharacterSize;
		}
		set
		{
			if (value != m_CharacterSize)
			{
				m_CharacterSize = value;
				UpdateText();
			}
		}
	}

	[Overridable]
	[CustomEditField(Sections = "Style")]
	public float MinCharacterSize
	{
		get
		{
			return m_MinCharacterSize;
		}
		set
		{
			if (value != m_MinCharacterSize)
			{
				m_MinCharacterSize = value;
				if (m_MinCharacterSize < 0.001f)
				{
					m_MinCharacterSize = 0.001f;
				}
				if (m_MinCharacterSize > m_CharacterSize)
				{
					m_MinCharacterSize = m_CharacterSize;
				}
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Style")]
	public bool RichText
	{
		get
		{
			return m_RichText;
		}
		set
		{
			if (value != m_RichText)
			{
				m_RichText = value;
				UpdateText();
			}
		}
	}

	[Overridable]
	[CustomEditField(Sections = "Style")]
	public Color TextColor
	{
		get
		{
			return m_TextColor;
		}
		set
		{
			if (!(value == m_TextColor))
			{
				m_TextColor = value;
				UpdateColor();
			}
		}
	}

	[Overridable]
	[CustomEditField(Hide = true)]
	public float TextAlpha
	{
		get
		{
			return m_TextColor.a;
		}
		set
		{
			if (value != m_TextColor.a)
			{
				m_TextColor.a = value;
				UpdateColor();
			}
		}
	}

	[CustomEditField(Sections = "Style")]
	public float BoldSize
	{
		get
		{
			return m_BoldSize;
		}
		set
		{
			if (value != m_BoldSize)
			{
				m_BoldSize = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Paragraph")]
	public bool WordWrap
	{
		get
		{
			return m_WordWrap;
		}
		set
		{
			if (value != m_WordWrap)
			{
				m_WordWrap = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Paragraph")]
	public bool ForceWrapLargeWords
	{
		get
		{
			return m_ForceWrapLargeWords;
		}
		set
		{
			if (value != m_ForceWrapLargeWords)
			{
				m_ForceWrapLargeWords = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Paragraph")]
	public bool ResizeToFit
	{
		get
		{
			return m_ResizeToFit;
		}
		set
		{
			if (value != m_ResizeToFit)
			{
				m_ResizeToFit = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Paragraph")]
	public bool ResizeToFitAndGrow
	{
		get
		{
			return m_ResizeToFitAndGrow;
		}
		set
		{
			if (value != m_ResizeToFitAndGrow)
			{
				m_ResizeToFitAndGrow = value;
				m_ResizeToFit |= value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Paragraph")]
	public float ResizeToGrowStep
	{
		get
		{
			return m_ResizeToGrowStep;
		}
		set
		{
			if (value != m_ResizeToGrowStep)
			{
				m_ResizeToGrowStep = Math.Max(0.01f, value);
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Underwear", Label = "Enable")]
	public bool Underwear
	{
		get
		{
			return m_Underwear;
		}
		set
		{
			if (value != m_Underwear)
			{
				m_Underwear = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Parent = "Underwear", Label = "Flip")]
	public bool UnderwearFlip
	{
		get
		{
			return m_UnderwearFlip;
		}
		set
		{
			if (value != m_UnderwearFlip)
			{
				m_UnderwearFlip = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Parent = "Underwear", Label = "Width")]
	public float UnderwearWidth
	{
		get
		{
			return m_UnderwearWidth;
		}
		set
		{
			if (value != m_UnderwearWidth)
			{
				m_UnderwearWidth = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Parent = "Underwear", Label = "Height")]
	public float UnderwearHeight
	{
		get
		{
			return m_UnderwearHeight;
		}
		set
		{
			if (value != m_UnderwearHeight)
			{
				m_UnderwearHeight = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Alignment", Label = "Enable")]
	[Overridable]
	public AlignmentOptions Alignment
	{
		get
		{
			return m_Alignment;
		}
		set
		{
			if (value != m_Alignment)
			{
				m_Alignment = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Parent = "Alignment")]
	[Overridable]
	public AnchorOptions Anchor
	{
		get
		{
			return m_Anchor;
		}
		set
		{
			if (value != m_Anchor)
			{
				m_Anchor = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Render/Bake")]
	public bool RenderToTexture
	{
		get
		{
			return m_RenderToTexture;
		}
		set
		{
			if (value != m_RenderToTexture)
			{
				m_RenderToTexture = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Render/Bake")]
	public GameObject RenderOnObject
	{
		get
		{
			return m_RenderOnObject;
		}
		set
		{
			if (!(value == m_RenderOnObject))
			{
				m_RenderOnObject = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Parent = "RenderToTexture")]
	public int TextureResolution
	{
		get
		{
			return m_Resolution;
		}
		set
		{
			if (value != m_Resolution)
			{
				m_Resolution = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Outline", Label = "Enable")]
	[Overridable]
	public bool Outline
	{
		get
		{
			return m_Outline;
		}
		set
		{
			if (value != m_Outline)
			{
				m_Outline = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Parent = "Outline", Label = "Size")]
	[Overridable]
	public float OutlineSize
	{
		get
		{
			return m_OutlineSize;
		}
		set
		{
			if (value != m_OutlineSize)
			{
				m_OutlineSize = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Parent = "Outline", Label = "Color")]
	[Overridable]
	public Color OutlineColor
	{
		get
		{
			return m_OutlineColor;
		}
		set
		{
			if (!(value == m_OutlineColor))
			{
				m_OutlineColor = value;
				UpdateColor();
			}
		}
	}

	[CustomEditField(Hide = true)]
	[Overridable]
	public float OutlineAlpha
	{
		get
		{
			return m_OutlineColor.a;
		}
		set
		{
			if (value != m_OutlineColor.a)
			{
				m_OutlineColor.a = value;
				UpdateColor();
			}
		}
	}

	[CustomEditField(Parent = "RenderToTexture")]
	public bool AntiAlias
	{
		get
		{
			return m_AntiAlias;
		}
		set
		{
			if (value != m_AntiAlias)
			{
				m_AntiAlias = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Parent = "AntiAlias")]
	public float AntiAliasAmount
	{
		get
		{
			return m_AntiAliasAmount;
		}
		set
		{
			if (value != m_AntiAliasAmount)
			{
				m_AntiAliasAmount = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Localization")]
	public LocalizationSettings LocalizeSettings
	{
		get
		{
			return m_LocalizedSettings;
		}
		set
		{
			m_LocalizedSettings = value;
			UpdateText();
		}
	}

	[CustomEditField(Parent = "AntiAlias")]
	public float AntiAliasEdge
	{
		get
		{
			return m_AntiAliasEdge;
		}
		set
		{
			if (value != m_AntiAliasEdge)
			{
				m_AntiAliasEdge = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Shadow", Label = "Enable")]
	[Overridable]
	public bool Shadow
	{
		get
		{
			return m_Shadow;
		}
		set
		{
			if (value != m_Shadow)
			{
				m_Shadow = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Parent = "Shadow")]
	[Overridable]
	public float ShadowOffset
	{
		get
		{
			return m_ShadowOffset;
		}
		set
		{
			if (value != m_ShadowOffset)
			{
				m_ShadowOffset = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Parent = "Shadow")]
	[Overridable]
	public float ShadowDepthOffset
	{
		get
		{
			return m_ShadowDepthOffset;
		}
		set
		{
			if (value != m_ShadowDepthOffset)
			{
				m_ShadowDepthOffset = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Parent = "Shadow")]
	[Overridable]
	public float ShadowBlur
	{
		get
		{
			return m_ShadowBlur;
		}
		set
		{
			if (value != m_ShadowBlur)
			{
				m_ShadowBlur = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Parent = "Shadow")]
	[Overridable]
	public Color ShadowColor
	{
		get
		{
			return m_ShadowColor;
		}
		set
		{
			if (!(value == m_ShadowColor))
			{
				m_ShadowColor = value;
				UpdateColor();
			}
		}
	}

	[CustomEditField(Parent = "Shadow")]
	[Overridable]
	public float ShadowAlpha
	{
		get
		{
			return m_ShadowColor.a;
		}
		set
		{
			if (value != m_ShadowColor.a)
			{
				m_ShadowColor.a = value;
				UpdateColor();
			}
		}
	}

	[CustomEditField(Parent = "Shadow")]
	public int ShadowRenderQueueOffset
	{
		get
		{
			return m_ShadowRenderQueueOffset;
		}
		set
		{
			if (value != m_ShadowRenderQueueOffset)
			{
				m_ShadowRenderQueueOffset = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Render")]
	public int RenderQueue
	{
		get
		{
			return m_RenderQueue;
		}
		set
		{
			if (value != m_RenderQueue)
			{
				m_RenderQueue = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Render")]
	public float AmbientLightBlend
	{
		get
		{
			return m_AmbientLightBlend;
		}
		set
		{
			if (value != m_AmbientLightBlend)
			{
				m_AmbientLightBlend = value;
				UpdateText();
			}
		}
	}

	[CustomEditField(Sections = "Render")]
	public List<Material> AdditionalMaterials
	{
		get
		{
			return m_AdditionalMaterials;
		}
		set
		{
			m_AdditionalMaterials = value;
		}
	}

	[CustomEditField(Parent = "RenderToTexture")]
	public Color GradientUpperColor
	{
		get
		{
			return m_GradientUpperColor;
		}
		set
		{
			if (!(value == m_GradientUpperColor))
			{
				m_GradientUpperColor = value;
				UpdateColor();
			}
		}
	}

	[CustomEditField(Hide = true)]
	public float GradientUpperAlpha
	{
		get
		{
			return m_GradientUpperColor.a;
		}
		set
		{
			if (value != m_GradientUpperColor.a)
			{
				m_GradientUpperColor.a = value;
				UpdateColor();
			}
		}
	}

	[CustomEditField(Parent = "RenderToTexture")]
	public Color GradientLowerColor
	{
		get
		{
			return m_GradientLowerColor;
		}
		set
		{
			if (!(value == m_GradientLowerColor))
			{
				m_GradientLowerColor = value;
				UpdateColor();
			}
		}
	}

	[CustomEditField(Hide = true)]
	public float GradientLowerAlpha
	{
		get
		{
			return m_GradientLowerColor.a;
		}
		set
		{
			if (value != m_GradientLowerColor.a)
			{
				m_GradientLowerColor.a = value;
				UpdateColor();
			}
		}
	}

	public bool IsMeshReady => m_meshReadyTracker.IsSet;

	public static UberText[] EnableAllTextInObject(GameObject obj, bool enable)
	{
		UberText[] componentsInChildren = obj.GetComponentsInChildren<UberText>();
		EnableAllTextObjects(componentsInChildren, enable);
		return componentsInChildren;
	}

	public static void EnableAllTextObjects(UberText[] objs, bool enable)
	{
		for (int i = 0; i < objs.Length; i++)
		{
			objs[i].gameObject.SetActive(enable);
		}
	}

	private void Awake()
	{
		m_UberTextRendering = new UberTextRendering();
		m_UberTextLineWrapper = new UberTextLineWrapper(m_UberTextRendering);
		m_wordWrapSettings = default(WordWrapSettings);
		m_UberTextRenderToTexture = new UberTextRenderToTexture();
		m_widgetTransform = GetComponent<WidgetTransform>();
		if (IsPartOfWidget)
		{
			m_UseEditorText = true;
			m_GameStringLookup = true;
			m_Cache = true;
		}
		if (!m_GameStringLookup && !m_TextSet && !m_UseEditorText && Application.IsPlaying(this))
		{
			m_Text = "";
		}
	}

	private void Start()
	{
		m_updated = false;
	}

	private void Update()
	{
		CheckObjectLayer();
		CheckFontTextureSize();
		RenderText();
	}

	private void CheckForWidgetTransform()
	{
		m_widgetTransform = GetComponent<WidgetTransform>();
		if (m_HadWidgetTransformLastCheck != IsPartOfWidget)
		{
			m_updated = false;
		}
		m_HadWidgetTransformLastCheck = IsPartOfWidget;
	}

	private void CheckObjectLayer()
	{
		int layer = base.gameObject.layer;
		if (m_CurrentLayer != layer || (m_UberTextRendering != null && !m_UberTextRendering.HasLayer(layer)) || (m_UberTextRenderToTexture != null && !m_UberTextRenderToTexture.HasLayer(layer)))
		{
			UpdateLayers();
		}
	}

	private void CheckFontTextureSize()
	{
		if (m_UberTextRendering != null)
		{
			Texture fontTexture = m_UberTextRendering.GetFontTexture();
			if ((bool)fontTexture && (m_PreviousFontSize.x != fontTexture.width || m_PreviousFontSize.y != fontTexture.height))
			{
				m_updated = false;
			}
		}
	}

	private void OnDisable()
	{
		if (m_UberTextRendering != null && m_UberTextRenderToTexture != null)
		{
			m_UberTextRenderToTexture.SetAllVisible(visible: false);
		}
	}

	private void OnDestroy()
	{
		CleanUp();
	}

	private void OnEnable()
	{
		m_updated = false;
		if (m_UberTextRendering != null)
		{
			m_UberTextRendering.SetActive(active: true);
		}
		if (m_UberTextRenderToTexture != null)
		{
			m_UberTextRenderToTexture.SetActive(active: true);
		}
		SetFont();
		UpdateNow();
	}

	private void OnDrawGizmos()
	{
		float width = GetWidth();
		float height = GetHeight();
		Gizmos.matrix = base.transform.localToWorldMatrix;
		if (IsPartOfWidget)
		{
			Gizmos.matrix *= Matrix4x4.TRS(GetTextCenter(), GetTextRotation(), Vector3.one);
		}
		Gizmos.color = new Color(0.3f, 0.3f, 0.35f, 0.2f);
		Gizmos.DrawCube(Vector3.zero, new Vector3(width, height, 0f));
		Gizmos.color = Color.black;
		Gizmos.DrawWireCube(Vector3.zero, new Vector3(width, height, 0f));
		if (m_Underwear)
		{
			float underwearWidth = m_UnderwearWidth;
			float underwearHeight = m_UnderwearHeight;
			if (m_LocalizedSettings != null)
			{
				LocalizationSettings.LocaleAdjustment locale = m_LocalizedSettings.GetLocale(Localization.GetLocale());
				if (locale != null)
				{
					if (locale.m_UnderwearWidth > 0f)
					{
						underwearWidth = locale.m_UnderwearWidth;
					}
					if (locale.m_UnderwearHeight > 0f)
					{
						underwearHeight = locale.m_UnderwearHeight;
					}
				}
			}
			float num = width * underwearWidth * 0.5f;
			float num2 = height * underwearHeight;
			if (m_UnderwearFlip)
			{
				Gizmos.DrawWireCube(new Vector3(0f - (width * 0.5f - num * 0.5f), height * 0.5f - num2 * 0.5f, 0f), new Vector3(num, num2, 0f));
				Gizmos.DrawWireCube(new Vector3(width * 0.5f - num * 0.5f, height * 0.5f - num2 * 0.5f, 0f), new Vector3(num, num2, 0f));
			}
			else
			{
				Gizmos.DrawWireCube(new Vector3(0f - (width * 0.5f - num * 0.5f), 0f - (height * 0.5f - num2 * 0.5f), 0f), new Vector3(num, num2, 0f));
				Gizmos.DrawWireCube(new Vector3(width * 0.5f - num * 0.5f, 0f - (height * 0.5f - num2 * 0.5f), 0f), new Vector3(num, num2, 0f));
			}
		}
		Gizmos.matrix = Matrix4x4.identity;
	}

	private void OnDrawGizmosSelected()
	{
		float width = GetWidth();
		float height = GetHeight();
		Gizmos.matrix = base.transform.localToWorldMatrix;
		if (IsPartOfWidget)
		{
			Gizmos.matrix *= Matrix4x4.TRS(GetTextCenter(), GetTextRotation(), Vector3.one);
		}
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(Vector3.zero, new Vector3(width + GetGizmoBuffer(width), height + GetGizmoBuffer(height), 0f));
		Gizmos.matrix = Matrix4x4.identity;
	}

	private float GetGizmoBuffer(float boundsValue)
	{
		return Mathf.Min(boundsValue * 0.04f, 0.1f);
	}

	public void Show()
	{
		m_Hidden = false;
		UpdateNow();
	}

	public void Hide()
	{
		m_Hidden = true;
		UpdateNow();
	}

	public bool isHidden()
	{
		return m_Hidden;
	}

	public void EditorAwake()
	{
		UpdateText();
	}

	public bool IsDone()
	{
		return m_updated;
	}

	public void UpdateText()
	{
		if (base.gameObject.activeInHierarchy)
		{
			m_updated = false;
		}
	}

	public void UpdateNow(bool updateIfInactive = false)
	{
		if (!(this == null) && !(base.gameObject == null) && (base.gameObject.activeInHierarchy || updateIfInactive))
		{
			m_updated = false;
			RenderText();
		}
	}

	public Bounds GetBounds()
	{
		Matrix4x4 matrix4x = base.transform.localToWorldMatrix * Matrix4x4.Rotate(GetTextRotation());
		Vector3 vector = matrix4x.MultiplyVector(Vector3.up) * (GetHeight() * 0.5f);
		Vector3 vector2 = matrix4x.MultiplyVector(Vector3.right) * (GetWidth() * 0.5f);
		Bounds result = default(Bounds);
		result.min = base.transform.position - vector2 + vector;
		result.max = base.transform.position + vector2 - vector;
		return result;
	}

	public Bounds GetLocalBounds()
	{
		Matrix4x4 matrix4x = Matrix4x4.Rotate(GetTextRotation());
		Vector3 vector = matrix4x.MultiplyVector(Vector3.up) * (GetHeight() * 0.5f);
		Vector3 vector2 = matrix4x.MultiplyVector(Vector3.right) * (GetWidth() * 0.5f);
		Bounds result = default(Bounds);
		result.min = base.transform.position - vector2 + vector;
		result.max = base.transform.position + vector2 - vector;
		return result;
	}

	public Bounds GetTextBounds()
	{
		if (!m_updated)
		{
			UpdateNow();
		}
		if (m_UberTextRendering == null)
		{
			return new Bounds(Vector3.zero, Vector3.zero);
		}
		Quaternion rotation = base.transform.rotation;
		base.transform.rotation = Quaternion.identity;
		Bounds textMeshBounds = m_UberTextRendering.GetTextMeshBounds();
		base.transform.rotation = rotation;
		return textMeshBounds;
	}

	public Bounds GetTextWorldSpaceBounds()
	{
		if (!m_updated)
		{
			UpdateNow();
		}
		if (m_UberTextRendering == null)
		{
			return new Bounds(Vector3.zero, Vector3.zero);
		}
		return m_UberTextRendering.GetTextMeshBounds();
	}

	public bool IsMultiLine()
	{
		return m_LineCount > 1;
	}

	public bool IsEllipsized()
	{
		return m_Ellipsized;
	}

	public void SetGameStringText(string gameStringTag)
	{
		Text = GameStrings.Get(gameStringTag);
	}

	public Font GetLocalizedFont()
	{
		if ((bool)m_LocalizedFont)
		{
			return m_LocalizedFont;
		}
		return m_Font;
	}

	public LocalizationSettings.LocaleAdjustment AddLocaleAdjustment(Locale locale)
	{
		return m_LocalizedSettings.AddLocale(locale);
	}

	public LocalizationSettings.LocaleAdjustment GetLocaleAdjustment(Locale locale)
	{
		return m_LocalizedSettings.GetLocale(locale);
	}

	public void RemoveLocaleAdjustment(Locale locale)
	{
		m_LocalizedSettings.RemoveLocale(locale);
	}

	public LocalizationSettings GetAllLocalizationSettings()
	{
		return m_LocalizedSettings;
	}

	public void SetFontWithoutLocalization(FontDefinition fontDef)
	{
		Font font = fontDef.m_Font;
		if (!(font == null) && (m_UberTextRendering == null || m_UberTextRendering.CanSetFont(font)))
		{
			m_Font = font;
			m_LocalizedFont = m_Font;
			m_LineSpaceModifier = fontDef.m_LineSpaceModifier;
			m_FontSizeModifier = fontDef.m_FontSizeModifier;
			m_SingleLineAdjustment = fontDef.m_SingleLineAdjustment;
			m_CharacterSizeModifier = fontDef.m_CharacterSizeModifier;
			m_UnboundCharacterSizeModifier = fontDef.m_UnboundCharacterSizeModifier;
			m_OutlineModifier = fontDef.m_OutlineModifier;
			m_isFontDefLoaded = true;
			if (m_UberTextRendering != null)
			{
				m_UberTextRendering.SetFont(m_Font);
			}
			UpdateNow();
		}
	}

	public string GetProcessedText()
	{
		if (m_UberTextRendering == null)
		{
			return string.Empty;
		}
		return m_UberTextRendering.GetText().Replace("<material=1></material>", string.Empty).Replace("<material=1>", "<b>")
			.Replace("</material>", "</b>");
	}

	public static void RebuildAllUberText()
	{
		UberText[] array = UnityEngine.Object.FindObjectsOfType<UberText>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].UpdateNow();
		}
	}

	private void RenderText()
	{
		if (!CanRenderText())
		{
			return;
		}
		if (string.IsNullOrEmpty(m_Text) || m_Hidden)
		{
			SetRenderedObjectsVisible(visible: false);
			return;
		}
		SetWorldWidthAndHeight();
		CreateTextMesh();
		UpdateTextPosition();
		SetFont();
		SetFontSize(m_FontSize);
		SetLineSpacing(m_LineSpacing);
		SetupTextMeshAlignment();
		m_UberTextRendering.SetLocale(Localization.GetLocale());
		SetupTextAndCharacterSize();
		SetupTextMeshAlignment();
		UpdateOutlineProperties();
		UpdateTexelSize();
		UpdateLayers();
		UpdateRenderQueue();
		UpdateColor();
		m_UberTextRendering.SetBoldEnabled(m_HasBoldText);
		m_UberTextRendering.SetOutlineEnabled(m_Outline);
		m_UberTextRendering.ApplyMaterials();
		if (m_RenderToTexture)
		{
			SetupRenderToTexture();
		}
		SetRenderedObjectsVisible(visible: true);
		if (m_RenderToTexture)
		{
			m_UberTextRenderToTexture.DoRenderToTexture();
			if (m_AntiAlias)
			{
				m_UberTextRenderToTexture.ApplyAntialiasing();
			}
		}
		m_PreviousText = m_Text;
		m_updated = true;
	}

	private bool CanRenderText()
	{
		if (m_RenderToTexture && m_UberTextRenderToTexture != null)
		{
			m_updated = m_updated && m_UberTextRenderToTexture.IsRenderTextureCreated();
		}
		if (m_updated || m_UberTextRendering == null || m_UberTextRenderToTexture == null)
		{
			return false;
		}
		if (!m_Font)
		{
			Debug.LogWarning($"UberText error: Font is null for {base.gameObject.name}");
			return false;
		}
		return true;
	}

	private void SetRenderedObjectsVisible(bool visible)
	{
		m_UberTextRendering.SetVisible(visible);
		m_UberTextRenderToTexture.SetAllVisible(visible);
	}

	private bool CanUseCachedText()
	{
		return 0 == 0;
	}

	private void SetupTextAndCharacterSize()
	{
		m_UberTextRendering.SetCharacterSize(m_CharacterSize * m_CharacterSizeModifier * 0.01f);
		string text = string.Empty;
		bool flag = false;
		bool num = CanUseCachedText();
		if (num)
		{
			m_CacheHash = new CachedTextKeyData
			{
				m_Text = m_Text,
				m_CharSize = m_CharacterSize,
				m_Font = m_Font,
				m_FontSize = m_FontSize,
				m_Height = GetHeight(),
				m_Width = GetWidth(),
				m_LineSpacing = m_LineSpacing
			}.GetHashCode();
			if (m_Cache && (m_WordWrap || m_ResizeToFit) && s_CachedText.ContainsKey(m_CacheHash))
			{
				CachedTextValues cachedTextValues = s_CachedText[m_CacheHash];
				if (cachedTextValues.m_OriginalTextHash == m_Text.GetHashCode())
				{
					text = cachedTextValues.m_Text;
					m_UberTextRendering.SetText(text);
					m_UberTextRendering.SetCharacterSize(cachedTextValues.m_CharSize);
					SetLineSpacing(m_LineSpacing);
					flag = true;
				}
			}
		}
		Quaternion rotation = base.transform.rotation;
		base.transform.rotation = Quaternion.identity;
		if (!flag)
		{
			string text2 = m_Text;
			text = string.Empty;
			if (m_GameStringLookup)
			{
				text2 = GameStrings.Get(text2.Trim());
			}
			if (Localization.GetLocale() != 0)
			{
				text2 = LocalizationFixes(text2);
			}
			text = ProcessText(text2);
			m_LineCount = LineCount(text);
			m_Ellipsized = false;
			if (m_WordWrap && !m_ResizeToFit)
			{
				m_UberTextRendering.SetText(WordWrapString(text, GetWidth()));
			}
			else
			{
				m_UberTextRendering.SetText(text);
			}
			m_UberTextRendering.SetCharacterSize(m_CharacterSize * m_CharacterSizeModifier * 0.01f);
		}
		if (m_ResizeToFit && !flag)
		{
			ResizeTextToFit(text);
		}
		base.transform.rotation = rotation;
		if (num && m_Cache && !flag && (m_WordWrap || m_ResizeToFit))
		{
			double result = 0.0;
			if (!double.TryParse(m_Text, out result) && m_Text.Length > 3)
			{
				s_CachedText[m_CacheHash] = new CachedTextValues();
				s_CachedText[m_CacheHash].m_Text = m_UberTextRendering.GetText();
				s_CachedText[m_CacheHash].m_CharSize = m_UberTextRendering.GetCharacterSize();
				s_CachedText[m_CacheHash].m_OriginalTextHash = m_Text.GetHashCode();
			}
		}
		float num2 = 1f;
		float unboundedCharSize = UberTextLocalization.GetUnboundedCharSize(m_LocalizedSettings);
		if (unboundedCharSize > 0f)
		{
			num2 = unboundedCharSize;
		}
		float characterSize = m_UberTextRendering.GetCharacterSize();
		characterSize *= m_UnboundCharacterSizeModifier * num2;
		m_UberTextRendering.SetCharacterSize(characterSize);
	}

	private void SetupTextMeshAlignment()
	{
		float width = GetWidth();
		float height = GetHeight();
		m_UberTextRendering.SetTextAlignment(m_Alignment);
		m_UberTextRendering.SetTextMeshGameObjectLocalPosition(GetTextCenter());
		m_UberTextRendering.SetTextMeshGameObjectLocalPositionOffset(UberTextLocalization.GetPositionOffset(m_LocalizedSettings));
		switch (m_Alignment)
		{
		case AlignmentOptions.Center:
			switch (m_Anchor)
			{
			case AnchorOptions.Upper:
				m_UberTextRendering.SetTextMeshGameObjectLocalPositionOffset(GetAlignmentOffset(0f, height * 0.5f));
				m_UberTextRendering.SetTextAnchor(TextAnchor.UpperCenter);
				break;
			case AnchorOptions.Middle:
				m_UberTextRendering.SetTextAnchor(TextAnchor.MiddleCenter);
				break;
			case AnchorOptions.Lower:
				m_UberTextRendering.SetTextMeshGameObjectLocalPositionOffset(GetAlignmentOffset(0f, (0f - height) * 0.5f));
				m_UberTextRendering.SetTextAnchor(TextAnchor.LowerCenter);
				break;
			}
			break;
		case AlignmentOptions.Left:
			switch (m_Anchor)
			{
			case AnchorOptions.Upper:
				m_UberTextRendering.SetTextMeshGameObjectLocalPositionOffset(GetAlignmentOffset((0f - width) * 0.5f, height * 0.5f));
				m_UberTextRendering.SetTextAnchor(TextAnchor.UpperLeft);
				break;
			case AnchorOptions.Middle:
				m_UberTextRendering.SetTextMeshGameObjectLocalPositionOffset(GetAlignmentOffset((0f - width) * 0.5f, 0f));
				m_UberTextRendering.SetTextAnchor(TextAnchor.MiddleLeft);
				break;
			case AnchorOptions.Lower:
				m_UberTextRendering.SetTextMeshGameObjectLocalPositionOffset(GetAlignmentOffset((0f - width) * 0.5f, (0f - height) * 0.5f));
				m_UberTextRendering.SetTextAnchor(TextAnchor.LowerLeft);
				break;
			}
			break;
		case AlignmentOptions.Right:
			switch (m_Anchor)
			{
			case AnchorOptions.Upper:
				m_UberTextRendering.SetTextMeshGameObjectLocalPositionOffset(GetAlignmentOffset(width * 0.5f, height * 0.5f));
				m_UberTextRendering.SetTextAnchor(TextAnchor.UpperRight);
				break;
			case AnchorOptions.Middle:
				m_UberTextRendering.SetTextMeshGameObjectLocalPositionOffset(GetAlignmentOffset(width * 0.5f, 0f));
				m_UberTextRendering.SetTextAnchor(TextAnchor.MiddleRight);
				break;
			case AnchorOptions.Lower:
				m_UberTextRendering.SetTextMeshGameObjectLocalPositionOffset(GetAlignmentOffset(width * 0.5f, (0f - height) * 0.5f));
				m_UberTextRendering.SetTextAnchor(TextAnchor.LowerRight);
				break;
			}
			break;
		}
	}

	private Vector3 GetAlignmentOffset(float width, float height)
	{
		if (!IsPartOfWidget)
		{
			return new Vector3(width, height, 0f);
		}
		return new Vector3(width, 0f, height);
	}

	private void UpdateTextPosition()
	{
		if (m_RenderToTexture)
		{
			m_UberTextRendering.SetTextMeshRenderToTextureTransform(Offset);
		}
		else
		{
			m_UberTextRendering.SetTextMeshTransform(base.gameObject, GetTextCenter(), GetTextRotation());
		}
	}

	private void UpdateColor()
	{
		m_updated = false;
		if (m_UberTextRendering != null && m_UberTextRenderToTexture != null)
		{
			m_UberTextRendering.SetTextColor(m_TextColor);
			m_UberTextRendering.SetOutlineColor(m_OutlineColor);
			m_UberTextRendering.SetAmbientLightBlend(m_AmbientLightBlend);
			m_UberTextRenderToTexture.SetShadowColor(m_ShadowColor);
			m_UberTextRenderToTexture.SetGradientColors(m_GradientUpperColor, m_GradientLowerColor);
		}
	}

	private void UpdateLayers()
	{
		m_CurrentLayer = base.gameObject.layer;
		if (m_UberTextRendering != null)
		{
			m_UberTextRendering.SetLayer(m_CurrentLayer);
		}
		if (m_UberTextRenderToTexture != null)
		{
			m_UberTextRenderToTexture.SetLayer(m_CurrentLayer);
		}
	}

	private void UpdateRenderQueue()
	{
		if (m_UberTextRendering != null && m_UberTextRenderToTexture != null)
		{
			m_UberTextRendering.SetRenderQueueIncrement(m_RenderQueue);
			m_UberTextRenderToTexture.SetRenderQueueIncrement(m_RenderQueue);
			m_UberTextRenderToTexture.SetShadowRenderQueueIncrement(m_RenderQueue + m_ShadowRenderQueueOffset);
		}
	}

	private void UpdateTexelSize()
	{
		float num = m_OutlineSize * m_OutlineModifier * UberTextLocalization.GetOutlineModifier(m_LocalizedSettings);
		Texture fontTexture = m_UberTextRendering.GetFontTexture();
		if (fontTexture == null)
		{
			if (Application.IsPlaying(this))
			{
				Debug.LogError($"UberText.UpdateTexelSize() - m_FontTexture == null!  text={m_Text}");
			}
			return;
		}
		Vector2 vector = TexelSize(fontTexture);
		if (!(vector == m_PreviousTexelSize))
		{
			m_UberTextRendering.SetBoldOffset(vector * m_BoldSize);
			m_UberTextRendering.SetOutlineOffset(vector * num);
			m_UberTextRendering.SetOutlineBoldOffset(vector * (num + m_BoldSize * 0.75f));
			m_UberTextRendering.SetTexelSize(vector);
			m_PreviousTexelSize = vector;
			m_PreviousFontSize = new Vector2Int(fontTexture.width, fontTexture.height);
		}
	}

	private void CreateTextMesh()
	{
		m_UberTextRendering.Init(base.gameObject);
		m_meshReadyTracker.SetAndDispatch();
		m_UberTextRendering.SetRichText(m_RichText);
		m_HasBoldText = false;
		if (m_Text.Contains("<b>"))
		{
			Bold();
		}
	}

	private void SetFont()
	{
		if (m_Font == null)
		{
			return;
		}
		if (!m_isFontDefLoaded && HearthstoneServices.TryGet<FontTable>(out var service))
		{
			FontDefinition fontDef = service.GetFontDef(m_Font);
			if (fontDef != null)
			{
				m_LocalizedFont = fontDef.m_Font;
				m_LineSpaceModifier = fontDef.m_LineSpaceModifier;
				m_FontSizeModifier = fontDef.m_FontSizeModifier;
				m_SingleLineAdjustment = fontDef.m_SingleLineAdjustment;
				m_CharacterSizeModifier = fontDef.m_CharacterSizeModifier;
				m_UnboundCharacterSizeModifier = fontDef.m_UnboundCharacterSizeModifier;
				m_OutlineModifier = fontDef.m_OutlineModifier;
				m_isFontDefLoaded = true;
			}
			else
			{
				Debug.LogErrorFormat("Error loading fontDef for UberText component={0} font={1}", base.name, m_Font);
			}
		}
		if (m_UberTextRendering != null)
		{
			if (m_LocalizedFont == null)
			{
				m_UberTextRendering.SetFont(m_Font);
			}
			else
			{
				m_UberTextRendering.SetFont(m_LocalizedFont);
			}
		}
	}

	private void SetFontSize(int fontSize)
	{
		fontSize = (int)(m_FontSizeModifier * UberTextLocalization.GetFontSizeModifier(m_LocalizedSettings) * (float)fontSize);
		m_UberTextRendering.SetFontSize(fontSize);
	}

	private float GetWidth()
	{
		float width = m_Width;
		if (IsPartOfWidget)
		{
			width = m_widgetTransform.Rect.width;
		}
		if (m_LocalizedSettings != null)
		{
			LocalizationSettings.LocaleAdjustment locale = m_LocalizedSettings.GetLocale(Localization.GetLocale());
			if (locale != null && locale.m_Width > 0f)
			{
				width = locale.m_Width;
			}
		}
		return width;
	}

	private float GetHeight()
	{
		float height = m_Height;
		if (IsPartOfWidget)
		{
			height = m_widgetTransform.Rect.height;
		}
		if (m_LocalizedSettings != null)
		{
			LocalizationSettings.LocaleAdjustment locale = m_LocalizedSettings.GetLocale(Localization.GetLocale());
			if (locale != null && locale.m_Height > 0f)
			{
				height = locale.m_Height;
			}
		}
		return height;
	}

	private void SetLineSpacing(float lineSpacing)
	{
		lineSpacing = ((LineCount(m_UberTextRendering.GetText()) != 1) ? (lineSpacing * (m_LineSpaceModifier * UberTextLocalization.GetLineSpaceModifier(m_LocalizedSettings))) : (lineSpacing + (m_SingleLineAdjustment + UberTextLocalization.GetSingleLineAdjustment(m_LocalizedSettings))));
		m_UberTextRendering.SetLineSpacing(lineSpacing);
	}

	private void SetupRenderToTexture()
	{
		m_UberTextRendering.SetTextMeshRenderToTextureTransform(Offset);
		Vector2Int renderTextureSize = CalcTextureSize();
		if ((bool)m_RenderOnObject)
		{
			m_UberTextRenderToTexture.InitOnRenderObject(m_RenderOnObject, renderTextureSize);
		}
		else
		{
			SetupRenderOnPlane(renderTextureSize);
			SetupShadow();
		}
		SetupCamera();
		SetupAntialias();
	}

	private void SetupRenderOnPlane(Vector2Int renderTextureSize)
	{
		Vector2 planeSize = new Vector2(GetWidth(), GetHeight());
		m_UberTextRenderToTexture.InitOnPlane(base.gameObject, planeSize, m_GradientUpperColor, m_GradientLowerColor, renderTextureSize);
		Vector3 zero = Vector3.zero;
		if (m_widgetTransform == null)
		{
			zero.x = -90f;
		}
		m_UberTextRenderToTexture.SetPlaneLocalPosition(GetTextCenter() + UberTextLocalization.GetPositionOffset(m_LocalizedSettings));
		m_UberTextRenderToTexture.SetPlaneRotation(base.transform.rotation);
		m_UberTextRenderToTexture.DoPlaneRotate(zero);
	}

	private void SetupAntialias()
	{
		if (m_AntiAlias)
		{
			m_UberTextRenderToTexture.SetAntialiasEdge(m_AntiAliasEdge);
			m_UberTextRenderToTexture.SetAntialiasOffset(m_AntiAliasAmount);
		}
	}

	private void SetupCamera()
	{
		Color cameraBackgroundColor = m_TextColor;
		if (m_Outline)
		{
			cameraBackgroundColor = m_OutlineColor;
		}
		m_UberTextRenderToTexture.SetCameraBackgroundColor(cameraBackgroundColor);
		m_UberTextRenderToTexture.SetCameraPosition(Alignment, m_Anchor, new Vector2(GetWidth(), GetHeight()), Offset);
	}

	private void ResizeTextToFit(string text)
	{
		if (text != null && !(text == string.Empty))
		{
			UberTextRendering.TransformBackup transformBackup = m_UberTextRendering.BackupTextMeshTransform();
			m_UberTextRendering.SetTextMeshGameObjectParent(null);
			m_UberTextRendering.SetTextMeshGameObjectLocalScale(Vector3.one);
			m_UberTextRendering.SetTextMeshGameObjectRotation(Quaternion.identity);
			float width = GetWidth();
			string trailingTag;
			string text2 = RemoveTagsFromWord(text, out trailingTag, m_RichText);
			if (text2 == null)
			{
				text2 = string.Empty;
			}
			m_UberTextRendering.SetText(text2);
			if (m_WordWrap)
			{
				m_UberTextRendering.SetText(WordWrapString(text, width));
			}
			if (m_ResizeToFitAndGrow)
			{
				ResizeToFitBounds_CharSize(text);
			}
			else
			{
				ReduceText_CharSize(text);
			}
			m_UberTextRendering.SetTextMeshGameObjectParent(transformBackup.Parent);
			m_UberTextRendering.SetTextMeshGameObjectLocalPosition(transformBackup.LocalPosition);
			m_UberTextRendering.SetTextMeshGameObjectLocalScale(transformBackup.LocalScale);
			m_UberTextRendering.SetTextMeshGameObjectRotation(transformBackup.Rotation);
			if (!m_WordWrap)
			{
				m_UberTextRendering.SetText(text);
			}
		}
	}

	private void ReduceText(string text, int step, int newSize)
	{
		if (m_FontSize == 1)
		{
			return;
		}
		SetFontSize(newSize);
		float num = GetHeight();
		float num2 = GetWidth();
		if (!m_RenderToTexture)
		{
			num = m_WorldHeight;
			num2 = m_WorldWidth;
		}
		if (!IsMultiLine())
		{
			SetLineSpacing(0f);
		}
		Bounds textMeshBounds = m_UberTextRendering.GetTextMeshBounds();
		float y = textMeshBounds.size.y;
		float x = textMeshBounds.size.x;
		int num3 = 0;
		while (y > num || x > num2)
		{
			num3++;
			if (num3 > 40)
			{
				break;
			}
			newSize -= step;
			if (newSize < m_MinFontSize)
			{
				newSize = m_MinFontSize;
				break;
			}
			SetFontSize(newSize);
			if (m_WordWrap)
			{
				m_UberTextRendering.SetText(WordWrapString(text, num2));
			}
			textMeshBounds = m_UberTextRendering.GetTextMeshBounds();
			y = textMeshBounds.size.y;
			x = textMeshBounds.size.x;
		}
		if (!IsMultiLine())
		{
			SetLineSpacing(m_LineSpacing);
		}
		m_FontSize = newSize;
	}

	private float Measure_IntraLine_Height()
	{
		string text = m_UberTextRendering.GetText();
		m_UberTextRendering.SetText("|");
		float y = m_UberTextRendering.GetTextMeshBounds().size.y;
		m_UberTextRendering.SetText("|\n|");
		float result = m_UberTextRendering.GetTextMeshBounds().size.y - y * 2f;
		m_UberTextRendering.SetText(text);
		return result;
	}

	private void ReduceText_CharSize(string text)
	{
		float height = GetHeight();
		float width = GetWidth();
		float num = m_UberTextRendering.GetCharacterSize();
		if (!IsMultiLine())
		{
			SetLineSpacing(0f);
		}
		else
		{
			SetLineSpacing(m_LineSpacing);
		}
		Bounds textMeshBounds = m_UberTextRendering.GetTextMeshBounds();
		float x = textMeshBounds.size.x;
		float y = textMeshBounds.size.y;
		int num2 = 0;
		y -= Measure_IntraLine_Height();
		float num3 = 1f;
		if (m_LocalizedSettings != null)
		{
			LocalizationSettings.LocaleAdjustment locale = m_LocalizedSettings.GetLocale(Localization.GetLocale());
			if (locale != null && locale.m_ResizeToFitWidthModifier > 0f)
			{
				num3 = locale.m_ResizeToFitWidthModifier;
			}
		}
		while (y > height || x > width * num3)
		{
			num2++;
			if (num2 > 40)
			{
				break;
			}
			num *= 0.95f;
			if (num <= m_MinCharacterSize * 0.01f)
			{
				num = m_MinCharacterSize * 0.01f;
				m_UberTextRendering.SetCharacterSize(num);
				if (m_WordWrap)
				{
					m_UberTextRendering.SetText(WordWrapString(text, width, ellipses: true));
				}
				break;
			}
			m_UberTextRendering.SetCharacterSize(num);
			if (m_WordWrap)
			{
				m_UberTextRendering.SetText(WordWrapString(text, width, ellipses: false));
			}
			if (LineCount(m_UberTextRendering.GetText()) > 1)
			{
				SetLineSpacing(m_LineSpacing);
			}
			else
			{
				SetLineSpacing(0f);
			}
			textMeshBounds = m_UberTextRendering.GetTextMeshBounds();
			x = textMeshBounds.size.x;
			y = textMeshBounds.size.y;
		}
		SetLineSpacing(m_LineSpacing);
	}

	private void ResizeToFitBounds_CharSize(string text)
	{
		float height = GetHeight();
		float width = GetWidth();
		float num = m_UberTextRendering.GetCharacterSize();
		SetLineSpacing(IsMultiLine() ? m_LineSpacing : 0f);
		Bounds textMeshBounds = m_UberTextRendering.GetTextMeshBounds();
		float x = textMeshBounds.size.x;
		float y = textMeshBounds.size.y;
		float num2 = y * 0.01f;
		float num3 = x * 0.01f;
		int num4 = 0;
		bool flag = x - num3 < width;
		bool flag2 = x + num3 > width;
		bool flag3 = y - num2 < height;
		bool flag4 = y + num2 > height;
		if (!m_WordWrap && (flag || flag2))
		{
			float val = height / y;
			float val2 = width / x;
			float num5 = Math.Min(val, val2);
			num *= num5;
			if (num <= m_MinCharacterSize * 0.01f)
			{
				num = m_MinCharacterSize * 0.01f;
			}
			m_UberTextRendering.SetCharacterSize(num);
		}
		while (m_WordWrap && (flag3 || flag4))
		{
			num4++;
			if (num4 > 40)
			{
				break;
			}
			float num6 = (flag3 ? (1f + m_ResizeToGrowStep) : (1f - m_ResizeToGrowStep));
			bool flag5 = false;
			string text2 = m_UberTextRendering.GetText();
			int num7 = LineCount(text2);
			num *= num6;
			if (num <= m_MinCharacterSize * 0.01f)
			{
				num = m_MinCharacterSize * 0.01f;
				flag5 = true;
			}
			m_UberTextRendering.SetCharacterSize(num);
			m_UberTextRendering.SetText(WordWrapString(text, width, ellipses: false));
			SetLineSpacing(IsMultiLine() ? m_LineSpacing : 0f);
			textMeshBounds = m_UberTextRendering.GetTextMeshBounds();
			x = textMeshBounds.size.x;
			y = textMeshBounds.size.y;
			num2 = y * 0.01f;
			bool num8 = num7 < LineCount(m_UberTextRendering.GetText());
			bool flag6 = !flag3 && y - num2 < height;
			bool flag7 = flag3 && y + num2 > height;
			if (num8 && flag7)
			{
				m_UberTextRendering.SetText(text2);
			}
			if (flag5 || flag6 || flag7)
			{
				break;
			}
		}
		SetLineSpacing(m_LineSpacing);
	}

	private Vector3 GetTextCenter()
	{
		if (IsPartOfWidget)
		{
			Vector3 vector = m_widgetTransform.Rect.center;
			return WidgetTransform.GetRotationMatrixFromZNegativeToDesiredFacing(m_widgetTransform.Facing) * vector;
		}
		return Vector3.zero;
	}

	private Quaternion GetTextRotation()
	{
		if (IsPartOfWidget)
		{
			return WidgetTransform.GetRotationFromZNegativeToDesiredFacing(m_widgetTransform.Facing);
		}
		return Quaternion.identity;
	}

	private void UpdateWordWrapSettings(float containerWidth, float underwearWidthLocalAdjustment, float underwearHeightLocalAdjustment, bool ellipsis)
	{
		m_wordWrapSettings.Clear();
		m_wordWrapSettings.OriginalWidth = containerWidth;
		m_wordWrapSettings.Width = GetWidth();
		m_wordWrapSettings.Height = GetHeight();
		m_wordWrapSettings.UseUnderwear = m_Underwear;
		m_wordWrapSettings.UnderwearWidth = m_UnderwearWidth;
		m_wordWrapSettings.UnderwearHeight = m_UnderwearHeight;
		m_wordWrapSettings.UnderwearWidthLocaleAdjustment = underwearWidthLocalAdjustment;
		m_wordWrapSettings.UnderwearHeightLocaleAdjustment = underwearHeightLocalAdjustment;
		m_wordWrapSettings.UnderwearFlip = m_UnderwearFlip;
		m_wordWrapSettings.Ellipsized = ellipsis;
		m_wordWrapSettings.RichText = m_RichText;
		m_wordWrapSettings.ResizeToFit = m_ResizeToFit;
		m_wordWrapSettings.ForceWrapLargeWords = m_ForceWrapLargeWords;
		m_wordWrapSettings.Alignment = m_Alignment;
	}

	private void AdjustUnderwearForLocale(out float localizedUnderwearWidth, out float localizedUnderwearHeight, float width)
	{
		localizedUnderwearWidth = m_UnderwearWidth;
		localizedUnderwearHeight = m_UnderwearHeight;
		LocalizationSettings.LocaleAdjustment localeAdjustment = m_LocalizedSettings?.GetLocale(Localization.GetLocale());
		if (localeAdjustment != null)
		{
			if (localeAdjustment.m_UnderwearWidth > 0f)
			{
				localizedUnderwearWidth = localeAdjustment.m_UnderwearWidth;
			}
			if (localeAdjustment.m_UnderwearHeight > 0f)
			{
				localizedUnderwearHeight = localeAdjustment.m_UnderwearHeight;
			}
		}
		float height = GetHeight();
		if (m_UnderwearFlip)
		{
			localizedUnderwearHeight = height * localizedUnderwearHeight;
		}
		else
		{
			localizedUnderwearHeight = height * (1f - localizedUnderwearHeight);
		}
		localizedUnderwearWidth = width * (1f - localizedUnderwearWidth);
	}

	private string WordWrapString(string text, float width, bool ellipses)
	{
		AdjustUnderwearForLocale(out var localizedUnderwearWidth, out var localizedUnderwearHeight, width);
		UpdateWordWrapSettings(width, localizedUnderwearWidth, localizedUnderwearHeight, ellipses);
		return m_UberTextLineWrapper.WordWrapString(text, m_wordWrapSettings, ref m_LineCount, ref m_Ellipsized);
	}

	private string WordWrapString(string text, float width)
	{
		return WordWrapString(text, width, ellipses: false);
	}

	private string ProcessText(string text)
	{
		if (!m_RichText)
		{
			return text;
		}
		if (!m_WordWrap)
		{
			text = RemoveLineBreakTagsHardSpace(text);
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("<material=1></material>");
		stringBuilder.Append(text);
		for (int i = 0; i < text.Length; i++)
		{
			if (text[i] == '<' && i <= text.Length - 2 && text[i + 1] == 'b' && (i + 4 >= text.Length || text[i + 3] != '<' || text[i + 4] != '/'))
			{
				Bold();
				stringBuilder.Replace("<b>", "<material=1>");
				stringBuilder.Replace("</b>", "</material>");
				i++;
			}
		}
		string text2 = stringBuilder.ToString();
		if (text2 == null)
		{
			Debug.LogWarning("UberText: ProcessText returned a null string!");
			text2 = string.Empty;
		}
		return text2;
	}

	private string LocalizationFixes(string text)
	{
		if (Localization.GetLocale() == Locale.thTH)
		{
			return FixThai(text);
		}
		return text;
	}

	private void SetupShadow()
	{
		if (!m_Shadow)
		{
			m_UberTextRenderToTexture.RemoveShadow();
			return;
		}
		float num = (0f - m_ShadowOffset) * 0.01f;
		float y = m_ShadowDepthOffset * 0.01f;
		m_UberTextRenderToTexture.InitShadow(base.gameObject, new Vector3(num, y, num));
		m_UberTextRenderToTexture.SetShadowColor(m_ShadowColor);
		m_UberTextRenderToTexture.SetShadowOffset(m_ShadowBlur);
	}

	private void UpdateOutlineProperties()
	{
		if (m_Outline)
		{
			Vector2 vector = TexelSize(m_UberTextRendering.GetFontTexture());
			float num = m_OutlineSize * m_OutlineModifier * UberTextLocalization.GetOutlineModifier(m_LocalizedSettings);
			Vector2 outlineBoldOffset = vector * (num + m_BoldSize * 0.75f);
			m_UberTextRendering.SetOutlineOffset(vector * num);
			m_UberTextRendering.SetOutlineBoldOffset(outlineBoldOffset);
			m_UberTextRendering.SetTexelSize(vector);
			m_UberTextRendering.SetTextColor(m_TextColor);
			m_UberTextRendering.SetOutlineColor(m_OutlineColor);
		}
	}

	private void Bold()
	{
		if (!m_HasBoldText)
		{
			m_HasBoldText = true;
			if (m_BoldSize > 10f)
			{
				m_BoldSize = 10f;
			}
			Vector2 vector = TexelSize(m_UberTextRendering.GetFontTexture());
			m_UberTextRendering.SetBoldOffset(vector * m_BoldSize);
		}
	}

	public void EnablePopupRendering(PopupRoot popupRoot)
	{
		m_popupRoot = popupRoot;
		m_meshReadyTracker.RegisterSetListener(ApplyPopupRendering);
	}

	public void DisablePopupRendering()
	{
		m_popupRoot = null;
		m_meshReadyTracker.RemoveSetListener(ApplyPopupRendering);
		RemovePopupRendering();
	}

	public bool ShouldPropagatePopupRendering()
	{
		return false;
	}

	private void ApplyPopupRendering(object unused)
	{
		if (m_UberTextRendering.GetTextMeshRenderer() != null && m_popupRoot != null)
		{
			GameObject gameObject = base.gameObject;
			if (!m_popupRenderingEnabled)
			{
				m_originalLayerBeforePopupRendering = gameObject.layer;
			}
			gameObject.layer = 29;
			UpdateLayers();
			m_popupRenderingEnabled = true;
			PopupRenderer popupRenderer = m_UberTextRendering.GetPopupRenderer();
			if ((bool)popupRenderer)
			{
				popupRenderer.EnablePopupRendering(m_popupRoot);
			}
			PopupRenderer popupRenderer2 = m_UberTextRenderToTexture.GetPopupRenderer();
			if ((bool)popupRenderer2)
			{
				popupRenderer2.EnablePopupRendering(m_popupRoot);
			}
		}
	}

	private void RemovePopupRendering()
	{
		if (m_UberTextRendering != null && m_UberTextRendering.GetTextMeshRenderer() != null)
		{
			m_popupRenderingEnabled = false;
			base.gameObject.layer = m_originalLayerBeforePopupRendering;
			UpdateLayers();
			m_UberTextRendering.DisablePopupRenderer();
			m_UberTextRenderToTexture.DisablePopupRenderer();
		}
	}

	public static void DisableCache()
	{
		s_disableCache = true;
		s_CachedText.Clear();
	}

	private void SetWorldWidthAndHeight()
	{
		float width = GetWidth();
		float height = GetHeight();
		Quaternion rotation = base.transform.rotation;
		base.transform.rotation = Quaternion.identity;
		Vector3 lossyScale = base.transform.lossyScale;
		float worldWidth = width;
		if (lossyScale.x > 0f)
		{
			worldWidth = width * lossyScale.x;
		}
		float worldHeight = height;
		if (lossyScale.y > 0f)
		{
			worldHeight = height * lossyScale.y;
		}
		base.transform.rotation = rotation;
		m_WorldWidth = worldWidth;
		m_WorldHeight = worldHeight;
	}

	public static Vector3 GetWorldScale(Transform xform)
	{
		Vector3 localScale = xform.localScale;
		if (xform.parent != null)
		{
			Transform parent = xform.parent;
			while (parent != null)
			{
				localScale.Scale(parent.localScale);
				parent = parent.parent;
			}
		}
		return localScale;
	}

	private Vector3 GetLossyWorldScale(Transform xform)
	{
		Quaternion rotation = xform.rotation;
		xform.rotation = Quaternion.identity;
		Vector3 lossyScale = base.transform.lossyScale;
		xform.rotation = rotation;
		return lossyScale;
	}

	private Vector2Int CalcTextureSize()
	{
		float width = GetWidth();
		float height = GetHeight();
		Vector2 vector = new Vector2(m_Resolution, m_Resolution);
		if (width > height)
		{
			vector.x = m_Resolution;
			vector.y = (float)m_Resolution * (height / width);
		}
		else
		{
			vector.x = (float)m_Resolution * (width / height);
			vector.y = m_Resolution;
		}
		if (HearthstoneServices.TryGet<GraphicsManager>(out var service) && service.RenderQualityLevel == GraphicsQuality.Low)
		{
			vector.x *= 0.75f;
			vector.y *= 0.75f;
		}
		return new Vector2Int((int)vector.x, (int)vector.y);
	}

	public static string RemoveTagsFromWord(string word, out string trailingTag, bool richText)
	{
		trailingTag = null;
		if (!richText)
		{
			return word;
		}
		bool flag = false;
		foreach (char c in word)
		{
			if (c == '<' || c == '[')
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			return word;
		}
		StringBuilder stringBuilder = new StringBuilder(0, word.Length);
		bool flag2 = false;
		for (int j = 0; j < word.Length; j++)
		{
			if (word[j] == '<')
			{
				if (j < word.Length - 1)
				{
					flag2 = true;
					if (j != 0 && word[j - 1] != ' ')
					{
						trailingTag = word.Substring(j);
					}
				}
				continue;
			}
			if (word[j] == '>')
			{
				flag2 = false;
				continue;
			}
			if (word[j] == '[' && j + 2 < word.Length && IsValidSquareBracketTag(word[j + 1]) && word[j + 2] == ']')
			{
				flag2 = true;
				continue;
			}
			if (word[j] == ']')
			{
				if (j - 2 >= 0 && IsValidSquareBracketTag(word[j - 1]) && word[j - 2] == '[')
				{
					flag2 = false;
					continue;
				}
				flag2 = false;
			}
			if (!flag2)
			{
				stringBuilder.Append(word[j]);
			}
		}
		return stringBuilder.ToString();
	}

	public static string RemoveLineBreakTagsHardSpace(string text)
	{
		StringBuilder stringBuilder = new StringBuilder(text.Length, text.Length);
		bool flag = false;
		for (int i = 0; i < text.Length; i++)
		{
			if (text[i] == '[' && i + 2 < text.Length && IsValidSquareBracketTag(text[i + 1]) && text[i + 2] == ']')
			{
				flag = true;
				continue;
			}
			if (text[i] == ']')
			{
				if (i - 2 >= 0 && IsValidSquareBracketTag(text[i - 1]) && text[i - 2] == '[')
				{
					flag = false;
					continue;
				}
				flag = false;
			}
			if (!flag)
			{
				if (text[i] == '_')
				{
					stringBuilder.Append(' ');
				}
				else
				{
					stringBuilder.Append(text[i]);
				}
			}
		}
		return stringBuilder.ToString();
	}

	private static bool IsValidSquareBracketTag(char ch)
	{
		if (ch == 'b' || ch == 'd' || ch == 'x')
		{
			return true;
		}
		return false;
	}

	private static bool IsWhitespaceOrUnderscore(char ch)
	{
		switch (ch)
		{
		case '\t':
		case '\n':
		case '\r':
		case ' ':
		case '_':
			return true;
		default:
			return false;
		}
	}

	public static string RemoveMarkupAndCollapseWhitespaces(string text)
	{
		if (text == null)
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = null;
		int num = 0;
		int startIndex = 0;
		int num2 = text.IndexOfAny(STRIP_CHARS_INDEX_OF_ANY, startIndex);
		while (num2 >= 0 && num2 < text.Length)
		{
			startIndex = num2 + 1;
			if ((text[num2] != ' ' || (num2 + 1 < text.Length && IsWhitespaceOrUnderscore(text[num2 + 1]))) && (text[num2] != '[' || (num2 + 2 < text.Length && IsValidSquareBracketTag(text[num2 + 1]) && text[num2 + 2] == ']')))
			{
				if (num2 > num)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					string value = text.Substring(num, num2 - num);
					stringBuilder.Append(value);
					num = startIndex;
				}
				if (IsWhitespaceOrUnderscore(text[num2]))
				{
					for (; startIndex < text.Length && IsWhitespaceOrUnderscore(text[startIndex]); startIndex++)
					{
					}
					if ((text[num2] != '\n' && text[num2] != '\r') || num2 <= 0 || text[num2 - 1] != '-')
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
						}
						stringBuilder.Append(' ');
					}
					num = startIndex;
				}
				else
				{
					char value2 = ((text[num2] == '<') ? '>' : ']');
					int num3 = text.IndexOf(value2, startIndex);
					if (num3 < 0)
					{
						startIndex = text.Length;
						num = startIndex;
						break;
					}
					startIndex = num3 + 1;
					num = startIndex;
				}
			}
			num2 = text.IndexOfAny(STRIP_CHARS_INDEX_OF_ANY, startIndex);
		}
		if (num < text.Length && num > 0)
		{
			stringBuilder.Append(text.Substring(num));
		}
		if (stringBuilder == null)
		{
			if (num == 0)
			{
				return text;
			}
			return string.Empty;
		}
		return stringBuilder.ToString();
	}

	private void CleanUp()
	{
		m_Offset = 0f;
		if (m_UberTextRendering != null)
		{
			m_UberTextRendering.Destroy();
		}
		if (m_UberTextRenderToTexture != null)
		{
			m_UberTextRenderToTexture.Destroy();
		}
		m_updated = false;
	}

	public static int LineCount(string s)
	{
		int num = 1;
		for (int i = 0; i < s.Length; i++)
		{
			if (s[i] == '\n')
			{
				num++;
			}
		}
		return num;
	}

	public Vector2 TexelSize(Texture texture)
	{
		if (!texture)
		{
			return Vector2.zero;
		}
		int frameCount = Time.frameCount;
		Font key = m_Font;
		if (m_LocalizedFont != null)
		{
			key = m_LocalizedFont;
		}
		if (s_TexelUpdateFrame.ContainsKey(key) && s_TexelUpdateFrame[key] == frameCount)
		{
			return s_TexelUpdateData[key];
		}
		Vector2 vector = default(Vector2);
		vector.x = 1f / (float)texture.width;
		vector.y = 1f / (float)texture.height;
		s_TexelUpdateFrame[key] = frameCount;
		s_TexelUpdateData[key] = vector;
		return vector;
	}

	private static void DeleteOldCacheFiles()
	{
		foreach (Locale value in Enum.GetValues(typeof(Locale)))
		{
			string text = $"{FileUtils.PersistentDataPath}/text_{value}.cache";
			if (File.Exists(text))
			{
				try
				{
					File.Delete(text);
				}
				catch (Exception ex)
				{
					Debug.LogError($"UberText.DeleteOldCacheFiles() - Failed to delete {text}. Reason={ex.Message}");
				}
			}
		}
	}

	private static string GetCacheFolderPath()
	{
		return $"{FileUtils.CachePath}/UberText";
	}

	private static string GetCacheFilePath()
	{
		return $"{GetCacheFolderPath()}/text_{Localization.GetLocale()}.cache";
	}

	private static void CreateCacheFolder()
	{
		string cacheFolderPath = GetCacheFolderPath();
		if (!Directory.Exists(cacheFolderPath))
		{
			try
			{
				Directory.CreateDirectory(cacheFolderPath);
			}
			catch (Exception ex)
			{
				Debug.LogError($"UberText.CreateCacheFolder() - Failed to create {cacheFolderPath}. Reason={ex.Message}");
			}
		}
	}

	public static void StoreCachedData()
	{
		if (s_disableCache)
		{
			return;
		}
		CreateCacheFolder();
		string cacheFilePath = GetCacheFilePath();
		using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(cacheFilePath, FileMode.Create)))
		{
			int num = 2;
			num = 84593;
			binaryWriter.Write(num);
			foreach (KeyValuePair<int, CachedTextValues> item in s_CachedText)
			{
				binaryWriter.Write(item.Key);
				binaryWriter.Write(item.Value.m_Text);
				binaryWriter.Write(item.Value.m_CharSize);
				binaryWriter.Write(item.Value.m_OriginalTextHash);
			}
		}
		Log.UberText.Print("UberText Cache Stored: " + cacheFilePath);
	}

	public static IEnumerator<IAsyncJobResult> Job_LoadCachedData()
	{
		if (s_disableCache)
		{
			yield break;
		}
		s_CachedText.Clear();
		DeleteOldCacheFiles();
		CreateCacheFolder();
		string cacheFilePath = GetCacheFilePath();
		if (!File.Exists(cacheFilePath))
		{
			yield break;
		}
		int num = 84593;
		try
		{
			using BinaryReader binaryReader = new BinaryReader(File.Open(cacheFilePath, FileMode.Open));
			if (binaryReader.BaseStream.Length == 0L || binaryReader.ReadInt32() != num || binaryReader.PeekChar() == -1)
			{
				yield break;
			}
			while (binaryReader.PeekChar() != -1)
			{
				int key = binaryReader.ReadInt32();
				CachedTextValues cachedTextValues = new CachedTextValues();
				cachedTextValues.m_Text = binaryReader.ReadString();
				cachedTextValues.m_CharSize = binaryReader.ReadSingle();
				cachedTextValues.m_OriginalTextHash = binaryReader.ReadInt32();
				s_CachedText.Add(key, cachedTextValues);
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning($"UberText LoadCachedData() failed: {ex.Message}");
			s_CachedText.Clear();
		}
		if (s_CachedText.Count > 50000)
		{
			s_CachedText.Clear();
		}
		Log.UberText.Print("UberText Cache Loaded: " + s_CachedText.Count);
	}

	private string FixThai(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		char[] array = text.ToCharArray();
		ThaiGlyphType[] array2 = new ThaiGlyphType[array.Count()];
		StringBuilder stringBuilder = new StringBuilder(text);
		for (int i = 0; i < array.Count(); i++)
		{
			char c = array[i];
			if (c < 'ก' || c > 'ฯ')
			{
				switch (c)
				{
				case 'ะ':
				case 'เ':
				case 'แ':
					break;
				case '\u0e48':
				case '\u0e49':
				case '\u0e4a':
				case '\u0e4b':
				case '\u0e4c':
					array2[i] = ThaiGlyphType.TONE_MARK;
					continue;
				default:
					switch (c)
					{
					case '\u0e31':
					case '\u0e34':
					case '\u0e35':
					case '\u0e36':
					case '\u0e37':
					case '\u0e47':
					case '\u0e4d':
						array2[i] = ThaiGlyphType.UPPER;
						break;
					case '\u0e38':
					case '\u0e39':
					case '\u0e3a':
						array2[i] = ThaiGlyphType.LOWER;
						break;
					}
					continue;
				}
			}
			switch (c)
			{
			case 'ป':
			case 'ฝ':
			case 'ฟ':
			case 'ฬ':
				array2[i] = ThaiGlyphType.BASE_ASCENDER;
				break;
			case 'ฎ':
			case 'ฏ':
				array2[i] = ThaiGlyphType.BASE_DESCENDER;
				break;
			default:
				array2[i] = ThaiGlyphType.BASE;
				break;
			}
		}
		for (int j = 0; j < array.Count(); j++)
		{
			char c2 = array[j];
			ThaiGlyphType thaiGlyphType = array2[j];
			stringBuilder[j] = c2;
			if (j < 1)
			{
				continue;
			}
			ThaiGlyphType thaiGlyphType2 = array2[j - 1];
			char c3 = array[j - 1];
			if (thaiGlyphType == ThaiGlyphType.UPPER && thaiGlyphType2 == ThaiGlyphType.BASE_ASCENDER)
			{
				switch (c2)
				{
				case '\u0e31':
					stringBuilder[j] = '\uf710';
					break;
				case '\u0e34':
					stringBuilder[j] = '\uf701';
					break;
				case '\u0e35':
					stringBuilder[j] = '\uf702';
					break;
				case '\u0e36':
					stringBuilder[j] = '\uf703';
					break;
				case '\u0e37':
					stringBuilder[j] = '\uf704';
					break;
				case '\u0e4d':
					stringBuilder[j] = '\uf711';
					break;
				case '\u0e47':
					stringBuilder[j] = '\uf712';
					break;
				}
				continue;
			}
			if (thaiGlyphType == ThaiGlyphType.LOWER && thaiGlyphType2 == ThaiGlyphType.BASE_DESCENDER)
			{
				stringBuilder[j] = (char)(c2 + 59616);
				continue;
			}
			if (thaiGlyphType == ThaiGlyphType.LOWER)
			{
				switch (c3)
				{
				case 'ญ':
					stringBuilder[j - 1] = '\uf70f';
					continue;
				case 'ฐ':
					stringBuilder[j - 1] = '\uf700';
					continue;
				}
			}
			if (thaiGlyphType != ThaiGlyphType.TONE_MARK)
			{
				continue;
			}
			if (j - 2 >= 0)
			{
				if (thaiGlyphType2 == ThaiGlyphType.UPPER && array2[j - 2] == ThaiGlyphType.BASE_ASCENDER)
				{
					stringBuilder[j] = (char)(c2 + 59595);
				}
				if (thaiGlyphType2 == ThaiGlyphType.LOWER && j > 1)
				{
					thaiGlyphType2 = array2[j - 2];
					c3 = array[j - 2];
				}
			}
			if (j < array.Count() - 1 && (array[j + 1] == 'ำ' || array[j + 1] == '\u0e4d'))
			{
				if (thaiGlyphType2 == ThaiGlyphType.BASE_ASCENDER)
				{
					stringBuilder[j] = (char)(c2 + 59595);
					stringBuilder.Insert(j + 1, '\uf711');
					stringBuilder.Insert(j + 2, c2);
					if (array[j + 1] == 'ำ')
					{
						stringBuilder[j + 1] = 'ำ';
					}
					j++;
					continue;
				}
			}
			else if (thaiGlyphType2 == ThaiGlyphType.BASE || thaiGlyphType2 == ThaiGlyphType.BASE_DESCENDER)
			{
				stringBuilder[j] = (char)(c2 + 59586);
				continue;
			}
			if (thaiGlyphType2 == ThaiGlyphType.BASE_ASCENDER)
			{
				stringBuilder[j] = (char)(c2 + 59581);
			}
		}
		return stringBuilder.ToString();
	}
}
