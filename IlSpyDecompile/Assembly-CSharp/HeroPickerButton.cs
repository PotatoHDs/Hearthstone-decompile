using System.Collections;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

public class HeroPickerButton : PegUIElement
{
	public delegate void UnlockedCallback(HeroPickerButton button);

	public GameObject m_heroClassIcon;

	public GameObject m_heroClassIconSepia;

	public UberText m_classLabel;

	public GameObject m_labelGradient;

	public GameObject m_button;

	public GameObject m_buttonFrame;

	public TAG_CLASS m_heroClass;

	public List<Material> CLASS_MATERIALS = new List<Material>();

	public HeroPickerButtonBones m_bones;

	public GameObject m_missingCardsIndicator;

	public UberText m_missingCardsIndicatorText;

	public TooltipZone m_tooltipZone;

	public GameObject m_crown;

	public UberText m_lockReasonText;

	public GameObject m_raiseAndLowerRoot;

	protected DefLoader.DisposableFullDef m_fullDef;

	protected TAG_PREMIUM m_premium;

	protected float? m_seed;

	private bool m_isSelected;

	private HighlightState m_highlightState;

	private bool m_locked;

	private long m_preconDeckID;

	private UnlockedCallback m_unlockedCallback;

	private bool m_isDeckValid = true;

	private static readonly Color BASIC_SET_COLOR_IN_PROGRESS = new Color(0.97f, 0.82f, 0.22f);

	public bool HasCardDef => m_fullDef?.CardDef != null;

	public string HeroPickerSelectedPrefab
	{
		get
		{
			if (!HasCardDef)
			{
				return null;
			}
			return m_fullDef.CardDef.m_HeroPickerSelectedPrefab;
		}
	}

	protected override void OnDestroy()
	{
		ReleaseFullDef();
		base.OnDestroy();
	}

	public void SetPreconDeckID(long preconDeckID)
	{
		m_preconDeckID = preconDeckID;
	}

	public long GetPreconDeckID()
	{
		return m_preconDeckID;
	}

	public CollectionDeck GetPreconCollectionDeck()
	{
		return CollectionManager.Get().GetDeck(m_preconDeckID);
	}

	public bool IsDeckValid()
	{
		return m_isDeckValid;
	}

	public void SetIsDeckValid(bool isValid)
	{
		if (m_isDeckValid != isValid)
		{
			m_isDeckValid = isValid;
		}
	}

	public virtual void UpdateDisplay(DefLoader.DisposableFullDef def, TAG_PREMIUM premium)
	{
		SetFullDef(def);
		SetPremium(premium);
	}

	public void SetClassIcon(Material mat)
	{
		Renderer component = m_heroClassIcon.GetComponent<Renderer>();
		component.SetMaterial(mat);
		component.GetMaterial().renderQueue = 3007;
		if (m_heroClassIconSepia != null)
		{
			Renderer component2 = m_heroClassIconSepia.GetComponent<Renderer>();
			component2.GetMaterial().SetTextureOffset("_MainTex", component.GetMaterial().GetTextureOffset("_MainTex"));
			component2.GetMaterial().SetTextureScale("_MainTex", component.GetMaterial().GetTextureScale("_MainTex"));
			component2.GetMaterial().renderQueue = 3007;
		}
	}

	public void SetClassname(string s)
	{
		m_classLabel.Text = s;
	}

	public virtual GuestHeroDbfRecord GetGuestHero()
	{
		return null;
	}

	public void HideTextAndGradient()
	{
		m_classLabel.Hide();
		m_labelGradient.SetActive(value: false);
	}

	public void SetFullDef(DefLoader.DisposableFullDef def)
	{
		ReleaseFullDef();
		m_fullDef = def?.Share();
		UpdatePortrait();
	}

	public EntityDef GetEntityDef()
	{
		return m_fullDef?.EntityDef;
	}

	public DefLoader.DisposableCardDef ShareEntityDef()
	{
		return m_fullDef?.DisposableCardDef?.Share();
	}

	public DefLoader.DisposableFullDef ShareFullDef()
	{
		return m_fullDef?.Share();
	}

	public void SetSelected(bool isSelected)
	{
		m_isSelected = isSelected;
		if (isSelected)
		{
			ShowIncompleteDeckTooltip(show: false);
			Lower();
		}
		else
		{
			Raise();
		}
	}

	public bool IsSelected()
	{
		return m_isSelected;
	}

	public void SetLockReasonText(string text)
	{
		if (!(m_lockReasonText == null))
		{
			m_lockReasonText.Text = text;
		}
	}

