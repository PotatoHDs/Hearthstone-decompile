using System;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x02001109 RID: 4361
	[RequireComponent(typeof(WidgetTemplate))]
	public class AchievementTileTray : MonoBehaviour
	{
		// Token: 0x0600BF1E RID: 48926 RVA: 0x003A3A82 File Offset: 0x003A1C82
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_widget.RegisterEventListener(new Widget.EventListenerDelegate(this.HandleEvent));
		}

		// Token: 0x0600BF1F RID: 48927 RVA: 0x003A3AA7 File Offset: 0x003A1CA7
		private void HandleEvent(string eventName)
		{
			if (eventName == "CODE_CATEGORY_CHANGED")
			{
				this.HandleCategoryChanged();
				return;
			}
			if (!(eventName == "CODE_SUBCATEGORY_SELECTED"))
			{
				return;
			}
			this.HandleSelectedSubcategoryChanged();
		}

		// Token: 0x0600BF20 RID: 48928 RVA: 0x003A3AD4 File Offset: 0x003A1CD4
		private void HandleSelectedSubcategoryChanged()
		{
			AchievementCategoryDataModel dataModel = this.m_widget.GetDataModel<AchievementCategoryDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state: no bound category");
				return;
			}
			EventDataModel dataModel2 = this.m_widget.GetDataModel<EventDataModel>();
			AchievementSubcategoryDataModel achievementSubcategoryDataModel = ((dataModel2 != null) ? dataModel2.Payload : null) as AchievementSubcategoryDataModel;
			if (achievementSubcategoryDataModel == null)
			{
				Debug.LogWarning("Unexpected state: no subcategory payload");
				return;
			}
			AchievementManager.Get().SelectSubcategory(dataModel, achievementSubcategoryDataModel);
		}

		// Token: 0x0600BF21 RID: 48929 RVA: 0x003A3B34 File Offset: 0x003A1D34
		private void HandleCategoryChanged()
		{
			AchievementCategoryDataModel dataModel = this.m_widget.GetDataModel<AchievementCategoryDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state: no bound category");
				return;
			}
			if (!this.m_initialized)
			{
				dataModel.SelectedSubcategory = null;
				this.m_initialized = true;
			}
			AchievementManager.Get().SelectSubcategory(dataModel, dataModel.Subcategories.Subcategories.First<AchievementSubcategoryDataModel>());
		}

		// Token: 0x04009B42 RID: 39746
		private Widget m_widget;

		// Token: 0x04009B43 RID: 39747
		private bool m_initialized;
	}
}
