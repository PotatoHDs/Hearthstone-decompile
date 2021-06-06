using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class RewardItemDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 17;

		private long m_PmtLicenseId;

		private RewardItemType m_ItemType;

		private int m_ItemId;

		private int m_Quantity;

		private PackDataModel m_Booster;

		private CardBackDataModel m_CardBack;

		private PriceDataModel m_Currency;

		private int m_AssetId;

		private CardDataModel m_Card;

		private RandomCardDataModel m_RandomCard;

		private DataModelProperty[] m_properties;

		public int DataModelId => 17;

		public string DataModelDisplayName => "reward_item";

		public long PmtLicenseId
		{
			get
			{
				return m_PmtLicenseId;
			}
			set
			{
				if (m_PmtLicenseId != value)
				{
					m_PmtLicenseId = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public RewardItemType ItemType
		{
			get
			{
				return m_ItemType;
			}
			set
			{
				if (m_ItemType != value)
				{
					m_ItemType = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int ItemId
		{
			get
			{
				return m_ItemId;
			}
			set
			{
				if (m_ItemId != value)
				{
					m_ItemId = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int Quantity
		{
			get
			{
				return m_Quantity;
			}
			set
			{
				if (m_Quantity != value)
				{
					m_Quantity = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public PackDataModel Booster
		{
			get
			{
				return m_Booster;
			}
			set
			{
				if (m_Booster != value)
				{
					RemoveNestedDataModel(m_Booster);
					RegisterNestedDataModel(value);
					m_Booster = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public CardBackDataModel CardBack
		{
			get
			{
				return m_CardBack;
			}
			set
			{
				if (m_CardBack != value)
				{
					RemoveNestedDataModel(m_CardBack);
					RegisterNestedDataModel(value);
					m_CardBack = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public PriceDataModel Currency
		{
			get
			{
				return m_Currency;
			}
			set
			{
				if (m_Currency != value)
				{
					RemoveNestedDataModel(m_Currency);
					RegisterNestedDataModel(value);
					m_Currency = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public int AssetId
		{
			get
			{
				return m_AssetId;
			}
			set
			{
				if (m_AssetId != value)
				{
					m_AssetId = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public CardDataModel Card
		{
			get
			{
				return m_Card;
			}
			set
			{
				if (m_Card != value)
				{
					RemoveNestedDataModel(m_Card);
					RegisterNestedDataModel(value);
					m_Card = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public RandomCardDataModel RandomCard
		{
			get
			{
				return m_RandomCard;
			}
			set
			{
				if (m_RandomCard != value)
				{
					RemoveNestedDataModel(m_RandomCard);
					RegisterNestedDataModel(value);
					m_RandomCard = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public RewardItemDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[10];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "pmt_license_id",
				Type = typeof(long)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "item_type",
				Type = typeof(RewardItemType)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "item_id",
				Type = typeof(int)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "quantity",
				Type = typeof(int)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "booster",
				Type = typeof(PackDataModel)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "card_back",
				Type = typeof(CardBackDataModel)
			};
			array[5] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "currency",
				Type = typeof(PriceDataModel)
			};
			array[6] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "asset_id",
				Type = typeof(int)
			};
			array[7] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 110,
				PropertyDisplayName = "card",
				Type = typeof(CardDataModel)
			};
			array[8] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 111,
				PropertyDisplayName = "random_card",
				Type = typeof(RandomCardDataModel)
			};
			array[9] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_Booster);
			RegisterNestedDataModel(m_CardBack);
			RegisterNestedDataModel(m_Currency);
			RegisterNestedDataModel(m_Card);
			RegisterNestedDataModel(m_RandomCard);
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_PmtLicenseId;
			int num2 = (num + m_PmtLicenseId.GetHashCode()) * 31;
			_ = m_ItemType;
			int num3 = (num2 + m_ItemType.GetHashCode()) * 31;
			_ = m_ItemId;
			int num4 = (num3 + m_ItemId.GetHashCode()) * 31;
			_ = m_Quantity;
			int num5 = ((((num4 + m_Quantity.GetHashCode()) * 31 + ((m_Booster != null) ? m_Booster.GetPropertiesHashCode() : 0)) * 31 + ((m_CardBack != null) ? m_CardBack.GetPropertiesHashCode() : 0)) * 31 + ((m_Currency != null) ? m_Currency.GetPropertiesHashCode() : 0)) * 31;
			_ = m_AssetId;
			return ((num5 + m_AssetId.GetHashCode()) * 31 + ((m_Card != null) ? m_Card.GetPropertiesHashCode() : 0)) * 31 + ((m_RandomCard != null) ? m_RandomCard.GetPropertiesHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_PmtLicenseId;
				return true;
			case 1:
				value = m_ItemType;
				return true;
			case 2:
				value = m_ItemId;
				return true;
			case 3:
				value = m_Quantity;
				return true;
			case 4:
				value = m_Booster;
				return true;
			case 5:
				value = m_CardBack;
				return true;
			case 6:
				value = m_Currency;
				return true;
			case 7:
				value = m_AssetId;
				return true;
			case 110:
				value = m_Card;
				return true;
			case 111:
				value = m_RandomCard;
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
				PmtLicenseId = ((value != null) ? ((long)value) : 0);
				return true;
			case 1:
				ItemType = ((value != null) ? ((RewardItemType)value) : RewardItemType.UNDEFINED);
				return true;
			case 2:
				ItemId = ((value != null) ? ((int)value) : 0);
				return true;
			case 3:
				Quantity = ((value != null) ? ((int)value) : 0);
				return true;
			case 4:
				Booster = ((value != null) ? ((PackDataModel)value) : null);
				return true;
			case 5:
				CardBack = ((value != null) ? ((CardBackDataModel)value) : null);
				return true;
			case 6:
				Currency = ((value != null) ? ((PriceDataModel)value) : null);
				return true;
			case 7:
				AssetId = ((value != null) ? ((int)value) : 0);
				return true;
			case 110:
				Card = ((value != null) ? ((CardDataModel)value) : null);
				return true;
			case 111:
				RandomCard = ((value != null) ? ((RandomCardDataModel)value) : null);
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
			case 6:
				info = Properties[6];
				return true;
			case 7:
				info = Properties[7];
				return true;
			case 110:
				info = Properties[8];
				return true;
			case 111:
				info = Properties[9];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
