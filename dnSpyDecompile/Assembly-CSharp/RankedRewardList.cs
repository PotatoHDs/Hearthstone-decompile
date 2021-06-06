using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x0200064D RID: 1613
[RequireComponent(typeof(WidgetTemplate))]
public class RankedRewardList : MonoBehaviour
{
	// Token: 0x06005AF6 RID: 23286 RVA: 0x001DA8B7 File Offset: 0x001D8AB7
	private void Awake()
	{
		this.m_widget = base.GetComponent<WidgetTemplate>();
	}

	// Token: 0x06005AF7 RID: 23287 RVA: 0x001DA8C8 File Offset: 0x001D8AC8
	public void Initialize(MedalInfoTranslator mit)
	{
		if (mit == null)
		{
			return;
		}
		bool isTooltipEnabled = false;
		bool hasEarnedCardBack = mit.HasEarnedSeasonCardBack();
		RankedPlayDataModel rankedPlayDataModel = mit.CreateDataModel(mit.GetBestCurrentRankFormatType(), RankedMedal.DisplayMode.Default, isTooltipEnabled, hasEarnedCardBack, null);
		this.m_widget.BindDataModel(rankedPlayDataModel, false);
		this.m_cardBackProgressText.Text = GameStrings.Format("GLUE_RANKED_REWARD_LIST_CARDBACK_PROGRESS", new object[]
		{
			mit.GetSeasonCardBackWinsRemaining()
		});
		int currentSeasonId = mit.GetCurrentSeasonId();
		CardBackDataModel dataModel = new CardBackDataModel
		{
			CardBackId = RankMgr.Get().GetRankedCardBackIdForSeasonId(currentSeasonId)
		};
		this.m_widget.BindDataModel(dataModel, false);
		PackDataModel dataModel2 = new PackDataModel
		{
			Type = (BoosterDbId)RankMgr.Get().GetRankedRewardBoosterIdForSeasonId(currentSeasonId),
			Quantity = 1
		};
		this.m_widget.BindDataModel(dataModel2, false);
		TranslatedMedalInfo currentMedal = mit.GetCurrentMedal(mit.GetBestCurrentRankFormatType());
		List<LeagueRankDbfRecord> ranks = currentMedal.LeagueConfig.Ranks;
		ranks.Sort((LeagueRankDbfRecord a, LeagueRankDbfRecord b) => a.StarLevel - b.StarLevel);
		this.m_currentRankSectionIndex = -1;
		RankedPlayListDataModel rankedPlayListDataModel = new RankedPlayListDataModel();
		foreach (LeagueRankDbfRecord leagueRankDbfRecord in ranks)
		{
			if (leagueRankDbfRecord.RewardBagId != 0)
			{
				RankedPlayDataModel item = MedalInfoTranslator.CreateTranslatedMedalInfo(currentMedal.format, currentMedal.leagueId, leagueRankDbfRecord.StarLevel, 0).CreateDataModel(RankedMedal.DisplayMode.Chest, false, false, null);
				rankedPlayListDataModel.Items.Add(item);
				if (rankedPlayDataModel.StarLevel >= leagueRankDbfRecord.StarLevel)
				{
					this.m_currentRankSectionIndex++;
				}
			}
		}
		this.m_widget.BindDataModel(rankedPlayListDataModel, false);
	}

	// Token: 0x17000552 RID: 1362
	// (get) Token: 0x06005AF8 RID: 23288 RVA: 0x001DAA74 File Offset: 0x001D8C74
	private bool IsReady
	{
		get
		{
			return this.m_widget != null && this.m_widget.IsReady && !this.m_widget.IsChangingStates;
		}
	}

	// Token: 0x06005AF9 RID: 23289 RVA: 0x001DAAA1 File Offset: 0x001D8CA1
	private void OnPlayMakerPopupIntroFinished()
	{
		base.StartCoroutine(this.ScrollToSectionWhenReady());
	}

	// Token: 0x06005AFA RID: 23290 RVA: 0x001DAAB0 File Offset: 0x001D8CB0
	private IEnumerator ScrollToSectionWhenReady()
	{
		if (this.m_currentRankSectionIndex < 0 || this.m_currentRankSectionIndex >= this.m_sections.Count)
		{
			yield break;
		}
		while (!this.IsReady)
		{
			yield return null;
		}
		this.m_scrollable.CenterObjectInView(this.m_sections[this.m_currentRankSectionIndex], 0f, null, iTween.EaseType.linear, 0f, false);
		yield break;
	}

	// Token: 0x04004DB8 RID: 19896
	public UberText m_cardBackProgressText;

	// Token: 0x04004DB9 RID: 19897
	public UIBScrollable m_scrollable;

	// Token: 0x04004DBA RID: 19898
	public List<GameObject> m_sections;

	// Token: 0x04004DBB RID: 19899
	private Widget m_widget;

	// Token: 0x04004DBC RID: 19900
	private int m_currentRankSectionIndex;
}
