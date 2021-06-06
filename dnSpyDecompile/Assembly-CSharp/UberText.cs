using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Blizzard.T5.Jobs;
using Hearthstone.UI;
using Hearthstone.UI.Internal;
using UnityEngine;

// Token: 0x02000AA7 RID: 2727
[ExecuteAlways]
[CustomEditClass]
public class UberText : MonoBehaviour, IPopupRendering
{
	// Token: 0x06009148 RID: 37192 RVA: 0x002F32B4 File Offset: 0x002F14B4
	public static UberText[] EnableAllTextInObject(GameObject obj, bool enable)
	{
		UberText[] componentsInChildren = obj.GetComponentsInChildren<UberText>();
		UberText.EnableAllTextObjects(componentsInChildren, enable);
		return componentsInChildren;
	}

	// Token: 0x06009149 RID: 37193 RVA: 0x002F32C4 File Offset: 0x002F14C4
	public static void EnableAllTextObjects(UberText[] objs, bool enable)
	{
		for (int i = 0; i < objs.Length; i++)
		{
			objs[i].gameObject.SetActive(enable);
		}
	}

	// Token: 0x17000831 RID: 2097
	// (get) Token: 0x0600914A RID: 37194 RVA: 0x002F32EF File Offset: 0x002F14EF
	private bool IsPartOfWidget
	{
		get
		{
			return this.m_widgetTransform != null;
		}
	}

	// Token: 0x17000832 RID: 2098
	// (get) Token: 0x0600914B RID: 37195 RVA: 0x002F32FD File Offset: 0x002F14FD
	protected float Offset
	{
		get
		{
			if (this.m_Offset == 0f)
			{
				UberText.s_offset -= 100f;
				this.m_Offset = UberText.s_offset;
			}
			return this.m_Offset;
		}
	}

	// Token: 0x17000833 RID: 2099
	// (get) Token: 0x0600914C RID: 37196 RVA: 0x002F332D File Offset: 0x002F152D
	// (set) Token: 0x0600914D RID: 37197 RVA: 0x002F3338 File Offset: 0x002F1538
	[Overridable]
	[CustomEditField(Sections = "Text", T = EditType.TEXT_AREA)]
	public string Text
	{
		get
		{
			return this.m_Text;
		}
		set
		{
			this.m_TextSet = true;
			this.m_TextSet = true;
			if (value == this.m_Text)
			{
				return;
			}
			this.m_Text = (value ?? string.Empty);
			if (this.m_Text.Any((char c) => char.IsSurrogate(c)))
			{
				IEnumerable<char> source = (from c in this.m_Text
				where !char.IsLowSurrogate(c)
				select c).Select(delegate(char c)
				{
					if (!char.IsHighSurrogate(c))
					{
						return c;
					}
					return '�';
				});
				this.m_Text = new string(source.ToArray<char>());
			}
			if (this.m_Text == this.m_PreviousText)
			{
				return;
			}
			this.UpdateNow(false);
		}
	}

	// Token: 0x17000834 RID: 2100
	// (get) Token: 0x0600914E RID: 37198 RVA: 0x002F3419 File Offset: 0x002F1619
	// (set) Token: 0x0600914F RID: 37199 RVA: 0x002F3421 File Offset: 0x002F1621
	[CustomEditField(Sections = "Text", HidePredicate = "IsPartOfWidget")]
	public bool GameStringLookup
	{
		get
		{
			return this.m_GameStringLookup;
		}
		set
		{
			if (value == this.m_GameStringLookup)
			{
				return;
			}
			this.m_GameStringLookup = value;
			this.UpdateText();
		}
	}

	// Token: 0x17000835 RID: 2101
	// (get) Token: 0x06009150 RID: 37200 RVA: 0x002F343A File Offset: 0x002F163A
	// (set) Token: 0x06009151 RID: 37201 RVA: 0x002F3442 File Offset: 0x002F1642
	[CustomEditField(Sections = "Text", HidePredicate = "IsPartOfWidget")]
	public bool UseEditorText
	{
		get
		{
			return this.m_UseEditorText;
		}
		set
		{
			if (value == this.m_UseEditorText)
			{
				return;
			}
			this.m_UseEditorText = value;
			this.UpdateText();
		}
	}

	// Token: 0x17000836 RID: 2102
	// (get) Token: 0x06009152 RID: 37202 RVA: 0x002F345B File Offset: 0x002F165B
	// (set) Token: 0x06009153 RID: 37203 RVA: 0x002F3463 File Offset: 0x002F1663
	[CustomEditField(Sections = "Text", HidePredicate = "IsPartOfWidget")]
	public bool Cache
	{
		get
		{
			return this.m_Cache;
		}
		set
		{
			this.m_Cache = value;
		}
	}

	// Token: 0x17000837 RID: 2103
	// (get) Token: 0x06009154 RID: 37204 RVA: 0x002F346C File Offset: 0x002F166C
	// (set) Token: 0x06009155 RID: 37205 RVA: 0x002F3474 File Offset: 0x002F1674
	[CustomEditField(Sections = "Size", HidePredicate = "IsPartOfWidget")]
	public float Width
	{
		get
		{
			return this.m_Width;
		}
		set
		{
			if (value == this.m_Width)
			{
				return;
			}
			this.m_Width = value;
			if (this.m_Width < 0.01f)
			{
				this.m_Width = 0.01f;
			}
			this.UpdateText();
		}
	}

	// Token: 0x17000838 RID: 2104
	// (get) Token: 0x06009156 RID: 37206 RVA: 0x002F34A5 File Offset: 0x002F16A5
	// (set) Token: 0x06009157 RID: 37207 RVA: 0x002F34AD File Offset: 0x002F16AD
	[CustomEditField(Sections = "Size", HidePredicate = "IsPartOfWidget")]
	public float Height
	{
		get
		{
			return this.m_Height;
		}
		set
		{
			if (value == this.m_Height)
			{
				return;
			}
			this.m_Height = value;
			if (this.m_Height < 0.01f)
			{
				this.m_Height = 0.01f;
			}
			this.UpdateText();
		}
	}

	// Token: 0x17000839 RID: 2105
	// (get) Token: 0x06009158 RID: 37208 RVA: 0x002F34DE File Offset: 0x002F16DE
	// (set) Token: 0x06009159 RID: 37209 RVA: 0x002F34E6 File Offset: 0x002F16E6
	[CustomEditField(Sections = "Size")]
	public float LineSpacing
	{
		get
		{
			return this.m_LineSpacing;
		}
		set
		{
			if (value == this.m_LineSpacing)
			{
				return;
			}
			this.m_LineSpacing = value;
			this.UpdateText();
		}
	}

	// Token: 0x1700083A RID: 2106
	// (get) Token: 0x0600915A RID: 37210 RVA: 0x002F34FF File Offset: 0x002F16FF
	// (set) Token: 0x0600915B RID: 37211 RVA: 0x002F3507 File Offset: 0x002F1707
	[CustomEditField(Sections = "Style")]
	public Font TrueTypeFont
	{
		get
		{
			return this.m_Font;
		}
		set
		{
			if (value == this.m_Font)
			{
				return;
			}
			this.m_Font = value;
			this.m_isFontDefLoaded = false;
			this.SetFont();
			this.UpdateText();
		}
	}

	// Token: 0x1700083B RID: 2107
	// (get) Token: 0x0600915C RID: 37212 RVA: 0x002F3532 File Offset: 0x002F1732
	// (set) Token: 0x0600915D RID: 37213 RVA: 0x002F353A File Offset: 0x002F173A
	[CustomEditField(Sections = "Style")]
	public int FontSize
	{
		get
		{
			return this.m_FontSize;
		}
		set
		{
			if (value == this.m_FontSize)
			{
				return;
			}
			this.m_FontSize = value;
			if (this.m_FontSize < 1)
			{
				this.m_FontSize = 1;
			}
			if (this.m_FontSize > 120)
			{
				this.m_FontSize = 120;
			}
			this.UpdateText();
		}
	}

	// Token: 0x1700083C RID: 2108
	// (get) Token: 0x0600915E RID: 37214 RVA: 0x002F3575 File Offset: 0x002F1775
	// (set) Token: 0x0600915F RID: 37215 RVA: 0x002F3580 File Offset: 0x002F1780
	[CustomEditField(Sections = "Style")]
	public int MinFontSize
	{
		get
		{
			return this.m_MinFontSize;
		}
		set
		{
			if (value == this.m_MinFontSize)
			{
				return;
			}
			this.m_MinFontSize = value;
			if (this.m_MinFontSize < 1)
			{
				this.m_MinFontSize = 1;
			}
			if (this.m_MinFontSize > this.m_FontSize)
			{
				this.m_MinFontSize = this.m_FontSize;
			}
			this.UpdateText();
		}
	}

	// Token: 0x1700083D RID: 2109
	// (get) Token: 0x06009160 RID: 37216 RVA: 0x002F35CE File Offset: 0x002F17CE
	// (set) Token: 0x06009161 RID: 37217 RVA: 0x002F35D6 File Offset: 0x002F17D6
	[Overridable]
	[CustomEditField(Sections = "Style")]
	public float CharacterSize
	{
		get
		{
			return this.m_CharacterSize;
		}
		set
		{
			if (value == this.m_CharacterSize)
			{
				return;
			}
			this.m_CharacterSize = value;
			this.UpdateText();
		}
	}

	// Token: 0x1700083E RID: 2110
	// (get) Token: 0x06009162 RID: 37218 RVA: 0x002F35EF File Offset: 0x002F17EF
	// (set) Token: 0x06009163 RID: 37219 RVA: 0x002F35F8 File Offset: 0x002F17F8
	[Overridable]
	[CustomEditField(Sections = "Style")]
	public float MinCharacterSize
	{
		get
		{
			return this.m_MinCharacterSize;
		}
		set
		{
			if (value == this.m_MinCharacterSize)
			{
				return;
			}
			this.m_MinCharacterSize = value;
			if (this.m_MinCharacterSize < 0.001f)
			{
				this.m_MinCharacterSize = 0.001f;
			}
			if (this.m_MinCharacterSize > this.m_CharacterSize)
			{
				this.m_MinCharacterSize = this.m_CharacterSize;
			}
			this.UpdateText();
		}
	}

	// Token: 0x1700083F RID: 2111
	// (get) Token: 0x06009164 RID: 37220 RVA: 0x002F364E File Offset: 0x002F184E
	// (set) Token: 0x06009165 RID: 37221 RVA: 0x002F3656 File Offset: 0x002F1856
	[CustomEditField(Sections = "Style")]
	public bool RichText
	{
		get
		{
			return this.m_RichText;
		}
		set
		{
			if (value == this.m_RichText)
			{
				return;
			}
			this.m_RichText = value;
			this.UpdateText();
		}
	}

	// Token: 0x17000840 RID: 2112
	// (get) Token: 0x06009166 RID: 37222 RVA: 0x002F366F File Offset: 0x002F186F
	// (set) Token: 0x06009167 RID: 37223 RVA: 0x002F3677 File Offset: 0x002F1877
	[Overridable]
	[CustomEditField(Sections = "Style")]
	public Color TextColor
	{
		get
		{
			return this.m_TextColor;
		}
		set
		{
			if (value == this.m_TextColor)
			{
				return;
			}
			this.m_TextColor = value;
			this.UpdateColor();
		}
	}

	// Token: 0x17000841 RID: 2113
	// (get) Token: 0x06009168 RID: 37224 RVA: 0x002F3695 File Offset: 0x002F1895
	// (set) Token: 0x06009169 RID: 37225 RVA: 0x002F36A2 File Offset: 0x002F18A2
	[Overridable]
	[CustomEditField(Hide = true)]
	public float TextAlpha
	{
		get
		{
			return this.m_TextColor.a;
		}
		set
		{
			if (value == this.m_TextColor.a)
			{
				return;
			}
			this.m_TextColor.a = value;
			this.UpdateColor();
		}
	}

	// Token: 0x17000842 RID: 2114
	// (get) Token: 0x0600916A RID: 37226 RVA: 0x002F36C5 File Offset: 0x002F18C5
	// (set) Token: 0x0600916B RID: 37227 RVA: 0x002F36CD File Offset: 0x002F18CD
	[CustomEditField(Sections = "Style")]
	public float BoldSize
	{
		get
		{
			return this.m_BoldSize;
		}
		set
		{
			if (value == this.m_BoldSize)
			{
				return;
			}
			this.m_BoldSize = value;
			this.UpdateText();
		}
	}

	// Token: 0x17000843 RID: 2115
	// (get) Token: 0x0600916C RID: 37228 RVA: 0x002F36E6 File Offset: 0x002F18E6
	// (set) Token: 0x0600916D RID: 37229 RVA: 0x002F36EE File Offset: 0x002F18EE
	[CustomEditField(Sections = "Paragraph")]
	public bool WordWrap
	{
		get
		{
			return this.m_WordWrap;
		}
		set
		{
			if (value == this.m_WordWrap)
			{
				return;
			}
			this.m_WordWrap = value;
			this.UpdateText();
		}
	}

	// Token: 0x17000844 RID: 2116
	// (get) Token: 0x0600916E RID: 37230 RVA: 0x002F3707 File Offset: 0x002F1907
	// (set) Token: 0x0600916F RID: 37231 RVA: 0x002F370F File Offset: 0x002F190F
	[CustomEditField(Sections = "Paragraph")]
	public bool ForceWrapLargeWords
	{
		get
		{
			return this.m_ForceWrapLargeWords;
		}
		set
		{
			if (value == this.m_ForceWrapLargeWords)
			{
				return;
			}
			this.m_ForceWrapLargeWords = value;
			this.UpdateText();
		}
	}

	// Token: 0x17000845 RID: 2117
	// (get) Token: 0x06009170 RID: 37232 RVA: 0x002F3728 File Offset: 0x002F1928
	// (set) Token: 0x06009171 RID: 37233 RVA: 0x002F3730 File Offset: 0x002F1930
	[CustomEditField(Sections = "Paragraph")]
	public bool ResizeToFit
	{
		get
		{
			return this.m_ResizeToFit;
		}
		set
		{
			if (value == this.m_ResizeToFit)
			{
				return;
			}
			this.m_ResizeToFit = value;
			this.UpdateText();
		}
	}

	// Token: 0x17000846 RID: 2118
	// (get) Token: 0x06009172 RID: 37234 RVA: 0x002F3749 File Offset: 0x002F1949
	// (set) Token: 0x06009173 RID: 37235 RVA: 0x002F3751 File Offset: 0x002F1951
	[CustomEditField(Sections = "Paragraph")]
	public bool ResizeToFitAndGrow
	{
		get
		{
			return this.m_ResizeToFitAndGrow;
		}
		set
		{
			if (value == this.m_ResizeToFitAndGrow)
			{
				return;
			}
			this.m_ResizeToFitAndGrow = value;
			this.m_ResizeToFit = (this.m_ResizeToFit || value);
			this.UpdateText();
		}
	}

	// Token: 0x17000847 RID: 2119
	// (get) Token: 0x06009174 RID: 37236 RVA: 0x002F3778 File Offset: 0x002F1978
	// (set) Token: 0x06009175 RID: 37237 RVA: 0x002F3780 File Offset: 0x002F1980
	[CustomEditField(Sections = "Paragraph")]
	public float ResizeToGrowStep
	{
		get
		{
			return this.m_ResizeToGrowStep;
		}
		set
		{
			if (value == this.m_ResizeToGrowStep)
			{
				return;
			}
			this.m_ResizeToGrowStep = Math.Max(0.01f, value);
			this.UpdateText();
		}
	}

