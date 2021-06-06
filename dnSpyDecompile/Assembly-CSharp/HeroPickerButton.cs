using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000B18 RID: 2840
public class HeroPickerButton : PegUIElement
{
	// Token: 0x060096E3 RID: 38627 RVA: 0x0030D018 File Offset: 0x0030B218
	protected override void OnDestroy()
	{
		this.ReleaseFullDef();
		base.OnDestroy();
	}

	// Token: 0x060096E4 RID: 38628 RVA: 0x0030D026 File Offset: 0x0030B226
	public void SetPreconDeckID(long preconDeckID)
	{
		this.m_preconDeckID = preconDeckID;
	}

	// Token: 0x060096E5 RID: 38629 RVA: 0x0030D02F File Offset: 0x0030B22F
	public long GetPreconDeckID()
	{
		return this.m_preconDeckID;
	}

	// Token: 0x060096E6 RID: 38630 RVA: 0x0030D037 File Offset: 0x0030B237
	public CollectionDeck GetPreconCollectionDeck()
	{
		return CollectionManager.Get().GetDeck(this.m_preconDeckID);
	}

	// Token: 0x060096E7 RID: 38631 RVA: 0x0030D049 File Offset: 0x0030B249
	public bool IsDeckValid()
	{
		return this.m_isDeckValid;
	}

	// Token: 0x060096E8 RID: 38632 RVA: 0x0030D051 File Offset: 0x0030B251
	public void SetIsDeckValid(bool isValid)
	{
		if (this.m_isDeckValid == isValid)
		{
			return;
		}
		this.m_isDeckValid = isValid;
	}

	// Token: 0x060096E9 RID: 38633 RVA: 0x0030D064 File Offset: 0x0030B264
	public virtual void UpdateDisplay(DefLoader.DisposableFullDef def, TAG_PREMIUM premium)
	{
		this.SetFullDef(def);
		this.SetPremium(premium);
	}

	// Token: 0x060096EA RID: 38634 RVA: 0x0030D074 File Offset: 0x0030B274
	public void SetClassIcon(Material mat)
	{
		Renderer component = this.m_heroClassIcon.GetComponent<Renderer>();
		component.SetMaterial(mat);
		component.GetMaterial().renderQueue = 3007;
		if (this.m_heroClassIconSepia != null)
		{
			Renderer component2 = this.m_heroClassIconSepia.GetComponent<Renderer>();
			component2.GetMaterial().SetTextureOffset("_MainTex", component.GetMaterial().GetTextureOffset("_MainTex"));
			component2.GetMaterial().SetTextureScale("_MainTex", component.GetMaterial().GetTextureScale("_MainTex"));
			component2.GetMaterial().renderQueue = 3007;
		}
	}

	// Token: 0x060096EB RID: 38635 RVA: 0x0030D10C File Offset: 0x0030B30C
	public void SetClassname(string s)
	{
		this.m_classLabel.Text = s;
	}

	// Token: 0x060096EC RID: 38636 RVA: 0x00090064 File Offset: 0x0008E264
	public virtual GuestHeroDbfRecord GetGuestHero()
	{
		return null;
	}

	// Token: 0x060096ED RID: 38637 RVA: 0x0030D11A File Offset: 0x0030B31A
	public void HideTextAndGradient()
	{
		this.m_classLabel.Hide();
		this.m_labelGradient.SetActive(false);
	}

	// Token: 0x060096EE RID: 38638 RVA: 0x0030D133 File Offset: 0x0030B333
	public void SetFullDef(DefLoader.DisposableFullDef def)
	{
		this.ReleaseFullDef();
		this.m_fullDef = ((def != null) ? def.Share() : null);
		this.UpdatePortrait();
	}

	// Token: 0x060096EF RID: 38639 RVA: 0x0030D153 File Offset: 0x0030B353
	public EntityDef GetEntityDef()
	{
		DefLoader.DisposableFullDef fullDef = this.m_fullDef;
		if (fullDef == null)
		{
			return null;
		}
		return fullDef.EntityDef;
	}

	// Token: 0x060096F0 RID: 38640 RVA: 0x0030D166 File Offset: 0x0030B366
	public DefLoader.DisposableCardDef ShareEntityDef()
	{
		DefLoader.DisposableFullDef fullDef = this.m_fullDef;
		if (fullDef == null)
		{
			return null;
		}
		DefLoader.DisposableCardDef disposableCardDef = fullDef.DisposableCardDef;
		if (disposableCardDef == null)
		{
			return null;
		}
		return disposableCardDef.Share();
	}

	// Token: 0x060096F1 RID: 38641 RVA: 0x0030D184 File Offset: 0x0030B384
	public DefLoader.DisposableFullDef ShareFullDef()
	{
		DefLoader.DisposableFullDef fullDef = this.m_fullDef;
		if (fullDef == null)
		{
			return null;
		}
		return fullDef.Share();
	}

