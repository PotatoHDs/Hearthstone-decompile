using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

[CustomEditClass]
public class AdventureChooserTray : AccordionMenuTray
{
	private const string s_DefaultPortraitMaterialTextureName = "_MainTex";

	private const int s_DefaultPortraitMaterialIndex = 0;

	[CustomEditField(Sections = "Sub Scene")]
	[SerializeField]
	public AdventureSubScene m_ParentSubScene;

	[CustomEditField(Sections = "Choose Frame")]
	[SerializeField]
	public GameObject m_ComingSoonCoverUpSign;

	[CustomEditField(Sections = "Choose Frame")]
	[SerializeField]
	public UberText m_ComingSoonCoverUpSignHeaderText;

	[CustomEditField(Sections = "Choose Frame")]
	[SerializeField]
	public UberText m_ComingSoonCoverUpSignDescriptionText;

	private AdventureChooserDescription m_CurrentChooserDescription;

	private Map<AdventureDbId, Map<AdventureModeDbId, AdventureChooserDescription>> m_Descriptions = new Map<AdventureDbId, Map<AdventureModeDbId, AdventureChooserDescription>>();

	private bool m_isTransitioning = true;

	private void Awake()
	{
		m_ChooseButton.Disable();
		m_BackButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			OnBackButton();
		});
		m_ChooseButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			ChangeSubScene();
		});
		AdventureConfig.Get().AddSelectedModeChangeListener(OnSelectedModeChange);
		AdventureProgressMgr.Get().RegisterProgressUpdatedListener(OnAdventureProgressUpdated);
		Box.Get().AddTransitionFinishedListener(OnBoxTransitionFinished);
		StartCoroutine(InitTrayWhenReady());
	}

	private void Start()
	{
		Navigation.PushUnique(OnNavigateBack);
		m_isStarted = true;
	}

	protected IEnumerator InitTrayWhenReady()
	{
		if (m_ChooseFrameScroller == null || m_ChooseFrameScroller.m_ScrollObject == null)
		{
			Debug.LogError("m_ChooseFrameScroller or m_ChooseFrameScroller.m_ScrollObject cannot be null. Unable to create button.", this);
			yield break;
		}
		bool num = AdventureConfig.Get().PreviousSubScene != AdventureData.Adventuresubscene.INVALID;
		int latestActiveAdventureWing = 0;
		AdventureDbId adventurePlayerShouldSee = AdventureConfig.GetAdventurePlayerShouldSee(out latestActiveAdventureWing);
		if (!num && adventurePlayerShouldSee != 0)
		{
			AdventureConfig.Get().SetSelectedAdventureMode(adventurePlayerShouldSee, AdventureConfig.GetDefaultModeDbIdForAdventure(adventurePlayerShouldSee));
			if (latestActiveAdventureWing != 0)
			{
				GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.PLAYER_OPTIONS, GameSaveKeySubkeyId.LATEST_ADVENTURE_WING_SEEN, latestActiveAdventureWing));
			}
		}
		List<AdventureDef> sortedAdventureDefs = AdventureScene.Get().GetSortedAdventureDefs();
		Map<AdventureDbId, List<AdventureDef>> map = new Map<AdventureDbId, List<AdventureDef>>();
		foreach (AdventureDef item in sortedAdventureDefs)
		{
			AdventureDbId adventureToNestUnder = item.m_AdventureToNestUnder;
			if (adventureToNestUnder != 0)
			{
				if (!map.ContainsKey(adventureToNestUnder))
				{
					map.Add(adventureToNestUnder, new List<AdventureDef>());
				}
				map[adventureToNestUnder].Add(item);
			}
		}
		List<Widget> buttonWidgets = new List<Widget>();
		foreach (AdventureDef item2 in sortedAdventureDefs)
		{
			if (AdventureConfig.ShouldDisplayAdventure(item2.GetAdventureId()) && !item2.IsNestedUnderAnotherAdventureOnChooserScreen)
			{
				List<AdventureDef> nestedAdvDefs = null;
				if (map.ContainsKey(item2.GetAdventureId()))
				{
					nestedAdvDefs = map[item2.GetAdventureId()];
				}
				Widget widget = CreateAdventureChooserButton(item2, nestedAdvDefs);
				if (widget != null)
				{
					buttonWidgets.Add(widget);
				}
			}
		}
		while (!buttonWidgets.TrueForAll((Widget w) => w.IsReady && !w.IsChangingStates))
		{
			yield return null;
		}
		OnButtonVisualUpdated();
		if (m_SelectedSubButton != null && m_ChooseFrameScroller != null)
		{
			m_ChooseFrameScroller.UpdateScroll();
			m_ChooseFrameScroller.CenterObjectInView(m_SelectedSubButton.gameObject, 0f, null, iTween.EaseType.easeOutCubic, 0f);
		}
		if (m_ParentSubScene != null)
		{
			m_ParentSubScene.SetIsLoaded(loaded: true);
			m_ParentSubScene.AddSubSceneTransitionFinishedListener(OnSubSceneTransitionFinished);
		}
		AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
		ShowComingSoonCoverUpSignIfActive(selectedAdventure);
	}

	private void OnDestroy()
	{
		if (AdventureConfig.Get() != null)
		{
			AdventureConfig.Get().RemoveSelectedModeChangeListener(OnSelectedModeChange);
		}
		if (Box.Get() != null)
		{
			Box.Get().RemoveTransitionFinishedListener(OnBoxTransitionFinished);
		}
		if (AdventureProgressMgr.Get() != null)
		{
			AdventureProgressMgr.Get().RemoveProgressUpdatedListener(OnAdventureProgressUpdated);
		}
		CancelInvoke("ShowDisabledAdventureModeRequirementsWarning");
	}

	private void OnBackButton()
	{
		Navigation.GoBack();
	}

	private static bool OnNavigateBack()
	{
		BackToMainMenu();
		return true;
	}

	private static void BackToMainMenu()
	{
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
	}

	private Widget CreateAdventureChooserButton(AdventureDef advDef, List<AdventureDef> nestedAdvDefs)
	{
		string assetString = m_DefaultChooserButtonPrefab;
		if (!string.IsNullOrEmpty(advDef.m_ChooserButtonPrefab))
		{
			assetString = advDef.m_ChooserButtonPrefab;
		}
		Widget widget = WidgetInstance.Create(assetString);
		widget.RegisterReadyListener(delegate
		{
			AdventureChooserButton newbutton = widget.transform.GetComponentInChildren<AdventureChooserButton>();
			if (!(newbutton == null))
			{
				GameUtils.SetParent(widget, m_ChooseFrameScroller.m_ScrollObject);
				AdventureDbId adventureId = advDef.GetAdventureId();
				newbutton.gameObject.name = $"{newbutton.gameObject.name}_{adventureId}";
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
				if (AdventureConfig.IsAdventureComingSoon(advDef.GetAdventureId()) && AreAllAdventuresComingSoon(nestedAdvDefs))
				{
					CreateAdventureChooserComingSoonSubButton(advDef, newbutton);
				}
				else
				{
					CreateAdventureChooserModeSubButtons(advDef, newbutton);
					if (nestedAdvDefs != null)
					{
						nestedAdvDefs.Sort((AdventureDef l, AdventureDef r) => l.GetSortOrder() - r.GetSortOrder());
						foreach (AdventureDef nestedAdvDef in nestedAdvDefs)
						{
							CreateAdventureChooserModeSubButtons(nestedAdvDef, newbutton);
						}
					}
				}
				newbutton.AddVisualUpdatedListener(base.OnButtonVisualUpdated);
				int index = m_ChooserButtons.Count;
				newbutton.AddToggleListener(delegate(bool toggle)
				{
					OnChooserButtonToggled(newbutton, toggle, index);
				});
				newbutton.AddModeSelectionListener(ButtonModeSelected);
				newbutton.AddExpandedListener(ButtonExpanded);
				m_ChooserButtons.Add(newbutton);
				newbutton.FireVisualUpdatedEvent();
			}
		});
		return widget;
	}

	private bool AreAllAdventuresComingSoon(List<AdventureDef> advDefs, bool emptyListDefault = true)
	{
		if (advDefs == null || advDefs.Count == 0)
		{
			return emptyListDefault;
		}
		foreach (AdventureDef advDef in advDefs)
		{
			if (!AdventureConfig.IsAdventureComingSoon(advDef.GetAdventureId()))
			{
				return false;
			}
		}
		return true;
	}

	private void CreateAdventureChooserModeSubButtons(AdventureDef advDef, AdventureChooserButton newbutton)
	{
		List<AdventureSubDef> sortedSubDefs = advDef.GetSortedSubDefs();
		AdventureDbId adventureId = advDef.GetAdventureId();
		AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
		AdventureModeDbId clientChooserAdventureMode = AdventureConfig.Get().GetClientChooserAdventureMode(adventureId);
		string subButtonPrefab = m_DefaultChooserSubButtonPrefab;
		if (!string.IsNullOrEmpty(advDef.m_ChooserSubButtonPrefab))
		{
			subButtonPrefab = advDef.m_ChooserSubButtonPrefab;
		}
		bool flag = true;
		if (advDef.IsNestedUnderAnotherAdventureOnChooserScreen)
		{
			flag = !AdventureConfig.IsAdventureComingSoon(adventureId) && AdventureProgressMgr.Get().IsAdventureComplete(advDef.m_AdventureToNestUnder);
		}
		foreach (AdventureSubDef item in sortedSubDefs)
		{
			AdventureModeDbId adventureModeId = item.GetAdventureModeId();
			AdventureChooserSubButton adventureChooserSubButton = newbutton.CreateSubButton(adventureId, adventureModeId, item, subButtonPrefab, flag && clientChooserAdventureMode == adventureModeId);
			if (!(adventureChooserSubButton == null))
			{
				bool flag2 = newbutton.Toggle && selectedAdventure == advDef.GetAdventureId() && clientChooserAdventureMode == adventureModeId;
				if (flag2)
				{
					adventureChooserSubButton.SetHighlight(enable: true);
					UpdateChooseButton(adventureId, adventureModeId);
					SetTitleText(adventureId, adventureModeId);
					m_SelectedSubButton = adventureChooserSubButton;
				}
				else if (AdventureConfig.IsFeaturedMode(adventureId, adventureModeId))
				{
					adventureChooserSubButton.SetNewGlow(enable: true);
				}
				bool flag3 = AdventureConfig.CanPlayMode(adventureId, adventureModeId);
				adventureChooserSubButton.SetDesaturate(!flag3);
				if (selectedAdventure == AdventureDbId.PRACTICE && adventureModeId == AdventureModeDbId.EXPERT && !flag3)
				{
					adventureChooserSubButton.SetContrast(0.3f);
				}
				CreateAdventureChooserDescriptionFromPrefab(adventureId, item, flag2);
			}
		}
	}

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
		ChooserSubButton chooserSubButton = newbutton.CreateComingSoonSubButton(adventureModeDbId, m_DefaultChooserComingSoonSubButtonPrefab);
		if (chooserSubButton == null)
		{
			Debug.LogError("newSubButton cannot be null. Unable to create newSubButton.", this);
			return;
		}
		if (newbutton.Toggle && clientChooserAdventureMode == adventureModeDbId)
		{
			chooserSubButton.SetHighlight(enable: true);
			UpdateChooseButton(adventureId, adventureModeDbId);
			SetTitleText(adventureId, adventureModeDbId);
			m_SelectedSubButton = chooserSubButton;
		}
		CreateAdventureChooserDescriptionFromPrefab(adventureId, subDef, newbutton.Toggle);
	}

	private void CreateAdventureChooserDescriptionFromPrefab(AdventureDbId adventureId, AdventureSubDef subDef, bool active)
	{
		if (string.IsNullOrEmpty(subDef.m_ChooserDescriptionPrefab))
		{
			return;
		}
		if (!m_Descriptions.TryGetValue(adventureId, out var value))
		{
			value = new Map<AdventureModeDbId, AdventureChooserDescription>();
			m_Descriptions[adventureId] = value;
		}
		string descText = subDef.GetDescription();
		string requiredText = null;
		if (!AdventureConfig.CanPlayMode(adventureId, subDef.GetAdventureModeId(), checkEventTimings: false))
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
		GameUtils.SetParent(adventureChooserDescription, m_DescriptionContainer);
		adventureChooserDescription.SetText(requiredText, descText);
		adventureChooserDescription.m_WidgetElement.RegisterReadyListener(delegate(Widget w)
		{
			if (w != null)
			{
				AdventureChooserDescriptionDataModel dataModel = new AdventureChooserDescriptionDataModel
				{
					Heroes = AdventureUtils.GetAvailableGuestHeroesAsCardListSortedByReleaseDate(adventureId)
				};
				StartCoroutine(UpdateDataModelWhenGameSaveDataIsReady(dataModel, adventureId, subDef.GetAdventureModeId(), active));
				w.BindDataModel(dataModel);
			}
		});
		adventureChooserDescription.gameObject.SetActive(active);
		value[subDef.GetAdventureModeId()] = adventureChooserDescription;
		if (active)
		{
			m_CurrentChooserDescription = adventureChooserDescription;
		}
	}

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
				}
				else
				{
					Log.Adventures.PrintWarning("Unable to set AdventureChooserDescriptionDataModel.HasNewHero - GameSaveData request failed!");
				}
			});
			yield break;
		}
		while (!GameSaveDataManager.Get().IsDataReady(adventureClientKey))
		{
			Log.Adventures.Print("Waiting for client key {0} before updating DataModel for that Adventure Chooser Description!", adventureClientKey);
			yield return null;
		}
		dataModel.HasNewHero = AdventureUtils.DoesAdventureHaveUnseenGuestHeroes(adventureId, modeId);
	}

	private AdventureChooserDescription GetAdventureChooserDescription(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		if (!m_Descriptions.TryGetValue(adventureId, out var value))
		{
			return null;
		}
		if (!value.TryGetValue(modeId, out var value2))
		{
			return null;
		}
		return value2;
	}

	private void ButtonModeSelected(ChooserSubButton btn)
	{
		foreach (ChooserButton chooserButton in m_ChooserButtons)
		{
			chooserButton.DisableSubButtonHighlights();
		}
		AdventureChooserSubButton adventureChooserSubButton = (AdventureChooserSubButton)(m_SelectedSubButton = (AdventureChooserSubButton)btn);
		if (AdventureConfig.MarkFeaturedMode(adventureChooserSubButton.GetAdventure(), adventureChooserSubButton.GetMode()))
		{
			btn.SetNewGlow(enable: false);
		}
		AdventureConfig.Get().SetSelectedAdventureMode(adventureChooserSubButton.GetAdventure(), adventureChooserSubButton.GetMode());
		SetTitleText(adventureChooserSubButton.GetAdventure(), adventureChooserSubButton.GetMode());
	}

	protected void ButtonExpanded(ChooserButton button, bool expand)
	{
		if (!expand)
		{
			return;
		}
		ToggleScrollable(enable: true);
		AdventureChooserButton adventureChooserButton = (AdventureChooserButton)button;
		ChooserSubButton[] subButtons = adventureChooserButton.GetSubButtons();
		foreach (ChooserSubButton chooserSubButton in subButtons)
		{
			AdventureChooserSubButton adventureChooserSubButton = (AdventureChooserSubButton)chooserSubButton;
			if (AdventureConfig.IsFeaturedMode(adventureChooserButton.GetAdventure(), adventureChooserSubButton.GetMode()))
			{
				chooserSubButton.Flash();
			}
			if (AdventureConfig.ShouldShowNewModePopup(adventureChooserButton.GetAdventure(), adventureChooserSubButton.GetMode()))
			{
				StartCoroutine(ShowNewModePopupOnSubButtonAfterScrollingFinished(adventureChooserSubButton));
			}
		}
	}

	private IEnumerator ShowNewModePopupOnSubButtonAfterScrollingFinished(AdventureChooserSubButton subButton)
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		while (m_isTransitioning)
		{
			yield return new WaitForEndOfFrame();
		}
		subButton.ShowNewModePopup(GameStrings.Get("GLUE_ADVENTURE_NEW_MODE_UNLOCKED_POPUP_TEXT"));
		subButton.HideNewModePopupAfterDelay();
		AdventureConfig.MarkHasSeenNewModePopup(subButton.GetAdventure(), subButton.GetMode());
	}

	private void SetTitleText(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)modeId);
		m_DescriptionTitleObject.Text = adventureDataRecord.Name;
	}

	private void OnSelectedModeChange(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		AdventureChooserDescription adventureChooserDescription = GetAdventureChooserDescription(adventureId, modeId);
		if (m_CurrentChooserDescription != adventureChooserDescription)
		{
			if (m_CurrentChooserDescription != null)
			{
				m_CurrentChooserDescription.gameObject.SetActive(value: false);
			}
			m_CurrentChooserDescription = adventureChooserDescription;
			if (m_CurrentChooserDescription != null)
			{
				m_CurrentChooserDescription.gameObject.SetActive(value: true);
			}
		}
		UpdateChooseButton(adventureId, modeId);
		if (m_ChooseButton.IsEnabled())
		{
			PlayMakerFSM component = m_ChooseButton.GetComponent<PlayMakerFSM>();
			if (component != null)
			{
				component.SendEvent("Burst");
			}
		}
		ShowComingSoonCoverUpSignIfActive(adventureId);
		if (!AdventureConfig.CanPlayMode(adventureId, modeId, checkEventTimings: false))
		{
			if (!m_isStarted)
			{
				Invoke("ShowDisabledAdventureModeRequirementsWarning", 0f);
			}
			else
			{
				ShowDisabledAdventureModeRequirementsWarning();
			}
		}
	}

	private void ShowComingSoonCoverUpSignIfActive(AdventureDbId adventureId)
	{
		if (AdventureConfig.IsAdventureComingSoon(adventureId))
		{
			m_ComingSoonCoverUpSign.SetActive(value: true);
			SetComingSoonCoverUpSignText(adventureId);
		}
		else
		{
			m_ComingSoonCoverUpSign.SetActive(value: false);
		}
	}

	private void SetComingSoonCoverUpSignText(AdventureDbId adventureId)
	{
		AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)adventureId);
		m_ComingSoonCoverUpSignHeaderText.Text = record.ComingSoonText;
		SpecialEventType eventType = SpecialEventManager.GetEventType(record.ComingSoonEvent);
		TimeSpan? timeSpan = SpecialEventManager.Get().GetEventEndTimeUtc(eventType) - DateTime.UtcNow;
		if (!timeSpan.HasValue)
		{
			m_ComingSoonCoverUpSignDescriptionText.Text = GameStrings.Get("GLOBAL_DATETIME_COMING_SOON");
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
		m_ComingSoonCoverUpSignDescriptionText.Text = TimeUtils.GetElapsedTimeString((long)timeSpan.Value.TotalSeconds, stringSet, roundUp: true);
	}

	private void ShowDisabledAdventureModeRequirementsWarning()
	{
		CancelInvoke("ShowDisabledAdventureModeRequirementsWarning");
		if (!m_isStarted || SceneMgr.Get().GetMode() != SceneMgr.Mode.ADVENTURE || !(m_ChooseButton != null) || m_ChooseButton.IsEnabled())
		{
			return;
		}
		AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
		AdventureModeDbId selectedMode = AdventureConfig.Get().GetSelectedMode();
		if (!AdventureConfig.CanPlayMode(selectedAdventure, selectedMode, checkEventTimings: false))
		{
			int modeId = (int)selectedMode;
			string text = GameUtils.GetAdventureDataRecord((int)selectedAdventure, modeId).RequirementsDescription;
			if (!string.IsNullOrEmpty(text))
			{
				Error.AddWarning(GameStrings.Get("GLUE_ADVENTURE_LOCKED"), text);
			}
		}
	}

	private void UpdateChooseButton(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		if (!m_AttemptedLoad && AdventureConfig.CanPlayMode(adventureId, modeId) && AdventureConfig.IsAdventureEventActive(adventureId))
		{
			m_ChooseButton.SetText(GameStrings.Get("GLOBAL_ADVENTURE_CHOOSE_BUTTON_TEXT"));
			if (!m_ChooseButton.IsEnabled())
			{
				m_ChooseButton.Enable();
			}
		}
		else
		{
			m_ChooseButton.SetText(GameStrings.Get("GLUE_QUEST_LOG_CLASS_LOCKED"));
			m_ChooseButton.Disable();
		}
	}

	private void OnBoxTransitionFinished(object userData)
	{
		if (!m_isStarted || SceneMgr.Get().GetMode() != SceneMgr.Mode.ADVENTURE)
		{
			return;
		}
		if (m_ChooseButton.IsEnabled())
		{
			PlayMakerFSM component = m_ChooseButton.GetComponent<PlayMakerFSM>();
			if (component != null)
			{
				component.SendEvent("Burst");
			}
		}
		else
		{
			ShowDisabledAdventureModeRequirementsWarning();
		}
		m_isTransitioning = false;
	}

	private void OnSubSceneTransitionFinished()
	{
		if (AdventureConfig.Get().CurrentSubScene == AdventureData.Adventuresubscene.CHOOSER && !SceneMgr.Get().IsTransitioning())
		{
			m_isTransitioning = false;
		}
	}

	private void ChangeSubScene()
	{
		m_AttemptedLoad = true;
		m_ChooseButton.SetText(GameStrings.Get("GLUE_LOADING"));
		m_ChooseButton.Disable();
		m_BackButton.SetEnabled(enabled: false);
		foreach (ChooserButton chooserButton in m_ChooserButtons)
		{
			chooserButton.SetEnabled(enabled: false);
		}
		StartCoroutine(WaitThenChangeSubScene());
	}

	private IEnumerator WaitThenChangeSubScene()
	{
		yield return null;
		AdventureConfig.Get().ChangeSubSceneToSelectedAdventure();
	}

	private void OnAdventureProgressUpdated(bool isStartupAction, AdventureMission.WingProgress oldProgress, AdventureMission.WingProgress newProgress, object userData)
	{
		if (newProgress == null || (oldProgress != null && oldProgress.IsOwned()) || !newProgress.IsOwned() || GameDbf.Wing.GetRecord(newProgress.Wing) == null)
		{
			return;
		}
		foreach (ChooserButton chooserButton in m_ChooserButtons)
		{
			ChooserSubButton[] subButtons = chooserButton.GetSubButtons();
			for (int i = 0; i < subButtons.Length; i++)
			{
				AdventureChooserSubButton adventureChooserSubButton = subButtons[i] as AdventureChooserSubButton;
				if (adventureChooserSubButton == null)
				{
					Debug.LogErrorFormat("AdventureChooserTray: Button is either null or not of type AdventureChooserSubButton: {0}", adventureChooserSubButton);
				}
				else
				{
					adventureChooserSubButton.ShowRemainingProgressCount();
					bool flag = AdventureConfig.CanPlayMode(adventureChooserSubButton.GetAdventure(), adventureChooserSubButton.GetMode());
					adventureChooserSubButton.SetDesaturate(!flag);
				}
			}
		}
	}
}
