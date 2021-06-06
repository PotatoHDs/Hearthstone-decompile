using System;
using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

// Token: 0x0200013B RID: 315
public class SetFilterItem : PegUIElement
{
	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x06001482 RID: 5250 RVA: 0x00075A8D File Offset: 0x00073C8D
	// (set) Token: 0x06001483 RID: 5251 RVA: 0x00075A95 File Offset: 0x00073C95
	public bool IsHeader
	{
		get
		{
			return this.m_isHeader;
		}
		set
		{
			this.m_isHeader = value;
		}
	}

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x06001484 RID: 5252 RVA: 0x00075A9E File Offset: 0x00073C9E
	// (set) Token: 0x06001485 RID: 5253 RVA: 0x00075AAB File Offset: 0x00073CAB
	public string Text
	{
		get
		{
			return this.m_uberText.Text;
		}
		set
		{
			this.m_uberText.Text = value;
		}
	}

	// Token: 0x170000EB RID: 235
	// (get) Token: 0x06001486 RID: 5254 RVA: 0x00075AB9 File Offset: 0x00073CB9
	// (set) Token: 0x06001487 RID: 5255 RVA: 0x00075AC1 File Offset: 0x00073CC1
	public FormatType FormatType
	{
		get
		{
			return this.m_formatType;
		}
		set
		{
			this.m_formatType = value;
		}
	}

	// Token: 0x170000EC RID: 236
	// (get) Token: 0x06001488 RID: 5256 RVA: 0x00075ACA File Offset: 0x00073CCA
	// (set) Token: 0x06001489 RID: 5257 RVA: 0x00075AD2 File Offset: 0x00073CD2
	public bool IsAllStandard
	{
		get
		{
			return this.m_isAllStandard;
		}
		set
		{
			this.m_isAllStandard = value;
		}
	}

	// Token: 0x170000ED RID: 237
	// (get) Token: 0x0600148A RID: 5258 RVA: 0x00075ADB File Offset: 0x00073CDB
	// (set) Token: 0x0600148B RID: 5259 RVA: 0x00075AE3 File Offset: 0x00073CE3
	public List<TAG_CARD_SET> CardSets
	{
		get
		{
			return this.m_cardSets;
		}
		set
		{
			this.m_cardSets = value;
		}
	}

	// Token: 0x170000EE RID: 238
	// (get) Token: 0x0600148C RID: 5260 RVA: 0x00075AEC File Offset: 0x00073CEC
	// (set) Token: 0x0600148D RID: 5261 RVA: 0x00075AF4 File Offset: 0x00073CF4
	public List<int> SpecificCards
	{
		get
		{
			return this.m_metaShakeupEvents;
		}
		set
		{
			this.m_metaShakeupEvents = value;
		}
	}

	// Token: 0x170000EF RID: 239
	// (get) Token: 0x0600148E RID: 5262 RVA: 0x00075AFD File Offset: 0x00073CFD
	// (set) Token: 0x0600148F RID: 5263 RVA: 0x00075B05 File Offset: 0x00073D05
	public float Height
	{
		get
		{
			return this.m_height;
		}
		set
		{
			this.m_height = value;
		}
	}

	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x06001490 RID: 5264 RVA: 0x00075B0E File Offset: 0x00073D0E
	// (set) Token: 0x06001491 RID: 5265 RVA: 0x00075B16 File Offset: 0x00073D16
	public SetFilterItem.ItemSelectedCallback Callback
	{
		get
		{
			return this.m_callback;
		}
		set
		{
			this.m_callback = value;
		}
	}

	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x06001492 RID: 5266 RVA: 0x00075B1F File Offset: 0x00073D1F
	// (set) Token: 0x06001493 RID: 5267 RVA: 0x00075B27 File Offset: 0x00073D27
	public string TooltipHeadline
	{
		get
		{
			return this.m_tooltipHeadline;
		}
		set
		{
			this.m_tooltipHeadline = value;
		}
	}

	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x06001494 RID: 5268 RVA: 0x00075B30 File Offset: 0x00073D30
	// (set) Token: 0x06001495 RID: 5269 RVA: 0x00075B38 File Offset: 0x00073D38
	public string TooltipDescription
	{
		get
		{
			return this.m_tooltipDescription;
		}
		set
		{
			this.m_tooltipDescription = value;
		}
	}

	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x06001496 RID: 5270 RVA: 0x00075B41 File Offset: 0x00073D41
	// (set) Token: 0x06001497 RID: 5271 RVA: 0x00075B49 File Offset: 0x00073D49
	public bool ShowTooltip
	{
		get
		{
			return this.m_showTooltip;
		}
		set
		{
			this.m_showTooltip = value;
		}
	}

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x06001498 RID: 5272 RVA: 0x00075B52 File Offset: 0x00073D52
	// (set) Token: 0x06001499 RID: 5273 RVA: 0x00075B6C File Offset: 0x00073D6C
	public Texture IconTexture
	{
		get
		{
			return this.m_icon.GetMaterial().GetTexture("_MainTex");
		}
		set
		{
			if (value == null)
			{
				this.m_icon.gameObject.SetActive(false);
			}
			else
			{
				this.m_icon.gameObject.SetActive(true);
			}
			this.m_icon.GetMaterial().SetTexture("_MainTex", value);
		}
	}

	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x0600149A RID: 5274 RVA: 0x00075BBC File Offset: 0x00073DBC
	// (set) Token: 0x0600149B RID: 5275 RVA: 0x00075BD8 File Offset: 0x00073DD8
	public UnityEngine.Vector2? IconOffset
	{
		get
		{
			return new UnityEngine.Vector2?(this.m_icon.GetMaterial().GetTextureOffset("_MainTex"));
		}
		set
		{
			if (value == null || this.IconTexture == null)
			{
				this.m_icon.gameObject.SetActive(false);
				return;
			}
			this.m_icon.gameObject.SetActive(this.IconTexture != null);
			this.m_icon.GetMaterial().SetTextureOffset("_MainTex", value.Value);
		}
	}

	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x0600149C RID: 5276 RVA: 0x00075C46 File Offset: 0x00073E46
	// (set) Token: 0x0600149D RID: 5277 RVA: 0x00075C4E File Offset: 0x00073E4E
	public TooltipZone Tooltip
	{
		get
		{
			return this.m_tooltipZone;
		}
		private set
		{
			this.m_tooltipZone = value;
		}
	}

