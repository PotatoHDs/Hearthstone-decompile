using UnityEngine;

[CustomEditClass]
public class AdventureChooserSubButton : ChooserSubButton
{
	[CustomEditField(Sections = "Progress UI")]
	public GameObject m_progressCounter;

	[CustomEditField(Sections = "Progress UI")]
	public UberText m_progressCounterText;

	[CustomEditField(Sections = "Progress UI")]
	public GameObject m_heroicSkull;

	public float m_ComingSoonBannerHeightOverride;

	private AdventureDbId m_TargetAdventure;

	private AdventureModeDbId m_TargetMode;

	public void SetAdventure(AdventureDbId id, AdventureModeDbId mode)
	{
		m_TargetAdventure = id;
		m_TargetMode = mode;
		ShowRemainingProgressCount();
	}

	public AdventureDbId GetAdventure()
	{
		return m_TargetAdventure;
	}

	public AdventureModeDbId GetMode()
	{
		return m_TargetMode;
	}

	public void ShowRemainingProgressCount()
	{
		int num = 0;
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)m_TargetAdventure, (int)m_TargetMode);
		if (adventureDataRecord != null && adventureDataRecord.ShowPlayableScenariosCount)
		{
			num = ((m_TargetMode != AdventureModeDbId.CLASS_CHALLENGE) ? AdventureProgressMgr.Get().GetNumPlayableAdventureScenarios(m_TargetAdventure, m_TargetMode) : AdventureProgressMgr.Get().GetPlayableClassChallenges(m_TargetAdventure, m_TargetMode));
		}
		if (GameUtils.IsModeHeroic(m_TargetMode))
		{
			if (m_heroicSkull != null)
			{
				if (num > 0)
				{
					m_heroicSkull.SetActive(value: true);
				}
				else
				{
					m_heroicSkull.SetActive(value: false);
				}
			}
			if (m_progressCounter != null)
			{
				m_progressCounter.SetActive(value: false);
			}
			return;
		}
		if (m_heroicSkull != null)
		{
			m_heroicSkull.SetActive(value: false);
		}
		if (m_progressCounter != null)
		{
			if (num > 0)
			{
				m_progressCounter.SetActive(value: true);
				m_progressCounterText.Text = num.ToString();
			}
			else
			{
				m_progressCounter.SetActive(value: false);
			}
		}
	}
}
