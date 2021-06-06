using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Hearthstone;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x020002B7 RID: 695
[CustomEditClass]
public class DraftDisplay : MonoBehaviour
{
	// Token: 0x06002409 RID: 9225 RVA: 0x000B3C54 File Offset: 0x000B1E54
	private void Awake()
	{
		DraftDisplay.s_instance = this;
		this.m_draftManager = DraftManager.Get();
		AssetLoader.Get().InstantiatePrefab("DraftHeroChooseButton.prefab:7640de5f1d8e50e4caf8dccc55f28c6a", new PrefabCallback<GameObject>(this.OnConfirmButtonLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		AssetLoader.Get().InstantiatePrefab("History_HeroPower.prefab:e73edf8ccea2b11429093f7a448eef53", new PrefabCallback<GameObject>(this.LoadHeroPowerCallback), null, AssetLoadingOptions.IgnorePrefabPosition);
		AssetLoader.Get().InstantiatePrefab(ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.HISTORY_HERO_POWER, TAG_PREMIUM.GOLDEN), new PrefabCallback<GameObject>(this.LoadGoldenHeroPowerCallback), null, AssetLoadingOptions.IgnorePrefabPosition);
		if (UniversalInputManager.UsePhoneUI)
		{
			AssetLoader.Get().InstantiatePrefab("BackButton_phone.prefab:08de22f2aa1facd42812215422eba8c7", new PrefabCallback<GameObject>(this.OnPhoneBackButtonLoaded), null, AssetLoadingOptions.None);
		}
		this.m_draftManager.RegisterDisplayHandlers();
		SceneMgr.Get().RegisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
		string text = this.m_draftManager.GetSceneHeadlineText();
		if (string.IsNullOrEmpty(text))
		{
			text = GameStrings.Get("GLUE_TOOLTIP_BUTTON_FORGE_HEADLINE");
		}
		this.m_forgeLabel.Text = text;
		this.m_instructionText.Text = string.Empty;
		this.m_pickArea.enabled = false;
		if (DemoMgr.Get().ArenaIs1WinMode())
		{
			Options.Get().SetBool(Option.HAS_SEEN_FORGE_PLAY_MODE, false);
			Options.Get().SetBool(Option.HAS_SEEN_FORGE_CARD_CHOICE, false);
			Options.Get().SetBool(Option.HAS_SEEN_FORGE, true);
			Options.Get().SetBool(Option.HAS_SEEN_FORGE_HERO_CHOICE, true);
			Options.Get().SetBool(Option.HAS_SEEN_FORGE_CARD_CHOICE2, false);
		}
	}

	// Token: 0x0600240A RID: 9226 RVA: 0x000B3DD4 File Offset: 0x000B1FD4
	private void OnDestroy()
	{
		foreach (DefLoader.DisposableFullDef disposableFullDef in this.m_heroPowerDefs)
		{
			if (disposableFullDef != null)
			{
				disposableFullDef.Dispose();
			}
		}
		foreach (DefLoader.DisposableFullDef disposableFullDef2 in this.m_subClassHeroPowerDefs)
		{
			if (disposableFullDef2 != null)
			{
				disposableFullDef2.Dispose();
			}
		}
		this.FadeEffectsOut();
		DraftDisplay.s_instance = null;
	}

	// Token: 0x0600240B RID: 9227 RVA: 0x000B3E34 File Offset: 0x000B2034
	private void Start()
	{
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		NetCache.Get().RegisterScreenForge(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		this.SetupRetireButton();
		this.m_playButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.PlayButtonPress));
		this.m_manaCurve.GetComponent<PegUIElement>().AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.ManaCurveOver));
		this.m_manaCurve.GetComponent<PegUIElement>().AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.ManaCurveOut));
		this.m_playButton.SetText(GameStrings.Get("GLOBAL_PLAY"));
		this.ShowPhonePlayButton(false);
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.SetupBackButton();
		}
		Network.Get().RequestDraftChoicesAndContents();
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_Arena);
		base.StartCoroutine(this.NotifySceneLoadedWhenReady());
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_draftDeckTray.gameObject.SetActive(true);
		}
	}

	// Token: 0x0600240C RID: 9228 RVA: 0x00019DD3 File Offset: 0x00017FD3
	private void Update()
	{
		Network.Get().ProcessNetwork();
	}

	// Token: 0x0600240D RID: 9229 RVA: 0x000B3F32 File Offset: 0x000B2132
	public static DraftDisplay Get()
	{
		return DraftDisplay.s_instance;
	}

	// Token: 0x0600240E RID: 9230 RVA: 0x000B3F39 File Offset: 0x000B2139
	public void OnOpenRewardsComplete()
	{
		this.ExitDraftScene();
	}

	// Token: 0x0600240F RID: 9231 RVA: 0x000B3F41 File Offset: 0x000B2141
	public void OnApplicationPause(bool pauseStatus)
	{
		if (GameMgr.Get().IsFindingGame())
		{
			this.CancelFindGame();
		}
	}

	// Token: 0x06002410 RID: 9232 RVA: 0x000B3F58 File Offset: 0x000B2158
	public void Unload()
	{
		Box.Get().SetToIgnoreFullScreenEffects(false);
		if (this.m_confirmButton != null)
		{
			UnityEngine.Object.Destroy(this.m_confirmButton.gameObject);
		}
		if (this.m_heroPower != null)
		{
			this.m_heroPower.Destroy();
		}
		if (this.m_chosenHero != null)
		{
			this.m_chosenHero.Destroy();
		}
		foreach (Actor actor in this.m_subclassHeroClones)
		{
			if (actor != null)
			{
				actor.Destroy();
			}
		}
		this.m_subclassHeroClones.Clear();
		foreach (Actor actor2 in this.m_subclassHeroPowerActors)
		{
			if (actor2 != null)
			{
				actor2.Destroy();
			}
		}
		this.m_currentLabels.Clear();
		this.m_draftManager.UnregisterDisplayHandlers();
		this.m_draftManager = null;
		DraftInputManager.Get().Unload();
	}

	// Token: 0x06002411 RID: 9233 RVA: 0x000B406C File Offset: 0x000B226C
	public void AcceptNewChoices(List<NetCache.CardDefinition> choices)
	{
		this.DestroyOldChoices();
		this.UpdateInstructionText();
		base.StartCoroutine(this.WaitForAnimsToFinishAndThenDisplayNewChoices(choices));
	}

	// Token: 0x06002412 RID: 9234 RVA: 0x000B4088 File Offset: 0x000B2288
	public void OnChoiceSelected(int chosenIndex)
	{
		DraftDisplay.DraftChoice draftChoice = this.m_choices[chosenIndex - 1];
		Actor actor = draftChoice.m_actor;
		if (!actor.GetEntityDef().IsHeroSkin() && !actor.GetEntityDef().IsHeroPower())
		{
			this.AddCardToManaCurve(actor.GetEntityDef());
			this.m_draftDeckTray.GetCardsContent().UpdateCardList(draftChoice.m_cardID, true, actor, null);
		}
	}

	// Token: 0x06002413 RID: 9235 RVA: 0x000B40EA File Offset: 0x000B22EA
	private IEnumerator WaitForAnimsToFinishAndThenDisplayNewChoices(List<NetCache.CardDefinition> choices)
	{
		while (!this.m_animationsComplete)
		{
			yield return null;
		}
		while (this.m_isHeroAnimating)
		{
			yield return null;
		}
		this.m_choices.Clear();
		for (int i = 0; i < choices.Count; i++)
		{
			NetCache.CardDefinition cardDefinition = choices[i];
			DraftDisplay.DraftChoice item = new DraftDisplay.DraftChoice
			{
				m_cardID = cardDefinition.Name,
				m_premium = cardDefinition.Premium,
				m_actor = null
			};
			this.m_choices.Add(item);
		}
		if (this.m_draftManager.GetSlotType() != DraftSlotType.DRAFT_SLOT_HERO)
		{
			while (this.m_chosenHero == null)
			{
				yield return null;
			}
		}
		this.m_skipHeroEmotes = false;
		for (int j = 0; j < this.m_choices.Count; j++)
		{
			DraftDisplay.DraftChoice draftChoice = this.m_choices[j];
			DraftDisplay.ChoiceCallback choiceCallback = new DraftDisplay.ChoiceCallback();
			choiceCallback.choiceID = j + 1;
			choiceCallback.slot = this.m_draftManager.GetSlot();
			choiceCallback.premium = draftChoice.m_premium;
			DefLoader.Get().LoadFullDef(draftChoice.m_cardID, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnFullDefLoaded), choiceCallback, null);
		}
		yield break;
	}

	// Token: 0x06002414 RID: 9236 RVA: 0x000B4100 File Offset: 0x000B2300
	public void SetDraftMode(DraftDisplay.DraftMode mode)
	{
		bool flag = this.m_currentMode != mode;
		this.m_currentMode = mode;
		if (!flag)
		{
			return;
		}
		Log.Arena.Print("SetDraftMode - " + this.m_currentMode, Array.Empty<object>());
		base.StartCoroutine(this.InitializeDraftScreen());
	}

	// Token: 0x06002415 RID: 9237 RVA: 0x000B4154 File Offset: 0x000B2354
	public DraftDisplay.DraftMode GetDraftMode()
	{
		return this.m_currentMode;
	}

	// Token: 0x06002416 RID: 9238 RVA: 0x000B415C File Offset: 0x000B235C
	public void CancelFindGame()
	{
		GameMgr.Get().CancelFindGame();
		this.HandleGameStartupFailure();
	}

	// Token: 0x06002417 RID: 9239 RVA: 0x000B4170 File Offset: 0x000B2370
	public void ZoomHeroCard(Actor hero, bool isDraftingHeroPower)
	{
		SoundManager.Get().LoadAndPlay("tournament_screen_select_hero.prefab:2b9bdf587ac07084b8f7d5c4bce33ecf");
		this.m_isHeroAnimating = true;
		hero.SetUnlit();
		iTween.MoveTo(hero.gameObject, this.m_bigHeroBone.position, 0.25f);
		iTween.ScaleTo(hero.gameObject, this.m_bigHeroBone.localScale, 0.25f);
		SoundManager.Get().LoadAndPlay("forge_hero_portrait_plate_rises.prefab:bffebffeb579074418432f59870e854e");
		this.FadeEffectsIn();
		SceneUtils.SetLayer(hero.gameObject, GameLayer.IgnoreFullScreenEffects);
		UniversalInputManager.Get().SetGameDialogActive(true);
		this.m_confirmButton.gameObject.SetActive(true);
		this.m_confirmButton.m_button.GetComponent<PlayMakerFSM>().SendEvent("Birth");
		this.m_confirmButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnConfirmButtonClicked));
		this.m_heroClickCatcher.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCancelButtonClicked));
		this.m_heroClickCatcher.gameObject.SetActive(true);
		hero.TurnOffCollider();
		hero.SetActorState(ActorStateType.CARD_IDLE);
		if (isDraftingHeroPower)
		{
			Actor[] subclassHeroPowerActors = this.m_subclassHeroPowerActors;
			for (int i = 0; i < subclassHeroPowerActors.Length; i++)
			{
				subclassHeroPowerActors[i].Hide();
			}
		}
		if (isDraftingHeroPower || !this.m_draftManager.HasSlotType(DraftSlotType.DRAFT_SLOT_HERO_POWER))
		{
			base.StartCoroutine(this.ShowHeroPowerWhenDefIsLoaded(isDraftingHeroPower));
		}
	}

	// Token: 0x06002418 RID: 9240 RVA: 0x000B42C0 File Offset: 0x000B24C0
	public void OnHeroClicked(int heroChoice)
	{
		Actor actor = null;
		bool flag = false;
		if (this.m_draftManager.GetSlotType() == DraftSlotType.DRAFT_SLOT_HERO)
		{
			actor = this.m_choices[heroChoice - 1].m_actor;
		}
		else if (this.m_draftManager.GetSlotType() == DraftSlotType.DRAFT_SLOT_HERO_POWER)
		{
			flag = true;
			actor = this.m_subclassHeroClones[heroChoice - 1];
			Actor heroPower = this.m_heroPowerCardActors[heroChoice - 1];
			this.m_heroPower = heroPower;
			this.m_heroPower.Hide();
		}
		if (actor != null)
		{
			this.m_zoomedHero = actor.GetCollider().gameObject.GetComponent<DraftCardVisual>();
			this.ZoomHeroCard(actor, flag);
		}
		else
		{
			Log.Arena.PrintWarning("DraftDisplay.OnHeroClicked: ChosenHeroActor is null! HeroChoice={0}", new object[]
			{
				heroChoice
			});
		}
		bool flag2 = true;
		if (!flag)
		{
			flag2 = this.IsHeroEmoteSpellReady(heroChoice - 1);
			base.StartCoroutine(this.WaitForSpellToLoadAndPlay(heroChoice - 1));
		}
		if (this.CanAutoDraft() && flag2)
		{
			this.OnConfirmButtonClicked(null);
		}
	}

	// Token: 0x06002419 RID: 9241 RVA: 0x000B43A8 File Offset: 0x000B25A8
	private void MakeHeroPowerGoldenIfPremium(DefLoader.DisposableFullDef heroPowerDef)
	{
		EntityDef entityDef = heroPowerDef.EntityDef;
		string heroCardId = CollectionManager.GetHeroCardId(entityDef.GetClass(), CardHero.HeroType.VANILLA);
		TAG_PREMIUM bestCardPremium = CollectionManager.Get().GetBestCardPremium(heroCardId);
		this.m_heroPower = ((bestCardPremium == TAG_PREMIUM.GOLDEN) ? this.m_goldenHeroPowerSkin : this.m_defaultHeroPowerSkin);
		this.m_heroPower.SetCardDef(heroPowerDef.DisposableCardDef);
		this.m_heroPower.SetEntityDef(entityDef);
		this.m_heroPower.SetPremium(bestCardPremium);
		this.m_heroPower.UpdateAllComponents();
	}

	// Token: 0x0600241A RID: 9242 RVA: 0x000B4421 File Offset: 0x000B2621
	private bool IsHeroEmoteSpellReady(int index)
	{
		return this.m_heroEmotes[index] != null || this.m_skipHeroEmotes;
	}

	// Token: 0x0600241B RID: 9243 RVA: 0x000B443B File Offset: 0x000B263B
	private IEnumerator WaitForSpellToLoadAndPlay(int index)
	{
		bool wasEmoteAlreadyReady = this.IsHeroEmoteSpellReady(index);
		while (!this.IsHeroEmoteSpellReady(index))
		{
			yield return null;
		}
		if (!this.m_skipHeroEmotes)
		{
			this.m_heroEmotes[index].Reactivate();
		}
		if (this.CanAutoDraft() && !wasEmoteAlreadyReady)
		{
			this.OnConfirmButtonClicked(null);
		}
		yield break;
	}

	// Token: 0x0600241C RID: 9244 RVA: 0x000B4451 File Offset: 0x000B2651
	public void ClickConfirmButton()
	{
		this.OnConfirmButtonClicked(null);
	}

	// Token: 0x0600241D RID: 9245 RVA: 0x000B445C File Offset: 0x000B265C
	private void OnConfirmButtonClicked(UIEvent e)
	{
		if (GameUtils.IsAnyTransitionActive())
		{
			return;
		}
		this.EnableBackButton(false);
		this.m_choices.ForEach(delegate(DraftDisplay.DraftChoice choice)
		{
			choice.m_actor.TurnOffCollider();
		});
		if (this.m_draftManager.GetSlotType() == DraftSlotType.DRAFT_SLOT_HERO_POWER)
		{
			this.m_subclassHeroClones.ForEach(delegate(Actor choice)
			{
				choice.TurnOffCollider();
			});
		}
		this.DoHeroSelectAnimation();
	}

	// Token: 0x0600241E RID: 9246 RVA: 0x000B44E0 File Offset: 0x000B26E0
	private void OnCancelButtonClicked(UIEvent e)
	{
		if (this.IsInHeroSelectMode())
		{
			this.DoHeroCancelAnimation();
			return;
		}
		Navigation.GoBack();
	}

	// Token: 0x0600241F RID: 9247 RVA: 0x000B44F8 File Offset: 0x000B26F8
	private void RemoveListeners()
	{
		this.m_confirmButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnConfirmButtonClicked));
		this.m_confirmButton.m_button.GetComponent<PlayMakerFSM>().SendEvent("Death");
		this.m_confirmButton.gameObject.SetActive(false);
		this.m_heroClickCatcher.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCancelButtonClicked));
		this.m_heroClickCatcher.gameObject.SetActive(false);
	}

	// Token: 0x06002420 RID: 9248 RVA: 0x000B4573 File Offset: 0x000B2773
	private void FadeEffectsIn()
	{
		if (this.m_fxActive)
		{
			return;
		}
		this.m_fxActive = true;
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.SetBlurBrightness(1f);
		fullScreenFXMgr.SetBlurDesaturation(0f);
		fullScreenFXMgr.StartStandardBlurVignette(0.4f);
	}

	// Token: 0x06002421 RID: 9249 RVA: 0x000B45AA File Offset: 0x000B27AA
	private void FadeEffectsOut()
	{
		if (!this.m_fxActive)
		{
			return;
		}
		this.m_fxActive = false;
		FullScreenFXMgr.Get().EndStandardBlurVignette(0f, new Action(this.OnFadeFinished));
	}

	// Token: 0x06002422 RID: 9250 RVA: 0x000B45D7 File Offset: 0x000B27D7
	private void OnFadeFinished()
	{
		if (this.m_chosenHero == null)
		{
			return;
		}
		SceneUtils.SetLayer(this.m_chosenHero.gameObject, GameLayer.Default);
	}

	// Token: 0x06002423 RID: 9251 RVA: 0x000B45FC File Offset: 0x000B27FC
	public void DoHeroCancelAnimation()
	{
		this.RemoveListeners();
		this.m_heroPower.Hide();
		Actor actor;
		if (this.m_draftManager.GetSlotType() == DraftSlotType.DRAFT_SLOT_HERO)
		{
			actor = this.m_choices[this.m_zoomedHero.GetChoiceNum() - 1].m_actor;
		}
		else
		{
			actor = this.m_subclassHeroClones[this.m_zoomedHero.GetChoiceNum() - 1];
			foreach (Actor actor2 in this.m_subclassHeroPowerActors)
			{
				actor2.Show();
				Spell componentInChildren = actor2.GetComponentInChildren<Spell>();
				if (componentInChildren != null)
				{
					componentInChildren.Deactivate();
					componentInChildren.Activate();
				}
			}
		}
		SceneUtils.SetLayer(actor.gameObject, GameLayer.Default);
		actor.TurnOnCollider();
		this.FadeEffectsOut();
		UniversalInputManager.Get().SetGameDialogActive(false);
		this.m_isHeroAnimating = false;
		this.m_pickArea.enabled = true;
		iTween.MoveTo(actor.gameObject, this.GetCardPosition(this.m_zoomedHero.GetChoiceNum() - 1, true), 0.25f);
		if (UniversalInputManager.UsePhoneUI)
		{
			iTween.ScaleTo(actor.gameObject, DraftDisplay.HERO_ACTOR_LOCAL_SCALE_PHONE, 0.25f);
		}
		else
		{
			iTween.ScaleTo(actor.gameObject, DraftDisplay.HERO_ACTOR_LOCAL_SCALE, 0.25f);
		}
		this.m_pickArea.enabled = false;
		this.m_zoomedHero = null;
	}

	// Token: 0x06002424 RID: 9252 RVA: 0x000B473F File Offset: 0x000B293F
	public bool IsInHeroSelectMode()
	{
		return this.m_zoomedHero != null;
	}

	// Token: 0x06002425 RID: 9253 RVA: 0x000B4750 File Offset: 0x000B2950
	private void DoHeroSelectAnimation()
	{
		bool flag = this.m_draftManager.GetSlotType() == DraftSlotType.DRAFT_SLOT_HERO_POWER;
		this.RemoveListeners();
		this.m_heroPower.transform.parent = null;
		if (!flag)
		{
			this.m_heroPower.Hide();
		}
		this.FadeEffectsOut();
		UniversalInputManager.Get().SetGameDialogActive(false);
		this.m_chosenHero = (flag ? this.m_zoomedHero.GetSubActor() : this.m_zoomedHero.GetActor());
		this.m_zoomedHero.SetChosenFlag(true);
		this.m_draftManager.MakeChoice(this.m_zoomedHero.GetChoiceNum(), this.m_chosenHero.GetPremium());
		if (UniversalInputManager.UsePhoneUI)
		{
			Actor actor;
			if (!flag)
			{
				actor = this.m_zoomedHero.GetActor();
			}
			else
			{
				actor = this.m_zoomedHero.GetSubActor();
				this.m_inPlayHeroPowerActor = this.m_subclassHeroPowerActors[this.m_zoomedHero.GetChoiceNum() - 1];
				Actor actor2 = this.m_zoomedHero.GetActor();
				actor2.transform.parent = this.m_socketHeroPowerBone;
				iTween.MoveTo(actor2.gameObject, iTween.Hash(new object[]
				{
					"position",
					Vector3.zero,
					"time",
					0.25f,
					"isLocal",
					true,
					"easeType",
					iTween.EaseType.easeInCubic,
					"oncomplete",
					"PhoneHeroPowerAnimationFinished",
					"oncompletetarget",
					base.gameObject
				}));
				iTween.ScaleTo(actor2.gameObject, iTween.Hash(new object[]
				{
					"scale",
					Vector3.one,
					"time",
					0.25f,
					"easeType",
					iTween.EaseType.easeInCubic
				}));
			}
			actor.transform.parent = this.m_socketHeroBone;
			iTween.MoveTo(actor.gameObject, iTween.Hash(new object[]
			{
				"position",
				Vector3.zero,
				"time",
				0.25f,
				"isLocal",
				true,
				"easeType",
				iTween.EaseType.easeInCubic,
				"oncomplete",
				"PhoneHeroAnimationFinished",
				"oncompletetarget",
				base.gameObject
			}));
			iTween.ScaleTo(actor.gameObject, iTween.Hash(new object[]
			{
				"scale",
				Vector3.one,
				"time",
				0.25f,
				"easeType",
				iTween.EaseType.easeInCubic
			}));
		}
		else
		{
			this.m_zoomedHero.GetActor().ActivateSpellBirthState(SpellType.CONSTRUCT);
			this.m_zoomedHero = null;
			this.m_isHeroAnimating = false;
		}
		SoundManager.Get().LoadAndPlay("forge_hero_portrait_plate_descend_and_impact.prefab:371e56744a872fc45a4bb3c043e684aa");
		this.ShowInnkeeperInstructions();
	}

	// Token: 0x06002426 RID: 9254 RVA: 0x000B4A56 File Offset: 0x000B2C56
	private void PhoneHeroAnimationFinished()
	{
		Log.Arena.Print("Phone Hero animation complete", Array.Empty<object>());
		this.m_zoomedHero = null;
		this.m_isHeroAnimating = false;
	}

	// Token: 0x06002427 RID: 9255 RVA: 0x000B4A7C File Offset: 0x000B2C7C
	private void PhoneHeroPowerAnimationFinished()
	{
		Log.Arena.Print("Phone Hero Power animation complete", Array.Empty<object>());
		this.m_inPlayHeroPowerActor.transform.parent = this.m_socketHeroPowerBone;
		this.m_inPlayHeroPowerActor.transform.localPosition = Vector3.zero;
		this.m_inPlayHeroPowerActor.transform.localScale = Vector3.one;
		this.m_inPlayHeroPowerActor.Show();
	}

	// Token: 0x06002428 RID: 9256 RVA: 0x000B4AE8 File Offset: 0x000B2CE8
	public void AddCardToManaCurve(EntityDef entityDef)
	{
		if (this.m_manaCurve == null)
		{
			Debug.LogWarning(string.Format("DraftDisplay.AddCardToManaCurve({0}) - m_manaCurve is null", entityDef));
			return;
		}
		this.m_manaCurve.AddCardToManaCurve(entityDef);
	}

	// Token: 0x06002429 RID: 9257 RVA: 0x000B4B18 File Offset: 0x000B2D18
	public List<DraftCardVisual> GetCardVisuals()
	{
		List<DraftCardVisual> list = new List<DraftCardVisual>();
		foreach (DraftDisplay.DraftChoice draftChoice in this.m_choices)
		{
			if (draftChoice.m_actor == null)
			{
				return null;
			}
			DraftCardVisual component = draftChoice.m_actor.GetCollider().gameObject.GetComponent<DraftCardVisual>();
			if (component != null)
			{
				list.Add(component);
			}
			else
			{
				if (draftChoice.m_subActor == null)
				{
					return null;
				}
				component = draftChoice.m_subActor.GetCollider().gameObject.GetComponent<DraftCardVisual>();
				if (!(component != null))
				{
					return null;
				}
				list.Add(component);
			}
		}
		return list;
	}

	// Token: 0x0600242A RID: 9258 RVA: 0x000B4BF4 File Offset: 0x000B2DF4
	public void HandleGameStartupFailure()
	{
		this.m_playButton.Enable();
		this.ShowPhonePlayButton(true);
		if (PresenceMgr.Get().CurrentStatus == Global.PresenceStatus.ARENA_QUEUE)
		{
			PresenceMgr.Get().SetPrevStatus();
		}
	}

	// Token: 0x0600242B RID: 9259 RVA: 0x000B4C24 File Offset: 0x000B2E24
	public void DoDeckCompleteAnims()
	{
		SoundManager.Get().LoadAndPlay("forge_commit_deck.prefab:1e3ef554bb2848b48816f336f2f91569");
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.m_DeckCompleteSpell.Activate();
			if (this.m_draftDeckTray != null)
			{
				this.m_draftDeckTray.GetCardsContent().ShowDeckCompleteEffects();
			}
		}
	}

	// Token: 0x0600242C RID: 9260 RVA: 0x000B4C7A File Offset: 0x000B2E7A
	private IEnumerator NotifySceneLoadedWhenReady()
	{
		while (this.m_confirmButton == null)
		{
			yield return null;
		}
		while (this.m_heroPower == null)
		{
			yield return null;
		}
		while (this.m_currentMode == DraftDisplay.DraftMode.INVALID)
		{
			yield return null;
		}
		while (!this.m_netCacheReady)
		{
			yield return null;
		}
		while (!AchieveManager.Get().IsReady())
		{
			yield return null;
		}
		this.InitManaCurve();
		this.m_draftDeckTray.Initialize();
		PegUIElement component = this.m_draftDeckTray.GetTooltipZone().gameObject.GetComponent<PegUIElement>();
		component.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.DeckHeaderOver));
		component.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.DeckHeaderOut));
		SceneMgr.Get().NotifySceneLoaded();
		yield break;
	}

	// Token: 0x0600242D RID: 9261 RVA: 0x000B4C89 File Offset: 0x000B2E89
	private IEnumerator InitializeDraftScreen()
	{
		while (!ArenaTrayDisplay.Get().IsReady())
		{
			yield return null;
		}
		if (!this.m_firstTimeIntroComplete && !Options.Get().GetBool(Option.HAS_SEEN_FORGE, false) && UserAttentionManager.CanShowAttentionGrabber("DraftDisplay.InitializeDraftScreen:" + Option.HAS_SEEN_FORGE))
		{
			while (SceneMgr.Get().IsTransitioning())
			{
				yield return null;
			}
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.ARENA_PURCHASE
			});
			this.m_firstTimeIntroComplete = true;
			this.DoFirstTimeIntro();
			yield break;
		}
		switch (this.m_currentMode)
		{
		case DraftDisplay.DraftMode.NO_ACTIVE_DRAFT:
		{
			while (SceneMgr.Get().IsTransitioning())
			{
				yield return null;
			}
			int numTicketsOwned = this.m_draftManager.GetNumTicketsOwned();
			if (StoreManager.Get().HasOutstandingPurchaseNotices(ProductType.PRODUCT_TYPE_DRAFT))
			{
				this.ShowPurchaseScreen();
			}
			else if (numTicketsOwned > 0)
			{
				this.ShowOutstandingTicketScreen(numTicketsOwned);
			}
			else
			{
				PresenceMgr.Get().SetStatus(new Enum[]
				{
					Global.PresenceStatus.ARENA_PURCHASE
				});
				this.ShowPurchaseScreen();
			}
			break;
		}
		case DraftDisplay.DraftMode.DRAFTING:
			if (StoreManager.Get().HasOutstandingPurchaseNotices(ProductType.PRODUCT_TYPE_DRAFT))
			{
				while (SceneMgr.Get().IsTransitioning())
				{
					yield return null;
				}
				this.ShowPurchaseScreen();
			}
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.ARENA_FORGE
			});
			if (this.m_draftManager.ShouldShowFreeArenaWinScreen())
			{
				this.ShowFreeArenaWinScreen();
			}
			else
			{
				this.ShowCurrentlyDraftingScreen();
			}
			break;
		case DraftDisplay.DraftMode.ACTIVE_DRAFT_DECK:
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.ARENA_IDLE
			});
			base.StartCoroutine(this.ShowActiveDraftScreen());
			break;
		case DraftDisplay.DraftMode.IN_REWARDS:
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.ARENA_REWARD
			});
			this.ShowDraftRewardsScreen();
			break;
		default:
			Debug.LogError(string.Format("DraftDisplay.InitializeDraftScreen(): don't know how to handle m_currentMode = {0}", this.m_currentMode));
			break;
		}
		yield break;
	}

	// Token: 0x0600242E RID: 9262 RVA: 0x000B4C98 File Offset: 0x000B2E98
	private void OnConfirmButtonLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_confirmButton = go.GetComponent<NormalButton>();
		this.m_confirmButton.SetText(GameStrings.Get("GLUE_CHOOSE"));
		this.m_confirmButton.gameObject.SetActive(false);
		SceneUtils.SetLayer(go, GameLayer.IgnoreFullScreenEffects);
	}

	// Token: 0x0600242F RID: 9263 RVA: 0x000B4CD4 File Offset: 0x000B2ED4
	private void OnPhoneBackButtonLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError("Phone Back Button failed to load!");
			return;
		}
		go.transform.SetParent(base.transform, true);
		this.m_backButton = go.GetComponent<UIBButton>();
		this.m_backButton.transform.parent = this.m_PhoneBackButtonBone;
		this.m_backButton.transform.position = this.m_PhoneBackButtonBone.position;
		this.m_backButton.transform.localScale = this.m_PhoneBackButtonBone.localScale;
		this.m_backButton.transform.rotation = Quaternion.identity;
		SceneUtils.SetLayer(go, GameLayer.Default);
		this.SetupBackButton();
	}

	// Token: 0x06002430 RID: 9264 RVA: 0x000B4D84 File Offset: 0x000B2F84
	private void OnHeroPowerActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning(string.Format("DeckPickerTrayDisplay.OnHeroPowerActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			return;
		}
		go.transform.SetParent(base.transform, true);
		this.m_inPlayHeroPowerActor = go.GetComponent<Actor>();
		if (this.m_inPlayHeroPowerActor == null)
		{
			Debug.LogWarning(string.Format("DeckPickerTrayDisplay.OnHeroPowerActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef));
			return;
		}
		this.m_inPlayHeroPowerActor.SetUnlit();
		this.m_inPlayHeroPowerActor.Hide();
	}

	// Token: 0x06002431 RID: 9265 RVA: 0x000B4E00 File Offset: 0x000B3000
	private void LoadHeroPowerCallback(Actor actor)
	{
		if (actor == null)
		{
			Debug.LogWarning("DeckPickerTrayDisplay.LoadHeroPowerCallback() - ERROR actor null.");
			return;
		}
		actor.transform.SetParent(base.transform, true);
		actor.TurnOffCollider();
		SceneUtils.SetLayer(actor.gameObject, GameLayer.IgnoreFullScreenEffects);
		this.m_heroPower = actor;
		actor.Hide();
	}

	// Token: 0x06002432 RID: 9266 RVA: 0x000B4E54 File Offset: 0x000B3054
	private void LoadHeroPowerCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning(string.Format("DeckPickerTrayDisplay.LoadHeroPowerCallback() - FAILED to load actor \"{0}\"", assetRef));
			return;
		}
		go.transform.SetParent(base.transform, true);
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarning(string.Format("DeckPickerTrayDisplay.LoadHeroPowerCallback() - ERROR actor \"{0}\" has no Actor component", assetRef));
			return;
		}
		component.TurnOffCollider();
		SceneUtils.SetLayer(component.gameObject, GameLayer.IgnoreFullScreenEffects);
		this.m_defaultHeroPowerSkin = component;
		this.m_heroPower = component;
		component.Hide();
	}

	// Token: 0x06002433 RID: 9267 RVA: 0x000B4ED5 File Offset: 0x000B30D5
	private void LoadGoldenHeroPowerCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.transform.SetParent(base.transform, true);
		this.m_goldenHeroPowerSkin = go.GetComponent<Actor>();
	}

	// Token: 0x06002434 RID: 9268 RVA: 0x000B4EF8 File Offset: 0x000B30F8
	private void ShowHeroPowerBigCard(bool isDraftingHeroPower)
	{
		if (this.m_heroPower == null)
		{
			return;
		}
		SceneUtils.SetLayer(this.m_heroPower.gameObject, GameLayer.IgnoreFullScreenEffects);
		Actor actor = this.m_zoomedHero.GetSubActor();
		if (actor == null)
		{
			actor = this.m_zoomedHero.GetActor();
		}
		this.m_heroPower.gameObject.transform.SetParent(actor.transform);
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_heroPower.gameObject.transform.localPosition = DraftDisplay.HERO_POWER_START_POSITION_PHONE;
			this.m_heroPower.gameObject.transform.localScale = DraftDisplay.HERO_POWER_SCALE_PHONE;
			return;
		}
		if (!isDraftingHeroPower)
		{
			this.m_heroPower.gameObject.transform.localPosition = DraftDisplay.HERO_POWER_START_POSITION;
			this.m_heroPower.gameObject.transform.localScale = DraftDisplay.HERO_POWER_SCALE;
			return;
		}
		this.m_heroPower.gameObject.transform.localPosition = DraftDisplay.HERO_POWER_START_POSITION;
		this.m_heroPower.gameObject.transform.localScale = DraftDisplay.DRAFTING_HERO_POWER_BIG_CARD_SCALE;
	}

	// Token: 0x06002435 RID: 9269 RVA: 0x000B5010 File Offset: 0x000B3210
	private void ShowHeroPower(Actor actor)
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_heroPower.gameObject.transform.localPosition = DraftDisplay.HERO_POWER_TOOLTIP_POSITION_PHONE;
			this.m_heroPower.gameObject.transform.localScale = DraftDisplay.HERO_POWER_TOOLTIP_SCALE_PHONE;
		}
		else
		{
			this.m_heroPower.gameObject.transform.localPosition = DraftDisplay.HERO_POWER_TOOLTIP_POSITION;
			this.m_heroPower.gameObject.transform.localScale = DraftDisplay.HERO_POWER_TOOLTIP_SCALE;
		}
		this.m_heroPower.SetFullDefFromActor(actor);
		this.m_heroPower.UpdateAllComponents();
		this.m_heroPower.Show();
	}

	// Token: 0x06002436 RID: 9270 RVA: 0x000B50B5 File Offset: 0x000B32B5
	private IEnumerator ShowHeroPowerWhenDefIsLoaded(bool isDraftingHeroPower = false)
	{
		if (this.m_zoomedHero == null)
		{
			yield break;
		}
		if (!isDraftingHeroPower)
		{
			while (this.m_heroPowerDefs[this.m_zoomedHero.GetChoiceNum() - 1] == null)
			{
				yield return null;
			}
			DefLoader.DisposableFullDef disposableFullDef = this.m_heroPowerDefs[this.m_zoomedHero.GetChoiceNum() - 1];
			this.MakeHeroPowerGoldenIfPremium(disposableFullDef);
			if (this.m_zoomedHero.GetActor().GetEntityDef().GetCardSet() == TAG_CARD_SET.HERO_SKINS)
			{
				disposableFullDef.CardDef.m_AlwaysRenderPremiumPortrait = true;
			}
		}
		this.m_heroPower.Show();
		this.ShowHeroPowerBigCard(isDraftingHeroPower);
		if (UniversalInputManager.UsePhoneUI)
		{
			iTween.MoveTo(this.m_heroPower.gameObject, iTween.Hash(new object[]
			{
				"position",
				DraftDisplay.HERO_POWER_POSITION_PHONE,
				"isLocal",
				true,
				"time",
				0.5f
			}));
		}
		else if (!isDraftingHeroPower)
		{
			iTween.MoveTo(this.m_heroPower.gameObject, iTween.Hash(new object[]
			{
				"position",
				DraftDisplay.HERO_POWER_POSITION,
				"isLocal",
				true,
				"time",
				0.5f
			}));
		}
		else
		{
			iTween.MoveTo(this.m_heroPower.gameObject, iTween.Hash(new object[]
			{
				"position",
				DraftDisplay.DRAFTING_HERO_POWER_POSITION,
				"isLocal",
				true,
				"time",
				0.5f
			}));
		}
		yield break;
	}

	// Token: 0x06002437 RID: 9271 RVA: 0x000B50CB File Offset: 0x000B32CB
	private IEnumerator WaitAndPositionHeroPower()
	{
		yield return new WaitForSeconds(0.35f);
		this.m_inPlayHeroPowerActor = this.m_subclassHeroPowerActors[this.m_draftManager.ChosenIndex - 1];
		this.m_inPlayHeroPowerActor.transform.localPosition = this.m_socketHeroPowerBone.transform.localPosition;
		this.m_inPlayHeroPowerActor.transform.localScale = this.m_socketHeroPowerBone.transform.localScale;
		this.SetupToDisplayHeroPowerTooltip(this.m_inPlayHeroPowerActor);
		Spell componentInChildren = this.m_inPlayHeroPowerActor.GetComponentInChildren<Spell>();
		if (componentInChildren != null)
		{
			componentInChildren.Activate();
		}
		this.m_inPlayHeroPowerActor.Show();
		DraftCardVisual componentInChildren2 = this.m_inPlayHeroPowerActor.GetComponentInChildren<DraftCardVisual>();
		if (componentInChildren2 != null)
		{
			UnityEngine.Object.Destroy(componentInChildren2);
		}
		yield break;
	}

	// Token: 0x06002438 RID: 9272 RVA: 0x000B50DC File Offset: 0x000B32DC
	private void DestroyOldChoices()
	{
		this.m_animationsComplete = false;
		for (int i = 1; i < this.m_choices.Count + 1; i++)
		{
			DraftDisplay.DraftChoice draftChoice = this.m_choices[i - 1];
			Actor actor = draftChoice.m_actor;
			if (!(actor == null))
			{
				Actor subActor = draftChoice.m_subActor;
				actor.TurnOffCollider();
				Spell spell = actor.GetSpell(DraftDisplay.GetSpellTypeForRarity(actor.GetEntityDef().GetRarity()));
				if (i == this.m_draftManager.ChosenIndex)
				{
					if (actor.GetEntityDef().IsHeroSkin())
					{
						using (List<HeroLabel>.Enumerator enumerator = this.m_currentLabels.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								HeroLabel heroLabel = enumerator.Current;
								heroLabel.FadeOut();
							}
							goto IL_282;
						}
					}
					if (actor.GetEntityDef().IsHeroPower())
					{
						actor.transform.parent = null;
						SceneUtils.SetLayer(actor.gameObject, GameLayer.IgnoreFullScreenEffects);
						if (!UniversalInputManager.UsePhoneUI)
						{
							this.m_heroPower = actor.Clone();
							this.m_heroPower.Hide();
							Spell componentInChildren = actor.GetComponentInChildren<Spell>();
							componentInChildren.AddFinishedCallback(new Spell.FinishedCallback(this.CleanupChoicesOnSpellFinish_HeroPower), actor);
							actor.Show();
							componentInChildren.Activate();
							base.StartCoroutine(this.WaitAndPositionHeroPower());
						}
						else
						{
							Actor[] subclassHeroPowerActors = this.m_subclassHeroPowerActors;
							for (int j = 0; j < subclassHeroPowerActors.Length; j++)
							{
								subclassHeroPowerActors[j].Hide();
							}
							this.SetupToDisplayHeroPowerTooltip(this.m_inPlayHeroPowerActor);
							this.m_heroPower.Hide();
						}
						using (List<HeroLabel>.Enumerator enumerator = this.m_currentLabels.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								HeroLabel heroLabel2 = enumerator.Current;
								if (heroLabel2 != null)
								{
									heroLabel2.FadeOut();
								}
							}
							goto IL_282;
						}
					}
					actor.GetSpell(SpellType.SUMMON_OUT_FORGE).AddFinishedCallback(new Spell.FinishedCallback(this.DestroyChoiceOnSpellFinish), actor);
					actor.ActivateSpellBirthState(SpellType.SUMMON_OUT_FORGE);
					spell.ActivateState(SpellStateType.DEATH);
					SoundManager.Get().LoadAndPlay("forge_select_card_1.prefab:b770cd64bb913f0409902629f975421e");
				}
				else
				{
					SoundManager.Get().LoadAndPlay("unselected_cards_dissipate.prefab:a68b6959b8e9ed4408bf2475f37fd97d");
					Spell spell2 = actor.GetSpell(SpellType.BURN);
					if (spell2 != null)
					{
						spell2.AddFinishedCallback(new Spell.FinishedCallback(this.DestroyChoiceOnSpellFinish), actor);
						actor.ActivateSpellBirthState(SpellType.BURN);
					}
					spell2 = ((subActor == null) ? null : subActor.GetSpell(SpellType.BURN));
					if (spell2 != null)
					{
						spell2.AddFinishedCallback(new Spell.FinishedCallback(this.DestroyChoiceOnSpellFinish), subActor);
						subActor.ActivateSpellBirthState(SpellType.BURN);
					}
					if (spell != null)
					{
						spell.ActivateState(SpellStateType.DEATH);
					}
				}
			}
			IL_282:;
		}
		base.StartCoroutine(this.CompleteAnims());
		this.m_inPositionAndShowChoices = false;
	}

	// Token: 0x06002439 RID: 9273 RVA: 0x000B53B4 File Offset: 0x000B35B4
	private void SetupToDisplayHeroPowerTooltip(Actor actor)
	{
		if (actor == null)
		{
			Log.Arena.PrintWarning("DraftDisplay.SetupToDisplayHeroPowerTooltip: Actor is null!", Array.Empty<object>());
			return;
		}
		PegUIElement pegUIElement = actor.gameObject.GetComponent<PegUIElement>();
		if (pegUIElement == null)
		{
			pegUIElement = actor.gameObject.AddComponent<PegUIElement>();
			pegUIElement.gameObject.AddComponent<BoxCollider>();
		}
		pegUIElement.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnMouseOverHeroPower));
		pegUIElement.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnMouseOutHeroPower));
		actor.Show();
	}

	// Token: 0x0600243A RID: 9274 RVA: 0x000B543A File Offset: 0x000B363A
	private IEnumerator CompleteAnims()
	{
		yield return new WaitForSeconds(0.5f);
		this.m_animationsComplete = true;
		yield break;
	}

	// Token: 0x0600243B RID: 9275 RVA: 0x000B544C File Offset: 0x000B364C
	private void CleanupChoicesOnSpellFinish_HeroPower(Spell spell, object actorObject)
	{
		foreach (Actor actor in this.m_subclassHeroClones)
		{
			actor.Hide();
		}
		foreach (Actor actor2 in this.m_subclassHeroPowerActors)
		{
			if (actor2 != this.m_inPlayHeroPowerActor)
			{
				actor2.Hide();
			}
		}
		this.DestroyChoiceOnSpellFinish(spell, actorObject);
	}

	// Token: 0x0600243C RID: 9276 RVA: 0x000B54D4 File Offset: 0x000B36D4
	private void DestroyChoiceOnSpellFinish(Spell spell, object actorObject)
	{
		Actor actor = (Actor)actorObject;
		base.StartCoroutine(this.DestroyObjectAfterDelay(actor.gameObject));
	}

	// Token: 0x0600243D RID: 9277 RVA: 0x000B54FB File Offset: 0x000B36FB
	private IEnumerator DestroyObjectAfterDelay(GameObject gameObjectToDestroy)
	{
		yield return new WaitForSeconds(5f);
		UnityEngine.Object.Destroy(gameObjectToDestroy);
		yield break;
	}

	// Token: 0x0600243E RID: 9278 RVA: 0x000B550C File Offset: 0x000B370C
	private void OnFullDefLoaded(string cardID, DefLoader.DisposableFullDef def, object userData)
	{
		try
		{
			if (def == null)
			{
				Debug.LogErrorFormat("Unable to load FullDef for cardID={0}", new object[]
				{
					cardID
				});
			}
			else
			{
				DraftDisplay.ChoiceCallback choiceCallback = (DraftDisplay.ChoiceCallback)userData;
				choiceCallback.fullDef = def;
				if (def.EntityDef.IsHeroSkin())
				{
					AssetLoader.Get().InstantiatePrefab(ActorNames.GetZoneActor(def.EntityDef, TAG_ZONE.PLAY), new PrefabCallback<GameObject>(this.OnActorLoaded), choiceCallback.Copy(), AssetLoadingOptions.IgnorePrefabPosition);
					DefLoader.Get().LoadCardDef(def.EntityDef.GetCardId(), new DefLoader.LoadDefCallback<DefLoader.DisposableCardDef>(this.OnCardDefLoaded), choiceCallback.choiceID, null);
					string heroPowerCardIdFromHero = GameUtils.GetHeroPowerCardIdFromHero(def.EntityDef.GetCardId());
					DefLoader.Get().LoadFullDef(heroPowerCardIdFromHero, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnHeroPowerFullDefLoaded), choiceCallback.choiceID, null);
				}
				else if (def.EntityDef.IsHeroPower())
				{
					AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(def.EntityDef, choiceCallback.premium), new PrefabCallback<GameObject>(this.OnActorLoaded), choiceCallback.Copy(), AssetLoadingOptions.IgnorePrefabPosition);
					AssetLoader.Get().InstantiatePrefab(ActorNames.GetZoneActor(def.EntityDef, TAG_ZONE.PLAY, choiceCallback.premium), new PrefabCallback<GameObject>(this.OnSubClassActorLoaded), choiceCallback.Copy(), AssetLoadingOptions.IgnorePrefabPosition);
				}
				else
				{
					AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(def.EntityDef, choiceCallback.premium), new PrefabCallback<GameObject>(this.OnActorLoaded), choiceCallback.Copy(), AssetLoadingOptions.IgnorePrefabPosition);
				}
			}
		}
		finally
		{
			if (def != null)
			{
				((IDisposable)def).Dispose();
			}
		}
	}

	// Token: 0x0600243F RID: 9279 RVA: 0x000B56C0 File Offset: 0x000B38C0
	private void OnHeroPowerFullDefLoaded(string cardID, DefLoader.DisposableFullDef def, object userData)
	{
		int num = (int)userData;
		DefLoader.DisposableFullDef disposableFullDef = this.m_heroPowerDefs[num - 1];
		if (disposableFullDef != null)
		{
			disposableFullDef.Dispose();
		}
		this.m_heroPowerDefs[num - 1] = def;
	}

	// Token: 0x06002440 RID: 9280 RVA: 0x000B56F4 File Offset: 0x000B38F4
	public void ShowInnkeeperInstructions()
	{
		if (this.m_draftManager.GetSlotType() == DraftSlotType.DRAFT_SLOT_HERO && !Options.Get().GetBool(Option.HAS_SEEN_FORGE_HERO_CHOICE, false) && UserAttentionManager.CanShowAttentionGrabber("DraftDisplay.UpdateInstructionText:" + Option.HAS_SEEN_FORGE_HERO_CHOICE))
		{
			if (!this.m_draftManager.HasSlotType(DraftSlotType.DRAFT_SLOT_HERO_POWER))
			{
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_FORGE_INST1_19"), "VO_INNKEEPER_FORGE_INST1_19.prefab:a0e06e90b545b274290dad8e442e83d0", 3f, null, false);
				Options.Get().SetBool(Option.HAS_SEEN_FORGE_HERO_CHOICE, true);
			}
			return;
		}
		if (this.m_draftManager.GetSlotType() == DraftSlotType.DRAFT_SLOT_CARD && !Options.Get().GetBool(Option.HAS_SEEN_FORGE_CARD_CHOICE, false) && UserAttentionManager.CanShowAttentionGrabber("DraftDisplay.DoHeroSelectAnimation:" + Option.HAS_SEEN_FORGE_CARD_CHOICE))
		{
			Options.Get().SetBool(Option.HAS_SEEN_FORGE_HERO_CHOICE, true);
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_FORGE_INST2_20"), "VO_INNKEEPER_FORGE_INST2_20.prefab:242b6a30031534e47b1f8ddd69370eac", 3f, null, false);
			Options.Get().SetBool(Option.HAS_SEEN_FORGE_CARD_CHOICE, true);
			return;
		}
		if (this.m_draftManager.GetSlotType() == DraftSlotType.DRAFT_SLOT_CARD && !Options.Get().GetBool(Option.HAS_SEEN_FORGE_CARD_CHOICE2, false) && UserAttentionManager.CanShowAttentionGrabber("DraftDisplay.UpdateInstructionText:" + Option.HAS_SEEN_FORGE_CARD_CHOICE2))
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_FORGE_INST3_21"), "VO_INNKEEPER_FORGE_INST3_21.prefab:06182dd3360965d4ea48952a6dd4a720", 3f, null, false);
			Options.Get().SetBool(Option.HAS_SEEN_FORGE_CARD_CHOICE2, true);
			return;
		}
	}

	// Token: 0x06002441 RID: 9281 RVA: 0x000B586C File Offset: 0x000B3A6C
	public void SetInstructionText()
	{
		switch (this.m_draftManager.GetSlotType())
		{
		case DraftSlotType.DRAFT_SLOT_CARD:
			this.m_instructionText.Text = GameStrings.Get("GLUE_DRAFT_INSTRUCTIONS");
			this.m_instructionDetailText.Text = "";
			return;
		case DraftSlotType.DRAFT_SLOT_HERO:
			this.m_instructionText.Text = GameStrings.Get("GLUE_DRAFT_HERO_INSTRUCTIONS");
			this.m_instructionDetailText.Text = "";
			return;
		case DraftSlotType.DRAFT_SLOT_HERO_POWER:
			this.m_instructionText.Text = GameStrings.Get("GLUE_DRAFT_HERO_POWER_INSTRUCTIONS_TITLE");
			this.m_instructionDetailText.Text = GameStrings.Get("GLUE_DRAFT_HERO_POWER_INSTRUCTIONS_DETAIL");
			return;
		default:
			this.m_instructionText.Text = GameStrings.Get("GLUE_DRAFT_INSTRUCTIONS");
			this.m_instructionDetailText.Text = "";
			return;
		}
	}

	// Token: 0x06002442 RID: 9282 RVA: 0x000B5938 File Offset: 0x000B3B38
	private void UpdateInstructionText()
	{
		if (this.GetDraftMode() == DraftDisplay.DraftMode.DRAFTING)
		{
			this.ShowInnkeeperInstructions();
			if (!UniversalInputManager.UsePhoneUI)
			{
				this.SetInstructionText();
				return;
			}
			DraftSlotType slotType = this.m_draftManager.GetSlotType();
			if (slotType == DraftSlotType.DRAFT_SLOT_HERO)
			{
				this.m_PhoneDeckControl.SetMode(ArenaPhoneControl.ControlMode.ChooseHero);
				return;
			}
			if (slotType == DraftSlotType.DRAFT_SLOT_HERO_POWER)
			{
				this.m_PhoneDeckControl.SetMode(ArenaPhoneControl.ControlMode.ChooseHeroPower);
				return;
			}
			if (this.m_draftManager.GetDraftDeck().GetTotalCardCount() > 0)
			{
				this.m_PhoneDeckControl.SetMode(ArenaPhoneControl.ControlMode.CardCountViewDeck);
				return;
			}
			this.m_PhoneDeckControl.SetMode(ArenaPhoneControl.ControlMode.ChooseCard);
			return;
		}
		else
		{
			if (this.GetDraftMode() != DraftDisplay.DraftMode.ACTIVE_DRAFT_DECK)
			{
				this.m_instructionText.Text = "";
				return;
			}
			if (UniversalInputManager.UsePhoneUI)
			{
				this.m_PhoneDeckControl.SetMode(ArenaPhoneControl.ControlMode.ViewDeck);
				return;
			}
			this.m_instructionText.Text = GameStrings.Get("GLUE_DRAFT_MATCH_PROG");
			return;
		}
	}

	// Token: 0x06002443 RID: 9283 RVA: 0x000B5A0C File Offset: 0x000B3C0C
	private void DoFirstTimeIntro()
	{
		Box.Get().SetToIgnoreFullScreenEffects(true);
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.m_playButton.Disable(false);
		}
		this.ShowPhonePlayButton(false);
		this.m_retireButton.Disable();
		if (this.m_manaCurve)
		{
			this.m_manaCurve.ResetBars();
		}
		if (!UniversalInputManager.UsePhoneUI)
		{
			StoreManager.Get().StartArenaTransaction(new Store.ExitCallback(this.OnStoreBackButtonPressed), null, true);
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_ARENA_1ST_TIME_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_ARENA_1ST_TIME_DESC");
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnFirstTimeIntroOkButtonPressed);
		popupInfo.m_id = "arena_first_time";
		DialogManager.Get().ShowPopup(popupInfo, delegate(DialogBase dialog, object userData)
		{
			this.m_firstTimeDialog = dialog;
			return true;
		});
		SoundManager.Get().LoadAndPlay("VO_INNKEEPER_ARENA_INTRO2.prefab:40f8c705d6df66445937a3ded7460725");
	}

	// Token: 0x06002444 RID: 9284 RVA: 0x000B5B06 File Offset: 0x000B3D06
	private void OnFirstTimeIntroOkButtonPressed(AlertPopup.Response response, object userData)
	{
		StoreManager.Get().HideStore(ShopType.ARENA_STORE);
		this.m_draftManager.RequestDraftBegin();
		Options.Get().SetBool(Option.HAS_SEEN_FORGE, true);
	}

	// Token: 0x06002445 RID: 9285 RVA: 0x000B5B30 File Offset: 0x000B3D30
	private void ShowFreeArenaWinScreen()
	{
		Box.Get().SetToIgnoreFullScreenEffects(true);
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.m_playButton.Disable(false);
		}
		this.ShowPhonePlayButton(false);
		this.m_retireButton.Disable();
		if (this.m_manaCurve)
		{
			this.m_manaCurve.ResetBars();
		}
		StoreManager.Get().HideStore(ShopType.ARENA_STORE);
		FreeArenaWinDialog.Info info = new FreeArenaWinDialog.Info();
		info.m_callbackOnHide = new DialogBase.HideCallback(this.OnFreeArenaWinOkButtonPress);
		info.m_winCount = this.m_draftManager.GetWins();
		DialogManager.Get().ShowFreeArenaWinPopup(UserAttentionBlocker.NONE, info);
	}

	// Token: 0x06002446 RID: 9286 RVA: 0x000B5BCC File Offset: 0x000B3DCC
	private void ShowOutstandingTicketScreen(int numTicketsOwned)
	{
		Box.Get().SetToIgnoreFullScreenEffects(true);
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.m_playButton.Disable(false);
		}
		this.ShowPhonePlayButton(false);
		this.m_retireButton.Disable();
		if (this.m_manaCurve)
		{
			this.m_manaCurve.ResetBars();
		}
		OutstandingDraftTicketDialog.Info info = new OutstandingDraftTicketDialog.Info();
		info.m_callbackOnEnter = new Action(this.OnOutstandingTicketEnterButtonPress);
		info.m_callbackOnCancel = new Action(this.OnOutstandingTicketCancelButtonPress);
		info.m_outstandingTicketCount = numTicketsOwned;
		DialogManager.Get().ShowOutstandingDraftTicketPopup(UserAttentionBlocker.NONE, info);
	}

	// Token: 0x06002447 RID: 9287 RVA: 0x000B5C64 File Offset: 0x000B3E64
	private void ShowPurchaseScreen()
	{
		Box.Get().SetToIgnoreFullScreenEffects(true);
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.m_playButton.Disable(false);
		}
		this.ShowPhonePlayButton(false);
		this.m_retireButton.Disable();
		if (this.m_manaCurve)
		{
			this.m_manaCurve.ResetBars();
		}
		if (DemoMgr.Get().ArenaIs1WinMode())
		{
			Network.Get().PurchaseViaGold(1, ProductType.PRODUCT_TYPE_DRAFT, 0);
			return;
		}
		StoreManager.Get().StartArenaTransaction(new Store.ExitCallback(this.OnStoreBackButtonPressed), null, false);
	}

	// Token: 0x06002448 RID: 9288 RVA: 0x000B5CF0 File Offset: 0x000B3EF0
	private void ShowCurrentlyDraftingScreen()
	{
		this.m_wasDrafting = true;
		ArenaTrayDisplay.Get().ShowPlainPaperBackground();
		StoreManager.Get().HideStore(ShopType.ARENA_STORE);
		this.UpdateInstructionText();
		this.m_retireButton.Disable();
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.m_playButton.Disable(false);
		}
		this.ShowPhonePlayButton(false);
		this.LoadAndPositionHeroCard();
		NarrativeManager.Get().OnArenaDraftStarted();
	}

	// Token: 0x06002449 RID: 9289 RVA: 0x000B5D5A File Offset: 0x000B3F5A
	private IEnumerator ShowActiveDraftScreen()
	{
		StoreManager.Get().HideStore(ShopType.ARENA_STORE);
		int losses = this.m_draftManager.GetLosses();
		this.DestroyOldChoices();
		this.m_retireButton.Enable();
		this.m_playButton.Enable();
		this.ShowPhonePlayButton(true);
		this.UpdateInstructionText();
		this.LoadAndPositionHeroCard();
		if (this.m_wasDrafting)
		{
			yield return new WaitForSeconds(0.3f);
		}
		ArenaTrayDisplay.Get().UpdateTray();
		if (UserAttentionManager.CanShowAttentionGrabber("DraftDisplay.ShowActiveDraftScreen"))
		{
			if (!Options.Get().GetBool(Option.HAS_SEEN_FORGE_PLAY_MODE, false))
			{
				if (this.m_draftManager.GetWins() == 0 && losses == 0)
				{
					NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_FORGE_COMPLETE_22"), "VO_INNKEEPER_ARENA_COMPLETE.prefab:d0c3736823e5a47479bc204abb7a6e71", 0f, null, false);
					Options.Get().SetBool(Option.HAS_SEEN_FORGE_PLAY_MODE, true);
				}
			}
			else if (losses == 2 && !Options.Get().GetBool(Option.HAS_SEEN_FORGE_2LOSS, false))
			{
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_FORGE_2LOSS_25"), "VO_INNKEEPER_FORGE_2LOSS_25.prefab:82e4f0325619e9d4e9a7fb384b6f7e47", 3f, null, false);
				Options.Get().SetBool(Option.HAS_SEEN_FORGE_2LOSS, true);
			}
			else if (this.m_draftManager.GetWins() == 1 && !Options.Get().GetBool(Option.HAS_SEEN_FORGE_1WIN, false))
			{
				while (GameToastMgr.Get().AreToastsActive())
				{
					yield return null;
				}
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(133.1f, NotificationManager.DEPTH, 54.2f), GameStrings.Get("VO_INNKEEPER_FORGE_1WIN"), "VO_INNKEEPER_ARENA_1WIN.prefab:31bb13e800c74c0439ee1a7bfc1e3499", 0f, null, false);
				Options.Get().SetBool(Option.HAS_SEEN_FORGE_1WIN, true);
			}
		}
		yield break;
	}

	// Token: 0x0600244A RID: 9290 RVA: 0x000B5D6C File Offset: 0x000B3F6C
	private void ShowDraftRewardsScreen()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.m_playButton.Disable(false);
		}
		this.ShowPhonePlayButton(false);
		this.EnableBackButton(false);
		this.m_retireButton.Disable();
		if (DemoMgr.Get().ArenaIs1WinMode())
		{
			base.StartCoroutine(this.RestartArena());
			return;
		}
		if (this.m_draftManager.ShouldActivateKey())
		{
			int maxWins = this.m_draftManager.GetMaxWins();
			if (this.m_draftManager.GetWins() >= maxWins && !Options.Get().GetBool(Option.HAS_SEEN_FORGE_MAX_WIN, false) && UserAttentionManager.CanShowAttentionGrabber("DraftDisplay.ShowDraftRewardsScreen:" + Option.HAS_SEEN_FORGE_MAX_WIN))
			{
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_MAX_ARENA_WINS_04"), "VO_INNKEEPER_MAX_ARENA_WINS_04.prefab:cdf8e488f2d17604499f2cc358cb35f6", 0f, null, false);
				Options.Get().SetBool(Option.HAS_SEEN_FORGE_MAX_WIN, true);
			}
			ArenaTrayDisplay.Get().UpdateTray(false);
			ArenaTrayDisplay.Get().ActivateKey();
			if (this.m_PhoneDeckControl != null)
			{
				this.m_PhoneDeckControl.SetMode(ArenaPhoneControl.ControlMode.Rewards);
			}
		}
		else
		{
			ArenaTrayDisplay.Get().ShowRewardsOpenAtStart();
		}
		this.LoadAndPositionHeroCard();
	}

	// Token: 0x0600244B RID: 9291 RVA: 0x000B5E8E File Offset: 0x000B408E
	private IEnumerator RestartArena()
	{
		Debug.LogWarning("Restarting");
		int wins = this.m_draftManager.GetWins();
		if (wins < 5)
		{
			DemoMgr.Get().CreateDemoText(GameStrings.Get("GLUE_BLIZZCON2013_ARENA_NO_PRIZE"), true);
		}
		else if (wins < 9)
		{
			DemoMgr.Get().CreateDemoText(GameStrings.Get("GLUE_BLIZZCON2013_ARENA_PRIZE"), true);
		}
		else if (wins == 9)
		{
			DemoMgr.Get().CreateDemoText(GameStrings.Get("GLUE_BLIZZCON2013_ARENA_GRAND_PRIZE"), true);
		}
		AssetLoader.Get().InstantiatePrefab("NumberLabel.prefab:597544d5ed24b994f95fe56e28584992", new PrefabCallback<GameObject>(this.LastArenaWinsLabelLoaded), wins, AssetLoadingOptions.IgnorePrefabPosition);
		this.m_currentLabels = new List<HeroLabel>();
		yield return new WaitForSeconds(6f);
		this.SetDraftMode(DraftDisplay.DraftMode.NO_ACTIVE_DRAFT);
		yield return new WaitForSeconds(2f);
		Network.Get().AckDraftRewards(this.m_draftManager.GetDraftDeck().ID, this.m_draftManager.GetSlot());
		yield return new WaitForSeconds(1f);
		ArenaTrayDisplay.Get().UpdateTray();
		if (this.m_chosenHero != null)
		{
			UnityEngine.Object.Destroy(this.m_chosenHero.gameObject);
		}
		yield return new WaitForSeconds(1f);
		Network.Get().PurchaseViaGold(1, ProductType.PRODUCT_TYPE_DRAFT, 0);
		yield return new WaitForSeconds(15f);
		if (wins >= 5)
		{
			DemoMgr.Get().MakeDemoTextClickable(true);
			DemoMgr.Get().NextDemoTipIsNewArenaMatch();
		}
		else
		{
			DemoMgr.Get().RemoveDemoTextDialog();
			DemoMgr.Get().CreateDemoText(GameStrings.Get("GLUE_BLIZZCON2013_ARENA"), false, true);
		}
		yield break;
	}

	// Token: 0x0600244C RID: 9292 RVA: 0x000B5EA0 File Offset: 0x000B40A0
	private void LastArenaWinsLabelLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		int num = (int)callbackData;
		go.GetComponent<UberText>().Text = "Last Arena: " + num + " Wins";
		go.transform.position = new Vector3(11.40591f, 1.341853f, 29.28797f);
		go.transform.localScale = new Vector3(15f, 15f, 15f);
	}

	// Token: 0x0600244D RID: 9293 RVA: 0x000B5F14 File Offset: 0x000B4114
	private void LoadAndPositionHeroCard()
	{
		if (this.m_chosenHero != null)
		{
			return;
		}
		CollectionDeck draftDeck = this.m_draftManager.GetDraftDeck();
		if (draftDeck == null)
		{
			Log.All.Print("bug 8052, null exception", Array.Empty<object>());
			return;
		}
		GameUtils.LoadAndPositionCardActor("Card_Play_Hero.prefab:42cbbd2c4969afb46b3887bb628de19d", draftDeck.HeroCardID, draftDeck.HeroPremium, new GameUtils.LoadActorCallback(this.OnHeroActorLoaded));
		string actorName;
		if (draftDeck.HeroPremium == TAG_PREMIUM.GOLDEN)
		{
			actorName = "Card_Play_HeroPower_Premium.prefab:015ad985f9ec49e4db327d131fd79901";
			GameUtils.LoadAndPositionCardActor("History_HeroPower_Premium.prefab:081da807b95b8495e9f16825c5164787", draftDeck.HeroPowerCardID, draftDeck.HeroPremium, new GameUtils.LoadActorCallback(this.LoadHeroPowerCallback));
		}
		else
		{
			actorName = "Card_Play_HeroPower.prefab:a3794839abb947146903a26be13e09af";
		}
		GameUtils.LoadAndPositionCardActor(actorName, draftDeck.HeroPowerCardID, draftDeck.HeroPremium, new GameUtils.LoadActorCallback(this.OnHeroPowerActorLoaded));
	}

	// Token: 0x0600244E RID: 9294 RVA: 0x000B5FD0 File Offset: 0x000B41D0
	private void OnNetCacheReady()
	{
		NetCache.Get().UnregisterNetCacheHandler(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		if (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.Forge)
		{
			this.m_netCacheReady = true;
			return;
		}
		if (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
		{
			return;
		}
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		Error.AddWarningLoc("GLOBAL_FEATURE_DISABLED_TITLE", "GLOBAL_FEATURE_DISABLED_MESSAGE_FORGE", Array.Empty<object>());
	}

	// Token: 0x0600244F RID: 9295 RVA: 0x000B6040 File Offset: 0x000B4240
	private void PositionAndShowChoices()
	{
		if (this.m_inPositionAndShowChoices)
		{
			return;
		}
		this.m_inPositionAndShowChoices = true;
		this.m_pickArea.enabled = true;
		for (int i = 0; i < this.m_choices.Count; i++)
		{
			DraftDisplay.DraftChoice draftChoice = this.m_choices[i];
			if (draftChoice.m_actor == null)
			{
				Debug.LogWarning(string.Format("DraftDisplay.PositionAndShowChoices(): WARNING found choice with null actor (cardID = {0}). Skipping...", draftChoice.m_cardID));
			}
			else
			{
				bool flag = draftChoice.m_actor.GetEntityDef().IsHeroSkin();
				bool flag2 = draftChoice.m_actor.GetEntityDef().IsHeroPower();
				Actor actor = null;
				Actor heroPowerActor = null;
				TAG_RARITY rarity;
				if (flag2)
				{
					SceneUtils.SetLayer(this.m_chosenHero.gameObject, GameLayer.Default);
					actor = this.m_chosenHero.Clone();
					UberShaderController[] componentsInChildren = actor.GetComponentsInChildren<UberShaderController>(true);
					if (componentsInChildren != null)
					{
						foreach (UberShaderController uberShaderController in componentsInChildren)
						{
							if (uberShaderController.UberShaderAnimation != null)
							{
								uberShaderController.UberShaderAnimation = UnityEngine.Object.Instantiate<UberShaderAnimation>(uberShaderController.UberShaderAnimation);
							}
						}
					}
					actor.transform.position = this.GetCardPosition(i, true);
					actor.Show();
					actor.ActivateSpellBirthState(SpellType.SUMMON_IN_FORGE);
					rarity = actor.GetEntityDef().GetRarity();
					actor.ActivateSpellBirthState(DraftDisplay.GetSpellTypeForRarity(rarity));
					this.m_subclassHeroClones.Add(actor);
					DraftCardVisual draftCardVisual = actor.GetCollider().gameObject.GetComponent<DraftCardVisual>();
					if (draftCardVisual == null)
					{
						draftCardVisual = actor.GetCollider().gameObject.AddComponent<DraftCardVisual>();
					}
					draftCardVisual.SetChoiceNum(i + 1);
					draftCardVisual.SetActor(draftChoice.m_actor);
					draftCardVisual.SetSubActor(actor);
					draftChoice.m_subActor = actor;
					actor.TurnOnCollider();
					heroPowerActor = this.m_subclassHeroPowerActors[i];
					heroPowerActor.transform.position = this.m_heroPowerBones[i].position;
					if (UniversalInputManager.UsePhoneUI)
					{
						heroPowerActor.transform.localScale = DraftDisplay.DRAFTING_HERO_POWER_SCALE_PHONE;
					}
					else
					{
						heroPowerActor.transform.localScale = DraftDisplay.DRAFTING_HERO_POWER_SCALE;
						SpellUtils.SetCustomSpellParent(UnityEngine.Object.Instantiate<Spell>(this.m_heroPowerChosenFadeOut), draftChoice.m_actor);
					}
					draftCardVisual = heroPowerActor.GetCollider().gameObject.AddComponent<DraftCardVisual>();
					draftCardVisual.SetChoiceNum(i + 1);
					draftCardVisual.SetActor(draftChoice.m_actor);
					draftCardVisual.SetSubActor(actor);
					heroPowerActor.TurnOnCollider();
					DefLoader.DisposableFullDef disposableFullDef = this.m_subClassHeroPowerDefs[i];
					heroPowerActor.SetPremium(draftChoice.m_premium);
					heroPowerActor.SetCardDef(disposableFullDef.DisposableCardDef);
					heroPowerActor.SetEntityDef(disposableFullDef.EntityDef);
					heroPowerActor.UpdateAllComponents();
					heroPowerActor.Hide();
					Spell spell = UnityEngine.Object.Instantiate<Spell>(this.m_heroPowerChosenFadeIn);
					SpellUtils.SetCustomSpellParent(spell, heroPowerActor);
					spell.transform.localPosition = new Vector3(spell.transform.localPosition.x, spell.transform.localPosition.y + 0.5f, spell.transform.localPosition.z);
				}
				else
				{
					draftChoice.m_actor.transform.position = this.GetCardPosition(i, flag);
					draftChoice.m_actor.Show();
					draftChoice.m_actor.ActivateSpellBirthState(SpellType.SUMMON_IN_FORGE);
					rarity = draftChoice.m_actor.GetEntityDef().GetRarity();
					draftChoice.m_actor.ActivateSpellBirthState(DraftDisplay.GetSpellTypeForRarity(rarity));
				}
				if (rarity - TAG_RARITY.COMMON > 1)
				{
					if (rarity - TAG_RARITY.RARE <= 2)
					{
						SoundManager.Get().LoadAndPlay("forge_rarity_card_appears.prefab:4ecbc5de846e50746986849690c01e6a");
					}
				}
				else
				{
					SoundManager.Get().LoadAndPlay("forge_normal_card_appears.prefab:3e1223a4e6503f2469fb0090db8da67e");
				}
				if (flag)
				{
					if (i == 0 && DemoMgr.Get().ArenaIs1WinMode())
					{
						DemoMgr.Get().CreateDemoText(GameStrings.Get("GLUE_BLIZZCON2013_ARENA"), false, true);
					}
					draftChoice.m_actor.GetHealthObject().Hide();
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_heroLabel);
					gameObject.transform.position = draftChoice.m_actor.GetMeshRenderer(false).transform.position;
					HeroLabel component = gameObject.GetComponent<HeroLabel>();
					if (UniversalInputManager.UsePhoneUI)
					{
						draftChoice.m_actor.transform.localScale = DraftDisplay.HERO_ACTOR_LOCAL_SCALE_PHONE;
						gameObject.transform.localScale = DraftDisplay.HERO_LABEL_SCALE_PHONE;
					}
					else
					{
						draftChoice.m_actor.transform.localScale = DraftDisplay.HERO_ACTOR_LOCAL_SCALE;
						gameObject.transform.localScale = DraftDisplay.HERO_LABEL_SCALE;
					}
					Color white = Color.white;
					if (this.m_draftManager.GetDraftPaperTextColorOverride(ref white))
					{
						component.SetColor(white);
					}
					gameObject.transform.SetParent(base.transform, true);
					component.UpdateText(draftChoice.m_actor.GetEntityDef().GetName(), GameStrings.GetClassName(draftChoice.m_actor.GetEntityDef().GetClass()).ToUpper());
					this.m_currentLabels.Add(component);
				}
				else if (flag2)
				{
					actor.GetHealthObject().Hide();
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.m_heroLabel);
					gameObject2.transform.position = actor.GetMeshRenderer(false).transform.position;
					HeroLabel newLabel = gameObject2.GetComponent<HeroLabel>();
					newLabel.m_nameText.Hide();
					newLabel.m_classText.Hide();
					actor.GetSpell(SpellType.SUMMON_IN_FORGE).AddSpellEventCallback(delegate(string eventName, object eventData, object userData)
					{
						if (eventName == SummonInForge.ACTOR_VISIBLE_EVENT)
						{
							heroPowerActor.Show();
							newLabel.m_classText.Show();
						}
					});
					if (UniversalInputManager.UsePhoneUI)
					{
						actor.transform.localScale = DraftDisplay.HERO_ACTOR_LOCAL_SCALE_PHONE;
						gameObject2.transform.localScale = DraftDisplay.HERO_LABEL_SCALE_PHONE;
					}
					else
					{
						actor.transform.localScale = DraftDisplay.HERO_ACTOR_LOCAL_SCALE;
						gameObject2.transform.localScale = DraftDisplay.HERO_LABEL_SCALE;
					}
					Color white2 = Color.white;
					if (this.m_draftManager.GetDraftPaperTextColorOverride(ref white2))
					{
						newLabel.SetColor(white2);
					}
					string classText = GameStrings.GetClassName(actor.GetEntityDef().GetClass()).ToUpper() + "-" + GameStrings.GetClassName(draftChoice.m_actor.GetEntityDef().GetClass()).ToUpper();
					newLabel.UpdateText(this.m_chosenHero.GetEntityDef().GetName(), classText);
					newLabel.m_classText.CharacterSize = 5f;
					this.m_currentLabels.Add(newLabel);
				}
				else if (UniversalInputManager.UsePhoneUI)
				{
					draftChoice.m_actor.transform.localScale = DraftDisplay.CHOICE_ACTOR_LOCAL_SCALE_PHONE;
				}
				else
				{
					draftChoice.m_actor.transform.localScale = DraftDisplay.CHOICE_ACTOR_LOCAL_SCALE;
				}
			}
		}
		this.EnableBackButton(true);
		base.StartCoroutine(this.RunAutoDraftCheat());
		this.m_pickArea.enabled = false;
	}

	// Token: 0x06002450 RID: 9296 RVA: 0x000B6743 File Offset: 0x000B4943
	private bool CanAutoDraft()
	{
		return HearthstoneApplication.IsInternal() && Vars.Key("Arena.AutoDraft").GetBool(false);
	}

	// Token: 0x06002451 RID: 9297 RVA: 0x000B6763 File Offset: 0x000B4963
	public IEnumerator RunAutoDraftCheat()
	{
		if (!this.CanAutoDraft())
		{
			yield break;
		}
		int frameStart = Time.frameCount;
		while (GameUtils.IsAnyTransitionActive() && Time.frameCount - frameStart < 120)
		{
			yield return null;
		}
		List<DraftCardVisual> draftChoices = this.GetCardVisuals();
		if (draftChoices != null && draftChoices.Count > 0)
		{
			int pickedIndex = UnityEngine.Random.Range(0, draftChoices.Count - 1);
			DraftCardVisual visual = draftChoices[pickedIndex];
			frameStart = Time.frameCount;
			while (visual.GetActor() == null && Time.frameCount - frameStart < 120)
			{
				yield return null;
			}
			if (visual.GetActor() != null)
			{
				string message = string.Format("autodraft'ing {0}\nto stop, use cmd 'autodraft off'", visual.GetActor().GetEntityDef().GetName());
				UIStatus.Get().AddInfo(message, 2f);
				draftChoices[pickedIndex].ChooseThisCard();
			}
			visual = null;
		}
		yield break;
	}

	// Token: 0x06002452 RID: 9298 RVA: 0x000B6774 File Offset: 0x000B4974
	private Vector3 GetCardPosition(int cardChoice, bool isHeroSkin)
	{
		float num = this.m_pickArea.bounds.center.x - this.m_pickArea.bounds.extents.x;
		float num2 = this.m_pickArea.bounds.size.x / 3f;
		float num3 = (this.m_choices.Count == 2) ? 0f : (-num2 / 2f);
		float num4 = 0f;
		if (isHeroSkin)
		{
			num4 = 1f;
		}
		return new Vector3(num + (float)(cardChoice + 1) * num2 + num3, this.m_pickArea.transform.position.y, this.m_pickArea.transform.position.z + num4);
	}

	// Token: 0x06002453 RID: 9299 RVA: 0x000B6838 File Offset: 0x000B4A38
	public static SpellType GetSpellTypeForRarity(TAG_RARITY rarity)
	{
		switch (rarity)
		{
		case TAG_RARITY.RARE:
			return SpellType.BURST_RARE;
		case TAG_RARITY.EPIC:
			return SpellType.BURST_EPIC;
		case TAG_RARITY.LEGENDARY:
			return SpellType.BURST_LEGENDARY;
		default:
			return SpellType.BURST_COMMON;
		}
	}

	// Token: 0x06002454 RID: 9300 RVA: 0x000B685C File Offset: 0x000B4A5C
	private void OnHeroActorLoaded(Actor actor)
	{
		actor.transform.SetParent(base.transform, true);
		this.m_chosenHero = actor;
		this.m_chosenHero.transform.parent = this.m_socketHeroBone;
		this.m_chosenHero.transform.localPosition = Vector3.zero;
		this.m_chosenHero.transform.localScale = Vector3.one;
		this.m_chosenHero.transform.localRotation = Quaternion.identity;
	}

	// Token: 0x06002455 RID: 9301 RVA: 0x000B68D8 File Offset: 0x000B4AD8
	private void OnHeroPowerActorLoaded(Actor actor)
	{
		actor.transform.SetParent(base.transform, true);
		this.m_inPlayHeroPowerActor = actor;
		this.SetupToDisplayHeroPowerTooltip(this.m_inPlayHeroPowerActor);
		this.m_inPlayHeroPowerActor.transform.parent = this.m_socketHeroPowerBone;
		this.m_inPlayHeroPowerActor.transform.localPosition = Vector3.zero;
		this.m_inPlayHeroPowerActor.transform.localScale = Vector3.one;
		this.m_inPlayHeroPowerActor.transform.localRotation = Quaternion.identity;
	}

	// Token: 0x06002456 RID: 9302 RVA: 0x000B695F File Offset: 0x000B4B5F
	private void OnMouseOverHeroPower(UIEvent uiEvent)
	{
		if (this.m_inPlayHeroPowerActor != null)
		{
			this.ShowHeroPower(this.m_inPlayHeroPowerActor);
		}
	}

	// Token: 0x06002457 RID: 9303 RVA: 0x000B697B File Offset: 0x000B4B7B
	private void OnMouseOutHeroPower(UIEvent uiEvent)
	{
		if (this.m_heroPower != null)
		{
			this.m_heroPower.Hide();
		}
	}

	// Token: 0x06002458 RID: 9304 RVA: 0x000B6998 File Offset: 0x000B4B98
	private void OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		DraftDisplay.ChoiceCallback choiceCallback = (DraftDisplay.ChoiceCallback)callbackData;
		using (DefLoader.DisposableFullDef fullDef = (choiceCallback != null) ? choiceCallback.fullDef : null)
		{
			if (go == null)
			{
				Debug.LogWarning(string.Format("DraftDisplay.OnActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			}
			else
			{
				go.transform.SetParent(base.transform, true);
				Actor component = go.GetComponent<Actor>();
				if (component == null)
				{
					Debug.LogWarning(string.Format("DraftDisplay.OnActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef));
				}
				else
				{
					DraftDisplay.DraftChoice draftChoice = this.m_choices.Find((DraftDisplay.DraftChoice obj) => obj.m_cardID.Equals(fullDef.EntityDef.GetCardId()));
					if (draftChoice == null)
					{
						Debug.LogWarningFormat("DraftDisplay.OnActorLoaded(): Could not find draft choice {0} (cardID = {1}) in m_choices.", new object[]
						{
							fullDef.EntityDef.GetName(),
							fullDef.EntityDef.GetCardId()
						});
						UnityEngine.Object.Destroy(go);
					}
					else
					{
						draftChoice.m_actor = component;
						draftChoice.m_actor.SetPremium(draftChoice.m_premium);
						draftChoice.m_actor.SetEntityDef(fullDef.EntityDef);
						draftChoice.m_actor.SetCardDef(fullDef.DisposableCardDef);
						draftChoice.m_actor.UpdateAllComponents();
						draftChoice.m_actor.gameObject.name = fullDef.CardDef.name + "_actor";
						draftChoice.m_actor.ContactShadow(true);
						if (draftChoice.m_actor.GetEntityDef().IsHeroPower())
						{
							this.m_heroPowerCardActors[choiceCallback.choiceID - 1] = draftChoice.m_actor;
							if (this.HaveActorsForAllChoices() && this.HaveAllSubclassHeroPowerDefs())
							{
								this.PositionAndShowChoices();
							}
							else
							{
								draftChoice.m_actor.Hide();
							}
						}
						else
						{
							DraftCardVisual draftCardVisual = draftChoice.m_actor.GetCollider().gameObject.AddComponent<DraftCardVisual>();
							draftCardVisual.SetActor(draftChoice.m_actor);
							draftCardVisual.SetChoiceNum(choiceCallback.choiceID);
							if (this.HaveActorsForAllChoices())
							{
								this.PositionAndShowChoices();
							}
							else
							{
								draftChoice.m_actor.Hide();
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06002459 RID: 9305 RVA: 0x000B6BC0 File Offset: 0x000B4DC0
	private void OnSubClassActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		DraftDisplay.ChoiceCallback choiceCallback = (DraftDisplay.ChoiceCallback)callbackData;
		using (DefLoader.DisposableFullDef fullDef = choiceCallback.fullDef)
		{
			if (go == null)
			{
				Debug.LogWarning(string.Format("DraftDisplay.OnDualClassActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			}
			else
			{
				go.transform.SetParent(base.transform, true);
				Actor component = go.GetComponent<Actor>();
				if (component == null)
				{
					Debug.LogWarning(string.Format("DraftDisplay.OnDualClassActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef));
				}
				else
				{
					DefLoader.DisposableFullDef disposableFullDef = this.m_subClassHeroPowerDefs[choiceCallback.choiceID - 1];
					if (disposableFullDef != null)
					{
						disposableFullDef.Dispose();
					}
					this.m_subClassHeroPowerDefs[choiceCallback.choiceID - 1] = fullDef.Share();
					this.m_subclassHeroPowerActors[choiceCallback.choiceID - 1] = component;
					if (this.HaveActorsForAllChoices() && this.HaveAllSubclassHeroPowerDefs())
					{
						this.PositionAndShowChoices();
					}
				}
			}
		}
	}

	// Token: 0x0600245A RID: 9306 RVA: 0x000B6CA0 File Offset: 0x000B4EA0
	private void OnCardDefLoaded(string cardId, DefLoader.DisposableCardDef def, object callbackData)
	{
		try
		{
			if (def != null)
			{
				foreach (EmoteEntryDef emoteEntryDef in ((def != null) ? def.CardDef.m_EmoteDefs : null))
				{
					if (emoteEntryDef.m_emoteType == EmoteType.PICKED)
					{
						AssetLoader.Get().InstantiatePrefab(emoteEntryDef.m_emoteSoundSpellPath, new PrefabCallback<GameObject>(this.OnStartEmoteLoaded), callbackData, AssetLoadingOptions.None);
					}
				}
			}
		}
		finally
		{
			if (def != null)
			{
				((IDisposable)def).Dispose();
			}
		}
	}

	// Token: 0x0600245B RID: 9307 RVA: 0x000B6D44 File Offset: 0x000B4F44
	private void OnStartEmoteLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		CardSoundSpell cardSoundSpell = null;
		if (go != null)
		{
			cardSoundSpell = go.GetComponent<CardSoundSpell>();
			go.transform.SetParent(base.transform, true);
		}
		this.m_skipHeroEmotes |= (cardSoundSpell == null);
		if (this.m_skipHeroEmotes)
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		int num = (int)callbackData;
		this.m_heroEmotes[num - 1] = cardSoundSpell;
	}

	// Token: 0x0600245C RID: 9308 RVA: 0x000B6DAC File Offset: 0x000B4FAC
	private bool HaveActorsForAllChoices()
	{
		using (List<DraftDisplay.DraftChoice>.Enumerator enumerator = this.m_choices.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.m_actor == null)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x0600245D RID: 9309 RVA: 0x000B6E0C File Offset: 0x000B500C
	private bool HaveAllSubclassHeroPowerDefs()
	{
		DefLoader.DisposableFullDef[] subClassHeroPowerDefs = this.m_subClassHeroPowerDefs;
		for (int i = 0; i < subClassHeroPowerDefs.Length; i++)
		{
			if (subClassHeroPowerDefs[i] == null)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600245E RID: 9310 RVA: 0x000B6E38 File Offset: 0x000B5038
	private void InitManaCurve()
	{
		CollectionDeck draftDeck = this.m_draftManager.GetDraftDeck();
		if (draftDeck == null)
		{
			return;
		}
		foreach (CollectionDeckSlot collectionDeckSlot in draftDeck.GetSlots())
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(collectionDeckSlot.CardID);
			for (int i = 0; i < collectionDeckSlot.Count; i++)
			{
				this.AddCardToManaCurve(entityDef);
			}
		}
	}

	// Token: 0x0600245F RID: 9311 RVA: 0x000B3F39 File Offset: 0x000B2139
	private void OnStoreBackButtonPressed(bool authorizationBackButtonPressed, object userData)
	{
		this.ExitDraftScene();
	}

	// Token: 0x06002460 RID: 9312 RVA: 0x000B6EC4 File Offset: 0x000B50C4
	private bool OnNavigateBack()
	{
		if (this.IsInHeroSelectMode())
		{
			this.DoHeroCancelAnimation();
			return false;
		}
		if (ArenaTrayDisplay.Get() == null)
		{
			return false;
		}
		ArenaTrayDisplay.Get().KeyFXCancel();
		this.ExitDraftScene();
		return true;
	}

	// Token: 0x06002461 RID: 9313 RVA: 0x00004EB5 File Offset: 0x000030B5
	private void BackButtonPress(UIEvent e)
	{
		Navigation.GoBack();
	}

	// Token: 0x06002462 RID: 9314 RVA: 0x000B6EF8 File Offset: 0x000B50F8
	private void ExitDraftScene()
	{
		GameMgr.Get().CancelFindGame();
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.m_playButton.Disable(false);
		}
		this.ShowPhonePlayButton(false);
		StoreManager.Get().HideStore(ShopType.ARENA_STORE);
		if (!SceneMgr.Get().IsInDuelsMode())
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.GAME_MODE, SceneMgr.TransitionHandlerType.NEXT_SCENE, null);
		}
		Box.Get().SetToIgnoreFullScreenEffects(false);
	}

	// Token: 0x06002463 RID: 9315 RVA: 0x000B6F60 File Offset: 0x000B5160
	private void PlayButtonPress(UIEvent e)
	{
		if (SetRotationManager.Get().CheckForSetRotationRollover())
		{
			return;
		}
		if (PlayerMigrationManager.Get() != null && PlayerMigrationManager.Get().CheckForPlayerMigrationRequired())
		{
			return;
		}
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.m_playButton.Disable(false);
		}
		this.ShowPhonePlayButton(false);
		this.m_draftManager.FindGame();
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.ARENA_QUEUE
		});
	}

	// Token: 0x06002464 RID: 9316 RVA: 0x000B6FD4 File Offset: 0x000B51D4
	private void RetireButtonPress(UIEvent e)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_FORGE_RETIRE_WARNING_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_FORGE_RETIRE_WARNING_DESC");
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnRetirePopupResponse);
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x06002465 RID: 9317 RVA: 0x000B7032 File Offset: 0x000B5232
	private void OnFreeArenaWinOkButtonPress(DialogBase dialog, object userData)
	{
		Options.Get().SetBool(Option.HAS_SEEN_FREE_ARENA_WIN_DIALOG_THIS_DRAFT, true);
		this.ShowCurrentlyDraftingScreen();
	}

	// Token: 0x06002466 RID: 9318 RVA: 0x000B704A File Offset: 0x000B524A
	private void OnOutstandingTicketEnterButtonPress()
	{
		this.m_draftManager.RequestDraftBegin();
		Options.Get().SetBool(Option.HAS_SEEN_FORGE, true);
	}

	// Token: 0x06002467 RID: 9319 RVA: 0x000B3F39 File Offset: 0x000B2139
	private void OnOutstandingTicketCancelButtonPress()
	{
		this.ExitDraftScene();
	}

	// Token: 0x06002468 RID: 9320 RVA: 0x000B7068 File Offset: 0x000B5268
	private void OnRetirePopupResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			return;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_draftDeckTray.gameObject.GetComponent<SlidingTray>().HideTray();
		}
		this.m_retireButton.Disable();
		this.EnableBackButton(false);
		Network.Get().DraftRetire(this.m_draftManager.GetDraftDeck().ID, this.m_draftManager.GetSlot(), this.m_draftManager.CurrentSeasonId);
	}

	// Token: 0x06002469 RID: 9321 RVA: 0x000B70E0 File Offset: 0x000B52E0
	private void ManaCurveOver(UIEvent e)
	{
		this.m_manaCurve.GetComponent<TooltipZone>().ShowTooltip(GameStrings.Get("GLUE_FORGE_MANATIP_HEADER"), GameStrings.Get("GLUE_FORGE_MANATIP_DESC"), UniversalInputManager.UsePhoneUI ? TooltipPanel.BOX_SCALE : TooltipPanel.FORGE_SCALE, 0);
	}

	// Token: 0x0600246A RID: 9322 RVA: 0x000B7130 File Offset: 0x000B5330
	private void ManaCurveOut(UIEvent e)
	{
		this.m_manaCurve.GetComponent<TooltipZone>().HideTooltip();
	}

	// Token: 0x0600246B RID: 9323 RVA: 0x000B7142 File Offset: 0x000B5342
	private void DeckHeaderOver(UIEvent e)
	{
		this.m_draftDeckTray.GetTooltipZone().ShowTooltip(GameStrings.Get("GLUE_ARENA_DECK_TOOLTIP_HEADER"), GameStrings.Get("GLUE_ARENA_DECK_TOOLTIP"), TooltipPanel.FORGE_SCALE, 0);
	}

	// Token: 0x0600246C RID: 9324 RVA: 0x000B7174 File Offset: 0x000B5374
	private void DeckHeaderOut(UIEvent e)
	{
		this.m_draftDeckTray.GetTooltipZone().HideTooltip();
	}

	// Token: 0x0600246D RID: 9325 RVA: 0x000B7188 File Offset: 0x000B5388
	private void SetupBackButton()
	{
		if (DemoMgr.Get().CantExitArena())
		{
			this.m_backButton.SetText("");
			return;
		}
		this.m_backButton.SetText(GameStrings.Get("GLOBAL_BACK"));
		this.m_backButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.BackButtonPress));
	}

	// Token: 0x0600246E RID: 9326 RVA: 0x000B71E0 File Offset: 0x000B53E0
	private void EnableBackButton(bool buttonEnabled)
	{
		if (buttonEnabled != this.m_backButton.IsEnabled())
		{
			this.m_backButton.Flip(buttonEnabled, false);
		}
		this.m_backButton.SetEnabled(buttonEnabled, false);
		if (this.m_PhoneBackButtonBone != null)
		{
			this.m_PhoneBackButtonBone.gameObject.SetActive(buttonEnabled);
		}
	}

	// Token: 0x0600246F RID: 9327 RVA: 0x000B7234 File Offset: 0x000B5434
	private void SetupRetireButton()
	{
		if (DemoMgr.Get().CantExitArena())
		{
			this.m_retireButton.SetText("");
			return;
		}
		this.m_retireButton.SetText(GameStrings.Get("GLUE_DRAFT_RETIRE_BUTTON"));
		this.m_retireButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.RetireButtonPress));
	}

	// Token: 0x06002470 RID: 9328 RVA: 0x000B728C File Offset: 0x000B548C
	private void ShowPhonePlayButton(bool show)
	{
		if (this.m_PhonePlayButtonTray == null)
		{
			return;
		}
		SlidingTray component = this.m_PhonePlayButtonTray.GetComponent<SlidingTray>();
		if (component == null)
		{
			return;
		}
		component.ToggleTraySlider(show, null, true);
	}

	// Token: 0x06002471 RID: 9329 RVA: 0x000B72C8 File Offset: 0x000B54C8
	private void OnScenePreUnload(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		if (prevMode != SceneMgr.Mode.DRAFT)
		{
			return;
		}
		StoreManager.Get().HideStore(ShopType.ARENA_STORE);
		DialogManager.Get().RemoveUniquePopupRequestFromQueue("arena_first_time");
		if (this.m_firstTimeDialog != null)
		{
			this.m_firstTimeDialog.Hide();
		}
		if (this.IsInHeroSelectMode())
		{
			this.m_zoomedHero.gameObject.SetActive(false);
			this.m_heroPower.gameObject.SetActive(false);
			this.m_confirmButton.gameObject.SetActive(false);
			UniversalInputManager.Get().SetGameDialogActive(false);
		}
	}

	// Token: 0x0400141C RID: 5148
	public Collider m_pickArea;

	// Token: 0x0400141D RID: 5149
	public UberText m_instructionText;

	// Token: 0x0400141E RID: 5150
	public UberText m_instructionDetailText;

	// Token: 0x0400141F RID: 5151
	public UberText m_forgeLabel;

	// Token: 0x04001420 RID: 5152
	public DraftManaCurve m_manaCurve;

	// Token: 0x04001421 RID: 5153
	public GameObject m_heroLabel;

	// Token: 0x04001422 RID: 5154
	public Spell m_DeckCompleteSpell;

	// Token: 0x04001423 RID: 5155
	public float m_DeckCardBarFlareUpDelay;

	// Token: 0x04001424 RID: 5156
	public Spell m_heroPowerChosenFadeOut;

	// Token: 0x04001425 RID: 5157
	public Spell m_heroPowerChosenFadeIn;

	// Token: 0x04001426 RID: 5158
	public PegUIElement m_heroClickCatcher;

	// Token: 0x04001427 RID: 5159
	public DraftPhoneDeckTray m_draftDeckTray;

	// Token: 0x04001428 RID: 5160
	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_backButton;

	// Token: 0x04001429 RID: 5161
	public StandardPegButtonNew m_retireButton;

	// Token: 0x0400142A RID: 5162
	public PlayButton m_playButton;

	// Token: 0x0400142B RID: 5163
	[CustomEditField(Sections = "Bones")]
	public Transform m_bigHeroBone;

	// Token: 0x0400142C RID: 5164
	public Transform m_socketHeroBone;

	// Token: 0x0400142D RID: 5165
	public List<Transform> m_heroPowerBones = new List<Transform>();

	// Token: 0x0400142E RID: 5166
	public Transform m_socketHeroPowerBone;

	// Token: 0x0400142F RID: 5167
	[CustomEditField(Sections = "Phone")]
	public GameObject m_PhonePlayButtonTray;

	// Token: 0x04001430 RID: 5168
	public Transform m_PhoneBackButtonBone;

	// Token: 0x04001431 RID: 5169
	public Transform m_PhoneDeckTrayHiddenBone;

	// Token: 0x04001432 RID: 5170
	public GameObject m_Phone3WayButtonRoot;

	// Token: 0x04001433 RID: 5171
	public GameObject m_PhoneChooseHero;

	// Token: 0x04001434 RID: 5172
	public GameObject m_PhoneLargeViewDeckButton;

	// Token: 0x04001435 RID: 5173
	public ArenaPhoneControl m_PhoneDeckControl;

	// Token: 0x04001436 RID: 5174
	private const string ALERTPOPUPID_FIRSTTIME = "arena_first_time";

	// Token: 0x04001437 RID: 5175
	private static readonly Vector3 CHOICE_ACTOR_LOCAL_SCALE = new Vector3(7.2f, 7.2f, 7.2f);

	// Token: 0x04001438 RID: 5176
	private static readonly Vector3 HERO_ACTOR_LOCAL_SCALE = new Vector3(8.285825f, 8.285825f, 8.285825f);

	// Token: 0x04001439 RID: 5177
	private static readonly Vector3 HERO_LABEL_SCALE = new Vector3(8f, 8f, 8f);

	// Token: 0x0400143A RID: 5178
	private static readonly Vector3 HERO_POWER_START_POSITION = new Vector3(0f, 0f, -0.3410472f);

	// Token: 0x0400143B RID: 5179
	private static readonly Vector3 HERO_POWER_POSITION = new Vector3(1.40873f, 0f, -0.3410472f);

	// Token: 0x0400143C RID: 5180
	private static readonly Vector3 HERO_POWER_SCALE = new Vector3(0.3419997f, 0.3419997f, 0.3419997f);

	// Token: 0x0400143D RID: 5181
	private static readonly Vector3 DRAFTING_HERO_POWER_POSITION = new Vector3(0.9f, 0.215f, -0.164f);

	// Token: 0x0400143E RID: 5182
	private static readonly Vector3 DRAFTING_HERO_POWER_BIG_CARD_SCALE = new Vector3(0.5f, 0.5f, 0.5f);

	// Token: 0x0400143F RID: 5183
	private static readonly Vector3 DRAFTING_HERO_POWER_SCALE = new Vector3(5f, 5f, 5f);

	// Token: 0x04001440 RID: 5184
	private static readonly Vector3 HERO_POWER_TOOLTIP_POSITION = new Vector3(-16.3f, 0.3f, -12.5f);

	// Token: 0x04001441 RID: 5185
	private static readonly Vector3 HERO_POWER_TOOLTIP_SCALE = new Vector3(7f, 7f, 7f);

	// Token: 0x04001442 RID: 5186
	private static readonly Vector3 CHOICE_ACTOR_LOCAL_SCALE_PHONE = new Vector3(14.5f, 14.5f, 14.5f) / DraftScene.DRAFT_SCENE_LOCAL_SCALE_INDEX_PHONE;

	// Token: 0x04001443 RID: 5187
	private static readonly Vector3 HERO_ACTOR_LOCAL_SCALE_PHONE = new Vector3(15.5f, 15.5f, 15.5f) / DraftScene.DRAFT_SCENE_LOCAL_SCALE_INDEX_PHONE;

	// Token: 0x04001444 RID: 5188
	private static readonly Vector3 HERO_LABEL_SCALE_PHONE = new Vector3(15f, 15f, 15f);

	// Token: 0x04001445 RID: 5189
	private static readonly Vector3 HERO_POWER_START_POSITION_PHONE = new Vector3(1.6f, 0.3f, -0.15f);

	// Token: 0x04001446 RID: 5190
	private static readonly Vector3 HERO_POWER_POSITION_PHONE = new Vector3(1.07f, 0.3f, -0.15f);

	// Token: 0x04001447 RID: 5191
	private static readonly Vector3 HERO_POWER_SCALE_PHONE = new Vector3(0.5f, 0.5f, 0.5f) / DraftScene.DRAFT_SCENE_LOCAL_SCALE_INDEX_PHONE;

	// Token: 0x04001448 RID: 5192
	private static readonly Vector3 DRAFTING_HERO_POWER_SCALE_PHONE = new Vector3(8f, 8f, 8f) / DraftScene.DRAFT_SCENE_LOCAL_SCALE_INDEX_PHONE;

	// Token: 0x04001449 RID: 5193
	private static readonly Vector3 HERO_POWER_TOOLTIP_POSITION_PHONE = new Vector3(-6.7f, 5f, -5f);

	// Token: 0x0400144A RID: 5194
	private static readonly Vector3 HERO_POWER_TOOLTIP_SCALE_PHONE = new Vector3(15f, 15f, 15f) / DraftScene.DRAFT_SCENE_LOCAL_SCALE_INDEX_PHONE;

	// Token: 0x0400144B RID: 5195
	private static DraftDisplay s_instance;

	// Token: 0x0400144C RID: 5196
	private DraftManager m_draftManager;

	// Token: 0x0400144D RID: 5197
	private List<DraftDisplay.DraftChoice> m_choices = new List<DraftDisplay.DraftChoice>();

	// Token: 0x0400144E RID: 5198
	private Actor[] m_heroPowerCardActors = new Actor[3];

	// Token: 0x0400144F RID: 5199
	private DefLoader.DisposableFullDef[] m_heroPowerDefs = new DefLoader.DisposableFullDef[3];

	// Token: 0x04001450 RID: 5200
	private DefLoader.DisposableFullDef[] m_subClassHeroPowerDefs = new DefLoader.DisposableFullDef[3];

	// Token: 0x04001451 RID: 5201
	private DraftDisplay.DraftMode m_currentMode;

	// Token: 0x04001452 RID: 5202
	private NormalButton m_confirmButton;

	// Token: 0x04001453 RID: 5203
	private Actor m_heroPower;

	// Token: 0x04001454 RID: 5204
	private Actor m_defaultHeroPowerSkin;

	// Token: 0x04001455 RID: 5205
	private Actor m_goldenHeroPowerSkin;

	// Token: 0x04001456 RID: 5206
	private bool m_netCacheReady;

	// Token: 0x04001457 RID: 5207
	private Actor m_chosenHero;

	// Token: 0x04001458 RID: 5208
	private Actor m_inPlayHeroPowerActor;

	// Token: 0x04001459 RID: 5209
	private bool m_animationsComplete = true;

	// Token: 0x0400145A RID: 5210
	private List<HeroLabel> m_currentLabels = new List<HeroLabel>();

	// Token: 0x0400145B RID: 5211
	private CardSoundSpell[] m_heroEmotes = new CardSoundSpell[3];

	// Token: 0x0400145C RID: 5212
	private bool m_skipHeroEmotes;

	// Token: 0x0400145D RID: 5213
	private bool m_isHeroAnimating;

	// Token: 0x0400145E RID: 5214
	private DraftCardVisual m_zoomedHero;

	// Token: 0x0400145F RID: 5215
	private bool m_wasDrafting;

	// Token: 0x04001460 RID: 5216
	private bool m_firstTimeIntroComplete;

	// Token: 0x04001461 RID: 5217
	private DialogBase m_firstTimeDialog;

	// Token: 0x04001462 RID: 5218
	private bool m_fxActive;

	// Token: 0x04001463 RID: 5219
	private bool m_inPositionAndShowChoices;

	// Token: 0x04001464 RID: 5220
	private List<Actor> m_subclassHeroClones = new List<Actor>();

	// Token: 0x04001465 RID: 5221
	private Actor[] m_subclassHeroPowerActors = new Actor[3];

	// Token: 0x020015B0 RID: 5552
	public enum DraftMode
	{
		// Token: 0x0400AEB1 RID: 44721
		INVALID,
		// Token: 0x0400AEB2 RID: 44722
		NO_ACTIVE_DRAFT,
		// Token: 0x0400AEB3 RID: 44723
		DRAFTING,
		// Token: 0x0400AEB4 RID: 44724
		ACTIVE_DRAFT_DECK,
		// Token: 0x0400AEB5 RID: 44725
		IN_REWARDS
	}

	// Token: 0x020015B1 RID: 5553
	private class ChoiceCallback
	{
		// Token: 0x0600E14D RID: 57677 RVA: 0x003FFB58 File Offset: 0x003FDD58
		public DraftDisplay.ChoiceCallback Copy()
		{
			DraftDisplay.ChoiceCallback choiceCallback = new DraftDisplay.ChoiceCallback();
			DefLoader.DisposableFullDef disposableFullDef = this.fullDef;
			choiceCallback.fullDef = ((disposableFullDef != null) ? disposableFullDef.Share() : null);
			choiceCallback.choiceID = this.choiceID;
			choiceCallback.slot = this.slot;
			choiceCallback.premium = this.premium;
			return choiceCallback;
		}

		// Token: 0x0400AEB6 RID: 44726
		public DefLoader.DisposableFullDef fullDef;

		// Token: 0x0400AEB7 RID: 44727
		public int choiceID;

		// Token: 0x0400AEB8 RID: 44728
		public int slot;

		// Token: 0x0400AEB9 RID: 44729
		public TAG_PREMIUM premium;
	}

	// Token: 0x020015B2 RID: 5554
	private class DraftChoice
	{
		// Token: 0x0400AEBA RID: 44730
		public string m_cardID = string.Empty;

		// Token: 0x0400AEBB RID: 44731
		public TAG_PREMIUM m_premium;

		// Token: 0x0400AEBC RID: 44732
		public Actor m_actor;

		// Token: 0x0400AEBD RID: 44733
		public Actor m_subActor;
	}
}
