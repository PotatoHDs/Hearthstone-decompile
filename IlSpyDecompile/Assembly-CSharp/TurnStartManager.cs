using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnStartManager : MonoBehaviour
{
	private class CardChange
	{
		public Card m_card;

		public TagDelta m_tagDelta;
	}

	public TurnStartIndicator m_turnStartPrefab;

	public List<TurnStartIndicator> m_alternativeTurnStartPrefab;

	public Spell m_OpponentExtraTurnSpell;

	public Spell m_FriendlyExtraTurnSpell;

	private static TurnStartManager s_instance;

	private TurnStartIndicator m_turnStartInstance;

	private Spell m_opponentExtraTurnSpellInstance;

	private Spell m_friendlyExtraTurnSpellInstance;

	private bool m_listeningForTurnEvents;

	private int m_manaCrystalsGained;

	private int m_manaCrystalsFilled;

	private List<Card> m_cardsToDraw = new List<Card>();

	private List<CardChange> m_exhaustedChangesToHandle = new List<CardChange>();

	private SpellController m_spellController;

	private bool m_blockingInput;

	private bool m_twoScoopsDisplayed;

	private bool m_twoScoopsRequestFromMetadata;

	private void Awake()
	{
		s_instance = this;
		if (GameState.Get() == null)
		{
			Debug.LogError($"TurnStartManager.Awake() - GameState already Shutdown before TurnStartManager was loaded.");
			return;
		}
		if (GameState.Get().IsGameCreated())
		{
			StartCoroutine(InstantiateTurnStartIndicator());
		}
		else
		{
			GameState.Get().RegisterCreateGameListener(OnCreateGame);
		}
		GameState.Get().RegisterGameOverListener(OnGameOver);
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static TurnStartManager Get()
	{
		return s_instance;
	}

	public bool IsListeningForTurnEvents()
	{
		return m_listeningForTurnEvents;
	}

	public void BeginListeningForTurnEvents(bool fromMetadata = false)
	{
		m_cardsToDraw.Clear();
		m_exhaustedChangesToHandle.Clear();
		m_manaCrystalsGained = 0;
		m_manaCrystalsFilled = 0;
		m_twoScoopsDisplayed = false;
		m_listeningForTurnEvents = true;
		m_blockingInput = true;
		m_twoScoopsRequestFromMetadata = fromMetadata;
	}

	public void NotifyOfManaCrystalGained(int amount)
	{
		m_manaCrystalsGained += amount;
	}

	public void NotifyOfManaCrystalFilled(int amount)
	{
		m_manaCrystalsFilled += amount;
	}

	public void NotifyOfCardDrawn(Entity drawnEntity)
	{
		m_cardsToDraw.Add(drawnEntity.GetCard());
	}

	public void NotifyOfExhaustedChange(Card card, TagDelta tagChange)
	{
		CardChange item = new CardChange
		{
			m_card = card,
			m_tagDelta = tagChange
		};
		m_exhaustedChangesToHandle.Add(item);
	}

	private int GetCurrentAlternativeAppearanceIndex()
	{
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return 0;
		}
		return gameState.GetGameEntity()?.GetTag(GAME_TAG.TURN_INDICATOR_ALTERNATIVE_APPEARANCE) ?? 0;
	}

	private bool IsTurnStartIndicatorDisabled()
	{
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return false;
		}
		GameEntity gameEntity = gameState.GetGameEntity();
		if (gameEntity == null)
		{
			return false;
		}
		return gameEntity.GetTag(GAME_TAG.DISABLE_TURN_INDICATORS) > 0;
	}

	public void ApplyAlternativeAppearance()
	{
		StartCoroutine(Get().InstantiateTurnStartIndicator());
	}

	private IEnumerator InstantiateTurnStartIndicator()
	{
		if (m_turnStartInstance != null)
		{
			while (m_turnStartInstance.IsShown())
			{
				yield return null;
			}
			UnityEngine.Object.Destroy(m_turnStartInstance);
		}
		if (!IsTurnStartIndicatorDisabled())
		{
			int currentAlternativeAppearanceIndex = GetCurrentAlternativeAppearanceIndex();
			if (currentAlternativeAppearanceIndex == 0)
			{
				m_turnStartInstance = UnityEngine.Object.Instantiate(m_turnStartPrefab);
			}
			else if (m_alternativeTurnStartPrefab.Count >= currentAlternativeAppearanceIndex)
			{
				m_turnStartInstance = UnityEngine.Object.Instantiate(m_alternativeTurnStartPrefab[currentAlternativeAppearanceIndex - 1]);
			}
			if (m_turnStartInstance != null)
			{
				m_turnStartInstance.transform.parent = base.transform;
			}
			else
			{
				Debug.LogError($"TurnStartManager.InstantiateTurnStartIndicator() - FAILED to instantiate turn start prefab for appearance {currentAlternativeAppearanceIndex}");
			}
		}
	}

	public Spell GetExtraTurnSpell(bool isFriendly = true)
	{
		Spell result = m_friendlyExtraTurnSpellInstance;
		if (!isFriendly)
		{
			result = m_opponentExtraTurnSpellInstance;
		}
		return result;
	}

	public Spell SetExtraTurnSpell(Spell extraTurnSpell, bool isFriendly = true)
	{
		if (isFriendly)
		{
			m_friendlyExtraTurnSpellInstance = extraTurnSpell;
		}
		else
		{
			m_opponentExtraTurnSpellInstance = extraTurnSpell;
		}
		return extraTurnSpell;
	}

	public void NotifyOfExtraTurn(Spell extraTurnSpell, bool isEnding = false, bool isFriendly = true)
	{
		if (!isEnding)
		{
			if (extraTurnSpell == null)
			{
				extraTurnSpell = ((!isFriendly) ? UnityEngine.Object.Instantiate(m_OpponentExtraTurnSpell) : UnityEngine.Object.Instantiate(m_FriendlyExtraTurnSpell));
				extraTurnSpell.Activate();
			}
		}
		else if (extraTurnSpell != null)
		{
			extraTurnSpell.ActivateState(SpellStateType.DEATH);
			extraTurnSpell.AddStateFinishedCallback(delegate(Spell spell, SpellStateType prevStateType, object userData)
			{
				if (spell.GetActiveState() == SpellStateType.NONE)
				{
					UnityEngine.Object.Destroy(spell.gameObject);
				}
			});
			extraTurnSpell = null;
		}
		SetExtraTurnSpell(extraTurnSpell, isFriendly);
	}

	public void NotifyOfSpellController(SpellController spellController)
	{
		m_spellController = spellController;
		BeginPlayingTurnEvents();
	}

	public void NotifyOfStartOfTurnChoice()
	{
		BeginPlayingTurnEvents();
	}

	public SpellController GetSpellController()
	{
		return m_spellController;
	}

	public int GetNumCardsToDraw()
	{
		return m_cardsToDraw.Count;
	}

	public List<Card> GetCardsToDraw()
	{
		return m_cardsToDraw;
	}

	public bool IsCardDrawHandled(Card card)
	{
		if (card == null)
		{
			return false;
		}
		return m_cardsToDraw.Contains(card);
	}

	public void DrawCardImmediately(Card card)
	{
		int num = m_cardsToDraw.IndexOf(card);
		if (num >= 0)
		{
			Card[] cards = m_cardsToDraw.GetRange(0, num + 1).ToArray();
			m_cardsToDraw.RemoveRange(0, num + 1);
			StartCoroutine(DrawCardsImmediatelyWithTiming(cards));
		}
	}

	private IEnumerator DrawCardsImmediatelyWithTiming(Card[] cards)
	{
		foreach (Card card in cards)
		{
			while (card.IsActorLoading())
			{
				yield return null;
			}
			card.DrawFriendlyCard();
		}
	}

	public void BeginPlayingTurnEvents()
	{
		StartCoroutine(RunTurnEventsWithTiming());
	}

	public void NotifyOfTriggerVisual()
	{
		DisplayTwoScoops();
	}

	public bool IsBlockingInput()
	{
		return m_blockingInput;
	}

	public bool IsTurnStartIndicatorShowing()
	{
		if (m_turnStartInstance == null)
		{
			return false;
		}
		return m_turnStartInstance.IsShown();
	}

	private void DisplayTwoScoops()
	{
		if (!m_twoScoopsDisplayed)
		{
			m_twoScoopsDisplayed = true;
			if (!(m_turnStartInstance == null))
			{
				m_turnStartInstance.SetReminderText(GameState.Get().GetGameEntity().GetTurnStartReminderText());
				m_turnStartInstance.Show();
				SoundManager.Get().LoadAndPlay("ALERT_YourTurn_0v2.prefab:201bcb34d33384e48ab226f7e797771f");
			}
		}
	}

	private IEnumerator RunTurnEventsWithTiming()
	{
		if (!IsListeningForTurnEvents())
		{
			yield break;
		}
		m_listeningForTurnEvents = false;
		if (GameMgr.Get().IsAI() && !m_twoScoopsDisplayed && !m_twoScoopsRequestFromMetadata)
		{
			yield return new WaitForSeconds(1f);
		}
		DisplayTwoScoops();
		Player friendlyPlayer = GameState.Get().GetFriendlySidePlayer();
		friendlyPlayer.ResetUnresolvedManaToBeReadied();
		friendlyPlayer.ReadyManaCrystal(m_manaCrystalsFilled);
		friendlyPlayer.AddManaCrystal(m_manaCrystalsGained, isTurnStart: true);
		friendlyPlayer.UpdateManaCounter();
		HandleExhaustedChanges();
		if (m_turnStartInstance != null && m_turnStartInstance.IsShown())
		{
			yield return new WaitForSeconds(m_turnStartInstance.GetDesiredDelayDuration());
		}
		if (m_cardsToDraw.Count > 0)
		{
			Card[] cardsToDraw = m_cardsToDraw.ToArray();
			m_cardsToDraw.Clear();
			friendlyPlayer.GetHandZone().UpdateLayout();
			Card[] array = cardsToDraw;
			foreach (Card card in array)
			{
				while (card.IsActorLoading())
				{
					yield return null;
				}
				card.DrawFriendlyCard();
			}
			while (!AreDrawnCardsReady(cardsToDraw))
			{
				yield return null;
			}
			if (HasActionsAfterCardDraw())
			{
				yield return new WaitForSeconds(0.35f);
			}
		}
		if ((bool)m_spellController)
		{
			m_spellController.DoPowerTaskList();
			while (m_spellController.IsProcessingTaskList())
			{
				yield return null;
			}
			m_spellController = null;
		}
		if (GameState.Get().IsLocalSidePlayerTurn())
		{
			m_blockingInput = false;
			EndTurnButton.Get().OnTurnStartManagerFinished();
			GameState.Get().GetGameEntity().OnTurnStartManagerFinished();
			if (GameState.Get().IsInMainOptionMode())
			{
				GameState.Get().EnterMainOptionMode();
			}
			GameState.Get().FireFriendlyTurnStartedEvent();
		}
	}

	private bool AreDrawnCardsReady(Card[] cardsToDraw)
	{
		return !Array.Find(cardsToDraw, (Card card) => !card.IsActorReady());
	}

	private bool HasActionsAfterCardDraw()
	{
		if (m_spellController != null)
		{
			return true;
		}
		Network.EntityChoices friendlyEntityChoices = GameState.Get().GetFriendlyEntityChoices();
		if (friendlyEntityChoices != null && friendlyEntityChoices.ChoiceType == CHOICE_TYPE.GENERAL)
		{
			return true;
		}
		return false;
	}

	private void HandleExhaustedChanges()
	{
		foreach (CardChange item in m_exhaustedChangesToHandle)
		{
			Card card = item.m_card;
			if (!(card == null))
			{
				TAG_ZONE zone = card.GetEntity().GetZone();
				if (zone == TAG_ZONE.PLAY || zone == TAG_ZONE.SECRET)
				{
					card.ShowExhaustedChange(item.m_tagDelta.newValue);
				}
			}
		}
		m_exhaustedChangesToHandle.Clear();
	}

	private void OnCreateGame(GameState.CreateGamePhase phase, object userData)
	{
		if (phase == GameState.CreateGamePhase.CREATED)
		{
			GameState.Get().UnregisterCreateGameListener(OnCreateGame);
			StartCoroutine(InstantiateTurnStartIndicator());
		}
	}

	private void OnGameOver(TAG_PLAYSTATE playState, object userData)
	{
		StopAllCoroutines();
	}
}
