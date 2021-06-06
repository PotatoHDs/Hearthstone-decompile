using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using PegasusUtil;
using UnityEngine;

// Token: 0x0200061C RID: 1564
public class PlayerMigrationManager : IService
{
	// Token: 0x17000523 RID: 1315
	// (get) Token: 0x060057A6 RID: 22438 RVA: 0x001CAF9D File Offset: 0x001C919D
	// (set) Token: 0x060057A7 RID: 22439 RVA: 0x001CAFA5 File Offset: 0x001C91A5
	public bool RestartRequired { get; private set; }

	// Token: 0x17000524 RID: 1316
	// (get) Token: 0x060057A8 RID: 22440 RVA: 0x001CAFAE File Offset: 0x001C91AE
	// (set) Token: 0x060057A9 RID: 22441 RVA: 0x001CAFB6 File Offset: 0x001C91B6
	public bool IsShowingPlayerMigrationRelogPopup { get; private set; }

	// Token: 0x060057AA RID: 22442 RVA: 0x001CAFBF File Offset: 0x001C91BF
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		serviceLocator.Get<Network>().RegisterNetHandler(GenericResponse.PacketID.ID, new Network.NetHandler(this.OnGenericResponse), null);
		yield break;
	}

	// Token: 0x060057AB RID: 22443 RVA: 0x001B7846 File Offset: 0x001B5A46
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(Network)
		};
	}

	// Token: 0x060057AC RID: 22444 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x060057AD RID: 22445 RVA: 0x001CAFD8 File Offset: 0x001C91D8
	public void ShowRestartAlert()
	{
		if (this.IsShowingPlayerMigrationRelogPopup)
		{
			return;
		}
		this.IsShowingPlayerMigrationRelogPopup = true;
		GameMgr gameMgr;
		if (HearthstoneServices.TryGet<GameMgr>(out gameMgr) && gameMgr.IsFindingGame())
		{
			GameMgr.Get().CancelFindGame();
		}
		Log.All.Print("Player Migration is required! Forcing the client to restart.", Array.Empty<object>());
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_PLAYER_MIGRATION_RESTART_HEADER");
		popupInfo.m_text = GameStrings.Get("GLOBAL_PLAYER_MIGRATION_RESTART_BODY");
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_disableBnetBar = true;
		popupInfo.m_blurWhenShown = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
		{
			if (HearthstoneApplication.AllowResetFromFatalError)
			{
				HearthstoneApplication.Get().Reset();
				return;
			}
			HearthstoneApplication.Get().Exit();
		};
		DialogManager.Get().ShowPopup(popupInfo);
		TelemetryManager.Client().SendRestartDueToPlayerMigration();
	}

	// Token: 0x060057AE RID: 22446 RVA: 0x001CB0A8 File Offset: 0x001C92A8
	public static PlayerMigrationManager Get()
	{
		return HearthstoneServices.Get<PlayerMigrationManager>();
	}

	// Token: 0x060057AF RID: 22447 RVA: 0x001CB0AF File Offset: 0x001C92AF
	public bool CheckForPlayerMigrationRequired()
	{
		return this.IsShowingPlayerMigrationRelogPopup || (this.RestartRequired && SceneMgr.Get() != null && !SceneMgr.Get().IsInGame());
	}

	// Token: 0x060057B0 RID: 22448 RVA: 0x001CB0DC File Offset: 0x001C92DC
	private void OnGenericResponse()
	{
		Network.GenericResponse genericResponse = Network.Get().GetGenericResponse();
		if (genericResponse == null)
		{
			Debug.LogError(string.Format("PlayerMigrationManager - GenericResponse parse error", Array.Empty<object>()));
			return;
		}
		if (Network.GenericResponse.Result.RESULT_DATA_MIGRATION_REQUIRED == genericResponse.ResultCode)
		{
			this.RestartRequired = true;
			if (this.CheckForPlayerMigrationRequired())
			{
				this.ShowRestartAlert();
			}
		}
	}
}
