using System;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x02001125 RID: 4389
	public class TavernPassLayout : MonoBehaviour
	{
		// Token: 0x0600C073 RID: 49267 RVA: 0x003AA786 File Offset: 0x003A8986
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_widget.RegisterReadyListener(delegate(object _)
			{
				this.OnWidgetReady();
			}, null, true);
		}

		// Token: 0x0600C074 RID: 49268 RVA: 0x003AA7B0 File Offset: 0x003A89B0
		private void OnWidgetReady()
		{
			RewardTrackDbfRecord rewardTrackAsset = RewardTrackManager.Get().RewardTrackAsset;
			if (rewardTrackAsset == null)
			{
				return;
			}
			this.m_widget.BindDataModel(RewardTrackManager.Get().TrackDataModel, false);
			this.m_widget.BindDataModel(RewardTrackFactory.CreatePaidRewardListDataModel(rewardTrackAsset), false);
		}

		// Token: 0x04009BED RID: 39917
		private Widget m_widget;
	}
}
