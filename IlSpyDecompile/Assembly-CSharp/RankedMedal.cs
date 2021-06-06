using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

[RequireComponent(typeof(WidgetTemplate))]
public class RankedMedal : MonoBehaviour
{
	public enum DisplayMode
	{
		Default,
		Stars,
		Chest
	}

	private TooltipZone m_tooltipZone;

	private Widget m_widget;

	public bool IsReady
	{
		get
		{
			if (m_widget == null || !m_widget.IsReady || m_widget.IsChangingStates)
			{
				return false;
			}
			RankedPlayDataModel rankedPlayDataModel = GetRankedPlayDataModel();
			if (rankedPlayDataModel == null)
			{
				return false;
			}
			if (rankedPlayDataModel.DisplayMode != DisplayMode.Chest && !rankedPlayDataModel.IsLegend && rankedPlayDataModel.MedalTexture == null && rankedPlayDataModel.MedalMaterial == null)
			{
				return false;
			}
			return true;
		}
	}

	private void Awake()
	{
		m_widget = GetComponent<WidgetTemplate>();
		m_widget.RegisterEventListener(WidgetEventListener);
		m_tooltipZone = GetComponent<TooltipZone>();
	}

	public void BindRankedPlayDataModel(RankedPlayDataModel dataModel)
	{
		if (dataModel != GetRankedPlayDataModel())
		{
			m_widget.BindDataModel(dataModel);
		}
	}

	private RankedPlayDataModel GetRankedPlayDataModel()
	{
		IDataModel model = null;
		m_widget.GetDataModel(123, out model);
		return model as RankedPlayDataModel;
	}

	private void WidgetEventListener(string eventName)
	{
		if (eventName.Equals("RollOver"))
		{
			OnRollOver();
		}
		else if (eventName.Equals("RollOut"))
		{
			OnRollOut();
		}
	}

	private void OnRollOver()
	{
		RankedPlayDataModel rankedPlayDataModel = GetRankedPlayDataModel();
		if (rankedPlayDataModel != null && rankedPlayDataModel.IsTooltipEnabled)
		{
			string bodytext = "";
			string headline = "";
			if (Options.Get().GetBool(Option.IN_RANKED_PLAY_MODE) || SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
			{
				bodytext = (rankedPlayDataModel.IsLegend ? GameStrings.Format("GLOBAL_MEDAL_TOOLTIP_BODY_LEGEND") : ((!new Map<FormatType, string>
				{
					{
						FormatType.FT_STANDARD,
						"GLOBAL_MEDAL_TOOLTIP_BODY_STANDARD"
					},
					{
						FormatType.FT_WILD,
						"GLOBAL_MEDAL_TOOLTIP_BODY_WILD"
					},
					{
						FormatType.FT_CLASSIC,
						"GLOBAL_MEDAL_TOOLTIP_BODY_CLASSIC"
					}
				}.TryGetValue(rankedPlayDataModel.FormatType, out var value)) ? ("UNKNOWN FORMAT TYPE " + rankedPlayDataModel.FormatType) : GameStrings.Format(value)));
				headline = ((!new Map<FormatType, string>
				{
					{
						FormatType.FT_STANDARD,
						"GLOBAL_MEDAL_TOOLTIP_BEST_RANK_STANDARD"
					},
					{
						FormatType.FT_WILD,
						"GLOBAL_MEDAL_TOOLTIP_BEST_RANK_WILD"
					},
					{
						FormatType.FT_CLASSIC,
						"GLOBAL_MEDAL_TOOLTIP_BEST_RANK_CLASSIC"
					}
				}.TryGetValue(rankedPlayDataModel.FormatType, out var value2)) ? ("UNKNOWN FORMAT TYPE " + rankedPlayDataModel.FormatType) : GameStrings.Format(value2, rankedPlayDataModel.RankName));
			}
			m_tooltipZone.ShowLayerTooltip(headline, bodytext);
			TooltipPanel tooltipPanel = m_tooltipZone.GetTooltipPanel();
			if ((bool)tooltipPanel)
			{
				tooltipPanel.m_name.WordWrap = false;
				tooltipPanel.m_name.Cache = false;
				tooltipPanel.m_name.UpdateNow();
			}
		}
	}

	private void OnRollOut()
	{
		m_tooltipZone.HideTooltip();
	}
}
