using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.DungeonCrawl;
using Hearthstone.Progression;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x0200003C RID: 60
[CustomEditClass]
public class AdventureDungeonCrawlPlayMat : MonoBehaviour
{
	// Token: 0x17000036 RID: 54
	// (get) Token: 0x060002A5 RID: 677 RVA: 0x00010DCB File Offset: 0x0000EFCB
	// (set) Token: 0x060002A6 RID: 678 RVA: 0x00010DD3 File Offset: 0x0000EFD3
	public bool IsNextMissionASpecialEncounter { get; set; }

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x060002A7 RID: 679 RVA: 0x00010DDC File Offset: 0x0000EFDC
	public PlayButton PlayButton
	{
		get
		{
			return this.m_playButton;
		}
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x00010DE4 File Offset: 0x0000EFE4
	private void Awake()
	{
		this.m_treasureSatchelReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnTreasureSatchelReady));
		this.m_treasureInspectReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnTreasureInspectReady));
		this.m_duelsPlayMatReference.RegisterReadyListener<DuelsPlayMat>(new Action<DuelsPlayMat>(this.OnPVPDRPlayMatReady));
		if (this.m_treasureSatchelWidget != null)
		{
			this.m_treasureSatchelWidget.gameObject.SetActive(false);
		}
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x00010E58 File Offset: 0x0000F058
	private void Start()
	{
		this.m_rewardOptions = new List<AdventureDungeonCrawlRewardOption>(this.m_rewardOptionNestedPrefabs.Count);
		for (int i = 0; i < this.m_rewardOptionNestedPrefabs.Count; i++)
		{
			NestedPrefabBase nestedPrefabBase = this.m_rewardOptionNestedPrefabs[i];
			if (nestedPrefabBase == null)
			{
				Debug.LogWarningFormat("AdventureDungeonCrawlPlayMat.Start - m_rewardOptionNestedPrefabs have null values. Skipping index {0}...", new object[]
				{
					i
				});
			}
			else
			{
				AdventureDungeonCrawlRewardOption component = nestedPrefabBase.PrefabGameObject(true).GetComponent<AdventureDungeonCrawlRewardOption>();
				if (i == 0)
				{
					TransformUtil.SetLocalPosX(component.m_deckTray.m_deckBigCard, 0.27f);
					component.m_deckTray.m_deckBigCard.m_flipHeroPowerHorizontalPosition = true;
				}
				else if (i == 1)
				{
					component.m_deckTray.m_deckBigCard.m_flipHeroPowerHorizontalPosition = true;
				}
				component.m_deckTray.m_deckBigCard.m_disableCollidersOnHeroPower = true;
				component.m_deckTray.m_deckBigCard.m_showTooltipsForAdventure = true;
				this.m_rewardOptions.Add(component);
			}
		}
		this.m_PlayButtonReference.RegisterReadyListener<PlayButton>(new Action<PlayButton>(this.OnPlayButtonReady));
		this.SetUpPlayButton();
		this.m_startCallFinished = true;
	}

	// Token: 0x060002AA RID: 682 RVA: 0x00010F67 File Offset: 0x0000F167
	private void OnDestroy()
	{
		DefLoader.DisposableCardDef bossCardDef = this.m_bossCardDef;
		if (bossCardDef != null)
		{
			bossCardDef.Dispose();
		}
		this.m_bossCardDef = null;
	}

	// Token: 0x060002AB RID: 683 RVA: 0x00010F84 File Offset: 0x0000F184
	public void Initialize(IDungeonCrawlData data)
	{
		this.m_dungeonCrawlData = data;
		this.SetPlaymatVisualStyle();
		foreach (AdventureDungeonCrawlRewardOption adventureDungeonCrawlRewardOption in this.m_rewardOptions)
		{
			adventureDungeonCrawlRewardOption.Initalize(this.m_dungeonCrawlData);
		}
		this.m_paperControllerReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnPaperControllerReady));
		this.m_paperControllerReference_phone.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnPaperControllerReady));
	}

	// Token: 0x060002AC RID: 684 RVA: 0x00011018 File Offset: 0x0000F218
	private void Update()
	{
		if (this.m_playMatState == AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE)
		{
			bool flag = true;
			if (this.m_currentOptionType == AdventureDungeonCrawlPlayMat.OptionType.INVALID)
			{
				return;
			}
			IEnumerable<AdventureOptionWidget> enumerable = null;
			if (this.m_currentOptionType == AdventureDungeonCrawlPlayMat.OptionType.HERO_POWER)
			{
				enumerable = this.m_heroPowerOptions.Cast<AdventureOptionWidget>();
			}
			else if (this.m_currentOptionType == AdventureDungeonCrawlPlayMat.OptionType.DECK)
			{
				enumerable = this.m_deckOptions.Cast<AdventureOptionWidget>();
			}
			else if (this.m_currentOptionType == AdventureDungeonCrawlPlayMat.OptionType.TREASURE_SATCHEL)
			{
				enumerable = this.m_treasureSatchelOptions.Cast<AdventureOptionWidget>();
				if (this.m_treasureSatchelWidget == null || !this.m_treasureSatchelWidget.IsReady || this.m_treasureSatchelWidget.HasPendingActions)
				{
					flag = false;
				}
			}
			if (enumerable != null)
			{
				using (IEnumerator<AdventureOptionWidget> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AdventureOptionWidget adventureOptionWidget = enumerator.Current;
						if (adventureOptionWidget.IsOutroPlaying || !adventureOptionWidget.IsReady)
						{
							flag = false;
							break;
						}
					}
					goto IL_F3;
				}
			}
			for (int i = 0; i < this.m_rewardOptions.Count; i++)
			{
				if (this.m_rewardOptions[i].OutroIsPlaying())
				{
					flag = false;
					break;
				}
			}
			IL_F3:
			if (flag)
			{
				this.SetPlayMatState(AdventureDungeonCrawlPlayMat.PlayMatState.READY_FOR_DATA, true);
			}
		}
	}

	// Token: 0x060002AD RID: 685 RVA: 0x00011134 File Offset: 0x0000F334
	public bool IsReady()
	{
		if (!this.m_startCallFinished)
		{
			return false;
		}
		if (this.m_bossActor == null)
		{
			return false;
		}
		if (this.m_playButton == null)
		{
			Log.Adventures.PrintWarning("PlayButton not ready yet!", Array.Empty<object>());
			return false;
		}
		return true;
	}

	// Token: 0x060002AE RID: 686 RVA: 0x00011180 File Offset: 0x0000F380
	public void SetTreasureSatchelOptionSelectedCallback(AdventureDungeonCrawlTreasureOption.TreasureSelectedOptionCallback callback)
	{
		if (this.m_treasureSatchelReference == null)
		{
			Debug.LogError("AdventureDungeonCrawlPlayMat.SetTreasureSatchelOptionSelectedCallback - m_treasureSatchelReference was null!");
			return;
		}
		this.m_treasureSatchelReference.RegisterReadyListener<Widget>(delegate(Widget widget)
		{
			if (this.m_treasureSatchelOptions != null)
			{
				foreach (AdventureDungeonCrawlTreasureOption adventureDungeonCrawlTreasureOption in this.m_treasureSatchelOptions)
				{
					if (adventureDungeonCrawlTreasureOption != null)
					{
						adventureDungeonCrawlTreasureOption.SetOptionCallbacks(callback, null, null);
					}
				}
			}
		});
	}

	// Token: 0x060002AF RID: 687 RVA: 0x000111CC File Offset: 0x0000F3CC
	public void SetDeckOptionSelectedCallback(AdventureDungeonCrawlDeckOption.DeckOptionSelectedCallback callback)
	{
		foreach (AdventureDungeonCrawlDeckOption adventureDungeonCrawlDeckOption in this.m_deckOptions)
		{
			adventureDungeonCrawlDeckOption.SetOptionCallbacks(callback, null, null);
		}
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x00011220 File Offset: 0x0000F420
	public void SetHeroPowerOptionCallback(AdventureDungeonCrawlHeroPowerOption.HeroPowerSelectedOptionCallback selectedCallback, AdventureDungeonCrawlHeroPowerOption.HeroPowerHoverOptionCallback rolloverCallback, AdventureDungeonCrawlHeroPowerOption.HeroPowerHoverOptionCallback rolloutCallback)
	{
		foreach (AdventureDungeonCrawlHeroPowerOption adventureDungeonCrawlHeroPowerOption in this.m_heroPowerOptions)
		{
			adventureDungeonCrawlHeroPowerOption.SetOptionCallbacks(selectedCallback, rolloverCallback, rolloutCallback);
		}
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x00011274 File Offset: 0x0000F474
	public void SetRewardOptionSelectedCallback(AdventureDungeonCrawlPlayMat.RewardOptionSelectedCallback callback)
	{
		using (List<AdventureDungeonCrawlRewardOption>.Enumerator enumerator = this.m_rewardOptions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				AdventureDungeonCrawlRewardOption rewardOption = enumerator.Current;
				rewardOption.SetOptionChosenCallback(delegate
				{
					callback(rewardOption.GetOptionData());
				});
			}
		}
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x000112F8 File Offset: 0x0000F4F8
	public void DeselectAllDeckOptionsWithoutId(int deckId)
	{
		foreach (AdventureDungeonCrawlDeckOption adventureDungeonCrawlDeckOption in this.m_deckOptions)
		{
			if (adventureDungeonCrawlDeckOption.DeckId != (long)deckId)
			{
				adventureDungeonCrawlDeckOption.Deselect();
			}
		}
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x00011354 File Offset: 0x0000F554
	public void SetBossActor(Actor bossActor)
	{
		this.m_bossActor = bossActor;
		if (this.m_bossActor != null && this.m_bossCardDef != null && this.m_bossEntityDef != null)
		{
			this.SetUpBossCard();
		}
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x00011384 File Offset: 0x0000F584
	public void SetBossFullDef(DefLoader.DisposableCardDef cardDef, EntityDef entityDef)
	{
		DefLoader.DisposableCardDef bossCardDef = this.m_bossCardDef;
		if (bossCardDef != null)
		{
			bossCardDef.Dispose();
		}
		this.m_bossCardDef = cardDef;
		this.m_bossEntityDef = entityDef;
		if (this.m_bossActor != null && this.m_bossCardDef != null && this.m_bossEntityDef != null)
		{
			this.SetUpBossCard();
		}
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x000113D4 File Offset: 0x0000F5D4
	private void SetUpBossCard()
	{
		if (this.m_bossActor == null)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlDisplay.SetUpBossCard - m_BossActor is null!", Array.Empty<object>());
			return;
		}
		if (this.m_bossCardDef == null || this.m_bossEntityDef == null)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlDisplay.SetUpBossCard - m_bossFullDef is null!", Array.Empty<object>());
			return;
		}
		this.m_bossActor.SetCardDef(this.m_bossCardDef);
		this.m_bossActor.SetEntityDef(this.m_bossEntityDef);
		this.m_bossActor.SetPremium(TAG_PREMIUM.NORMAL);
		PegUIElement component = this.m_bossActor.GetCollider().gameObject.GetComponent<PegUIElement>();
		if (component)
		{
			component.AddEventListener(UIEventType.ROLLOVER, delegate(UIEvent e)
			{
				SoundManager.Get().LoadAndPlay(this.m_bossMouseOverSFXDefault);
			});
		}
		else
		{
			Debug.LogError("Could not find PegUIElement for Boss");
		}
		this.m_bossActor.SetCardbackUpdateIgnore(true);
		if (this.m_cardBack != null)
		{
			this.m_bossActor.m_cardMesh.GetComponent<Renderer>().GetMaterial(this.m_bossActor.m_cardBackMatIdx).mainTexture = this.m_cardBack.m_CardBackTexture;
		}
		this.m_bossActor.Show();
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x000114EC File Offset: 0x0000F6EC
	public void SetCardBack(int cardBackId)
	{
		this.m_loadingCardback = true;
		if (!CardBackManager.Get().LoadCardBackByIndex(cardBackId, new CardBackManager.LoadCardBackData.LoadCardBackCallback(this.OnCardBackLoaded), null))
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlPlayMat.SetCardBack() - failed to load CardBack {0}", new object[]
			{
				cardBackId
			});
			this.m_loadingCardback = false;
			return;
		}
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x00011540 File Offset: 0x0000F740
	public void SetPlayerHeroDbId(int heroDbId)
	{
		this.m_playerHeroDbId = heroDbId;
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x0001154C File Offset: 0x0000F74C
	private void OnCardBackLoaded(CardBackManager.LoadCardBackData cardbackData)
	{
		this.m_loadingCardback = false;
		this.m_cardBack = cardbackData.m_CardBack;
		if (this.m_bossActor != null)
		{
			this.m_bossActor.m_cardMesh.GetComponent<Renderer>().GetMaterial(this.m_bossActor.m_cardBackMatIdx).mainTexture = this.m_cardBack.m_CardBackTexture;
		}
		if (this.m_cardBackBones.Count < 1)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlPlayMat.OnCardBackLoaded() - Can't attach the cardbacks to a bone, as m_cardBackBones are not defined!", Array.Empty<object>());
			return;
		}
		this.m_nextBossCardBack = cardbackData.m_GameObject;
		Actor component = this.m_nextBossCardBack.GetComponent<Actor>();
		if (component != null)
		{
			component.SetCardbackUpdateIgnore(true);
		}
		GameUtils.SetParent(this.m_nextBossCardBack, this.m_nextBossBackBone, true);
		this.m_cardBacks.Clear();
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x00011614 File Offset: 0x0000F814
	public void SetUpDefeatedBosses(List<long> defeatedBossIds, int bossesPerRun)
	{
		if (this.m_setUpDefeatedBossesCompleted)
		{
			Debug.LogError("Calling SetUpDefeatedBosses, when this has already been called! Please investigate - you should not be doing this!");
			return;
		}
		this.m_numBossesDefeated = ((defeatedBossIds == null) ? 0 : defeatedBossIds.Count);
		this.m_bossesPerRun = bossesPerRun;
		int num = Mathf.Min(this.m_bossCardBones.Count - 1, this.m_numBossesDefeated);
		if (this.m_numBossesDefeated >= bossesPerRun)
		{
			Log.Adventures.PrintWarning("AdventureDungeonCrawlPlayMat.SetUpDefeatedBosses() - Your run is done!  Why are you trying to set up defeated bosses?", Array.Empty<object>());
			return;
		}
		if (this.m_defeatedBossActors.Count < this.m_numBossesDefeated)
		{
			if (this.m_bossActor == null)
			{
				Log.Adventures.PrintError("AdventureDungeonCrawlDisplay attempting to clone from m_BossActor, but it is null!", Array.Empty<object>());
			}
			else
			{
				while (this.m_defeatedBossActors.Count < num)
				{
					Actor component = UnityEngine.Object.Instantiate<GameObject>(this.m_bossActor.gameObject).GetComponent<Actor>();
					GameUtils.SetParent(component, this.m_bossCardBones[this.m_defeatedBossActors.Count], true);
					component.GetHealthObject().Hide();
					this.m_defeatedBossActors.Add(component);
				}
			}
		}
		if (num > 0 && this.m_defeatedBossActors.Count >= num)
		{
			int num2 = (int)defeatedBossIds[defeatedBossIds.Count - 1];
			string text = GameUtils.TranslateDbIdToCardId(num2, false);
			if (text == null)
			{
				Log.Adventures.PrintWarning("AdventureDungeonCrawlPlayMat.SetUpDefeatedBosses() - No cardId for last defeated boss dbId {0}!", new object[]
				{
					num2
				});
			}
			else
			{
				this.m_topDefeatedBoss = this.m_defeatedBossActors[this.m_defeatedBossActors.Count - 1];
				using (DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(text, null))
				{
					this.m_topDefeatedBoss.SetEntityDef(fullDef.EntityDef);
					this.m_topDefeatedBoss.SetCardDef(fullDef.DisposableCardDef);
					this.m_topDefeatedBoss.SetPremium(TAG_PREMIUM.NORMAL);
					this.m_topDefeatedBoss.UpdateAllComponents();
				}
			}
		}
		TransformUtil.AttachAndPreserveLocalTransform(this.m_nextBossBone, this.m_bossCardBones[Mathf.Min(num, this.m_bossCardBones.Count - 1)]);
		if (num == 0)
		{
			this.m_allCards.SetActive(false);
		}
		this.m_setUpDefeatedBossesCompleted = true;
	}

	// Token: 0x060002BA RID: 698 RVA: 0x0001182C File Offset: 0x0000FA2C
	public void SetUpCardBacks(int numUndefeatedBosses, AdventureDungeonCrawlPlayMat.AssetLoadCompletedCallback callback)
	{
		base.StartCoroutine(this.SetUpCardBacksWhenReady(numUndefeatedBosses, callback));
	}

	// Token: 0x060002BB RID: 699 RVA: 0x0001183D File Offset: 0x0000FA3D
	private IEnumerator SetUpCardBacksWhenReady(int numUndefeatedBosses, AdventureDungeonCrawlPlayMat.AssetLoadCompletedCallback callback)
	{
		while (this.m_loadingCardback)
		{
			yield return null;
		}
		if (this.m_nextBossCardBack == null)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlPlayMat.SetUpCardBacksWhenReady() - done loading cardback, but it must have failed!  Can't make more cardbacks!", Array.Empty<object>());
			if (callback != null)
			{
				callback();
			}
			yield break;
		}
		int num = Mathf.Min(numUndefeatedBosses, this.m_cardBackBones.Count);
		if (num == 0)
		{
			this.m_facedownCards.SetActive(false);
		}
		else
		{
			while (this.m_cardBacks.Count < num)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_nextBossCardBack);
				Actor component = gameObject.GetComponent<Actor>();
				if (component != null)
				{
					component.SetCardbackUpdateIgnore(true);
				}
				GameUtils.SetParent(gameObject, this.m_cardBackBones[this.m_cardBacks.Count], true);
				this.m_cardBacks.Add(gameObject);
			}
		}
		if (callback != null)
		{
			callback();
		}
		yield break;
	}

	// Token: 0x060002BC RID: 700 RVA: 0x0001185A File Offset: 0x0000FA5A
	private void SetUpPlayButton()
	{
		if (UniversalInputManager.UsePhoneUI && this.m_MobilePlayButtonSlidingTrayBone != null)
		{
			GameUtils.SetParent(this.m_PlayButtonRoot, this.m_MobilePlayButtonSlidingTrayBone, false);
			this.m_PlayButtonPlate.SetActive(true);
		}
	}

	// Token: 0x060002BD RID: 701 RVA: 0x00011894 File Offset: 0x0000FA94
	private void EnablePlayButton(bool enabled)
	{
		if (enabled)
		{
			this.m_playButton.Enable();
		}
		else
		{
			this.m_playButton.Disable(false);
		}
		if (UniversalInputManager.UsePhoneUI && this.m_MobilePlayButtonSlidingTrayBone != null)
		{
			this.m_MobilePlayButtonSlidingTrayBone.ToggleTraySlider(enabled, null, this.m_allowPlayButtonAnimation);
		}
	}

	// Token: 0x060002BE RID: 702 RVA: 0x000118EC File Offset: 0x0000FAEC
	public void ShowTreasureOptions(List<long> treasureOptions)
	{
		if (treasureOptions == null || treasureOptions.Count == 0)
		{
			Log.Adventures.PrintWarning("AdventureDungeonCrawlPlayMat - Attempting to show Treasure, but no treasure was passed in!", Array.Empty<object>());
			return;
		}
		this.m_currentOptionType = AdventureDungeonCrawlPlayMat.OptionType.TREASURE;
		this.SetPlayMatState(AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_OPTIONS, false);
		for (int i = 0; i < treasureOptions.Count; i++)
		{
			if (i < this.m_rewardOptions.Count && this.m_rewardOptions[i] != null)
			{
				this.m_rewardOptions[i].SetRewardData(new AdventureDungeonCrawlRewardOption.OptionData(AdventureDungeonCrawlPlayMat.OptionType.TREASURE, new List<long>
				{
					treasureOptions[i]
				}, i));
			}
		}
		this.SetPlayMatStateAsInitializedAndPlayTransition();
	}

	// Token: 0x060002BF RID: 703 RVA: 0x0001198C File Offset: 0x0000FB8C
	public void ShowLootOptions(List<long> classLootOptionsA, List<long> classLootOptionsB, List<long> classLootOptionsC)
	{
		if ((classLootOptionsA == null || classLootOptionsA.Count == 0) && (classLootOptionsB == null || classLootOptionsB.Count == 0) && (classLootOptionsC == null || classLootOptionsC.Count == 0))
		{
			Log.Adventures.PrintWarning("AdventureDungeonCrawlPlayMat - Attempting to show Loot, but no loot was passed in!", Array.Empty<object>());
			return;
		}
		List<List<long>> list = new List<List<long>>
		{
			classLootOptionsA,
			classLootOptionsB,
			classLootOptionsC
		};
		this.m_currentOptionType = AdventureDungeonCrawlPlayMat.OptionType.LOOT;
		this.SetPlayMatState(AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_OPTIONS, false);
		for (int i = 0; i < this.m_rewardOptions.Count; i++)
		{
			AdventureDungeonCrawlRewardOption adventureDungeonCrawlRewardOption = this.m_rewardOptions[i];
			if (i >= list.Count)
			{
				break;
			}
			adventureDungeonCrawlRewardOption.SetRewardData(new AdventureDungeonCrawlRewardOption.OptionData(AdventureDungeonCrawlPlayMat.OptionType.LOOT, list[i], i));
		}
		this.SetPlayMatStateAsInitializedAndPlayTransition();
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x00011A40 File Offset: 0x0000FC40
	public void ShowShrineOptions(List<long> shrineOptions)
	{
		this.m_currentOptionType = AdventureDungeonCrawlPlayMat.OptionType.SHRINE_TREASURE;
		this.SetPlayMatState(AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_OPTIONS, false);
		if (shrineOptions == null || shrineOptions.Count == 0)
		{
			Debug.LogError("ShowShrineOptions - No shrines provided.");
			return;
		}
		int num = 0;
		while (num < this.m_rewardOptions.Count && shrineOptions.Count > num)
		{
			this.m_rewardOptions[num].SetRewardData(new AdventureDungeonCrawlRewardOption.OptionData(AdventureDungeonCrawlPlayMat.OptionType.SHRINE_TREASURE, new List<long>
			{
				shrineOptions[num]
			}, num));
			num++;
		}
		this.SetPlayMatStateAsInitializedAndPlayTransition();
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x00011AC4 File Offset: 0x0000FCC4
	public void ShowTreasureSatchel(List<AdventureLoadoutTreasuresDbfRecord> adventureLoadoutTreasures, GameSaveKeyId adventureGameSaveServerKey, GameSaveKeyId adventureGameSaveClientKey)
	{
		if (this.m_treasureSatchelWidget == null)
		{
			Debug.LogError("AdventureDungeonCrawlPlayMat.ShowTreasureSatchel - m_treasureSatchel is null!");
			return;
		}
		this.m_currentOptionType = AdventureDungeonCrawlPlayMat.OptionType.TREASURE_SATCHEL;
		this.SetPlayMatState(AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_OPTIONS, false);
		this.m_treasureSatchelWidget.gameObject.SetActive(true);
		this.m_treasureSatchelWidget.Hide();
		base.StartCoroutine(this.ShowTreasureSatchelWhenReady(adventureLoadoutTreasures, adventureGameSaveServerKey, adventureGameSaveClientKey));
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x00011B25 File Offset: 0x0000FD25
	private IEnumerator ShowTreasureSatchelWhenReady(List<AdventureLoadoutTreasuresDbfRecord> adventureLoadoutTreasures, GameSaveKeyId adventureGameSaveServerKey, GameSaveKeyId adventureGameSaveClientKey)
	{
		AdventureDungeonCrawlPlayMat.<>c__DisplayClass128_0 CS$<>8__locals1 = new AdventureDungeonCrawlPlayMat.<>c__DisplayClass128_0();
		CS$<>8__locals1.adventureGameSaveClientKey = adventureGameSaveClientKey;
		while (!this.m_subsceneTransitionComplete || !this.m_treasureSatchelWidget.IsReady || this.m_treasureSatchelWidget.IsChangingStates)
		{
			yield return null;
		}
		this.m_treasureSatchelWidget.TriggerEvent("CODE_TREASURE_SATCHEL_SHOW", default(Widget.TriggerEventParameters));
		while (this.m_treasureSatchelWidget.IsChangingStates)
		{
			yield return null;
		}
		this.m_treasureSatchelWidget.Show();
		if (adventureLoadoutTreasures.Count > this.m_treasureSatchelOptions.Count)
		{
			Log.Adventures.PrintWarning("AdventureDungeonCrawlPlayMat.ShowTreasureSatchelWhenReady - there are more Adventure Loadout Treasures than option visuals to show them! Number of Loadout Treasures: {0} Number of PlayMat options: {1}", new object[]
			{
				adventureLoadoutTreasures.Count,
				this.m_treasureSatchelOptions.Count
			});
		}
		IDataModel dataModel;
		this.m_treasureSatchelWidget.GetDataModel(32, out dataModel);
		AdventureTreasureSatchelDataModel satchelDataModel = dataModel as AdventureTreasureSatchelDataModel;
		if (satchelDataModel == null)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlPlayMat.ShowTreasureSatchelWhenReady - satchel has no data model!", Array.Empty<object>());
			yield break;
		}
		satchelDataModel.Cards.Clear();
		List<long> treasureRunWins = this.TreasureWinsForScenario(adventureGameSaveServerKey, (int)this.m_dungeonCrawlData.GetMission());
		List<long> newlyUnlockedTreasures;
		GameSaveDataManager.Get().GetSubkeyValue(CS$<>8__locals1.adventureGameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEWLY_UNLOCKED_LOADOUT_TREASURES, out newlyUnlockedTreasures);
		int num5;
		for (int i = 0; i < adventureLoadoutTreasures.Count; i = num5)
		{
			AdventureDungeonCrawlPlayMat.<>c__DisplayClass128_1 CS$<>8__locals2 = new AdventureDungeonCrawlPlayMat.<>c__DisplayClass128_1();
			CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
			if (i > this.m_treasureSatchelOptions.Count - 1)
			{
				Log.Adventures.PrintWarning("AdventureDungeonCrawlPlayMat.ShowTreasureSatchelWhenReady - there are not enough Adventure Loadout Treasures to fill the PlayMat options!  Number of CardDataModels: {0} Number of PlayMat options: {1}", new object[]
				{
					adventureLoadoutTreasures.Count,
					this.m_treasureSatchelOptions.Count
				});
				break;
			}
			bool locked = false;
			bool upgraded = false;
			string lockedText = string.Empty;
			string text = adventureLoadoutTreasures[i].UnlockCriteriaText;
			CS$<>8__locals2.cardDbId = adventureLoadoutTreasures[i].CardId;
			bool flag = adventureLoadoutTreasures[i].UnlockAchievement > 0;
			if (adventureLoadoutTreasures[i].UnlockValue > 0 || flag)
			{
				long num;
				bool flag2;
				locked = !this.m_dungeonCrawlData.AdventureTreasureIsUnlocked(adventureGameSaveServerKey, adventureLoadoutTreasures[i], out num, out flag2);
				if (locked && !string.IsNullOrEmpty(text))
				{
					int num2 = 0;
					if (adventureLoadoutTreasures[i].UnlockAchievement > 0)
					{
						num2 = AchievementManager.Get().GetAchievementDataModel(adventureLoadoutTreasures[i].UnlockAchievement).Quota;
					}
					int num3 = adventureLoadoutTreasures[i].UnlockValue + num2;
					lockedText = string.Format(text, num, num3);
				}
			}
			if (adventureLoadoutTreasures[i].UpgradeValue > 0)
			{
				long num4;
				upgraded = this.m_dungeonCrawlData.AdventureTreasureIsUpgraded(adventureGameSaveServerKey, adventureLoadoutTreasures[i], out num4);
				if (upgraded)
				{
					CS$<>8__locals2.cardDbId = adventureLoadoutTreasures[i].UpgradedCardId;
				}
			}
			bool completed = treasureRunWins != null && treasureRunWins.Contains((long)CS$<>8__locals2.cardDbId);
			bool newlyUnlocked = newlyUnlockedTreasures != null && newlyUnlockedTreasures.Contains((long)CS$<>8__locals2.cardDbId);
			CS$<>8__locals2.treasureSatchelOption = this.m_treasureSatchelOptions[i];
			CardDataModel cardDataModel = new CardDataModel();
			satchelDataModel.Cards.Add(cardDataModel);
			if (CS$<>8__locals2.cardDbId != 0 && cardDataModel != null)
			{
				string cardId = GameUtils.TranslateDbIdToCardId(CS$<>8__locals2.cardDbId, false);
				CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(cardId);
				cardDataModel.CardId = cardId;
				cardDataModel.FlavorText = ((cardRecord != null) ? cardRecord.FlavorText : null);
			}
			while (!CS$<>8__locals2.treasureSatchelOption.IsReady)
			{
				yield return null;
			}
			CS$<>8__locals2.treasureSatchelOption.Init((long)CS$<>8__locals2.cardDbId, locked, lockedText, upgraded, completed, newlyUnlocked, delegate
			{
				if (!CS$<>8__locals2.treasureSatchelOption.IsNewlyUnlocked)
				{
					return;
				}
				GameSaveDataManager.SubkeySaveRequest subkeySaveRequest = GameSaveDataManager.Get().GenerateSaveRequestToRemoveValueFromSubkeyIfItExists(CS$<>8__locals2.CS$<>8__locals1.adventureGameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEWLY_UNLOCKED_LOADOUT_TREASURES, (long)CS$<>8__locals2.cardDbId);
				if (subkeySaveRequest != null)
				{
					Log.Adventures.Print("Treasure Card {0} was Newly Unlocked but the player just acknowledged it, so saving that it is no longer Newly Unlocked.", new object[]
					{
						CS$<>8__locals2.cardDbId
					});
					GameSaveDataManager.Get().SaveSubkey(subkeySaveRequest, null);
					CS$<>8__locals2.treasureSatchelOption.IsNewlyUnlocked = false;
				}
			});
			CS$<>8__locals2 = null;
			lockedText = null;
			num5 = i + 1;
		}
		foreach (AdventureDungeonCrawlTreasureOption treasureSatchelOption in this.m_treasureSatchelOptions)
		{
			while (!treasureSatchelOption.IsReady)
			{
				yield return null;
			}
			treasureSatchelOption = null;
		}
		List<AdventureDungeonCrawlTreasureOption>.Enumerator enumerator = default(List<AdventureDungeonCrawlTreasureOption>.Enumerator);
		this.SetPlayMatStateAsInitializedAndPlayTransition();
		yield break;
		yield break;
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x00011B4C File Offset: 0x0000FD4C
	public void ShowHeroPowers(List<AdventureHeroPowerDbfRecord> adventureHeroPowers, GameSaveKeyId adventureGameSaveServerKey, GameSaveKeyId adventureGameSaveClientKey)
	{
		this.m_currentOptionType = AdventureDungeonCrawlPlayMat.OptionType.HERO_POWER;
		this.SetPlayMatState(AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_OPTIONS, false);
		if (adventureHeroPowers.Count > this.m_heroPowerOptions.Count)
		{
			Log.Adventures.PrintWarning("There are more Adventure Hero Powers than option visuals to shown them! Number of Hero Powers: {0} Number of PlayMat options: {1}", new object[]
			{
				adventureHeroPowers.Count,
				this.m_heroPowerOptions.Count
			});
		}
		List<long> list = this.HeroPowerWinsForScenario(adventureGameSaveServerKey, (int)this.m_dungeonCrawlData.GetMission());
		List<long> list2;
		GameSaveDataManager.Get().GetSubkeyValue(adventureGameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEWLY_UNLOCKED_HERO_POWERS, out list2);
		for (int i = 0; i < this.m_heroPowerOptions.Count; i++)
		{
			if (i > adventureHeroPowers.Count - 1)
			{
				Log.Adventures.PrintWarning("There are not enough Adventure Hero Powers to fill the PlayMat options!  Number of Hero Powers: {0} Number of PlayMat options: {1}", new object[]
				{
					adventureHeroPowers.Count,
					this.m_heroPowerOptions.Count
				});
				break;
			}
			bool flag = false;
			string lockedText = string.Empty;
			string text = adventureHeroPowers[i].UnlockCriteriaText;
			int heroPowerDbId = adventureHeroPowers[i].CardId;
			bool flag2 = adventureHeroPowers[i].UnlockAchievement > 0;
			if (adventureHeroPowers[i].UnlockValue > 0 || flag2)
			{
				long num;
				bool flag3;
				flag = !this.m_dungeonCrawlData.AdventureHeroPowerIsUnlocked(adventureGameSaveServerKey, adventureHeroPowers[i], out num, out flag3);
				if (flag && !string.IsNullOrEmpty(text))
				{
					int num2 = 0;
					if (adventureHeroPowers[i].UnlockAchievement > 0)
					{
						num2 = AchievementManager.Get().GetAchievementDataModel(adventureHeroPowers[i].UnlockAchievement).Quota;
					}
					int num3 = adventureHeroPowers[i].UnlockValue + num2;
					lockedText = string.Format(text, num, num3);
				}
			}
			bool completed = list != null && list.Contains((long)heroPowerDbId);
			bool newlyUnlocked = list2 != null && list2.Contains((long)heroPowerDbId);
			AdventureDungeonCrawlHeroPowerOption heroPowerOption = this.m_heroPowerOptions[i];
			heroPowerOption.Init((long)heroPowerDbId, flag, lockedText, completed, newlyUnlocked, delegate
			{
				if (!heroPowerOption.IsNewlyUnlocked)
				{
					return;
				}
				GameSaveDataManager.SubkeySaveRequest subkeySaveRequest = GameSaveDataManager.Get().GenerateSaveRequestToRemoveValueFromSubkeyIfItExists(adventureGameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEWLY_UNLOCKED_HERO_POWERS, (long)heroPowerDbId);
				if (subkeySaveRequest != null)
				{
					Log.Adventures.Print("Hero Power {0} was Newly Unlocked but the player just acknowledged it, so saving that it is no longer Newly Unlocked.", new object[]
					{
						heroPowerDbId
					});
					GameSaveDataManager.Get().SaveSubkey(subkeySaveRequest, null);
					heroPowerOption.IsNewlyUnlocked = false;
				}
			});
		}
		this.SetPlayMatStateAsInitializedAndPlayTransition();
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x00011DA0 File Offset: 0x0000FFA0
	public void ShowDecks(List<AdventureDeckDbfRecord> adventureDecks, GameSaveKeyId adventureGameSaveServerKey, GameSaveKeyId adventureGameSaveClientKey)
	{
		this.m_currentOptionType = AdventureDungeonCrawlPlayMat.OptionType.DECK;
		this.SetPlayMatState(AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_OPTIONS, false);
		if (adventureDecks.Count > this.m_deckOptions.Count)
		{
			Log.Adventures.PrintWarning("There are more Adventure Decks than option visuals to shown them! Number of Decks: {0} Number of PlayMat options: {1}", new object[]
			{
				adventureDecks.Count,
				this.m_deckOptions.Count
			});
		}
		List<long> list = this.DeckWinsForScenario(adventureGameSaveServerKey, (int)this.m_dungeonCrawlData.GetMission());
		List<long> list2;
		GameSaveDataManager.Get().GetSubkeyValue(adventureGameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEWLY_UNLOCKED_DECKS, out list2);
		for (int i = 0; i < this.m_deckOptions.Count; i++)
		{
			if (i > adventureDecks.Count - 1)
			{
				Log.Adventures.PrintWarning("There are not enough Adventure Decks to fill the PlayMat options!  Number of Decks: {0} Number of PlayMat options: {1}", new object[]
				{
					adventureDecks.Count,
					this.m_deckOptions.Count
				});
				break;
			}
			bool flag = false;
			string lockedText = string.Empty;
			string text = adventureDecks[i].UnlockCriteriaText;
			if (adventureDecks[i].UnlockValue > 0)
			{
				long num;
				bool flag2;
				flag = !this.m_dungeonCrawlData.AdventureDeckIsUnlocked(adventureGameSaveServerKey, adventureDecks[i], out num, out flag2);
				if (flag && !string.IsNullOrEmpty(text))
				{
					lockedText = string.Format(text, num, adventureDecks[i].UnlockValue);
				}
			}
			AdventureDeckDbfRecord deckRecord = adventureDecks[i];
			bool completed = list != null && list.Contains((long)deckRecord.DeckId);
			bool newlyUnlocked = list2 != null && list2.Contains((long)deckRecord.DeckId);
			AdventureDungeonCrawlDeckOption deckOption = this.m_deckOptions[i];
			deckOption.Init(adventureDecks[i], flag, lockedText, completed, newlyUnlocked, delegate
			{
				if (!deckOption.IsNewlyUnlocked)
				{
					return;
				}
				GameSaveDataManager.SubkeySaveRequest subkeySaveRequest = GameSaveDataManager.Get().GenerateSaveRequestToRemoveValueFromSubkeyIfItExists(adventureGameSaveClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEWLY_UNLOCKED_DECKS, (long)deckRecord.DeckId);
				if (subkeySaveRequest != null)
				{
					Log.Adventures.Print("Deck {0} was Newly Unlocked but the player just acknowledged it, so saving that it is no longer Newly Unlocked.", new object[]
					{
						deckRecord.DeckId
					});
					GameSaveDataManager.Get().SaveSubkey(subkeySaveRequest, null);
					deckOption.IsNewlyUnlocked = false;
				}
			});
		}
		this.m_playButton.SetText("GLUE_CHOOSE");
		this.SetPlayMatStateAsInitializedAndPlayTransition();
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x00011FBC File Offset: 0x000101BC
	private GameSaveDataManager.AdventureDungeonCrawlWingProgressSubkeys WingProgressSubkeysForScenario(int scenarioId)
	{
		GameSaveDataManager.AdventureDungeonCrawlWingProgressSubkeys result;
		GameSaveDataManager.GetProgressSubkeysForDungeonCrawlWing(GameUtils.GetWingRecordFromMissionId(scenarioId), out result);
		return result;
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x00011FD8 File Offset: 0x000101D8
	private List<long> HeroPowerWinsForScenario(GameSaveKeyId adventureGameSaveServerKey, int scenarioId)
	{
		GameSaveDataManager.AdventureDungeonCrawlWingProgressSubkeys adventureDungeonCrawlWingProgressSubkeys = this.WingProgressSubkeysForScenario(scenarioId);
		if (adventureDungeonCrawlWingProgressSubkeys.heroPowerWins == (GameSaveKeySubkeyId)0)
		{
			return new List<long>();
		}
		List<long> result;
		GameSaveDataManager.Get().GetSubkeyValue(adventureGameSaveServerKey, adventureDungeonCrawlWingProgressSubkeys.heroPowerWins, out result);
		return result;
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x00012010 File Offset: 0x00010210
	private List<long> DeckWinsForScenario(GameSaveKeyId adventureGameSaveServerKey, int scenarioId)
	{
		GameSaveDataManager.AdventureDungeonCrawlWingProgressSubkeys adventureDungeonCrawlWingProgressSubkeys = this.WingProgressSubkeysForScenario(scenarioId);
		if (adventureDungeonCrawlWingProgressSubkeys.deckWins == (GameSaveKeySubkeyId)0)
		{
			return new List<long>();
		}
		List<long> result;
		GameSaveDataManager.Get().GetSubkeyValue(adventureGameSaveServerKey, adventureDungeonCrawlWingProgressSubkeys.deckWins, out result);
		return result;
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x00012048 File Offset: 0x00010248
	private List<long> TreasureWinsForScenario(GameSaveKeyId adventureGameSaveServerKey, int scenarioId)
	{
		GameSaveDataManager.AdventureDungeonCrawlWingProgressSubkeys adventureDungeonCrawlWingProgressSubkeys = this.WingProgressSubkeysForScenario(scenarioId);
		if (adventureDungeonCrawlWingProgressSubkeys.treasureWins == (GameSaveKeySubkeyId)0)
		{
			return new List<long>();
		}
		List<long> result;
		GameSaveDataManager.Get().GetSubkeyValue(adventureGameSaveServerKey, adventureDungeonCrawlWingProgressSubkeys.treasureWins, out result);
		return result;
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x00012080 File Offset: 0x00010280
	public void ShowNextBoss(string playButtonText)
	{
		this.SetPlayMatState(AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_NEXT_BOSS, true);
		this.m_playButton.SetText(playButtonText);
	}

	// Token: 0x060002CA RID: 714 RVA: 0x00012096 File Offset: 0x00010296
	public void ShowEmptyState()
	{
		this.SetPlayMatState(AdventureDungeonCrawlPlayMat.PlayMatState.READY_FOR_DATA, true);
	}

	// Token: 0x060002CB RID: 715 RVA: 0x000120A0 File Offset: 0x000102A0
	public void ShowPVPDRActiveRun(string playButtonText)
	{
		this.SetPlayMatState(AdventureDungeonCrawlPlayMat.PlayMatState.PVPDR_ACTIVE, true);
		this.m_playButton.SetText(playButtonText);
	}

	// Token: 0x060002CC RID: 716 RVA: 0x000120B6 File Offset: 0x000102B6
	public void ShowPVPDRReward()
	{
		this.SetPlayMatState(AdventureDungeonCrawlPlayMat.PlayMatState.PVPDR_REWARD, true);
	}

	// Token: 0x060002CD RID: 717 RVA: 0x000120C0 File Offset: 0x000102C0
	public void SetShouldShowBossHeroPowerTooltip(bool show)
	{
		this.m_shouldShowBossHeroPowerTooltip = show;
	}

	// Token: 0x060002CE RID: 718 RVA: 0x000120C9 File Offset: 0x000102C9
	public void ShowBossHeroPowerTooltip()
	{
		base.StartCoroutine(this.ShowBossHeroPowerTooltipWhenReady());
	}

	// Token: 0x060002CF RID: 719 RVA: 0x000120D8 File Offset: 0x000102D8
	private IEnumerator ShowBossHeroPowerTooltipWhenReady()
	{
		yield return new WaitForSeconds(0.5f);
		bool wasWaitingOnVO = false;
		while (NotificationManager.Get().IsQuotePlaying)
		{
			wasWaitingOnVO = true;
			yield return new WaitForEndOfFrame();
		}
		if (wasWaitingOnVO)
		{
			yield return new WaitForSeconds(this.m_bossHeroPowerTooltipDelayAfterVo);
		}
		if (this.m_bossHeroPowerTooltip != null && !this.m_bossHeroPowerTooltip.IsDying())
		{
			yield break;
		}
		if (!this.m_shouldShowBossHeroPowerTooltip)
		{
			yield break;
		}
		this.m_bossHeroPowerTooltip = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.m_bossHeroPowerTooltipBone.transform.localPosition, this.m_bossHeroPowerTooltipBone.transform.localScale, GameStrings.Get(AdventureDungeonCrawlPlayMat.HERO_POWER_TOOLTIP_STRING), true, NotificationManager.PopupTextType.BASIC);
		this.m_bossHeroPowerTooltip.ShowPopUpArrow(Notification.PopUpArrowDirection.Left);
		this.m_bossHeroPowerTooltip.PulseReminderEveryXSeconds(this.m_bossHeroPowerTooltipPulseRate);
		yield break;
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x000120E8 File Offset: 0x000102E8
	public void HideBossHeroPowerTooltip(bool immediate = false)
	{
		this.m_shouldShowBossHeroPowerTooltip = false;
		if (this.m_bossHeroPowerTooltip != null)
		{
			if (immediate)
			{
				UnityEngine.Object.Destroy(this.m_bossHeroPowerTooltip.gameObject);
				this.m_bossHeroPowerTooltip = null;
				return;
			}
			Notification bossHeroPowerTooltip = this.m_bossHeroPowerTooltip;
			bossHeroPowerTooltip.OnFinishDeathState = (Action<int>)Delegate.Combine(bossHeroPowerTooltip.OnFinishDeathState, new Action<int>(delegate(int groupId)
			{
				this.m_bossHeroPowerTooltip = null;
			}));
			this.m_bossHeroPowerTooltip.PlayDeath();
		}
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x00012157 File Offset: 0x00010357
	public AdventureDungeonCrawlPlayMat.PlayMatState GetPlayMatState()
	{
		return this.m_playMatState;
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x0001215F File Offset: 0x0001035F
	public AdventureDungeonCrawlPlayMat.OptionType GetPlayMatOptionType()
	{
		return this.m_currentOptionType;
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x00012168 File Offset: 0x00010368
	private void SetPlayMatState(AdventureDungeonCrawlPlayMat.PlayMatState state, bool setAsInitialized)
	{
		if (AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE == this.m_playMatState && state != AdventureDungeonCrawlPlayMat.PlayMatState.READY_FOR_DATA)
		{
			Log.Adventures.PrintError("Attempting to set Adventure Dungeon Crawl Play Mat to state {0}, but still in state TRANSITIONING_FROM_PREV_STATE! This is not allowed!", new object[]
			{
				state
			});
			return;
		}
		Log.Adventures.Print("Setting Adventure Dungeon Crawl Play Mat to state {0}", new object[]
		{
			state
		});
		this.m_playMatStateInitialized = false;
		if (AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE != state)
		{
			this.m_nextBossPane.SetActive(AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_NEXT_BOSS == state);
			this.m_optionsPane.SetActive(AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_OPTIONS == state);
			this.m_bossGraveyardPane.gameObject.SetActive(AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_BOSS_GRAVEYARD == state);
			this.m_duelsPlayMat.gameObject.SetActive(AdventureDungeonCrawlPlayMat.PlayMatState.PVPDR_ACTIVE == state || AdventureDungeonCrawlPlayMat.PlayMatState.PVPDR_REWARD == state);
			this.SetHeaderTextForState(state);
		}
		this.HandleDuelsPlayMatStateChange(state);
		if (AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_OPTIONS == state && this.m_selectedOptionClickBlocker != null)
		{
			this.m_selectedOptionClickBlocker.SetActive(true);
		}
		this.EnablePlayButton(false);
		if (this.m_playMatState != AdventureDungeonCrawlPlayMat.PlayMatState.READY_FOR_DATA && this.m_playMatState != AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE)
		{
			this.m_lastVisualPlayMatState = this.m_playMatState;
		}
		this.m_playMatState = state;
		if (setAsInitialized)
		{
			this.SetPlayMatStateAsInitializedAndPlayTransition();
		}
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x00012276 File Offset: 0x00010476
	private void SetPlayMatStateAsInitializedAndPlayTransition()
	{
		this.m_playMatStateInitialized = true;
		if (this.m_subsceneTransitionComplete)
		{
			this.PlayStateTransition(this.m_playMatState);
		}
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x00012294 File Offset: 0x00010494
	private void SetHeaderTextForState(AdventureDungeonCrawlPlayMat.PlayMatState state)
	{
		this.SetHeaderOverrideStrings();
		this.m_headerText.Show();
		switch (state)
		{
		case AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_OPTIONS:
			switch (this.m_currentOptionType)
			{
			case AdventureDungeonCrawlPlayMat.OptionType.LOOT:
			{
				string key = string.IsNullOrEmpty(this.m_chooseLootHeaderStringOverride) ? "GLUE_ADVENTURE_DUNGEON_CRAWL_CHOOSE_LOOT" : this.m_chooseLootHeaderStringOverride;
				this.m_headerText.Text = GameStrings.Get(key);
				return;
			}
			case AdventureDungeonCrawlPlayMat.OptionType.TREASURE:
			{
				string key2 = string.IsNullOrEmpty(this.m_chooseTreasureHeaderStringOverride) ? "GLUE_ADVENTURE_DUNGEON_CRAWL_CHOOSE_TREASURE" : this.m_chooseTreasureHeaderStringOverride;
				this.m_headerText.Text = GameStrings.Get(key2);
				return;
			}
			case AdventureDungeonCrawlPlayMat.OptionType.SHRINE_TREASURE:
				this.m_headerText.Text = GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_CHOOSE_SHRINE");
				return;
			case AdventureDungeonCrawlPlayMat.OptionType.HERO_POWER:
				this.m_headerText.Text = GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_CHOOSE_HERO_POWER");
				return;
			case AdventureDungeonCrawlPlayMat.OptionType.DECK:
				this.m_headerText.Text = GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_CHOOSE_DECK");
				return;
			case AdventureDungeonCrawlPlayMat.OptionType.TREASURE_SATCHEL:
				this.m_headerText.Hide();
				return;
			default:
				return;
			}
			break;
		case AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE:
		case AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_BOSS_GRAVEYARD:
			break;
		case AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_NEXT_BOSS:
			if (this.IsNextMissionASpecialEncounter)
			{
				this.m_headerText.Text = GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_SPECIAL_ENCOUNTER");
				return;
			}
			this.m_headerText.Text = GameStrings.Format("GLUE_ADVENTURE_DUNGEON_CRAWL_CHALLENGE_COUNT", new object[]
			{
				this.m_numBossesDefeated + 1,
				this.m_bossesPerRun
			});
			return;
		case AdventureDungeonCrawlPlayMat.PlayMatState.PVPDR_ACTIVE:
		case AdventureDungeonCrawlPlayMat.PlayMatState.PVPDR_REWARD:
			this.m_headerText.Hide();
			break;
		default:
			return;
		}
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x00012408 File Offset: 0x00010608
	public void ShowRunEnd(List<long> defeatedBossIds, long bossWhoDefeatedMeId, int numTotalBosses, bool hasCompletedAdventureWithAllClasses, bool firstTimeCompletedAsClass, int numClassesCompleted, GameSaveKeyId adventureGameSaveDataServerKey, GameSaveKeyId adventureGameSaveDataClientKey, AdventureDungeonCrawlPlayMat.AssetLoadCompletedCallback loadCompletedCallback, AdventureDungeonCrawlBossGraveyard.RunEndSequenceCompletedCallback sequenceCompletedCallback)
	{
		base.StartCoroutine(this.ShowRunEndAfterGraveyardIsInitialized(defeatedBossIds, bossWhoDefeatedMeId, numTotalBosses, hasCompletedAdventureWithAllClasses, firstTimeCompletedAsClass, numClassesCompleted, adventureGameSaveDataServerKey, adventureGameSaveDataClientKey, loadCompletedCallback, sequenceCompletedCallback));
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x00012433 File Offset: 0x00010633
	public void OnSubSceneLoaded()
	{
		this.HideContentBeforeIntroAnims();
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x0001243B File Offset: 0x0001063B
	public void OnSubSceneTransitionComplete()
	{
		base.StartCoroutine(this.ProcessSubsceneTransitionCompleteWhenReady());
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x0001244A File Offset: 0x0001064A
	private IEnumerator ProcessSubsceneTransitionCompleteWhenReady()
	{
		while (GameUtils.IsAnyTransitionActive() || PopupDisplayManager.Get().IsShowing)
		{
			yield return null;
		}
		this.m_subsceneTransitionComplete = true;
		this.m_allowPlayButtonAnimation = true;
		if (this.m_bossGraveyard != null)
		{
			this.m_bossGraveyard.OnSubSceneTransitionComplete();
		}
		this.PlayStateTransition(this.m_playMatState);
		yield break;
	}

	// Token: 0x060002DA RID: 730 RVA: 0x0001245C File Offset: 0x0001065C
	public void PlayRewardOptionSelected(AdventureDungeonCrawlRewardOption.OptionData optionData)
	{
		this.SetPlayMatState(AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE, true);
		for (int i = 0; i < this.m_rewardOptions.Count; i++)
		{
			this.m_rewardOptions[i].DisableInteraction();
			this.m_rewardOptions[i].PlayOutro(optionData.index == i);
		}
	}

	// Token: 0x060002DB RID: 731 RVA: 0x000124B2 File Offset: 0x000106B2
	public void PlayDeckOptionSelected()
	{
		this.PlayWidgetOptionSelected(this.m_deckOptions.Cast<AdventureOptionWidget>());
	}

	// Token: 0x060002DC RID: 732 RVA: 0x000124C5 File Offset: 0x000106C5
	public void PlayHeroPowerOptionSelected()
	{
		this.PlayWidgetOptionSelected(this.m_heroPowerOptions.Cast<AdventureOptionWidget>());
	}

	// Token: 0x060002DD RID: 733 RVA: 0x000124D8 File Offset: 0x000106D8
	public void PlayTreasureSatchelOptionSelected()
	{
		this.PlayWidgetOptionSelected(this.m_treasureSatchelOptions.Cast<AdventureOptionWidget>());
	}

	// Token: 0x060002DE RID: 734 RVA: 0x000124EC File Offset: 0x000106EC
	public void PlayTreasureSatchelOptionHidden()
	{
		this.PlayWidgetOptionSelected(this.m_treasureSatchelOptions.Cast<AdventureOptionWidget>());
		if (this.m_treasureSatchelWidget != null)
		{
			this.m_treasureSatchelWidget.TriggerEvent("PLAY_SATCHEL_MOTE_OUT", default(Widget.TriggerEventParameters));
		}
	}

	// Token: 0x060002DF RID: 735 RVA: 0x00012534 File Offset: 0x00010734
	private void PlayWidgetOptionSelected(IEnumerable<AdventureOptionWidget> options)
	{
		this.SetPlayMatState(AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE, true);
		foreach (AdventureOptionWidget adventureOptionWidget in options)
		{
			adventureOptionWidget.PlayOutro();
		}
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x00012584 File Offset: 0x00010784
	public Actor GetActorToAnimateFrom(string cardId, int index)
	{
		if (this.m_currentOptionType != AdventureDungeonCrawlPlayMat.OptionType.TREASURE_SATCHEL)
		{
			if (index < 0 || index > this.m_rewardOptions.Count)
			{
				return null;
			}
			return this.m_rewardOptions[index].GetActorFromCardId(cardId);
		}
		else
		{
			if (index < 0 || index > this.m_treasureSatchelOptions.Count)
			{
				return null;
			}
			return this.m_treasureSatchelOptions[index].CardActor;
		}
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x000125E8 File Offset: 0x000107E8
	private void PlayStateTransition(AdventureDungeonCrawlPlayMat.PlayMatState state)
	{
		if (!this.m_playMatStateInitialized)
		{
			return;
		}
		if (state == AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_NEXT_BOSS)
		{
			base.StartCoroutine(this.PlayNextBossAnimations(this.m_lastVisualPlayMatState == AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_OPTIONS));
			return;
		}
		if (state == AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_OPTIONS)
		{
			base.StartCoroutine(this.HandleOptionIntroAnimations());
			return;
		}
		if (state == AdventureDungeonCrawlPlayMat.PlayMatState.PVPDR_ACTIVE)
		{
			this.EnablePlayButton(true);
			return;
		}
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x0001263B File Offset: 0x0001083B
	private IEnumerator PlayNextBossAnimations(bool transitionFromPrevState)
	{
		bool flag = this.m_defeatedBossActors.Count == 0;
		bool finalBoss = this.m_defeatedBossActors.Count == this.m_bossesPerRun - 1;
		if (flag)
		{
			this.m_allCards.SetActive(true);
			this.m_bossDeckDropAnimation.Play();
			SoundManager.Get().LoadAndPlay(this.m_bossDeckDropSFXOverride);
			while (this.m_bossDeckDropAnimation.isPlaying)
			{
				yield return null;
			}
			yield return new WaitForSeconds(this.m_delayAfterDeckDrop);
		}
		else if (transitionFromPrevState)
		{
			if (this.m_nextBossCardBack != null)
			{
				Actor component = this.m_nextBossCardBack.GetComponent<Actor>();
				if (component != null)
				{
					component.ActivateSpellBirthState(SpellType.SUMMON_IN_DUNGEON_CRAWL);
					component.ActivateSpellBirthState(DraftDisplay.GetSpellTypeForRarity(TAG_RARITY.RARE));
				}
			}
			if (this.m_topDefeatedBoss != null)
			{
				this.m_topDefeatedBoss.ActivateSpellBirthState(SpellType.SUMMON_IN_DUNGEON_CRAWL);
				this.m_topDefeatedBoss.ActivateSpellBirthState(DraftDisplay.GetSpellTypeForRarity(TAG_RARITY.RARE));
			}
			SoundManager.Get().LoadAndPlay(this.m_bossDeckMagicallyAppearSFXOverride);
			yield return new WaitForSeconds(0.7f);
		}
		this.m_nextBossFlipAnimation.Play(finalBoss ? this.m_nextBossFlipLargeName : this.m_nextBossFlipSmallName);
		SoundManager.Get().LoadAndPlay(finalBoss ? this.m_nextBossFlipLargeSFXOverride : this.m_nextBossFlipSmallSFXOverride);
		yield return new WaitForSeconds(this.m_nextBossFlipCrowdReactionDelay);
		string input = this.m_nextBossFlipCrowdReactionMediumSFXOverride;
		if (this.m_numBossesDefeated == this.m_bossesPerRun - 1)
		{
			input = this.m_nextBossFlipCrowdReactionLargeSFXOverride;
		}
		else if (this.m_numBossesDefeated <= 3)
		{
			input = this.m_nextBossFlipCrowdReactionSmallSFXOverride;
		}
		SoundManager.Get().LoadAndPlay(input);
		while (this.m_nextBossFlipAnimation.isPlaying)
		{
			yield return null;
		}
		this.EnablePlayButton(true);
		this.ShowBossHeroPowerTooltip();
		this.PlayNextBossVO();
		yield break;
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x00012654 File Offset: 0x00010854
	private void PlayNextBossVO()
	{
		if (this.m_bossActor.GetEntityDef() == null)
		{
			return;
		}
		WingDbId wingIdFromMissionId = GameUtils.GetWingIdFromMissionId(this.m_dungeonCrawlData.GetMission());
		int referenceID = GameUtils.TranslateCardIdToDbId(this.m_bossActor.GetEntityDef().GetCardId(), false);
		if (this.m_numBossesDefeated + 1 < this.m_bossesPerRun || !DungeonCrawlSubDef_VOLines.PlayVOLine(this.m_dungeonCrawlData.GetSelectedAdventure(), wingIdFromMissionId, this.m_playerHeroDbId, DungeonCrawlSubDef_VOLines.VOEventType.FINAL_BOSS_REVEAL, referenceID, false))
		{
			DungeonCrawlSubDef_VOLines.PlayVOLine(this.m_dungeonCrawlData.GetSelectedAdventure(), wingIdFromMissionId, this.m_playerHeroDbId, DungeonCrawlSubDef_VOLines.BOSS_REVEAL_EVENTS, referenceID, false);
		}
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x000126E6 File Offset: 0x000108E6
	private IEnumerator HandleOptionIntroAnimations()
	{
		if (this.m_currentOptionType == AdventureDungeonCrawlPlayMat.OptionType.TREASURE || this.m_currentOptionType == AdventureDungeonCrawlPlayMat.OptionType.SHRINE_TREASURE)
		{
			yield return base.StartCoroutine(this.PlayRewardOptionAnimations(this.m_rewardOptions, 0f));
		}
		else if (this.m_currentOptionType == AdventureDungeonCrawlPlayMat.OptionType.LOOT)
		{
			List<AdventureDungeonCrawlRewardOption> list = new List<AdventureDungeonCrawlRewardOption>(this.m_rewardOptions);
			if (this.m_rewardOptions.Count >= 2)
			{
				list[0] = this.m_rewardOptions[1];
				list[1] = this.m_rewardOptions[0];
			}
			yield return base.StartCoroutine(this.PlayRewardOptionAnimations(list, this.m_lootDropDelay));
		}
		else if (this.m_currentOptionType == AdventureDungeonCrawlPlayMat.OptionType.HERO_POWER)
		{
			yield return base.StartCoroutine(this.PlayWidgetOptionAnimations(this.m_heroPowerOptions.Cast<AdventureOptionWidget>(), 0f));
		}
		else if (this.m_currentOptionType == AdventureDungeonCrawlPlayMat.OptionType.DECK)
		{
			yield return base.StartCoroutine(this.PlayWidgetOptionAnimations(this.m_deckOptions.Cast<AdventureOptionWidget>(), this.m_lootDropDelay));
		}
		else if (this.m_currentOptionType == AdventureDungeonCrawlPlayMat.OptionType.TREASURE_SATCHEL)
		{
			yield return base.StartCoroutine(this.PlayWidgetOptionAnimations(this.m_treasureSatchelOptions.Cast<AdventureOptionWidget>(), 0f));
		}
		if (this.m_selectedOptionClickBlocker != null)
		{
			this.m_selectedOptionClickBlocker.SetActive(false);
		}
		this.PlaySelectedOptionVO();
		yield break;
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x000126F5 File Offset: 0x000108F5
	private IEnumerator PlayRewardOptionAnimations(IEnumerable<AdventureDungeonCrawlRewardOption> options, float dropDelay)
	{
		this.HideContentBeforeIntroAnims();
		yield return new WaitForSeconds(0.5f);
		foreach (AdventureDungeonCrawlRewardOption option in options)
		{
			while (!option.IsInitialized())
			{
				yield return null;
			}
			option = null;
		}
		IEnumerator<AdventureDungeonCrawlRewardOption> enumerator = null;
		foreach (AdventureDungeonCrawlRewardOption adventureDungeonCrawlRewardOption in options)
		{
			adventureDungeonCrawlRewardOption.gameObject.SetActive(true);
			adventureDungeonCrawlRewardOption.PlayIntro();
			yield return new WaitForSeconds(dropDelay);
		}
		enumerator = null;
		foreach (AdventureDungeonCrawlRewardOption option in options)
		{
			while (option.IntroIsPlaying())
			{
				yield return null;
			}
			option = null;
		}
		enumerator = null;
		int num = 0;
		using (IEnumerator<AdventureDungeonCrawlRewardOption> enumerator2 = options.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				AdventureDungeonCrawlRewardOption adventureDungeonCrawlRewardOption2 = enumerator2.Current;
				if (!adventureDungeonCrawlRewardOption2.gameObject.activeInHierarchy)
				{
					Debug.LogWarning("AdventureDungeonCrawlPlayMat: The reward option at " + num + " was inactive when it was supposed to show");
					adventureDungeonCrawlRewardOption2.gameObject.SetActive(true);
				}
				num++;
			}
			yield break;
		}
		yield break;
		yield break;
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x00012712 File Offset: 0x00010912
	private IEnumerator PlayWidgetOptionAnimations(IEnumerable<AdventureOptionWidget> options, float dropDelay)
	{
		foreach (AdventureOptionWidget option in options)
		{
			while (!option.IsReady)
			{
				yield return null;
			}
			option = null;
		}
		IEnumerator<AdventureOptionWidget> enumerator = null;
		foreach (AdventureOptionWidget adventureOptionWidget in options)
		{
			adventureOptionWidget.PlayIntro();
			if (dropDelay > 0f)
			{
				yield return new WaitForSeconds(dropDelay);
			}
		}
		enumerator = null;
		foreach (AdventureOptionWidget option in options)
		{
			while (option.IsIntroPlaying)
			{
				yield return null;
			}
			option = null;
		}
		enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x00012728 File Offset: 0x00010928
	private void PlaySelectedOptionVO()
	{
		if (this.m_currentOptionType == AdventureDungeonCrawlPlayMat.OptionType.TREASURE || this.m_currentOptionType == AdventureDungeonCrawlPlayMat.OptionType.SHRINE_TREASURE || this.m_currentOptionType == AdventureDungeonCrawlPlayMat.OptionType.TREASURE_SATCHEL)
		{
			this.PlayTreasureOfferVO();
			return;
		}
		if (this.m_currentOptionType == AdventureDungeonCrawlPlayMat.OptionType.LOOT)
		{
			this.PlayLootPackOfferVO();
			return;
		}
		if (this.m_currentOptionType == AdventureDungeonCrawlPlayMat.OptionType.HERO_POWER)
		{
			this.PlayHeroPowerOfferVO();
			return;
		}
		if (this.m_currentOptionType == AdventureDungeonCrawlPlayMat.OptionType.DECK)
		{
			this.PlayDeckOfferVO();
		}
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x00012788 File Offset: 0x00010988
	private void PlayTreasureOfferVO()
	{
		Options.Get().SetBool(Option.HAS_JUST_SEEN_LOOT_NO_TAKE_CANDLE_VO, false);
		WingDbId wingIdFromMissionId = GameUtils.GetWingIdFromMissionId(this.m_dungeonCrawlData.GetMission());
		if (!DungeonCrawlSubDef_VOLines.PlayVOLine(this.m_dungeonCrawlData.GetSelectedAdventure(), wingIdFromMissionId, this.m_playerHeroDbId, DungeonCrawlSubDef_VOLines.OFFER_TREASURE_EVENTS, 0, true))
		{
			foreach (AdventureDungeonCrawlRewardOption adventureDungeonCrawlRewardOption in this.m_rewardOptions)
			{
				int treasureDatabaseID = adventureDungeonCrawlRewardOption.GetTreasureDatabaseID();
				if (DungeonCrawlSubDef_VOLines.PlayVOLine(this.m_dungeonCrawlData.GetSelectedAdventure(), wingIdFromMissionId, this.m_playerHeroDbId, DungeonCrawlSubDef_VOLines.OFFER_TREASURE_EVENTS, treasureDatabaseID, true))
				{
					if (treasureDatabaseID == 47251)
					{
						Options.Get().SetBool(Option.HAS_JUST_SEEN_LOOT_NO_TAKE_CANDLE_VO, true);
						break;
					}
					break;
				}
			}
		}
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x00012854 File Offset: 0x00010A54
	private void PlayLootPackOfferVO()
	{
		WingDbId wingIdFromMissionId = GameUtils.GetWingIdFromMissionId(this.m_dungeonCrawlData.GetMission());
		DungeonCrawlSubDef_VOLines.PlayVOLine(this.m_dungeonCrawlData.GetSelectedAdventure(), wingIdFromMissionId, this.m_playerHeroDbId, DungeonCrawlSubDef_VOLines.OFFER_LOOT_PACKS_EVENTS, 0, true);
	}

	// Token: 0x060002EA RID: 746 RVA: 0x00012894 File Offset: 0x00010A94
	private void PlayHeroPowerOfferVO()
	{
		WingDbId wingIdFromMissionId = GameUtils.GetWingIdFromMissionId(this.m_dungeonCrawlData.GetMission());
		DungeonCrawlSubDef_VOLines.PlayVOLine(this.m_dungeonCrawlData.GetSelectedAdventure(), wingIdFromMissionId, this.m_playerHeroDbId, DungeonCrawlSubDef_VOLines.OFFER_HERO_POWER_EVENTS, (int)this.m_dungeonCrawlData.SelectedHeroPowerDbId, true);
	}

	// Token: 0x060002EB RID: 747 RVA: 0x000128DC File Offset: 0x00010ADC
	private void PlayDeckOfferVO()
	{
		WingDbId wingIdFromMissionId = GameUtils.GetWingIdFromMissionId(this.m_dungeonCrawlData.GetMission());
		DungeonCrawlSubDef_VOLines.PlayVOLine(this.m_dungeonCrawlData.GetSelectedAdventure(), wingIdFromMissionId, this.m_playerHeroDbId, DungeonCrawlSubDef_VOLines.OFFER_DECK_EVENTS, (int)this.m_dungeonCrawlData.SelectedDeckId, true);
	}

	// Token: 0x060002EC RID: 748 RVA: 0x00012924 File Offset: 0x00010B24
	private void HideContentBeforeIntroAnims()
	{
		if (this.m_playMatState == AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_OPTIONS)
		{
			foreach (AdventureDungeonCrawlRewardOption adventureDungeonCrawlRewardOption in this.m_rewardOptions)
			{
				if (adventureDungeonCrawlRewardOption != null)
				{
					adventureDungeonCrawlRewardOption.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x060002ED RID: 749 RVA: 0x00012990 File Offset: 0x00010B90
	private IEnumerator ShowRunEndAfterGraveyardIsInitialized(List<long> defeatedBossIds, long bossWhoDefeatedMeId, int numTotalBosses, bool hasCompletedAdventureWithAllClasses, bool firstTimeCompletedAsClass, int numClassesCompleted, GameSaveKeyId adventureGameSaveDataServerKey, GameSaveKeyId adventureGameSaveDataClientKey, AdventureDungeonCrawlPlayMat.AssetLoadCompletedCallback loadCompletedCallback, AdventureDungeonCrawlBossGraveyard.RunEndSequenceCompletedCallback sequenceCompletedCallback)
	{
		this.SetPlayMatState(AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_BOSS_GRAVEYARD, false);
		while (!this.m_bossGraveyardPane.PrefabIsLoaded() || this.m_paperController == null)
		{
			yield return null;
		}
		yield return new WaitForEndOfFrame();
		if (this.m_bossGraveyard == null)
		{
			this.m_bossGraveyard = this.m_bossGraveyardPane.PrefabGameObject(false).GetComponent<AdventureDungeonCrawlBossGraveyard>();
			if (this.m_subsceneTransitionComplete)
			{
				this.m_bossGraveyard.OnSubSceneTransitionComplete();
			}
		}
		if (this.m_paperController != null && UniversalInputManager.UsePhoneUI)
		{
			this.m_paperController.gameObject.SetActive(false);
		}
		this.SetPlayMatStateAsInitializedAndPlayTransition();
		this.m_bossGraveyard.ShowRunEnd(this.m_dungeonCrawlData, defeatedBossIds, bossWhoDefeatedMeId, numTotalBosses, hasCompletedAdventureWithAllClasses, firstTimeCompletedAsClass, numClassesCompleted, this.m_playerHeroDbId, adventureGameSaveDataServerKey, adventureGameSaveDataClientKey, loadCompletedCallback, sequenceCompletedCallback);
		yield break;
	}

	// Token: 0x060002EE RID: 750 RVA: 0x000129F8 File Offset: 0x00010BF8
	private void SetPlaymatVisualStyle()
	{
		DungeonRunVisualStyle visualStyle = this.m_dungeonCrawlData.VisualStyle;
		this.m_nextBossFlipSmallSFXOverride = this.m_nextBossFlipSmallSFXDefault;
		this.m_nextBossFlipLargeSFXOverride = this.m_nextBossFlipLargeSFXDefault;
		this.m_nextBossFlipCrowdReactionSmallSFXOverride = this.m_nextBossFlipCrowdReactionSmallSFXDefault;
		this.m_nextBossFlipCrowdReactionMediumSFXOverride = this.m_nextBossFlipCrowdReactionMediumSFXDefault;
		this.m_nextBossFlipCrowdReactionLargeSFXOverride = this.m_nextBossFlipCrowdReactionLargeSFXDefault;
		this.m_bossDeckDropSFXOverride = this.m_bossDeckDropSFXDefault;
		this.m_bossDeckMagicallyAppearSFXOverride = this.m_bossDeckMagicallyAppearSFXDefault;
		foreach (AdventureDungeonCrawlPlayMat.PlaymatStyleOverride playmatStyleOverride in this.m_playmatStyleOverride)
		{
			if (playmatStyleOverride.VisualStyle == visualStyle)
			{
				this.m_matchingPlaymatStyle = playmatStyleOverride;
				if (UniversalInputManager.UsePhoneUI)
				{
					this.m_headerText.TextColor = playmatStyleOverride.PhoneHeaderTextColor;
					this.m_headerText.OutlineColor = playmatStyleOverride.PhoneHeaderOutlineColor;
				}
				if (this.m_nextBossPlayNewParticlesScript != null)
				{
					this.m_nextBossPlayNewParticlesScript.m_Target = playmatStyleOverride.NextBossDustEffectSmall.gameObject;
					this.m_nextBossPlayNewParticlesScript.m_Target2 = playmatStyleOverride.NextBossDustEffectLargeMotes.gameObject;
					this.m_nextBossPlayNewParticlesScript.m_Target3 = playmatStyleOverride.NextBossDustEffectLarge.gameObject;
				}
				if (this.m_facedownBossesPlayNewParticlesScript != null)
				{
					this.m_facedownBossesPlayNewParticlesScript.m_Target = playmatStyleOverride.FacedownBossesDustEffect.gameObject;
				}
				if (!string.IsNullOrEmpty(playmatStyleOverride.NextBossFlipSmallSFX))
				{
					this.m_nextBossFlipSmallSFXOverride = playmatStyleOverride.NextBossFlipSmallSFX;
				}
				if (!string.IsNullOrEmpty(playmatStyleOverride.NextBossFlipLargeSFX))
				{
					this.m_nextBossFlipLargeSFXOverride = playmatStyleOverride.NextBossFlipLargeSFX;
				}
				if (!string.IsNullOrEmpty(playmatStyleOverride.NextBossFlipCrowdReactionSmallSFX))
				{
					this.m_nextBossFlipCrowdReactionSmallSFXOverride = playmatStyleOverride.NextBossFlipCrowdReactionSmallSFX;
				}
				if (!string.IsNullOrEmpty(playmatStyleOverride.NextBossFlipCrowdReactionMediumSFX))
				{
					this.m_nextBossFlipCrowdReactionMediumSFXOverride = playmatStyleOverride.NextBossFlipCrowdReactionMediumSFX;
				}
				if (!string.IsNullOrEmpty(playmatStyleOverride.NextBossFlipCrowdReactionLargeSFX))
				{
					this.m_nextBossFlipCrowdReactionLargeSFXOverride = playmatStyleOverride.NextBossFlipCrowdReactionLargeSFX;
				}
				if (!string.IsNullOrEmpty(playmatStyleOverride.BossDeckDropSFX))
				{
					this.m_bossDeckDropSFXOverride = playmatStyleOverride.BossDeckDropSFX;
				}
				if (!string.IsNullOrEmpty(playmatStyleOverride.BossDeckMagicallyAppearSFX))
				{
					this.m_bossDeckMagicallyAppearSFXOverride = playmatStyleOverride.BossDeckMagicallyAppearSFX;
					break;
				}
				break;
			}
		}
	}

	// Token: 0x060002EF RID: 751 RVA: 0x00012C1C File Offset: 0x00010E1C
	private void SetHeaderOverrideStrings()
	{
		if (this.m_matchingPlaymatStyle == null)
		{
			return;
		}
		AdventureDungeonCrawlPlayMat.HeaderStringOverride headerStringOverride = null;
		AdventureDungeonCrawlPlayMat.HeaderStringOverride headerStringOverride2 = null;
		if (this.m_matchingPlaymatStyle.ChooseTreasureHeaderString.Any<AdventureDungeonCrawlPlayMat.HeaderStringOverride>())
		{
			headerStringOverride = (from s in this.m_matchingPlaymatStyle.ChooseTreasureHeaderString
			orderby s.MinimumDefeatedBosses descending
			select s).First((AdventureDungeonCrawlPlayMat.HeaderStringOverride s) => s.MinimumDefeatedBosses <= this.m_numBossesDefeated);
		}
		if (this.m_matchingPlaymatStyle.ChooseLootHeaderString.Any<AdventureDungeonCrawlPlayMat.HeaderStringOverride>())
		{
			headerStringOverride2 = (from s in this.m_matchingPlaymatStyle.ChooseLootHeaderString
			orderby s.MinimumDefeatedBosses descending
			select s).First((AdventureDungeonCrawlPlayMat.HeaderStringOverride s) => s.MinimumDefeatedBosses <= this.m_numBossesDefeated);
		}
		if (headerStringOverride != null)
		{
			this.m_chooseTreasureHeaderStringOverride = headerStringOverride.HeaderString;
		}
		if (headerStringOverride2 != null)
		{
			this.m_chooseLootHeaderStringOverride = headerStringOverride2.HeaderString;
		}
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x00012CFA File Offset: 0x00010EFA
	private void OnPlayButtonReady(PlayButton playButton)
	{
		if (playButton == null)
		{
			Error.AddDevWarning("UI Error!", "PlayButtonReference is null, or does not have a PlayButton component on it!", Array.Empty<object>());
			return;
		}
		this.m_playButton = playButton;
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x00012D24 File Offset: 0x00010F24
	private void OnPaperControllerReady(VisualController paperController)
	{
		if (paperController == null)
		{
			Error.AddDevWarning("UI Issue!", "PlayMat's m_paperControllerReference is null! Can't set the correct PlayMat texture!.", Array.Empty<object>());
			return;
		}
		if (this.m_dungeonCrawlData == null)
		{
			Error.AddDevWarning("UI Issue!", "PlayMat's m_dungeonCrawlData is null! Can't set the correct PlayMat texture!.", Array.Empty<object>());
			return;
		}
		this.m_paperController = paperController;
		int mission = (int)this.m_dungeonCrawlData.GetMission();
		WingDbfRecord wingRecordFromMissionId = GameUtils.GetWingRecordFromMissionId(mission);
		if (wingRecordFromMissionId == null)
		{
			Log.Adventures.PrintError("No WingDbfRecord found for ScenarioDbId {0}!", new object[]
			{
				mission
			});
			return;
		}
		paperController.SetState(wingRecordFromMissionId.VisualStateName);
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x00012DB8 File Offset: 0x00010FB8
	private void OnTreasureSatchelReady(Widget widget)
	{
		if (widget == null)
		{
			Debug.LogError("AdventureDungeonCrawlPlayMat.OnTreasureSatchelReady - widget was null!");
			return;
		}
		AdventureTreasureSatchelDataModel adventureTreasureSatchelDataModel = new AdventureTreasureSatchelDataModel();
		this.m_treasureSatchelOptions = new List<AdventureDungeonCrawlTreasureOption>(widget.GetComponentsInChildren<AdventureDungeonCrawlTreasureOption>());
		foreach (AdventureDungeonCrawlTreasureOption adventureDungeonCrawlTreasureOption in this.m_treasureSatchelOptions)
		{
			adventureTreasureSatchelDataModel.LoadoutOptions.Add(adventureDungeonCrawlTreasureOption.GetDataModel());
		}
		widget.BindDataModel(adventureTreasureSatchelDataModel, false);
		widget.RegisterEventListener(delegate(string eventName)
		{
			if (eventName == "CODE_TREASURE_SATCHEL_OUTRO_COMPLETE")
			{
				widget.Hide();
				return;
			}
			if (eventName == "CODE_TREASURE_OPTION_SELECTED")
			{
				IDataModel dataModel;
				if (this.m_treasureInspectWidget == null || !this.m_treasureInspectWidget.GetDataModel(27, out dataModel))
				{
					Debug.LogError("AdventureDungeonCrawlPlayMat.OnTreasureSatchelReady - selected event called with no CardDataModel found or treasure inspect widget didn't load!");
					return;
				}
				if (!(dataModel is CardDataModel))
				{
					Debug.LogError("AdventureDungeonCrawlPlayMat.OnTreasureSatchelReady - selected event called but CardDataModel was null!");
					return;
				}
				EventDataModel dataModel2 = widget.GetDataModel<EventDataModel>();
				int num = 0;
				if (dataModel2.Payload is IConvertible)
				{
					num = Convert.ToInt32(dataModel2.Payload);
				}
				for (int i = 0; i < this.m_treasureSatchelOptions.Count; i++)
				{
					AdventureDungeonCrawlTreasureOption adventureDungeonCrawlTreasureOption2 = this.m_treasureSatchelOptions[i];
					if (i == num)
					{
						adventureDungeonCrawlTreasureOption2.Select();
					}
					else
					{
						adventureDungeonCrawlTreasureOption2.Deselect();
					}
				}
			}
		});
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x00012E84 File Offset: 0x00011084
	private void OnTreasureInspectReady(Widget widget)
	{
		if (widget == null)
		{
			Debug.LogError("AdventureDungeonCrawlPlayMat.OnTreasureSatchelReady - widget was null!");
			return;
		}
		this.m_treasureInspectWidget = widget;
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x00012EA1 File Offset: 0x000110A1
	public bool IsPaperControllerReady()
	{
		return this.m_paperController != null && !this.m_paperController.IsChangingStates;
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x00012EC4 File Offset: 0x000110C4
	public void OnPVPDRPlayMatReady(DuelsPlayMat playMat)
	{
		if (playMat == null)
		{
			Debug.LogError("AdventureDungeonCrawlPlayMat.OnPVPDRPlayMatReady - widget loaded did not have DuelsPlayMat script!");
			return;
		}
		this.m_duelsPlayMat = playMat;
		this.m_duelsPlayWidget = this.m_duelsPlayMat.GetComponent<Widget>();
		if (PvPDungeonRunDisplay.Get() != null)
		{
			this.m_duelsPlayWidget.BindDataModel(PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel(), false);
		}
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x00012F20 File Offset: 0x00011120
	public bool IsReadyToShowDuelsRewards()
	{
		return this.m_duelsReadyToShowRewards;
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x00012F28 File Offset: 0x00011128
	private void OnDuelsVaultOpened()
	{
		this.m_duelsReadyToShowRewards = true;
		this.m_duelsPlayMat.SetLeverButtonEnabled(false);
		this.m_duelsPlayMat.RemoveVaultDoorOpenedListener(new Action(this.OnDuelsVaultOpened));
		if (DuelsConfig.Get().GetRewardNoticeToShow() == null)
		{
			AdventureDungeonCrawlDisplay.Get().EndDuelsSession(0L);
		}
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x00012F77 File Offset: 0x00011177
	private void OnDuelsVaultClicked()
	{
		AdventureDungeonCrawlDisplay.Get().SetShowDeckButtonEnabled(false);
		this.m_duelsPlayMat.RemoveVaultDoorClickedListener(new Action(this.OnDuelsVaultClicked));
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x00012F9B File Offset: 0x0001119B
	public void OnDuelsRewardsAccepted()
	{
		this.m_duelsReadyToShowRewards = false;
	}

	// Token: 0x060002FA RID: 762 RVA: 0x00012FA4 File Offset: 0x000111A4
	private void HandleDuelsPlayMatStateChange(AdventureDungeonCrawlPlayMat.PlayMatState state)
	{
		if (AdventureDungeonCrawlPlayMat.PlayMatState.PVPDR_ACTIVE != state && AdventureDungeonCrawlPlayMat.PlayMatState.PVPDR_REWARD != state)
		{
			return;
		}
		bool isPaidEntry = PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().IsPaidEntry;
		this.m_duelsPlayWidget.TriggerEvent(isPaidEntry ? DuelsConfig.ANIMATE_PAID_STATE : DuelsConfig.ANIMATE_FREE_STATE, default(Widget.TriggerEventParameters));
		DuelsConfig.Get().ResetLastGameResult();
		this.m_duelsPlayMat.SetLeverButtonEnabled(AdventureDungeonCrawlPlayMat.PlayMatState.PVPDR_REWARD == state && isPaidEntry);
		if (AdventureDungeonCrawlPlayMat.PlayMatState.PVPDR_REWARD == state)
		{
			this.m_playButton.Disable(false);
			if (isPaidEntry)
			{
				this.m_duelsPlayMat.RegisterVaultDoorOpenedListener(new Action(this.OnDuelsVaultOpened));
				this.m_duelsPlayMat.RegisterVaultDoorClickedListener(new Action(this.OnDuelsVaultClicked));
				return;
			}
			AdventureDungeonCrawlDisplay.Get().EndDuelsSession(0L);
		}
	}

	// Token: 0x040001C7 RID: 455
	[CustomEditField(Sections = "UI")]
	public UberText m_headerText;

	// Token: 0x040001C8 RID: 456
	[CustomEditField(Sections = "UI")]
	public AsyncReference m_PlayButtonReference;

	// Token: 0x040001C9 RID: 457
	[CustomEditField(Sections = "UI")]
	public GameObject m_PlayButtonRoot;

	// Token: 0x040001CA RID: 458
	[CustomEditField(Sections = "UI")]
	public GameObject m_PlayButtonPlate;

	// Token: 0x040001CB RID: 459
	[CustomEditField(Sections = "UI")]
	public List<NestedPrefabPlatformOverride> m_rewardOptionNestedPrefabs = new List<NestedPrefabPlatformOverride>();

	// Token: 0x040001CC RID: 460
	[CustomEditField(Sections = "UI")]
	public List<AdventureDungeonCrawlHeroPowerOption> m_heroPowerOptions;

	// Token: 0x040001CD RID: 461
	[CustomEditField(Sections = "UI")]
	public List<AdventureDungeonCrawlDeckOption> m_deckOptions;

	// Token: 0x040001CE RID: 462
	[CustomEditField(Sections = "UI")]
	public Widget m_treasureSatchelWidget;

	// Token: 0x040001CF RID: 463
	[CustomEditField(Sections = "UI")]
	public GameObject m_optionsPane;

	// Token: 0x040001D0 RID: 464
	[CustomEditField(Sections = "UI")]
	public GameObject m_nextBossPane;

	// Token: 0x040001D1 RID: 465
	[CustomEditField(Sections = "UI")]
	public NestedPrefabBase m_bossGraveyardPane;

	// Token: 0x040001D2 RID: 466
	[CustomEditField(Sections = "UI")]
	public GameObject m_allCards;

	// Token: 0x040001D3 RID: 467
	[CustomEditField(Sections = "UI")]
	public GameObject m_facedownCards;

	// Token: 0x040001D4 RID: 468
	[CustomEditField(Sections = "UI")]
	public GameObject m_bossHeroPowerTooltipBone;

	// Token: 0x040001D5 RID: 469
	[CustomEditField(Sections = "UI")]
	public float m_bossHeroPowerTooltipPulseRate;

	// Token: 0x040001D6 RID: 470
	[CustomEditField(Sections = "UI")]
	public float m_bossHeroPowerTooltipDelayAfterVo;

	// Token: 0x040001D7 RID: 471
	[CustomEditField(Sections = "UI")]
	public PlayNewParticles m_nextBossPlayNewParticlesScript;

	// Token: 0x040001D8 RID: 472
	[CustomEditField(Sections = "UI")]
	public PlayNewParticles m_facedownBossesPlayNewParticlesScript;

	// Token: 0x040001D9 RID: 473
	[CustomEditField(Sections = "UI")]
	public AsyncReference m_treasureSatchelReference;

	// Token: 0x040001DA RID: 474
	[CustomEditField(Sections = "UI")]
	public AsyncReference m_treasureInspectReference;

	// Token: 0x040001DB RID: 475
	[CustomEditField(Sections = "UI")]
	public AsyncReference m_platformControllerReference;

	// Token: 0x040001DC RID: 476
	[CustomEditField(Sections = "UI")]
	public AsyncReference m_paperControllerReference;

	// Token: 0x040001DD RID: 477
	[CustomEditField(Sections = "UI")]
	public AsyncReference m_paperControllerReference_phone;

	// Token: 0x040001DE RID: 478
	[CustomEditField(Sections = "UI")]
	public GameObject m_selectedOptionClickBlocker;

	// Token: 0x040001DF RID: 479
	[CustomEditField(Sections = "Animations")]
	public Animation m_nextBossFlipAnimation;

	// Token: 0x040001E0 RID: 480
	[CustomEditField(Sections = "Animations")]
	public string m_nextBossFlipSmallName;

	// Token: 0x040001E1 RID: 481
	[CustomEditField(Sections = "Animations")]
	public string m_nextBossFlipLargeName;

	// Token: 0x040001E2 RID: 482
	[CustomEditField(Sections = "Animations")]
	public Animation m_bossDeckDropAnimation;

	// Token: 0x040001E3 RID: 483
	[CustomEditField(Sections = "Animations")]
	public float m_delayAfterDeckDrop = 1f;

	// Token: 0x040001E4 RID: 484
	[CustomEditField(Sections = "Animations")]
	public float m_lootDropDelay = 0.05f;

	// Token: 0x040001E5 RID: 485
	[CustomEditField(Sections = "SFX", T = EditType.SOUND_PREFAB)]
	public string m_nextBossFlipSmallSFXDefault;

	// Token: 0x040001E6 RID: 486
	[CustomEditField(Sections = "SFX", T = EditType.SOUND_PREFAB)]
	public string m_nextBossFlipLargeSFXDefault;

	// Token: 0x040001E7 RID: 487
	[CustomEditField(Sections = "SFX", T = EditType.SOUND_PREFAB)]
	public string m_nextBossFlipCrowdReactionSmallSFXDefault;

	// Token: 0x040001E8 RID: 488
	[CustomEditField(Sections = "SFX", T = EditType.SOUND_PREFAB)]
	public string m_nextBossFlipCrowdReactionMediumSFXDefault;

	// Token: 0x040001E9 RID: 489
	[CustomEditField(Sections = "SFX", T = EditType.SOUND_PREFAB)]
	public string m_nextBossFlipCrowdReactionLargeSFXDefault;

	// Token: 0x040001EA RID: 490
	[CustomEditField(Sections = "SFX")]
	public float m_nextBossFlipCrowdReactionDelay = 0.5f;

	// Token: 0x040001EB RID: 491
	[CustomEditField(Sections = "SFX", T = EditType.SOUND_PREFAB)]
	public string m_bossDeckDropSFXDefault;

	// Token: 0x040001EC RID: 492
	[CustomEditField(Sections = "SFX", T = EditType.SOUND_PREFAB)]
	public string m_bossDeckMagicallyAppearSFXDefault;

	// Token: 0x040001ED RID: 493
	[CustomEditField(Sections = "SFX", T = EditType.SOUND_PREFAB)]
	public string m_bossMouseOverSFXDefault;

	// Token: 0x040001EE RID: 494
	[CustomEditField(Sections = "Styles")]
	public List<AdventureDungeonCrawlPlayMat.PlaymatStyleOverride> m_playmatStyleOverride;

	// Token: 0x040001EF RID: 495
	[CustomEditField(Sections = "Bones")]
	public Transform m_nextBossBone;

	// Token: 0x040001F0 RID: 496
	[CustomEditField(Sections = "Bones")]
	public Transform m_nextBossFaceBone;

	// Token: 0x040001F1 RID: 497
	[CustomEditField(Sections = "Bones")]
	public Transform m_nextBossBackBone;

	// Token: 0x040001F2 RID: 498
	[CustomEditField(Sections = "Bones")]
	public List<Transform> m_bossCardBones = new List<Transform>();

	// Token: 0x040001F3 RID: 499
	[CustomEditField(Sections = "Bones")]
	public GameObject m_BossPowerBone;

	// Token: 0x040001F4 RID: 500
	[CustomEditField(Sections = "Bones")]
	public List<Transform> m_cardBackBones = new List<Transform>();

	// Token: 0x040001F5 RID: 501
	[CustomEditField(Sections = "Bones")]
	public SlidingTray m_MobilePlayButtonSlidingTrayBone;

	// Token: 0x040001F6 RID: 502
	[CustomEditField(Sections = "PVPDR")]
	public AsyncReference m_duelsPlayMatReference;

	// Token: 0x040001F8 RID: 504
	private AdventureDungeonCrawlPlayMat.PlayMatState m_playMatState;

	// Token: 0x040001F9 RID: 505
	private AdventureDungeonCrawlPlayMat.PlayMatState m_lastVisualPlayMatState;

	// Token: 0x040001FA RID: 506
	private bool m_startCallFinished;

	// Token: 0x040001FB RID: 507
	private Actor m_bossActor;

	// Token: 0x040001FC RID: 508
	private DefLoader.DisposableCardDef m_bossCardDef;

	// Token: 0x040001FD RID: 509
	private EntityDef m_bossEntityDef;

	// Token: 0x040001FE RID: 510
	private List<Actor> m_defeatedBossActors = new List<Actor>();

	// Token: 0x040001FF RID: 511
	private GameObject m_nextBossCardBack;

	// Token: 0x04000200 RID: 512
	private Actor m_topDefeatedBoss;

	// Token: 0x04000201 RID: 513
	private List<GameObject> m_cardBacks = new List<GameObject>();

	// Token: 0x04000202 RID: 514
	private PlayButton m_playButton;

	// Token: 0x04000203 RID: 515
	private List<AdventureDungeonCrawlRewardOption> m_rewardOptions;

	// Token: 0x04000204 RID: 516
	private AdventureDungeonCrawlBossGraveyard m_bossGraveyard;

	// Token: 0x04000205 RID: 517
	private bool m_subsceneTransitionComplete;

	// Token: 0x04000206 RID: 518
	private global::CardBack m_cardBack;

	// Token: 0x04000207 RID: 519
	private AdventureDungeonCrawlPlayMat.OptionType m_currentOptionType;

	// Token: 0x04000208 RID: 520
	private int m_numBossesDefeated;

	// Token: 0x04000209 RID: 521
	private int m_bossesPerRun;

	// Token: 0x0400020A RID: 522
	private bool m_allowPlayButtonAnimation;

	// Token: 0x0400020B RID: 523
	private bool m_setUpDefeatedBossesCompleted;

	// Token: 0x0400020C RID: 524
	private int m_playerHeroDbId;

	// Token: 0x0400020D RID: 525
	private AdventureDungeonCrawlPlayMat.PlaymatStyleOverride m_matchingPlaymatStyle;

	// Token: 0x0400020E RID: 526
	private string m_nextBossFlipSmallSFXOverride;

	// Token: 0x0400020F RID: 527
	private string m_nextBossFlipLargeSFXOverride;

	// Token: 0x04000210 RID: 528
	private string m_nextBossFlipCrowdReactionSmallSFXOverride;

	// Token: 0x04000211 RID: 529
	private string m_nextBossFlipCrowdReactionMediumSFXOverride;

	// Token: 0x04000212 RID: 530
	private string m_nextBossFlipCrowdReactionLargeSFXOverride;

	// Token: 0x04000213 RID: 531
	private string m_bossDeckDropSFXOverride;

	// Token: 0x04000214 RID: 532
	private string m_bossDeckMagicallyAppearSFXOverride;

	// Token: 0x04000215 RID: 533
	private string m_chooseTreasureHeaderStringOverride;

	// Token: 0x04000216 RID: 534
	private string m_chooseLootHeaderStringOverride;

	// Token: 0x04000217 RID: 535
	private List<AdventureDungeonCrawlTreasureOption> m_treasureSatchelOptions;

	// Token: 0x04000218 RID: 536
	public Widget m_treasureInspectWidget;

	// Token: 0x04000219 RID: 537
	private bool m_loadingCardback;

	// Token: 0x0400021A RID: 538
	private Notification m_bossHeroPowerTooltip;

	// Token: 0x0400021B RID: 539
	private bool m_shouldShowBossHeroPowerTooltip;

	// Token: 0x0400021C RID: 540
	private VisualController m_paperController;

	// Token: 0x0400021D RID: 541
	private IDungeonCrawlData m_dungeonCrawlData;

	// Token: 0x0400021E RID: 542
	private static readonly PlatformDependentValue<string> HERO_POWER_TOOLTIP_STRING = new PlatformDependentValue<string>(PlatformCategory.Screen)
	{
		PC = "GLUE_ADVENTURE_DUNGEON_CRAWL_BOSS_HERO_POWER_TOOLTIP",
		Phone = "GLUE_ADVENTURE_DUNGEON_CRAWL_BOSS_HERO_POWER_TOOLTIP_PHONE"
	};

	// Token: 0x0400021F RID: 543
	private const string TREASURE_SATCHEL_OPTION_SELECTED_EVENT = "CODE_TREASURE_OPTION_SELECTED";

	// Token: 0x04000220 RID: 544
	private const string TREASURE_SATCHEL_SHOW_EVENT = "CODE_TREASURE_SATCHEL_SHOW";

	// Token: 0x04000221 RID: 545
	private const string TREASURE_SATCHEL_OUTRO_COMPLETE_EVENT = "CODE_TREASURE_SATCHEL_OUTRO_COMPLETE";

	// Token: 0x04000222 RID: 546
	private bool m_playMatStateInitialized;

	// Token: 0x04000223 RID: 547
	private Widget m_duelsPlayWidget;

	// Token: 0x04000224 RID: 548
	private DuelsPlayMat m_duelsPlayMat;

	// Token: 0x04000225 RID: 549
	private bool m_duelsReadyToShowRewards;

	// Token: 0x020012CA RID: 4810
	// (Invoke) Token: 0x0600D56C RID: 54636
	public delegate void RewardOptionSelectedCallback(AdventureDungeonCrawlRewardOption.OptionData rewardData);

	// Token: 0x020012CB RID: 4811
	// (Invoke) Token: 0x0600D570 RID: 54640
	public delegate void AssetLoadCompletedCallback();

	// Token: 0x020012CC RID: 4812
	public enum PlayMatState
	{
		// Token: 0x0400A494 RID: 42132
		READY_FOR_DATA,
		// Token: 0x0400A495 RID: 42133
		SHOWING_OPTIONS,
		// Token: 0x0400A496 RID: 42134
		TRANSITIONING_FROM_PREV_STATE,
		// Token: 0x0400A497 RID: 42135
		SHOWING_NEXT_BOSS,
		// Token: 0x0400A498 RID: 42136
		SHOWING_BOSS_GRAVEYARD,
		// Token: 0x0400A499 RID: 42137
		PVPDR_ACTIVE,
		// Token: 0x0400A49A RID: 42138
		PVPDR_REWARD
	}

	// Token: 0x020012CD RID: 4813
	public enum OptionType
	{
		// Token: 0x0400A49C RID: 42140
		INVALID,
		// Token: 0x0400A49D RID: 42141
		LOOT,
		// Token: 0x0400A49E RID: 42142
		TREASURE,
		// Token: 0x0400A49F RID: 42143
		SHRINE_TREASURE,
		// Token: 0x0400A4A0 RID: 42144
		HERO_POWER,
		// Token: 0x0400A4A1 RID: 42145
		DECK,
		// Token: 0x0400A4A2 RID: 42146
		TREASURE_SATCHEL
	}

	// Token: 0x020012CE RID: 4814
	[Serializable]
	public class PlaymatStyleOverride
	{
		// Token: 0x0400A4A3 RID: 42147
		public DungeonRunVisualStyle VisualStyle;

		// Token: 0x0400A4A4 RID: 42148
		public Color PhoneHeaderTextColor;

		// Token: 0x0400A4A5 RID: 42149
		public Color PhoneHeaderOutlineColor;

		// Token: 0x0400A4A6 RID: 42150
		public ParticleSystem NextBossDustEffectSmall;

		// Token: 0x0400A4A7 RID: 42151
		public ParticleSystem NextBossDustEffectLarge;

		// Token: 0x0400A4A8 RID: 42152
		public ParticleSystem NextBossDustEffectLargeMotes;

		// Token: 0x0400A4A9 RID: 42153
		public ParticleSystem FacedownBossesDustEffect;

		// Token: 0x0400A4AA RID: 42154
		[CustomEditField(Sections = "SFX Overrides", T = EditType.SOUND_PREFAB)]
		public string NextBossFlipSmallSFX;

		// Token: 0x0400A4AB RID: 42155
		[CustomEditField(Sections = "SFX Overrides", T = EditType.SOUND_PREFAB)]
		public string NextBossFlipLargeSFX;

		// Token: 0x0400A4AC RID: 42156
		[CustomEditField(Sections = "SFX Overrides", T = EditType.SOUND_PREFAB)]
		public string NextBossFlipCrowdReactionSmallSFX;

		// Token: 0x0400A4AD RID: 42157
		[CustomEditField(Sections = "SFX Overrides", T = EditType.SOUND_PREFAB)]
		public string NextBossFlipCrowdReactionMediumSFX;

		// Token: 0x0400A4AE RID: 42158
		[CustomEditField(Sections = "SFX Overrides", T = EditType.SOUND_PREFAB)]
		public string NextBossFlipCrowdReactionLargeSFX;

		// Token: 0x0400A4AF RID: 42159
		[CustomEditField(Sections = "SFX Overrides", T = EditType.SOUND_PREFAB)]
		public string BossDeckDropSFX;

		// Token: 0x0400A4B0 RID: 42160
		[CustomEditField(Sections = "SFX Overrides", T = EditType.SOUND_PREFAB)]
		public string BossDeckMagicallyAppearSFX;

		// Token: 0x0400A4B1 RID: 42161
		[CustomEditField(Sections = "String Overrides")]
		public List<AdventureDungeonCrawlPlayMat.HeaderStringOverride> ChooseTreasureHeaderString;

		// Token: 0x0400A4B2 RID: 42162
		[CustomEditField(Sections = "String Overrides")]
		public List<AdventureDungeonCrawlPlayMat.HeaderStringOverride> ChooseLootHeaderString;
	}

	// Token: 0x020012CF RID: 4815
	[Serializable]
	public class HeaderStringOverride
	{
		// Token: 0x0400A4B3 RID: 42163
		public int MinimumDefeatedBosses;

		// Token: 0x0400A4B4 RID: 42164
		public string HeaderString;
	}
}
