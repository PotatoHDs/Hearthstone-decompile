using Hearthstone.UI;
using UnityEngine;

public class DungeonCrawlDeckSelect : MonoBehaviour
{
	public AsyncReference m_heroDetailsWigetReference;

	public AsyncReference m_deckListWidgetReference;

	private SlidingTray m_slidingTray;

	private DungeonCrawlHeroDetails m_heroDetails;

	private AdventureDungeonCrawlDeckTray m_deckTray;

	private PlayButton m_playButton;

	public bool isReady
	{
		get
		{
			if (m_heroDetailsWigetReference.IsReady && m_deckListWidgetReference.IsReady && m_heroDetails != null)
			{
				return m_deckTray != null;
			}
			return false;
		}
	}

	public SlidingTray slidingTray => m_slidingTray;

	public DungeonCrawlHeroDetails heroDetails => m_heroDetails;

	public AdventureDungeonCrawlDeckTray deckTray => m_deckTray;

	public PlayButton playButton => m_playButton;

	private void Awake()
	{
		m_heroDetailsWigetReference.RegisterReadyListener<DungeonCrawlHeroDetails>(OnHeroDetailsWidgetReady);
		m_deckListWidgetReference.RegisterReadyListener<AdventureDungeonCrawlDeckTray>(OnDeckListWidgetReady);
		m_slidingTray = GetComponentInParent<SlidingTray>();
		if (m_slidingTray != null)
		{
			m_slidingTray.RegisterTrayToggleListener(OnSlidingTrayToggled);
		}
	}

	private void OnHeroDetailsWidgetReady(DungeonCrawlHeroDetails details)
	{
		m_heroDetails = details;
		m_playButton = m_heroDetails.GetComponentInChildren<PlayButton>();
		m_playButton.AddEventListener(UIEventType.RELEASE, OnPlayButtonReleased);
	}

	private void OnDeckListWidgetReady(AdventureDungeonCrawlDeckTray tray)
	{
		m_deckTray = tray;
	}

	private void OnPlayButtonReleased(UIEvent e)
	{
		slidingTray.ToggleTraySlider(show: false);
		m_playButton.SetEnabled(enabled: false);
	}

	private void OnSlidingTrayToggled(bool isShowing)
	{
		m_playButton.SetEnabled(isShowing);
	}
}