	public void Lower()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			Activate(enable: false);
		}
		float num = ((!m_locked) ? (-0.7f) : 0.7f);
		Hashtable args = iTween.Hash("position", new Vector3(GetOriginalLocalPosition().x, GetOriginalLocalPosition().y + num, GetOriginalLocalPosition().z), "time", 0.1f, "easeType", iTween.EaseType.linear, "isLocal", true);
		iTween.MoveTo((m_raiseAndLowerRoot != null) ? m_raiseAndLowerRoot : base.gameObject, args);
	}

	public void Raise()
	{
		if (!m_isSelected)
		{
			Activate(enable: true);
			Hashtable args = iTween.Hash("position", new Vector3(GetOriginalLocalPosition().x, GetOriginalLocalPosition().y, GetOriginalLocalPosition().z), "time", 0.1f, "easeType", iTween.EaseType.linear, "isLocal", true);
			iTween.MoveTo(m_raiseAndLowerRoot, args);
		}
	}

	public void SetHighlightState(ActorStateType stateType)
	{
		if (m_highlightState == null)
		{
			m_highlightState = GetComponentInChildren<HighlightState>();
		}
		if (m_highlightState != null)
		{
			m_highlightState.ChangeState(stateType);
		}
		ShowIncompleteDeckTooltip(stateType == ActorStateType.HIGHLIGHT_MOUSE_OVER);
	}

	public void ShowIncompleteDeckTooltip(bool show)
	{
		if (show)
		{
			if (!IsDeckValid())
			{
				m_tooltipZone.ShowTooltip(GameStrings.Get("GLUE_INCOMPLETE_DECK_HEADER"), GameStrings.Get("GLUE_INCOMPLETE_DECK_DESC"), 4f);
			}
		}
		else
		{
			m_tooltipZone.HideTooltip();
		}
	}

	public void Activate(bool enable)
	{
		SetEnabled(enable);
	}

	public virtual void Lock()
	{
		m_locked = true;
	}

	public virtual void Unlock()
	{
		m_locked = false;
		if (m_unlockedCallback != null)
		{
			m_unlockedCallback(this);
		}
	}

	public void OnAnimationTriggerUnlock()
	{
		Unlock();
		Activate(enable: true);
	}

	public void SetProgress(int acknowledgedProgress, int currProgress, int maxProgress)
	{
		SetProgress(acknowledgedProgress, currProgress, maxProgress, shouldAnimate: true);
	}

	public void SetProgress(int acknowledgedProgress, int currProgress, int maxProgress, bool shouldAnimate)
	{
		if (currProgress >= maxProgress)
		{
			Unlock();
		}
	}

	public bool IsLocked()
	{
		return m_locked;
	}

	public void SetUnlockedCallback(UnlockedCallback unlockedCallback)
	{
		m_unlockedCallback = unlockedCallback;
	}

	public TAG_PREMIUM GetPremium()
	{
		return m_premium;
	}

	public void SetPremium(TAG_PREMIUM premium)
	{
		m_premium = premium;
		UpdatePortrait();
	}

	public HeroPickerOptionDataModel GetDataModel()
	{
		WidgetTemplate component = GetComponent<WidgetTemplate>();
		IDataModel model = null;
		if (component != null && !component.GetDataModel(6, out model))
		{
			model = new HeroPickerOptionDataModel();
			component.BindDataModel(model);
		}
		return model as HeroPickerOptionDataModel;
	}

	protected Material GetClassIconMaterial(TAG_CLASS classTag)
	{
		int index = 0;
		switch (classTag)
		{
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
		case TAG_CLASS.INVALID:
		case TAG_CLASS.NEUTRAL:
			index = 10;
			break;
		}
		return CLASS_MATERIALS[index];
	}

	protected virtual void UpdatePortrait()
	{
		CardDef cardDef = m_fullDef?.CardDef;
		if (cardDef == null)
		{
			return;
		}
		Material deckPickerPortrait = cardDef.GetDeckPickerPortrait();
		if (deckPickerPortrait == null)
		{
			return;
		}
		DeckPickerHero component = GetComponent<DeckPickerHero>();
		Renderer component2 = component.m_PortraitMesh.GetComponent<Renderer>();
		List<Material> materials = component2.GetMaterials();
		Material premiumPortraitMaterial = cardDef.GetPremiumPortraitMaterial();
		if (m_premium == TAG_PREMIUM.GOLDEN && premiumPortraitMaterial != null)
		{
			materials[component.m_PortraitMaterialIndex] = premiumPortraitMaterial;
			materials[component.m_PortraitMaterialIndex].mainTextureOffset = deckPickerPortrait.mainTextureOffset;
			materials[component.m_PortraitMaterialIndex].mainTextureScale = deckPickerPortrait.mainTextureScale;
			materials[component.m_PortraitMaterialIndex].SetTexture("_ShadowTex", null);
			if (!m_seed.HasValue)
			{
				m_seed = Random.value;
			}
			Material material = component2.GetMaterial();
			if (material.HasProperty("_Seed"))
			{
				material.SetFloat("_Seed", m_seed.Value);
			}
		}
		else
		{
			materials[component.m_PortraitMaterialIndex] = deckPickerPortrait;
		}
		component2.SetMaterials(materials);
		if ((bool)cardDef.GetPremiumPortraitAnimation())
		{
			UberShaderController uberShaderController = component.m_PortraitMesh.GetComponent<UberShaderController>();
			if (uberShaderController == null)
			{
				uberShaderController = component.m_PortraitMesh.AddComponent<UberShaderController>();
			}
			uberShaderController.UberShaderAnimation = Object.Instantiate(cardDef.GetPremiumPortraitAnimation());
			uberShaderController.m_MaterialIndex = 0;
		}
	}

	protected override void OnRelease()
	{
		if (m_isDeckValid)
		{
			Lower();
		}
	}

	protected void ReleaseFullDef()
	{
		m_fullDef?.Dispose();
		m_fullDef = null;
	}

	public void SetDivotTexture(Texture texture)
	{
		GetComponent<DeckPickerHero>().m_DivotMesh.GetMaterial().mainTexture = texture;
	}

	public void SetDivotVisible(bool visible)
	{
		GetComponent<DeckPickerHero>().m_DivotMesh.gameObject.SetActive(visible);
	}
}
