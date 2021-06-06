using System;
using Hearthstone.UI;

// Token: 0x02000682 RID: 1666
public abstract class WidgetReward : Reward
{
	// Token: 0x06005D47 RID: 23879 RVA: 0x001E4EA2 File Offset: 0x001E30A2
	protected override void Start()
	{
		base.Start();
		this.m_rewardWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnRewardWidgetReady));
	}

	// Token: 0x06005D48 RID: 23880 RVA: 0x001E4EC1 File Offset: 0x001E30C1
	private void OnRewardWidgetReady(Widget widget)
	{
		this.m_rewardWidget = widget;
		if (this.m_rewardWidget == null)
		{
			return;
		}
		if (this.m_hidden)
		{
			this.m_rewardWidget.Hide();
		}
	}

	// Token: 0x06005D49 RID: 23881 RVA: 0x001E4EEC File Offset: 0x001E30EC
	public override void Hide(bool animate = false)
	{
		if (this.m_shown)
		{
			FullScreenFXMgr.Get().EndStandardBlurVignette(0.5f, null);
		}
		base.Hide(animate);
	}

	// Token: 0x06005D4A RID: 23882 RVA: 0x001E4F10 File Offset: 0x001E3110
	protected override void ShowReward(bool updateCacheValues)
	{
		this.m_hidden = false;
		this.m_rewardWidget.Show();
		SceneUtils.SetLayer(base.gameObject, GameLayer.IgnoreFullScreenEffects);
		this.m_rewardWidget.TriggerEvent("PLAY_REWARD_UNLOCKED_ANIM", default(Widget.TriggerEventParameters));
		FullScreenFXMgr.Get().StartStandardBlurVignette(0.5f);
	}

	// Token: 0x06005D4B RID: 23883 RVA: 0x001E4F68 File Offset: 0x001E3168
	protected override void HideReward()
	{
		base.HideReward();
		if (this.m_rewardWidget != null)
		{
			this.m_rewardWidget.TriggerEvent("STOP_REWARD_UNLOCKED_ANIM", default(Widget.TriggerEventParameters));
			this.m_rewardWidget.Hide();
		}
		this.m_hidden = true;
	}

	// Token: 0x04004EE0 RID: 20192
	public AsyncReference m_rewardWidgetReference;

	// Token: 0x04004EE1 RID: 20193
	protected Widget m_rewardWidget;

	// Token: 0x04004EE2 RID: 20194
	protected bool m_hidden;

	// Token: 0x04004EE3 RID: 20195
	private const string PLAY_REWARD_UNLOCK_ANIM_EVENT_NAME = "PLAY_REWARD_UNLOCKED_ANIM";

	// Token: 0x04004EE4 RID: 20196
	private const string STOP_REWARD_UNLOCK_ANIM_EVENT_NAME = "STOP_REWARD_UNLOCKED_ANIM";
}
