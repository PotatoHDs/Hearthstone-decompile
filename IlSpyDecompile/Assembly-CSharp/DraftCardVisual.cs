public class DraftCardVisual : CardSelectionHandler
{
	private Actor m_subActor;

	private bool m_chosen;

	protected override void Awake()
	{
		base.Awake();
		SetChosenCallback(ChooseThisCard);
	}

	public void SetSubActor(Actor actor)
	{
		m_subActor = actor;
	}

	public Actor GetSubActor()
	{
		return m_subActor;
	}

	public void ChooseThisCard()
	{
		if (!GameUtils.IsAnyTransitionActive())
		{
			Log.Arena.Print($"Client chooses: {m_actor.GetEntityDef().GetName()} ({m_actor.GetEntityDef().GetCardId()})");
			if (m_actor.GetEntityDef().IsHeroSkin() || m_actor.GetEntityDef().IsHeroPower())
			{
				DraftDisplay.Get().OnHeroClicked(m_cardChoice);
				return;
			}
			m_chosen = true;
			DraftManager.Get().MakeChoice(m_cardChoice, m_actor.GetPremium());
		}
	}

	public bool IsChosen()
	{
		return m_chosen;
	}

	public void SetChosenFlag(bool bOn)
	{
		m_chosen = bOn;
	}

	protected override void OnOver(InteractionState oldState)
	{
		base.OnOver(oldState);
		if (m_subActor != null)
		{
			m_subActor.SetActorState(ActorStateType.CARD_MOUSE_OVER);
		}
	}

	protected override void OnOut(InteractionState oldState)
	{
		base.OnOut(oldState);
		if (m_subActor != null)
		{
			m_subActor.SetActorState(ActorStateType.CARD_IDLE);
		}
	}
}
