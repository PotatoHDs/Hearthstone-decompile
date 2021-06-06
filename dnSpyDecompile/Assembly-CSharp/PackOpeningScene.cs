using System;
using UnityEngine;

// Token: 0x02000617 RID: 1559
public class PackOpeningScene : PegasusScene
{
	// Token: 0x0600578D RID: 22413 RVA: 0x001CAA73 File Offset: 0x001C8C73
	protected override void Awake()
	{
		base.Awake();
		AssetLoader.Get().InstantiatePrefab("PackOpening.prefab:1eb13e056b6780048bba1ae1c7a250cf", new PrefabCallback<GameObject>(this.OnUIScreenLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x0600578E RID: 22414 RVA: 0x00019DD3 File Offset: 0x00017FD3
	private void Update()
	{
		Network.Get().ProcessNetwork();
	}

	// Token: 0x0600578F RID: 22415 RVA: 0x001CAAA0 File Offset: 0x001C8CA0
	private void OnUIScreenLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("PackOpeningScene.OnPackOpeningLoaded() - failed to load {0}", assetRef));
			return;
		}
		this.m_packOpening = go.GetComponent<PackOpening>();
		if (this.m_packOpening == null)
		{
			Debug.LogError(string.Format("PackOpeningScene.OnPackOpeningLoaded() - {0} did not have a {1} component", base.name, typeof(PackOpening)));
			return;
		}
	}

	// Token: 0x04004B24 RID: 19236
	private PackOpening m_packOpening;
}
