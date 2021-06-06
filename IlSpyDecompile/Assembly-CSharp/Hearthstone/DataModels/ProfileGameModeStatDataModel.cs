using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class ProfileGameModeStatDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 214;

		private string m_ModeName;

		private int m_ModeIcon;

		private string m_StatName;

		private DataModelList<int> m_StatValue = new DataModelList<int>();

		private string m_StatDesc;

		private DataModelProperty[] m_properties;

		public int DataModelId => 214;

		public string DataModelDisplayName => "profile_game_mode_stat";

		public string ModeName
		{
			get
			{
				return m_ModeName;
			}
			set
			{
				if (!(m_ModeName == value))
				{
					m_ModeName = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int ModeIcon
		{
			get
			{
				return m_ModeIcon;
			}
			set
			{
				if (m_ModeIcon != value)
				{
					m_ModeIcon = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string StatName
		{
			get
			{
				return m_StatName;
			}
			set
			{
				if (!(m_StatName == value))
				{
					m_StatName = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<int> StatValue
		{
			get
			{
				return m_StatValue;
			}
			set
			{
				if (m_StatValue != value)
				{
					RemoveNestedDataModel(m_StatValue);
					RegisterNestedDataModel(value);
					m_StatValue = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string StatDesc
		{
			get
			{
				return m_StatDesc;
			}
			set
			{
				if (!(m_StatDesc == value))
				{
					m_StatDesc = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public ProfileGameModeStatDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[5];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "mode_name",
				Type = typeof(string)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "mode_icon",
				Type = typeof(int)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "stat_name",
				Type = typeof(string)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "stat_value",
				Type = typeof(DataModelList<int>)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "stat_desc",
				Type = typeof(string)
			};
			array[4] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_StatValue);
		}

		public int GetPropertiesHashCode()
		{
			int num = (17 * 31 + ((m_ModeName != null) ? m_ModeName.GetHashCode() : 0)) * 31;
			_ = m_ModeIcon;
			return (((num + m_ModeIcon.GetHashCode()) * 31 + ((m_StatName != null) ? m_StatName.GetHashCode() : 0)) * 31 + ((m_StatValue != null) ? m_StatValue.GetPropertiesHashCode() : 0)) * 31 + ((m_StatDesc != null) ? m_StatDesc.GetHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_ModeName;
				return true;
			case 1:
				value = m_ModeIcon;
				return true;
			case 2:
				value = m_StatName;
				return true;
			case 3:
				value = m_StatValue;
				return true;
			case 4:
				value = m_StatDesc;
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
				ModeName = ((value != null) ? ((string)value) : null);
				return true;
			case 1:
				ModeIcon = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				StatName = ((value != null) ? ((string)value) : null);
				return true;
			case 3:
				StatValue = ((value != null) ? ((DataModelList<int>)value) : null);
				return true;
			case 4:
				StatDesc = ((value != null) ? ((string)value) : null);
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
