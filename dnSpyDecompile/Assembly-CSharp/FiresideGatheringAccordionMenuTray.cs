using System;
using System.Collections;
using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

// Token: 0x020002E2 RID: 738
[CustomEditClass]
public class FiresideGatheringAccordionMenuTray : AccordionMenuTray
{
	// Token: 0x06002688 RID: 9864 RVA: 0x000C16AC File Offset: 0x000BF8AC
	private void Awake()
	{
		this.m_ChooseButton.Disable(false);
		this.m_BackButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.OnBackButton();
		});
		this.m_ChooseButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.GoToSelectedMode();
		});
		if (this.m_ChooseFrameScroller == null || this.m_ChooseFrameScroller.m_ScrollObject == null)
		{
			Debug.LogError("m_ChooseFrameScroller or m_ChooseFrameScroller.m_ScrollObject cannot be null. Unable to create button.", this);
			return;
		}
		this.CreateChooserButtons();
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
		base.OnButtonVisualUpdated();
		Box.Get().AddTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxTransitionFinished));
		this.m_FiresideGatheringPlayButtonLantern.gameObject.SetActive(false);
	}

	// Token: 0x06002689 RID: 9865 RVA: 0x000C176D File Offset: 0x000BF96D
	private void OnDestroy()
	{
		if (Box.Get() != null)
		{
			Box.Get().RemoveTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxTransitionFinished));
		}
	}

	// Token: 0x0600268A RID: 9866 RVA: 0x000C1793 File Offset: 0x000BF993
	private void Start()
	{
		Navigation.PushUnique(new Navigation.NavigateBackHandler(FiresideGatheringAccordionMenuTray.OnNavigateBack));
		this.m_isStarted = true;
	}

	// Token: 0x0600268B RID: 9867 RVA: 0x00004EB5 File Offset: 0x000030B5
	private void OnBackButton()
	{
		Navigation.GoBack();
	}

	// Token: 0x0600268C RID: 9868 RVA: 0x000C17AD File Offset: 0x000BF9AD
	private static bool OnNavigateBack()
	{
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		FiresideGatheringManager.Get().CurrentFiresideGatheringMode = FiresideGatheringManager.FiresideGatheringMode.NONE;
		FiresideGatheringManager.Get().ShowReturnToFSGSceneTooltip();
		return true;
	}

	// Token: 0x0600268D RID: 9869 RVA: 0x000C17D2 File Offset: 0x000BF9D2
	private void GoToSelectedMode()
	{
		this.GoToSpecifiedModeAutomatically(this.m_selectedMode, this.m_selectedFormatType);
	}

	// Token: 0x0600268E RID: 9870 RVA: 0x000C17E8 File Offset: 0x000BF9E8
	public void GoToSpecifiedModeAutomatically(FiresideGatheringManager.FiresideGatheringMode newMode, FormatType newFormatType)
	{
		this.m_ChooseButton.SetText(GameStrings.Get("GLUE_LOADING"));
		this.m_ChooseButton.Disable(false);
		this.m_BackButton.SetEnabled(false, false);
		this.m_FiresideGatheringPlayButtonLantern.SetLanternLit(false);
		base.StartCoroutine(this.WaitThenGoToSelectedMode(newMode, newFormatType));
	}

	// Token: 0x0600268F RID: 9871 RVA: 0x000C183E File Offset: 0x000BFA3E
	private IEnumerator WaitThenGoToSelectedMode(FiresideGatheringManager.FiresideGatheringMode newMode, FormatType newFormatType)
	{
		yield return null;
		FiresideGatheringManager.Get().CurrentFiresideGatheringMode = newMode;
		if (newMode == FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE_BRAWL || newMode == FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL)
		{
			TavernBrawlManager.Get().CurrentBrawlType = ((newMode == FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL) ? BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING : BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
		}
		if (newMode == FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE)
		{
			Options.SetFormatType(newFormatType);
			FiresideGatheringDisplay.Get().ShowDeckPickerTray();
			Navigation.Push(new Navigation.NavigateBackHandler(this.OnFriendlyBackButtonReleased));
		}
		else
		{
			FiresideGatheringDisplay.Get().ShowTavernBrawlTray();
			Navigation.Push(new Navigation.NavigateBackHandler(this.OnTavernBrawlBackButtonReleased));
		}
		yield break;
	}

	// Token: 0x06002690 RID: 9872 RVA: 0x000C185B File Offset: 0x000BFA5B
	private bool OnFriendlyBackButtonReleased()
	{
		FiresideGatheringDisplay.Get().HideDeckPickerTray();
		this.ReenableButtons();
		return true;
	}

	// Token: 0x06002691 RID: 9873 RVA: 0x000C186E File Offset: 0x000BFA6E
	private bool OnTavernBrawlBackButtonReleased()
	{
		FiresideGatheringDisplay.Get().HideTavernBrawlTray();
		this.ReenableButtons();
		return true;
	}

	// Token: 0x06002692 RID: 9874 RVA: 0x000C1884 File Offset: 0x000BFA84
	private void ReenableButtons()
	{
		if (this.m_SelectedSubButton != null)
		{
			this.m_ChooseButton.SetText(GameStrings.Get("GLOBAL_ADVENTURE_CHOOSE_BUTTON_TEXT"));
			this.m_ChooseButton.Enable();
			this.m_FiresideGatheringPlayButtonLantern.SetLanternLit(true);
		}
		else
		{
			this.m_ChooseButton.Disable(false);
			this.m_ChooseButton.SetText(string.Empty);
			this.m_FiresideGatheringPlayButtonLantern.SetLanternLit(false);
		}
		this.m_BackButton.SetEnabled(true, false);
	}

	// Token: 0x06002693 RID: 9875 RVA: 0x000C1904 File Offset: 0x000BFB04
	private void OnBoxTransitionFinished(object userData)
	{
		if (!this.m_isStarted || SceneMgr.Get().GetMode() != SceneMgr.Mode.FIRESIDE_GATHERING)
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
	}

	// Token: 0x06002694 RID: 9876 RVA: 0x000C1955 File Offset: 0x000BFB55
	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
		this.CreateFiresideBrawlSubButtons(this.m_brawlsButton);
		this.m_brawlsButton.Toggle = true;
	}

	// Token: 0x06002695 RID: 9877 RVA: 0x000C1988 File Offset: 0x000BFB88
	private void CreateChooserButtons()
	{
		this.m_brawlsButton = GameUtils.LoadGameObjectWithComponent<FiresideGatheringChooserButton>(this.m_DefaultChooserButtonPrefab);
		this.m_duelsButton = GameUtils.LoadGameObjectWithComponent<FiresideGatheringChooserButton>(this.m_DefaultChooserButtonPrefab);
		if (this.m_brawlsButton == null || this.m_duelsButton == null)
		{
			return;
		}
		GameUtils.SetParent(this.m_brawlsButton, this.m_ChooseFrameScroller.m_ScrollObject, false);
		GameUtils.SetParent(this.m_duelsButton, this.m_ChooseFrameScroller.m_ScrollObject, false);
		this.m_brawlsButton.SetButtonText(GameStrings.Get("GLUE_FIRESIDE_GATHERING_BRAWL"));
		this.m_brawlsButton.SetPortraitTexture(this.m_BrawlsButtonTexture);
		this.m_brawlsButton.SetPortraitTiling(this.m_BrawlsTextureTiling);
		this.m_brawlsButton.SetPortraitOffset(this.m_BrawlsTextureOffset);
		this.m_brawlsButton.ShowLantern();
		this.m_duelsButton.SetButtonText(GameStrings.Get("GLUE_FIRESIDE_GATHERING_DUELS_BUTTON"));
		this.m_duelsButton.SetPortraitTexture(this.m_DuelsButtonTexture);
		this.m_duelsButton.SetPortraitTiling(this.m_DuelsTextureTiling);
		this.m_duelsButton.SetPortraitOffset(this.m_DuelsTextureOffset);
		this.m_duelsButton.ShowSwords();
		this.CreateFriendlyDuelSubButtons(this.m_duelsButton);
		this.m_brawlsButton.AddVisualUpdatedListener(new ChooserButton.VisualUpdated(base.OnButtonVisualUpdated));
		this.m_duelsButton.AddVisualUpdatedListener(new ChooserButton.VisualUpdated(base.OnButtonVisualUpdated));
		this.m_brawlsButton.Toggle = false;
		this.m_duelsButton.Toggle = false;
		this.m_brawlsButton.AddToggleListener(delegate(bool toggle)
		{
			base.OnChooserButtonToggled(this.m_brawlsButton, toggle, 0);
			this.m_ChooseButton.Disable(false);
			this.m_FiresideGatheringPlayButtonLantern.SetLanternLit(false);
		});
		this.m_brawlsButton.AddModeSelectionListener(new ChooserButton.ModeSelection(this.ButtonModeSelected));
		this.m_brawlsButton.AddExpandedListener(new ChooserButton.Expanded(this.ButtonExpanded));
		this.m_ChooserButtons.Add(this.m_brawlsButton);
		this.m_duelsButton.AddToggleListener(delegate(bool toggle)
		{
			base.OnChooserButtonToggled(this.m_duelsButton, toggle, 1);
			this.m_ChooseButton.Disable(false);
			this.m_FiresideGatheringPlayButtonLantern.SetLanternLit(false);
		});
		this.m_duelsButton.AddModeSelectionListener(new ChooserButton.ModeSelection(this.ButtonModeSelected));
		this.m_duelsButton.AddExpandedListener(new ChooserButton.Expanded(this.ButtonExpanded));
		this.m_ChooserButtons.Add(this.m_duelsButton);
	}

	// Token: 0x06002696 RID: 9878 RVA: 0x000C1BA6 File Offset: 0x000BFDA6
	protected void ButtonExpanded(ChooserButton button, bool expand)
	{
		if (!expand)
		{
			return;
		}
		base.ToggleScrollable(true);
	}

	// Token: 0x06002697 RID: 9879 RVA: 0x000C1BB4 File Offset: 0x000BFDB4
	private void ButtonModeSelected(ChooserSubButton btn)
	{
		this.m_SelectedSubButton = btn;
		FiresideGatheringChooserSubButton firesideGatheringChooserSubButton = (FiresideGatheringChooserSubButton)btn;
		this.m_selectedFormatType = firesideGatheringChooserSubButton.AssociatedFormatType;
		this.m_selectedMode = firesideGatheringChooserSubButton.AssociatedMode;
		TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING);
		if (mission != null)
		{
			mission.SetSelectedBrawlLibraryItemId(firesideGatheringChooserSubButton.AssociatedBrawlLibraryItemId);
		}
		this.OnSelectedModeChanged();
	}

	// Token: 0x06002698 RID: 9880 RVA: 0x000C1C08 File Offset: 0x000BFE08
	private void CreateFiresideBrawlSubButtons(FiresideGatheringChooserButton brawlsButton)
	{
		TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING);
		if (mission == null)
		{
			return;
		}
		List<GameContentScenario> currentFsgBrawls = FiresideGatheringManager.Get().CurrentFsgBrawls;
		for (int i = 0; i < currentFsgBrawls.Count; i++)
		{
			GameContentScenario gameContentScenario = currentFsgBrawls[i];
			bool useAsLastSelected = gameContentScenario.LibraryItemId == mission.SelectedBrawlLibraryItemId;
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(gameContentScenario.ScenarioId);
			string buttonText = (record != null) ? record.Name : GameStrings.Get("GLUE_TOOLTIP_BUTTON_TAVERN_BRAWL_HEADLINE");
			FiresideGatheringChooserSubButton firesideGatheringChooserSubButton = brawlsButton.CreateSubButton(this.m_DefaultChooserSubButtonPrefab, useAsLastSelected);
			firesideGatheringChooserSubButton.SetButtonText(buttonText);
			firesideGatheringChooserSubButton.SetOfficialBrawlRotationIcon(false);
			firesideGatheringChooserSubButton.AssociatedFormatType = gameContentScenario.FormatType;
			firesideGatheringChooserSubButton.AssociatedMode = FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL;
			firesideGatheringChooserSubButton.AssociatedBrawlLibraryItemId = gameContentScenario.LibraryItemId;
			firesideGatheringChooserSubButton.SetMaterialFromButtonIndex(0);
		}
	}

	// Token: 0x06002699 RID: 9881 RVA: 0x000C1CD4 File Offset: 0x000BFED4
	private void CreateFriendlyDuelSubButtons(FiresideGatheringChooserButton duelsButton)
	{
		int num = 0;
		FiresideGatheringChooserSubButton firesideGatheringChooserSubButton = duelsButton.CreateSubButton(this.m_DefaultChooserSubButtonPrefab, false);
		firesideGatheringChooserSubButton.SetButtonText(GameStrings.Get("GLUE_COLLECTION_DECK_FORMAT_STANDARD"));
		firesideGatheringChooserSubButton.AssociatedFormatType = FormatType.FT_STANDARD;
		firesideGatheringChooserSubButton.AssociatedMode = FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE;
		firesideGatheringChooserSubButton.AssociatedBrawlLibraryItemId = 0;
		firesideGatheringChooserSubButton.SetMaterialFromButtonIndex(num++);
		if (CollectionManager.Get().ShouldAccountSeeStandardWild())
		{
			FiresideGatheringChooserSubButton firesideGatheringChooserSubButton2 = duelsButton.CreateSubButton(this.m_DefaultChooserSubButtonPrefab, false);
			firesideGatheringChooserSubButton2.SetButtonText(GameStrings.Get("GLUE_COLLECTION_DECK_FORMAT_WILD"));
			firesideGatheringChooserSubButton2.AssociatedFormatType = FormatType.FT_WILD;
			firesideGatheringChooserSubButton2.AssociatedMode = FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE;
			firesideGatheringChooserSubButton2.AssociatedBrawlLibraryItemId = 0;
			firesideGatheringChooserSubButton2.SetMaterialFromButtonIndex(num++);
		}
		if (TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL))
		{
			FiresideGatheringChooserSubButton firesideGatheringChooserSubButton3 = duelsButton.CreateSubButton(this.m_DefaultChooserSubButtonPrefab, false);
			firesideGatheringChooserSubButton3.SetButtonText(GameStrings.Get("GLOBAL_TAVERN_BRAWL"));
			firesideGatheringChooserSubButton3.AssociatedFormatType = FormatType.FT_UNKNOWN;
			firesideGatheringChooserSubButton3.AssociatedMode = FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE_BRAWL;
			firesideGatheringChooserSubButton3.AssociatedBrawlLibraryItemId = 0;
			firesideGatheringChooserSubButton3.SetMaterialFromButtonIndex(num++);
		}
	}

	// Token: 0x0600269A RID: 9882 RVA: 0x000C1DB0 File Offset: 0x000BFFB0
	private void OnSelectedModeChanged()
	{
		this.m_ChooseButton.Enable();
		this.m_ChooseButton.SetText(GameStrings.Get("GLOBAL_ADVENTURE_CHOOSE_BUTTON_TEXT"));
		this.m_FiresideGatheringPlayButtonLantern.gameObject.SetActive(this.m_selectedMode == FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL);
		this.m_FiresideGatheringPlayButtonLantern.SetLanternLit(true);
		if (this.m_ChooseButton.IsEnabled())
		{
			PlayMakerFSM component = this.m_ChooseButton.GetComponent<PlayMakerFSM>();
			if (component != null)
			{
				component.SendEvent("Burst");
			}
		}
	}

	// Token: 0x040015DF RID: 5599
	[CustomEditField(Sections = "Chooser Button", T = EditType.TEXTURE)]
	public string m_BrawlsButtonTexture;

	// Token: 0x040015E0 RID: 5600
	[CustomEditField(Sections = "Chooser Buttons")]
	public UnityEngine.Vector2 m_BrawlsTextureTiling;

	// Token: 0x040015E1 RID: 5601
	[CustomEditField(Sections = "Chooser Buttons")]
	public UnityEngine.Vector2 m_BrawlsTextureOffset;

	// Token: 0x040015E2 RID: 5602
	[CustomEditField(Sections = "Chooser Button", T = EditType.TEXTURE)]
	public string m_DuelsButtonTexture;

	// Token: 0x040015E3 RID: 5603
	[CustomEditField(Sections = "Chooser Buttons")]
	public UnityEngine.Vector2 m_DuelsTextureTiling;

	// Token: 0x040015E4 RID: 5604
	[CustomEditField(Sections = "Chooser Buttons")]
	public UnityEngine.Vector2 m_DuelsTextureOffset;

	// Token: 0x040015E5 RID: 5605
	[CustomEditField(Sections = "Prefab Bones")]
	public GameObject m_DeckPickerTrayContainer;

	// Token: 0x040015E6 RID: 5606
	[CustomEditField(Sections = "Choose Frame")]
	public FiresideGatheringPlayButtonLantern m_FiresideGatheringPlayButtonLantern;

	// Token: 0x040015E7 RID: 5607
	private FiresideGatheringManager.FiresideGatheringMode m_selectedMode;

	// Token: 0x040015E8 RID: 5608
	private FormatType m_selectedFormatType;

	// Token: 0x040015E9 RID: 5609
	private DeckPickerTrayDisplay m_deckPickerTrayDisplay;

	// Token: 0x040015EA RID: 5610
	private FiresideGatheringChooserButton m_brawlsButton;

	// Token: 0x040015EB RID: 5611
	private FiresideGatheringChooserButton m_duelsButton;
}
