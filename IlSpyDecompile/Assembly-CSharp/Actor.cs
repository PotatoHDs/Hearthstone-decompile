using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.AssetManager;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class Actor : MonoBehaviour, IVisibleWidgetComponent
{
	protected readonly UnityEngine.Vector2 GEM_TEXTURE_OFFSET_RARE = new UnityEngine.Vector2(0.5f, 0f);

	protected readonly UnityEngine.Vector2 GEM_TEXTURE_OFFSET_EPIC = new UnityEngine.Vector2(0f, 0.5f);

	protected readonly UnityEngine.Vector2 GEM_TEXTURE_OFFSET_LEGENDARY = new UnityEngine.Vector2(0.5f, 0.5f);

	protected readonly UnityEngine.Vector2 GEM_TEXTURE_OFFSET_COMMON = new UnityEngine.Vector2(0f, 0f);

	protected readonly Color GEM_COLOR_RARE = new Color(0.1529f, 0.498f, 1f);

	protected readonly Color GEM_COLOR_EPIC = new Color(0.596f, 0.1568f, 0.7333f);

	protected readonly Color GEM_COLOR_LEGENDARY = new Color(1f, 0.5333f, 0f);

	protected readonly Color GEM_COLOR_COMMON = new Color(0.549f, 0.549f, 0.549f);

	protected readonly Color CLASS_COLOR_GENERIC = new Color(0.7f, 0.7f, 0.7f);

	protected readonly Color CLASS_COLOR_WARLOCK = new Color(0.33f, 0.2f, 0.4f);

	protected readonly Color CLASS_COLOR_ROGUE = new Color(0.23f, 0.23f, 0.23f);

	protected readonly Color CLASS_COLOR_DRUID = new Color(0.42f, 0.29f, 0.14f);

	protected readonly Color CLASS_COLOR_SHAMAN = new Color(0f, 0.32f, 0.71f);

	protected readonly Color CLASS_COLOR_HUNTER = new Color(0.26f, 0.54f, 0.18f);

	protected readonly Color CLASS_COLOR_MAGE = new Color(0.44f, 0.48f, 0.69f);

	protected readonly Color CLASS_COLOR_PALADIN = new Color(0.71f, 0.49f, 0.2f);

	protected readonly Color CLASS_COLOR_PRIEST = new Color(1f, 1f, 1f);

	protected readonly Color CLASS_COLOR_WARRIOR = new Color(0.43f, 0.14f, 0.14f);

	protected readonly Color CLASS_COLOR_DEATHKNIGHT = new Color(0.0666667f, 0.5294f, 0.5843f);

	protected readonly Color CLASS_COLOR_DEMONHUNTER = new Color(0.0902f, 0.2275f, 0.1961f);

	private readonly Color MISSING_CARD_WILD_GOLDEN_COLOR = new Color(0.518f, 0.361f, 0f, 0.68f);

	private readonly Color MISSING_CARD_STANDARD_GOLDEN_COLOR = new Color(0.867f, 0.675f, 0.22f, 0.53f);

	protected readonly Color MISSING_CARD_WILD_DIAMOND_COLOR = new Color(0.4705f, 0.3058f, 0.0117f, 0.6784f);

	protected readonly string MISSING_CARD_WILD_DIAMOND_CONTRAST_KEY = "_Contrast";

	protected readonly float MISSING_CARD_WILD_DIAMOND_CONTRAST = 0.4f;

	protected readonly string MISSING_CARD_WILD_DIAMOND_INTENSITY_KEY = "_Intensity";

	protected readonly float MISSING_CARD_WILD_DIAMOND_INTENSITY = 1.7f;

	protected readonly float WATERMARK_ALPHA_VALUE = 99f / 128f;

	public GameObject m_cardMesh;

	public int m_cardFrontMatIdx = -1;

	public int m_cardBackMatIdx = -1;

	public int m_premiumRibbon = -1;

	public GameObject m_portraitMesh;

	public GameObject m_portraitMeshRTT;

	public GameObject m_portraitMeshRTT_background;

	public bool m_usePlayPortrait;

	public int m_portraitFrameMatIdx = -1;

	public int m_portraitMatIdx = -1;

	public GameObject m_nameBannerMesh;

	public GameObject m_descriptionMesh;

	public GameObject m_descriptionTrimMesh;

	public GameObject m_watermarkMesh;

	public GameObject m_rarityFrameMesh;

	public GameObject m_rarityGemMesh;

	public GameObject m_racePlateMesh;

	public Mesh m_spellDescriptionMeshNeutral;

	public Mesh m_spellDescriptionMeshSchool;

	public GameObject m_attackObject;

	public GameObject m_healthObject;

	public GameObject m_armorObject;

	public GameObject m_manaObject;

	public GameObject m_racePlateObject;

	public GameObject m_cardTypeAnchorObject;

	public GameObject m_eliteObject;

	public GameObject m_classIconObject;

	public GameObject m_heroSpotLight;

	public GameObject m_glints;

	public GameObject m_armorSpellBone;

	public NestedPrefab m_multiClassBannerContainer;

	public NestedPrefab m_bannedRibbonContainer;

	public GameObject m_bannerContainer;

	public GameObject m_banner;

	public GameObject m_bannerBottom;

	public UberText m_bannerText;

	public UberText m_costTextMesh;

	public UberText m_attackTextMesh;

	public UberText m_healthTextMesh;

	public UberText m_armorTextMesh;

	public UberText m_nameTextMesh;

	public UberText m_powersTextMesh;

	public UberText m_raceTextMesh;

	public UberText m_secretText;

	public GameObject m_missingCardEffect;

	public GameObject m_ghostCardGameObject;

	public GameObject m_diamondPortraitR2T;

	[CustomEditField(T = EditType.ACTOR)]
	public string m_spellTablePrefab;

	protected Card m_card;

	protected Entity m_entity;

	protected CardDefHandle m_cardDefHandle = new CardDefHandle();

	protected EntityDef m_entityDef;

	protected TAG_PREMIUM m_premiumType;

	protected ProjectedShadow m_projectedShadow;

	protected bool m_shown = true;

	protected bool m_shadowVisible;

	protected ActorStateMgr m_actorStateMgr;

	protected ActorStateType m_actorState = ActorStateType.CARD_IDLE;

	protected bool forceIdleState;

	protected GameObject m_rootObject;

	protected GameObject m_bones;

	protected MeshRenderer m_meshRenderer;

	protected MeshRenderer m_meshRendererPortrait;

	protected int m_legacyPortraitMaterialIndex = -1;

	protected int m_legacyCardColorMaterialIndex = -1;

	protected Material m_initialPortraitMaterial;

	protected Material m_initialPremiumRibbonMaterial;

	protected SpellTable m_sharedSpellTable;

	protected bool m_useSharedSpellTable;

	protected Map<SpellType, Spell> m_localSpellTable;

	protected SpellTable m_spellTable;

	protected ArmorSpell m_armorSpell;

	protected GameObject m_hiddenCardStandIn;

	protected bool m_shadowform;

	protected GhostCard.Type m_ghostCard;

	protected TAG_PREMIUM m_ghostPremium;

	protected bool m_missingcard;

	protected bool m_armorSpellLoading;

	protected bool m_materialEffectsSeeded;

	protected Player.Side? m_cardBackSideOverride;

	protected CardBackManager.CardBackSlot? m_cardBackSlotOverride;

	private string m_cardDefPowerTextOverride;

	protected bool m_ignoreUpdateCardback;

	protected bool isPortraitMaterialDirty;

	protected Texture m_portraitTextureOverride;

	protected bool m_blockTextComponentUpdate;

	protected bool m_armorSpellDisabledForTransition;

	protected MultiClassBannerTransition m_multiClassBanner;

	protected UberShaderController m_uberShaderController;

	protected bool m_ignoreHideStats;

	protected TAG_CARD_SET m_watermarkCardSetOverride;

	protected bool m_useShortName;

	protected GameObject m_bannedRibbon;

	protected GameObject m_shadowObject;

	protected GameObject m_uniqueShadowObject;

	private int m_initialMissingCardRenderQueue;

	private int m_initialShadowRenderQueue;

	private int m_initialUniqueShadowRenderQueue;

	private bool m_shadowObjectInitialized;

	private bool m_usesMultiClassBanner;

	private GameObject m_diamondModelObject;

	private DiamondRenderToTexture m_diamondRenderToTexture;

	private string m_diamondModelShown;

	private bool m_portraitMeshDirty = true;

	private AssetHandle<Texture> m_watermarkTex;

	private AssetHandle<Texture> m_cardColorTex;

	private readonly UnityEngine.Vector2 descriptionMesh_WithoutRace_TextureOffset = new UnityEngine.Vector2(-0.04f, 0.07f);

	private readonly UnityEngine.Vector2 descriptionMesh_WithRace_TextureOffset = new UnityEngine.Vector2(-0.04f, 0f);

	public bool IsDesiredHidden { get; private set; }

	public bool IsDesiredHiddenInHierarchy
	{
		get
		{
			if (IsDesiredHidden)
			{
				return true;
			}
			WidgetTemplate componentInParent = GetComponentInParent<WidgetTemplate>();
			if (componentInParent != null && componentInParent.IsDesiredHiddenInHierarchy)
			{
				return true;
			}
			return false;
		}
	}

	public bool HandlesChildVisibility => true;

	public bool HasCardDef => m_cardDefHandle.Get() != null;

	public string CardDefName
	{
		get
		{
			if (!HasCardDef)
			{
				return null;
			}
			return m_cardDefHandle.Get().name;
		}
	}

	public Material DeckCardBarPortrait
	{
		get
		{
			if (!HasCardDef)
			{
				return null;
			}
			return m_cardDefHandle.Get().GetDeckCardBarPortrait();
		}
	}

	public Texture PortraitTexture
	{
		get
		{
			if (!HasCardDef)
			{
				return null;
			}
			return m_cardDefHandle.Get().GetPortraitTexture();
		}
	}

	public Material PremiumPortraitMaterial
	{
		get
		{
			if (!HasCardDef)
			{
				return null;
			}
			return m_cardDefHandle.Get().GetPremiumPortraitMaterial();
		}
	}

	public UberShaderAnimation PremiumPortraitAnimation
	{
		get
		{
			if (!HasCardDef)
			{
				return null;
			}
			return m_cardDefHandle.Get().GetPremiumPortraitAnimation();
		}
	}

	public CardPortraitQuality CardPortraitQuality => CardPortraitQuality.GetFromDef(m_cardDefHandle.Get());

	public CardEffectDef PlayEffectDef
	{
		get
		{
			if (!HasCardDef)
			{
				return null;
			}
			return m_cardDefHandle.Get().m_PlayEffectDef;
		}
	}

	public bool PremiumAnimationAvailable => CardTextureLoader.PremiumAnimationAvailable(m_cardDefHandle.Get());

	public string SocketInEffectFriendly
	{
		get
		{
			if (!HasCardDef)
			{
				return null;
			}
			return m_cardDefHandle.Get().m_SocketInEffectFriendly;
		}
	}

	public string SocketInEffectFriendlyPhone
	{
		get
		{
			if (!HasCardDef)
			{
				return null;
			}
			return m_cardDefHandle.Get().m_SocketInEffectFriendlyPhone;
		}
	}

	public string SocketInEffectOpponent
	{
		get
		{
			if (!HasCardDef)
			{
				return null;
			}
			return m_cardDefHandle.Get().m_SocketInEffectOpponent;
		}
	}

	public string SocketInEffectOpponentPhone
	{
		get
		{
			if (!HasCardDef)
			{
				return null;
			}
			return m_cardDefHandle.Get().m_SocketInEffectOpponentPhone;
		}
	}

	public bool SocketInOverrideHeroAnimation
	{
		get
		{
			if (!HasCardDef)
			{
				return false;
			}
			return m_cardDefHandle.Get().m_SocketInOverrideHeroAnimation;
		}
	}

	public bool SocketInParentEffectToHero
	{
		get
		{
			if (!HasCardDef)
			{
				return false;
			}
			return m_cardDefHandle.Get().m_SocketInParentEffectToHero;
		}
	}

	public List<EmoteEntryDef> EmoteDefs
	{
		get
		{
			if (!HasCardDef)
			{
				return null;
			}
			return m_cardDefHandle.Get().m_EmoteDefs;
		}
	}

	public bool AlwaysRenderPremiumPortrait
	{
		get
		{
			if (m_cardDefHandle != null && m_cardDefHandle.Get() != null)
			{
				return m_cardDefHandle.Get().m_AlwaysRenderPremiumPortrait;
			}
			return false;
		}
		set
		{
			if (m_cardDefHandle != null && m_cardDefHandle.Get() != null)
			{
				m_cardDefHandle.Get().m_AlwaysRenderPremiumPortrait = value;
			}
		}
	}

	public CardSilhouetteOverride CardSilhouetteOverride
	{
		get
		{
			if (!HasCardDef)
			{
				return CardSilhouetteOverride.None;
			}
			return m_cardDefHandle.Get().m_CardSilhouetteOverride;
		}
	}

	public virtual void Awake()
	{
		AssignRootObject();
		AssignBones();
		AssignMeshRenderers();
		AssignSpells();
		SetUpBanner();
	}

	private void OnEnable()
	{
		if (isPortraitMaterialDirty)
		{
			UpdateAllComponents();
		}
	}

	private void Start()
	{
		Init();
	}

	private void OnDestroy()
	{
		if (CardBackManager.Get() != null)
		{
			CardBackManager.Get().UnregisterUpdateCardbacksListener(UpdateCardBack);
		}
		ReleaseCardDef();
		if ((bool)m_diamondPortraitR2T)
		{
			UnityEngine.Object.Destroy(m_diamondPortraitR2T);
		}
		DestroyCreatedMaterials();
		AssetHandle.SafeDispose(ref m_watermarkTex);
		AssetHandle.SafeDispose(ref m_cardColorTex);
	}

	public void Init()
	{
		if (CardBackManager.Get() != null)
		{
			CardBackManager.Get().RegisterUpdateCardbacksListener(UpdateCardBack);
		}
		if (m_portraitMesh != null && m_portraitMatIdx >= 0)
		{
			m_initialPortraitMaterial = m_portraitMesh.GetComponent<Renderer>().GetSharedMaterial(m_portraitMatIdx);
		}
		else if (m_legacyPortraitMaterialIndex >= 0)
		{
			m_initialPortraitMaterial = m_meshRenderer.GetSharedMaterial(m_legacyPortraitMaterialIndex);
		}
		if (m_premiumRibbon > -1)
		{
			m_initialPremiumRibbonMaterial = m_cardMesh.GetComponent<Renderer>().GetMaterial(m_premiumRibbon);
		}
		if (m_rootObject != null)
		{
			TransformUtil.Identity(m_rootObject.transform);
		}
		if (m_actorStateMgr != null)
		{
			m_actorStateMgr.ChangeState(m_actorState);
		}
		m_projectedShadow = GetComponent<ProjectedShadow>();
		if (m_shown)
		{
			ShowImpl(ignoreSpells: false);
		}
		else
		{
			HideImpl(ignoreSpells: false);
		}
	}

	public void Destroy()
	{
		if (!base.gameObject)
		{
			return;
		}
		if (m_localSpellTable != null)
		{
			Spell[] array = m_localSpellTable.Values.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Deactivate();
			}
		}
		if (m_spellTable != null)
		{
			for (int j = 0; j < m_spellTable.m_Table.Count; j++)
			{
				if (!(m_spellTable.m_Table[j].m_Spell == null))
				{
					m_spellTable.m_Table[j].m_Spell.Deactivate();
				}
			}
		}
		ReleaseCardDef();
		DestroyCreatedMaterials();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void DestroyCreatedMaterials()
	{
		if (m_initialPremiumRibbonMaterial != null)
		{
			UnityEngine.Object.Destroy(m_initialPremiumRibbonMaterial);
			m_initialPremiumRibbonMaterial = null;
		}
	}

	public virtual Actor Clone()
	{
		GameObject obj = UnityEngine.Object.Instantiate(base.gameObject, base.transform.position, base.transform.rotation);
		Actor component = obj.GetComponent<Actor>();
		component.SetEntity(m_entity);
		component.SetEntityDef(m_entityDef);
		component.SetCard(m_card);
		component.SetPremium(m_premiumType);
		component.SetWatermarkCardSetOverride(m_watermarkCardSetOverride);
		obj.transform.localScale = base.gameObject.transform.localScale;
		obj.transform.position = base.gameObject.transform.position;
		component.SetActorState(m_actorState);
		if (m_shown)
		{
			component.ShowImpl(ignoreSpells: false);
		}
		else
		{
			component.HideImpl(ignoreSpells: false);
		}
		return component;
	}

	public Card GetCard()
	{
		return m_card;
	}

	public void SetCard(Card card)
	{
		if (m_card == card)
		{
			return;
		}
		if (card == null)
		{
			m_card = null;
			base.transform.parent = null;
			return;
		}
		m_card = card;
		base.transform.parent = card.transform;
		TransformUtil.Identity(base.transform);
		if (m_rootObject != null)
		{
			TransformUtil.Identity(m_rootObject.transform);
		}
	}

	public void SetFullDefFromEntity(Entity entity)
	{
		if (entity != null)
		{
			SetEntityDef(entity.GetEntityDef());
			SetCardDefFromEntity(entity);
		}
	}

	public void SetFullDefFromActor(Actor other)
	{
		if (other != null)
		{
			SetCardDefFromActor(other);
			SetEntityDef(other.m_entityDef);
		}
	}

	public void SetFullDef(DefLoader.DisposableFullDef fullDef)
	{
		SetCardDef(fullDef.DisposableCardDef);
		SetEntityDef(fullDef.EntityDef);
	}

	public DefLoader.DisposableCardDef ShareDisposableCardDef()
	{
		return m_cardDefHandle.Share();
	}

	public void SetCardDefFromEntity(Entity entity)
	{
		if (entity != null)
		{
			using DefLoader.DisposableCardDef cardDef = entity.ShareDisposableCardDef();
			SetCardDef(cardDef);
		}
	}

	public void SetCardDefFromActor(Actor other)
	{
		if (other != null)
		{
			m_cardDefHandle.Set(other.m_cardDefHandle);
		}
	}

	public void SetCardDefFromCard(Card card)
	{
		if (card != null)
		{
			using DefLoader.DisposableCardDef cardDef = card.ShareDisposableCardDef();
			m_cardDefHandle.SetCardDef(cardDef);
		}
	}

	public void SetCardDef(DefLoader.DisposableCardDef cardDef)
	{
		if (m_cardDefHandle.SetCardDef(cardDef))
		{
			LoadArmorSpell();
		}
	}

	public void ReleaseCardDef()
	{
		m_cardDefHandle.ReleaseCardDef();
	}

	public void SetIgnoreHideStats(bool ignore)
	{
		m_ignoreHideStats = ignore;
	}

	private bool HasHideStats(EntityBase entity)
	{
		if (m_ignoreHideStats)
		{
			return false;
		}
		if (!entity.HasTag(GAME_TAG.HIDE_STATS))
		{
			return entity.IsDormant();
		}
		return true;
	}

	public void SetWatermarkCardSetOverride(TAG_CARD_SET cardSetOverride)
	{
		if (!Enum.IsDefined(typeof(TAG_CARD_SET), cardSetOverride))
		{
			m_watermarkCardSetOverride = TAG_CARD_SET.INVALID;
		}
		else
		{
			m_watermarkCardSetOverride = cardSetOverride;
		}
	}

	public Entity GetEntity()
	{
		return m_entity;
	}

	public void SetEntity(Entity entity)
	{
		m_entity = entity;
		if (m_entity != null)
		{
			SetPremium(m_entity.GetPremiumType());
			SetWatermarkCardSetOverride(m_entity.GetWatermarkCardSetOverride());
		}
	}

	public EntityDef GetEntityDef()
	{
		return m_entityDef;
	}

	public void SetEntityDef(EntityDef entityDef)
	{
		m_entityDef = entityDef;
		if (m_entityDef != null)
		{
			m_cardDefHandle.SetCardId(m_entityDef.GetCardId());
		}
	}

	public virtual void SetPremium(TAG_PREMIUM premium)
	{
		m_premiumType = premium;
	}

	public TAG_PREMIUM GetPremium()
	{
		return m_premiumType;
	}

	public TAG_CARD_SET GetCardSet()
	{
		if (m_entityDef == null && m_entity == null)
		{
			return TAG_CARD_SET.NONE;
		}
		if (m_entityDef != null)
		{
			return m_entityDef.GetCardSet();
		}
		return m_entity.GetCardSet();
	}

	public ActorStateType GetActorStateType()
	{
		if (!(m_actorStateMgr == null))
		{
			return m_actorStateMgr.GetActiveStateType();
		}
		return ActorStateType.NONE;
	}

	public void SetActorState(ActorStateType stateType)
	{
		m_actorState = stateType;
		if (!(m_actorStateMgr == null))
		{
			if (forceIdleState)
			{
				m_actorState = ActorStateType.CARD_IDLE;
			}
			m_actorStateMgr.ChangeState(m_actorState);
		}
	}

	public void ToggleForceIdle(bool bOn)
	{
		forceIdleState = bOn;
	}

	public void TurnOffCollider()
	{
		ToggleCollider(enabled: false);
	}

	public void TurnOnCollider()
	{
		ToggleCollider(enabled: true);
	}

	public void ToggleCollider(bool enabled)
	{
		MeshRenderer meshRenderer = GetMeshRenderer();
		if (!(meshRenderer == null) && !(meshRenderer.gameObject.GetComponent<Collider>() == null))
		{
			meshRenderer.gameObject.GetComponent<Collider>().enabled = enabled;
		}
	}

	public bool IsColliderEnabled()
	{
		MeshRenderer meshRenderer = GetMeshRenderer();
		if (meshRenderer == null || meshRenderer.gameObject.GetComponent<Collider>() == null)
		{
			return false;
		}
		return meshRenderer.gameObject.GetComponent<Collider>().enabled;
	}

	public TAG_RARITY GetRarity()
	{
		if (m_entityDef != null)
		{
			return m_entityDef.GetRarity();
		}
		if (m_entity != null)
		{
			return m_entity.GetRarity();
		}
		return TAG_RARITY.FREE;
	}

	public bool IsElite()
	{
		if (m_entityDef != null)
		{
			return m_entityDef.IsElite();
		}
		if (m_entity != null)
		{
			return m_entity.IsElite();
		}
		return false;
	}

	public bool IsMultiClass()
	{
		if (m_entityDef != null)
		{
			return m_entityDef.IsMultiClass();
		}
		if (m_entity != null)
		{
			return m_entity.IsMultiClass();
		}
		return false;
	}

	public void SetHiddenStandIn(GameObject standIn)
	{
		m_hiddenCardStandIn = standIn;
	}

	public GameObject GetHiddenStandIn()
	{
		return m_hiddenCardStandIn;
	}

	public void SetShadowform(bool shadowform)
	{
		m_shadowform = shadowform;
	}

	public UberShaderController GetUberShaderController()
	{
		if (m_uberShaderController == null)
		{
			m_uberShaderController = m_portraitMesh.GetComponent<UberShaderController>();
		}
		return m_uberShaderController;
	}

	public bool UsesMultiClassBanner()
	{
		return m_usesMultiClassBanner;
	}

	public void SetVisibility(bool isVisible, bool isInternal)
	{
		SetVisibility(isVisible, ignoreSpells: false, isInternal);
	}

	protected void SetVisibility(bool isVisible, bool ignoreSpells, bool isInternal)
	{
		if (isVisible != m_shown)
		{
			if (!isInternal)
			{
				IsDesiredHidden = !isVisible;
			}
			m_shown = isVisible;
			if (isVisible)
			{
				ShowImpl(ignoreSpells);
			}
			else
			{
				HideImpl(ignoreSpells);
			}
		}
	}

	public bool IsShown()
	{
		return m_shown;
	}

	public void Show()
	{
		SetVisibility(isVisible: true, ignoreSpells: false, isInternal: false);
	}

	public void Show(bool ignoreSpells)
	{
		SetVisibility(isVisible: true, ignoreSpells, isInternal: false);
	}

	public void ShowSpellTable()
	{
		if (m_localSpellTable != null)
		{
			foreach (Spell value in m_localSpellTable.Values)
			{
				value.Show();
			}
		}
		if (m_spellTable != null)
		{
			m_spellTable.Show();
		}
	}

	public void Hide()
	{
		SetVisibility(isVisible: false, ignoreSpells: false, isInternal: false);
	}

	public void Hide(bool ignoreSpells)
	{
		SetVisibility(isVisible: false, ignoreSpells, isInternal: false);
	}

	public void HideSpellTable()
	{
		if (m_localSpellTable != null)
		{
			foreach (Spell value in m_localSpellTable.Values)
			{
				if (value.GetSpellType() != 0)
				{
					value.Hide();
				}
			}
		}
		if (m_spellTable != null)
		{
			m_spellTable.Hide();
		}
	}

	protected virtual void ShowImpl(bool ignoreSpells)
	{
		if (m_rootObject != null)
		{
			m_rootObject.SetActive(value: true);
		}
		if ((bool)m_diamondRenderToTexture)
		{
			m_diamondRenderToTexture.enabled = true;
		}
		ShowArmorSpell();
		ShowAllText();
		UpdateAllComponents();
		if ((bool)m_projectedShadow)
		{
			m_projectedShadow.enabled = true;
		}
		if (m_actorStateMgr != null)
		{
			m_actorStateMgr.ShowStateMgr();
		}
		if (!ignoreSpells)
		{
			ShowSpellTable();
		}
		if (m_ghostCardGameObject != null)
		{
			m_ghostCardGameObject.SetActive(value: true);
		}
		HighlightState componentInChildren = GetComponentInChildren<HighlightState>();
		if ((bool)componentInChildren)
		{
			componentInChildren.Show();
		}
	}

	protected virtual void HideImpl(bool ignoreSpells)
	{
		if (m_rootObject != null)
		{
			m_rootObject.SetActive(value: false);
		}
		UpdateContactShadow();
		HideArmorSpell();
		if (m_actorStateMgr != null)
		{
			m_actorStateMgr.HideStateMgr();
		}
		if ((bool)m_projectedShadow)
		{
			m_projectedShadow.enabled = false;
		}
		if (m_ghostCardGameObject != null)
		{
			m_ghostCardGameObject.SetActive(value: false);
		}
		if (!ignoreSpells)
		{
			HideSpellTable();
		}
		if (m_missingCardEffect != null)
		{
			UpdateMissingCardArt();
		}
		if ((bool)m_diamondRenderToTexture)
		{
			m_diamondRenderToTexture.enabled = false;
		}
		HighlightState componentInChildren = GetComponentInChildren<HighlightState>();
		if ((bool)componentInChildren)
		{
			componentInChildren.Hide();
		}
	}

	public ActorStateMgr GetActorStateMgr()
	{
		return m_actorStateMgr;
	}

	public Collider GetCollider()
	{
		if (GetMeshRenderer() == null)
		{
			return null;
		}
		return GetMeshRenderer().gameObject.GetComponent<Collider>();
	}

	public GameObject GetRootObject()
	{
		return m_rootObject;
	}

	public MeshRenderer GetMeshRenderer(bool getPortrait = false)
	{
		if (m_premiumType == TAG_PREMIUM.DIAMOND)
		{
			if (getPortrait)
			{
				return m_meshRendererPortrait;
			}
			return m_meshRenderer;
		}
		return m_meshRenderer;
	}

	public GameObject GetBones()
	{
		return m_bones;
	}

	public UberText GetPowersText()
	{
		return m_powersTextMesh;
	}

	public UberText GetRaceText()
	{
		return m_raceTextMesh;
	}

	public UberText GetNameText()
	{
		return m_nameTextMesh;
	}

	public Light GetHeroSpotlight()
	{
		if (m_heroSpotLight == null)
		{
			return null;
		}
		return m_heroSpotLight.GetComponent<Light>();
	}

	public GameObject FindBone(string boneName)
	{
		if (m_bones == null)
		{
			return null;
		}
		return SceneUtils.FindChildBySubstring(m_bones, boneName);
	}

	public GameObject GetCardTypeBannerAnchor()
	{
		if (m_cardTypeAnchorObject == null)
		{
			return base.gameObject;
		}
		return m_cardTypeAnchorObject;
	}

	public UberText GetAttackText()
	{
		return m_attackTextMesh;
	}

	public GameObject GetAttackTextObject()
	{
		if (m_attackTextMesh == null)
		{
			return null;
		}
		return m_attackTextMesh.gameObject;
	}

	public GemObject GetAttackObject()
	{
		if (m_attackObject == null)
		{
			return null;
		}
		return m_attackObject.GetComponent<GemObject>();
	}

	public GemObject GetHealthObject()
	{
		if (m_healthObject == null)
		{
			return null;
		}
		return m_healthObject.GetComponent<GemObject>();
	}

	public GameObject GetWeaponShields()
	{
		if (m_healthObject != null && m_healthObject.GetComponent<GemObject>() == null)
		{
			return m_healthObject;
		}
		return null;
	}

	public GameObject GetWeaponSwords()
	{
		if (m_attackObject != null && m_attackObject.GetComponent<GemObject>() == null)
		{
			return m_attackObject;
		}
		return null;
	}

	public GemObject GetArmorObject()
	{
		if (m_armorObject == null)
		{
			return null;
		}
		return m_armorObject.GetComponent<GemObject>();
	}

	public UberText GetHealthText()
	{
		return m_healthTextMesh;
	}

	public GameObject GetHealthTextObject()
	{
		if (m_healthTextMesh == null)
		{
			return null;
		}
		return m_healthTextMesh.gameObject;
	}

	public UberText GetCostText()
	{
		if (m_costTextMesh == null)
		{
			return null;
		}
		return m_costTextMesh;
	}

	public GameObject GetCostTextObject()
	{
		if (m_costTextMesh == null)
		{
			return null;
		}
		return m_costTextMesh.gameObject;
	}

	public UberText GetSecretText()
	{
		return m_secretText;
	}

	public void UpdateAllComponents()
	{
		UpdateTextComponents();
		UpdateMaterials();
		UpdateTextures();
		UpdateCardBack();
		UpdateMeshComponents();
		UpdateRootObjectSpellComponents();
		UpdateMissingCardArt();
		UpdateGhostCardEffect();
		UpdateDiamondCardArt();
		UpdatePortraitMaterialAnimation();
		UpdateContactShadow();
		if (PlatformSettings.OS == OSCategory.Mac && (bool)m_nameTextMesh)
		{
			StartCoroutine(DelayedUpdateNameText());
		}
	}

	private IEnumerator DelayedUpdateNameText()
	{
		yield return null;
		if ((bool)m_nameTextMesh)
		{
			m_nameTextMesh.UpdateNow();
		}
	}

	public bool MissingCardEffect(bool refreshOnFocus = true)
	{
		if ((bool)m_missingCardEffect)
		{
			RenderToTexture component = m_missingCardEffect.GetComponent<RenderToTexture>();
			if ((bool)component)
			{
				component.DontRefreshOnFocus = !refreshOnFocus;
				m_initialMissingCardRenderQueue = component.m_RenderQueue;
				m_missingcard = true;
				UpdateAllComponents();
				return true;
			}
		}
		return false;
	}

	public void DisableMissingCardEffect()
	{
		m_missingcard = false;
		if ((bool)m_missingCardEffect)
		{
			RenderToTexture component = m_missingCardEffect.GetComponent<RenderToTexture>();
			if ((bool)component)
			{
				component.enabled = false;
			}
			MaterialShaderAnimation(animationEnabled: true);
		}
	}

	public void UpdateMissingCardArt()
	{
		if (!m_missingcard || m_missingCardEffect == null)
		{
			return;
		}
		RenderToTexture component = m_missingCardEffect.GetComponent<RenderToTexture>();
		if (component == null)
		{
			return;
		}
		if (m_rootObject.activeSelf)
		{
			MaterialShaderAnimation(animationEnabled: false);
			TAG_PREMIUM premium = GetPremium();
			bool flag = CollectionManager.Get().GetThemeShowing() == FormatType.FT_WILD;
			if (premium == TAG_PREMIUM.GOLDEN)
			{
				if (flag)
				{
					component.m_Material.color = MISSING_CARD_WILD_GOLDEN_COLOR;
				}
				else
				{
					component.m_Material.color = MISSING_CARD_STANDARD_GOLDEN_COLOR;
				}
			}
			else if (premium == TAG_PREMIUM.DIAMOND && flag)
			{
				Material material = component.m_Material;
				material.color = MISSING_CARD_WILD_DIAMOND_COLOR;
				material.SetFloat(MISSING_CARD_WILD_DIAMOND_CONTRAST_KEY, MISSING_CARD_WILD_DIAMOND_CONTRAST);
				material.SetFloat(MISSING_CARD_WILD_DIAMOND_INTENSITY_KEY, MISSING_CARD_WILD_DIAMOND_INTENSITY);
			}
			component.enabled = true;
			component.Show(render: true);
		}
		else
		{
			component.enabled = false;
			component.Hide();
		}
	}

	public void SetMissingCardMaterial(Material missingCardMat)
	{
		if (m_missingCardEffect == null || missingCardMat == null)
		{
			return;
		}
		RenderToTexture component = m_missingCardEffect.GetComponent<RenderToTexture>();
		if (component == null)
		{
			return;
		}
		component.m_Material = missingCardMat;
		if (m_rootObject.activeSelf)
		{
			MaterialShaderAnimation(animationEnabled: false);
			if (component.enabled)
			{
				component.Render();
			}
		}
	}

	public bool isMissingCard()
	{
		if (m_missingCardEffect == null)
		{
			return false;
		}
		RenderToTexture component = m_missingCardEffect.GetComponent<RenderToTexture>();
		if (component == null)
		{
			return false;
		}
		return component.enabled;
	}

	public void SetMissingCardRenderQueue(bool reset, int renderQueue)
	{
		RenderToTexture component = m_missingCardEffect.GetComponent<RenderToTexture>();
		if (!(component == null))
		{
			component.m_RenderQueue = (reset ? m_initialMissingCardRenderQueue : renderQueue);
		}
	}

	public void GhostCardEffect(GhostCard.Type ghostType, TAG_PREMIUM premium = TAG_PREMIUM.NORMAL, bool update = true)
	{
		if (m_ghostCard != ghostType || m_ghostPremium != premium)
		{
			m_ghostCard = ghostType;
			m_ghostPremium = premium;
			if (update)
			{
				UpdateAllComponents();
			}
		}
	}

	private void UpdateGhostCardEffect()
	{
		if (m_ghostCardGameObject == null)
		{
			return;
		}
		GhostCard component = m_ghostCardGameObject.GetComponent<GhostCard>();
		if (!(component == null))
		{
			if (m_ghostCard != 0)
			{
				component.SetGhostType(m_ghostCard);
				component.SetPremium(m_ghostPremium);
				component.RenderGhostCard();
			}
			else
			{
				component.DisableGhost();
			}
		}
	}

	public bool isGhostCard()
	{
		if (m_ghostCard != 0)
		{
			return m_ghostCardGameObject;
		}
		return false;
	}

	public bool DoesDiamondModelExistOnCardDef()
	{
		CardDef cardDef = m_cardDefHandle.Get();
		if (cardDef == null)
		{
			return false;
		}
		return !string.IsNullOrEmpty(cardDef.m_DiamondModel);
	}

	public bool IsEntityStateBadForDiamondVisuals()
	{
		if (GameState.Get() != null && !GameState.Get().AllowDiamondCards())
		{
			return true;
		}
		GetEntity();
		if (m_entity == null)
		{
			return false;
		}
		bool num = m_entity.HasTag(GAME_TAG.FROZEN);
		bool flag = m_entity.HasTag(GAME_TAG.REBORN);
		bool flag2 = m_entity.HasTag(GAME_TAG.STEALTH);
		bool flag3 = m_entity.HasTag(GAME_TAG.DORMANT);
		bool flag4 = m_entity.HasTag(GAME_TAG.ENRAGED);
		bool flag5 = m_entity.HasTag(GAME_TAG.CANT_BE_TARGETED_BY_SPELLS) && m_entity.HasTag(GAME_TAG.CANT_BE_TARGETED_BY_HERO_POWERS);
		Card card = GetCard();
		if (card != null)
		{
			Spell actorSpell = card.GetActorSpell(SpellType.DORMANT, loadIfNeeded: false);
			if (actorSpell != null && actorSpell.GetActiveState() != 0)
			{
				flag3 = true;
			}
		}
		bool flag6 = false;
		if (m_card != null && m_card.GetZone() is ZoneGraveyard)
		{
			flag6 = true;
		}
		return num || flag || flag2 || flag3 || flag4 || flag5 || flag6;
	}

	public void UpdateDiamondCardArt()
	{
		if (m_premiumType != TAG_PREMIUM.DIAMOND)
		{
			return;
		}
		if (m_portraitMesh != null && m_portraitMeshRTT != null)
		{
			bool num = IsEntityStateBadForDiamondVisuals();
			bool flag = DoesDiamondModelExistOnCardDef();
			if (num || !flag)
			{
				m_portraitMesh.SetActive(value: true);
				m_portraitMeshRTT.SetActive(value: false);
			}
			else
			{
				m_portraitMesh.SetActive(value: false);
				m_portraitMeshRTT.SetActive(value: true);
			}
		}
		if (m_cardDefHandle.Get() == null)
		{
			return;
		}
		if (DoesDiamondModelExistOnCardDef() && m_rootObject != null)
		{
			bool flag2 = m_diamondModelObject != null;
			string diamondModel = m_cardDefHandle.Get().m_DiamondModel;
			if ((bool)m_diamondPortraitR2T && !m_diamondRenderToTexture)
			{
				m_diamondRenderToTexture = m_diamondPortraitR2T.GetComponent<DiamondRenderToTexture>();
			}
			if (flag2 && diamondModel != m_diamondModelShown)
			{
				UnityEngine.Object.Destroy(m_diamondModelObject);
				m_diamondModelObject = null;
				flag2 = false;
				if ((bool)m_diamondRenderToTexture)
				{
					m_diamondRenderToTexture.enabled = false;
				}
			}
			if (!flag2)
			{
				m_diamondModelObject = AssetLoader.Get().InstantiatePrefab(diamondModel, AssetLoadingOptions.IgnorePrefabPosition);
				m_diamondModelShown = diamondModel;
				m_diamondModelObject.transform.parent = m_rootObject.transform;
				if ((bool)m_diamondRenderToTexture)
				{
					m_diamondRenderToTexture.m_ObjectToRender = m_diamondModelObject;
					m_diamondRenderToTexture.m_ClearColor = m_cardDefHandle.Get().m_DiamondPlaneRTT_CearColor;
				}
				m_portraitMeshDirty = true;
			}
			else if ((bool)m_diamondRenderToTexture)
			{
				m_diamondRenderToTexture.UpdateMaterialBlend(m_usePlayPortrait);
			}
			else
			{
				m_diamondModelObject.SetActive(value: false);
			}
		}
		if (m_portraitMeshDirty && m_portraitMeshRTT != null && m_portraitMeshRTT_background != null)
		{
			AssetReference assetReference = null;
			assetReference = ((!(m_card == null)) ? ((AssetReference)(m_usePlayPortrait ? m_cardDefHandle.Get().m_DiamondPlaneRTT_Play : m_cardDefHandle.Get().m_DiamondPlaneRTT_Hand)) : ((AssetReference)m_cardDefHandle.Get().m_DiamondPlaneRTT_Hand));
			MeshFilter component = m_portraitMeshRTT.GetComponent<MeshFilter>();
			if (component != null && assetReference != null)
			{
				using AssetHandle<Mesh> assetHandle = AssetLoader.Get().LoadAsset<Mesh>(assetReference);
				if (assetHandle != null)
				{
					component.sharedMesh = assetHandle;
				}
			}
			AssetReference assetReference2 = m_cardDefHandle.Get().m_DiamondPortraitTexturePath;
			Renderer component2 = m_portraitMeshRTT_background.GetComponent<Renderer>();
			if (component2 != null && component2.GetSharedMaterial().HasProperty("_MainTex") && assetReference2 != null)
			{
				using AssetHandle<Texture2D> assetHandle2 = AssetLoader.Get().LoadAsset<Texture2D>(assetReference2);
				if (assetHandle2 != null)
				{
					GetMaterialInstance(m_portraitMeshRTT_background.GetComponent<Renderer>()).SetTexture("_MainTex", (Texture2D)assetHandle2);
				}
			}
			HighlightState componentInChildren = GetComponentInChildren<HighlightState>();
			if (componentInChildren != null && componentInChildren.isActiveAndEnabled)
			{
				componentInChildren.ContinuousUpdate(0.1f);
			}
			m_portraitMeshDirty = false;
		}
		if ((bool)m_diamondRenderToTexture)
		{
			m_diamondRenderToTexture.enabled = m_shown;
		}
		if (!DoesDiamondModelExistOnCardDef() && m_diamondModelObject != null)
		{
			UnityEngine.Object.Destroy(m_diamondModelObject);
			m_diamondModelObject = null;
		}
		if (m_diamondModelObject == null && m_diamondPortraitR2T != null && (bool)m_diamondRenderToTexture && m_diamondRenderToTexture.enabled)
		{
			m_diamondRenderToTexture.enabled = false;
		}
	}

	public void UpdateMaterials()
	{
		if (base.gameObject.activeInHierarchy)
		{
			StartCoroutine(UpdatePortraitMaterials());
		}
		else
		{
			isPortraitMaterialDirty = true;
		}
	}

	public void OverrideAllMeshMaterials(Material material)
	{
		if (!(m_rootObject == null))
		{
			RecursivelyReplaceMaterialsList(m_rootObject.transform, material);
		}
	}

	public void SetUnlit()
	{
		SetLightBlend(0f, includeInactive: true);
	}

	public void SetLit()
	{
		SetLightBlend(1f, includeInactive: true);
	}

	public void SetLightBlend(float blendValue, bool includeInactive = false)
	{
		SetLightBlend(base.gameObject, blendValue, includeInactive);
	}

	private void SetLightBlend(GameObject go, float blendValue, bool includeInactive = false)
	{
		Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>(includeInactive);
		foreach (Renderer renderer in componentsInChildren)
		{
			if (!renderer.gameObject.activeInHierarchy)
			{
				DeferredEnableHandler.AttachTo(renderer, delegate
				{
					SetRendererLightBlend(renderer, blendValue);
				});
			}
			else
			{
				SetRendererLightBlend(renderer, blendValue);
			}
		}
		UberText[] componentsInChildren2 = go.GetComponentsInChildren<UberText>(includeInactive);
		for (int i = 0; i < componentsInChildren2.Length; i++)
		{
			componentsInChildren2[i].AmbientLightBlend = blendValue;
		}
	}

	private void SetRendererLightBlend(Renderer renderer, float blendValue)
	{
		foreach (Material material in renderer.GetMaterials())
		{
			if (!(material == null) && material.HasProperty("_LightingBlend"))
			{
				material.SetFloat("_LightingBlend", blendValue);
			}
		}
	}

	private void RecursivelyReplaceMaterialsList(Transform transformToRecurse, Material newMaterialPrefab)
	{
		bool flag = true;
		if (transformToRecurse.GetComponent<MaterialReplacementExclude>() != null)
		{
			flag = false;
		}
		else if (transformToRecurse.GetComponent<UberText>() != null)
		{
			flag = false;
		}
		else if (transformToRecurse.GetComponent<Renderer>() == null)
		{
			flag = false;
		}
		if (flag)
		{
			ReplaceMaterialsList(transformToRecurse.GetComponent<Renderer>(), newMaterialPrefab);
		}
		foreach (Transform item in transformToRecurse)
		{
			RecursivelyReplaceMaterialsList(item, newMaterialPrefab);
		}
	}

	private void ReplaceMaterialsList(Renderer renderer, Material newMaterialPrefab)
	{
		List<Material> materials = renderer.GetMaterials();
		int count = materials.Count;
		Material[] array = new Material[count];
		for (int i = 0; i < count; i++)
		{
			Material oldMaterial = materials[i];
			array[i] = CreateReplacementMaterial(oldMaterial, newMaterialPrefab);
		}
		renderer.SetMaterials(array);
		if (!(renderer != m_meshRenderer))
		{
			UpdatePortraitTexture();
		}
	}

	private Material CreateReplacementMaterial(Material oldMaterial, Material newMaterialPrefab)
	{
		Material material = UnityEngine.Object.Instantiate(newMaterialPrefab);
		material.mainTexture = oldMaterial.mainTexture;
		return material;
	}

	public void SeedMaterialEffects()
	{
		if (m_materialEffectsSeeded)
		{
			return;
		}
		m_materialEffectsSeeded = true;
		Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>();
		float value = UnityEngine.Random.Range(0f, 2f);
		Renderer[] array = componentsInChildren;
		foreach (Renderer renderer in array)
		{
			List<Material> sharedMaterials = renderer.GetSharedMaterials();
			if (sharedMaterials.Count == 1)
			{
				Material material = sharedMaterials[0];
				if (material.HasProperty("_Seed") && material.GetFloat("_Seed") == 0f)
				{
					GetMaterialInstance(renderer).SetFloat("_Seed", value);
				}
				continue;
			}
			List<Material> materials = renderer.GetMaterials();
			if (materials == null || materials.Count == 0)
			{
				continue;
			}
			foreach (Material item in materials)
			{
				if (!(item == null) && item.HasProperty("_Seed") && item.GetFloat("_Seed") == 0f)
				{
					item.SetFloat("_Seed", value);
				}
			}
		}
	}

	public void MaterialShaderAnimation(bool animationEnabled)
	{
		if ((bool)m_diamondPortraitR2T)
		{
			return;
		}
		float value = 0f;
		if (animationEnabled)
		{
			value = 1f;
		}
		Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>(includeInactive: true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			foreach (Material sharedMaterial in componentsInChildren[i].GetSharedMaterials())
			{
				if (!(sharedMaterial == null) && sharedMaterial.HasProperty("_TimeScale"))
				{
					sharedMaterial.SetFloat("_TimeScale", value);
				}
			}
		}
	}

	public CardBackManager.CardBackSlot GetCardBackSlot()
	{
		if (m_cardBackSlotOverride.HasValue)
		{
			return m_cardBackSlotOverride.Value;
		}
		Player.Side side = Player.Side.FRIENDLY;
		if (m_cardBackSideOverride.HasValue)
		{
			side = m_cardBackSideOverride.Value;
		}
		else if (m_entity != null)
		{
			Player controller = m_entity.GetController();
			if (controller != null)
			{
				side = controller.GetSide();
			}
		}
		if (side == Player.Side.FRIENDLY)
		{
			return CardBackManager.CardBackSlot.FRIENDLY;
		}
		return CardBackManager.CardBackSlot.OPPONENT;
	}

	public void SetCardBackSideOverride(Player.Side? sideOverride)
	{
		m_cardBackSideOverride = sideOverride;
	}

	public void SetCardBackSlotOverride(CardBackManager.CardBackSlot? slotOverride)
	{
		m_cardBackSlotOverride = slotOverride;
	}

	public bool GetCardbackUpdateIgnore()
	{
		return m_ignoreUpdateCardback;
	}

	public void SetCardbackUpdateIgnore(bool ignoreUpdate)
	{
		m_ignoreUpdateCardback = ignoreUpdate;
	}

	public void UpdateCardBack()
	{
		if (m_ignoreUpdateCardback)
		{
			return;
		}
		CardBackManager cardBackManager = CardBackManager.Get();
		if (cardBackManager != null)
		{
			CardBackManager.CardBackSlot cardBackSlot = GetCardBackSlot();
			UpdateCardBackDisplay(cardBackSlot);
			UpdateCardBackDragEffect();
			if (!(m_cardMesh == null) && m_cardBackMatIdx >= 0)
			{
				cardBackManager.SetCardBackTexture(m_cardMesh.GetComponent<Renderer>(), m_cardBackMatIdx, cardBackSlot);
			}
		}
	}

	public void EnableCardbackShadow(bool enabled)
	{
		CardBackDisplay componentInChildren = GetComponentInChildren<CardBackDisplay>();
		if (!(componentInChildren == null))
		{
			componentInChildren.EnableShadow(enabled);
		}
	}

	private void UpdateCardBackDragEffect()
	{
		if (SceneMgr.Get() != null && SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
		{
			CardBackDragEffect componentInChildren = GetComponentInChildren<CardBackDragEffect>();
			if (!(componentInChildren == null))
			{
				componentInChildren.SetEffect();
			}
		}
	}

	private void UpdateCardBackDisplay(CardBackManager.CardBackSlot slot)
	{
		CardBackDisplay componentInChildren = GetComponentInChildren<CardBackDisplay>();
		if (!(componentInChildren == null))
		{
			componentInChildren.SetCardBack(slot);
		}
	}

	public void UpdateTextures()
	{
		UpdatePortraitTexture();
	}

	public void UpdatePortraitTexture()
	{
		if (m_portraitTextureOverride != null)
		{
			SetPortraitTexture(m_portraitTextureOverride);
		}
		else if (m_cardDefHandle.Get() != null)
		{
			SetPortraitTexture(m_cardDefHandle.Get().GetPortraitTexture());
		}
	}

	public void SetPortraitTexture(Texture texture)
	{
		if (!(m_cardDefHandle.Get() != null) || (m_premiumType < TAG_PREMIUM.GOLDEN && !m_cardDefHandle.Get().m_AlwaysRenderPremiumPortrait) || !IsPremiumPortraitEnabled() || !(m_cardDefHandle.Get().GetPremiumPortraitMaterial() != null))
		{
			Material portraitMaterial = GetPortraitMaterial();
			if (!(portraitMaterial == null))
			{
				portraitMaterial.mainTexture = texture;
			}
		}
	}

	public void SetPortraitTextureOverride(Texture portrait)
	{
		m_portraitTextureOverride = portrait;
		UpdatePortraitTexture();
	}

	public Texture GetPortraitTexture()
	{
		Material portraitMaterial = GetPortraitMaterial();
		if (portraitMaterial == null)
		{
			return null;
		}
		return portraitMaterial.mainTexture;
	}

	public Texture GetStaticPortraitTexture()
	{
		if (m_portraitTextureOverride != null)
		{
			return m_portraitTextureOverride;
		}
		return m_cardDefHandle.Get().GetPortraitTexture();
	}

	private IEnumerator UpdatePortraitMaterials()
	{
		isPortraitMaterialDirty = false;
		if (m_shadowform)
		{
			yield break;
		}
		CardDef cardDef = m_cardDefHandle.Get();
		if (!cardDef)
		{
			yield break;
		}
		if ((m_premiumType >= TAG_PREMIUM.GOLDEN || cardDef.m_AlwaysRenderPremiumPortrait) && IsPremiumPortraitEnabled())
		{
			if ((bool)cardDef && !cardDef.IsPremiumLoaded())
			{
				yield return null;
			}
			if ((bool)cardDef && cardDef.GetPremiumPortraitMaterial() != null)
			{
				SetPortraitMaterial(cardDef.GetPremiumPortraitMaterial());
			}
			else if (m_initialPortraitMaterial != null)
			{
				SetPortraitMaterial(m_initialPortraitMaterial);
			}
		}
		else
		{
			SetPortraitMaterial(m_initialPortraitMaterial);
		}
		UpdatePortraitTexture();
	}

	private void UpdatePortraitMaterialAnimation()
	{
		if (m_cardDefHandle.Get() == null || m_cardDefHandle.Get().GetPremiumPortraitAnimation() == null || m_portraitMesh == null)
		{
			return;
		}
		m_uberShaderController = m_portraitMesh.GetComponent<UberShaderController>();
		if (m_uberShaderController == null)
		{
			m_uberShaderController = m_portraitMesh.gameObject.AddComponent<UberShaderController>();
			m_uberShaderController.UberShaderAnimation = UnityEngine.Object.Instantiate(m_cardDefHandle.Get().GetPremiumPortraitAnimation());
		}
		else
		{
			if (m_uberShaderController.UberShaderAnimation.name.Replace("(Clone)", "") == m_cardDefHandle.Get().GetPremiumPortraitAnimation().name)
			{
				return;
			}
			m_uberShaderController.UberShaderAnimation = UnityEngine.Object.Instantiate(m_cardDefHandle.Get().GetPremiumPortraitAnimation());
		}
		m_uberShaderController.m_MaterialIndex = m_portraitMatIdx;
		if (isGhostCard() && m_ghostCard != GhostCard.Type.DORMANT)
		{
			m_uberShaderController.enabled = false;
		}
		else
		{
			m_uberShaderController.enabled = true;
		}
	}

	public void SetPortraitMaterial(Material material)
	{
		if (material == null)
		{
			return;
		}
		if (m_portraitMesh != null && m_portraitMatIdx > -1)
		{
			Renderer component = m_portraitMesh.GetComponent<Renderer>();
			Material material2 = component.GetMaterial(m_portraitMatIdx);
			if (material2.mainTexture == material.mainTexture && material2.shader == material.shader)
			{
				return;
			}
			component.SetMaterial(m_portraitMatIdx, material);
			float value = 0f;
			if ((bool)m_card)
			{
				Zone zone = m_card.GetZone();
				if (zone is ZonePlay || zone is ZoneWeapon || zone is ZoneHeroPower)
				{
					value = 1f;
				}
			}
			foreach (Material material3 in component.GetMaterials())
			{
				if (material3.HasProperty("_LightingBlend"))
				{
					material3.SetFloat("_LightingBlend", value);
				}
				if (material3.HasProperty("_Seed") && material3.GetFloat("_Seed") == 0f)
				{
					material3.SetFloat("_Seed", UnityEngine.Random.Range(0f, 2f));
				}
			}
		}
		else if (m_legacyPortraitMaterialIndex >= 0 && !(m_meshRenderer.GetMaterial(m_legacyPortraitMaterialIndex) == material))
		{
			m_meshRenderer.SetMaterial(m_legacyPortraitMaterialIndex, material);
		}
	}

	public GameObject GetPortraitMesh()
	{
		return m_portraitMesh;
	}

	public virtual Material GetPortraitMaterial()
	{
		if (m_portraitMesh != null)
		{
			Renderer component = m_portraitMesh.GetComponent<Renderer>();
			if (0 <= m_portraitMatIdx && m_portraitMatIdx < component.GetSharedMaterials().Count)
			{
				if (!Application.isPlaying)
				{
					return component.GetSharedMaterial(m_portraitMatIdx);
				}
				return component.GetMaterial(m_portraitMatIdx);
			}
		}
		if (m_legacyPortraitMaterialIndex >= 0)
		{
			return m_meshRenderer.GetMaterial(m_legacyPortraitMaterialIndex);
		}
		return null;
	}

	protected virtual bool IsPremiumPortraitEnabled()
	{
		if (GameState.Get() != null && GameState.Get().GetGameEntity() != null && GameState.Get().GetGameEntity().HasTag(GAME_TAG.DISABLE_GOLDEN_ANIMATIONS))
		{
			return false;
		}
		if (GraphicsManager.Get() != null)
		{
			return !GraphicsManager.Get().isVeryLowQualityDevice();
		}
		return false;
	}

	public void SetBlockTextComponentUpdate(bool block)
	{
		m_blockTextComponentUpdate = block;
	}

	public virtual void UpdateTextComponents()
	{
		if (!m_blockTextComponentUpdate)
		{
			if (m_entityDef != null)
			{
				UpdateTextComponentsDef(m_entityDef);
			}
			else
			{
				UpdateTextComponents(m_entity);
			}
		}
	}

	public virtual void UpdateTextComponentsDef(EntityDef entityDef)
	{
		if (entityDef != null)
		{
			UpdateCostTextMesh(entityDef);
			UpdateAttackTextMesh(entityDef);
			UpdateHealthTextMesh(entityDef);
			UpdateArmorTextMesh(entityDef);
			UpdateNameText();
			UpdatePowersText();
			UpdateRace(entityDef.GetRaceText());
			UpdateSecretAndQuestText();
			UpdateBannedRibbonTextMesh(entityDef);
		}
	}

	private void UpdateCostTextMesh(EntityDef entityDef)
	{
		if (!(m_costTextMesh == null))
		{
			if (HasHideStats(entityDef) || entityDef.HasTag(GAME_TAG.HIDE_COST) || UseTechLevelManaGem())
			{
				m_costTextMesh.Text = "";
			}
			else if (entityDef.HasTriggerVisual() && entityDef.IsHeroPowerOrGameModeButton())
			{
				m_costTextMesh.Text = "";
			}
			else
			{
				m_costTextMesh.Text = Convert.ToString(entityDef.GetTag(GAME_TAG.COST));
			}
		}
	}

	private void UpdateAttackTextMesh(EntityDef entityDef)
	{
		int num = entityDef.GetTag(GAME_TAG.ATK);
		if (m_attackTextMesh != null && (HasHideStats(entityDef) || entityDef.HasTag(GAME_TAG.HIDE_ATTACK)))
		{
			m_attackTextMesh.Text = "";
			m_attackTextMesh.gameObject.SetActive(value: false);
			GemObject gemObject = SceneUtils.FindComponentInThisOrParents<GemObject>(m_attackTextMesh.gameObject);
			if (gemObject != null)
			{
				gemObject.Hide();
				gemObject.SetHideNumberFlag(enable: true);
			}
		}
		else if (entityDef.IsHero())
		{
			if (num == 0)
			{
				if (m_attackObject != null && m_attackObject.activeSelf)
				{
					m_attackObject.SetActive(value: false);
				}
				if (m_attackTextMesh != null)
				{
					m_attackTextMesh.Text = "";
				}
			}
			else
			{
				if (m_attackObject != null && !m_attackObject.activeSelf)
				{
					m_attackObject.SetActive(value: true);
				}
				if (m_attackTextMesh != null)
				{
					m_attackTextMesh.Text = Convert.ToString(num);
				}
			}
		}
		else if (m_attackTextMesh != null)
		{
			m_attackTextMesh.Text = Convert.ToString(num);
		}
	}

	private void UpdateHealthTextMesh(EntityDef entityDef)
	{
		if (m_healthTextMesh == null)
		{
			return;
		}
		if (HasHideStats(entityDef) || entityDef.HasTag(GAME_TAG.HIDE_HEALTH))
		{
			m_healthTextMesh.Text = "";
			m_healthTextMesh.gameObject.SetActive(value: false);
			GemObject gemObject = SceneUtils.FindComponentInThisOrParents<GemObject>(m_healthTextMesh.gameObject);
			if (gemObject != null)
			{
				gemObject.Hide();
				gemObject.SetHideNumberFlag(enable: true);
			}
		}
		else if (entityDef.IsWeapon())
		{
			m_healthTextMesh.Text = Convert.ToString(entityDef.GetTag(GAME_TAG.DURABILITY));
		}
		else
		{
			m_healthTextMesh.Text = Convert.ToString(entityDef.GetTag(GAME_TAG.HEALTH));
		}
	}

	private void UpdateArmorTextMesh(EntityDef entityDef)
	{
		if (m_armorTextMesh == null)
		{
			return;
		}
		int num = entityDef.GetTag(GAME_TAG.ARMOR);
		if (num == 0 || HasHideStats(entityDef))
		{
			if (m_armorObject != null && m_armorObject.activeSelf)
			{
				m_armorObject.SetActive(value: false);
			}
			m_armorTextMesh.Text = "";
		}
		else
		{
			if (m_armorObject != null && !m_armorObject.activeSelf)
			{
				m_armorObject.SetActive(value: true);
			}
			m_armorTextMesh.Text = Convert.ToString(num);
		}
	}

	private void UpdateBannedRibbonTextMesh(EntityDef entityDef)
	{
		if (!(m_bannedRibbonContainer == null))
		{
			m_bannedRibbonContainer.gameObject.SetActive(value: false);
			if (!(m_bannedRibbon == null) && !entityDef.IsCustomCoin() && !CraftingManager.GetIsInCraftingMode() && RankMgr.Get().HasLocalPlayerMedalInfo && RankMgr.Get().IsCardLockedInCurrentLeague(entityDef))
			{
				m_bannedRibbonContainer.gameObject.SetActive(value: true);
				m_bannedRibbon.SetActive(value: true);
				m_bannedRibbon.GetComponentInChildren<UberText>().Text = RankMgr.Get().GetLocalPlayerStandardLeagueConfig().LockedCardUnplayableText;
			}
		}
	}

	public void UpdateMinionStatsImmediately()
	{
		if (m_entity == null || !m_entity.IsMinion() || HasHideStats(m_entity))
		{
			return;
		}
		if (m_attackTextMesh != null && !m_entity.HasTag(GAME_TAG.HIDE_ATTACK))
		{
			UpdateTextColorToGreenOrWhite(m_attackTextMesh, m_entity.GetDefATK(), m_entity.GetATK());
			m_attackTextMesh.Text = Convert.ToString(m_entity.GetATK());
		}
		if (!(m_healthTextMesh != null) || m_entity.HasTag(GAME_TAG.HIDE_HEALTH))
		{
			return;
		}
		int num = 0;
		if (m_entity.HasTag(GAME_TAG.ENABLE_HEALTH_DISPLAY))
		{
			num = m_entity.GetTag(GAME_TAG.HEALTH_DISPLAY);
			if (m_entity.HasTag(GAME_TAG.HEALTH_DISPLAY_NEGATIVE))
			{
				num = -num;
			}
			switch (m_entity.GetTag(GAME_TAG.HEALTH_DISPLAY_COLOR))
			{
			case 0:
				UpdateTextColor(m_healthTextMesh, num, num);
				break;
			case 1:
				UpdateTextColor(m_healthTextMesh, num + 1, num);
				break;
			case 2:
				UpdateTextColor(m_healthTextMesh, num - 1, num);
				break;
			}
		}
		else
		{
			int health = m_entity.GetHealth();
			int defHealth = m_entity.GetDefHealth();
			num = health - m_entity.GetDamage();
			if (m_entity.GetDamage() > 0)
			{
				UpdateTextColor(m_healthTextMesh, health, num);
			}
			else if (health > defHealth)
			{
				UpdateTextColor(m_healthTextMesh, defHealth, num);
			}
			else
			{
				UpdateTextColor(m_healthTextMesh, num, num);
			}
		}
		m_healthTextMesh.Text = Convert.ToString(num);
	}

	public virtual void UpdateTextComponents(Entity entity)
	{
		if (entity != null)
		{
			UpdateCostTextMesh(entity);
			UpdateAttackTextMesh(entity);
			UpdateHealthTextMesh(entity);
			UpdateArmorTextMesh(entity);
			UpdateNameText();
			UpdatePowersText();
			UpdateRace(entity.GetRaceText());
			UpdateSecretAndQuestText();
		}
	}

	private int GetSecretCostByClass(TAG_CLASS classType)
	{
		switch (classType)
		{
		case TAG_CLASS.PALADIN:
			return 1;
		case TAG_CLASS.HUNTER:
		case TAG_CLASS.ROGUE:
			return 2;
		case TAG_CLASS.MAGE:
			return 3;
		case TAG_CLASS.WARRIOR:
			return 0;
		default:
			return -1;
		}
	}

	private void UpdateCostTextMesh(Entity entity)
	{
		if (m_costTextMesh == null)
		{
			return;
		}
		if (HasHideStats(m_entity) || m_entity.HasTag(GAME_TAG.HIDE_COST) || UseTechLevelManaGem())
		{
			UpdateNumberText(m_costTextMesh, "", shouldHide: false);
			return;
		}
		if (m_entity.IsSecret() && m_entity.IsHidden() && m_entity.IsControlledByConcealedPlayer())
		{
			int secretCostByClass = GetSecretCostByClass(entity.GetClass());
			if (secretCostByClass >= 0)
			{
				UpdateTextColor(m_costTextMesh, secretCostByClass, entity.GetCost(), higherIsBetter: true);
			}
			else
			{
				m_costTextMesh.TextColor = Color.white;
			}
		}
		else
		{
			UpdateTextColor(m_costTextMesh, entity.GetDefCost(), entity.GetCost(), higherIsBetter: true);
		}
		if (m_entity.HasTriggerVisual() && m_entity.IsHeroPowerOrGameModeButton())
		{
			UpdateNumberText(m_costTextMesh, "", shouldHide: true);
		}
		else
		{
			UpdateNumberText(m_costTextMesh, Convert.ToString(entity.GetCost()));
		}
	}

	private void UpdateAttackTextMesh(Entity entity)
	{
		if (m_attackTextMesh == null)
		{
			return;
		}
		if (HasHideStats(entity) || entity.HasTag(GAME_TAG.HIDE_ATTACK))
		{
			UpdateNumberText(m_attackTextMesh, "", shouldHide: true);
		}
		else if (entity.IsHero())
		{
			int aTK = entity.GetATK();
			if (aTK == 0)
			{
				UpdateNumberText(m_attackTextMesh, "", shouldHide: true);
				return;
			}
			Card weaponCard = entity.GetController().GetWeaponCard();
			int defNumber = 0;
			if (weaponCard != null)
			{
				defNumber = weaponCard.GetEntity().GetATK();
			}
			UpdateTextColorToGreenOrWhite(m_attackTextMesh, defNumber, aTK);
			UpdateNumberText(m_attackTextMesh, Convert.ToString(aTK));
		}
		else
		{
			int num = entity.GetATK();
			if (entity.IsDormant() && entity.HasCachedTagForDormant(GAME_TAG.ATK))
			{
				num = entity.GetCachedTagForDormant(GAME_TAG.ATK);
			}
			UpdateTextColorToGreenOrWhite(m_attackTextMesh, entity.GetDefATK(), num);
			UpdateNumberText(m_attackTextMesh, Convert.ToString(num));
		}
	}

	private void UpdateHealthTextMesh(Entity entity)
	{
		if (!(m_healthTextMesh != null) || (entity.IsHero() && entity.GetZone() == TAG_ZONE.GRAVEYARD))
		{
			return;
		}
		if (HasHideStats(entity) || entity.HasTag(GAME_TAG.HIDE_HEALTH))
		{
			UpdateNumberText(m_healthTextMesh, "", shouldHide: true);
			return;
		}
		int num;
		int num2;
		if (entity.IsWeapon())
		{
			num = entity.GetDurability();
			num2 = entity.GetDefDurability();
		}
		else
		{
			num = entity.GetHealth();
			num2 = entity.GetDefHealth();
		}
		int num3 = entity.GetDamage();
		if (entity.IsDormant())
		{
			if (entity.HasCachedTagForDormant(GAME_TAG.HEALTH))
			{
				num = entity.GetCachedTagForDormant(GAME_TAG.HEALTH);
			}
			if (entity.HasCachedTagForDormant(GAME_TAG.DAMAGE))
			{
				num3 = entity.GetCachedTagForDormant(GAME_TAG.DAMAGE);
			}
		}
		int num4 = num - num3;
		if (m_entity.HasTag(GAME_TAG.ENABLE_HEALTH_DISPLAY))
		{
			num4 = m_entity.GetTag(GAME_TAG.HEALTH_DISPLAY);
			if (m_entity.HasTag(GAME_TAG.HEALTH_DISPLAY_NEGATIVE))
			{
				num4 = -num4;
			}
			switch (m_entity.GetTag(GAME_TAG.HEALTH_DISPLAY_COLOR))
			{
			case 0:
				UpdateTextColor(m_healthTextMesh, num4, num4);
				break;
			case 1:
				UpdateTextColor(m_healthTextMesh, num4 + 1, num4);
				break;
			case 2:
				UpdateTextColor(m_healthTextMesh, num4 - 1, num4);
				break;
			}
		}
		else if (entity.GetDamage() > 0)
		{
			UpdateTextColor(m_healthTextMesh, num, num4);
		}
		else if (num > num2)
		{
			UpdateTextColor(m_healthTextMesh, num2, num4);
		}
		else
		{
			UpdateTextColor(m_healthTextMesh, num4, num4);
		}
		UpdateNumberText(m_healthTextMesh, Convert.ToString(num4));
	}

	private void UpdateArmorTextMesh(Entity entity)
	{
		if (m_armorTextMesh == null)
		{
			return;
		}
		if (HasHideStats(entity))
		{
			UpdateNumberText(m_armorTextMesh, "", shouldHide: true);
			return;
		}
		int armor = entity.GetArmor();
		if (armor == 0)
		{
			UpdateNumberText(m_armorTextMesh, "", shouldHide: true);
		}
		else
		{
			UpdateNumberText(m_armorTextMesh, Convert.ToString(armor));
		}
	}

	public void SetCardDefPowerTextOverride(string text)
	{
		m_cardDefPowerTextOverride = text;
	}

	public void UpdatePowersText()
	{
		if (m_powersTextMesh == null)
		{
			return;
		}
		string text;
		if (ShouldUseEntityDefForPowersText())
		{
			text = (string.IsNullOrEmpty(m_cardDefPowerTextOverride) ? m_entityDef.GetCardTextInHand() : m_cardDefPowerTextOverride);
		}
		else
		{
			text = ((m_entity.IsSecret() && m_entity.IsHidden() && m_entity.IsControlledByConcealedPlayer()) ? GameStrings.Get("GAMEPLAY_SECRET_DESC") : ((!m_entity.IsHistoryDupe()) ? m_entity.GetCardTextInHand() : m_entity.GetCardTextInHistory()));
			if (GameState.Get() != null && GameState.Get().GetGameEntity() != null)
			{
				text = GameState.Get().GetGameEntity().UpdateCardText(m_card, this, text);
			}
		}
		UpdateText(m_powersTextMesh, text);
	}

	private bool ShouldUseEntityDefForPowersText()
	{
		if (m_entityDef == null)
		{
			return false;
		}
		if (m_entity == null)
		{
			return true;
		}
		if (m_entity.GetCardTextBuilder().ShouldUseEntityForTextInPlay())
		{
			return false;
		}
		return true;
	}

	private void UpdateNumberText(UberText textMesh, string newText)
	{
		UpdateNumberText(textMesh, newText, shouldHide: false);
	}

	private void UpdateNumberText(UberText textMesh, string newText, bool shouldHide)
	{
		GemObject gemObject = SceneUtils.FindComponentInThisOrParents<GemObject>(textMesh.gameObject);
		if (gemObject != null)
		{
			if (!gemObject.IsNumberHidden())
			{
				if (shouldHide)
				{
					textMesh.gameObject.SetActive(value: false);
					if (GetHistoryCard() != null || GetHistoryChildCard() != null)
					{
						gemObject.Hide();
					}
					else
					{
						gemObject.ScaleToZero();
					}
				}
				else if (textMesh.Text != newText)
				{
					gemObject.Jiggle();
				}
			}
			else if (!shouldHide)
			{
				textMesh.gameObject.SetActive(value: true);
				gemObject.SetToZeroThenEnlarge();
			}
			gemObject.Initialize();
			gemObject.SetHideNumberFlag(shouldHide);
		}
		textMesh.Text = newText;
	}

	public void UpdateNameText()
	{
		if (m_nameTextMesh == null)
		{
			return;
		}
		string text = "";
		bool flag = false;
		if (m_entity != null)
		{
			if (m_entityDef == null)
			{
				flag = m_entity.IsSecret() && m_entity.IsHidden() && m_entity.IsControlledByConcealedPlayer();
			}
			text = m_entity.GetName();
		}
		else if (m_entityDef != null)
		{
			string shortName = m_entityDef.GetShortName();
			text = ((m_useShortName && shortName != null) ? shortName : m_entityDef.GetName());
		}
		if (flag)
		{
			text = ((!GameState.Get().GetBooleanGameOption(GameEntityOption.USE_SECRET_CLASS_NAMES)) ? GameStrings.Get("GAMEPLAY_SECRET_NAME") : (m_entity.GetClass() switch
			{
				TAG_CLASS.PALADIN => GameStrings.Get("GAMEPLAY_SECRET_NAME_PALADIN"), 
				TAG_CLASS.MAGE => GameStrings.Get("GAMEPLAY_SECRET_NAME_MAGE"), 
				TAG_CLASS.HUNTER => GameStrings.Get("GAMEPLAY_SECRET_NAME_HUNTER"), 
				TAG_CLASS.ROGUE => GameStrings.Get("GAMEPLAY_SECRET_NAME_ROGUE"), 
				_ => GameStrings.Get("GAMEPLAY_SECRET_NAME"), 
			}));
		}
		UpdateText(m_nameTextMesh, text);
	}

	private void UpdateSecretAndQuestText()
	{
		if (!m_secretText)
		{
			return;
		}
		string text = "?";
		if (m_entity != null)
		{
			if (m_entity.IsQuest() || m_entity.IsSideQuest())
			{
				text = "!";
			}
			else if (m_entity.IsPuzzle())
			{
				text = "P";
			}
		}
		if ((bool)UniversalInputManager.UsePhoneUI && m_entity != null)
		{
			TransformUtil.SetLocalPosZ(m_secretText, -0.01f);
			Player controller = m_entity.GetController();
			if (controller != null && m_entity.IsSecret())
			{
				ZoneSecret secretZone = controller.GetSecretZone();
				if ((bool)secretZone)
				{
					int secretCount = secretZone.GetSecretCount();
					if (secretCount > 1)
					{
						text = secretCount.ToString();
						TransformUtil.SetLocalPosZ(m_secretText, -0.03f);
					}
				}
			}
			else if (controller != null && m_entity.IsSideQuest())
			{
				TransformUtil.SetLocalPosZ(m_secretText, 0.01f);
				ZoneSecret secretZone2 = controller.GetSecretZone();
				if ((bool)secretZone2)
				{
					int sideQuestCount = secretZone2.GetSideQuestCount();
					if (sideQuestCount > 1)
					{
						text = sideQuestCount.ToString();
						TransformUtil.SetLocalPosZ(m_secretText, -0.02f);
					}
				}
			}
			Transform transform = m_secretText.transform.parent.Find("Secret_mesh");
			if (transform != null && transform.gameObject != null)
			{
				SphereCollider component = transform.gameObject.GetComponent<SphereCollider>();
				if (component != null)
				{
					component.radius = 0.5f;
				}
			}
		}
		UpdateText(m_secretText, text);
	}

	private void UpdateText(UberText uberTextMesh, string text)
	{
		if (!(uberTextMesh == null))
		{
			uberTextMesh.Text = text;
		}
	}

	private void UpdateTextColor(UberText originalMesh, int defNumber, int currentNumber)
	{
		UpdateTextColor(originalMesh, defNumber, currentNumber, higherIsBetter: false);
	}

	private void UpdateTextColor(UberText uberTextMesh, int defNumber, int currentNumber, bool higherIsBetter)
	{
		if ((defNumber > currentNumber && higherIsBetter) || (defNumber < currentNumber && !higherIsBetter))
		{
			uberTextMesh.TextColor = Color.green;
		}
		else if ((defNumber < currentNumber && higherIsBetter) || (defNumber > currentNumber && !higherIsBetter))
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				uberTextMesh.TextColor = new Color(1f, 10f / 51f, 10f / 51f);
			}
			else
			{
				uberTextMesh.TextColor = Color.red;
			}
		}
		else if (defNumber == currentNumber)
		{
			uberTextMesh.TextColor = Color.white;
		}
	}

	private void UpdateTextColorToGreenOrWhite(UberText uberTextMesh, int defNumber, int currentNumber)
	{
		if (defNumber < currentNumber)
		{
			uberTextMesh.TextColor = Color.green;
		}
		else
		{
			uberTextMesh.TextColor = Color.white;
		}
	}

	private void DisableTextMesh(UberText mesh)
	{
		if (!(mesh == null))
		{
			mesh.gameObject.SetActive(value: false);
		}
	}

	public void SetUseShortName(bool useShortName)
	{
		m_useShortName = useShortName;
	}

	public void OverrideNameText(UberText newText)
	{
		if (m_nameTextMesh != null)
		{
			m_nameTextMesh.gameObject.SetActive(value: false);
		}
		m_nameTextMesh = newText;
		UpdateNameText();
		if (m_shown && newText != null)
		{
			newText.gameObject.SetActive(value: true);
		}
	}

	public void HideAllText()
	{
		ToggleTextVisibility(bOn: false);
	}

	public void ShowAllText()
	{
		ToggleTextVisibility(bOn: true);
	}

	private void ToggleTextVisibility(bool bOn)
	{
		if (m_healthTextMesh != null)
		{
			m_healthTextMesh.gameObject.SetActive(bOn);
		}
		if (m_armorTextMesh != null)
		{
			m_armorTextMesh.gameObject.SetActive(bOn);
		}
		if (m_attackTextMesh != null)
		{
			m_attackTextMesh.gameObject.SetActive(bOn);
		}
		if (m_nameTextMesh != null)
		{
			m_nameTextMesh.gameObject.SetActive(bOn);
			if ((bool)m_nameTextMesh.RenderOnObject)
			{
				m_nameTextMesh.RenderOnObject.GetComponent<Renderer>().enabled = bOn;
			}
		}
		if (m_powersTextMesh != null)
		{
			m_powersTextMesh.gameObject.SetActive(bOn);
		}
		if (m_costTextMesh != null)
		{
			m_costTextMesh.gameObject.SetActive(bOn);
		}
		if (m_raceTextMesh != null)
		{
			m_raceTextMesh.gameObject.SetActive(bOn);
		}
		if ((bool)m_secretText)
		{
			m_secretText.gameObject.SetActive(bOn);
		}
	}

	public void CreateBannedRibbon()
	{
		if (m_bannedRibbonContainer != null)
		{
			m_bannedRibbonContainer.gameObject.SetActive(value: true);
			m_bannedRibbon = m_bannedRibbonContainer.PrefabGameObject(instantiateIfNeeded: true);
		}
	}

	public bool IsContactShadowEnabled()
	{
		return m_shadowVisible;
	}

	public bool HasContactShadowObject()
	{
		if (!(m_shadowObject != null))
		{
			return m_uniqueShadowObject != null;
		}
		return true;
	}

	public void ContactShadow(bool visible)
	{
		m_shadowVisible = visible;
		if (!m_shadowObjectInitialized)
		{
			CacheShadowObjects();
		}
		UpdateContactShadow();
	}

	public void UpdateContactShadow()
	{
		Renderer renderer = ((m_shadowObject != null) ? m_shadowObject.GetComponent<Renderer>() : null);
		Renderer renderer2 = ((m_uniqueShadowObject != null) ? m_uniqueShadowObject.GetComponent<Renderer>() : null);
		if (m_shadowVisible && m_shown)
		{
			if (IsElite())
			{
				if (renderer != null)
				{
					renderer.enabled = false;
				}
				if (renderer2 != null)
				{
					renderer2.enabled = true;
				}
			}
			else
			{
				if (renderer != null)
				{
					renderer.enabled = true;
				}
				if (renderer2 != null)
				{
					renderer2.enabled = false;
				}
			}
		}
		else
		{
			if (renderer != null)
			{
				renderer.enabled = false;
			}
			if (renderer2 != null)
			{
				renderer2.enabled = false;
			}
		}
	}

	public void MoveShadowToMissingCard(bool reset, int renderQueue = 0)
	{
		Transform transform = null;
		if (reset && m_cardMesh != null)
		{
			transform = m_cardMesh.transform;
		}
		else
		{
			if (reset || !(m_missingCardEffect != null))
			{
				return;
			}
			transform = m_missingCardEffect.transform;
		}
		bool flag = IsElite();
		GameObject gameObject = (flag ? m_uniqueShadowObject : m_shadowObject);
		Renderer renderer = ((gameObject != null) ? gameObject.GetComponent<Renderer>() : null);
		if (!(renderer == null))
		{
			int num = (flag ? m_initialUniqueShadowRenderQueue : m_initialShadowRenderQueue);
			int renderQueue2 = (reset ? num : (renderer.GetMaterial().renderQueue + renderQueue));
			gameObject.transform.parent = transform;
			renderer.GetMaterial().renderQueue = renderQueue2;
		}
	}

	public void UpdateMeshComponents()
	{
		UpdateRarityComponent();
		UpdateDescriptionMesh();
		UpdateEliteComponent();
		UpdatePremiumComponents();
		UpdateCardColor();
	}

	private void UpdateRarityComponent()
	{
		if ((bool)m_rarityGemMesh)
		{
			UnityEngine.Vector2 offset;
			Color tint;
			bool rarityTextureOffset = GetRarityTextureOffset(out offset, out tint);
			SceneUtils.EnableRenderers(m_rarityGemMesh, rarityTextureOffset, includeInactive: true);
			if ((bool)m_rarityFrameMesh)
			{
				SceneUtils.EnableRenderers(m_rarityFrameMesh, rarityTextureOffset, includeInactive: true);
			}
			if (rarityTextureOffset)
			{
				Material materialInstance = GetMaterialInstance(m_rarityGemMesh.GetComponent<Renderer>());
				materialInstance.mainTextureOffset = offset;
				materialInstance.SetColor("_tint", tint);
			}
		}
	}

	private bool GetRarityTextureOffset(out UnityEngine.Vector2 offset, out Color tint)
	{
		offset = GEM_TEXTURE_OFFSET_COMMON;
		tint = GEM_COLOR_COMMON;
		if (m_entityDef == null && m_entity == null)
		{
			return false;
		}
		TAG_CARD_SET tAG_CARD_SET = ((m_entityDef == null) ? m_entity.GetCardSet() : m_entityDef.GetCardSet());
		if (tAG_CARD_SET == TAG_CARD_SET.MISSIONS)
		{
			return false;
		}
		switch (GetRarity())
		{
		case TAG_RARITY.COMMON:
			offset = GEM_TEXTURE_OFFSET_COMMON;
			tint = GEM_COLOR_COMMON;
			break;
		case TAG_RARITY.RARE:
			offset = GEM_TEXTURE_OFFSET_RARE;
			tint = GEM_COLOR_RARE;
			break;
		case TAG_RARITY.EPIC:
			offset = GEM_TEXTURE_OFFSET_EPIC;
			tint = GEM_COLOR_EPIC;
			break;
		case TAG_RARITY.LEGENDARY:
			offset = GEM_TEXTURE_OFFSET_LEGENDARY;
			tint = GEM_COLOR_LEGENDARY;
			break;
		default:
			return false;
		}
		return true;
	}

	private void UpdateDescriptionMesh()
	{
		bool flag = true;
		if (m_premiumType == TAG_PREMIUM.NORMAL)
		{
			EntityBase entityBase = (EntityBase)(((object)m_entity) ?? ((object)m_entityDef));
			if (entityBase != null && entityBase.IsWeapon() && entityBase.GetClass() == TAG_CLASS.DEATHKNIGHT)
			{
				flag = false;
			}
		}
		if (m_descriptionMesh != null)
		{
			Renderer component = m_descriptionMesh.GetComponent<Renderer>();
			if (component != null)
			{
				component.enabled = flag;
			}
		}
		if (m_descriptionTrimMesh != null)
		{
			Renderer component = m_descriptionTrimMesh.GetComponent<Renderer>();
			if (component != null)
			{
				component.enabled = flag;
			}
		}
		if (flag)
		{
			UpdateWatermark();
		}
	}

	private void UpdateWatermark()
	{
		if (m_entityDef == null && m_entity == null)
		{
			return;
		}
		string text = null;
		string watermarkTextureOverride = (m_entityDef ?? m_entity.GetEntityDef()).GetWatermarkTextureOverride();
		TAG_CARD_SET cardSetId = GetCardSet();
		if (m_watermarkCardSetOverride != 0)
		{
			cardSetId = m_watermarkCardSetOverride;
		}
		else if (!string.IsNullOrEmpty(watermarkTextureOverride))
		{
			text = watermarkTextureOverride;
		}
		if (text == null)
		{
			CardSetDbfRecord cardSet = GameDbf.GetIndex().GetCardSet(cardSetId);
			if (cardSet != null)
			{
				text = cardSet.CardWatermarkTexture;
			}
		}
		float num = 0f;
		num = (((m_entityDef == null || !m_entityDef.HasTag(GAME_TAG.HIDE_WATERMARK)) && (m_entity == null || !m_entity.HasTag(GAME_TAG.HIDE_WATERMARK))) ? WATERMARK_ALPHA_VALUE : 0f);
		if (m_descriptionMesh != null && m_descriptionMesh.GetComponent<Renderer>() != null && m_descriptionMesh.GetComponent<Renderer>().GetSharedMaterial().HasProperty("_SecondTint") && m_descriptionMesh.GetComponent<Renderer>().GetSharedMaterial().HasProperty("_SecondTex"))
		{
			if (!string.IsNullOrEmpty(text))
			{
				AssetLoader.Get().LoadAsset(ref m_watermarkTex, text);
				GetMaterialInstance(m_descriptionMesh.GetComponent<Renderer>()).SetTexture("_SecondTex", m_watermarkTex);
			}
			else
			{
				num = 0f;
			}
			Material materialInstance = GetMaterialInstance(m_descriptionMesh.GetComponent<Renderer>());
			Color color = materialInstance.GetColor("_SecondTint");
			color.a = num;
			materialInstance.SetColor("_SecondTint", color);
		}
		if (m_watermarkMesh != null && m_watermarkMesh.GetComponent<Renderer>() != null && m_watermarkMesh.GetComponent<Renderer>().GetSharedMaterial().HasProperty("_Color") && m_watermarkMesh.GetComponent<Renderer>().GetSharedMaterial().HasProperty("_MainTex"))
		{
			if (!string.IsNullOrEmpty(text))
			{
				AssetLoader.Get().LoadAsset(ref m_watermarkTex, text);
				GetMaterialInstance(m_watermarkMesh.GetComponent<Renderer>()).SetTexture("_MainTex", m_watermarkTex);
			}
			else
			{
				num = 0f;
			}
			Material materialInstance2 = GetMaterialInstance(m_watermarkMesh.GetComponent<Renderer>());
			Color color2 = materialInstance2.GetColor("_Color");
			color2.a = num;
			materialInstance2.SetColor("_Color", color2);
		}
	}

	private void UpdateEliteComponent()
	{
		if (!(m_eliteObject == null))
		{
			bool enable = IsElite();
			SceneUtils.EnableRenderers(m_eliteObject, enable, includeInactive: true);
		}
	}

	private void UpdatePremiumComponents()
	{
		if (m_premiumType != 0 && !(m_glints == null))
		{
			m_glints.SetActive(value: true);
			Renderer[] componentsInChildren = m_glints.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = true;
			}
		}
	}

	private void UpdateRace(string raceText)
	{
		if (m_entityDef == null && m_entity == null)
		{
			return;
		}
		bool flag = ((m_entity != null) ? m_entity.IsMinion() : m_entityDef.IsMinion());
		bool flag2 = ((m_entity != null) ? m_entity.IsSpell() : m_entityDef.IsSpell());
		bool flag3 = ((m_entity != null) ? m_entity.IsWeapon() : m_entityDef.IsWeapon());
		bool flag4 = ((m_entity != null) ? m_entity.IsHero() : m_entityDef.IsHero());
		if ((flag && m_racePlateObject == null) || flag4 || (flag2 && (m_descriptionMesh == null || m_spellDescriptionMeshNeutral == null || m_spellDescriptionMeshSchool == null)))
		{
			return;
		}
		bool flag5 = !string.IsNullOrEmpty(raceText);
		if (flag)
		{
			MeshRenderer[] components = m_racePlateObject.GetComponents<MeshRenderer>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].enabled = flag5;
			}
		}
		else if (flag2)
		{
			MeshFilter[] components2 = m_descriptionMesh.GetComponents<MeshFilter>();
			foreach (MeshFilter meshFilter in components2)
			{
				if (flag5)
				{
					meshFilter.sharedMesh = m_spellDescriptionMeshSchool;
				}
				else
				{
					meshFilter.sharedMesh = m_spellDescriptionMeshNeutral;
				}
			}
		}
		if (flag5 || flag3)
		{
			if (m_descriptionMesh != null && m_descriptionMesh.GetComponent<Renderer>() != null)
			{
				GetMaterialInstance(m_descriptionMesh.GetComponent<Renderer>()).SetTextureOffset("_SecondTex", descriptionMesh_WithRace_TextureOffset);
			}
			if (m_watermarkMesh != null && m_watermarkMesh.GetComponent<Renderer>() != null)
			{
				GetMaterialInstance(m_watermarkMesh.GetComponent<Renderer>()).SetTextureOffset("_MainTex", descriptionMesh_WithRace_TextureOffset);
			}
		}
		else
		{
			if (m_descriptionMesh != null && m_descriptionMesh.GetComponent<Renderer>() != null)
			{
				GetMaterialInstance(m_descriptionMesh.GetComponent<Renderer>()).SetTextureOffset("_SecondTex", descriptionMesh_WithoutRace_TextureOffset);
			}
			if (m_watermarkMesh != null && m_watermarkMesh.GetComponent<Renderer>() != null)
			{
				GetMaterialInstance(m_watermarkMesh.GetComponent<Renderer>()).SetTextureOffset("_MainTex", descriptionMesh_WithoutRace_TextureOffset);
			}
		}
		if (!(m_raceTextMesh == null))
		{
			if (Localization.GetLocale() == Locale.thTH)
			{
				m_raceTextMesh.ResizeToFit = false;
				m_raceTextMesh.ResizeToFitAndGrow = false;
			}
			m_raceTextMesh.Text = raceText;
		}
	}

	private static Material GetMaterialInstance(Renderer r)
	{
		return r.GetMaterial();
	}

	public MultiClassBannerTransition GetMultiClassBanner()
	{
		return m_multiClassBanner;
	}

	public void UpdateCardColor()
	{
		if ((m_legacyPortraitMaterialIndex < 0 && m_cardMesh == null) || (GetEntityDef() == null && GetEntity() == null))
		{
			return;
		}
		bool flag = false;
		int num = 0;
		m_usesMultiClassBanner = false;
		TAG_CARDTYPE cardType;
		TAG_CLASS tAG_CLASS;
		if (m_entityDef != null)
		{
			cardType = m_entityDef.GetCardType();
			tAG_CLASS = m_entityDef.GetClass();
			flag = m_entityDef.IsMultiClass();
			num = m_entityDef.GetTag(GAME_TAG.MULTI_CLASS_GROUP);
		}
		else if (m_entity != null)
		{
			cardType = m_entity.GetCardType();
			tAG_CLASS = m_entity.GetClass();
			flag = m_entity.IsMultiClass();
			num = m_entity.GetTag(GAME_TAG.MULTI_CLASS_GROUP);
		}
		else
		{
			cardType = TAG_CARDTYPE.INVALID;
			tAG_CLASS = TAG_CLASS.INVALID;
			flag = false;
			num = 0;
		}
		Color magenta = Color.magenta;
		CardColorSwitcher.CardColorType cardColorType;
		switch (tAG_CLASS)
		{
		case TAG_CLASS.WARLOCK:
			cardColorType = CardColorSwitcher.CardColorType.TYPE_WARLOCK;
			magenta = CLASS_COLOR_WARLOCK;
			break;
		case TAG_CLASS.ROGUE:
			cardColorType = CardColorSwitcher.CardColorType.TYPE_ROGUE;
			magenta = CLASS_COLOR_ROGUE;
			break;
		case TAG_CLASS.DRUID:
			cardColorType = CardColorSwitcher.CardColorType.TYPE_DRUID;
			magenta = CLASS_COLOR_DRUID;
			break;
		case TAG_CLASS.HUNTER:
			cardColorType = CardColorSwitcher.CardColorType.TYPE_HUNTER;
			magenta = CLASS_COLOR_HUNTER;
			break;
		case TAG_CLASS.MAGE:
			cardColorType = CardColorSwitcher.CardColorType.TYPE_MAGE;
			magenta = CLASS_COLOR_MAGE;
			break;
		case TAG_CLASS.PALADIN:
			cardColorType = CardColorSwitcher.CardColorType.TYPE_PALADIN;
			magenta = CLASS_COLOR_PALADIN;
			break;
		case TAG_CLASS.PRIEST:
			cardColorType = CardColorSwitcher.CardColorType.TYPE_PRIEST;
			magenta = CLASS_COLOR_PRIEST;
			break;
		case TAG_CLASS.SHAMAN:
			cardColorType = CardColorSwitcher.CardColorType.TYPE_SHAMAN;
			magenta = CLASS_COLOR_SHAMAN;
			break;
		case TAG_CLASS.WARRIOR:
			cardColorType = CardColorSwitcher.CardColorType.TYPE_WARRIOR;
			magenta = CLASS_COLOR_WARRIOR;
			break;
		case TAG_CLASS.DREAM:
			cardColorType = CardColorSwitcher.CardColorType.TYPE_HUNTER;
			magenta = CLASS_COLOR_HUNTER;
			break;
		case TAG_CLASS.DEATHKNIGHT:
			cardColorType = CardColorSwitcher.CardColorType.TYPE_DEATHKNIGHT;
			magenta = CLASS_COLOR_DEATHKNIGHT;
			break;
		case TAG_CLASS.DEMONHUNTER:
			cardColorType = CardColorSwitcher.CardColorType.TYPE_DEMONHUNTER;
			magenta = CLASS_COLOR_DEMONHUNTER;
			break;
		default:
			cardColorType = CardColorSwitcher.CardColorType.TYPE_GENERIC;
			cardColorType = CardColorSwitcher.CardColorType.TYPE_GENERIC;
			magenta = CLASS_COLOR_GENERIC;
			break;
		}
		if (flag)
		{
			cardColorType = CardColorSwitcher.CardColorType.TYPE_GENERIC;
			MultiClassGroupDbfRecord record = GameDbf.MultiClassGroup.GetRecord(num);
			if (record != null)
			{
				cardColorType = (CardColorSwitcher.CardColorType)record.CardColorType;
			}
			if (record != null && !string.IsNullOrEmpty(record.IconAssetPath) && m_multiClassBannerContainer != null)
			{
				m_usesMultiClassBanner = true;
				m_multiClassBannerContainer.gameObject.SetActive(value: true);
				m_multiClassBanner = m_multiClassBannerContainer.PrefabGameObject(instantiateIfNeeded: true).GetComponent<MultiClassBannerTransition>();
				if (m_multiClassBanner != null)
				{
					IEnumerable<TAG_CLASS> enumerable;
					if (m_entityDef != null)
					{
						enumerable = m_entityDef.GetClasses(MultiClassBannerTransition.CompareClasses);
					}
					else if (m_entity != null)
					{
						enumerable = m_entity.GetClasses(MultiClassBannerTransition.CompareClasses);
						if (m_entity.GetZone() == TAG_ZONE.HAND && !m_entity.IsHistoryDupe())
						{
							foreach (TAG_CLASS item in enumerable)
							{
								if (item == m_entity.GetHero().GetClass())
								{
									enumerable = new TAG_CLASS[1] { item };
									break;
								}
							}
						}
					}
					else
					{
						enumerable = new List<TAG_CLASS>();
					}
					m_multiClassBanner.SetClasses(enumerable);
					m_multiClassBanner.SetMultiClassGroup(num);
					if (m_premiumType >= TAG_PREMIUM.GOLDEN)
					{
						m_multiClassBanner.SetGoldenCardMesh(m_cardMesh, m_premiumRibbon);
					}
					Vector3 localPosition = m_manaObject.transform.localPosition;
					Vector3 localPosition2 = m_costTextMesh.transform.localPosition;
					localPosition.y = 0.027f;
					localPosition2.y = 0.088f;
					m_manaObject.transform.localPosition = localPosition;
					m_costTextMesh.transform.localPosition = localPosition2;
				}
			}
		}
		else
		{
			if (m_premiumRibbon > -1 && m_initialPremiumRibbonMaterial != null)
			{
				Renderer component = m_cardMesh.GetComponent<Renderer>();
				if (m_premiumRibbon < component.GetMaterials().Count)
				{
					component.SetMaterial(m_premiumRibbon, m_initialPremiumRibbonMaterial);
				}
			}
			if (m_multiClassBannerContainer != null)
			{
				m_multiClassBannerContainer.gameObject.SetActive(value: false);
			}
		}
		SetMaterial(cardType, cardColorType, magenta);
	}

	private void SetMaterial(TAG_CARDTYPE cardType, CardColorSwitcher.CardColorType colorType, Color cardColor)
	{
		switch (m_premiumType)
		{
		case TAG_PREMIUM.DIAMOND:
			SetMaterialGolden(cardType, colorType, cardColor);
			break;
		case TAG_PREMIUM.GOLDEN:
			SetMaterialGolden(cardType, colorType, cardColor);
			break;
		case TAG_PREMIUM.NORMAL:
			SetMaterialNormal(cardType, colorType, cardColor);
			break;
		default:
			Debug.LogWarning($"Actor.SetMaterial(): unexpected premium type {m_premiumType}");
			break;
		}
	}

	private void SetHistoryHeroBannerColor()
	{
		if (m_entity != null && !m_entity.IsControlledByFriendlySidePlayer() && m_entity.IsHistoryDupe())
		{
			Transform transform = GetRootObject().transform.Find("History_Hero_Banner");
			if (!(transform == null))
			{
				GetMaterialInstance(transform.GetComponent<Renderer>()).mainTextureOffset = new UnityEngine.Vector2(0.005f, -0.505f);
			}
		}
	}

	private void GetDualClassColors(CardColorSwitcher.CardColorType dualClassCombo, out Color left, out Color right)
	{
		switch (dualClassCombo)
		{
		case CardColorSwitcher.CardColorType.TYPE_PALADIN_PRIEST:
			left = CLASS_COLOR_PALADIN;
			right = CLASS_COLOR_PRIEST;
			break;
		case CardColorSwitcher.CardColorType.TYPE_WARLOCK_PRIEST:
			left = CLASS_COLOR_PRIEST;
			right = CLASS_COLOR_WARLOCK;
			break;
		case CardColorSwitcher.CardColorType.TYPE_WARLOCK_DEMONHUNTER:
			left = CLASS_COLOR_WARLOCK;
			right = CLASS_COLOR_DEMONHUNTER;
			break;
		case CardColorSwitcher.CardColorType.TYPE_HUNTER_DEMONHUNTER:
			left = CLASS_COLOR_DEMONHUNTER;
			right = CLASS_COLOR_HUNTER;
			break;
		case CardColorSwitcher.CardColorType.TYPE_DRUID_HUNTER:
			left = CLASS_COLOR_HUNTER;
			right = CLASS_COLOR_DRUID;
			break;
		case CardColorSwitcher.CardColorType.TYPE_DRUID_SHAMAN:
			left = CLASS_COLOR_DRUID;
			right = CLASS_COLOR_SHAMAN;
			break;
		case CardColorSwitcher.CardColorType.TYPE_SHAMAN_MAGE:
			left = CLASS_COLOR_SHAMAN;
			right = CLASS_COLOR_MAGE;
			break;
		case CardColorSwitcher.CardColorType.TYPE_MAGE_ROGUE:
			left = CLASS_COLOR_MAGE;
			right = CLASS_COLOR_ROGUE;
			break;
		case CardColorSwitcher.CardColorType.TYPE_WARRIOR_ROGUE:
			left = CLASS_COLOR_ROGUE;
			right = CLASS_COLOR_WARRIOR;
			break;
		case CardColorSwitcher.CardColorType.TYPE_WARRIOR_PALADIN:
			left = CLASS_COLOR_WARRIOR;
			right = CLASS_COLOR_PALADIN;
			break;
		default:
			left = (right = Color.magenta);
			break;
		}
	}

	private void SetMaterialGolden(TAG_CARDTYPE cardType, CardColorSwitcher.CardColorType colorType, Color cardColor)
	{
		if (m_cardMesh != null && m_premiumRibbon >= 0)
		{
			Material material = m_cardMesh.GetComponent<Renderer>().GetMaterial(m_premiumRibbon);
			if (colorType >= CardColorSwitcher.CardColorType.TYPE_GENERIC && colorType <= CardColorSwitcher.CardColorType.TYPE_DEMONHUNTER)
			{
				material.color = cardColor;
				material.SetFloat("_EnableDualClass", 0f);
			}
			else
			{
				GetDualClassColors(colorType, out var left, out var right);
				material.SetFloat("_EnableDualClass", 1f);
				material.SetColor("_Color", left);
				material.SetColor("_SecondColor", right);
			}
		}
		if (cardType == TAG_CARDTYPE.HERO)
		{
			SetHistoryHeroBannerColor();
		}
	}

	private void SetMaterialNormal(TAG_CARDTYPE cardType, CardColorSwitcher.CardColorType colorType, Color cardColor)
	{
		switch (cardType)
		{
		case TAG_CARDTYPE.SPELL:
			SetMaterialWithTexture(cardType, colorType);
			break;
		case TAG_CARDTYPE.MINION:
			SetMaterialWithTexture(cardType, colorType);
			break;
		case TAG_CARDTYPE.WEAPON:
			SetMaterialWeapon(colorType, cardColor);
			break;
		case TAG_CARDTYPE.HERO:
			SetMaterialHero(colorType);
			break;
		case TAG_CARDTYPE.ENCHANTMENT:
			break;
		}
	}

	private void SetMaterialWithTexture(TAG_CARDTYPE cardType, CardColorSwitcher.CardColorType colorType)
	{
		if (CardColorSwitcher.Get() == null)
		{
			return;
		}
		AssetLoader.Get().LoadAsset(ref m_cardColorTex, CardColorSwitcher.Get().GetTexture(cardType, colorType));
		if ((bool)m_cardMesh)
		{
			if (m_cardFrontMatIdx > -1)
			{
				m_cardMesh.GetComponent<Renderer>().GetMaterial(m_cardFrontMatIdx).mainTexture = m_cardColorTex;
			}
			switch (cardType)
			{
			case TAG_CARDTYPE.WEAPON:
				if (colorType != CardColorSwitcher.CardColorType.TYPE_DEATHKNIGHT)
				{
					break;
				}
				goto case TAG_CARDTYPE.SPELL;
			case TAG_CARDTYPE.SPELL:
				if ((bool)m_portraitMesh && m_portraitFrameMatIdx > -1)
				{
					m_portraitMesh.GetComponent<Renderer>().GetMaterial(m_portraitFrameMatIdx).mainTexture = m_cardColorTex;
				}
				break;
			}
		}
		else if (m_legacyCardColorMaterialIndex >= 0 && m_meshRenderer != null)
		{
			m_meshRenderer.GetMaterial(m_legacyCardColorMaterialIndex).mainTexture = m_cardColorTex;
		}
	}

	private void SetMaterialHero(CardColorSwitcher.CardColorType colorType)
	{
		SetMaterialWithTexture(TAG_CARDTYPE.HERO, colorType);
		SetHistoryHeroBannerColor();
	}

	private void SetMaterialWeapon(CardColorSwitcher.CardColorType colorType, Color cardColor)
	{
		if ((bool)CardColorSwitcher.Get() && !string.IsNullOrEmpty(CardColorSwitcher.Get().GetTexture(TAG_CARDTYPE.WEAPON, colorType)))
		{
			SetMaterialWithTexture(TAG_CARDTYPE.WEAPON, colorType);
		}
		else if ((bool)m_descriptionTrimMesh)
		{
			GetMaterialInstance(m_descriptionTrimMesh.GetComponent<Renderer>()).SetColor("_Color", cardColor);
		}
	}

	public bool UseTechLevelManaGem()
	{
		if (m_entity != null && !m_entity.IsMinion())
		{
			return false;
		}
		if (m_entityDef != null && !m_entityDef.IsMinion())
		{
			return false;
		}
		if (GameState.Get() == null)
		{
			return false;
		}
		return GameState.Get().GetGameEntity()?.HasTag(GAME_TAG.TECH_LEVEL_MANA_GEM) ?? false;
	}

	public bool UseCoinManaGem()
	{
		if (GameState.Get() != null && GameState.Get().GetGameEntity() != null)
		{
			return GameState.Get().GetGameEntity().HasTag(GAME_TAG.COIN_MANA_GEM);
		}
		return false;
	}

	public bool UseCoinManaGemForChoiceCard()
	{
		if (GameState.Get() != null && GameState.Get().GetGameEntity() != null)
		{
			return GameState.Get().GetGameEntity().HasTag(GAME_TAG.COIN_MANA_GEM_FOR_CHOICE_CARDS);
		}
		return false;
	}

	public HistoryCard GetHistoryCard()
	{
		if (base.transform.parent == null)
		{
			return null;
		}
		return base.transform.parent.gameObject.GetComponent<HistoryCard>();
	}

	public HistoryChildCard GetHistoryChildCard()
	{
		if (base.transform.parent == null)
		{
			return null;
		}
		return base.transform.parent.gameObject.GetComponent<HistoryChildCard>();
	}

	public void SetHistoryItem(HistoryItem card)
	{
		if (card == null)
		{
			base.transform.parent = null;
			return;
		}
		base.transform.parent = card.transform;
		TransformUtil.Identity(base.transform);
		if (m_rootObject != null)
		{
			TransformUtil.Identity(m_rootObject.transform);
		}
		m_entity = card.GetEntity();
		UpdateTextComponents(m_entity);
		UpdateMeshComponents();
		if (m_premiumType >= TAG_PREMIUM.GOLDEN && card.GetPortraitGoldenMaterial() != null && IsPremiumPortraitEnabled())
		{
			Material portraitGoldenMaterial = card.GetPortraitGoldenMaterial();
			SetPortraitMaterial(portraitGoldenMaterial);
		}
		else
		{
			Texture portraitTexture = card.GetPortraitTexture();
			SetPortraitTextureOverride(portraitTexture);
		}
		if (!(m_spellTable != null))
		{
			return;
		}
		foreach (SpellTableEntry item in m_spellTable.m_Table)
		{
			Spell spell = item.m_Spell;
			if (!(spell == null))
			{
				spell.m_BlockServerEvents = false;
			}
		}
	}

	public SpellTable GetSpellTable()
	{
		return m_spellTable;
	}

	public Spell LoadSpell(SpellType spellType)
	{
		Spell spell = null;
		if (m_card != null)
		{
			spell = m_card.GetSpellTableOverride(spellType);
		}
		if (spell == null)
		{
			TAG_CARD_SET cardSet = GetCardSet();
			string cardSetSpellOverride = GameDbf.GetIndex().GetCardSetSpellOverride(cardSet, spellType);
			if (!string.IsNullOrEmpty(cardSetSpellOverride))
			{
				spell = SpellTable.InstantiateSpell(spellType, cardSetSpellOverride);
			}
		}
		if (spell == null && m_sharedSpellTable != null)
		{
			spell = m_sharedSpellTable.GetSpell(spellType);
		}
		if (spell == null)
		{
			return null;
		}
		if (m_localSpellTable.ContainsKey(spellType))
		{
			m_localSpellTable.Remove(spellType);
		}
		m_localSpellTable.Add(spellType, spell);
		Transform obj = spell.gameObject.transform;
		Transform parent = base.gameObject.transform;
		TransformUtil.AttachAndPreserveLocalTransform(obj, parent);
		obj.localScale.Scale(m_sharedSpellTable.gameObject.transform.localScale);
		SceneUtils.SetLayer(spell.gameObject, (GameLayer)base.gameObject.layer);
		spell.OnLoad();
		if (m_actorStateMgr != null)
		{
			spell.AddStateStartedCallback(OnSpellStateStarted);
		}
		return spell;
	}

	public Spell GetLoadedSpell(SpellType spellType)
	{
		Spell value = null;
		if (m_localSpellTable != null)
		{
			m_localSpellTable.TryGetValue(spellType, out value);
		}
		if (value == null)
		{
			value = LoadSpell(spellType);
		}
		return value;
	}

	public Spell ActivateTaunt()
	{
		DeactivateTaunt();
		if (GetEntity().IsStealthed() && !Options.Get().GetBool(Option.HAS_SEEN_STEALTH_TAUNTER, defaultVal: false))
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_STEALTH_TAUNT3_22"), "VO_INNKEEPER_STEALTH_TAUNT3_22.prefab:7ec7cc35d1556434ebca64bfe4e770cb");
			Options.Get().SetBool(Option.HAS_SEEN_STEALTH_TAUNTER, val: true);
		}
		if (m_premiumType == TAG_PREMIUM.GOLDEN)
		{
			if (GetEntity().IsStealthed() || GetEntity().IsTauntIgnored())
			{
				return ActivateSpellBirthState(SpellType.TAUNT_PREMIUM_STEALTH);
			}
			return ActivateSpellBirthState(SpellType.TAUNT_PREMIUM);
		}
		if (m_premiumType == TAG_PREMIUM.DIAMOND)
		{
			if (GetEntity().IsStealthed() || GetEntity().IsTauntIgnored())
			{
				return ActivateSpellBirthState(SpellType.TAUNT_DIAMOND_STEALTH);
			}
			return ActivateSpellBirthState(SpellType.TAUNT_DIAMOND);
		}
		if (GetEntity().IsStealthed() || GetEntity().IsTauntIgnored())
		{
			return ActivateSpellBirthState(SpellType.TAUNT_STEALTH);
		}
		return ActivateSpellBirthState(SpellType.TAUNT);
	}

	public void DeactivateTaunt()
	{
		if (IsSpellActive(SpellType.TAUNT))
		{
			ActivateSpellDeathState(SpellType.TAUNT);
		}
		if (IsSpellActive(SpellType.TAUNT_PREMIUM))
		{
			ActivateSpellDeathState(SpellType.TAUNT_PREMIUM);
		}
		if (IsSpellActive(SpellType.TAUNT_PREMIUM_STEALTH))
		{
			ActivateSpellDeathState(SpellType.TAUNT_PREMIUM_STEALTH);
		}
		if (IsSpellActive(SpellType.TAUNT_STEALTH))
		{
			ActivateSpellDeathState(SpellType.TAUNT_STEALTH);
		}
		if (IsSpellActive(SpellType.TAUNT_DIAMOND))
		{
			ActivateSpellDeathState(SpellType.TAUNT_DIAMOND);
		}
		if (IsSpellActive(SpellType.TAUNT_DIAMOND_STEALTH))
		{
			ActivateSpellDeathState(SpellType.TAUNT_DIAMOND_STEALTH);
		}
	}

	public Spell GetSpell(SpellType spellType)
	{
		Spell spell = null;
		if (m_useSharedSpellTable)
		{
			spell = GetLoadedSpell(spellType);
		}
		else if (m_spellTable != null)
		{
			m_spellTable.FindSpell(spellType, out spell);
		}
		return spell;
	}

	public Spell GetSpellIfLoaded(SpellType spellType)
	{
		Spell result = null;
		if (m_useSharedSpellTable)
		{
			GetSpellIfLoaded(spellType, out result);
		}
		else if (m_spellTable != null)
		{
			m_spellTable.FindSpell(spellType, out result);
		}
		return result;
	}

	public bool GetSpellIfLoaded(SpellType spellType, out Spell result)
	{
		if (m_localSpellTable == null || !m_localSpellTable.ContainsKey(spellType))
		{
			result = null;
			return false;
		}
		result = m_localSpellTable[spellType];
		return result != null;
	}

	public Spell ActivateSpellBirthState(SpellType spellType)
	{
		Spell spell = GetSpell(spellType);
		if (spell == null)
		{
			return null;
		}
		spell.ActivateState(SpellStateType.BIRTH);
		return spell;
	}

	public bool IsSpellActive(SpellType spellType)
	{
		Spell spellIfLoaded = GetSpellIfLoaded(spellType);
		if (spellIfLoaded == null)
		{
			return false;
		}
		return spellIfLoaded.IsActive();
	}

	public void ActivateSpellDeathState(SpellType spellType)
	{
		Spell spellIfLoaded = GetSpellIfLoaded(spellType);
		if (!(spellIfLoaded == null))
		{
			spellIfLoaded.ActivateState(SpellStateType.DEATH);
		}
	}

	public void ActivateSpellCancelState(SpellType spellType)
	{
		Spell spellIfLoaded = GetSpellIfLoaded(spellType);
		if (!(spellIfLoaded == null))
		{
			spellIfLoaded.ActivateState(SpellStateType.CANCEL);
		}
	}

	public void ActivateAllSpellsDeathStates()
	{
		if (m_useSharedSpellTable)
		{
			foreach (Spell value in m_localSpellTable.Values)
			{
				if (value.IsActive())
				{
					value.ActivateState(SpellStateType.DEATH);
				}
			}
		}
		else
		{
			if (!(m_spellTable != null))
			{
				return;
			}
			foreach (SpellTableEntry item in m_spellTable.m_Table)
			{
				Spell spell = item.m_Spell;
				if (!(spell == null) && spell.IsActive())
				{
					spell.ActivateState(SpellStateType.DEATH);
				}
			}
		}
	}

	public void DoCardDeathVisuals()
	{
		foreach (SpellType value in Enum.GetValues(typeof(SpellType)))
		{
			if (IsSpellActive(value) && value != SpellType.GHOSTLY_DEATH && value != SpellType.DEATH && value != SpellType.DEATHRATTLE_DEATH && value != SpellType.REBORN_DEATH && value != SpellType.DAMAGE)
			{
				ActivateSpellDeathState(value);
			}
		}
	}

	public void DeactivateAllSpells()
	{
		if (m_useSharedSpellTable)
		{
			foreach (SpellType item in new List<SpellType>(m_localSpellTable.Keys))
			{
				Spell spell = m_localSpellTable[item];
				if (spell != null)
				{
					spell.Deactivate();
				}
			}
		}
		else
		{
			if (!(m_spellTable != null))
			{
				return;
			}
			foreach (SpellTableEntry item2 in m_spellTable.m_Table)
			{
				Spell spell2 = item2.m_Spell;
				if (!(spell2 == null))
				{
					spell2.Deactivate();
				}
			}
		}
	}

	public void DestroySpell(SpellType spellType)
	{
		if (m_useSharedSpellTable)
		{
			if (m_localSpellTable.TryGetValue(spellType, out var value))
			{
				UnityEngine.Object.Destroy(value.gameObject);
				m_localSpellTable.Remove(spellType);
			}
		}
		else
		{
			Debug.LogError($"Actor.DestroySpell() - FAILED to destroy {spellType} because the Actor is not using a shared spell table.");
		}
	}

	public void DisableArmorSpellForTransition()
	{
		m_armorSpellDisabledForTransition = true;
	}

	public void EnableArmorSpellAfterTransition()
	{
		m_armorSpellDisabledForTransition = false;
	}

	public void HideArmorSpell()
	{
		if (m_armorSpell != null)
		{
			m_armorSpell.SetArmor(0);
			m_armorSpell.Deactivate();
			m_armorSpell.gameObject.SetActive(value: false);
		}
	}

	public void ShowArmorSpell()
	{
		if (m_armorSpell != null && !m_armorSpellDisabledForTransition)
		{
			m_armorSpell.gameObject.SetActive(value: true);
			UpdateArmorSpell();
		}
	}

	private void UpdateRootObjectSpellComponents()
	{
		if (m_entity != null)
		{
			if (m_armorSpellLoading)
			{
				StartCoroutine(UpdateArmorSpellWhenLoaded());
			}
			if (m_armorSpell != null)
			{
				UpdateArmorSpell();
			}
		}
	}

	private IEnumerator UpdateArmorSpellWhenLoaded()
	{
		while (m_armorSpellLoading)
		{
			yield return null;
		}
		UpdateArmorSpell();
	}

	private void UpdateArmorSpell()
	{
		if (!m_armorSpell.gameObject.activeInHierarchy || m_entity == null)
		{
			return;
		}
		int armor = m_entity.GetArmor();
		int armor2 = m_armorSpell.GetArmor();
		m_armorSpell.SetArmor(armor);
		if (armor > 0)
		{
			bool flag = m_armorSpell.IsShown();
			if (!flag)
			{
				m_armorSpell.Show();
			}
			if (armor2 <= 0)
			{
				StartCoroutine(ActivateArmorSpell(SpellStateType.BIRTH, armorShouldBeOn: true));
			}
			else if (armor2 > armor)
			{
				StartCoroutine(ActivateArmorSpell(SpellStateType.ACTION, armorShouldBeOn: true));
			}
			else if (armor2 < armor)
			{
				StartCoroutine(ActivateArmorSpell(SpellStateType.CANCEL, armorShouldBeOn: true));
			}
			else if (!flag)
			{
				StartCoroutine(ActivateArmorSpell(SpellStateType.IDLE, armorShouldBeOn: true));
			}
		}
		else if (armor2 > 0)
		{
			StartCoroutine(ActivateArmorSpell(SpellStateType.DEATH, armorShouldBeOn: false));
		}
	}

	private IEnumerator ActivateArmorSpell(SpellStateType stateType, bool armorShouldBeOn)
	{
		while (m_armorSpell.GetActiveState() != 0)
		{
			yield return null;
		}
		if (m_armorSpell.GetActiveState() != stateType)
		{
			int armor = m_entity.GetArmor();
			if ((!armorShouldBeOn || armor > 0) && (armorShouldBeOn || armor <= 0))
			{
				m_armorSpell.ActivateState(stateType);
			}
		}
	}

	private void OnSpellStateStarted(Spell spell, SpellStateType prevStateType, object userData)
	{
		spell.AddStateStartedCallback(OnSpellStateStarted);
		m_actorStateMgr.RefreshStateMgr();
		if ((bool)m_projectedShadow)
		{
			m_projectedShadow.UpdateContactShadow();
		}
	}

	private void AssignRootObject()
	{
		m_rootObject = SceneUtils.FindChildBySubstring(base.gameObject, "RootObject");
	}

	private void AssignBones()
	{
		m_bones = SceneUtils.FindChildBySubstring(base.gameObject, "Bones");
	}

	private void AssignMeshRenderers()
	{
		MeshRenderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<MeshRenderer>(includeInactive: true);
		foreach (MeshRenderer meshRenderer in componentsInChildren)
		{
			if (meshRenderer.gameObject.name.Equals("Mesh", StringComparison.OrdinalIgnoreCase))
			{
				m_meshRenderer = meshRenderer;
				MeshRenderer[] componentsInChildren2 = meshRenderer.gameObject.GetComponentsInChildren<MeshRenderer>(includeInactive: true);
				foreach (MeshRenderer meshRenderer2 in componentsInChildren2)
				{
					AssignMaterials(meshRenderer2);
				}
				break;
			}
		}
		if (m_portraitMesh != null)
		{
			m_meshRendererPortrait = m_portraitMesh.GetComponent<MeshRenderer>();
		}
	}

	private void AssignMaterials(MeshRenderer meshRenderer)
	{
		List<Material> sharedMaterials = meshRenderer.GetSharedMaterials();
		for (int i = 0; i < sharedMaterials.Count; i++)
		{
			Material material = sharedMaterials[i];
			if (!(material == null))
			{
				if (material.name.LastIndexOf("Portrait", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					m_legacyPortraitMaterialIndex = i;
				}
				else if (material.name.IndexOf("Card_Inhand_Ability_Warlock", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					m_legacyCardColorMaterialIndex = i;
				}
				else if (material.name.IndexOf("Card_Inhand_Warlock", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					m_legacyCardColorMaterialIndex = i;
				}
				else if (material.name.IndexOf("Card_Inhand_Weapon_Warlock", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					m_legacyCardColorMaterialIndex = i;
				}
			}
		}
	}

	public void ShowSideQuestProgressBanner()
	{
		ResetBanner();
		if (m_entity != null && !(m_banner == null) && !(m_bannerBottom == null) && !(m_bannerText == null))
		{
			m_banner.SetActive(value: true);
			m_bannerBottom.SetActive(value: true);
			m_bannerText.gameObject.SetActive(value: true);
			m_bannerText.Text = GameStrings.Format("GLUE_SIDEQUEST_PROGRESS_BANNER", m_entity.GetTag(GAME_TAG.QUEST_PROGRESS), m_entity.GetTag(GAME_TAG.QUEST_PROGRESS_TOTAL));
		}
	}

	public void HideSideQuestProgressBanner()
	{
		if (!(m_banner == null) && !(m_bannerBottom == null) && !(m_bannerText == null))
		{
			m_banner.SetActive(value: false);
			m_bannerBottom.SetActive(value: false);
			m_bannerText.gameObject.SetActive(value: false);
		}
	}

	private void AssignSpells()
	{
		m_spellTable = base.gameObject.GetComponentInChildren<SpellTable>();
		m_actorStateMgr = base.gameObject.GetComponentInChildren<ActorStateMgr>();
		if (m_spellTable == null)
		{
			if (string.IsNullOrEmpty(m_spellTablePrefab))
			{
				return;
			}
			SpellCache spellCache = SpellCache.Get();
			if (spellCache != null)
			{
				SpellTable spellTable = spellCache.GetSpellTable(m_spellTablePrefab);
				if (spellTable != null)
				{
					m_useSharedSpellTable = true;
					m_sharedSpellTable = spellTable;
					m_localSpellTable = new Map<SpellType, Spell>();
				}
				else
				{
					Debug.LogWarning("failed to load spell table: " + m_spellTablePrefab);
				}
			}
			else
			{
				Debug.LogWarning("Null spell cache: " + m_spellTablePrefab);
			}
		}
		else
		{
			if (!(m_actorStateMgr != null))
			{
				return;
			}
			foreach (SpellTableEntry item in m_spellTable.m_Table)
			{
				if (!(item.m_Spell == null))
				{
					item.m_Spell.AddStateStartedCallback(OnSpellStateStarted);
				}
			}
		}
	}

	private void SetUpBanner()
	{
		if (!(m_banner == null) && !(m_bannerBottom == null) && !(m_bannerText == null))
		{
			ResetBanner();
		}
	}

	private void ResetBanner()
	{
		if (!(m_banner == null) && !(m_bannerBottom == null) && !(m_bannerText == null))
		{
			m_banner.SetActive(value: false);
			m_bannerBottom.SetActive(value: false);
			m_bannerText.gameObject.SetActive(value: false);
			m_banner.transform.parent = base.transform;
			m_bannerBottom.transform.parent = base.transform;
			m_bannerText.transform.parent = base.transform;
		}
	}

	private void CacheShadowObjects()
	{
		m_shadowObject = SceneUtils.FindChildByTag(base.gameObject, "FakeShadow", includeInactive: true);
		m_uniqueShadowObject = SceneUtils.FindChildByTag(base.gameObject, "FakeShadowUnique", includeInactive: true);
		Renderer renderer = ((m_uniqueShadowObject != null) ? m_uniqueShadowObject.GetComponent<Renderer>() : null);
		Renderer renderer2 = ((m_shadowObject != null) ? m_shadowObject.GetComponent<Renderer>() : null);
		if (renderer != null)
		{
			m_initialUniqueShadowRenderQueue = renderer.GetMaterial().renderQueue;
		}
		if (renderer2 != null)
		{
			m_initialShadowRenderQueue = renderer2.GetMaterial().renderQueue;
		}
		m_shadowObjectInitialized = true;
	}

	private void LoadArmorSpell()
	{
		if (!(m_armorSpellBone == null))
		{
			m_armorSpellLoading = true;
			string text = "Hero_Armor.prefab:e4d519d1080fe4656967bf5140ca3587";
			CardDef cardDef = m_cardDefHandle.Get();
			if (cardDef != null && !string.IsNullOrEmpty(cardDef.m_CustomHeroArmorSpell))
			{
				text = cardDef.m_CustomHeroArmorSpell;
			}
			AssetLoader.Get().InstantiatePrefab(text, OnArmorSpellLoaded);
		}
	}

	private void OnArmorSpellLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"{assetRef} - Actor.OnArmorSpellLoaded() - failed to load Hero_Armor spell! m_armorSpell GameObject = null!");
			return;
		}
		m_armorSpellLoading = false;
		m_armorSpell = go.GetComponent<ArmorSpell>();
		if (m_armorSpell == null)
		{
			Debug.LogError($"{assetRef} - Actor.OnArmorSpellLoaded() - failed to load Hero_Armor spell! m_armorSpell Spell = null!");
			return;
		}
		go.transform.parent = m_armorSpellBone.transform;
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		go.transform.localScale = Vector3.one;
	}

	public bool HasSameCardDef(CardDef cardDef)
	{
		return m_cardDefHandle.Get() == cardDef;
	}
}
