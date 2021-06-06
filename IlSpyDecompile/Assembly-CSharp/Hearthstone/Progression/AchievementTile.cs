using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	public class AchievementTile : MonoBehaviour
	{
		private Widget m_widget;

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			m_widget.RegisterDoneChangingStatesListener(delegate
			{
				if (ProgressUtils.ShowDebugIds)
				{
					m_widget.TriggerEvent("DEBUG_SHOW_ID");
				}
			}, null, callImmediatelyIfSet: true, doOnce: true);
		}

		public void BindDataModel(AchievementDataModel achievement)
		{
			m_widget.BindDataModel(achievement);
		}
	}
}
