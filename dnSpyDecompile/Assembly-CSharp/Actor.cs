using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.AssetManager;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

// Token: 0x02000845 RID: 2117
[CustomEditClass]
public class Actor : MonoBehaviour, IVisibleWidgetComponent
{
	// Token: 0x06007175 RID: 29045 RVA: 0x002487D0 File Offset: 0x002469D0
	public virtual void Awake()
	{
		this.AssignRootObject();
		this.AssignBones();
		this.AssignMeshRenderers();
		this.AssignSpells();
		this.SetUpBanner();
	}

	// Token: 0x06007176 RID: 29046 RVA: 0x002487F0 File Offset: 0x002469F0
	private void OnEnable()
	{
		if (this.isPortraitMaterialDirty)
		{
			this.UpdateAllComponents();
		}
	}

	// Token: 0x06007177 RID: 29047 RVA: 0x00248800 File Offset: 0x00246A00
	private void Start()
	{
		this.Init();
	}

	// Token: 0x06007178 RID: 29048 RVA: 0x00248808 File Offset: 0x00246A08
	private void OnDestroy()
	{
		if (CardBackManager.Get() != null)
		{
			CardBackManager.Get().UnregisterUpdateCardbacksListener(new CardBackManager.UpdateCardbacksCallback(this.UpdateCardBack));
		}
		this.ReleaseCardDef();
		if (this.m_diamondPortraitR2T)
		{
			UnityEngine.Object.Destroy(this.m_diamondPortraitR2T);
		}
		this.DestroyCreatedMaterials();
		AssetHandle.SafeDispose<Texture>(ref this.m_watermarkTex);
		AssetHandle.SafeDispose<Texture>(ref this.m_cardColorTex);
	}

	// Token: 0x06007179 RID: 29049 RVA: 0x00248870 File Offset: 0x00246A70
	public void Init()
	{
		if (CardBackManager.Get() != null)
		{
			CardBackManager.Get().RegisterUpdateCardbacksListener(new CardBackManager.UpdateCardbacksCallback(this.UpdateCardBack));
		}
		if (this.m_portraitMesh != null && this.m_portraitMatIdx >= 0)
		{
			this.m_initialPortraitMaterial = this.m_portraitMesh.GetComponent<Renderer>().GetSharedMaterial(this.m_portraitMatIdx);
		}
		else if (this.m_legacyPortraitMaterialIndex >= 0)
		{
			this.m_initialPortraitMaterial = this.m_meshRenderer.GetSharedMaterial(this.m_legacyPortraitMaterialIndex);
		}
		if (this.m_premiumRibbon > -1)
		{
			this.m_initialPremiumRibbonMaterial = this.m_cardMesh.GetComponent<Renderer>().GetMaterial(this.m_premiumRibbon);
		}
		if (this.m_rootObject != null)
		{
			TransformUtil.Identity(this.m_rootObject.transform);
		}
		if (this.m_actorStateMgr != null)
		{
			this.m_actorStateMgr.ChangeState(this.m_actorState);
		}
		this.m_projectedShadow = base.GetComponent<ProjectedShadow>();
		if (this.m_shown)
		{
			this.ShowImpl(false);
			return;
		}
		this.HideImpl(false);
	}

	// Token: 0x0600717A RID: 29050 RVA: 0x00248978 File Offset: 0x00246B78
	public void Destroy()
	{
		if (!base.gameObject)
		{
			return;
		}
		if (this.m_localSpellTable != null)
		{
			Spell[] array = this.m_localSpellTable.Values.ToArray<Spell>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Deactivate();
			}
		}
		if (this.m_spellTable != null)
		{
			for (int j = 0; j < this.m_spellTable.m_Table.Count; j++)
			{
				if (!(this.m_spellTable.m_Table[j].m_Spell == null))
				{
					this.m_spellTable.m_Table[j].m_Spell.Deactivate();
				}
			}
		}
		this.ReleaseCardDef();
		this.DestroyCreatedMaterials();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600717B RID: 29051 RVA: 0x00248A3B File Offset: 0x00246C3B
	private void DestroyCreatedMaterials()
	{
		if (this.m_initialPremiumRibbonMaterial != null)
		{
			UnityEngine.Object.Destroy(this.m_initialPremiumRibbonMaterial);
			this.m_initialPremiumRibbonMaterial = null;
		}
	}

