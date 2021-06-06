using System.Collections;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class FiresideGatheringScene : PegasusScene
{
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_FiresideGatheringPrefab;

	private bool m_unloading;

	private bool m_collectionLoaded;

	private bool m_tavernBrawlLoaded;

	private bool m_firesideGatheringPrefabLoaded;

	private void Start()
	{
		AssetLoader.Get().InstantiatePrefab((string)m_FiresideGatheringPrefab, OnFiresideGatheringPrefabLoaded);
		TavernBrawlManager.Get().CurrentBrawlType = BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING;
		if (TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING))
		{
			TavernBrawlManager.Get().EnsureAllDataReady(OnFiresideBrawlServerDataReady);
		}
		else
		{
			OnFiresideBrawlServerDataReady();
		}
	}

	private void Update()
	{
		Network.Get().ProcessNetwork();
	}

	public override bool IsUnloading()
	{
		return m_unloading;
	}

	public override void Unload()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			BnetBar.Get().ToggleActive(active: true);
		}
		m_unloading = true;
		m_unloading = false;
	}

	public void OnFiresideGatheringPrefabLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_firesideGatheringPrefabLoaded = true;
		if (go == null)
		{
			Debug.LogError($"TavernBrawlScene.OnTavernBrawlLoaded() - failed to load screen {assetRef}");
		}
	}

	private void OnFiresideBrawlServerDataReady()
	{
		StartCoroutine(NotifySceneLoadedWhenReady());
		TavernBrawlManager.Get().CurrentBrawlType = BrawlType.BRAWL_TYPE_TAVERN_BRAWL;
		if (TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL))
		{
			TavernBrawlManager.Get().EnsureAllDataReady(OnTavernBrawlServerDataReady);
		}
		else
		{
			OnTavernBrawlServerDataReady();
		}
		CollectionManager.Get().RequestDeckContentsForDecksWithoutContentsLoaded(OnCollectionDataReady);
	}

	private void OnTavernBrawlServerDataReady()
	{
		m_tavernBrawlLoaded = true;
		OnAllPresenceDataLoaded();
	}

	private void OnCollectionDataReady()
	{
		m_collectionLoaded = true;
		OnAllPresenceDataLoaded();
	}

	private void OnAllPresenceDataLoaded()
	{
		if (m_collectionLoaded && m_tavernBrawlLoaded)
		{
			FiresideGatheringManager.Get().UpdateDeckValidity();
		}
	}

	private IEnumerator NotifySceneLoadedWhenReady()
	{
		while (!m_firesideGatheringPrefabLoaded)
		{
			yield return 0;
		}
		SceneMgr.Get().NotifySceneLoaded();
	}
}
