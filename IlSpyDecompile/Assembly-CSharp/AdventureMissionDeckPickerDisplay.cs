using UnityEngine;

[CustomEditClass]
public class AdventureMissionDeckPickerDisplay : MonoBehaviour
{
	public GameObject m_deckPickerTrayContainer;

	private DeckPickerTrayDisplay m_deckPickerTray;

	private void Awake()
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(UniversalInputManager.UsePhoneUI ? "DeckPickerTray_phone.prefab:a30124f640b5b92459bf820a4e3b1ca7" : "DeckPickerTray.prefab:3e13b59cdca14074bbce2b7d903ed895", AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogError("Unable to load DeckPickerTray.");
			return;
		}
		m_deckPickerTray = gameObject.GetComponent<DeckPickerTrayDisplay>();
		if (m_deckPickerTray == null)
		{
			Debug.LogError("DeckPickerTrayDisplay component not found in DeckPickerTray object.");
			return;
		}
		if (m_deckPickerTrayContainer != null)
		{
			GameUtils.SetParent(m_deckPickerTray, m_deckPickerTrayContainer);
		}
		m_deckPickerTray.AddDeckTrayLoadedListener(OnTrayLoaded);
		m_deckPickerTray.InitAssets();
		m_deckPickerTray.SetPlayButtonText(GameStrings.Get("GLOBAL_PLAY"));
		AdventureConfig adventureConfig = AdventureConfig.Get();
		AdventureDbId selectedAdventure = adventureConfig.GetSelectedAdventure();
		AdventureModeDbId selectedMode = adventureConfig.GetSelectedMode();
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)selectedAdventure, (int)selectedMode);
		m_deckPickerTray.SetHeaderText(adventureDataRecord.Name);
	}

	private void OnTrayLoaded()
	{
		AdventureSubScene component = GetComponent<AdventureSubScene>();
		if (component != null)
		{
			component.SetIsLoaded(loaded: true);
		}
	}

	public DeckPickerTrayDisplay GetDeckPickerTrayDisplay()
	{
		return m_deckPickerTray;
	}
}
