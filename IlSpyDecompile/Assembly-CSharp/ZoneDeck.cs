using UnityEngine;

public class ZoneDeck : Zone
{
	public Actor m_ThicknessFull;

	public Actor m_Thickness75;

	public Actor m_Thickness50;

	public Actor m_Thickness25;

	public Actor m_Thickness1;

	public Spell m_DeckFatigueGlow;

	public TooltipZone m_deckTooltipZone;

	public TooltipZone m_playerHandTooltipZone;

	public TooltipZone m_playerManaTooltipZone;

	private const int MAX_THICKNESS_CARD_COUNT = 26;

	private bool m_suppressEmotes;

	private bool m_warnedAboutLastCard;

	private bool m_warnedAboutNoCards;

	private bool m_wasFatigued;

	public void Awake()
	{
		if (m_deckTooltipZone != null && m_playerHandTooltipZone != null)
		{
			m_deckTooltipZone.SetTooltipChangeCallback(TooltipChanged);
		}
	}

	public void TooltipChanged(bool shown)
	{
		if (!shown)
		{
			m_playerHandTooltipZone.HideTooltip();
			if (m_playerManaTooltipZone != null)
			{
				m_playerManaTooltipZone.HideTooltip();
			}
		}
	}

	public override void Reset()
	{
		base.Reset();
		UpdateLayout();
	}

	public override void UpdateLayout()
	{
		m_updatingLayout++;
		if (IsBlockingLayout())
		{
			UpdateLayoutFinished();
			return;
		}
		UpdateThickness();
		UpdateDeckStateEmotes();
		for (int i = 0; i < m_cards.Count; i++)
		{
			Card card = m_cards[i];
			if (!card.IsDoNotSort())
			{
				card.HideCard();
				SetCardToInDeckState(card);
			}
		}
		UpdateLayoutFinished();
	}

	public override void OnHealingDoesDamageEntityEnteredPlay()
	{
	}

	public override void OnHealingDoesDamageEntityMousedOut()
	{
	}

	public override void OnHealingDoesDamageEntityMousedOver()
	{
	}

	public override void OnLifestealDoesDamageEntityEnteredPlay()
	{
	}

	public override void OnLifestealDoesDamageEntityMousedOut()
	{
	}

	public override void OnLifestealDoesDamageEntityMousedOver()
	{
	}

	public void SetVisibility(bool visible)
	{
		base.gameObject.SetActive(visible);
	}

	public bool GetVisibility()
	{
		return base.gameObject.activeSelf;
	}

	public void SetCardToInDeckState(Card card)
	{
		card.transform.localEulerAngles = new Vector3(270f, 270f, 0f);
		card.transform.position = base.transform.position;
		card.transform.localScale = new Vector3(0.88f, 0.88f, 0.88f);
		card.EnableTransitioningZones(enable: false);
	}

	public void DoFatigueGlow()
	{
		if (!(m_DeckFatigueGlow == null) && GameState.Get().GetBooleanGameOption(GameEntityOption.ALLOW_FATIGUE))
		{
			m_DeckFatigueGlow.ActivateState(SpellStateType.ACTION);
		}
	}

	public bool IsFatigued()
	{
		return m_cards.Count == 0;
	}

	public Actor GetActiveThickness()
	{
		if (m_ThicknessFull.GetMeshRenderer().enabled)
		{
			return m_ThicknessFull;
		}
		if (m_Thickness75.GetMeshRenderer().enabled)
		{
			return m_Thickness75;
		}
		if (m_Thickness50.GetMeshRenderer().enabled)
		{
			return m_Thickness50;
		}
		if (m_Thickness25.GetMeshRenderer().enabled)
		{
			return m_Thickness25;
		}
		if (m_Thickness1.GetMeshRenderer().enabled)
		{
			return m_Thickness1;
		}
		return null;
	}

	public Actor GetThicknessForLayout()
	{
		Actor activeThickness = GetActiveThickness();
		if (activeThickness != null)
		{
			return activeThickness;
		}
		return m_Thickness1;
	}

	public bool AreEmotesSuppressed()
	{
		return m_suppressEmotes;
	}

	public void SetSuppressEmotes(bool suppress)
	{
		m_suppressEmotes = suppress;
	}

	private void UpdateThickness()
	{
		m_ThicknessFull.GetMeshRenderer().enabled = false;
		m_Thickness75.GetMeshRenderer().enabled = false;
		m_Thickness50.GetMeshRenderer().enabled = false;
		m_Thickness25.GetMeshRenderer().enabled = false;
		m_Thickness1.GetMeshRenderer().enabled = false;
		int count = m_cards.Count;
		if (count == 0)
		{
			if (!m_wasFatigued && GameState.Get().GetBooleanGameOption(GameEntityOption.ALLOW_FATIGUE))
			{
				m_DeckFatigueGlow.ActivateState(SpellStateType.BIRTH);
				m_wasFatigued = true;
			}
			return;
		}
		if (m_wasFatigued && GameState.Get().GetBooleanGameOption(GameEntityOption.ALLOW_FATIGUE))
		{
			m_DeckFatigueGlow.ActivateState(SpellStateType.DEATH);
			m_wasFatigued = false;
		}
		if (count == 1)
		{
			m_Thickness1.GetMeshRenderer().enabled = true;
			return;
		}
		float num = (float)count / 26f;
		if (num > 0.75f)
		{
			m_ThicknessFull.GetMeshRenderer().enabled = true;
		}
		else if (num > 0.5f)
		{
			m_Thickness75.GetMeshRenderer().enabled = true;
		}
		else if (num > 0.25f)
		{
			m_Thickness50.GetMeshRenderer().enabled = true;
		}
		else if (num > 0f)
		{
			m_Thickness25.GetMeshRenderer().enabled = true;
		}
	}

	private void UpdateDeckStateEmotes()
	{
		if (!GameState.Get().IsPastBeginPhase() || m_suppressEmotes || GameState.Get().GetGameEntity().HasTag(GAME_TAG.HIDE_OUT_OF_CARDS_WARNING))
		{
			return;
		}
		int count = m_cards.Count;
		if (count <= 0 && !m_warnedAboutNoCards)
		{
			m_warnedAboutNoCards = true;
			m_warnedAboutLastCard = true;
			m_controller.GetHeroCard().PlayEmote(EmoteType.NOCARDS);
			return;
		}
		if (count == 1 && !m_warnedAboutLastCard)
		{
			m_warnedAboutLastCard = true;
			m_controller.GetHeroCard().PlayEmote(EmoteType.LOWCARDS);
			return;
		}
		if (m_warnedAboutLastCard && count > 1)
		{
			m_warnedAboutLastCard = false;
		}
		if (m_warnedAboutNoCards && count > 0)
		{
			m_warnedAboutNoCards = false;
		}
	}
}
