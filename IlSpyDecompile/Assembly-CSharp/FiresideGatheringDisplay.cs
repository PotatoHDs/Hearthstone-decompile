using System;
using System.Collections;
using Assets;
using PegasusShared;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditClass]
public class FiresideGatheringDisplay : MonoBehaviour
{
	[CustomEditField(Sections = "Main Tray Fields")]
	public GameObject m_FiresideGatheringDisplayTray;

	[CustomEditField(Sections = "Main Tray Fields")]
	public GameObject m_TrayContainer;

	[CustomEditField(Sections = "Main Tray Fields")]
	public GameObject m_TavernSignContainer;

	[CustomEditField(Sections = "Main Tray Fields")]
	public Vector3 m_TrayHideOffset;

	[CustomEditField(Sections = "Main Tray Fields")]
	public FiresideGatheringAccordionMenuTray m_AccordionMenuTray;

	[CustomEditField(Sections = "Main Tray Fields", T = EditType.SOUND_PREFAB)]
	public string m_LobbyArriveAudio = "VO_Innkeeper_Male_Dwarf_FSG_LobbyArrive_01.prefab:5071efa83205af742a82b7456b7a6060";

	[CustomEditField(Sections = "Sub Tray Fields")]
	public Vector3_MobileOverride m_TavernBrawlTrayPosition;

	[CustomEditField(Sections = "Sub Tray Fields")]
	public Vector3_MobileOverride m_CollectionManagerTrayPosition;

	[CustomEditField(Sections = "Sub Tray Fields")]
	public Vector3_MobileOverride m_DeckPickerTrayPosition;

	[CustomEditField(Sections = "Main Tray Fields", T = EditType.GAME_OBJECT)]
	public string m_TavernBrawlDisplayPrefab;

	[CustomEditField(Sections = "Opponent Picker Fields")]
	public GameObject m_OpponentPickerTrayContainer;

	[CustomEditField(Sections = "Opponent Picker Fields")]
	public GameObject_MobileOverride m_OpponentPickerTrayPrefab;

	[CustomEditField(Sections = "Opponent Picker Fields")]
	public Vector3 m_OpponentPickerTrayHideOffset;

	[CustomEditField(Sections = "Animation Settings")]
	public float m_trayAnimationTime = 0.5f;

	[CustomEditField(Sections = "Animation Settings")]
	public iTween.EaseType m_trayInEaseType = iTween.EaseType.easeOutBounce;

	[CustomEditField(Sections = "Animation Settings")]
	public iTween.EaseType m_trayOutEaseType = iTween.EaseType.easeOutCubic;

	private static FiresideGatheringDisplay s_instance;

	private FiresideGatheringOpponentPickerTrayDisplay m_opponentPickerTray;

	private Vector3 m_opponentPickerTrayShowPos;

	private DeckPickerTrayDisplay m_deckPickerTray;

	private Vector3 m_trayShowPos;

	private readonly Vector3 m_firesideGatheringDisplayTrayHidePosition = new Vector3(0f, 0f, -5000f);

