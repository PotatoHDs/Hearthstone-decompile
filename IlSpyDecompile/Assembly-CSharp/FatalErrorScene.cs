using UnityEngine;

public class FatalErrorScene : PegasusScene
{
	protected override void Awake()
	{
		AssetLoader.Get().InstantiatePrefab("FatalErrorScreen.prefab:b1524cacda5324547ac72995309dad14", OnFatalErrorScreenLoaded);
		base.Awake();
		Navigation.Clear();
		if (HearthstoneServices.TryGet<Network>(out var service))
		{
			service.AppAbort();
		}
		UserAttentionManager.StartBlocking(UserAttentionBlocker.FATAL_ERROR_SCENE);
		if (DialogManager.Get() != null)
		{
			DialogManager.Get().ClearAllImmediately();
		}
		Camera[] allCameras = Camera.allCameras;
		for (int i = 0; i < allCameras.Length; i++)
		{
			FullScreenEffects component = allCameras[i].GetComponent<FullScreenEffects>();
			if (!(component == null))
			{
				component.Disable();
			}
		}
	}

	private void Start()
	{
		SceneMgr.Get().NotifySceneLoaded();
	}

	public override void Unload()
	{
		UserAttentionManager.StopBlocking(UserAttentionBlocker.FATAL_ERROR_SCENE);
	}

	private void OnFatalErrorScreenLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			base.gameObject.AddComponent<FatalErrorDialog>();
		}
	}
}
