using System;

// Token: 0x02000635 RID: 1589
public class QuestLogButton : PegUIElement
{
	// Token: 0x06005984 RID: 22916 RVA: 0x001D2E9C File Offset: 0x001D109C
	private void Start()
	{
		this.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnButtonOver));
		this.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnButtonOut));
		if (SoundManager.Get() != null)
		{
			SoundManager.Get().Load("quest_log_button_mouse_over.prefab:e5102eddbc19ec84297aa49eecb66397");
		}
	}

	// Token: 0x06005985 RID: 22917 RVA: 0x001D2EF0 File Offset: 0x001D10F0
	private void OnButtonOver(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("quest_log_button_mouse_over.prefab:e5102eddbc19ec84297aa49eecb66397", base.gameObject);
		this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
		TooltipZone component = base.GetComponent<TooltipZone>();
		if (component == null)
		{
			return;
		}
		component.ShowBoxTooltip(GameStrings.Get("GLUE_TOOLTIP_BUTTON_QUESTLOG_HEADLINE"), GameStrings.Get("GLUE_TOOLTIP_BUTTON_QUESTLOG_DESC"), 0);
	}

	// Token: 0x06005986 RID: 22918 RVA: 0x001D2F54 File Offset: 0x001D1154
	private void OnButtonOut(UIEvent e)
	{
		this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		TooltipZone component = base.GetComponent<TooltipZone>();
		if (component != null)
		{
			component.HideTooltip();
		}
	}

	// Token: 0x04004C8B RID: 19595
	public HighlightState m_highlight;
}
