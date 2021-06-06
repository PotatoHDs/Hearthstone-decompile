using System.Collections;
using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class FiresideGatheringAccordionMenuTray : AccordionMenuTray
{
	[CustomEditField(Sections = "Chooser Button", T = EditType.TEXTURE)]
	public string m_BrawlsButtonTexture;

	[CustomEditField(Sections = "Chooser Buttons")]
	public UnityEngine.Vector2 m_BrawlsTextureTiling;

	[CustomEditField(Sections = "Chooser Buttons")]
	public UnityEngine.Vector2 m_BrawlsTextureOffset;

	[CustomEditField(Sections = "Chooser Button", T = EditType.TEXTURE)]
	public string m_DuelsButtonTexture;

	[CustomEditField(Sections = "Chooser Buttons")]
	public UnityEngine.Vector2 m_DuelsTextureTiling;

	[CustomEditField(Sections = "Chooser Buttons")]
	public UnityEngine.Vector2 m_DuelsTextureOffset;

	[CustomEditField(Sections = "Prefab Bones")]
	public GameObject m_DeckPickerTrayContainer;

	[CustomEditField(Sections = "Choose Frame")]
	public FiresideGatheringPlayButtonLantern m_FiresideGatheringPlayButtonLantern;

	private FiresideGatheringManager.FiresideGatheringMode m_selectedMode;

	private FormatType m_selectedFormatType;

	private DeckPickerTrayDisplay m_deckPickerTrayDisplay;

	private FiresideGatheringChooserButton m_brawlsButton;

	private FiresideGatheringChooserButton m_duelsButton;

	private void Awake()
	{
		m_ChooseButton.Disable();
		m_BackButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			OnBackButton();
		});
		m_ChooseButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			GoToSelectedMode();
		});
		if (m_ChooseFrameScroller == null || m_ChooseFrameScroller.m_ScrollObject == null)
		{
			Debug.LogError("m_ChooseFrameScroller or m_ChooseFrameScroller.m_ScrollObject cannot be null. Unable to create button.", this);
			return;
		}
		CreateChooserButtons();
		SceneMgr.Get().RegisterSceneLoadedEvent(OnSceneLoaded);
		OnButtonVisualUpdated();
		Box.Get().AddTransitionFinishedListener(OnBoxTransitionFinished);
		m_FiresideGatheringPlayButtonLantern.gameObject.SetActive(value: false);
	}

	private void OnDestroy()
	{
		if (Box.Get() != null)
		{
			Box.Get().RemoveTransitionFinishedListener(OnBoxTransitionFinished);
		}
	}

	private void Start()
	{
		Navigation.PushUnique(OnNavigateBack);
		m_isStarted = true;
	}

	private void OnBackButton()
	{
		Navigation.GoBack();
	}

	private static bool OnNavigateBack()
	{
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
		FiresideGatheringManager.Get().CurrentFiresideGatheringMode = FiresideGatheringManager.FiresideGatheringMode.NONE;
		FiresideGatheringManager.Get().ShowReturnToFSGSceneTooltip();
		return true;
	}

	private void GoToSelectedMode()
	{
		GoToSpecifiedModeAutomatically(m_selectedMode, m_selectedFormatType);
	}

	public void GoToSpecifiedModeAutomatically(FiresideGatheringManager.FiresideGatheringMode newMode, FormatType newFormatType)
	{
		m_ChooseButton.SetText(GameStrings.Get("GLUE_LOADING"));
		m_ChooseButton.Disable();
		m_BackButton.SetEnabled(enabled: false);
		m_FiresideGatheringPlayButtonLantern.SetLanternLit(lit: false);
		StartCoroutine(WaitThenGoToSelectedMode(newMode, newFormatType));
	}

	private IEnumerator WaitThenGoToSelectedMode(FiresideGatheringManager.FiresideGatheringMode newMode, FormatType newFormatType)
	{
		yield return null;
		FiresideGatheringManager.Get().CurrentFiresideGatheringMode = newMode;
		if (newMode == FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE_BRAWL || newMode == FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL)
		{
			TavernBrawlManager.Get().CurrentBrawlType = ((newMode != FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL) ? BrawlType.BRAWL_TYPE_TAVERN_BRAWL : BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING);
		}
		if (newMode == FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE)
		{
			Options.SetFormatType(newFormatType);
			FiresideGatheringDisplay.Get().ShowDeckPickerTray();
			Navigation.Push(OnFriendlyBackButtonReleased);
		}
		else
		{
			FiresideGatheringDisplay.Get().ShowTavernBrawlTray();
			Navigation.Push(OnTavernBrawlBackButtonReleased);
		}
	}

	private bool OnFriendlyBackButtonReleased()
	{
		FiresideGatheringDisplay.Get().HideDeckPickerTray();
		ReenableButtons();
		return true;
	}

	private bool OnTavernBrawlBackButtonReleased()
	{
		FiresideGatheringDisplay.Get().HideTavernBrawlTray();
		ReenableButtons();
		return true;
	}

	private void ReenableButtons()
	{
		if (m_SelectedSubButton != null)
		{
			m_ChooseButton.SetText(GameStrings.Get("GLOBAL_ADVENTURE_CHOOSE_BUTTON_TEXT"));
			m_ChooseButton.Enable();
			m_FiresideGatheringPlayButtonLantern.SetLanternLit(lit: true);
		}
		else
		{
			m_ChooseButton.Disable();
			m_ChooseButton.SetText(string.Empty);
			m_FiresideGatheringPlayButtonLantern.SetLanternLit(lit: false);
		}
		m_BackButton.SetEnabled(enabled: true);
	}

	private void OnBoxTransitionFinished(object userData)
	{
		if (m_isStarted && SceneMgr.Get().GetMode() == SceneMgr.Mode.FIRESIDE_GATHERING && m_ChooseButton.IsEnabled())
		{
			PlayMakerFSM component = m_ChooseButton.GetComponent<PlayMakerFSM>();
			if (component != null)
			{
				component.SendEvent("Burst");
			}
		}
	}

	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		SceneMgr.Get().UnregisterSceneLoadedEvent(OnSceneLoaded);
		CreateFiresideBrawlSubButtons(m_brawlsButton);
		m_brawlsButton.Toggle = true;
	}

	private void CreateChooserButtons()
	{
		m_brawlsButton = GameUtils.LoadGameObjectWithComponent<FiresideGatheringChooserButton>(m_DefaultChooserButtonPrefab);
		m_duelsButton = GameUtils.LoadGameObjectWithComponent<FiresideGatheringChooserButton>(m_DefaultChooserButtonPrefab);
		if (!(m_brawlsButton == null) && !(m_duelsButton == null))
		{
			GameUtils.SetParent(m_brawlsButton, m_ChooseFrameScroller.m_ScrollObject);
			GameUtils.SetParent(m_duelsButton, m_ChooseFrameScroller.m_ScrollObject);
			m_brawlsButton.SetButtonText(GameStrings.Get("GLUE_FIRESIDE_GATHERING_BRAWL"));
			m_brawlsButton.SetPortraitTexture(m_BrawlsButtonTexture);
			m_brawlsButton.SetPortraitTiling(m_BrawlsTextureTiling);
			m_brawlsButton.SetPortraitOffset(m_BrawlsTextureOffset);
			m_brawlsButton.ShowLantern();
			m_duelsButton.SetButtonText(GameStrings.Get("GLUE_FIRESIDE_GATHERING_DUELS_BUTTON"));
			m_duelsButton.SetPortraitTexture(m_DuelsButtonTexture);
			m_duelsButton.SetPortraitTiling(m_DuelsTextureTiling);
			m_duelsButton.SetPortraitOffset(m_DuelsTextureOffset);
			m_duelsButton.ShowSwords();
			CreateFriendlyDuelSubButtons(m_duelsButton);
			m_brawlsButton.AddVisualUpdatedListener(base.OnButtonVisualUpdated);
			m_duelsButton.AddVisualUpdatedListener(base.OnButtonVisualUpdated);
			m_brawlsButton.Toggle = false;
			m_duelsButton.Toggle = false;
			m_brawlsButton.AddToggleListener(delegate(bool toggle)
			{
				OnChooserButtonToggled(m_brawlsButton, toggle, 0);
				m_ChooseButton.Disable();
				m_FiresideGatheringPlayButtonLantern.SetLanternLit(lit: false);
			});
			m_brawlsButton.AddModeSelectionListener(ButtonModeSelected);
			m_brawlsButton.AddExpandedListener(ButtonExpanded);
			m_ChooserButtons.Add(m_brawlsButton);
			m_duelsButton.AddToggleListener(delegate(bool toggle)
			{
				OnChooserButtonToggled(m_duelsButton, toggle, 1);
				m_ChooseButton.Disable();
				m_FiresideGatheringPlayButtonLantern.SetLanternLit(lit: false);
			});
			m_duelsButton.AddModeSelectionListener(ButtonModeSelected);
			m_duelsButton.AddExpandedListener(ButtonExpanded);
			m_ChooserButtons.Add(m_duelsButton);
		}
	}

	protected void ButtonExpanded(ChooserButton button, bool expand)
	{
		if (expand)
		{
			ToggleScrollable(enable: true);
		}
	}

	private void ButtonModeSelected(ChooserSubButton btn)
	{
		m_SelectedSubButton = btn;
		FiresideGatheringChooserSubButton firesideGatheringChooserSubButton = (FiresideGatheringChooserSubButton)btn;
		m_selectedFormatType = firesideGatheringChooserSubButton.AssociatedFormatType;
		m_selectedMode = firesideGatheringChooserSubButton.AssociatedMode;
		TavernBrawlManager.Get().GetMission(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)?.SetSelectedBrawlLibraryItemId(firesideGatheringChooserSubButton.AssociatedBrawlLibraryItemId);
		OnSelectedModeChanged();
	}

	private void CreateFiresideBrawlSubButtons(FiresideGatheringChooserButton brawlsButton)
	{
		TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING);
		if (mission != null)
		{
			List<GameContentScenario> currentFsgBrawls = FiresideGatheringManager.Get().CurrentFsgBrawls;
			for (int i = 0; i < currentFsgBrawls.Count; i++)
			{
				GameContentScenario gameContentScenario = currentFsgBrawls[i];
				bool useAsLastSelected = gameContentScenario.LibraryItemId == mission.SelectedBrawlLibraryItemId;
				ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(gameContentScenario.ScenarioId);
				string buttonText = ((record != null) ? ((string)record.Name) : GameStrings.Get("GLUE_TOOLTIP_BUTTON_TAVERN_BRAWL_HEADLINE"));
				FiresideGatheringChooserSubButton firesideGatheringChooserSubButton = brawlsButton.CreateSubButton(m_DefaultChooserSubButtonPrefab, useAsLastSelected);
				firesideGatheringChooserSubButton.SetButtonText(buttonText);
				firesideGatheringChooserSubButton.SetOfficialBrawlRotationIcon(active: false);
				firesideGatheringChooserSubButton.AssociatedFormatType = gameContentScenario.FormatType;
				firesideGatheringChooserSubButton.AssociatedMode = FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL;
				firesideGatheringChooserSubButton.AssociatedBrawlLibraryItemId = gameContentScenario.LibraryItemId;
				firesideGatheringChooserSubButton.SetMaterialFromButtonIndex(0);
			}
		}
	}

	private void CreateFriendlyDuelSubButtons(FiresideGatheringChooserButton duelsButton)
	{
		int num = 0;
		FiresideGatheringChooserSubButton firesideGatheringChooserSubButton = duelsButton.CreateSubButton(m_DefaultChooserSubButtonPrefab, useAsLastSelected: false);
		firesideGatheringChooserSubButton.SetButtonText(GameStrings.Get("GLUE_COLLECTION_DECK_FORMAT_STANDARD"));
		firesideGatheringChooserSubButton.AssociatedFormatType = FormatType.FT_STANDARD;
		firesideGatheringChooserSubButton.AssociatedMode = FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE;
		firesideGatheringChooserSubButton.AssociatedBrawlLibraryItemId = 0;
		firesideGatheringChooserSubButton.SetMaterialFromButtonIndex(num++);
		if (CollectionManager.Get().ShouldAccountSeeStandardWild())
		{
			FiresideGatheringChooserSubButton firesideGatheringChooserSubButton2 = duelsButton.CreateSubButton(m_DefaultChooserSubButtonPrefab, useAsLastSelected: false);
			firesideGatheringChooserSubButton2.SetButtonText(GameStrings.Get("GLUE_COLLECTION_DECK_FORMAT_WILD"));
			firesideGatheringChooserSubButton2.AssociatedFormatType = FormatType.FT_WILD;
			firesideGatheringChooserSubButton2.AssociatedMode = FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE;
			firesideGatheringChooserSubButton2.AssociatedBrawlLibraryItemId = 0;
			firesideGatheringChooserSubButton2.SetMaterialFromButtonIndex(num++);
		}
		if (TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL))
		{
			FiresideGatheringChooserSubButton firesideGatheringChooserSubButton3 = duelsButton.CreateSubButton(m_DefaultChooserSubButtonPrefab, useAsLastSelected: false);
			firesideGatheringChooserSubButton3.SetButtonText(GameStrings.Get("GLOBAL_TAVERN_BRAWL"));
			firesideGatheringChooserSubButton3.AssociatedFormatType = FormatType.FT_UNKNOWN;
			firesideGatheringChooserSubButton3.AssociatedMode = FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE_BRAWL;
			firesideGatheringChooserSubButton3.AssociatedBrawlLibraryItemId = 0;
			firesideGatheringChooserSubButton3.SetMaterialFromButtonIndex(num++);
		}
	}

	private void OnSelectedModeChanged()
	{
		m_ChooseButton.Enable();
		m_ChooseButton.SetText(GameStrings.Get("GLOBAL_ADVENTURE_CHOOSE_BUTTON_TEXT"));
		m_FiresideGatheringPlayButtonLantern.gameObject.SetActive(m_selectedMode == FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL);
		m_FiresideGatheringPlayButtonLantern.SetLanternLit(lit: true);
		if (m_ChooseButton.IsEnabled())
		{
			PlayMakerFSM component = m_ChooseButton.GetComponent<PlayMakerFSM>();
			if (component != null)
			{
				component.SendEvent("Burst");
			}
		}
	}
}
