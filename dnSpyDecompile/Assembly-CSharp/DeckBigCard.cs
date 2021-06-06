using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000123 RID: 291
public class DeckBigCard : MonoBehaviour
{
	// Token: 0x14000019 RID: 25
	// (add) Token: 0x06001331 RID: 4913 RVA: 0x0006DFB4 File Offset: 0x0006C1B4
	// (remove) Token: 0x06001332 RID: 4914 RVA: 0x0006DFEC File Offset: 0x0006C1EC
	public event DeckBigCard.OnBigCardShownHandler OnBigCardShown;

	// Token: 0x06001333 RID: 4915 RVA: 0x0006E021 File Offset: 0x0006C221
	private void Awake()
	{
		this.m_firstShowFrame = 0;
		this.m_defaultLocalScale = base.transform.localScale;
		this.m_defaultLocalPosition = base.transform.localPosition;
	}

	// Token: 0x06001334 RID: 4916 RVA: 0x0006E04C File Offset: 0x0006C24C
	private void OnDestroy()
	{
		DefLoader.DisposableCardDef cardDef = this.m_cardDef;
		if (cardDef != null)
		{
			cardDef.Dispose();
		}
		this.m_cardDef = null;
	}

	// Token: 0x06001335 RID: 4917 RVA: 0x0006E068 File Offset: 0x0006C268
	public void Show(EntityDef entityDef, TAG_PREMIUM premium, DefLoader.DisposableCardDef cardDef, Vector3 sourcePosition, GhostCard.Type ghosted, float delay = 0f)
	{
		if (false)
		{
			int frameCount = Time.frameCount;
			if (this.m_firstShowFrame == 0)
			{
				this.m_firstShowFrame = frameCount;
			}
			else if (frameCount - this.m_firstShowFrame <= 1)
			{
				return;
			}
		}
		base.StopCoroutine("ShowWithDelayInternal");
		this.m_shown = true;
		this.m_entityDef = entityDef;
		this.m_premium = premium;
		DefLoader.DisposableCardDef cardDef2 = this.m_cardDef;
		if (cardDef2 != null)
		{
			cardDef2.Dispose();
		}
		DefLoader.DisposableCardDef cardDef3 = cardDef;
		this.m_cardDef = ((cardDef3 != null) ? cardDef3.Share() : null);
		this.m_ghosted = ghosted;
		if (delay > 0f)
		{
			KeyValuePair<float, Action> keyValuePair = new KeyValuePair<float, Action>(delay, delegate()
			{
				this.Show(entityDef, premium, cardDef, sourcePosition, ghosted, 0f);
			});
			base.StartCoroutine("ShowWithDelayInternal", keyValuePair);
			return;
		}
		if (!UniversalInputManager.UsePhoneUI)
		{
			float z = this.m_bottomPosition.transform.position.z;
			float z2 = this.m_topPosition.transform.position.z;
			float z3 = Mathf.Clamp(sourcePosition.z, z, z2);
			TransformUtil.SetPosZ(base.transform, z3);
			this.m_defaultLocalPosition = base.transform.localPosition;
		}
		if (!this.m_actorCacheInit)
		{
			this.m_actorCacheInit = true;
			this.m_actorCache.AddActorLoadedListener(new HandActorCache.ActorLoadedCallback(this.OnActorLoaded));
			this.m_actorCache.Initialize();
		}
		if (this.m_actorCache.IsInitializing())
		{
			return;
		}
		this.Show(sourcePosition.z);
	}

	// Token: 0x06001336 RID: 4918 RVA: 0x0006E219 File Offset: 0x0006C419
	public void Hide(EntityDef entityDef, TAG_PREMIUM premium)
	{
		if (this.m_entityDef != entityDef)
		{
			return;
		}
		if (this.m_premium != premium)
		{
			return;
		}
		this.Hide();
	}

	// Token: 0x06001337 RID: 4919 RVA: 0x0006E235 File Offset: 0x0006C435
	public void ForceHide()
	{
		this.Hide();
	}

	// Token: 0x06001338 RID: 4920 RVA: 0x0006E240 File Offset: 0x0006C440
	public void SetCreatorName(string creatorName)
	{
		if (this.m_createdByText == null)
		{
			return;
		}
		if (string.IsNullOrEmpty(creatorName))
		{
			this.m_createdByText.Text = string.Empty;
			base.transform.localPosition = this.m_defaultLocalPosition;
			base.transform.localScale = this.m_defaultLocalScale;
			return;
		}
		this.m_createdByText.Text = GameStrings.Format("GAMEPLAY_HISTORY_CREATED_BY", new object[]
		{
			creatorName
		});
		base.transform.localPosition = this.m_defaultLocalPosition + this.m_positionOffsetWithCreatorBanner;
		base.transform.localScale = this.m_defaultLocalScale * this.m_scaleMultiplierWithCreatorBanner;
	}

