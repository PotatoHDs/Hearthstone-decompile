using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000302 RID: 770
[CustomEditClass]
public class ChoiceCardMgr : MonoBehaviour
{
	// Token: 0x060028FE RID: 10494 RVA: 0x000CFD40 File Offset: 0x000CDF40
	private void Awake()
	{
		ChoiceCardMgr.s_instance = this;
		foreach (ChoiceCardMgr.TagSpecificChoiceEffect tagSpecificChoiceEffect in this.m_TagSpecificChoiceEffectData)
		{
			GAME_TAG tag = tagSpecificChoiceEffect.m_Tag;
			if (tag <= GAME_TAG.GEARS)
			{
				if (tag != GAME_TAG.ADAPT)
				{
					if (tag == GAME_TAG.GEARS)
					{
						if (tagSpecificChoiceEffect.m_ValueSpellMap.Count > 0)
						{
							this.m_GearsChoiceEffectData = tagSpecificChoiceEffect.m_ValueSpellMap[0].m_ChoiceEffectData;
						}
					}
				}
				else if (tagSpecificChoiceEffect.m_ValueSpellMap.Count > 0)
				{
					this.m_AdaptChoiceEffectData = tagSpecificChoiceEffect.m_ValueSpellMap[0].m_ChoiceEffectData;
				}
			}
			else if (tag != GAME_TAG.USE_DISCOVER_VISUALS)
			{
				if (tag == GAME_TAG.GOOD_OL_GENERIC_FRIENDLY_DRAGON_DISCOVER_VISUALS)
				{
					if (tagSpecificChoiceEffect.m_ValueSpellMap.Count > 0)
					{
						this.m_DragonChoiceEffectData = tagSpecificChoiceEffect.m_ValueSpellMap[0].m_ChoiceEffectData;
					}
				}
			}
			else if (tagSpecificChoiceEffect.m_ValueSpellMap.Count > 0)
			{
				this.m_DiscoverChoiceEffectData = tagSpecificChoiceEffect.m_ValueSpellMap[0].m_ChoiceEffectData;
			}
		}
	}

	// Token: 0x060028FF RID: 10495 RVA: 0x000CFE70 File Offset: 0x000CE070
	private void OnDestroy()
	{
		ChoiceCardMgr.s_instance = null;
	}

	// Token: 0x06002900 RID: 10496 RVA: 0x000CFE78 File Offset: 0x000CE078
	private void Start()
	{
		if (GameState.Get() == null)
		{
			Debug.LogError(string.Format("ChoiceCardMgr.Start() - GameState already Shutdown before ChoiceCardMgr was loaded.", Array.Empty<object>()));
			return;
		}
		GameState.Get().RegisterEntityChoicesReceivedListener(new GameState.EntityChoicesReceivedCallback(this.OnEntityChoicesReceived));
		GameState.Get().RegisterEntitiesChosenReceivedListener(new GameState.EntitiesChosenReceivedCallback(this.OnEntitiesChosenReceived));
		GameState.Get().RegisterGameOverListener(new GameState.GameOverCallback(this.OnGameOver), null);
	}

	// Token: 0x06002901 RID: 10497 RVA: 0x000CFEE7 File Offset: 0x000CE0E7
	public static ChoiceCardMgr Get()
	{
		return ChoiceCardMgr.s_instance;
	}

	// Token: 0x06002902 RID: 10498 RVA: 0x000CFEEE File Offset: 0x000CE0EE
	public bool RestoreEnlargedHandAfterChoice()
	{
		return this.m_restoreEnlargedHand;
	}

	// Token: 0x06002903 RID: 10499 RVA: 0x000CFEF8 File Offset: 0x000CE0F8
	public List<Card> GetFriendlyCards()
	{
		if (this.m_subOptionState != null)
		{
			return this.m_subOptionState.m_cards;
		}
		int friendlyPlayerId = GameState.Get().GetFriendlyPlayerId();
		ChoiceCardMgr.ChoiceState choiceState;
		if (this.m_choiceStateMap.TryGetValue(friendlyPlayerId, out choiceState))
		{
			return choiceState.m_cards;
		}
		return null;
	}

	// Token: 0x06002904 RID: 10500 RVA: 0x000CFF3C File Offset: 0x000CE13C
	public bool IsShown()
	{
		return this.m_subOptionState != null || this.m_choiceStateMap.Count > 0;
	}

	// Token: 0x06002905 RID: 10501 RVA: 0x000CFF5C File Offset: 0x000CE15C
	public bool IsFriendlyShown()
	{
		if (this.m_subOptionState != null)
		{
			return true;
		}
		int friendlyPlayerId = GameState.Get().GetFriendlyPlayerId();
		return this.m_choiceStateMap.ContainsKey(friendlyPlayerId);
	}

	// Token: 0x06002906 RID: 10502 RVA: 0x000CFF8F File Offset: 0x000CE18F
	public bool HasSubOption()
	{
		return this.m_subOptionState != null;
	}

	// Token: 0x06002907 RID: 10503 RVA: 0x000CFF9A File Offset: 0x000CE19A
	public Card GetSubOptionParentCard()
	{
		if (this.m_subOptionState != null)
		{
			return this.m_subOptionState.m_parentCard;
		}
		return null;
	}

	// Token: 0x06002908 RID: 10504 RVA: 0x000CFFB1 File Offset: 0x000CE1B1
	public void ClearSubOptions()
	{
		this.m_subOptionState = null;
	}

	// Token: 0x06002909 RID: 10505 RVA: 0x000CFFBA File Offset: 0x000CE1BA
	public void ShowSubOptions(Card parentCard)
	{
		this.m_subOptionState = new ChoiceCardMgr.SubOptionState();
		this.m_subOptionState.m_parentCard = parentCard;
		base.StartCoroutine(this.WaitThenShowSubOptions());
	}

	// Token: 0x0600290A RID: 10506 RVA: 0x000CFFE0 File Offset: 0x000CE1E0
	public void QuenePendingCancelSubOptions()
	{
		this.m_pendingCancelSubOptionState = this.m_subOptionState;
	}

	// Token: 0x0600290B RID: 10507 RVA: 0x000CFFEE File Offset: 0x000CE1EE
	public bool HasPendingCancelSubOptions()
	{
		return this.m_pendingCancelSubOptionState != null && this.m_pendingCancelSubOptionState == this.m_subOptionState;
	}

	// Token: 0x0600290C RID: 10508 RVA: 0x000D0008 File Offset: 0x000CE208
	public void ClearPendingCancelSubOptions()
	{
		this.m_pendingCancelSubOptionState = null;
	}

	// Token: 0x0600290D RID: 10509 RVA: 0x000D0014 File Offset: 0x000CE214
	public bool IsWaitingToShowSubOptions()
	{
		if (!this.HasSubOption())
		{
			return false;
		}
		Entity entity = this.m_subOptionState.m_parentCard.GetEntity();
		Player controller = entity.GetController();
		Zone zone = this.m_subOptionState.m_parentCard.GetZone();
		if (entity.IsMinion())
		{
			if (zone.m_ServerTag == TAG_ZONE.SETASIDE)
			{
				return false;
			}
			ZonePlay battlefieldZone = controller.GetBattlefieldZone();
			if (zone != battlefieldZone)
			{
				return true;
			}
			if (this.m_subOptionState.m_parentCard.GetZonePosition() == 0)
			{
				return true;
			}
		}
		if (entity.IsHero())
		{
			ZoneHero heroZone = controller.GetHeroZone();
			if (zone != heroZone)
			{
				return true;
			}
			if (!this.m_subOptionState.m_parentCard.IsActorReady())
			{
				return true;
			}
		}
		return !entity.HasSubCards();
	}

	// Token: 0x0600290E RID: 10510 RVA: 0x000D00C8 File Offset: 0x000CE2C8
	public void CancelSubOptions()
	{
		if (!this.HasSubOption())
		{
			return;
		}
		Entity entity = this.m_subOptionState.m_parentCard.GetEntity();
		Card card = entity.GetCard();
		for (int i = 0; i < this.m_subOptionState.m_cards.Count; i++)
		{
			Spell subOptionSpell = card.GetSubOptionSpell(i, 0, false);
			if (subOptionSpell)
			{
				SpellStateType activeState = subOptionSpell.GetActiveState();
				if (activeState != SpellStateType.NONE && activeState != SpellStateType.CANCEL)
				{
					subOptionSpell.ActivateState(SpellStateType.CANCEL);
				}
			}
		}
		card.ActivateHandStateSpells(false);
		if (entity.IsHeroPowerOrGameModeButton())
		{
			entity.SetTagAndHandleChange<int>(GAME_TAG.EXHAUSTED, 0);
		}
		this.HideSubOptions(null);
	}

	// Token: 0x0600290F RID: 10511 RVA: 0x000D015A File Offset: 0x000CE35A
	public void OnSubOptionClicked(Entity chosenEntity)
	{
		if (!this.HasSubOption())
		{
			return;
		}
		this.HideSubOptions(chosenEntity);
	}

	// Token: 0x06002910 RID: 10512 RVA: 0x000D016C File Offset: 0x000CE36C
	public bool HasChoices()
	{
		return this.m_choiceStateMap.Count > 0;
	}

	// Token: 0x06002911 RID: 10513 RVA: 0x000D017C File Offset: 0x000CE37C
	public bool HasChoices(int playerId)
	{
		return this.m_choiceStateMap.ContainsKey(playerId);
	}

	// Token: 0x06002912 RID: 10514 RVA: 0x000D018C File Offset: 0x000CE38C
	public bool HasFriendlyChoices()
	{
		int friendlyPlayerId = GameState.Get().GetFriendlyPlayerId();
		return this.HasChoices(friendlyPlayerId);
	}

