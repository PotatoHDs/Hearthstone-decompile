using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

public class GameplayErrorManager : IService
{
	private static GameplayErrorCloud s_messageInstance;

	private GUIStyle m_errorDisplayStyle;

	private string m_message;

	private float m_displaySecsLeft;

	private UberText m_uberText;

	private GameplayErrorManagerData Data { get; set; }

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		LoadResource loadData = new LoadResource("ServiceData/GameplayErrorManagerData", LoadResourceFlags.FailOnError);
		yield return loadData;
		Data = loadData.LoadedAsset as GameplayErrorManagerData;
		serviceLocator.Get<SceneMgr>().RegisterScenePreUnloadEvent(OnPreUnload);
		m_message = "";
		m_errorDisplayStyle = new GUIStyle();
		m_errorDisplayStyle.fontSize = 24;
		m_errorDisplayStyle.fontStyle = FontStyle.Bold;
		m_errorDisplayStyle.alignment = TextAnchor.UpperCenter;
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(SceneMgr) };
	}

	public void Shutdown()
	{
	}

	public static GameplayErrorManager Get()
	{
		return HearthstoneServices.Get<GameplayErrorManager>();
	}

	private void OnPreUnload(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		HideMessage();
	}

	private void LoadUbertextIfNeeded()
	{
		if (s_messageInstance == null || m_uberText == null)
		{
			s_messageInstance = UnityEngine.Object.Instantiate(Data.m_errorMessagePrefab);
			if (s_messageInstance.GetComponent<HSDontDestroyOnLoad>() == null)
			{
				s_messageInstance.gameObject.AddComponent<HSDontDestroyOnLoad>();
			}
			m_uberText = s_messageInstance.gameObject.GetComponentInChildren<UberText>(includeInactive: true);
		}
	}

	public void DisplayMessage(string message)
	{
		LoadUbertextIfNeeded();
		m_message = message;
		m_displaySecsLeft = (float)message.Length * 0.1f;
		if (CollectionManager.Get().IsInEditMode())
		{
			s_messageInstance.transform.localPosition = Data.m_messagePositionInCollectionManager;
		}
		else
		{
			s_messageInstance.transform.localPosition = Data.m_messagePositionInGame;
		}
		m_uberText.gameObject.transform.localPosition = Data.m_mobileTextAdjustment;
		s_messageInstance.ShowMessage(m_message, m_displaySecsLeft);
		SoundManager.Get().LoadAndPlay("UI_no_can_do.prefab:7b1a22774f818544387c0f2ca4fea02c");
	}

	private void HideMessage()
	{
		if (s_messageInstance != null)
		{
			LoadUbertextIfNeeded();
			s_messageInstance.Hide();
		}
	}
}