	private void Awake()
	{
		s_instance = this;
		if (FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.NONE || FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.MAIN_SCREEN)
		{
			FiresideGatheringManager.Get().CurrentFiresideGatheringMode = FiresideGatheringManager.FiresideGatheringMode.MAIN_SCREEN;
		}
		else
		{
			FormatType @enum = Options.Get().GetEnum<FormatType>(Option.FORMAT_TYPE);
			m_AccordionMenuTray.GoToSpecifiedModeAutomatically(FiresideGatheringManager.Get().CurrentFiresideGatheringMode, @enum);
		}
		GameObject gameObject = (GameObject)GameUtils.Instantiate(m_OpponentPickerTrayPrefab, m_OpponentPickerTrayContainer);
		m_opponentPickerTray = gameObject.GetComponent<FiresideGatheringOpponentPickerTrayDisplay>();
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			SceneUtils.SetLayer(m_opponentPickerTray, GameLayer.IgnoreFullScreenEffects);
		}
		m_opponentPickerTray.Init();
		m_opponentPickerTray.gameObject.SetActive(value: false);
		m_opponentPickerTrayShowPos = m_opponentPickerTray.transform.localPosition;
		m_opponentPickerTray.transform.localPosition = GetOpponentPickerHidePosition();
		m_trayShowPos = Vector3.zero;
		m_TrayContainer.transform.localPosition = GetTrayHidePosition();
		bool num = FiresideGatheringManager.Get().ShowSmallSignIfNeeded(m_TavernSignContainer.transform);
		Box.Get().AddTransitionFinishedListener(OnBoxTransitionFinished);
		if (!num)
		{
			FiresideGatheringManager.Get().OnSignClosed += OnTavernSignAnimationComplete;
		}
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_TavernBrawl);
		SetFiresideGatheringPresenceStatus();
	}

	private void OnDestroy()
	{
		s_instance = null;
		FiresideGatheringManager.Get().OnSignClosed -= OnTavernSignAnimationComplete;
		if (Box.Get() != null)
		{
			Box.Get().RemoveTransitionFinishedListener(OnBoxTransitionFinished);
		}
	}

	public static FiresideGatheringDisplay Get()
	{
		return s_instance;
	}

	public Vector3 GetOpponentPickerShowPosition()
	{
		return m_opponentPickerTrayShowPos;
	}

	public Vector3 GetOpponentPickerHidePosition()
	{
		return m_opponentPickerTrayShowPos + m_OpponentPickerTrayHideOffset;
	}

	public Vector3 GetTrayShowPosition()
	{
		return m_trayShowPos;
	}

	public Vector3 GetTrayHidePosition()
	{
		return m_trayShowPos + m_TrayHideOffset;
	}

	public void ShowDeckPickerTray()
	{
		AssetLoader.Get().InstantiatePrefab(UniversalInputManager.UsePhoneUI ? "DeckPickerTray_phone.prefab:a30124f640b5b92459bf820a4e3b1ca7" : "DeckPickerTray.prefab:3e13b59cdca14074bbce2b7d903ed895", delegate(AssetReference name, GameObject go, object data)
		{
			if (go == null)
			{
				Debug.LogError("Unable to load DeckPickerTray.");
			}
			else
			{
				m_deckPickerTray = go.GetComponent<DeckPickerTrayDisplay>();
				if (m_TrayContainer != null)
				{
					GameUtils.SetParent(m_deckPickerTray, m_TrayContainer);
				}
				m_deckPickerTray.InitAssets();
				m_deckPickerTray.SetHeaderText(GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_TITLE"));
				m_deckPickerTray.transform.localPosition = m_DeckPickerTrayPosition;
				Navigation.RemoveHandler(DeckPickerTrayDisplay.OnNavigateBack);
				StartCoroutine(ShowDeckPickerTrayWhenReady());
			}
		}, null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	public void HideDeckPickerTray()
	{
		HideTray(delegate
		{
			if (m_deckPickerTray != null && m_deckPickerTray.gameObject != null)
			{
				UnityEngine.Object.Destroy(m_deckPickerTray.gameObject);
			}
		});
	}

	public void ShowTavernBrawlTray()
	{
		SceneMgr.Get().RegisterSceneLoadedEvent(OnTavernBrawlSceneLoaded);
		SceneManager.LoadSceneAsync("TavernBrawl", LoadSceneMode.Additive);
	}

	public void HideTavernBrawlTray()
	{
		HideTray(delegate
		{
			if (CollectionManager.Get().GetCollectibleDisplay() != null)
			{
				CollectionManager.Get().GetCollectibleDisplay().Unload();
				UnityEngine.Object.Destroy(CollectionManager.Get().GetCollectibleDisplay().gameObject);
			}
			if (TavernBrawlDisplay.Get() != null)
			{
				TavernBrawlDisplay.Get().Unload();
				UnityEngine.Object.Destroy(TavernBrawlDisplay.Get().gameObject);
			}
		});
	}

	public void ShowOpponentPickerTray(Action onTrayHiddenListener)
	{
		FiresideGatheringOpponentPickerTrayDisplay.Get().RegisterTrayHiddenListener(onTrayHiddenListener);
		FiresideGatheringOpponentPickerTrayDisplay.Get().Show();
	}

	private void ShowTray()
	{
		iTween.Stop(m_TrayContainer);
		m_TrayContainer.SetActive(value: true);
		Hashtable args = iTween.Hash("position", GetTrayShowPosition(), "isLocal", true, "time", m_trayAnimationTime, "easetype", m_trayInEaseType, "oncomplete", (Action<object>)delegate
		{
			HideFiresideGatheringDisplayTray(hidden: true);
		}, "delay", 0.001f);
		iTween.MoveTo(m_TrayContainer, args);
		SoundManager.Get().LoadAndPlay("choose_opponent_panel_slide_on.prefab:66491d3d01ed663429ab80daf6a5e880");
	}

	public void HideTray(Action onComplete)
	{
		HideFiresideGatheringDisplayTray(hidden: false);
		iTween.Stop(m_TrayContainer);
		Hashtable args = iTween.Hash("position", GetTrayHidePosition(), "isLocal", true, "time", m_trayAnimationTime, "easetype", m_trayOutEaseType, "oncomplete", (Action<object>)delegate
		{
			onComplete();
		}, "delay", 0.001f);
		iTween.MoveTo(m_TrayContainer, args);
		SoundManager.Get().LoadAndPlay("choose_opponent_panel_slide_off.prefab:3139d09eb94899d41b9bf612649f47bf");
		SetFiresideGatheringPresenceStatus();
	}

	private void HideFiresideGatheringDisplayTray(bool hidden)
	{
		m_FiresideGatheringDisplayTray.transform.localPosition = (hidden ? m_firesideGatheringDisplayTrayHidePosition : Vector3.zero);
	}

	private IEnumerator ShowDeckPickerTrayWhenReady()
	{
		while (!NetCache.Get().IsNetObjectAvailable<NetCache.NetCacheDecks>())
		{
			yield return null;
		}
		while (!CollectionManager.Get().AreAllDeckContentsReady())
		{
			yield return null;
		}
		ShowTray();
	}

	private void OnTavernBrawlSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object callbackData)
	{
		if (!(scene.GetType() != typeof(TavernBrawlScene)))
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(OnTavernBrawlSceneLoaded);
			StartCoroutine(WaitThenShowTavernBrawlTray());
		}
	}

	private IEnumerator WaitThenShowTavernBrawlTray()
	{
		while (TavernBrawlDisplay.Get() == null)
		{
			yield return null;
		}
		if (m_TrayContainer != null)
		{
			GameUtils.SetParent(TavernBrawlDisplay.Get(), m_TrayContainer);
		}
		TavernBrawlDisplay.Get().transform.localPosition = m_TavernBrawlTrayPosition;
		if (TavernBrawlManager.Get().CurrentMission() != null && TavernBrawlManager.Get().CurrentMission().canEditDeck)
		{
			while (CollectionManager.Get().GetCollectibleDisplay() == null)
			{
				yield return null;
			}
			if (m_TrayContainer != null)
			{
				GameUtils.SetParent(CollectionManager.Get().GetCollectibleDisplay(), m_TrayContainer);
			}
			CollectionManager.Get().GetCollectibleDisplay().transform.localPosition = m_CollectionManagerTrayPosition;
		}
		ShowTray();
	}

	private void OnTavernSignAnimationComplete()
	{
		if (FiresideGatheringManager.Get().CurrentFiresideGatheringMode != 0)
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("GLUE_FIRESIDE_GATHERING_INNKEEPER_LOBBY_ARRIVE"), m_LobbyArriveAudio);
		}
	}

	private void SetFiresideGatheringPresenceStatus()
	{
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.HUB);
	}

	private void OnBoxTransitionFinished(object userdata)
	{
		FiresideGatheringManager.Get().EnableTransitionInputBlocker(enabled: false);
	}
}
