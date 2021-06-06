using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;

// Token: 0x02000899 RID: 2201
public class DisconnectMgr : IService
{
	// Token: 0x06007923 RID: 31011 RVA: 0x00278013 File Offset: 0x00276213
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield break;
	}

	// Token: 0x06007924 RID: 31012 RVA: 0x0027801B File Offset: 0x0027621B
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(GameMgr),
			typeof(Network)
		};
	}

	// Token: 0x06007925 RID: 31013 RVA: 0x00278040 File Offset: 0x00276240
	public void Shutdown()
	{
		SceneMgr sceneMgr;
		if (HearthstoneServices.TryGet<SceneMgr>(out sceneMgr))
		{
			sceneMgr.UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
		}
	}

	// Token: 0x06007926 RID: 31014 RVA: 0x00278069 File Offset: 0x00276269
	public static DisconnectMgr Get()
	{
		return HearthstoneServices.Get<DisconnectMgr>();
	}

	// Token: 0x06007927 RID: 31015 RVA: 0x00278070 File Offset: 0x00276270
	public void DisconnectFromGameplay()
	{
		PerformanceAnalytics performanceAnalytics = PerformanceAnalytics.Get();
		if (performanceAnalytics != null)
		{
			performanceAnalytics.DisconnectEvent(SceneMgr.Get().GetMode().ToString());
		}
		SceneMgr.Mode postDisconnectSceneMode = GameMgr.Get().GetPostDisconnectSceneMode();
		GameMgr.Get().PreparePostGameSceneMode(postDisconnectSceneMode);
		if (postDisconnectSceneMode == SceneMgr.Mode.INVALID)
		{
			Network.Get().ShowBreakingNewsOrError("GLOBAL_ERROR_NETWORK_LOST_GAME_CONNECTION", 0f);
			return;
		}
		if (Network.Get().WasDisconnectRequested())
		{
			SceneMgr.Get().SetNextMode(postDisconnectSceneMode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			return;
		}
		this.ShowGameplayDialog(postDisconnectSceneMode);
	}

	// Token: 0x06007928 RID: 31016 RVA: 0x002780F4 File Offset: 0x002762F4
	private void ShowGameplayDialog(SceneMgr.Mode nextMode)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_ERROR_NETWORK_TITLE");
		popupInfo.m_text = GameStrings.Get("GLOBAL_ERROR_NETWORK_LOST_GAME_CONNECTION");
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.NONE;
		popupInfo.m_layerToUse = new GameLayer?(GameLayer.UI);
		DialogManager.Get().ShowPopup(popupInfo, new DialogManager.DialogProcessCallback(this.OnGameplayDialogProcessed), nextMode);
	}

	// Token: 0x06007929 RID: 31017 RVA: 0x00278158 File Offset: 0x00276358
	private bool OnGameplayDialogProcessed(DialogBase dialog, object userData)
	{
		this.m_dialog = (AlertPopup)dialog;
		SceneMgr.Mode mode = (SceneMgr.Mode)userData;
		SceneMgr.Get().SetNextMode(mode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
		return true;
	}

	// Token: 0x0600792A RID: 31018 RVA: 0x0027819C File Offset: 0x0027639C
	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded), userData);
		this.UpdateGameplayDialog();
	}

	// Token: 0x0600792B RID: 31019 RVA: 0x002781BC File Offset: 0x002763BC
	private void UpdateGameplayDialog()
	{
		if (this.m_dialog != null)
		{
			AlertPopup.PopupInfo info = this.m_dialog.GetInfo();
			info.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			info.m_responseCallback = new AlertPopup.ResponseCallback(this.OnGameplayDialogResponse);
			this.m_dialog.UpdateInfo(info);
		}
	}

	// Token: 0x0600792C RID: 31020 RVA: 0x00278208 File Offset: 0x00276408
	private void OnGameplayDialogResponse(AlertPopup.Response response, object userData)
	{
		this.m_dialog = null;
		if (!Network.IsLoggedIn())
		{
			Network.Get().ShowBreakingNewsOrError("GLOBAL_ERROR_NETWORK_LOST_GAME_CONNECTION", 0f);
			return;
		}
		SpectatorManager.Get().LeaveSpectatorMode();
	}

	// Token: 0x04005E39 RID: 24121
	private AlertPopup m_dialog;
}
