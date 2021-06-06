using UnityEngine;

public class PracticeDisplay : MonoBehaviour
{
	public GameObject m_deckPickerTrayContainer;

	public GameObject m_practicePickerTrayContainer;

	public GameObject_MobileOverride m_practicePickerTrayPrefab;

	public Vector3_MobileOverride m_practicePickerTrayHideOffset;

	private static PracticeDisplay s_instance;

	private PracticePickerTrayDisplay m_practicePickerTray;

	private Vector3 m_practicePickerTrayShowPos;

	private DeckPickerTrayDisplay m_deckPickerTray;

	private void Awake()
	{
		s_instance = this;
		GameObject gameObject = (GameObject)GameUtils.Instantiate(m_practicePickerTrayPrefab, m_practicePickerTrayContainer);
		m_practicePickerTray = gameObject.GetComponent<PracticePickerTrayDisplay>();
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			SceneUtils.SetLayer(m_practicePickerTray, GameLayer.IgnoreFullScreenEffects);
		}
		AssetLoader.Get().InstantiatePrefab(UniversalInputManager.UsePhoneUI ? "DeckPickerTray_phone.prefab:a30124f640b5b92459bf820a4e3b1ca7" : "DeckPickerTray.prefab:3e13b59cdca14074bbce2b7d903ed895", delegate(AssetReference name, GameObject go, object data)
		{
			if (go == null)
			{
				Debug.LogError("Unable to load DeckPickerTray.");
			}
			else
			{
				m_deckPickerTray = go.GetComponent<DeckPickerTrayDisplay>();
				if (m_deckPickerTray == null)
				{
					Debug.LogError("DeckPickerTrayDisplay component not found in DeckPickerTray object.");
				}
				else
				{
					if (m_deckPickerTrayContainer != null)
					{
						GameUtils.SetParent(m_deckPickerTray, m_deckPickerTrayContainer);
					}
					AdventureSubScene component = GetComponent<AdventureSubScene>();
					if (component != null)
					{
						m_practicePickerTray.AddTrayLoadedListener(delegate
						{
							OnTrayPartLoaded();
							m_practicePickerTray.gameObject.SetActive(value: false);
						});
						m_deckPickerTray.AddDeckTrayLoadedListener(OnTrayPartLoaded);
						if (m_practicePickerTray.IsLoaded() && m_deckPickerTray.IsLoaded())
						{
							component.SetIsLoaded(loaded: true);
						}
					}
					InitializeTrays();
					CheatMgr.Get().RegisterCheatHandler("replaymissions", OnProcessCheat_replaymissions);
					CheatMgr.Get().RegisterCheatHandler("replaymission", OnProcessCheat_replaymissions);
					NetCache.Get().RegisterScreenPractice(OnNetCacheReady);
				}
			}
		}, null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	private void OnTrayPartLoaded()
	{
		AdventureSubScene component = GetComponent<AdventureSubScene>();
		if (component != null)
		{
			component.SetIsLoaded(IsLoaded());
		}
	}

	private void OnDestroy()
	{
		NetCache.Get().UnregisterNetCacheHandler(OnNetCacheReady);
		if (CheatMgr.Get() != null)
		{
			CheatMgr.Get().UnregisterCheatHandler("replaymissions", OnProcessCheat_replaymissions);
			CheatMgr.Get().UnregisterCheatHandler("replaymission", OnProcessCheat_replaymissions);
		}
		s_instance = null;
	}

	public static PracticeDisplay Get()
	{
		return s_instance;
	}

	public bool IsLoaded()
	{
		if (m_practicePickerTray.IsLoaded())
		{
			return m_deckPickerTray.IsLoaded();
		}
		return false;
	}

	private bool OnProcessCheat_replaymissions(string func, string[] args, string rawArgs)
	{
		AssetLoader.Get().InstantiatePrefab("ReplayTutorialDebug.prefab:895d5f9524722b24582e50484279bba1");
		return true;
	}

	public Vector3 GetPracticePickerShowPosition()
	{
		return m_practicePickerTrayShowPos;
	}

	public Vector3 GetPracticePickerHidePosition()
	{
		return m_practicePickerTrayShowPos + m_practicePickerTrayHideOffset;
	}

	private void OnNetCacheReady()
	{
		NetCache.Get().UnregisterNetCacheHandler(OnNetCacheReady);
		if (!NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.Practice && !SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			Error.AddWarningLoc("GLOBAL_FEATURE_DISABLED_TITLE", "GLOBAL_FEATURE_DISABLED_MESSAGE_PRACTICE");
		}
	}

	private void InitializeTrays()
	{
		AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
		int selectedMode = (int)AdventureConfig.Get().GetSelectedMode();
		string headerText = GameUtils.GetAdventureDataRecord((int)selectedAdventure, selectedMode).Name;
		m_deckPickerTray.SetHeaderText(headerText);
		m_deckPickerTray.InitAssets();
		m_practicePickerTray.Init();
		m_practicePickerTrayShowPos = m_practicePickerTray.transform.localPosition;
		m_practicePickerTray.transform.localPosition = GetPracticePickerHidePosition();
	}
}
