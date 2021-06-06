using System;
using System.Collections;
using Assets;
using UnityEngine;

// Token: 0x020002FB RID: 763
[CustomEditClass]
public class GameModeScene : PegasusScene
{
	// Token: 0x0600288E RID: 10382 RVA: 0x000CBCB8 File Offset: 0x000C9EB8
	private void Start()
	{
		GameSaveDataManager.Get().Request(GameSaveKeyId.GAME_MODE_SCENE, new GameSaveDataManager.OnRequestDataResponseDelegate(this.OnGameSaveDataReceived));
		base.StartCoroutine(this.NotifySceneLoadedWhenReady());
	}

	// Token: 0x0600288F RID: 10383 RVA: 0x00019DD3 File Offset: 0x00017FD3
	private void Update()
	{
		Network.Get().ProcessNetwork();
	}

	// Token: 0x06002890 RID: 10384 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool IsUnloading()
	{
		return false;
	}

	// Token: 0x06002891 RID: 10385 RVA: 0x000CBCE4 File Offset: 0x000C9EE4
	public override void Unload()
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			BnetBar bnetBar = BnetBar.Get();
			if (bnetBar != null)
			{
				bnetBar.ToggleActive(true);
			}
		}
		if (this.m_gameModeDisplayRoot != null && this.m_gameModeDisplayRoot.gameObject != null)
		{
			UnityEngine.Object.Destroy(this.m_gameModeDisplayRoot.gameObject);
		}
	}

	// Token: 0x06002892 RID: 10386 RVA: 0x000CBD44 File Offset: 0x000C9F44
	public override void ExecuteSceneDrivenTransition(Action onTransitionCompleteCallback)
	{
		this.m_gameModeDisplay.ShowSlidingTrayAfterSceneLoad(onTransitionCompleteCallback);
	}

	// Token: 0x06002893 RID: 10387 RVA: 0x000CBD52 File Offset: 0x000C9F52
	private void OnScreenPrefabLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_screenPrefabLoaded = true;
		if (go == null)
		{
			Debug.LogError(string.Format("GameModeScene.OnScreenLoaded() - failed to load screen {0}", assetRef));
			return;
		}
		this.m_gameModeDisplayRoot = go;
	}

	// Token: 0x06002894 RID: 10388 RVA: 0x000CBD7C File Offset: 0x000C9F7C
	private void OnGameSaveDataReceived(bool success)
	{
		this.m_gameSaveDataReceived = true;
	}

	// Token: 0x06002895 RID: 10389 RVA: 0x000CBD85 File Offset: 0x000C9F85
	private IEnumerator NotifySceneLoadedWhenReady()
	{
		while (!this.m_gameSaveDataReceived)
		{
			yield return null;
		}
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.HUB
		});
		AssetLoader.Get().InstantiatePrefab(this.m_screenPrefab, new PrefabCallback<GameObject>(this.OnScreenPrefabLoaded), null, AssetLoadingOptions.None);
		while (!this.m_screenPrefabLoaded)
		{
			yield return null;
		}
		while (this.m_gameModeDisplayRoot == null)
		{
			yield return null;
		}
		while (this.m_gameModeDisplayRoot.GetComponentInChildren<GameModeDisplay>() == null)
		{
			yield return null;
		}
		this.m_gameModeDisplay = this.m_gameModeDisplayRoot.GetComponentInChildren<GameModeDisplay>();
		while (!this.m_gameModeDisplay.IsFinishedLoading)
		{
			yield return null;
		}
		SceneMgr.Get().NotifySceneLoaded();
		yield break;
	}

	// Token: 0x04001712 RID: 5906
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_screenPrefab;

	// Token: 0x04001713 RID: 5907
	private bool m_screenPrefabLoaded;

	// Token: 0x04001714 RID: 5908
	private bool m_gameSaveDataReceived;

	// Token: 0x04001715 RID: 5909
	private GameModeDisplay m_gameModeDisplay;

	// Token: 0x04001716 RID: 5910
	private GameObject m_gameModeDisplayRoot;
}
