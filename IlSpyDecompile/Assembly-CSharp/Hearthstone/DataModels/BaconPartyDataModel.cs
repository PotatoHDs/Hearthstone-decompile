using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class BaconPartyDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 154;

		private bool m_Active;

		private int m_Size;

		private int m_Ready;

		private bool m_PrivateGame;

		private DataModelProperty[] m_properties;

		public int DataModelId => 154;

		public string DataModelDisplayName => "baconparty";

		public bool Active
		{
			get
			{
				return m_Active;
			}
			set
			{
				if (m_Active != value)
				{
					m_Active = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Size
		{
			get
			{
				return m_Size;
			}
			set
			{
				if (m_Size != value)
				{
					m_Size = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Ready
		{
			get
			{
				return m_Ready;
			}
			set
			{
				if (m_Ready != value)
				{
					m_Ready = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool PrivateGame
		{
			get
			{
				return m_PrivateGame;
			}
			set
			{
				if (m_PrivateGame != value)
				{
					m_PrivateGame = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public BaconPartyDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[4];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "active",
				Type = typeof(bool)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "size",
				Type = typeof(int)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "ready",
				Type = typeof(int)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "private_game",
				Type = typeof(bool)
			};
			array[3] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_Active;
			int num2 = (num + m_Active.GetHashCode()) * 31;
			_ = m_Size;
			int num3 = (num2 + m_Size.GetHashCode()) * 31;
			_ = m_Ready;
			int num4 = (num3 + m_Ready.GetHashCode()) * 31;
			_ = m_PrivateGame;
			return num4 + m_PrivateGame.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_Active;
				return true;
			case 1:
				value = m_Size;
				return true;
			case 2:
				value = m_Ready;
				return true;
			case 3:
				value = m_PrivateGame;
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
				Active = value != null && (bool)value;
				return true;
			case 1:
				Size = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				Ready = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				PrivateGame = value != null && (bool)value;
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
