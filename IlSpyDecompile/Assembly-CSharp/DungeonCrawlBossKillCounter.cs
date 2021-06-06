using System;
using System.Collections.Generic;
using Hearthstone.DungeonCrawl;
using UnityEngine;

public class DungeonCrawlBossKillCounter : MonoBehaviour
{
	[Serializable]
	public class BossKillCounterStyleOverride
	{
		public DungeonRunVisualStyle VisualStyle;

		public string BossWinsRunNotCompletedString;

		public string BossWinsRunCompletedString;

		public string RunWinsString;

		public Material NumberHolderShadowMaterial;

		public Color DescriptionTextColor;
	}

	public UberText[] m_bossWinsText;

	public UberText m_runWinsText;

	public UberText m_runNotCompleteBossWinsHeader;

	public UberText m_runCompleteBossWinsHeader;

	public UberText m_runCompleteRunWinsHeader;

	public UberText m_fullPanelText;

	public GameObject m_runNotCompletedPanel;

	public GameObject m_runCompletedPanel;

	public GameObject[] m_numberHolderShadow;

	public BossKillCounterStyleOverride[] m_bossKillCounterStyle;

	private long m_bossWins;

	private long m_runWins;

	private TAG_CLASS m_heroClass;

	private string m_bossWinsHeaderRunNotCompletedString;

	private string m_bossWinsHeaderRunCompletedString;

	private IDungeonCrawlData m_dungeonCrawlData;

	private void Awake()
	{
		m_runNotCompletedPanel.SetActive(value: false);
		m_runCompletedPanel.SetActive(value: false);
	}

	public void SetDungeonRunData(IDungeonCrawlData data)
	{
		m_dungeonCrawlData = data;
		SetBossKillCounterVisualStyle();
	}

	public void SetHeroClass(TAG_CLASS heroClass)
	{
		m_heroClass = heroClass;
	}

	public void SetBossWins(long bossWins)
	{
		m_bossWins = bossWins;
		UberText[] bossWinsText = m_bossWinsText;
		for (int i = 0; i < bossWinsText.Length; i++)
		{
			bossWinsText[i].Text = m_bossWins.ToString();
		}
	}

	public void SetRunWins(long runWins)
	{
		m_runWins = runWins;
		m_runWinsText.Text = m_runWins.ToString();
	}

	public void UpdateLayout()
	{
		AdventureDataDbfRecord selectedAdventureDataRecord = m_dungeonCrawlData.GetSelectedAdventureDataRecord();
		if (selectedAdventureDataRecord != null && selectedAdventureDataRecord.DungeonCrawlShowBossKillCount)
		{
			m_fullPanelText.gameObject.SetActive(value: false);
			bool flag = m_runWins > 0;
			m_runNotCompletedPanel.SetActive(!flag);
			m_runCompletedPanel.SetActive(flag);
			if (!flag && m_runNotCompleteBossWinsHeader != null)
			{
				bool preferClassNameOverHeroName = SceneMgr.Get().IsInDuelsMode();
				m_runNotCompleteBossWinsHeader.Text = GameStrings.Format(m_bossWinsHeaderRunNotCompletedString, GetDisplayableClassName(preferClassNameOverHeroName));
			}
			else if (flag && m_runCompleteBossWinsHeader != null)
			{
				m_runCompleteBossWinsHeader.Text = GameStrings.Format(m_bossWinsHeaderRunCompletedString);
			}
		}
		else
		{
			m_runNotCompletedPanel.SetActive(value: false);
			m_runCompletedPanel.SetActive(value: false);
			m_fullPanelText.gameObject.SetActive(value: true);
			ScenarioDbId missionToPlay = m_dungeonCrawlData.GetMissionToPlay();
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord((int)missionToPlay);
			if (record != null)
			{
				m_fullPanelText.Text = record.Description;
			}
		}
	}

	private string GetDisplayableClassName(bool preferClassNameOverHeroName)
	{
		string className = GameStrings.GetClassName(m_heroClass);
		if (preferClassNameOverHeroName)
		{
			return className;
		}
		AdventureDbId currentAdventure = m_dungeonCrawlData.GetSelectedAdventure();
		List<AdventureGuestHeroesDbfRecord> records = GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)currentAdventure);
		List<CardDbfRecord> list = new List<CardDbfRecord>();
		foreach (AdventureGuestHeroesDbfRecord item in records)
		{
			list.Add(GameDbf.Card.GetRecord(GameUtils.GetCardIdFromGuestHeroDbId(item.GuestHeroId)));
		}
		foreach (CardDbfRecord cardRecord in list)
		{
			if (GameUtils.GetTagClassFromCardDbId(cardRecord.ID) == m_heroClass)
			{
				GuestHeroDbfRecord record = GameDbf.GuestHero.GetRecord((GuestHeroDbfRecord r) => r.CardId == cardRecord.ID);
				if (record != null)
				{
					return record.Name;
				}
			}
		}
		return className;
	}

	private void SetBossKillCounterVisualStyle()
	{
		DungeonRunVisualStyle visualStyle = m_dungeonCrawlData.VisualStyle;
		BossKillCounterStyleOverride[] bossKillCounterStyle = m_bossKillCounterStyle;
		foreach (BossKillCounterStyleOverride bossKillCounterStyleOverride in bossKillCounterStyle)
		{
			if (visualStyle != bossKillCounterStyleOverride.VisualStyle)
			{
				continue;
			}
			m_bossWinsHeaderRunNotCompletedString = bossKillCounterStyleOverride.BossWinsRunNotCompletedString;
			m_bossWinsHeaderRunCompletedString = bossKillCounterStyleOverride.BossWinsRunCompletedString;
			m_runCompleteRunWinsHeader.Text = bossKillCounterStyleOverride.RunWinsString;
			GameObject[] numberHolderShadow = m_numberHolderShadow;
			for (int j = 0; j < numberHolderShadow.Length; j++)
			{
				MeshRenderer component = numberHolderShadow[j].GetComponent<MeshRenderer>();
				if (component != null && bossKillCounterStyleOverride.NumberHolderShadowMaterial != null)
				{
					component.SetMaterial(bossKillCounterStyleOverride.NumberHolderShadowMaterial);
				}
			}
			m_runNotCompleteBossWinsHeader.TextColor = bossKillCounterStyleOverride.DescriptionTextColor;
			m_runCompleteBossWinsHeader.TextColor = bossKillCounterStyleOverride.DescriptionTextColor;
			m_runCompleteRunWinsHeader.TextColor = bossKillCounterStyleOverride.DescriptionTextColor;
			m_fullPanelText.TextColor = bossKillCounterStyleOverride.DescriptionTextColor;
			break;
		}
	}
}
