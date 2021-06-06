using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x02001111 RID: 4369
	public class ProfileGameModeStat : MonoBehaviour
	{
		// Token: 0x0600BF54 RID: 48980 RVA: 0x003A471F File Offset: 0x003A291F
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_widget.RegisterEventListener(new Widget.EventListenerDelegate(this.WidgetEventListener));
			this.m_tooltipZone = base.GetComponent<TooltipZone>();
		}

		// Token: 0x0600BF55 RID: 48981 RVA: 0x003A4750 File Offset: 0x003A2950
		private ProfileGameModeStatDataModel GetGameModeStatDataModel()
		{
			IDataModel dataModel = null;
			this.m_widget.GetDataModel(214, out dataModel);
			return dataModel as ProfileGameModeStatDataModel;
		}

		// Token: 0x0600BF56 RID: 48982 RVA: 0x003A4778 File Offset: 0x003A2978
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

		// Token: 0x0600BF57 RID: 48983 RVA: 0x003A47A4 File Offset: 0x003A29A4
		private void OnRollOver()
		{
			ProfileGameModeStatDataModel gameModeStatDataModel = this.GetGameModeStatDataModel();
			if (gameModeStatDataModel == null)
			{
				return;
			}
			this.m_tooltipZone.ShowLayerTooltip(gameModeStatDataModel.StatName, gameModeStatDataModel.StatDesc, 0);
		}

		// Token: 0x0600BF58 RID: 48984 RVA: 0x003A47D5 File Offset: 0x003A29D5
		private void OnRollOut()
		{
			this.m_tooltipZone.HideTooltip();
		}

		// Token: 0x04009B65 RID: 39781
		private Widget m_widget;

		// Token: 0x04009B66 RID: 39782
		private TooltipZone m_tooltipZone;
	}
}
