using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class RandomCardDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 125;

		private TAG_RARITY m_Rarity;

		private TAG_PREMIUM m_Premium;

		private int m_Count;

		private DataModelProperty[] m_properties;

		public int DataModelId => 125;

		public string DataModelDisplayName => "random_card";

		public TAG_RARITY Rarity
		{
			get
			{
				return m_Rarity;
			}
			set
			{
				if (m_Rarity != value)
				{
					m_Rarity = value;
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

		public DataModelProperty[] Properties => m_properties;

		public RandomCardDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[3];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "rarity",
				Type = typeof(TAG_RARITY)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "premium",
				Type = typeof(TAG_PREMIUM)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "count",
				Type = typeof(int)
			};
			array[2] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_Rarity;
			int num2 = (num + m_Rarity.GetHashCode()) * 31;
			_ = m_Premium;
			int num3 = (num2 + m_Premium.GetHashCode()) * 31;
			_ = m_Count;
			return num3 + m_Count.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_Rarity;
				return true;
			case 1:
				value = m_Premium;
				return true;
			case 2:
				value = m_Count;
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
				Rarity = ((value != null) ? ((TAG_RARITY)value) : TAG_RARITY.INVALID);
				return true;
			case 1:
				Premium = ((value != null) ? ((TAG_PREMIUM)value) : TAG_PREMIUM.NORMAL);
				return true;
			case 2:
				Count = ((value != null) ? ((int)value) : 0);
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
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
