using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000303 RID: 771
public class EmoteHandler : MonoBehaviour
{
	// Token: 0x06002956 RID: 10582 RVA: 0x000D1F60 File Offset: 0x000D0160
	private void Awake()
	{
		EmoteHandler.s_instance = this;
		base.GetComponent<Collider>().enabled = false;
	}

	// Token: 0x06002957 RID: 10583 RVA: 0x000D1F74 File Offset: 0x000D0174
	private void Start()
	{
		GameState.Get().RegisterHeroChangedListener(new GameState.HeroChangedCallback(this.OnHeroChanged), null);
		this.DetermineAvailableEmotes();
	}

	// Token: 0x06002958 RID: 10584 RVA: 0x000D1F94 File Offset: 0x000D0194
	private void DetermineAvailableEmotes()
	{
		this.m_availableEmotes = new List<EmoteOption>();
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.USES_PREMIUM_EMOTES) && !GameState.Get().GetFriendlySidePlayer().HasTag(GAME_TAG.BATTLEGROUNDS_PREMIUM_EMOTES))
		{
			return;
		}
		foreach (EmoteOption emoteOption in this.m_DefaultEmotes)
		{
			this.m_availableEmotes.Add(emoteOption);
			emoteOption.gameObject.SetActive(true);
		}
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		for (int i = 0; i < 6; i++)
		{
			int tag = friendlySidePlayer.GetTag(GAME_TAG.OVERRIDE_EMOTE_0 + i);
			if (tag > 0 && tag < this.m_EmoteOverrides.Count && (this.m_EmoteOverrides[tag].ShouldPlayerUseEmoteOverride(friendlySidePlayer) || GameState.Get().GetBooleanGameOption(GameEntityOption.USES_PREMIUM_EMOTES)))
			{
				this.m_availableEmotes[i].gameObject.SetActive(false);
				this.m_availableEmotes[i] = this.m_EmoteOverrides[tag];
				TransformUtil.CopyWorld(this.m_availableEmotes[i], this.m_DefaultEmotes[i]);
				this.m_availableEmotes[i].gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06002959 RID: 10585 RVA: 0x000D20F8 File Offset: 0x000D02F8
	private void OnDestroy()
	{
		EmoteHandler.s_instance = null;
	}

	// Token: 0x0600295A RID: 10586 RVA: 0x000D2100 File Offset: 0x000D0300
	private void Update()
	{
		if (GameState.Get() == null)
		{
			return;
		}
		this.m_timeSinceLastEmote += Time.unscaledDeltaTime;
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		if (opposingSidePlayer == null)
		{
			return;
		}
		Card heroCard = opposingSidePlayer.GetHeroCard();
		if (heroCard == null)
		{
			return;
		}
		this.UpdateTimeSinceEmoteFinished(heroCard, this.m_timeSinceEmoteFinishedOpposing);
		this.UpdateTimeSinceEmoteFinished(GameState.Get().GetFriendlySidePlayer().GetHeroCard(), this.m_timeSinceEmoteFinishedFriendly);
	}

	// Token: 0x0600295B RID: 10587 RVA: 0x000D2170 File Offset: 0x000D0370
	public void UpdateTimeSinceEmoteFinished(Card heroCard, Map<EmoteType, float> timeSinceEmoteFinished)
	{
		if (heroCard == null)
		{
			return;
		}
		EmoteEntry activeEmoteSound = heroCard.GetActiveEmoteSound();
		if (activeEmoteSound != null)
		{
			timeSinceEmoteFinished[activeEmoteSound.GetEmoteType()] = 0f;
		}
		foreach (EmoteType emoteType in timeSinceEmoteFinished.Keys.ToList<EmoteType>())
		{
			if (activeEmoteSound != null && emoteType == activeEmoteSound.GetEmoteType())
			{
				timeSinceEmoteFinished[emoteType] = 0f;
			}
			else
			{
				float num = timeSinceEmoteFinished[emoteType];
				num += Time.unscaledDeltaTime;
				timeSinceEmoteFinished[emoteType] = num;
				if (num > 8f)
				{
					timeSinceEmoteFinished.Remove(emoteType);
				}
			}
		}
	}

	// Token: 0x0600295C RID: 10588 RVA: 0x000D2228 File Offset: 0x000D0428
	public static EmoteHandler Get()
	{
		return EmoteHandler.s_instance;
	}

	// Token: 0x0600295D RID: 10589 RVA: 0x000D222F File Offset: 0x000D042F
	public void ChangeAvailableEmotes()
	{
		this.HideEmotes();
		this.DetermineAvailableEmotes();
	}

	// Token: 0x0600295E RID: 10590 RVA: 0x000D223D File Offset: 0x000D043D
	public void ResetTimeSinceLastEmote()
	{
		if (this.m_timeSinceLastEmote < 9f)
		{
			this.m_chainedEmotes++;
		}
		else
		{
			this.m_chainedEmotes = 0;
		}
		this.m_timeSinceLastEmote = 0f;
	}

	// Token: 0x0600295F RID: 10591 RVA: 0x000D226E File Offset: 0x000D046E
	public bool IsResponseEmote(EmoteType type)
	{
		return type == EmoteType.MIRROR_GREETINGS;
	}

	// Token: 0x06002960 RID: 10592 RVA: 0x000D2278 File Offset: 0x000D0478
	public bool ShouldUseEmoteResponse(EmoteType desiredEmoteType, Player.Side heroSide)
	{
		Card heroPowerCard = GameState.Get().GetOpposingSidePlayer().GetHeroPowerCard();
		Card heroPowerCard2 = GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard();
		if (heroPowerCard == null || heroPowerCard.GetEntity() == null || heroPowerCard2 == null || heroPowerCard2.GetEntity() == null || heroPowerCard.GetEntity().GetCardId() != heroPowerCard2.GetEntity().GetCardId())
		{
			return false;
		}
		Card heroCard = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		Card heroCard2 = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		if (heroCard == null || heroCard.GetEntity() == null || heroCard2 == null || heroCard2.GetEntity() == null)
		{
			return false;
		}
		string cardId = heroCard.GetEntity().GetCardId();
		string cardId2 = heroCard2.GetEntity().GetCardId();
		if (cardId != cardId2 && (!cardId.StartsWith("HERO_") || !cardId2.StartsWith("HERO_") || string.Compare(cardId, "HERO_".Length, cardId2, "HERO_".Length, 2) != 0))
		{
			return false;
		}
		float num = 0f;
		float num2 = 6f;
		if (heroSide == Player.Side.FRIENDLY)
		{
			if (!this.m_timeSinceEmoteFinishedOpposing.TryGetValue(desiredEmoteType, out num))
			{
				return false;
			}
		}
		else
		{
			if (!this.m_timeSinceEmoteFinishedFriendly.TryGetValue(desiredEmoteType, out num))
			{
				return false;
			}
			num2 += 2f;
		}
		return num <= num2;
	}

	// Token: 0x06002961 RID: 10593 RVA: 0x000D23D6 File Offset: 0x000D05D6
	public EmoteType GetEmoteResponseType(EmoteType desiredEmoteType)
	{
		if (desiredEmoteType == EmoteType.GREETINGS)
		{
			return EmoteType.MIRROR_GREETINGS;
		}
		return EmoteType.INVALID;
	}

	// Token: 0x06002962 RID: 10594 RVA: 0x000D23E0 File Offset: 0x000D05E0
	public EmoteType GetEmoteAntiResponseType(EmoteType desiredEmoteType)
	{
		if (desiredEmoteType == EmoteType.MIRROR_GREETINGS)
		{
			return EmoteType.GREETINGS;
		}
		return EmoteType.INVALID;
	}

	// Token: 0x06002963 RID: 10595 RVA: 0x000D23EC File Offset: 0x000D05EC
	public void ShowEmotes()
	{
		if (this.m_emotesShown)
		{
			return;
		}
		if (GameState.Get().IsBusy())
		{
			return;
		}
		this.m_shownAtFrame = Time.frameCount;
		this.m_emotesShown = true;
		base.GetComponent<Collider>().enabled = true;
		foreach (EmoteOption emoteOption in this.m_availableEmotes)
		{
			emoteOption.Enable();
		}
	}

	// Token: 0x06002964 RID: 10596 RVA: 0x000D2470 File Offset: 0x000D0670
	public void HideEmotes()
	{
		if (!this.m_emotesShown)
		{
			return;
		}
		this.m_mousedOverEmote = null;
		this.m_emotesShown = false;
		base.GetComponent<Collider>().enabled = false;
		foreach (EmoteOption emoteOption in this.m_availableEmotes)
		{
			emoteOption.Disable();
		}
	}

	// Token: 0x06002965 RID: 10597 RVA: 0x000D24E4 File Offset: 0x000D06E4
	public bool AreEmotesActive()
	{
		return this.m_emotesShown;
	}

	// Token: 0x06002966 RID: 10598 RVA: 0x000D24EC File Offset: 0x000D06EC
	public void HandleInput()
	{
		RaycastHit raycastHit;
		if (!this.HitTestEmotes(out raycastHit))
		{
			this.HideEmotes();
			return;
		}
		if (GameState.Get().IsBusy())
		{
			this.HideEmotes();
			return;
		}
		EmoteOption component = raycastHit.transform.gameObject.GetComponent<EmoteOption>();
		if (component == null)
		{
			if (this.m_mousedOverEmote != null)
			{
				this.m_mousedOverEmote.HandleMouseOut();
				this.m_mousedOverEmote = null;
			}
		}
		else if (this.m_mousedOverEmote == null)
		{
			this.m_mousedOverEmote = component;
			this.m_mousedOverEmote.HandleMouseOver();
		}
		else if (this.m_mousedOverEmote != component)
		{
			this.m_mousedOverEmote.HandleMouseOut();
			this.m_mousedOverEmote = component;
			component.HandleMouseOver();
		}
		if (UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			if (this.m_mousedOverEmote != null)
			{
				if (!this.EmoteSpamBlocked())
				{
					this.m_totalEmotes++;
					if (!GameState.Get().GetGameEntity().HasTag(GAME_TAG.ALL_TARGETS_RANDOM))
					{
						this.m_mousedOverEmote.DoClick();
						return;
					}
					List<EmoteOption> list = new List<EmoteOption>();
					foreach (EmoteOption emoteOption in this.m_availableEmotes.Concat(this.m_HiddenEmotes))
					{
						if (emoteOption.CanPlayerUseEmoteType(GameState.Get().GetFriendlySidePlayer()))
						{
							list.Add(emoteOption);
						}
					}
					if (list.Count > 0)
					{
						int index = UnityEngine.Random.Range(0, list.Count);
						list[index].DoClick();
						return;
					}
					Log.All.PrintError("EmoteHandler.HandleInput() - No usable emotes exist.", Array.Empty<object>());
					return;
				}
			}
			else if (UniversalInputManager.Get().IsTouchMode() && Time.frameCount != this.m_shownAtFrame)
			{
				this.HideEmotes();
			}
		}
	}

	// Token: 0x06002967 RID: 10599 RVA: 0x000D26C0 File Offset: 0x000D08C0
	public bool IsMouseOverEmoteOption()
	{
		RaycastHit raycastHit;
		return UniversalInputManager.Get().GetInputHitInfo(GameLayer.Default.LayerBit(), out raycastHit) && raycastHit.transform.gameObject.GetComponent<EmoteOption>() != null;
	}

	// Token: 0x06002968 RID: 10600 RVA: 0x000D2704 File Offset: 0x000D0904
	private bool IsVisualEmoteUnfinished()
	{
		if (GameState.Get() == null)
		{
			return false;
		}
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		if (friendlySidePlayer == null)
		{
			return false;
		}
		Card heroCard = friendlySidePlayer.GetHeroCard();
		return heroCard != null && heroCard.HasUnfinishedEmoteSpell();
	}

	// Token: 0x06002969 RID: 10601 RVA: 0x000D2748 File Offset: 0x000D0948
	public bool EmoteSpamBlocked()
	{
		if (this.IsVisualEmoteUnfinished())
		{
			return true;
		}
		if (GameMgr.Get().IsFriendly() || GameMgr.Get().IsAI())
		{
			return false;
		}
		if (this.m_totalEmotes >= 25)
		{
			return this.m_timeSinceLastEmote < 45f;
		}
		if (this.m_totalEmotes >= 20)
		{
			return this.m_timeSinceLastEmote < 15f;
		}
		if (this.m_chainedEmotes >= 2)
		{
			return this.m_timeSinceLastEmote < 15f;
		}
		return this.m_timeSinceLastEmote < 4f;
	}

	// Token: 0x0600296A RID: 10602 RVA: 0x000D27D0 File Offset: 0x000D09D0
	public bool IsValidEmoteTypeForOpponent(EmoteType emoteType)
	{
		List<EmoteOption> list = new List<EmoteOption>();
		foreach (EmoteOption item in this.m_DefaultEmotes)
		{
			list.Add(item);
		}
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		for (int i = 0; i < 6; i++)
		{
			int tag = opposingSidePlayer.GetTag(GAME_TAG.OVERRIDE_EMOTE_0 + i);
			if (tag > 0 && tag < this.m_EmoteOverrides.Count && this.m_EmoteOverrides[tag].CanPlayerUseEmoteType(opposingSidePlayer))
			{
				list[i] = this.m_EmoteOverrides[tag];
			}
		}
		using (List<EmoteOption>.Enumerator enumerator = list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.HasEmoteTypeForPlayer(emoteType, opposingSidePlayer))
				{
					return true;
				}
			}
		}
		if (GameState.Get().GetGameEntity().HasTag(GAME_TAG.ALL_TARGETS_RANDOM))
		{
			using (List<EmoteOption>.Enumerator enumerator = this.m_HiddenEmotes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasEmoteTypeForPlayer(emoteType, opposingSidePlayer))
					{
						return true;
					}
				}
			}
		}
		if (this.IsResponseEmote(emoteType))
		{
			EmoteType emoteAntiResponseType = this.GetEmoteAntiResponseType(emoteType);
			if (this.ShouldUseEmoteResponse(emoteAntiResponseType, Player.Side.OPPOSING))
			{
				return this.IsValidEmoteTypeForOpponent(emoteAntiResponseType);
			}
		}
		return false;
	}

	// Token: 0x0600296B RID: 10603 RVA: 0x000D2960 File Offset: 0x000D0B60
	private void OnHeroChanged(Player player, object userData)
	{
		if (!player.IsFriendlySide())
		{
			return;
		}
		this.DetermineAvailableEmotes();
		foreach (EmoteOption emoteOption in this.m_availableEmotes)
		{
			emoteOption.UpdateEmoteType();
		}
	}

	// Token: 0x0600296C RID: 10604 RVA: 0x000D29C0 File Offset: 0x000D0BC0
	private bool HitTestEmotes(out RaycastHit hitInfo)
	{
		return UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast.LayerBit(), out hitInfo) && (this.IsMousedOverHero(hitInfo) || this.IsMousedOverSelf(hitInfo) || this.IsMousedOverEmote(hitInfo));
	}

	// Token: 0x0600296D RID: 10605 RVA: 0x000D2A18 File Offset: 0x000D0C18
	private bool IsMousedOverHero(RaycastHit cardHitInfo)
	{
		Actor actor = SceneUtils.FindComponentInParents<Actor>(cardHitInfo.transform);
		if (actor == null)
		{
			return false;
		}
		Card card = actor.GetCard();
		return !(card == null) && (card.GetEntity().IsHero() && card.GetZone() is ZoneHero);
	}

	// Token: 0x0600296E RID: 10606 RVA: 0x000D2A6C File Offset: 0x000D0C6C
	private bool IsMousedOverSelf(RaycastHit cardHitInfo)
	{
		return base.GetComponent<Collider>() == cardHitInfo.collider;
	}

	// Token: 0x0600296F RID: 10607 RVA: 0x000D2A80 File Offset: 0x000D0C80
	private bool IsMousedOverEmote(RaycastHit cardHitInfo)
	{
		foreach (EmoteOption emoteOption in this.m_availableEmotes)
		{
			if (cardHitInfo.transform == emoteOption.transform)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04001771 RID: 6001
	public List<EmoteOption> m_DefaultEmotes;

	// Token: 0x04001772 RID: 6002
	public List<EmoteOption> m_EmoteOverrides;

	// Token: 0x04001773 RID: 6003
	public List<EmoteOption> m_HiddenEmotes;

	// Token: 0x04001774 RID: 6004
	private List<EmoteOption> m_availableEmotes;

	// Token: 0x04001775 RID: 6005
	private const float MIN_TIME_BETWEEN_EMOTES = 4f;

	// Token: 0x04001776 RID: 6006
	private const float TIME_WINDOW_TO_BE_CONSIDERED_A_CHAIN = 5f;

	// Token: 0x04001777 RID: 6007
	private const float SPAMMER_MIN_TIME_BETWEEN_EMOTES = 15f;

	// Token: 0x04001778 RID: 6008
	private const float UBER_SPAMMER_MIN_TIME_BETWEEN_EMOTES = 45f;

	// Token: 0x04001779 RID: 6009
	private const int NUM_EMOTES_BEFORE_CONSIDERED_A_SPAMMER = 20;

	// Token: 0x0400177A RID: 6010
	private const int NUM_EMOTES_BEFORE_CONSIDERED_UBER_SPAMMER = 25;

	// Token: 0x0400177B RID: 6011
	private const int NUM_CHAIN_EMOTES_BEFORE_CONSIDERED_SPAM = 2;

	// Token: 0x0400177C RID: 6012
	private const int EMOTE_COUNT = 6;

	// Token: 0x0400177D RID: 6013
	private const float MAX_TIME_FOR_EMOTE_RESPONSE = 6f;

	// Token: 0x0400177E RID: 6014
	private const float EMOTE_RESPONSE_SERVER_DELAY_SLUSH = 2f;

	// Token: 0x0400177F RID: 6015
	private static EmoteHandler s_instance;

	// Token: 0x04001780 RID: 6016
	private bool m_emotesShown;

	// Token: 0x04001781 RID: 6017
	private int m_shownAtFrame;

	// Token: 0x04001782 RID: 6018
	private EmoteOption m_mousedOverEmote;

	// Token: 0x04001783 RID: 6019
	private float m_timeSinceLastEmote = 4f;

	// Token: 0x04001784 RID: 6020
	private int m_totalEmotes;

	// Token: 0x04001785 RID: 6021
	private int m_chainedEmotes;

	// Token: 0x04001786 RID: 6022
	private Map<EmoteType, float> m_timeSinceEmoteFinishedOpposing = new Map<EmoteType, float>();

	// Token: 0x04001787 RID: 6023
	private Map<EmoteType, float> m_timeSinceEmoteFinishedFriendly = new Map<EmoteType, float>();
}
