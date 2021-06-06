using Hearthstone.DungeonCrawl;
using UnityEngine;

public class AdventureSubSceneDungeonRun : AdventureSubSceneDisplay
{
	private DungeonCrawlServices m_dungeonCrawlServices;

	private GameObject m_dungeonCrawlDisplay;

	private void Start()
	{
		if (!m_dungeonCrawlDisplay)
		{
			DungeonCrawlUtil.LoadDungeonRunPrefab(OnDungeonRunLoaded);
		}
		else
		{
			OnDungeonRunLoaded(m_dungeonCrawlDisplay);
		}
	}

	private void OnDungeonRunLoaded(GameObject go)
	{
		m_dungeonCrawlDisplay = go;
		m_dungeonCrawlServices = DungeonCrawlUtil.CreateAdventureDungeonCrawlServices(base.AssetLoadingHelper);
		AdventureDungeonCrawlDisplay component = go.GetComponent<AdventureDungeonCrawlDisplay>();
		if (component != null)
		{
			GameUtils.SetParent(go, base.transform);
			component.StartRun(m_dungeonCrawlServices);
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_dungeonCrawlDisplay.transform.localPosition = new Vector3(0f, 5.5f, 0f);
		}
	}

	protected override void OnSubSceneTransitionComplete()
	{
		m_dungeonCrawlServices.SubsceneController.OnTransitionComplete();
	}
}
