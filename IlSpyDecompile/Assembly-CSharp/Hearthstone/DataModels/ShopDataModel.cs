using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class ShopDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 24;

		private bool m_IsWild;

		private DataModelList<ProductTierDataModel> m_Tiers = new DataModelList<ProductTierDataModel>();

		private ProductDataModel m_VirtualCurrency;

		private ProductDataModel m_BoosterCurrency;

		private bool m_AutoconvertCurrency;

		private PriceDataModel m_VirtualCurrencyBalance;

		private PriceDataModel m_BoosterCurrencyBalance;

		private PriceDataModel m_GoldBalance;

		private PriceDataModel m_DustBalance;

		private bool m_HasNewItems;

		private int m_TavernTicketBalance;

		private DataModelProperty[] m_properties;

		public int DataModelId => 24;

		public string DataModelDisplayName => "shop";

		public bool IsWild
		{
			get
			{
				return m_IsWild;
			}
			set
			{
				if (m_IsWild != value)
				{
					m_IsWild = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<ProductTierDataModel> Tiers
		{
			get
			{
				return m_Tiers;
			}
			set
			{
				if (m_Tiers != value)
				{
					RemoveNestedDataModel(m_Tiers);
					RegisterNestedDataModel(value);
					m_Tiers = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public ProductDataModel VirtualCurrency
		{
			get
			{
				return m_VirtualCurrency;
			}
			set
			{
				if (m_VirtualCurrency != value)
				{
					RemoveNestedDataModel(m_VirtualCurrency);
					RegisterNestedDataModel(value);
					m_VirtualCurrency = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public ProductDataModel BoosterCurrency
		{
			get
			{
				return m_BoosterCurrency;
			}
			set
			{
				if (m_BoosterCurrency != value)
				{
					RemoveNestedDataModel(m_BoosterCurrency);
					RegisterNestedDataModel(value);
					m_BoosterCurrency = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool AutoconvertCurrency
		{
			get
			{
				return m_AutoconvertCurrency;
			}
			set
			{
				if (m_AutoconvertCurrency != value)
				{
					m_AutoconvertCurrency = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public PriceDataModel VirtualCurrencyBalance
		{
			get
			{
				return m_VirtualCurrencyBalance;
			}
			set
			{
				if (m_VirtualCurrencyBalance != value)
				{
					RemoveNestedDataModel(m_VirtualCurrencyBalance);
					RegisterNestedDataModel(value);
					m_VirtualCurrencyBalance = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public PriceDataModel BoosterCurrencyBalance
		{
			get
			{
				return m_BoosterCurrencyBalance;
			}
			set
			{
				if (m_BoosterCurrencyBalance != value)
				{
					RemoveNestedDataModel(m_BoosterCurrencyBalance);
					RegisterNestedDataModel(value);
					m_BoosterCurrencyBalance = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public PriceDataModel GoldBalance
		{
			get
			{
				return m_GoldBalance;
			}
			set
			{
				if (m_GoldBalance != value)
				{
					RemoveNestedDataModel(m_GoldBalance);
					RegisterNestedDataModel(value);
					m_GoldBalance = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public PriceDataModel DustBalance
		{
			get
			{
				return m_DustBalance;
			}
			set
			{
				if (m_DustBalance != value)
				{
					RemoveNestedDataModel(m_DustBalance);
					RegisterNestedDataModel(value);
					m_DustBalance = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public bool HasNewItems
		{
			get
			{
				return m_HasNewItems;
			}
			set
			{
				if (m_HasNewItems != value)
				{
					m_HasNewItems = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int TavernTicketBalance
		{
			get
			{
				return m_TavernTicketBalance;
			}
			set
			{
				if (m_TavernTicketBalance != value)
				{
					m_TavernTicketBalance = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public ShopDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[11];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "is_wild",
				Type = typeof(bool)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "tiers",
				Type = typeof(DataModelList<ProductTierDataModel>)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "virtual_currency",
				Type = typeof(ProductDataModel)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "booster_currency",
				Type = typeof(ProductDataModel)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "autoconvert_currency",
				Type = typeof(bool)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "virtual_currency_balance",
				Type = typeof(PriceDataModel)
			};
			array[5] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "booster_currency_balance",
				Type = typeof(PriceDataModel)
			};
			array[6] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 8,
				PropertyDisplayName = "gold_balance",
				Type = typeof(PriceDataModel)
			};
			array[7] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 9,
				PropertyDisplayName = "dust_balance",
				Type = typeof(PriceDataModel)
			};
			array[8] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 10,
				PropertyDisplayName = "has_new_items",
				Type = typeof(bool)
			};
			array[9] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 11,
				PropertyDisplayName = "tavern_ticket_balance",
				Type = typeof(int)
			};
			array[10] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_Tiers);
			RegisterNestedDataModel(m_VirtualCurrency);
			RegisterNestedDataModel(m_BoosterCurrency);
			RegisterNestedDataModel(m_VirtualCurrencyBalance);
			RegisterNestedDataModel(m_BoosterCurrencyBalance);
			RegisterNestedDataModel(m_GoldBalance);
			RegisterNestedDataModel(m_DustBalance);
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_IsWild;
			int num2 = ((((num + m_IsWild.GetHashCode()) * 31 + ((m_Tiers != null) ? m_Tiers.GetPropertiesHashCode() : 0)) * 31 + ((m_VirtualCurrency != null) ? m_VirtualCurrency.GetPropertiesHashCode() : 0)) * 31 + ((m_BoosterCurrency != null) ? m_BoosterCurrency.GetPropertiesHashCode() : 0)) * 31;
			_ = m_AutoconvertCurrency;
			int num3 = (((((num2 + m_AutoconvertCurrency.GetHashCode()) * 31 + ((m_VirtualCurrencyBalance != null) ? m_VirtualCurrencyBalance.GetPropertiesHashCode() : 0)) * 31 + ((m_BoosterCurrencyBalance != null) ? m_BoosterCurrencyBalance.GetPropertiesHashCode() : 0)) * 31 + ((m_GoldBalance != null) ? m_GoldBalance.GetPropertiesHashCode() : 0)) * 31 + ((m_DustBalance != null) ? m_DustBalance.GetPropertiesHashCode() : 0)) * 31;
			_ = m_HasNewItems;
			int num4 = (num3 + m_HasNewItems.GetHashCode()) * 31;
			_ = m_TavernTicketBalance;
			return num4 + m_TavernTicketBalance.GetHashCode();
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_IsWild;
				return true;
			case 1:
				value = m_Tiers;
				return true;
			case 3:
				value = m_VirtualCurrency;
				return true;
			case 4:
				value = m_BoosterCurrency;
				return true;
			case 5:
				value = m_AutoconvertCurrency;
				return true;
			case 6:
				value = m_VirtualCurrencyBalance;
				return true;
			case 7:
				value = m_BoosterCurrencyBalance;
				return true;
			case 8:
				value = m_GoldBalance;
				return true;
			case 9:
				value = m_DustBalance;
				return true;
			case 10:
				value = m_HasNewItems;
				return true;
			case 11:
				value = m_TavernTicketBalance;
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
				IsWild = value != null && (bool)value;
				return true;
			case 1:
				Tiers = ((value != null) ? ((DataModelList<ProductTierDataModel>)value) : null);
				return true;
			case 3:
				VirtualCurrency = ((value != null) ? ((ProductDataModel)value) : null);
				return true;
			case 4:
				BoosterCurrency = ((value != null) ? ((ProductDataModel)value) : null);
				return true;
			case 5:
				AutoconvertCurrency = value != null && (bool)value;
				return true;
			case 6:
				VirtualCurrencyBalance = ((value != null) ? ((PriceDataModel)value) : null);
				return true;
			case 7:
				BoosterCurrencyBalance = ((value != null) ? ((PriceDataModel)value) : null);
				return true;
			case 8:
				GoldBalance = ((value != null) ? ((PriceDataModel)value) : null);
				return true;
			case 9:
				DustBalance = ((value != null) ? ((PriceDataModel)value) : null);
				return true;
			case 10:
				HasNewItems = value != null && (bool)value;
				return true;
			case 11:
				TavernTicketBalance = ((value != null) ? ((int)value) : 0);
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
			case 7:
				info = Properties[6];
				return true;
			case 8:
				info = Properties[7];
				return true;
			case 9:
				info = Properties[8];
				return true;
			case 10:
				info = Properties[9];
				return true;
			case 11:
				info = Properties[10];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
