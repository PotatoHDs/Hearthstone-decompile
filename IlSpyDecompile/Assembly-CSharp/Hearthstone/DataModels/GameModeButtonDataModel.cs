using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class GameModeButtonDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 172;

		private string m_Name;

		private string m_Description;

		private string m_ButtonState;

		private int m_GameModeRecordId;

		private bool m_IsNew;

		private bool m_IsEarlyAccess;

		private bool m_IsBeta;

		private DataModelProperty[] m_properties;

		public int DataModelId => 172;

		public string DataModelDisplayName => "gamemodebutton";

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

		public string Description
		{
			get
			{
				return m_Description;
			}
			set
			{
				if (!(m_Description == value))
				{
					m_Description = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string ButtonState
		{
			get
			{
				return m_ButtonState;
			}
			set
			{
				if (!(m_ButtonState == value))
				{
					m_ButtonState = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int GameModeRecordId
		{
			get
			{
				return m_GameModeRecordId;
			}
			set
			{
				if (m_GameModeRecordId != value)
				{
					m_GameModeRecordId = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsNew
		{
			get
			{
				return m_IsNew;
			}
			set
			{
				if (m_IsNew != value)
				{
					m_IsNew = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsEarlyAccess
		{
			get
			{
				return m_IsEarlyAccess;
			}
			set
			{
				if (m_IsEarlyAccess != value)
				{
					m_IsEarlyAccess = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool IsBeta
		{
			get
			{
				return m_IsBeta;
			}
			set
			{
				if (m_IsBeta != value)
				{
					m_IsBeta = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public GameModeButtonDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[7];
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
				PropertyDisplayName = "description",
				Type = typeof(string)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "buttonstate",
				Type = typeof(string)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "gamemoderecordid",
				Type = typeof(int)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "isnew",
				Type = typeof(bool)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 234,
				PropertyDisplayName = "isearlyaccess",
				Type = typeof(bool)
			};
			array[5] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "isbeta",
				Type = typeof(bool)
			};
			array[6] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			int num = (((17 * 31 + ((m_Name != null) ? m_Name.GetHashCode() : 0)) * 31 + ((m_Description != null) ? m_Description.GetHashCode() : 0)) * 31 + ((m_ButtonState != null) ? m_ButtonState.GetHashCode() : 0)) * 31;
			_ = m_GameModeRecordId;
			int num2 = (num + m_GameModeRecordId.GetHashCode()) * 31;
			_ = m_IsNew;
			int num3 = (num2 + m_IsNew.GetHashCode()) * 31;
			_ = m_IsEarlyAccess;
			int num4 = (num3 + m_IsEarlyAccess.GetHashCode()) * 31;
			_ = m_IsBeta;
			return num4 + m_IsBeta.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_Name;
				return true;
			case 1:
				value = m_Description;
				return true;
			case 2:
				value = m_ButtonState;
				return true;
			case 3:
				value = m_GameModeRecordId;
				return true;
			case 4:
				value = m_IsNew;
				return true;
			case 234:
				value = m_IsEarlyAccess;
				return true;
			case 5:
				value = m_IsBeta;
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
				Description = ((value != null) ? ((string)value) : null);
				return true;
			case 2:
				ButtonState = ((value != null) ? ((string)value) : null);
				return true;
			case 3:
				GameModeRecordId = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				IsNew = value != null && (bool)value;
				return true;
			case 234:
				IsEarlyAccess = value != null && (bool)value;
				return true;
			case 5:
				IsBeta = value != null && (bool)value;
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
			case 234:
				info = Properties[5];
				return true;
			case 5:
				info = Properties[6];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