	// Token: 0x06002913 RID: 10515 RVA: 0x000D01AC File Offset: 0x000CE3AC
	public PowerTaskList GetPreChoiceTaskList(int playerId)
	{
		ChoiceCardMgr.ChoiceState choiceState;
		if (this.m_choiceStateMap.TryGetValue(playerId, out choiceState))
		{
			return choiceState.m_preTaskList;
		}
		return null;
	}

	// Token: 0x06002914 RID: 10516 RVA: 0x000D01D4 File Offset: 0x000CE3D4
	public PowerTaskList GetFriendlyPreChoiceTaskList()
	{
		int friendlyPlayerId = GameState.Get().GetFriendlyPlayerId();
		return this.GetPreChoiceTaskList(friendlyPlayerId);
	}

	// Token: 0x06002915 RID: 10517 RVA: 0x000D01F4 File Offset: 0x000CE3F4
	public bool IsWaitingToStartChoices(int playerId)
	{
		ChoiceCardMgr.ChoiceState choiceState;
		return this.m_choiceStateMap.TryGetValue(playerId, out choiceState) && choiceState.m_waitingToStart;
	}

	// Token: 0x06002916 RID: 10518 RVA: 0x000D021C File Offset: 0x000CE41C
	public bool IsFriendlyWaitingToStartChoices()
	{
		int friendlyPlayerId = GameState.Get().GetFriendlyPlayerId();
		return this.IsWaitingToStartChoices(friendlyPlayerId);
	}

	// Token: 0x06002917 RID: 10519 RVA: 0x000D023C File Offset: 0x000CE43C
	public void OnSendChoices(Network.EntityChoices choicePacket, List<Entity> chosenEntities)
	{
		if (choicePacket.ChoiceType != CHOICE_TYPE.GENERAL)
		{
			return;
		}
		int friendlyPlayerId = GameState.Get().GetFriendlyPlayerId();
		ChoiceCardMgr.ChoiceState choiceState;
		if (!this.m_choiceStateMap.TryGetValue(friendlyPlayerId, out choiceState))
		{
			Error.AddDevFatal("ChoiceCardMgr.OnSendChoices() - there is no ChoiceState for friendly player {0}", new object[]
			{
				friendlyPlayerId
			});
			return;
		}
		choiceState.m_chosenEntities = new List<Entity>(chosenEntities);
		this.ConcealChoicesFromInput(friendlyPlayerId, choiceState);
	}

	// Token: 0x06002918 RID: 10520 RVA: 0x000D029C File Offset: 0x000CE49C
	public void OnChosenEntityAdded(Entity entity)
	{
		if (entity == null)
		{
			Log.Gameplay.PrintError("ChoiceCardMgr.OnChosenEntityAdded(): null entity passed!", Array.Empty<object>());
			return;
		}
		Network.EntityChoices friendlyEntityChoices = GameState.Get().GetFriendlyEntityChoices();
		if (friendlyEntityChoices == null || friendlyEntityChoices.IsSingleChoice())
		{
			return;
		}
		if (!this.m_choiceStateMap.ContainsKey(GameState.Get().GetFriendlyPlayerId()))
		{
			return;
		}
		ChoiceCardMgr.ChoiceState choiceState = this.m_choiceStateMap[GameState.Get().GetFriendlyPlayerId()];
		if (choiceState.m_xObjs == null)
		{
			Log.Gameplay.PrintError("ChoiceCardMgr.OnChosenEntityAdded(): ChoiceState does not have an m_xObjs map!", Array.Empty<object>());
			return;
		}
		if (choiceState.m_xObjs.ContainsKey(entity.GetEntityId()))
		{
			return;
		}
		Card card = entity.GetCard();
		if (card == null)
		{
			Log.Gameplay.PrintError("ChoiceCardMgr.OnChosenEntityAdded(): Entity does not have a card!", Array.Empty<object>());
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_ChoiceData.m_xPrefab);
		TransformUtil.AttachAndPreserveLocalTransform(gameObject.transform, card.transform);
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localPosition = Vector3.zero;
		choiceState.m_xObjs.Add(entity.GetEntityId(), gameObject);
	}

	// Token: 0x06002919 RID: 10521 RVA: 0x000D03B0 File Offset: 0x000CE5B0
	public void OnChosenEntityRemoved(Entity entity)
	{
		if (entity == null)
		{
			Log.Gameplay.PrintError("ChoiceCardMgr.OnChosenEntityRemoved(): null entity passed!", Array.Empty<object>());
			return;
		}
		Network.EntityChoices friendlyEntityChoices = GameState.Get().GetFriendlyEntityChoices();
		if (friendlyEntityChoices == null || friendlyEntityChoices.IsSingleChoice())
		{
			return;
		}
		if (!this.m_choiceStateMap.ContainsKey(GameState.Get().GetFriendlyPlayerId()))
		{
			return;
		}
		ChoiceCardMgr.ChoiceState choiceState = this.m_choiceStateMap[GameState.Get().GetFriendlyPlayerId()];
		if (choiceState.m_xObjs == null)
		{
			Log.Gameplay.PrintError("ChoiceCardMgr.OnChosenEntityRemoved(): ChoiceState does not have an m_xObjs map!", Array.Empty<object>());
			return;
		}
		int entityId = entity.GetEntityId();
		if (!choiceState.m_xObjs.ContainsKey(entityId))
		{
			return;
		}
		GameObject obj = choiceState.m_xObjs[entityId];
		choiceState.m_xObjs.Remove(entityId);
		UnityEngine.Object.Destroy(obj);
	}

	// Token: 0x0600291A RID: 10522 RVA: 0x000D046D File Offset: 0x000CE66D
	private void OnEntityChoicesReceived(Network.EntityChoices choices, PowerTaskList preChoiceTaskList, object userData)
	{
		if (choices.ChoiceType != CHOICE_TYPE.GENERAL)
		{
			return;
		}
		base.StartCoroutine(this.WaitThenStartChoices(choices, preChoiceTaskList));
	}

	// Token: 0x0600291B RID: 10523 RVA: 0x000D0488 File Offset: 0x000CE688
	private bool OnEntitiesChosenReceived(Network.EntitiesChosen chosen, object userData)
	{
		if (chosen.ChoiceType != CHOICE_TYPE.GENERAL)
		{
			return false;
		}
		base.StartCoroutine(this.WaitThenConcealChoicesFromPacket(chosen));
		return true;
	}

	// Token: 0x0600291C RID: 10524 RVA: 0x000D04A4 File Offset: 0x000CE6A4
	private void OnGameOver(TAG_PLAYSTATE playState, object userData)
	{
		base.StopAllCoroutines();
		this.CancelSubOptions();
		this.CancelChoices();
	}

	// Token: 0x0600291D RID: 10525 RVA: 0x000D04B8 File Offset: 0x000CE6B8
	private IEnumerator WaitThenStartChoices(Network.EntityChoices choices, PowerTaskList preChoiceTaskList)
	{
		int playerId = choices.PlayerId;
		ChoiceCardMgr.ChoiceState state = new ChoiceCardMgr.ChoiceState();
		this.m_choiceStateMap.Add(playerId, state);
		state.m_waitingToStart = true;
		state.m_hasBeenConcealed = false;
		state.m_hasBeenRevealed = false;
		state.m_choiceID = choices.ID;
		state.m_hideChosen = choices.HideChosen;
		state.m_sourceEntityId = choices.Source;
		state.m_preTaskList = preChoiceTaskList;
		state.m_xObjs = new Map<int, GameObject>();
		Entity entity = GameState.Get().GetEntity(choices.Source);
		if (entity != null)
		{
			state.m_showFromDeck = entity.HasTag(GAME_TAG.SHOW_DISCOVER_FROM_DECK);
		}
		PowerProcessor powerProcessor = GameState.Get().GetPowerProcessor();
		if (powerProcessor.HasTaskList(state.m_preTaskList))
		{
			Log.Power.Print("ChoiceCardMgr.WaitThenShowChoices() - id={0} WAIT for taskList {1}", new object[]
			{
				choices.ID,
				preChoiceTaskList.GetId()
			});
		}
		while (powerProcessor.HasTaskList(state.m_preTaskList))
		{
			yield return null;
		}
		HistoryManager historyManager = HistoryManager.Get();
		if (historyManager.HasBigCard() && historyManager.GetCurrentBigCard().GetEntity().GetEntityId() == state.m_sourceEntityId)
		{
			historyManager.HandleClickOnBigCard(historyManager.GetCurrentBigCard());
		}
		Log.Power.Print("ChoiceCardMgr.WaitThenShowChoices() - id={0} BEGIN", new object[]
		{
			choices.ID
		});
		List<Card> linkedChoiceCards = new List<Card>();
		Entity entity2 = GameState.Get().GetEntity(state.m_sourceEntityId);
		for (int j = 0; j < choices.Entities.Count; j++)
		{
			int id = choices.Entities[j];
			Entity entity3 = GameState.Get().GetEntity(id);
			Card card = entity3.GetCard();
			if (card == null)
			{
				Error.AddDevFatal("ChoiceCardMgr.WaitThenShowChoices() - Entity {0} (option {1}) has no Card", new object[]
				{
					entity3,
					j
				});
			}
			else
			{
				if (entity3.HasTag(GAME_TAG.LINKED_ENTITY))
				{
					int realTimeLinkedEntityId = entity3.GetRealTimeLinkedEntityId();
					Entity entity4 = GameState.Get().GetEntity(realTimeLinkedEntityId);
					if (entity4 != null && entity4.GetCard() != null)
					{
						linkedChoiceCards.Add(entity4.GetCard());
					}
				}
				state.m_cards.Add(card);
				base.StartCoroutine(this.LoadChoiceCardActors(entity2, entity3, card));
			}
		}
		int num;
		for (int i = 0; i < linkedChoiceCards.Count; i = num)
		{
			Card linkedCard = linkedChoiceCards[i];
			while (linkedCard != null && !linkedCard.IsActorReady())
			{
				yield return null;
			}
			linkedCard = null;
			num = i + 1;
		}
		for (int i = 0; i < state.m_cards.Count; i = num)
		{
			Card linkedCard = state.m_cards[i];
			while (!this.IsChoiceCardReady(linkedCard))
			{
				yield return null;
			}
			linkedCard = null;
			num = i + 1;
		}
		int friendlyPlayerId = GameState.Get().GetFriendlyPlayerId();
		bool friendly = playerId == friendlyPlayerId;
		if (friendly)
		{
			while (GameState.Get().IsTurnStartManagerBlockingInput())
			{
				if (GameState.Get().IsTurnStartManagerActive())
				{
					TurnStartManager.Get().NotifyOfStartOfTurnChoice();
				}
				yield return null;
			}
		}
		state.m_isFriendly = friendly;
		state.m_waitingToStart = false;
		this.PopulateTransformDatas(state);
		this.StartChoices(state);
		yield break;
	}

