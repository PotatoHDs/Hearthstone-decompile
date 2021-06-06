using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000865 RID: 2149
public class BoardEvents : MonoBehaviour
{
	// Token: 0x06007402 RID: 29698 RVA: 0x00253810 File Offset: 0x00251A10
	private void Awake()
	{
		BoardEvents.s_instance = this;
	}

	// Token: 0x06007403 RID: 29699 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x06007404 RID: 29700 RVA: 0x00253818 File Offset: 0x00251A18
	private void Update()
	{
		if (Time.timeSinceLevelLoad > this.m_nextFastProcessTime)
		{
			this.m_nextFastProcessTime = Time.timeSinceLevelLoad + 0.15f;
			this.ProcessImmediateEvents();
			return;
		}
		if (Time.timeSinceLevelLoad > this.m_nextProcessTime)
		{
			this.m_nextProcessTime = Time.timeSinceLevelLoad + 1.25f;
			this.ProcessEvents();
		}
	}

	// Token: 0x06007405 RID: 29701 RVA: 0x0025386E File Offset: 0x00251A6E
	private void OnDestroy()
	{
		BoardEvents.s_instance = null;
	}

	// Token: 0x06007406 RID: 29702 RVA: 0x00253878 File Offset: 0x00251A78
	public static BoardEvents Get()
	{
		if (BoardEvents.s_instance == null)
		{
			Board board = Board.Get();
			if (board == null)
			{
				return null;
			}
			BoardEvents.s_instance = board.gameObject.AddComponent<BoardEvents>();
		}
		return BoardEvents.s_instance;
	}

	// Token: 0x06007407 RID: 29703 RVA: 0x002538B8 File Offset: 0x00251AB8
	public void RegisterLargeShakeEvent(BoardEvents.LargeShakeEventDelegate callback)
	{
		if (this.m_largeShakeEventCallbacks == null)
		{
			this.m_largeShakeEventCallbacks = new List<BoardEvents.LargeShakeEventDelegate>();
		}
		this.m_largeShakeEventCallbacks.Add(callback);
	}

	// Token: 0x06007408 RID: 29704 RVA: 0x002538D9 File Offset: 0x00251AD9
	public void RegisterFriendlyHeroDamageEvent(BoardEvents.EventDelegate callback, float minimumWeight = 1f)
	{
		this.m_friendlyHeroDamageCallacks = this.RegisterEvent(this.m_friendlyHeroDamageCallacks, callback, minimumWeight);
	}

	// Token: 0x06007409 RID: 29705 RVA: 0x002538EF File Offset: 0x00251AEF
	public void RegisterOpponentHeroDamageEvent(BoardEvents.EventDelegate callback, float minimumWeight = 1f)
	{
		this.m_opponentHeroDamageCallacks = this.RegisterEvent(this.m_opponentHeroDamageCallacks, callback, minimumWeight);
	}

	// Token: 0x0600740A RID: 29706 RVA: 0x00253905 File Offset: 0x00251B05
	public void RegisterFriendlyMinionDamageEvent(BoardEvents.EventDelegate callback, float minimumWeight = 1f)
	{
		this.m_friendlyMinionDamageCallacks = this.RegisterEvent(this.m_friendlyMinionDamageCallacks, callback, minimumWeight);
	}

	// Token: 0x0600740B RID: 29707 RVA: 0x0025391B File Offset: 0x00251B1B
	public void RegisterOpponentMinionDamageEvent(BoardEvents.EventDelegate callback, float minimumWeight = 1f)
	{
		this.m_opponentMinionDamageCallacks = this.RegisterEvent(this.m_opponentMinionDamageCallacks, callback, minimumWeight);
	}

	// Token: 0x0600740C RID: 29708 RVA: 0x00253931 File Offset: 0x00251B31
	public void RegisterFriendlyHeroHealEvent(BoardEvents.EventDelegate callback, float minimumWeight = 1f)
	{
		this.m_friendlyHeroHealCallbacks = this.RegisterEvent(this.m_friendlyHeroHealCallbacks, callback, minimumWeight);
	}

	// Token: 0x0600740D RID: 29709 RVA: 0x00253947 File Offset: 0x00251B47
	public void RegisterOpponentHeroHealEvent(BoardEvents.EventDelegate callback, float minimumWeight = 1f)
	{
		this.m_opponentHeroHealCallbacks = this.RegisterEvent(this.m_opponentHeroHealCallbacks, callback, minimumWeight);
	}

