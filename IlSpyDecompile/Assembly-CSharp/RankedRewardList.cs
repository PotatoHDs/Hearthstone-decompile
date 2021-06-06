using System.Collections;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

[RequireComponent(typeof(WidgetTemplate))]
public class RankedRewardList : MonoBehaviour
{
	public UberText m_cardBackProgressText;

	public UIBScrollable m_scrollable;

	public List<GameObject> m_sections;

	private Widget m_widget;

	private int m_currentRankSectionIndex;

	private bool IsReady
	{
		get
		{
			if (m_widget != null && m_widget.IsReady)
			{
				return !m_widget.IsChangingStates;
			}
			return false;
		}
	}

	private void Awake()
	{
		m_widget = GetComponent<WidgetTemplate>();
	}

	public void Initialize(MedalInfoTranslator mit)
	{
		if (mit == null)
		{
			return;
		}
		bool isTooltipEnabled = false;
		bool hasEarnedCardBack = mit.HasEarnedSeasonCardBack();
		RankedPlayDataModel rankedPlayDataModel = mit.CreateDataModel(mit.GetBestCurrentRankFormatType(), RankedMedal.DisplayMode.Default, isTooltipEnabled, hasEarnedCardBack);
		m_widget.BindDataModel(rankedPlayDataModel);
		m_cardBackProgressText.Text = GameStrings.Format("GLUE_RANKED_REWARD_LIST_CARDBACK_PROGRESS", mit.GetSeasonCardBackWinsRemaining());
		int currentSeasonId = mit.GetCurrentSeasonId();
		CardBackDataModel dataModel = new CardBackDataModel
		{
			CardBackId = RankMgr.Get().GetRankedCardBackIdForSeasonId(currentSeasonId)
		};
		m_widget.BindDataModel(dataModel);
		PackDataModel dataModel2 = new PackDataModel
		{
			Type = (BoosterDbId)RankMgr.Get().GetRankedRewardBoosterIdForSeasonId(currentSeasonId),
			Quantity = 1
		};
		m_widget.BindDataModel(dataModel2);
		TranslatedMedalInfo currentMedal = mit.GetCurrentMedal(mit.GetBestCurrentRankFormatType());
		List<LeagueRankDbfRecord> ranks = currentMedal.LeagueConfig.Ranks;
		ranks.Sort((LeagueRankDbfRecord a, LeagueRankDbfRecord b) => a.StarLevel - b.StarLevel);
		m_currentRankSectionIndex = -1;
		RankedPlayListDataModel rankedPlayListDataModel = new RankedPlayListDataModel();
		foreach (LeagueRankDbfRecord item2 in ranks)
		{
			if (item2.RewardBagId != 0)
			{
				RankedPlayDataModel item = MedalInfoTranslator.CreateTranslatedMedalInfo(currentMedal.format, currentMedal.leagueId, item2.StarLevel, 0).CreateDataModel(RankedMedal.DisplayMode.Chest);
				rankedPlayListDataModel.Items.Add(item);
				if (rankedPlayDataModel.StarLevel >= item2.StarLevel)
				{
					m_currentRankSectionIndex++;
				}
			}
		}
		m_widget.BindDataModel(rankedPlayListDataModel);
	}

	private void OnPlayMakerPopupIntroFinished()
	{
		StartCoroutine(ScrollToSectionWhenReady());
	}

	private IEnumerator ScrollToSectionWhenReady()
	{
		if (m_currentRankSectionIndex >= 0 && m_currentRankSectionIndex < m_sections.Count)
		{
			while (!IsReady)
			{
				yield return null;
			}
			m_scrollable.CenterObjectInView(m_sections[m_currentRankSectionIndex], 0f, null, iTween.EaseType.linear, 0f);
		}
	}
}
