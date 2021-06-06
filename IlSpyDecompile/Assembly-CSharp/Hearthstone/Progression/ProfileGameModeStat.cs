using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	public class ProfileGameModeStat : MonoBehaviour
	{
		private Widget m_widget;

		private TooltipZone m_tooltipZone;

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			m_widget.RegisterEventListener(WidgetEventListener);
			m_tooltipZone = GetComponent<TooltipZone>();
		}

		private ProfileGameModeStatDataModel GetGameModeStatDataModel()
		{
			IDataModel model = null;
			m_widget.GetDataModel(214, out model);
			return model as ProfileGameModeStatDataModel;
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
			ProfileGameModeStatDataModel gameModeStatDataModel = GetGameModeStatDataModel();
			if (gameModeStatDataModel != null)
			{
				m_tooltipZone.ShowLayerTooltip(gameModeStatDataModel.StatName, gameModeStatDataModel.StatDesc);
			}
		}

		private void OnRollOut()
		{
			m_tooltipZone.HideTooltip();
		}
	}
}
