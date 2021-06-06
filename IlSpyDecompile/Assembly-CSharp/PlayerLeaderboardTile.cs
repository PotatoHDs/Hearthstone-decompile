using System.Collections.Generic;
using UnityEngine;

public class PlayerLeaderboardTile : MonoBehaviour
{
	public ProgressBar m_HealthBar;

	public GameObject m_IconSwords;

	public GameObject m_IconSkull;

	public GameObject m_IconFirst;

	public GameObject m_IconSecond;

	public GameObject m_IconThird;

	public GameObject m_IconFourth;

	public GameObject m_IconSplat;

	public PlayerLeaderboardIcon m_IconTechUp;

	public PlayerLeaderboardIcon m_IconHotStreak;

	public PlayerLeaderboardIcon m_IconTriple;

	public UberText m_splatText;

	private const string POP_OUT_PLAYMAKER_STATE = "PopOut";

	private const string POP_IN_PLAYMAKER_STATE = "PopIn";

	private const string REVEALED_PLAYMAKER_STATE = "Reveal";

	private const string UNREVEALED_PLAYMAKER_STATE = "Unrevealed";

	private const string TECH_LEVEL_PLAYMAKER_VARIABLE = "TechLevel";

	private Queue<PlayerLeaderboardManager.PlayerTileEvent> m_incomingNotifications;

	private bool m_notificationActive;

	private PlayerLeaderboardIcon m_currentAnimatingObject;

	private int m_ownerId = -1;

	public void Awake()
	{
		m_incomingNotifications = new Queue<PlayerLeaderboardManager.PlayerTileEvent>();
	}

	public void Update()
	{
		if (!m_notificationActive && m_incomingNotifications != null && m_incomingNotifications.Count > 0)
		{
			ShowNotification(m_incomingNotifications.Dequeue());
		}
		else if (!m_IconTechUp.PlaymakerIsShowing() && !m_IconTriple.PlaymakerIsShowing() && !m_IconHotStreak.PlaymakerIsShowing())
		{
			m_notificationActive = false;
		}
	}

	public void SetCurrentHealth(float healthPercent)
	{
		SetHealthBarActive(healthPercent > 0f);
		SetSkullIconActive(healthPercent == 0f);
		m_HealthBar.SetProgressBar(healthPercent);
	}

	public void SetSplatAmount(int value)
	{
		m_IconSplat.gameObject.SetActive(value != 0);
		m_splatText.gameObject.SetActive(value != 0);
		SceneUtils.EnableRenderers(m_IconSplat.GetComponent<DamageSplatSpell>().m_BloodSplat.gameObject, value != 0);
		m_splatText.Text = value.ToString();
	}

	public void SetHealthBarActive(bool active)
	{
		m_HealthBar.gameObject.SetActive(active);
	}

	public void SetSwordsIconActive(bool active)
	{
		m_IconSwords.SetActive(active);
	}

	public void SetSkullIconActive(bool active)
	{
		m_IconSkull.SetActive(active);
	}

	public void SetPlaceIcon(int currentPlace)
	{
		m_IconFirst.SetActive(value: false);
		m_IconSecond.SetActive(value: false);
		m_IconThird.SetActive(value: false);
		m_IconFourth.SetActive(value: false);
		switch (currentPlace)
		{
		case 1:
			m_IconFirst.SetActive(value: true);
			break;
		case 2:
			m_IconSecond.SetActive(value: true);
			break;
		case 3:
			m_IconThird.SetActive(value: true);
			break;
		case 4:
			m_IconFourth.SetActive(value: true);
			break;
		}
	}

	public void SetTilePopOutActive(bool active)
	{
		PlayMakerFSM component = GetComponent<PlayMakerFSM>();
		if (!(component == null))
		{
			component.SetState(active ? "PopOut" : "PopIn");
		}
	}

	public void SetTileRevealed(bool revealed, bool isNextOpponent)
	{
		PlayMakerFSM component = GetComponent<PlayMakerFSM>();
		if (!(component == null))
		{
			component.FsmVariables.GetFsmInt("IsNextOpponent").Value = (isNextOpponent ? 1 : 0);
			component.SetState(revealed ? "Reveal" : "Unrevealed");
		}
	}

	public void SetOwnerId(int playerId)
	{
		m_ownerId = playerId;
	}

	public void NotifyEvent(PlayerLeaderboardManager.PlayerTileEvent notificationType)
	{
		m_incomingNotifications.Enqueue(notificationType);
	}

	private void ShowNotification(PlayerLeaderboardManager.PlayerTileEvent notificationType)
	{
		switch (notificationType)
		{
		default:
			return;
		case PlayerLeaderboardManager.PlayerTileEvent.WIN_STREAK:
			m_currentAnimatingObject = m_IconHotStreak;
			break;
		case PlayerLeaderboardManager.PlayerTileEvent.TECH_LEVEL:
		{
			int value = 1;
			if (GameState.Get().GetPlayerInfoMap().ContainsKey(m_ownerId) && GameState.Get().GetPlayerInfoMap()[m_ownerId].GetPlayerHero() != null)
			{
				value = GameState.Get().GetPlayerInfoMap()[m_ownerId].GetPlayerHero().GetRealTimePlayerTechLevel();
			}
			value = Mathf.Clamp(value, 1, 6);
			if (value == 1)
			{
				return;
			}
			m_IconTechUp.SetPlaymakerValue("TechLevel", value);
			m_currentAnimatingObject = m_IconTechUp;
			break;
		}
		case PlayerLeaderboardManager.PlayerTileEvent.TRIPLE:
			m_currentAnimatingObject = m_IconTriple;
			break;
		}
		m_notificationActive = true;
		m_currentAnimatingObject.gameObject.SetActive(value: true);
		m_currentAnimatingObject.ClearText();
		m_currentAnimatingObject.PlaymakerShow();
	}
}
