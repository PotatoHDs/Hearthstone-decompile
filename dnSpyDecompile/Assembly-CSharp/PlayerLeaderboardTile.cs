using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200033A RID: 826
public class PlayerLeaderboardTile : MonoBehaviour
{
	// Token: 0x06002F68 RID: 12136 RVA: 0x000F20D4 File Offset: 0x000F02D4
	public void Awake()
	{
		this.m_incomingNotifications = new Queue<PlayerLeaderboardManager.PlayerTileEvent>();
	}

	// Token: 0x06002F69 RID: 12137 RVA: 0x000F20E4 File Offset: 0x000F02E4
	public void Update()
	{
		if (!this.m_notificationActive && this.m_incomingNotifications != null && this.m_incomingNotifications.Count > 0)
		{
			this.ShowNotification(this.m_incomingNotifications.Dequeue());
			return;
		}
		if (!this.m_IconTechUp.PlaymakerIsShowing() && !this.m_IconTriple.PlaymakerIsShowing() && !this.m_IconHotStreak.PlaymakerIsShowing())
		{
			this.m_notificationActive = false;
		}
	}

	// Token: 0x06002F6A RID: 12138 RVA: 0x000F214F File Offset: 0x000F034F
	public void SetCurrentHealth(float healthPercent)
	{
		this.SetHealthBarActive(healthPercent > 0f);
		this.SetSkullIconActive(healthPercent == 0f);
		this.m_HealthBar.SetProgressBar(healthPercent);
	}

	// Token: 0x06002F6B RID: 12139 RVA: 0x000F217C File Offset: 0x000F037C
	public void SetSplatAmount(int value)
	{
		this.m_IconSplat.gameObject.SetActive(value != 0);
		this.m_splatText.gameObject.SetActive(value != 0);
		SceneUtils.EnableRenderers(this.m_IconSplat.GetComponent<DamageSplatSpell>().m_BloodSplat.gameObject, value != 0);
		this.m_splatText.Text = value.ToString();
	}

	// Token: 0x06002F6C RID: 12140 RVA: 0x000F21E1 File Offset: 0x000F03E1
	public void SetHealthBarActive(bool active)
	{
		this.m_HealthBar.gameObject.SetActive(active);
	}

	// Token: 0x06002F6D RID: 12141 RVA: 0x000F21F4 File Offset: 0x000F03F4
	public void SetSwordsIconActive(bool active)
	{
		this.m_IconSwords.SetActive(active);
	}

	// Token: 0x06002F6E RID: 12142 RVA: 0x000F2202 File Offset: 0x000F0402
	public void SetSkullIconActive(bool active)
	{
		this.m_IconSkull.SetActive(active);
	}

	// Token: 0x06002F6F RID: 12143 RVA: 0x000F2210 File Offset: 0x000F0410
	public void SetPlaceIcon(int currentPlace)
	{
		this.m_IconFirst.SetActive(false);
		this.m_IconSecond.SetActive(false);
		this.m_IconThird.SetActive(false);
		this.m_IconFourth.SetActive(false);
		switch (currentPlace)
		{
		case 1:
			this.m_IconFirst.SetActive(true);
			return;
		case 2:
			this.m_IconSecond.SetActive(true);
			return;
		case 3:
			this.m_IconThird.SetActive(true);
			return;
		case 4:
			this.m_IconFourth.SetActive(true);
			return;
		default:
			return;
		}
	}

	// Token: 0x06002F70 RID: 12144 RVA: 0x000F229C File Offset: 0x000F049C
	public void SetTilePopOutActive(bool active)
	{
		PlayMakerFSM component = base.GetComponent<PlayMakerFSM>();
		if (component == null)
		{
			return;
		}
		component.SetState(active ? "PopOut" : "PopIn");
	}

	// Token: 0x06002F71 RID: 12145 RVA: 0x000F22D0 File Offset: 0x000F04D0
	public void SetTileRevealed(bool revealed, bool isNextOpponent)
	{
		PlayMakerFSM component = base.GetComponent<PlayMakerFSM>();
		if (component == null)
		{
			return;
		}
		component.FsmVariables.GetFsmInt("IsNextOpponent").Value = (isNextOpponent ? 1 : 0);
		component.SetState(revealed ? "Reveal" : "Unrevealed");
	}

