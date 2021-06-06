using System;
using UnityEngine;

// Token: 0x02000358 RID: 856
public class ZoneDeck : Zone
{
	// Token: 0x060031BB RID: 12731 RVA: 0x000FE9A6 File Offset: 0x000FCBA6
	public void Awake()
	{
		if (this.m_deckTooltipZone != null && this.m_playerHandTooltipZone != null)
		{
			this.m_deckTooltipZone.SetTooltipChangeCallback(new TooltipZone.TooltipChangeCallback(this.TooltipChanged));
		}
	}

	// Token: 0x060031BC RID: 12732 RVA: 0x000FE9DB File Offset: 0x000FCBDB
	public void TooltipChanged(bool shown)
	{
		if (!shown)
		{
			this.m_playerHandTooltipZone.HideTooltip();
			if (this.m_playerManaTooltipZone != null)
			{
				this.m_playerManaTooltipZone.HideTooltip();
			}
		}
	}

	// Token: 0x060031BD RID: 12733 RVA: 0x000FEA04 File Offset: 0x000FCC04
	public override void Reset()
	{
		base.Reset();
		this.UpdateLayout();
	}

	// Token: 0x060031BE RID: 12734 RVA: 0x000FEA14 File Offset: 0x000FCC14
	public override void UpdateLayout()
	{
		this.m_updatingLayout++;
		if (base.IsBlockingLayout())
		{
			base.UpdateLayoutFinished();
			return;
		}
		this.UpdateThickness();
		this.UpdateDeckStateEmotes();
		for (int i = 0; i < this.m_cards.Count; i++)
		{
			Card card = this.m_cards[i];
			if (!card.IsDoNotSort())
			{
				card.HideCard();
				this.SetCardToInDeckState(card);
			}
		}
		base.UpdateLayoutFinished();
	}

	// Token: 0x060031BF RID: 12735 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnHealingDoesDamageEntityEnteredPlay()
	{
	}

	// Token: 0x060031C0 RID: 12736 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnHealingDoesDamageEntityMousedOut()
	{
	}

	// Token: 0x060031C1 RID: 12737 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnHealingDoesDamageEntityMousedOver()
	{
	}

	// Token: 0x060031C2 RID: 12738 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnLifestealDoesDamageEntityEnteredPlay()
	{
	}

	// Token: 0x060031C3 RID: 12739 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnLifestealDoesDamageEntityMousedOut()
	{
	}

	// Token: 0x060031C4 RID: 12740 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnLifestealDoesDamageEntityMousedOver()
	{
	}

	// Token: 0x060031C5 RID: 12741 RVA: 0x00036768 File Offset: 0x00034968
	public void SetVisibility(bool visible)
	{
		base.gameObject.SetActive(visible);
	}

	// Token: 0x060031C6 RID: 12742 RVA: 0x000261C2 File Offset: 0x000243C2
	public bool GetVisibility()
	{
		return base.gameObject.activeSelf;
	}

	// Token: 0x060031C7 RID: 12743 RVA: 0x000FEA88 File Offset: 0x000FCC88
	public void SetCardToInDeckState(Card card)
	{
		card.transform.localEulerAngles = new Vector3(270f, 270f, 0f);
		card.transform.position = base.transform.position;
		card.transform.localScale = new Vector3(0.88f, 0.88f, 0.88f);
		card.EnableTransitioningZones(false);
	}

	// Token: 0x060031C8 RID: 12744 RVA: 0x000FEAF0 File Offset: 0x000FCCF0
	public void DoFatigueGlow()
	{
		if (this.m_DeckFatigueGlow == null || !GameState.Get().GetBooleanGameOption(GameEntityOption.ALLOW_FATIGUE))
		{
			return;
		}
		this.m_DeckFatigueGlow.ActivateState(SpellStateType.ACTION);
	}

	// Token: 0x060031C9 RID: 12745 RVA: 0x000FEB1B File Offset: 0x000FCD1B
	public bool IsFatigued()
	{
		return this.m_cards.Count == 0;
	}

	// Token: 0x060031CA RID: 12746 RVA: 0x000FEB2C File Offset: 0x000FCD2C
	public Actor GetActiveThickness()
	{
		if (this.m_ThicknessFull.GetMeshRenderer(false).enabled)
		{
			return this.m_ThicknessFull;
		}
		if (this.m_Thickness75.GetMeshRenderer(false).enabled)
		{
			return this.m_Thickness75;
		}
		if (this.m_Thickness50.GetMeshRenderer(false).enabled)
		{
			return this.m_Thickness50;
		}
		if (this.m_Thickness25.GetMeshRenderer(false).enabled)
		{
			return this.m_Thickness25;
		}
		if (this.m_Thickness1.GetMeshRenderer(false).enabled)
		{
			return this.m_Thickness1;
		}
		return null;
	}

	// Token: 0x060031CB RID: 12747 RVA: 0x000FEBBC File Offset: 0x000FCDBC
	public Actor GetThicknessForLayout()
	{
		Actor activeThickness = this.GetActiveThickness();
		if (activeThickness != null)
		{
			return activeThickness;
		}
		return this.m_Thickness1;
	}

