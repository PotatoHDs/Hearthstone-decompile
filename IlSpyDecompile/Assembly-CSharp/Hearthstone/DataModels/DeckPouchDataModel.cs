using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class DeckPouchDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 289;

		private AdventureLoadoutOptionDataModel m_Pouch;

		private int m_RemainingDust;

		private int m_TotalDust;

		private DeckDetailsDataModel m_Details;

		private TAG_CLASS m_Class;

		private DataModelProperty[] m_properties;

		public int DataModelId => 289;

		public string DataModelDisplayName => "deck_pouch";

		public AdventureLoadoutOptionDataModel Pouch
		{
			get
			{
				return m_Pouch;
			}
			set
			{
				if (m_Pouch != value)
				{
					RemoveNestedDataModel(m_Pouch);
					RegisterNestedDataModel(value);
					m_Pouch = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int RemainingDust
		{
			get
			{
				return m_RemainingDust;
			}
			set
			{
				if (m_RemainingDust != value)
				{
					m_RemainingDust = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int TotalDust
		{
			get
			{
				return m_TotalDust;
			}
			set
			{
				if (m_TotalDust != value)
				{
					m_TotalDust = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DeckDetailsDataModel Details
		{
			get
			{
				return m_Details;
			}
			set
			{
				if (m_Details != value)
				{
					RemoveNestedDataModel(m_Details);
					RegisterNestedDataModel(value);
					m_Details = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public TAG_CLASS Class
		{
			get
			{
				return m_Class;
			}
			set
			{
				if (m_Class != value)
				{
					m_Class = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public DeckPouchDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[5];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "pouch",
				Type = typeof(AdventureLoadoutOptionDataModel)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "remaining_dust",
				Type = typeof(int)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "total_dust",
				Type = typeof(int)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "details",
				Type = typeof(DeckDetailsDataModel)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "class",
				Type = typeof(TAG_CLASS)
			};
			array[4] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_Pouch);
			RegisterNestedDataModel(m_Details);
		}

		public int GetPropertiesHashCode()
		{
			int num = (17 * 31 + ((m_Pouch != null) ? m_Pouch.GetPropertiesHashCode() : 0)) * 31;
			_ = m_RemainingDust;
			int num2 = (num + m_RemainingDust.GetHashCode()) * 31;
			_ = m_TotalDust;
			int num3 = ((num2 + m_TotalDust.GetHashCode()) * 31 + ((m_Details != null) ? m_Details.GetPropertiesHashCode() : 0)) * 31;
			_ = m_Class;
			return num3 + m_Class.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_Pouch;
				return true;
			case 1:
				value = m_RemainingDust;
				return true;
			case 2:
				value = m_TotalDust;
				return true;
			case 3:
				value = m_Details;
				return true;
			case 4:
				value = m_Class;
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
				Pouch = ((value != null) ? ((AdventureLoadoutOptionDataModel)value) : null);
				return true;
			case 1:
				RemainingDust = ((value != null) ? ((int)value) : 0);
				return true;
			case 2:
				TotalDust = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				Details = ((value != null) ? ((DeckDetailsDataModel)value) : null);
				return true;
			case 4:
				Class = ((value != null) ? ((TAG_CLASS)value) : TAG_CLASS.INVALID);
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
