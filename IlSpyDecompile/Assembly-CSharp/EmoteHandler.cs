using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EmoteHandler : MonoBehaviour
{
	public List<EmoteOption> m_DefaultEmotes;

	public List<EmoteOption> m_EmoteOverrides;

	public List<EmoteOption> m_HiddenEmotes;

	private List<EmoteOption> m_availableEmotes;

	private const float MIN_TIME_BETWEEN_EMOTES = 4f;

	private const float TIME_WINDOW_TO_BE_CONSIDERED_A_CHAIN = 5f;

	private const float SPAMMER_MIN_TIME_BETWEEN_EMOTES = 15f;

	private const float UBER_SPAMMER_MIN_TIME_BETWEEN_EMOTES = 45f;

	private const int NUM_EMOTES_BEFORE_CONSIDERED_A_SPAMMER = 20;

	private const int NUM_EMOTES_BEFORE_CONSIDERED_UBER_SPAMMER = 25;

	private const int NUM_CHAIN_EMOTES_BEFORE_CONSIDERED_SPAM = 2;

	private const int EMOTE_COUNT = 6;

	private const float MAX_TIME_FOR_EMOTE_RESPONSE = 6f;

	private const float EMOTE_RESPONSE_SERVER_DELAY_SLUSH = 2f;

	private static EmoteHandler s_instance;

	private bool m_emotesShown;

	private int m_shownAtFrame;

	private EmoteOption m_mousedOverEmote;

	private float m_timeSinceLastEmote = 4f;

	private int m_totalEmotes;

	private int m_chainedEmotes;

	private Map<EmoteType, float> m_timeSinceEmoteFinishedOpposing = new Map<EmoteType, float>();

	private Map<EmoteType, float> m_timeSinceEmoteFinishedFriendly = new Map<EmoteType, float>();

	private void Awake()
	{
		s_instance = this;
		GetComponent<Collider>().enabled = false;
	}

	private void Start()
	{
		GameState.Get().RegisterHeroChangedListener(OnHeroChanged);
		DetermineAvailableEmotes();
	}

	private void DetermineAvailableEmotes()
	{
		m_availableEmotes = new List<EmoteOption>();
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.USES_PREMIUM_EMOTES) && !GameState.Get().GetFriendlySidePlayer().HasTag(GAME_TAG.BATTLEGROUNDS_PREMIUM_EMOTES))
		{
			return;
		}
		foreach (EmoteOption defaultEmote in m_DefaultEmotes)
		{
			m_availableEmotes.Add(defaultEmote);
			defaultEmote.gameObject.SetActive(value: true);
		}
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		for (int i = 0; i < 6; i++)
		{
			int num = friendlySidePlayer.GetTag((GAME_TAG)(740 + i));
			if (num > 0 && num < m_EmoteOverrides.Count && (m_EmoteOverrides[num].ShouldPlayerUseEmoteOverride(friendlySidePlayer) || GameState.Get().GetBooleanGameOption(GameEntityOption.USES_PREMIUM_EMOTES)))
			{
				m_availableEmotes[i].gameObject.SetActive(value: false);
				m_availableEmotes[i] = m_EmoteOverrides[num];
				TransformUtil.CopyWorld(m_availableEmotes[i], m_DefaultEmotes[i]);
				m_availableEmotes[i].gameObject.SetActive(value: true);
			}
		}
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	private void Update()
	{
		if (GameState.Get() == null)
		{
			return;
		}
		m_timeSinceLastEmote += Time.unscaledDeltaTime;
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		if (opposingSidePlayer != null)
		{
			Card heroCard = opposingSidePlayer.GetHeroCard();
			if (!(heroCard == null))
			{
				UpdateTimeSinceEmoteFinished(heroCard, m_timeSinceEmoteFinishedOpposing);
				UpdateTimeSinceEmoteFinished(GameState.Get().GetFriendlySidePlayer().GetHeroCard(), m_timeSinceEmoteFinishedFriendly);
			}
		}
	}

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
		foreach (EmoteType item in timeSinceEmoteFinished.Keys.ToList())
		{
			if (activeEmoteSound != null && item == activeEmoteSound.GetEmoteType())
			{
				timeSinceEmoteFinished[item] = 0f;
				continue;
			}
			float num = timeSinceEmoteFinished[item];
			num = (timeSinceEmoteFinished[item] = num + Time.unscaledDeltaTime);
			if (num > 8f)
			{
				timeSinceEmoteFinished.Remove(item);
			}
		}
	}

	public static EmoteHandler Get()
	{
		return s_instance;
	}

	public void ChangeAvailableEmotes()
	{
		HideEmotes();
		DetermineAvailableEmotes();
	}

	public void ResetTimeSinceLastEmote()
	{
		if (m_timeSinceLastEmote < 9f)
		{
			m_chainedEmotes++;
		}
		else
		{
			m_chainedEmotes = 0;
		}
		m_timeSinceLastEmote = 0f;
	}

	public bool IsResponseEmote(EmoteType type)
	{
		return type == EmoteType.MIRROR_GREETINGS;
	}

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
		float value = 0f;
		float num = 6f;
		if (heroSide == Player.Side.FRIENDLY)
		{
			if (!m_timeSinceEmoteFinishedOpposing.TryGetValue(desiredEmoteType, out value))
			{
				return false;
			}
		}
		else
		{
			if (!m_timeSinceEmoteFinishedFriendly.TryGetValue(desiredEmoteType, out value))
			{
				return false;
			}
			num += 2f;
		}
		return value <= num;
	}

	public EmoteType GetEmoteResponseType(EmoteType desiredEmoteType)
	{
		if (desiredEmoteType == EmoteType.GREETINGS)
		{
			return EmoteType.MIRROR_GREETINGS;
		}
		return EmoteType.INVALID;
	}

	public EmoteType GetEmoteAntiResponseType(EmoteType desiredEmoteType)
	{
		if (desiredEmoteType == EmoteType.MIRROR_GREETINGS)
		{
			return EmoteType.GREETINGS;
		}
		return EmoteType.INVALID;
	}

	public void ShowEmotes()
	{
		if (m_emotesShown || GameState.Get().IsBusy())
		{
			return;
		}
		m_shownAtFrame = Time.frameCount;
		m_emotesShown = true;
		GetComponent<Collider>().enabled = true;
		foreach (EmoteOption availableEmote in m_availableEmotes)
		{
			availableEmote.Enable();
		}
	}

	public void HideEmotes()
	{
		if (!m_emotesShown)
		{
			return;
		}
		m_mousedOverEmote = null;
		m_emotesShown = false;
		GetComponent<Collider>().enabled = false;
		foreach (EmoteOption availableEmote in m_availableEmotes)
		{
			availableEmote.Disable();
		}
	}

	public bool AreEmotesActive()
	{
		return m_emotesShown;
	}

	public void HandleInput()
	{
		if (!HitTestEmotes(out var hitInfo))
		{
			HideEmotes();
			return;
		}
		if (GameState.Get().IsBusy())
		{
			HideEmotes();
			return;
		}
		EmoteOption component = hitInfo.transform.gameObject.GetComponent<EmoteOption>();
		if (component == null)
		{
			if (m_mousedOverEmote != null)
			{
				m_mousedOverEmote.HandleMouseOut();
				m_mousedOverEmote = null;
			}
		}
		else if (m_mousedOverEmote == null)
		{
			m_mousedOverEmote = component;
			m_mousedOverEmote.HandleMouseOver();
		}
		else if (m_mousedOverEmote != component)
		{
			m_mousedOverEmote.HandleMouseOut();
			m_mousedOverEmote = component;
			component.HandleMouseOver();
		}
		if (!UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			return;
		}
		if (m_mousedOverEmote != null)
		{
			if (EmoteSpamBlocked())
			{
				return;
			}
			m_totalEmotes++;
			if (GameState.Get().GetGameEntity().HasTag(GAME_TAG.ALL_TARGETS_RANDOM))
			{
				List<EmoteOption> list = new List<EmoteOption>();
				foreach (EmoteOption item in m_availableEmotes.Concat(m_HiddenEmotes))
				{
					if (item.CanPlayerUseEmoteType(GameState.Get().GetFriendlySidePlayer()))
					{
						list.Add(item);
					}
				}
				if (list.Count > 0)
				{
					int index = Random.Range(0, list.Count);
					list[index].DoClick();
				}
				else
				{
					Log.All.PrintError("EmoteHandler.HandleInput() - No usable emotes exist.");
				}
			}
			else
			{
				m_mousedOverEmote.DoClick();
			}
		}
		else if (UniversalInputManager.Get().IsTouchMode() && Time.frameCount != m_shownAtFrame)
		{
			HideEmotes();
		}
	}

	public bool IsMouseOverEmoteOption()
	{
		if (UniversalInputManager.Get().GetInputHitInfo(GameLayer.Default.LayerBit(), out var hitInfo) && hitInfo.transform.gameObject.GetComponent<EmoteOption>() != null)
		{
			return true;
		}
		return false;
	}

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
		if (heroCard != null && heroCard.HasUnfinishedEmoteSpell())
		{
			return true;
		}
		return false;
	}

	public bool EmoteSpamBlocked()
	{
		if (IsVisualEmoteUnfinished())
		{
			return true;
		}
		if (GameMgr.Get().IsFriendly() || GameMgr.Get().IsAI())
		{
			return false;
		}
		if (m_totalEmotes >= 25)
		{
			return m_timeSinceLastEmote < 45f;
		}
		if (m_totalEmotes >= 20)
		{
			return m_timeSinceLastEmote < 15f;
		}
		if (m_chainedEmotes >= 2)
		{
			return m_timeSinceLastEmote < 15f;
		}
		return m_timeSinceLastEmote < 4f;
	}

	public bool IsValidEmoteTypeForOpponent(EmoteType emoteType)
	{
		List<EmoteOption> list = new List<EmoteOption>();
		foreach (EmoteOption defaultEmote in m_DefaultEmotes)
		{
			list.Add(defaultEmote);
		}
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		for (int i = 0; i < 6; i++)
		{
			int num = opposingSidePlayer.GetTag((GAME_TAG)(740 + i));
			if (num > 0 && num < m_EmoteOverrides.Count && m_EmoteOverrides[num].CanPlayerUseEmoteType(opposingSidePlayer))
			{
				list[i] = m_EmoteOverrides[num];
			}
		}
		foreach (EmoteOption item in list)
		{
			if (item.HasEmoteTypeForPlayer(emoteType, opposingSidePlayer))
			{
				return true;
			}
		}
		if (GameState.Get().GetGameEntity().HasTag(GAME_TAG.ALL_TARGETS_RANDOM))
		{
			foreach (EmoteOption hiddenEmote in m_HiddenEmotes)
			{
				if (hiddenEmote.HasEmoteTypeForPlayer(emoteType, opposingSidePlayer))
				{
					return true;
				}
			}
		}
		if (IsResponseEmote(emoteType))
		{
			EmoteType emoteAntiResponseType = GetEmoteAntiResponseType(emoteType);
			if (ShouldUseEmoteResponse(emoteAntiResponseType, Player.Side.OPPOSING))
			{
				return IsValidEmoteTypeForOpponent(emoteAntiResponseType);
			}
		}
		return false;
	}

	private void OnHeroChanged(Player player, object userData)
	{
		if (!player.IsFriendlySide())
		{
			return;
		}
		DetermineAvailableEmotes();
		foreach (EmoteOption availableEmote in m_availableEmotes)
		{
			availableEmote.UpdateEmoteType();
		}
	}

	private bool HitTestEmotes(out RaycastHit hitInfo)
	{
		if (!UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast.LayerBit(), out hitInfo))
		{
			return false;
		}
		if (IsMousedOverHero(hitInfo))
		{
			return true;
		}
		if (IsMousedOverSelf(hitInfo))
		{
			return true;
		}
		if (IsMousedOverEmote(hitInfo))
		{
			return true;
		}
		return false;
	}

	private bool IsMousedOverHero(RaycastHit cardHitInfo)
	{
		Actor actor = SceneUtils.FindComponentInParents<Actor>(cardHitInfo.transform);
		if (actor == null)
		{
			return false;
		}
		Card card = actor.GetCard();
		if (card == null)
		{
			return false;
		}
		if (card.GetEntity().IsHero() && card.GetZone() is ZoneHero)
		{
			return true;
		}
		return false;
	}

	private bool IsMousedOverSelf(RaycastHit cardHitInfo)
	{
		return GetComponent<Collider>() == cardHitInfo.collider;
	}

	private bool IsMousedOverEmote(RaycastHit cardHitInfo)
	{
		foreach (EmoteOption availableEmote in m_availableEmotes)
		{
			if (cardHitInfo.transform == availableEmote.transform)
			{
				return true;
			}
		}
		return false;
	}
}