	// Token: 0x060096F2 RID: 38642 RVA: 0x0030D197 File Offset: 0x0030B397
	public void SetSelected(bool isSelected)
	{
		this.m_isSelected = isSelected;
		if (isSelected)
		{
			this.ShowIncompleteDeckTooltip(false);
			this.Lower();
			return;
		}
		this.Raise();
	}

	// Token: 0x060096F3 RID: 38643 RVA: 0x0030D1B7 File Offset: 0x0030B3B7
	public bool IsSelected()
	{
		return this.m_isSelected;
	}

	// Token: 0x060096F4 RID: 38644 RVA: 0x0030D1BF File Offset: 0x0030B3BF
	public void SetLockReasonText(string text)
	{
		if (this.m_lockReasonText == null)
		{
			return;
		}
		this.m_lockReasonText.Text = text;
	}

	// Token: 0x060096F5 RID: 38645 RVA: 0x0030D1DC File Offset: 0x0030B3DC
	public void Lower()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.Activate(false);
		}
		float num;
		if (this.m_locked)
		{
			num = 0.7f;
		}
		else
		{
			num = -0.7f;
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			new Vector3(base.GetOriginalLocalPosition().x, base.GetOriginalLocalPosition().y + num, base.GetOriginalLocalPosition().z),
			"time",
			0.1f,
			"easeType",
			iTween.EaseType.linear,
			"isLocal",
			true
		});
		iTween.MoveTo((this.m_raiseAndLowerRoot != null) ? this.m_raiseAndLowerRoot : base.gameObject, args);
	}

	// Token: 0x060096F6 RID: 38646 RVA: 0x0030D2B0 File Offset: 0x0030B4B0
	public void Raise()
	{
		if (this.m_isSelected)
		{
			return;
		}
		this.Activate(true);
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			new Vector3(base.GetOriginalLocalPosition().x, base.GetOriginalLocalPosition().y, base.GetOriginalLocalPosition().z),
			"time",
			0.1f,
			"easeType",
			iTween.EaseType.linear,
			"isLocal",
			true
		});
		iTween.MoveTo(this.m_raiseAndLowerRoot, args);
	}

	// Token: 0x060096F7 RID: 38647 RVA: 0x0030D354 File Offset: 0x0030B554
	public void SetHighlightState(ActorStateType stateType)
	{
		if (this.m_highlightState == null)
		{
			this.m_highlightState = base.GetComponentInChildren<HighlightState>();
		}
		if (this.m_highlightState != null)
		{
			this.m_highlightState.ChangeState(stateType);
		}
		this.ShowIncompleteDeckTooltip(stateType == ActorStateType.HIGHLIGHT_MOUSE_OVER);
	}

	// Token: 0x060096F8 RID: 38648 RVA: 0x0030D3A1 File Offset: 0x0030B5A1
	public void ShowIncompleteDeckTooltip(bool show)
	{
		if (show)
		{
			if (!this.IsDeckValid())
			{
				this.m_tooltipZone.ShowTooltip(GameStrings.Get("GLUE_INCOMPLETE_DECK_HEADER"), GameStrings.Get("GLUE_INCOMPLETE_DECK_DESC"), 4f, 0);
				return;
			}
		}
		else
		{
			this.m_tooltipZone.HideTooltip();
		}
	}

	// Token: 0x060096F9 RID: 38649 RVA: 0x0030D3E0 File Offset: 0x0030B5E0
	public void Activate(bool enable)
	{
		this.SetEnabled(enable, false);
	}

	// Token: 0x060096FA RID: 38650 RVA: 0x0030D3EA File Offset: 0x0030B5EA
	public virtual void Lock()
	{
		this.m_locked = true;
	}

	// Token: 0x060096FB RID: 38651 RVA: 0x0030D3F3 File Offset: 0x0030B5F3
	public virtual void Unlock()
	{
		this.m_locked = false;
		if (this.m_unlockedCallback != null)
		{
			this.m_unlockedCallback(this);
		}
	}

	// Token: 0x060096FC RID: 38652 RVA: 0x0030D410 File Offset: 0x0030B610
	public void OnAnimationTriggerUnlock()
	{
		this.Unlock();
		this.Activate(true);
	}

	// Token: 0x060096FD RID: 38653 RVA: 0x0030D41F File Offset: 0x0030B61F
	public void SetProgress(int acknowledgedProgress, int currProgress, int maxProgress)
	{
		this.SetProgress(acknowledgedProgress, currProgress, maxProgress, true);
	}

	// Token: 0x060096FE RID: 38654 RVA: 0x0030D42B File Offset: 0x0030B62B
	public void SetProgress(int acknowledgedProgress, int currProgress, int maxProgress, bool shouldAnimate)
	{
		if (currProgress >= maxProgress)
		{
			this.Unlock();
		}
	}

	// Token: 0x060096FF RID: 38655 RVA: 0x0030D437 File Offset: 0x0030B637
	public bool IsLocked()
	{
		return this.m_locked;
	}

	// Token: 0x06009700 RID: 38656 RVA: 0x0030D43F File Offset: 0x0030B63F
	public void SetUnlockedCallback(HeroPickerButton.UnlockedCallback unlockedCallback)
	{
		this.m_unlockedCallback = unlockedCallback;
	}

	// Token: 0x06009701 RID: 38657 RVA: 0x0030D448 File Offset: 0x0030B648
	public TAG_PREMIUM GetPremium()
	{
		return this.m_premium;
	}

	// Token: 0x06009702 RID: 38658 RVA: 0x0030D450 File Offset: 0x0030B650
	public void SetPremium(TAG_PREMIUM premium)
	{
		this.m_premium = premium;
		this.UpdatePortrait();
	}

	// Token: 0x06009703 RID: 38659 RVA: 0x0030D460 File Offset: 0x0030B660
	public HeroPickerOptionDataModel GetDataModel()
	{
		WidgetTemplate component = base.GetComponent<WidgetTemplate>();
		IDataModel dataModel = null;
		if (component != null && !component.GetDataModel(6, out dataModel))
		{
			dataModel = new HeroPickerOptionDataModel();
			component.BindDataModel(dataModel, false);
		}
		return dataModel as HeroPickerOptionDataModel;
	}

	// Token: 0x1700088E RID: 2190
	// (get) Token: 0x06009704 RID: 38660 RVA: 0x0030D49E File Offset: 0x0030B69E
	public bool HasCardDef
	{
		get
		{
			DefLoader.DisposableFullDef fullDef = this.m_fullDef;
			return ((fullDef != null) ? fullDef.CardDef : null) != null;
		}
	}

	// Token: 0x1700088F RID: 2191
	// (get) Token: 0x06009705 RID: 38661 RVA: 0x0030D4B8 File Offset: 0x0030B6B8
	public string HeroPickerSelectedPrefab
	{
		get
		{
			if (!this.HasCardDef)
			{
				return null;
			}
			return this.m_fullDef.CardDef.m_HeroPickerSelectedPrefab;
		}
	}

	// Token: 0x06009706 RID: 38662 RVA: 0x0030D4D4 File Offset: 0x0030B6D4
	protected Material GetClassIconMaterial(TAG_CLASS classTag)
	{
		int index = 0;
		switch (classTag)
		{
		case TAG_CLASS.INVALID:
		case TAG_CLASS.NEUTRAL:
			index = 10;
			break;
		case TAG_CLASS.DRUID:
			index = 5;
			break;
		case TAG_CLASS.HUNTER:
			index = 4;
			break;
		case TAG_CLASS.MAGE:
			index = 7;
			break;
		case TAG_CLASS.PALADIN:
			index = 3;
			break;
		case TAG_CLASS.PRIEST:
			index = 8;
			break;
		case TAG_CLASS.ROGUE:
			index = 2;
			break;
		case TAG_CLASS.SHAMAN:
			index = 1;
			break;
		case TAG_CLASS.WARLOCK:
			index = 6;
			break;
		case TAG_CLASS.WARRIOR:
			index = 0;
			break;
		case TAG_CLASS.DEMONHUNTER:
			index = 9;
			break;
		}
		return this.CLASS_MATERIALS[index];
	}

	// Token: 0x06009707 RID: 38663 RVA: 0x0030D560 File Offset: 0x0030B760
	protected virtual void UpdatePortrait()
	{
		DefLoader.DisposableFullDef fullDef = this.m_fullDef;
		CardDef cardDef = (fullDef != null) ? fullDef.CardDef : null;
		if (cardDef == null)
		{
			return;
		}
		Material deckPickerPortrait = cardDef.GetDeckPickerPortrait();
		if (deckPickerPortrait == null)
		{
			return;
		}
		DeckPickerHero component = base.GetComponent<DeckPickerHero>();
		Renderer component2 = component.m_PortraitMesh.GetComponent<Renderer>();
		List<Material> materials = component2.GetMaterials();
		Material premiumPortraitMaterial = cardDef.GetPremiumPortraitMaterial();
		if (this.m_premium == TAG_PREMIUM.GOLDEN && premiumPortraitMaterial != null)
		{
			materials[component.m_PortraitMaterialIndex] = premiumPortraitMaterial;
			materials[component.m_PortraitMaterialIndex].mainTextureOffset = deckPickerPortrait.mainTextureOffset;
			materials[component.m_PortraitMaterialIndex].mainTextureScale = deckPickerPortrait.mainTextureScale;
			materials[component.m_PortraitMaterialIndex].SetTexture("_ShadowTex", null);
			if (this.m_seed == null)
			{
				this.m_seed = new float?(UnityEngine.Random.value);
			}
			Material material = component2.GetMaterial();
			if (material.HasProperty("_Seed"))
			{
				material.SetFloat("_Seed", this.m_seed.Value);
			}
		}
		else
		{
			materials[component.m_PortraitMaterialIndex] = deckPickerPortrait;
		}
		component2.SetMaterials(materials);
		if (cardDef.GetPremiumPortraitAnimation())
		{
			UberShaderController uberShaderController = component.m_PortraitMesh.GetComponent<UberShaderController>();
			if (uberShaderController == null)
			{
				uberShaderController = component.m_PortraitMesh.AddComponent<UberShaderController>();
			}
			uberShaderController.UberShaderAnimation = UnityEngine.Object.Instantiate<UberShaderAnimation>(cardDef.GetPremiumPortraitAnimation());
			uberShaderController.m_MaterialIndex = 0;
		}
	}

	// Token: 0x06009708 RID: 38664 RVA: 0x0030D6DB File Offset: 0x0030B8DB
	protected override void OnRelease()
	{
		if (this.m_isDeckValid)
		{
			this.Lower();
		}
	}

	// Token: 0x06009709 RID: 38665 RVA: 0x0030D6EB File Offset: 0x0030B8EB
	protected void ReleaseFullDef()
	{
		DefLoader.DisposableFullDef fullDef = this.m_fullDef;
		if (fullDef != null)
		{
			fullDef.Dispose();
		}
		this.m_fullDef = null;
	}

	// Token: 0x0600970A RID: 38666 RVA: 0x0030D705 File Offset: 0x0030B905
	public void SetDivotTexture(Texture texture)
	{
		base.GetComponent<DeckPickerHero>().m_DivotMesh.GetMaterial().mainTexture = texture;
	}

	// Token: 0x0600970B RID: 38667 RVA: 0x0030D71D File Offset: 0x0030B91D
	public void SetDivotVisible(bool visible)
	{
		base.GetComponent<DeckPickerHero>().m_DivotMesh.gameObject.SetActive(visible);
	}

	// Token: 0x04007E5F RID: 32351
	public GameObject m_heroClassIcon;

	// Token: 0x04007E60 RID: 32352
	public GameObject m_heroClassIconSepia;

	// Token: 0x04007E61 RID: 32353
	public UberText m_classLabel;

	// Token: 0x04007E62 RID: 32354
	public GameObject m_labelGradient;

	// Token: 0x04007E63 RID: 32355
	public GameObject m_button;

	// Token: 0x04007E64 RID: 32356
	public GameObject m_buttonFrame;

	// Token: 0x04007E65 RID: 32357
	public TAG_CLASS m_heroClass;

	// Token: 0x04007E66 RID: 32358
	public List<Material> CLASS_MATERIALS = new List<Material>();

	// Token: 0x04007E67 RID: 32359
	public HeroPickerButtonBones m_bones;

	// Token: 0x04007E68 RID: 32360
	public GameObject m_missingCardsIndicator;

	// Token: 0x04007E69 RID: 32361
	public UberText m_missingCardsIndicatorText;

	// Token: 0x04007E6A RID: 32362
	public TooltipZone m_tooltipZone;

	// Token: 0x04007E6B RID: 32363
	public GameObject m_crown;

	// Token: 0x04007E6C RID: 32364
	public UberText m_lockReasonText;

	// Token: 0x04007E6D RID: 32365
	public GameObject m_raiseAndLowerRoot;

	// Token: 0x04007E6E RID: 32366
	protected DefLoader.DisposableFullDef m_fullDef;

	// Token: 0x04007E6F RID: 32367
	protected TAG_PREMIUM m_premium;

	// Token: 0x04007E70 RID: 32368
	protected float? m_seed;

	// Token: 0x04007E71 RID: 32369
	private bool m_isSelected;

	// Token: 0x04007E72 RID: 32370
	private HighlightState m_highlightState;

	// Token: 0x04007E73 RID: 32371
	private bool m_locked;

	// Token: 0x04007E74 RID: 32372
	private long m_preconDeckID;

	// Token: 0x04007E75 RID: 32373
	private HeroPickerButton.UnlockedCallback m_unlockedCallback;

	// Token: 0x04007E76 RID: 32374
	private bool m_isDeckValid = true;

	// Token: 0x04007E77 RID: 32375
	private static readonly Color BASIC_SET_COLOR_IN_PROGRESS = new Color(0.97f, 0.82f, 0.22f);

	// Token: 0x02002763 RID: 10083
	// (Invoke) Token: 0x060139CE RID: 80334
	public delegate void UnlockedCallback(HeroPickerButton button);
}
