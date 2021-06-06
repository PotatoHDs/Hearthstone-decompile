using System;
using System.Collections.Generic;
using Hearthstone.DungeonCrawl;
using UnityEngine;

// Token: 0x02000062 RID: 98
public class DungeonCrawlBossKillCounter : MonoBehaviour
{
	// Token: 0x0600058D RID: 1421 RVA: 0x00020162 File Offset: 0x0001E362
	private void Awake()
	{
		this.m_runNotCompletedPanel.SetActive(false);
		this.m_runCompletedPanel.SetActive(false);
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x0002017C File Offset: 0x0001E37C
	public void SetDungeonRunData(IDungeonCrawlData data)
	{
		this.m_dungeonCrawlData = data;
		this.SetBossKillCounterVisualStyle();
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x0002018B File Offset: 0x0001E38B
	public void SetHeroClass(TAG_CLASS heroClass)
	{
		this.m_heroClass = heroClass;
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x00020194 File Offset: 0x0001E394
	public void SetBossWins(long bossWins)
	{
		this.m_bossWins = bossWins;
		UberText[] bossWinsText = this.m_bossWinsText;
		for (int i = 0; i < bossWinsText.Length; i++)
		{
			bossWinsText[i].Text = this.m_bossWins.ToString();
		}
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x000201D0 File Offset: 0x0001E3D0
	public void SetRunWins(long runWins)
	{
		this.m_runWins = runWins;
		this.m_runWinsText.Text = this.m_runWins.ToString();
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x000201F0 File Offset: 0x0001E3F0
	public void UpdateLayout()
	{
		AdventureDataDbfRecord selectedAdventureDataRecord = this.m_dungeonCrawlData.GetSelectedAdventureDataRecord();
		if (selectedAdventureDataRecord != null && selectedAdventureDataRecord.DungeonCrawlShowBossKillCount)
		{
			this.m_fullPanelText.gameObject.SetActive(false);
			bool flag = this.m_runWins > 0L;
			this.m_runNotCompletedPanel.SetActive(!flag);
			this.m_runCompletedPanel.SetActive(flag);
			if (!flag && this.m_runNotCompleteBossWinsHeader != null)
			{
				bool preferClassNameOverHeroName = SceneMgr.Get().IsInDuelsMode();
				this.m_runNotCompleteBossWinsHeader.Text = GameStrings.Format(this.m_bossWinsHeaderRunNotCompletedString, new object[]
				{
					this.GetDisplayableClassName(preferClassNameOverHeroName)
				});
				return;
			}
			if (flag && this.m_runCompleteBossWinsHeader != null)
			{
				this.m_runCompleteBossWinsHeader.Text = GameStrings.Format(this.m_bossWinsHeaderRunCompletedString, Array.Empty<object>());
				return;
			}
		}
		else
		{
			this.m_runNotCompletedPanel.SetActive(false);
			this.m_runCompletedPanel.SetActive(false);
			this.m_fullPanelText.gameObject.SetActive(true);
			ScenarioDbId missionToPlay = this.m_dungeonCrawlData.GetMissionToPlay();
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord((int)missionToPlay);
			if (record != null)
			{
				this.m_fullPanelText.Text = record.Description;
			}
		}
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x00020324 File Offset: 0x0001E524
	private string GetDisplayableClassName(bool preferClassNameOverHeroName)
	{
		string result = GameStrings.GetClassName(this.m_heroClass);
		if (preferClassNameOverHeroName)
		{
			return result;
		}
		AdventureDbId currentAdventure = this.m_dungeonCrawlData.GetSelectedAdventure();
		List<AdventureGuestHeroesDbfRecord> records = GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)currentAdventure, -1);
		List<CardDbfRecord> list = new List<CardDbfRecord>();
		foreach (AdventureGuestHeroesDbfRecord adventureGuestHeroesDbfRecord in records)
		{
			list.Add(GameDbf.Card.GetRecord(GameUtils.GetCardIdFromGuestHeroDbId(adventureGuestHeroesDbfRecord.GuestHeroId)));
		}
		using (List<CardDbfRecord>.Enumerator enumerator2 = list.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				CardDbfRecord cardRecord = enumerator2.Current;
				if (GameUtils.GetTagClassFromCardDbId(cardRecord.ID) == this.m_heroClass)
				{
					GuestHeroDbfRecord record = GameDbf.GuestHero.GetRecord((GuestHeroDbfRecord r) => r.CardId == cardRecord.ID);
					if (record != null)
					{
						result = record.Name;
						break;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x00020458 File Offset: 0x0001E658
	private void SetBossKillCounterVisualStyle()
	{
		DungeonRunVisualStyle visualStyle = this.m_dungeonCrawlData.VisualStyle;
		foreach (DungeonCrawlBossKillCounter.BossKillCounterStyleOverride bossKillCounterStyleOverride in this.m_bossKillCounterStyle)
		{
			if (visualStyle == bossKillCounterStyleOverride.VisualStyle)
			{
				this.m_bossWinsHeaderRunNotCompletedString = bossKillCounterStyleOverride.BossWinsRunNotCompletedString;
				this.m_bossWinsHeaderRunCompletedString = bossKillCounterStyleOverride.BossWinsRunCompletedString;
				this.m_runCompleteRunWinsHeader.Text = bossKillCounterStyleOverride.RunWinsString;
				GameObject[] numberHolderShadow = this.m_numberHolderShadow;
				for (int j = 0; j < numberHolderShadow.Length; j++)
				{
					MeshRenderer component = numberHolderShadow[j].GetComponent<MeshRenderer>();
					if (component != null && bossKillCounterStyleOverride.NumberHolderShadowMaterial != null)
					{
						component.SetMaterial(bossKillCounterStyleOverride.NumberHolderShadowMaterial);
					}
				}
				this.m_runNotCompleteBossWinsHeader.TextColor = bossKillCounterStyleOverride.DescriptionTextColor;
				this.m_runCompleteBossWinsHeader.TextColor = bossKillCounterStyleOverride.DescriptionTextColor;
				this.m_runCompleteRunWinsHeader.TextColor = bossKillCounterStyleOverride.DescriptionTextColor;
				this.m_fullPanelText.TextColor = bossKillCounterStyleOverride.DescriptionTextColor;
				return;
			}
		}
	}

	// Token: 0x040003E4 RID: 996
	public UberText[] m_bossWinsText;

	// Token: 0x040003E5 RID: 997
	public UberText m_runWinsText;

	// Token: 0x040003E6 RID: 998
	public UberText m_runNotCompleteBossWinsHeader;

	// Token: 0x040003E7 RID: 999
	public UberText m_runCompleteBossWinsHeader;

	// Token: 0x040003E8 RID: 1000
	public UberText m_runCompleteRunWinsHeader;

	// Token: 0x040003E9 RID: 1001
	public UberText m_fullPanelText;

	// Token: 0x040003EA RID: 1002
	public GameObject m_runNotCompletedPanel;

	// Token: 0x040003EB RID: 1003
	public GameObject m_runCompletedPanel;

	// Token: 0x040003EC RID: 1004
	public GameObject[] m_numberHolderShadow;

	// Token: 0x040003ED RID: 1005
	public DungeonCrawlBossKillCounter.BossKillCounterStyleOverride[] m_bossKillCounterStyle;

	// Token: 0x040003EE RID: 1006
	private long m_bossWins;

	// Token: 0x040003EF RID: 1007
	private long m_runWins;

	// Token: 0x040003F0 RID: 1008
	private TAG_CLASS m_heroClass;

	// Token: 0x040003F1 RID: 1009
	private string m_bossWinsHeaderRunNotCompletedString;

	// Token: 0x040003F2 RID: 1010
	private string m_bossWinsHeaderRunCompletedString;

	// Token: 0x040003F3 RID: 1011
	private IDungeonCrawlData m_dungeonCrawlData;

	// Token: 0x02001354 RID: 4948
	[Serializable]
	public class BossKillCounterStyleOverride
	{
		// Token: 0x0400A611 RID: 42513
		public DungeonRunVisualStyle VisualStyle;

		// Token: 0x0400A612 RID: 42514
		public string BossWinsRunNotCompletedString;

		// Token: 0x0400A613 RID: 42515
		public string BossWinsRunCompletedString;

		// Token: 0x0400A614 RID: 42516
		public string RunWinsString;

		// Token: 0x0400A615 RID: 42517
		public Material NumberHolderShadowMaterial;

		// Token: 0x0400A616 RID: 42518
		public Color DescriptionTextColor;
	}
}
