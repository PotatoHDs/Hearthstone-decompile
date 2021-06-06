using UnityEngine;

public class PlayerLeaderboardMainCardActor : Actor
{
	public UberText m_playerNameText;

	public UberText m_alternateNameText;

	public GameObject m_playerNameBackground;

	public GameObject m_fullSelectionHighlight;

	public GameObject m_lockIcon;

	public GameObject m_lockedHeroBackground;

	private const string BACON_ALTERNATE_NAME_STRING_ID = "GAMEPLAY_BACON_ALTERNATE_PLAYER_NAME";

	private UberText m_pausedHealthTextMesh;

	public void UpdatePlayerNameText(string text)
	{
		if (m_playerNameText != null)
		{
			m_playerNameText.Text = text;
		}
	}

	public void UpdateAlternateNameText(string text)
	{
		if (m_alternateNameText != null)
		{
			m_alternateNameText.SetGameStringText(GameStrings.Format("GAMEPLAY_BACON_ALTERNATE_PLAYER_NAME", text));
			m_alternateNameText.UpdateNow(updateIfInactive: true);
		}
	}

	protected override void ShowImpl(bool ignoreSpells)
	{
		base.ShowImpl(ignoreSpells);
		if (m_nameTextMesh != null)
		{
			m_nameTextMesh.gameObject.SetActive(value: false);
			if ((bool)m_nameTextMesh.RenderOnObject)
			{
				m_nameTextMesh.RenderOnObject.GetComponent<Renderer>().enabled = false;
			}
		}
	}

	public void SetAlternateNameTextActive(bool active)
	{
		if (m_alternateNameText != null)
		{
			m_alternateNameText.gameObject.SetActive(active);
		}
	}

	public void SetFullyHighlighted(bool highlighted)
	{
		m_fullSelectionHighlight.SetActive(highlighted);
	}

	public void PauseHealthUpdates()
	{
		m_pausedHealthTextMesh = m_healthTextMesh;
		m_healthTextMesh = null;
	}

	public void ResumeHealthUpdates()
	{
		if (!(m_pausedHealthTextMesh == null))
		{
			m_healthTextMesh = m_pausedHealthTextMesh;
			m_pausedHealthTextMesh = null;
			UpdateMinionStatsImmediately();
		}
	}

	public void ToggleLockedHeroView(bool isOn)
	{
		m_lockedHeroBackground.SetActive(isOn);
		m_lockIcon.SetActive(isOn);
		if (isOn)
		{
			SetAlternateNameTextActive(active: false);
			m_playerNameBackground.SetActive(value: false);
			m_nameTextMesh.gameObject.SetActive(value: false);
			GetHealthObject().Hide();
			GetAttackObject().Hide();
		}
		SetFullyHighlighted(highlighted: false);
	}
}
