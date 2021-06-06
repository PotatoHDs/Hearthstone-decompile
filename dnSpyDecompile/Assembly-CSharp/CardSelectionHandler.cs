using System;
using UnityEngine;

// Token: 0x020002B4 RID: 692
public class CardSelectionHandler : PegUIElement
{
	// Token: 0x060023E4 RID: 9188 RVA: 0x000B3693 File Offset: 0x000B1893
	public void SetActor(Actor actor)
	{
		this.m_actor = actor;
	}

	// Token: 0x060023E5 RID: 9189 RVA: 0x000B369C File Offset: 0x000B189C
	public Actor GetActor()
	{
		return this.m_actor;
	}

	// Token: 0x060023E6 RID: 9190 RVA: 0x000B36A4 File Offset: 0x000B18A4
	public void SetChoiceNum(int num)
	{
		this.m_cardChoice = num;
	}

	// Token: 0x060023E7 RID: 9191 RVA: 0x000B36AD File Offset: 0x000B18AD
	public int GetChoiceNum()
	{
		return this.m_cardChoice;
	}

	// Token: 0x060023E8 RID: 9192 RVA: 0x000B36B5 File Offset: 0x000B18B5
	public void SetChosenCallback(CardSelectionHandler.CardChosenCallback callback)
	{
		this.m_cardChosenCallback = callback;
	}

	// Token: 0x060023E9 RID: 9193 RVA: 0x000B36BE File Offset: 0x000B18BE
	protected override void OnPress()
	{
		this.m_mouseOverTimer = Time.realtimeSinceStartup;
	}

	// Token: 0x060023EA RID: 9194 RVA: 0x000B36CB File Offset: 0x000B18CB
	protected override void OnRelease()
	{
		if (UniversalInputManager.Get().IsTouchMode() && Time.realtimeSinceStartup - this.m_mouseOverTimer >= 0.4f)
		{
			return;
		}
		if (this.m_cardChosenCallback != null)
		{
			this.m_cardChosenCallback();
		}
	}

	// Token: 0x060023EB RID: 9195 RVA: 0x000B3700 File Offset: 0x000B1900
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		if (this.m_actor.GetEntityDef().IsHeroSkin() || this.m_actor.GetEntityDef().IsHeroPower())
		{
			SoundManager.Get().LoadAndPlay("collection_manager_hero_mouse_over.prefab:653cc8000b988cd468d2210a209adce6");
		}
		else
		{
			SoundManager.Get().LoadAndPlay("collection_manager_card_mouse_over.prefab:0d4e20bc78956bc48b5e2963ec39211c");
		}
		this.m_actor.SetActorState(ActorStateType.CARD_MOUSE_OVER);
		TooltipPanelManager.Get().UpdateKeywordHelpForForge(this.m_actor.GetEntityDef(), this.m_actor, this.m_cardChoice);
	}

	// Token: 0x060023EC RID: 9196 RVA: 0x000B3788 File Offset: 0x000B1988
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		this.m_actor.SetActorState(ActorStateType.CARD_IDLE);
		TooltipPanelManager.Get().HideKeywordHelp();
	}

	// Token: 0x0400140C RID: 5132
	protected Actor m_actor;

	// Token: 0x0400140D RID: 5133
	protected int m_cardChoice = -1;

	// Token: 0x0400140E RID: 5134
	private CardSelectionHandler.CardChosenCallback m_cardChosenCallback;

	// Token: 0x0400140F RID: 5135
	private const float MOUSE_OVER_DELAY = 0.4f;

	// Token: 0x04001410 RID: 5136
	private float m_mouseOverTimer;

	// Token: 0x020015AE RID: 5550
	// (Invoke) Token: 0x0600E144 RID: 57668
	public delegate void CardChosenCallback();
}
