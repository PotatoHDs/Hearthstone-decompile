using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x02001107 RID: 4359
	[RequireComponent(typeof(WidgetTemplate))]
	public class AchievementSubcategory : MonoBehaviour
	{
		// Token: 0x0600BF15 RID: 48917 RVA: 0x003A395E File Offset: 0x003A1B5E
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_widget.RegisterEventListener(new Widget.EventListenerDelegate(this.HandleEvent));
			this.m_sectionListWidget.BindDataModel(this.m_sections, false);
		}

		// Token: 0x0600BF16 RID: 48918 RVA: 0x003A3995 File Offset: 0x003A1B95
		private void HandleEvent(string eventName)
		{
			if (eventName == "CODE_SUBCATEGORY_CHANGED")
			{
				this.HandleSubcategoryChanged();
			}
		}

		// Token: 0x0600BF17 RID: 48919 RVA: 0x003A39AC File Offset: 0x003A1BAC
		private void HandleSubcategoryChanged()
		{
			AchievementCategoryDataModel dataModel = this.m_widget.GetDataModel<AchievementCategoryDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state: no bound category");
				return;
			}
			AchievementSubcategoryDataModel selectedSubcategory = dataModel.SelectedSubcategory;
			if (selectedSubcategory == null)
			{
				Debug.LogWarning("Unexpected state: no bound subcategory");
				return;
			}
			this.UpdateAchievementSections(selectedSubcategory);
		}

		// Token: 0x0600BF18 RID: 48920 RVA: 0x003A39EF File Offset: 0x003A1BEF
		private void UpdateAchievementSections(AchievementSubcategoryDataModel subcategory)
		{
			this.m_sections.Sections = subcategory.Sections.Sections;
		}

		// Token: 0x04009B3E RID: 39742
		public Widget m_sectionListWidget;

		// Token: 0x04009B3F RID: 39743
		private Widget m_widget;

		// Token: 0x04009B40 RID: 39744
		private readonly AchievementSectionListDataModel m_sections = new AchievementSectionListDataModel();
	}
}
