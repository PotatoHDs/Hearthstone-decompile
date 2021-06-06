using System;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000063 RID: 99
public class DungeonCrawlDeckSelect : MonoBehaviour
{
	// Token: 0x1700004D RID: 77
	// (get) Token: 0x06000596 RID: 1430 RVA: 0x00020556 File Offset: 0x0001E756
	public bool isReady
	{
		get
		{
			return this.m_heroDetailsWigetReference.IsReady && this.m_deckListWidgetReference.IsReady && this.m_heroDetails != null && this.m_deckTray != null;
		}
	}

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x06000597 RID: 1431 RVA: 0x0002058E File Offset: 0x0001E78E
	public SlidingTray slidingTray
	{
		get
		{
			return this.m_slidingTray;
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x06000598 RID: 1432 RVA: 0x00020596 File Offset: 0x0001E796
	public DungeonCrawlHeroDetails heroDetails
	{
		get
		{
			return this.m_heroDetails;
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x06000599 RID: 1433 RVA: 0x0002059E File Offset: 0x0001E79E
	public AdventureDungeonCrawlDeckTray deckTray
	{
		get
		{
			return this.m_deckTray;
		}
	}

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x0600059A RID: 1434 RVA: 0x000205A6 File Offset: 0x0001E7A6
	public PlayButton playButton
	{
		get
		{
			return this.m_playButton;
		}
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x000205B0 File Offset: 0x0001E7B0
	private void Awake()
	{
		this.m_heroDetailsWigetReference.RegisterReadyListener<DungeonCrawlHeroDetails>(new Action<DungeonCrawlHeroDetails>(this.OnHeroDetailsWidgetReady));
		this.m_deckListWidgetReference.RegisterReadyListener<AdventureDungeonCrawlDeckTray>(new Action<AdventureDungeonCrawlDeckTray>(this.OnDeckListWidgetReady));
		this.m_slidingTray = base.GetComponentInParent<SlidingTray>();
		if (this.m_slidingTray != null)
		{
			this.m_slidingTray.RegisterTrayToggleListener(new SlidingTray.TrayToggledListener(this.OnSlidingTrayToggled));
		}
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x0002061C File Offset: 0x0001E81C
	private void OnHeroDetailsWidgetReady(DungeonCrawlHeroDetails details)
	{
		this.m_heroDetails = details;
		this.m_playButton = this.m_heroDetails.GetComponentInChildren<PlayButton>();
		this.m_playButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnPlayButtonReleased));
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x0002064F File Offset: 0x0001E84F
	private void OnDeckListWidgetReady(AdventureDungeonCrawlDeckTray tray)
	{
		this.m_deckTray = tray;
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x00020658 File Offset: 0x0001E858
	private void OnPlayButtonReleased(UIEvent e)
	{
		this.slidingTray.ToggleTraySlider(false, null, true);
		this.m_playButton.SetEnabled(false, false);
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x00020675 File Offset: 0x0001E875
	private void OnSlidingTrayToggled(bool isShowing)
	{
		this.m_playButton.SetEnabled(isShowing, false);
	}

	// Token: 0x040003F4 RID: 1012
	public AsyncReference m_heroDetailsWigetReference;

	// Token: 0x040003F5 RID: 1013
	public AsyncReference m_deckListWidgetReference;

	// Token: 0x040003F6 RID: 1014
	private SlidingTray m_slidingTray;

	// Token: 0x040003F7 RID: 1015
	private DungeonCrawlHeroDetails m_heroDetails;

	// Token: 0x040003F8 RID: 1016
	private AdventureDungeonCrawlDeckTray m_deckTray;

	// Token: 0x040003F9 RID: 1017
	private PlayButton m_playButton;
}