	// Token: 0x0600740E RID: 29710 RVA: 0x0025395D File Offset: 0x00251B5D
	public void RegisterFriendlyMinionHealEvent(BoardEvents.EventDelegate callback, float minimumWeight = 1f)
	{
		this.m_friendlyMinionHealCallbacks = this.RegisterEvent(this.m_friendlyMinionHealCallbacks, callback, minimumWeight);
	}

	// Token: 0x0600740F RID: 29711 RVA: 0x00253973 File Offset: 0x00251B73
	public void RegisterOpponentMinionHealEvent(BoardEvents.EventDelegate callback, float minimumWeight = 1f)
	{
		this.m_opponentMinionHealCallbacks = this.RegisterEvent(this.m_opponentMinionHealCallbacks, callback, minimumWeight);
	}

	// Token: 0x06007410 RID: 29712 RVA: 0x00253989 File Offset: 0x00251B89
	public void RegisterFriendlyLegendaryMinionSpawnEvent(BoardEvents.EventDelegate callback, float minimumWeight = 1f)
	{
		this.m_frindlyLegendaryMinionSpawnCallbacks = this.RegisterEvent(this.m_frindlyLegendaryMinionSpawnCallbacks, callback, minimumWeight);
	}

	// Token: 0x06007411 RID: 29713 RVA: 0x0025399F File Offset: 0x00251B9F
	public void RegisterOppenentLegendaryMinionSpawnEvent(BoardEvents.EventDelegate callback, float minimumWeight = 1f)
	{
		this.m_opponentLegendaryMinionSpawnCallbacks = this.RegisterEvent(this.m_opponentLegendaryMinionSpawnCallbacks, callback, minimumWeight);
	}

	// Token: 0x06007412 RID: 29714 RVA: 0x002539B5 File Offset: 0x00251BB5
	public void RegisterFriendlyMinionSpawnEvent(BoardEvents.EventDelegate callback, float minimumWeight = 1f)
	{
		this.m_frindlyMinionSpawnCallbacks = this.RegisterEvent(this.m_frindlyMinionSpawnCallbacks, callback, minimumWeight);
	}

	// Token: 0x06007413 RID: 29715 RVA: 0x002539CB File Offset: 0x00251BCB
	public void RegisterOppenentMinionSpawnEvent(BoardEvents.EventDelegate callback, float minimumWeight = 1f)
	{
		this.m_opponentMinionSpawnCallbacks = this.RegisterEvent(this.m_opponentMinionSpawnCallbacks, callback, minimumWeight);
	}

	// Token: 0x06007414 RID: 29716 RVA: 0x002539E1 File Offset: 0x00251BE1
	public void RegisterFriendlyLegendaryMinionDeathEvent(BoardEvents.EventDelegate callback, float minimumWeight = 1f)
	{
		this.m_frindlyLegendaryMinionDeathCallbacks = this.RegisterEvent(this.m_frindlyLegendaryMinionDeathCallbacks, callback, minimumWeight);
	}

	// Token: 0x06007415 RID: 29717 RVA: 0x002539F7 File Offset: 0x00251BF7
	public void RegisterOppenentLegendaryMinionDeathEvent(BoardEvents.EventDelegate callback, float minimumWeight = 1f)
	{
		this.m_opponentLegendaryMinionDeathCallbacks = this.RegisterEvent(this.m_opponentLegendaryMinionDeathCallbacks, callback, minimumWeight);
	}

	// Token: 0x06007416 RID: 29718 RVA: 0x00253A0D File Offset: 0x00251C0D
	public void RegisterFriendlyMinionDeathEvent(BoardEvents.EventDelegate callback, float minimumWeight = 1f)
	{
		this.m_frindlyMinionDeathCallbacks = this.RegisterEvent(this.m_frindlyMinionDeathCallbacks, callback, minimumWeight);
	}

	// Token: 0x06007417 RID: 29719 RVA: 0x00253A23 File Offset: 0x00251C23
	public void RegisterOppenentMinionDeathEvent(BoardEvents.EventDelegate callback, float minimumWeight = 1f)
	{
		this.m_opponentMinionDeathCallbacks = this.RegisterEvent(this.m_opponentMinionDeathCallbacks, callback, minimumWeight);
	}