	// Token: 0x17000848 RID: 2120
	// (get) Token: 0x06009176 RID: 37238 RVA: 0x002F37A3 File Offset: 0x002F19A3
	// (set) Token: 0x06009177 RID: 37239 RVA: 0x002F37AB File Offset: 0x002F19AB
	[CustomEditField(Sections = "Underwear", Label = "Enable")]
	public bool Underwear
	{
		get
		{
			return this.m_Underwear;
		}
		set
		{
			if (value == this.m_Underwear)
			{
				return;
			}
			this.m_Underwear = value;
			this.UpdateText();
		}
	}

	// Token: 0x17000849 RID: 2121
	// (get) Token: 0x06009178 RID: 37240 RVA: 0x002F37C4 File Offset: 0x002F19C4
	// (set) Token: 0x06009179 RID: 37241 RVA: 0x002F37CC File Offset: 0x002F19CC
	[CustomEditField(Parent = "Underwear", Label = "Flip")]
	public bool UnderwearFlip
	{
		get
		{
			return this.m_UnderwearFlip;
		}
		set
		{
			if (value == this.m_UnderwearFlip)
			{
				return;
			}
			this.m_UnderwearFlip = value;
			this.UpdateText();
		}
	}

	// Token: 0x1700084A RID: 2122
	// (get) Token: 0x0600917A RID: 37242 RVA: 0x002F37E5 File Offset: 0x002F19E5
	// (set) Token: 0x0600917B RID: 37243 RVA: 0x002F37ED File Offset: 0x002F19ED
	[CustomEditField(Parent = "Underwear", Label = "Width")]
	public float UnderwearWidth
	{
		get
		{
			return this.m_UnderwearWidth;
		}
		set
		{
			if (value == this.m_UnderwearWidth)
			{
				return;
			}
			this.m_UnderwearWidth = value;
			this.UpdateText();
		}
	}

	// Token: 0x1700084B RID: 2123
	// (get) Token: 0x0600917C RID: 37244 RVA: 0x002F3806 File Offset: 0x002F1A06
	// (set) Token: 0x0600917D RID: 37245 RVA: 0x002F380E File Offset: 0x002F1A0E
	[CustomEditField(Parent = "Underwear", Label = "Height")]
	public float UnderwearHeight
	{
		get
		{
			return this.m_UnderwearHeight;
		}
		set
		{
			if (value == this.m_UnderwearHeight)
			{
				return;
			}
			this.m_UnderwearHeight = value;
			this.UpdateText();
		}
	}

	// Token: 0x1700084C RID: 2124
	// (get) Token: 0x0600917E RID: 37246 RVA: 0x002F3827 File Offset: 0x002F1A27
	// (set) Token: 0x0600917F RID: 37247 RVA: 0x002F382F File Offset: 0x002F1A2F
	[CustomEditField(Sections = "Alignment", Label = "Enable")]
	[Overridable]
	public UberText.AlignmentOptions Alignment
	{
		get
		{
			return this.m_Alignment;
		}
		set
		{
			if (value == this.m_Alignment)
			{
				return;
			}
			this.m_Alignment = value;
			this.UpdateText();
		}
	}

	// Token: 0x1700084D RID: 2125
	// (get) Token: 0x06009180 RID: 37248 RVA: 0x002F3848 File Offset: 0x002F1A48
	// (set) Token: 0x06009181 RID: 37249 RVA: 0x002F3850 File Offset: 0x002F1A50
	[CustomEditField(Parent = "Alignment")]
	[Overridable]
	public UberText.AnchorOptions Anchor
	{
		get
		{
			return this.m_Anchor;
		}
		set
		{
			if (value == this.m_Anchor)
			{
				return;
			}
			this.m_Anchor = value;
			this.UpdateText();
		}
	}

	// Token: 0x1700084E RID: 2126
	// (get) Token: 0x06009182 RID: 37250 RVA: 0x002F3869 File Offset: 0x002F1A69
	// (set) Token: 0x06009183 RID: 37251 RVA: 0x002F3871 File Offset: 0x002F1A71
	[CustomEditField(Sections = "Render/Bake")]
	public bool RenderToTexture
	{
		get
		{
			return this.m_RenderToTexture;
		}
		set
		{
			if (value == this.m_RenderToTexture)
			{
				return;
			}
			this.m_RenderToTexture = value;
			this.UpdateText();
		}
	}

	// Token: 0x1700084F RID: 2127
	// (get) Token: 0x06009184 RID: 37252 RVA: 0x002F388A File Offset: 0x002F1A8A
	// (set) Token: 0x06009185 RID: 37253 RVA: 0x002F3892 File Offset: 0x002F1A92
	[CustomEditField(Sections = "Render/Bake")]
	public GameObject RenderOnObject
	{
		get
		{
			return this.m_RenderOnObject;
		}
		set
		{
			if (value == this.m_RenderOnObject)
			{
				return;
			}
			this.m_RenderOnObject = value;
			this.UpdateText();
		}
	}

	// Token: 0x17000850 RID: 2128
	// (get) Token: 0x06009186 RID: 37254 RVA: 0x002F38B0 File Offset: 0x002F1AB0
	// (set) Token: 0x06009187 RID: 37255 RVA: 0x002F38B8 File Offset: 0x002F1AB8
	[CustomEditField(Parent = "RenderToTexture")]
	public int TextureResolution
	{
		get
		{
			return this.m_Resolution;
		}
		set
		{
			if (value == this.m_Resolution)
			{
				return;
			}
			this.m_Resolution = value;
			this.UpdateText();
		}
	}

	// Token: 0x17000851 RID: 2129
	// (get) Token: 0x06009188 RID: 37256 RVA: 0x002F38D1 File Offset: 0x002F1AD1
	// (set) Token: 0x06009189 RID: 37257 RVA: 0x002F38D9 File Offset: 0x002F1AD9
	[CustomEditField(Sections = "Outline", Label = "Enable")]
	[Overridable]
	public bool Outline
	{
		get
		{
			return this.m_Outline;
		}
		set
		{
			if (value == this.m_Outline)
			{
				return;
			}
			this.m_Outline = value;
			this.UpdateText();
		}
	}

	// Token: 0x17000852 RID: 2130
	// (get) Token: 0x0600918A RID: 37258 RVA: 0x002F38F2 File Offset: 0x002F1AF2
	// (set) Token: 0x0600918B RID: 37259 RVA: 0x002F38FA File Offset: 0x002F1AFA
	[CustomEditField(Parent = "Outline", Label = "Size")]
	[Overridable]
	public float OutlineSize
	{
		get
		{
			return this.m_OutlineSize;
		}
		set
		{
			if (value == this.m_OutlineSize)
			{
				return;
			}
			this.m_OutlineSize = value;
			this.UpdateText();
		}
	}

	// Token: 0x17000853 RID: 2131
	// (get) Token: 0x0600918C RID: 37260 RVA: 0x002F3913 File Offset: 0x002F1B13
	// (set) Token: 0x0600918D RID: 37261 RVA: 0x002F391B File Offset: 0x002F1B1B
	[CustomEditField(Parent = "Outline", Label = "Color")]
	[Overridable]
	public Color OutlineColor
	{
		get
		{
			return this.m_OutlineColor;
		}
		set
		{
			if (value == this.m_OutlineColor)
			{
				return;
			}
			this.m_OutlineColor = value;
			this.UpdateColor();
		}
	}

	// Token: 0x17000854 RID: 2132
	// (get) Token: 0x0600918E RID: 37262 RVA: 0x002F3939 File Offset: 0x002F1B39
	// (set) Token: 0x0600918F RID: 37263 RVA: 0x002F3946 File Offset: 0x002F1B46
	[CustomEditField(Hide = true)]
	[Overridable]
	public float OutlineAlpha
	{
		get
		{
			return this.m_OutlineColor.a;
		}
		set
		{
			if (value == this.m_OutlineColor.a)
			{
				return;
			}
			this.m_OutlineColor.a = value;
			this.UpdateColor();
		}
	}

	// Token: 0x17000855 RID: 2133
	// (get) Token: 0x06009190 RID: 37264 RVA: 0x002F3969 File Offset: 0x002F1B69
	// (set) Token: 0x06009191 RID: 37265 RVA: 0x002F3971 File Offset: 0x002F1B71
	[CustomEditField(Parent = "RenderToTexture")]
	public bool AntiAlias
	{
		get
		{
			return this.m_AntiAlias;
		}
		set
		{
			if (value == this.m_AntiAlias)
			{
				return;
			}
			this.m_AntiAlias = value;
			this.UpdateText();
		}
	}

	// Token: 0x17000856 RID: 2134
	// (get) Token: 0x06009192 RID: 37266 RVA: 0x002F398A File Offset: 0x002F1B8A
	// (set) Token: 0x06009193 RID: 37267 RVA: 0x002F3992 File Offset: 0x002F1B92
	[CustomEditField(Parent = "AntiAlias")]
	public float AntiAliasAmount
	{
		get
		{
			return this.m_AntiAliasAmount;
		}
		set
		{
			if (value == this.m_AntiAliasAmount)
			{
				return;
			}
			this.m_AntiAliasAmount = value;
			this.UpdateText();
		}
	}

	// Token: 0x17000857 RID: 2135
	// (get) Token: 0x06009194 RID: 37268 RVA: 0x002F39AB File Offset: 0x002F1BAB
	// (set) Token: 0x06009195 RID: 37269 RVA: 0x002F39B3 File Offset: 0x002F1BB3
	[CustomEditField(Sections = "Localization")]
	public UberText.LocalizationSettings LocalizeSettings
	{
		get
		{
			return this.m_LocalizedSettings;
		}
		set
		{
			this.m_LocalizedSettings = value;
			this.UpdateText();
		}
	}

	// Token: 0x17000858 RID: 2136
	// (get) Token: 0x06009196 RID: 37270 RVA: 0x002F39C2 File Offset: 0x002F1BC2
	// (set) Token: 0x06009197 RID: 37271 RVA: 0x002F39CA File Offset: 0x002F1BCA
	[CustomEditField(Parent = "AntiAlias")]
	public float AntiAliasEdge
	{
		get
		{
			return this.m_AntiAliasEdge;
		}
		set
		{
			if (value == this.m_AntiAliasEdge)
			{
				return;
			}
			this.m_AntiAliasEdge = value;
			this.UpdateText();
		}
	}

	// Token: 0x17000859 RID: 2137
	// (get) Token: 0x06009198 RID: 37272 RVA: 0x002F39E3 File Offset: 0x002F1BE3
	// (set) Token: 0x06009199 RID: 37273 RVA: 0x002F39EB File Offset: 0x002F1BEB
	[CustomEditField(Sections = "Shadow", Label = "Enable")]
	[Overridable]
	public bool Shadow
	{
		get
		{
			return this.m_Shadow;
		}
		set
		{
			if (value == this.m_Shadow)
			{
				return;
			}
			this.m_Shadow = value;
			this.UpdateText();
		}
	}

	// Token: 0x1700085A RID: 2138
	// (get) Token: 0x0600919A RID: 37274 RVA: 0x002F3A04 File Offset: 0x002F1C04
	// (set) Token: 0x0600919B RID: 37275 RVA: 0x002F3A0C File Offset: 0x002F1C0C
	[CustomEditField(Parent = "Shadow")]
	[Overridable]
	public float ShadowOffset
	{
		get
		{
			return this.m_ShadowOffset;
		}
		set
		{
			if (value == this.m_ShadowOffset)
			{
				return;
			}
			this.m_ShadowOffset = value;
			this.UpdateText();
		}
	}

	// Token: 0x1700085B RID: 2139
	// (get) Token: 0x0600919C RID: 37276 RVA: 0x002F3A25 File Offset: 0x002F1C25
	// (set) Token: 0x0600919D RID: 37277 RVA: 0x002F3A2D File Offset: 0x002F1C2D
	[CustomEditField(Parent = "Shadow")]
	[Overridable]
	public float ShadowDepthOffset
	{
		get
		{
			return this.m_ShadowDepthOffset;
		}
		set
		{
			if (value == this.m_ShadowDepthOffset)
			{
				return;
			}
			this.m_ShadowDepthOffset = value;
			this.UpdateText();
		}
	}

	// Token: 0x1700085C RID: 2140
	// (get) Token: 0x0600919E RID: 37278 RVA: 0x002F3A46 File Offset: 0x002F1C46
	// (set) Token: 0x0600919F RID: 37279 RVA: 0x002F3A4E File Offset: 0x002F1C4E
	[CustomEditField(Parent = "Shadow")]
	[Overridable]
	public float ShadowBlur
	{
		get
		{
			return this.m_ShadowBlur;
		}
		set
		{
			if (value == this.m_ShadowBlur)
			{
				return;
			}
			this.m_ShadowBlur = value;
			this.UpdateText();
		}
	}

	// Token: 0x1700085D RID: 2141
	// (get) Token: 0x060091A0 RID: 37280 RVA: 0x002F3A67 File Offset: 0x002F1C67
	// (set) Token: 0x060091A1 RID: 37281 RVA: 0x002F3A6F File Offset: 0x002F1C6F
	[CustomEditField(Parent = "Shadow")]
	[Overridable]
	public Color ShadowColor
	{
		get
		{
			return this.m_ShadowColor;
		}
		set
		{
			if (value == this.m_ShadowColor)
			{
				return;
			}
			this.m_ShadowColor = value;
			this.UpdateColor();
		}
	}

	// Token: 0x1700085E RID: 2142
	// (get) Token: 0x060091A2 RID: 37282 RVA: 0x002F3A8D File Offset: 0x002F1C8D
	// (set) Token: 0x060091A3 RID: 37283 RVA: 0x002F3A9A File Offset: 0x002F1C9A
	[CustomEditField(Parent = "Shadow")]
	[Overridable]
	public float ShadowAlpha
	{
		get
		{
			return this.m_ShadowColor.a;
		}
		set
		{
			if (value == this.m_ShadowColor.a)
			{
				return;
			}
			this.m_ShadowColor.a = value;
			this.UpdateColor();
		}
	}

	// Token: 0x1700085F RID: 2143
	// (get) Token: 0x060091A4 RID: 37284 RVA: 0x002F3ABD File Offset: 0x002F1CBD
	// (set) Token: 0x060091A5 RID: 37285 RVA: 0x002F3AC5 File Offset: 0x002F1CC5
	[CustomEditField(Parent = "Shadow")]
	public int ShadowRenderQueueOffset
	{
		get
		{
			return this.m_ShadowRenderQueueOffset;
		}
		set
		{
			if (value == this.m_ShadowRenderQueueOffset)
			{
				return;
			}
			this.m_ShadowRenderQueueOffset = value;
			this.UpdateText();
		}
	}

	// Token: 0x17000860 RID: 2144
	// (get) Token: 0x060091A6 RID: 37286 RVA: 0x002F3ADE File Offset: 0x002F1CDE
	// (set) Token: 0x060091A7 RID: 37287 RVA: 0x002F3AE6 File Offset: 0x002F1CE6
	[CustomEditField(Sections = "Render")]
	public int RenderQueue
	{
		get
		{
			return this.m_RenderQueue;
		}
		set
		{
			if (value == this.m_RenderQueue)
			{
				return;
			}
			this.m_RenderQueue = value;
			this.UpdateText();
		}
	}

