using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	[RequireComponent(typeof(WidgetTemplate))]
	internal class AchievementPinTray : MonoBehaviour
	{
		public Listable[] columns;

		private Widget m_widget;

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			m_widget.RegisterReadyListener(delegate
			{
				BindAchievementCategories();
			});
		}

		private void BindAchievementCategories()
		{
			if (columns == null)
			{
				return;
			}
			AchievementCategoryListDataModel dataModel = m_widget.GetDataModel<AchievementCategoryListDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state - no bound achievement category list");
				return;
			}
			foreach (var item in columns.WithIndex())
			{
				var (listable, columnIndex) = item;
				listable.BindDataModel(new AchievementCategoryListDataModel
				{
					Categories = dataModel.Categories.Where((AchievementCategoryDataModel _, int index) => index % columns.Length == columnIndex).ToDataModelList()
				});
			}
		}
	}
}
