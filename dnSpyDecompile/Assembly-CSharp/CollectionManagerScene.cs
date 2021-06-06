using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000113 RID: 275
[CustomEditClass]
public class CollectionManagerScene : PegasusScene
{
	// Token: 0x060011D5 RID: 4565 RVA: 0x00065D11 File Offset: 0x00063F11
	protected override void Awake()
	{
		base.Awake();
		AssetLoader.Get().InstantiatePrefab(this.m_CollectionManagerPrefab, new PrefabCallback<GameObject>(this.OnUIScreenLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x060011D6 RID: 4566 RVA: 0x00019DD3 File Offset: 0x00017FD3
	private void Update()
	{
		Network.Get().ProcessNetwork();
	}

	// Token: 0x060011D7 RID: 4567 RVA: 0x00065D42 File Offset: 0x00063F42
	public override bool IsUnloading()
	{
		return this.m_unloading;
	}

	// Token: 0x060011D8 RID: 4568 RVA: 0x00065D4A File Offset: 0x00063F4A
	public override void Unload()
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			BnetBar.Get().ToggleActive(true);
		}
		this.m_unloading = true;
		CollectionManager.Get().GetCollectibleDisplay().Unload();
		Network.Get().SendAckCardsSeen();
		this.m_unloading = false;
	}

	// Token: 0x060011D9 RID: 4569 RVA: 0x00065D8A File Offset: 0x00063F8A
	private void OnUIScreenLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("CollectionManagerScene.OnUIScreenLoaded() - failed to load screen {0}", assetRef));
			return;
		}
		base.StartCoroutine(this.NotifySceneLoadedWhenReady());
	}

	// Token: 0x060011DA RID: 4570 RVA: 0x00065DB3 File Offset: 0x00063FB3
	private IEnumerator NotifySceneLoadedWhenReady()
	{
		while (!CollectionManager.Get().GetCollectibleDisplay().IsReady())
		{
			yield return null;
		}
		SceneMgr.Get().NotifySceneLoaded();
		yield break;
	}

	// Token: 0x04000B7A RID: 2938
	private bool m_unloading;

	// Token: 0x04000B7B RID: 2939
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_CollectionManagerPrefab;
}