	// Token: 0x17000861 RID: 2145
	// (get) Token: 0x060091A8 RID: 37288 RVA: 0x002F3AFF File Offset: 0x002F1CFF
	// (set) Token: 0x060091A9 RID: 37289 RVA: 0x002F3B07 File Offset: 0x002F1D07
	[CustomEditField(Sections = "Render")]
	public float AmbientLightBlend
	{
		get
		{
			return this.m_AmbientLightBlend;
		}
		set
		{
			if (value == this.m_AmbientLightBlend)
			{
				return;
			}
			this.m_AmbientLightBlend = value;
			this.UpdateText();
		}
	}

	// Token: 0x17000862 RID: 2146
	// (get) Token: 0x060091AA RID: 37290 RVA: 0x002F3B20 File Offset: 0x002F1D20
	// (set) Token: 0x060091AB RID: 37291 RVA: 0x002F3B28 File Offset: 0x002F1D28
	[CustomEditField(Sections = "Render")]
	public List<Material> AdditionalMaterials
	{
		get
		{
			return this.m_AdditionalMaterials;
		}
		set
		{
			this.m_AdditionalMaterials = value;
		}
	}

	// Token: 0x17000863 RID: 2147
	// (get) Token: 0x060091AC RID: 37292 RVA: 0x002F3B31 File Offset: 0x002F1D31
	// (set) Token: 0x060091AD RID: 37293 RVA: 0x002F3B39 File Offset: 0x002F1D39
	[CustomEditField(Parent = "RenderToTexture")]
	public Color GradientUpperColor
	{
		get
		{
			return this.m_GradientUpperColor;
		}
		set
		{
			if (value == this.m_GradientUpperColor)
			{
				return;
			}
			this.m_GradientUpperColor = value;
			this.UpdateColor();
		}
	}

	// Token: 0x17000864 RID: 2148
	// (get) Token: 0x060091AE RID: 37294 RVA: 0x002F3B57 File Offset: 0x002F1D57
	// (set) Token: 0x060091AF RID: 37295 RVA: 0x002F3B64 File Offset: 0x002F1D64
	[CustomEditField(Hide = true)]
	public float GradientUpperAlpha
	{
		get
		{
			return this.m_GradientUpperColor.a;
		}
		set
		{
			if (value == this.m_GradientUpperColor.a)
			{
				return;
			}
			this.m_GradientUpperColor.a = value;
			this.UpdateColor();
		}
	}

	// Token: 0x17000865 RID: 2149
	// (get) Token: 0x060091B0 RID: 37296 RVA: 0x002F3B87 File Offset: 0x002F1D87
	// (set) Token: 0x060091B1 RID: 37297 RVA: 0x002F3B8F File Offset: 0x002F1D8F
	[CustomEditField(Parent = "RenderToTexture")]
	public Color GradientLowerColor
	{
		get
		{
			return this.m_GradientLowerColor;
		}
		set
		{
			if (value == this.m_GradientLowerColor)
			{
				return;
			}
			this.m_GradientLowerColor = value;
			this.UpdateColor();
		}
	}

	// Token: 0x17000866 RID: 2150
	// (get) Token: 0x060091B2 RID: 37298 RVA: 0x002F3BAD File Offset: 0x002F1DAD
	// (set) Token: 0x060091B3 RID: 37299 RVA: 0x002F3BBA File Offset: 0x002F1DBA
	[CustomEditField(Hide = true)]
	public float GradientLowerAlpha
	{
		get
		{
			return this.m_GradientLowerColor.a;
		}
		set
		{
			if (value == this.m_GradientLowerColor.a)
			{
				return;
			}
			this.m_GradientLowerColor.a = value;
			this.UpdateColor();
		}
	}

	// Token: 0x17000867 RID: 2151
	// (get) Token: 0x060091B4 RID: 37300 RVA: 0x002F3BDD File Offset: 0x002F1DDD
	public bool IsMeshReady
	{
		get
		{
			return this.m_meshReadyTracker.IsSet;
		}
	}

	// Token: 0x060091B5 RID: 37301 RVA: 0x002F3BEC File Offset: 0x002F1DEC
	private void Awake()
	{
		this.m_UberTextRendering = new UberTextRendering();
		this.m_UberTextLineWrapper = new UberTextLineWrapper(this.m_UberTextRendering);
		this.m_wordWrapSettings = default(WordWrapSettings);
		this.m_UberTextRenderToTexture = new UberTextRenderToTexture();
		this.m_widgetTransform = base.GetComponent<WidgetTransform>();
		if (this.IsPartOfWidget)
		{
			this.m_UseEditorText = true;
			this.m_GameStringLookup = true;
			this.m_Cache = true;
		}
		if (!this.m_GameStringLookup && !this.m_TextSet && !this.m_UseEditorText && Application.IsPlaying(this))
		{
			this.m_Text = "";
		}
	}

	// Token: 0x060091B6 RID: 37302 RVA: 0x002F3C80 File Offset: 0x002F1E80
	private void Start()
	{
		this.m_updated = false;
	}

	// Token: 0x060091B7 RID: 37303 RVA: 0x002F3C89 File Offset: 0x002F1E89
	private void Update()
	{
		this.CheckObjectLayer();
		this.CheckFontTextureSize();
		this.RenderText();
	}

	// Token: 0x060091B8 RID: 37304 RVA: 0x002F3C9D File Offset: 0x002F1E9D
	private void CheckForWidgetTransform()
	{
		this.m_widgetTransform = base.GetComponent<WidgetTransform>();
		if (this.m_HadWidgetTransformLastCheck != this.IsPartOfWidget)
		{
			this.m_updated = false;
		}
		this.m_HadWidgetTransformLastCheck = this.IsPartOfWidget;
	}

	// Token: 0x060091B9 RID: 37305 RVA: 0x002F3CCC File Offset: 0x002F1ECC
	private void CheckObjectLayer()
	{
		int layer = base.gameObject.layer;
		if (this.m_CurrentLayer != layer || (this.m_UberTextRendering != null && !this.m_UberTextRendering.HasLayer(layer)) || (this.m_UberTextRenderToTexture != null && !this.m_UberTextRenderToTexture.HasLayer(layer)))
		{
			this.UpdateLayers();
		}
	}

	// Token: 0x060091BA RID: 37306 RVA: 0x002F3D20 File Offset: 0x002F1F20
	private void CheckFontTextureSize()
	{
		if (this.m_UberTextRendering != null)
		{
			Texture fontTexture = this.m_UberTextRendering.GetFontTexture();
			if (fontTexture && (this.m_PreviousFontSize.x != fontTexture.width || this.m_PreviousFontSize.y != fontTexture.height))
			{
				this.m_updated = false;
			}
		}
	}

	// Token: 0x060091BB RID: 37307 RVA: 0x002F3D76 File Offset: 0x002F1F76
	private void OnDisable()
	{
		if (this.m_UberTextRendering == null || this.m_UberTextRenderToTexture == null)
		{
			return;
		}
		this.m_UberTextRenderToTexture.SetAllVisible(false);
	}

	// Token: 0x060091BC RID: 37308 RVA: 0x002F3D95 File Offset: 0x002F1F95
	private void OnDestroy()
	{
		this.CleanUp();
	}

	// Token: 0x060091BD RID: 37309 RVA: 0x002F3D9D File Offset: 0x002F1F9D
	private void OnEnable()
	{
		this.m_updated = false;
		if (this.m_UberTextRendering != null)
		{
			this.m_UberTextRendering.SetActive(true);
		}
		if (this.m_UberTextRenderToTexture != null)
		{
			this.m_UberTextRenderToTexture.SetActive(true);
		}
		this.SetFont();
		this.UpdateNow(false);
	}

	// Token: 0x060091BE RID: 37310 RVA: 0x002F3DDC File Offset: 0x002F1FDC
	private void OnDrawGizmos()
	{
		float width = this.GetWidth();
		float height = this.GetHeight();
		Gizmos.matrix = base.transform.localToWorldMatrix;
		if (this.IsPartOfWidget)
		{
			Gizmos.matrix *= Matrix4x4.TRS(this.GetTextCenter(), this.GetTextRotation(), Vector3.one);
		}
		Gizmos.color = new Color(0.3f, 0.3f, 0.35f, 0.2f);
		Gizmos.DrawCube(Vector3.zero, new Vector3(width, height, 0f));
		Gizmos.color = Color.black;
		Gizmos.DrawWireCube(Vector3.zero, new Vector3(width, height, 0f));
		if (this.m_Underwear)
		{
			float underwearWidth = this.m_UnderwearWidth;
			float underwearHeight = this.m_UnderwearHeight;
			if (this.m_LocalizedSettings != null)
			{
				UberText.LocalizationSettings.LocaleAdjustment locale = this.m_LocalizedSettings.GetLocale(Localization.GetLocale());
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
			if (this.m_UnderwearFlip)
			{
				Gizmos.DrawWireCube(new Vector3(-(width * 0.5f - num * 0.5f), height * 0.5f - num2 * 0.5f, 0f), new Vector3(num, num2, 0f));
				Gizmos.DrawWireCube(new Vector3(width * 0.5f - num * 0.5f, height * 0.5f - num2 * 0.5f, 0f), new Vector3(num, num2, 0f));
			}
			else
			{
				Gizmos.DrawWireCube(new Vector3(-(width * 0.5f - num * 0.5f), -(height * 0.5f - num2 * 0.5f), 0f), new Vector3(num, num2, 0f));
				Gizmos.DrawWireCube(new Vector3(width * 0.5f - num * 0.5f, -(height * 0.5f - num2 * 0.5f), 0f), new Vector3(num, num2, 0f));
			}
		}
		Gizmos.matrix = Matrix4x4.identity;
	}

	// Token: 0x060091BF RID: 37311 RVA: 0x002F4008 File Offset: 0x002F2208
	private void OnDrawGizmosSelected()
	{
		float width = this.GetWidth();
		float height = this.GetHeight();
		Gizmos.matrix = base.transform.localToWorldMatrix;
		if (this.IsPartOfWidget)
		{
			Gizmos.matrix *= Matrix4x4.TRS(this.GetTextCenter(), this.GetTextRotation(), Vector3.one);
		}
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(Vector3.zero, new Vector3(width + this.GetGizmoBuffer(width), height + this.GetGizmoBuffer(height), 0f));
		Gizmos.matrix = Matrix4x4.identity;
	}

	// Token: 0x060091C0 RID: 37312 RVA: 0x002F409A File Offset: 0x002F229A
	private float GetGizmoBuffer(float boundsValue)
	{
		return Mathf.Min(boundsValue * 0.04f, 0.1f);
	}

	// Token: 0x060091C1 RID: 37313 RVA: 0x002F40AD File Offset: 0x002F22AD
	public void Show()
	{
		this.m_Hidden = false;
		this.UpdateNow(false);
	}

	// Token: 0x060091C2 RID: 37314 RVA: 0x002F40BD File Offset: 0x002F22BD
	public void Hide()
	{
		this.m_Hidden = true;
		this.UpdateNow(false);
	}

	// Token: 0x060091C3 RID: 37315 RVA: 0x002F40CD File Offset: 0x002F22CD
	public bool isHidden()
	{
		return this.m_Hidden;
	}

	// Token: 0x060091C4 RID: 37316 RVA: 0x002F40D5 File Offset: 0x002F22D5
	public void EditorAwake()
	{
		this.UpdateText();
	}

	// Token: 0x060091C5 RID: 37317 RVA: 0x002F40DD File Offset: 0x002F22DD
	public bool IsDone()
	{
		return this.m_updated;
	}

	// Token: 0x060091C6 RID: 37318 RVA: 0x002F40E5 File Offset: 0x002F22E5
	public void UpdateText()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.m_updated = false;
	}

	// Token: 0x060091C7 RID: 37319 RVA: 0x002F40FC File Offset: 0x002F22FC
	public void UpdateNow(bool updateIfInactive = false)
	{
		if (this == null || base.gameObject == null || (!base.gameObject.activeInHierarchy && !updateIfInactive))
		{
			return;
		}
		this.m_updated = false;
		this.RenderText();
	}

	// Token: 0x060091C8 RID: 37320 RVA: 0x002F4134 File Offset: 0x002F2334
	public Bounds GetBounds()
	{
		Matrix4x4 matrix4x = base.transform.localToWorldMatrix * Matrix4x4.Rotate(this.GetTextRotation());
		Vector3 b = matrix4x.MultiplyVector(Vector3.up) * (this.GetHeight() * 0.5f);
		Vector3 b2 = matrix4x.MultiplyVector(Vector3.right) * (this.GetWidth() * 0.5f);
		return new Bounds
		{
			min = base.transform.position - b2 + b,
			max = base.transform.position + b2 - b
		};
	}

	// Token: 0x060091C9 RID: 37321 RVA: 0x002F41E0 File Offset: 0x002F23E0
	public Bounds GetLocalBounds()
	{
		Matrix4x4 matrix4x = Matrix4x4.Rotate(this.GetTextRotation());
		Vector3 b = matrix4x.MultiplyVector(Vector3.up) * (this.GetHeight() * 0.5f);
		Vector3 b2 = matrix4x.MultiplyVector(Vector3.right) * (this.GetWidth() * 0.5f);
		return new Bounds
		{
			min = base.transform.position - b2 + b,
			max = base.transform.position + b2 - b
		};
	}

	// Token: 0x060091CA RID: 37322 RVA: 0x002F427C File Offset: 0x002F247C
	public Bounds GetTextBounds()
	{
		if (!this.m_updated)
		{
			this.UpdateNow(false);
		}
		if (this.m_UberTextRendering == null)
		{
			return new Bounds(Vector3.zero, Vector3.zero);
		}
		Quaternion rotation = base.transform.rotation;
		base.transform.rotation = Quaternion.identity;
		Bounds textMeshBounds = this.m_UberTextRendering.GetTextMeshBounds();
		base.transform.rotation = rotation;
		return textMeshBounds;
	}

	// Token: 0x060091CB RID: 37323 RVA: 0x002F42E3 File Offset: 0x002F24E3
	public Bounds GetTextWorldSpaceBounds()
	{
		if (!this.m_updated)
		{
			this.UpdateNow(false);
		}
		if (this.m_UberTextRendering == null)
		{
			return new Bounds(Vector3.zero, Vector3.zero);
		}
		return this.m_UberTextRendering.GetTextMeshBounds();
	}

	// Token: 0x060091CC RID: 37324 RVA: 0x002F4317 File Offset: 0x002F2517
	public bool IsMultiLine()
	{
		return this.m_LineCount > 1;
	}

	// Token: 0x060091CD RID: 37325 RVA: 0x002F4322 File Offset: 0x002F2522
	public bool IsEllipsized()
	{
		return this.m_Ellipsized;
	}

	// Token: 0x060091CE RID: 37326 RVA: 0x002F432A File Offset: 0x002F252A
	public void SetGameStringText(string gameStringTag)
	{
		this.Text = GameStrings.Get(gameStringTag);
	}

	// Token: 0x060091CF RID: 37327 RVA: 0x002F4338 File Offset: 0x002F2538
	public Font GetLocalizedFont()
	{
		if (this.m_LocalizedFont)
		{
			return this.m_LocalizedFont;
		}
		return this.m_Font;
	}

