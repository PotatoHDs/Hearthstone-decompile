using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x0200002E RID: 46
[CustomEditClass]
public class AdventureChooserTray : AccordionMenuTray
{
	// Token: 0x06000178 RID: 376 RVA: 0x00008B84 File Offset: 0x00006D84
	private void Awake()
	{
		this.m_ChooseButton.Disable(false);
		this.m_BackButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.OnBackButton();
		});
		this.m_ChooseButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.ChangeSubScene();
		});
		AdventureConfig.Get().AddSelectedModeChangeListener(new AdventureConfig.SelectedModeChange(this.OnSelectedModeChange));
		AdventureProgressMgr.Get().RegisterProgressUpdatedListener(new AdventureProgressMgr.AdventureProgressUpdatedCallback(this.OnAdventureProgressUpdated));
		Box.Get().AddTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxTransitionFinished));
		base.StartCoroutine(this.InitTrayWhenReady());
	}

	// Token: 0x06000179 RID: 377 RVA: 0x00008C1F File Offset: 0x00006E1F
	private void Start()
	{
		Navigation.PushUnique(new Navigation.NavigateBackHandler(AdventureChooserTray.OnNavigateBack));
		this.m_isStarted = true;
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00008C39 File Offset: 0x00006E39
	protected IEnumerator InitTrayWhenReady()
	{
		if (this.m_ChooseFrameScroller == null || this.m_ChooseFrameScroller.m_ScrollObject == null)
		{
			Debug.LogError("m_ChooseFrameScroller or m_ChooseFrameScroller.m_ScrollObject cannot be null. Unable to create button.", this);
			yield break;
		}
		bool flag = AdventureConfig.Get().PreviousSubScene != AdventureData.Adventuresubscene.INVALID;
		int num = 0;
		AdventureDbId adventurePlayerShouldSee = AdventureConfig.GetAdventurePlayerShouldSee(out num);
		if (!flag && adventurePlayerShouldSee != AdventureDbId.INVALID)
		{
			AdventureConfig.Get().SetSelectedAdventureMode(adventurePlayerShouldSee, AdventureConfig.GetDefaultModeDbIdForAdventure(adventurePlayerShouldSee));
			if (num != 0)
			{
				GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.PLAYER_OPTIONS, GameSaveKeySubkeyId.LATEST_ADVENTURE_WING_SEEN, new long[]
				{
					(long)num
				}), null);
			}
		}
		List<AdventureDef> sortedAdventureDefs = AdventureScene.Get().GetSortedAdventureDefs();
		Map<AdventureDbId, List<AdventureDef>> map = new Map<AdventureDbId, List<AdventureDef>>();
		foreach (AdventureDef adventureDef in sortedAdventureDefs)
		{
			AdventureDbId adventureToNestUnder = adventureDef.m_AdventureToNestUnder;
			if (adventureToNestUnder != AdventureDbId.INVALID)
			{
				if (!map.ContainsKey(adventureToNestUnder))
				{
					map.Add(adventureToNestUnder, new List<AdventureDef>());
				}
				map[adventureToNestUnder].Add(adventureDef);
			}
		}
		List<Widget> buttonWidgets = new List<Widget>();
		using (List<AdventureDef>.Enumerator enumerator = sortedAdventureDefs.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				AdventureDef adventureDef2 = enumerator.Current;
				if (AdventureConfig.ShouldDisplayAdventure(adventureDef2.GetAdventureId()) && !adventureDef2.IsNestedUnderAnotherAdventureOnChooserScreen)
				{
					List<AdventureDef> nestedAdvDefs = null;
					if (map.ContainsKey(adventureDef2.GetAdventureId()))
					{
						nestedAdvDefs = map[adventureDef2.GetAdventureId()];
					}
					Widget widget = this.CreateAdventureChooserButton(adventureDef2, nestedAdvDefs);
					if (widget != null)
					{
						buttonWidgets.Add(widget);
					}
				}
			}
			goto IL_1CA;
		}
		IL_1B3:
		yield return null;
		IL_1CA:
		if (buttonWidgets.TrueForAll((Widget w) => w.IsReady && !w.IsChangingStates))
		{
			base.OnButtonVisualUpdated();
			if (this.m_SelectedSubButton != null && this.m_ChooseFrameScroller != null)
			{
				this.m_ChooseFrameScroller.UpdateScroll();
				this.m_ChooseFrameScroller.CenterObjectInView(this.m_SelectedSubButton.gameObject, 0f, null, iTween.EaseType.easeOutCubic, 0f, false);
			}
			if (this.m_ParentSubScene != null)
			{
				this.m_ParentSubScene.SetIsLoaded(true);
				this.m_ParentSubScene.AddSubSceneTransitionFinishedListener(new AdventureSubScene.SubSceneTransitionFinished(this.OnSubSceneTransitionFinished));
			}
			AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
			this.ShowComingSoonCoverUpSignIfActive(selectedAdventure);
			yield break;
		}
		goto IL_1B3;
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00008C48 File Offset: 0x00006E48
	private void OnDestroy()
	{
		if (AdventureConfig.Get() != null)
		{
			AdventureConfig.Get().RemoveSelectedModeChangeListener(new AdventureConfig.SelectedModeChange(this.OnSelectedModeChange));
		}
		if (Box.Get() != null)
		{
			Box.Get().RemoveTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxTransitionFinished));
		}
		if (AdventureProgressMgr.Get() != null)
		{
			AdventureProgressMgr.Get().RemoveProgressUpdatedListener(new AdventureProgressMgr.AdventureProgressUpdatedCallback(this.OnAdventureProgressUpdated));
		}
		base.CancelInvoke("ShowDisabledAdventureModeRequirementsWarning");
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00004EB5 File Offset: 0x000030B5
	private void OnBackButton()
	{
		Navigation.GoBack();
	}

	// Token: 0x0600017D RID: 381 RVA: 0x00008CC5 File Offset: 0x00006EC5
	private static bool OnNavigateBack()
	{
		AdventureChooserTray.BackToMainMenu();
		return true;
	}

	// Token: 0x0600017E RID: 382 RVA: 0x00008CCD File Offset: 0x00006ECD
	private static void BackToMainMenu()
	{
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
	}

	// Token: 0x0600017F RID: 383 RVA: 0x00008CDC File Offset: 0x00006EDC
	private Widget CreateAdventureChooserButton(AdventureDef advDef, List<AdventureDef> nestedAdvDefs)
	{
		string assetString = this.m_DefaultChooserButtonPrefab;
		if (!string.IsNullOrEmpty(advDef.m_ChooserButtonPrefab))
		{
			assetString = advDef.m_ChooserButtonPrefab;
		}
		Widget widget = WidgetInstance.Create(assetString, false);
		widget.RegisterReadyListener(delegate(object _)
		{
			AdventureChooserButton newbutton = widget.transform.GetComponentInChildren<AdventureChooserButton>();
			if (newbutton == null)
			{
				return;
			}
			GameUtils.SetParent(widget, this.m_ChooseFrameScroller.m_ScrollObject, false);
			AdventureDbId adventureId = advDef.GetAdventureId();
			newbutton.gameObject.name = string.Format("{0}_{1}", newbutton.gameObject.name, adventureId);
			newbutton.SetAdventure(adventureId);
			newbutton.SetButtonText(advDef.GetAdventureName());
			newbutton.SetPortraitTexture(advDef.m_Texture);
			newbutton.SetPortraitTiling(advDef.m_TextureTiling);
			newbutton.SetPortraitOffset(advDef.m_TextureOffset);
			AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
			AdventureDef adventureDef = AdventureScene.Get().GetAdventureDef(selectedAdventure);
			if (selectedAdventure == adventureId || (adventureDef != null && adventureDef.m_AdventureToNestUnder == adventureId))
			{
				newbutton.Toggle = true;
			}
			if (AdventureConfig.IsAdventureComingSoon(advDef.GetAdventureId()) && this.AreAllAdventuresComingSoon(nestedAdvDefs, true))
			{
				this.CreateAdventureChooserComingSoonSubButton(advDef, newbutton);
			}
			else
			{
				this.CreateAdventureChooserModeSubButtons(advDef, newbutton);
				if (nestedAdvDefs != null)
				{
					nestedAdvDefs.Sort((AdventureDef l, AdventureDef r) => l.GetSortOrder() - r.GetSortOrder());
					foreach (AdventureDef advDef2 in nestedAdvDefs)
					{
						this.CreateAdventureChooserModeSubButtons(advDef2, newbutton);
					}
				}
			}
			newbutton.AddVisualUpdatedListener(new ChooserButton.VisualUpdated(this.OnButtonVisualUpdated));
			int index = this.m_ChooserButtons.Count;
			newbutton.AddToggleListener(delegate(bool toggle)
			{
				this.OnChooserButtonToggled(newbutton, toggle, index);
			});
			newbutton.AddModeSelectionListener(new ChooserButton.ModeSelection(this.ButtonModeSelected));
			newbutton.AddExpandedListener(new ChooserButton.Expanded(this.ButtonExpanded));
			this.m_ChooserButtons.Add(newbutton);
			newbutton.FireVisualUpdatedEvent();
		}, null, true);
		return widget;
	}

	// Token: 0x06000180 RID: 384 RVA: 0x00008D58 File Offset: 0x00006F58
	private bool AreAllAdventuresComingSoon(List<AdventureDef> advDefs, bool emptyListDefault = true)
	{
		if (advDefs == null || advDefs.Count == 0)
		{
			return emptyListDefault;
		}
		using (List<AdventureDef>.Enumerator enumerator = advDefs.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!AdventureConfig.IsAdventureComingSoon(enumerator.Current.GetAdventureId()))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06000181 RID: 385 RVA: 0x00008DC0 File Offset: 0x00006FC0
	private void CreateAdventureChooserModeSubButtons(AdventureDef advDef, AdventureChooserButton newbutton)
	{
		List<AdventureSubDef> sortedSubDefs = advDef.GetSortedSubDefs();
		AdventureDbId adventureId = advDef.GetAdventureId();
		AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
		AdventureModeDbId clientChooserAdventureMode = AdventureConfig.Get().GetClientChooserAdventureMode(adventureId);
		string subButtonPrefab = this.m_DefaultChooserSubButtonPrefab;
		if (!string.IsNullOrEmpty(advDef.m_ChooserSubButtonPrefab))
		{
			subButtonPrefab = advDef.m_ChooserSubButtonPrefab;
		}
		bool flag = true;
		if (advDef.IsNestedUnderAnotherAdventureOnChooserScreen)
		{
			flag = (!AdventureConfig.IsAdventureComingSoon(adventureId) && AdventureProgressMgr.Get().IsAdventureComplete(advDef.m_AdventureToNestUnder));
		}
		foreach (AdventureSubDef adventureSubDef in sortedSubDefs)
		{
			AdventureModeDbId adventureModeId = adventureSubDef.GetAdventureModeId();
			AdventureChooserSubButton adventureChooserSubButton = newbutton.CreateSubButton(adventureId, adventureModeId, adventureSubDef, subButtonPrefab, flag && clientChooserAdventureMode == adventureModeId);
			if (!(adventureChooserSubButton == null))
			{
				bool flag2 = newbutton.Toggle && selectedAdventure == advDef.GetAdventureId() && clientChooserAdventureMode == adventureModeId;
				if (flag2)
				{
					adventureChooserSubButton.SetHighlight(true);
					this.UpdateChooseButton(adventureId, adventureModeId);
					this.SetTitleText(adventureId, adventureModeId);
					this.m_SelectedSubButton = adventureChooserSubButton;
				}
				else if (AdventureConfig.IsFeaturedMode(adventureId, adventureModeId))
				{
					adventureChooserSubButton.SetNewGlow(true);
				}
				bool flag3 = AdventureConfig.CanPlayMode(adventureId, adventureModeId, true);
				adventureChooserSubButton.SetDesaturate(!flag3);
				if (selectedAdventure == AdventureDbId.PRACTICE && adventureModeId == AdventureModeDbId.EXPERT && !flag3)
				{
					adventureChooserSubButton.SetContrast(0.3f);
				}
				this.CreateAdventureChooserDescriptionFromPrefab(adventureId, adventureSubDef, flag2);
			}
		}
	}

	// Token: 0x06000182 RID: 386 RVA: 0x00008F38 File Offset: 0x00007138
	private void CreateAdventureChooserComingSoonSubButton(AdventureDef advDef, AdventureChooserButton newbutton)
	{
		AdventureDbId adventureId = advDef.GetAdventureId();
		AdventureModeDbId clientChooserAdventureMode = AdventureConfig.Get().GetClientChooserAdventureMode(adventureId);
		List<AdventureSubDef> sortedSubDefs = advDef.GetSortedSubDefs();
		AdventureSubDef subDef = new AdventureSubDef();
		AdventureModeDbId adventureModeDbId = AdventureModeDbId.LINEAR;
		if (sortedSubDefs.Count > 0)
		{
			subDef = sortedSubDefs[0];
			adventureModeDbId = sortedSubDefs[0].GetAdventureModeId();
		}
		ChooserSubButton chooserSubButton = newbutton.CreateComingSoonSubButton(adventureModeDbId, this.m_DefaultChooserComingSoonSubButtonPrefab);
		if (chooserSubButton == null)
		{
			Debug.LogError("newSubButton cannot be null. Unable to create newSubButton.", this);
			return;
		}
		if (newbutton.Toggle && clientChooserAdventureMode == adventureModeDbId)
		{
			chooserSubButton.SetHighlight(true);
			this.UpdateChooseButton(adventureId, adventureModeDbId);
			this.SetTitleText(adventureId, adventureModeDbId);
			this.m_SelectedSubButton = chooserSubButton;
		}
		this.CreateAdventureChooserDescriptionFromPrefab(adventureId, subDef, newbutton.Toggle);
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00008FF0 File Offset: 0x000071F0
	private void CreateAdventureChooserDescriptionFromPrefab(AdventureDbId adventureId, AdventureSubDef subDef, bool active)
	{
		if (string.IsNullOrEmpty(subDef.m_ChooserDescriptionPrefab))
		{
			return;
		}
		Map<AdventureModeDbId, AdventureChooserDescription> map;
		if (!this.m_Descriptions.TryGetValue(adventureId, out map))
		{
			map = new Map<AdventureModeDbId, AdventureChooserDescription>();
			this.m_Descriptions[adventureId] = map;
		}
		string descText = subDef.GetDescription();
		string requiredText = null;
		if (!AdventureConfig.CanPlayMode(adventureId, subDef.GetAdventureModeId(), false))
		{
			requiredText = subDef.GetRequirementsDescription();
			if (!string.IsNullOrEmpty(subDef.GetLockedDescription()))
			{
				descText = subDef.GetLockedDescription();
			}
		}
		AdventureChooserDescription adventureChooserDescription = GameUtils.LoadGameObjectWithComponent<AdventureChooserDescription>(subDef.m_ChooserDescriptionPrefab);
		if (adventureChooserDescription == null)
		{
			return;
		}
		GameUtils.SetParent(adventureChooserDescription, this.m_DescriptionContainer, false);
		adventureChooserDescription.SetText(requiredText, descText);
		adventureChooserDescription.m_WidgetElement.RegisterReadyListener<Widget>(delegate(Widget w)
		{
			if (w != null)
			{
				AdventureChooserDescriptionDataModel adventureChooserDescriptionDataModel = new AdventureChooserDescriptionDataModel();
				adventureChooserDescriptionDataModel.Heroes = AdventureUtils.GetAvailableGuestHeroesAsCardListSortedByReleaseDate(adventureId);
				this.StartCoroutine(this.UpdateDataModelWhenGameSaveDataIsReady(adventureChooserDescriptionDataModel, adventureId, subDef.GetAdventureModeId(), active));
				w.BindDataModel(adventureChooserDescriptionDataModel, false);
			}
		});
		adventureChooserDescription.gameObject.SetActive(active);
		map[subDef.GetAdventureModeId()] = adventureChooserDescription;
		if (active)
		{
			this.m_CurrentChooserDescription = adventureChooserDescription;
		}
	}

	// Token: 0x06000184 RID: 388 RVA: 0x0000913B File Offset: 0x0000733B
	private IEnumerator UpdateDataModelWhenGameSaveDataIsReady(AdventureChooserDescriptionDataModel dataModel, AdventureDbId adventureId, AdventureModeDbId modeId, bool active)
	{
		dataModel.HasNewHero = false;
		if (!AdventureUtils.DoesAdventureShowNewlyUnlockedGuestHeroTreatment(adventureId))
		{
			yield break;
		}
		AdventureDataDbfRecord adventureDataRecord = AdventureConfig.GetAdventureDataRecord(adventureId, modeId);
		GameSaveKeyId adventureClientKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey;
		if (!GameSaveDataManager.IsGameSaveKeyValid(adventureClientKey))
		{
			yield break;
		}
		if (active && !GameSaveDataManager.Get().IsDataReady(adventureClientKey))
		{
			GameSaveDataManager.Get().Request(adventureClientKey, delegate(bool success)
			{
				if (success)
				{
					dataModel.HasNewHero = AdventureUtils.DoesAdventureHaveUnseenGuestHeroes(adventureId, modeId);
					return;
				}
				Log.Adventures.PrintWarning("Unable to set AdventureChooserDescriptionDataModel.HasNewHero - GameSaveData request failed!", Array.Empty<object>());
			});
			yield break;
		}
		while (!GameSaveDataManager.Get().IsDataReady(adventureClientKey))
		{
			Log.Adventures.Print("Waiting for client key {0} before updating DataModel for that Adventure Chooser Description!", new object[]
			{
				adventureClientKey
			});
			yield return null;
		}
		dataModel.HasNewHero = AdventureUtils.DoesAdventureHaveUnseenGuestHeroes(adventureId, modeId);
		yield break;
	}

	// Token: 0x06000185 RID: 389 RVA: 0x00009160 File Offset: 0x00007360
	private AdventureChooserDescription GetAdventureChooserDescription(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		Map<AdventureModeDbId, AdventureChooserDescription> map;
		if (!this.m_Descriptions.TryGetValue(adventureId, out map))
		{
			return null;
		}
		AdventureChooserDescription result;
		if (!map.TryGetValue(modeId, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x06000186 RID: 390 RVA: 0x00009190 File Offset: 0x00007390
	private void ButtonModeSelected(ChooserSubButton btn)
	{
		foreach (ChooserButton chooserButton in this.m_ChooserButtons)
		{
			chooserButton.DisableSubButtonHighlights();
		}
		AdventureChooserSubButton adventureChooserSubButton = (AdventureChooserSubButton)btn;
		this.m_SelectedSubButton = adventureChooserSubButton;
		if (AdventureConfig.MarkFeaturedMode(adventureChooserSubButton.GetAdventure(), adventureChooserSubButton.GetMode()))
		{
			btn.SetNewGlow(false);
		}
		AdventureConfig.Get().SetSelectedAdventureMode(adventureChooserSubButton.GetAdventure(), adventureChooserSubButton.GetMode());
		this.SetTitleText(adventureChooserSubButton.GetAdventure(), adventureChooserSubButton.GetMode());
	}

	// Token: 0x06000187 RID: 391 RVA: 0x00009230 File Offset: 0x00007430
	protected void ButtonExpanded(ChooserButton button, bool expand)
	{
		if (!expand)
		{
			return;
		}
		base.ToggleScrollable(true);
		AdventureChooserButton adventureChooserButton = (AdventureChooserButton)button;
		foreach (ChooserSubButton chooserSubButton in adventureChooserButton.GetSubButtons())
		{
			AdventureChooserSubButton adventureChooserSubButton = (AdventureChooserSubButton)chooserSubButton;
			if (AdventureConfig.IsFeaturedMode(adventureChooserButton.GetAdventure(), adventureChooserSubButton.GetMode()))
			{
				chooserSubButton.Flash();
			}
			if (AdventureConfig.ShouldShowNewModePopup(adventureChooserButton.GetAdventure(), adventureChooserSubButton.GetMode()))
			{
				base.StartCoroutine(this.ShowNewModePopupOnSubButtonAfterScrollingFinished(adventureChooserSubButton));
			}
		}
	}

	// Token: 0x06000188 RID: 392 RVA: 0x000092AD File Offset: 0x000074AD
	private IEnumerator ShowNewModePopupOnSubButtonAfterScrollingFinished(AdventureChooserSubButton subButton)
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		while (this.m_isTransitioning)
		{
			yield return new WaitForEndOfFrame();
		}
		subButton.ShowNewModePopup(GameStrings.Get("GLUE_ADVENTURE_NEW_MODE_UNLOCKED_POPUP_TEXT"));
		subButton.HideNewModePopupAfterDelay();
		AdventureConfig.MarkHasSeenNewModePopup(subButton.GetAdventure(), subButton.GetMode());
		yield break;
	}

	// Token: 0x06000189 RID: 393 RVA: 0x000092C4 File Offset: 0x000074C4
	private void SetTitleText(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)modeId);
		this.m_DescriptionTitleObject.Text = adventureDataRecord.Name;
	}

	// Token: 0x0600018A RID: 394 RVA: 0x000092F0 File Offset: 0x000074F0
	private void OnSelectedModeChange(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		AdventureChooserDescription adventureChooserDescription = this.GetAdventureChooserDescription(adventureId, modeId);
		if (this.m_CurrentChooserDescription != adventureChooserDescription)
		{
			if (this.m_CurrentChooserDescription != null)
			{
				this.m_CurrentChooserDescription.gameObject.SetActive(false);
			}
			this.m_CurrentChooserDescription = adventureChooserDescription;
			if (this.m_CurrentChooserDescription != null)
			{
				this.m_CurrentChooserDescription.gameObject.SetActive(true);
			}
		}
		this.UpdateChooseButton(adventureId, modeId);
		if (this.m_ChooseButton.IsEnabled())
		{
			PlayMakerFSM component = this.m_ChooseButton.GetComponent<PlayMakerFSM>();
			if (component != null)
			{
				component.SendEvent("Burst");
			}
		}
		this.ShowComingSoonCoverUpSignIfActive(adventureId);
		if (!AdventureConfig.CanPlayMode(adventureId, modeId, false))
		{
			if (!this.m_isStarted)
			{
				base.Invoke("ShowDisabledAdventureModeRequirementsWarning", 0f);
				return;
			}
			this.ShowDisabledAdventureModeRequirementsWarning();
		}
	}

	// Token: 0x0600018B RID: 395 RVA: 0x000093BE File Offset: 0x000075BE
	private void ShowComingSoonCoverUpSignIfActive(AdventureDbId adventureId)
	{
		if (AdventureConfig.IsAdventureComingSoon(adventureId))
		{
			this.m_ComingSoonCoverUpSign.SetActive(true);
			this.SetComingSoonCoverUpSignText(adventureId);
			return;
		}
		this.m_ComingSoonCoverUpSign.SetActive(false);
	}

	// Token: 0x0600018C RID: 396 RVA: 0x000093E8 File Offset: 0x000075E8
	private void SetComingSoonCoverUpSignText(AdventureDbId adventureId)
	{
		AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)adventureId);
		this.m_ComingSoonCoverUpSignHeaderText.Text = record.ComingSoonText;
		SpecialEventType eventType = SpecialEventManager.GetEventType(record.ComingSoonEvent);
		TimeSpan? timeSpan = SpecialEventManager.Get().GetEventEndTimeUtc(eventType) - DateTime.UtcNow;
		if (timeSpan == null)
		{
			this.m_ComingSoonCoverUpSignDescriptionText.Text = GameStrings.Get("GLOBAL_DATETIME_COMING_SOON");
			return;
		}
		TimeUtils.ElapsedStringSet stringSet = new TimeUtils.ElapsedStringSet
		{
			m_minutes = "GLOBAL_DATETIME_COMING_SOON_MINUTES",
			m_hours = "GLOBAL_DATETIME_COMING_SOON_HOURS",
			m_days = "GLOBAL_DATETIME_COMING_SOON_DAYS",
			m_weeks = "GLOBAL_DATETIME_COMING_SOON_WEEKS",
			m_monthAgo = "GLOBAL_DATETIME_COMING_SOON"
		};
		this.m_ComingSoonCoverUpSignDescriptionText.Text = TimeUtils.GetElapsedTimeString((long)timeSpan.Value.TotalSeconds, stringSet, true);
	}

	// Token: 0x0600018D RID: 397 RVA: 0x000094E0 File Offset: 0x000076E0
	private void ShowDisabledAdventureModeRequirementsWarning()
	{
		base.CancelInvoke("ShowDisabledAdventureModeRequirementsWarning");
		if (!this.m_isStarted || SceneMgr.Get().GetMode() != SceneMgr.Mode.ADVENTURE)
		{
			return;
		}
		if (this.m_ChooseButton != null && !this.m_ChooseButton.IsEnabled())
		{
			AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
			AdventureModeDbId selectedMode = AdventureConfig.Get().GetSelectedMode();
			if (!AdventureConfig.CanPlayMode(selectedAdventure, selectedMode, false))
			{
				int adventureId = (int)selectedAdventure;
				int modeId = (int)selectedMode;
				string text = GameUtils.GetAdventureDataRecord(adventureId, modeId).RequirementsDescription;
				if (!string.IsNullOrEmpty(text))
				{
					Error.AddWarning(GameStrings.Get("GLUE_ADVENTURE_LOCKED"), text, Array.Empty<object>());
				}
			}
		}
	}

	// Token: 0x0600018E RID: 398 RVA: 0x0000957C File Offset: 0x0000777C
	private void UpdateChooseButton(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		if (!this.m_AttemptedLoad && AdventureConfig.CanPlayMode(adventureId, modeId, true) && AdventureConfig.IsAdventureEventActive(adventureId))
		{
			this.m_ChooseButton.SetText(GameStrings.Get("GLOBAL_ADVENTURE_CHOOSE_BUTTON_TEXT"));
			if (!this.m_ChooseButton.IsEnabled())
			{
				this.m_ChooseButton.Enable();
				return;
			}
		}
		else
		{
			this.m_ChooseButton.SetText(GameStrings.Get("GLUE_QUEST_LOG_CLASS_LOCKED"));
			this.m_ChooseButton.Disable(false);
		}
	}

	// Token: 0x0600018F RID: 399 RVA: 0x000095F4 File Offset: 0x000077F4
	private void OnBoxTransitionFinished(object userData)
	{
		if (!this.m_isStarted || SceneMgr.Get().GetMode() != SceneMgr.Mode.ADVENTURE)
		{
			return;
		}
		if (this.m_ChooseButton.IsEnabled())
		{
			PlayMakerFSM component = this.m_ChooseButton.GetComponent<PlayMakerFSM>();
			if (component != null)
			{
				component.SendEvent("Burst");
			}
		}
		else
		{
			this.ShowDisabledAdventureModeRequirementsWarning();
		}
		this.m_isTransitioning = false;
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00009654 File Offset: 0x00007854
	private void OnSubSceneTransitionFinished()
	{
		if (AdventureConfig.Get().CurrentSubScene == AdventureData.Adventuresubscene.CHOOSER && !SceneMgr.Get().IsTransitioning())
		{
			this.m_isTransitioning = false;
		}
	}

	// Token: 0x06000191 RID: 401 RVA: 0x00009678 File Offset: 0x00007878
	private void ChangeSubScene()
	{
		this.m_AttemptedLoad = true;
		this.m_ChooseButton.SetText(GameStrings.Get("GLUE_LOADING"));
		this.m_ChooseButton.Disable(false);
		this.m_BackButton.SetEnabled(false, false);
		foreach (ChooserButton chooserButton in this.m_ChooserButtons)
		{
			chooserButton.SetEnabled(false, false);
		}
		base.StartCoroutine(this.WaitThenChangeSubScene());
	}

	// Token: 0x06000192 RID: 402 RVA: 0x0000970C File Offset: 0x0000790C
	private IEnumerator WaitThenChangeSubScene()
	{
		yield return null;
		AdventureConfig.Get().ChangeSubSceneToSelectedAdventure();
		yield break;
	}

	// Token: 0x06000193 RID: 403 RVA: 0x00009714 File Offset: 0x00007914
	private void OnAdventureProgressUpdated(bool isStartupAction, AdventureMission.WingProgress oldProgress, AdventureMission.WingProgress newProgress, object userData)
	{
		if (newProgress == null)
		{
			return;
		}
		if ((oldProgress != null && oldProgress.IsOwned()) || !newProgress.IsOwned())
		{
			return;
		}
		if (GameDbf.Wing.GetRecord(newProgress.Wing) == null)
		{
			return;
		}
		foreach (ChooserButton chooserButton in this.m_ChooserButtons)
		{
			ChooserSubButton[] subButtons = chooserButton.GetSubButtons();
			for (int i = 0; i < subButtons.Length; i++)
			{
				AdventureChooserSubButton adventureChooserSubButton = subButtons[i] as AdventureChooserSubButton;
				if (adventureChooserSubButton == null)
				{
					Debug.LogErrorFormat("AdventureChooserTray: Button is either null or not of type AdventureChooserSubButton: {0}", new object[]
					{
						adventureChooserSubButton
					});
				}
				else
				{
					adventureChooserSubButton.ShowRemainingProgressCount();
					bool flag = AdventureConfig.CanPlayMode(adventureChooserSubButton.GetAdventure(), adventureChooserSubButton.GetMode(), true);
					adventureChooserSubButton.SetDesaturate(!flag);
				}
			}
		}
	}

	// Token: 0x04000102 RID: 258
	private const string s_DefaultPortraitMaterialTextureName = "_MainTex";

	// Token: 0x04000103 RID: 259
	private const int s_DefaultPortraitMaterialIndex = 0;

	// Token: 0x04000104 RID: 260
	[CustomEditField(Sections = "Sub Scene")]
	[SerializeField]
	public AdventureSubScene m_ParentSubScene;

	// Token: 0x04000105 RID: 261
	[CustomEditField(Sections = "Choose Frame")]
	[SerializeField]
	public GameObject m_ComingSoonCoverUpSign;

	// Token: 0x04000106 RID: 262
	[CustomEditField(Sections = "Choose Frame")]
	[SerializeField]
	public UberText m_ComingSoonCoverUpSignHeaderText;

	// Token: 0x04000107 RID: 263
	[CustomEditField(Sections = "Choose Frame")]
	[SerializeField]
	public UberText m_ComingSoonCoverUpSignDescriptionText;

	// Token: 0x04000108 RID: 264
	private AdventureChooserDescription m_CurrentChooserDescription;

	// Token: 0x04000109 RID: 265
	private Map<AdventureDbId, Map<AdventureModeDbId, AdventureChooserDescription>> m_Descriptions = new Map<AdventureDbId, Map<AdventureModeDbId, AdventureChooserDescription>>();

	// Token: 0x0400010A RID: 266
	private bool m_isTransitioning = true;
}