	// Token: 0x060031CC RID: 12748 RVA: 0x000FEBE1 File Offset: 0x000FCDE1
	public bool AreEmotesSuppressed()
	{
		return this.m_suppressEmotes;
	}

	// Token: 0x060031CD RID: 12749 RVA: 0x000FEBE9 File Offset: 0x000FCDE9
	public void SetSuppressEmotes(bool suppress)
	{
		this.m_suppressEmotes = suppress;
	}

	// Token: 0x060031CE RID: 12750 RVA: 0x000FEBF4 File Offset: 0x000FCDF4
	private void UpdateThickness()
	{
		this.m_ThicknessFull.GetMeshRenderer(false).enabled = false;
		this.m_Thickness75.GetMeshRenderer(false).enabled = false;
		this.m_Thickness50.GetMeshRenderer(false).enabled = false;
		this.m_Thickness25.GetMeshRenderer(false).enabled = false;
		this.m_Thickness1.GetMeshRenderer(false).enabled = false;
		int count = this.m_cards.Count;
		if (count == 0)
		{
			if (!this.m_wasFatigued && GameState.Get().GetBooleanGameOption(GameEntityOption.ALLOW_FATIGUE))
			{
				this.m_DeckFatigueGlow.ActivateState(SpellStateType.BIRTH);
				this.m_wasFatigued = true;
			}
			return;
		}
		if (this.m_wasFatigued && GameState.Get().GetBooleanGameOption(GameEntityOption.ALLOW_FATIGUE))
		{
			this.m_DeckFatigueGlow.ActivateState(SpellStateType.DEATH);
			this.m_wasFatigued = false;
		}
		if (count == 1)
		{
			this.m_Thickness1.GetMeshRenderer(false).enabled = true;
			return;
		}
		float num = (float)count / 26f;
		if (num > 0.75f)
		{
			this.m_ThicknessFull.GetMeshRenderer(false).enabled = true;
			return;
		}
		if (num > 0.5f)
		{
			this.m_Thickness75.GetMeshRenderer(false).enabled = true;
			return;
		}
		if (num > 0.25f)
		{
			this.m_Thickness50.GetMeshRenderer(false).enabled = true;
			return;
		}
		if (num > 0f)
		{
			this.m_Thickness25.GetMeshRenderer(false).enabled = true;
		}
	}

	// Token: 0x060031CF RID: 12751 RVA: 0x000FED48 File Offset: 0x000FCF48
	private void UpdateDeckStateEmotes()
	{
		if (!GameState.Get().IsPastBeginPhase())
		{
			return;
		}
		if (this.m_suppressEmotes)
		{
			return;
		}
		if (GameState.Get().GetGameEntity().HasTag(GAME_TAG.HIDE_OUT_OF_CARDS_WARNING))
		{
			return;
		}
		int count = this.m_cards.Count;
		if (count <= 0 && !this.m_warnedAboutNoCards)
		{
			this.m_warnedAboutNoCards = true;
			this.m_warnedAboutLastCard = true;
			this.m_controller.GetHeroCard().PlayEmote(EmoteType.NOCARDS);
			return;
		}
		if (count == 1 && !this.m_warnedAboutLastCard)
		{
			this.m_warnedAboutLastCard = true;
			this.m_controller.GetHeroCard().PlayEmote(EmoteType.LOWCARDS);
			return;
		}
		if (this.m_warnedAboutLastCard && count > 1)
		{
			this.m_warnedAboutLastCard = false;
		}
		if (this.m_warnedAboutNoCards && count > 0)
		{
			this.m_warnedAboutNoCards = false;
		}
	}

	// Token: 0x04001B97 RID: 7063
	public Actor m_ThicknessFull;

	// Token: 0x04001B98 RID: 7064
	public Actor m_Thickness75;

	// Token: 0x04001B99 RID: 7065
	public Actor m_Thickness50;

	// Token: 0x04001B9A RID: 7066
	public Actor m_Thickness25;

	// Token: 0x04001B9B RID: 7067
	public Actor m_Thickness1;

	// Token: 0x04001B9C RID: 7068
	public Spell m_DeckFatigueGlow;

	// Token: 0x04001B9D RID: 7069
	public TooltipZone m_deckTooltipZone;

	// Token: 0x04001B9E RID: 7070
	public TooltipZone m_playerHandTooltipZone;

	// Token: 0x04001B9F RID: 7071
	public TooltipZone m_playerManaTooltipZone;

	// Token: 0x04001BA0 RID: 7072
	private const int MAX_THICKNESS_CARD_COUNT = 26;

	// Token: 0x04001BA1 RID: 7073
	private bool m_suppressEmotes;

	// Token: 0x04001BA2 RID: 7074
	private bool m_warnedAboutLastCard;

	// Token: 0x04001BA3 RID: 7075
	private bool m_warnedAboutNoCards;

	// Token: 0x04001BA4 RID: 7076
	private bool m_wasFatigued;
}
