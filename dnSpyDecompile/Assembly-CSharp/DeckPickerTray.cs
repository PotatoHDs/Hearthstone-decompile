using System;
using UnityEngine;

// Token: 0x020002A8 RID: 680
public class DeckPickerTray
{
	// Token: 0x06002281 RID: 8833 RVA: 0x000AAC0B File Offset: 0x000A8E0B
	public static DeckPickerTray Get()
	{
		if (DeckPickerTray.s_instance == null)
		{
			DeckPickerTray.s_instance = new DeckPickerTray();
		}
		return DeckPickerTray.s_instance;
	}

	// Token: 0x06002282 RID: 8834 RVA: 0x000AAC23 File Offset: 0x000A8E23
	public static bool IsInitialized()
	{
		return DeckPickerTray.s_instance != null;
	}

	// Token: 0x06002283 RID: 8835 RVA: 0x000AAC2D File Offset: 0x000A8E2D
	public static AbsDeckPickerTrayDisplay GetTray()
	{
		if (DeckPickerTray.s_instance == null)
		{
			return null;
		}
		return DeckPickerTray.s_instance.m_deckPickerTrayDisplay;
	}

	// Token: 0x06002284 RID: 8836 RVA: 0x000AAC42 File Offset: 0x000A8E42
	public void SetDeckPickerTrayDisplayReference(AbsDeckPickerTrayDisplay deckPickerTrayDisplay)
	{
		this.m_deckPickerTrayDisplay = deckPickerTrayDisplay;
	}

	// Token: 0x06002285 RID: 8837 RVA: 0x000AAC4B File Offset: 0x000A8E4B
	public void RegisterHandlers()
	{
		if (this.m_registeredHandlers)
		{
			return;
		}
		GameMgr.Get().RegisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		this.m_registeredHandlers = true;
	}

	// Token: 0x06002286 RID: 8838 RVA: 0x000AAC73 File Offset: 0x000A8E73
	public void UnregisterHandlers()
	{
		if (!this.m_registeredHandlers)
		{
			return;
		}
		GameMgr gameMgr = GameMgr.Get();
		if (gameMgr != null)
		{
			gameMgr.UnregisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		}
		this.m_registeredHandlers = false;
	}

	// Token: 0x06002287 RID: 8839 RVA: 0x000AACA2 File Offset: 0x000A8EA2
	public void Unload()
	{
		this.UnregisterHandlers();
	}

	// Token: 0x06002288 RID: 8840 RVA: 0x000AACAC File Offset: 0x000A8EAC
	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		if (this.m_deckPickerTrayDisplay == null)
		{
			if (DeckPickerTrayDisplay.Get() != null)
			{
				this.m_deckPickerTrayDisplay = DeckPickerTrayDisplay.Get();
			}
			else
			{
				if (!(GuestHeroPickerTrayDisplay.Get() != null))
				{
					Debug.LogError("DeckPickerTray has OnFindGameEvent registered but the HeroPickerTrayDisplay does not exist. Exiting...");
					return false;
				}
				this.m_deckPickerTrayDisplay = GuestHeroPickerTrayDisplay.Get();
			}
		}
		switch (eventData.m_state)
		{
		case FindGameState.CLIENT_CANCELED:
			this.m_deckPickerTrayDisplay.HandleGameStartupFailure();
			break;
		case FindGameState.CLIENT_ERROR:
		case FindGameState.BNET_QUEUE_CANCELED:
		case FindGameState.BNET_ERROR:
			this.m_deckPickerTrayDisplay.HandleGameStartupFailure();
			break;
		case FindGameState.SERVER_GAME_STARTED:
			this.m_deckPickerTrayDisplay.OnServerGameStarted();
			break;
		case FindGameState.SERVER_GAME_CANCELED:
			this.m_deckPickerTrayDisplay.OnServerGameCanceled();
			break;
		}
		return false;
	}

	// Token: 0x0400131F RID: 4895
	private static DeckPickerTray s_instance;

	// Token: 0x04001320 RID: 4896
	private bool m_registeredHandlers;

	// Token: 0x04001321 RID: 4897
	private AbsDeckPickerTrayDisplay m_deckPickerTrayDisplay;
}
