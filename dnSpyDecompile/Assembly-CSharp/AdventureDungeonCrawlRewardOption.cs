using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.DungeonCrawl;
using PegasusShared;
using UnityEngine;

// Token: 0x0200003D RID: 61
[CustomEditClass]
public class AdventureDungeonCrawlRewardOption : MonoBehaviour
{
	// Token: 0x06000301 RID: 769 RVA: 0x00013119 File Offset: 0x00011319
	public void Initalize(IDungeonCrawlData data)
	{
		this.m_dungeonRunData = data;
		this.SetRewardOptionVisualStyle();
	}

	// Token: 0x06000302 RID: 770 RVA: 0x00013128 File Offset: 0x00011328
	private void Start()
	{
		this.m_chooseButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.OptionChosen();
		});
	}

	// Token: 0x06000303 RID: 771 RVA: 0x00013144 File Offset: 0x00011344
	public void SetRewardData(AdventureDungeonCrawlRewardOption.OptionData optionData)
	{
		this.m_optionData = optionData;
		this.EnableInteraction();
		if (this.m_bigCardActor != null)
		{
			this.m_bigCardActor.Destroy();
			this.m_bigCardActor = null;
		}
		if (!AdventureDungeonCrawlRewardOption.OptionTypeIsTreasure(optionData.optionType))
		{
			if (optionData.optionType == AdventureDungeonCrawlPlayMat.OptionType.LOOT)
			{
				this.SetLootCrateContents(optionData);
			}
			return;
		}
		this.m_lootCrate.gameObject.SetActive(false);
		long num = (long)AdventureDungeonCrawlRewardOption.GetTreasureDatabaseID(optionData);
		if (num == 0L)
		{
			Log.Adventures.PrintWarning("Treasure choice has no dbId!", Array.Empty<object>());
			return;
		}
		string text = GameUtils.TranslateDbIdToCardId((int)num, false);
		if (text == null)
		{
			Log.Adventures.PrintWarning("AdventureDungeonCrawlRewardOption.SetRewardData() - No cardId for dbId {0}!", new object[]
			{
				num
			});
			return;
		}
		DefLoader.Get().LoadFullDef(text, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnTreasureFullDefLoaded), optionData, null);
	}

	// Token: 0x06000304 RID: 772 RVA: 0x00013218 File Offset: 0x00011418
	public Actor GetActorFromCardId(string cardId)
	{
		if (this.m_deckTray != null)
		{
			DeckTrayDeckTileVisual cardTileVisual = this.m_deckTray.GetCardsContent().GetCardTileVisual(cardId);
			if (cardTileVisual != null)
			{
				return cardTileVisual.GetActor();
			}
		}
		if (this.m_bigCardActor != null && this.m_bigCardActor.GetEntityDef() != null && this.m_bigCardActor.GetEntityDef().GetCardId() == cardId)
		{
			return this.m_bigCardActor;
		}
		return null;
	}

	// Token: 0x06000305 RID: 773 RVA: 0x00013290 File Offset: 0x00011490
	public int GetTreasureDatabaseID()
	{
		return AdventureDungeonCrawlRewardOption.GetTreasureDatabaseID(this.m_optionData);
	}

	// Token: 0x06000306 RID: 774 RVA: 0x0001329D File Offset: 0x0001149D
	public static int GetTreasureDatabaseID(AdventureDungeonCrawlRewardOption.OptionData optionData)
	{
		if (!AdventureDungeonCrawlRewardOption.OptionTypeIsTreasure(optionData.optionType))
		{
			return 0;
		}
		if (optionData.options.Count < 1)
		{
			return 0;
		}
		return (int)optionData.options[0];
	}

	// Token: 0x06000307 RID: 775 RVA: 0x000132CC File Offset: 0x000114CC
	private void OnTreasureFullDefLoaded(string cardID, DefLoader.DisposableFullDef def, object userData)
	{
		AdventureDungeonCrawlRewardOption.OnFullDefLoadedData onFullDefLoadedData = new AdventureDungeonCrawlRewardOption.OnFullDefLoadedData
		{
			optionData = (AdventureDungeonCrawlRewardOption.OptionData)userData,
			fullDef = def
		};
		AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(def.EntityDef, TAG_PREMIUM.NORMAL), new PrefabCallback<GameObject>(this.OnTreasureActorLoaded), onFullDefLoadedData, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x06000308 RID: 776 RVA: 0x00013328 File Offset: 0x00011528
	private void OnTreasureActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		AdventureDungeonCrawlRewardOption.OnFullDefLoadedData onFullDefLoadedData = (AdventureDungeonCrawlRewardOption.OnFullDefLoadedData)callbackData;
		using (onFullDefLoadedData.fullDef)
		{
			if (go == null)
			{
				Debug.LogWarning(string.Format("AdventureDungeonCrawlRewardOption.OnActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			}
			else
			{
				Actor component = go.GetComponent<Actor>();
				if (component == null)
				{
					Debug.LogWarning(string.Format("AdventureDungeonCrawlRewardOption.OnActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef));
				}
				else
				{
					component.SetPremium(TAG_PREMIUM.NORMAL);
					component.SetEntityDef(onFullDefLoadedData.fullDef.EntityDef);
					component.SetCardDef(onFullDefLoadedData.fullDef.DisposableCardDef);
					component.UpdateAllComponents();
					component.ContactShadow(true);
					component.transform.parent = this.m_bigCardBone;
					component.transform.localPosition = Vector3.zero;
					component.transform.localScale = Vector3.one;
					if (onFullDefLoadedData.optionData.optionType == AdventureDungeonCrawlPlayMat.OptionType.SHRINE_TREASURE)
					{
						this.ShrineClassBanner.SetActive(true);
						GameUtils.SetParent(this.ShrineClassBanner, component.GetCardTypeBannerAnchor(), true);
						this.ShrineClassBannerText.Text = GameStrings.GetClassName(onFullDefLoadedData.fullDef.EntityDef.GetClass());
						component.transform.localScale = Vector3.one * this.ShrineClassBannerScalePercent;
						component.transform.localPosition += this.ShrineCardPositionOffset;
					}
					component.Hide();
					CardSelectionHandler cardSelectionHandler = component.GetCollider().gameObject.AddComponent<CardSelectionHandler>();
					cardSelectionHandler.SetActor(component);
					cardSelectionHandler.SetChoiceNum(this.m_optionData.index + 1);
					cardSelectionHandler.SetChosenCallback(new CardSelectionHandler.CardChosenCallback(this.OptionChosen));
					this.m_bigCardActor = component;
				}
			}
		}
	}

	// Token: 0x06000309 RID: 777 RVA: 0x000134E0 File Offset: 0x000116E0
	public AdventureDungeonCrawlRewardOption.OptionData GetOptionData()
	{
		return this.m_optionData;
	}

	// Token: 0x0600030A RID: 778 RVA: 0x000134E8 File Offset: 0x000116E8
	public void SetOptionChosenCallback(AdventureDungeonCrawlRewardOption.OptionChosenCallback callback)
	{
		this.m_optionChosenCallback = callback;
	}

	// Token: 0x0600030B RID: 779 RVA: 0x000134F1 File Offset: 0x000116F1
	public bool IsInitialized()
	{
		return !AdventureDungeonCrawlRewardOption.OptionTypeIsTreasure(this.m_optionData.optionType) || this.m_bigCardActor != null;
	}

	// Token: 0x0600030C RID: 780 RVA: 0x00013514 File Offset: 0x00011714
	public void PlayIntro()
	{
		if (AdventureDungeonCrawlRewardOption.OptionTypeIsTreasure(this.m_optionData.optionType))
		{
			if (this.m_bigCardActor == null)
			{
				Log.Adventures.PrintError("AdventureDungeonCrawlRewardOption.PlayIntro() - attempting to play intro for TREASURE, but m_bigCardActor is null!", Array.Empty<object>());
				return;
			}
			this.m_bigCardActor.Show();
			this.m_bigCardActor.ActivateSpellBirthState(SpellType.SUMMON_IN_FORGE);
			this.m_bigCardActor.ActivateSpellBirthState(DraftDisplay.GetSpellTypeForRarity(TAG_RARITY.RARE));
			if (!string.IsNullOrEmpty(this.m_treasureCardAppearsSFX))
			{
				SoundManager.Get().LoadAndPlay(this.m_treasureCardAppearsSFX);
				return;
			}
		}
		else if (this.m_optionData.optionType == AdventureDungeonCrawlPlayMat.OptionType.LOOT)
		{
			this.m_lootCrateFSM.SendEvent(this.m_lootCrateDropAnimName);
		}
	}

	// Token: 0x0600030D RID: 781 RVA: 0x000135C3 File Offset: 0x000117C3
	public bool IntroIsPlaying()
	{
		return !AdventureDungeonCrawlRewardOption.OptionTypeIsTreasure(this.m_optionData.optionType) && this.m_optionData.optionType == AdventureDungeonCrawlPlayMat.OptionType.LOOT && this.m_lootCrateFSM.ActiveStateName != this.m_lootCrateAnimDoneStateName;
	}

	// Token: 0x0600030E RID: 782 RVA: 0x00013600 File Offset: 0x00011800
	private void EnableInteraction()
	{
		if (this.m_bigCardActor != null && this.m_bigCardActor.GetCollider() != null)
		{
			this.m_bigCardActor.GetCollider().enabled = true;
		}
		this.m_chooseButton.SetEnabled(true, false);
	}

	// Token: 0x0600030F RID: 783 RVA: 0x0001364C File Offset: 0x0001184C
	public void DisableInteraction()
	{
		if (this.m_bigCardActor != null && this.m_bigCardActor.GetCollider() != null)
		{
			this.m_bigCardActor.GetCollider().enabled = false;
		}
		this.m_chooseButton.SetEnabled(false, false);
	}

	// Token: 0x06000310 RID: 784 RVA: 0x00013698 File Offset: 0x00011898
	public void PlayOutro(bool thisOptionSelected)
	{
		if (AdventureDungeonCrawlRewardOption.OptionTypeIsTreasure(this.m_optionData.optionType))
		{
			if (this.m_bigCardActor == null)
			{
				Log.Adventures.PrintWarning("AdventureDungeonCrawlRewardOption.PlayIntro() - attempting to play outro for TREASURE, but m_bigCardActor is null!", Array.Empty<object>());
				return;
			}
			this.m_outroSpellIsPlaying = true;
			Spell spell = this.m_bigCardActor.GetSpell(DraftDisplay.GetSpellTypeForRarity(TAG_RARITY.RARE));
			if (thisOptionSelected)
			{
				this.m_bigCardActor.GetSpell(SpellType.SUMMON_OUT_FORGE).AddFinishedCallback(new Spell.FinishedCallback(this.OutroSpellFinished), this.m_bigCardActor);
				this.m_bigCardActor.ActivateSpellBirthState(SpellType.SUMMON_OUT_FORGE);
				if (spell != null)
				{
					spell.ActivateState(SpellStateType.DEATH);
				}
				if (!string.IsNullOrEmpty(this.m_treasureCardSelectedSFX))
				{
					SoundManager.Get().LoadAndPlay(this.m_treasureCardSelectedSFX);
					return;
				}
			}
			else
			{
				Spell spell2 = this.m_bigCardActor.GetSpell(SpellType.BURN);
				if (spell2 != null)
				{
					spell2.AddFinishedCallback(new Spell.FinishedCallback(this.OutroSpellFinished), this.m_bigCardActor);
					this.m_bigCardActor.ActivateSpellBirthState(SpellType.BURN);
				}
				else
				{
					this.OutroSpellFinished(null, this.m_bigCardActor);
				}
				if (spell != null)
				{
					spell.ActivateState(SpellStateType.DEATH);
				}
				if (!string.IsNullOrEmpty(this.m_treasureCardDissipateWhenNotSelectedSFX))
				{
					SoundManager.Get().LoadAndPlay(this.m_treasureCardDissipateWhenNotSelectedSFX);
					return;
				}
			}
		}
		else if (this.m_optionData.optionType == AdventureDungeonCrawlPlayMat.OptionType.LOOT)
		{
			this.m_lootCrateFSM.SendEvent(thisOptionSelected ? this.m_lootCrateSummonAnimName : this.m_lootCrateBurnAnimName);
		}
	}

	// Token: 0x06000311 RID: 785 RVA: 0x0001380C File Offset: 0x00011A0C
	private void OutroSpellFinished(Spell spell, object actorObject)
	{
		Actor actor = (Actor)actorObject;
		base.StartCoroutine(this.WaitForAnimToFinishThenDestroy(actor.gameObject));
	}

	// Token: 0x06000312 RID: 786 RVA: 0x00013833 File Offset: 0x00011A33
	private IEnumerator WaitForAnimToFinishThenDestroy(GameObject gameObjectToDestroy)
	{
		yield return new WaitForSeconds(this.m_treasureOutroAnimDelay);
		this.m_outroSpellIsPlaying = false;
		yield return new WaitForSeconds(5f);
		UnityEngine.Object.Destroy(gameObjectToDestroy);
		yield break;
	}

	// Token: 0x06000313 RID: 787 RVA: 0x0001384C File Offset: 0x00011A4C
	public bool OutroIsPlaying()
	{
		if (AdventureDungeonCrawlRewardOption.OptionTypeIsTreasure(this.m_optionData.optionType))
		{
			return this.m_outroSpellIsPlaying;
		}
		return this.m_optionData.optionType == AdventureDungeonCrawlPlayMat.OptionType.LOOT && this.m_lootCrateFSM.ActiveStateName != this.m_lootCrateAnimDoneStateName;
	}

	// Token: 0x06000314 RID: 788 RVA: 0x00013898 File Offset: 0x00011A98
	private void OptionChosen()
	{
		if (this.m_optionChosenCallback != null)
		{
			this.m_optionChosenCallback();
		}
	}

	// Token: 0x06000315 RID: 789 RVA: 0x000138B0 File Offset: 0x00011AB0
	private void SetRewardOptionVisualStyle()
	{
		DungeonRunVisualStyle visualStyle = this.m_dungeonRunData.VisualStyle;
		foreach (AdventureDungeonCrawlRewardOption.AdventureDungeonCrawlRewardOptionStyleOverride adventureDungeonCrawlRewardOptionStyleOverride in this.m_rewardOptionStyle)
		{
			if (visualStyle == adventureDungeonCrawlRewardOptionStyleOverride.VisualStyle)
			{
				if (this.m_particleScript != null)
				{
					this.m_particleScript.m_Target = adventureDungeonCrawlRewardOptionStyleOverride.SlamDustEffect;
					break;
				}
				break;
			}
		}
	}

	// Token: 0x06000316 RID: 790 RVA: 0x00013934 File Offset: 0x00011B34
	private static bool OptionTypeIsTreasure(AdventureDungeonCrawlPlayMat.OptionType optionType)
	{
		return optionType == AdventureDungeonCrawlPlayMat.OptionType.SHRINE_TREASURE || optionType == AdventureDungeonCrawlPlayMat.OptionType.TREASURE;
	}

	// Token: 0x06000317 RID: 791 RVA: 0x00013940 File Offset: 0x00011B40
	private void SetLootCrateContents(AdventureDungeonCrawlRewardOption.OptionData optionData)
	{
		this.m_lootCrate.gameObject.SetActive(true);
		CollectionDeck collectionDeck = new CollectionDeck
		{
			Type = DeckType.CLIENT_ONLY_DECK,
			FormatType = FormatType.FT_WILD,
			ForceDuplicatesIntoSeparateSlots = true
		};
		for (int i = 0; i < optionData.options.Count; i++)
		{
			long num = optionData.options[i];
			if (i == 0)
			{
				if (num == 0L)
				{
					this.m_optionName.Text = "";
				}
				else
				{
					EntityDef entityDef = DefLoader.Get().GetEntityDef((int)num, true);
					this.m_optionName.Text = entityDef.GetName();
				}
			}
			else
			{
				string text = GameUtils.TranslateDbIdToCardId((int)num, false);
				if (string.IsNullOrEmpty(text))
				{
					Log.Adventures.PrintWarning("AdventureDungeonCrawlRewardOption.SetRewardData() - No cardId for dbId {0}!", new object[]
					{
						num
					});
				}
				else
				{
					collectionDeck.AddCard(text, TAG_PREMIUM.NORMAL, false);
				}
			}
		}
		this.m_deckTray.SetDungeonCrawlDeck(collectionDeck, false);
	}

	// Token: 0x04000226 RID: 550
	public UberText m_optionName;

	// Token: 0x04000227 RID: 551
	public GameObject m_lootCrate;

	// Token: 0x04000228 RID: 552
	public AdventureDungeonCrawlDeckTray m_deckTray;

	// Token: 0x04000229 RID: 553
	public UIBButton m_chooseButton;

	// Token: 0x0400022A RID: 554
	public Transform m_bigCardBone;

	// Token: 0x0400022B RID: 555
	public float m_treasureOutroAnimDelay = 0.5f;

	// Token: 0x0400022C RID: 556
	[CustomEditField(Sections = "Animations")]
	public PlayMakerFSM m_lootCrateFSM;

	// Token: 0x0400022D RID: 557
	[CustomEditField(Sections = "Animations")]
	public string m_lootCrateDropAnimName;

	// Token: 0x0400022E RID: 558
	[CustomEditField(Sections = "Animations")]
	public string m_lootCrateSummonAnimName;

	// Token: 0x0400022F RID: 559
	[CustomEditField(Sections = "Animations")]
	public string m_lootCrateBurnAnimName;

	// Token: 0x04000230 RID: 560
	[CustomEditField(Sections = "Animations")]
	public string m_lootCrateAnimDoneStateName;

	// Token: 0x04000231 RID: 561
	[CustomEditField(Sections = "SFX", T = EditType.SOUND_PREFAB)]
	public string m_treasureCardAppearsSFX;

	// Token: 0x04000232 RID: 562
	[CustomEditField(Sections = "SFX", T = EditType.SOUND_PREFAB)]
	public string m_treasureCardSelectedSFX;

	// Token: 0x04000233 RID: 563
	[CustomEditField(Sections = "SFX", T = EditType.SOUND_PREFAB)]
	public string m_treasureCardDissipateWhenNotSelectedSFX;

	// Token: 0x04000234 RID: 564
	[CustomEditField(Sections = "Particles")]
	public PlayNewParticles m_particleScript;

	// Token: 0x04000235 RID: 565
	[CustomEditField(Sections = "Shrines")]
	public GameObject ShrineClassBanner;

	// Token: 0x04000236 RID: 566
	[CustomEditField(Sections = "Shrines")]
	public UberText ShrineClassBannerText;

	// Token: 0x04000237 RID: 567
	[CustomEditField(Sections = "Shrines")]
	public float ShrineClassBannerScalePercent;

	// Token: 0x04000238 RID: 568
	[CustomEditField(Sections = "Shrines")]
	public Vector3 ShrineCardPositionOffset;

	// Token: 0x04000239 RID: 569
	public List<AdventureDungeonCrawlRewardOption.AdventureDungeonCrawlRewardOptionStyleOverride> m_rewardOptionStyle;

	// Token: 0x0400023A RID: 570
	protected IDungeonCrawlData m_dungeonRunData;

	// Token: 0x0400023B RID: 571
	public const float LEFT_MOST_BIG_CARD_X_POS = 0.27f;

	// Token: 0x0400023C RID: 572
	private AdventureDungeonCrawlRewardOption.OptionData m_optionData;

	// Token: 0x0400023D RID: 573
	private Actor m_bigCardActor;

	// Token: 0x0400023E RID: 574
	private const TAG_RARITY TREASURE_CARD_RARITY = TAG_RARITY.RARE;

	// Token: 0x0400023F RID: 575
	private AdventureDungeonCrawlRewardOption.OptionChosenCallback m_optionChosenCallback;

	// Token: 0x04000240 RID: 576
	private bool m_outroSpellIsPlaying;

	// Token: 0x020012E4 RID: 4836
	[Serializable]
	public class AdventureDungeonCrawlRewardOptionStyleOverride
	{
		// Token: 0x0400A50C RID: 42252
		public DungeonRunVisualStyle VisualStyle;

		// Token: 0x0400A50D RID: 42253
		public GameObject SlamDustEffect;
	}

	// Token: 0x020012E5 RID: 4837
	// (Invoke) Token: 0x0600D5C8 RID: 54728
	public delegate void OptionChosenCallback();

	// Token: 0x020012E6 RID: 4838
	public struct OptionData
	{
		// Token: 0x0600D5CB RID: 54731 RVA: 0x003E88C6 File Offset: 0x003E6AC6
		public OptionData(AdventureDungeonCrawlPlayMat.OptionType optionType, List<long> options, int index)
		{
			this.optionType = optionType;
			this.options = new List<long>(options);
			this.index = index;
		}

		// Token: 0x0400A50E RID: 42254
		public readonly AdventureDungeonCrawlPlayMat.OptionType optionType;

		// Token: 0x0400A50F RID: 42255
		public readonly List<long> options;

		// Token: 0x0400A510 RID: 42256
		public readonly int index;
	}

	// Token: 0x020012E7 RID: 4839
	private struct OnFullDefLoadedData
	{
		// Token: 0x0400A511 RID: 42257
		public AdventureDungeonCrawlRewardOption.OptionData optionData;

		// Token: 0x0400A512 RID: 42258
		public DefLoader.DisposableFullDef fullDef;
	}
}
