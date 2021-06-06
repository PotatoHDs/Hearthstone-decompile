using System.Collections;
using UnityEngine;

[CustomEditClass]
public class CollectionManagerScene : PegasusScene
{
	private bool m_unloading;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_CollectionManagerPrefab;

	protected override void Awake()
	{
		base.Awake();
		AssetLoader.Get().InstantiatePrefab((string)m_CollectionManagerPrefab, OnUIScreenLoaded);
	}

	private void Update()
	{
		Network.Get().ProcessNetwork();
	}

	public override bool IsUnloading()
	{
		return m_unloading;
	}

	public override void Unload()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			BnetBar.Get().ToggleActive(active: true);
		}
		m_unloading = true;
		CollectionManager.Get().GetCollectibleDisplay().Unload();
		Network.Get().SendAckCardsSeen();
		m_unloading = false;
	}

	private void OnUIScreenLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"CollectionManagerScene.OnUIScreenLoaded() - failed to load screen {assetRef}");
		}
		else
		{
			StartCoroutine(NotifySceneLoadedWhenReady());
		}
	}

	private IEnumerator NotifySceneLoadedWhenReady()
	{
		while (!CollectionManager.Get().GetCollectibleDisplay().IsReady())
		{
			yield return null;
		}
		SceneMgr.Get().NotifySceneLoaded();
	}
}
