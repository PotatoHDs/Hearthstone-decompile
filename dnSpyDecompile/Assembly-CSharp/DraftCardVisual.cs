using System;

// Token: 0x020002B6 RID: 694
public class DraftCardVisual : CardSelectionHandler
{
	// Token: 0x06002400 RID: 9216 RVA: 0x000B3B22 File Offset: 0x000B1D22
	protected override void Awake()
	{
		base.Awake();
		base.SetChosenCallback(new CardSelectionHandler.CardChosenCallback(this.ChooseThisCard));
	}

	// Token: 0x06002401 RID: 9217 RVA: 0x000B3B3C File Offset: 0x000B1D3C
	public void SetSubActor(Actor actor)
	{
		this.m_subActor = actor;
	}

	// Token: 0x06002402 RID: 9218 RVA: 0x000B3B45 File Offset: 0x000B1D45
	public Actor GetSubActor()
	{
		return this.m_subActor;
	}

	// Token: 0x06002403 RID: 9219 RVA: 0x000B3B50 File Offset: 0x000B1D50
	public void ChooseThisCard()
	{
		if (GameUtils.IsAnyTransitionActive())
		{
			return;
		}
		Log.Arena.Print(string.Format("Client chooses: {0} ({1})", this.m_actor.GetEntityDef().GetName(), this.m_actor.GetEntityDef().GetCardId()), Array.Empty<object>());
		if (this.m_actor.GetEntityDef().IsHeroSkin() || this.m_actor.GetEntityDef().IsHeroPower())
		{
			DraftDisplay.Get().OnHeroClicked(this.m_cardChoice);
			return;
		}
		this.m_chosen = true;
		DraftManager.Get().MakeChoice(this.m_cardChoice, this.m_actor.GetPremium());
	}

	// Token: 0x06002404 RID: 9220 RVA: 0x000B3BF5 File Offset: 0x000B1DF5
	public bool IsChosen()
	{
		return this.m_chosen;
	}

	// Token: 0x06002405 RID: 9221 RVA: 0x000B3BFD File Offset: 0x000B1DFD
	public void SetChosenFlag(bool bOn)
	{
		this.m_chosen = bOn;
	}

	// Token: 0x06002406 RID: 9222 RVA: 0x000B3C06 File Offset: 0x000B1E06
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		base.OnOver(oldState);
		if (this.m_subActor != null)
		{
			this.m_subActor.SetActorState(ActorStateType.CARD_MOUSE_OVER);
		}
	}

	// Token: 0x06002407 RID: 9223 RVA: 0x000B3C29 File Offset: 0x000B1E29
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		base.OnOut(oldState);
		if (this.m_subActor != null)
		{
			this.m_subActor.SetActorState(ActorStateType.CARD_IDLE);
		}
	}

	// Token: 0x0400141A RID: 5146
	private Actor m_subActor;

	// Token: 0x0400141B RID: 5147
	private bool m_chosen;
}
