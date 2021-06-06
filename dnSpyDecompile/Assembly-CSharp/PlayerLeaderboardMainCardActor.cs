using System;
using UnityEngine;

// Token: 0x02000336 RID: 822
public class PlayerLeaderboardMainCardActor : Actor
{
	// Token: 0x06002F1C RID: 12060 RVA: 0x000F016A File Offset: 0x000EE36A
	public void UpdatePlayerNameText(string text)
	{
		if (this.m_playerNameText != null)
		{
			this.m_playerNameText.Text = text;
		}
	}

	// Token: 0x06002F1D RID: 12061 RVA: 0x000F0186 File Offset: 0x000EE386
	public void UpdateAlternateNameText(string text)
	{
		if (this.m_alternateNameText != null)
		{
			this.m_alternateNameText.SetGameStringText(GameStrings.Format("GAMEPLAY_BACON_ALTERNATE_PLAYER_NAME", new object[]
			{
				text
			}));
			this.m_alternateNameText.UpdateNow(true);
		}
	}

	// Token: 0x06002F1E RID: 12062 RVA: 0x000F01C4 File Offset: 0x000EE3C4
	protected override void ShowImpl(bool ignoreSpells)
	{
		base.ShowImpl(ignoreSpells);
		if (this.m_nameTextMesh != null)
		{
			this.m_nameTextMesh.gameObject.SetActive(false);
			if (this.m_nameTextMesh.RenderOnObject)
			{
				this.m_nameTextMesh.RenderOnObject.GetComponent<Renderer>().enabled = false;
			}
		}
	}

	// Token: 0x06002F1F RID: 12063 RVA: 0x000F021F File Offset: 0x000EE41F
	public void SetAlternateNameTextActive(bool active)
	{
		if (this.m_alternateNameText != null)
		{
			this.m_alternateNameText.gameObject.SetActive(active);
		}
	}

	// Token: 0x06002F20 RID: 12064 RVA: 0x000F0240 File Offset: 0x000EE440
	public void SetFullyHighlighted(bool highlighted)
	{
		this.m_fullSelectionHighlight.SetActive(highlighted);
	}

	// Token: 0x06002F21 RID: 12065 RVA: 0x000F024E File Offset: 0x000EE44E
	public void PauseHealthUpdates()
	{
		this.m_pausedHealthTextMesh = this.m_healthTextMesh;
		this.m_healthTextMesh = null;
	}

	// Token: 0x06002F22 RID: 12066 RVA: 0x000F0263 File Offset: 0x000EE463
	public void ResumeHealthUpdates()
	{
		if (this.m_pausedHealthTextMesh == null)
		{
			return;
		}
		this.m_healthTextMesh = this.m_pausedHealthTextMesh;
		this.m_pausedHealthTextMesh = null;
		base.UpdateMinionStatsImmediately();
	}

	// Token: 0x06002F23 RID: 12067 RVA: 0x000F0290 File Offset: 0x000EE490
	public void ToggleLockedHeroView(bool isOn)
	{
		this.m_lockedHeroBackground.SetActive(isOn);
		this.m_lockIcon.SetActive(isOn);
		if (isOn)
		{
			this.SetAlternateNameTextActive(false);
			this.m_playerNameBackground.SetActive(false);
			this.m_nameTextMesh.gameObject.SetActive(false);
			base.GetHealthObject().Hide();
			base.GetAttackObject().Hide();
		}
		this.SetFullyHighlighted(false);
	}

	// Token: 0x04001A47 RID: 6727
	public UberText m_playerNameText;

	// Token: 0x04001A48 RID: 6728
	public UberText m_alternateNameText;

	// Token: 0x04001A49 RID: 6729
	public GameObject m_playerNameBackground;

	// Token: 0x04001A4A RID: 6730
	public GameObject m_fullSelectionHighlight;

	// Token: 0x04001A4B RID: 6731
	public GameObject m_lockIcon;

	// Token: 0x04001A4C RID: 6732
	public GameObject m_lockedHeroBackground;

	// Token: 0x04001A4D RID: 6733
	private const string BACON_ALTERNATE_NAME_STRING_ID = "GAMEPLAY_BACON_ALTERNATE_PLAYER_NAME";

	// Token: 0x04001A4E RID: 6734
	private UberText m_pausedHealthTextMesh;
}
