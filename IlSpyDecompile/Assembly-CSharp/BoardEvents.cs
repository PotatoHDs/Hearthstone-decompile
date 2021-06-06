using System.Collections.Generic;
using UnityEngine;

public class BoardEvents : MonoBehaviour
{
	public enum EVENT_TYPE
	{
		FriendlyHeroDamage,
		OpponentHeroDamage,
		FriendlyHeroHeal,
		OpponentHeroHeal,
		FriendlyLegendaryMinionSpawn,
		OpponentLegendaryMinionSpawn,
		FriendlyLegendaryMinionDeath,
		OpponentLegendaryMinionDeath,
		FriendlyMinionSpawn,
		OpponentMinionSpawn,
		FriendlyMinionDeath,
		OpponentMinionDeath,
		FriendlyMinionDamage,
		OpponentMinionDamage,
		FriendlyMinionHeal,
		OpponentMinionHeal,
		LargeMinionShake
	}

	public class EventData
	{
		public EVENT_TYPE m_eventType;

		public float m_timeStamp;

		public Card m_card;

		public float m_value;

		public TAG_RARITY m_rarity;
	}

	public class EventCallback
	{
		public EventDelegate callback;

		public float minimumWeight;
	}

	public delegate void LargeShakeEventDelegate();

	public delegate void EventDelegate(float weight);

	private const float AI_PROCESS_INTERVAL = 3.5f;

	private const float PROCESS_INTERVAL = 1.25f;

	private const float FAST_PROCESS_INTERVAL = 0.15f;

	private float m_nextProcessTime;

	private float m_nextFastProcessTime;

	private LinkedList<EventData> m_events = new LinkedList<EventData>();

	private LinkedList<EventData> m_fastEvents = new LinkedList<EventData>();

	private List<LinkedListNode<EventData>> m_removeEvents = new List<LinkedListNode<EventData>>();

	private Dictionary<EVENT_TYPE, float> m_weights = new Dictionary<EVENT_TYPE, float>();

	private List<LargeShakeEventDelegate> m_largeShakeEventCallbacks;

	private List<EventCallback> m_friendlyHeroDamageCallacks;

	private List<EventCallback> m_opponentHeroDamageCallacks;

	private List<EventCallback> m_opponentMinionDamageCallacks;

	private List<EventCallback> m_friendlyMinionDamageCallacks;

	private List<EventCallback> m_friendlyHeroHealCallbacks;

	private List<EventCallback> m_opponentHeroHealCallbacks;

	private List<EventCallback> m_friendlyMinionHealCallbacks;

	private List<EventCallback> m_opponentMinionHealCallbacks;

	private List<EventCallback> m_frindlyLegendaryMinionSpawnCallbacks;

	private List<EventCallback> m_opponentLegendaryMinionSpawnCallbacks;

	private List<EventCallback> m_frindlyMinionSpawnCallbacks;

	private List<EventCallback> m_opponentMinionSpawnCallbacks;

	private List<EventCallback> m_frindlyLegendaryMinionDeathCallbacks;

	private List<EventCallback> m_opponentLegendaryMinionDeathCallbacks;

	private List<EventCallback> m_frindlyMinionDeathCallbacks;

	private List<EventCallback> m_opponentMinionDeathCallbacks;

	private static BoardEvents s_instance;