	// Token: 0x06002F72 RID: 12146 RVA: 0x000F231F File Offset: 0x000F051F
	public void SetOwnerId(int playerId)
	{
		this.m_ownerId = playerId;
	}

	// Token: 0x06002F73 RID: 12147 RVA: 0x000F2328 File Offset: 0x000F0528
	public void NotifyEvent(PlayerLeaderboardManager.PlayerTileEvent notificationType)
	{
		this.m_incomingNotifications.Enqueue(notificationType);
	}

	// Token: 0x06002F74 RID: 12148 RVA: 0x000F2338 File Offset: 0x000F0538
	private void ShowNotification(PlayerLeaderboardManager.PlayerTileEvent notificationType)
	{
		switch (notificationType)
		{
		case PlayerLeaderboardManager.PlayerTileEvent.TRIPLE:
			this.m_currentAnimatingObject = this.m_IconTriple;
			break;
		case PlayerLeaderboardManager.PlayerTileEvent.WIN_STREAK:
			this.m_currentAnimatingObject = this.m_IconHotStreak;
			break;
		case PlayerLeaderboardManager.PlayerTileEvent.TECH_LEVEL:
		{
			int num = 1;
			if (GameState.Get().GetPlayerInfoMap().ContainsKey(this.m_ownerId) && GameState.Get().GetPlayerInfoMap()[this.m_ownerId].GetPlayerHero() != null)
			{
				num = GameState.Get().GetPlayerInfoMap()[this.m_ownerId].GetPlayerHero().GetRealTimePlayerTechLevel();
			}
			num = Mathf.Clamp(num, 1, 6);
			if (num == 1)
			{
				return;
			}
			this.m_IconTechUp.SetPlaymakerValue("TechLevel", num);
			this.m_currentAnimatingObject = this.m_IconTechUp;
			break;
		}
		default:
			return;
		}
		this.m_notificationActive = true;
		this.m_currentAnimatingObject.gameObject.SetActive(true);
		this.m_currentAnimatingObject.ClearText();
		this.m_currentAnimatingObject.PlaymakerShow();
	}

	// Token: 0x04001A7F RID: 6783
	public ProgressBar m_HealthBar;

	// Token: 0x04001A80 RID: 6784
	public GameObject m_IconSwords;

	// Token: 0x04001A81 RID: 6785
	public GameObject m_IconSkull;

	// Token: 0x04001A82 RID: 6786
	public GameObject m_IconFirst;

	// Token: 0x04001A83 RID: 6787
	public GameObject m_IconSecond;

	// Token: 0x04001A84 RID: 6788
	public GameObject m_IconThird;

	// Token: 0x04001A85 RID: 6789
	public GameObject m_IconFourth;

	// Token: 0x04001A86 RID: 6790
	public GameObject m_IconSplat;

	// Token: 0x04001A87 RID: 6791
	public PlayerLeaderboardIcon m_IconTechUp;

	// Token: 0x04001A88 RID: 6792
	public PlayerLeaderboardIcon m_IconHotStreak;

	// Token: 0x04001A89 RID: 6793
	public PlayerLeaderboardIcon m_IconTriple;

	// Token: 0x04001A8A RID: 6794
	public UberText m_splatText;

	// Token: 0x04001A8B RID: 6795
	private const string POP_OUT_PLAYMAKER_STATE = "PopOut";

	// Token: 0x04001A8C RID: 6796
	private const string POP_IN_PLAYMAKER_STATE = "PopIn";

	// Token: 0x04001A8D RID: 6797
	private const string REVEALED_PLAYMAKER_STATE = "Reveal";

	// Token: 0x04001A8E RID: 6798
	private const string UNREVEALED_PLAYMAKER_STATE = "Unrevealed";

	// Token: 0x04001A8F RID: 6799
	private const string TECH_LEVEL_PLAYMAKER_VARIABLE = "TechLevel";

	// Token: 0x04001A90 RID: 6800
	private Queue<PlayerLeaderboardManager.PlayerTileEvent> m_incomingNotifications;

	// Token: 0x04001A91 RID: 6801
	private bool m_notificationActive;

	// Token: 0x04001A92 RID: 6802
	private PlayerLeaderboardIcon m_currentAnimatingObject;

	// Token: 0x04001A93 RID: 6803
	private int m_ownerId = -1;
}
