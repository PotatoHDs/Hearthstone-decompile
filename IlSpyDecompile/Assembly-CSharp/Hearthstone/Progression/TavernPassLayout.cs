using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	public class TavernPassLayout : MonoBehaviour
	{
		private Widget m_widget;

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			m_widget.RegisterReadyListener(delegate
			{
				OnWidgetReady();
			});
		}

		private void OnWidgetReady()
		{
			RewardTrackDbfRecord rewardTrackAsset = RewardTrackManager.Get().RewardTrackAsset;
			if (rewardTrackAsset != null)
			{
				m_widget.BindDataModel(RewardTrackManager.Get().TrackDataModel);
				m_widget.BindDataModel(RewardTrackFactory.CreatePaidRewardListDataModel(rewardTrackAsset));
			}
		}
	}
}
