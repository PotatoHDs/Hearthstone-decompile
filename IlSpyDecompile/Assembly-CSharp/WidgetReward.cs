using Hearthstone.UI;

public abstract class WidgetReward : Reward
{
	public AsyncReference m_rewardWidgetReference;

	protected Widget m_rewardWidget;

	protected bool m_hidden;

	private const string PLAY_REWARD_UNLOCK_ANIM_EVENT_NAME = "PLAY_REWARD_UNLOCKED_ANIM";

	private const string STOP_REWARD_UNLOCK_ANIM_EVENT_NAME = "STOP_REWARD_UNLOCKED_ANIM";

	protected override void Start()
	{
		base.Start();
		m_rewardWidgetReference.RegisterReadyListener<Widget>(OnRewardWidgetReady);
	}

	private void OnRewardWidgetReady(Widget widget)
	{
		m_rewardWidget = widget;
		if (!(m_rewardWidget == null) && m_hidden)
		{
			m_rewardWidget.Hide();
		}
	}

	public override void Hide(bool animate = false)
	{
		if (m_shown)
		{
			FullScreenFXMgr.Get().EndStandardBlurVignette(0.5f);
		}
		base.Hide(animate);
	}

	protected override void ShowReward(bool updateCacheValues)
	{
		m_hidden = false;
		m_rewardWidget.Show();
		SceneUtils.SetLayer(base.gameObject, GameLayer.IgnoreFullScreenEffects);
		m_rewardWidget.TriggerEvent("PLAY_REWARD_UNLOCKED_ANIM");
		FullScreenFXMgr.Get().StartStandardBlurVignette(0.5f);
	}

	protected override void HideReward()
	{
		base.HideReward();
		if (m_rewardWidget != null)
		{
			m_rewardWidget.TriggerEvent("STOP_REWARD_UNLOCKED_ANIM");
			m_rewardWidget.Hide();
		}
		m_hidden = true;
	}
}