	// Token: 0x06001339 RID: 4921 RVA: 0x0006E2EE File Offset: 0x0006C4EE
	public void OffsetByVector(Vector3 offset)
	{
		this.m_defaultLocalPosition += offset;
		base.transform.localPosition = this.m_defaultLocalPosition;
	}

	// Token: 0x0600133A RID: 4922 RVA: 0x0006E313 File Offset: 0x0006C513
	public void SetHideBigHeroPower(bool hide)
	{
		this.m_hideBigHeroPower = hide;
	}

	// Token: 0x0600133B RID: 4923 RVA: 0x0006E31C File Offset: 0x0006C51C
	private IEnumerator ShowWithDelayInternal(KeyValuePair<float, Action> args)
	{
		yield return new WaitForSeconds(args.Key);
		args.Value();
		yield break;
	}

	// Token: 0x0600133C RID: 4924 RVA: 0x0006E32C File Offset: 0x0006C52C
	private void OnActorLoaded(string assetName, Actor actor, object callbackData)
	{
		if (actor == null)
		{
			Debug.LogWarning(string.Format("DeckBigCard.OnActorLoaded() - FAILED to load {0}", assetName));
			return;
		}
		actor.TurnOffCollider();
		actor.Hide();
		actor.transform.parent = base.transform;
		TransformUtil.Identity(actor.transform);
		SceneUtils.SetLayer(actor, base.gameObject.layer);
		if (!this.m_actorCache.IsInitializing() && this.m_shown)
		{
			this.Show(0f);
		}
	}

	// Token: 0x0600133D RID: 4925 RVA: 0x0006E3AC File Offset: 0x0006C5AC
	private void Show(float sourceZ = 0f)
	{
		this.m_shownActor = this.m_actorCache.GetActor(this.m_entityDef, this.m_premium);
		if (this.m_shownActor == null)
		{
			return;
		}
		this.m_shownActor.SetEntityDef(this.m_entityDef);
		this.m_shownActor.SetPremium(this.m_premium);
		DefLoader.DisposableCardDef cardDef = this.m_cardDef;
		if (cardDef != null)
		{
			cardDef.Dispose();
		}
		this.m_cardDef = DefLoader.Get().GetCardDef(this.m_entityDef.GetCardId(), new CardPortraitQuality(3, this.m_premium));
		this.m_shownActor.SetCardDef(this.m_cardDef);
		this.m_shownActor.GhostCardEffect(this.m_ghosted, this.m_premium, true);
		if (this.m_shownActor.isGhostCard())
		{
			GhostCard component = this.m_shownActor.m_ghostCardGameObject.GetComponent<GhostCard>();
			component.SetRenderQueue(DeckBigCard.GHOST_CARD_RENDER_QUEUE);
			component.SetBigCard(true);
		}
		if (this.m_showTooltipsForAdventure && !this.m_shownActor.GetEntityDef().IsHero())
		{
			TooltipPanelManager.Get().UpdateKeywordHelpForAdventure(this.m_shownActor.GetEntityDef(), this.m_shownActor);
		}
		this.m_shownActor.UpdateAllComponents();
		if (this.m_missingCardMaterial != null)
		{
			this.m_shownActor.SetMissingCardMaterial(this.m_missingCardMaterial);
		}
		this.m_shownActor.Show();
		CollectibleDisplay collectibleDisplay = CollectionManager.Get().GetCollectibleDisplay();
		bool flag = collectibleDisplay != null && collectibleDisplay.GetViewMode() == CollectionUtils.ViewMode.DECK_TEMPLATE;
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (editedDeck != null && editedDeck.Locked)
		{
			return;
		}
		if (this.m_ghosted != GhostCard.Type.NONE)
		{
			TooltipPanelManager.Orientation orientation = TooltipPanelManager.Orientation.LeftMiddle;
			if (UniversalInputManager.UsePhoneUI && flag)
			{
				orientation = ((sourceZ > 0f) ? TooltipPanelManager.Orientation.RightBottom : TooltipPanelManager.Orientation.RightTop);
			}
			TooltipPanelManager.Get().UpdateGhostCardHelpForCollectionManager(this.m_shownActor, this.m_ghosted, orientation);
		}
		if (this.m_shownActor.GetEntityDef().IsHero() && !this.m_hideBigHeroPower)
		{
			string heroPowerCardIdFromHero = GameUtils.GetHeroPowerCardIdFromHero(this.m_shownActor.GetEntityDef().GetCardId());
			this.ShowHeroPowerCard(heroPowerCardIdFromHero, this.m_shownActor.GetPremium());
		}
		if (this.m_createdByText != null && !string.IsNullOrEmpty(this.m_createdByText.Text))
		{
			this.m_createdByText.gameObject.SetActive(true);
		}
		if (this.OnBigCardShown != null)
		{
			this.OnBigCardShown(this.m_shownActor, this.m_entityDef);
		}
	}