	// Token: 0x060091D0 RID: 37328 RVA: 0x002F4354 File Offset: 0x002F2554
	public UberText.LocalizationSettings.LocaleAdjustment AddLocaleAdjustment(Locale locale)
	{
		return this.m_LocalizedSettings.AddLocale(locale);
	}

	// Token: 0x060091D1 RID: 37329 RVA: 0x002F4362 File Offset: 0x002F2562
	public UberText.LocalizationSettings.LocaleAdjustment GetLocaleAdjustment(Locale locale)
	{
		return this.m_LocalizedSettings.GetLocale(locale);
	}

	// Token: 0x060091D2 RID: 37330 RVA: 0x002F4370 File Offset: 0x002F2570
	public void RemoveLocaleAdjustment(Locale locale)
	{
		this.m_LocalizedSettings.RemoveLocale(locale);
	}

	// Token: 0x060091D3 RID: 37331 RVA: 0x002F39AB File Offset: 0x002F1BAB
	public UberText.LocalizationSettings GetAllLocalizationSettings()
	{
		return this.m_LocalizedSettings;
	}

	// Token: 0x060091D4 RID: 37332 RVA: 0x002F4380 File Offset: 0x002F2580
	public void SetFontWithoutLocalization(FontDefinition fontDef)
	{
		Font font = fontDef.m_Font;
		if (font == null)
		{
			return;
		}
		if (this.m_UberTextRendering != null && !this.m_UberTextRendering.CanSetFont(font))
		{
			return;
		}
		this.m_Font = font;
		this.m_LocalizedFont = this.m_Font;
		this.m_LineSpaceModifier = fontDef.m_LineSpaceModifier;
		this.m_FontSizeModifier = fontDef.m_FontSizeModifier;
		this.m_SingleLineAdjustment = fontDef.m_SingleLineAdjustment;
		this.m_CharacterSizeModifier = fontDef.m_CharacterSizeModifier;
		this.m_UnboundCharacterSizeModifier = fontDef.m_UnboundCharacterSizeModifier;
		this.m_OutlineModifier = fontDef.m_OutlineModifier;
		this.m_isFontDefLoaded = true;
		if (this.m_UberTextRendering != null)
		{
			this.m_UberTextRendering.SetFont(this.m_Font);
		}
		this.UpdateNow(false);
	}

	// Token: 0x060091D5 RID: 37333 RVA: 0x002F4438 File Offset: 0x002F2638
	public string GetProcessedText()
	{
		if (this.m_UberTextRendering == null)
		{
			return string.Empty;
		}
		return this.m_UberTextRendering.GetText().Replace("<material=1></material>", string.Empty).Replace("<material=1>", "<b>").Replace("</material>", "</b>");
	}

