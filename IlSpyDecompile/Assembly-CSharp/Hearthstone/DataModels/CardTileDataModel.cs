using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class CardTileDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 262;

		private string m_CardId;

		private TAG_PREMIUM m_Premium;

		private int m_Count;

		private bool m_Selected;

		private DataModelProperty[] m_properties;

		public int DataModelId => 262;

		public string DataModelDisplayName => "card_tile";

		public string CardId
		{
			get
			{
				return m_CardId;
			}
			set
			{
				if (!(m_CardId == value))
				{
					m_CardId = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public TAG_PREMIUM Premium
		{
			get
			{
				return m_Premium;
			}
			set
			{
				if (m_Premium != value)
				{
					m_Premium = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Count
		{
			get
			{
				return m_Count;
			}
			set
			{
				if (m_Count != value)
				{
					m_Count = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool Selected
		{
			get
			{
				return m_Selected;
			}
			set
			{
				if (m_Selected != value)
				{
					m_Selected = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public CardTileDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[4];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 263,
				PropertyDisplayName = "id",
				Type = typeof(string)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 264,
				PropertyDisplayName = "premium",
				Type = typeof(TAG_PREMIUM)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 265,
				PropertyDisplayName = "count",
				Type = typeof(int)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 277,
				PropertyDisplayName = "selected",
				Type = typeof(bool)
			};
			array[3] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			int num = (17 * 31 + ((m_CardId != null) ? m_CardId.GetHashCode() : 0)) * 31;
			_ = m_Premium;
			int num2 = (num + m_Premium.GetHashCode()) * 31;
			_ = m_Count;
			int num3 = (num2 + m_Count.GetHashCode()) * 31;
			_ = m_Selected;
			return num3 + m_Selected.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 263:
				value = m_CardId;
				return true;
			case 264:
				value = m_Premium;
				return true;
			case 265:
				value = m_Count;
				return true;
			case 277:
				value = m_Selected;
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
			case 263:
				CardId = ((value != null) ? ((string)value) : null);
				return true;
			case 264:
				Premium = ((value != null) ? ((TAG_PREMIUM)value) : TAG_PREMIUM.NORMAL);
				return true;
			case 265:
				Count = ((value != null) ? ((int)value) : 0);
				return true;
			case 277:
				Selected = value != null && (bool)value;
				return true;
			default:
				return false;
			}
		}

		public bool GetPropertyInfo(int id, out DataModelProperty info)
		{
			switch (id)
			{
			case 263:
				info = Properties[0];
				return true;
			case 264:
				info = Properties[1];
				return true;
			case 265:
				info = Properties[2];
				return true;
			case 277:
				info = Properties[3];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
