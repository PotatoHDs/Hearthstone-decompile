using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200034D RID: 845
public class TurnStartManager : MonoBehaviour
{
	// Token: 0x06003117 RID: 12567 RVA: 0x000FCE04 File Offset: 0x000FB004
	private void Awake()
	{
		TurnStartManager.s_instance = this;
		if (GameState.Get() == null)
		{
			Debug.LogError(string.Format("TurnStartManager.Awake() - GameState already Shutdown before TurnStartManager was loaded.", Array.Empty<object>()));
			return;
		}
		if (GameState.Get().IsGameCreated())
		{
			base.StartCoroutine(this.InstantiateTurnStartIndicator());
		}
		else
		{
			GameState.Get().RegisterCreateGameListener(new GameState.CreateGameCallback(this.OnCreateGame));
		}
		GameState.Get().RegisterGameOverListener(new GameState.GameOverCallback(this.OnGameOver), null);
	}

	// Token: 0x06003118 RID: 12568 RVA: 0x000FCE7D File Offset: 0x000FB07D
	private void OnDestroy()
	{
		TurnStartManager.s_instance = null;
	}

	// Token: 0x06003119 RID: 12569 RVA: 0x000FCE85 File Offset: 0x000FB085
	public static TurnStartManager Get()
	{
		return TurnStartManager.s_instance;
	}

	// Token: 0x0600311A RID: 12570 RVA: 0x000FCE8C File Offset: 0x000FB08C
	public bool IsListeningForTurnEvents()
	{
		return this.m_listeningForTurnEvents;
	}

	// Token: 0x0600311B RID: 12571 RVA: 0x000FCE94 File Offset: 0x000FB094
	public void BeginListeningForTurnEvents(bool fromMetadata = false)
	{
		this.m_cardsToDraw.Clear();
		this.m_exhaustedChangesToHandle.Clear();
		this.m_manaCrystalsGained = 0;
		this.m_manaCrystalsFilled = 0;
		this.m_twoScoopsDisplayed = false;
		this.m_listeningForTurnEvents = true;
		this.m_blockingInput = true;
		this.m_twoScoopsRequestFromMetadata = fromMetadata;
	}

	// Token: 0x0600311C RID: 12572 RVA: 0x000FCEE1 File Offset: 0x000FB0E1
	public void NotifyOfManaCrystalGained(int amount)
	{
		this.m_manaCrystalsGained += amount;
	}

	// Token: 0x0600311D RID: 12573 RVA: 0x000FCEF1 File Offset: 0x000FB0F1
	public void NotifyOfManaCrystalFilled(int amount)
	{
		this.m_manaCrystalsFilled += amount;
	}

	// Token: 0x0600311E RID: 12574 RVA: 0x000FCF01 File Offset: 0x000FB101
	public void NotifyOfCardDrawn(Entity drawnEntity)
	{
		this.m_cardsToDraw.Add(drawnEntity.GetCard());
	}

	// Token: 0x0600311F RID: 12575 RVA: 0x000FCF14 File Offset: 0x000FB114
	public void NotifyOfExhaustedChange(Card card, TagDelta tagChange)
	{
		TurnStartManager.CardChange item = new TurnStartManager.CardChange
		{
			m_card = card,
			m_tagDelta = tagChange
		};
		this.m_exhaustedChangesToHandle.Add(item);
	}