	// Token: 0x0600149E RID: 5278 RVA: 0x00075C58 File Offset: 0x00073E58
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		if (this.m_mouseOverGlow != null && !UniversalInputManager.Get().IsTouchMode())
		{
			this.m_mouseOverGlow.SetActive(true);
			SoundManager.Get().LoadAndPlay("Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9", base.gameObject);
		}
		if (this.m_tooltipZone != null && this.m_showTooltip)
		{
			this.m_tooltipZone.ShowBoxTooltip(this.m_tooltipHeadline, this.m_tooltipDescription, 0);
		}
	}

	// Token: 0x0600149F RID: 5279 RVA: 0x00075CD4 File Offset: 0x00073ED4
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		if (this.m_mouseOverGlow != null)
		{
			this.m_mouseOverGlow.SetActive(false);
		}
		if (!this.m_isSelected && this.m_pressedShadow != null)
		{
			this.m_pressedShadow.SetActive(false);
		}
		if (this.m_tooltipZone != null)
		{
			this.m_tooltipZone.HideTooltip();
		}
	}

	// Token: 0x060014A0 RID: 5280 RVA: 0x00075D36 File Offset: 0x00073F36
	public void SetSelected(bool selected)
	{
		this.m_selectedGlow.SetActive(selected);
		if (this.m_pressedShadow != null)
		{
			this.m_pressedShadow.SetActive(selected);
		}
		this.m_isSelected = selected;
	}

	// Token: 0x060014A1 RID: 5281 RVA: 0x00075D65 File Offset: 0x00073F65
	protected override void OnPress()
	{
		if (this.m_pressedShadow != null && !UniversalInputManager.Get().IsTouchMode())
		{
			this.m_pressedShadow.SetActive(true);
		}
	}

	// Token: 0x060014A2 RID: 5282 RVA: 0x00075D8D File Offset: 0x00073F8D
	protected override void OnRelease()
	{
		if (!this.m_isSelected && this.m_pressedShadow != null)
		{
			this.m_pressedShadow.SetActive(false);
			SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		}
	}

	// Token: 0x060014A3 RID: 5283 RVA: 0x00075DC5 File Offset: 0x00073FC5
	public void SetIconFxActive(bool active)
	{
		if (this.m_iconFX == null)
		{
			return;
		}
		this.m_iconFX.SetActive(active);
	}

	// Token: 0x04000DBD RID: 3517
	public UberText m_uberText;

	// Token: 0x04000DBE RID: 3518
	public GameObject m_selectedGlow;

	// Token: 0x04000DBF RID: 3519
	public MeshRenderer m_icon;

	// Token: 0x04000DC0 RID: 3520
	public GameObject m_mouseOverGlow;

	// Token: 0x04000DC1 RID: 3521
	public GameObject m_pressedShadow;

	// Token: 0x04000DC2 RID: 3522
	public GameObject m_iconFX;

	// Token: 0x04000DC3 RID: 3523
	public TooltipZone m_tooltipZone;

	// Token: 0x04000DC4 RID: 3524
	private bool m_isHeader;

	// Token: 0x04000DC5 RID: 3525
	private FormatType m_formatType;

	// Token: 0x04000DC6 RID: 3526
	private bool m_isAllStandard;

	// Token: 0x04000DC7 RID: 3527
	private List<TAG_CARD_SET> m_cardSets;

	// Token: 0x04000DC8 RID: 3528
	private List<int> m_metaShakeupEvents;

	// Token: 0x04000DC9 RID: 3529
	private float m_height;

	// Token: 0x04000DCA RID: 3530
	private SetFilterItem.ItemSelectedCallback m_callback;

	// Token: 0x04000DCB RID: 3531
	private bool m_isSelected;

	// Token: 0x04000DCC RID: 3532
	private string m_tooltipHeadline;

	// Token: 0x04000DCD RID: 3533
	private string m_tooltipDescription;

	// Token: 0x04000DCE RID: 3534
	private bool m_showTooltip;

	// Token: 0x020014E0 RID: 5344
	// (Invoke) Token: 0x0600DC90 RID: 56464
	public delegate void ItemSelectedCallback(List<TAG_CARD_SET> cardSets, List<int> specificCards, FormatType formatType, SetFilterItem selectedItem, bool transitionPage);
}