	// Token: 0x0600717C RID: 29052 RVA: 0x00248A60 File Offset: 0x00246C60
	public virtual Actor Clone()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(base.gameObject, base.transform.position, base.transform.rotation);
		Actor component = gameObject.GetComponent<Actor>();
		component.SetEntity(this.m_entity);
		component.SetEntityDef(this.m_entityDef);
		component.SetCard(this.m_card);
		component.SetPremium(this.m_premiumType);
		component.SetWatermarkCardSetOverride(this.m_watermarkCardSetOverride);
		gameObject.transform.localScale = base.gameObject.transform.localScale;
		gameObject.transform.position = base.gameObject.transform.position;
		component.SetActorState(this.m_actorState);
		if (this.m_shown)
		{
			component.ShowImpl(false);
		}
		else
		{
			component.HideImpl(false);
		}
		return component;
	}

	// Token: 0x0600717D RID: 29053 RVA: 0x00248B2B File Offset: 0x00246D2B
	public global::Card GetCard()
	{
		return this.m_card;
	}

	// Token: 0x0600717E RID: 29054 RVA: 0x00248B34 File Offset: 0x00246D34
	public void SetCard(global::Card card)
	{
		if (this.m_card == card)
		{
			return;
		}
		if (card == null)
		{
			this.m_card = null;
			base.transform.parent = null;
			return;
		}
		this.m_card = card;
		base.transform.parent = card.transform;
		TransformUtil.Identity(base.transform);
		if (this.m_rootObject != null)
		{
			TransformUtil.Identity(this.m_rootObject.transform);
		}
	}

	// Token: 0x0600717F RID: 29055 RVA: 0x00248BAE File Offset: 0x00246DAE
	public void SetFullDefFromEntity(Entity entity)
	{
		if (entity != null)
		{
			this.SetEntityDef(entity.GetEntityDef());
			this.SetCardDefFromEntity(entity);
		}
	}

	// Token: 0x06007180 RID: 29056 RVA: 0x00248BC6 File Offset: 0x00246DC6
	public void SetFullDefFromActor(Actor other)
	{
		if (other != null)
		{
			this.SetCardDefFromActor(other);
			this.SetEntityDef(other.m_entityDef);
		}
	}

	// Token: 0x06007181 RID: 29057 RVA: 0x00248BE4 File Offset: 0x00246DE4
	public void SetFullDef(DefLoader.DisposableFullDef fullDef)
	{
		this.SetCardDef(fullDef.DisposableCardDef);
		this.SetEntityDef(fullDef.EntityDef);
	}

	// Token: 0x06007182 RID: 29058 RVA: 0x00248BFE File Offset: 0x00246DFE
	public DefLoader.DisposableCardDef ShareDisposableCardDef()
	{
		return this.m_cardDefHandle.Share();
	}

	// Token: 0x06007183 RID: 29059 RVA: 0x00248C0C File Offset: 0x00246E0C
	public void SetCardDefFromEntity(Entity entity)
	{
		if (entity != null)
		{
			using (DefLoader.DisposableCardDef disposableCardDef = entity.ShareDisposableCardDef())
			{
				this.SetCardDef(disposableCardDef);
			}
		}
	}

	// Token: 0x06007184 RID: 29060 RVA: 0x00248C48 File Offset: 0x00246E48
	public void SetCardDefFromActor(Actor other)
	{
		if (other != null)
		{
			this.m_cardDefHandle.Set(other.m_cardDefHandle);
		}
	}

	// Token: 0x06007185 RID: 29061 RVA: 0x00248C64 File Offset: 0x00246E64
	public void SetCardDefFromCard(global::Card card)
	{
		if (card != null)
		{
			using (DefLoader.DisposableCardDef disposableCardDef = card.ShareDisposableCardDef())
			{
				this.m_cardDefHandle.SetCardDef(disposableCardDef);
			}
		}
	}

	// Token: 0x06007186 RID: 29062 RVA: 0x00248CAC File Offset: 0x00246EAC
	public void SetCardDef(DefLoader.DisposableCardDef cardDef)
	{
		if (this.m_cardDefHandle.SetCardDef(cardDef))
		{
			this.LoadArmorSpell();
		}
	}

	// Token: 0x06007187 RID: 29063 RVA: 0x00248CC2 File Offset: 0x00246EC2
	public void ReleaseCardDef()
	{
		this.m_cardDefHandle.ReleaseCardDef();
	}

	// Token: 0x06007188 RID: 29064 RVA: 0x00248CCF File Offset: 0x00246ECF
	public void SetIgnoreHideStats(bool ignore)
	{
		this.m_ignoreHideStats = ignore;
	}

	// Token: 0x06007189 RID: 29065 RVA: 0x00248CD8 File Offset: 0x00246ED8
	private bool HasHideStats(EntityBase entity)
	{
		return !this.m_ignoreHideStats && (entity.HasTag(GAME_TAG.HIDE_STATS) || entity.IsDormant());
	}

	// Token: 0x0600718A RID: 29066 RVA: 0x00248CF9 File Offset: 0x00246EF9
	public void SetWatermarkCardSetOverride(TAG_CARD_SET cardSetOverride)
	{
		if (!Enum.IsDefined(typeof(TAG_CARD_SET), cardSetOverride))
		{
			this.m_watermarkCardSetOverride = TAG_CARD_SET.INVALID;
			return;
		}
		this.m_watermarkCardSetOverride = cardSetOverride;
	}

	// Token: 0x0600718B RID: 29067 RVA: 0x00248D21 File Offset: 0x00246F21
	public Entity GetEntity()
	{
		return this.m_entity;
	}

	// Token: 0x0600718C RID: 29068 RVA: 0x00248D29 File Offset: 0x00246F29
	public void SetEntity(Entity entity)
	{
		this.m_entity = entity;
		if (this.m_entity == null)
		{
			return;
		}
		this.SetPremium(this.m_entity.GetPremiumType());
		this.SetWatermarkCardSetOverride(this.m_entity.GetWatermarkCardSetOverride());
	}

	// Token: 0x0600718D RID: 29069 RVA: 0x00248D5D File Offset: 0x00246F5D
	public EntityDef GetEntityDef()
	{
		return this.m_entityDef;
	}

	// Token: 0x0600718E RID: 29070 RVA: 0x00248D65 File Offset: 0x00246F65
	public void SetEntityDef(EntityDef entityDef)
	{
		this.m_entityDef = entityDef;
		if (this.m_entityDef == null)
		{
			return;
		}
		this.m_cardDefHandle.SetCardId(this.m_entityDef.GetCardId());
	}

	// Token: 0x0600718F RID: 29071 RVA: 0x00248D8D File Offset: 0x00246F8D
	public virtual void SetPremium(TAG_PREMIUM premium)
	{
		this.m_premiumType = premium;
	}

	// Token: 0x06007190 RID: 29072 RVA: 0x00248D96 File Offset: 0x00246F96
	public TAG_PREMIUM GetPremium()
	{
		return this.m_premiumType;
	}

	// Token: 0x06007191 RID: 29073 RVA: 0x00248DA0 File Offset: 0x00246FA0
	public TAG_CARD_SET GetCardSet()
	{
		if (this.m_entityDef == null && this.m_entity == null)
		{
			return TAG_CARD_SET.NONE;
		}
		TAG_CARD_SET cardSet;
		if (this.m_entityDef != null)
		{
			cardSet = this.m_entityDef.GetCardSet();
		}
		else
		{
			cardSet = this.m_entity.GetCardSet();
		}
		return cardSet;
	}

	// Token: 0x06007192 RID: 29074 RVA: 0x00248DE2 File Offset: 0x00246FE2
	public ActorStateType GetActorStateType()
	{
		if (!(this.m_actorStateMgr == null))
		{
			return this.m_actorStateMgr.GetActiveStateType();
		}
		return ActorStateType.NONE;
	}

	// Token: 0x06007193 RID: 29075 RVA: 0x00248DFF File Offset: 0x00246FFF
	public void SetActorState(ActorStateType stateType)
	{
		this.m_actorState = stateType;
		if (this.m_actorStateMgr == null)
		{
			return;
		}
		if (this.forceIdleState)
		{
			this.m_actorState = ActorStateType.CARD_IDLE;
		}
		this.m_actorStateMgr.ChangeState(this.m_actorState);
	}

	// Token: 0x06007194 RID: 29076 RVA: 0x00248E38 File Offset: 0x00247038
	public void ToggleForceIdle(bool bOn)
	{
		this.forceIdleState = bOn;
	}

	// Token: 0x06007195 RID: 29077 RVA: 0x00248E41 File Offset: 0x00247041
	public void TurnOffCollider()
	{
		this.ToggleCollider(false);
	}

	// Token: 0x06007196 RID: 29078 RVA: 0x00248E4A File Offset: 0x0024704A
	public void TurnOnCollider()
	{
		this.ToggleCollider(true);
	}

	// Token: 0x06007197 RID: 29079 RVA: 0x00248E54 File Offset: 0x00247054
	public void ToggleCollider(bool enabled)
	{
		MeshRenderer meshRenderer = this.GetMeshRenderer(false);
		if (meshRenderer == null || meshRenderer.gameObject.GetComponent<Collider>() == null)
		{
			return;
		}
		meshRenderer.gameObject.GetComponent<Collider>().enabled = enabled;
	}

	// Token: 0x06007198 RID: 29080 RVA: 0x00248E98 File Offset: 0x00247098
	public bool IsColliderEnabled()
	{
		MeshRenderer meshRenderer = this.GetMeshRenderer(false);
		return !(meshRenderer == null) && !(meshRenderer.gameObject.GetComponent<Collider>() == null) && meshRenderer.gameObject.GetComponent<Collider>().enabled;
	}

	// Token: 0x06007199 RID: 29081 RVA: 0x00248EDB File Offset: 0x002470DB
	public TAG_RARITY GetRarity()
	{
		if (this.m_entityDef != null)
		{
			return this.m_entityDef.GetRarity();
		}
		if (this.m_entity != null)
		{
			return this.m_entity.GetRarity();
		}
		return TAG_RARITY.FREE;
	}

	// Token: 0x0600719A RID: 29082 RVA: 0x00248F06 File Offset: 0x00247106
	public bool IsElite()
	{
		if (this.m_entityDef != null)
		{
			return this.m_entityDef.IsElite();
		}
		return this.m_entity != null && this.m_entity.IsElite();
	}

	// Token: 0x0600719B RID: 29083 RVA: 0x00248F31 File Offset: 0x00247131
	public bool IsMultiClass()
	{
		if (this.m_entityDef != null)
		{
			return this.m_entityDef.IsMultiClass();
		}
		return this.m_entity != null && this.m_entity.IsMultiClass();
	}

	// Token: 0x0600719C RID: 29084 RVA: 0x00248F5C File Offset: 0x0024715C
	public void SetHiddenStandIn(GameObject standIn)
	{
		this.m_hiddenCardStandIn = standIn;
	}

	// Token: 0x0600719D RID: 29085 RVA: 0x00248F65 File Offset: 0x00247165
	public GameObject GetHiddenStandIn()
	{
		return this.m_hiddenCardStandIn;
	}

	// Token: 0x0600719E RID: 29086 RVA: 0x00248F6D File Offset: 0x0024716D
	public void SetShadowform(bool shadowform)
	{
		this.m_shadowform = shadowform;
	}

	// Token: 0x0600719F RID: 29087 RVA: 0x00248F76 File Offset: 0x00247176
	public UberShaderController GetUberShaderController()
	{
		if (this.m_uberShaderController == null)
		{
			this.m_uberShaderController = this.m_portraitMesh.GetComponent<UberShaderController>();
		}
		return this.m_uberShaderController;
	}

	// Token: 0x060071A0 RID: 29088 RVA: 0x00248F9D File Offset: 0x0024719D
	public bool UsesMultiClassBanner()
	{
		return this.m_usesMultiClassBanner;
	}

	// Token: 0x170006B6 RID: 1718
	// (get) Token: 0x060071A1 RID: 29089 RVA: 0x00248FA5 File Offset: 0x002471A5
	// (set) Token: 0x060071A2 RID: 29090 RVA: 0x00248FAD File Offset: 0x002471AD
	public bool IsDesiredHidden { get; private set; }

	// Token: 0x170006B7 RID: 1719
	// (get) Token: 0x060071A3 RID: 29091 RVA: 0x00248FB8 File Offset: 0x002471B8
	public bool IsDesiredHiddenInHierarchy
	{
		get
		{
			if (this.IsDesiredHidden)
			{
				return true;
			}
			WidgetTemplate componentInParent = base.GetComponentInParent<WidgetTemplate>();
			return componentInParent != null && componentInParent.IsDesiredHiddenInHierarchy;
		}
	}

	// Token: 0x170006B8 RID: 1720
	// (get) Token: 0x060071A4 RID: 29092 RVA: 0x000052EC File Offset: 0x000034EC
	public bool HandlesChildVisibility
	{
		get
		{
			return true;
		}
	}

	// Token: 0x060071A5 RID: 29093 RVA: 0x00248FEA File Offset: 0x002471EA
	public void SetVisibility(bool isVisible, bool isInternal)
	{
		this.SetVisibility(isVisible, false, isInternal);
	}

	// Token: 0x060071A6 RID: 29094 RVA: 0x00248FF5 File Offset: 0x002471F5
	protected void SetVisibility(bool isVisible, bool ignoreSpells, bool isInternal)
	{
		if (isVisible == this.m_shown)
		{
			return;
		}
		if (!isInternal)
		{
			this.IsDesiredHidden = !isVisible;
		}
		this.m_shown = isVisible;
		if (isVisible)
		{
			this.ShowImpl(ignoreSpells);
			return;
		}
		this.HideImpl(ignoreSpells);
	}

	// Token: 0x060071A7 RID: 29095 RVA: 0x00249027 File Offset: 0x00247227
	public bool IsShown()
	{
		return this.m_shown;
	}

	// Token: 0x060071A8 RID: 29096 RVA: 0x0024902F File Offset: 0x0024722F
	public void Show()
	{
		this.SetVisibility(true, false, false);
	}

	// Token: 0x060071A9 RID: 29097 RVA: 0x0024903A File Offset: 0x0024723A
	public void Show(bool ignoreSpells)
	{
		this.SetVisibility(true, ignoreSpells, false);
	}

	// Token: 0x060071AA RID: 29098 RVA: 0x00249048 File Offset: 0x00247248
	public void ShowSpellTable()
	{
		if (this.m_localSpellTable != null)
		{
			foreach (Spell spell in this.m_localSpellTable.Values)
			{
				spell.Show();
			}
		}
		if (this.m_spellTable != null)
		{
			this.m_spellTable.Show();
		}
	}

	// Token: 0x060071AB RID: 29099 RVA: 0x002490C0 File Offset: 0x002472C0
	public void Hide()
	{
		this.SetVisibility(false, false, false);
	}

	// Token: 0x060071AC RID: 29100 RVA: 0x002490CB File Offset: 0x002472CB
	public void Hide(bool ignoreSpells)
	{
		this.SetVisibility(false, ignoreSpells, false);
	}

	// Token: 0x060071AD RID: 29101 RVA: 0x002490D8 File Offset: 0x002472D8
	public void HideSpellTable()
	{
		if (this.m_localSpellTable != null)
		{
			foreach (Spell spell in this.m_localSpellTable.Values)
			{
				if (spell.GetSpellType() != SpellType.NONE)
				{
					spell.Hide();
				}
			}
		}
		if (this.m_spellTable != null)
		{
			this.m_spellTable.Hide();
		}
	}

	// Token: 0x060071AE RID: 29102 RVA: 0x00249158 File Offset: 0x00247358
	protected virtual void ShowImpl(bool ignoreSpells)
	{
		if (this.m_rootObject != null)
		{
			this.m_rootObject.SetActive(true);
		}
		if (this.m_diamondRenderToTexture)
		{
			this.m_diamondRenderToTexture.enabled = true;
		}
		this.ShowArmorSpell();
		this.ShowAllText();
		this.UpdateAllComponents();
		if (this.m_projectedShadow)
		{
			this.m_projectedShadow.enabled = true;
		}
		if (this.m_actorStateMgr != null)
		{
			this.m_actorStateMgr.ShowStateMgr();
		}
		if (!ignoreSpells)
		{
			this.ShowSpellTable();
		}
		if (this.m_ghostCardGameObject != null)
		{
			this.m_ghostCardGameObject.SetActive(true);
		}
		HighlightState componentInChildren = base.GetComponentInChildren<HighlightState>();
		if (componentInChildren)
		{
			componentInChildren.Show();
		}
	}

	// Token: 0x060071AF RID: 29103 RVA: 0x00249214 File Offset: 0x00247414
	protected virtual void HideImpl(bool ignoreSpells)
	{
		if (this.m_rootObject != null)
		{
			this.m_rootObject.SetActive(false);
		}
		this.UpdateContactShadow();
		this.HideArmorSpell();
		if (this.m_actorStateMgr != null)
		{
			this.m_actorStateMgr.HideStateMgr();
		}
		if (this.m_projectedShadow)
		{
			this.m_projectedShadow.enabled = false;
		}
		if (this.m_ghostCardGameObject != null)
		{
			this.m_ghostCardGameObject.SetActive(false);
		}
		if (!ignoreSpells)
		{
			this.HideSpellTable();
		}
		if (this.m_missingCardEffect != null)
		{
			this.UpdateMissingCardArt();
		}
		if (this.m_diamondRenderToTexture)
		{
			this.m_diamondRenderToTexture.enabled = false;
		}
		HighlightState componentInChildren = base.GetComponentInChildren<HighlightState>();
		if (componentInChildren)
		{
			componentInChildren.Hide();
		}
	}

	// Token: 0x060071B0 RID: 29104 RVA: 0x002492DE File Offset: 0x002474DE
	public ActorStateMgr GetActorStateMgr()
	{
		return this.m_actorStateMgr;
	}

	// Token: 0x060071B1 RID: 29105 RVA: 0x002492E6 File Offset: 0x002474E6
	public Collider GetCollider()
	{
		if (this.GetMeshRenderer(false) == null)
		{
			return null;
		}
		return this.GetMeshRenderer(false).gameObject.GetComponent<Collider>();
	}

	// Token: 0x060071B2 RID: 29106 RVA: 0x0024930A File Offset: 0x0024750A
	public GameObject GetRootObject()
	{
		return this.m_rootObject;
	}

	// Token: 0x060071B3 RID: 29107 RVA: 0x00249312 File Offset: 0x00247512
	public MeshRenderer GetMeshRenderer(bool getPortrait = false)
	{
		if (this.m_premiumType != TAG_PREMIUM.DIAMOND)
		{
			return this.m_meshRenderer;
		}
		if (getPortrait)
		{
			return this.m_meshRendererPortrait;
		}
		return this.m_meshRenderer;
	}

	// Token: 0x060071B4 RID: 29108 RVA: 0x00249334 File Offset: 0x00247534
	public GameObject GetBones()
	{
		return this.m_bones;
	}

	// Token: 0x060071B5 RID: 29109 RVA: 0x0024933C File Offset: 0x0024753C
	public UberText GetPowersText()
	{
		return this.m_powersTextMesh;
	}

	// Token: 0x060071B6 RID: 29110 RVA: 0x00249344 File Offset: 0x00247544
	public UberText GetRaceText()
	{
		return this.m_raceTextMesh;
	}

	// Token: 0x060071B7 RID: 29111 RVA: 0x0024934C File Offset: 0x0024754C
	public UberText GetNameText()
	{
		return this.m_nameTextMesh;
	}

	// Token: 0x060071B8 RID: 29112 RVA: 0x00249354 File Offset: 0x00247554
	public Light GetHeroSpotlight()
	{
		if (this.m_heroSpotLight == null)
		{
			return null;
		}
		return this.m_heroSpotLight.GetComponent<Light>();
	}

	// Token: 0x060071B9 RID: 29113 RVA: 0x00249371 File Offset: 0x00247571
	public GameObject FindBone(string boneName)
	{
		if (this.m_bones == null)
		{
			return null;
		}
		return SceneUtils.FindChildBySubstring(this.m_bones, boneName);
	}

	// Token: 0x060071BA RID: 29114 RVA: 0x0024938F File Offset: 0x0024758F
	public GameObject GetCardTypeBannerAnchor()
	{
		if (this.m_cardTypeAnchorObject == null)
		{
			return base.gameObject;
		}
		return this.m_cardTypeAnchorObject;
	}

	// Token: 0x060071BB RID: 29115 RVA: 0x002493AC File Offset: 0x002475AC
	public UberText GetAttackText()
	{
		return this.m_attackTextMesh;
	}

	// Token: 0x060071BC RID: 29116 RVA: 0x002493B4 File Offset: 0x002475B4
	public GameObject GetAttackTextObject()
	{
		if (this.m_attackTextMesh == null)
		{
			return null;
		}
		return this.m_attackTextMesh.gameObject;
	}

	// Token: 0x060071BD RID: 29117 RVA: 0x002493D1 File Offset: 0x002475D1
	public GemObject GetAttackObject()
	{
		if (this.m_attackObject == null)
		{
			return null;
		}
		return this.m_attackObject.GetComponent<GemObject>();
	}

	// Token: 0x060071BE RID: 29118 RVA: 0x002493EE File Offset: 0x002475EE
	public GemObject GetHealthObject()
	{
		if (this.m_healthObject == null)
		{
			return null;
		}
		return this.m_healthObject.GetComponent<GemObject>();
	}

	// Token: 0x060071BF RID: 29119 RVA: 0x0024940B File Offset: 0x0024760B
	public GameObject GetWeaponShields()
	{
		if (this.m_healthObject != null && this.m_healthObject.GetComponent<GemObject>() == null)
		{
			return this.m_healthObject;
		}
		return null;
	}

	// Token: 0x060071C0 RID: 29120 RVA: 0x00249436 File Offset: 0x00247636
	public GameObject GetWeaponSwords()
	{
		if (this.m_attackObject != null && this.m_attackObject.GetComponent<GemObject>() == null)
		{
			return this.m_attackObject;
		}
		return null;
	}

	// Token: 0x060071C1 RID: 29121 RVA: 0x00249461 File Offset: 0x00247661
	public GemObject GetArmorObject()
	{
		if (this.m_armorObject == null)
		{
			return null;
		}
		return this.m_armorObject.GetComponent<GemObject>();
	}

	// Token: 0x060071C2 RID: 29122 RVA: 0x0024947E File Offset: 0x0024767E
	public UberText GetHealthText()
	{
		return this.m_healthTextMesh;
	}

	// Token: 0x060071C3 RID: 29123 RVA: 0x00249486 File Offset: 0x00247686
	public GameObject GetHealthTextObject()
	{
		if (this.m_healthTextMesh == null)
		{
			return null;
		}
		return this.m_healthTextMesh.gameObject;
	}

	// Token: 0x060071C4 RID: 29124 RVA: 0x002494A3 File Offset: 0x002476A3
	public UberText GetCostText()
	{
		if (this.m_costTextMesh == null)
		{
			return null;
		}
		return this.m_costTextMesh;
	}

	// Token: 0x060071C5 RID: 29125 RVA: 0x002494BB File Offset: 0x002476BB
	public GameObject GetCostTextObject()
	{
		if (this.m_costTextMesh == null)
		{
			return null;
		}
		return this.m_costTextMesh.gameObject;
	}

	// Token: 0x060071C6 RID: 29126 RVA: 0x002494D8 File Offset: 0x002476D8
	public UberText GetSecretText()
	{
		return this.m_secretText;
	}

	// Token: 0x060071C7 RID: 29127 RVA: 0x002494E0 File Offset: 0x002476E0
	public void UpdateAllComponents()
	{
		this.UpdateTextComponents();
		this.UpdateMaterials();
		this.UpdateTextures();
		this.UpdateCardBack();
		this.UpdateMeshComponents();
		this.UpdateRootObjectSpellComponents();
		this.UpdateMissingCardArt();
		this.UpdateGhostCardEffect();
		this.UpdateDiamondCardArt();
		this.UpdatePortraitMaterialAnimation();
		this.UpdateContactShadow();
		if (PlatformSettings.OS == OSCategory.Mac && this.m_nameTextMesh)
		{
			base.StartCoroutine(this.DelayedUpdateNameText());
		}
	}

	// Token: 0x060071C8 RID: 29128 RVA: 0x00249551 File Offset: 0x00247751
	private IEnumerator DelayedUpdateNameText()
	{
		yield return null;
		if (this.m_nameTextMesh)
		{
			this.m_nameTextMesh.UpdateNow(false);
		}
		yield break;
	}

	// Token: 0x060071C9 RID: 29129 RVA: 0x00249560 File Offset: 0x00247760
	public bool MissingCardEffect(bool refreshOnFocus = true)
	{
		if (this.m_missingCardEffect)
		{
			RenderToTexture component = this.m_missingCardEffect.GetComponent<RenderToTexture>();
			if (component)
			{
				component.DontRefreshOnFocus = !refreshOnFocus;
				this.m_initialMissingCardRenderQueue = component.m_RenderQueue;
				this.m_missingcard = true;
				this.UpdateAllComponents();
				return true;
			}
		}
		return false;
	}

	// Token: 0x060071CA RID: 29130 RVA: 0x002495B4 File Offset: 0x002477B4
	public void DisableMissingCardEffect()
	{
		this.m_missingcard = false;
		if (this.m_missingCardEffect)
		{
			RenderToTexture component = this.m_missingCardEffect.GetComponent<RenderToTexture>();
			if (component)
			{
				component.enabled = false;
			}
			this.MaterialShaderAnimation(true);
		}
	}

	// Token: 0x060071CB RID: 29131 RVA: 0x002495F8 File Offset: 0x002477F8
	public void UpdateMissingCardArt()
	{
		if (!this.m_missingcard)
		{
			return;
		}
		if (this.m_missingCardEffect == null)
		{
			return;
		}
		RenderToTexture component = this.m_missingCardEffect.GetComponent<RenderToTexture>();
		if (component == null)
		{
			return;
		}
		if (this.m_rootObject.activeSelf)
		{
			this.MaterialShaderAnimation(false);
			TAG_PREMIUM premium = this.GetPremium();
			bool flag = CollectionManager.Get().GetThemeShowing(null) == FormatType.FT_WILD;
			if (premium == TAG_PREMIUM.GOLDEN)
			{
				if (flag)
				{
					component.m_Material.color = this.MISSING_CARD_WILD_GOLDEN_COLOR;
				}
				else
				{
					component.m_Material.color = this.MISSING_CARD_STANDARD_GOLDEN_COLOR;
				}
			}
			else if (premium == TAG_PREMIUM.DIAMOND && flag)
			{
				Material material = component.m_Material;
				material.color = this.MISSING_CARD_WILD_DIAMOND_COLOR;
				material.SetFloat(this.MISSING_CARD_WILD_DIAMOND_CONTRAST_KEY, this.MISSING_CARD_WILD_DIAMOND_CONTRAST);
				material.SetFloat(this.MISSING_CARD_WILD_DIAMOND_INTENSITY_KEY, this.MISSING_CARD_WILD_DIAMOND_INTENSITY);
			}
			component.enabled = true;
			component.Show(true);
			return;
		}
		component.enabled = false;
		component.Hide();
	}

	// Token: 0x060071CC RID: 29132 RVA: 0x002496E8 File Offset: 0x002478E8
	public void SetMissingCardMaterial(Material missingCardMat)
	{
		if (this.m_missingCardEffect == null || missingCardMat == null)
		{
			return;
		}
		RenderToTexture component = this.m_missingCardEffect.GetComponent<RenderToTexture>();
		if (component == null)
		{
			return;
		}
		component.m_Material = missingCardMat;
		if (this.m_rootObject.activeSelf)
		{
			this.MaterialShaderAnimation(false);
			if (component.enabled)
			{
				component.Render();
			}
		}
	}

	// Token: 0x060071CD RID: 29133 RVA: 0x00249750 File Offset: 0x00247950
	public bool isMissingCard()
	{
		if (this.m_missingCardEffect == null)
		{
			return false;
		}
		RenderToTexture component = this.m_missingCardEffect.GetComponent<RenderToTexture>();
		return !(component == null) && component.enabled;
	}

	// Token: 0x060071CE RID: 29134 RVA: 0x0024978C File Offset: 0x0024798C
	public void SetMissingCardRenderQueue(bool reset, int renderQueue)
	{
		RenderToTexture component = this.m_missingCardEffect.GetComponent<RenderToTexture>();
		if (component == null)
		{
			return;
		}
		component.m_RenderQueue = (reset ? this.m_initialMissingCardRenderQueue : renderQueue);
	}

	// Token: 0x060071CF RID: 29135 RVA: 0x002497C1 File Offset: 0x002479C1
	public void GhostCardEffect(GhostCard.Type ghostType, TAG_PREMIUM premium = TAG_PREMIUM.NORMAL, bool update = true)
	{
		if (this.m_ghostCard == ghostType && this.m_ghostPremium == premium)
		{
			return;
		}
		this.m_ghostCard = ghostType;
		this.m_ghostPremium = premium;
		if (update)
		{
			this.UpdateAllComponents();
		}
	}

	// Token: 0x060071D0 RID: 29136 RVA: 0x002497F0 File Offset: 0x002479F0
	private void UpdateGhostCardEffect()
	{
		if (this.m_ghostCardGameObject == null)
		{
			return;
		}
		GhostCard component = this.m_ghostCardGameObject.GetComponent<GhostCard>();
		if (component == null)
		{
			return;
		}
		if (this.m_ghostCard != GhostCard.Type.NONE)
		{
			component.SetGhostType(this.m_ghostCard);
			component.SetPremium(this.m_ghostPremium);
			component.RenderGhostCard();
			return;
		}
		component.DisableGhost();
	}

	// Token: 0x060071D1 RID: 29137 RVA: 0x0024984F File Offset: 0x00247A4F
	public bool isGhostCard()
	{
		return this.m_ghostCard != GhostCard.Type.NONE && this.m_ghostCardGameObject;
	}

	// Token: 0x060071D2 RID: 29138 RVA: 0x00249868 File Offset: 0x00247A68
	public bool DoesDiamondModelExistOnCardDef()
	{
		global::CardDef cardDef = this.m_cardDefHandle.Get();
		return !(cardDef == null) && !string.IsNullOrEmpty(cardDef.m_DiamondModel);
	}

	// Token: 0x060071D3 RID: 29139 RVA: 0x0024989C File Offset: 0x00247A9C
	public bool IsEntityStateBadForDiamondVisuals()
	{
		if (GameState.Get() != null && !GameState.Get().AllowDiamondCards())
		{
			return true;
		}
		this.GetEntity();
		if (this.m_entity == null)
		{
			return false;
		}
		bool flag = this.m_entity.HasTag(GAME_TAG.FROZEN);
		bool flag2 = this.m_entity.HasTag(GAME_TAG.REBORN);
		bool flag3 = this.m_entity.HasTag(GAME_TAG.STEALTH);
		bool flag4 = this.m_entity.HasTag(GAME_TAG.DORMANT);
		bool flag5 = this.m_entity.HasTag(GAME_TAG.ENRAGED);
		bool flag6 = this.m_entity.HasTag(GAME_TAG.CANT_BE_TARGETED_BY_SPELLS) && this.m_entity.HasTag(GAME_TAG.CANT_BE_TARGETED_BY_HERO_POWERS);
		global::Card card = this.GetCard();
		if (card != null)
		{
			Spell actorSpell = card.GetActorSpell(SpellType.DORMANT, false);
			if (actorSpell != null && actorSpell.GetActiveState() != SpellStateType.NONE)
			{
				flag4 = true;
			}
		}
		bool flag7 = false;
		if (this.m_card != null && this.m_card.GetZone() is ZoneGraveyard)
		{
			flag7 = true;
		}
		return flag || flag2 || flag3 || flag4 || flag5 || flag6 || flag7;
	}

	// Token: 0x060071D4 RID: 29140 RVA: 0x002499B4 File Offset: 0x00247BB4
	public void UpdateDiamondCardArt()
	{
		if (this.m_premiumType != TAG_PREMIUM.DIAMOND)
		{
			return;
		}
		if (this.m_portraitMesh != null && this.m_portraitMeshRTT != null)
		{
			bool flag = this.IsEntityStateBadForDiamondVisuals();
			bool flag2 = this.DoesDiamondModelExistOnCardDef();
			if (flag || !flag2)
			{
				this.m_portraitMesh.SetActive(true);
				this.m_portraitMeshRTT.SetActive(false);
			}
			else
			{
				this.m_portraitMesh.SetActive(false);
				this.m_portraitMeshRTT.SetActive(true);
			}
		}
		if (this.m_cardDefHandle.Get() == null)
		{
			return;
		}
		if (this.DoesDiamondModelExistOnCardDef() && this.m_rootObject != null)
		{
			bool flag3 = this.m_diamondModelObject != null;
			string diamondModel = this.m_cardDefHandle.Get().m_DiamondModel;
			if (this.m_diamondPortraitR2T && !this.m_diamondRenderToTexture)
			{
				this.m_diamondRenderToTexture = this.m_diamondPortraitR2T.GetComponent<DiamondRenderToTexture>();
			}
			if (flag3 && diamondModel != this.m_diamondModelShown)
			{
				UnityEngine.Object.Destroy(this.m_diamondModelObject);
				this.m_diamondModelObject = null;
				flag3 = false;
				if (this.m_diamondRenderToTexture)
				{
					this.m_diamondRenderToTexture.enabled = false;
				}
			}
			if (!flag3)
			{
				this.m_diamondModelObject = AssetLoader.Get().InstantiatePrefab(diamondModel, AssetLoadingOptions.IgnorePrefabPosition);
				this.m_diamondModelShown = diamondModel;
				this.m_diamondModelObject.transform.parent = this.m_rootObject.transform;
				if (this.m_diamondRenderToTexture)
				{
					this.m_diamondRenderToTexture.m_ObjectToRender = this.m_diamondModelObject;
					this.m_diamondRenderToTexture.m_ClearColor = this.m_cardDefHandle.Get().m_DiamondPlaneRTT_CearColor;
				}
				this.m_portraitMeshDirty = true;
			}
			else if (this.m_diamondRenderToTexture)
			{
				this.m_diamondRenderToTexture.UpdateMaterialBlend(this.m_usePlayPortrait);
			}
			else
			{
				this.m_diamondModelObject.SetActive(false);
			}
		}
		if (this.m_portraitMeshDirty && this.m_portraitMeshRTT != null && this.m_portraitMeshRTT_background != null)
		{
			AssetReference assetReference;
			if (this.m_card == null)
			{
				assetReference = this.m_cardDefHandle.Get().m_DiamondPlaneRTT_Hand;
			}
			else
			{
				assetReference = (this.m_usePlayPortrait ? this.m_cardDefHandle.Get().m_DiamondPlaneRTT_Play : this.m_cardDefHandle.Get().m_DiamondPlaneRTT_Hand);
			}
			MeshFilter component = this.m_portraitMeshRTT.GetComponent<MeshFilter>();
			if (component != null && assetReference != null)
			{
				using (AssetHandle<Mesh> assetHandle = AssetLoader.Get().LoadAsset<Mesh>(assetReference, AssetLoadingOptions.None))
				{
					if (assetHandle != null)
					{
						component.sharedMesh = assetHandle;
					}
				}
			}
			AssetReference assetReference2 = this.m_cardDefHandle.Get().m_DiamondPortraitTexturePath;
			Renderer component2 = this.m_portraitMeshRTT_background.GetComponent<Renderer>();
			if (component2 != null && component2.GetSharedMaterial().HasProperty("_MainTex") && assetReference2 != null)
			{
				using (AssetHandle<Texture2D> assetHandle2 = AssetLoader.Get().LoadAsset<Texture2D>(assetReference2, AssetLoadingOptions.None))
				{
					if (assetHandle2 != null)
					{
						Actor.GetMaterialInstance(this.m_portraitMeshRTT_background.GetComponent<Renderer>()).SetTexture("_MainTex", assetHandle2);
					}
				}
			}
			HighlightState componentInChildren = base.GetComponentInChildren<HighlightState>();
			if (componentInChildren != null && componentInChildren.isActiveAndEnabled)
			{
				componentInChildren.ContinuousUpdate(0.1f);
			}
			this.m_portraitMeshDirty = false;
		}
		if (this.m_diamondRenderToTexture)
		{
			this.m_diamondRenderToTexture.enabled = this.m_shown;
		}
		if (!this.DoesDiamondModelExistOnCardDef() && this.m_diamondModelObject != null)
		{
			UnityEngine.Object.Destroy(this.m_diamondModelObject);
			this.m_diamondModelObject = null;
		}
		if (this.m_diamondModelObject == null && this.m_diamondPortraitR2T != null && this.m_diamondRenderToTexture && this.m_diamondRenderToTexture.enabled)
		{
			this.m_diamondRenderToTexture.enabled = false;
		}
	}

	// Token: 0x060071D5 RID: 29141 RVA: 0x00249DB4 File Offset: 0x00247FB4
	public void UpdateMaterials()
	{
		if (base.gameObject.activeInHierarchy)
		{
			base.StartCoroutine(this.UpdatePortraitMaterials());
			return;
		}
		this.isPortraitMaterialDirty = true;
	}

	// Token: 0x060071D6 RID: 29142 RVA: 0x00249DD8 File Offset: 0x00247FD8
	public void OverrideAllMeshMaterials(Material material)
	{
		if (this.m_rootObject == null)
		{
			return;
		}
		this.RecursivelyReplaceMaterialsList(this.m_rootObject.transform, material);
	}

	// Token: 0x060071D7 RID: 29143 RVA: 0x00249DFB File Offset: 0x00247FFB
	public void SetUnlit()
	{
		this.SetLightBlend(0f, true);
	}

	// Token: 0x060071D8 RID: 29144 RVA: 0x00249E09 File Offset: 0x00248009
	public void SetLit()
	{
		this.SetLightBlend(1f, true);
	}

	// Token: 0x060071D9 RID: 29145 RVA: 0x00249E17 File Offset: 0x00248017
	public void SetLightBlend(float blendValue, bool includeInactive = false)
	{
		this.SetLightBlend(base.gameObject, blendValue, includeInactive);
	}

	// Token: 0x060071DA RID: 29146 RVA: 0x00249E28 File Offset: 0x00248028
	private void SetLightBlend(GameObject go, float blendValue, bool includeInactive = false)
	{
		Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>(includeInactive);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Renderer renderer = componentsInChildren[i];
			if (!renderer.gameObject.activeInHierarchy)
			{
				DeferredEnableHandler.AttachTo(renderer, delegate()
				{
					this.SetRendererLightBlend(renderer, blendValue);
				});
			}
			else
			{
				this.SetRendererLightBlend(renderer, blendValue);
			}
		}
		UberText[] componentsInChildren2 = go.GetComponentsInChildren<UberText>(includeInactive);
		for (int i = 0; i < componentsInChildren2.Length; i++)
		{
			componentsInChildren2[i].AmbientLightBlend = blendValue;
		}
	}

	// Token: 0x060071DB RID: 29147 RVA: 0x00249EE0 File Offset: 0x002480E0
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

	// Token: 0x060071DC RID: 29148 RVA: 0x00249F50 File Offset: 0x00248150
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
			this.ReplaceMaterialsList(transformToRecurse.GetComponent<Renderer>(), newMaterialPrefab);
		}
		foreach (object obj in transformToRecurse)
		{
			Transform transformToRecurse2 = (Transform)obj;
			this.RecursivelyReplaceMaterialsList(transformToRecurse2, newMaterialPrefab);
		}
	}

	// Token: 0x060071DD RID: 29149 RVA: 0x00249FEC File Offset: 0x002481EC
	private void ReplaceMaterialsList(Renderer renderer, Material newMaterialPrefab)
	{
		List<Material> materials = renderer.GetMaterials();
		int count = materials.Count;
		Material[] array = new Material[count];
		for (int i = 0; i < count; i++)
		{
			Material oldMaterial = materials[i];
			array[i] = this.CreateReplacementMaterial(oldMaterial, newMaterialPrefab);
		}
		renderer.SetMaterials(array);
		if (renderer != this.m_meshRenderer)
		{
			return;
		}
		this.UpdatePortraitTexture();
	}

	// Token: 0x060071DE RID: 29150 RVA: 0x0024A04B File Offset: 0x0024824B
	private Material CreateReplacementMaterial(Material oldMaterial, Material newMaterialPrefab)
	{
		Material material = UnityEngine.Object.Instantiate<Material>(newMaterialPrefab);
		material.mainTexture = oldMaterial.mainTexture;
		return material;
	}

	// Token: 0x060071DF RID: 29151 RVA: 0x0024A060 File Offset: 0x00248260
	public void SeedMaterialEffects()
	{
		if (this.m_materialEffectsSeeded)
		{
			return;
		}
		this.m_materialEffectsSeeded = true;
		Renderer[] componentsInChildren = base.GetComponentsInChildren<Renderer>();
		float value = UnityEngine.Random.Range(0f, 2f);
		foreach (Renderer renderer in componentsInChildren)
		{
			List<Material> sharedMaterials = renderer.GetSharedMaterials();
			if (sharedMaterials.Count == 1)
			{
				Material material = sharedMaterials[0];
				if (material.HasProperty("_Seed") && material.GetFloat("_Seed") == 0f)
				{
					Actor.GetMaterialInstance(renderer).SetFloat("_Seed", value);
				}
			}
			else
			{
				List<Material> materials = renderer.GetMaterials();
				if (materials != null && materials.Count != 0)
				{
					foreach (Material material2 in materials)
					{
						if (!(material2 == null) && material2.HasProperty("_Seed") && material2.GetFloat("_Seed") == 0f)
						{
							material2.SetFloat("_Seed", value);
						}
					}
				}
			}
		}
	}

	// Token: 0x060071E0 RID: 29152 RVA: 0x0024A18C File Offset: 0x0024838C
	public void MaterialShaderAnimation(bool animationEnabled)
	{
		if (this.m_diamondPortraitR2T)
		{
			return;
		}
		float value = 0f;
		if (animationEnabled)
		{
			value = 1f;
		}
		Renderer[] componentsInChildren = base.GetComponentsInChildren<Renderer>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			foreach (Material material in componentsInChildren[i].GetSharedMaterials())
			{
				if (!(material == null) && material.HasProperty("_TimeScale"))
				{
					material.SetFloat("_TimeScale", value);
				}
			}
		}
	}

	// Token: 0x060071E1 RID: 29153 RVA: 0x0024A234 File Offset: 0x00248434
	public CardBackManager.CardBackSlot GetCardBackSlot()
	{
		if (this.m_cardBackSlotOverride != null)
		{
			return this.m_cardBackSlotOverride.Value;
		}
		Player.Side side = Player.Side.FRIENDLY;
		if (this.m_cardBackSideOverride != null)
		{
			side = this.m_cardBackSideOverride.Value;
		}
		else if (this.m_entity != null)
		{
			Player controller = this.m_entity.GetController();
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

	// Token: 0x060071E2 RID: 29154 RVA: 0x0024A29C File Offset: 0x0024849C
	public void SetCardBackSideOverride(Player.Side? sideOverride)
	{
		this.m_cardBackSideOverride = sideOverride;
	}

	// Token: 0x060071E3 RID: 29155 RVA: 0x0024A2A5 File Offset: 0x002484A5
	public void SetCardBackSlotOverride(CardBackManager.CardBackSlot? slotOverride)
	{
		this.m_cardBackSlotOverride = slotOverride;
	}

	// Token: 0x060071E4 RID: 29156 RVA: 0x0024A2AE File Offset: 0x002484AE
	public bool GetCardbackUpdateIgnore()
	{
		return this.m_ignoreUpdateCardback;
	}

	// Token: 0x060071E5 RID: 29157 RVA: 0x0024A2B6 File Offset: 0x002484B6
	public void SetCardbackUpdateIgnore(bool ignoreUpdate)
	{
		this.m_ignoreUpdateCardback = ignoreUpdate;
	}

	// Token: 0x060071E6 RID: 29158 RVA: 0x0024A2C0 File Offset: 0x002484C0
	public void UpdateCardBack()
	{
		if (this.m_ignoreUpdateCardback)
		{
			return;
		}
		CardBackManager cardBackManager = CardBackManager.Get();
		if (cardBackManager == null)
		{
			return;
		}
		CardBackManager.CardBackSlot cardBackSlot = this.GetCardBackSlot();
		this.UpdateCardBackDisplay(cardBackSlot);
		this.UpdateCardBackDragEffect();
		if (this.m_cardMesh == null || this.m_cardBackMatIdx < 0)
		{
			return;
		}
		cardBackManager.SetCardBackTexture(this.m_cardMesh.GetComponent<Renderer>(), this.m_cardBackMatIdx, cardBackSlot);
	}

	// Token: 0x060071E7 RID: 29159 RVA: 0x0024A324 File Offset: 0x00248524
	public void EnableCardbackShadow(bool enabled)
	{
		CardBackDisplay componentInChildren = base.GetComponentInChildren<CardBackDisplay>();
		if (componentInChildren == null)
		{
			return;
		}
		componentInChildren.EnableShadow(enabled);
	}

	// Token: 0x060071E8 RID: 29160 RVA: 0x0024A34C File Offset: 0x0024854C
	private void UpdateCardBackDragEffect()
	{
		if (SceneMgr.Get() == null)
		{
			return;
		}
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY)
		{
			return;
		}
		CardBackDragEffect componentInChildren = base.GetComponentInChildren<CardBackDragEffect>();
		if (componentInChildren == null)
		{
			return;
		}
		componentInChildren.SetEffect();
	}

	// Token: 0x060071E9 RID: 29161 RVA: 0x0024A388 File Offset: 0x00248588
	private void UpdateCardBackDisplay(CardBackManager.CardBackSlot slot)
	{
		CardBackDisplay componentInChildren = base.GetComponentInChildren<CardBackDisplay>();
		if (componentInChildren == null)
		{
			return;
		}
		componentInChildren.SetCardBack(slot);
	}

	// Token: 0x060071EA RID: 29162 RVA: 0x0024A3AD File Offset: 0x002485AD
	public void UpdateTextures()
	{
		this.UpdatePortraitTexture();
	}

	// Token: 0x060071EB RID: 29163 RVA: 0x0024A3B8 File Offset: 0x002485B8
	public void UpdatePortraitTexture()
	{
		if (this.m_portraitTextureOverride != null)
		{
			this.SetPortraitTexture(this.m_portraitTextureOverride);
			return;
		}
		if (this.m_cardDefHandle.Get() != null)
		{
			this.SetPortraitTexture(this.m_cardDefHandle.Get().GetPortraitTexture());
		}
	}

	// Token: 0x060071EC RID: 29164 RVA: 0x0024A40C File Offset: 0x0024860C
	public void SetPortraitTexture(Texture texture)
	{
		if (this.m_cardDefHandle.Get() != null && (this.m_premiumType >= TAG_PREMIUM.GOLDEN || this.m_cardDefHandle.Get().m_AlwaysRenderPremiumPortrait) && this.IsPremiumPortraitEnabled() && this.m_cardDefHandle.Get().GetPremiumPortraitMaterial() != null)
		{
			return;
		}
		Material portraitMaterial = this.GetPortraitMaterial();
		if (portraitMaterial == null)
		{
			return;
		}
		portraitMaterial.mainTexture = texture;
	}

	// Token: 0x060071ED RID: 29165 RVA: 0x0024A480 File Offset: 0x00248680
	public void SetPortraitTextureOverride(Texture portrait)
	{
		this.m_portraitTextureOverride = portrait;
		this.UpdatePortraitTexture();
	}

	// Token: 0x060071EE RID: 29166 RVA: 0x0024A490 File Offset: 0x00248690
	public Texture GetPortraitTexture()
	{
		Material portraitMaterial = this.GetPortraitMaterial();
		if (portraitMaterial == null)
		{
			return null;
		}
		return portraitMaterial.mainTexture;
	}

	// Token: 0x060071EF RID: 29167 RVA: 0x0024A4B5 File Offset: 0x002486B5
	public Texture GetStaticPortraitTexture()
	{
		if (this.m_portraitTextureOverride != null)
		{
			return this.m_portraitTextureOverride;
		}
		return this.m_cardDefHandle.Get().GetPortraitTexture();
	}

	// Token: 0x060071F0 RID: 29168 RVA: 0x0024A4DC File Offset: 0x002486DC
	private IEnumerator UpdatePortraitMaterials()
	{
		this.isPortraitMaterialDirty = false;
		if (this.m_shadowform)
		{
			yield break;
		}
		global::CardDef cardDef = this.m_cardDefHandle.Get();
		if (!cardDef)
		{
			yield break;
		}
		if ((this.m_premiumType >= TAG_PREMIUM.GOLDEN || cardDef.m_AlwaysRenderPremiumPortrait) && this.IsPremiumPortraitEnabled())
		{
			if (cardDef && !cardDef.IsPremiumLoaded())
			{
				yield return null;
			}
			if (cardDef && cardDef.GetPremiumPortraitMaterial() != null)
			{
				this.SetPortraitMaterial(cardDef.GetPremiumPortraitMaterial());
			}
			else if (this.m_initialPortraitMaterial != null)
			{
				this.SetPortraitMaterial(this.m_initialPortraitMaterial);
			}
		}
		else
		{
			this.SetPortraitMaterial(this.m_initialPortraitMaterial);
		}
		this.UpdatePortraitTexture();
		yield break;
	}

	// Token: 0x060071F1 RID: 29169 RVA: 0x0024A4EC File Offset: 0x002486EC
	private void UpdatePortraitMaterialAnimation()
	{
		if (this.m_cardDefHandle.Get() == null || this.m_cardDefHandle.Get().GetPremiumPortraitAnimation() == null || this.m_portraitMesh == null)
		{
			return;
		}
		this.m_uberShaderController = this.m_portraitMesh.GetComponent<UberShaderController>();
		if (this.m_uberShaderController == null)
		{
			this.m_uberShaderController = this.m_portraitMesh.gameObject.AddComponent<UberShaderController>();
			this.m_uberShaderController.UberShaderAnimation = UnityEngine.Object.Instantiate<UberShaderAnimation>(this.m_cardDefHandle.Get().GetPremiumPortraitAnimation());
		}
		else
		{
			if (this.m_uberShaderController.UberShaderAnimation.name.Replace("(Clone)", "") == this.m_cardDefHandle.Get().GetPremiumPortraitAnimation().name)
			{
				return;
			}
			this.m_uberShaderController.UberShaderAnimation = UnityEngine.Object.Instantiate<UberShaderAnimation>(this.m_cardDefHandle.Get().GetPremiumPortraitAnimation());
		}
		this.m_uberShaderController.m_MaterialIndex = this.m_portraitMatIdx;
		if (this.isGhostCard() && this.m_ghostCard != GhostCard.Type.DORMANT)
		{
			this.m_uberShaderController.enabled = false;
			return;
		}
		this.m_uberShaderController.enabled = true;
	}

	// Token: 0x060071F2 RID: 29170 RVA: 0x0024A624 File Offset: 0x00248824
	public void SetPortraitMaterial(Material material)
	{
		if (material == null)
		{
			return;
		}
		if (this.m_portraitMesh != null && this.m_portraitMatIdx > -1)
		{
			Renderer component = this.m_portraitMesh.GetComponent<Renderer>();
			Material material2 = component.GetMaterial(this.m_portraitMatIdx);
			if (material2.mainTexture == material.mainTexture && material2.shader == material.shader)
			{
				return;
			}
			component.SetMaterial(this.m_portraitMatIdx, material);
			float value = 0f;
			if (this.m_card)
			{
				Zone zone = this.m_card.GetZone();
				if (zone is ZonePlay || zone is ZoneWeapon || zone is ZoneHeroPower)
				{
					value = 1f;
				}
			}
			using (List<Material>.Enumerator enumerator = component.GetMaterials().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Material material3 = enumerator.Current;
					if (material3.HasProperty("_LightingBlend"))
					{
						material3.SetFloat("_LightingBlend", value);
					}
					if (material3.HasProperty("_Seed") && material3.GetFloat("_Seed") == 0f)
					{
						material3.SetFloat("_Seed", UnityEngine.Random.Range(0f, 2f));
					}
				}
				return;
			}
		}
		if (this.m_legacyPortraitMaterialIndex >= 0)
		{
			if (this.m_meshRenderer.GetMaterial(this.m_legacyPortraitMaterialIndex) == material)
			{
				return;
			}
			this.m_meshRenderer.SetMaterial(this.m_legacyPortraitMaterialIndex, material);
		}
	}

	// Token: 0x060071F3 RID: 29171 RVA: 0x0024A7B0 File Offset: 0x002489B0
	public GameObject GetPortraitMesh()
	{
		return this.m_portraitMesh;
	}

	// Token: 0x060071F4 RID: 29172 RVA: 0x0024A7B8 File Offset: 0x002489B8
	public virtual Material GetPortraitMaterial()
	{
		if (this.m_portraitMesh != null)
		{
			Renderer component = this.m_portraitMesh.GetComponent<Renderer>();
			if (0 <= this.m_portraitMatIdx && this.m_portraitMatIdx < component.GetSharedMaterials().Count)
			{
				if (!Application.isPlaying)
				{
					return component.GetSharedMaterial(this.m_portraitMatIdx);
				}
				return component.GetMaterial(this.m_portraitMatIdx);
			}
		}
		if (this.m_legacyPortraitMaterialIndex >= 0)
		{
			return this.m_meshRenderer.GetMaterial(this.m_legacyPortraitMaterialIndex);
		}
		return null;
	}

	// Token: 0x060071F5 RID: 29173 RVA: 0x0024A838 File Offset: 0x00248A38
	protected virtual bool IsPremiumPortraitEnabled()
	{
		return (GameState.Get() == null || GameState.Get().GetGameEntity() == null || !GameState.Get().GetGameEntity().HasTag(GAME_TAG.DISABLE_GOLDEN_ANIMATIONS)) && GraphicsManager.Get() != null && !GraphicsManager.Get().isVeryLowQualityDevice();
	}

	// Token: 0x060071F6 RID: 29174 RVA: 0x0024A886 File Offset: 0x00248A86
	public void SetBlockTextComponentUpdate(bool block)
	{
		this.m_blockTextComponentUpdate = block;
	}

	// Token: 0x060071F7 RID: 29175 RVA: 0x0024A88F File Offset: 0x00248A8F
	public virtual void UpdateTextComponents()
	{
		if (this.m_blockTextComponentUpdate)
		{
			return;
		}
		if (this.m_entityDef != null)
		{
			this.UpdateTextComponentsDef(this.m_entityDef);
			return;
		}
		this.UpdateTextComponents(this.m_entity);
	}

	// Token: 0x060071F8 RID: 29176 RVA: 0x0024A8BC File Offset: 0x00248ABC
	public virtual void UpdateTextComponentsDef(EntityDef entityDef)
	{
		if (entityDef == null)
		{
			return;
		}
		this.UpdateCostTextMesh(entityDef);
		this.UpdateAttackTextMesh(entityDef);
		this.UpdateHealthTextMesh(entityDef);
		this.UpdateArmorTextMesh(entityDef);
		this.UpdateNameText();
		this.UpdatePowersText();
		this.UpdateRace(entityDef.GetRaceText());
		this.UpdateSecretAndQuestText();
		this.UpdateBannedRibbonTextMesh(entityDef);
	}

	// Token: 0x060071F9 RID: 29177 RVA: 0x0024A910 File Offset: 0x00248B10
	private void UpdateCostTextMesh(EntityDef entityDef)
	{
		if (this.m_costTextMesh == null)
		{
			return;
		}
		if (this.HasHideStats(entityDef) || entityDef.HasTag(GAME_TAG.HIDE_COST) || this.UseTechLevelManaGem())
		{
			this.m_costTextMesh.Text = "";
			return;
		}
		if (entityDef.HasTriggerVisual() && entityDef.IsHeroPowerOrGameModeButton())
		{
			this.m_costTextMesh.Text = "";
			return;
		}
		this.m_costTextMesh.Text = Convert.ToString(entityDef.GetTag(GAME_TAG.COST));
	}

	// Token: 0x060071FA RID: 29178 RVA: 0x0024A994 File Offset: 0x00248B94
	private void UpdateAttackTextMesh(EntityDef entityDef)
	{
		int tag = entityDef.GetTag(GAME_TAG.ATK);
		if (this.m_attackTextMesh != null && (this.HasHideStats(entityDef) || entityDef.HasTag(GAME_TAG.HIDE_ATTACK)))
		{
			this.m_attackTextMesh.Text = "";
			this.m_attackTextMesh.gameObject.SetActive(false);
			GemObject gemObject = SceneUtils.FindComponentInThisOrParents<GemObject>(this.m_attackTextMesh.gameObject);
			if (gemObject != null)
			{
				gemObject.Hide();
				gemObject.SetHideNumberFlag(true);
				return;
			}
		}
		else if (entityDef.IsHero())
		{
			if (tag == 0)
			{
				if (this.m_attackObject != null && this.m_attackObject.activeSelf)
				{
					this.m_attackObject.SetActive(false);
				}
				if (this.m_attackTextMesh != null)
				{
					this.m_attackTextMesh.Text = "";
					return;
				}
			}
			else
			{
				if (this.m_attackObject != null && !this.m_attackObject.activeSelf)
				{
					this.m_attackObject.SetActive(true);
				}
				if (this.m_attackTextMesh != null)
				{
					this.m_attackTextMesh.Text = Convert.ToString(tag);
					return;
				}
			}
		}
		else if (this.m_attackTextMesh != null)
		{
			this.m_attackTextMesh.Text = Convert.ToString(tag);
		}
	}

	// Token: 0x060071FB RID: 29179 RVA: 0x0024AAD4 File Offset: 0x00248CD4
	private void UpdateHealthTextMesh(EntityDef entityDef)
	{
		if (this.m_healthTextMesh == null)
		{
			return;
		}
		if (this.HasHideStats(entityDef) || entityDef.HasTag(GAME_TAG.HIDE_HEALTH))
		{
			this.m_healthTextMesh.Text = "";
			this.m_healthTextMesh.gameObject.SetActive(false);
			GemObject gemObject = SceneUtils.FindComponentInThisOrParents<GemObject>(this.m_healthTextMesh.gameObject);
			if (gemObject != null)
			{
				gemObject.Hide();
				gemObject.SetHideNumberFlag(true);
				return;
			}
		}
		else
		{
			if (entityDef.IsWeapon())
			{
				this.m_healthTextMesh.Text = Convert.ToString(entityDef.GetTag(GAME_TAG.DURABILITY));
				return;
			}
			this.m_healthTextMesh.Text = Convert.ToString(entityDef.GetTag(GAME_TAG.HEALTH));
		}
	}

	// Token: 0x060071FC RID: 29180 RVA: 0x0024AB8C File Offset: 0x00248D8C
	private void UpdateArmorTextMesh(EntityDef entityDef)
	{
		if (this.m_armorTextMesh == null)
		{
			return;
		}
		int tag = entityDef.GetTag(GAME_TAG.ARMOR);
		if (tag == 0 || this.HasHideStats(entityDef))
		{
			if (this.m_armorObject != null && this.m_armorObject.activeSelf)
			{
				this.m_armorObject.SetActive(false);
			}
			this.m_armorTextMesh.Text = "";
			return;
		}
		if (this.m_armorObject != null && !this.m_armorObject.activeSelf)
		{
			this.m_armorObject.SetActive(true);
		}
		this.m_armorTextMesh.Text = Convert.ToString(tag);
	}

	// Token: 0x060071FD RID: 29181 RVA: 0x0024AC30 File Offset: 0x00248E30
	private void UpdateBannedRibbonTextMesh(EntityDef entityDef)
	{
		if (this.m_bannedRibbonContainer == null)
		{
			return;
		}
		this.m_bannedRibbonContainer.gameObject.SetActive(false);
		if (this.m_bannedRibbon == null)
		{
			return;
		}
		if (entityDef.IsCustomCoin())
		{
			return;
		}
		if (CraftingManager.GetIsInCraftingMode())
		{
			return;
		}
		if (RankMgr.Get().HasLocalPlayerMedalInfo && RankMgr.Get().IsCardLockedInCurrentLeague(entityDef))
		{
			this.m_bannedRibbonContainer.gameObject.SetActive(true);
			this.m_bannedRibbon.SetActive(true);
			this.m_bannedRibbon.GetComponentInChildren<UberText>().Text = RankMgr.Get().GetLocalPlayerStandardLeagueConfig().LockedCardUnplayableText;
		}
	}

	// Token: 0x060071FE RID: 29182 RVA: 0x0024ACD8 File Offset: 0x00248ED8
	public void UpdateMinionStatsImmediately()
	{
		if (this.m_entity == null || !this.m_entity.IsMinion() || this.HasHideStats(this.m_entity))
		{
			return;
		}
		if (this.m_attackTextMesh != null && !this.m_entity.HasTag(GAME_TAG.HIDE_ATTACK))
		{
			this.UpdateTextColorToGreenOrWhite(this.m_attackTextMesh, this.m_entity.GetDefATK(), this.m_entity.GetATK());
			this.m_attackTextMesh.Text = Convert.ToString(this.m_entity.GetATK());
		}
		if (this.m_healthTextMesh != null && !this.m_entity.HasTag(GAME_TAG.HIDE_HEALTH))
		{
			int num;
			if (this.m_entity.HasTag(GAME_TAG.ENABLE_HEALTH_DISPLAY))
			{
				num = this.m_entity.GetTag(GAME_TAG.HEALTH_DISPLAY);
				if (this.m_entity.HasTag(GAME_TAG.HEALTH_DISPLAY_NEGATIVE))
				{
					num = -num;
				}
				int tag = this.m_entity.GetTag(GAME_TAG.HEALTH_DISPLAY_COLOR);
				if (tag == 0)
				{
					this.UpdateTextColor(this.m_healthTextMesh, num, num);
				}
				else if (tag == 1)
				{
					this.UpdateTextColor(this.m_healthTextMesh, num + 1, num);
				}
				else if (tag == 2)
				{
					this.UpdateTextColor(this.m_healthTextMesh, num - 1, num);
				}
			}
			else
			{
				int health = this.m_entity.GetHealth();
				int defHealth = this.m_entity.GetDefHealth();
				num = health - this.m_entity.GetDamage();
				if (this.m_entity.GetDamage() > 0)
				{
					this.UpdateTextColor(this.m_healthTextMesh, health, num);
				}
				else if (health > defHealth)
				{
					this.UpdateTextColor(this.m_healthTextMesh, defHealth, num);
				}
				else
				{
					this.UpdateTextColor(this.m_healthTextMesh, num, num);
				}
			}
			this.m_healthTextMesh.Text = Convert.ToString(num);
		}
	}

	// Token: 0x060071FF RID: 29183 RVA: 0x0024AE90 File Offset: 0x00249090
	public virtual void UpdateTextComponents(Entity entity)
	{
		if (entity == null)
		{
			return;
		}
		this.UpdateCostTextMesh(entity);
		this.UpdateAttackTextMesh(entity);
		this.UpdateHealthTextMesh(entity);
		this.UpdateArmorTextMesh(entity);
		this.UpdateNameText();
		this.UpdatePowersText();
		this.UpdateRace(entity.GetRaceText());
		this.UpdateSecretAndQuestText();
	}

	// Token: 0x06007200 RID: 29184 RVA: 0x0024AED0 File Offset: 0x002490D0
	private int GetSecretCostByClass(TAG_CLASS classType)
	{
		switch (classType)
		{
		case TAG_CLASS.HUNTER:
		case TAG_CLASS.ROGUE:
			return 2;
		case TAG_CLASS.MAGE:
			return 3;
		case TAG_CLASS.PALADIN:
			return 1;
		case TAG_CLASS.WARRIOR:
			return 0;
		}
		return -1;
	}

	// Token: 0x06007201 RID: 29185 RVA: 0x0024AF08 File Offset: 0x00249108
	private void UpdateCostTextMesh(Entity entity)
	{
		if (this.m_costTextMesh == null)
		{
			return;
		}
		if (this.HasHideStats(this.m_entity) || this.m_entity.HasTag(GAME_TAG.HIDE_COST) || this.UseTechLevelManaGem())
		{
			this.UpdateNumberText(this.m_costTextMesh, "", false);
			return;
		}
		if (this.m_entity.IsSecret() && this.m_entity.IsHidden() && this.m_entity.IsControlledByConcealedPlayer())
		{
			int secretCostByClass = this.GetSecretCostByClass(entity.GetClass());
			if (secretCostByClass >= 0)
			{
				this.UpdateTextColor(this.m_costTextMesh, secretCostByClass, entity.GetCost(), true);
			}
			else
			{
				this.m_costTextMesh.TextColor = Color.white;
			}
		}
		else
		{
			this.UpdateTextColor(this.m_costTextMesh, entity.GetDefCost(), entity.GetCost(), true);
		}
		if (this.m_entity.HasTriggerVisual() && this.m_entity.IsHeroPowerOrGameModeButton())
		{
			this.UpdateNumberText(this.m_costTextMesh, "", true);
			return;
		}
		this.UpdateNumberText(this.m_costTextMesh, Convert.ToString(entity.GetCost()));
	}

	// Token: 0x06007202 RID: 29186 RVA: 0x0024B01C File Offset: 0x0024921C
	private void UpdateAttackTextMesh(Entity entity)
	{
		if (this.m_attackTextMesh == null)
		{
			return;
		}
		if (this.HasHideStats(entity) || entity.HasTag(GAME_TAG.HIDE_ATTACK))
		{
			this.UpdateNumberText(this.m_attackTextMesh, "", true);
			return;
		}
		if (!entity.IsHero())
		{
			int num = entity.GetATK();
			if (entity.IsDormant() && entity.HasCachedTagForDormant(GAME_TAG.ATK))
			{
				num = entity.GetCachedTagForDormant(GAME_TAG.ATK);
			}
			this.UpdateTextColorToGreenOrWhite(this.m_attackTextMesh, entity.GetDefATK(), num);
			this.UpdateNumberText(this.m_attackTextMesh, Convert.ToString(num));
			return;
		}
		int atk = entity.GetATK();
		if (atk == 0)
		{
			this.UpdateNumberText(this.m_attackTextMesh, "", true);
			return;
		}
		global::Card weaponCard = entity.GetController().GetWeaponCard();
		int defNumber = 0;
		if (weaponCard != null)
		{
			defNumber = weaponCard.GetEntity().GetATK();
		}
		this.UpdateTextColorToGreenOrWhite(this.m_attackTextMesh, defNumber, atk);
		this.UpdateNumberText(this.m_attackTextMesh, Convert.ToString(atk));
	}

	// Token: 0x06007203 RID: 29187 RVA: 0x0024B114 File Offset: 0x00249314
	private void UpdateHealthTextMesh(Entity entity)
	{
		if (this.m_healthTextMesh != null && (!entity.IsHero() || entity.GetZone() != TAG_ZONE.GRAVEYARD))
		{
			if (this.HasHideStats(entity) || entity.HasTag(GAME_TAG.HIDE_HEALTH))
			{
				this.UpdateNumberText(this.m_healthTextMesh, "", true);
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
			if (this.m_entity.HasTag(GAME_TAG.ENABLE_HEALTH_DISPLAY))
			{
				num4 = this.m_entity.GetTag(GAME_TAG.HEALTH_DISPLAY);
				if (this.m_entity.HasTag(GAME_TAG.HEALTH_DISPLAY_NEGATIVE))
				{
					num4 = -num4;
				}
				int tag = this.m_entity.GetTag(GAME_TAG.HEALTH_DISPLAY_COLOR);
				if (tag == 0)
				{
					this.UpdateTextColor(this.m_healthTextMesh, num4, num4);
				}
				else if (tag == 1)
				{
					this.UpdateTextColor(this.m_healthTextMesh, num4 + 1, num4);
				}
				else if (tag == 2)
				{
					this.UpdateTextColor(this.m_healthTextMesh, num4 - 1, num4);
				}
			}
			else if (entity.GetDamage() > 0)
			{
				this.UpdateTextColor(this.m_healthTextMesh, num, num4);
			}
			else if (num > num2)
			{
				this.UpdateTextColor(this.m_healthTextMesh, num2, num4);
			}
			else
			{
				this.UpdateTextColor(this.m_healthTextMesh, num4, num4);
			}
			this.UpdateNumberText(this.m_healthTextMesh, Convert.ToString(num4));
		}
	}

	// Token: 0x06007204 RID: 29188 RVA: 0x0024B2A8 File Offset: 0x002494A8
	private void UpdateArmorTextMesh(Entity entity)
	{
		if (this.m_armorTextMesh == null)
		{
			return;
		}
		if (this.HasHideStats(entity))
		{
			this.UpdateNumberText(this.m_armorTextMesh, "", true);
			return;
		}
		int armor = entity.GetArmor();
		if (armor == 0)
		{
			this.UpdateNumberText(this.m_armorTextMesh, "", true);
			return;
		}
		this.UpdateNumberText(this.m_armorTextMesh, Convert.ToString(armor));
	}

	// Token: 0x06007205 RID: 29189 RVA: 0x0024B30F File Offset: 0x0024950F
	public void SetCardDefPowerTextOverride(string text)
	{
		this.m_cardDefPowerTextOverride = text;
	}

	// Token: 0x06007206 RID: 29190 RVA: 0x0024B318 File Offset: 0x00249518
	public void UpdatePowersText()
	{
		if (this.m_powersTextMesh == null)
		{
			return;
		}
		string text;
		if (this.ShouldUseEntityDefForPowersText())
		{
			text = (string.IsNullOrEmpty(this.m_cardDefPowerTextOverride) ? this.m_entityDef.GetCardTextInHand() : this.m_cardDefPowerTextOverride);
		}
		else
		{
			if (this.m_entity.IsSecret() && this.m_entity.IsHidden() && this.m_entity.IsControlledByConcealedPlayer())
			{
				text = GameStrings.Get("GAMEPLAY_SECRET_DESC");
			}
			else if (this.m_entity.IsHistoryDupe())
			{
				text = this.m_entity.GetCardTextInHistory();
			}
			else
			{
				text = this.m_entity.GetCardTextInHand();
			}
			if (GameState.Get() != null && GameState.Get().GetGameEntity() != null)
			{
				text = GameState.Get().GetGameEntity().UpdateCardText(this.m_card, this, text);
			}
		}
		this.UpdateText(this.m_powersTextMesh, text);
	}

	// Token: 0x06007207 RID: 29191 RVA: 0x0024B3F5 File Offset: 0x002495F5
	private bool ShouldUseEntityDefForPowersText()
	{
		return this.m_entityDef != null && (this.m_entity == null || !this.m_entity.GetCardTextBuilder().ShouldUseEntityForTextInPlay());
	}

	// Token: 0x06007208 RID: 29192 RVA: 0x0024B420 File Offset: 0x00249620
	private void UpdateNumberText(UberText textMesh, string newText)
	{
		this.UpdateNumberText(textMesh, newText, false);
	}

	// Token: 0x06007209 RID: 29193 RVA: 0x0024B42C File Offset: 0x0024962C
	private void UpdateNumberText(UberText textMesh, string newText, bool shouldHide)
	{
		GemObject gemObject = SceneUtils.FindComponentInThisOrParents<GemObject>(textMesh.gameObject);
		if (gemObject != null)
		{
			if (!gemObject.IsNumberHidden())
			{
				if (shouldHide)
				{
					textMesh.gameObject.SetActive(false);
					if (this.GetHistoryCard() != null || this.GetHistoryChildCard() != null)
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
				textMesh.gameObject.SetActive(true);
				gemObject.SetToZeroThenEnlarge();
			}
			gemObject.Initialize();
			gemObject.SetHideNumberFlag(shouldHide);
		}
		textMesh.Text = newText;
	}

	// Token: 0x0600720A RID: 29194 RVA: 0x0024B4D0 File Offset: 0x002496D0
	public void UpdateNameText()
	{
		if (this.m_nameTextMesh == null)
		{
			return;
		}
		string text = "";
		bool flag = false;
		if (this.m_entity != null)
		{
			if (this.m_entityDef == null)
			{
				flag = (this.m_entity.IsSecret() && this.m_entity.IsHidden() && this.m_entity.IsControlledByConcealedPlayer());
			}
			text = this.m_entity.GetName();
		}
		else if (this.m_entityDef != null)
		{
			string shortName = this.m_entityDef.GetShortName();
			text = ((this.m_useShortName && shortName != null) ? shortName : this.m_entityDef.GetName());
		}
		if (flag)
		{
			if (GameState.Get().GetBooleanGameOption(GameEntityOption.USE_SECRET_CLASS_NAMES))
			{
				switch (this.m_entity.GetClass())
				{
				case TAG_CLASS.HUNTER:
					text = GameStrings.Get("GAMEPLAY_SECRET_NAME_HUNTER");
					goto IL_116;
				case TAG_CLASS.MAGE:
					text = GameStrings.Get("GAMEPLAY_SECRET_NAME_MAGE");
					goto IL_116;
				case TAG_CLASS.PALADIN:
					text = GameStrings.Get("GAMEPLAY_SECRET_NAME_PALADIN");
					goto IL_116;
				case TAG_CLASS.ROGUE:
					text = GameStrings.Get("GAMEPLAY_SECRET_NAME_ROGUE");
					goto IL_116;
				}
				text = GameStrings.Get("GAMEPLAY_SECRET_NAME");
			}
			else
			{
				text = GameStrings.Get("GAMEPLAY_SECRET_NAME");
			}
		}
		IL_116:
		this.UpdateText(this.m_nameTextMesh, text);
	}

	// Token: 0x0600720B RID: 29195 RVA: 0x0024B600 File Offset: 0x00249800
	private void UpdateSecretAndQuestText()
	{
		if (!this.m_secretText)
		{
			return;
		}
		string text = "?";
		if (this.m_entity != null)
		{
			if (this.m_entity.IsQuest() || this.m_entity.IsSideQuest())
			{
				text = "!";
			}
			else if (this.m_entity.IsPuzzle())
			{
				text = "P";
			}
		}
		if (UniversalInputManager.UsePhoneUI && this.m_entity != null)
		{
			TransformUtil.SetLocalPosZ(this.m_secretText, -0.01f);
			Player controller = this.m_entity.GetController();
			if (controller != null && this.m_entity.IsSecret())
			{
				ZoneSecret secretZone = controller.GetSecretZone();
				if (secretZone)
				{
					int secretCount = secretZone.GetSecretCount();
					if (secretCount > 1)
					{
						text = secretCount.ToString();
						TransformUtil.SetLocalPosZ(this.m_secretText, -0.03f);
					}
				}
			}
			else if (controller != null && this.m_entity.IsSideQuest())
			{
				TransformUtil.SetLocalPosZ(this.m_secretText, 0.01f);
				ZoneSecret secretZone2 = controller.GetSecretZone();
				if (secretZone2)
				{
					int sideQuestCount = secretZone2.GetSideQuestCount();
					if (sideQuestCount > 1)
					{
						text = sideQuestCount.ToString();
						TransformUtil.SetLocalPosZ(this.m_secretText, -0.02f);
					}
				}
			}
			Transform transform = this.m_secretText.transform.parent.Find("Secret_mesh");
			if (transform != null && transform.gameObject != null)
			{
				SphereCollider component = transform.gameObject.GetComponent<SphereCollider>();
				if (component != null)
				{
					component.radius = 0.5f;
				}
			}
		}
		this.UpdateText(this.m_secretText, text);
	}

	// Token: 0x0600720C RID: 29196 RVA: 0x0024B793 File Offset: 0x00249993
	private void UpdateText(UberText uberTextMesh, string text)
	{
		if (uberTextMesh == null)
		{
			return;
		}
		uberTextMesh.Text = text;
	}

	// Token: 0x0600720D RID: 29197 RVA: 0x0024B7A6 File Offset: 0x002499A6
	private void UpdateTextColor(UberText originalMesh, int defNumber, int currentNumber)
	{
		this.UpdateTextColor(originalMesh, defNumber, currentNumber, false);
	}

	// Token: 0x0600720E RID: 29198 RVA: 0x0024B7B4 File Offset: 0x002499B4
	private void UpdateTextColor(UberText uberTextMesh, int defNumber, int currentNumber, bool higherIsBetter)
	{
		if ((defNumber > currentNumber && higherIsBetter) || (defNumber < currentNumber && !higherIsBetter))
		{
			uberTextMesh.TextColor = Color.green;
			return;
		}
		if ((defNumber >= currentNumber || !higherIsBetter) && (defNumber <= currentNumber || higherIsBetter))
		{
			if (defNumber == currentNumber)
			{
				uberTextMesh.TextColor = Color.white;
			}
			return;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			uberTextMesh.TextColor = new Color(1f, 0.19607843f, 0.19607843f);
			return;
		}
		uberTextMesh.TextColor = Color.red;
	}

	// Token: 0x0600720F RID: 29199 RVA: 0x0024B831 File Offset: 0x00249A31
	private void UpdateTextColorToGreenOrWhite(UberText uberTextMesh, int defNumber, int currentNumber)
	{
		if (defNumber < currentNumber)
		{
			uberTextMesh.TextColor = Color.green;
			return;
		}
		uberTextMesh.TextColor = Color.white;
	}

	// Token: 0x06007210 RID: 29200 RVA: 0x0024B84E File Offset: 0x00249A4E
	private void DisableTextMesh(UberText mesh)
	{
		if (mesh == null)
		{
			return;
		}
		mesh.gameObject.SetActive(false);
	}

	// Token: 0x06007211 RID: 29201 RVA: 0x0024B866 File Offset: 0x00249A66
	public void SetUseShortName(bool useShortName)
	{
		this.m_useShortName = useShortName;
	}

	// Token: 0x06007212 RID: 29202 RVA: 0x0024B870 File Offset: 0x00249A70
	public void OverrideNameText(UberText newText)
	{
		if (this.m_nameTextMesh != null)
		{
			this.m_nameTextMesh.gameObject.SetActive(false);
		}
		this.m_nameTextMesh = newText;
		this.UpdateNameText();
		if (this.m_shown && newText != null)
		{
			newText.gameObject.SetActive(true);
		}
	}

	// Token: 0x06007213 RID: 29203 RVA: 0x0024B8C6 File Offset: 0x00249AC6
	public void HideAllText()
	{
		this.ToggleTextVisibility(false);
	}

	// Token: 0x06007214 RID: 29204 RVA: 0x0024B8CF File Offset: 0x00249ACF
	public void ShowAllText()
	{
		this.ToggleTextVisibility(true);
	}

	// Token: 0x06007215 RID: 29205 RVA: 0x0024B8D8 File Offset: 0x00249AD8
	private void ToggleTextVisibility(bool bOn)
	{
		if (this.m_healthTextMesh != null)
		{
			this.m_healthTextMesh.gameObject.SetActive(bOn);
		}
		if (this.m_armorTextMesh != null)
		{
			this.m_armorTextMesh.gameObject.SetActive(bOn);
		}
		if (this.m_attackTextMesh != null)
		{
			this.m_attackTextMesh.gameObject.SetActive(bOn);
		}
		if (this.m_nameTextMesh != null)
		{
			this.m_nameTextMesh.gameObject.SetActive(bOn);
			if (this.m_nameTextMesh.RenderOnObject)
			{
				this.m_nameTextMesh.RenderOnObject.GetComponent<Renderer>().enabled = bOn;
			}
		}
		if (this.m_powersTextMesh != null)
		{
			this.m_powersTextMesh.gameObject.SetActive(bOn);
		}
		if (this.m_costTextMesh != null)
		{
			this.m_costTextMesh.gameObject.SetActive(bOn);
		}
		if (this.m_raceTextMesh != null)
		{
			this.m_raceTextMesh.gameObject.SetActive(bOn);
		}
		if (this.m_secretText)
		{
			this.m_secretText.gameObject.SetActive(bOn);
		}
	}

	// Token: 0x06007216 RID: 29206 RVA: 0x0024BA04 File Offset: 0x00249C04
	public void CreateBannedRibbon()
	{
		if (this.m_bannedRibbonContainer != null)
		{
			this.m_bannedRibbonContainer.gameObject.SetActive(true);
			this.m_bannedRibbon = this.m_bannedRibbonContainer.PrefabGameObject(true);
		}
	}

	// Token: 0x06007217 RID: 29207 RVA: 0x0024BA37 File Offset: 0x00249C37
	public bool IsContactShadowEnabled()
	{
		return this.m_shadowVisible;
	}

	// Token: 0x06007218 RID: 29208 RVA: 0x0024BA3F File Offset: 0x00249C3F
	public bool HasContactShadowObject()
	{
		return this.m_shadowObject != null || this.m_uniqueShadowObject != null;
	}

	// Token: 0x06007219 RID: 29209 RVA: 0x0024BA5D File Offset: 0x00249C5D
	public void ContactShadow(bool visible)
	{
		this.m_shadowVisible = visible;
		if (!this.m_shadowObjectInitialized)
		{
			this.CacheShadowObjects();
		}
		this.UpdateContactShadow();
	}

	// Token: 0x0600721A RID: 29210 RVA: 0x0024BA7C File Offset: 0x00249C7C
	public void UpdateContactShadow()
	{
		Renderer renderer = (this.m_shadowObject != null) ? this.m_shadowObject.GetComponent<Renderer>() : null;
		Renderer renderer2 = (this.m_uniqueShadowObject != null) ? this.m_uniqueShadowObject.GetComponent<Renderer>() : null;
		if (this.m_shadowVisible && this.m_shown)
		{
			if (this.IsElite())
			{
				if (renderer != null)
				{
					renderer.enabled = false;
				}
				if (renderer2 != null)
				{
					renderer2.enabled = true;
					return;
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
					return;
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

	// Token: 0x0600721B RID: 29211 RVA: 0x0024BB40 File Offset: 0x00249D40
	public void MoveShadowToMissingCard(bool reset, int renderQueue = 0)
	{
		Transform transform;
		if (reset && this.m_cardMesh != null)
		{
			transform = this.m_cardMesh.transform;
		}
		else
		{
			if (reset || !(this.m_missingCardEffect != null))
			{
				return;
			}
			transform = this.m_missingCardEffect.transform;
		}
		bool flag = this.IsElite();
		GameObject gameObject = flag ? this.m_uniqueShadowObject : this.m_shadowObject;
		Renderer renderer = (gameObject != null) ? gameObject.GetComponent<Renderer>() : null;
		if (renderer == null)
		{
			return;
		}
		int num = flag ? this.m_initialUniqueShadowRenderQueue : this.m_initialShadowRenderQueue;
		int renderQueue2 = reset ? num : (renderer.GetMaterial().renderQueue + renderQueue);
		gameObject.transform.parent = transform;
		renderer.GetMaterial().renderQueue = renderQueue2;
	}

	// Token: 0x0600721C RID: 29212 RVA: 0x0024BC06 File Offset: 0x00249E06
	public void UpdateMeshComponents()
	{
		this.UpdateRarityComponent();
		this.UpdateDescriptionMesh();
		this.UpdateEliteComponent();
		this.UpdatePremiumComponents();
		this.UpdateCardColor();
	}

	// Token: 0x0600721D RID: 29213 RVA: 0x0024BC28 File Offset: 0x00249E28
	private void UpdateRarityComponent()
	{
		if (!this.m_rarityGemMesh)
		{
			return;
		}
		UnityEngine.Vector2 mainTextureOffset;
		Color value;
		bool rarityTextureOffset = this.GetRarityTextureOffset(out mainTextureOffset, out value);
		SceneUtils.EnableRenderers(this.m_rarityGemMesh, rarityTextureOffset, true);
		if (this.m_rarityFrameMesh)
		{
			SceneUtils.EnableRenderers(this.m_rarityFrameMesh, rarityTextureOffset, true);
		}
		if (!rarityTextureOffset)
		{
			return;
		}
		Material materialInstance = Actor.GetMaterialInstance(this.m_rarityGemMesh.GetComponent<Renderer>());
		materialInstance.mainTextureOffset = mainTextureOffset;
		materialInstance.SetColor("_tint", value);
	}

	// Token: 0x0600721E RID: 29214 RVA: 0x0024BC9C File Offset: 0x00249E9C
	private bool GetRarityTextureOffset(out UnityEngine.Vector2 offset, out Color tint)
	{
		offset = this.GEM_TEXTURE_OFFSET_COMMON;
		tint = this.GEM_COLOR_COMMON;
		if (this.m_entityDef == null && this.m_entity == null)
		{
			return false;
		}
		TAG_CARD_SET cardSet;
		if (this.m_entityDef != null)
		{
			cardSet = this.m_entityDef.GetCardSet();
		}
		else
		{
			cardSet = this.m_entity.GetCardSet();
		}
		if (cardSet == TAG_CARD_SET.MISSIONS)
		{
			return false;
		}
		switch (this.GetRarity())
		{
		case TAG_RARITY.COMMON:
			offset = this.GEM_TEXTURE_OFFSET_COMMON;
			tint = this.GEM_COLOR_COMMON;
			return true;
		case TAG_RARITY.RARE:
			offset = this.GEM_TEXTURE_OFFSET_RARE;
			tint = this.GEM_COLOR_RARE;
			return true;
		case TAG_RARITY.EPIC:
			offset = this.GEM_TEXTURE_OFFSET_EPIC;
			tint = this.GEM_COLOR_EPIC;
			return true;
		case TAG_RARITY.LEGENDARY:
			offset = this.GEM_TEXTURE_OFFSET_LEGENDARY;
			tint = this.GEM_COLOR_LEGENDARY;
			return true;
		}
		return false;
	}

	// Token: 0x0600721F RID: 29215 RVA: 0x0024BD8C File Offset: 0x00249F8C
	private void UpdateDescriptionMesh()
	{
		bool flag = true;
		if (this.m_premiumType == TAG_PREMIUM.NORMAL)
		{
			EntityBase entityBase = this.m_entity ?? this.m_entityDef;
			if (entityBase != null && entityBase.IsWeapon() && entityBase.GetClass() == TAG_CLASS.DEATHKNIGHT)
			{
				flag = false;
			}
		}
		if (this.m_descriptionMesh != null)
		{
			Renderer component = this.m_descriptionMesh.GetComponent<Renderer>();
			if (component != null)
			{
				component.enabled = flag;
			}
		}
		if (this.m_descriptionTrimMesh != null)
		{
			Renderer component = this.m_descriptionTrimMesh.GetComponent<Renderer>();
			if (component != null)
			{
				component.enabled = flag;
			}
		}
		if (flag)
		{
			this.UpdateWatermark();
		}
	}

	// Token: 0x06007220 RID: 29216 RVA: 0x0024BE28 File Offset: 0x0024A028
	private void UpdateWatermark()
	{
		if (this.m_entityDef == null && this.m_entity == null)
		{
			return;
		}
		string text = null;
		string watermarkTextureOverride = (this.m_entityDef ?? this.m_entity.GetEntityDef()).GetWatermarkTextureOverride();
		TAG_CARD_SET cardSetId = this.GetCardSet();
		if (this.m_watermarkCardSetOverride != TAG_CARD_SET.INVALID)
		{
			cardSetId = this.m_watermarkCardSetOverride;
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
		float a;
		if ((this.m_entityDef != null && this.m_entityDef.HasTag(GAME_TAG.HIDE_WATERMARK)) || (this.m_entity != null && this.m_entity.HasTag(GAME_TAG.HIDE_WATERMARK)))
		{
			a = 0f;
		}
		else
		{
			a = this.WATERMARK_ALPHA_VALUE;
		}
		if (this.m_descriptionMesh != null && this.m_descriptionMesh.GetComponent<Renderer>() != null && this.m_descriptionMesh.GetComponent<Renderer>().GetSharedMaterial().HasProperty("_SecondTint") && this.m_descriptionMesh.GetComponent<Renderer>().GetSharedMaterial().HasProperty("_SecondTex"))
		{
			if (!string.IsNullOrEmpty(text))
			{
				AssetLoader.Get().LoadAsset<Texture>(ref this.m_watermarkTex, text, AssetLoadingOptions.None);
				Actor.GetMaterialInstance(this.m_descriptionMesh.GetComponent<Renderer>()).SetTexture("_SecondTex", this.m_watermarkTex);
			}
			else
			{
				a = 0f;
			}
			Material materialInstance = Actor.GetMaterialInstance(this.m_descriptionMesh.GetComponent<Renderer>());
			Color color = materialInstance.GetColor("_SecondTint");
			color.a = a;
			materialInstance.SetColor("_SecondTint", color);
		}
		if (this.m_watermarkMesh != null && this.m_watermarkMesh.GetComponent<Renderer>() != null && this.m_watermarkMesh.GetComponent<Renderer>().GetSharedMaterial().HasProperty("_Color") && this.m_watermarkMesh.GetComponent<Renderer>().GetSharedMaterial().HasProperty("_MainTex"))
		{
			if (!string.IsNullOrEmpty(text))
			{
				AssetLoader.Get().LoadAsset<Texture>(ref this.m_watermarkTex, text, AssetLoadingOptions.None);
				Actor.GetMaterialInstance(this.m_watermarkMesh.GetComponent<Renderer>()).SetTexture("_MainTex", this.m_watermarkTex);
			}
			else
			{
				a = 0f;
			}
			Material materialInstance2 = Actor.GetMaterialInstance(this.m_watermarkMesh.GetComponent<Renderer>());
			Color color2 = materialInstance2.GetColor("_Color");
			color2.a = a;
			materialInstance2.SetColor("_Color", color2);
		}
	}

	// Token: 0x06007221 RID: 29217 RVA: 0x0024C0AC File Offset: 0x0024A2AC
	private void UpdateEliteComponent()
	{
		if (this.m_eliteObject == null)
		{
			return;
		}
		bool enable = this.IsElite();
		SceneUtils.EnableRenderers(this.m_eliteObject, enable, true);
	}

	// Token: 0x06007222 RID: 29218 RVA: 0x0024C0DC File Offset: 0x0024A2DC
	private void UpdatePremiumComponents()
	{
		if (this.m_premiumType == TAG_PREMIUM.NORMAL)
		{
			return;
		}
		if (this.m_glints == null)
		{
			return;
		}
		this.m_glints.SetActive(true);
		Renderer[] componentsInChildren = this.m_glints.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = true;
		}
	}

	// Token: 0x06007223 RID: 29219 RVA: 0x0024C130 File Offset: 0x0024A330
	private void UpdateRace(string raceText)
	{
		if (this.m_entityDef == null && this.m_entity == null)
		{
			return;
		}
		bool flag = (this.m_entity != null) ? this.m_entity.IsMinion() : this.m_entityDef.IsMinion();
		bool flag2 = (this.m_entity != null) ? this.m_entity.IsSpell() : this.m_entityDef.IsSpell();
		bool flag3 = (this.m_entity != null) ? this.m_entity.IsWeapon() : this.m_entityDef.IsWeapon();
		bool flag4 = (this.m_entity != null) ? this.m_entity.IsHero() : this.m_entityDef.IsHero();
		if (flag && this.m_racePlateObject == null)
		{
			return;
		}
		if (flag4)
		{
			return;
		}
		if (flag2 && (this.m_descriptionMesh == null || this.m_spellDescriptionMeshNeutral == null || this.m_spellDescriptionMeshSchool == null))
		{
			return;
		}
		bool flag5 = !string.IsNullOrEmpty(raceText);
		if (flag)
		{
			MeshRenderer[] components = this.m_racePlateObject.GetComponents<MeshRenderer>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].enabled = flag5;
			}
		}
		else if (flag2)
		{
			foreach (MeshFilter meshFilter in this.m_descriptionMesh.GetComponents<MeshFilter>())
			{
				if (flag5)
				{
					meshFilter.sharedMesh = this.m_spellDescriptionMeshSchool;
				}
				else
				{
					meshFilter.sharedMesh = this.m_spellDescriptionMeshNeutral;
				}
			}
		}
		if (flag5 || flag3)
		{
			if (this.m_descriptionMesh != null && this.m_descriptionMesh.GetComponent<Renderer>() != null)
			{
				Actor.GetMaterialInstance(this.m_descriptionMesh.GetComponent<Renderer>()).SetTextureOffset("_SecondTex", this.descriptionMesh_WithRace_TextureOffset);
			}
			if (this.m_watermarkMesh != null && this.m_watermarkMesh.GetComponent<Renderer>() != null)
			{
				Actor.GetMaterialInstance(this.m_watermarkMesh.GetComponent<Renderer>()).SetTextureOffset("_MainTex", this.descriptionMesh_WithRace_TextureOffset);
			}
		}
		else
		{
			if (this.m_descriptionMesh != null && this.m_descriptionMesh.GetComponent<Renderer>() != null)
			{
				Actor.GetMaterialInstance(this.m_descriptionMesh.GetComponent<Renderer>()).SetTextureOffset("_SecondTex", this.descriptionMesh_WithoutRace_TextureOffset);
			}
			if (this.m_watermarkMesh != null && this.m_watermarkMesh.GetComponent<Renderer>() != null)
			{
				Actor.GetMaterialInstance(this.m_watermarkMesh.GetComponent<Renderer>()).SetTextureOffset("_MainTex", this.descriptionMesh_WithoutRace_TextureOffset);
			}
		}
		if (this.m_raceTextMesh == null)
		{
			return;
		}
		if (Localization.GetLocale() == Locale.thTH)
		{
			this.m_raceTextMesh.ResizeToFit = false;
			this.m_raceTextMesh.ResizeToFitAndGrow = false;
		}
		this.m_raceTextMesh.Text = raceText;
	}

	// Token: 0x06007224 RID: 29220 RVA: 0x0024C3F0 File Offset: 0x0024A5F0
	private static Material GetMaterialInstance(Renderer r)
	{
		return r.GetMaterial();
	}

	// Token: 0x06007225 RID: 29221 RVA: 0x0024C3F8 File Offset: 0x0024A5F8
	public MultiClassBannerTransition GetMultiClassBanner()
	{
		return this.m_multiClassBanner;
	}

	// Token: 0x06007226 RID: 29222 RVA: 0x0024C400 File Offset: 0x0024A600
	public void UpdateCardColor()
	{
		if (this.m_legacyPortraitMaterialIndex < 0 && this.m_cardMesh == null)
		{
			return;
		}
		if (this.GetEntityDef() == null && this.GetEntity() == null)
		{
			return;
		}
		int num = 0;
		this.m_usesMultiClassBanner = false;
		TAG_CARDTYPE cardType;
		TAG_CLASS tag_CLASS;
		bool flag;
		if (this.m_entityDef != null)
		{
			cardType = this.m_entityDef.GetCardType();
			tag_CLASS = this.m_entityDef.GetClass();
			flag = this.m_entityDef.IsMultiClass();
			num = this.m_entityDef.GetTag(GAME_TAG.MULTI_CLASS_GROUP);
		}
		else if (this.m_entity != null)
		{
			cardType = this.m_entity.GetCardType();
			tag_CLASS = this.m_entity.GetClass();
			flag = this.m_entity.IsMultiClass();
			num = this.m_entity.GetTag(GAME_TAG.MULTI_CLASS_GROUP);
		}
		else
		{
			cardType = TAG_CARDTYPE.INVALID;
			tag_CLASS = TAG_CLASS.INVALID;
			flag = false;
			num = 0;
		}
		Color cardColor = Color.magenta;
		CardColorSwitcher.CardColorType colorType;
		switch (tag_CLASS)
		{
		case TAG_CLASS.DEATHKNIGHT:
			colorType = CardColorSwitcher.CardColorType.TYPE_DEATHKNIGHT;
			cardColor = this.CLASS_COLOR_DEATHKNIGHT;
			goto IL_1BC;
		case TAG_CLASS.DRUID:
			colorType = CardColorSwitcher.CardColorType.TYPE_DRUID;
			cardColor = this.CLASS_COLOR_DRUID;
			goto IL_1BC;
		case TAG_CLASS.HUNTER:
			colorType = CardColorSwitcher.CardColorType.TYPE_HUNTER;
			cardColor = this.CLASS_COLOR_HUNTER;
			goto IL_1BC;
		case TAG_CLASS.MAGE:
			colorType = CardColorSwitcher.CardColorType.TYPE_MAGE;
			cardColor = this.CLASS_COLOR_MAGE;
			goto IL_1BC;
		case TAG_CLASS.PALADIN:
			colorType = CardColorSwitcher.CardColorType.TYPE_PALADIN;
			cardColor = this.CLASS_COLOR_PALADIN;
			goto IL_1BC;
		case TAG_CLASS.PRIEST:
			colorType = CardColorSwitcher.CardColorType.TYPE_PRIEST;
			cardColor = this.CLASS_COLOR_PRIEST;
			goto IL_1BC;
		case TAG_CLASS.ROGUE:
			colorType = CardColorSwitcher.CardColorType.TYPE_ROGUE;
			cardColor = this.CLASS_COLOR_ROGUE;
			goto IL_1BC;
		case TAG_CLASS.SHAMAN:
			colorType = CardColorSwitcher.CardColorType.TYPE_SHAMAN;
			cardColor = this.CLASS_COLOR_SHAMAN;
			goto IL_1BC;
		case TAG_CLASS.WARLOCK:
			colorType = CardColorSwitcher.CardColorType.TYPE_WARLOCK;
			cardColor = this.CLASS_COLOR_WARLOCK;
			goto IL_1BC;
		case TAG_CLASS.WARRIOR:
			colorType = CardColorSwitcher.CardColorType.TYPE_WARRIOR;
			cardColor = this.CLASS_COLOR_WARRIOR;
			goto IL_1BC;
		case TAG_CLASS.DREAM:
			colorType = CardColorSwitcher.CardColorType.TYPE_HUNTER;
			cardColor = this.CLASS_COLOR_HUNTER;
			goto IL_1BC;
		case TAG_CLASS.DEMONHUNTER:
			colorType = CardColorSwitcher.CardColorType.TYPE_DEMONHUNTER;
			cardColor = this.CLASS_COLOR_DEMONHUNTER;
			goto IL_1BC;
		}
		colorType = CardColorSwitcher.CardColorType.TYPE_GENERIC;
		colorType = CardColorSwitcher.CardColorType.TYPE_GENERIC;
		cardColor = this.CLASS_COLOR_GENERIC;
		IL_1BC:
		if (flag)
		{
			colorType = CardColorSwitcher.CardColorType.TYPE_GENERIC;
			MultiClassGroupDbfRecord record = GameDbf.MultiClassGroup.GetRecord(num);
			if (record != null)
			{
				colorType = (CardColorSwitcher.CardColorType)record.CardColorType;
			}
			if (record != null && !string.IsNullOrEmpty(record.IconAssetPath) && this.m_multiClassBannerContainer != null)
			{
				this.m_usesMultiClassBanner = true;
				this.m_multiClassBannerContainer.gameObject.SetActive(true);
				this.m_multiClassBanner = this.m_multiClassBannerContainer.PrefabGameObject(true).GetComponent<MultiClassBannerTransition>();
				if (this.m_multiClassBanner != null)
				{
					IEnumerable<TAG_CLASS> enumerable;
					if (this.m_entityDef != null)
					{
						enumerable = this.m_entityDef.GetClasses(new Comparison<TAG_CLASS>(MultiClassBannerTransition.CompareClasses));
					}
					else
					{
						if (this.m_entity != null)
						{
							enumerable = this.m_entity.GetClasses(new Comparison<TAG_CLASS>(MultiClassBannerTransition.CompareClasses));
							if (this.m_entity.GetZone() != TAG_ZONE.HAND || this.m_entity.IsHistoryDupe())
							{
								goto IL_302;
							}
							using (IEnumerator<TAG_CLASS> enumerator = enumerable.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									TAG_CLASS tag_CLASS2 = enumerator.Current;
									if (tag_CLASS2 == this.m_entity.GetHero().GetClass())
									{
										enumerable = new TAG_CLASS[]
										{
											tag_CLASS2
										};
										break;
									}
								}
								goto IL_302;
							}
						}
						enumerable = new List<TAG_CLASS>();
					}
					IL_302:
					this.m_multiClassBanner.SetClasses(enumerable);
					this.m_multiClassBanner.SetMultiClassGroup(num);
					if (this.m_premiumType >= TAG_PREMIUM.GOLDEN)
					{
						this.m_multiClassBanner.SetGoldenCardMesh(this.m_cardMesh, this.m_premiumRibbon);
					}
					Vector3 localPosition = this.m_manaObject.transform.localPosition;
					Vector3 localPosition2 = this.m_costTextMesh.transform.localPosition;
					localPosition.y = 0.027f;
					localPosition2.y = 0.088f;
					this.m_manaObject.transform.localPosition = localPosition;
					this.m_costTextMesh.transform.localPosition = localPosition2;
				}
			}
		}
		else
		{
			if (this.m_premiumRibbon > -1 && this.m_initialPremiumRibbonMaterial != null)
			{
				Renderer component = this.m_cardMesh.GetComponent<Renderer>();
				if (this.m_premiumRibbon < component.GetMaterials().Count)
				{
					component.SetMaterial(this.m_premiumRibbon, this.m_initialPremiumRibbonMaterial);
				}
			}
			if (this.m_multiClassBannerContainer != null)
			{
				this.m_multiClassBannerContainer.gameObject.SetActive(false);
			}
		}
		this.SetMaterial(cardType, colorType, cardColor);
	}

	// Token: 0x06007227 RID: 29223 RVA: 0x0024C830 File Offset: 0x0024AA30
	private void SetMaterial(TAG_CARDTYPE cardType, CardColorSwitcher.CardColorType colorType, Color cardColor)
	{
		switch (this.m_premiumType)
		{
		case TAG_PREMIUM.NORMAL:
			this.SetMaterialNormal(cardType, colorType, cardColor);
			return;
		case TAG_PREMIUM.GOLDEN:
			this.SetMaterialGolden(cardType, colorType, cardColor);
			return;
		case TAG_PREMIUM.DIAMOND:
			this.SetMaterialGolden(cardType, colorType, cardColor);
			return;
		default:
			Debug.LogWarning(string.Format("Actor.SetMaterial(): unexpected premium type {0}", this.m_premiumType));
			return;
		}
	}

	// Token: 0x06007228 RID: 29224 RVA: 0x0024C890 File Offset: 0x0024AA90
	private void SetHistoryHeroBannerColor()
	{
		if (this.m_entity == null)
		{
			return;
		}
		if (this.m_entity.IsControlledByFriendlySidePlayer())
		{
			return;
		}
		if (!this.m_entity.IsHistoryDupe())
		{
			return;
		}
		Transform transform = this.GetRootObject().transform.Find("History_Hero_Banner");
		if (transform == null)
		{
			return;
		}
		Actor.GetMaterialInstance(transform.GetComponent<Renderer>()).mainTextureOffset = new UnityEngine.Vector2(0.005f, -0.505f);
	}

	// Token: 0x06007229 RID: 29225 RVA: 0x0024C904 File Offset: 0x0024AB04
	private void GetDualClassColors(CardColorSwitcher.CardColorType dualClassCombo, out Color left, out Color right)
	{
		switch (dualClassCombo)
		{
		case CardColorSwitcher.CardColorType.TYPE_PALADIN_PRIEST:
			left = this.CLASS_COLOR_PALADIN;
			right = this.CLASS_COLOR_PRIEST;
			return;
		case CardColorSwitcher.CardColorType.TYPE_WARLOCK_PRIEST:
			left = this.CLASS_COLOR_PRIEST;
			right = this.CLASS_COLOR_WARLOCK;
			return;
		case CardColorSwitcher.CardColorType.TYPE_WARLOCK_DEMONHUNTER:
			left = this.CLASS_COLOR_WARLOCK;
			right = this.CLASS_COLOR_DEMONHUNTER;
			return;
		case CardColorSwitcher.CardColorType.TYPE_HUNTER_DEMONHUNTER:
			left = this.CLASS_COLOR_DEMONHUNTER;
			right = this.CLASS_COLOR_HUNTER;
			return;
		case CardColorSwitcher.CardColorType.TYPE_DRUID_HUNTER:
			left = this.CLASS_COLOR_HUNTER;
			right = this.CLASS_COLOR_DRUID;
			return;
		case CardColorSwitcher.CardColorType.TYPE_DRUID_SHAMAN:
			left = this.CLASS_COLOR_DRUID;
			right = this.CLASS_COLOR_SHAMAN;
			return;
		case CardColorSwitcher.CardColorType.TYPE_SHAMAN_MAGE:
			left = this.CLASS_COLOR_SHAMAN;
			right = this.CLASS_COLOR_MAGE;
			return;
		case CardColorSwitcher.CardColorType.TYPE_MAGE_ROGUE:
			left = this.CLASS_COLOR_MAGE;
			right = this.CLASS_COLOR_ROGUE;
			return;
		case CardColorSwitcher.CardColorType.TYPE_WARRIOR_ROGUE:
			left = this.CLASS_COLOR_ROGUE;
			right = this.CLASS_COLOR_WARRIOR;
			return;
		case CardColorSwitcher.CardColorType.TYPE_WARRIOR_PALADIN:
			left = this.CLASS_COLOR_WARRIOR;
			right = this.CLASS_COLOR_PALADIN;
			return;
		default:
			left = (right = Color.magenta);
			return;
		}
	}

	// Token: 0x0600722A RID: 29226 RVA: 0x0024CA58 File Offset: 0x0024AC58
	private void SetMaterialGolden(TAG_CARDTYPE cardType, CardColorSwitcher.CardColorType colorType, Color cardColor)
	{
		if (this.m_cardMesh != null && this.m_premiumRibbon >= 0)
		{
			Material material = this.m_cardMesh.GetComponent<Renderer>().GetMaterial(this.m_premiumRibbon);
			if (colorType >= CardColorSwitcher.CardColorType.TYPE_GENERIC && colorType <= CardColorSwitcher.CardColorType.TYPE_DEMONHUNTER)
			{
				material.color = cardColor;
				material.SetFloat("_EnableDualClass", 0f);
			}
			else
			{
				Color value;
				Color value2;
				this.GetDualClassColors(colorType, out value, out value2);
				material.SetFloat("_EnableDualClass", 1f);
				material.SetColor("_Color", value);
				material.SetColor("_SecondColor", value2);
			}
		}
		if (cardType == TAG_CARDTYPE.HERO)
		{
			this.SetHistoryHeroBannerColor();
		}
	}

	// Token: 0x0600722B RID: 29227 RVA: 0x0024CAF4 File Offset: 0x0024ACF4
	private void SetMaterialNormal(TAG_CARDTYPE cardType, CardColorSwitcher.CardColorType colorType, Color cardColor)
	{
		switch (cardType)
		{
		case TAG_CARDTYPE.HERO:
			this.SetMaterialHero(colorType);
			break;
		case TAG_CARDTYPE.MINION:
			this.SetMaterialWithTexture(cardType, colorType);
			return;
		case TAG_CARDTYPE.SPELL:
			this.SetMaterialWithTexture(cardType, colorType);
			return;
		case TAG_CARDTYPE.ENCHANTMENT:
			break;
		case TAG_CARDTYPE.WEAPON:
			this.SetMaterialWeapon(colorType, cardColor);
			return;
		default:
			return;
		}
	}

	// Token: 0x0600722C RID: 29228 RVA: 0x0024CB40 File Offset: 0x0024AD40
	private void SetMaterialWithTexture(TAG_CARDTYPE cardType, CardColorSwitcher.CardColorType colorType)
	{
		if (CardColorSwitcher.Get() == null)
		{
			return;
		}
		AssetLoader.Get().LoadAsset<Texture>(ref this.m_cardColorTex, CardColorSwitcher.Get().GetTexture(cardType, colorType), AssetLoadingOptions.None);
		if (this.m_cardMesh)
		{
			if (this.m_cardFrontMatIdx > -1)
			{
				this.m_cardMesh.GetComponent<Renderer>().GetMaterial(this.m_cardFrontMatIdx).mainTexture = this.m_cardColorTex;
			}
			if ((cardType == TAG_CARDTYPE.SPELL || (cardType == TAG_CARDTYPE.WEAPON && colorType == CardColorSwitcher.CardColorType.TYPE_DEATHKNIGHT)) && this.m_portraitMesh && this.m_portraitFrameMatIdx > -1)
			{
				this.m_portraitMesh.GetComponent<Renderer>().GetMaterial(this.m_portraitFrameMatIdx).mainTexture = this.m_cardColorTex;
				return;
			}
		}
		else if (this.m_legacyCardColorMaterialIndex >= 0 && this.m_meshRenderer != null)
		{
			this.m_meshRenderer.GetMaterial(this.m_legacyCardColorMaterialIndex).mainTexture = this.m_cardColorTex;
		}
	}

	// Token: 0x0600722D RID: 29229 RVA: 0x0024CC37 File Offset: 0x0024AE37
	private void SetMaterialHero(CardColorSwitcher.CardColorType colorType)
	{
		this.SetMaterialWithTexture(TAG_CARDTYPE.HERO, colorType);
		this.SetHistoryHeroBannerColor();
	}

	// Token: 0x0600722E RID: 29230 RVA: 0x0024CC48 File Offset: 0x0024AE48
	private void SetMaterialWeapon(CardColorSwitcher.CardColorType colorType, Color cardColor)
	{
		if (CardColorSwitcher.Get() && !string.IsNullOrEmpty(CardColorSwitcher.Get().GetTexture(TAG_CARDTYPE.WEAPON, colorType)))
		{
			this.SetMaterialWithTexture(TAG_CARDTYPE.WEAPON, colorType);
			return;
		}
		if (this.m_descriptionTrimMesh)
		{
			Actor.GetMaterialInstance(this.m_descriptionTrimMesh.GetComponent<Renderer>()).SetColor("_Color", cardColor);
		}
	}

	// Token: 0x0600722F RID: 29231 RVA: 0x0024CCAC File Offset: 0x0024AEAC
	public bool UseTechLevelManaGem()
	{
		if (this.m_entity != null && !this.m_entity.IsMinion())
		{
			return false;
		}
		if (this.m_entityDef != null && !this.m_entityDef.IsMinion())
		{
			return false;
		}
		if (GameState.Get() == null)
		{
			return false;
		}
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		return gameEntity != null && gameEntity.HasTag(GAME_TAG.TECH_LEVEL_MANA_GEM);
	}

	// Token: 0x06007230 RID: 29232 RVA: 0x0024CD0B File Offset: 0x0024AF0B
	public bool UseCoinManaGem()
	{
		return GameState.Get() != null && GameState.Get().GetGameEntity() != null && GameState.Get().GetGameEntity().HasTag(GAME_TAG.COIN_MANA_GEM);
	}

	// Token: 0x06007231 RID: 29233 RVA: 0x0024CD36 File Offset: 0x0024AF36
	public bool UseCoinManaGemForChoiceCard()
	{
		return GameState.Get() != null && GameState.Get().GetGameEntity() != null && GameState.Get().GetGameEntity().HasTag(GAME_TAG.COIN_MANA_GEM_FOR_CHOICE_CARDS);
	}

	// Token: 0x06007232 RID: 29234 RVA: 0x0024CD61 File Offset: 0x0024AF61
	public HistoryCard GetHistoryCard()
	{
		if (base.transform.parent == null)
		{
			return null;
		}
		return base.transform.parent.gameObject.GetComponent<HistoryCard>();
	}

	// Token: 0x06007233 RID: 29235 RVA: 0x0024CD8D File Offset: 0x0024AF8D
	public HistoryChildCard GetHistoryChildCard()
	{
		if (base.transform.parent == null)
		{
			return null;
		}
		return base.transform.parent.gameObject.GetComponent<HistoryChildCard>();
	}

	// Token: 0x06007234 RID: 29236 RVA: 0x0024CDBC File Offset: 0x0024AFBC
	public void SetHistoryItem(HistoryItem card)
	{
		if (card == null)
		{
			base.transform.parent = null;
			return;
		}
		base.transform.parent = card.transform;
		TransformUtil.Identity(base.transform);
		if (this.m_rootObject != null)
		{
			TransformUtil.Identity(this.m_rootObject.transform);
		}
		this.m_entity = card.GetEntity();
		this.UpdateTextComponents(this.m_entity);
		this.UpdateMeshComponents();
		if (this.m_premiumType >= TAG_PREMIUM.GOLDEN && card.GetPortraitGoldenMaterial() != null && this.IsPremiumPortraitEnabled())
		{
			Material portraitGoldenMaterial = card.GetPortraitGoldenMaterial();
			this.SetPortraitMaterial(portraitGoldenMaterial);
		}
		else
		{
			Texture portraitTexture = card.GetPortraitTexture();
			this.SetPortraitTextureOverride(portraitTexture);
		}
		if (this.m_spellTable != null)
		{
			foreach (SpellTableEntry spellTableEntry in this.m_spellTable.m_Table)
			{
				Spell spell = spellTableEntry.m_Spell;
				if (!(spell == null))
				{
					spell.m_BlockServerEvents = false;
				}
			}
		}
	}

	// Token: 0x06007235 RID: 29237 RVA: 0x0024CEDC File Offset: 0x0024B0DC
	public SpellTable GetSpellTable()
	{
		return this.m_spellTable;
	}

	// Token: 0x06007236 RID: 29238 RVA: 0x0024CEE4 File Offset: 0x0024B0E4
	public Spell LoadSpell(SpellType spellType)
	{
		Spell spell = null;
		if (this.m_card != null)
		{
			spell = this.m_card.GetSpellTableOverride(spellType);
		}
		if (spell == null)
		{
			TAG_CARD_SET cardSet = this.GetCardSet();
			string cardSetSpellOverride = GameDbf.GetIndex().GetCardSetSpellOverride(cardSet, spellType);
			if (!string.IsNullOrEmpty(cardSetSpellOverride))
			{
				spell = SpellTable.InstantiateSpell(spellType, cardSetSpellOverride);
			}
		}
		if (spell == null && this.m_sharedSpellTable != null)
		{
			spell = this.m_sharedSpellTable.GetSpell(spellType);
		}
		if (spell == null)
		{
			return null;
		}
		if (this.m_localSpellTable.ContainsKey(spellType))
		{
			this.m_localSpellTable.Remove(spellType);
		}
		this.m_localSpellTable.Add(spellType, spell);
		Transform transform = spell.gameObject.transform;
		Transform transform2 = base.gameObject.transform;
		TransformUtil.AttachAndPreserveLocalTransform(transform, transform2);
		transform.localScale.Scale(this.m_sharedSpellTable.gameObject.transform.localScale);
		SceneUtils.SetLayer(spell.gameObject, (GameLayer)base.gameObject.layer);
		spell.OnLoad();
		if (this.m_actorStateMgr != null)
		{
			spell.AddStateStartedCallback(new Spell.StateStartedCallback(this.OnSpellStateStarted));
		}
		return spell;
	}

	// Token: 0x06007237 RID: 29239 RVA: 0x0024D010 File Offset: 0x0024B210
	public Spell GetLoadedSpell(SpellType spellType)
	{
		Spell spell = null;
		if (this.m_localSpellTable != null)
		{
			this.m_localSpellTable.TryGetValue(spellType, out spell);
		}
		if (spell == null)
		{
			spell = this.LoadSpell(spellType);
		}
		return spell;
	}

	// Token: 0x06007238 RID: 29240 RVA: 0x0024D048 File Offset: 0x0024B248
	public Spell ActivateTaunt()
	{
		this.DeactivateTaunt();
		if (this.GetEntity().IsStealthed() && !Options.Get().GetBool(Option.HAS_SEEN_STEALTH_TAUNTER, false))
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_STEALTH_TAUNT3_22"), "VO_INNKEEPER_STEALTH_TAUNT3_22.prefab:7ec7cc35d1556434ebca64bfe4e770cb", 0f, null, false);
			Options.Get().SetBool(Option.HAS_SEEN_STEALTH_TAUNTER, true);
		}
		if (this.m_premiumType == TAG_PREMIUM.GOLDEN)
		{
			if (this.GetEntity().IsStealthed() || this.GetEntity().IsTauntIgnored())
			{
				return this.ActivateSpellBirthState(SpellType.TAUNT_PREMIUM_STEALTH);
			}
			return this.ActivateSpellBirthState(SpellType.TAUNT_PREMIUM);
		}
		else if (this.m_premiumType == TAG_PREMIUM.DIAMOND)
		{
			if (this.GetEntity().IsStealthed() || this.GetEntity().IsTauntIgnored())
			{
				return this.ActivateSpellBirthState(SpellType.TAUNT_DIAMOND_STEALTH);
			}
			return this.ActivateSpellBirthState(SpellType.TAUNT_DIAMOND);
		}
		else
		{
			if (this.GetEntity().IsStealthed() || this.GetEntity().IsTauntIgnored())
			{
				return this.ActivateSpellBirthState(SpellType.TAUNT_STEALTH);
			}
			return this.ActivateSpellBirthState(SpellType.TAUNT);
		}
	}

	// Token: 0x06007239 RID: 29241 RVA: 0x0024D148 File Offset: 0x0024B348
	public void DeactivateTaunt()
	{
		if (this.IsSpellActive(SpellType.TAUNT))
		{
			this.ActivateSpellDeathState(SpellType.TAUNT);
		}
		if (this.IsSpellActive(SpellType.TAUNT_PREMIUM))
		{
			this.ActivateSpellDeathState(SpellType.TAUNT_PREMIUM);
		}
		if (this.IsSpellActive(SpellType.TAUNT_PREMIUM_STEALTH))
		{
			this.ActivateSpellDeathState(SpellType.TAUNT_PREMIUM_STEALTH);
		}
		if (this.IsSpellActive(SpellType.TAUNT_STEALTH))
		{
			this.ActivateSpellDeathState(SpellType.TAUNT_STEALTH);
		}
		if (this.IsSpellActive(SpellType.TAUNT_DIAMOND))
		{
			this.ActivateSpellDeathState(SpellType.TAUNT_DIAMOND);
		}
		if (this.IsSpellActive(SpellType.TAUNT_DIAMOND_STEALTH))
		{
			this.ActivateSpellDeathState(SpellType.TAUNT_DIAMOND_STEALTH);
		}
	}

	// Token: 0x0600723A RID: 29242 RVA: 0x0024D1CC File Offset: 0x0024B3CC
	public Spell GetSpell(SpellType spellType)
	{
		Spell result = null;
		if (this.m_useSharedSpellTable)
		{
			result = this.GetLoadedSpell(spellType);
		}
		else if (this.m_spellTable != null)
		{
			this.m_spellTable.FindSpell(spellType, out result);
		}
		return result;
	}

	// Token: 0x0600723B RID: 29243 RVA: 0x0024D20C File Offset: 0x0024B40C
	public Spell GetSpellIfLoaded(SpellType spellType)
	{
		Spell result = null;
		if (this.m_useSharedSpellTable)
		{
			this.GetSpellIfLoaded(spellType, out result);
		}
		else if (this.m_spellTable != null)
		{
			this.m_spellTable.FindSpell(spellType, out result);
		}
		return result;
	}

	// Token: 0x0600723C RID: 29244 RVA: 0x0024D24D File Offset: 0x0024B44D
	public bool GetSpellIfLoaded(SpellType spellType, out Spell result)
	{
		if (this.m_localSpellTable == null || !this.m_localSpellTable.ContainsKey(spellType))
		{
			result = null;
			return false;
		}
		result = this.m_localSpellTable[spellType];
		return result != null;
	}

	// Token: 0x0600723D RID: 29245 RVA: 0x0024D280 File Offset: 0x0024B480
	public Spell ActivateSpellBirthState(SpellType spellType)
	{
		Spell spell = this.GetSpell(spellType);
		if (spell == null)
		{
			return null;
		}
		spell.ActivateState(SpellStateType.BIRTH);
		return spell;
	}

	// Token: 0x0600723E RID: 29246 RVA: 0x0024D2A8 File Offset: 0x0024B4A8
	public bool IsSpellActive(SpellType spellType)
	{
		Spell spellIfLoaded = this.GetSpellIfLoaded(spellType);
		return !(spellIfLoaded == null) && spellIfLoaded.IsActive();
	}

	// Token: 0x0600723F RID: 29247 RVA: 0x0024D2D0 File Offset: 0x0024B4D0
	public void ActivateSpellDeathState(SpellType spellType)
	{
		Spell spellIfLoaded = this.GetSpellIfLoaded(spellType);
		if (spellIfLoaded == null)
		{
			return;
		}
		spellIfLoaded.ActivateState(SpellStateType.DEATH);
	}

	// Token: 0x06007240 RID: 29248 RVA: 0x0024D2F8 File Offset: 0x0024B4F8
	public void ActivateSpellCancelState(SpellType spellType)
	{
		Spell spellIfLoaded = this.GetSpellIfLoaded(spellType);
		if (spellIfLoaded == null)
		{
			return;
		}
		spellIfLoaded.ActivateState(SpellStateType.CANCEL);
	}

	// Token: 0x06007241 RID: 29249 RVA: 0x0024D320 File Offset: 0x0024B520
	public void ActivateAllSpellsDeathStates()
	{
		if (this.m_useSharedSpellTable)
		{
			using (Map<SpellType, Spell>.ValueCollection.Enumerator enumerator = this.m_localSpellTable.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Spell spell = enumerator.Current;
					if (spell.IsActive())
					{
						spell.ActivateState(SpellStateType.DEATH);
					}
				}
				return;
			}
		}
		if (this.m_spellTable != null)
		{
			foreach (SpellTableEntry spellTableEntry in this.m_spellTable.m_Table)
			{
				Spell spell2 = spellTableEntry.m_Spell;
				if (!(spell2 == null) && spell2.IsActive())
				{
					spell2.ActivateState(SpellStateType.DEATH);
				}
			}
		}
	}

	// Token: 0x06007242 RID: 29250 RVA: 0x0024D3F4 File Offset: 0x0024B5F4
	public void DoCardDeathVisuals()
	{
		foreach (object obj in Enum.GetValues(typeof(SpellType)))
		{
			SpellType spellType = (SpellType)obj;
			if (this.IsSpellActive(spellType) && spellType != SpellType.GHOSTLY_DEATH && spellType != SpellType.DEATH && spellType != SpellType.DEATHRATTLE_DEATH && spellType != SpellType.REBORN_DEATH && spellType != SpellType.DAMAGE)
			{
				this.ActivateSpellDeathState(spellType);
			}
		}
	}

	// Token: 0x06007243 RID: 29251 RVA: 0x0024D47C File Offset: 0x0024B67C
	public void DeactivateAllSpells()
	{
		if (this.m_useSharedSpellTable)
		{
			using (List<SpellType>.Enumerator enumerator = new List<SpellType>(this.m_localSpellTable.Keys).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SpellType key = enumerator.Current;
					Spell spell = this.m_localSpellTable[key];
					if (spell != null)
					{
						spell.Deactivate();
					}
				}
				return;
			}
		}
		if (this.m_spellTable != null)
		{
			foreach (SpellTableEntry spellTableEntry in this.m_spellTable.m_Table)
			{
				Spell spell2 = spellTableEntry.m_Spell;
				if (!(spell2 == null))
				{
					spell2.Deactivate();
				}
			}
		}
	}

	// Token: 0x06007244 RID: 29252 RVA: 0x0024D55C File Offset: 0x0024B75C
	public void DestroySpell(SpellType spellType)
	{
		if (this.m_useSharedSpellTable)
		{
			Spell spell;
			if (this.m_localSpellTable.TryGetValue(spellType, out spell))
			{
				UnityEngine.Object.Destroy(spell.gameObject);
				this.m_localSpellTable.Remove(spellType);
				return;
			}
		}
		else
		{
			Debug.LogError(string.Format("Actor.DestroySpell() - FAILED to destroy {0} because the Actor is not using a shared spell table.", spellType));
		}
	}

	// Token: 0x06007245 RID: 29253 RVA: 0x0024D5AF File Offset: 0x0024B7AF
	public void DisableArmorSpellForTransition()
	{
		this.m_armorSpellDisabledForTransition = true;
	}

	// Token: 0x06007246 RID: 29254 RVA: 0x0024D5B8 File Offset: 0x0024B7B8
	public void EnableArmorSpellAfterTransition()
	{
		this.m_armorSpellDisabledForTransition = false;
	}

	// Token: 0x06007247 RID: 29255 RVA: 0x0024D5C1 File Offset: 0x0024B7C1
	public void HideArmorSpell()
	{
		if (this.m_armorSpell != null)
		{
			this.m_armorSpell.SetArmor(0);
			this.m_armorSpell.Deactivate();
			this.m_armorSpell.gameObject.SetActive(false);
		}
	}

	// Token: 0x06007248 RID: 29256 RVA: 0x0024D5F9 File Offset: 0x0024B7F9
	public void ShowArmorSpell()
	{
		if (this.m_armorSpell != null && !this.m_armorSpellDisabledForTransition)
		{
			this.m_armorSpell.gameObject.SetActive(true);
			this.UpdateArmorSpell();
		}
	}

	// Token: 0x06007249 RID: 29257 RVA: 0x0024D628 File Offset: 0x0024B828
	private void UpdateRootObjectSpellComponents()
	{
		if (this.m_entity == null)
		{
			return;
		}
		if (this.m_armorSpellLoading)
		{
			base.StartCoroutine(this.UpdateArmorSpellWhenLoaded());
		}
		if (this.m_armorSpell != null)
		{
			this.UpdateArmorSpell();
		}
	}

	// Token: 0x0600724A RID: 29258 RVA: 0x0024D65C File Offset: 0x0024B85C
	private IEnumerator UpdateArmorSpellWhenLoaded()
	{
		while (this.m_armorSpellLoading)
		{
			yield return null;
		}
		this.UpdateArmorSpell();
		yield break;
	}

	// Token: 0x0600724B RID: 29259 RVA: 0x0024D66C File Offset: 0x0024B86C
	private void UpdateArmorSpell()
	{
		if (!this.m_armorSpell.gameObject.activeInHierarchy)
		{
			return;
		}
		if (this.m_entity == null)
		{
			return;
		}
		int armor = this.m_entity.GetArmor();
		int armor2 = this.m_armorSpell.GetArmor();
		this.m_armorSpell.SetArmor(armor);
		if (armor > 0)
		{
			bool flag = this.m_armorSpell.IsShown();
			if (!flag)
			{
				this.m_armorSpell.Show();
			}
			if (armor2 <= 0)
			{
				base.StartCoroutine(this.ActivateArmorSpell(SpellStateType.BIRTH, true));
				return;
			}
			if (armor2 > armor)
			{
				base.StartCoroutine(this.ActivateArmorSpell(SpellStateType.ACTION, true));
				return;
			}
			if (armor2 < armor)
			{
				base.StartCoroutine(this.ActivateArmorSpell(SpellStateType.CANCEL, true));
				return;
			}
			if (!flag)
			{
				base.StartCoroutine(this.ActivateArmorSpell(SpellStateType.IDLE, true));
				return;
			}
		}
		else if (armor2 > 0)
		{
			base.StartCoroutine(this.ActivateArmorSpell(SpellStateType.DEATH, false));
		}
	}

	// Token: 0x0600724C RID: 29260 RVA: 0x0024D739 File Offset: 0x0024B939
	private IEnumerator ActivateArmorSpell(SpellStateType stateType, bool armorShouldBeOn)
	{
		while (this.m_armorSpell.GetActiveState() != SpellStateType.NONE)
		{
			yield return null;
		}
		if (this.m_armorSpell.GetActiveState() == stateType)
		{
			yield break;
		}
		int armor = this.m_entity.GetArmor();
		if ((armorShouldBeOn && armor <= 0) || (!armorShouldBeOn && armor > 0))
		{
			yield break;
		}
		this.m_armorSpell.ActivateState(stateType);
		yield break;
	}

	// Token: 0x0600724D RID: 29261 RVA: 0x0024D756 File Offset: 0x0024B956
	private void OnSpellStateStarted(Spell spell, SpellStateType prevStateType, object userData)
	{
		spell.AddStateStartedCallback(new Spell.StateStartedCallback(this.OnSpellStateStarted));
		this.m_actorStateMgr.RefreshStateMgr();
		if (this.m_projectedShadow)
		{
			this.m_projectedShadow.UpdateContactShadow();
		}
	}

	// Token: 0x0600724E RID: 29262 RVA: 0x0024D78D File Offset: 0x0024B98D
	private void AssignRootObject()
	{
		this.m_rootObject = SceneUtils.FindChildBySubstring(base.gameObject, "RootObject");
	}

	// Token: 0x0600724F RID: 29263 RVA: 0x0024D7A5 File Offset: 0x0024B9A5
	private void AssignBones()
	{
		this.m_bones = SceneUtils.FindChildBySubstring(base.gameObject, "Bones");
	}

	// Token: 0x06007250 RID: 29264 RVA: 0x0024D7C0 File Offset: 0x0024B9C0
	private void AssignMeshRenderers()
	{
		foreach (MeshRenderer meshRenderer in base.gameObject.GetComponentsInChildren<MeshRenderer>(true))
		{
			if (meshRenderer.gameObject.name.Equals("Mesh", StringComparison.OrdinalIgnoreCase))
			{
				this.m_meshRenderer = meshRenderer;
				foreach (MeshRenderer meshRenderer2 in meshRenderer.gameObject.GetComponentsInChildren<MeshRenderer>(true))
				{
					this.AssignMaterials(meshRenderer2);
				}
				break;
			}
		}
		if (this.m_portraitMesh != null)
		{
			this.m_meshRendererPortrait = this.m_portraitMesh.GetComponent<MeshRenderer>();
		}
	}

	// Token: 0x06007251 RID: 29265 RVA: 0x0024D85C File Offset: 0x0024BA5C
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
					this.m_legacyPortraitMaterialIndex = i;
				}
				else if (material.name.IndexOf("Card_Inhand_Ability_Warlock", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					this.m_legacyCardColorMaterialIndex = i;
				}
				else if (material.name.IndexOf("Card_Inhand_Warlock", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					this.m_legacyCardColorMaterialIndex = i;
				}
				else if (material.name.IndexOf("Card_Inhand_Weapon_Warlock", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					this.m_legacyCardColorMaterialIndex = i;
				}
			}
		}
	}

	// Token: 0x06007252 RID: 29266 RVA: 0x0024D90C File Offset: 0x0024BB0C
	public void ShowSideQuestProgressBanner()
	{
		this.ResetBanner();
		if (this.m_entity == null)
		{
			return;
		}
		if (this.m_banner == null || this.m_bannerBottom == null || this.m_bannerText == null)
		{
			return;
		}
		this.m_banner.SetActive(true);
		this.m_bannerBottom.SetActive(true);
		this.m_bannerText.gameObject.SetActive(true);
		this.m_bannerText.Text = GameStrings.Format("GLUE_SIDEQUEST_PROGRESS_BANNER", new object[]
		{
			this.m_entity.GetTag(GAME_TAG.QUEST_PROGRESS),
			this.m_entity.GetTag(GAME_TAG.QUEST_PROGRESS_TOTAL)
		});
	}

	// Token: 0x06007253 RID: 29267 RVA: 0x0024D9C8 File Offset: 0x0024BBC8
	public void HideSideQuestProgressBanner()
	{
		if (this.m_banner == null || this.m_bannerBottom == null || this.m_bannerText == null)
		{
			return;
		}
		this.m_banner.SetActive(false);
		this.m_bannerBottom.SetActive(false);
		this.m_bannerText.gameObject.SetActive(false);
	}

	// Token: 0x06007254 RID: 29268 RVA: 0x0024DA2C File Offset: 0x0024BC2C
	private void AssignSpells()
	{
		this.m_spellTable = base.gameObject.GetComponentInChildren<SpellTable>();
		this.m_actorStateMgr = base.gameObject.GetComponentInChildren<ActorStateMgr>();
		if (this.m_spellTable == null)
		{
			if (!string.IsNullOrEmpty(this.m_spellTablePrefab))
			{
				SpellCache spellCache = SpellCache.Get();
				if (spellCache == null)
				{
					Debug.LogWarning("Null spell cache: " + this.m_spellTablePrefab);
					return;
				}
				SpellTable spellTable = spellCache.GetSpellTable(this.m_spellTablePrefab);
				if (spellTable != null)
				{
					this.m_useSharedSpellTable = true;
					this.m_sharedSpellTable = spellTable;
					this.m_localSpellTable = new Map<SpellType, Spell>();
					return;
				}
				Debug.LogWarning("failed to load spell table: " + this.m_spellTablePrefab);
				return;
			}
		}
		else if (this.m_actorStateMgr != null)
		{
			foreach (SpellTableEntry spellTableEntry in this.m_spellTable.m_Table)
			{
				if (!(spellTableEntry.m_Spell == null))
				{
					spellTableEntry.m_Spell.AddStateStartedCallback(new Spell.StateStartedCallback(this.OnSpellStateStarted));
				}
			}
		}
	}

	// Token: 0x06007255 RID: 29269 RVA: 0x0024DB58 File Offset: 0x0024BD58
	private void SetUpBanner()
	{
		if (this.m_banner == null || this.m_bannerBottom == null || this.m_bannerText == null)
		{
			return;
		}
		this.ResetBanner();
	}

	// Token: 0x06007256 RID: 29270 RVA: 0x0024DB8C File Offset: 0x0024BD8C
	private void ResetBanner()
	{
		if (this.m_banner == null || this.m_bannerBottom == null || this.m_bannerText == null)
		{
			return;
		}
		this.m_banner.SetActive(false);
		this.m_bannerBottom.SetActive(false);
		this.m_bannerText.gameObject.SetActive(false);
		this.m_banner.transform.parent = base.transform;
		this.m_bannerBottom.transform.parent = base.transform;
		this.m_bannerText.transform.parent = base.transform;
	}

	// Token: 0x06007257 RID: 29271 RVA: 0x0024DC30 File Offset: 0x0024BE30
	private void CacheShadowObjects()
	{
		this.m_shadowObject = SceneUtils.FindChildByTag(base.gameObject, "FakeShadow", true);
		this.m_uniqueShadowObject = SceneUtils.FindChildByTag(base.gameObject, "FakeShadowUnique", true);
		Renderer renderer = (this.m_uniqueShadowObject != null) ? this.m_uniqueShadowObject.GetComponent<Renderer>() : null;
		Renderer renderer2 = (this.m_shadowObject != null) ? this.m_shadowObject.GetComponent<Renderer>() : null;
		if (renderer != null)
		{
			this.m_initialUniqueShadowRenderQueue = renderer.GetMaterial().renderQueue;
		}
		if (renderer2 != null)
		{
			this.m_initialShadowRenderQueue = renderer2.GetMaterial().renderQueue;
		}
		this.m_shadowObjectInitialized = true;
	}

	// Token: 0x06007258 RID: 29272 RVA: 0x0024DCE0 File Offset: 0x0024BEE0
	private void LoadArmorSpell()
	{
		if (this.m_armorSpellBone == null)
		{
			return;
		}
		this.m_armorSpellLoading = true;
		string input = "Hero_Armor.prefab:e4d519d1080fe4656967bf5140ca3587";
		global::CardDef cardDef = this.m_cardDefHandle.Get();
		if (cardDef != null && !string.IsNullOrEmpty(cardDef.m_CustomHeroArmorSpell))
		{
			input = cardDef.m_CustomHeroArmorSpell;
		}
		AssetLoader.Get().InstantiatePrefab(input, new PrefabCallback<GameObject>(this.OnArmorSpellLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x06007259 RID: 29273 RVA: 0x0024DD54 File Offset: 0x0024BF54
	private void OnArmorSpellLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("{0} - Actor.OnArmorSpellLoaded() - failed to load Hero_Armor spell! m_armorSpell GameObject = null!", assetRef));
			return;
		}
		this.m_armorSpellLoading = false;
		this.m_armorSpell = go.GetComponent<ArmorSpell>();
		if (this.m_armorSpell == null)
		{
			Debug.LogError(string.Format("{0} - Actor.OnArmorSpellLoaded() - failed to load Hero_Armor spell! m_armorSpell Spell = null!", assetRef));
			return;
		}
		go.transform.parent = this.m_armorSpellBone.transform;
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		go.transform.localScale = Vector3.one;
	}

	// Token: 0x170006B9 RID: 1721
	// (get) Token: 0x0600725A RID: 29274 RVA: 0x0024DDF3 File Offset: 0x0024BFF3
	public bool HasCardDef
	{
		get
		{
			return this.m_cardDefHandle.Get() != null;
		}
	}

	// Token: 0x0600725B RID: 29275 RVA: 0x0024DE06 File Offset: 0x0024C006
	public bool HasSameCardDef(global::CardDef cardDef)
	{
		return this.m_cardDefHandle.Get() == cardDef;
	}

	// Token: 0x170006BA RID: 1722
	// (get) Token: 0x0600725C RID: 29276 RVA: 0x0024DE19 File Offset: 0x0024C019
	public string CardDefName
	{
		get
		{
			if (!this.HasCardDef)
			{
				return null;
			}
			return this.m_cardDefHandle.Get().name;
		}
	}

	// Token: 0x170006BB RID: 1723
	// (get) Token: 0x0600725D RID: 29277 RVA: 0x0024DE35 File Offset: 0x0024C035
	public Material DeckCardBarPortrait
	{
		get
		{
			if (!this.HasCardDef)
			{
				return null;
			}
			return this.m_cardDefHandle.Get().GetDeckCardBarPortrait();
		}
	}

	// Token: 0x170006BC RID: 1724
	// (get) Token: 0x0600725E RID: 29278 RVA: 0x0024DE51 File Offset: 0x0024C051
	public Texture PortraitTexture
	{
		get
		{
			if (!this.HasCardDef)
			{
				return null;
			}
			return this.m_cardDefHandle.Get().GetPortraitTexture();
		}
	}

	// Token: 0x170006BD RID: 1725
	// (get) Token: 0x0600725F RID: 29279 RVA: 0x0024DE6D File Offset: 0x0024C06D
	public Material PremiumPortraitMaterial
	{
		get
		{
			if (!this.HasCardDef)
			{
				return null;
			}
			return this.m_cardDefHandle.Get().GetPremiumPortraitMaterial();
		}
	}

	// Token: 0x170006BE RID: 1726
	// (get) Token: 0x06007260 RID: 29280 RVA: 0x0024DE89 File Offset: 0x0024C089
	public UberShaderAnimation PremiumPortraitAnimation
	{
		get
		{
			if (!this.HasCardDef)
			{
				return null;
			}
			return this.m_cardDefHandle.Get().GetPremiumPortraitAnimation();
		}
	}

	// Token: 0x170006BF RID: 1727
	// (get) Token: 0x06007261 RID: 29281 RVA: 0x0024DEA5 File Offset: 0x0024C0A5
	public CardPortraitQuality CardPortraitQuality
	{
		get
		{
			return CardPortraitQuality.GetFromDef(this.m_cardDefHandle.Get());
		}
	}

	// Token: 0x170006C0 RID: 1728
	// (get) Token: 0x06007262 RID: 29282 RVA: 0x0024DEB7 File Offset: 0x0024C0B7
	public CardEffectDef PlayEffectDef
	{
		get
		{
			if (!this.HasCardDef)
			{
				return null;
			}
			return this.m_cardDefHandle.Get().m_PlayEffectDef;
		}
	}

	// Token: 0x170006C1 RID: 1729
	// (get) Token: 0x06007263 RID: 29283 RVA: 0x0024DED3 File Offset: 0x0024C0D3
	public bool PremiumAnimationAvailable
	{
		get
		{
			return CardTextureLoader.PremiumAnimationAvailable(this.m_cardDefHandle.Get());
		}
	}

	// Token: 0x170006C2 RID: 1730
	// (get) Token: 0x06007264 RID: 29284 RVA: 0x0024DEE5 File Offset: 0x0024C0E5
	public string SocketInEffectFriendly
	{
		get
		{
			if (!this.HasCardDef)
			{
				return null;
			}
			return this.m_cardDefHandle.Get().m_SocketInEffectFriendly;
		}
	}

	// Token: 0x170006C3 RID: 1731
	// (get) Token: 0x06007265 RID: 29285 RVA: 0x0024DF01 File Offset: 0x0024C101
	public string SocketInEffectFriendlyPhone
	{
		get
		{
			if (!this.HasCardDef)
			{
				return null;
			}
			return this.m_cardDefHandle.Get().m_SocketInEffectFriendlyPhone;
		}
	}

	// Token: 0x170006C4 RID: 1732
	// (get) Token: 0x06007266 RID: 29286 RVA: 0x0024DF1D File Offset: 0x0024C11D
	public string SocketInEffectOpponent
	{
		get
		{
			if (!this.HasCardDef)
			{
				return null;
			}
			return this.m_cardDefHandle.Get().m_SocketInEffectOpponent;
		}
	}

	// Token: 0x170006C5 RID: 1733
	// (get) Token: 0x06007267 RID: 29287 RVA: 0x0024DF39 File Offset: 0x0024C139
	public string SocketInEffectOpponentPhone
	{
		get
		{
			if (!this.HasCardDef)
			{
				return null;
			}
			return this.m_cardDefHandle.Get().m_SocketInEffectOpponentPhone;
		}
	}

	// Token: 0x170006C6 RID: 1734
	// (get) Token: 0x06007268 RID: 29288 RVA: 0x0024DF55 File Offset: 0x0024C155
	public bool SocketInOverrideHeroAnimation
	{
		get
		{
			return this.HasCardDef && this.m_cardDefHandle.Get().m_SocketInOverrideHeroAnimation;
		}
	}

	// Token: 0x170006C7 RID: 1735
	// (get) Token: 0x06007269 RID: 29289 RVA: 0x0024DF71 File Offset: 0x0024C171
	public bool SocketInParentEffectToHero
	{
		get
		{
			return this.HasCardDef && this.m_cardDefHandle.Get().m_SocketInParentEffectToHero;
		}
	}

	// Token: 0x170006C8 RID: 1736
	// (get) Token: 0x0600726A RID: 29290 RVA: 0x0024DF8D File Offset: 0x0024C18D
	public List<EmoteEntryDef> EmoteDefs
	{
		get
		{
			if (!this.HasCardDef)
			{
				return null;
			}
			return this.m_cardDefHandle.Get().m_EmoteDefs;
		}
	}

	// Token: 0x170006C9 RID: 1737
	// (get) Token: 0x0600726B RID: 29291 RVA: 0x0024DFA9 File Offset: 0x0024C1A9
	// (set) Token: 0x0600726C RID: 29292 RVA: 0x0024DFD8 File Offset: 0x0024C1D8
	public bool AlwaysRenderPremiumPortrait
	{
		get
		{
			return this.m_cardDefHandle != null && this.m_cardDefHandle.Get() != null && this.m_cardDefHandle.Get().m_AlwaysRenderPremiumPortrait;
		}
		set
		{
			if (this.m_cardDefHandle != null && this.m_cardDefHandle.Get() != null)
			{
				this.m_cardDefHandle.Get().m_AlwaysRenderPremiumPortrait = value;
			}
		}
	}

	// Token: 0x170006CA RID: 1738
	// (get) Token: 0x0600726D RID: 29293 RVA: 0x0024E006 File Offset: 0x0024C206
	public CardSilhouetteOverride CardSilhouetteOverride
	{
		get
		{
			if (!this.HasCardDef)
			{
				return CardSilhouetteOverride.None;
			}
			return this.m_cardDefHandle.Get().m_CardSilhouetteOverride;
		}
	}

	// Token: 0x04005AC2 RID: 23234
	protected readonly UnityEngine.Vector2 GEM_TEXTURE_OFFSET_RARE = new UnityEngine.Vector2(0.5f, 0f);

	// Token: 0x04005AC3 RID: 23235
	protected readonly UnityEngine.Vector2 GEM_TEXTURE_OFFSET_EPIC = new UnityEngine.Vector2(0f, 0.5f);

	// Token: 0x04005AC4 RID: 23236
	protected readonly UnityEngine.Vector2 GEM_TEXTURE_OFFSET_LEGENDARY = new UnityEngine.Vector2(0.5f, 0.5f);

	// Token: 0x04005AC5 RID: 23237
	protected readonly UnityEngine.Vector2 GEM_TEXTURE_OFFSET_COMMON = new UnityEngine.Vector2(0f, 0f);

	// Token: 0x04005AC6 RID: 23238
	protected readonly Color GEM_COLOR_RARE = new Color(0.1529f, 0.498f, 1f);

	// Token: 0x04005AC7 RID: 23239
	protected readonly Color GEM_COLOR_EPIC = new Color(0.596f, 0.1568f, 0.7333f);

	// Token: 0x04005AC8 RID: 23240
	protected readonly Color GEM_COLOR_LEGENDARY = new Color(1f, 0.5333f, 0f);

	// Token: 0x04005AC9 RID: 23241
	protected readonly Color GEM_COLOR_COMMON = new Color(0.549f, 0.549f, 0.549f);

	// Token: 0x04005ACA RID: 23242
	protected readonly Color CLASS_COLOR_GENERIC = new Color(0.7f, 0.7f, 0.7f);

	// Token: 0x04005ACB RID: 23243
	protected readonly Color CLASS_COLOR_WARLOCK = new Color(0.33f, 0.2f, 0.4f);

	// Token: 0x04005ACC RID: 23244
	protected readonly Color CLASS_COLOR_ROGUE = new Color(0.23f, 0.23f, 0.23f);

	// Token: 0x04005ACD RID: 23245
	protected readonly Color CLASS_COLOR_DRUID = new Color(0.42f, 0.29f, 0.14f);

	// Token: 0x04005ACE RID: 23246
	protected readonly Color CLASS_COLOR_SHAMAN = new Color(0f, 0.32f, 0.71f);

	// Token: 0x04005ACF RID: 23247
	protected readonly Color CLASS_COLOR_HUNTER = new Color(0.26f, 0.54f, 0.18f);

	// Token: 0x04005AD0 RID: 23248
	protected readonly Color CLASS_COLOR_MAGE = new Color(0.44f, 0.48f, 0.69f);

	// Token: 0x04005AD1 RID: 23249
	protected readonly Color CLASS_COLOR_PALADIN = new Color(0.71f, 0.49f, 0.2f);

	// Token: 0x04005AD2 RID: 23250
	protected readonly Color CLASS_COLOR_PRIEST = new Color(1f, 1f, 1f);

	// Token: 0x04005AD3 RID: 23251
	protected readonly Color CLASS_COLOR_WARRIOR = new Color(0.43f, 0.14f, 0.14f);

	// Token: 0x04005AD4 RID: 23252
	protected readonly Color CLASS_COLOR_DEATHKNIGHT = new Color(0.0666667f, 0.5294f, 0.5843f);

	// Token: 0x04005AD5 RID: 23253
	protected readonly Color CLASS_COLOR_DEMONHUNTER = new Color(0.0902f, 0.2275f, 0.1961f);

	// Token: 0x04005AD6 RID: 23254
	private readonly Color MISSING_CARD_WILD_GOLDEN_COLOR = new Color(0.518f, 0.361f, 0f, 0.68f);

	// Token: 0x04005AD7 RID: 23255
	private readonly Color MISSING_CARD_STANDARD_GOLDEN_COLOR = new Color(0.867f, 0.675f, 0.22f, 0.53f);

	// Token: 0x04005AD8 RID: 23256
	protected readonly Color MISSING_CARD_WILD_DIAMOND_COLOR = new Color(0.4705f, 0.3058f, 0.0117f, 0.6784f);

	// Token: 0x04005AD9 RID: 23257
	protected readonly string MISSING_CARD_WILD_DIAMOND_CONTRAST_KEY = "_Contrast";

	// Token: 0x04005ADA RID: 23258
	protected readonly float MISSING_CARD_WILD_DIAMOND_CONTRAST = 0.4f;

	// Token: 0x04005ADB RID: 23259
	protected readonly string MISSING_CARD_WILD_DIAMOND_INTENSITY_KEY = "_Intensity";

	// Token: 0x04005ADC RID: 23260
	protected readonly float MISSING_CARD_WILD_DIAMOND_INTENSITY = 1.7f;

	// Token: 0x04005ADD RID: 23261
	protected readonly float WATERMARK_ALPHA_VALUE = 0.7734375f;

	// Token: 0x04005ADE RID: 23262
	public GameObject m_cardMesh;

	// Token: 0x04005ADF RID: 23263
	public int m_cardFrontMatIdx = -1;

	// Token: 0x04005AE0 RID: 23264
	public int m_cardBackMatIdx = -1;

	// Token: 0x04005AE1 RID: 23265
	public int m_premiumRibbon = -1;

	// Token: 0x04005AE2 RID: 23266
	public GameObject m_portraitMesh;

	// Token: 0x04005AE3 RID: 23267
	public GameObject m_portraitMeshRTT;

	// Token: 0x04005AE4 RID: 23268
	public GameObject m_portraitMeshRTT_background;

	// Token: 0x04005AE5 RID: 23269
	public bool m_usePlayPortrait;

	// Token: 0x04005AE6 RID: 23270
	public int m_portraitFrameMatIdx = -1;

	// Token: 0x04005AE7 RID: 23271
	public int m_portraitMatIdx = -1;

	// Token: 0x04005AE8 RID: 23272
	public GameObject m_nameBannerMesh;

	// Token: 0x04005AE9 RID: 23273
	public GameObject m_descriptionMesh;

	// Token: 0x04005AEA RID: 23274
	public GameObject m_descriptionTrimMesh;

	// Token: 0x04005AEB RID: 23275
	public GameObject m_watermarkMesh;

	// Token: 0x04005AEC RID: 23276
	public GameObject m_rarityFrameMesh;

	// Token: 0x04005AED RID: 23277
	public GameObject m_rarityGemMesh;

	// Token: 0x04005AEE RID: 23278
	public GameObject m_racePlateMesh;

	// Token: 0x04005AEF RID: 23279
	public Mesh m_spellDescriptionMeshNeutral;

	// Token: 0x04005AF0 RID: 23280
	public Mesh m_spellDescriptionMeshSchool;

	// Token: 0x04005AF1 RID: 23281
	public GameObject m_attackObject;

	// Token: 0x04005AF2 RID: 23282
	public GameObject m_healthObject;

	// Token: 0x04005AF3 RID: 23283
	public GameObject m_armorObject;

	// Token: 0x04005AF4 RID: 23284
	public GameObject m_manaObject;

	// Token: 0x04005AF5 RID: 23285
	public GameObject m_racePlateObject;

	// Token: 0x04005AF6 RID: 23286
	public GameObject m_cardTypeAnchorObject;

	// Token: 0x04005AF7 RID: 23287
	public GameObject m_eliteObject;

	// Token: 0x04005AF8 RID: 23288
	public GameObject m_classIconObject;

	// Token: 0x04005AF9 RID: 23289
	public GameObject m_heroSpotLight;

	// Token: 0x04005AFA RID: 23290
	public GameObject m_glints;

	// Token: 0x04005AFB RID: 23291
	public GameObject m_armorSpellBone;

	// Token: 0x04005AFC RID: 23292
	public NestedPrefab m_multiClassBannerContainer;

	// Token: 0x04005AFD RID: 23293
	public NestedPrefab m_bannedRibbonContainer;

	// Token: 0x04005AFE RID: 23294
	public GameObject m_bannerContainer;

	// Token: 0x04005AFF RID: 23295
	public GameObject m_banner;

	// Token: 0x04005B00 RID: 23296
	public GameObject m_bannerBottom;

	// Token: 0x04005B01 RID: 23297
	public UberText m_bannerText;

	// Token: 0x04005B02 RID: 23298
	public UberText m_costTextMesh;

	// Token: 0x04005B03 RID: 23299
	public UberText m_attackTextMesh;

	// Token: 0x04005B04 RID: 23300
	public UberText m_healthTextMesh;

	// Token: 0x04005B05 RID: 23301
	public UberText m_armorTextMesh;

	// Token: 0x04005B06 RID: 23302
	public UberText m_nameTextMesh;

	// Token: 0x04005B07 RID: 23303
	public UberText m_powersTextMesh;

	// Token: 0x04005B08 RID: 23304
	public UberText m_raceTextMesh;

	// Token: 0x04005B09 RID: 23305
	public UberText m_secretText;

	// Token: 0x04005B0A RID: 23306
	public GameObject m_missingCardEffect;

	// Token: 0x04005B0B RID: 23307
	public GameObject m_ghostCardGameObject;

	// Token: 0x04005B0C RID: 23308
	public GameObject m_diamondPortraitR2T;

	// Token: 0x04005B0D RID: 23309
	[CustomEditField(T = EditType.ACTOR)]
	public string m_spellTablePrefab;

	// Token: 0x04005B0E RID: 23310
	protected global::Card m_card;

	// Token: 0x04005B0F RID: 23311
	protected Entity m_entity;

	// Token: 0x04005B10 RID: 23312
	protected CardDefHandle m_cardDefHandle = new CardDefHandle();

	// Token: 0x04005B11 RID: 23313
	protected EntityDef m_entityDef;

	// Token: 0x04005B12 RID: 23314
	protected TAG_PREMIUM m_premiumType;

	// Token: 0x04005B13 RID: 23315
	protected ProjectedShadow m_projectedShadow;

	// Token: 0x04005B14 RID: 23316
	protected bool m_shown = true;

	// Token: 0x04005B15 RID: 23317
	protected bool m_shadowVisible;

	// Token: 0x04005B16 RID: 23318
	protected ActorStateMgr m_actorStateMgr;

	// Token: 0x04005B17 RID: 23319
	protected ActorStateType m_actorState = ActorStateType.CARD_IDLE;

	// Token: 0x04005B18 RID: 23320
	protected bool forceIdleState;

	// Token: 0x04005B19 RID: 23321
	protected GameObject m_rootObject;

	// Token: 0x04005B1A RID: 23322
	protected GameObject m_bones;

	// Token: 0x04005B1B RID: 23323
	protected MeshRenderer m_meshRenderer;

	// Token: 0x04005B1C RID: 23324
	protected MeshRenderer m_meshRendererPortrait;

	// Token: 0x04005B1D RID: 23325
	protected int m_legacyPortraitMaterialIndex = -1;

	// Token: 0x04005B1E RID: 23326
	protected int m_legacyCardColorMaterialIndex = -1;

	// Token: 0x04005B1F RID: 23327
	protected Material m_initialPortraitMaterial;

	// Token: 0x04005B20 RID: 23328
	protected Material m_initialPremiumRibbonMaterial;

	// Token: 0x04005B21 RID: 23329
	protected SpellTable m_sharedSpellTable;

	// Token: 0x04005B22 RID: 23330
	protected bool m_useSharedSpellTable;

	// Token: 0x04005B23 RID: 23331
	protected Map<SpellType, Spell> m_localSpellTable;

	// Token: 0x04005B24 RID: 23332
	protected SpellTable m_spellTable;

	// Token: 0x04005B25 RID: 23333
	protected ArmorSpell m_armorSpell;

	// Token: 0x04005B26 RID: 23334
	protected GameObject m_hiddenCardStandIn;

	// Token: 0x04005B27 RID: 23335
	protected bool m_shadowform;

	// Token: 0x04005B28 RID: 23336
	protected GhostCard.Type m_ghostCard;

	// Token: 0x04005B29 RID: 23337
	protected TAG_PREMIUM m_ghostPremium;

	// Token: 0x04005B2A RID: 23338
	protected bool m_missingcard;

	// Token: 0x04005B2B RID: 23339
	protected bool m_armorSpellLoading;

	// Token: 0x04005B2C RID: 23340
	protected bool m_materialEffectsSeeded;

	// Token: 0x04005B2D RID: 23341
	protected Player.Side? m_cardBackSideOverride;

	// Token: 0x04005B2E RID: 23342
	protected CardBackManager.CardBackSlot? m_cardBackSlotOverride;

	// Token: 0x04005B2F RID: 23343
	private string m_cardDefPowerTextOverride;

	// Token: 0x04005B30 RID: 23344
	protected bool m_ignoreUpdateCardback;

	// Token: 0x04005B31 RID: 23345
	protected bool isPortraitMaterialDirty;

	// Token: 0x04005B32 RID: 23346
	protected Texture m_portraitTextureOverride;

	// Token: 0x04005B33 RID: 23347
	protected bool m_blockTextComponentUpdate;

	// Token: 0x04005B34 RID: 23348
	protected bool m_armorSpellDisabledForTransition;

	// Token: 0x04005B35 RID: 23349
	protected MultiClassBannerTransition m_multiClassBanner;

	// Token: 0x04005B36 RID: 23350
	protected UberShaderController m_uberShaderController;

	// Token: 0x04005B37 RID: 23351
	protected bool m_ignoreHideStats;

	// Token: 0x04005B38 RID: 23352
	protected TAG_CARD_SET m_watermarkCardSetOverride;

	// Token: 0x04005B39 RID: 23353
	protected bool m_useShortName;

	// Token: 0x04005B3A RID: 23354
	protected GameObject m_bannedRibbon;

	// Token: 0x04005B3B RID: 23355
	protected GameObject m_shadowObject;

	// Token: 0x04005B3C RID: 23356
	protected GameObject m_uniqueShadowObject;

	// Token: 0x04005B3D RID: 23357
	private int m_initialMissingCardRenderQueue;

	// Token: 0x04005B3E RID: 23358
	private int m_initialShadowRenderQueue;

	// Token: 0x04005B3F RID: 23359
	private int m_initialUniqueShadowRenderQueue;

	// Token: 0x04005B40 RID: 23360
	private bool m_shadowObjectInitialized;

	// Token: 0x04005B41 RID: 23361
	private bool m_usesMultiClassBanner;

	// Token: 0x04005B42 RID: 23362
	private GameObject m_diamondModelObject;

	// Token: 0x04005B43 RID: 23363
	private DiamondRenderToTexture m_diamondRenderToTexture;

	// Token: 0x04005B44 RID: 23364
	private string m_diamondModelShown;

	// Token: 0x04005B45 RID: 23365
	private bool m_portraitMeshDirty = true;

	// Token: 0x04005B46 RID: 23366
	private AssetHandle<Texture> m_watermarkTex;

	// Token: 0x04005B47 RID: 23367
	private AssetHandle<Texture> m_cardColorTex;

	// Token: 0x04005B49 RID: 23369
	private readonly UnityEngine.Vector2 descriptionMesh_WithoutRace_TextureOffset = new UnityEngine.Vector2(-0.04f, 0.07f);

	// Token: 0x04005B4A RID: 23370
	private readonly UnityEngine.Vector2 descriptionMesh_WithRace_TextureOffset = new UnityEngine.Vector2(-0.04f, 0f);
}
