using UnityEngine;

public class EnemyEmoteHandler : MonoBehaviour
{
	public GameObject m_SquelchEmote;

	public MeshRenderer m_SquelchEmoteBackplate;

	public UberText m_SquelchEmoteText;

	public string m_SquelchStringTag;

	public string m_UnsquelchStringTag;

	private static EnemyEmoteHandler s_instance;

	private Vector3 m_squelchEmoteStartingScale;

	private bool m_emotesShown;

	private int m_shownAtFrame;

	private bool m_squelchMousedOver;

	private Map<int, bool> m_squelched;

	private const int PLAYERS_IN_BATTLEGROUNDS = 8;

	private void Awake()
	{
		s_instance = this;
		GetComponent<Collider>().enabled = false;
		m_squelchEmoteStartingScale = m_SquelchEmote.transform.localScale;
		m_squelched = new Map<int, bool>(8);
		for (int i = 0; i < 8; i++)
		{
			m_squelched.Add(i + 1, value: false);
		}
		m_SquelchEmoteText.gameObject.SetActive(value: false);
		m_SquelchEmoteBackplate.enabled = false;
		m_SquelchEmote.transform.localScale = Vector3.zero;
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static EnemyEmoteHandler Get()
	{
		return s_instance;
	}

	public bool AreEmotesActive()
	{
		return m_emotesShown;
	}

	public bool IsSquelched(int playerId)
	{
		if (m_squelched.ContainsKey(playerId))
		{
			return m_squelched[playerId];
		}
		return false;
	}

	private bool AnySquelched()
	{
		foreach (bool value in m_squelched.Values)
		{
			if (value)
			{
				return true;
			}
		}
		return false;
	}

	public void ShowEmotes()
	{
		if (!m_emotesShown)
		{
			m_emotesShown = true;
			GetComponent<Collider>().enabled = true;
			m_shownAtFrame = Time.frameCount;
			if (AnySquelched())
			{
				m_SquelchEmoteText.Text = GameStrings.Get(m_UnsquelchStringTag);
			}
			else
			{
				m_SquelchEmoteText.Text = GameStrings.Get(m_SquelchStringTag);
			}
			m_SquelchEmoteBackplate.enabled = true;
			m_SquelchEmoteText.gameObject.SetActive(value: true);
			m_SquelchEmote.GetComponent<Collider>().enabled = true;
			iTween.Stop(m_SquelchEmote);
			iTween.ScaleTo(m_SquelchEmote, iTween.Hash("scale", m_squelchEmoteStartingScale, "time", 0.5f, "ignoretimescale", true, "easetype", iTween.EaseType.easeOutElastic));
		}
	}

	public void HideEmotes()
	{
		if (m_emotesShown)
		{
			m_emotesShown = false;
			GetComponent<Collider>().enabled = false;
			m_SquelchEmote.GetComponent<Collider>().enabled = false;
			iTween.Stop(m_SquelchEmote);
			iTween.ScaleTo(m_SquelchEmote, iTween.Hash("scale", Vector3.zero, "time", 0.1f, "ignoretimescale", true, "easetype", iTween.EaseType.linear, "oncompletetarget", base.gameObject, "oncomplete", "FinishDisable"));
		}
	}

	public void HandleInput()
	{
		if (!HitTestEmotes(out var hitInfo))
		{
			HideEmotes();
			return;
		}
		if (hitInfo.transform.gameObject != m_SquelchEmote)
		{
			if (m_squelchMousedOver)
			{
				MouseOutSquelch();
				m_squelchMousedOver = false;
			}
		}
		else if (!m_squelchMousedOver)
		{
			m_squelchMousedOver = true;
			MouseOverSquelch();
		}
		if (UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			if (m_squelchMousedOver)
			{
				DoSquelchClick();
			}
			else if (UniversalInputManager.Get().IsTouchMode() && Time.frameCount != m_shownAtFrame)
			{
				HideEmotes();
			}
		}
	}

	public bool IsMouseOverEmoteOption()
	{
		if (UniversalInputManager.Get().GetInputHitInfo(GameLayer.Default.LayerBit(), out var hitInfo) && hitInfo.transform.gameObject == m_SquelchEmote)
		{
			return true;
		}
		return false;
	}

	private void MouseOverSquelch()
	{
		iTween.ScaleTo(m_SquelchEmote, iTween.Hash("scale", m_squelchEmoteStartingScale * 1.1f, "time", 0.2f, "ignoretimescale", true));
	}

	private void MouseOutSquelch()
	{
		iTween.ScaleTo(m_SquelchEmote, iTween.Hash("scale", m_squelchEmoteStartingScale, "time", 0.2f, "ignoretimescale", true));
	}

	private void DoSquelchClick()
	{
		m_squelched[GameState.Get().GetOpposingPlayerId()] = !m_squelched[GameState.Get().GetOpposingPlayerId()];
		HideEmotes();
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
		if (card.GetEntity().IsHero())
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
		if (cardHitInfo.transform == m_SquelchEmote.transform)
		{
			return true;
		}
		return false;
	}
}