	// Token: 0x060091D6 RID: 37334 RVA: 0x002F448C File Offset: 0x002F268C
	public static void RebuildAllUberText()
	{
		UberText[] array = UnityEngine.Object.FindObjectsOfType<UberText>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].UpdateNow(false);
		}
	}

	// Token: 0x060091D7 RID: 37335 RVA: 0x002F44B8 File Offset: 0x002F26B8
	private void RenderText()
	{
		if (!this.CanRenderText())
		{
			return;
		}
		if (string.IsNullOrEmpty(this.m_Text) || this.m_Hidden)
		{
			this.SetRenderedObjectsVisible(false);
			return;
		}
		this.SetWorldWidthAndHeight();
		this.CreateTextMesh();
		this.UpdateTextPosition();
		this.SetFont();
		this.SetFontSize(this.m_FontSize);
		this.SetLineSpacing(this.m_LineSpacing);
		this.SetupTextMeshAlignment();
		this.m_UberTextRendering.SetLocale(Localization.GetLocale());
		this.SetupTextAndCharacterSize();
		this.SetupTextMeshAlignment();
		this.UpdateOutlineProperties();
		this.UpdateTexelSize();
		this.UpdateLayers();
		this.UpdateRenderQueue();
		this.UpdateColor();
		this.m_UberTextRendering.SetBoldEnabled(this.m_HasBoldText);
		this.m_UberTextRendering.SetOutlineEnabled(this.m_Outline);
		this.m_UberTextRendering.ApplyMaterials();
		if (this.m_RenderToTexture)
		{
			this.SetupRenderToTexture();
		}
		this.SetRenderedObjectsVisible(true);
		if (this.m_RenderToTexture)
		{
			this.m_UberTextRenderToTexture.DoRenderToTexture();
			if (this.m_AntiAlias)
			{
				this.m_UberTextRenderToTexture.ApplyAntialiasing();
			}
		}
		this.m_PreviousText = this.m_Text;
		this.m_updated = true;
	}

	// Token: 0x060091D8 RID: 37336 RVA: 0x002F45D8 File Offset: 0x002F27D8
	private bool CanRenderText()
	{
		if (this.m_RenderToTexture && this.m_UberTextRenderToTexture != null)
		{
			this.m_updated = (this.m_updated && this.m_UberTextRenderToTexture.IsRenderTextureCreated());
		}
		if (this.m_updated || this.m_UberTextRendering == null || this.m_UberTextRenderToTexture == null)
		{
			return false;
		}
		if (!this.m_Font)
		{
			Debug.LogWarning(string.Format("UberText error: Font is null for {0}", base.gameObject.name));
			return false;
		}
		return true;
	}

	// Token: 0x060091D9 RID: 37337 RVA: 0x002F4655 File Offset: 0x002F2855
	private void SetRenderedObjectsVisible(bool visible)
	{
		this.m_UberTextRendering.SetVisible(visible);
		this.m_UberTextRenderToTexture.SetAllVisible(visible);
	}

	// Token: 0x060091DA RID: 37338 RVA: 0x002F466F File Offset: 0x002F286F
	private bool CanUseCachedText()
	{
		return 0 == 0;
	}

	// Token: 0x060091DB RID: 37339 RVA: 0x002F4678 File Offset: 0x002F2878
	private void SetupTextAndCharacterSize()
	{
		this.m_UberTextRendering.SetCharacterSize(this.m_CharacterSize * this.m_CharacterSizeModifier * 0.01f);
		string text = string.Empty;
		bool flag = false;
		bool flag2 = this.CanUseCachedText();
		if (flag2)
		{
			this.m_CacheHash = new UberText.CachedTextKeyData
			{
				m_Text = this.m_Text,
				m_CharSize = this.m_CharacterSize,
				m_Font = this.m_Font,
				m_FontSize = this.m_FontSize,
				m_Height = this.GetHeight(),
				m_Width = this.GetWidth(),
				m_LineSpacing = this.m_LineSpacing
			}.GetHashCode();
			if (this.m_Cache && (this.m_WordWrap || this.m_ResizeToFit) && UberText.s_CachedText.ContainsKey(this.m_CacheHash))
			{
				UberText.CachedTextValues cachedTextValues = UberText.s_CachedText[this.m_CacheHash];
				if (cachedTextValues.m_OriginalTextHash == this.m_Text.GetHashCode())
				{
					text = cachedTextValues.m_Text;
					this.m_UberTextRendering.SetText(text);
					this.m_UberTextRendering.SetCharacterSize(cachedTextValues.m_CharSize);
					this.SetLineSpacing(this.m_LineSpacing);
					flag = true;
				}
			}
		}
		Quaternion rotation = base.transform.rotation;
		base.transform.rotation = Quaternion.identity;
		if (!flag)
		{
			string text2 = this.m_Text;
			text = string.Empty;
			if (this.m_GameStringLookup)
			{
				text2 = GameStrings.Get(text2.Trim());
			}
			if (Localization.GetLocale() != Locale.enUS)
			{
				text2 = this.LocalizationFixes(text2);
			}
			text = this.ProcessText(text2);
			this.m_LineCount = UberText.LineCount(text);
			this.m_Ellipsized = false;
			if (this.m_WordWrap && !this.m_ResizeToFit)
			{
				this.m_UberTextRendering.SetText(this.WordWrapString(text, this.GetWidth()));
			}
			else
			{
				this.m_UberTextRendering.SetText(text);
			}
			this.m_UberTextRendering.SetCharacterSize(this.m_CharacterSize * this.m_CharacterSizeModifier * 0.01f);
		}
		if (this.m_ResizeToFit && !flag)
		{
			this.ResizeTextToFit(text);
		}
		base.transform.rotation = rotation;
		if (flag2 && this.m_Cache && !flag && (this.m_WordWrap || this.m_ResizeToFit))
		{
			double num = 0.0;
			if (!double.TryParse(this.m_Text, out num) && this.m_Text.Length > 3)
			{
				UberText.s_CachedText[this.m_CacheHash] = new UberText.CachedTextValues();
				UberText.s_CachedText[this.m_CacheHash].m_Text = this.m_UberTextRendering.GetText();
				UberText.s_CachedText[this.m_CacheHash].m_CharSize = this.m_UberTextRendering.GetCharacterSize();
				UberText.s_CachedText[this.m_CacheHash].m_OriginalTextHash = this.m_Text.GetHashCode();
			}
		}
		float num2 = 1f;
		float unboundedCharSize = UberTextLocalization.GetUnboundedCharSize(this.m_LocalizedSettings);
		if (unboundedCharSize > 0f)
		{
			num2 = unboundedCharSize;
		}
		float num3 = this.m_UberTextRendering.GetCharacterSize();
		num3 *= this.m_UnboundCharacterSizeModifier * num2;
		this.m_UberTextRendering.SetCharacterSize(num3);
	}

	// Token: 0x060091DC RID: 37340 RVA: 0x002F49A0 File Offset: 0x002F2BA0
	private void SetupTextMeshAlignment()
	{
		float width = this.GetWidth();
		float height = this.GetHeight();
		this.m_UberTextRendering.SetTextAlignment(this.m_Alignment);
		this.m_UberTextRendering.SetTextMeshGameObjectLocalPosition(this.GetTextCenter());
		this.m_UberTextRendering.SetTextMeshGameObjectLocalPositionOffset(UberTextLocalization.GetPositionOffset(this.m_LocalizedSettings));
		switch (this.m_Alignment)
		{
		case UberText.AlignmentOptions.Left:
			switch (this.m_Anchor)
			{
			case UberText.AnchorOptions.Upper:
				this.m_UberTextRendering.SetTextMeshGameObjectLocalPositionOffset(this.GetAlignmentOffset(-width * 0.5f, height * 0.5f));
				this.m_UberTextRendering.SetTextAnchor(TextAnchor.UpperLeft);
				return;
			case UberText.AnchorOptions.Middle:
				this.m_UberTextRendering.SetTextMeshGameObjectLocalPositionOffset(this.GetAlignmentOffset(-width * 0.5f, 0f));
				this.m_UberTextRendering.SetTextAnchor(TextAnchor.MiddleLeft);
				return;
			case UberText.AnchorOptions.Lower:
				this.m_UberTextRendering.SetTextMeshGameObjectLocalPositionOffset(this.GetAlignmentOffset(-width * 0.5f, -height * 0.5f));
				this.m_UberTextRendering.SetTextAnchor(TextAnchor.LowerLeft);
				return;
			default:
				return;
			}
			break;
		case UberText.AlignmentOptions.Center:
			switch (this.m_Anchor)
			{
			case UberText.AnchorOptions.Upper:
				this.m_UberTextRendering.SetTextMeshGameObjectLocalPositionOffset(this.GetAlignmentOffset(0f, height * 0.5f));
				this.m_UberTextRendering.SetTextAnchor(TextAnchor.UpperCenter);
				return;
			case UberText.AnchorOptions.Middle:
				this.m_UberTextRendering.SetTextAnchor(TextAnchor.MiddleCenter);
				return;
			case UberText.AnchorOptions.Lower:
				this.m_UberTextRendering.SetTextMeshGameObjectLocalPositionOffset(this.GetAlignmentOffset(0f, -height * 0.5f));
				this.m_UberTextRendering.SetTextAnchor(TextAnchor.LowerCenter);
				return;
			default:
				return;
			}
			break;
		case UberText.AlignmentOptions.Right:
			switch (this.m_Anchor)
			{
			case UberText.AnchorOptions.Upper:
				this.m_UberTextRendering.SetTextMeshGameObjectLocalPositionOffset(this.GetAlignmentOffset(width * 0.5f, height * 0.5f));
				this.m_UberTextRendering.SetTextAnchor(TextAnchor.UpperRight);
				return;
			case UberText.AnchorOptions.Middle:
				this.m_UberTextRendering.SetTextMeshGameObjectLocalPositionOffset(this.GetAlignmentOffset(width * 0.5f, 0f));
				this.m_UberTextRendering.SetTextAnchor(TextAnchor.MiddleRight);
				return;
			case UberText.AnchorOptions.Lower:
				this.m_UberTextRendering.SetTextMeshGameObjectLocalPositionOffset(this.GetAlignmentOffset(width * 0.5f, -height * 0.5f));
				this.m_UberTextRendering.SetTextAnchor(TextAnchor.LowerRight);
				return;
			default:
				return;
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x060091DD RID: 37341 RVA: 0x002F4BC5 File Offset: 0x002F2DC5
	private Vector3 GetAlignmentOffset(float width, float height)
	{
		if (!this.IsPartOfWidget)
		{
			return new Vector3(width, height, 0f);
		}
		return new Vector3(width, 0f, height);
	}

	// Token: 0x060091DE RID: 37342 RVA: 0x002F4BE8 File Offset: 0x002F2DE8
	private void UpdateTextPosition()
	{
		if (this.m_RenderToTexture)
		{
			this.m_UberTextRendering.SetTextMeshRenderToTextureTransform(this.Offset);
			return;
		}
		this.m_UberTextRendering.SetTextMeshTransform(base.gameObject, this.GetTextCenter(), this.GetTextRotation());
	}

	// Token: 0x060091DF RID: 37343 RVA: 0x002F4C24 File Offset: 0x002F2E24
	private void UpdateColor()
	{
		this.m_updated = false;
		if (this.m_UberTextRendering == null || this.m_UberTextRenderToTexture == null)
		{
			return;
		}
		this.m_UberTextRendering.SetTextColor(this.m_TextColor);
		this.m_UberTextRendering.SetOutlineColor(this.m_OutlineColor);
		this.m_UberTextRendering.SetAmbientLightBlend(this.m_AmbientLightBlend);
		this.m_UberTextRenderToTexture.SetShadowColor(this.m_ShadowColor);
		this.m_UberTextRenderToTexture.SetGradientColors(this.m_GradientUpperColor, this.m_GradientLowerColor);
	}

	// Token: 0x060091E0 RID: 37344 RVA: 0x002F4CA4 File Offset: 0x002F2EA4
	private void UpdateLayers()
	{
		this.m_CurrentLayer = base.gameObject.layer;
		if (this.m_UberTextRendering != null)
		{
			this.m_UberTextRendering.SetLayer(this.m_CurrentLayer);
		}
		if (this.m_UberTextRenderToTexture != null)
		{
			this.m_UberTextRenderToTexture.SetLayer(this.m_CurrentLayer);
		}
	}

	// Token: 0x060091E1 RID: 37345 RVA: 0x002F4CF4 File Offset: 0x002F2EF4
	private void UpdateRenderQueue()
	{
		if (this.m_UberTextRendering == null || this.m_UberTextRenderToTexture == null)
		{
			return;
		}
		this.m_UberTextRendering.SetRenderQueueIncrement(this.m_RenderQueue);
		this.m_UberTextRenderToTexture.SetRenderQueueIncrement(this.m_RenderQueue);
		this.m_UberTextRenderToTexture.SetShadowRenderQueueIncrement(this.m_RenderQueue + this.m_ShadowRenderQueueOffset);
	}

	// Token: 0x060091E2 RID: 37346 RVA: 0x002F4D4C File Offset: 0x002F2F4C
	private void UpdateTexelSize()
	{
		float num = this.m_OutlineSize * this.m_OutlineModifier * UberTextLocalization.GetOutlineModifier(this.m_LocalizedSettings);
		Texture fontTexture = this.m_UberTextRendering.GetFontTexture();
		if (fontTexture == null)
		{
			if (!Application.IsPlaying(this))
			{
				return;
			}
			Debug.LogError(string.Format("UberText.UpdateTexelSize() - m_FontTexture == null!  text={0}", this.m_Text));
			return;
		}
		else
		{
			Vector2 vector = this.TexelSize(fontTexture);
			if (vector == this.m_PreviousTexelSize)
			{
				return;
			}
			this.m_UberTextRendering.SetBoldOffset(vector * this.m_BoldSize);
			this.m_UberTextRendering.SetOutlineOffset(vector * num);
			this.m_UberTextRendering.SetOutlineBoldOffset(vector * (num + this.m_BoldSize * 0.75f));
			this.m_UberTextRendering.SetTexelSize(vector);
			this.m_PreviousTexelSize = vector;
			this.m_PreviousFontSize = new Vector2Int(fontTexture.width, fontTexture.height);
			return;
		}
	}

	// Token: 0x060091E3 RID: 37347 RVA: 0x002F4E30 File Offset: 0x002F3030
	private void CreateTextMesh()
	{
		this.m_UberTextRendering.Init(base.gameObject);
		this.m_meshReadyTracker.SetAndDispatch();
		this.m_UberTextRendering.SetRichText(this.m_RichText);
		this.m_HasBoldText = false;
		if (this.m_Text.Contains("<b>"))
		{
			this.Bold();
		}
	}

	// Token: 0x060091E4 RID: 37348 RVA: 0x002F4E8C File Offset: 0x002F308C
	private void SetFont()
	{
		if (this.m_Font == null)
		{
			return;
		}
		FontTable fontTable;
		if (!this.m_isFontDefLoaded && HearthstoneServices.TryGet<FontTable>(out fontTable))
		{
			FontDefinition fontDef = fontTable.GetFontDef(this.m_Font);
			if (fontDef != null)
			{
				this.m_LocalizedFont = fontDef.m_Font;
				this.m_LineSpaceModifier = fontDef.m_LineSpaceModifier;
				this.m_FontSizeModifier = fontDef.m_FontSizeModifier;
				this.m_SingleLineAdjustment = fontDef.m_SingleLineAdjustment;
				this.m_CharacterSizeModifier = fontDef.m_CharacterSizeModifier;
				this.m_UnboundCharacterSizeModifier = fontDef.m_UnboundCharacterSizeModifier;
				this.m_OutlineModifier = fontDef.m_OutlineModifier;
				this.m_isFontDefLoaded = true;
			}
			else
			{
				Debug.LogErrorFormat("Error loading fontDef for UberText component={0} font={1}", new object[]
				{
					base.name,
					this.m_Font
				});
			}
		}
		if (this.m_UberTextRendering != null)
		{
			if (this.m_LocalizedFont == null)
			{
				this.m_UberTextRendering.SetFont(this.m_Font);
				return;
			}
			this.m_UberTextRendering.SetFont(this.m_LocalizedFont);
		}
	}

	// Token: 0x060091E5 RID: 37349 RVA: 0x002F4F8D File Offset: 0x002F318D
	private void SetFontSize(int fontSize)
	{
		fontSize = (int)(this.m_FontSizeModifier * UberTextLocalization.GetFontSizeModifier(this.m_LocalizedSettings) * (float)fontSize);
		this.m_UberTextRendering.SetFontSize(fontSize);
	}

	// Token: 0x060091E6 RID: 37350 RVA: 0x002F4FB4 File Offset: 0x002F31B4
	private float GetWidth()
	{
		float width = this.m_Width;
		if (this.IsPartOfWidget)
		{
			width = this.m_widgetTransform.Rect.width;
		}
		if (this.m_LocalizedSettings != null)
		{
			UberText.LocalizationSettings.LocaleAdjustment locale = this.m_LocalizedSettings.GetLocale(Localization.GetLocale());
			if (locale != null && locale.m_Width > 0f)
			{
				width = locale.m_Width;
			}
		}
		return width;
	}

	// Token: 0x060091E7 RID: 37351 RVA: 0x002F5018 File Offset: 0x002F3218
	private float GetHeight()
	{
		float height = this.m_Height;
		if (this.IsPartOfWidget)
		{
			height = this.m_widgetTransform.Rect.height;
		}
		if (this.m_LocalizedSettings != null)
		{
			UberText.LocalizationSettings.LocaleAdjustment locale = this.m_LocalizedSettings.GetLocale(Localization.GetLocale());
			if (locale != null && locale.m_Height > 0f)
			{
				height = locale.m_Height;
			}
		}
		return height;
	}

	// Token: 0x060091E8 RID: 37352 RVA: 0x002F507C File Offset: 0x002F327C
	private void SetLineSpacing(float lineSpacing)
	{
		if (UberText.LineCount(this.m_UberTextRendering.GetText()) == 1)
		{
			lineSpacing += this.m_SingleLineAdjustment + UberTextLocalization.GetSingleLineAdjustment(this.m_LocalizedSettings);
		}
		else
		{
			lineSpacing *= this.m_LineSpaceModifier * UberTextLocalization.GetLineSpaceModifier(this.m_LocalizedSettings);
		}
		this.m_UberTextRendering.SetLineSpacing(lineSpacing);
	}

	// Token: 0x060091E9 RID: 37353 RVA: 0x002F50D8 File Offset: 0x002F32D8
	private void SetupRenderToTexture()
	{
		this.m_UberTextRendering.SetTextMeshRenderToTextureTransform(this.Offset);
		Vector2Int renderTextureSize = this.CalcTextureSize();
		if (this.m_RenderOnObject)
		{
			this.m_UberTextRenderToTexture.InitOnRenderObject(this.m_RenderOnObject, renderTextureSize);
		}
		else
		{
			this.SetupRenderOnPlane(renderTextureSize);
			this.SetupShadow();
		}
		this.SetupCamera();
		this.SetupAntialias();
	}

	// Token: 0x060091EA RID: 37354 RVA: 0x002F5138 File Offset: 0x002F3338
	private void SetupRenderOnPlane(Vector2Int renderTextureSize)
	{
		Vector2 planeSize = new Vector2(this.GetWidth(), this.GetHeight());
		this.m_UberTextRenderToTexture.InitOnPlane(base.gameObject, planeSize, this.m_GradientUpperColor, this.m_GradientLowerColor, renderTextureSize);
		Vector3 zero = Vector3.zero;
		if (this.m_widgetTransform == null)
		{
			zero.x = -90f;
		}
		this.m_UberTextRenderToTexture.SetPlaneLocalPosition(this.GetTextCenter() + UberTextLocalization.GetPositionOffset(this.m_LocalizedSettings));
		this.m_UberTextRenderToTexture.SetPlaneRotation(base.transform.rotation);
		this.m_UberTextRenderToTexture.DoPlaneRotate(zero);
	}

	// Token: 0x060091EB RID: 37355 RVA: 0x002F51DA File Offset: 0x002F33DA
	private void SetupAntialias()
	{
		if (this.m_AntiAlias)
		{
			this.m_UberTextRenderToTexture.SetAntialiasEdge(this.m_AntiAliasEdge);
			this.m_UberTextRenderToTexture.SetAntialiasOffset(this.m_AntiAliasAmount);
		}
	}

	// Token: 0x060091EC RID: 37356 RVA: 0x002F5208 File Offset: 0x002F3408
	private void SetupCamera()
	{
		Color cameraBackgroundColor = this.m_TextColor;
		if (this.m_Outline)
		{
			cameraBackgroundColor = this.m_OutlineColor;
		}
		this.m_UberTextRenderToTexture.SetCameraBackgroundColor(cameraBackgroundColor);
		this.m_UberTextRenderToTexture.SetCameraPosition(this.Alignment, this.m_Anchor, new Vector2(this.GetWidth(), this.GetHeight()), this.Offset);
	}

	// Token: 0x060091ED RID: 37357 RVA: 0x002F5268 File Offset: 0x002F3468
	private void ResizeTextToFit(string text)
	{
		if (text == null || text == string.Empty)
		{
			return;
		}
		UberTextRendering.TransformBackup transformBackup = this.m_UberTextRendering.BackupTextMeshTransform();
		this.m_UberTextRendering.SetTextMeshGameObjectParent(null);
		this.m_UberTextRendering.SetTextMeshGameObjectLocalScale(Vector3.one);
		this.m_UberTextRendering.SetTextMeshGameObjectRotation(Quaternion.identity);
		float width = this.GetWidth();
		string text3;
		string text2 = UberText.RemoveTagsFromWord(text, out text3, this.m_RichText);
		if (text2 == null)
		{
			text2 = string.Empty;
		}
		this.m_UberTextRendering.SetText(text2);
		if (this.m_WordWrap)
		{
			this.m_UberTextRendering.SetText(this.WordWrapString(text, width));
		}
		if (this.m_ResizeToFitAndGrow)
		{
			this.ResizeToFitBounds_CharSize(text);
		}
		else
		{
			this.ReduceText_CharSize(text);
		}
		this.m_UberTextRendering.SetTextMeshGameObjectParent(transformBackup.Parent);
		this.m_UberTextRendering.SetTextMeshGameObjectLocalPosition(transformBackup.LocalPosition);
		this.m_UberTextRendering.SetTextMeshGameObjectLocalScale(transformBackup.LocalScale);
		this.m_UberTextRendering.SetTextMeshGameObjectRotation(transformBackup.Rotation);
		if (!this.m_WordWrap)
		{
			this.m_UberTextRendering.SetText(text);
		}
	}

	// Token: 0x060091EE RID: 37358 RVA: 0x002F5374 File Offset: 0x002F3574
	private void ReduceText(string text, int step, int newSize)
	{
		if (this.m_FontSize == 1)
		{
			return;
		}
		this.SetFontSize(newSize);
		float num = this.GetHeight();
		float num2 = this.GetWidth();
		if (!this.m_RenderToTexture)
		{
			num = this.m_WorldHeight;
			num2 = this.m_WorldWidth;
		}
		if (!this.IsMultiLine())
		{
			this.SetLineSpacing(0f);
		}
		Bounds textMeshBounds = this.m_UberTextRendering.GetTextMeshBounds();
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
			if (newSize < this.m_MinFontSize)
			{
				newSize = this.m_MinFontSize;
				break;
			}
			this.SetFontSize(newSize);
			if (this.m_WordWrap)
			{
				this.m_UberTextRendering.SetText(this.WordWrapString(text, num2));
			}
			textMeshBounds = this.m_UberTextRendering.GetTextMeshBounds();
			y = textMeshBounds.size.y;
			x = textMeshBounds.size.x;
		}
		if (!this.IsMultiLine())
		{
			this.SetLineSpacing(this.m_LineSpacing);
		}
		this.m_FontSize = newSize;
	}

	// Token: 0x060091EF RID: 37359 RVA: 0x002F5488 File Offset: 0x002F3688
	private float Measure_IntraLine_Height()
	{
		string text = this.m_UberTextRendering.GetText();
		this.m_UberTextRendering.SetText("|");
		float y = this.m_UberTextRendering.GetTextMeshBounds().size.y;
		this.m_UberTextRendering.SetText("|\n|");
		float result = this.m_UberTextRendering.GetTextMeshBounds().size.y - y * 2f;
		this.m_UberTextRendering.SetText(text);
		return result;
	}

	// Token: 0x060091F0 RID: 37360 RVA: 0x002F5508 File Offset: 0x002F3708
	private void ReduceText_CharSize(string text)
	{
		float height = this.GetHeight();
		float width = this.GetWidth();
		float num = this.m_UberTextRendering.GetCharacterSize();
		if (!this.IsMultiLine())
		{
			this.SetLineSpacing(0f);
		}
		else
		{
			this.SetLineSpacing(this.m_LineSpacing);
		}
		Bounds textMeshBounds = this.m_UberTextRendering.GetTextMeshBounds();
		float x = textMeshBounds.size.x;
		float num2 = textMeshBounds.size.y;
		int num3 = 0;
		num2 -= this.Measure_IntraLine_Height();
		float num4 = 1f;
		if (this.m_LocalizedSettings != null)
		{
			UberText.LocalizationSettings.LocaleAdjustment locale = this.m_LocalizedSettings.GetLocale(Localization.GetLocale());
			if (locale != null && locale.m_ResizeToFitWidthModifier > 0f)
			{
				num4 = locale.m_ResizeToFitWidthModifier;
			}
		}
		while (num2 > height || x > width * num4)
		{
			num3++;
			if (num3 > 40)
			{
				break;
			}
			num *= 0.95f;
			if (num <= this.m_MinCharacterSize * 0.01f)
			{
				num = this.m_MinCharacterSize * 0.01f;
				this.m_UberTextRendering.SetCharacterSize(num);
				if (this.m_WordWrap)
				{
					this.m_UberTextRendering.SetText(this.WordWrapString(text, width, true));
					break;
				}
				break;
			}
			else
			{
				this.m_UberTextRendering.SetCharacterSize(num);
				if (this.m_WordWrap)
				{
					this.m_UberTextRendering.SetText(this.WordWrapString(text, width, false));
				}
				if (UberText.LineCount(this.m_UberTextRendering.GetText()) > 1)
				{
					this.SetLineSpacing(this.m_LineSpacing);
				}
				else
				{
					this.SetLineSpacing(0f);
				}
				textMeshBounds = this.m_UberTextRendering.GetTextMeshBounds();
				x = textMeshBounds.size.x;
				num2 = textMeshBounds.size.y;
			}
		}
		this.SetLineSpacing(this.m_LineSpacing);
	}

	// Token: 0x060091F1 RID: 37361 RVA: 0x002F56D0 File Offset: 0x002F38D0
	private void ResizeToFitBounds_CharSize(string text)
	{
		float height = this.GetHeight();
		float width = this.GetWidth();
		float num = this.m_UberTextRendering.GetCharacterSize();
		this.SetLineSpacing(this.IsMultiLine() ? this.m_LineSpacing : 0f);
		Bounds textMeshBounds = this.m_UberTextRendering.GetTextMeshBounds();
		float x = textMeshBounds.size.x;
		float y = textMeshBounds.size.y;
		float num2 = y * 0.01f;
		float num3 = x * 0.01f;
		int num4 = 0;
		bool flag = x - num3 < width;
		bool flag2 = x + num3 > width;
		bool flag3 = y - num2 < height;
		bool flag4 = y + num2 > height;
		if (!this.m_WordWrap && (flag || flag2))
		{
			float val = height / y;
			float val2 = width / x;
			float num5 = Math.Min(val, val2);
			num *= num5;
			if (num <= this.m_MinCharacterSize * 0.01f)
			{
				num = this.m_MinCharacterSize * 0.01f;
			}
			this.m_UberTextRendering.SetCharacterSize(num);
		}
		while (this.m_WordWrap && (flag3 || flag4))
		{
			num4++;
			if (num4 > 40)
			{
				break;
			}
			float num6 = flag3 ? (1f + this.m_ResizeToGrowStep) : (1f - this.m_ResizeToGrowStep);
			bool flag5 = false;
			string text2 = this.m_UberTextRendering.GetText();
			int num7 = UberText.LineCount(text2);
			num *= num6;
			if (num <= this.m_MinCharacterSize * 0.01f)
			{
				num = this.m_MinCharacterSize * 0.01f;
				flag5 = true;
			}
			this.m_UberTextRendering.SetCharacterSize(num);
			this.m_UberTextRendering.SetText(this.WordWrapString(text, width, false));
			this.SetLineSpacing(this.IsMultiLine() ? this.m_LineSpacing : 0f);
			textMeshBounds = this.m_UberTextRendering.GetTextMeshBounds();
			x = textMeshBounds.size.x;
			y = textMeshBounds.size.y;
			num2 = y * 0.01f;
			bool flag6 = num7 < UberText.LineCount(this.m_UberTextRendering.GetText());
			bool flag7 = !flag3 && y - num2 < height;
			bool flag8 = flag3 && y + num2 > height;
			if (flag6 && flag8)
			{
				this.m_UberTextRendering.SetText(text2);
			}
			if (flag5 || flag7 || flag8)
			{
				break;
			}
		}
		this.SetLineSpacing(this.m_LineSpacing);
	}

	// Token: 0x060091F2 RID: 37362 RVA: 0x002F5918 File Offset: 0x002F3B18
	private Vector3 GetTextCenter()
	{
		if (this.IsPartOfWidget)
		{
			Vector3 v = this.m_widgetTransform.Rect.center;
			return WidgetTransform.GetRotationMatrixFromZNegativeToDesiredFacing(this.m_widgetTransform.Facing) * v;
		}
		return Vector3.zero;
	}

	// Token: 0x060091F3 RID: 37363 RVA: 0x002F596C File Offset: 0x002F3B6C
	private Quaternion GetTextRotation()
	{
		if (this.IsPartOfWidget)
		{
			return WidgetTransform.GetRotationFromZNegativeToDesiredFacing(this.m_widgetTransform.Facing);
		}
		return Quaternion.identity;
	}

	// Token: 0x060091F4 RID: 37364 RVA: 0x002F598C File Offset: 0x002F3B8C
	private void UpdateWordWrapSettings(float containerWidth, float underwearWidthLocalAdjustment, float underwearHeightLocalAdjustment, bool ellipsis)
	{
		this.m_wordWrapSettings.Clear();
		this.m_wordWrapSettings.OriginalWidth = containerWidth;
		this.m_wordWrapSettings.Width = this.GetWidth();
		this.m_wordWrapSettings.Height = this.GetHeight();
		this.m_wordWrapSettings.UseUnderwear = this.m_Underwear;
		this.m_wordWrapSettings.UnderwearWidth = this.m_UnderwearWidth;
		this.m_wordWrapSettings.UnderwearHeight = this.m_UnderwearHeight;
		this.m_wordWrapSettings.UnderwearWidthLocaleAdjustment = underwearWidthLocalAdjustment;
		this.m_wordWrapSettings.UnderwearHeightLocaleAdjustment = underwearHeightLocalAdjustment;
		this.m_wordWrapSettings.UnderwearFlip = this.m_UnderwearFlip;
		this.m_wordWrapSettings.Ellipsized = ellipsis;
		this.m_wordWrapSettings.RichText = this.m_RichText;
		this.m_wordWrapSettings.ResizeToFit = this.m_ResizeToFit;
		this.m_wordWrapSettings.ForceWrapLargeWords = this.m_ForceWrapLargeWords;
		this.m_wordWrapSettings.Alignment = this.m_Alignment;
	}

	// Token: 0x060091F5 RID: 37365 RVA: 0x002F5A80 File Offset: 0x002F3C80
	private void AdjustUnderwearForLocale(out float localizedUnderwearWidth, out float localizedUnderwearHeight, float width)
	{
		localizedUnderwearWidth = this.m_UnderwearWidth;
		localizedUnderwearHeight = this.m_UnderwearHeight;
		UberText.LocalizationSettings localizedSettings = this.m_LocalizedSettings;
		UberText.LocalizationSettings.LocaleAdjustment localeAdjustment = (localizedSettings != null) ? localizedSettings.GetLocale(Localization.GetLocale()) : null;
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
		float height = this.GetHeight();
		if (this.m_UnderwearFlip)
		{
			localizedUnderwearHeight = height * localizedUnderwearHeight;
		}
		else
		{
			localizedUnderwearHeight = height * (1f - localizedUnderwearHeight);
		}
		localizedUnderwearWidth = width * (1f - localizedUnderwearWidth);
	}

	// Token: 0x060091F6 RID: 37366 RVA: 0x002F5B14 File Offset: 0x002F3D14
	private string WordWrapString(string text, float width, bool ellipses)
	{
		float underwearWidthLocalAdjustment;
		float underwearHeightLocalAdjustment;
		this.AdjustUnderwearForLocale(out underwearWidthLocalAdjustment, out underwearHeightLocalAdjustment, width);
		this.UpdateWordWrapSettings(width, underwearWidthLocalAdjustment, underwearHeightLocalAdjustment, ellipses);
		return this.m_UberTextLineWrapper.WordWrapString(text, this.m_wordWrapSettings, ref this.m_LineCount, ref this.m_Ellipsized);
	}

	// Token: 0x060091F7 RID: 37367 RVA: 0x002F5B54 File Offset: 0x002F3D54
	private string WordWrapString(string text, float width)
	{
		return this.WordWrapString(text, width, false);
	}

	// Token: 0x060091F8 RID: 37368 RVA: 0x002F5B60 File Offset: 0x002F3D60
	private string ProcessText(string text)
	{
		if (!this.m_RichText)
		{
			return text;
		}
		if (!this.m_WordWrap)
		{
			text = UberText.RemoveLineBreakTagsHardSpace(text);
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("<material=1></material>");
		stringBuilder.Append(text);
		for (int i = 0; i < text.Length; i++)
		{
			if (text[i] == '<' && i <= text.Length - 2 && text[i + 1] == 'b' && (i + 4 >= text.Length || text[i + 3] != '<' || text[i + 4] != '/'))
			{
				this.Bold();
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

	// Token: 0x060091F9 RID: 37369 RVA: 0x002F5C44 File Offset: 0x002F3E44
	private string LocalizationFixes(string text)
	{
		if (Localization.GetLocale() == Locale.thTH)
		{
			return this.FixThai(text);
		}
		return text;
	}

	// Token: 0x060091FA RID: 37370 RVA: 0x002F5C58 File Offset: 0x002F3E58
	private void SetupShadow()
	{
		if (!this.m_Shadow)
		{
			this.m_UberTextRenderToTexture.RemoveShadow();
			return;
		}
		float num = -this.m_ShadowOffset * 0.01f;
		float y = this.m_ShadowDepthOffset * 0.01f;
		this.m_UberTextRenderToTexture.InitShadow(base.gameObject, new Vector3(num, y, num));
		this.m_UberTextRenderToTexture.SetShadowColor(this.m_ShadowColor);
		this.m_UberTextRenderToTexture.SetShadowOffset(this.m_ShadowBlur);
	}

	// Token: 0x060091FB RID: 37371 RVA: 0x002F5CD0 File Offset: 0x002F3ED0
	private void UpdateOutlineProperties()
	{
		if (this.m_Outline)
		{
			Vector2 vector = this.TexelSize(this.m_UberTextRendering.GetFontTexture());
			float num = this.m_OutlineSize * this.m_OutlineModifier * UberTextLocalization.GetOutlineModifier(this.m_LocalizedSettings);
			Vector2 outlineBoldOffset = vector * (num + this.m_BoldSize * 0.75f);
			this.m_UberTextRendering.SetOutlineOffset(vector * num);
			this.m_UberTextRendering.SetOutlineBoldOffset(outlineBoldOffset);
			this.m_UberTextRendering.SetTexelSize(vector);
			this.m_UberTextRendering.SetTextColor(this.m_TextColor);
			this.m_UberTextRendering.SetOutlineColor(this.m_OutlineColor);
		}
	}

	// Token: 0x060091FC RID: 37372 RVA: 0x002F5D78 File Offset: 0x002F3F78
	private void Bold()
	{
		if (!this.m_HasBoldText)
		{
			this.m_HasBoldText = true;
			if (this.m_BoldSize > 10f)
			{
				this.m_BoldSize = 10f;
			}
			Vector2 a = this.TexelSize(this.m_UberTextRendering.GetFontTexture());
			this.m_UberTextRendering.SetBoldOffset(a * this.m_BoldSize);
		}
	}

	// Token: 0x060091FD RID: 37373 RVA: 0x002F5DD5 File Offset: 0x002F3FD5
	public void EnablePopupRendering(PopupRoot popupRoot)
	{
		this.m_popupRoot = popupRoot;
		this.m_meshReadyTracker.RegisterSetListener(new Action<object>(this.ApplyPopupRendering), null, true, false);
	}

	// Token: 0x060091FE RID: 37374 RVA: 0x002F5DF8 File Offset: 0x002F3FF8
	public void DisablePopupRendering()
	{
		this.m_popupRoot = null;
		this.m_meshReadyTracker.RemoveSetListener(new Action<object>(this.ApplyPopupRendering));
		this.RemovePopupRendering();
	}

	// Token: 0x060091FF RID: 37375 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public bool ShouldPropagatePopupRendering()
	{
		return false;
	}

	// Token: 0x06009200 RID: 37376 RVA: 0x002F5E20 File Offset: 0x002F4020
	private void ApplyPopupRendering(object unused)
	{
		if (this.m_UberTextRendering.GetTextMeshRenderer() != null && this.m_popupRoot != null)
		{
			GameObject gameObject = base.gameObject;
			if (!this.m_popupRenderingEnabled)
			{
				this.m_originalLayerBeforePopupRendering = gameObject.layer;
			}
			gameObject.layer = 29;
			this.UpdateLayers();
			this.m_popupRenderingEnabled = true;
			PopupRenderer popupRenderer = this.m_UberTextRendering.GetPopupRenderer();
			if (popupRenderer)
			{
				popupRenderer.EnablePopupRendering(this.m_popupRoot);
			}
			PopupRenderer popupRenderer2 = this.m_UberTextRenderToTexture.GetPopupRenderer();
			if (popupRenderer2)
			{
				popupRenderer2.EnablePopupRendering(this.m_popupRoot);
			}
		}
	}

	// Token: 0x06009201 RID: 37377 RVA: 0x002F5EC0 File Offset: 0x002F40C0
	private void RemovePopupRendering()
	{
		if (this.m_UberTextRendering == null)
		{
			return;
		}
		if (this.m_UberTextRendering.GetTextMeshRenderer() != null)
		{
			this.m_popupRenderingEnabled = false;
			base.gameObject.layer = this.m_originalLayerBeforePopupRendering;
			this.UpdateLayers();
			this.m_UberTextRendering.DisablePopupRenderer();
			this.m_UberTextRenderToTexture.DisablePopupRenderer();
		}
	}

	// Token: 0x06009202 RID: 37378 RVA: 0x002F5F1D File Offset: 0x002F411D
	public static void DisableCache()
	{
		UberText.s_disableCache = true;
		UberText.s_CachedText.Clear();
	}

	// Token: 0x06009203 RID: 37379 RVA: 0x002F5F30 File Offset: 0x002F4130
	private void SetWorldWidthAndHeight()
	{
		float width = this.GetWidth();
		float height = this.GetHeight();
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
		this.m_WorldWidth = worldWidth;
		this.m_WorldHeight = worldHeight;
	}

	// Token: 0x06009204 RID: 37380 RVA: 0x002F5FC4 File Offset: 0x002F41C4
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

	// Token: 0x06009205 RID: 37381 RVA: 0x002F6010 File Offset: 0x002F4210
	private Vector3 GetLossyWorldScale(Transform xform)
	{
		Quaternion rotation = xform.rotation;
		xform.rotation = Quaternion.identity;
		Vector3 lossyScale = base.transform.lossyScale;
		xform.rotation = rotation;
		return lossyScale;
	}

	// Token: 0x06009206 RID: 37382 RVA: 0x002F6044 File Offset: 0x002F4244
	private Vector2Int CalcTextureSize()
	{
		float width = this.GetWidth();
		float height = this.GetHeight();
		Vector2 vector = new Vector2((float)this.m_Resolution, (float)this.m_Resolution);
		if (width > height)
		{
			vector.x = (float)this.m_Resolution;
			vector.y = (float)this.m_Resolution * (height / width);
		}
		else
		{
			vector.x = (float)this.m_Resolution * (width / height);
			vector.y = (float)this.m_Resolution;
		}
		GraphicsManager graphicsManager;
		if (HearthstoneServices.TryGet<GraphicsManager>(out graphicsManager) && graphicsManager.RenderQualityLevel == GraphicsQuality.Low)
		{
			vector.x *= 0.75f;
			vector.y *= 0.75f;
		}
		return new Vector2Int((int)vector.x, (int)vector.y);
	}

	// Token: 0x06009207 RID: 37383 RVA: 0x002F6100 File Offset: 0x002F4300
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
			}
			else if (word[j] == '>')
			{
				flag2 = false;
			}
			else if (word[j] == '[' && j + 2 < word.Length && UberText.IsValidSquareBracketTag(word[j + 1]) && word[j + 2] == ']')
			{
				flag2 = true;
			}
			else
			{
				if (word[j] == ']')
				{
					if (j - 2 >= 0 && UberText.IsValidSquareBracketTag(word[j - 1]) && word[j - 2] == '[')
					{
						flag2 = false;
						goto IL_133;
					}
					flag2 = false;
				}
				if (!flag2)
				{
					stringBuilder.Append(word[j]);
				}
			}
			IL_133:;
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06009208 RID: 37384 RVA: 0x002F625C File Offset: 0x002F445C
	public static string RemoveLineBreakTagsHardSpace(string text)
	{
		StringBuilder stringBuilder = new StringBuilder(text.Length, text.Length);
		bool flag = false;
		for (int i = 0; i < text.Length; i++)
		{
			if (text[i] == '[' && i + 2 < text.Length && UberText.IsValidSquareBracketTag(text[i + 1]) && text[i + 2] == ']')
			{
				flag = true;
			}
			else
			{
				if (text[i] == ']')
				{
					if (i - 2 >= 0 && UberText.IsValidSquareBracketTag(text[i - 1]) && text[i - 2] == '[')
					{
						flag = false;
						goto IL_AD;
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
			IL_AD:;
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06009209 RID: 37385 RVA: 0x002F632C File Offset: 0x002F452C
	private static bool IsValidSquareBracketTag(char ch)
	{
		return ch == 'b' || ch == 'd' || ch == 'x';
	}

	// Token: 0x0600920A RID: 37386 RVA: 0x002F6340 File Offset: 0x002F4540
	private static bool IsWhitespaceOrUnderscore(char ch)
	{
		switch (ch)
		{
		case '\t':
		case '\n':
		case '\r':
			break;
		case '\v':
		case '\f':
			return false;
		default:
			if (ch != ' ' && ch != '_')
			{
				return false;
			}
			break;
		}
		return true;
	}

	// Token: 0x0600920B RID: 37387 RVA: 0x002F636C File Offset: 0x002F456C
	public static string RemoveMarkupAndCollapseWhitespaces(string text)
	{
		if (text == null)
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = null;
		int num = 0;
		int num2 = 0;
		int num3 = text.IndexOfAny(UberText.STRIP_CHARS_INDEX_OF_ANY, num2);
		while (num3 >= 0 && num3 < text.Length)
		{
			num2 = num3 + 1;
			if ((text[num3] != ' ' || (num3 + 1 < text.Length && UberText.IsWhitespaceOrUnderscore(text[num3 + 1]))) && (text[num3] != '[' || (num3 + 2 < text.Length && UberText.IsValidSquareBracketTag(text[num3 + 1]) && text[num3 + 2] == ']')))
			{
				if (num3 > num)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					string value = text.Substring(num, num3 - num);
					stringBuilder.Append(value);
				}
				if (UberText.IsWhitespaceOrUnderscore(text[num3]))
				{
					while (num2 < text.Length && UberText.IsWhitespaceOrUnderscore(text[num2]))
					{
						num2++;
					}
					if ((text[num3] != '\n' && text[num3] != '\r') || num3 <= 0 || text[num3 - 1] != '-')
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
						}
						stringBuilder.Append(' ');
					}
					num = num2;
				}
				else
				{
					char value2 = (text[num3] == '<') ? '>' : ']';
					int num4 = text.IndexOf(value2, num2);
					if (num4 < 0)
					{
						num2 = text.Length;
						num = num2;
						break;
					}
					num2 = num4 + 1;
					num = num2;
				}
			}
			num3 = text.IndexOfAny(UberText.STRIP_CHARS_INDEX_OF_ANY, num2);
		}
		if (num < text.Length && num > 0)
		{
			stringBuilder.Append(text.Substring(num));
		}
		if (stringBuilder != null)
		{
			return stringBuilder.ToString();
		}
		if (num == 0)
		{
			return text;
		}
		return string.Empty;
	}

	// Token: 0x0600920C RID: 37388 RVA: 0x002F6513 File Offset: 0x002F4713
	private void CleanUp()
	{
		this.m_Offset = 0f;
		if (this.m_UberTextRendering != null)
		{
			this.m_UberTextRendering.Destroy();
		}
		if (this.m_UberTextRenderToTexture != null)
		{
			this.m_UberTextRenderToTexture.Destroy();
		}
		this.m_updated = false;
	}

	// Token: 0x0600920D RID: 37389 RVA: 0x002F6550 File Offset: 0x002F4750
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

	// Token: 0x0600920E RID: 37390 RVA: 0x002F6580 File Offset: 0x002F4780
	public Vector2 TexelSize(Texture texture)
	{
		if (!texture)
		{
			return Vector2.zero;
		}
		int frameCount = Time.frameCount;
		Font key = this.m_Font;
		if (this.m_LocalizedFont != null)
		{
			key = this.m_LocalizedFont;
		}
		if (UberText.s_TexelUpdateFrame.ContainsKey(key) && UberText.s_TexelUpdateFrame[key] == frameCount)
		{
			return UberText.s_TexelUpdateData[key];
		}
		Vector2 vector = default(Vector2);
		vector.x = 1f / (float)texture.width;
		vector.y = 1f / (float)texture.height;
		UberText.s_TexelUpdateFrame[key] = frameCount;
		UberText.s_TexelUpdateData[key] = vector;
		return vector;
	}

	// Token: 0x0600920F RID: 37391 RVA: 0x002F6630 File Offset: 0x002F4830
	private static void DeleteOldCacheFiles()
	{
		foreach (object obj in Enum.GetValues(typeof(Locale)))
		{
			Locale locale = (Locale)obj;
			string text = string.Format("{0}/text_{1}.cache", FileUtils.PersistentDataPath, locale);
			if (File.Exists(text))
			{
				try
				{
					File.Delete(text);
				}
				catch (Exception ex)
				{
					Debug.LogError(string.Format("UberText.DeleteOldCacheFiles() - Failed to delete {0}. Reason={1}", text, ex.Message));
				}
			}
		}
	}

	// Token: 0x06009210 RID: 37392 RVA: 0x002F66DC File Offset: 0x002F48DC
	private static string GetCacheFolderPath()
	{
		return string.Format("{0}/UberText", FileUtils.CachePath);
	}

	// Token: 0x06009211 RID: 37393 RVA: 0x002F66ED File Offset: 0x002F48ED
	private static string GetCacheFilePath()
	{
		return string.Format("{0}/text_{1}.cache", UberText.GetCacheFolderPath(), Localization.GetLocale());
	}

	// Token: 0x06009212 RID: 37394 RVA: 0x002F6708 File Offset: 0x002F4908
	private static void CreateCacheFolder()
	{
		string cacheFolderPath = UberText.GetCacheFolderPath();
		if (Directory.Exists(cacheFolderPath))
		{
			return;
		}
		try
		{
			Directory.CreateDirectory(cacheFolderPath);
		}
		catch (Exception ex)
		{
			Debug.LogError(string.Format("UberText.CreateCacheFolder() - Failed to create {0}. Reason={1}", cacheFolderPath, ex.Message));
		}
	}

	// Token: 0x06009213 RID: 37395 RVA: 0x002F6758 File Offset: 0x002F4958
	public static void StoreCachedData()
	{
		if (UberText.s_disableCache)
		{
			return;
		}
		UberText.CreateCacheFolder();
		string cacheFilePath = UberText.GetCacheFilePath();
		using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(cacheFilePath, FileMode.Create)))
		{
			int value = 84593;
			binaryWriter.Write(value);
			foreach (KeyValuePair<int, UberText.CachedTextValues> keyValuePair in UberText.s_CachedText)
			{
				binaryWriter.Write(keyValuePair.Key);
				binaryWriter.Write(keyValuePair.Value.m_Text);
				binaryWriter.Write(keyValuePair.Value.m_CharSize);
				binaryWriter.Write(keyValuePair.Value.m_OriginalTextHash);
			}
		}
		Log.UberText.Print("UberText Cache Stored: " + cacheFilePath, Array.Empty<object>());
	}

	// Token: 0x06009214 RID: 37396 RVA: 0x002F6848 File Offset: 0x002F4A48
	public static IEnumerator<IAsyncJobResult> Job_LoadCachedData()
	{
		if (UberText.s_disableCache)
		{
			yield break;
		}
		UberText.s_CachedText.Clear();
		UberText.DeleteOldCacheFiles();
		UberText.CreateCacheFolder();
		string cacheFilePath = UberText.GetCacheFilePath();
		if (!File.Exists(cacheFilePath))
		{
			yield break;
		}
		int num = 84593;
		try
		{
			using (BinaryReader binaryReader = new BinaryReader(File.Open(cacheFilePath, FileMode.Open)))
			{
				if (binaryReader.BaseStream.Length == 0L)
				{
					yield break;
				}
				if (binaryReader.ReadInt32() != num)
				{
					yield break;
				}
				if (binaryReader.PeekChar() == -1)
				{
					yield break;
				}
				while (binaryReader.PeekChar() != -1)
				{
					int key = binaryReader.ReadInt32();
					UberText.CachedTextValues cachedTextValues = new UberText.CachedTextValues();
					cachedTextValues.m_Text = binaryReader.ReadString();
					cachedTextValues.m_CharSize = binaryReader.ReadSingle();
					cachedTextValues.m_OriginalTextHash = binaryReader.ReadInt32();
					UberText.s_CachedText.Add(key, cachedTextValues);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning(string.Format("UberText LoadCachedData() failed: {0}", ex.Message));
			UberText.s_CachedText.Clear();
		}
		if (UberText.s_CachedText.Count > 50000)
		{
			UberText.s_CachedText.Clear();
		}
		Log.UberText.Print("UberText Cache Loaded: " + UberText.s_CachedText.Count.ToString(), Array.Empty<object>());
		yield break;
	}

	// Token: 0x06009215 RID: 37397 RVA: 0x002F6850 File Offset: 0x002F4A50
	private string FixThai(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		char[] array = text.ToCharArray();
		UberText.ThaiGlyphType[] array2 = new UberText.ThaiGlyphType[array.Count<char>()];
		StringBuilder stringBuilder = new StringBuilder(text);
		for (int i = 0; i < array.Count<char>(); i++)
		{
			char c = array[i];
			if ((c >= 'ก' && c <= 'ฯ') || c == 'ะ' || c == 'เ' || c == 'แ')
			{
				if (c == 'ฝ' || c == 'ฟ' || c == 'ป' || c == 'ฬ')
				{
					array2[i] = UberText.ThaiGlyphType.BASE_ASCENDER;
				}
				else if (c == 'ฏ' || c == 'ฎ')
				{
					array2[i] = UberText.ThaiGlyphType.BASE_DESCENDER;
				}
				else
				{
					array2[i] = UberText.ThaiGlyphType.BASE;
				}
			}
			else if (c >= '่' && c <= '์')
			{
				array2[i] = UberText.ThaiGlyphType.TONE_MARK;
			}
			else if (c == 'ั' || c == 'ิ' || c == 'ี' || c == 'ึ' || c == 'ื' || c == '็' || c == 'ํ')
			{
				array2[i] = UberText.ThaiGlyphType.UPPER;
			}
			else if (c == 'ุ' || c == 'ู' || c == 'ฺ')
			{
				array2[i] = UberText.ThaiGlyphType.LOWER;
			}
		}
		for (int j = 0; j < array.Count<char>(); j++)
		{
			char c2 = array[j];
			UberText.ThaiGlyphType thaiGlyphType = array2[j];
			stringBuilder[j] = c2;
			if (j >= 1)
			{
				UberText.ThaiGlyphType thaiGlyphType2 = array2[j - 1];
				char c3 = array[j - 1];
				if (thaiGlyphType == UberText.ThaiGlyphType.UPPER && thaiGlyphType2 == UberText.ThaiGlyphType.BASE_ASCENDER)
				{
					switch (c2)
					{
					case 'ั':
						stringBuilder[j] = '';
						break;
					case 'า':
					case 'ำ':
						break;
					case 'ิ':
						stringBuilder[j] = '';
						break;
					case 'ี':
						stringBuilder[j] = '';
						break;
					case 'ึ':
						stringBuilder[j] = '';
						break;
					case 'ื':
						stringBuilder[j] = '';
						break;
					default:
						if (c2 != '็')
						{
							if (c2 == 'ํ')
							{
								stringBuilder[j] = '';
							}
						}
						else
						{
							stringBuilder[j] = '';
						}
						break;
					}
				}
				else if (thaiGlyphType == UberText.ThaiGlyphType.LOWER && thaiGlyphType2 == UberText.ThaiGlyphType.BASE_DESCENDER)
				{
					stringBuilder[j] = c2 + '';
				}
				else
				{
					if (thaiGlyphType == UberText.ThaiGlyphType.LOWER)
					{
						if (c3 == 'ญ')
						{
							stringBuilder[j - 1] = '';
							goto IL_391;
						}
						if (c3 == 'ฐ')
						{
							stringBuilder[j - 1] = '';
							goto IL_391;
						}
					}
					if (thaiGlyphType == UberText.ThaiGlyphType.TONE_MARK)
					{
						if (j - 2 >= 0)
						{
							if (thaiGlyphType2 == UberText.ThaiGlyphType.UPPER && array2[j - 2] == UberText.ThaiGlyphType.BASE_ASCENDER)
							{
								stringBuilder[j] = c2 + '';
							}
							if (thaiGlyphType2 == UberText.ThaiGlyphType.LOWER && j > 1)
							{
								thaiGlyphType2 = array2[j - 2];
								c3 = array[j - 2];
							}
						}
						if (j < array.Count<char>() - 1 && (array[j + 1] == 'ำ' || array[j + 1] == 'ํ'))
						{
							if (thaiGlyphType2 == UberText.ThaiGlyphType.BASE_ASCENDER)
							{
								stringBuilder[j] = c2 + '';
								stringBuilder.Insert(j + 1, '');
								stringBuilder.Insert(j + 2, c2);
								if (array[j + 1] == 'ำ')
								{
									stringBuilder[j + 1] = 'ำ';
								}
								j++;
								goto IL_391;
							}
						}
						else if (thaiGlyphType2 == UberText.ThaiGlyphType.BASE || thaiGlyphType2 == UberText.ThaiGlyphType.BASE_DESCENDER)
						{
							stringBuilder[j] = c2 + '';
							goto IL_391;
						}
						if (thaiGlyphType2 == UberText.ThaiGlyphType.BASE_ASCENDER)
						{
							stringBuilder[j] = c2 + '';
						}
					}
				}
			}
			IL_391:;
		}
		return stringBuilder.ToString();
	}

	// Token: 0x04007A2D RID: 31277
	private const int CACHE_FILE_VERSION_TEMP = 2;

	// Token: 0x04007A2E RID: 31278
	private const int CACHE_FILE_MAX_SIZE = 50000;

	// Token: 0x04007A2F RID: 31279
	private const string FONT_NAME_BLIZZARD_GLOBAL = "BlizzardGlobal";

	// Token: 0x04007A30 RID: 31280
	private const string FONT_NAME_BELWE_OUTLINE = "Belwe_Outline";

	// Token: 0x04007A31 RID: 31281
	private const string FONT_NAME_BELWE = "Belwe";

	// Token: 0x04007A32 RID: 31282
	private const string FONT_NAME_FRANKLIN_GOTHIC = "FranklinGothic";

	// Token: 0x04007A33 RID: 31283
	private const float CHARACTER_SIZE_SCALE = 0.01f;

	// Token: 0x04007A34 RID: 31284
	private const float BOLD_MAX_SIZE = 10f;

	// Token: 0x04007A35 RID: 31285
	public const int WORD_CHARACTER_BUFFER = 3;

	// Token: 0x04007A36 RID: 31286
	private const int MAX_RESIZE_TEXT_ITERATION_COUNT = 40;

	// Token: 0x04007A37 RID: 31287
	[SerializeField]
	private string m_Text = "Uber Text";

	// Token: 0x04007A38 RID: 31288
	[SerializeField]
	private bool m_GameStringLookup;

	// Token: 0x04007A39 RID: 31289
	[SerializeField]
	private bool m_UseEditorText;

	// Token: 0x04007A3A RID: 31290
	[SerializeField]
	private float m_Width = 1f;

	// Token: 0x04007A3B RID: 31291
	[SerializeField]
	private float m_Height = 1f;

	// Token: 0x04007A3C RID: 31292
	[SerializeField]
	private float m_LineSpacing = 1f;

	// Token: 0x04007A3D RID: 31293
	[SerializeField]
	private Font m_Font;

	// Token: 0x04007A3E RID: 31294
	[SerializeField]
	private int m_FontSize = 35;

	// Token: 0x04007A3F RID: 31295
	[SerializeField]
	private int m_MinFontSize = 10;

	// Token: 0x04007A40 RID: 31296
	[SerializeField]
	private float m_CharacterSize = 5f;

	// Token: 0x04007A41 RID: 31297
	[SerializeField]
	private float m_MinCharacterSize = 1f;

	// Token: 0x04007A42 RID: 31298
	[SerializeField]
	private bool m_RichText = true;

	// Token: 0x04007A43 RID: 31299
	[SerializeField]
	private Color m_TextColor = Color.white;

	// Token: 0x04007A44 RID: 31300
	[SerializeField]
	private float m_BoldSize;

	// Token: 0x04007A45 RID: 31301
	[SerializeField]
	private bool m_WordWrap;

	// Token: 0x04007A46 RID: 31302
	[SerializeField]
	private bool m_ForceWrapLargeWords;

	// Token: 0x04007A47 RID: 31303
	[SerializeField]
	private bool m_ResizeToFit;

	// Token: 0x04007A48 RID: 31304
	[SerializeField]
	private bool m_ResizeToFitAndGrow;

	// Token: 0x04007A49 RID: 31305
	[SerializeField]
	private float m_ResizeToGrowStep = 0.05f;

	// Token: 0x04007A4A RID: 31306
	[SerializeField]
	private bool m_Underwear;

	// Token: 0x04007A4B RID: 31307
	[SerializeField]
	private bool m_UnderwearFlip;

	// Token: 0x04007A4C RID: 31308
	[SerializeField]
	private float m_UnderwearWidth = 0.2f;

	// Token: 0x04007A4D RID: 31309
	[SerializeField]
	private float m_UnderwearHeight = 0.2f;

	// Token: 0x04007A4E RID: 31310
	[SerializeField]
	private UberText.AlignmentOptions m_Alignment = UberText.AlignmentOptions.Center;

	// Token: 0x04007A4F RID: 31311
	[SerializeField]
	private UberText.AnchorOptions m_Anchor = UberText.AnchorOptions.Middle;

	// Token: 0x04007A50 RID: 31312
	[SerializeField]
	private bool m_RenderToTexture;

	// Token: 0x04007A51 RID: 31313
	[SerializeField]
	private GameObject m_RenderOnObject;

	// Token: 0x04007A52 RID: 31314
	[SerializeField]
	private int m_Resolution = 256;

	// Token: 0x04007A53 RID: 31315
	[SerializeField]
	private bool m_Outline;

	// Token: 0x04007A54 RID: 31316
	[SerializeField]
	private float m_OutlineSize = 1f;

	// Token: 0x04007A55 RID: 31317
	[SerializeField]
	private Color m_OutlineColor = Color.black;

	// Token: 0x04007A56 RID: 31318
	[SerializeField]
	private bool m_AntiAlias;

	// Token: 0x04007A57 RID: 31319
	[SerializeField]
	private float m_AntiAliasAmount = 0.5f;

	// Token: 0x04007A58 RID: 31320
	[SerializeField]
	private float m_AntiAliasEdge = 0.5f;

	// Token: 0x04007A59 RID: 31321
	[SerializeField]
	private bool m_Shadow;

	// Token: 0x04007A5A RID: 31322
	[SerializeField]
	private float m_ShadowOffset = 1f;

	// Token: 0x04007A5B RID: 31323
	[SerializeField]
	private float m_ShadowDepthOffset;

	// Token: 0x04007A5C RID: 31324
	[SerializeField]
	private Color m_ShadowColor = new Color(0.1f, 0.1f, 0.1f, 0.333f);

	// Token: 0x04007A5D RID: 31325
	[SerializeField]
	private float m_ShadowBlur = 1.5f;

	// Token: 0x04007A5E RID: 31326
	[SerializeField]
	private int m_ShadowRenderQueueOffset = -1;

	// Token: 0x04007A5F RID: 31327
	[SerializeField]
	private int m_RenderQueue;

	// Token: 0x04007A60 RID: 31328
	[SerializeField]
	private float m_AmbientLightBlend;

	// Token: 0x04007A61 RID: 31329
	[SerializeField]
	private List<Material> m_AdditionalMaterials = new List<Material>();

	// Token: 0x04007A62 RID: 31330
	[SerializeField]
	private Color m_GradientUpperColor = Color.white;

	// Token: 0x04007A63 RID: 31331
	[SerializeField]
	private Color m_GradientLowerColor = Color.white;

	// Token: 0x04007A64 RID: 31332
	[SerializeField]
	private bool m_Cache = true;

	// Token: 0x04007A65 RID: 31333
	[SerializeField]
	private UberText.LocalizationSettings m_LocalizedSettings;

	// Token: 0x04007A66 RID: 31334
	private bool m_isFontDefLoaded;

	// Token: 0x04007A67 RID: 31335
	private Font m_LocalizedFont;

	// Token: 0x04007A68 RID: 31336
	private float m_LineSpaceModifier = 1f;

	// Token: 0x04007A69 RID: 31337
	private float m_SingleLineAdjustment;

	// Token: 0x04007A6A RID: 31338
	private float m_FontSizeModifier = 1f;

	// Token: 0x04007A6B RID: 31339
	private float m_CharacterSizeModifier = 1f;

	// Token: 0x04007A6C RID: 31340
	private float m_UnboundCharacterSizeModifier = 1f;

	// Token: 0x04007A6D RID: 31341
	private float m_OutlineModifier = 1f;

	// Token: 0x04007A6E RID: 31342
	private float m_WorldWidth;

	// Token: 0x04007A6F RID: 31343
	private float m_WorldHeight;

	// Token: 0x04007A70 RID: 31344
	private bool m_updated;

	// Token: 0x04007A71 RID: 31345
	private string m_PreviousText = string.Empty;

	// Token: 0x04007A72 RID: 31346
	private int m_LineCount;

	// Token: 0x04007A73 RID: 31347
	private Vector2 m_PreviousTexelSize;

	// Token: 0x04007A74 RID: 31348
	private Vector2Int m_PreviousFontSize;

	// Token: 0x04007A75 RID: 31349
	private bool m_Ellipsized;

	// Token: 0x04007A76 RID: 31350
	private int m_CacheHash;

	// Token: 0x04007A77 RID: 31351
	private bool m_Hidden;

	// Token: 0x04007A78 RID: 31352
	private bool m_TextSet;

	// Token: 0x04007A79 RID: 31353
	private FlagStateTracker m_meshReadyTracker;

	// Token: 0x04007A7A RID: 31354
	private Bounds m_UnderwearLeftBounds;

	// Token: 0x04007A7B RID: 31355
	private Bounds m_UnderwearRightBounds;

	// Token: 0x04007A7C RID: 31356
	private WidgetTransform m_widgetTransform;

	// Token: 0x04007A7D RID: 31357
	private bool m_HadWidgetTransformLastCheck;

	// Token: 0x04007A7E RID: 31358
	private UberTextRendering m_UberTextRendering;

	// Token: 0x04007A7F RID: 31359
	private UberTextLineWrapper m_UberTextLineWrapper;

	// Token: 0x04007A80 RID: 31360
	private UberTextRenderToTexture m_UberTextRenderToTexture;

	// Token: 0x04007A81 RID: 31361
	private bool m_HasBoldText;

	// Token: 0x04007A82 RID: 31362
	private int m_CurrentLayer;

	// Token: 0x04007A83 RID: 31363
	private static float s_offset = -3000f;

	// Token: 0x04007A84 RID: 31364
	private float m_Offset;

	// Token: 0x04007A85 RID: 31365
	private readonly StringBuilder m_forceLargeWrapRichTextString = new StringBuilder();

	// Token: 0x04007A86 RID: 31366
	private readonly StringBuilder m_forceLargeWrapTestString = new StringBuilder();

	// Token: 0x04007A87 RID: 31367
	private WordWrapSettings m_wordWrapSettings;

	// Token: 0x04007A88 RID: 31368
	private static bool s_disableCache = false;

	// Token: 0x04007A89 RID: 31369
	private static Map<int, UberText.CachedTextValues> s_CachedText = new Map<int, UberText.CachedTextValues>();

	// Token: 0x04007A8A RID: 31370
	private static Map<Font, int> s_TexelUpdateFrame = new Map<Font, int>();

	// Token: 0x04007A8B RID: 31371
	private static Map<Font, Vector2> s_TexelUpdateData = new Map<Font, Vector2>();

	// Token: 0x04007A8C RID: 31372
	private PopupRoot m_popupRoot;

	// Token: 0x04007A8D RID: 31373
	private int m_originalLayerBeforePopupRendering;

	// Token: 0x04007A8E RID: 31374
	private bool m_popupRenderingEnabled;

	// Token: 0x04007A8F RID: 31375
	private static readonly char[] STRIP_CHARS_INDEX_OF_ANY = new char[]
	{
		'<',
		'[',
		'\\',
		' ',
		'\t',
		'\r',
		'\n',
		'_'
	};

	// Token: 0x020026E2 RID: 9954
	[Serializable]
	public class LocalizationSettings
	{
		// Token: 0x06013895 RID: 80021 RVA: 0x005376F8 File Offset: 0x005358F8
		public LocalizationSettings()
		{
			this.m_LocaleAdjustments = new List<UberText.LocalizationSettings.LocaleAdjustment>();
		}

		// Token: 0x06013896 RID: 80022 RVA: 0x0053770B File Offset: 0x0053590B
		public bool HasLocale(Locale locale)
		{
			return this.GetLocale(locale) != null;
		}

		// Token: 0x06013897 RID: 80023 RVA: 0x0053771C File Offset: 0x0053591C
		public UberText.LocalizationSettings.LocaleAdjustment GetLocale(Locale locale)
		{
			foreach (UberText.LocalizationSettings.LocaleAdjustment localeAdjustment in this.m_LocaleAdjustments)
			{
				if (localeAdjustment.m_Locale == locale)
				{
					return localeAdjustment;
				}
			}
			return null;
		}

		// Token: 0x06013898 RID: 80024 RVA: 0x00537778 File Offset: 0x00535978
		public UberText.LocalizationSettings.LocaleAdjustment AddLocale(Locale locale)
		{
			UberText.LocalizationSettings.LocaleAdjustment localeAdjustment = this.GetLocale(locale);
			if (localeAdjustment != null)
			{
				return localeAdjustment;
			}
			localeAdjustment = new UberText.LocalizationSettings.LocaleAdjustment(locale);
			this.m_LocaleAdjustments.Add(localeAdjustment);
			return localeAdjustment;
		}

		// Token: 0x06013899 RID: 80025 RVA: 0x005377A8 File Offset: 0x005359A8
		public void RemoveLocale(Locale locale)
		{
			for (int i = 0; i < this.m_LocaleAdjustments.Count; i++)
			{
				if (this.m_LocaleAdjustments[i].m_Locale == locale)
				{
					this.m_LocaleAdjustments.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x0400F26E RID: 62062
		public List<UberText.LocalizationSettings.LocaleAdjustment> m_LocaleAdjustments;

		// Token: 0x020029A6 RID: 10662
		[Serializable]
		public class LocaleAdjustment
		{
			// Token: 0x06013FAB RID: 81835 RVA: 0x00541FC0 File Offset: 0x005401C0
			public LocaleAdjustment()
			{
				this.m_Locale = Locale.enUS;
			}

			// Token: 0x06013FAC RID: 81836 RVA: 0x0054201C File Offset: 0x0054021C
			public LocaleAdjustment(Locale locale)
			{
				this.m_Locale = locale;
			}

			// Token: 0x0400FDF9 RID: 65017
			public Locale m_Locale;

			// Token: 0x0400FDFA RID: 65018
			public float m_LineSpaceModifier = 1f;

			// Token: 0x0400FDFB RID: 65019
			public float m_SingleLineAdjustment;

			// Token: 0x0400FDFC RID: 65020
			public float m_Width;

			// Token: 0x0400FDFD RID: 65021
			public float m_Height;

			// Token: 0x0400FDFE RID: 65022
			public float m_FontSizeModifier = 1f;

			// Token: 0x0400FDFF RID: 65023
			public float m_UnderwearWidth;

			// Token: 0x0400FE00 RID: 65024
			public float m_UnderwearHeight;

			// Token: 0x0400FE01 RID: 65025
			public float m_OutlineModifier = 1f;

			// Token: 0x0400FE02 RID: 65026
			public float m_UnboundCharacterSizeModifier = 1f;

			// Token: 0x0400FE03 RID: 65027
			public float m_ResizeToFitWidthModifier = 1f;

			// Token: 0x0400FE04 RID: 65028
			public Vector3 m_PositionOffset = Vector3.zero;
		}
	}

	// Token: 0x020026E3 RID: 9955
	private class CachedTextKeyData
	{
		// Token: 0x0601389A RID: 80026 RVA: 0x005377EC File Offset: 0x005359EC
		public override int GetHashCode()
		{
			return this.m_Text.Length + this.m_FontSize + this.m_Text.GetHashCode() + this.m_FontSize.GetHashCode() - this.m_CharSize.GetHashCode() + this.m_Width.GetHashCode() - this.m_Height.GetHashCode() + this.m_Font.GetHashCode() - this.m_LineSpacing.GetHashCode();
		}

		// Token: 0x0400F26F RID: 62063
		public string m_Text;

		// Token: 0x0400F270 RID: 62064
		public int m_FontSize;

		// Token: 0x0400F271 RID: 62065
		public float m_CharSize;

		// Token: 0x0400F272 RID: 62066
		public float m_Width;

		// Token: 0x0400F273 RID: 62067
		public float m_Height;

		// Token: 0x0400F274 RID: 62068
		public Font m_Font;

		// Token: 0x0400F275 RID: 62069
		public float m_LineSpacing;
	}

	// Token: 0x020026E4 RID: 9956
	[Serializable]
	private class CachedTextValues
	{
		// Token: 0x0400F276 RID: 62070
		public string m_Text;

		// Token: 0x0400F277 RID: 62071
		public float m_CharSize;

		// Token: 0x0400F278 RID: 62072
		public int m_OriginalTextHash;
	}

	// Token: 0x020026E5 RID: 9957
	public enum AlignmentOptions
	{
		// Token: 0x0400F27A RID: 62074
		Left,
		// Token: 0x0400F27B RID: 62075
		Center,
		// Token: 0x0400F27C RID: 62076
		Right
	}

	// Token: 0x020026E6 RID: 9958
	public enum AnchorOptions
	{
		// Token: 0x0400F27E RID: 62078
		Upper,
		// Token: 0x0400F27F RID: 62079
		Middle,
		// Token: 0x0400F280 RID: 62080
		Lower
	}

	// Token: 0x020026E7 RID: 9959
	private enum TextRenderMaterial
	{
		// Token: 0x0400F282 RID: 62082
		Text,
		// Token: 0x0400F283 RID: 62083
		Bold,
		// Token: 0x0400F284 RID: 62084
		Outline,
		// Token: 0x0400F285 RID: 62085
		InlineImages
	}

	// Token: 0x020026E8 RID: 9960
	private enum Fonts
	{
		// Token: 0x0400F287 RID: 62087
		BlizzardGlobal,
		// Token: 0x0400F288 RID: 62088
		Belwe,
		// Token: 0x0400F289 RID: 62089
		BelweOutline,
		// Token: 0x0400F28A RID: 62090
		FranklinGothic
	}

	// Token: 0x020026E9 RID: 9961
	private enum ThaiGlyphType
	{
		// Token: 0x0400F28C RID: 62092
		BASE,
		// Token: 0x0400F28D RID: 62093
		BASE_ASCENDER,
		// Token: 0x0400F28E RID: 62094
		BASE_DESCENDER,
		// Token: 0x0400F28F RID: 62095
		TONE_MARK,
		// Token: 0x0400F290 RID: 62096
		UPPER,
		// Token: 0x0400F291 RID: 62097
		LOWER
	}
}
