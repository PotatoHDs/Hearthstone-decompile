using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class AchievementCategoryDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 225;

		private string m_Name;

		private string m_Icon;

		private AchievementSubcategoryListDataModel m_Subcategories;

		private AchievementStatsDataModel m_Stats;

		private AchievementSubcategoryDataModel m_SelectedSubcategory;

		private int m_ID;

		private DataModelProperty[] m_properties;

		public int DataModelId => 225;

		public string DataModelDisplayName => "achievement_category";

		public string Name
		{
			get
			{
				return m_Name;
			}
			set
			{
				if (!(m_Name == value))
				{
					m_Name = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string Icon
		{
			get
			{
				return m_Icon;
			}
			set
			{
				if (!(m_Icon == value))
				{
					m_Icon = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public AchievementSubcategoryListDataModel Subcategories
		{
			get
			{
				return m_Subcategories;
			}
			set
			{
				if (m_Subcategories != value)
				{
					RemoveNestedDataModel(m_Subcategories);
					RegisterNestedDataModel(value);
					m_Subcategories = value;
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

		public AchievementSubcategoryDataModel SelectedSubcategory
		{
			get
			{
				return m_SelectedSubcategory;
			}
			set
			{
				if (m_SelectedSubcategory != value)
				{
					RemoveNestedDataModel(m_SelectedSubcategory);
					RegisterNestedDataModel(value);
					m_SelectedSubcategory = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int ID
		{
			get
			{
				return m_ID;
			}
			set
			{
				if (m_ID != value)
				{
					m_ID = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public AchievementCategoryDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[6];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "name",
				Type = typeof(string)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "icon",
				Type = typeof(string)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "subcategories",
				Type = typeof(AchievementSubcategoryListDataModel)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "stats",
				Type = typeof(AchievementStatsDataModel)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "selected_subcategory",
				Type = typeof(AchievementSubcategoryDataModel)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "id",
				Type = typeof(int)
			};
			array[5] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_Subcategories);
			RegisterNestedDataModel(m_Stats);
			RegisterNestedDataModel(m_SelectedSubcategory);
		}

		public int GetPropertiesHashCode()
		{
			int num = (((((17 * 31 + ((m_Name != null) ? m_Name.GetHashCode() : 0)) * 31 + ((m_Icon != null) ? m_Icon.GetHashCode() : 0)) * 31 + ((m_Subcategories != null) ? m_Subcategories.GetPropertiesHashCode() : 0)) * 31 + ((m_Stats != null) ? m_Stats.GetPropertiesHashCode() : 0)) * 31 + ((m_SelectedSubcategory != null) ? m_SelectedSubcategory.GetPropertiesHashCode() : 0)) * 31;
			_ = m_ID;
			return num + m_ID.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_Name;
				return true;
			case 1:
				value = m_Icon;
				return true;
			case 2:
				value = m_Subcategories;
				return true;
			case 3:
				value = m_Stats;
				return true;
			case 4:
				value = m_SelectedSubcategory;
				return true;
			case 5:
				value = m_ID;
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
				Name = ((value != null) ? ((string)value) : null);
				return true;
			case 1:
				Icon = ((value != null) ? ((string)value) : null);
				return true;
			case 2:
				Subcategories = ((value != null) ? ((AchievementSubcategoryListDataModel)value) : null);
				return true;
			case 3:
				Stats = ((value != null) ? ((AchievementStatsDataModel)value) : null);
				return true;
			case 4:
				SelectedSubcategory = ((value != null) ? ((AchievementSubcategoryDataModel)value) : null);
				return true;
			case 5:
				ID = ((value != null) ? ((int)value) : 0);
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
			case 2:
				info = Properties[2];
				return true;
			case 3:
				info = Properties[3];
				return true;
			case 4:
				info = Properties[4];
				return true;
			case 5:
				info = Properties[5];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