	// Token: 0x06007418 RID: 29720 RVA: 0x00253A3C File Offset: 0x00251C3C
	private List<BoardEvents.EventCallback> RegisterEvent(List<BoardEvents.EventCallback> eventList, BoardEvents.EventDelegate callback, float minimumWeight)
	{
		if (eventList == null)
		{
			eventList = new List<BoardEvents.EventCallback>();
		}
		eventList.Add(new BoardEvents.EventCallback
		{
			callback = callback,
			minimumWeight = minimumWeight
		});
		return eventList;
	}

	// Token: 0x06007419 RID: 29721 RVA: 0x00253A70 File Offset: 0x00251C70
	public void MinionShakeEvent(ShakeMinionIntensity shakeIntensity, float customIntensity)
	{
		if (shakeIntensity != ShakeMinionIntensity.LargeShake)
		{
			return;
		}
		BoardEvents.EventData eventData = new BoardEvents.EventData();
		eventData.m_timeStamp = Time.timeSinceLevelLoad;
		eventData.m_eventType = BoardEvents.EVENT_TYPE.LargeMinionShake;
		this.m_fastEvents.AddLast(eventData);
	}

	// Token: 0x0600741A RID: 29722 RVA: 0x00253AA8 File Offset: 0x00251CA8
	public void DamageEvent(Card targetCard, float damage)
	{
		Entity entity = targetCard.GetEntity();
		if (entity == null)
		{
			return;
		}
		BoardEvents.EventData eventData = new BoardEvents.EventData();
		eventData.m_card = targetCard;
		eventData.m_timeStamp = Time.timeSinceLevelLoad;
		if (entity.IsHero())
		{
			if (targetCard.GetControllerSide() == Player.Side.FRIENDLY)
			{
				eventData.m_eventType = BoardEvents.EVENT_TYPE.FriendlyHeroDamage;
			}
			else
			{
				eventData.m_eventType = BoardEvents.EVENT_TYPE.OpponentHeroDamage;
			}
		}
		else if (targetCard.GetControllerSide() == Player.Side.FRIENDLY)
		{
			eventData.m_eventType = BoardEvents.EVENT_TYPE.FriendlyMinionDamage;
		}
		else
		{
			eventData.m_eventType = BoardEvents.EVENT_TYPE.OpponentMinionDamage;
		}
		eventData.m_value = damage;
		eventData.m_rarity = entity.GetRarity();
		this.m_events.AddLast(eventData);
	}

	// Token: 0x0600741B RID: 29723 RVA: 0x00253B38 File Offset: 0x00251D38
	public void HealEvent(Card targetCard, float health)
	{
		Entity entity = targetCard.GetEntity();
		if (entity == null)
		{
			return;
		}
		BoardEvents.EventData eventData = new BoardEvents.EventData();
		eventData.m_card = targetCard;
		eventData.m_timeStamp = Time.timeSinceLevelLoad;
		if (entity.IsHero())
		{
			if (targetCard.GetControllerSide() == Player.Side.FRIENDLY)
			{
				eventData.m_eventType = BoardEvents.EVENT_TYPE.FriendlyHeroHeal;
			}
			else
			{
				eventData.m_eventType = BoardEvents.EVENT_TYPE.OpponentHeroHeal;
			}
		}
		else if (targetCard.GetControllerSide() == Player.Side.FRIENDLY)
		{
			eventData.m_eventType = BoardEvents.EVENT_TYPE.FriendlyMinionHeal;
		}
		else
		{
			eventData.m_eventType = BoardEvents.EVENT_TYPE.OpponentMinionHeal;
		}
		eventData.m_value = health;
		eventData.m_rarity = entity.GetRarity();
		this.m_events.AddLast(eventData);
	}

