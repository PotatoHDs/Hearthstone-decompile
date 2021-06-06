using System;
using Hearthstone.DungeonCrawl;
using UnityEngine;

// Token: 0x02000051 RID: 81
public class AdventureSubSceneDungeonRun : AdventureSubSceneDisplay
{
	// Token: 0x06000495 RID: 1173 RVA: 0x0001B515 File Offset: 0x00019715
	private void Start()
	{
		if (!this.m_dungeonCrawlDisplay)
		{
			DungeonCrawlUtil.LoadDungeonRunPrefab(new DungeonCrawlUtil.DungeonRunLoadCallback(this.OnDungeonRunLoaded));
			return;
		}
		this.OnDungeonRunLoaded(this.m_dungeonCrawlDisplay);
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x0001B544 File Offset: 0x00019744
	private void OnDungeonRunLoaded(GameObject go)
	{
		this.m_dungeonCrawlDisplay = go;
		this.m_dungeonCrawlServices = DungeonCrawlUtil.CreateAdventureDungeonCrawlServices(base.AssetLoadingHelper);
		AdventureDungeonCrawlDisplay component = go.GetComponent<AdventureDungeonCrawlDisplay>();
		if (component != null)
		{
			GameUtils.SetParent(go, base.transform, false);
			component.StartRun(this.m_dungeonCrawlServices);
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_dungeonCrawlDisplay.transform.localPosition = new Vector3(0f, 5.5f, 0f);
		}
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x0001B5C2 File Offset: 0x000197C2
	protected override void OnSubSceneTransitionComplete()
	{
		this.m_dungeonCrawlServices.SubsceneController.OnTransitionComplete();
	}

	// Token: 0x04000339 RID: 825
	private DungeonCrawlServices m_dungeonCrawlServices;

	// Token: 0x0400033A RID: 826
	private GameObject m_dungeonCrawlDisplay;
}
