using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class ChoiceCardMgr : MonoBehaviour
{
	[Serializable]
	public class CommonData
	{
		public float m_FriendlyCardWidth = 2.85f;

		public float m_OpponentCardWidth = 1.5f;

		public int m_MaxCardsBeforeAdjusting = 3;

		public PlatformDependentValue<float> m_FourCardScale = new PlatformDependentValue<float>(PlatformCategory.Screen)
		{
			PC = 1f,
			Tablet = 1f,
			Phone = 0.8f
		};

		public PlatformDependentValue<float> m_FiveCardScale = new PlatformDependentValue<float>(PlatformCategory.Screen)
		{
			PC = 0.85f,
			Tablet = 0.85f,
			Phone = 0.65f
		};

		public PlatformDependentValue<float> m_SixPlusCardScale = new PlatformDependentValue<float>(PlatformCategory.Screen)
		{
			PC = 0.7f,
			Tablet = 0.7f,
			Phone = 0.55f
		};
	}

	[Serializable]
	public class ChoiceData
	{
		public string m_FriendlyBoneName = "FriendlyChoice";

		public string m_OpponentBoneName = "OpponentChoice";

		public string m_BannerBoneName = "ChoiceBanner";

		public string m_ToggleChoiceButtonBoneName = "ToggleChoiceButton";

		public string m_ConfirmChoiceButtonBoneName = "ConfirmChoiceButton";

		public float m_MinShowTime = 1f;

		public Banner m_BannerPrefab;

		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string m_ButtonPrefab;

		public GameObject m_xPrefab;

		public float m_CardShowTime = 0.2f;

		public float m_CardHideTime = 0.2f;

		public float m_UiShowTime = 0.5f;

		public float m_HorizontalPadding = 0.75f;

		public PlatformDependentValue<float> m_HorizontalPaddingFourCards = new PlatformDependentValue<float>(PlatformCategory.Screen)
		{
			PC = 0.6f,
			Tablet = 0.5f,
			Phone = 0.4f
		};

		public PlatformDependentValue<float> m_HorizontalPaddingFiveCards = new PlatformDependentValue<float>(PlatformCategory.Screen)
		{
			PC = 0.3f,
			Tablet = 0.3f,
			Phone = 0.3f
		};

		public PlatformDependentValue<float> m_HorizontalPaddingSixPlusCards = new PlatformDependentValue<float>(PlatformCategory.Screen)
		{
			PC = 0.2f,
			Tablet = 0.2f,
			Phone = 0.2f
		};
	}

	[Serializable]
	public class SubOptionData
	{
		public string m_BoneName = "SubOption";

		public float m_AdjacentCardXOffset = 0.75f;

		public float m_PhoneMaxAdjacentCardXOffset = 0.1f;

		public float m_MinionParentXOffset = 0.9f;

		public float m_CardShowTime = 0.2f;
	}

	[Serializable]
	public class ChoiceEffectData
	{
		public bool m_AlwaysPlayEffect;

		public bool m_PlayOncePerCard;

		public Spell m_Spell;
	}

	[Serializable]
	public class TagSpecificChoiceEffect
	{
		public GAME_TAG m_Tag;

		public List<TagValueSpecificChoiceEffect> m_ValueSpellMap;
	}

	[Serializable]
	public class TagValueSpecificChoiceEffect
	{
		public int m_Value;

		public ChoiceEffectData m_ChoiceEffectData;
	}

	[Serializable]
	public class CardSpecificChoiceEffect
	{
		public string m_CardID;

		public ChoiceEffectData m_ChoiceEffectData;
	}

	private class SubOptionState
	{
		public List<Card> m_cards = new List<Card>();

		public Card m_parentCard;
	}

	public struct TransformData
	{
		public Vector3 Position { get; set; }

		public Vector3 RotationAngles { get; set; }

		public Vector3 LocalScale { get; set; }
	}

	public class ChoiceState
	{
		public int m_choiceID;

		public bool m_isFriendly;

		public List<Card> m_cards = new List<Card>();

		public List<TransformData> m_cardTransforms = new List<TransformData>();

		public bool m_waitingToStart;

		public bool m_hasBeenRevealed;

		public bool m_hasBeenConcealed;

		public bool m_hideChosen;

		public int m_choiceActor;

		public PowerTaskList m_preTaskList;

		public int m_sourceEntityId;

		public List<Entity> m_chosenEntities;

		public Map<int, GameObject> m_xObjs;

		public List<Spell> m_choiceEffectSpells = new List<Spell>();

		public bool m_showFromDeck;
	}

	public CommonData m_CommonData = new CommonData();

	public ChoiceData m_ChoiceData = new ChoiceData();

	public SubOptionData m_SubOptionData = new SubOptionData();

	public List<TagSpecificChoiceEffect> m_TagSpecificChoiceEffectData = new List<TagSpecificChoiceEffect>();

	public List<CardSpecificChoiceEffect> m_CardSpecificChoiceEffectData = new List<CardSpecificChoiceEffect>();

	private ChoiceEffectData m_DiscoverChoiceEffectData = new ChoiceEffectData();

	private ChoiceEffectData m_AdaptChoiceEffectData = new ChoiceEffectData();

	private ChoiceEffectData m_GearsChoiceEffectData = new ChoiceEffectData();

	private ChoiceEffectData m_DragonChoiceEffectData = new ChoiceEffectData();

	private static readonly Vector3 INVISIBLE_SCALE = new Vector3(0.0001f, 0.0001f, 0.0001f);

	private static ChoiceCardMgr s_instance;

	private SubOptionState m_subOptionState;

	private SubOptionState m_pendingCancelSubOptionState;

	private Map<int, ChoiceState> m_choiceStateMap = new Map<int, ChoiceState>();

	private Banner m_choiceBanner;

	private NormalButton m_toggleChoiceButton;

	private NormalButton m_confirmChoiceButton;

	private bool m_friendlyChoicesShown;

	private bool m_restoreEnlargedHand;

	private ChoiceState m_lastShownChoiceState;

	private void Awake()
	{
		s_instance = this;
		foreach (TagSpecificChoiceEffect tagSpecificChoiceEffectDatum in m_TagSpecificChoiceEffectData)
		{
			switch (tagSpecificChoiceEffectDatum.m_Tag)
			{
			case GAME_TAG.USE_DISCOVER_VISUALS:
				if (tagSpecificChoiceEffectDatum.m_ValueSpellMap.Count > 0)
				{
					m_DiscoverChoiceEffectData = tagSpecificChoiceEffectDatum.m_ValueSpellMap[0].m_ChoiceEffectData;
				}
				break;
			case GAME_TAG.ADAPT:
				if (tagSpecificChoiceEffectDatum.m_ValueSpellMap.Count > 0)
				{
					m_AdaptChoiceEffectData = tagSpecificChoiceEffectDatum.m_ValueSpellMap[0].m_ChoiceEffectData;
				}
				break;
			case GAME_TAG.GEARS:
				if (tagSpecificChoiceEffectDatum.m_ValueSpellMap.Count > 0)
				{
					m_GearsChoiceEffectData = tagSpecificChoiceEffectDatum.m_ValueSpellMap[0].m_ChoiceEffectData;
				}
				break;
			case GAME_TAG.GOOD_OL_GENERIC_FRIENDLY_DRAGON_DISCOVER_VISUALS:
				if (tagSpecificChoiceEffectDatum.m_ValueSpellMap.Count > 0)
				{
					m_DragonChoiceEffectData = tagSpecificChoiceEffectDatum.m_ValueSpellMap[0].m_ChoiceEffectData;
				}
				break;
			}
		}
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	private void Start()
	{
		if (GameState.Get() == null)
		{
			Debug.LogError($"ChoiceCardMgr.Start() - GameState already Shutdown before ChoiceCardMgr was loaded.");
			return;
		}
		GameState.Get().RegisterEntityChoicesReceivedListener(OnEntityChoicesReceived);
		GameState.Get().RegisterEntitiesChosenReceivedListener(OnEntitiesChosenReceived);
		GameState.Get().RegisterGameOverListener(OnGameOver);
	}

	public static ChoiceCardMgr Get()
	{
		return s_instance;
	}

	public bool RestoreEnlargedHandAfterChoice()
	{
		return m_restoreEnlargedHand;
	}

	public List<Card> GetFriendlyCards()
	{
		if (m_subOptionState != null)
		{
			return m_subOptionState.m_cards;
		}
		int friendlyPlayerId = GameState.Get().GetFriendlyPlayerId();
		if (m_choiceStateMap.TryGetValue(friendlyPlayerId, out var value))
		{
			return value.m_cards;
		}
		return null;
	}

	public bool IsShown()
	{
		if (m_subOptionState != null)
		{
			return true;
		}
		if (m_choiceStateMap.Count > 0)
		{
			return true;
		}
		return false;
	}

	public bool IsFriendlyShown()
	{
		if (m_subOptionState != null)
		{
			return true;
		}
		int friendlyPlayerId = GameState.Get().GetFriendlyPlayerId();
		if (m_choiceStateMap.ContainsKey(friendlyPlayerId))
		{
			return true;
		}
		return false;
	}

	public bool HasSubOption()
	{
		return m_subOptionState != null;
	}

	public Card GetSubOptionParentCard()
	{
		if (m_subOptionState != null)
		{
			return m_subOptionState.m_parentCard;
		}
		return null;
	}

	public void ClearSubOptions()
	{
		m_subOptionState = null;
	}

	public void ShowSubOptions(Card parentCard)
	{
		m_subOptionState = new SubOptionState();
		m_subOptionState.m_parentCard = parentCard;
		StartCoroutine(WaitThenShowSubOptions());
	}

	public void QuenePendingCancelSubOptions()
	{
		m_pendingCancelSubOptionState = m_subOptionState;
	}

	public bool HasPendingCancelSubOptions()
	{
		if (m_pendingCancelSubOptionState != null)
		{
			return m_pendingCancelSubOptionState == m_subOptionState;
		}
		return false;
	}

	public void ClearPendingCancelSubOptions()
	{
		m_pendingCancelSubOptionState = null;
	}

	public bool IsWaitingToShowSubOptions()
	{
		if (!HasSubOption())
		{
			return false;
		}
		Entity entity = m_subOptionState.m_parentCard.GetEntity();
		Player controller = entity.GetController();
		Zone zone = m_subOptionState.m_parentCard.GetZone();
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
			if (m_subOptionState.m_parentCard.GetZonePosition() == 0)
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
			if (!m_subOptionState.m_parentCard.IsActorReady())
			{
				return true;
			}
		}
		if (!entity.HasSubCards())
		{
			return true;
		}
		return false;
	}

	public void CancelSubOptions()
	{
		if (!HasSubOption())
		{
			return;
		}
		Entity entity = m_subOptionState.m_parentCard.GetEntity();
		Card card = entity.GetCard();
		for (int i = 0; i < m_subOptionState.m_cards.Count; i++)
		{
			Spell subOptionSpell = card.GetSubOptionSpell(i, 0, loadIfNeeded: false);
			if ((bool)subOptionSpell)
			{
				SpellStateType activeState = subOptionSpell.GetActiveState();
				if (activeState != 0 && activeState != SpellStateType.CANCEL)
				{
					subOptionSpell.ActivateState(SpellStateType.CANCEL);
				}
			}
		}
		card.ActivateHandStateSpells();
		if (entity.IsHeroPowerOrGameModeButton())
		{
			entity.SetTagAndHandleChange(GAME_TAG.EXHAUSTED, 0);
		}
		HideSubOptions();
	}

	public void OnSubOptionClicked(Entity chosenEntity)
	{
		if (HasSubOption())
		{
			HideSubOptions(chosenEntity);
		}
	}

	public bool HasChoices()
	{
		return m_choiceStateMap.Count > 0;
	}

	public bool HasChoices(int playerId)
	{
		return m_choiceStateMap.ContainsKey(playerId);
	}

	public bool HasFriendlyChoices()
	{
		int friendlyPlayerId = GameState.Get().GetFriendlyPlayerId();
		return HasChoices(friendlyPlayerId);
	}

	public PowerTaskList GetPreChoiceTaskList(int playerId)
	{
		if (m_choiceStateMap.TryGetValue(playerId, out var value))
		{
			return value.m_preTaskList;
		}
		return null;
	}

	public PowerTaskList GetFriendlyPreChoiceTaskList()
	{
		int friendlyPlayerId = GameState.Get().GetFriendlyPlayerId();
		return GetPreChoiceTaskList(friendlyPlayerId);
	}

	public bool IsWaitingToStartChoices(int playerId)
	{
		if (m_choiceStateMap.TryGetValue(playerId, out var value))
		{
			return value.m_waitingToStart;
		}
		return false;
	}

	public bool IsFriendlyWaitingToStartChoices()
	{
		int friendlyPlayerId = GameState.Get().GetFriendlyPlayerId();
		return IsWaitingToStartChoices(friendlyPlayerId);
	}

	public void OnSendChoices(Network.EntityChoices choicePacket, List<Entity> chosenEntities)
	{
		if (choicePacket.ChoiceType == CHOICE_TYPE.GENERAL)
		{
			int friendlyPlayerId = GameState.Get().GetFriendlyPlayerId();
			if (!m_choiceStateMap.TryGetValue(friendlyPlayerId, out var value))
			{
				Error.AddDevFatal("ChoiceCardMgr.OnSendChoices() - there is no ChoiceState for friendly player {0}", friendlyPlayerId);
			}
			else
			{
				value.m_chosenEntities = new List<Entity>(chosenEntities);
				ConcealChoicesFromInput(friendlyPlayerId, value);
			}
		}
	}

	public void OnChosenEntityAdded(Entity entity)
	{
		if (entity == null)
		{
			Log.Gameplay.PrintError("ChoiceCardMgr.OnChosenEntityAdded(): null entity passed!");
			return;
		}
		Network.EntityChoices friendlyEntityChoices = GameState.Get().GetFriendlyEntityChoices();
		if (friendlyEntityChoices == null || friendlyEntityChoices.IsSingleChoice() || !m_choiceStateMap.ContainsKey(GameState.Get().GetFriendlyPlayerId()))
		{
			return;
		}
		ChoiceState choiceState = m_choiceStateMap[GameState.Get().GetFriendlyPlayerId()];
		if (choiceState.m_xObjs == null)
		{
			Log.Gameplay.PrintError("ChoiceCardMgr.OnChosenEntityAdded(): ChoiceState does not have an m_xObjs map!");
		}
		else if (!choiceState.m_xObjs.ContainsKey(entity.GetEntityId()))
		{
			Card card = entity.GetCard();
			if (card == null)
			{
				Log.Gameplay.PrintError("ChoiceCardMgr.OnChosenEntityAdded(): Entity does not have a card!");
				return;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate(m_ChoiceData.m_xPrefab);
			TransformUtil.AttachAndPreserveLocalTransform(gameObject.transform, card.transform);
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localPosition = Vector3.zero;
			choiceState.m_xObjs.Add(entity.GetEntityId(), gameObject);
		}
	}

	public void OnChosenEntityRemoved(Entity entity)
	{
		if (entity == null)
		{
			Log.Gameplay.PrintError("ChoiceCardMgr.OnChosenEntityRemoved(): null entity passed!");
			return;
		}
		Network.EntityChoices friendlyEntityChoices = GameState.Get().GetFriendlyEntityChoices();
		if (friendlyEntityChoices == null || friendlyEntityChoices.IsSingleChoice() || !m_choiceStateMap.ContainsKey(GameState.Get().GetFriendlyPlayerId()))
		{
			return;
		}
		ChoiceState choiceState = m_choiceStateMap[GameState.Get().GetFriendlyPlayerId()];
		if (choiceState.m_xObjs == null)
		{
			Log.Gameplay.PrintError("ChoiceCardMgr.OnChosenEntityRemoved(): ChoiceState does not have an m_xObjs map!");
			return;
		}
		int entityId = entity.GetEntityId();
		if (choiceState.m_xObjs.ContainsKey(entityId))
		{
			GameObject obj = choiceState.m_xObjs[entityId];
			choiceState.m_xObjs.Remove(entityId);
			UnityEngine.Object.Destroy(obj);
		}
	}

	private void OnEntityChoicesReceived(Network.EntityChoices choices, PowerTaskList preChoiceTaskList, object userData)
	{
		if (choices.ChoiceType == CHOICE_TYPE.GENERAL)
		{
			StartCoroutine(WaitThenStartChoices(choices, preChoiceTaskList));
		}
	}

	private bool OnEntitiesChosenReceived(Network.EntitiesChosen chosen, object userData)
	{
		if (chosen.ChoiceType != CHOICE_TYPE.GENERAL)
		{
			return false;
		}
		StartCoroutine(WaitThenConcealChoicesFromPacket(chosen));
		return true;
	}

	private void OnGameOver(TAG_PLAYSTATE playState, object userData)
	{
		StopAllCoroutines();
		CancelSubOptions();
		CancelChoices();
	}

	private IEnumerator WaitThenStartChoices(Network.EntityChoices choices, PowerTaskList preChoiceTaskList)
	{
		int playerId = choices.PlayerId;
		ChoiceState state = new ChoiceState();
		m_choiceStateMap.Add(playerId, state);
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
			Log.Power.Print("ChoiceCardMgr.WaitThenShowChoices() - id={0} WAIT for taskList {1}", choices.ID, preChoiceTaskList.GetId());
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
		Log.Power.Print("ChoiceCardMgr.WaitThenShowChoices() - id={0} BEGIN", choices.ID);
		List<Card> linkedChoiceCards = new List<Card>();
		Entity entity2 = GameState.Get().GetEntity(state.m_sourceEntityId);
		for (int k = 0; k < choices.Entities.Count; k++)
		{
			int id = choices.Entities[k];
			Entity entity3 = GameState.Get().GetEntity(id);
			Card card = entity3.GetCard();
			if (card == null)
			{
				Error.AddDevFatal("ChoiceCardMgr.WaitThenShowChoices() - Entity {0} (option {1}) has no Card", entity3, k);
				continue;
			}
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
			StartCoroutine(LoadChoiceCardActors(entity2, entity3, card));
		}
		int j = 0;
		while (j < linkedChoiceCards.Count)
		{
			Card linkedCard = linkedChoiceCards[j];
			while (linkedCard != null && !linkedCard.IsActorReady())
			{
				yield return null;
			}
			int num = j + 1;
			j = num;
		}
		j = 0;
		while (j < state.m_cards.Count)
		{
			Card linkedCard = state.m_cards[j];
			while (!IsChoiceCardReady(linkedCard))
			{
				yield return null;
			}
			int num = j + 1;
			j = num;
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
		PopulateTransformDatas(state);
		StartChoices(state);
	}

	private IEnumerator LoadChoiceCardActors(Entity source, Entity entity, Card card)
	{
		while (!IsEntityReady(entity))
		{
			yield return null;
		}
		card.HideCard();
		while (!IsCardReady(card))
		{
			yield return null;
		}
		CHOICE_ACTOR cHOICE_ACTOR = CHOICE_ACTOR.CARD;
		if (source.HasTag(GAME_TAG.CHOICE_ACTOR_TYPE))
		{
			cHOICE_ACTOR = (CHOICE_ACTOR)source.GetTag(GAME_TAG.CHOICE_ACTOR_TYPE);
		}
		if ((uint)cHOICE_ACTOR > 1u && cHOICE_ACTOR == CHOICE_ACTOR.HERO)
		{
			LoadHeroChoiceCardActor(source, entity, card);
			card.ActivateHandStateSpells();
		}
		else
		{
			card.ForceLoadHandActor();
			card.ActivateHandStateSpells();
		}
	}

	private void LoadHeroChoiceCardActor(Entity source, Entity entity, Card card)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("Choose_Hero.prefab:1834beb8747ef06439f3a1b86a35ff3d", AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Log.Gameplay.PrintWarning(string.Format("ChoiceCardManager.LoadHeroChoiceActor() - FAILED to load actor \"{0}\"", "Choose_Hero.prefab:1834beb8747ef06439f3a1b86a35ff3d"));
			return;
		}
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Log.Gameplay.PrintWarning(string.Format("ChoiceCardManager.LoadHeroChoiceActor() - ERROR actor \"{0}\" has no Actor component", "Choose_Hero.prefab:1834beb8747ef06439f3a1b86a35ff3d"));
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
		SceneUtils.SetLayer(component.gameObject, base.gameObject.layer);
		component.GetMeshRenderer().gameObject.layer = 8;
		ConfigureHeroChoiceActor(source, entity, component as HeroChoiceActor);
	}

	private void ConfigureHeroChoiceActor(Entity source, Entity entity, HeroChoiceActor actor)
	{
		if (actor == null)
		{
			return;
		}
		if (entity == null || source == null)
		{
			actor.SetNameTextActive(active: false);
			return;
		}
		CHOICE_NAME_DISPLAY cHOICE_NAME_DISPLAY = CHOICE_NAME_DISPLAY.INVALID;
		if (source.HasTag(GAME_TAG.CHOICE_NAME_DISPLAY_TYPE))
		{
			cHOICE_NAME_DISPLAY = (CHOICE_NAME_DISPLAY)source.GetTag(GAME_TAG.CHOICE_NAME_DISPLAY_TYPE);
		}
		switch (cHOICE_NAME_DISPLAY)
		{
		case CHOICE_NAME_DISPLAY.HERO:
			actor.SetNameText(entity.GetName());
			actor.SetNameTextActive(active: true);
			break;
		case CHOICE_NAME_DISPLAY.PLAYER:
		{
			int num = entity.GetTag(GAME_TAG.PLAYER_ID);
			if (num == 0)
			{
				num = entity.GetTag(GAME_TAG.PLAYER_ID_LOOKUP);
			}
			actor.SetNameText(GameState.Get().GetGameEntity().GetBestNameForPlayer(num));
			actor.SetNameTextActive(active: true);
			break;
		}
		default:
			actor.SetNameTextActive(active: false);
			break;
		}
	}

	private bool IsChoiceCardReady(Card card)
	{
		Entity entity = card.GetEntity();
		if (!IsEntityReady(entity))
		{
			return false;
		}
		if (!IsCardReady(card))
		{
			return false;
		}
		if (!IsCardActorReady(card))
		{
			return false;
		}
		return true;
	}

	private void PopulateTransformDatas(ChoiceState state)
	{
		bool isFriendly = state.m_isFriendly;
		state.m_cardTransforms.Clear();
		int count = state.m_cards.Count;
		float num = m_ChoiceData.m_HorizontalPadding;
		if (isFriendly && count > m_CommonData.m_MaxCardsBeforeAdjusting)
		{
			num = GetPaddingForCardCount(count);
		}
		float num2 = (isFriendly ? m_CommonData.m_FriendlyCardWidth : m_CommonData.m_OpponentCardWidth);
		float num3 = 1f;
		if (isFriendly && count > m_CommonData.m_MaxCardsBeforeAdjusting)
		{
			num3 = GetScaleForCardCount(count);
			num2 *= num3;
		}
		float num4 = 0.5f * num2;
		float num5 = num2 * (float)count + num * (float)(count - 1);
		float num6 = 0.5f * num5;
		string text = (isFriendly ? m_ChoiceData.m_FriendlyBoneName : m_ChoiceData.m_OpponentBoneName);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			text += "_phone";
		}
		Transform obj = Board.Get().FindBone(text);
		Vector3 position = obj.position;
		Vector3 eulerAngles = obj.rotation.eulerAngles;
		Vector3 localScale = obj.localScale;
		float num7 = position.x - num6 + num4;
		for (int i = 0; i < count; i++)
		{
			TransformData item = default(TransformData);
			Vector3 position2 = default(Vector3);
			position2.x = num7;
			position2.y = position.y;
			position2.z = position.z;
			item.Position = position2;
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

	private float GetScaleForCardCount(int cardCount)
	{
		if (cardCount <= m_CommonData.m_MaxCardsBeforeAdjusting)
		{
			return 1f;
		}
		return cardCount switch
		{
			4 => m_CommonData.m_FourCardScale, 
			5 => m_CommonData.m_FiveCardScale, 
			_ => m_CommonData.m_SixPlusCardScale, 
		};
	}

	private float GetPaddingForCardCount(int cardCount)
	{
		if (cardCount <= m_CommonData.m_MaxCardsBeforeAdjusting)
		{
			return m_ChoiceData.m_HorizontalPadding;
		}
		return cardCount switch
		{
			4 => m_ChoiceData.m_HorizontalPaddingFourCards, 
			5 => m_ChoiceData.m_HorizontalPaddingFiveCards, 
			_ => m_ChoiceData.m_HorizontalPaddingSixPlusCards, 
		};
	}

	private void StartChoices(ChoiceState state)
	{
		m_lastShownChoiceState = state;
		int count = state.m_cards.Count;
		for (int i = 0; i < count; i++)
		{
			Card card = state.m_cards[i];
			TransformData transformData = state.m_cardTransforms[i];
			card.transform.position = transformData.Position;
			card.transform.rotation = Quaternion.Euler(transformData.RotationAngles);
			card.transform.localScale = transformData.LocalScale;
		}
		RevealChoiceCards(state);
	}

	private void RevealChoiceCards(ChoiceState state)
	{
		Spell customChoiceRevealSpell = GetCustomChoiceRevealSpell(state);
		if (customChoiceRevealSpell != null)
		{
			RevealChoiceCardsUsingCustomSpell(customChoiceRevealSpell, state);
		}
		else
		{
			DefaultRevealChoiceCards(state);
		}
	}

	private void DefaultRevealChoiceCards(ChoiceState choiceState)
	{
		bool isFriendly = choiceState.m_isFriendly;
		if (isFriendly)
		{
			ShowChoiceUi(choiceState);
		}
		ShowChoiceCards(choiceState, isFriendly);
		choiceState.m_hasBeenRevealed = true;
	}

	private void ShowChoiceCards(ChoiceState state, bool friendly)
	{
		StartCoroutine(PlayCardAnimation(state, friendly));
	}

	private void GetDeckTransform(ZoneDeck deckZone, out Vector3 startPos, out Vector3 startRot, out Vector3 startScale)
	{
		Actor thicknessForLayout = deckZone.GetThicknessForLayout();
		startPos = thicknessForLayout.GetMeshRenderer().bounds.center + Card.IN_DECK_OFFSET;
		startRot = Card.IN_DECK_ANGLES;
		startScale = Card.IN_DECK_SCALE;
	}

	private IEnumerator PlayCardAnimation(ChoiceState state, bool friendly)
	{
		if (state.m_showFromDeck)
		{
			state.m_showFromDeck = false;
			ZoneDeck deckZone = GameState.Get().GetEntity(state.m_sourceEntityId).GetController()
				.GetDeckZone();
			GetDeckTransform(deckZone, out var deckPos, out var deckRot, out var deckScale);
			float timingBonus = 0.1f;
			int cardCount = state.m_cards.Count;
			int i = 0;
			while (i < cardCount)
			{
				Card card = state.m_cards[i];
				card.ShowCard();
				GameObject cardObject = card.gameObject;
				cardObject.transform.position = deckPos;
				cardObject.transform.rotation = Quaternion.Euler(deckRot);
				cardObject.transform.localScale = deckScale;
				TransformData transformData = state.m_cardTransforms[i];
				iTween.Stop(cardObject);
				Vector3[] array = new Vector3[3]
				{
					cardObject.transform.position,
					new Vector3(cardObject.transform.position.x, cardObject.transform.position.y + 3.6f, cardObject.transform.position.z),
					transformData.Position
				};
				iTween.MoveTo(cardObject, iTween.Hash("path", array, "time", MulliganManager.ANIMATION_TIME_DEAL_CARD, "easetype", iTween.EaseType.easeInSineOutExpo));
				iTween.ScaleTo(cardObject, MulliganManager.FRIENDLY_PLAYER_CARD_SCALE, MulliganManager.ANIMATION_TIME_DEAL_CARD);
				iTween.RotateTo(cardObject, iTween.Hash("rotation", new Vector3(0f, 0f, 0f), "time", MulliganManager.ANIMATION_TIME_DEAL_CARD, "delay", MulliganManager.ANIMATION_TIME_DEAL_CARD / 16f));
				yield return new WaitForSeconds(0.04f);
				SoundManager.Get().LoadAndPlay("FX_GameStart09_CardsOntoTable.prefab:da502e035813b5742a04d2ef4f588255", cardObject);
				yield return new WaitForSeconds(0.05f + timingBonus);
				timingBonus = 0f;
				int num = i + 1;
				i = num;
			}
		}
		else
		{
			int count = state.m_cards.Count;
			for (int j = 0; j < count; j++)
			{
				Card card2 = state.m_cards[j];
				TransformData transformData2 = state.m_cardTransforms[j];
				card2.ShowCard();
				card2.transform.localScale = INVISIBLE_SCALE;
				iTween.Stop(card2.gameObject);
				iTween.RotateTo(card2.gameObject, transformData2.RotationAngles, m_ChoiceData.m_CardShowTime);
				iTween.ScaleTo(card2.gameObject, transformData2.LocalScale, m_ChoiceData.m_CardShowTime);
				iTween.MoveTo(card2.gameObject, transformData2.Position, m_ChoiceData.m_CardShowTime);
				ActivateChoiceCardStateSpells(card2);
			}
		}
		PlayChoiceEffects(state, friendly);
	}

	private void PlayChoiceEffects(ChoiceState state, bool friendly)
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
		ChoiceEffectData choiceEffectDataForCard = GetChoiceEffectDataForCard(entity.GetCard());
		if (choiceEffectDataForCard == null || choiceEffectDataForCard.m_Spell == null || (state.m_hasBeenRevealed && !choiceEffectDataForCard.m_AlwaysPlayEffect))
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
			foreach (Card card in state.m_cards)
			{
				Spell spell2 = UnityEngine.Object.Instantiate(choiceEffectDataForCard.m_Spell);
				TransformUtil.AttachAndPreserveLocalTransform(spell2.transform, card.GetActor().transform);
				spell2.AddStateFinishedCallback(callback);
				spell2.Activate();
				state.m_choiceEffectSpells.Add(spell2);
			}
		}
		else
		{
			Spell spell3 = UnityEngine.Object.Instantiate(choiceEffectDataForCard.m_Spell);
			spell3.AddStateFinishedCallback(callback);
			spell3.Activate();
			state.m_choiceEffectSpells.Add(spell3);
		}
	}

	private void ActivateChoiceCardStateSpells(Card card)
	{
		if (!(card.GetActor() != null))
		{
			return;
		}
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
			}
		}
		else
		{
			card.GetActor().DestroySpell(SpellType.TECH_LEVEL_MANA_GEM);
		}
	}

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

	private void DeactivateChoiceEffects(ChoiceState state)
	{
		foreach (Spell choiceEffectSpell in state.m_choiceEffectSpells)
		{
			if (!(choiceEffectSpell == null) && choiceEffectSpell.HasUsableState(SpellStateType.DEATH))
			{
				choiceEffectSpell.ActivateState(SpellStateType.DEATH);
			}
		}
		state.m_choiceEffectSpells.Clear();
	}

	private ChoiceEffectData GetChoiceEffectDataForCard(Card sourceCard)
	{
		if (sourceCard == null)
		{
			return null;
		}
		foreach (CardSpecificChoiceEffect cardSpecificChoiceEffectDatum in m_CardSpecificChoiceEffectData)
		{
			if (cardSpecificChoiceEffectDatum.m_CardID == sourceCard.GetEntity().GetCardId())
			{
				return cardSpecificChoiceEffectDatum.m_ChoiceEffectData;
			}
		}
		foreach (TagSpecificChoiceEffect tagSpecificChoiceEffectDatum in m_TagSpecificChoiceEffectData)
		{
			if (!sourceCard.GetEntity().HasTag(tagSpecificChoiceEffectDatum.m_Tag))
			{
				continue;
			}
			foreach (TagValueSpecificChoiceEffect item in tagSpecificChoiceEffectDatum.m_ValueSpellMap)
			{
				if (item.m_Value == sourceCard.GetEntity().GetTag(tagSpecificChoiceEffectDatum.m_Tag))
				{
					return item.m_ChoiceEffectData;
				}
			}
		}
		if (sourceCard.GetEntity().HasTag(GAME_TAG.USE_DISCOVER_VISUALS))
		{
			return m_DiscoverChoiceEffectData;
		}
		if (sourceCard.GetEntity().HasReferencedTag(GAME_TAG.ADAPT))
		{
			return m_AdaptChoiceEffectData;
		}
		if (sourceCard.GetEntity().HasTag(GAME_TAG.GEARS))
		{
			return m_GearsChoiceEffectData;
		}
		if (sourceCard.GetEntity().HasTag(GAME_TAG.GOOD_OL_GENERIC_FRIENDLY_DRAGON_DISCOVER_VISUALS))
		{
			return m_DragonChoiceEffectData;
		}
		return null;
	}

	private IEnumerator WaitThenConcealChoicesFromPacket(Network.EntitiesChosen chosen)
	{
		int playerId = chosen.PlayerId;
		if (m_choiceStateMap.TryGetValue(playerId, out var choiceState))
		{
			if (choiceState.m_waitingToStart || !choiceState.m_hasBeenRevealed)
			{
				Log.Power.Print("ChoiceCardMgr.WaitThenHideChoicesFromPacket() - id={0} BEGIN WAIT for EntityChoice", chosen.ID);
				while (choiceState.m_waitingToStart)
				{
					yield return null;
				}
				while (!choiceState.m_hasBeenRevealed)
				{
					yield return null;
				}
				yield return new WaitForSeconds(m_ChoiceData.m_MinShowTime);
			}
		}
		else if (m_lastShownChoiceState.m_choiceID == chosen.ID)
		{
			choiceState = m_lastShownChoiceState;
		}
		if (choiceState == null)
		{
			Log.Power.Print("ChoiceCardMgr.WaitThenHideChoicesFromPacket(): Unable to find ChoiceState corresponding to EntitiesChosen packet with ID %d.", chosen.ID);
			Log.Power.Print("ChoiceCardMgr.WaitThenHideChoicesFromPacket() - id={0} END WAIT", chosen.ID);
			GameState.Get().OnEntitiesChosenProcessed(chosen);
		}
		else
		{
			ResolveConflictBetweenLocalChoiceAndServerPacket(choiceState, chosen);
			Log.Power.Print("ChoiceCardMgr.WaitThenHideChoicesFromPacket() - id={0} END WAIT", chosen.ID);
			ConcealChoicesFromPacket(playerId, choiceState, chosen);
		}
	}

	private void ResolveConflictBetweenLocalChoiceAndServerPacket(ChoiceState choiceState, Network.EntitiesChosen chosen)
	{
		if (DoesLocalChoiceMatchPacket(choiceState.m_chosenEntities, chosen.Entities))
		{
			return;
		}
		choiceState.m_chosenEntities = new List<Entity>();
		foreach (int entity2 in chosen.Entities)
		{
			Entity entity = GameState.Get().GetEntity(entity2);
			if (entity != null)
			{
				choiceState.m_chosenEntities.Add(entity);
			}
		}
		if (!choiceState.m_hasBeenConcealed)
		{
			return;
		}
		foreach (Card card in choiceState.m_cards)
		{
			card.ShowCard();
		}
		choiceState.m_hasBeenConcealed = false;
	}

	private bool DoesLocalChoiceMatchPacket(List<Entity> localChoices, List<int> packetChoices)
	{
		if (localChoices == null || packetChoices == null)
		{
			Log.Power.Print($"ChoiceCardMgr.DoesLocalChoiceMatchPacket(): Null list passed in! localChoices={localChoices}, packetChoices={packetChoices}.");
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

	private void ConcealChoicesFromPacket(int playerId, ChoiceState choiceState, Network.EntitiesChosen chosen)
	{
		if (choiceState.m_isFriendly)
		{
			HideChoiceUI();
		}
		Spell customChoiceConcealSpell = GetCustomChoiceConcealSpell(choiceState);
		if (customChoiceConcealSpell != null)
		{
			ConcealChoiceCardsUsingCustomSpell(customChoiceConcealSpell, choiceState, chosen);
		}
		else
		{
			DefaultConcealChoicesFromPacket(playerId, choiceState, chosen);
		}
	}

	private void DefaultConcealChoicesFromPacket(int playerId, ChoiceState choiceState, Network.EntitiesChosen chosen)
	{
		if (!choiceState.m_hasBeenConcealed)
		{
			List<Card> cards = choiceState.m_cards;
			bool hideChosen = choiceState.m_hideChosen;
			for (int i = 0; i < cards.Count; i++)
			{
				Card card = cards[i];
				if (hideChosen || !WasCardChosen(card, chosen.Entities))
				{
					card.DeactivateHandStateSpells(card.GetActor());
					DeactivateChoiceCardStateSpells(card);
					card.HideCard();
				}
			}
			DeactivateChoiceEffects(choiceState);
			choiceState.m_hasBeenConcealed = true;
		}
		OnFinishedConcealChoices(playerId);
		GameState.Get().OnEntitiesChosenProcessed(chosen);
	}

	private bool WasCardChosen(Card card, List<int> chosenEntityIds)
	{
		Entity entity = card.GetEntity();
		int entityId = entity.GetEntityId();
		return chosenEntityIds.FindIndex((int currEntityId) => entityId == currEntityId) >= 0;
	}

	private void ConcealChoicesFromInput(int playerId, ChoiceState choiceState)
	{
		if (choiceState.m_isFriendly)
		{
			HideChoiceUI();
		}
		if (!(GetCustomChoiceConcealSpell(choiceState) == null))
		{
			return;
		}
		for (int i = 0; i < choiceState.m_cards.Count; i++)
		{
			Card card = choiceState.m_cards[i];
			Entity entity = card.GetEntity();
			if (choiceState.m_hideChosen || !choiceState.m_chosenEntities.Contains(entity))
			{
				card.HideCard();
				card.DeactivateHandStateSpells(card.GetActor());
				DeactivateChoiceCardStateSpells(card);
			}
		}
		DeactivateChoiceEffects(choiceState);
		choiceState.m_hasBeenConcealed = true;
		OnFinishedConcealChoices(playerId);
	}

	private void OnFinishedConcealChoices(int playerId)
	{
		if (!m_choiceStateMap.ContainsKey(playerId))
		{
			return;
		}
		foreach (GameObject value in m_choiceStateMap[playerId].m_xObjs.Values)
		{
			UnityEngine.Object.Destroy(value);
		}
		m_choiceStateMap.Remove(playerId);
	}

	private void HideChoiceCards(ChoiceState state)
	{
		for (int i = 0; i < state.m_cards.Count; i++)
		{
			Card card = state.m_cards[i];
			HideChoiceCard(card);
		}
		DeactivateChoiceEffects(state);
	}

	private void HideChoiceCard(Card card)
	{
		Action<object> action = delegate(object userData)
		{
			((Card)userData).HideCard();
		};
		iTween.Stop(card.gameObject);
		Hashtable args = iTween.Hash("scale", INVISIBLE_SCALE, "time", m_ChoiceData.m_CardHideTime, "oncomplete", action, "oncompleteparams", card, "oncompletetarget", base.gameObject);
		iTween.ScaleTo(card.gameObject, args);
	}

	private void ShowChoiceUi(ChoiceState choiceState)
	{
		ShowChoiceBanner(choiceState.m_cards);
		ShowChoiceButtons();
		HideEnlargedHand();
	}

	private void HideChoiceUI()
	{
		HideChoiceBanner();
		HideChoiceButtons();
		RestoreEnlargedHand();
	}

	private void ShowChoiceBanner(List<Card> cards)
	{
		HideChoiceBanner();
		Network.EntityChoices friendlyEntityChoices = GameState.Get().GetFriendlyEntityChoices();
		Transform transform = Board.Get().FindBone(m_ChoiceData.m_BannerBoneName);
		m_choiceBanner = UnityEngine.Object.Instantiate(m_ChoiceData.m_BannerPrefab, transform.position, transform.rotation);
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
				if (text == null)
				{
					text = GameStrings.Get("GAMEPLAY_CHOOSE_ONE");
					foreach (Card card in cards)
					{
						if (null != card && card.GetEntity().IsHeroPower())
						{
							text = GameStrings.Get("GAMEPLAY_CHOOSE_ONE_HERO_POWER");
							break;
						}
					}
				}
			}
			else
			{
				text = $"[PH] Choose {friendlyEntityChoices.CountMin} to {friendlyEntityChoices.CountMax}";
			}
		}
		m_choiceBanner.SetText(text);
		Vector3 localScale = m_choiceBanner.transform.localScale;
		m_choiceBanner.transform.localScale = INVISIBLE_SCALE;
		Hashtable args = iTween.Hash("scale", localScale, "time", m_ChoiceData.m_UiShowTime);
		iTween.ScaleTo(m_choiceBanner.gameObject, args);
	}

	private void HideChoiceBanner()
	{
		if ((bool)m_choiceBanner)
		{
			UnityEngine.Object.Destroy(m_choiceBanner.gameObject);
		}
	}

	private void ShowChoiceButtons()
	{
		Network.EntityChoices friendlyEntityChoices = GameState.Get().GetFriendlyEntityChoices();
		if (friendlyEntityChoices == null)
		{
			return;
		}
		HideChoiceButtons();
		string text = m_ChoiceData.m_ToggleChoiceButtonBoneName;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			text += "_phone";
		}
		m_toggleChoiceButton = CreateChoiceButton(text, ChoiceButton_OnPress, ToggleChoiceButton_OnRelease, GameStrings.Get("GLOBAL_HIDE"));
		if (!friendlyEntityChoices.IsSingleChoice())
		{
			text = m_ChoiceData.m_ConfirmChoiceButtonBoneName;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				text += "_phone";
			}
			m_confirmChoiceButton = CreateChoiceButton(text, ChoiceButton_OnPress, ConfirmChoiceButton_OnRelease, GameStrings.Get("GLOBAL_CONFIRM"));
		}
	}

	private NormalButton CreateChoiceButton(string boneName, UIEvent.Handler OnPressHandler, UIEvent.Handler OnReleaseHandler, string buttonText)
	{
		NormalButton component = AssetLoader.Get().InstantiatePrefab(m_ChoiceData.m_ButtonPrefab, AssetLoadingOptions.IgnorePrefabPosition).GetComponent<NormalButton>();
		component.GetButtonUberText().TextAlpha = 1f;
		Transform source = Board.Get().FindBone(boneName);
		TransformUtil.CopyWorld(component, source);
		m_friendlyChoicesShown = true;
		component.AddEventListener(UIEventType.PRESS, OnPressHandler);
		component.AddEventListener(UIEventType.RELEASE, OnReleaseHandler);
		component.SetText(buttonText);
		component.m_button.GetComponent<Spell>().ActivateState(SpellStateType.BIRTH);
		return component;
	}

	private void HideChoiceButtons()
	{
		if (m_toggleChoiceButton != null)
		{
			UnityEngine.Object.Destroy(m_toggleChoiceButton.gameObject);
			m_toggleChoiceButton = null;
		}
		if (m_confirmChoiceButton != null)
		{
			UnityEngine.Object.Destroy(m_confirmChoiceButton.gameObject);
			m_confirmChoiceButton = null;
		}
	}

	private void HideEnlargedHand()
	{
		ZoneHand handZone = GameState.Get().GetFriendlySidePlayer().GetHandZone();
		if (handZone.HandEnlarged())
		{
			m_restoreEnlargedHand = true;
			handZone.SetHandEnlarged(enlarged: false);
		}
	}

	private void RestoreEnlargedHand()
	{
		if (!m_restoreEnlargedHand)
		{
			return;
		}
		m_restoreEnlargedHand = false;
		if (!GameState.Get().IsInTargetMode())
		{
			ZoneHand handZone = GameState.Get().GetFriendlySidePlayer().GetHandZone();
			if (!handZone.HandEnlarged())
			{
				handZone.SetHandEnlarged(enlarged: true);
			}
		}
	}

	private void ChoiceButton_OnPress(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("UI_MouseClick_01.prefab:fa537702a0db1c3478c989967458788b");
	}

	private void ToggleChoiceButton_OnRelease(UIEvent e)
	{
		int friendlyPlayerId = GameState.Get().GetFriendlyPlayerId();
		ChoiceState state = m_choiceStateMap[friendlyPlayerId];
		if (m_friendlyChoicesShown)
		{
			m_toggleChoiceButton.SetText(GameStrings.Get("GLOBAL_SHOW"));
			HideChoiceCards(state);
			m_friendlyChoicesShown = false;
		}
		else
		{
			m_toggleChoiceButton.SetText(GameStrings.Get("GLOBAL_HIDE"));
			ShowChoiceCards(state, friendly: true);
			m_friendlyChoicesShown = true;
		}
		ToggleChoiceBannerVisibility(m_friendlyChoicesShown);
	}

	private void ToggleChoiceBannerVisibility(bool visible)
	{
		m_choiceBanner.gameObject.SetActive(visible);
	}

	private void ConfirmChoiceButton_OnRelease(UIEvent e)
	{
		GameState.Get().SendChoices();
	}

	private void CancelChoices()
	{
		HideChoiceUI();
		foreach (ChoiceState value in m_choiceStateMap.Values)
		{
			for (int i = 0; i < value.m_cards.Count; i++)
			{
				Card card = value.m_cards[i];
				card.HideCard();
				card.DeactivateHandStateSpells(card.GetActor());
				DeactivateChoiceCardStateSpells(card);
			}
		}
		m_choiceStateMap.Clear();
	}

	private IEnumerator WaitThenShowSubOptions()
	{
		while (IsWaitingToShowSubOptions())
		{
			yield return null;
			if (m_subOptionState == null)
			{
				yield break;
			}
		}
		ShowSubOptions();
	}

	private void ShowSubOptions()
	{
		GameState gameState = GameState.Get();
		Card parentCard = m_subOptionState.m_parentCard;
		Entity entity = m_subOptionState.m_parentCard.GetEntity();
		string text = m_SubOptionData.m_BoneName;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			text += "_phone";
		}
		Transform transform = Board.Get().FindBone(text);
		float friendlyCardWidth = m_CommonData.m_FriendlyCardWidth;
		float x = transform.position.x;
		ZonePlay battlefieldZone = entity.GetController().GetBattlefieldZone();
		List<int> subCardIDs = entity.GetSubCardIDs();
		if (entity.IsMinion() && !UniversalInputManager.UsePhoneUI && subCardIDs.Count <= 2)
		{
			int zonePosition = parentCard.GetZonePosition();
			x = battlefieldZone.GetCardPosition(parentCard).x;
			if (zonePosition > 5)
			{
				friendlyCardWidth += m_SubOptionData.m_AdjacentCardXOffset;
				x -= m_CommonData.m_FriendlyCardWidth * 1.5f + m_SubOptionData.m_AdjacentCardXOffset + m_SubOptionData.m_MinionParentXOffset;
			}
			else if (zonePosition == 1 && battlefieldZone.GetCards().Count > 6)
			{
				friendlyCardWidth += m_SubOptionData.m_AdjacentCardXOffset;
				x += m_CommonData.m_FriendlyCardWidth / 2f + m_SubOptionData.m_MinionParentXOffset;
			}
			else
			{
				friendlyCardWidth += m_SubOptionData.m_MinionParentXOffset * 2f;
				x -= m_CommonData.m_FriendlyCardWidth / 2f + m_SubOptionData.m_MinionParentXOffset;
			}
		}
		else
		{
			int count = subCardIDs.Count;
			friendlyCardWidth += ((count > m_CommonData.m_MaxCardsBeforeAdjusting) ? m_SubOptionData.m_PhoneMaxAdjacentCardXOffset : m_SubOptionData.m_AdjacentCardXOffset);
			x -= friendlyCardWidth / 2f * (float)(count - 1);
		}
		for (int i = 0; i < subCardIDs.Count; i++)
		{
			int id = subCardIDs[i];
			Card card = gameState.GetEntity(id).GetCard();
			if (!(card == null))
			{
				m_subOptionState.m_cards.Add(card);
				card.ForceLoadHandActor();
				card.transform.position = parentCard.transform.position;
				card.transform.localScale = INVISIBLE_SCALE;
				Vector3 position = default(Vector3);
				position.x = x + (float)i * friendlyCardWidth;
				position.y = transform.position.y;
				position.z = transform.position.z;
				iTween.MoveTo(card.gameObject, position, m_SubOptionData.m_CardShowTime);
				Vector3 localScale = transform.localScale;
				if (subCardIDs.Count > m_CommonData.m_MaxCardsBeforeAdjusting)
				{
					float scaleForCardCount = GetScaleForCardCount(subCardIDs.Count);
					localScale.x *= scaleForCardCount;
					localScale.y *= scaleForCardCount;
					localScale.z *= scaleForCardCount;
				}
				iTween.ScaleTo(card.gameObject, localScale, m_SubOptionData.m_CardShowTime);
				card.ActivateHandStateSpells();
			}
		}
		HideEnlargedHand();
	}

	private void HideSubOptions(Entity chosenEntity = null)
	{
		for (int i = 0; i < m_subOptionState.m_cards.Count; i++)
		{
			Card card = m_subOptionState.m_cards[i];
			card.DeactivateHandStateSpells();
			DeactivateChoiceCardStateSpells(card);
			if (card.GetEntity() != chosenEntity)
			{
				card.HideCard();
			}
		}
		RestoreEnlargedHand();
	}

	private bool IsEntityReady(Entity entity)
	{
		if (entity.GetZone() != TAG_ZONE.SETASIDE)
		{
			return false;
		}
		if (entity.IsBusy())
		{
			return false;
		}
		return true;
	}

	private bool IsCardReady(Card card)
	{
		return card.HasCardDef;
	}

	private bool IsCardActorReady(Card card)
	{
		return card.IsActorReady();
	}

	private Spell GetCustomChoiceRevealSpell(ChoiceState choiceState)
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

	private Spell GetCustomChoiceConcealSpell(ChoiceState choiceState)
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

	private void RevealChoiceCardsUsingCustomSpell(Spell customChoiceRevealSpell, ChoiceState state)
	{
		CustomChoiceSpell customChoiceSpell = customChoiceRevealSpell as CustomChoiceSpell;
		if (customChoiceSpell != null)
		{
			customChoiceSpell.SetChoiceState(state);
		}
		customChoiceRevealSpell.AddFinishedCallback(OnCustomChoiceRevealSpellFinished, state);
		customChoiceRevealSpell.Activate();
	}

	private void OnCustomChoiceRevealSpellFinished(Spell spell, object userData)
	{
		ChoiceState choiceState = userData as ChoiceState;
		if (choiceState == null)
		{
			Log.Power.PrintError("userData passed to ChoiceCardMgr.OnCustomChoiceRevealSpellFinished() is not of type ChoiceState.");
		}
		if (choiceState.m_isFriendly)
		{
			ShowChoiceUi(choiceState);
		}
		foreach (Card card in choiceState.m_cards)
		{
			card.ShowCard();
			ActivateChoiceCardStateSpells(card);
		}
		PlayChoiceEffects(choiceState, choiceState.m_isFriendly);
		choiceState.m_hasBeenRevealed = true;
	}

	private void ConcealChoiceCardsUsingCustomSpell(Spell customChoiceConcealSpell, ChoiceState choiceState, Network.EntitiesChosen chosen)
	{
		if (customChoiceConcealSpell.IsActive())
		{
			Log.Power.PrintError("ChoiceCardMgr.HideChoicesFromPacket(): CustomChoiceConcealSpell is already active!");
		}
		CustomChoiceSpell customChoiceSpell = customChoiceConcealSpell as CustomChoiceSpell;
		if (customChoiceSpell != null)
		{
			customChoiceSpell.SetChoiceState(choiceState);
		}
		DeactivateChoiceEffects(choiceState);
		choiceState.m_hasBeenConcealed = true;
		customChoiceConcealSpell.AddFinishedCallback(OnCustomChoiceConcealSpellFinished, chosen);
		customChoiceConcealSpell.Activate();
	}

	private void OnCustomChoiceConcealSpellFinished(Spell spell, object userData)
	{
		Network.EntitiesChosen entitiesChosen = userData as Network.EntitiesChosen;
		OnFinishedConcealChoices(entitiesChosen.PlayerId);
		GameState.Get().OnEntitiesChosenProcessed(entitiesChosen);
	}
}
