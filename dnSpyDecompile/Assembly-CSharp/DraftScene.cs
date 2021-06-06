using System;
using UnityEngine;

// Token: 0x020002BC RID: 700
public class DraftScene : PegasusScene
{
	// Token: 0x060024CE RID: 9422 RVA: 0x000B9484 File Offset: 0x000B7684
	protected override void Awake()
	{
		base.Awake();
		if (UniversalInputManager.UsePhoneUI)
		{
			AssetLoader.Get().InstantiatePrefab("Draft_phone.prefab:a872557dedbdbe04389e66eda39ae7a7", new PrefabCallback<GameObject>(this.OnPhoneUIScreenLoaded), null, AssetLoadingOptions.None);
			return;
		}
		AssetLoader.Get().InstantiatePrefab("Draft.prefab:b005af870d543804588964c20097e43a", new PrefabCallback<GameObject>(this.OnUIScreenLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x060024CF RID: 9423 RVA: 0x000B94EA File Offset: 0x000B76EA
	public override bool IsUnloading()
	{
		return this.m_unloading;
	}

	// Token: 0x060024D0 RID: 9424 RVA: 0x000B94F2 File Offset: 0x000B76F2
	public override void Unload()
	{
		this.m_unloading = true;
		DraftDisplay.Get().Unload();
		UnityEngine.Object.Destroy(DraftDisplay.Get().gameObject);
		UnityEngine.Object.Destroy(this.m_loadedUIScreenObject);
		this.m_unloading = false;
	}

	// Token: 0x060024D1 RID: 9425 RVA: 0x000B9526 File Offset: 0x000B7726
	private void OnUIScreenLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("DraftScene.OnUIScreenLoaded() - failed to load go {0}", assetRef));
			return;
		}
		this.m_loadedUIScreenObject = go;
		go.transform.position = DraftScene.DRAFT_SCENE_POSITION;
	}

	// Token: 0x060024D2 RID: 9426 RVA: 0x000B955C File Offset: 0x000B775C
	private void OnPhoneUIScreenLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("DraftScene.OnUIScreenLoaded() - failed to load go {0}", assetRef));
			return;
		}
		this.m_loadedUIScreenObject = go;
		go.transform.position = DraftScene.DRAFT_SCENE_POSITION_PHONE;
		go.transform.localScale = DraftScene.DRAFT_SCENE_LOCAL_SCALE_PHONE;
	}

	// Token: 0x04001484 RID: 5252
	private bool m_unloading;

	// Token: 0x04001485 RID: 5253
	private GameObject m_loadedUIScreenObject;

	// Token: 0x04001486 RID: 5254
	private static readonly Vector3 DRAFT_SCENE_POSITION = new Vector3(-0.5f, 1.27f, 0f);

	// Token: 0x04001487 RID: 5255
	private static readonly Vector3 DRAFT_SCENE_POSITION_PHONE = new Vector3(26.1f, 0f, -9.88f);

	// Token: 0x04001488 RID: 5256
	public static readonly float DRAFT_SCENE_LOCAL_SCALE_INDEX_PHONE = 1.38f;

	// Token: 0x04001489 RID: 5257
	private static readonly Vector3 DRAFT_SCENE_LOCAL_SCALE_PHONE = Vector3.one * DraftScene.DRAFT_SCENE_LOCAL_SCALE_INDEX_PHONE;
}
