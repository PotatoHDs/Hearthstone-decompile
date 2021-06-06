using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

// Token: 0x02000649 RID: 1609
[RequireComponent(typeof(WidgetTemplate))]
public class RankedMedal : MonoBehaviour
{
	// Token: 0x06005ABF RID: 23231 RVA: 0x001D9BB9 File Offset: 0x001D7DB9
	private void Awake()
	{
		this.m_widget = base.GetComponent<WidgetTemplate>();
		this.m_widget.RegisterEventListener(new Widget.EventListenerDelegate(this.WidgetEventListener));
		this.m_tooltipZone = base.GetComponent<TooltipZone>();
	}

	// Token: 0x1700054E RID: 1358
	// (get) Token: 0x06005AC0 RID: 23232 RVA: 0x001D9BEC File Offset: 0x001D7DEC
	public bool IsReady
	{
		get
		{
			if (this.m_widget == null || !this.m_widget.IsReady || this.m_widget.IsChangingStates)
			{
				return false;
			}
			RankedPlayDataModel rankedPlayDataModel = this.GetRankedPlayDataModel();
			return rankedPlayDataModel != null && (rankedPlayDataModel.DisplayMode == RankedMedal.DisplayMode.Chest || rankedPlayDataModel.IsLegend || !(rankedPlayDataModel.MedalTexture == null) || !(rankedPlayDataModel.MedalMaterial == null));
		}
	}

	// Token: 0x06005AC1 RID: 23233 RVA: 0x001D9C5F File Offset: 0x001D7E5F
	public void BindRankedPlayDataModel(RankedPlayDataModel dataModel)
	{
		if (dataModel != this.GetRankedPlayDataModel())
		{
			this.m_widget.BindDataModel(dataModel, false);
		}
	}

	// Token: 0x06005AC2 RID: 23234 RVA: 0x001D9C78 File Offset: 0x001D7E78
	private RankedPlayDataModel GetRankedPlayDataModel()
	{
		IDataModel dataModel = null;
		this.m_widget.GetDataModel(123, out dataModel);
		return dataModel as RankedPlayDataModel;
	}

	// Token: 0x06005AC3 RID: 23235 RVA: 0x001D9C9D File Offset: 0x001D7E9D
	private void WidgetEventListener(string eventName)
	{
		if (eventName.Equals("RollOver"))
		{
			this.OnRollOver();
			return;
		}
		if (eventName.Equals("RollOut"))
		{
			this.OnRollOut();
		}
	}

	// Token: 0x06005AC4 RID: 23236 RVA: 0x001D9CC8 File Offset: 0x001D7EC8
	private void OnRollOver()
	{
		RankedPlayDataModel rankedPlayDataModel = this.GetRankedPlayDataModel();
		if (rankedPlayDataModel == null || !rankedPlayDataModel.IsTooltipEnabled)
		{
			return;
		}
		string bodytext = "";
		string headline = "";
		if (Options.Get().GetBool(Option.IN_RANKED_PLAY_MODE) || SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
		{
			string key;
			if (rankedPlayDataModel.IsLegend)
			{
				bodytext = GameStrings.Format("GLOBAL_MEDAL_TOOLTIP_BODY_LEGEND", Array.Empty<object>());
			}
			else if (new Map<FormatType, string>
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
			}.TryGetValue(rankedPlayDataModel.FormatType, out key))
			{
				bodytext = GameStrings.Format(key, Array.Empty<object>());
			}
			else
			{
				bodytext = "UNKNOWN FORMAT TYPE " + rankedPlayDataModel.FormatType.ToString();
			}
			string key2;
			if (new Map<FormatType, string>
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
			}.TryGetValue(rankedPlayDataModel.FormatType, out key2))
			{
				headline = GameStrings.Format(key2, new object[]
				{
					rankedPlayDataModel.RankName
				});
			}
			else
			{
				headline = "UNKNOWN FORMAT TYPE " + rankedPlayDataModel.FormatType.ToString();
			}
		}
		this.m_tooltipZone.ShowLayerTooltip(headline, bodytext, 0);
		TooltipPanel tooltipPanel = this.m_tooltipZone.GetTooltipPanel(0);
		if (!tooltipPanel)
		{
			return;
		}
		tooltipPanel.m_name.WordWrap = false;
		tooltipPanel.m_name.Cache = false;
		tooltipPanel.m_name.UpdateNow(false);
	}

	// Token: 0x06005AC5 RID: 23237 RVA: 0x001D9E50 File Offset: 0x001D8050
	private void OnRollOut()
	{
		this.m_tooltipZone.HideTooltip();
	}

	// Token: 0x04004D9A RID: 19866
	private TooltipZone m_tooltipZone;

	// Token: 0x04004D9B RID: 19867
	private Widget m_widget;

	// Token: 0x0200215A RID: 8538
	public enum DisplayMode
	{
		// Token: 0x0400DFFE RID: 57342
		Default,
		// Token: 0x0400DFFF RID: 57343
		Stars,
		// Token: 0x0400E000 RID: 57344
		Chest
	}
}
