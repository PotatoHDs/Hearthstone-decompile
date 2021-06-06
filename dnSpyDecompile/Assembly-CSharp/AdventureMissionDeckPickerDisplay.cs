using System;
using UnityEngine;

// Token: 0x02000043 RID: 67
[CustomEditClass]
public class AdventureMissionDeckPickerDisplay : MonoBehaviour
{
	// Token: 0x06000371 RID: 881 RVA: 0x00015880 File Offset: 0x00013A80
	private void Awake()
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(UniversalInputManager.UsePhoneUI ? "DeckPickerTray_phone.prefab:a30124f640b5b92459bf820a4e3b1ca7" : "DeckPickerTray.prefab:3e13b59cdca14074bbce2b7d903ed895", AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogError("Unable to load DeckPickerTray.");
			return;
		}
		this.m_deckPickerTray = gameObject.GetComponent<DeckPickerTrayDisplay>();
		if (this.m_deckPickerTray == null)
		{
			Debug.LogError("DeckPickerTrayDisplay component not found in DeckPickerTray object.");
			return;
		}
		if (this.m_deckPickerTrayContainer != null)
		{
			GameUtils.SetParent(this.m_deckPickerTray, this.m_deckPickerTrayContainer, false);
		}
		this.m_deckPickerTray.AddDeckTrayLoadedListener(new AbsDeckPickerTrayDisplay.DeckTrayLoaded(this.OnTrayLoaded));
		this.m_deckPickerTray.InitAssets();
		this.m_deckPickerTray.SetPlayButtonText(GameStrings.Get("GLOBAL_PLAY"));
		AdventureConfig adventureConfig = AdventureConfig.Get();
		AdventureDbId selectedAdventure = adventureConfig.GetSelectedAdventure();
		AdventureModeDbId selectedMode = adventureConfig.GetSelectedMode();
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)selectedAdventure, (int)selectedMode);
		this.m_deckPickerTray.SetHeaderText(adventureDataRecord.Name);
	}

	// Token: 0x06000372 RID: 882 RVA: 0x00015978 File Offset: 0x00013B78
	private void OnTrayLoaded()
	{
		AdventureSubScene component = base.GetComponent<AdventureSubScene>();
		if (component != null)
		{
			component.SetIsLoaded(true);
		}
	}

	// Token: 0x06000373 RID: 883 RVA: 0x0001599C File Offset: 0x00013B9C
	public DeckPickerTrayDisplay GetDeckPickerTrayDisplay()
	{
		return this.m_deckPickerTray;
	}

	// Token: 0x04000270 RID: 624
	public GameObject m_deckPickerTrayContainer;

	// Token: 0x04000271 RID: 625
	private DeckPickerTrayDisplay m_deckPickerTray;
}
