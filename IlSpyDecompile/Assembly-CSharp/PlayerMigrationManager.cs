using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using PegasusUtil;
using UnityEngine;

public class PlayerMigrationManager : IService
{
	public bool RestartRequired { get; private set; }

	public bool IsShowingPlayerMigrationRelogPopup { get; private set; }

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		serviceLocator.Get<Network>().RegisterNetHandler(GenericResponse.PacketID.ID, OnGenericResponse);
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(Network) };
	}

	public void Shutdown()
	{
	}

	public void ShowRestartAlert()
	{
		if (IsShowingPlayerMigrationRelogPopup)
		{
			return;
		}
		IsShowingPlayerMigrationRelogPopup = true;
		if (HearthstoneServices.TryGet<GameMgr>(out var service) && service.IsFindingGame())
		{
			GameMgr.Get().CancelFindGame();
		}
		Log.All.Print("Player Migration is required! Forcing the client to restart.");
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_PLAYER_MIGRATION_RESTART_HEADER");
		popupInfo.m_text = GameStrings.Get("GLOBAL_PLAYER_MIGRATION_RESTART_BODY");
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_disableBnetBar = true;
		popupInfo.m_blurWhenShown = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_responseCallback = delegate
		{
			if ((bool)HearthstoneApplication.AllowResetFromFatalError)
			{
				HearthstoneApplication.Get().Reset();
			}
			else
			{
				HearthstoneApplication.Get().Exit();
			}
		};
		DialogManager.Get().ShowPopup(popupInfo);
		TelemetryManager.Client().SendRestartDueToPlayerMigration();
	}

	public static PlayerMigrationManager Get()
	{
		return HearthstoneServices.Get<PlayerMigrationManager>();
	}

	public bool CheckForPlayerMigrationRequired()
	{
		if (IsShowingPlayerMigrationRelogPopup)
		{
			return true;
		}
		if (!RestartRequired)
		{
			return false;
		}
		if (SceneMgr.Get() == null || SceneMgr.Get().IsInGame())
		{
			return false;
		}
		return true;
	}

	private void OnGenericResponse()
	{
		Network.GenericResponse genericResponse = Network.Get().GetGenericResponse();
		if (genericResponse == null)
		{
			Debug.LogError($"PlayerMigrationManager - GenericResponse parse error");
		}
		else if (Network.GenericResponse.Result.RESULT_DATA_MIGRATION_REQUIRED == genericResponse.ResultCode)
		{
			RestartRequired = true;
			if (CheckForPlayerMigrationRequired())
			{
				ShowRestartAlert();
			}
		}
	}
}