	// Token: 0x0600741C RID: 29724 RVA: 0x00253BC8 File Offset: 0x00251DC8
	public void SummonedEvent(Card minionCard)
	{
		Entity entity = minionCard.GetEntity();
		if (entity == null)
		{
			return;
		}
		if (!entity.IsMinion())
		{
			return;
		}
		BoardEvents.EventData eventData = new BoardEvents.EventData();
		eventData.m_card = minionCard;
		eventData.m_timeStamp = Time.timeSinceLevelLoad;
		if (entity.GetRarity() == TAG_RARITY.LEGENDARY)
		{
			if (minionCard.GetControllerSide() == Player.Side.FRIENDLY)
			{
				eventData.m_eventType = BoardEvents.EVENT_TYPE.FriendlyLegendaryMinionSpawn;
			}
			else
			{
				eventData.m_eventType = BoardEvents.EVENT_TYPE.OpponentLegendaryMinionSpawn;
			}
		}
		else if (minionCard.GetControllerSide() == Player.Side.FRIENDLY)
		{
			eventData.m_eventType = BoardEvents.EVENT_TYPE.FriendlyMinionSpawn;
		}
		else
		{
			eventData.m_eventType = BoardEvents.EVENT_TYPE.OpponentMinionSpawn;
		}
		eventData.m_value = (float)entity.GetDefCost();
		eventData.m_rarity = entity.GetRarity();
		this.m_events.AddLast(eventData);
	}

	// Token: 0x0600741D RID: 29725 RVA: 0x00253C68 File Offset: 0x00251E68
	public void DeathEvent(Card card)
	{
		Entity entity = card.GetEntity();
		if (entity == null)
		{
			return;
		}
		BoardEvents.EventData eventData = new BoardEvents.EventData();
		eventData.m_card = card;
		eventData.m_timeStamp = Time.timeSinceLevelLoad;
		if (entity.GetRarity() == TAG_RARITY.LEGENDARY)
		{
			if (card.GetControllerSide() == Player.Side.FRIENDLY)
			{
				eventData.m_eventType = BoardEvents.EVENT_TYPE.FriendlyLegendaryMinionDeath;
			}
			else
			{
				eventData.m_eventType = BoardEvents.EVENT_TYPE.OpponentLegendaryMinionDeath;
			}
		}
		else if (card.GetControllerSide() == Player.Side.FRIENDLY)
		{
			eventData.m_eventType = BoardEvents.EVENT_TYPE.FriendlyMinionDeath;
		}
		else
		{
			eventData.m_eventType = BoardEvents.EVENT_TYPE.OpponentMinionDeath;
		}
		eventData.m_value = (float)entity.GetDefCost();
		eventData.m_rarity = entity.GetRarity();
		this.m_events.AddLast(eventData);
	}

	// Token: 0x0600741E RID: 29726 RVA: 0x00253D00 File Offset: 0x00251F00
	private void ProcessImmediateEvents()
	{
		if (this.m_fastEvents.Count == 0)
		{
			return;
		}
		if (this.m_largeShakeEventCallbacks == null)
		{
			return;
		}
		LinkedListNode<BoardEvents.EventData> linkedListNode = this.m_fastEvents.First;
		while (linkedListNode != null)
		{
			BoardEvents.EventData value = linkedListNode.Value;
			LinkedListNode<BoardEvents.EventData> item = linkedListNode;
			linkedListNode = linkedListNode.Next;
			if (value.m_timeStamp + 0.15f < Time.timeSinceLevelLoad)
			{
				this.m_removeEvents.Add(item);
			}
			else if (value.m_eventType == BoardEvents.EVENT_TYPE.LargeMinionShake)
			{
				this.AddWeight(BoardEvents.EVENT_TYPE.LargeMinionShake, 1f);
				this.m_removeEvents.Add(item);
			}
		}
		for (int i = 0; i < this.m_removeEvents.Count; i++)
		{
			LinkedListNode<BoardEvents.EventData> linkedListNode2 = this.m_removeEvents[i];
			if (linkedListNode2 != null)
			{
				this.m_fastEvents.Remove(linkedListNode2);
			}
		}
		this.m_removeEvents.Clear();
		if (this.m_weights.ContainsKey(BoardEvents.EVENT_TYPE.LargeMinionShake) && this.m_weights[BoardEvents.EVENT_TYPE.LargeMinionShake] > 0f)
		{
			this.LargeShakeEvent();
		}
		this.m_weights.Clear();
	}

