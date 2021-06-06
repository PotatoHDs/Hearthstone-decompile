using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x02001108 RID: 4360
	public class AchievementTile : MonoBehaviour
	{
		// Token: 0x0600BF1A RID: 48922 RVA: 0x003A3A1A File Offset: 0x003A1C1A
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_widget.RegisterDoneChangingStatesListener(delegate(object _)
			{
				if (ProgressUtils.ShowDebugIds)
				{
					this.m_widget.TriggerEvent("DEBUG_SHOW_ID", default(Widget.TriggerEventParameters));
				}
			}, null, true, true);
		}

		// Token: 0x0600BF1B RID: 48923 RVA: 0x003A3A42 File Offset: 0x003A1C42
		public void BindDataModel(AchievementDataModel achievement)
		{
			this.m_widget.BindDataModel(achievement, false);
		}

		// Token: 0x04009B41 RID: 39745
		private Widget m_widget;
	}
}
