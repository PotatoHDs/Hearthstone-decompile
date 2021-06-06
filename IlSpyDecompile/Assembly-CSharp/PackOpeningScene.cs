using UnityEngine;

public class PackOpeningScene : PegasusScene
{
	private PackOpening m_packOpening;

	protected override void Awake()
	{
		base.Awake();
		AssetLoader.Get().InstantiatePrefab("PackOpening.prefab:1eb13e056b6780048bba1ae1c7a250cf", OnUIScreenLoaded);
	}

	private void Update()
	{
		Network.Get().ProcessNetwork();
	}

	private void OnUIScreenLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"PackOpeningScene.OnPackOpeningLoaded() - failed to load {assetRef}");
			return;
		}
		m_packOpening = go.GetComponent<PackOpening>();
		if (m_packOpening == null)
		{
			Debug.LogError($"PackOpeningScene.OnPackOpeningLoaded() - {base.name} did not have a {typeof(PackOpening)} component");
		}
	}
}