	// Token: 0x0600741F RID: 29727 RVA: 0x00253E00 File Offset: 0x00252000
	private void ProcessEvents()
	{
		if (this.m_events.Count == 0)
		{
			return;
		}
		LinkedListNode<BoardEvents.EventData> linkedListNode = this.m_events.First;
		while (linkedListNode != null)
		{
			BoardEvents.EventData value = linkedListNode.Value;
			LinkedListNode<BoardEvents.EventData> item = linkedListNode;
			linkedListNode = linkedListNode.Next;
			if (value.m_timeStamp + 3.5f < Time.timeSinceLevelLoad)
			{
				this.m_removeEvents.Add(item);
			}
			else
			{
				this.AddWeight(value.m_eventType, value.m_value);
			}
		}
		for (int i = 0; i < this.m_removeEvents.Count; i++)
		{
			LinkedListNode<BoardEvents.EventData> linkedListNode2 = this.m_removeEvents[i];
			if (linkedListNode2 != null)
			{
				this.m_events.Remove(linkedListNode2);
			}
		}
		this.m_removeEvents.Clear();
		if (this.m_weights.Count == 0)
		{
			return;
		}
		BoardEvents.EVENT_TYPE? event_TYPE = null;
		float num = -1f;
		foreach (BoardEvents.EVENT_TYPE event_TYPE2 in this.m_weights.Keys)
		{
			if (this.m_weights[event_TYPE2] >= num)
			{
				event_TYPE = new BoardEvents.EVENT_TYPE?(event_TYPE2);
				num = this.m_weights[event_TYPE2];
			}
		}
		if (event_TYPE == null)
		{
			return;
		}
		if (event_TYPE != null)
		{
			switch (event_TYPE.GetValueOrDefault())
			{
			case BoardEvents.EVENT_TYPE.FriendlyHeroDamage:
				this.CallbackEvent(this.m_friendlyHeroDamageCallacks, num);
				this.m_events.Clear();
				goto IL_362;
			case BoardEvents.EVENT_TYPE.OpponentHeroDamage:
				this.CallbackEvent(this.m_opponentHeroDamageCallacks, num);
				this.m_events.Clear();
				goto IL_362;
			case BoardEvents.EVENT_TYPE.FriendlyHeroHeal:
				this.CallbackEvent(this.m_friendlyHeroHealCallbacks, num);
				this.m_events.Clear();
				goto IL_362;
			case BoardEvents.EVENT_TYPE.OpponentHeroHeal:
				this.CallbackEvent(this.m_opponentHeroHealCallbacks, num);
				this.m_events.Clear();
				goto IL_362;
			case BoardEvents.EVENT_TYPE.FriendlyLegendaryMinionSpawn:
				this.CallbackEvent(this.m_frindlyLegendaryMinionSpawnCallbacks, num);
				this.m_events.Clear();
				goto IL_362;
			case BoardEvents.EVENT_TYPE.OpponentLegendaryMinionSpawn:
				this.CallbackEvent(this.m_opponentLegendaryMinionSpawnCallbacks, num);
				this.m_events.Clear();
				goto IL_362;
			case BoardEvents.EVENT_TYPE.FriendlyLegendaryMinionDeath:
				this.CallbackEvent(this.m_frindlyLegendaryMinionDeathCallbacks, num);
				this.m_events.Clear();
				goto IL_362;
			case BoardEvents.EVENT_TYPE.OpponentLegendaryMinionDeath:
				this.CallbackEvent(this.m_opponentLegendaryMinionDeathCallbacks, num);
				this.m_events.Clear();
				goto IL_362;
			case BoardEvents.EVENT_TYPE.FriendlyMinionSpawn:
				this.CallbackEvent(this.m_frindlyMinionSpawnCallbacks, num);
				this.m_events.Clear();
				goto IL_362;
			case BoardEvents.EVENT_TYPE.OpponentMinionSpawn:
				this.CallbackEvent(this.m_opponentMinionSpawnCallbacks, num);
				this.m_events.Clear();
				goto IL_362;
			case BoardEvents.EVENT_TYPE.FriendlyMinionDeath:
				this.CallbackEvent(this.m_frindlyMinionDeathCallbacks, num);
				this.m_events.Clear();
				goto IL_362;
			case BoardEvents.EVENT_TYPE.OpponentMinionDeath:
				this.CallbackEvent(this.m_opponentMinionDeathCallbacks, num);
				this.m_events.Clear();
				goto IL_362;
			case BoardEvents.EVENT_TYPE.FriendlyMinionDamage:
				this.CallbackEvent(this.m_friendlyMinionDamageCallacks, num);
				this.m_events.Clear();
				goto IL_362;
			case BoardEvents.EVENT_TYPE.OpponentMinionDamage:
				this.CallbackEvent(this.m_opponentMinionDamageCallacks, num);
				this.m_events.Clear();
				goto IL_362;
			case BoardEvents.EVENT_TYPE.FriendlyMinionHeal:
				this.CallbackEvent(this.m_friendlyMinionHealCallbacks, num);
				this.m_events.Clear();
				goto IL_362;
			case BoardEvents.EVENT_TYPE.OpponentMinionHeal:
				this.CallbackEvent(this.m_opponentMinionHealCallbacks, num);
				this.m_events.Clear();
				goto IL_362;
			}
		}
		Debug.LogWarning(string.Format("BoardEvents: Event type unknown when processing event weights: {0}", event_TYPE));
		IL_362:
		this.m_weights.Clear();
	}

