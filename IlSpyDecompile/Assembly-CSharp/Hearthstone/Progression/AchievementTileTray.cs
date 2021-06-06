using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	[RequireComponent(typeof(WidgetTemplate))]
	public class AchievementTileTray : MonoBehaviour
	{
		private Widget m_widget;

		private bool m_initialized;

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			m_widget.RegisterEventListener(HandleEvent);
		}

		private void HandleEvent(string eventName)
		{
			if (!(eventName == "CODE_CATEGORY_CHANGED"))
			{
				if (eventName == "CODE_SUBCATEGORY_SELECTED")
				{
					HandleSelectedSubcategoryChanged();
				}
			}
			else
			{
				HandleCategoryChanged();
			}
		}

		private void HandleSelectedSubcategoryChanged()
		{
			AchievementCategoryDataModel dataModel = m_widget.GetDataModel<AchievementCategoryDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state: no bound category");
				return;
			}
			AchievementSubcategoryDataModel achievementSubcategoryDataModel = m_widget.GetDataModel<EventDataModel>()?.Payload as AchievementSubcategoryDataModel;
			if (achievementSubcategoryDataModel == null)
			{
				Debug.LogWarning("Unexpected state: no subcategory payload");
			}
			else
			{
				AchievementManager.Get().SelectSubcategory(dataModel, achievementSubcategoryDataModel);
			}
		}

		private void HandleCategoryChanged()
		{
			AchievementCategoryDataModel dataModel = m_widget.GetDataModel<AchievementCategoryDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state: no bound category");
				return;
			}
			if (!m_initialized)
			{
				dataModel.SelectedSubcategory = null;
				m_initialized = true;
			}
			AchievementManager.Get().SelectSubcategory(dataModel, dataModel.Subcategories.Subcategories.First());
		}
	}
}
