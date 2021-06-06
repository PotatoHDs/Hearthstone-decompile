using System;
using System.Collections;
using Assets;
using UnityEngine;

[CustomEditClass]
public class GameModeScene : PegasusScene
{
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_screenPrefab;

	private bool m_screenPrefabLoaded;

	private bool m_gameSaveDataReceived;

	private GameModeDisplay m_gameModeDisplay;

	private GameObject m_gameModeDisplayRoot;

	private void Start()
	{
		GameSaveDataManager.Get().Request(GameSaveKeyId.GAME_MODE_SCENE, OnGameSaveDataReceived);
		StartCoroutine(NotifySceneLoadedWhenReady());
	}

	private void Update()
	{
		Network.Get().ProcessNetwork();
	}

	public override bool IsUnloading()
	{
		return false;
	}

	public override void Unload()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			BnetBar bnetBar = BnetBar.Get();
			if (bnetBar != null)
			{
				bnetBar.ToggleActive(active: true);
			}
		}
		if (m_gameModeDisplayRoot != null && m_gameModeDisplayRoot.gameObject != null)
		{
			UnityEngine.Object.Destroy(m_gameModeDisplayRoot.gameObject);
		}
	}

	public override void ExecuteSceneDrivenTransition(Action onTransitionCompleteCallback)
	{
		m_gameModeDisplay.ShowSlidingTrayAfterSceneLoad(onTransitionCompleteCallback);
	}

	private void OnScreenPrefabLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_screenPrefabLoaded = true;
		if (go == null)
		{
			Debug.LogError($"GameModeScene.OnScreenLoaded() - failed to load screen {assetRef}");
		}
		else
		{
			m_gameModeDisplayRoot = go;
		}
	}

	private void OnGameSaveDataReceived(bool success)
	{
		m_gameSaveDataReceived = true;
	}

	private IEnumerator NotifySceneLoadedWhenReady()
	{
		while (!m_gameSaveDataReceived)
		{
			yield return null;
		}
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.HUB);
		AssetLoader.Get().InstantiatePrefab((string)m_screenPrefab, OnScreenPrefabLoaded);
		while (!m_screenPrefabLoaded)
		{
			yield return null;
		}
		while (m_gameModeDisplayRoot == null)
		{
			yield return null;
		}
		while (m_gameModeDisplayRoot.GetComponentInChildren<GameModeDisplay>() == null)
		{
			yield return null;
		}
		m_gameModeDisplay = m_gameModeDisplayRoot.GetComponentInChildren<GameModeDisplay>();
		while (!m_gameModeDisplay.IsFinishedLoading)
		{
			yield return null;
		}
		SceneMgr.Get().NotifySceneLoaded();
	}
}
