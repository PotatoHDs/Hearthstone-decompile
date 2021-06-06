using UnityEngine;

public class DeckPickerTray
{
	private static DeckPickerTray s_instance;

	private bool m_registeredHandlers;

	private AbsDeckPickerTrayDisplay m_deckPickerTrayDisplay;

	public static DeckPickerTray Get()
	{
		if (s_instance == null)
		{
			s_instance = new DeckPickerTray();
		}
		return s_instance;
	}

	public static bool IsInitialized()
	{
		return s_instance != null;
	}

	public static AbsDeckPickerTrayDisplay GetTray()
	{
		if (s_instance == null)
		{
			return null;
		}
		return s_instance.m_deckPickerTrayDisplay;
	}

	public void SetDeckPickerTrayDisplayReference(AbsDeckPickerTrayDisplay deckPickerTrayDisplay)
	{
		m_deckPickerTrayDisplay = deckPickerTrayDisplay;
	}

	public void RegisterHandlers()
	{
		if (!m_registeredHandlers)
		{
			GameMgr.Get().RegisterFindGameEvent(OnFindGameEvent);
			m_registeredHandlers = true;
		}
	}

	public void UnregisterHandlers()
	{
		if (m_registeredHandlers)
		{
			GameMgr.Get()?.UnregisterFindGameEvent(OnFindGameEvent);
			m_registeredHandlers = false;
		}
	}

	public void Unload()
	{
		UnregisterHandlers();
	}

	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		if (m_deckPickerTrayDisplay == null)
		{
			if (DeckPickerTrayDisplay.Get() != null)
			{
				m_deckPickerTrayDisplay = DeckPickerTrayDisplay.Get();
			}
			else
			{
				if (!(GuestHeroPickerTrayDisplay.Get() != null))
				{
					Debug.LogError("DeckPickerTray has OnFindGameEvent registered but the HeroPickerTrayDisplay does not exist. Exiting...");
					return false;
				}
				m_deckPickerTrayDisplay = GuestHeroPickerTrayDisplay.Get();
			}
		}
		switch (eventData.m_state)
		{
		case FindGameState.CLIENT_CANCELED:
			m_deckPickerTrayDisplay.HandleGameStartupFailure();
			break;
		case FindGameState.CLIENT_ERROR:
		case FindGameState.BNET_QUEUE_CANCELED:
		case FindGameState.BNET_ERROR:
			m_deckPickerTrayDisplay.HandleGameStartupFailure();
			break;
		case FindGameState.SERVER_GAME_STARTED:
			m_deckPickerTrayDisplay.OnServerGameStarted();
			break;
		case FindGameState.SERVER_GAME_CANCELED:
			m_deckPickerTrayDisplay.OnServerGameCanceled();
			break;
		}
		return false;
	}
}