	// Token: 0x06007420 RID: 29728 RVA: 0x0025418C File Offset: 0x0025238C
	private void LargeShakeEvent()
	{
		if (this.m_largeShakeEventCallbacks == null)
		{
			return;
		}
		for (int i = this.m_largeShakeEventCallbacks.Count - 1; i >= 0; i--)
		{
			if (this.m_largeShakeEventCallbacks[i] == null)
			{
				this.m_largeShakeEventCallbacks.RemoveAt(i);
			}
			else
			{
				this.m_largeShakeEventCallbacks[i]();
			}
		}
	}

	// Token: 0x06007421 RID: 29729 RVA: 0x002541E8 File Offset: 0x002523E8
	private void CallbackEvent(List<BoardEvents.EventCallback> eventList, float weight)
	{
		if (eventList == null)
		{
			return;
		}
		for (int i = eventList.Count - 1; i >= 0; i--)
		{
			if (eventList[i] == null)
			{
				eventList.RemoveAt(i);
			}
			else if (weight >= eventList[i].minimumWeight)
			{
				eventList[i].callback(weight);
			}
		}
	}

	// Token: 0x06007422 RID: 29730 RVA: 0x00254240 File Offset: 0x00252440
	private void AddWeight(BoardEvents.EVENT_TYPE eventType, float weight)
	{
		if (this.m_weights.ContainsKey(eventType))
		{
			Dictionary<BoardEvents.EVENT_TYPE, float> weights = this.m_weights;
			weights[eventType] += weight;
			return;
		}
		this.m_weights.Add(eventType, weight);
	}

	// Token: 0x04005C29 RID: 23593
	private const float AI_PROCESS_INTERVAL = 3.5f;

	// Token: 0x04005C2A RID: 23594
	private const float PROCESS_INTERVAL = 1.25f;

	// Token: 0x04005C2B RID: 23595
	private const float FAST_PROCESS_INTERVAL = 0.15f;

	// Token: 0x04005C2C RID: 23596
	private float m_nextProcessTime;

	// Token: 0x04005C2D RID: 23597
	private float m_nextFastProcessTime;

	// Token: 0x04005C2E RID: 23598
	private LinkedList<BoardEvents.EventData> m_events = new LinkedList<BoardEvents.EventData>();

	// Token: 0x04005C2F RID: 23599
	private LinkedList<BoardEvents.EventData> m_fastEvents = new LinkedList<BoardEvents.EventData>();

	// Token: 0x04005C30 RID: 23600
	private List<LinkedListNode<BoardEvents.EventData>> m_removeEvents = new List<LinkedListNode<BoardEvents.EventData>>();

	// Token: 0x04005C31 RID: 23601
	private Dictionary<BoardEvents.EVENT_TYPE, float> m_weights = new Dictionary<BoardEvents.EVENT_TYPE, float>();

	// Token: 0x04005C32 RID: 23602
	private List<BoardEvents.LargeShakeEventDelegate> m_largeShakeEventCallbacks;

	// Token: 0x04005C33 RID: 23603
	private List<BoardEvents.EventCallback> m_friendlyHeroDamageCallacks;

	// Token: 0x04005C34 RID: 23604
	private List<BoardEvents.EventCallback> m_opponentHeroDamageCallacks;

	// Token: 0x04005C35 RID: 23605
	private List<BoardEvents.EventCallback> m_opponentMinionDamageCallacks;

	// Token: 0x04005C36 RID: 23606
	private List<BoardEvents.EventCallback> m_friendlyMinionDamageCallacks;

	// Token: 0x04005C37 RID: 23607
	private List<BoardEvents.EventCallback> m_friendlyHeroHealCallbacks;

