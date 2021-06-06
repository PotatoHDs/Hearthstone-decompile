using System;
using UnityEngine;

// Token: 0x020002DE RID: 734
public class FatalErrorScene : PegasusScene
{
	// Token: 0x0600266A RID: 9834 RVA: 0x000C1010 File Offset: 0x000BF210
	protected override void Awake()
	{
		AssetLoader.Get().InstantiatePrefab("FatalErrorScreen.prefab:b1524cacda5324547ac72995309dad14", new PrefabCallback<GameObject>(this.OnFatalErrorScreenLoaded), null, AssetLoadingOptions.None);
		base.Awake();
		Navigation.Clear();
		Network network;
		if (HearthstoneServices.TryGet<Network>(out network))
		{
			network.AppAbort();
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

	// Token: 0x0600266B RID: 9835 RVA: 0x000C10A3 File Offset: 0x000BF2A3
	private void Start()
	{
		SceneMgr.Get().NotifySceneLoaded();
	}

	// Token: 0x0600266C RID: 9836 RVA: 0x000C10AF File Offset: 0x000BF2AF
	public override void Unload()
	{
		UserAttentionManager.StopBlocking(UserAttentionBlocker.FATAL_ERROR_SCENE);
	}

	// Token: 0x0600266D RID: 9837 RVA: 0x000C10B7 File Offset: 0x000BF2B7
	private void OnFatalErrorScreenLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			base.gameObject.AddComponent<FatalErrorDialog>();
		}
	}
}
