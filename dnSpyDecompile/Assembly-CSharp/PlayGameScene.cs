using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000688 RID: 1672
public abstract class PlayGameScene : PegasusScene
{
	// Token: 0x06005D8D RID: 23949 RVA: 0x001E6FD6 File Offset: 0x001E51D6
	protected void Start()
	{
		AssetLoader.Get().InstantiatePrefab(this.GetScreenPath(), new PrefabCallback<GameObject>(this.OnUIScreenLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x06005D8E RID: 23950 RVA: 0x00019DD3 File Offset: 0x00017FD3
	protected void Update()
	{
		Network.Get().ProcessNetwork();
	}

	// Token: 0x06005D8F RID: 23951 RVA: 0x001E6FFC File Offset: 0x001E51FC
	public void OnDeckPickerLoaded(AbsDeckPickerTrayDisplay deckPickerTrayDisplay)
	{
		this.m_deckPickerIsLoaded = true;
		this.m_deckPickerTrayDisplay = deckPickerTrayDisplay;
	}

	// Token: 0x06005D90 RID: 23952
	public abstract string GetScreenPath();

	// Token: 0x06005D91 RID: 23953 RVA: 0x001E700C File Offset: 0x001E520C
	public override void PreUnload()
	{
		if (this.m_deckPickerTrayDisplay == null)
		{
			this.m_deckPickerTrayDisplay = DeckPickerTrayDisplay.Get();
		}
		if (this.m_deckPickerTrayDisplay != null)
		{
			this.m_deckPickerTrayDisplay.PreUnload();
		}
	}

	// Token: 0x06005D92 RID: 23954 RVA: 0x001E7040 File Offset: 0x001E5240
	public override void Unload()
	{
		if (this.m_deckPickerTrayDisplay == null)
		{
			this.m_deckPickerTrayDisplay = DeckPickerTrayDisplay.Get();
		}
		if (this.m_deckPickerTrayDisplay != null)
		{
			this.m_deckPickerTrayDisplay.Unload();
		}
		this.m_deckPickerIsLoaded = false;
	}

	// Token: 0x06005D93 RID: 23955 RVA: 0x001E707B File Offset: 0x001E527B
	private void OnUIScreenLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("PlayGameScene.OnUIScreenLoaded() - failed to load screen {0}", assetRef));
			return;
		}
		base.StartCoroutine(this.WaitForAllToBeLoaded());
	}

	// Token: 0x06005D94 RID: 23956 RVA: 0x001E70A4 File Offset: 0x001E52A4
	private IEnumerator WaitForAllToBeLoaded()
	{
		while (!this.IsLoaded())
		{
			yield return null;
		}
		SceneMgr.Get().NotifySceneLoaded();
		yield break;
	}

	// Token: 0x06005D95 RID: 23957 RVA: 0x001E70B3 File Offset: 0x001E52B3
	protected virtual bool IsLoaded()
	{
		return this.m_deckPickerIsLoaded;
	}

	// Token: 0x04004F16 RID: 20246
	private bool m_deckPickerIsLoaded;

	// Token: 0x04004F17 RID: 20247
	private AbsDeckPickerTrayDisplay m_deckPickerTrayDisplay;
}