	// Token: 0x04005C38 RID: 23608
	private List<BoardEvents.EventCallback> m_opponentHeroHealCallbacks;

	// Token: 0x04005C39 RID: 23609
	private List<BoardEvents.EventCallback> m_friendlyMinionHealCallbacks;

	// Token: 0x04005C3A RID: 23610
	private List<BoardEvents.EventCallback> m_opponentMinionHealCallbacks;

	// Token: 0x04005C3B RID: 23611
	private List<BoardEvents.EventCallback> m_frindlyLegendaryMinionSpawnCallbacks;

	// Token: 0x04005C3C RID: 23612
	private List<BoardEvents.EventCallback> m_opponentLegendaryMinionSpawnCallbacks;

	// Token: 0x04005C3D RID: 23613
	private List<BoardEvents.EventCallback> m_frindlyMinionSpawnCallbacks;

	// Token: 0x04005C3E RID: 23614
	private List<BoardEvents.EventCallback> m_opponentMinionSpawnCallbacks;

	// Token: 0x04005C3F RID: 23615
	private List<BoardEvents.EventCallback> m_frindlyLegendaryMinionDeathCallbacks;

	// Token: 0x04005C40 RID: 23616
	private List<BoardEvents.EventCallback> m_opponentLegendaryMinionDeathCallbacks;

	// Token: 0x04005C41 RID: 23617
	private List<BoardEvents.EventCallback> m_frindlyMinionDeathCallbacks;

	// Token: 0x04005C42 RID: 23618
	private List<BoardEvents.EventCallback> m_opponentMinionDeathCallbacks;

	// Token: 0x04005C43 RID: 23619
	private static BoardEvents s_instance;

	// Token: 0x02002460 RID: 9312
	public enum EVENT_TYPE
	{
		// Token: 0x0400EA08 RID: 59912
		FriendlyHeroDamage,
		// Token: 0x0400EA09 RID: 59913
		OpponentHeroDamage,
		// Token: 0x0400EA0A RID: 59914
		FriendlyHeroHeal,
		// Token: 0x0400EA0B RID: 59915
		OpponentHeroHeal,
		// Token: 0x0400EA0C RID: 59916
		FriendlyLegendaryMinionSpawn,
		// Token: 0x0400EA0D RID: 59917
		OpponentLegendaryMinionSpawn,
		// Token: 0x0400EA0E RID: 59918
		FriendlyLegendaryMinionDeath,
		// Token: 0x0400EA0F RID: 59919
		OpponentLegendaryMinionDeath,
		// Token: 0x0400EA10 RID: 59920
		FriendlyMinionSpawn,
		// Token: 0x0400EA11 RID: 59921
		OpponentMinionSpawn,
		// Token: 0x0400EA12 RID: 59922
		FriendlyMinionDeath,
		// Token: 0x0400EA13 RID: 59923
		OpponentMinionDeath,
		// Token: 0x0400EA14 RID: 59924
		FriendlyMinionDamage,
		// Token: 0x0400EA15 RID: 59925
		OpponentMinionDamage,
		// Token: 0x0400EA16 RID: 59926
		FriendlyMinionHeal,
		// Token: 0x0400EA17 RID: 59927
		OpponentMinionHeal,
		// Token: 0x0400EA18 RID: 59928
		LargeMinionShake
	}

	// Token: 0x02002461 RID: 9313
	public class EventData
	{
		// Token: 0x0400EA19 RID: 59929
		public BoardEvents.EVENT_TYPE m_eventType;

		// Token: 0x0400EA1A RID: 59930
		public float m_timeStamp;

		// Token: 0x0400EA1B RID: 59931
		public Card m_card;

		// Token: 0x0400EA1C RID: 59932
		public float m_value;

		// Token: 0x0400EA1D RID: 59933
		public TAG_RARITY m_rarity;
	}

	// Token: 0x02002462 RID: 9314
	public class EventCallback
	{
		// Token: 0x0400EA1E RID: 59934
		public BoardEvents.EventDelegate callback;

		// Token: 0x0400EA1F RID: 59935
		public float minimumWeight;
	}

	// Token: 0x02002463 RID: 9315
	// (Invoke) Token: 0x06012F24 RID: 77604
	public delegate void LargeShakeEventDelegate();

	// Token: 0x02002464 RID: 9316
	// (Invoke) Token: 0x06012F28 RID: 77608
	public delegate void EventDelegate(float weight);
}