	// Token: 0x06003120 RID: 12576 RVA: 0x000FCF44 File Offset: 0x000FB144
	private int GetCurrentAlternativeAppearanceIndex()
	{
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return 0;
		}
		GameEntity gameEntity = gameState.GetGameEntity();
		if (gameEntity == null)
		{
			return 0;
		}
		return gameEntity.GetTag(GAME_TAG.TURN_INDICATOR_ALTERNATIVE_APPEARANCE);
	}

	// Token: 0x06003121 RID: 12577 RVA: 0x000FCF74 File Offset: 0x000FB174
	private bool IsTurnStartIndicatorDisabled()
	{
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return false;
		}
		GameEntity gameEntity = gameState.GetGameEntity();
		return gameEntity != null && gameEntity.GetTag(GAME_TAG.DISABLE_TURN_INDICATORS) > 0;
	}

	// Token: 0x06003122 RID: 12578 RVA: 0x000FCFA6 File Offset: 0x000FB1A6
	public void ApplyAlternativeAppearance()
	{
		base.StartCoroutine(TurnStartManager.Get().InstantiateTurnStartIndicator());
	}

	// Token: 0x06003123 RID: 12579 RVA: 0x000FCFB9 File Offset: 0x000FB1B9
	private IEnumerator InstantiateTurnStartIndicator()
	{
		if (this.m_turnStartInstance != null)
		{
			while (this.m_turnStartInstance.IsShown())
			{
				yield return null;
			}
			UnityEngine.Object.Destroy(this.m_turnStartInstance);
		}
		if (this.IsTurnStartIndicatorDisabled())
		{
			yield break;
		}
		int currentAlternativeAppearanceIndex = this.GetCurrentAlternativeAppearanceIndex();
		if (currentAlternativeAppearanceIndex == 0)
		{
			this.m_turnStartInstance = UnityEngine.Object.Instantiate<TurnStartIndicator>(this.m_turnStartPrefab);
		}
		else if (this.m_alternativeTurnStartPrefab.Count >= currentAlternativeAppearanceIndex)
		{
			this.m_turnStartInstance = UnityEngine.Object.Instantiate<TurnStartIndicator>(this.m_alternativeTurnStartPrefab[currentAlternativeAppearanceIndex - 1]);
		}
		if (this.m_turnStartInstance != null)
		{
			this.m_turnStartInstance.transform.parent = base.transform;
		}
		else
		{
			Debug.LogError(string.Format("TurnStartManager.InstantiateTurnStartIndicator() - FAILED to instantiate turn start prefab for appearance {0}", currentAlternativeAppearanceIndex));
		}
		yield break;
	}

	// Token: 0x06003124 RID: 12580 RVA: 0x000FCFC8 File Offset: 0x000FB1C8
	public Spell GetExtraTurnSpell(bool isFriendly = true)
	{
		Spell result = this.m_friendlyExtraTurnSpellInstance;
		if (!isFriendly)
		{
			result = this.m_opponentExtraTurnSpellInstance;
		}
		return result;
	}

	// Token: 0x06003125 RID: 12581 RVA: 0x000FCFE7 File Offset: 0x000FB1E7
	public Spell SetExtraTurnSpell(Spell extraTurnSpell, bool isFriendly = true)
	{
		if (isFriendly)
		{
			this.m_friendlyExtraTurnSpellInstance = extraTurnSpell;
		}
		else
		{
			this.m_opponentExtraTurnSpellInstance = extraTurnSpell;
		}
		return extraTurnSpell;
	}

	// Token: 0x06003126 RID: 12582 RVA: 0x000FD000 File Offset: 0x000FB200
	public void NotifyOfExtraTurn(Spell extraTurnSpell, bool isEnding = false, bool isFriendly = true)
	{
		if (!isEnding)
		{
			if (extraTurnSpell == null)
			{
				if (isFriendly)
				{
					extraTurnSpell = UnityEngine.Object.Instantiate<Spell>(this.m_FriendlyExtraTurnSpell);
				}
				else
				{
					extraTurnSpell = UnityEngine.Object.Instantiate<Spell>(this.m_OpponentExtraTurnSpell);
				}
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
		this.SetExtraTurnSpell(extraTurnSpell, isFriendly);
	}

	// Token: 0x06003127 RID: 12583 RVA: 0x000FD081 File Offset: 0x000FB281
	public void NotifyOfSpellController(SpellController spellController)
	{
		this.m_spellController = spellController;
		this.BeginPlayingTurnEvents();
	}

	// Token: 0x06003128 RID: 12584 RVA: 0x000FD090 File Offset: 0x000FB290
	public void NotifyOfStartOfTurnChoice()
	{
		this.BeginPlayingTurnEvents();
	}

	// Token: 0x06003129 RID: 12585 RVA: 0x000FD098 File Offset: 0x000FB298
	public SpellController GetSpellController()
	{
		return this.m_spellController;
	}

	// Token: 0x0600312A RID: 12586 RVA: 0x000FD0A0 File Offset: 0x000FB2A0
	public int GetNumCardsToDraw()
	{
		return this.m_cardsToDraw.Count;
	}

	// Token: 0x0600312B RID: 12587 RVA: 0x000FD0AD File Offset: 0x000FB2AD
	public List<Card> GetCardsToDraw()
	{
		return this.m_cardsToDraw;
	}

	// Token: 0x0600312C RID: 12588 RVA: 0x000FD0B5 File Offset: 0x000FB2B5
	public bool IsCardDrawHandled(Card card)
	{
		return !(card == null) && this.m_cardsToDraw.Contains(card);
	}

	// Token: 0x0600312D RID: 12589 RVA: 0x000FD0D0 File Offset: 0x000FB2D0
	public void DrawCardImmediately(Card card)
	{
		int num = this.m_cardsToDraw.IndexOf(card);
		if (num < 0)
		{
			return;
		}
		Card[] cards = this.m_cardsToDraw.GetRange(0, num + 1).ToArray();
		this.m_cardsToDraw.RemoveRange(0, num + 1);
		base.StartCoroutine(this.DrawCardsImmediatelyWithTiming(cards));
	}

	// Token: 0x0600312E RID: 12590 RVA: 0x000FD121 File Offset: 0x000FB321
	private IEnumerator DrawCardsImmediatelyWithTiming(Card[] cards)
	{
		foreach (Card card in cards)
		{
			while (card.IsActorLoading())
			{
				yield return null;
			}
			card.DrawFriendlyCard();
			card = null;
		}
		Card[] array = null;
		yield break;
	}

	// Token: 0x0600312F RID: 12591 RVA: 0x000FD130 File Offset: 0x000FB330
	public void BeginPlayingTurnEvents()
	{
		base.StartCoroutine(this.RunTurnEventsWithTiming());
	}

	// Token: 0x06003130 RID: 12592 RVA: 0x000FD13F File Offset: 0x000FB33F
	public void NotifyOfTriggerVisual()
	{
		this.DisplayTwoScoops();
	}

	// Token: 0x06003131 RID: 12593 RVA: 0x000FD147 File Offset: 0x000FB347
	public bool IsBlockingInput()
	{
		return this.m_blockingInput;
	}

	// Token: 0x06003132 RID: 12594 RVA: 0x000FD14F File Offset: 0x000FB34F
	public bool IsTurnStartIndicatorShowing()
	{
		return !(this.m_turnStartInstance == null) && this.m_turnStartInstance.IsShown();
	}

	// Token: 0x06003133 RID: 12595 RVA: 0x000FD16C File Offset: 0x000FB36C
	private void DisplayTwoScoops()
	{
		if (this.m_twoScoopsDisplayed)
		{
			return;
		}
		this.m_twoScoopsDisplayed = true;
		if (this.m_turnStartInstance == null)
		{
			return;
		}
		this.m_turnStartInstance.SetReminderText(GameState.Get().GetGameEntity().GetTurnStartReminderText());
		this.m_turnStartInstance.Show();
		SoundManager.Get().LoadAndPlay("ALERT_YourTurn_0v2.prefab:201bcb34d33384e48ab226f7e797771f");
	}

	// Token: 0x06003134 RID: 12596 RVA: 0x000FD1D1 File Offset: 0x000FB3D1
	private IEnumerator RunTurnEventsWithTiming()
	{
		if (!this.IsListeningForTurnEvents())
		{
			yield break;
		}
		this.m_listeningForTurnEvents = false;
		if (GameMgr.Get().IsAI() && !this.m_twoScoopsDisplayed && !this.m_twoScoopsRequestFromMetadata)
		{
			yield return new WaitForSeconds(1f);
		}
		this.DisplayTwoScoops();
		Player friendlyPlayer = GameState.Get().GetFriendlySidePlayer();
		friendlyPlayer.ResetUnresolvedManaToBeReadied();
		friendlyPlayer.ReadyManaCrystal(this.m_manaCrystalsFilled);
		friendlyPlayer.AddManaCrystal(this.m_manaCrystalsGained, true);
		friendlyPlayer.UpdateManaCounter();
		this.HandleExhaustedChanges();
		if (this.m_turnStartInstance != null && this.m_turnStartInstance.IsShown())
		{
			yield return new WaitForSeconds(this.m_turnStartInstance.GetDesiredDelayDuration());
		}
		if (this.m_cardsToDraw.Count > 0)
		{
			Card[] cardsToDraw = this.m_cardsToDraw.ToArray();
			this.m_cardsToDraw.Clear();
			friendlyPlayer.GetHandZone().UpdateLayout();
			foreach (Card card in cardsToDraw)
			{
				while (card.IsActorLoading())
				{
					yield return null;
				}
				card.DrawFriendlyCard();
				card = null;
			}
			Card[] array = null;
			while (!this.AreDrawnCardsReady(cardsToDraw))
			{
				yield return null;
			}
			if (this.HasActionsAfterCardDraw())
			{
				yield return new WaitForSeconds(0.35f);
			}
			cardsToDraw = null;
		}
		if (this.m_spellController)
		{
			this.m_spellController.DoPowerTaskList();
			while (this.m_spellController.IsProcessingTaskList())
			{
				yield return null;
			}
			this.m_spellController = null;
		}
		if (GameState.Get().IsLocalSidePlayerTurn())
		{
			this.m_blockingInput = false;
			EndTurnButton.Get().OnTurnStartManagerFinished();
			GameState.Get().GetGameEntity().OnTurnStartManagerFinished();
			if (GameState.Get().IsInMainOptionMode())
			{
				GameState.Get().EnterMainOptionMode();
			}
			GameState.Get().FireFriendlyTurnStartedEvent();
		}
		yield break;
	}

	// Token: 0x06003135 RID: 12597 RVA: 0x000FD1E0 File Offset: 0x000FB3E0
	private bool AreDrawnCardsReady(Card[] cardsToDraw)
	{
		return !Array.Find<Card>(cardsToDraw, (Card card) => !card.IsActorReady());
	}

	// Token: 0x06003136 RID: 12598 RVA: 0x000FD210 File Offset: 0x000FB410
	private bool HasActionsAfterCardDraw()
	{
		if (this.m_spellController != null)
		{
			return true;
		}
		Network.EntityChoices friendlyEntityChoices = GameState.Get().GetFriendlyEntityChoices();
		return friendlyEntityChoices != null && friendlyEntityChoices.ChoiceType == CHOICE_TYPE.GENERAL;
	}

	// Token: 0x06003137 RID: 12599 RVA: 0x000FD248 File Offset: 0x000FB448
	private void HandleExhaustedChanges()
	{
		foreach (TurnStartManager.CardChange cardChange in this.m_exhaustedChangesToHandle)
		{
			Card card = cardChange.m_card;
			if (!(card == null))
			{
				TAG_ZONE zone = card.GetEntity().GetZone();
				if (zone == TAG_ZONE.PLAY || zone == TAG_ZONE.SECRET)
				{
					card.ShowExhaustedChange(cardChange.m_tagDelta.newValue);
				}
			}
		}
		this.m_exhaustedChangesToHandle.Clear();
	}

	// Token: 0x06003138 RID: 12600 RVA: 0x000FD2D4 File Offset: 0x000FB4D4
	private void OnCreateGame(GameState.CreateGamePhase phase, object userData)
	{
		if (phase != GameState.CreateGamePhase.CREATED)
		{
			return;
		}
		GameState.Get().UnregisterCreateGameListener(new GameState.CreateGameCallback(this.OnCreateGame));
		base.StartCoroutine(this.InstantiateTurnStartIndicator());
	}

	// Token: 0x06003139 RID: 12601 RVA: 0x000FD2FF File Offset: 0x000FB4FF
	private void OnGameOver(TAG_PLAYSTATE playState, object userData)
	{
		base.StopAllCoroutines();
	}

	// Token: 0x04001B4A RID: 6986
	public TurnStartIndicator m_turnStartPrefab;

	// Token: 0x04001B4B RID: 6987
	public List<TurnStartIndicator> m_alternativeTurnStartPrefab;

	// Token: 0x04001B4C RID: 6988
	public Spell m_OpponentExtraTurnSpell;

	// Token: 0x04001B4D RID: 6989
	public Spell m_FriendlyExtraTurnSpell;

	// Token: 0x04001B4E RID: 6990
	private static TurnStartManager s_instance;

	// Token: 0x04001B4F RID: 6991
	private TurnStartIndicator m_turnStartInstance;

	// Token: 0x04001B50 RID: 6992
	private Spell m_opponentExtraTurnSpellInstance;

	// Token: 0x04001B51 RID: 6993
	private Spell m_friendlyExtraTurnSpellInstance;

	// Token: 0x04001B52 RID: 6994
	private bool m_listeningForTurnEvents;

	// Token: 0x04001B53 RID: 6995
	private int m_manaCrystalsGained;

	// Token: 0x04001B54 RID: 6996
	private int m_manaCrystalsFilled;

	// Token: 0x04001B55 RID: 6997
	private List<Card> m_cardsToDraw = new List<Card>();

	// Token: 0x04001B56 RID: 6998
	private List<TurnStartManager.CardChange> m_exhaustedChangesToHandle = new List<TurnStartManager.CardChange>();

	// Token: 0x04001B57 RID: 6999
	private SpellController m_spellController;

	// Token: 0x04001B58 RID: 7000
	private bool m_blockingInput;

	// Token: 0x04001B59 RID: 7001
	private bool m_twoScoopsDisplayed;

	// Token: 0x04001B5A RID: 7002
	private bool m_twoScoopsRequestFromMetadata;

	// Token: 0x020016F4 RID: 5876
	private class CardChange
	{
		// Token: 0x0400B30D RID: 45837
		public Card m_card;

		// Token: 0x0400B30E RID: 45838
		public TagDelta m_tagDelta;
	}
}
