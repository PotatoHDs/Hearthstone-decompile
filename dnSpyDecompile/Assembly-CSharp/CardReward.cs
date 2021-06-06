using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

// Token: 0x02000666 RID: 1638
public class CardReward : Reward
{
	// Token: 0x06005C17 RID: 23575 RVA: 0x001DEE44 File Offset: 0x001DD044
	public void MakeActorsUnlit()
	{
		foreach (Actor actor in this.m_actors)
		{
			actor.SetUnlit();
		}
	}

	// Token: 0x06005C18 RID: 23576 RVA: 0x001DEE94 File Offset: 0x001DD094
	protected override void InitData()
	{
		base.SetData(new CardRewardData(), false);
	}

	// Token: 0x06005C19 RID: 23577 RVA: 0x001DEEA4 File Offset: 0x001DD0A4
	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		CardRewardData cardRewardData = base.Data as CardRewardData;
		if (cardRewardData == null)
		{
			Debug.LogWarning(string.Format("CardReward.OnDataSet() - data {0} is not CardRewardData", base.Data));
			return;
		}
		if (string.IsNullOrEmpty(cardRewardData.CardID))
		{
			Debug.LogWarning(string.Format("CardReward.OnDataSet() - data {0} has invalid cardID", cardRewardData));
			return;
		}
		base.SetReady(false);
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardRewardData.CardID);
		if (entityDef.IsHeroSkin())
		{
			AssetLoader.Get().InstantiatePrefab("Card_Play_Hero.prefab:42cbbd2c4969afb46b3887bb628de19d", new PrefabCallback<GameObject>(this.OnHeroActorLoaded), entityDef, AssetLoadingOptions.IgnorePrefabPosition);
			this.m_goToRotate = this.m_heroCardRoot;
			this.m_cardCount.Hide();
			if (cardRewardData.Premium == TAG_PREMIUM.GOLDEN)
			{
				this.SetUpGoldenHeroAchieves();
				return;
			}
			if (GameUtils.IsVanillaHero(cardRewardData.CardID))
			{
				this.SetupHeroAchieves();
				return;
			}
		}
		else
		{
			if (UniversalInputManager.UsePhoneUI || !this.m_showCardCount)
			{
				this.m_cardCount.Hide();
			}
			AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(entityDef, cardRewardData.Premium), new PrefabCallback<GameObject>(this.OnActorLoaded), entityDef, AssetLoadingOptions.IgnorePrefabPosition);
			this.m_goToRotate = this.m_nonHeroCardsRoot;
		}
	}

	// Token: 0x06005C1A RID: 23578 RVA: 0x001DEFCC File Offset: 0x001DD1CC
	protected override void ShowReward(bool updateCacheValues)
	{
		CardRewardData cardRewardData = base.Data as CardRewardData;
		this.InitRewardText();
		if (((cardRewardData.FixedReward != null && cardRewardData.FixedReward.UseQuestToast) || !GameUtils.IsVanillaHero(cardRewardData.CardID)) && DefLoader.Get().GetEntityDef(cardRewardData.CardID).IsHeroSkin() && this.m_rewardBanner != null)
		{
			this.m_rewardBanner.gameObject.SetActive(false);
		}
		if (!this.m_showCardCount)
		{
			this.m_cardCount.Hide();
		}
		this.m_root.SetActive(true);
		if (this.m_RotateIn)
		{
			this.m_goToRotate.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
			Hashtable args = iTween.Hash(new object[]
			{
				"amount",
				new Vector3(0f, 0f, 540f),
				"time",
				1.5f,
				"easeType",
				iTween.EaseType.easeOutElastic,
				"space",
				Space.Self
			});
			iTween.RotateAdd(this.m_goToRotate.gameObject, args);
		}
		SoundManager.Get().LoadAndPlay("game_end_reward.prefab:6c28275a79f151a478d49afc04533e72");
		this.PlayHeroEmote();
	}

	// Token: 0x06005C1B RID: 23579 RVA: 0x001DDEDB File Offset: 0x001DC0DB
	protected override void HideReward()
	{
		base.HideReward();
		this.m_root.SetActive(false);
	}

	// Token: 0x06005C1C RID: 23580 RVA: 0x001DF124 File Offset: 0x001DD324
	private void OnFullDefLoaded(string cardID, DefLoader.DisposableFullDef fullDef, object callbackData)
	{
		try
		{
			if (fullDef == null)
			{
				Debug.LogWarning(string.Format("CardReward.OnFullDefLoaded() - fullDef for CardID {0} is null", cardID));
			}
			else if (fullDef.EntityDef == null)
			{
				Debug.LogWarning(string.Format("CardReward.OnFullDefLoaded() - entityDef for CardID {0} is null", cardID));
			}
			else if (fullDef.CardDef == null)
			{
				Debug.LogWarning(string.Format("CardReward.OnFullDefLoaded() - cardDef for CardID {0} is null", cardID));
			}
			else
			{
				foreach (Actor actor in this.m_actors)
				{
					this.FinishSettingUpActor(actor, fullDef.DisposableCardDef);
				}
				foreach (EmoteEntryDef emoteEntryDef in fullDef.CardDef.m_EmoteDefs)
				{
					if (emoteEntryDef.m_emoteType == EmoteType.START)
					{
						AssetLoader.Get().InstantiatePrefab(emoteEntryDef.m_emoteSoundSpellPath, new PrefabCallback<GameObject>(this.OnStartEmoteLoaded), null, AssetLoadingOptions.None);
					}
				}
				base.SetReady(true);
			}
		}
		finally
		{
			if (fullDef != null)
			{
				((IDisposable)fullDef).Dispose();
			}
		}
	}

	// Token: 0x06005C1D RID: 23581 RVA: 0x001DF28C File Offset: 0x001DD48C
	private void OnStartEmoteLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			return;
		}
		CardSoundSpell component = go.GetComponent<CardSoundSpell>();
		if (component == null)
		{
			return;
		}
		this.m_emote = component;
	}

	// Token: 0x06005C1E RID: 23582 RVA: 0x001DF2BB File Offset: 0x001DD4BB
	private void PlayHeroEmote()
	{
		if (this.m_emote == null)
		{
			return;
		}
		this.m_emote.Reactivate();
	}

	// Token: 0x06005C1F RID: 23583 RVA: 0x001DF2D8 File Offset: 0x001DD4D8
	private void OnHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		EntityDef entityDef = (EntityDef)callbackData;
		Actor component = go.GetComponent<Actor>();
		component.SetEntityDef(entityDef);
		component.transform.parent = this.m_heroCardRoot.transform;
		component.transform.localScale = Vector3.one;
		component.transform.localPosition = Vector3.zero;
		component.transform.localRotation = Quaternion.identity;
		component.TurnOffCollider();
		component.m_healthObject.SetActive(false);
		CardRewardData cardRewardData = base.Data as CardRewardData;
		if ((cardRewardData.FixedReward != null && cardRewardData.FixedReward.UseQuestToast) || !GameUtils.IsVanillaHero(cardRewardData.CardID))
		{
			PlatformDependentValue<Vector3> val = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(1.35f, 1.35f, 1.35f),
				Phone = new Vector3(1.3f, 1.3f, 1.3f)
			};
			PlatformDependentValue<Vector3> val2 = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(0f, 0f, -0.2f),
				Phone = new Vector3(0f, 0f, -0.3f)
			};
			component.transform.localScale = val;
			component.transform.localPosition = val2;
		}
		SceneUtils.SetLayer(component.gameObject, GameLayer.IgnoreFullScreenEffects);
		this.m_actors.Add(component);
		DefLoader.Get().LoadFullDef(entityDef.GetCardId(), new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnFullDefLoaded), new CardPortraitQuality(3, true), null);
	}

	// Token: 0x06005C20 RID: 23584 RVA: 0x001DF45C File Offset: 0x001DD65C
	private void OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		EntityDef entityDef = (EntityDef)callbackData;
		if (go == null)
		{
			Log.MissingAssets.PrintWarning("CardReward.OnActorLoaded - Failed to load actor {0}", new object[]
			{
				assetRef
			});
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Log.MissingAssets.PrintWarning("CardReward.OnActorLoaded - No actor found in {0}", new object[]
			{
				assetRef
			});
			return;
		}
		this.StartSettingUpNonHeroActor(component, entityDef, this.m_cardParent.transform);
		CardRewardData cardRewardData = base.Data as CardRewardData;
		this.m_cardCount.SetCount(cardRewardData.Count);
		if (cardRewardData.Count > 1)
		{
			Actor actor = UnityEngine.Object.Instantiate<Actor>(component);
			this.StartSettingUpNonHeroActor(actor, entityDef, this.m_duplicateCardParent.transform);
		}
		DefLoader.Get().LoadFullDef(entityDef.GetCardId(), new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnFullDefLoaded), entityDef, new CardPortraitQuality(3, true));
	}

	// Token: 0x06005C21 RID: 23585 RVA: 0x001DF534 File Offset: 0x001DD734
	private void StartSettingUpNonHeroActor(Actor actor, EntityDef entityDef, Transform parentTransform)
	{
		actor.SetEntityDef(entityDef);
		actor.transform.parent = parentTransform;
		TAG_CARDTYPE cardType = entityDef.GetCardType();
		if (!CardReward.CARD_SCALE.ContainsKey(cardType))
		{
			Debug.LogWarning("CardReward - No CARD_SCALE exists for card type " + cardType);
			actor.transform.localScale = CardReward.CARD_SCALE[TAG_CARDTYPE.MINION];
		}
		else
		{
			actor.transform.localScale = CardReward.CARD_SCALE[cardType];
		}
		actor.transform.localPosition = Vector3.zero;
		actor.transform.localRotation = Quaternion.identity;
		actor.TurnOffCollider();
		if (base.Data.Origin != NetCache.ProfileNotice.NoticeOrigin.ACHIEVEMENT)
		{
			SceneUtils.SetLayer(actor.gameObject, GameLayer.IgnoreFullScreenEffects);
		}
		this.m_actors.Add(actor);
	}

	// Token: 0x06005C22 RID: 23586 RVA: 0x001DF5F8 File Offset: 0x001DD7F8
	private void FinishSettingUpActor(Actor actor, DefLoader.DisposableCardDef cardDef)
	{
		CardRewardData cardRewardData = base.Data as CardRewardData;
		actor.SetCardDef(cardDef);
		actor.SetPremium(cardRewardData.Premium);
		actor.CreateBannedRibbon();
		actor.UpdateAllComponents();
	}

	// Token: 0x06005C23 RID: 23587 RVA: 0x001DF630 File Offset: 0x001DD830
	private void SetupHeroAchieves()
	{
		List<global::Achievement> achievesInGroup = AchieveManager.Get().GetAchievesInGroup(Achieve.Type.HERO);
		List<global::Achievement> achievesInGroup2 = AchieveManager.Get().GetAchievesInGroup(Achieve.Type.HERO, true);
		int count = achievesInGroup.Count;
		int count2 = achievesInGroup2.Count;
		CardRewardData cardRewardData = base.Data as CardRewardData;
		string className = GameStrings.GetClassName(DefLoader.Get().GetEntityDef(cardRewardData.CardID).GetClass());
		string headline = GameStrings.Format("GLOBAL_REWARD_HERO_HEADLINE", new object[]
		{
			className
		});
		string details = GameStrings.Format("GLOBAL_REWARD_HERO_DETAILS", new object[]
		{
			count2,
			count
		});
		base.SetRewardText(headline, details, string.Empty);
	}

	// Token: 0x06005C24 RID: 23588 RVA: 0x001DF6D4 File Offset: 0x001DD8D4
	private void SetUpGoldenHeroAchieves()
	{
		string headline = GameStrings.Get("GLOBAL_REWARD_GOLDEN_HERO_HEADLINE");
		base.SetRewardText(headline, string.Empty, string.Empty);
	}

	// Token: 0x06005C25 RID: 23589 RVA: 0x001DF700 File Offset: 0x001DD900
	private void InitRewardText()
	{
		CardRewardData cardRewardData = base.Data as CardRewardData;
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardRewardData.CardID);
		if (!entityDef.IsHeroSkin())
		{
			string headline = GameStrings.Get("GLOBAL_REWARD_CARD_HEADLINE");
			string details = string.Empty;
			string source = string.Empty;
			entityDef.GetCardSet();
			TAG_CLASS @class = entityDef.GetClass();
			string className = GameStrings.GetClassName(@class);
			if (GameMgr.Get().IsTutorial())
			{
				details = GameUtils.GetCurrentTutorialCardRewardDetails();
			}
			else if (entityDef.IsCoreCard())
			{
				int num = 16;
				if (@class == TAG_CLASS.NEUTRAL)
				{
					num = 75;
				}
				int coreCardsIOwn = CollectionManager.Get().GetCoreCardsIOwn(@class);
				if (cardRewardData.Premium == TAG_PREMIUM.GOLDEN)
				{
					details = string.Empty;
				}
				else
				{
					if (num == coreCardsIOwn)
					{
						cardRewardData.InnKeeperLine = CardRewardData.InnKeeperTrigger.CORE_CLASS_SET_COMPLETE;
					}
					else if (coreCardsIOwn == 4)
					{
						cardRewardData.InnKeeperLine = CardRewardData.InnKeeperTrigger.SECOND_REWARD_EVER;
					}
					details = GameStrings.Format("GLOBAL_REWARD_CORE_CARD_DETAILS", new object[]
					{
						coreCardsIOwn,
						num,
						className
					});
				}
			}
			if (base.Data.Origin == NetCache.ProfileNotice.NoticeOrigin.LEVEL_UP)
			{
				TAG_CLASS tag_CLASS = (TAG_CLASS)base.Data.OriginData;
				NetCache.HeroLevel heroLevel = GameUtils.GetHeroLevel(tag_CLASS);
				source = GameStrings.Format("GLOBAL_REWARD_CARD_LEVEL_UP", new object[]
				{
					heroLevel.CurrentLevel.Level.ToString(),
					GameStrings.GetClassName(tag_CLASS)
				});
			}
			else
			{
				source = string.Empty;
			}
			base.SetRewardText(headline, details, source);
		}
	}

	// Token: 0x04004E63 RID: 20067
	public GameObject m_nonHeroCardsRoot;

	// Token: 0x04004E64 RID: 20068
	public GameObject m_heroCardRoot;

	// Token: 0x04004E65 RID: 20069
	public GameObject m_cardParent;

	// Token: 0x04004E66 RID: 20070
	public GameObject m_duplicateCardParent;

	// Token: 0x04004E67 RID: 20071
	public CardRewardCount m_cardCount;

	// Token: 0x04004E68 RID: 20072
	public bool m_showCardCount = true;

	// Token: 0x04004E69 RID: 20073
	public bool m_RotateIn = true;

	// Token: 0x04004E6A RID: 20074
	private static readonly Map<TAG_CARDTYPE, Vector3> CARD_SCALE = new Map<TAG_CARDTYPE, Vector3>
	{
		{
			TAG_CARDTYPE.SPELL,
			new Vector3(1f, 1f, 1f)
		},
		{
			TAG_CARDTYPE.MINION,
			new Vector3(1f, 1f, 1f)
		},
		{
			TAG_CARDTYPE.WEAPON,
			new Vector3(1f, 0.5f, 1f)
		},
		{
			TAG_CARDTYPE.HERO,
			new Vector3(1f, 1f, 1f)
		}
	};

	// Token: 0x04004E6B RID: 20075
	private List<Actor> m_actors = new List<Actor>();

	// Token: 0x04004E6C RID: 20076
	private GameObject m_goToRotate;

	// Token: 0x04004E6D RID: 20077
	private CardSoundSpell m_emote;
}
