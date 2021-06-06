public class QuestLogButton : PegUIElement
{
	public HighlightState m_highlight;

	private void Start()
	{
		AddEventListener(UIEventType.ROLLOVER, OnButtonOver);
		AddEventListener(UIEventType.ROLLOUT, OnButtonOut);
		if (SoundManager.Get() != null)
		{
			SoundManager.Get().Load("quest_log_button_mouse_over.prefab:e5102eddbc19ec84297aa49eecb66397");
		}
	}

	private void OnButtonOver(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("quest_log_button_mouse_over.prefab:e5102eddbc19ec84297aa49eecb66397", base.gameObject);
		m_highlight.ChangeState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
		TooltipZone component = GetComponent<TooltipZone>();
		if (!(component == null))
		{
			component.ShowBoxTooltip(GameStrings.Get("GLUE_TOOLTIP_BUTTON_QUESTLOG_HEADLINE"), GameStrings.Get("GLUE_TOOLTIP_BUTTON_QUESTLOG_DESC"));
		}
	}

	private void OnButtonOut(UIEvent e)
	{
		m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		TooltipZone component = GetComponent<TooltipZone>();
		if (component != null)
		{
			component.HideTooltip();
		}
	}
}