	// Token: 0x0600291E RID: 10526 RVA: 0x000D04D5 File Offset: 0x000CE6D5
	private IEnumerator LoadChoiceCardActors(Entity source, Entity entity, Card card)
	{
		while (!this.IsEntityReady(entity))
		{
			yield return null;
		}
		card.HideCard();
		while (!this.IsCardReady(card))
		{
			yield return null;
		}
		CHOICE_ACTOR choice_ACTOR = CHOICE_ACTOR.CARD;
		if (source.HasTag(GAME_TAG.CHOICE_ACTOR_TYPE))
		{
			choice_ACTOR = (CHOICE_ACTOR)source.GetTag(GAME_TAG.CHOICE_ACTOR_TYPE);
		}
		if (choice_ACTOR > CHOICE_ACTOR.CARD && choice_ACTOR == CHOICE_ACTOR.HERO)
		{
			this.LoadHeroChoiceCardActor(source, entity, card);
			card.ActivateHandStateSpells(false);
		}
		else
		{
			card.ForceLoadHandActor();
			card.ActivateHandStateSpells(false);
		}
		yield break;
	}

	// Token: 0x0600291F RID: 10527 RVA: 0x000D04FC File Offset: 0x000CE6FC
	private void LoadHeroChoiceCardActor(Entity source, Entity entity, Card card)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("Choose_Hero.prefab:1834beb8747ef06439f3a1b86a35ff3d", AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Log.Gameplay.PrintWarning(string.Format("ChoiceCardManager.LoadHeroChoiceActor() - FAILED to load actor \"{0}\"", "Choose_Hero.prefab:1834beb8747ef06439f3a1b86a35ff3d"), Array.Empty<object>());
			return;
		}
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Log.Gameplay.PrintWarning(string.Format("ChoiceCardManager.LoadHeroChoiceActor() - ERROR actor \"{0}\" has no Actor component", "Choose_Hero.prefab:1834beb8747ef06439f3a1b86a35ff3d"), Array.Empty<object>());
			return;
		}
		if (card.GetActor() != null)
		{
			card.GetActor().Destroy();
		}
		card.SetActor(component);
		component.SetCard(card);
		component.SetCardDefFromCard(card);
		component.SetPremium(card.GetPremium());
		component.UpdateAllComponents();
		component.SetEntity(entity);
		component.UpdateAllComponents();
		component.SetUnlit();
		SceneUtils.SetLayer(component.gameObject, base.gameObject.layer, null);
		component.GetMeshRenderer(false).gameObject.layer = 8;
		this.ConfigureHeroChoiceActor(source, entity, component as HeroChoiceActor);
	}

	// Token: 0x06002920 RID: 10528 RVA: 0x000D0608 File Offset: 0x000CE808
	private void ConfigureHeroChoiceActor(Entity source, Entity entity, HeroChoiceActor actor)
	{
		if (actor == null)
		{
			return;
		}
		if (entity == null || source == null)
		{
			actor.SetNameTextActive(false);
			return;
		}
		CHOICE_NAME_DISPLAY choice_NAME_DISPLAY = CHOICE_NAME_DISPLAY.INVALID;
		if (source.HasTag(GAME_TAG.CHOICE_NAME_DISPLAY_TYPE))
		{
			choice_NAME_DISPLAY = (CHOICE_NAME_DISPLAY)source.GetTag(GAME_TAG.CHOICE_NAME_DISPLAY_TYPE);
		}
		switch (choice_NAME_DISPLAY)
		{
		case CHOICE_NAME_DISPLAY.PLAYER:
		{
			int tag = entity.GetTag(GAME_TAG.PLAYER_ID);
			if (tag == 0)
			{
				tag = entity.GetTag(GAME_TAG.PLAYER_ID_LOOKUP);
			}
			actor.SetNameText(GameState.Get().GetGameEntity().GetBestNameForPlayer(tag));
			actor.SetNameTextActive(true);
			return;
		}
		case CHOICE_NAME_DISPLAY.HERO:
			actor.SetNameText(entity.GetName());
			actor.SetNameTextActive(true);
			return;
		}
		actor.SetNameTextActive(false);
	}

	// Token: 0x06002921 RID: 10529 RVA: 0x000D06B0 File Offset: 0x000CE8B0
	private bool IsChoiceCardReady(Card card)
	{
		Entity entity = card.GetEntity();
		return this.IsEntityReady(entity) && this.IsCardReady(card) && this.IsCardActorReady(card);
	}

	// Token: 0x06002922 RID: 10530 RVA: 0x000D06E8 File Offset: 0x000CE8E8
	private void PopulateTransformDatas(ChoiceCardMgr.ChoiceState state)
	{
		bool isFriendly = state.m_isFriendly;
		state.m_cardTransforms.Clear();
		int count = state.m_cards.Count;
		float num = this.m_ChoiceData.m_HorizontalPadding;
		if (isFriendly && count > this.m_CommonData.m_MaxCardsBeforeAdjusting)
		{
			num = this.GetPaddingForCardCount(count);
		}
		float num2 = isFriendly ? this.m_CommonData.m_FriendlyCardWidth : this.m_CommonData.m_OpponentCardWidth;
		float num3 = 1f;
		if (isFriendly && count > this.m_CommonData.m_MaxCardsBeforeAdjusting)
		{
			num3 = this.GetScaleForCardCount(count);
			num2 *= num3;
		}
		float num4 = 0.5f * num2;
		float num5 = num2 * (float)count + num * (float)(count - 1);
		float num6 = 0.5f * num5;
		string text = isFriendly ? this.m_ChoiceData.m_FriendlyBoneName : this.m_ChoiceData.m_OpponentBoneName;
		if (UniversalInputManager.UsePhoneUI)
		{
			text += "_phone";
		}
		Transform transform = Board.Get().FindBone(text);
		Vector3 position = transform.position;
		Vector3 eulerAngles = transform.rotation.eulerAngles;
		Vector3 localScale = transform.localScale;
		float num7 = position.x - num6 + num4;
		for (int i = 0; i < count; i++)
		{
			ChoiceCardMgr.TransformData item = default(ChoiceCardMgr.TransformData);
			item.Position = new Vector3
			{
				x = num7,
				y = position.y,
				z = position.z
			};
			Vector3 localScale2 = localScale;
			localScale2.x *= num3;
			localScale2.y *= num3;
			localScale2.z *= num3;
			item.LocalScale = localScale2;
			item.RotationAngles = eulerAngles;
			state.m_cardTransforms.Add(item);
			num7 += num2 + num;
		}
	}

	// Token: 0x06002923 RID: 10531 RVA: 0x000D08AC File Offset: 0x000CEAAC
	private float GetScaleForCardCount(int cardCount)
	{
		if (cardCount <= this.m_CommonData.m_MaxCardsBeforeAdjusting)
		{
			return 1f;
		}
		if (cardCount == 4)
		{
			return this.m_CommonData.m_FourCardScale;
		}
		if (cardCount == 5)
		{
			return this.m_CommonData.m_FiveCardScale;
		}
		return this.m_CommonData.m_SixPlusCardScale;
	}

	// Token: 0x06002924 RID: 10532 RVA: 0x000D0908 File Offset: 0x000CEB08
	private float GetPaddingForCardCount(int cardCount)
	{
		if (cardCount <= this.m_CommonData.m_MaxCardsBeforeAdjusting)
		{
			return this.m_ChoiceData.m_HorizontalPadding;
		}
		if (cardCount == 4)
		{
			return this.m_ChoiceData.m_HorizontalPaddingFourCards;
		}
		if (cardCount == 5)
		{
			return this.m_ChoiceData.m_HorizontalPaddingFiveCards;
		}
		return this.m_ChoiceData.m_HorizontalPaddingSixPlusCards;
	}

	// Token: 0x06002925 RID: 10533 RVA: 0x000D096C File Offset: 0x000CEB6C
	private void StartChoices(ChoiceCardMgr.ChoiceState state)
	{
		this.m_lastShownChoiceState = state;
		int count = state.m_cards.Count;
		for (int i = 0; i < count; i++)
		{
			Card card = state.m_cards[i];
			ChoiceCardMgr.TransformData transformData = state.m_cardTransforms[i];
			card.transform.position = transformData.Position;
			card.transform.rotation = Quaternion.Euler(transformData.RotationAngles);
			card.transform.localScale = transformData.LocalScale;
		}
		this.RevealChoiceCards(state);
	}

	// Token: 0x06002926 RID: 10534 RVA: 0x000D09F4 File Offset: 0x000CEBF4
	private void RevealChoiceCards(ChoiceCardMgr.ChoiceState state)
	{
		Spell customChoiceRevealSpell = this.GetCustomChoiceRevealSpell(state);
		if (customChoiceRevealSpell != null)
		{
			this.RevealChoiceCardsUsingCustomSpell(customChoiceRevealSpell, state);
			return;
		}
		this.DefaultRevealChoiceCards(state);
	}

	// Token: 0x06002927 RID: 10535 RVA: 0x000D0A24 File Offset: 0x000CEC24
	private void DefaultRevealChoiceCards(ChoiceCardMgr.ChoiceState choiceState)
	{
		bool isFriendly = choiceState.m_isFriendly;
		if (isFriendly)
		{
			this.ShowChoiceUi(choiceState);
		}
		this.ShowChoiceCards(choiceState, isFriendly);
		choiceState.m_hasBeenRevealed = true;
	}

	// Token: 0x06002928 RID: 10536 RVA: 0x000D0A51 File Offset: 0x000CEC51
	private void ShowChoiceCards(ChoiceCardMgr.ChoiceState state, bool friendly)
	{
		base.StartCoroutine(this.PlayCardAnimation(state, friendly));
	}

	// Token: 0x06002929 RID: 10537 RVA: 0x000D0A64 File Offset: 0x000CEC64
	private void GetDeckTransform(ZoneDeck deckZone, out Vector3 startPos, out Vector3 startRot, out Vector3 startScale)
	{
		Actor thicknessForLayout = deckZone.GetThicknessForLayout();
		startPos = thicknessForLayout.GetMeshRenderer(false).bounds.center + Card.IN_DECK_OFFSET;
		startRot = Card.IN_DECK_ANGLES;
		startScale = Card.IN_DECK_SCALE;
	}

	// Token: 0x0600292A RID: 10538 RVA: 0x000D0AB3 File Offset: 0x000CECB3
	private IEnumerator PlayCardAnimation(ChoiceCardMgr.ChoiceState state, bool friendly)
	{
		if (state.m_showFromDeck)
		{
			state.m_showFromDeck = false;
			ZoneDeck deckZone = GameState.Get().GetEntity(state.m_sourceEntityId).GetController().GetDeckZone();
			Vector3 deckPos;
			Vector3 deckRot;
			Vector3 deckScale;
			this.GetDeckTransform(deckZone, out deckPos, out deckRot, out deckScale);
			float timingBonus = 0.1f;
			int cardCount = state.m_cards.Count;
			int num;
			for (int i = 0; i < cardCount; i = num)
			{
				Card card = state.m_cards[i];
				card.ShowCard();
				GameObject cardObject = card.gameObject;
				cardObject.transform.position = deckPos;
				cardObject.transform.rotation = Quaternion.Euler(deckRot);
				cardObject.transform.localScale = deckScale;
				ChoiceCardMgr.TransformData transformData = state.m_cardTransforms[i];
				iTween.Stop(cardObject);
				iTween.MoveTo(cardObject, iTween.Hash(new object[]
				{
					"path",
					new Vector3[]
					{
						cardObject.transform.position,
						new Vector3(cardObject.transform.position.x, cardObject.transform.position.y + 3.6f, cardObject.transform.position.z),
						transformData.Position
					},
					"time",
					MulliganManager.ANIMATION_TIME_DEAL_CARD,
					"easetype",
					iTween.EaseType.easeInSineOutExpo
				}));
				iTween.ScaleTo(cardObject, MulliganManager.FRIENDLY_PLAYER_CARD_SCALE, MulliganManager.ANIMATION_TIME_DEAL_CARD);
				iTween.RotateTo(cardObject, iTween.Hash(new object[]
				{
					"rotation",
					new Vector3(0f, 0f, 0f),
					"time",
					MulliganManager.ANIMATION_TIME_DEAL_CARD,
					"delay",
					MulliganManager.ANIMATION_TIME_DEAL_CARD / 16f
				}));
				yield return new WaitForSeconds(0.04f);
				SoundManager.Get().LoadAndPlay("FX_GameStart09_CardsOntoTable.prefab:da502e035813b5742a04d2ef4f588255", cardObject);
				yield return new WaitForSeconds(0.05f + timingBonus);
				timingBonus = 0f;
				cardObject = null;
				num = i + 1;
			}
			deckPos = default(Vector3);
			deckRot = default(Vector3);
			deckScale = default(Vector3);
		}
		else
		{
			int count = state.m_cards.Count;
			for (int j = 0; j < count; j++)
			{
				Card card2 = state.m_cards[j];
				ChoiceCardMgr.TransformData transformData2 = state.m_cardTransforms[j];
				card2.ShowCard();
				card2.transform.localScale = ChoiceCardMgr.INVISIBLE_SCALE;
				iTween.Stop(card2.gameObject);
				iTween.RotateTo(card2.gameObject, transformData2.RotationAngles, this.m_ChoiceData.m_CardShowTime);
				iTween.ScaleTo(card2.gameObject, transformData2.LocalScale, this.m_ChoiceData.m_CardShowTime);
				iTween.MoveTo(card2.gameObject, transformData2.Position, this.m_ChoiceData.m_CardShowTime);
				this.ActivateChoiceCardStateSpells(card2);
			}
		}
		this.PlayChoiceEffects(state, friendly);
		yield break;
	}

	// Token: 0x0600292B RID: 10539 RVA: 0x000D0AD0 File Offset: 0x000CECD0
	private void PlayChoiceEffects(ChoiceCardMgr.ChoiceState state, bool friendly)
	{
		if (!friendly)
		{
			return;
		}
		Entity entity = GameState.Get().GetEntity(state.m_sourceEntityId);
		if (entity == null)
		{
			return;
		}
		ChoiceCardMgr.ChoiceEffectData choiceEffectDataForCard = this.GetChoiceEffectDataForCard(entity.GetCard());
		if (choiceEffectDataForCard == null || choiceEffectDataForCard.m_Spell == null)
		{
			return;
		}
		if (state.m_hasBeenRevealed && !choiceEffectDataForCard.m_AlwaysPlayEffect)
		{
			return;
		}
		Spell.StateFinishedCallback callback = delegate(Spell spell, SpellStateType prevStateType, object userData)
		{
			if (spell.GetActiveState() == SpellStateType.NONE)
			{
				UnityEngine.Object.Destroy(spell.gameObject);
			}
		};
		if (choiceEffectDataForCard.m_PlayOncePerCard)
		{
			using (List<Card>.Enumerator enumerator = state.m_cards.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Card card = enumerator.Current;
					Spell spell3 = UnityEngine.Object.Instantiate<Spell>(choiceEffectDataForCard.m_Spell);
					TransformUtil.AttachAndPreserveLocalTransform(spell3.transform, card.GetActor().transform);
					spell3.AddStateFinishedCallback(callback);
					spell3.Activate();
					state.m_choiceEffectSpells.Add(spell3);
				}
				return;
			}
		}
		Spell spell2 = UnityEngine.Object.Instantiate<Spell>(choiceEffectDataForCard.m_Spell);
		spell2.AddStateFinishedCallback(callback);
		spell2.Activate();
		state.m_choiceEffectSpells.Add(spell2);
	}

	// Token: 0x0600292C RID: 10540 RVA: 0x000D0BF8 File Offset: 0x000CEDF8
	private void ActivateChoiceCardStateSpells(Card card)
	{
		if (card.GetActor() != null)
		{
			if (card.GetActor().UseCoinManaGemForChoiceCard())
			{
				card.GetActor().ActivateSpellBirthState(SpellType.COIN_MANA_GEM);
			}
			else
			{
				card.GetActor().DestroySpell(SpellType.COIN_MANA_GEM);
			}
			if (card.GetActor().UseTechLevelManaGem())
			{
				Spell spell = card.GetActor().GetSpell(SpellType.TECH_LEVEL_MANA_GEM);
				if (spell != null && card.GetEntity() != null)
				{
					spell.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("TechLevel").Value = card.GetEntity().GetTechLevel();
					spell.ActivateState(SpellStateType.BIRTH);
					return;
				}
			}
			else
			{
				card.GetActor().DestroySpell(SpellType.TECH_LEVEL_MANA_GEM);
			}
		}
	}

	// Token: 0x0600292D RID: 10541 RVA: 0x000D0CB4 File Offset: 0x000CEEB4
	private void DeactivateChoiceCardStateSpells(Card card)
	{
		if (card.GetActor() != null)
		{
			if (card.GetActor().UseCoinManaGemForChoiceCard())
			{
				card.GetActor().DestroySpell(SpellType.COIN_MANA_GEM);
			}
			if (card.GetActor().UseTechLevelManaGem())
			{
				card.GetActor().DestroySpell(SpellType.TECH_LEVEL_MANA_GEM);
			}
		}
	}

	// Token: 0x0600292E RID: 10542 RVA: 0x000D0D0C File Offset: 0x000CEF0C
	private void DeactivateChoiceEffects(ChoiceCardMgr.ChoiceState state)
	{
		foreach (Spell spell in state.m_choiceEffectSpells)
		{
			if (!(spell == null) && spell.HasUsableState(SpellStateType.DEATH))
			{
				spell.ActivateState(SpellStateType.DEATH);
			}
		}
		state.m_choiceEffectSpells.Clear();
	}

	// Token: 0x0600292F RID: 10543 RVA: 0x000D0D7C File Offset: 0x000CEF7C
	private ChoiceCardMgr.ChoiceEffectData GetChoiceEffectDataForCard(Card sourceCard)
	{
		if (sourceCard == null)
		{
			return null;
		}
		foreach (ChoiceCardMgr.CardSpecificChoiceEffect cardSpecificChoiceEffect in this.m_CardSpecificChoiceEffectData)
		{
			if (cardSpecificChoiceEffect.m_CardID == sourceCard.GetEntity().GetCardId())
			{
				return cardSpecificChoiceEffect.m_ChoiceEffectData;
			}
		}
		foreach (ChoiceCardMgr.TagSpecificChoiceEffect tagSpecificChoiceEffect in this.m_TagSpecificChoiceEffectData)
		{
			if (sourceCard.GetEntity().HasTag(tagSpecificChoiceEffect.m_Tag))
			{
				foreach (ChoiceCardMgr.TagValueSpecificChoiceEffect tagValueSpecificChoiceEffect in tagSpecificChoiceEffect.m_ValueSpellMap)
				{
					if (tagValueSpecificChoiceEffect.m_Value == sourceCard.GetEntity().GetTag(tagSpecificChoiceEffect.m_Tag))
					{
						return tagValueSpecificChoiceEffect.m_ChoiceEffectData;
					}
				}
			}
		}
		if (sourceCard.GetEntity().HasTag(GAME_TAG.USE_DISCOVER_VISUALS))
		{
			return this.m_DiscoverChoiceEffectData;
		}
		if (sourceCard.GetEntity().HasReferencedTag(GAME_TAG.ADAPT))
		{
			return this.m_AdaptChoiceEffectData;
		}
		if (sourceCard.GetEntity().HasTag(GAME_TAG.GEARS))
		{
			return this.m_GearsChoiceEffectData;
		}
		if (sourceCard.GetEntity().HasTag(GAME_TAG.GOOD_OL_GENERIC_FRIENDLY_DRAGON_DISCOVER_VISUALS))
		{
			return this.m_DragonChoiceEffectData;
		}
		return null;
	}

	// Token: 0x06002930 RID: 10544 RVA: 0x000D0F14 File Offset: 0x000CF114
	private IEnumerator WaitThenConcealChoicesFromPacket(Network.EntitiesChosen chosen)
	{
		int playerId = chosen.PlayerId;
		ChoiceCardMgr.ChoiceState choiceState;
		if (this.m_choiceStateMap.TryGetValue(playerId, out choiceState))
		{
			if (choiceState.m_waitingToStart || !choiceState.m_hasBeenRevealed)
			{
				Log.Power.Print("ChoiceCardMgr.WaitThenHideChoicesFromPacket() - id={0} BEGIN WAIT for EntityChoice", new object[]
				{
					chosen.ID
				});
				while (choiceState.m_waitingToStart)
				{
					yield return null;
				}
				while (!choiceState.m_hasBeenRevealed)
				{
					yield return null;
				}
				yield return new WaitForSeconds(this.m_ChoiceData.m_MinShowTime);
			}
		}
		else if (this.m_lastShownChoiceState.m_choiceID == chosen.ID)
		{
			choiceState = this.m_lastShownChoiceState;
		}
		if (choiceState == null)
		{
			Log.Power.Print("ChoiceCardMgr.WaitThenHideChoicesFromPacket(): Unable to find ChoiceState corresponding to EntitiesChosen packet with ID %d.", new object[]
			{
				chosen.ID
			});
			Log.Power.Print("ChoiceCardMgr.WaitThenHideChoicesFromPacket() - id={0} END WAIT", new object[]
			{
				chosen.ID
			});
			GameState.Get().OnEntitiesChosenProcessed(chosen);
			yield break;
		}
		this.ResolveConflictBetweenLocalChoiceAndServerPacket(choiceState, chosen);
		Log.Power.Print("ChoiceCardMgr.WaitThenHideChoicesFromPacket() - id={0} END WAIT", new object[]
		{
			chosen.ID
		});
		this.ConcealChoicesFromPacket(playerId, choiceState, chosen);
		yield break;
	}

	// Token: 0x06002931 RID: 10545 RVA: 0x000D0F2C File Offset: 0x000CF12C
	private void ResolveConflictBetweenLocalChoiceAndServerPacket(ChoiceCardMgr.ChoiceState choiceState, Network.EntitiesChosen chosen)
	{
		if (!this.DoesLocalChoiceMatchPacket(choiceState.m_chosenEntities, chosen.Entities))
		{
			choiceState.m_chosenEntities = new List<Entity>();
			foreach (int id in chosen.Entities)
			{
				Entity entity = GameState.Get().GetEntity(id);
				if (entity != null)
				{
					choiceState.m_chosenEntities.Add(entity);
				}
			}
			if (choiceState.m_hasBeenConcealed)
			{
				foreach (Card card in choiceState.m_cards)
				{
					card.ShowCard();
				}
				choiceState.m_hasBeenConcealed = false;
			}
		}
	}

	// Token: 0x06002932 RID: 10546 RVA: 0x000D1004 File Offset: 0x000CF204
	private bool DoesLocalChoiceMatchPacket(List<Entity> localChoices, List<int> packetChoices)
	{
		if (localChoices == null || packetChoices == null)
		{
			Log.Power.Print(string.Format("ChoiceCardMgr.DoesLocalChoiceMatchPacket(): Null list passed in! localChoices={0}, packetChoices={1}.", localChoices, packetChoices), Array.Empty<object>());
			return false;
		}
		if (localChoices.Count != packetChoices.Count)
		{
			return false;
		}
		for (int i = 0; i < packetChoices.Count; i++)
		{
			int id = packetChoices[i];
			Entity entity = GameState.Get().GetEntity(id);
			if (!localChoices.Contains(entity))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06002933 RID: 10547 RVA: 0x000D1078 File Offset: 0x000CF278
	private void ConcealChoicesFromPacket(int playerId, ChoiceCardMgr.ChoiceState choiceState, Network.EntitiesChosen chosen)
	{
		if (choiceState.m_isFriendly)
		{
			this.HideChoiceUI();
		}
		Spell customChoiceConcealSpell = this.GetCustomChoiceConcealSpell(choiceState);
		if (customChoiceConcealSpell != null)
		{
			this.ConcealChoiceCardsUsingCustomSpell(customChoiceConcealSpell, choiceState, chosen);
			return;
		}
		this.DefaultConcealChoicesFromPacket(playerId, choiceState, chosen);
	}

	// Token: 0x06002934 RID: 10548 RVA: 0x000D10B8 File Offset: 0x000CF2B8
	private void DefaultConcealChoicesFromPacket(int playerId, ChoiceCardMgr.ChoiceState choiceState, Network.EntitiesChosen chosen)
	{
		if (!choiceState.m_hasBeenConcealed)
		{
			List<Card> cards = choiceState.m_cards;
			bool hideChosen = choiceState.m_hideChosen;
			for (int i = 0; i < cards.Count; i++)
			{
				Card card = cards[i];
				if (hideChosen || !this.WasCardChosen(card, chosen.Entities))
				{
					card.DeactivateHandStateSpells(card.GetActor());
					this.DeactivateChoiceCardStateSpells(card);
					card.HideCard();
				}
			}
			this.DeactivateChoiceEffects(choiceState);
			choiceState.m_hasBeenConcealed = true;
		}
		this.OnFinishedConcealChoices(playerId);
		GameState.Get().OnEntitiesChosenProcessed(chosen);
	}

	// Token: 0x06002935 RID: 10549 RVA: 0x000D1140 File Offset: 0x000CF340
	private bool WasCardChosen(Card card, List<int> chosenEntityIds)
	{
		Entity entity = card.GetEntity();
		int entityId = entity.GetEntityId();
		return chosenEntityIds.FindIndex((int currEntityId) => entityId == currEntityId) >= 0;
	}

	// Token: 0x06002936 RID: 10550 RVA: 0x000D1180 File Offset: 0x000CF380
	private void ConcealChoicesFromInput(int playerId, ChoiceCardMgr.ChoiceState choiceState)
	{
		if (choiceState.m_isFriendly)
		{
			this.HideChoiceUI();
		}
		if (this.GetCustomChoiceConcealSpell(choiceState) == null)
		{
			for (int i = 0; i < choiceState.m_cards.Count; i++)
			{
				Card card = choiceState.m_cards[i];
				Entity entity = card.GetEntity();
				if (choiceState.m_hideChosen || !choiceState.m_chosenEntities.Contains(entity))
				{
					card.HideCard();
					card.DeactivateHandStateSpells(card.GetActor());
					this.DeactivateChoiceCardStateSpells(card);
				}
			}
			this.DeactivateChoiceEffects(choiceState);
			choiceState.m_hasBeenConcealed = true;
			this.OnFinishedConcealChoices(playerId);
		}
	}

	// Token: 0x06002937 RID: 10551 RVA: 0x000D1218 File Offset: 0x000CF418
	private void OnFinishedConcealChoices(int playerId)
	{
		if (this.m_choiceStateMap.ContainsKey(playerId))
		{
			foreach (GameObject obj in this.m_choiceStateMap[playerId].m_xObjs.Values)
			{
				UnityEngine.Object.Destroy(obj);
			}
			this.m_choiceStateMap.Remove(playerId);
		}
	}

	// Token: 0x06002938 RID: 10552 RVA: 0x000D1294 File Offset: 0x000CF494
	private void HideChoiceCards(ChoiceCardMgr.ChoiceState state)
	{
		for (int i = 0; i < state.m_cards.Count; i++)
		{
			Card card = state.m_cards[i];
			this.HideChoiceCard(card);
		}
		this.DeactivateChoiceEffects(state);
	}

	// Token: 0x06002939 RID: 10553 RVA: 0x000D12D4 File Offset: 0x000CF4D4
	private void HideChoiceCard(Card card)
	{
		Action<object> action = delegate(object userData)
		{
			((Card)userData).HideCard();
		};
		iTween.Stop(card.gameObject);
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			ChoiceCardMgr.INVISIBLE_SCALE,
			"time",
			this.m_ChoiceData.m_CardHideTime,
			"oncomplete",
			action,
			"oncompleteparams",
			card,
			"oncompletetarget",
			base.gameObject
		});
		iTween.ScaleTo(card.gameObject, args);
	}

	// Token: 0x0600293A RID: 10554 RVA: 0x000D137F File Offset: 0x000CF57F
	private void ShowChoiceUi(ChoiceCardMgr.ChoiceState choiceState)
	{
		this.ShowChoiceBanner(choiceState.m_cards);
		this.ShowChoiceButtons();
		this.HideEnlargedHand();
	}

	// Token: 0x0600293B RID: 10555 RVA: 0x000D1399 File Offset: 0x000CF599
	private void HideChoiceUI()
	{
		this.HideChoiceBanner();
		this.HideChoiceButtons();
		this.RestoreEnlargedHand();
	}

	// Token: 0x0600293C RID: 10556 RVA: 0x000D13B0 File Offset: 0x000CF5B0
	private void ShowChoiceBanner(List<Card> cards)
	{
		this.HideChoiceBanner();
		Network.EntityChoices friendlyEntityChoices = GameState.Get().GetFriendlyEntityChoices();
		Transform transform = Board.Get().FindBone(this.m_ChoiceData.m_BannerBoneName);
		this.m_choiceBanner = UnityEngine.Object.Instantiate<Banner>(this.m_ChoiceData.m_BannerPrefab, transform.position, transform.rotation);
		string text = GameState.Get().GetGameEntity().CustomChoiceBannerText();
		if (text == null)
		{
			if (friendlyEntityChoices.IsSingleChoice())
			{
				Entity entity = GameState.Get().GetEntity(friendlyEntityChoices.Source);
				if (entity != null)
				{
					string cardDiscoverString = GameDbf.GetIndex().GetCardDiscoverString(entity.GetCardId());
					if (cardDiscoverString != null)
					{
						text = GameStrings.Get(cardDiscoverString);
					}
				}
				if (text != null)
				{
					goto IL_11F;
				}
				text = GameStrings.Get("GAMEPLAY_CHOOSE_ONE");
				using (List<Card>.Enumerator enumerator = cards.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Card card = enumerator.Current;
						if (null != card && card.GetEntity().IsHeroPower())
						{
							text = GameStrings.Get("GAMEPLAY_CHOOSE_ONE_HERO_POWER");
							break;
						}
					}
					goto IL_11F;
				}
			}
			text = string.Format("[PH] Choose {0} to {1}", friendlyEntityChoices.CountMin, friendlyEntityChoices.CountMax);
		}
		IL_11F:
		this.m_choiceBanner.SetText(text);
		Vector3 localScale = this.m_choiceBanner.transform.localScale;
		this.m_choiceBanner.transform.localScale = ChoiceCardMgr.INVISIBLE_SCALE;
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			localScale,
			"time",
			this.m_ChoiceData.m_UiShowTime
		});
		iTween.ScaleTo(this.m_choiceBanner.gameObject, args);
	}

	// Token: 0x0600293D RID: 10557 RVA: 0x000D156C File Offset: 0x000CF76C
	private void HideChoiceBanner()
	{
		if (!this.m_choiceBanner)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.m_choiceBanner.gameObject);
	}

	// Token: 0x0600293E RID: 10558 RVA: 0x000D158C File Offset: 0x000CF78C
	private void ShowChoiceButtons()
	{
		Network.EntityChoices friendlyEntityChoices = GameState.Get().GetFriendlyEntityChoices();
		if (friendlyEntityChoices == null)
		{
			return;
		}
		this.HideChoiceButtons();
		string text = this.m_ChoiceData.m_ToggleChoiceButtonBoneName;
		if (UniversalInputManager.UsePhoneUI)
		{
			text += "_phone";
		}
		this.m_toggleChoiceButton = this.CreateChoiceButton(text, new UIEvent.Handler(this.ChoiceButton_OnPress), new UIEvent.Handler(this.ToggleChoiceButton_OnRelease), GameStrings.Get("GLOBAL_HIDE"));
		if (!friendlyEntityChoices.IsSingleChoice())
		{
			text = this.m_ChoiceData.m_ConfirmChoiceButtonBoneName;
			if (UniversalInputManager.UsePhoneUI)
			{
				text += "_phone";
			}
			this.m_confirmChoiceButton = this.CreateChoiceButton(text, new UIEvent.Handler(this.ChoiceButton_OnPress), new UIEvent.Handler(this.ConfirmChoiceButton_OnRelease), GameStrings.Get("GLOBAL_CONFIRM"));
		}
	}

	// Token: 0x0600293F RID: 10559 RVA: 0x000D165C File Offset: 0x000CF85C
	private NormalButton CreateChoiceButton(string boneName, UIEvent.Handler OnPressHandler, UIEvent.Handler OnReleaseHandler, string buttonText)
	{
		NormalButton component = AssetLoader.Get().InstantiatePrefab(this.m_ChoiceData.m_ButtonPrefab, AssetLoadingOptions.IgnorePrefabPosition).GetComponent<NormalButton>();
		component.GetButtonUberText().TextAlpha = 1f;
		Transform source = Board.Get().FindBone(boneName);
		TransformUtil.CopyWorld(component, source);
		this.m_friendlyChoicesShown = true;
		component.AddEventListener(UIEventType.PRESS, OnPressHandler);
		component.AddEventListener(UIEventType.RELEASE, OnReleaseHandler);
		component.SetText(buttonText);
		component.m_button.GetComponent<Spell>().ActivateState(SpellStateType.BIRTH);
		return component;
	}

	// Token: 0x06002940 RID: 10560 RVA: 0x000D16E0 File Offset: 0x000CF8E0
	private void HideChoiceButtons()
	{
		if (this.m_toggleChoiceButton != null)
		{
			UnityEngine.Object.Destroy(this.m_toggleChoiceButton.gameObject);
			this.m_toggleChoiceButton = null;
		}
		if (this.m_confirmChoiceButton != null)
		{
			UnityEngine.Object.Destroy(this.m_confirmChoiceButton.gameObject);
			this.m_confirmChoiceButton = null;
		}
	}

	// Token: 0x06002941 RID: 10561 RVA: 0x000D1738 File Offset: 0x000CF938
	private void HideEnlargedHand()
	{
		ZoneHand handZone = GameState.Get().GetFriendlySidePlayer().GetHandZone();
		if (handZone.HandEnlarged())
		{
			this.m_restoreEnlargedHand = true;
			handZone.SetHandEnlarged(false);
		}
	}

	// Token: 0x06002942 RID: 10562 RVA: 0x000D176C File Offset: 0x000CF96C
	private void RestoreEnlargedHand()
	{
		if (this.m_restoreEnlargedHand)
		{
			this.m_restoreEnlargedHand = false;
			if (GameState.Get().IsInTargetMode())
			{
				return;
			}
			ZoneHand handZone = GameState.Get().GetFriendlySidePlayer().GetHandZone();
			if (!handZone.HandEnlarged())
			{
				handZone.SetHandEnlarged(true);
			}
		}
	}

	// Token: 0x06002943 RID: 10563 RVA: 0x000D17B4 File Offset: 0x000CF9B4
	private void ChoiceButton_OnPress(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("UI_MouseClick_01.prefab:fa537702a0db1c3478c989967458788b");
	}

	// Token: 0x06002944 RID: 10564 RVA: 0x000D17CC File Offset: 0x000CF9CC
	private void ToggleChoiceButton_OnRelease(UIEvent e)
	{
		int friendlyPlayerId = GameState.Get().GetFriendlyPlayerId();
		ChoiceCardMgr.ChoiceState state = this.m_choiceStateMap[friendlyPlayerId];
		if (this.m_friendlyChoicesShown)
		{
			this.m_toggleChoiceButton.SetText(GameStrings.Get("GLOBAL_SHOW"));
			this.HideChoiceCards(state);
			this.m_friendlyChoicesShown = false;
		}
		else
		{
			this.m_toggleChoiceButton.SetText(GameStrings.Get("GLOBAL_HIDE"));
			this.ShowChoiceCards(state, true);
			this.m_friendlyChoicesShown = true;
		}
		this.ToggleChoiceBannerVisibility(this.m_friendlyChoicesShown);
	}

	// Token: 0x06002945 RID: 10565 RVA: 0x000D184E File Offset: 0x000CFA4E
	private void ToggleChoiceBannerVisibility(bool visible)
	{
		this.m_choiceBanner.gameObject.SetActive(visible);
	}

	// Token: 0x06002946 RID: 10566 RVA: 0x000D1861 File Offset: 0x000CFA61
	private void ConfirmChoiceButton_OnRelease(UIEvent e)
	{
		GameState.Get().SendChoices();
	}

	// Token: 0x06002947 RID: 10567 RVA: 0x000D1870 File Offset: 0x000CFA70
	private void CancelChoices()
	{
		this.HideChoiceUI();
		foreach (ChoiceCardMgr.ChoiceState choiceState in this.m_choiceStateMap.Values)
		{
			for (int i = 0; i < choiceState.m_cards.Count; i++)
			{
				Card card = choiceState.m_cards[i];
				card.HideCard();
				card.DeactivateHandStateSpells(card.GetActor());
				this.DeactivateChoiceCardStateSpells(card);
			}
		}
		this.m_choiceStateMap.Clear();
	}

	// Token: 0x06002948 RID: 10568 RVA: 0x000D1910 File Offset: 0x000CFB10
	private IEnumerator WaitThenShowSubOptions()
	{
		while (this.IsWaitingToShowSubOptions())
		{
			yield return null;
			if (this.m_subOptionState == null)
			{
				yield break;
			}
		}
		this.ShowSubOptions();
		yield break;
	}

	// Token: 0x06002949 RID: 10569 RVA: 0x000D1920 File Offset: 0x000CFB20
	private void ShowSubOptions()
	{
		GameState gameState = GameState.Get();
		Card parentCard = this.m_subOptionState.m_parentCard;
		Entity entity = this.m_subOptionState.m_parentCard.GetEntity();
		string text = this.m_SubOptionData.m_BoneName;
		if (UniversalInputManager.UsePhoneUI)
		{
			text += "_phone";
		}
		Transform transform = Board.Get().FindBone(text);
		float num = this.m_CommonData.m_FriendlyCardWidth;
		float num2 = transform.position.x;
		ZonePlay battlefieldZone = entity.GetController().GetBattlefieldZone();
		List<int> subCardIDs = entity.GetSubCardIDs();
		if (entity.IsMinion() && !UniversalInputManager.UsePhoneUI && subCardIDs.Count <= 2)
		{
			int zonePosition = parentCard.GetZonePosition();
			num2 = battlefieldZone.GetCardPosition(parentCard).x;
			if (zonePosition > 5)
			{
				num += this.m_SubOptionData.m_AdjacentCardXOffset;
				num2 -= this.m_CommonData.m_FriendlyCardWidth * 1.5f + this.m_SubOptionData.m_AdjacentCardXOffset + this.m_SubOptionData.m_MinionParentXOffset;
			}
			else if (zonePosition == 1 && battlefieldZone.GetCards().Count > 6)
			{
				num += this.m_SubOptionData.m_AdjacentCardXOffset;
				num2 += this.m_CommonData.m_FriendlyCardWidth / 2f + this.m_SubOptionData.m_MinionParentXOffset;
			}
			else
			{
				num += this.m_SubOptionData.m_MinionParentXOffset * 2f;
				num2 -= this.m_CommonData.m_FriendlyCardWidth / 2f + this.m_SubOptionData.m_MinionParentXOffset;
			}
		}
		else
		{
			int count = subCardIDs.Count;
			num += ((count > this.m_CommonData.m_MaxCardsBeforeAdjusting) ? this.m_SubOptionData.m_PhoneMaxAdjacentCardXOffset : this.m_SubOptionData.m_AdjacentCardXOffset);
			num2 -= num / 2f * (float)(count - 1);
		}
		for (int i = 0; i < subCardIDs.Count; i++)
		{
			int id = subCardIDs[i];
			Card card = gameState.GetEntity(id).GetCard();
			if (!(card == null))
			{
				this.m_subOptionState.m_cards.Add(card);
				card.ForceLoadHandActor();
				card.transform.position = parentCard.transform.position;
				card.transform.localScale = ChoiceCardMgr.INVISIBLE_SCALE;
				Vector3 position = default(Vector3);
				position.x = num2 + (float)i * num;
				position.y = transform.position.y;
				position.z = transform.position.z;
				iTween.MoveTo(card.gameObject, position, this.m_SubOptionData.m_CardShowTime);
				Vector3 localScale = transform.localScale;
				if (subCardIDs.Count > this.m_CommonData.m_MaxCardsBeforeAdjusting)
				{
					float scaleForCardCount = this.GetScaleForCardCount(subCardIDs.Count);
					localScale.x *= scaleForCardCount;
					localScale.y *= scaleForCardCount;
					localScale.z *= scaleForCardCount;
				}
				iTween.ScaleTo(card.gameObject, localScale, this.m_SubOptionData.m_CardShowTime);
				card.ActivateHandStateSpells(false);
			}
		}
		this.HideEnlargedHand();
	}

	// Token: 0x0600294A RID: 10570 RVA: 0x000D1C54 File Offset: 0x000CFE54
	private void HideSubOptions(Entity chosenEntity = null)
	{
		for (int i = 0; i < this.m_subOptionState.m_cards.Count; i++)
		{
			Card card = this.m_subOptionState.m_cards[i];
			card.DeactivateHandStateSpells(null);
			this.DeactivateChoiceCardStateSpells(card);
			if (card.GetEntity() != chosenEntity)
			{
				card.HideCard();
			}
		}
		this.RestoreEnlargedHand();
	}

	// Token: 0x0600294B RID: 10571 RVA: 0x000D1CB1 File Offset: 0x000CFEB1
	private bool IsEntityReady(Entity entity)
	{
		return entity.GetZone() == TAG_ZONE.SETASIDE && !entity.IsBusy();
	}

	// Token: 0x0600294C RID: 10572 RVA: 0x000D1CC9 File Offset: 0x000CFEC9
	private bool IsCardReady(Card card)
	{
		return card.HasCardDef;
	}

	// Token: 0x0600294D RID: 10573 RVA: 0x000D1CD1 File Offset: 0x000CFED1
	private bool IsCardActorReady(Card card)
	{
		return card.IsActorReady();
	}

	// Token: 0x0600294E RID: 10574 RVA: 0x000D1CDC File Offset: 0x000CFEDC
	private Spell GetCustomChoiceRevealSpell(ChoiceCardMgr.ChoiceState choiceState)
	{
		Entity entity = GameState.Get().GetEntity(choiceState.m_sourceEntityId);
		if (entity == null)
		{
			return null;
		}
		Card card = entity.GetCard();
		if (card == null)
		{
			return null;
		}
		return card.GetCustomChoiceRevealSpell();
	}

	// Token: 0x0600294F RID: 10575 RVA: 0x000D1D18 File Offset: 0x000CFF18
	private Spell GetCustomChoiceConcealSpell(ChoiceCardMgr.ChoiceState choiceState)
	{
		Entity entity = GameState.Get().GetEntity(choiceState.m_sourceEntityId);
		if (entity == null)
		{
			return null;
		}
		Card card = entity.GetCard();
		if (card == null)
		{
			return null;
		}
		return card.GetCustomChoiceConcealSpell();
	}

	// Token: 0x06002950 RID: 10576 RVA: 0x000D1D54 File Offset: 0x000CFF54
	private void RevealChoiceCardsUsingCustomSpell(Spell customChoiceRevealSpell, ChoiceCardMgr.ChoiceState state)
	{
		CustomChoiceSpell customChoiceSpell = customChoiceRevealSpell as CustomChoiceSpell;
		if (customChoiceSpell != null)
		{
			customChoiceSpell.SetChoiceState(state);
		}
		customChoiceRevealSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnCustomChoiceRevealSpellFinished), state);
		customChoiceRevealSpell.Activate();
	}

	// Token: 0x06002951 RID: 10577 RVA: 0x000D1D94 File Offset: 0x000CFF94
	private void OnCustomChoiceRevealSpellFinished(Spell spell, object userData)
	{
		ChoiceCardMgr.ChoiceState choiceState = userData as ChoiceCardMgr.ChoiceState;
		if (choiceState == null)
		{
			Log.Power.PrintError("userData passed to ChoiceCardMgr.OnCustomChoiceRevealSpellFinished() is not of type ChoiceState.", Array.Empty<object>());
		}
		if (choiceState.m_isFriendly)
		{
			this.ShowChoiceUi(choiceState);
		}
		foreach (Card card in choiceState.m_cards)
		{
			card.ShowCard();
			this.ActivateChoiceCardStateSpells(card);
		}
		this.PlayChoiceEffects(choiceState, choiceState.m_isFriendly);
		choiceState.m_hasBeenRevealed = true;
	}

	// Token: 0x06002952 RID: 10578 RVA: 0x000D1E30 File Offset: 0x000D0030
	private void ConcealChoiceCardsUsingCustomSpell(Spell customChoiceConcealSpell, ChoiceCardMgr.ChoiceState choiceState, Network.EntitiesChosen chosen)
	{
		if (customChoiceConcealSpell.IsActive())
		{
			Log.Power.PrintError("ChoiceCardMgr.HideChoicesFromPacket(): CustomChoiceConcealSpell is already active!", Array.Empty<object>());
		}
		CustomChoiceSpell customChoiceSpell = customChoiceConcealSpell as CustomChoiceSpell;
		if (customChoiceSpell != null)
		{
			customChoiceSpell.SetChoiceState(choiceState);
		}
		this.DeactivateChoiceEffects(choiceState);
		choiceState.m_hasBeenConcealed = true;
		customChoiceConcealSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnCustomChoiceConcealSpellFinished), chosen);
		customChoiceConcealSpell.Activate();
	}

	// Token: 0x06002953 RID: 10579 RVA: 0x000D1E98 File Offset: 0x000D0098
	private void OnCustomChoiceConcealSpellFinished(Spell spell, object userData)
	{
		Network.EntitiesChosen entitiesChosen = userData as Network.EntitiesChosen;
		this.OnFinishedConcealChoices(entitiesChosen.PlayerId);
		GameState.Get().OnEntitiesChosenProcessed(entitiesChosen);
	}

	// Token: 0x0400175D RID: 5981
	public ChoiceCardMgr.CommonData m_CommonData = new ChoiceCardMgr.CommonData();

	// Token: 0x0400175E RID: 5982
	public ChoiceCardMgr.ChoiceData m_ChoiceData = new ChoiceCardMgr.ChoiceData();

	// Token: 0x0400175F RID: 5983
	public ChoiceCardMgr.SubOptionData m_SubOptionData = new ChoiceCardMgr.SubOptionData();

	// Token: 0x04001760 RID: 5984
	public List<ChoiceCardMgr.TagSpecificChoiceEffect> m_TagSpecificChoiceEffectData = new List<ChoiceCardMgr.TagSpecificChoiceEffect>();

	// Token: 0x04001761 RID: 5985
	public List<ChoiceCardMgr.CardSpecificChoiceEffect> m_CardSpecificChoiceEffectData = new List<ChoiceCardMgr.CardSpecificChoiceEffect>();

	// Token: 0x04001762 RID: 5986
	private ChoiceCardMgr.ChoiceEffectData m_DiscoverChoiceEffectData = new ChoiceCardMgr.ChoiceEffectData();

	// Token: 0x04001763 RID: 5987
	private ChoiceCardMgr.ChoiceEffectData m_AdaptChoiceEffectData = new ChoiceCardMgr.ChoiceEffectData();

	// Token: 0x04001764 RID: 5988
	private ChoiceCardMgr.ChoiceEffectData m_GearsChoiceEffectData = new ChoiceCardMgr.ChoiceEffectData();

	// Token: 0x04001765 RID: 5989
	private ChoiceCardMgr.ChoiceEffectData m_DragonChoiceEffectData = new ChoiceCardMgr.ChoiceEffectData();

	// Token: 0x04001766 RID: 5990
	private static readonly Vector3 INVISIBLE_SCALE = new Vector3(0.0001f, 0.0001f, 0.0001f);

	// Token: 0x04001767 RID: 5991
	private static ChoiceCardMgr s_instance;

	// Token: 0x04001768 RID: 5992
	private ChoiceCardMgr.SubOptionState m_subOptionState;

	// Token: 0x04001769 RID: 5993
	private ChoiceCardMgr.SubOptionState m_pendingCancelSubOptionState;

	// Token: 0x0400176A RID: 5994
	private Map<int, ChoiceCardMgr.ChoiceState> m_choiceStateMap = new Map<int, ChoiceCardMgr.ChoiceState>();

	// Token: 0x0400176B RID: 5995
	private Banner m_choiceBanner;

	// Token: 0x0400176C RID: 5996
	private NormalButton m_toggleChoiceButton;

	// Token: 0x0400176D RID: 5997
	private NormalButton m_confirmChoiceButton;

	// Token: 0x0400176E RID: 5998
	private bool m_friendlyChoicesShown;

	// Token: 0x0400176F RID: 5999
	private bool m_restoreEnlargedHand;

	// Token: 0x04001770 RID: 6000
	private ChoiceCardMgr.ChoiceState m_lastShownChoiceState;

	// Token: 0x02001630 RID: 5680
	[Serializable]
	public class CommonData
	{
		// Token: 0x0400B014 RID: 45076
		public float m_FriendlyCardWidth = 2.85f;

		// Token: 0x0400B015 RID: 45077
		public float m_OpponentCardWidth = 1.5f;

		// Token: 0x0400B016 RID: 45078
		public int m_MaxCardsBeforeAdjusting = 3;

		// Token: 0x0400B017 RID: 45079
		public PlatformDependentValue<float> m_FourCardScale = new PlatformDependentValue<float>(PlatformCategory.Screen)
		{
			PC = 1f,
			Tablet = 1f,
			Phone = 0.8f
		};

		// Token: 0x0400B018 RID: 45080
		public PlatformDependentValue<float> m_FiveCardScale = new PlatformDependentValue<float>(PlatformCategory.Screen)
		{
			PC = 0.85f,
			Tablet = 0.85f,
			Phone = 0.65f
		};

		// Token: 0x0400B019 RID: 45081
		public PlatformDependentValue<float> m_SixPlusCardScale = new PlatformDependentValue<float>(PlatformCategory.Screen)
		{
			PC = 0.7f,
			Tablet = 0.7f,
			Phone = 0.55f
		};
	}

	// Token: 0x02001631 RID: 5681
	[Serializable]
	public class ChoiceData
	{
		// Token: 0x0400B01A RID: 45082
		public string m_FriendlyBoneName = "FriendlyChoice";

		// Token: 0x0400B01B RID: 45083
		public string m_OpponentBoneName = "OpponentChoice";

		// Token: 0x0400B01C RID: 45084
		public string m_BannerBoneName = "ChoiceBanner";

		// Token: 0x0400B01D RID: 45085
		public string m_ToggleChoiceButtonBoneName = "ToggleChoiceButton";

		// Token: 0x0400B01E RID: 45086
		public string m_ConfirmChoiceButtonBoneName = "ConfirmChoiceButton";

		// Token: 0x0400B01F RID: 45087
		public float m_MinShowTime = 1f;

		// Token: 0x0400B020 RID: 45088
		public Banner m_BannerPrefab;

		// Token: 0x0400B021 RID: 45089
		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string m_ButtonPrefab;

		// Token: 0x0400B022 RID: 45090
		public GameObject m_xPrefab;

		// Token: 0x0400B023 RID: 45091
		public float m_CardShowTime = 0.2f;

		// Token: 0x0400B024 RID: 45092
		public float m_CardHideTime = 0.2f;

		// Token: 0x0400B025 RID: 45093
		public float m_UiShowTime = 0.5f;

		// Token: 0x0400B026 RID: 45094
		public float m_HorizontalPadding = 0.75f;

		// Token: 0x0400B027 RID: 45095
		public PlatformDependentValue<float> m_HorizontalPaddingFourCards = new PlatformDependentValue<float>(PlatformCategory.Screen)
		{
			PC = 0.6f,
			Tablet = 0.5f,
			Phone = 0.4f
		};

		// Token: 0x0400B028 RID: 45096
		public PlatformDependentValue<float> m_HorizontalPaddingFiveCards = new PlatformDependentValue<float>(PlatformCategory.Screen)
		{
			PC = 0.3f,
			Tablet = 0.3f,
			Phone = 0.3f
		};

		// Token: 0x0400B029 RID: 45097
		public PlatformDependentValue<float> m_HorizontalPaddingSixPlusCards = new PlatformDependentValue<float>(PlatformCategory.Screen)
		{
			PC = 0.2f,
			Tablet = 0.2f,
			Phone = 0.2f
		};
	}

	// Token: 0x02001632 RID: 5682
	[Serializable]
	public class SubOptionData
	{
		// Token: 0x0400B02A RID: 45098
		public string m_BoneName = "SubOption";

		// Token: 0x0400B02B RID: 45099
		public float m_AdjacentCardXOffset = 0.75f;

		// Token: 0x0400B02C RID: 45100
		public float m_PhoneMaxAdjacentCardXOffset = 0.1f;

		// Token: 0x0400B02D RID: 45101
		public float m_MinionParentXOffset = 0.9f;

		// Token: 0x0400B02E RID: 45102
		public float m_CardShowTime = 0.2f;
	}

	// Token: 0x02001633 RID: 5683
	[Serializable]
	public class ChoiceEffectData
	{
		// Token: 0x0400B02F RID: 45103
		public bool m_AlwaysPlayEffect;

		// Token: 0x0400B030 RID: 45104
		public bool m_PlayOncePerCard;

		// Token: 0x0400B031 RID: 45105
		public Spell m_Spell;
	}

	// Token: 0x02001634 RID: 5684
	[Serializable]
	public class TagSpecificChoiceEffect
	{
		// Token: 0x0400B032 RID: 45106
		public GAME_TAG m_Tag;

		// Token: 0x0400B033 RID: 45107
		public List<ChoiceCardMgr.TagValueSpecificChoiceEffect> m_ValueSpellMap;
	}

	// Token: 0x02001635 RID: 5685
	[Serializable]
	public class TagValueSpecificChoiceEffect
	{
		// Token: 0x0400B034 RID: 45108
		public int m_Value;

		// Token: 0x0400B035 RID: 45109
		public ChoiceCardMgr.ChoiceEffectData m_ChoiceEffectData;
	}

	// Token: 0x02001636 RID: 5686
	[Serializable]
	public class CardSpecificChoiceEffect
	{
		// Token: 0x0400B036 RID: 45110
		public string m_CardID;

		// Token: 0x0400B037 RID: 45111
		public ChoiceCardMgr.ChoiceEffectData m_ChoiceEffectData;
	}

	// Token: 0x02001637 RID: 5687
	private class SubOptionState
	{
		// Token: 0x0400B038 RID: 45112
		public List<Card> m_cards = new List<Card>();

		// Token: 0x0400B039 RID: 45113
		public Card m_parentCard;
	}

	// Token: 0x02001638 RID: 5688
	public struct TransformData
	{
		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x0600E328 RID: 58152 RVA: 0x004043DA File Offset: 0x004025DA
		// (set) Token: 0x0600E329 RID: 58153 RVA: 0x004043E2 File Offset: 0x004025E2
		public Vector3 Position { get; set; }

		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x0600E32A RID: 58154 RVA: 0x004043EB File Offset: 0x004025EB
		// (set) Token: 0x0600E32B RID: 58155 RVA: 0x004043F3 File Offset: 0x004025F3
		public Vector3 RotationAngles { get; set; }

		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x0600E32C RID: 58156 RVA: 0x004043FC File Offset: 0x004025FC
		// (set) Token: 0x0600E32D RID: 58157 RVA: 0x00404404 File Offset: 0x00402604
		public Vector3 LocalScale { get; set; }
	}

	// Token: 0x02001639 RID: 5689
	public class ChoiceState
	{
		// Token: 0x0400B03D RID: 45117
		public int m_choiceID;

		// Token: 0x0400B03E RID: 45118
		public bool m_isFriendly;

		// Token: 0x0400B03F RID: 45119
		public List<Card> m_cards = new List<Card>();

		// Token: 0x0400B040 RID: 45120
		public List<ChoiceCardMgr.TransformData> m_cardTransforms = new List<ChoiceCardMgr.TransformData>();

		// Token: 0x0400B041 RID: 45121
		public bool m_waitingToStart;

		// Token: 0x0400B042 RID: 45122
		public bool m_hasBeenRevealed;

		// Token: 0x0400B043 RID: 45123
		public bool m_hasBeenConcealed;

		// Token: 0x0400B044 RID: 45124
		public bool m_hideChosen;

		// Token: 0x0400B045 RID: 45125
		public int m_choiceActor;

		// Token: 0x0400B046 RID: 45126
		public PowerTaskList m_preTaskList;

		// Token: 0x0400B047 RID: 45127
		public int m_sourceEntityId;

		// Token: 0x0400B048 RID: 45128
		public List<Entity> m_chosenEntities;

		// Token: 0x0400B049 RID: 45129
		public Map<int, GameObject> m_xObjs;

		// Token: 0x0400B04A RID: 45130
		public List<Spell> m_choiceEffectSpells = new List<Spell>();

		// Token: 0x0400B04B RID: 45131
		public bool m_showFromDeck;
	}
}
