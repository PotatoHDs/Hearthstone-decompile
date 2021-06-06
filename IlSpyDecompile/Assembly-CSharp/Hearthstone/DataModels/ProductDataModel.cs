using Hearthstone.UI;

namespace Hearthstone.DataModels
{
	public class ProductDataModel : DataModelEventDispatcher, IDataModel, IDataModelProperties
	{
		public const int ModelId = 15;

		private long m_PmtId;

		private string m_Name;

		private string m_Description;

		private DataModelList<string> m_Tags = new DataModelList<string>();

		private DataModelList<RewardItemDataModel> m_Items = new DataModelList<RewardItemDataModel>();

		private DataModelList<PriceDataModel> m_Prices = new DataModelList<PriceDataModel>();

		private DataModelList<ProductDataModel> m_Variants = new DataModelList<ProductDataModel>();

		private ProductAvailability m_Availability;

		private RewardListDataModel m_RewardList;

		private string m_DescriptionHeader;

		private string m_VariantName;

		private string m_FlavorText;

		private DataModelProperty[] m_properties;

		public int DataModelId => 15;

		public string DataModelDisplayName => "product";

		public long PmtId
		{
			get
			{
				return m_PmtId;
			}
			set
			{
				if (m_PmtId != value)
				{
					m_PmtId = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

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

		public DataModelList<string> Tags
		{
			get
			{
				return m_Tags;
			}
			set
			{
				if (m_Tags != value)
				{
					RemoveNestedDataModel(m_Tags);
					RegisterNestedDataModel(value);
					m_Tags = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<RewardItemDataModel> Items
		{
			get
			{
				return m_Items;
			}
			set
			{
				if (m_Items != value)
				{
					RemoveNestedDataModel(m_Items);
					RegisterNestedDataModel(value);
					m_Items = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<PriceDataModel> Prices
		{
			get
			{
				return m_Prices;
			}
			set
			{
				if (m_Prices != value)
				{
					RemoveNestedDataModel(m_Prices);
					RegisterNestedDataModel(value);
					m_Prices = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelList<ProductDataModel> Variants
		{
			get
			{
				return m_Variants;
			}
			set
			{
				if (m_Variants != value)
				{
					RemoveNestedDataModel(m_Variants);
					RegisterNestedDataModel(value);
					m_Variants = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public ProductAvailability Availability
		{
			get
			{
				return m_Availability;
			}
			set
			{
				if (m_Availability != value)
				{
					m_Availability = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public RewardListDataModel RewardList
		{
			get
			{
				return m_RewardList;
			}
			set
			{
				if (m_RewardList != value)
				{
					RemoveNestedDataModel(m_RewardList);
					RegisterNestedDataModel(value);
					m_RewardList = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string DescriptionHeader
		{
			get
			{
				return m_DescriptionHeader;
			}
			set
			{
				if (!(m_DescriptionHeader == value))
				{
					m_DescriptionHeader = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string VariantName
		{
			get
			{
				return m_VariantName;
			}
			set
			{
				if (!(m_VariantName == value))
				{
					m_VariantName = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public string FlavorText
		{
			get
			{
				return m_FlavorText;
			}
			set
			{
				if (!(m_FlavorText == value))
				{
					m_FlavorText = value;
					DispatchChangedListeners();
					DataContext.DataVersion++;
				}
			}
		}

		public DataModelProperty[] Properties => m_properties;

		public ProductDataModel()
		{
			DataModelProperty[] array = new DataModelProperty[12];
			DataModelProperty dataModelProperty = new DataModelProperty
			{
				PropertyId = 0,
				PropertyDisplayName = "pmt_id",
				Type = typeof(long)
			};
			array[0] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 1,
				PropertyDisplayName = "name",
				Type = typeof(string)
			};
			array[1] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 2,
				PropertyDisplayName = "description",
				Type = typeof(string)
			};
			array[2] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 3,
				PropertyDisplayName = "tags",
				Type = typeof(DataModelList<string>)
			};
			array[3] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 4,
				PropertyDisplayName = "items",
				Type = typeof(DataModelList<RewardItemDataModel>)
			};
			array[4] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 5,
				PropertyDisplayName = "prices",
				Type = typeof(DataModelList<PriceDataModel>)
			};
			array[5] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 6,
				PropertyDisplayName = "variants",
				Type = typeof(DataModelList<ProductDataModel>)
			};
			array[6] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 7,
				PropertyDisplayName = "availability",
				Type = typeof(ProductAvailability)
			};
			array[7] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 36,
				PropertyDisplayName = "reward_list",
				Type = typeof(RewardListDataModel)
			};
			array[8] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 38,
				PropertyDisplayName = "description_header",
				Type = typeof(string)
			};
			array[9] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 39,
				PropertyDisplayName = "variant_name",
				Type = typeof(string)
			};
			array[10] = dataModelProperty;
			dataModelProperty = new DataModelProperty
			{
				PropertyId = 40,
				PropertyDisplayName = "flavor_text",
				Type = typeof(string)
			};
			array[11] = dataModelProperty;
			m_properties = array;
			base._002Ector();
			RegisterNestedDataModel(m_Tags);
			RegisterNestedDataModel(m_Items);
			RegisterNestedDataModel(m_Prices);
			RegisterNestedDataModel(m_Variants);
			RegisterNestedDataModel(m_RewardList);
		}

		public int GetPropertiesHashCode()
		{
			int num = 17 * 31;
			_ = m_PmtId;
			int num2 = (((((((num + m_PmtId.GetHashCode()) * 31 + ((m_Name != null) ? m_Name.GetHashCode() : 0)) * 31 + ((m_Description != null) ? m_Description.GetHashCode() : 0)) * 31 + ((m_Tags != null) ? m_Tags.GetPropertiesHashCode() : 0)) * 31 + ((m_Items != null) ? m_Items.GetPropertiesHashCode() : 0)) * 31 + ((m_Prices != null) ? m_Prices.GetPropertiesHashCode() : 0)) * 31 + ((m_Variants != null) ? m_Variants.GetPropertiesHashCode() : 0)) * 31;
			_ = m_Availability;
			return ((((num2 + m_Availability.GetHashCode()) * 31 + ((m_RewardList != null) ? m_RewardList.GetPropertiesHashCode() : 0)) * 31 + ((m_DescriptionHeader != null) ? m_DescriptionHeader.GetHashCode() : 0)) * 31 + ((m_VariantName != null) ? m_VariantName.GetHashCode() : 0)) * 31 + ((m_FlavorText != null) ? m_FlavorText.GetHashCode() : 0);
		}

		public bool GetPropertyValue(int id, out object value)
		{
			switch (id)
			{
			case 0:
				value = m_PmtId;
				return true;
			case 1:
				value = m_Name;
				return true;
			case 2:
				value = m_Description;
				return true;
			case 3:
				value = m_Tags;
				return true;
			case 4:
				value = m_Items;
				return true;
			case 5:
				value = m_Prices;
				return true;
			case 6:
				value = m_Variants;
				return true;
			case 7:
				value = m_Availability;
				return true;
			case 36:
				value = m_RewardList;
				return true;
			case 38:
				value = m_DescriptionHeader;
				return true;
			case 39:
				value = m_VariantName;
				return true;
			case 40:
				value = m_FlavorText;
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
				PmtId = ((value != null) ? ((long)value) : 0);
				return true;
			case 1:
				Name = ((value != null) ? ((string)value) : null);
				return true;
			case 2:
				Description = ((value != null) ? ((string)value) : null);
				return true;
			case 3:
				Tags = ((value != null) ? ((DataModelList<string>)value) : null);
				return true;
			case 4:
				Items = ((value != null) ? ((DataModelList<RewardItemDataModel>)value) : null);
				return true;
			case 5:
				Prices = ((value != null) ? ((DataModelList<PriceDataModel>)value) : null);
				return true;
			case 6:
				Variants = ((value != null) ? ((DataModelList<ProductDataModel>)value) : null);
				return true;
			case 7:
				Availability = ((value != null) ? ((ProductAvailability)value) : ProductAvailability.UNDEFINED);
				return true;
			case 36:
				RewardList = ((value != null) ? ((RewardListDataModel)value) : null);
				return true;
			case 38:
				DescriptionHeader = ((value != null) ? ((string)value) : null);
				return true;
			case 39:
				VariantName = ((value != null) ? ((string)value) : null);
				return true;
			case 40:
				FlavorText = ((value != null) ? ((string)value) : null);
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
			case 36:
				info = Properties[8];
				return true;
			case 38:
				info = Properties[9];
				return true;
			case 39:
				info = Properties[10];
				return true;
			case 40:
				info = Properties[11];
				return true;
			default:
				info = default(DataModelProperty);
				return false;
			}
		}
	}
}
