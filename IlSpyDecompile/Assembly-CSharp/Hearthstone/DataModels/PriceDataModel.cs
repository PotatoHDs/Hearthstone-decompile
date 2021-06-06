using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class PriceDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 29;

		private float m_Amount;

		private CurrencyType m_Currency;

		private string m_DisplayText;

		private DataModelProperty[] m_properties;

		public int DataModelId => 29;

		public string DataModelDisplayName => "price";

		public float Amount
		{
			get
			{
				return m_Amount;
			}
			set
			{
				if (m_Amount != value)
				{
					m_Amount = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public CurrencyType Currency
		{
			get
			{
				return m_Currency;
			}
			set
			{
				if (m_Currency != value)
				{
					m_Currency = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string DisplayText
		{
			get
			{
				return m_DisplayText;
			}
			set
			{
				if (!(m_DisplayText == value))
				{
					m_DisplayText = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public PriceDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[3];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "amount",
				Type = typeof(float)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "currency",
				Type = typeof(CurrencyType)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "display_text",
				Type = typeof(string)
			};
			array[2] = dataModelProperty;
			m_properties = array;
			base._002Ector();
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_Amount;
			int num2 = (num + m_Amount.GetHashCode()) * 31;
			_ = m_Currency;
			return (num2 + m_Currency.GetHashCode()) * 31 + ((m_DisplayText != null) ? m_DisplayText.GetHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_Amount;
				return true;
			case 1:
				value = m_Currency;
				return true;
			case 2:
				value = m_DisplayText;
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
				Amount = ((value != null) ? ((float)value) : 0f);
				return true;
			case 1:
				Currency = ((value != null) ? ((CurrencyType)value) : CurrencyType.NONE);
				return true;
			case 2:
				DisplayText = ((value != null) ? ((string)value) : null);
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
