using System.Collections;
using UnityEngine;

public abstract class PlayGameScene : PegasusScene
{
	private bool m_deckPickerIsLoaded;

	private AbsDeckPickerTrayDisplay m_deckPickerTrayDisplay;

	protected void Start()
	{
		AssetLoader.Get().InstantiatePrefab(GetScreenPath(), OnUIScreenLoaded);
	}

	protected void Update()
	{
		Network.Get().ProcessNetwork();
	}

	public void OnDeckPickerLoaded(AbsDeckPickerTrayDisplay deckPickerTrayDisplay)
	{
		m_deckPickerIsLoaded = true;
		m_deckPickerTrayDisplay = deckPickerTrayDisplay;
	}

	public abstract string GetScreenPath();

	public override void PreUnload()
	{
		if (m_deckPickerTrayDisplay == null)
		{
			m_deckPickerTrayDisplay = DeckPickerTrayDisplay.Get();
		}
		if (m_deckPickerTrayDisplay != null)
		{
			m_deckPickerTrayDisplay.PreUnload();
		}
	}

	public override void Unload()
	{
		if (m_deckPickerTrayDisplay == null)
		{
			m_deckPickerTrayDisplay = DeckPickerTrayDisplay.Get();
		}
		if (m_deckPickerTrayDisplay != null)
		{
			m_deckPickerTrayDisplay.Unload();
		}
		m_deckPickerIsLoaded = false;
	}

	private void OnUIScreenLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"PlayGameScene.OnUIScreenLoaded() - failed to load screen {assetRef}");
		}
		else
		{
			StartCoroutine(WaitForAllToBeLoaded());
		}
	}

	private IEnumerator WaitForAllToBeLoaded()
	{
		while (!IsLoaded())
		{
			yield return null;
		}
		SceneMgr.Get().NotifySceneLoaded();
	}

	protected virtual bool IsLoaded()
	{
		return m_deckPickerIsLoaded;
	}
}
