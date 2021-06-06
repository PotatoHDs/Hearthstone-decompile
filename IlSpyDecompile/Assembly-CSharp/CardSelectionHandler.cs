using UnityEngine;

public class CardSelectionHandler : PegUIElement
{
	public delegate void CardChosenCallback();

	protected Actor m_actor;

	protected int m_cardChoice = -1;

	private CardChosenCallback m_cardChosenCallback;

	private const float MOUSE_OVER_DELAY = 0.4f;

	private float m_mouseOverTimer;

	public void SetActor(Actor actor)
	{
		m_actor = actor;
	}

	public Actor GetActor()
	{
		return m_actor;
	}

	public void SetChoiceNum(int num)
	{
		m_cardChoice = num;
	}

	public int GetChoiceNum()
	{
		return m_cardChoice;
	}

	public void SetChosenCallback(CardChosenCallback callback)
	{
		m_cardChosenCallback = callback;
	}

	protected override void OnPress()
	{
		m_mouseOverTimer = Time.realtimeSinceStartup;
	}

	protected override void OnRelease()
	{
		if ((!UniversalInputManager.Get().IsTouchMode() || !(Time.realtimeSinceStartup - m_mouseOverTimer >= 0.4f)) && m_cardChosenCallback != null)
		{
			m_cardChosenCallback();
		}
	}

	protected override void OnOver(InteractionState oldState)
	{
		if (m_actor.GetEntityDef().IsHeroSkin() || m_actor.GetEntityDef().IsHeroPower())
		{
			SoundManager.Get().LoadAndPlay("collection_manager_hero_mouse_over.prefab:653cc8000b988cd468d2210a209adce6");
		}
		else
		{
			SoundManager.Get().LoadAndPlay("collection_manager_card_mouse_over.prefab:0d4e20bc78956bc48b5e2963ec39211c");
		}
		m_actor.SetActorState(ActorStateType.CARD_MOUSE_OVER);
		TooltipPanelManager.Get().UpdateKeywordHelpForForge(m_actor.GetEntityDef(), m_actor, m_cardChoice);
	}

	protected override void OnOut(InteractionState oldState)
	{
		m_actor.SetActorState(ActorStateType.CARD_IDLE);
		TooltipPanelManager.Get().HideKeywordHelp();
	}
}
