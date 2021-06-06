using System;
using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020002F9 RID: 761
public class FriendlyDisplay : MonoBehaviour
{
	// Token: 0x06002868 RID: 10344 RVA: 0x000CB0BD File Offset: 0x000C92BD
	private void Awake()
	{
		FriendlyDisplay.s_instance = this;
		this.InitHeroPicker();
	}

	// Token: 0x06002869 RID: 10345 RVA: 0x000CB0CB File Offset: 0x000C92CB
	private void OnDestroy()
	{
		FriendlyDisplay.s_instance = null;
		this.m_deckPickerTray.RemovePlayButtonListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnPlayButtonPressed));
	}

	// Token: 0x0600286A RID: 10346 RVA: 0x000CB0EB File Offset: 0x000C92EB
	public static FriendlyDisplay Get()
	{
		return FriendlyDisplay.s_instance;
	}

	// Token: 0x0600286B RID: 10347 RVA: 0x000CB0F2 File Offset: 0x000C92F2
	public void Unload()
	{
		this.m_deckPickerTray.RemovePlayButtonListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnPlayButtonPressed));
	}

	// Token: 0x0600286C RID: 10348 RVA: 0x000CB10C File Offset: 0x000C930C
	private void OnTrayControllerReady(VisualController trayController)
	{
		if (trayController == null)
		{
			Debug.LogError("trayController was null in OnTrayControllerReady!");
			return;
		}
		this.OnDeckPickerTrayLoaded(trayController.Owner.transform.parent.gameObject);
	}

	// Token: 0x0600286D RID: 10349 RVA: 0x000CB140 File Offset: 0x000C9340
	private void OnDeckPickerTrayLoaded(GameObject go)
	{
		if (go == null)
		{
			Debug.LogError("Unable to load DeckPickerTray.");
			return;
		}
		this.m_deckPickerTray = go.GetComponentInChildren<AbsDeckPickerTrayDisplay>();
		if (this.m_deckPickerTray == null)
		{
			Debug.LogError("AbsDeckPickerTrayDisplay component not found in AbsDeckPickerTray object.");
			return;
		}
		if (this.m_deckPickerTrayContainer == null)
		{
			Debug.LogError("deckPickerTrayContainer was not set in the prefab.");
			return;
		}
		GameUtils.SetParent(go, this.m_deckPickerTrayContainer, false);
		this.DisableOtherModeStuff();
		NetCache.Get().RegisterScreenFriendly(null);
		MusicManager.Get().StartPlaylist(FriendChallengeMgr.Get().IsChallengeTavernBrawl() ? MusicPlaylistType.UI_TavernBrawl : MusicPlaylistType.UI_Friendly);
		this.m_deckPickerTray.SetHeaderText(GameStrings.Get(FriendChallengeMgr.Get().IsChallengeTavernBrawl() ? "GLOBAL_TAVERN_BRAWL" : "GLOBAL_FRIEND_CHALLENGE_TITLE"));
		this.m_deckPickerTray.InitAssets();
		this.m_deckPickerTray.AddPlayButtonListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnPlayButtonPressed));
	}

	// Token: 0x0600286E RID: 10350 RVA: 0x000CB228 File Offset: 0x000C9428
	private void DisableOtherModeStuff()
	{
		if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY)
		{
			return;
		}
		Camera camera = CameraUtils.FindFullScreenEffectsCamera(true);
		if (camera != null)
		{
			camera.GetComponent<FullScreenEffects>().Disable();
		}
	}

	// Token: 0x0600286F RID: 10351 RVA: 0x000CB260 File Offset: 0x000C9460
	private void OnPlayButtonPressed(UIEvent uiEvent)
	{
		if (DeckPickerTrayDisplay.Get() != null)
		{
			CollectionDeck selectedCollectionDeck = DeckPickerTrayDisplay.Get().GetSelectedCollectionDeck();
			if (selectedCollectionDeck != null)
			{
				Log.Decks.PrintInfo("Finding Friendly Game With Deck:", Array.Empty<object>());
				selectedCollectionDeck.LogDeckStringInformation();
			}
		}
	}

	// Token: 0x06002870 RID: 10352 RVA: 0x000CB2A4 File Offset: 0x000C94A4
	private void InitHeroPicker()
	{
		bool flag = GuestHeroPickerDisplay.Get() != null || HeroPickerDisplay.Get() != null;
		int num = -1;
		if (FriendChallengeMgr.Get().IsChallengeTavernBrawl())
		{
			num = TavernBrawlManager.Get().CurrentMission().missionId;
		}
		if (flag)
		{
			Log.All.PrintWarning("Attempting to load HeroPickerDisplay a second time!", Array.Empty<object>());
			return;
		}
		List<ScenarioGuestHeroesDbfRecord> list = null;
		if (num > 0)
		{
			list = GameUtils.GetScenarioGuestHeroes(num);
		}
		if (list != null && list.Count > 0)
		{
			this.m_guestHeroTrayControllerReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnTrayControllerReady));
			this.m_guestHeroTrayControllerReference_phone.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnTrayControllerReady));
			if (this.m_guestHeroPickerWidgetContainer != null)
			{
				this.m_guestHeroPickerWidgetContainer.SetActive(true);
				return;
			}
		}
		else
		{
			AssetLoader.Get().InstantiatePrefab(UniversalInputManager.UsePhoneUI ? "DeckPickerTray_phone.prefab:a30124f640b5b92459bf820a4e3b1ca7" : "DeckPickerTray.prefab:3e13b59cdca14074bbce2b7d903ed895", delegate(AssetReference name, GameObject go, object data)
			{
				this.OnDeckPickerTrayLoaded(go);
			}, null, AssetLoadingOptions.IgnorePrefabPosition);
			if (this.m_guestHeroPickerWidgetContainer != null && this.m_guestHeroPickerWidgetContainer.activeInHierarchy)
			{
				this.m_guestHeroPickerWidgetContainer.SetActive(false);
				Log.All.PrintError("The guest hero picker was activated (loaded) in Friendly.prefab even though we aren't using guest heroes.", Array.Empty<object>());
			}
		}
	}

	// Token: 0x040016F7 RID: 5879
	public AsyncReference m_guestHeroTrayControllerReference;

	// Token: 0x040016F8 RID: 5880
	public AsyncReference m_guestHeroTrayControllerReference_phone;

	// Token: 0x040016F9 RID: 5881
	public GameObject m_deckPickerTrayContainer;

	// Token: 0x040016FA RID: 5882
	public GameObject m_guestHeroPickerWidgetContainer;

	// Token: 0x040016FB RID: 5883
	private static FriendlyDisplay s_instance;

	// Token: 0x040016FC RID: 5884
	private AbsDeckPickerTrayDisplay m_deckPickerTray;
}
