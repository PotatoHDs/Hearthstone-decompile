using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class AchievementSubcategoryDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 227;

		private string m_Name;

		private string m_FullName;

		private string m_Icon;

		private AchievementSectionListDataModel m_Sections;

		private AchievementStatsDataModel m_Stats;

		private int m_ID;

		private DataModelProperty[] m_properties;

		public int DataModelId => 227;

		public string DataModelDisplayName => "achievement_subcategory";

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

		public string FullName
		{
			get
			{
				return m_FullName;
			}
			set
			{
				if (!(m_FullName == value))
				{
					m_FullName = value;
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

		public AchievementSectionListDataModel Sections
		{
			get
			{
				return m_Sections;
			}
			set
			{
				if (m_Sections != value)
				{
					RemoveNestedDataModel(m_Sections);
					RegisterNestedDataModel(value);
					m_Sections = value;
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

		public AchievementSubcategoryDataModel()
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
				PropertyDisplayName = "full_name",
				Type = typeof(string)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "icon",
				Type = typeof(string)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "sections",
				Type = typeof(AchievementSectionListDataModel)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "stats",
				Type = typeof(AchievementStatsDataModel)
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
			RegisterNestedDataModel(m_Sections);
			RegisterNestedDataModel(m_Stats);
		}

		public int GetPropertiesHashCode()
		{
			int num = (((((17 * 31 + ((m_Name != null) ? m_Name.GetHashCode() : 0)) * 31 + ((m_FullName != null) ? m_FullName.GetHashCode() : 0)) * 31 + ((m_Icon != null) ? m_Icon.GetHashCode() : 0)) * 31 + ((m_Sections != null) ? m_Sections.GetPropertiesHashCode() : 0)) * 31 + ((m_Stats != null) ? m_Stats.GetPropertiesHashCode() : 0)) * 31;
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
				value = m_FullName;
				return true;
			case 2:
				value = m_Icon;
				return true;
			case 3:
				value = m_Sections;
				return true;
			case 4:
				value = m_Stats;
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
				FullName = ((value != null) ? ((string)value) : null);
				return true;
			case 2:
				Icon = ((value != null) ? ((string)value) : null);
				return true;
			case 3:
				Sections = ((value != null) ? ((AchievementSectionListDataModel)value) : null);
				return true;
			case 4:
				Stats = ((value != null) ? ((AchievementStatsDataModel)value) : null);
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
