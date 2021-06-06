using System;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x02001104 RID: 4356
	[RequireComponent(typeof(WidgetTemplate))]
	internal class AchievementPinTray : MonoBehaviour
	{
		// Token: 0x0600BEDC RID: 48860 RVA: 0x003A2E72 File Offset: 0x003A1072
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_widget.RegisterReadyListener(delegate(object _)
			{
				this.BindAchievementCategories();
			}, null, true);
		}

		// Token: 0x0600BEDD RID: 48861 RVA: 0x003A2E9C File Offset: 0x003A109C
		private void BindAchievementCategories()
		{
			if (this.columns == null)
			{
				return;
			}
			AchievementCategoryListDataModel dataModel = this.m_widget.GetDataModel<AchievementCategoryListDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state - no bound achievement category list");
				return;
			}
			foreach (ValueTuple<Listable, int> valueTuple in this.columns.WithIndex<Listable>())
			{
				WidgetBehavior item = valueTuple.Item1;
				int columnIndex = valueTuple.Item2;
				item.BindDataModel(new AchievementCategoryListDataModel
				{
					Categories = dataModel.Categories.Where((AchievementCategoryDataModel _, int index) => index % this.columns.Length == columnIndex).ToDataModelList<AchievementCategoryDataModel>()
				}, true, false);
			}
		}

		// Token: 0x04009B25 RID: 39717
		public Listable[] columns;

		// Token: 0x04009B26 RID: 39718
		private Widget m_widget;
	}
}