	private void Awake()
	{
		s_instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (Time.timeSinceLevelLoad > m_nextFastProcessTime)
		{
			m_nextFastProcessTime = Time.timeSinceLevelLoad + 0.15f;
			ProcessImmediateEvents();
		}
		else if (Time.timeSinceLevelLoad > m_nextProcessTime)
		{
			m_nextProcessTime = Time.timeSinceLevelLoad + 1.25f;
			ProcessEvents();
		}
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static BoardEvents Get()
	{
		if (s_instance == null)
		{
			Board board = Board.Get();
			if (board == null)
			{
				return null;
			}
			s_instance = board.gameObject.AddComponent<BoardEvents>();
		}
		return s_instance;
	}

	public void RegisterLargeShakeEvent(LargeShakeEventDelegate callback)
	{
		if (m_largeShakeEventCallbacks == null)
		{
			m_largeShakeEventCallbacks = new List<LargeShakeEventDelegate>();
		}
		m_largeShakeEventCallbacks.Add(callback);
	}

	public void RegisterFriendlyHeroDamageEvent(EventDelegate callback, float minimumWeight = 1f)
	{
		m_friendlyHeroDamageCallacks = RegisterEvent(m_friendlyHeroDamageCallacks, callback, minimumWeight);
	}

	public void RegisterOpponentHeroDamageEvent(EventDelegate callback, float minimumWeight = 1f)
	{
		m_opponentHeroDamageCallacks = RegisterEvent(m_opponentHeroDamageCallacks, callback, minimumWeight);
	}

	public void RegisterFriendlyMinionDamageEvent(EventDelegate callback, float minimumWeight = 1f)
	{
		m_friendlyMinionDamageCallacks = RegisterEvent(m_friendlyMinionDamageCallacks, callback, minimumWeight);
	}

	public void RegisterOpponentMinionDamageEvent(EventDelegate callback, float minimumWeight = 1f)
	{
		m_opponentMinionDamageCallacks = RegisterEvent(m_opponentMinionDamageCallacks, callback, minimumWeight);
	}

	public void RegisterFriendlyHeroHealEvent(EventDelegate callback, float minimumWeight = 1f)
	{
		m_friendlyHeroHealCallbacks = RegisterEvent(m_friendlyHeroHealCallbacks, callback, minimumWeight);
	}

	public void RegisterOpponentHeroHealEvent(EventDelegate callback, float minimumWeight = 1f)
	{
		m_opponentHeroHealCallbacks = RegisterEvent(m_opponentHeroHealCallbacks, callback, minimumWeight);
	}

	public void RegisterFriendlyMinionHealEvent(EventDelegate callback, float minimumWeight = 1f)
	{
		m_friendlyMinionHealCallbacks = RegisterEvent(m_friendlyMinionHealCallbacks, callback, minimumWeight);
	}

	public void RegisterOpponentMinionHealEvent(EventDelegate callback, float minimumWeight = 1f)
	{
		m_opponentMinionHealCallbacks = RegisterEvent(m_opponentMinionHealCallbacks, callback, minimumWeight);
	}

	public void RegisterFriendlyLegendaryMinionSpawnEvent(EventDelegate callback, float minimumWeight = 1f)
	{
		m_frindlyLegendaryMinionSpawnCallbacks = RegisterEvent(m_frindlyLegendaryMinionSpawnCallbacks, callback, minimumWeight);
	}

	public void RegisterOppenentLegendaryMinionSpawnEvent(EventDelegate callback, float minimumWeight = 1f)
	{
		m_opponentLegendaryMinionSpawnCallbacks = RegisterEvent(m_opponentLegendaryMinionSpawnCallbacks, callback, minimumWeight);
	}

	public void RegisterFriendlyMinionSpawnEvent(EventDelegate callback, float minimumWeight = 1f)
	{
		m_frindlyMinionSpawnCallbacks = RegisterEvent(m_frindlyMinionSpawnCallbacks, callback, minimumWeight);
	}

	public void RegisterOppenentMinionSpawnEvent(EventDelegate callback, float minimumWeight = 1f)
	{
		m_opponentMinionSpawnCallbacks = RegisterEvent(m_opponentMinionSpawnCallbacks, callback, minimumWeight);
	}

	public void RegisterFriendlyLegendaryMinionDeathEvent(EventDelegate callback, float minimumWeight = 1f)
	{
		m_frindlyLegendaryMinionDeathCallbacks = RegisterEvent(m_frindlyLegendaryMinionDeathCallbacks, callback, minimumWeight);
	}

	public void RegisterOppenentLegendaryMinionDeathEvent(EventDelegate callback, float minimumWeight = 1f)
	{
		m_opponentLegendaryMinionDeathCallbacks = RegisterEvent(m_opponentLegendaryMinionDeathCallbacks, callback, minimumWeight);
	}

	public void RegisterFriendlyMinionDeathEvent(EventDelegate callback, float minimumWeight = 1f)
	{
		m_frindlyMinionDeathCallbacks = RegisterEvent(m_frindlyMinionDeathCallbacks, callback, minimumWeight);
	}

	public void RegisterOppenentMinionDeathEvent(EventDelegate callback, float minimumWeight = 1f)
	{
		m_opponentMinionDeathCallbacks = RegisterEvent(m_opponentMinionDeathCallbacks, callback, minimumWeight);
	}

	private List<EventCallback> RegisterEvent(List<EventCallback> eventList, EventDelegate callback, float minimumWeight)
	{
		if (eventList == null)
		{
			eventList = new List<EventCallback>();
		}
		EventCallback eventCallback = new EventCallback();
		eventCallback.callback = callback;
		eventCallback.minimumWeight = minimumWeight;
		eventList.Add(eventCallback);
		return eventList;
	}

	public void MinionShakeEvent(ShakeMinionIntensity shakeIntensity, float customIntensity)
	{
		if (shakeIntensity == ShakeMinionIntensity.LargeShake)
		{
			EventData eventData = new EventData();
			eventData.m_timeStamp = Time.timeSinceLevelLoad;
			eventData.m_eventType = EVENT_TYPE.LargeMinionShake;
			m_fastEvents.AddLast(eventData);
		}
	}

	public void DamageEvent(Card targetCard, float damage)
	{
		Entity entity = targetCard.GetEntity();
		if (entity == null)
		{
			return;
		}
		EventData eventData = new EventData();
		eventData.m_card = targetCard;
		eventData.m_timeStamp = Time.timeSinceLevelLoad;
		if (entity.IsHero())
		{
			if (targetCard.GetControllerSide() == Player.Side.FRIENDLY)
			{
				eventData.m_eventType = EVENT_TYPE.FriendlyHeroDamage;
			}
			else
			{
				eventData.m_eventType = EVENT_TYPE.OpponentHeroDamage;
			}
		}
		else if (targetCard.GetControllerSide() == Player.Side.FRIENDLY)
		{
			eventData.m_eventType = EVENT_TYPE.FriendlyMinionDamage;
		}
		else
		{
			eventData.m_eventType = EVENT_TYPE.OpponentMinionDamage;
		}
		eventData.m_value = damage;
		eventData.m_rarity = entity.GetRarity();
		m_events.AddLast(eventData);
	}

	public void HealEvent(Card targetCard, float health)
	{
		Entity entity = targetCard.GetEntity();
		if (entity == null)
		{
			return;
		}
		EventData eventData = new EventData();
		eventData.m_card = targetCard;
		eventData.m_timeStamp = Time.timeSinceLevelLoad;
		if (entity.IsHero())
		{
			if (targetCard.GetControllerSide() == Player.Side.FRIENDLY)
			{
				eventData.m_eventType = EVENT_TYPE.FriendlyHeroHeal;
			}
			else
			{
				eventData.m_eventType = EVENT_TYPE.OpponentHeroHeal;
			}
		}
		else if (targetCard.GetControllerSide() == Player.Side.FRIENDLY)
		{
			eventData.m_eventType = EVENT_TYPE.FriendlyMinionHeal;
		}
		else
		{
			eventData.m_eventType = EVENT_TYPE.OpponentMinionHeal;
		}
		eventData.m_value = health;
		eventData.m_rarity = entity.GetRarity();
		m_events.AddLast(eventData);
	}

	public void SummonedEvent(Card minionCard)
	{
		Entity entity = minionCard.GetEntity();
		if (entity == null || !entity.IsMinion())
		{
			return;
		}
		EventData eventData = new EventData();
		eventData.m_card = minionCard;
		eventData.m_timeStamp = Time.timeSinceLevelLoad;
		if (entity.GetRarity() == TAG_RARITY.LEGENDARY)
		{
			if (minionCard.GetControllerSide() == Player.Side.FRIENDLY)
			{
				eventData.m_eventType = EVENT_TYPE.FriendlyLegendaryMinionSpawn;
			}
			else
			{
				eventData.m_eventType = EVENT_TYPE.OpponentLegendaryMinionSpawn;
			}
		}
		else if (minionCard.GetControllerSide() == Player.Side.FRIENDLY)
		{
			eventData.m_eventType = EVENT_TYPE.FriendlyMinionSpawn;
		}
		else
		{
			eventData.m_eventType = EVENT_TYPE.OpponentMinionSpawn;
		}
		eventData.m_value = entity.GetDefCost();
		eventData.m_rarity = entity.GetRarity();
		m_events.AddLast(eventData);
	}

	public void DeathEvent(Card card)
	{
		Entity entity = card.GetEntity();
		if (entity == null)
		{
			return;
		}
		EventData eventData = new EventData();
		eventData.m_card = card;
		eventData.m_timeStamp = Time.timeSinceLevelLoad;
		if (entity.GetRarity() == TAG_RARITY.LEGENDARY)
		{
			if (card.GetControllerSide() == Player.Side.FRIENDLY)
			{
				eventData.m_eventType = EVENT_TYPE.FriendlyLegendaryMinionDeath;
			}
			else
			{
				eventData.m_eventType = EVENT_TYPE.OpponentLegendaryMinionDeath;
			}
		}
		else if (card.GetControllerSide() == Player.Side.FRIENDLY)
		{
			eventData.m_eventType = EVENT_TYPE.FriendlyMinionDeath;
		}
		else
		{
			eventData.m_eventType = EVENT_TYPE.OpponentMinionDeath;
		}
		eventData.m_value = entity.GetDefCost();
		eventData.m_rarity = entity.GetRarity();
		m_events.AddLast(eventData);
	}

	private void ProcessImmediateEvents()
	{
		if (m_fastEvents.Count == 0 || m_largeShakeEventCallbacks == null)
		{
			return;
		}
		LinkedListNode<EventData> linkedListNode = m_fastEvents.First;
		while (linkedListNode != null)
		{
			EventData value = linkedListNode.Value;
			LinkedListNode<EventData> item = linkedListNode;
			linkedListNode = linkedListNode.Next;
			if (value.m_timeStamp + 0.15f < Time.timeSinceLevelLoad)
			{
				m_removeEvents.Add(item);
			}
			else if (value.m_eventType == EVENT_TYPE.LargeMinionShake)
			{
				AddWeight(EVENT_TYPE.LargeMinionShake, 1f);
				m_removeEvents.Add(item);
			}
		}
		for (int i = 0; i < m_removeEvents.Count; i++)
		{
			LinkedListNode<EventData> linkedListNode2 = m_removeEvents[i];
			if (linkedListNode2 != null)
			{
				m_fastEvents.Remove(linkedListNode2);
			}
		}
		m_removeEvents.Clear();
		if (m_weights.ContainsKey(EVENT_TYPE.LargeMinionShake) && m_weights[EVENT_TYPE.LargeMinionShake] > 0f)
		{
			LargeShakeEvent();
		}
		m_weights.Clear();
	}

	private void ProcessEvents()
	{
		if (m_events.Count == 0)
		{
			return;
		}
		LinkedListNode<EventData> linkedListNode = m_events.First;
		while (linkedListNode != null)
		{
			EventData value = linkedListNode.Value;
			LinkedListNode<EventData> item = linkedListNode;
			linkedListNode = linkedListNode.Next;
			if (value.m_timeStamp + 3.5f < Time.timeSinceLevelLoad)
			{
				m_removeEvents.Add(item);
			}
			else
			{
				AddWeight(value.m_eventType, value.m_value);
			}
		}
		for (int i = 0; i < m_removeEvents.Count; i++)
		{
			LinkedListNode<EventData> linkedListNode2 = m_removeEvents[i];
			if (linkedListNode2 != null)
			{
				m_events.Remove(linkedListNode2);
			}
		}
		m_removeEvents.Clear();
		if (m_weights.Count == 0)
		{
			return;
		}
		EVENT_TYPE? eVENT_TYPE = null;
		float num = -1f;
		foreach (EVENT_TYPE key in m_weights.Keys)
		{
			if (!(m_weights[key] < num))
			{
				eVENT_TYPE = key;
				num = m_weights[key];
			}
		}
		if (eVENT_TYPE.HasValue)
		{
			switch (eVENT_TYPE)
			{
			case EVENT_TYPE.FriendlyHeroDamage:
				CallbackEvent(m_friendlyHeroDamageCallacks, num);
				m_events.Clear();
				break;
			case EVENT_TYPE.OpponentHeroDamage:
				CallbackEvent(m_opponentHeroDamageCallacks, num);
				m_events.Clear();
				break;
			case EVENT_TYPE.FriendlyHeroHeal:
				CallbackEvent(m_friendlyHeroHealCallbacks, num);
				m_events.Clear();
				break;
			case EVENT_TYPE.OpponentHeroHeal:
				CallbackEvent(m_opponentHeroHealCallbacks, num);
				m_events.Clear();
				break;
			case EVENT_TYPE.FriendlyLegendaryMinionSpawn:
				CallbackEvent(m_frindlyLegendaryMinionSpawnCallbacks, num);
				m_events.Clear();
				break;
			case EVENT_TYPE.OpponentLegendaryMinionSpawn:
				CallbackEvent(m_opponentLegendaryMinionSpawnCallbacks, num);
				m_events.Clear();
				break;
			case EVENT_TYPE.FriendlyLegendaryMinionDeath:
				CallbackEvent(m_frindlyLegendaryMinionDeathCallbacks, num);
				m_events.Clear();
				break;
			case EVENT_TYPE.OpponentLegendaryMinionDeath:
				CallbackEvent(m_opponentLegendaryMinionDeathCallbacks, num);
				m_events.Clear();
				break;
			case EVENT_TYPE.FriendlyMinionSpawn:
				CallbackEvent(m_frindlyMinionSpawnCallbacks, num);
				m_events.Clear();
				break;
			case EVENT_TYPE.OpponentMinionSpawn:
				CallbackEvent(m_opponentMinionSpawnCallbacks, num);
				m_events.Clear();
				break;
			case EVENT_TYPE.FriendlyMinionDeath:
				CallbackEvent(m_frindlyMinionDeathCallbacks, num);
				m_events.Clear();
				break;
			case EVENT_TYPE.OpponentMinionDeath:
				CallbackEvent(m_opponentMinionDeathCallbacks, num);
				m_events.Clear();
				break;
			case EVENT_TYPE.FriendlyMinionDamage:
				CallbackEvent(m_friendlyMinionDamageCallacks, num);
				m_events.Clear();
				break;
			case EVENT_TYPE.OpponentMinionDamage:
				CallbackEvent(m_opponentMinionDamageCallacks, num);
				m_events.Clear();
				break;
			case EVENT_TYPE.FriendlyMinionHeal:
				CallbackEvent(m_friendlyMinionHealCallbacks, num);
				m_events.Clear();
				break;
			case EVENT_TYPE.OpponentMinionHeal:
				CallbackEvent(m_opponentMinionHealCallbacks, num);
				m_events.Clear();
				break;
			default:
				Debug.LogWarning($"BoardEvents: Event type unknown when processing event weights: {eVENT_TYPE}");
				break;
			}
			m_weights.Clear();
		}
	}

	private void LargeShakeEvent()
	{
		if (m_largeShakeEventCallbacks == null)
		{
			return;
		}
		for (int num = m_largeShakeEventCallbacks.Count - 1; num >= 0; num--)
		{
			if (m_largeShakeEventCallbacks[num] == null)
			{
				m_largeShakeEventCallbacks.RemoveAt(num);
			}
			else
			{
				m_largeShakeEventCallbacks[num]();
			}
		}
	}

	private void CallbackEvent(List<EventCallback> eventList, float weight)
	{
		if (eventList == null)
		{
			return;
		}
		for (int num = eventList.Count - 1; num >= 0; num--)
		{
			if (eventList[num] == null)
			{
				eventList.RemoveAt(num);
			}
			else if (weight >= eventList[num].minimumWeight)
			{
				eventList[num].callback(weight);
			}
		}
	}

	private void AddWeight(EVENT_TYPE eventType, float weight)
	{
		if (m_weights.ContainsKey(eventType))
		{
			m_weights[eventType] += weight;
		}
		else
		{
			m_weights.Add(eventType, weight);
		}
	}
}
