using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class AchievementSectionDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 226;

		private string m_Name;

		private AchievementListDataModel m_Achievements;

		private int m_ID;

		private int m_Index;

		private int m_TileCount;

		private int m_PreviousTileCount;

		private DataModelProperty[] m_properties;

		public int DataModelId => 226;

		public string DataModelDisplayName => "achievement_section";

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

		public AchievementListDataModel Achievements
		{
			get
			{
				return m_Achievements;
			}
			set
			{
				if (m_Achievements != value)
				{
					RemoveNestedDataModel(m_Achievements);
					RegisterNestedDataModel(value);
					m_Achievements = value;
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

		public int Index
		{
			get
			{
				return m_Index;
			}
			set
			{
				if (m_Index != value)
				{
					m_Index = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int TileCount
		{
			get
			{
				return m_TileCount;
			}
			set
			{
				if (m_TileCount != value)
				{
					m_TileCount = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int PreviousTileCount
		{
			get
			{
				return m_PreviousTileCount;
			}
			set
			{
				if (m_PreviousTileCount != value)
				{
					m_PreviousTileCount = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public AchievementSectionDataModel()
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
				PropertyDisplayName = "achievements",
				Type = typeof(AchievementListDataModel)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "id",
				Type = typeof(int)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "index",
				Type = typeof(int)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "tile_count",
				Type = typeof(int)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "previous_tile_count",
				Type = typeof(int)
			};
			array[5] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_Achievements);
		}

		public int GetPropertiesHashCode()
		{
			int num = ((17 * 31 + ((m_Name != null) ? m_Name.GetHashCode() : 0)) * 31 + ((m_Achievements != null) ? m_Achievements.GetPropertiesHashCode() : 0)) * 31;
			_ = m_ID;
			int num2 = (num + m_ID.GetHashCode()) * 31;
			_ = m_Index;
			int num3 = (num2 + m_Index.GetHashCode()) * 31;
			_ = m_TileCount;
			int num4 = (num3 + m_TileCount.GetHashCode()) * 31;
			_ = m_PreviousTileCount;
			return num4 + m_PreviousTileCount.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_Name;
				return true;
			case 1:
				value = m_Achievements;
				return true;
			case 3:
				value = m_ID;
				return true;
			case 4:
				value = m_Index;
				return true;
			case 5:
				value = m_TileCount;
				return true;
			case 6:
				value = m_PreviousTileCount;
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
				Achievements = ((value != null) ? ((AchievementListDataModel)value) : null);
				return true;
			case 3:
				ID = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				Index = ((value != null) ? ((int)value) : 0);
				return true;
			case 5:
				TileCount = ((value != null) ? ((int)value) : 0);
				return true;
			case 6:
				PreviousTileCount = ((value != null) ? ((int)value) : 0);
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
			case 3:
				info = Properties[2];
				return true;
			case 4:
				info = Properties[3];
				return true;
			case 5:
				info = Properties[4];
				return true;
			case 6:
				info = Properties[5];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
