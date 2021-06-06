using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	[RequireComponent(typeof(WidgetTemplate))]
	public class AchievementSubcategory : MonoBehaviour
	{
		public Widget m_sectionListWidget;

		private Widget m_widget;

		private readonly AchievementSectionListDataModel m_sections = new AchievementSectionListDataModel();

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			m_widget.RegisterEventListener(HandleEvent);
			m_sectionListWidget.BindDataModel(m_sections);
		}

		private void HandleEvent(string eventName)
		{
			if (eventName == "CODE_SUBCATEGORY_CHANGED")
			{
				HandleSubcategoryChanged();
			}
		}

		private void HandleSubcategoryChanged()
		{
			AchievementCategoryDataModel dataModel = m_widget.GetDataModel<AchievementCategoryDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state: no bound category");
				return;
			}
			AchievementSubcategoryDataModel selectedSubcategory = dataModel.SelectedSubcategory;
			if (selectedSubcategory == null)
			{
				Debug.LogWarning("Unexpected state: no bound subcategory");
			}
			else
			{
				UpdateAchievementSections(selectedSubcategory);
			}
		}

		private void UpdateAchievementSections(AchievementSubcategoryDataModel subcategory)
		{
			m_sections.Sections = subcategory.Sections.Sections;
		}
	}
}
