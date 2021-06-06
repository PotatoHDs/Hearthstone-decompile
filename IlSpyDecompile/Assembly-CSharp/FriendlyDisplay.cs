using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

public class FriendlyDisplay : MonoBehaviour
{
	public AsyncReference m_guestHeroTrayControllerReference;

	public AsyncReference m_guestHeroTrayControllerReference_phone;

	public GameObject m_deckPickerTrayContainer;

	public GameObject m_guestHeroPickerWidgetContainer;

	private static FriendlyDisplay s_instance;

	private AbsDeckPickerTrayDisplay m_deckPickerTray;

	private void Awake()
	{
		s_instance = this;
		InitHeroPicker();
	}

	private void OnDestroy()
	{
		s_instance = null;
		m_deckPickerTray.RemovePlayButtonListener(UIEventType.RELEASE, OnPlayButtonPressed);
	}

	public static FriendlyDisplay Get()
	{
		return s_instance;
	}

	public void Unload()
	{
		m_deckPickerTray.RemovePlayButtonListener(UIEventType.RELEASE, OnPlayButtonPressed);
	}

	private void OnTrayControllerReady(VisualController trayController)
	{
		if (trayController == null)
		{
			Debug.LogError("trayController was null in OnTrayControllerReady!");
		}
		else
		{
			OnDeckPickerTrayLoaded(trayController.Owner.transform.parent.gameObject);
		}
	}

	private void OnDeckPickerTrayLoaded(GameObject go)
	{
		if (go == null)
		{
			Debug.LogError("Unable to load DeckPickerTray.");
			return;
		}
		m_deckPickerTray = go.GetComponentInChildren<AbsDeckPickerTrayDisplay>();
		if (m_deckPickerTray == null)
		{
			Debug.LogError("AbsDeckPickerTrayDisplay component not found in AbsDeckPickerTray object.");
			return;
		}
		if (m_deckPickerTrayContainer == null)
		{
			Debug.LogError("deckPickerTrayContainer was not set in the prefab.");
			return;
		}
		GameUtils.SetParent(go, m_deckPickerTrayContainer);
		DisableOtherModeStuff();
		NetCache.Get().RegisterScreenFriendly(null);
		MusicManager.Get().StartPlaylist(FriendChallengeMgr.Get().IsChallengeTavernBrawl() ? MusicPlaylistType.UI_TavernBrawl : MusicPlaylistType.UI_Friendly);
		m_deckPickerTray.SetHeaderText(GameStrings.Get(FriendChallengeMgr.Get().IsChallengeTavernBrawl() ? "GLOBAL_TAVERN_BRAWL" : "GLOBAL_FRIEND_CHALLENGE_TITLE"));
		m_deckPickerTray.InitAssets();
		m_deckPickerTray.AddPlayButtonListener(UIEventType.RELEASE, OnPlayButtonPressed);
	}

	private void DisableOtherModeStuff()
	{
		if (SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.GAMEPLAY)
		{
			Camera camera = CameraUtils.FindFullScreenEffectsCamera(activeOnly: true);
			if (camera != null)
			{
				camera.GetComponent<FullScreenEffects>().Disable();
			}
		}
	}

	private void OnPlayButtonPressed(UIEvent uiEvent)
	{
		if (DeckPickerTrayDisplay.Get() != null)
		{
			CollectionDeck selectedCollectionDeck = DeckPickerTrayDisplay.Get().GetSelectedCollectionDeck();
			if (selectedCollectionDeck != null)
			{
				Log.Decks.PrintInfo("Finding Friendly Game With Deck:");
				selectedCollectionDeck.LogDeckStringInformation();
			}
		}
	}

	private void InitHeroPicker()
	{
		bool num = GuestHeroPickerDisplay.Get() != null || HeroPickerDisplay.Get() != null;
		int num2 = -1;
		if (FriendChallengeMgr.Get().IsChallengeTavernBrawl())
		{
			num2 = TavernBrawlManager.Get().CurrentMission().missionId;
		}
		if (num)
		{
			Log.All.PrintWarning("Attempting to load HeroPickerDisplay a second time!");
			return;
		}
		List<ScenarioGuestHeroesDbfRecord> list = null;
		if (num2 > 0)
		{
			list = GameUtils.GetScenarioGuestHeroes(num2);
		}
		if (list != null && list.Count > 0)
		{
			m_guestHeroTrayControllerReference.RegisterReadyListener<VisualController>(OnTrayControllerReady);
			m_guestHeroTrayControllerReference_phone.RegisterReadyListener<VisualController>(OnTrayControllerReady);
			if (m_guestHeroPickerWidgetContainer != null)
			{
				m_guestHeroPickerWidgetContainer.SetActive(value: true);
			}
			return;
		}
		AssetLoader.Get().InstantiatePrefab(UniversalInputManager.UsePhoneUI ? "DeckPickerTray_phone.prefab:a30124f640b5b92459bf820a4e3b1ca7" : "DeckPickerTray.prefab:3e13b59cdca14074bbce2b7d903ed895", delegate(AssetReference name, GameObject go, object data)
		{
			OnDeckPickerTrayLoaded(go);
		}, null, AssetLoadingOptions.IgnorePrefabPosition);
		if (m_guestHeroPickerWidgetContainer != null && m_guestHeroPickerWidgetContainer.activeInHierarchy)
		{
			m_guestHeroPickerWidgetContainer.SetActive(value: false);
			Log.All.PrintError("The guest hero picker was activated (loaded) in Friendly.prefab even though we aren't using guest heroes.");
		}
	}
}
