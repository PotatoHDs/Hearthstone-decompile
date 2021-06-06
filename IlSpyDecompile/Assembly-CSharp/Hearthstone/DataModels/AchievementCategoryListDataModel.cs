using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class AchievementCategoryListDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 239;

		private DataModelList<AchievementCategoryDataModel> m_Categories = new DataModelList<AchievementCategoryDataModel>();

		private AchievementStatsDataModel m_Stats;

		private DataModelProperty[] m_properties;

		public int DataModelId => 239;

		public string DataModelDisplayName => "achievement_category_list";

		public DataModelList<AchievementCategoryDataModel> Categories
		{
			get
			{
				return m_Categories;
			}
			set
			{
				if (m_Categories != value)
				{
					RemoveNestedDataModel(m_Categories);
					RegisterNestedDataModel(value);
					m_Categories = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public AchievementStatsDataModel Stats
		{
			get
			{
				return m_Stats;
			}
			set
			{
				if (m_Stats != value)
				{
					RemoveNestedDataModel(m_Stats);
					RegisterNestedDataModel(value);
					m_Stats = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public AchievementCategoryListDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[2];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "categories",
				Type = typeof(DataModelList<AchievementCategoryDataModel>)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "stats",
				Type = typeof(AchievementStatsDataModel)
			};
			array[1] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_Categories);
			RegisterNestedDataModel(m_Stats);
		}

		public int GetPropertiesHashCode()
		{
			return (17 * 31 + ((m_Categories != null) ? m_Categories.GetPropertiesHashCode() : 0)) * 31 + ((m_Stats != null) ? m_Stats.GetPropertiesHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_Categories;
				return true;
			case 1:
				value = m_Stats;
				return true;
			default:
				value = null;
				return false;
			}
		}

		public bool SetPropertyValue(int id, object value)
		{
			switch (id)
			{
			case 0:
				Categories = ((value != null) ? ((DataModelList<AchievementCategoryDataModel>)value) : null);
				return true;
			case 1:
				Stats = ((value != null) ? ((AchievementStatsDataModel)value) : null);
				return true;
			default:
				return false;
			}
		}

		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			switch (id)
			{
			case 0:
				info = Properties[0];
				return true;
			case 1:
				info = Properties[1];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