	// Token: 0x0600133E RID: 4926 RVA: 0x0006E608 File Offset: 0x0006C808
	private void Hide()
	{
		base.StopCoroutine("ShowWithDelayInternal");
		if (this.m_showTooltipsForAdventure)
		{
			TooltipPanelManager.Get().HideKeywordHelp();
		}
		if (this.m_createdByText != null)
		{
			this.m_createdByText.gameObject.SetActive(false);
		}
		this.m_shown = false;
		if (this.m_shownActor == null)
		{
			return;
		}
		this.m_shownActor.Hide();
		this.m_shownActor = null;
		if (!this.m_hideBigHeroPower)
		{
			this.HideHeroPowerCard();
		}
		TooltipPanelManager.Get().HideTooltipPanels();
	}

	// Token: 0x0600133F RID: 4927 RVA: 0x0006E694 File Offset: 0x0006C894
	private void ShowHeroPowerCard(string heroPowerCardId, TAG_PREMIUM premium)
	{
		AssetLoader.Get().InstantiatePrefab(ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.HISTORY_HERO_POWER, premium), delegate(AssetReference actorName, GameObject actorGameObject, object data)
		{
			this.OnHeroPowerActorLoaded(actorName, actorGameObject, heroPowerCardId, premium);
		}, heroPowerCardId, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x06001340 RID: 4928 RVA: 0x0006E6EC File Offset: 0x0006C8EC
	private void OnHeroPowerActorLoaded(AssetReference assetRef, GameObject actorGameObject, string heroPowerCardId, TAG_PREMIUM premium)
	{
		if (actorGameObject == null)
		{
			Debug.LogError(string.Format("CollectionDeckTray.OnDeckBigHeroPowerActorLoaded: Unable to load actor for hero power: {0}", assetRef));
			return;
		}
		if (this.m_shownHeroPowerActor != null)
		{
			UnityEngine.Object.Destroy(this.m_shownHeroPowerActor.gameObject);
		}
		this.m_shownHeroPowerActor = actorGameObject.GetComponent<Actor>();
		if (!this.m_shown)
		{
			this.HideHeroPowerCard();
			return;
		}
		this.m_shownHeroPowerActor.Show();
		if (this.m_disableCollidersOnHeroPower)
		{
			this.m_shownHeroPowerActor.TurnOffCollider();
		}
		using (DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(heroPowerCardId, this.m_shownHeroPowerActor.CardPortraitQuality))
		{
			this.m_shownHeroPowerActor.SetCardDef(fullDef.DisposableCardDef);
			this.m_shownHeroPowerActor.SetEntityDef(fullDef.EntityDef);
			this.m_shownHeroPowerActor.SetPremium(premium);
			this.m_shownHeroPowerActor.GhostCardEffect(this.m_ghosted, premium, true);
			if (this.m_shownActor.isGhostCard())
			{
				this.m_shownHeroPowerActor.m_ghostCardGameObject.GetComponent<GhostCard>().SetRenderQueue(DeckBigCard.GHOST_CARD_RENDER_QUEUE);
			}
			this.m_shownHeroPowerActor.SetUnlit();
			this.m_shownHeroPowerActor.UpdateAllComponents();
			this.m_shownHeroPowerActor.gameObject.transform.parent = base.transform;
			this.m_shownHeroPowerActor.transform.localPosition = DeckBigCard.HERO_POWER_START_POSITION;
			this.m_shownHeroPowerActor.transform.localScale = DeckBigCard.HERO_POWER_START_SCALE;
			Vector3 vector = DeckBigCard.HERO_POWER_POSITION;
			Vector3 vector2 = DeckBigCard.HERO_POWER_SCALE;
			if (this.m_flipHeroPowerHorizontalPosition)
			{
				vector.x = -vector.x;
			}
			iTween.MoveTo(this.m_shownHeroPowerActor.gameObject, iTween.Hash(new object[]
			{
				"position",
				vector,
				"isLocal",
				true,
				"time",
				DeckBigCard.HERO_POWER_TWEEN_TIME
			}));
			iTween.ScaleTo(this.m_shownHeroPowerActor.gameObject, iTween.Hash(new object[]
			{
				"scale",
				vector2,
				"isLocal",
				true,
				"time",
				DeckBigCard.HERO_POWER_TWEEN_TIME
			}));
		}
	}

	// Token: 0x06001341 RID: 4929 RVA: 0x0006E948 File Offset: 0x0006CB48
	private void HideHeroPowerCard()
	{
		if (this.m_shownHeroPowerActor != null)
		{
			UnityEngine.Object.Destroy(this.m_shownHeroPowerActor.gameObject);
		}
	}

	// Token: 0x04000C6E RID: 3182
	public GameObject m_topPosition;

	// Token: 0x04000C6F RID: 3183
	public GameObject m_bottomPosition;

	// Token: 0x04000C70 RID: 3184
	public Material m_missingCardMaterial;

	// Token: 0x04000C71 RID: 3185
	public Material m_ghostCardMaterial;

	// Token: 0x04000C72 RID: 3186
	public Material m_invalidCardMaterial;

	// Token: 0x04000C73 RID: 3187
	public bool m_disableCollidersOnHeroPower;

	// Token: 0x04000C74 RID: 3188
	public bool m_flipHeroPowerHorizontalPosition;

	// Token: 0x04000C75 RID: 3189
	public bool m_showTooltipsForAdventure;

	// Token: 0x04000C76 RID: 3190
	public UberText m_createdByText;

	// Token: 0x04000C77 RID: 3191
	public Vector3 m_positionOffsetWithCreatorBanner;

	// Token: 0x04000C78 RID: 3192
	public float m_scaleMultiplierWithCreatorBanner = 1f;

	// Token: 0x04000C79 RID: 3193
	private HandActorCache m_actorCache = new HandActorCache();

	// Token: 0x04000C7A RID: 3194
	private bool m_actorCacheInit;

	// Token: 0x04000C7B RID: 3195
	private bool m_hideBigHeroPower;

	// Token: 0x04000C7C RID: 3196
	private bool m_shown;

	// Token: 0x04000C7D RID: 3197
	private EntityDef m_entityDef;

	// Token: 0x04000C7E RID: 3198
	private TAG_PREMIUM m_premium;

	// Token: 0x04000C7F RID: 3199
	private DefLoader.DisposableCardDef m_cardDef;

	// Token: 0x04000C80 RID: 3200
	private Actor m_shownActor;

	// Token: 0x04000C81 RID: 3201
	private Actor m_shownHeroPowerActor;

	// Token: 0x04000C82 RID: 3202
	private GhostCard.Type m_ghosted;

	// Token: 0x04000C83 RID: 3203
	private int m_firstShowFrame;

	// Token: 0x04000C84 RID: 3204
	private Vector3 m_defaultLocalPosition;

	// Token: 0x04000C85 RID: 3205
	private Vector3 m_defaultLocalScale;

	// Token: 0x04000C86 RID: 3206
	private static readonly PlatformDependentValue<Vector3> HERO_POWER_START_POSITION = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(0f, -0.5f, 0f)
	};

	// Token: 0x04000C87 RID: 3207
	private static readonly PlatformDependentValue<Vector3> HERO_POWER_START_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(0f, 0f, 0f)
	};

	// Token: 0x04000C88 RID: 3208
	private static readonly PlatformDependentValue<Vector3> HERO_POWER_POSITION = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(-2.11f, 0f, -0.12f),
		Phone = new Vector3(-2.05f, 0f, -0.12f)
	};

	// Token: 0x04000C89 RID: 3209
	private static readonly PlatformDependentValue<Vector3> HERO_POWER_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(0.8117157f, 0.8117157f, 0.8117157f),
		Phone = new Vector3(0.8117157f, 0.8117157f, 0.8117157f)
	};

	// Token: 0x04000C8A RID: 3210
	private static readonly float HERO_POWER_TWEEN_TIME = 0.5f;

	// Token: 0x04000C8B RID: 3211
	private static readonly int GHOST_CARD_RENDER_QUEUE = 72;

	// Token: 0x020014AD RID: 5293
	// (Invoke) Token: 0x0600DBCA RID: 56266
	public delegate void OnBigCardShownHandler(Actor shownActor, EntityDef entityDef);
}
