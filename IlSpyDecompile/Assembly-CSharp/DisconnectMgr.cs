using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;

public class DisconnectMgr : IService
{
	private AlertPopup m_dialog;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[2]
		{
			typeof(GameMgr),
			typeof(Network)
		};
	}

	public void Shutdown()
	{
		if (HearthstoneServices.TryGet<SceneMgr>(out var service))
		{
			service.UnregisterSceneLoadedEvent(OnSceneLoaded);
		}
	}

	public static DisconnectMgr Get()
	{
		return HearthstoneServices.Get<DisconnectMgr>();
	}

	public void DisconnectFromGameplay()
	{
		PerformanceAnalytics.Get()?.DisconnectEvent(SceneMgr.Get().GetMode().ToString());
		SceneMgr.Mode postDisconnectSceneMode = GameMgr.Get().GetPostDisconnectSceneMode();
		GameMgr.Get().PreparePostGameSceneMode(postDisconnectSceneMode);
		if (postDisconnectSceneMode == SceneMgr.Mode.INVALID)
		{
			Network.Get().ShowBreakingNewsOrError("GLOBAL_ERROR_NETWORK_LOST_GAME_CONNECTION");
		}
		else if (Network.Get().WasDisconnectRequested())
		{
			SceneMgr.Get().SetNextMode(postDisconnectSceneMode);
		}
		else
		{
			ShowGameplayDialog(postDisconnectSceneMode);
		}
	}

	private void ShowGameplayDialog(SceneMgr.Mode nextMode)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_ERROR_NETWORK_TITLE");
		popupInfo.m_text = GameStrings.Get("GLOBAL_ERROR_NETWORK_LOST_GAME_CONNECTION");
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.NONE;
		popupInfo.m_layerToUse = GameLayer.UI;
		DialogManager.Get().ShowPopup(popupInfo, OnGameplayDialogProcessed, nextMode);
	}

	private bool OnGameplayDialogProcessed(DialogBase dialog, object userData)
	{
		m_dialog = (AlertPopup)dialog;
		SceneMgr.Mode mode = (SceneMgr.Mode)userData;
		SceneMgr.Get().SetNextMode(mode);
		SceneMgr.Get().RegisterSceneLoadedEvent(OnSceneLoaded);
		return true;
	}

	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		SceneMgr.Get().UnregisterSceneLoadedEvent(OnSceneLoaded, userData);
		UpdateGameplayDialog();
	}

	private void UpdateGameplayDialog()
	{
		if (m_dialog != null)
		{
			AlertPopup.PopupInfo info = m_dialog.GetInfo();
			info.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			info.m_responseCallback = OnGameplayDialogResponse;
			m_dialog.UpdateInfo(info);
		}
	}

	private void OnGameplayDialogResponse(AlertPopup.Response response, object userData)
	{
		m_dialog = null;
		if (!Network.IsLoggedIn())
		{
			Network.Get().ShowBreakingNewsOrError("GLOBAL_ERROR_NETWORK_LOST_GAME_CONNECTION");
		}
		else
		{
			SpectatorManager.Get().LeaveSpectatorMode();
		}
	}
}
