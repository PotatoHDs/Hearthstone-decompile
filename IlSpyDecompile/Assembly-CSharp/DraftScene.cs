using UnityEngine;

public class DraftScene : PegasusScene
{
	private bool m_unloading;

	private GameObject m_loadedUIScreenObject;

	private static readonly Vector3 DRAFT_SCENE_POSITION = new Vector3(-0.5f, 1.27f, 0f);

	private static readonly Vector3 DRAFT_SCENE_POSITION_PHONE = new Vector3(26.1f, 0f, -9.88f);

	public static readonly float DRAFT_SCENE_LOCAL_SCALE_INDEX_PHONE = 1.38f;

	private static readonly Vector3 DRAFT_SCENE_LOCAL_SCALE_PHONE = Vector3.one * DRAFT_SCENE_LOCAL_SCALE_INDEX_PHONE;

	protected override void Awake()
	{
		base.Awake();
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			AssetLoader.Get().InstantiatePrefab("Draft_phone.prefab:a872557dedbdbe04389e66eda39ae7a7", OnPhoneUIScreenLoaded);
		}
		else
		{
			AssetLoader.Get().InstantiatePrefab("Draft.prefab:b005af870d543804588964c20097e43a", OnUIScreenLoaded);
		}
	}

	public override bool IsUnloading()
	{
		return m_unloading;
	}

	public override void Unload()
	{
		m_unloading = true;
		DraftDisplay.Get().Unload();
		Object.Destroy(DraftDisplay.Get().gameObject);
		Object.Destroy(m_loadedUIScreenObject);
		m_unloading = false;
	}

	private void OnUIScreenLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"DraftScene.OnUIScreenLoaded() - failed to load go {assetRef}");
			return;
		}
		m_loadedUIScreenObject = go;
		go.transform.position = DRAFT_SCENE_POSITION;
	}

	private void OnPhoneUIScreenLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"DraftScene.OnUIScreenLoaded() - failed to load go {assetRef}");
			return;
		}
		m_loadedUIScreenObject = go;
		go.transform.position = DRAFT_SCENE_POSITION_PHONE;
		go.transform.localScale = DRAFT_SCENE_LOCAL_SCALE_PHONE;
	}
}
