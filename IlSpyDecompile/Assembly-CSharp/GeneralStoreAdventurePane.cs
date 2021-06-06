using System.Collections.Generic;
using Assets;
using UnityEngine;

[CustomEditClass]
public class GeneralStoreAdventurePane : GeneralStorePane
{
	[SerializeField]
	private Vector3 m_adventureButtonSpacing;

	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_adventureSelectionSound;

	private List<GeneralStoreAdventureSelectorButton> m_adventureButtons = new List<GeneralStoreAdventureSelectorButton>();

	private GeneralStoreAdventureContent m_adventureContent;

	private bool m_paneInitialized;

	[CustomEditField(Sections = "Layout")]
	public Vector3 AdventureButtonSpacing
	{
		get
		{
			return m_adventureButtonSpacing;
		}
		set
		{
			m_adventureButtonSpacing = value;
			UpdateAdventureButtonPositions();
		}
	}

	private void Awake()
	{
		m_adventureContent = m_parentContent as GeneralStoreAdventureContent;
		if (m_adventureContent == null)
		{
			Debug.LogError("m_adventureContent is not the correct type: GeneralStoreAdventureContent");
		}
	}

	public override void StoreShown(bool isCurrent)
	{
		if (!m_paneInitialized)
		{
			m_paneInitialized = true;
			SetUpAdventureButtons();
		}
		UpdateAdventureButtonPositions();
		SetupInitialSelectedAdventure();
		if (AchieveManager.Get().HasUnlockedFeature(Achieve.Unlocks.VANILLA_HEROES))
		{
			AchieveManager.Get().NotifyOfClick(Achievement.ClickTriggerType.BUTTON_ADVENTURE);
		}
	}

	protected override void OnRefresh()
	{
		foreach (GeneralStoreAdventureSelectorButton adventureButton in m_adventureButtons)
		{
			adventureButton.UpdateState();
		}
	}

	private void SetUpAdventureButtons()
	{
		foreach (KeyValuePair<int, StoreAdventureDef> storeAdventureDef in m_adventureContent.GetStoreAdventureDefs())
		{
			AdventureDbId adventureId = (AdventureDbId)storeAdventureDef.Key;
			StoreManager.Get().GetAvailableAdventureBundle(adventureId, GeneralStoreAdventureContent.REQUIRE_REAL_MONEY_BUNDLE_OPTION, out var bundle);
			if (bundle == null)
			{
				continue;
			}
			string storeButtonPrefab = storeAdventureDef.Value.m_storeButtonPrefab;
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(storeButtonPrefab);
			if (gameObject == null)
			{
				continue;
			}
			GeneralStoreAdventureSelectorButton advButton = gameObject.GetComponent<GeneralStoreAdventureSelectorButton>();
			if (advButton == null)
			{
				Debug.LogError($"{storeButtonPrefab} does not contain GeneralStoreAdventureSelectorButton component.");
				Object.Destroy(gameObject);
				continue;
			}
			GameUtils.SetParent(advButton, m_paneContainer, withRotation: true);
			SceneUtils.SetLayer(advButton, m_paneContainer.layer);
			advButton.AddEventListener(UIEventType.RELEASE, delegate
			{
				OnAdventureSelectorButtonClicked(advButton, adventureId);
			});
			advButton.SetAdventureId(adventureId);
			m_adventureButtons.Add(advButton);
		}
		UpdateAdventureButtonPositions();
	}

	private void OnAdventureSelectorButtonClicked(GeneralStoreAdventureSelectorButton btn, AdventureDbId adventureId)
	{
		if (!m_parentContent.IsContentActive() || !btn.IsAvailable())
		{
			return;
		}
		m_adventureContent.SetAdventureId(adventureId);
		foreach (GeneralStoreAdventureSelectorButton adventureButton in m_adventureButtons)
		{
			adventureButton.Unselect();
		}
		btn.Select();
		Options.Get().SetInt(Option.LAST_SELECTED_STORE_ADVENTURE_ID, (int)btn.GetAdventureId());
		if (!string.IsNullOrEmpty(m_adventureSelectionSound))
		{
			SoundManager.Get().LoadAndPlay(m_adventureSelectionSound);
		}
	}

	private void UpdateAdventureButtonPositions()
	{
		GeneralStoreAdventureSelectorButton[] array = m_adventureButtons.ToArray();
		int i = 0;
		int num = 0;
		for (; i < array.Length; i++)
		{
			array[i].transform.localPosition = m_adventureButtonSpacing * num++;
		}
	}

	private void SetupInitialSelectedAdventure()
	{
		AdventureDbId adventureDbId = Options.Get().GetEnum(Option.LAST_SELECTED_STORE_ADVENTURE_ID, AdventureDbId.INVALID);
		Network.Bundle bundle = null;
		StoreManager.Get().GetAvailableAdventureBundle(adventureDbId, GeneralStoreAdventureContent.REQUIRE_REAL_MONEY_BUNDLE_OPTION, out bundle);
		if (bundle == null)
		{
			adventureDbId = AdventureDbId.INVALID;
		}
		foreach (GeneralStoreAdventureSelectorButton adventureButton in m_adventureButtons)
		{
			if (adventureButton.GetAdventureId() == adventureDbId)
			{
				m_adventureContent.SetAdventureId(adventureDbId);
				adventureButton.Select();
			}
			else
			{
				adventureButton.Unselect();
			}
		}
	}
}
