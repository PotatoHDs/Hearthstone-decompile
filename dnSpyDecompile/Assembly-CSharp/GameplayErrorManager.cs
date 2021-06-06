using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

// Token: 0x0200030E RID: 782
public class GameplayErrorManager : IService
{
	// Token: 0x17000503 RID: 1283
	// (get) Token: 0x06002ACF RID: 10959 RVA: 0x000D7BC3 File Offset: 0x000D5DC3
	// (set) Token: 0x06002AD0 RID: 10960 RVA: 0x000D7BCB File Offset: 0x000D5DCB
	private GameplayErrorManagerData Data { get; set; }

	// Token: 0x06002AD1 RID: 10961 RVA: 0x000D7BD4 File Offset: 0x000D5DD4
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		LoadResource loadData = new LoadResource("ServiceData/GameplayErrorManagerData", LoadResourceFlags.FailOnError);
		yield return loadData;
		this.Data = (loadData.LoadedAsset as GameplayErrorManagerData);
		serviceLocator.Get<SceneMgr>().RegisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnPreUnload));
		this.m_message = "";
		this.m_errorDisplayStyle = new GUIStyle();
		this.m_errorDisplayStyle.fontSize = 24;
		this.m_errorDisplayStyle.fontStyle = FontStyle.Bold;
		this.m_errorDisplayStyle.alignment = TextAnchor.UpperCenter;
		yield break;
	}

	// Token: 0x06002AD2 RID: 10962 RVA: 0x000D7BEA File Offset: 0x000D5DEA
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(SceneMgr)
		};
	}

	// Token: 0x06002AD3 RID: 10963 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x06002AD4 RID: 10964 RVA: 0x000D7BFF File Offset: 0x000D5DFF
	public static GameplayErrorManager Get()
	{
		return HearthstoneServices.Get<GameplayErrorManager>();
	}

	// Token: 0x06002AD5 RID: 10965 RVA: 0x000D7C06 File Offset: 0x000D5E06
	private void OnPreUnload(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		this.HideMessage();
	}

	// Token: 0x06002AD6 RID: 10966 RVA: 0x000D7C10 File Offset: 0x000D5E10
	private void LoadUbertextIfNeeded()
	{
		if (GameplayErrorManager.s_messageInstance == null || this.m_uberText == null)
		{
			GameplayErrorManager.s_messageInstance = UnityEngine.Object.Instantiate<GameplayErrorCloud>(this.Data.m_errorMessagePrefab);
			if (GameplayErrorManager.s_messageInstance.GetComponent<HSDontDestroyOnLoad>() == null)
			{
				GameplayErrorManager.s_messageInstance.gameObject.AddComponent<HSDontDestroyOnLoad>();
			}
			this.m_uberText = GameplayErrorManager.s_messageInstance.gameObject.GetComponentInChildren<UberText>(true);
		}
	}

	// Token: 0x06002AD7 RID: 10967 RVA: 0x000D7C88 File Offset: 0x000D5E88
	public void DisplayMessage(string message)
	{
		this.LoadUbertextIfNeeded();
		this.m_message = message;
		this.m_displaySecsLeft = (float)message.Length * 0.1f;
		if (CollectionManager.Get().IsInEditMode())
		{
			GameplayErrorManager.s_messageInstance.transform.localPosition = this.Data.m_messagePositionInCollectionManager;
		}
		else
		{
			GameplayErrorManager.s_messageInstance.transform.localPosition = this.Data.m_messagePositionInGame;
		}
		this.m_uberText.gameObject.transform.localPosition = this.Data.m_mobileTextAdjustment;
		GameplayErrorManager.s_messageInstance.ShowMessage(this.m_message, this.m_displaySecsLeft);
		SoundManager.Get().LoadAndPlay("UI_no_can_do.prefab:7b1a22774f818544387c0f2ca4fea02c");
	}

	// Token: 0x06002AD8 RID: 10968 RVA: 0x000D7D50 File Offset: 0x000D5F50
	private void HideMessage()
	{
		if (GameplayErrorManager.s_messageInstance != null)
		{
			this.LoadUbertextIfNeeded();
			GameplayErrorManager.s_messageInstance.Hide();
		}
	}

	// Token: 0x040017FF RID: 6143
	private static GameplayErrorCloud s_messageInstance;

	// Token: 0x04001800 RID: 6144
	private GUIStyle m_errorDisplayStyle;

	// Token: 0x04001801 RID: 6145
	private string m_message;

	// Token: 0x04001802 RID: 6146
	private float m_displaySecsLeft;

	// Token: 0x04001803 RID: 6147
	private UberText m_uberText;
}
