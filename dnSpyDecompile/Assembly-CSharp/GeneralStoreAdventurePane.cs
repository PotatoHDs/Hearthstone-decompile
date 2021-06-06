using System;
using System.Collections.Generic;
using Assets;
using UnityEngine;

// Token: 0x020006F5 RID: 1781
[CustomEditClass]
public class GeneralStoreAdventurePane : GeneralStorePane
{
	// Token: 0x170005E9 RID: 1513
	// (get) Token: 0x0600635D RID: 25437 RVA: 0x00206415 File Offset: 0x00204615
	// (set) Token: 0x0600635E RID: 25438 RVA: 0x0020641D File Offset: 0x0020461D
	[CustomEditField(Sections = "Layout")]
	public Vector3 AdventureButtonSpacing
	{
		get
		{
			return this.m_adventureButtonSpacing;
		}
		set
		{
			this.m_adventureButtonSpacing = value;
			this.UpdateAdventureButtonPositions();
		}
	}

	// Token: 0x0600635F RID: 25439 RVA: 0x0020642C File Offset: 0x0020462C
	private void Awake()
	{
		this.m_adventureContent = (this.m_parentContent as GeneralStoreAdventureContent);
		if (this.m_adventureContent == null)
		{
			Debug.LogError("m_adventureContent is not the correct type: GeneralStoreAdventureContent");
		}
	}

	// Token: 0x06006360 RID: 25440 RVA: 0x00206457 File Offset: 0x00204657
	public override void StoreShown(bool isCurrent)
	{
		if (!this.m_paneInitialized)
		{
			this.m_paneInitialized = true;
			this.SetUpAdventureButtons();
		}
		this.UpdateAdventureButtonPositions();
		this.SetupInitialSelectedAdventure();
		if (AchieveManager.Get().HasUnlockedFeature(Achieve.Unlocks.VANILLA_HEROES))
		{
			AchieveManager.Get().NotifyOfClick(global::Achievement.ClickTriggerType.BUTTON_ADVENTURE);
		}
	}

	// Token: 0x06006361 RID: 25441 RVA: 0x00206494 File Offset: 0x00204694
	protected override void OnRefresh()
	{
		foreach (GeneralStoreAdventureSelectorButton generalStoreAdventureSelectorButton in this.m_adventureButtons)
		{
			generalStoreAdventureSelectorButton.UpdateState();
		}
	}

	// Token: 0x06006362 RID: 25442 RVA: 0x002064E4 File Offset: 0x002046E4
	private void SetUpAdventureButtons()
	{
		foreach (KeyValuePair<int, StoreAdventureDef> keyValuePair in this.m_adventureContent.GetStoreAdventureDefs())
		{
			AdventureDbId adventureId = (AdventureDbId)keyValuePair.Key;
			Network.Bundle bundle;
			StoreManager.Get().GetAvailableAdventureBundle(adventureId, GeneralStoreAdventureContent.REQUIRE_REAL_MONEY_BUNDLE_OPTION, out bundle);
			if (bundle != null)
			{
				string storeButtonPrefab = keyValuePair.Value.m_storeButtonPrefab;
				GameObject gameObject = AssetLoader.Get().InstantiatePrefab(storeButtonPrefab, AssetLoadingOptions.None);
				if (!(gameObject == null))
				{
					GeneralStoreAdventureSelectorButton advButton = gameObject.GetComponent<GeneralStoreAdventureSelectorButton>();
					if (advButton == null)
					{
						Debug.LogError(string.Format("{0} does not contain GeneralStoreAdventureSelectorButton component.", storeButtonPrefab));
						UnityEngine.Object.Destroy(gameObject);
					}
					else
					{
						GameUtils.SetParent(advButton, this.m_paneContainer, true);
						SceneUtils.SetLayer(advButton, this.m_paneContainer.layer);
						advButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
						{
							this.OnAdventureSelectorButtonClicked(advButton, adventureId);
						});
						advButton.SetAdventureId(adventureId);
						this.m_adventureButtons.Add(advButton);
					}
				}
			}
		}
		this.UpdateAdventureButtonPositions();
	}

	// Token: 0x06006363 RID: 25443 RVA: 0x0020664C File Offset: 0x0020484C
	private void OnAdventureSelectorButtonClicked(GeneralStoreAdventureSelectorButton btn, AdventureDbId adventureId)
	{
		if (!this.m_parentContent.IsContentActive())
		{
			return;
		}
		if (!btn.IsAvailable())
		{
			return;
		}
		this.m_adventureContent.SetAdventureId(adventureId, false);
		foreach (GeneralStoreAdventureSelectorButton generalStoreAdventureSelectorButton in this.m_adventureButtons)
		{
			generalStoreAdventureSelectorButton.Unselect();
		}
		btn.Select();
		Options.Get().SetInt(Option.LAST_SELECTED_STORE_ADVENTURE_ID, (int)btn.GetAdventureId());
		if (!string.IsNullOrEmpty(this.m_adventureSelectionSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_adventureSelectionSound);
		}
	}

	// Token: 0x06006364 RID: 25444 RVA: 0x00206700 File Offset: 0x00204900
	private void UpdateAdventureButtonPositions()
	{
		GeneralStoreAdventureSelectorButton[] array = this.m_adventureButtons.ToArray();
		int i = 0;
		int num = 0;
		while (i < array.Length)
		{
			array[i].transform.localPosition = this.m_adventureButtonSpacing * (float)num++;
			i++;
		}
	}

	// Token: 0x06006365 RID: 25445 RVA: 0x00206748 File Offset: 0x00204948
	private void SetupInitialSelectedAdventure()
	{
		AdventureDbId adventureDbId = Options.Get().GetEnum<AdventureDbId>(Option.LAST_SELECTED_STORE_ADVENTURE_ID, AdventureDbId.INVALID);
		Network.Bundle bundle = null;
		StoreManager.Get().GetAvailableAdventureBundle(adventureDbId, GeneralStoreAdventureContent.REQUIRE_REAL_MONEY_BUNDLE_OPTION, out bundle);
		if (bundle == null)
		{
			adventureDbId = AdventureDbId.INVALID;
		}
		foreach (GeneralStoreAdventureSelectorButton generalStoreAdventureSelectorButton in this.m_adventureButtons)
		{
			if (generalStoreAdventureSelectorButton.GetAdventureId() == adventureDbId)
			{
				this.m_adventureContent.SetAdventureId(adventureDbId, false);
				generalStoreAdventureSelectorButton.Select();
			}
			else
			{
				generalStoreAdventureSelectorButton.Unselect();
			}
		}
	}

	// Token: 0x0400526E RID: 21102
	[SerializeField]
	private Vector3 m_adventureButtonSpacing;

	// Token: 0x0400526F RID: 21103
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_adventureSelectionSound;

	// Token: 0x04005270 RID: 21104
	private List<GeneralStoreAdventureSelectorButton> m_adventureButtons = new List<GeneralStoreAdventureSelectorButton>();

	// Token: 0x04005271 RID: 21105
	private GeneralStoreAdventureContent m_adventureContent;

	// Token: 0x04005272 RID: 21106
	private bool m_paneInitialized;
}
