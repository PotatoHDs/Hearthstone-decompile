using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class CardReward : Reward
{
	public GameObject m_nonHeroCardsRoot;

	public GameObject m_heroCardRoot;

	public GameObject m_cardParent;

	public GameObject m_duplicateCardParent;

	public CardRewardCount m_cardCount;

	public bool m_showCardCount = true;

	public bool m_RotateIn = true;

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

	private List<Actor> m_actors = new List<Actor>();

	private GameObject m_goToRotate;

	private CardSoundSpell m_emote;

	public void MakeActorsUnlit()
	{
		foreach (Actor actor in m_actors)
		{
			actor.SetUnlit();
		}
	}

	protected override void InitData()
	{
		SetData(new CardRewardData(), updateVisuals: false);
	}

	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		CardRewardData cardRewardData = base.Data as CardRewardData;
		if (cardRewardData == null)
		{
			Debug.LogWarning($"CardReward.OnDataSet() - data {base.Data} is not CardRewardData");
			return;
		}
		if (string.IsNullOrEmpty(cardRewardData.CardID))
		{
			Debug.LogWarning($"CardReward.OnDataSet() - data {cardRewardData} has invalid cardID");
			return;
		}
		SetReady(ready: false);
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardRewardData.CardID);
		if (entityDef.IsHeroSkin())
		{
			AssetLoader.Get().InstantiatePrefab("Card_Play_Hero.prefab:42cbbd2c4969afb46b3887bb628de19d", OnHeroActorLoaded, entityDef, AssetLoadingOptions.IgnorePrefabPosition);
			m_goToRotate = m_heroCardRoot;
			m_cardCount.Hide();
			if (cardRewardData.Premium == TAG_PREMIUM.GOLDEN)
			{
				SetUpGoldenHeroAchieves();
			}
			else if (GameUtils.IsVanillaHero(cardRewardData.CardID))
			{
				SetupHeroAchieves();
			}
		}
		else
		{
			if ((bool)UniversalInputManager.UsePhoneUI || !m_showCardCount)
			{
				m_cardCount.Hide();
			}
			AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(entityDef, cardRewardData.Premium), OnActorLoaded, entityDef, AssetLoadingOptions.IgnorePrefabPosition);
			m_goToRotate = m_nonHeroCardsRoot;
		}
	}

	protected override void ShowReward(bool updateCacheValues)
	{
		CardRewardData cardRewardData = base.Data as CardRewardData;
		InitRewardText();
		if (((cardRewardData.FixedReward != null && cardRewardData.FixedReward.UseQuestToast) || !GameUtils.IsVanillaHero(cardRewardData.CardID)) && DefLoader.Get().GetEntityDef(cardRewardData.CardID).IsHeroSkin() && m_rewardBanner != null)
		{
			m_rewardBanner.gameObject.SetActive(value: false);
		}
		if (!m_showCardCount)
		{
			m_cardCount.Hide();
		}
		m_root.SetActive(value: true);
		if (m_RotateIn)
		{
			m_goToRotate.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
			Hashtable args = iTween.Hash("amount", new Vector3(0f, 0f, 540f), "time", 1.5f, "easeType", iTween.EaseType.easeOutElastic, "space", Space.Self);
			iTween.RotateAdd(m_goToRotate.gameObject, args);
		}
		SoundManager.Get().LoadAndPlay("game_end_reward.prefab:6c28275a79f151a478d49afc04533e72");
		PlayHeroEmote();
	}

	protected override void HideReward()
	{
		base.HideReward();
		m_root.SetActive(value: false);
	}

	private void OnFullDefLoaded(string cardID, DefLoader.DisposableFullDef fullDef, object callbackData)
	{
		using (fullDef)
		{
			if (fullDef == null)
			{
				Debug.LogWarning($"CardReward.OnFullDefLoaded() - fullDef for CardID {cardID} is null");
				return;
			}
			if (fullDef.EntityDef == null)
			{
				Debug.LogWarning($"CardReward.OnFullDefLoaded() - entityDef for CardID {cardID} is null");
				return;
			}
			if (fullDef.CardDef == null)
			{
				Debug.LogWarning($"CardReward.OnFullDefLoaded() - cardDef for CardID {cardID} is null");
				return;
			}
			foreach (Actor actor in m_actors)
			{
				FinishSettingUpActor(actor, fullDef.DisposableCardDef);
			}
			foreach (EmoteEntryDef emoteDef in fullDef.CardDef.m_EmoteDefs)
			{
				if (emoteDef.m_emoteType == EmoteType.START)
				{
					AssetLoader.Get().InstantiatePrefab(emoteDef.m_emoteSoundSpellPath, OnStartEmoteLoaded);
				}
			}
			SetReady(ready: true);
		}
	}

	private void OnStartEmoteLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (!(go == null))
		{
			CardSoundSpell component = go.GetComponent<CardSoundSpell>();
			if (!(component == null))
			{
				m_emote = component;
			}
		}
	}

	private void PlayHeroEmote()
	{
		if (!(m_emote == null))
		{
			m_emote.Reactivate();
		}
	}

	private void OnHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		EntityDef entityDef = (EntityDef)callbackData;
		Actor component = go.GetComponent<Actor>();
		component.SetEntityDef(entityDef);
		component.transform.parent = m_heroCardRoot.transform;
		component.transform.localScale = Vector3.one;
		component.transform.localPosition = Vector3.zero;
		component.transform.localRotation = Quaternion.identity;
		component.TurnOffCollider();
		component.m_healthObject.SetActive(value: false);
		CardRewardData cardRewardData = base.Data as CardRewardData;
		if ((cardRewardData.FixedReward != null && cardRewardData.FixedReward.UseQuestToast) || !GameUtils.IsVanillaHero(cardRewardData.CardID))
		{
			PlatformDependentValue<Vector3> platformDependentValue = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(1.35f, 1.35f, 1.35f),
				Phone = new Vector3(1.3f, 1.3f, 1.3f)
			};
			PlatformDependentValue<Vector3> platformDependentValue2 = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(0f, 0f, -0.2f),
				Phone = new Vector3(0f, 0f, -0.3f)
			};
			component.transform.localScale = platformDependentValue;
			component.transform.localPosition = platformDependentValue2;
		}
		SceneUtils.SetLayer(component.gameObject, GameLayer.IgnoreFullScreenEffects);
		m_actors.Add(component);
		DefLoader.Get().LoadFullDef(entityDef.GetCardId(), OnFullDefLoaded, new CardPortraitQuality(3, loadPremium: true));
	}

	private void OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		EntityDef entityDef = (EntityDef)callbackData;
		if (go == null)
		{
			Log.MissingAssets.PrintWarning("CardReward.OnActorLoaded - Failed to load actor {0}", assetRef);
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Log.MissingAssets.PrintWarning("CardReward.OnActorLoaded - No actor found in {0}", assetRef);
			return;
		}
		StartSettingUpNonHeroActor(component, entityDef, m_cardParent.transform);
		CardRewardData cardRewardData = base.Data as CardRewardData;
		m_cardCount.SetCount(cardRewardData.Count);
		if (cardRewardData.Count > 1)
		{
			Actor actor = Object.Instantiate(component);
			StartSettingUpNonHeroActor(actor, entityDef, m_duplicateCardParent.transform);
		}
		DefLoader.Get().LoadFullDef(entityDef.GetCardId(), OnFullDefLoaded, entityDef, new CardPortraitQuality(3, loadPremium: true));
	}

	private void StartSettingUpNonHeroActor(Actor actor, EntityDef entityDef, Transform parentTransform)
	{
		actor.SetEntityDef(entityDef);
		actor.transform.parent = parentTransform;
		TAG_CARDTYPE cardType = entityDef.GetCardType();
		if (!CARD_SCALE.ContainsKey(cardType))
		{
			Debug.LogWarning("CardReward - No CARD_SCALE exists for card type " + cardType);
			actor.transform.localScale = CARD_SCALE[TAG_CARDTYPE.MINION];
		}
		else
		{
			actor.transform.localScale = CARD_SCALE[cardType];
		}
		actor.transform.localPosition = Vector3.zero;
		actor.transform.localRotation = Quaternion.identity;
		actor.TurnOffCollider();
		if (base.Data.Origin != NetCache.ProfileNotice.NoticeOrigin.ACHIEVEMENT)
		{
			SceneUtils.SetLayer(actor.gameObject, GameLayer.IgnoreFullScreenEffects);
		}
		m_actors.Add(actor);
	}

	private void FinishSettingUpActor(Actor actor, DefLoader.DisposableCardDef cardDef)
	{
		CardRewardData cardRewardData = base.Data as CardRewardData;
		actor.SetCardDef(cardDef);
		actor.SetPremium(cardRewardData.Premium);
		actor.CreateBannedRibbon();
		actor.UpdateAllComponents();
	}

	private void SetupHeroAchieves()
	{
		List<Achievement> achievesInGroup = AchieveManager.Get().GetAchievesInGroup(Achieve.Type.HERO);
		List<Achievement> achievesInGroup2 = AchieveManager.Get().GetAchievesInGroup(Achieve.Type.HERO, isComplete: true);
		int count = achievesInGroup.Count;
		int count2 = achievesInGroup2.Count;
		CardRewardData cardRewardData = base.Data as CardRewardData;
		string className = GameStrings.GetClassName(DefLoader.Get().GetEntityDef(cardRewardData.CardID).GetClass());
		string headline = GameStrings.Format("GLOBAL_REWARD_HERO_HEADLINE", className);
		string details = GameStrings.Format("GLOBAL_REWARD_HERO_DETAILS", count2, count);
		SetRewardText(headline, details, string.Empty);
	}

	private void SetUpGoldenHeroAchieves()
	{
		string headline = GameStrings.Get("GLOBAL_REWARD_GOLDEN_HERO_HEADLINE");
		SetRewardText(headline, string.Empty, string.Empty);
	}

	private void InitRewardText()
	{
		CardRewardData cardRewardData = base.Data as CardRewardData;
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardRewardData.CardID);
		if (entityDef.IsHeroSkin())
		{
			return;
		}
		string headline = GameStrings.Get("GLOBAL_REWARD_CARD_HEADLINE");
		string details = string.Empty;
		string empty = string.Empty;
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
				details = GameStrings.Format("GLOBAL_REWARD_CORE_CARD_DETAILS", coreCardsIOwn, num, className);
			}
		}
		if (base.Data.Origin == NetCache.ProfileNotice.NoticeOrigin.LEVEL_UP)
		{
			TAG_CLASS heroClass = (TAG_CLASS)base.Data.OriginData;
			NetCache.HeroLevel heroLevel = GameUtils.GetHeroLevel(heroClass);
			empty = GameStrings.Format("GLOBAL_REWARD_CARD_LEVEL_UP", heroLevel.CurrentLevel.Level.ToString(), GameStrings.GetClassName(heroClass));
		}
		else
		{
			empty = string.Empty;
		}
		SetRewardText(headline, details, empty);
	}
}
